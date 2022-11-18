using ItCommerce.Api.Net.Extra;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using XAct.Diagnostics.Services.Implementations;

namespace ItCommerce.DTO.DbMethods
{
    public class DtoMecef : IDto
    {
        public static string ReplaceSpecialChar(string lib)
        {
            string libelle = lib.Replace("\r", "^xa;").Replace("\n", "^xd;").Replace(",", "^x2c;").Replace("<", "^lt;").Replace(">", "^gt;").Replace("&", "^amp;");
            return libelle;
        }

        #region normalisation
        public static facture normalizeSale(int id)
        {
            IT_COMMERCEEntities db = ItCommerceFactory.GetEntity();
            var sale = db.vente_produit.Find(id);
            param_mecef Num_Port = db.param_mecef.Where(x => x.code == "PORT").FirstOrDefault();
            param_mecef T_AIB = db.param_mecef.Where(x => x.code == "AIB").FirstOrDefault();
            SerialPort ComPort = new SerialPort();
            SPHandler spHandler = new SPHandler();
            HttpRequestMessage Request = new HttpRequestMessage();

            ComPort.PortName = "COM" + Num_Port.valeur;

            ComPort.BaudRate = 115200;      //convert Text to Integer
            ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None"); //convert Text to Parity
            ComPort.DataBits = 8;        //convert Text to Integer
            ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");  //convert Text to stop bits

            spHandler.SetPort(ComPort.PortName, ComPort.BaudRate, 1000);
            spHandler.Open();

            //  Normalisation
            try
            {
                int seq = 21;
                seq++;

                // Command C1h
                var cmd_c1 = Trame.Command("", seq++, "c1", "");
                string c1h_response = spHandler.ExecuteCommand(cmd_c1);

                if (c1h_response != "\u0015")
                {
                    //Récuperer les données
                    string val = c1h_response.Split('?')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.NIM = val2[0];
                    GlobalVars.IFU = val2[1];
                    GlobalVars.DT = val2[2];
                    GlobalVars.TC = val2[3];
                    GlobalVars.FVC = val2[4];
                    GlobalVars.FRC = val2[5];
                    GlobalVars.TAXA = val2[6];
                    GlobalVars.TAXB = val2[7];
                    GlobalVars.TAXC = val2[8];
                    GlobalVars.TAXD = val2[9];
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation !");
                }

                var user = db.profils.Find(sale.id_profil);

                //Command COh
                string data1 = $"{sale.id_profil},{user.user.nom},{GlobalVars.IFU},{GlobalVars.TAXA},{GlobalVars.TAXB},{GlobalVars.TAXC},{GlobalVars.TAXD},{"FV"}";

                if ((sale.id_client != null))
                {
                    var customer = db.clients.Find(sale.id_client);
                    if (!string.IsNullOrEmpty(customer.raison_sociale) && !string.IsNullOrEmpty(customer.ifu))
                    {
                        data1 += $",{customer.ifu},{customer.raison_sociale}";

                    }
                    else if (!string.IsNullOrEmpty(customer.nom))
                    {
                        data1 += $",{customer.nom},{customer.prenom}";
                    }
                }

                if (sale.t_aib != 0)
                {
                    data1 += $",AIB{sale.t_aib}";
                }

                byte[] ba1 = Encoding.Default.GetBytes(data1);
                var hex1 = BitConverter.ToString(ba1);
                var hexString1 = hex1.Replace("-", "");

                string trame_c0 = Trame.Command(hex1, seq++, "c0", hexString1);
                string c0h_response = spHandler.ExecuteCommand(trame_c0);

                if (c0h_response != "\u0015")
                {
                    //Récuperer les données
                    string val = c0h_response.Split('?')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.FC = val2[0];
                    GlobalVars.TC = val2[1];
                }
                else
                {

                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation !");
                }

                // Parcourir chaque ligne de facture
                List<vente_produit_details> liste = db.vente_produit_details.Where(x => x.id_vente == sale.id).ToList<vente_produit_details>();

                foreach (var item in liste)
                {
                    //Command 31h
                    string tab = Encoding.ASCII.GetString(SPHandler.StringToByteArray("09"));
                    string data2 = ""; // Eau{tab}A300

                    double Prix = item.p_vente + item.mt_tva;
                    double PrixOrigin = Prix;

                    if (item.mt_remise != 0)
                    {
                        Prix -= item.mt_remise;
                    }

                    // double PrixOrigin = item.PUHT + item.TVA;
                    string PrixDescrip = "";

                    if (item.mt_remise != 0)
                    {
                        PrixDescrip = $"Remise de {item.mt_remise}FCFA";
                    }

                    if (item.quantite == 1)
                    {
                        data2 = $"{item.produit_type_mesure.produit.nom}{tab}{item.tax}{Prix}";
                    }
                    else
                    {
                        data2 = $"{item.produit_type_mesure.produit.nom}{tab}{item.tax}{Prix}*{item.quantite.ToString().Replace(',', '.')}";
                    }

                    data2 = ReplaceSpecialChar(data2);

                    if (!string.IsNullOrEmpty(item.libellets) && item.ts != null)
                    {
                        data2 += $";{item.ts},{item.libellets}";
                    }

                    if (Prix != PrixOrigin)
                    {
                        data2 += $"{tab}{PrixOrigin},{PrixDescrip}";
                    }

                    byte[] ba2 = Encoding.Default.GetBytes(data2);
                    var hex2 = BitConverter.ToString(ba2);
                    var hexString2 = hex2.Replace("-", "");

                    string trame_31 = Trame.Command(hex2, seq++, "31", hexString2);
                    string trh_response = spHandler.ExecuteCommand(trame_31);
                }

                //Command 33h
                var cmd_33 = Trame.Command("", seq++, "33", "");
                string total_response = spHandler.ExecuteCommand(cmd_33);

                if (total_response != "\u0015")
                {
                    //Récuperer les données
                    string val = total_response.Split('\u0001')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    //Affecter aux variables globales
                    GlobalVars.MV = val2[0].Substring(3, val2[0].Length - 3);
                }
                else
                {

                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation !");
                }

                //Command 35h
                var cmd_35 = Trame.Command("", seq++, "35", "");
                string tth_response = spHandler.ExecuteCommand(cmd_35);

                //Command 38h
                var cmd_38 = Trame.Command("", seq++, "38", "");
                string t8h_response = spHandler.ExecuteCommand(cmd_38);

                if (t8h_response != "\u0015")
                {

                    //Récuperer les données
                    string val = t8h_response.Split('\u0001')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    //Affecter aux variables globales
                    GlobalVars.FC_ = val2[0];
                    GlobalVars.TC_ = val2[1];
                    GlobalVars.FT_ = val2[2];
                    GlobalVars.DT_ = val2[3];

                    if (val2[6] != null)
                    {
                        GlobalVars.SIG_ = val2[6];
                    }
                    else
                    {
                        GlobalVars.SIG_ = "";
                    }
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation !");
                }

                // QR Code: { F};{ NIM};{ SIG};{ IFU};{ DT}
                string code = $"F;{GlobalVars.NIM};{GlobalVars.SIG_};{GlobalVars.IFU};{GlobalVars.DT_}";

                facture facture_Mecef = new facture()
                {
                    id_vente = id,
                    QRCode_Mecef = code,
                    NIM_Mecef = GlobalVars.NIM,
                    Code_Mecef = GlobalVars.SIG_,
                    Compteur_Total_Mecef = GlobalVars.TC,
                    Compteur_Type_Facture_Mecef = GlobalVars.FC,
                    Date_Mecef = GlobalVars.DT_,
                    API = false,
                    IFU = GlobalVars.IFU
                };

                db.factures.Add(facture_Mecef);
                db.SaveChanges();

                return facture_Mecef;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Echec de la normalisation ! Machine non connectée");
            }

        }
        #endregion normalisation

        #region normalisation e-Mecef
        public static async Task<facture> normalizeSaleOnline(int id)
        {
            IT_COMMERCEEntities db = ItCommerceFactory.GetEntity();
            var sale = db.vente_produit.Find(id);
            param_mecef Num_Port = db.param_mecef.Where(x => x.code == "PORT").FirstOrDefault();
            param_mecef IFU = db.param_mecef.Where(x => x.code == "IFU").FirstOrDefault();
            param_mecef TOKEN = db.param_mecef.Where(x => x.code == "TOKEN").FirstOrDefault();
            param_mecef NIM = db.param_mecef.Where(x => x.code == "NIM").FirstOrDefault();

            //MessageBox.Show("Veuillez connecter votre machine de normalisation de facture avant d'éditer la facture", "Erreur d'édition", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //this.Close();
            StatusResponse statusResponse = new StatusResponse();
            InvoiceRequestData invoiceRequestData = null;
            List<Item> items = new List<Item>();

            // Requete pour avoir le statut de l'API
            statusResponse = await eMCFServices.GetStatutAsync(TOKEN.valeur);

            try
            {
                //Init total
                double total = 0;

                //infos vendeur
                var Seller = db.profils.Find(sale.id_profil);
                string idVendeur = Seller.id.ToString();
                //string nomVendeur = Seller.user.nom + Seller.user.prenoms;
                string nomVendeur = Seller.user.prenoms;

                //infos client
                string customer_IFU = null;
                string customer_NAME = null;

                if (sale.id_client != null)
                {
                    var customer = db.clients.Find(sale.id_client);
                    if (!string.IsNullOrEmpty(customer.raison_sociale) && !string.IsNullOrEmpty(customer.ifu))
                    {
                        customer_IFU = customer.ifu;
                        customer_NAME = customer.raison_sociale;

                    }
                    else if (!string.IsNullOrEmpty(customer.nom))
                    {
                        customer_IFU = null;
                        customer_NAME = customer.nom + " " + customer.prenom;
                    }
                }

                //infos facture
                string refFacture = sale.reference;
                string typeFacture = "FV";
                string Objet = "";
                string paiement = "ESPECES";

                // Control AIB   
                string aib = null;
                if (sale.t_aib > 0)
                {
                    if (sale.t_aib == 1)
                    {
                        aib = "A";
                    }
                    else if (sale.t_aib == 5)
                    {
                        aib = "B";
                    }
                }

                paiement = "ESPECES";
                try
                {
                    if (invoiceRequestData == null)
                    {
                        invoiceRequestData = new InvoiceRequestData
                        {
                            ifu = IFU.valeur,
                            aib = string.IsNullOrEmpty(aib) ? null : aib,
                            type = typeFacture,
                            @operator = new Operator
                            {
                                id = idVendeur,
                                name = nomVendeur
                            },
                            client = new Client
                            {
                                ifu = customer_IFU,
                                name = customer_NAME
                            },
                            reference = refFacture,
                        };

                        // Parcourir chaque ligne de facture
                        List<vente_produit_details> liste = db.vente_produit_details.Where(x => x.id_vente == sale.id).ToList<vente_produit_details>();

                        foreach (var item in liste)
                        {
                            //initialisatin des données
                            string nomProduit = item.produit_type_mesure.produit.nom;
                            //quantité
                            string qte = item.quantite.ToString();
                            float _qte = float.Parse(qte);

                            string prixUnitaire = item.p_vente.ToString();
                            string prixRemise = item.mt_remise.ToString();
                            string taxeSpecifique = (item.ts == null || item.ts.ToString().StartsWith("0")) ? null : item.ts.ToString();
                            string nomTaxeSpecifique = string.IsNullOrEmpty(item.libellets) ? null : item.libellets;
                            string tauxImposition = item.tax;

                            //prix hors taxe
                            double pricettc = double.Parse(prixUnitaire);
                            int puttc = Convert.ToInt32(pricettc);

                            //prix taxe spécifique
                            double pricets = 0;
                            if (!string.IsNullOrEmpty(taxeSpecifique))
                            {
                                pricets = double.Parse(taxeSpecifique);
                            }

                            //Application du taux d'imposition
                            if (tauxImposition == "B" || tauxImposition == "D")
                            {
                                puttc += puttc * 18 / 100;
                            }

                            //Application remise
                            int? remise = null;
                            if (prixRemise.Trim() != "" && !prixRemise.StartsWith(".") && !prixRemise.StartsWith("0"))
                            {
                                //string rem = prixRemise.Replace(".", ",");
                                float _rem = float.Parse(prixRemise);
                                remise = puttc - Convert.ToInt32(_rem);
                            }

                            items.Add(new Item
                            {
                                code = null,
                                name = nomProduit,
                                price = puttc,
                                quantity = _qte,
                                originalPrice = remise,
                                priceModification = remise != null ? "Remise de " + prixRemise : null,
                                taxSpecific = Convert.ToInt32(pricets),
                                taxGroup = tauxImposition
                            });

                            double ttc = puttc * _qte;
                            total += ttc;
                            if (sale.t_aib > 0)
                            {
                                float _taib = (float)sale.t_aib;

                                total += (ttc / 1.18) * Convert.ToInt32(_taib) / 100;
                            }
                            if (!string.IsNullOrEmpty(taxeSpecifique) && !taxeSpecifique.StartsWith("."))
                            {
                                total += double.Parse(taxeSpecifique);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation de la facture !");
                }

                if (total != 0)
                {
                    invoiceRequestData.payment = new Payment[] {
                                new Payment {
                                    amount = Convert.ToInt32(total),
                                    name = paiement
                                }
                            };
                }
                invoiceRequestData.items = items.ToArray();

                // Enr. la facture en attente
                InvoiceResponseData invoiceResponseData = await eMCFServices.PostInvoice(TOKEN.valeur, invoiceRequestData);

                // Normalisation
                SecurityElements securityElements = await eMCFServices.ConfirmInvoice(TOKEN.valeur, invoiceResponseData.uid, "confirm");

                // Genere le code mecef à enregistrer
                string codeMecef = securityElements.codeMECeFDGI.Replace("-", "");

                // Genere la date mecef à enregistrer
                string date = securityElements.dateTime;
                string ladate = date.Split(' ')[0];
                string heure = date.Split(' ')[1];

                string[] elemen = ladate.Split('/');
                string annee = elemen[2];
                string mois = elemen[1];
                string jour = elemen[0];

                string[] elemen1 = heure.Split(':');
                string hr = elemen1[0];
                string min = elemen1[1];
                string snd = elemen1[2];

                string dateTime = $"{annee}{mois}{jour}{hr}{min}{snd}";

                // Génere les n° et type de facture
                string compteurs = securityElements.counters;
                string fc_tc = compteurs.Split(' ')[0];
                string typeFacture1 = compteurs.Split(' ')[1];

                string[] count = fc_tc.Split('/');
                string fc = count[0];
                string tc = count[1];

                facture facture_Mecef = new facture()
                {
                    id_vente = id,
                    QRCode_Mecef = securityElements.qrCode,
                    NIM_Mecef = securityElements.nim,
                    Code_Mecef = codeMecef,
                    Compteur_Total_Mecef = tc,
                    Compteur_Type_Facture_Mecef = fc,
                    Date_Mecef = dateTime,
                    API = true,
                    IFU = IFU.valeur
                };

                db.factures.Add(facture_Mecef);
                db.SaveChanges();
                return facture_Mecef;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                    throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation de la facture !");
            }
        }
        #endregion normalisation e-Mecef

        #region choix méthode d'annulation
        public static facture_avoir choiceCancelMethod(int id)
        {
            IT_COMMERCEEntities db = ItCommerceFactory.GetEntity();
            param_mecef searchParam = db.param_mecef.Where(x => x.code == "EMECEF").FirstOrDefault();
            param_mecef searchParamTwo = db.param_mecef.Where(x => x.code == "MECEF").FirstOrDefault();
            facture_avoir annulation = new facture_avoir();

            if (searchParam.valeur == "true")
            {
                //load normalisation
                annulation = cancelSaleOnline(id).Result;
            }
            else if (searchParamTwo.valeur == "true")
            {
                //load normalisation
                annulation = cancelSale(id);
            }
            return annulation;
        }
        #endregion choix méthode d'annulation 


        #region facture d'avoir e-Mecef
        public static async Task<facture_avoir> cancelSaleOnline(int id)
        {
            IT_COMMERCEEntities db = ItCommerceFactory.GetEntity();
            var sale = db.vente_produit.Find(id);
            param_mecef Num_Port = db.param_mecef.Where(x => x.code == "PORT").FirstOrDefault();
            param_mecef T_AIB = db.param_mecef.Where(x => x.code == "AIB").FirstOrDefault();
            param_mecef IFU = db.param_mecef.Where(x => x.code == "IFU").FirstOrDefault();
            param_mecef TOKEN = db.param_mecef.Where(x => x.code == "TOKEN").FirstOrDefault();
            SerialPort ComPort = new SerialPort();
            SPHandler spHandler = new SPHandler();

            facture facture = db.factures.Where(x => x.id_vente == id).FirstOrDefault();

            if (facture == null)
            {
                throw new InvalidOperationException("Echec de l'annulation! Cette facture n'avait pas été normalisée.");
            }
            string NIM = facture.NIM_Mecef;
            string TC = facture.Compteur_Total_Mecef;


            facture_avoir AlreadyCanceled = db.factures_avoir.Where(x => x.id_vente == id).FirstOrDefault();
            if (AlreadyCanceled != null)
            {
                throw new InvalidOperationException("Cette facture a déjà été annulée!");
            }

            StatusResponse statusResponse = new StatusResponse();

            InvoiceRequestData invoiceRequestData = null;
            List<Item> items = new List<Item>();

            // Requete pour avoir le statut de l'API
            statusResponse = await eMCFServices.GetStatutAsync(TOKEN.valeur);
            try
            {
                double total = 0;
                //infos vendeur
                var Seller = db.profils.Find(sale.id_profil);
                string idVendeur = Seller.id.ToString();
                string nomVendeur = Seller.user.nom + Seller.user.prenoms;

                //infos client
                string customer_IFU = null;
                string customer_NAME = null;

                if (sale.id_client != null)
                {
                    var customer = db.clients.Find(sale.id_client);
                    if (!string.IsNullOrEmpty(customer.raison_sociale) && !string.IsNullOrEmpty(customer.ifu))
                    {
                        customer_IFU = customer.ifu;
                        customer_NAME = customer.raison_sociale;

                    }
                    else if (!string.IsNullOrEmpty(customer.nom))
                    {
                        customer_IFU = null;
                        customer_NAME = customer.nom + " " + customer.prenom;
                    }
                }

                //infos facture
                string refFacture = sale.reference;
                string typeFacture = "FA";
                string Objet = "";
                string paiement = "ESPECES";
                string refFactureOriginale = facture.Code_Mecef;

                // Control AIB   
                string aib = null;
                if (sale.t_aib > 0)
                {
                    if (sale.t_aib == 1)
                    {
                        aib = "A";
                    }
                    else if (sale.t_aib == 5)
                    {
                        aib = "B";
                    }
                }

                // Parcourir chaque ligne de facture
                List<vente_produit_details> liste = db.vente_produit_details.Where(x => x.id_vente == sale.id).ToList<vente_produit_details>();

                try
                {
                    if (invoiceRequestData == null)
                    {
                        invoiceRequestData = new InvoiceRequestData
                        {
                            ifu = IFU.valeur,
                            aib = string.IsNullOrEmpty(aib) ? null : aib,
                            type = typeFacture,
                            @operator = new Operator
                            {
                                id = idVendeur,
                                name = nomVendeur
                            },
                            client = new Client
                            {
                                ifu = customer_IFU,
                                name = customer_NAME
                            },
                            reference = typeFacture == "FV" ? null : refFactureOriginale
                        };

                        foreach (var item in liste)
                        {

                            //initialisatin des données
                            string nomProduit = item.produit_type_mesure.produit.nom;
                            string qte = item.quantite.ToString();
                            float _qte = float.Parse(qte);
                            string prixUnitaire = item.p_vente.ToString();
                            string prixRemise = item.mt_remise.ToString();
                            string taxeSpecifique = (item.ts == null || item.ts.ToString().StartsWith("0")) ? null : item.ts.ToString(); ;
                            string nomTaxeSpecifique = string.IsNullOrEmpty(item.libellets) ? null : item.libellets;
                            string tauxImposition = item.tax;

                            //prix hors taxe
                            double pricettc = double.Parse(prixUnitaire);
                            int puttc = Convert.ToInt32(pricettc);

                            //prix taxe spécifique
                            double pricets = 0;
                            if (!string.IsNullOrEmpty(taxeSpecifique))
                            {
                                pricets = double.Parse(taxeSpecifique);
                            }

                            //Application du taux d'imposition
                            if (tauxImposition == "B" || tauxImposition == "D")
                            {
                                puttc += puttc * 18 / 100;
                            }

                            //Application remise
                            int? remise = null;
                            if (prixRemise.Trim() != "" && !prixRemise.StartsWith(".") && !prixRemise.StartsWith("0"))
                            {
                                //string rem = prixRemise.Replace(".", ",");
                                float _rem = float.Parse(prixRemise);
                                remise = puttc - Convert.ToInt32(_rem);
                            }

                            items.Add(new Item
                            {
                                code = null,
                                name = nomProduit,
                                price = puttc,
                                quantity = _qte,
                                originalPrice = remise,
                                priceModification = remise != null ? "Remise de " + prixRemise : null,
                                taxSpecific = Convert.ToInt32(pricets),
                                taxGroup = tauxImposition
                            });

                            if (!string.IsNullOrEmpty(taxeSpecifique) && !taxeSpecifique.StartsWith("."))
                            {
                                total += double.Parse(taxeSpecifique);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
                }


                if (total != 0)
                {
                    invoiceRequestData.payment = new Payment[] {
                                new Payment {
                                    amount = Convert.ToInt32(total),
                                    name = paiement
                                }
                            };
                }

                invoiceRequestData.items = items.ToArray();

                // Enr. la facture en attente
                InvoiceResponseData invoiceResponseData = await eMCFServices.PostInvoice(TOKEN.valeur, invoiceRequestData);

                // Normalisation
                SecurityElements securityElements = await eMCFServices.ConfirmInvoice(TOKEN.valeur, invoiceResponseData.uid, "confirm");

                // Genere le code mecef à enregistrer
                string codeMecef = securityElements.codeMECeFDGI.Replace("-", "");

                // Genere la date mecef à enregistrer
                string date = securityElements.dateTime;
                string ladate = date.Split(' ')[0];
                string heure = date.Split(' ')[1];

                string[] elemen = ladate.Split('/');
                string annee = elemen[2];
                string mois = elemen[1];
                string jour = elemen[0];

                string[] elemen1 = heure.Split(':');
                string hr = elemen1[0];
                string min = elemen1[1];
                string snd = elemen1[2];

                string dateTime = $"{annee}{mois}{jour}{hr}{min}{snd}";

                // Génere les n° et type de facture
                string compteurs = securityElements.counters;
                string fc_tc = compteurs.Split(' ')[0];
                string typeFacture1 = compteurs.Split(' ')[1];

                string[] count = fc_tc.Split('/');
                string fc = count[0];
                string tc = count[1];

                facture_avoir facture_Mecef_Avoir = new facture_avoir()
                {
                    id_vente = id,
                    id_facture = facture.id,
                    QRCode_Mecef_Avoir = securityElements.qrCode,
                    Code_Mecef_Avoir = codeMecef,
                    Compteur_Total_Mecef_Avoir = tc,
                    Compteur_Type_Facture_Mecef_Avoir = fc,
                    Date_Mecef_Avoir = dateTime,
                    IFU = IFU.valeur,
                    NIM_Avoir = NIM,
                };

                sale.avec_facture = false;
                db.factures_avoir.Add(facture_Mecef_Avoir);
                db.SaveChanges();

                return facture_Mecef_Avoir;
            }
            catch (Exception ex)
            {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
            }
        }

        #endregion facture d'avoir e-Mecef

        #region facture d'avoir
        public static facture_avoir cancelSale(int id)
        {
            IT_COMMERCEEntities db = ItCommerceFactory.GetEntity();
            var sale = db.vente_produit.Find(id);
            param_mecef Num_Port = db.param_mecef.Where(x => x.code == "PORT").FirstOrDefault();
            param_mecef T_AIB = db.param_mecef.Where(x => x.code == "AIB").FirstOrDefault();
            SerialPort ComPort = new SerialPort();
            SPHandler spHandler = new SPHandler();

            facture facture = db.factures.Where(x => x.id_vente == id).FirstOrDefault();

            if (facture == null)
            {
                throw new InvalidOperationException("Echec de l'annulation! Cette facture n'avait pas été normalisée.");
            }
            string NIM = facture.NIM_Mecef;
            string TC = facture.Compteur_Total_Mecef;


            facture_avoir AlreadyCanceled = db.factures_avoir.Where(x => x.id_vente == id).FirstOrDefault();
            if (AlreadyCanceled != null)
            {
                throw new InvalidOperationException("Cette facture a déjà été annulée!");
            }

            ComPort.PortName = "COM" + Num_Port.valeur;

            //ComPort.PortName = "COM4";
            ComPort.BaudRate = 115200;      //convert Text to Integer
            ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None"); //convert Text to Parity
            ComPort.DataBits = 8;        //convert Text to Integer
            ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");  //convert Text to stop bits

            spHandler.SetPort(ComPort.PortName, ComPort.BaudRate, 1000);
            spHandler.Open();

            // Normalisation
            try
            {
                int seq = 21;
                seq++;

                // Command C1h
                var cmd_c1 = Trame.Command("", seq++, "c1", "");
                string c1h_response = spHandler.ExecuteCommand(cmd_c1);

                if (c1h_response != "\u0015")
                {
                    // Récuperer les données
                    string val = c1h_response.Split('?')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.NIM = val2[0];
                    GlobalVars.IFU = val2[1];
                    GlobalVars.DT = val2[2];
                    GlobalVars.TC = val2[3];
                    GlobalVars.FVC = val2[4];
                    GlobalVars.FRC = val2[5];
                    GlobalVars.TAXA = val2[6];
                    GlobalVars.TAXB = val2[7];
                    GlobalVars.TAXC = val2[8];
                    GlobalVars.TAXD = val2[9];
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
                }

                var user = db.profils.Find(sale.id_profil);
                // Command COh
                string data1 = $"{sale.id_profil},{user.user.nom},{GlobalVars.IFU},{GlobalVars.TAXA},{GlobalVars.TAXB},{GlobalVars.TAXC},{GlobalVars.TAXD},FA,{NIM}-{TC}";

                if ((sale.id_client != null))
                {
                    var customer = db.clients.Find(sale.id_client);
                    if (!string.IsNullOrEmpty(customer.raison_sociale) && !string.IsNullOrEmpty(customer.ifu))
                    {
                        data1 += $",{customer.ifu},{customer.raison_sociale}";

                    }
                    else if (!string.IsNullOrEmpty(customer.nom))
                    {
                        data1 += $",{customer.nom},{customer.prenom}";
                    }
                }

                if (sale.t_aib != 0)
                {
                    data1 += $",AIB{sale.t_aib}";
                }

                byte[] ba1 = Encoding.Default.GetBytes(data1);
                var hex1 = BitConverter.ToString(ba1);
                var hexString1 = hex1.Replace("-", "");

                string trame_c0 = Trame.Command(hex1, seq++, "c0", hexString1);
                string c0h_response = spHandler.ExecuteCommand(trame_c0);

                if (c0h_response != "\u0015")
                {
                    // Récuperer les données
                    string val = c0h_response.Split('?')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.FC = val2[0];
                    GlobalVars.TC = val2[1];
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
                }

                // Parcourir chaque ligne de facture
                List<vente_produit_details> liste = db.vente_produit_details.Where(x => x.id_vente == sale.id).ToList<vente_produit_details>();
                foreach (var item in liste)
                {
                    // Command 31h
                    string tab = Encoding.ASCII.GetString(SPHandler.StringToByteArray("09"));
                    string data2 = ""; // Eau{tab}A300

                    double Prix = item.p_vente + item.mt_tva;
                    double PrixOrigin = Prix;

                    if (item.mt_remise != 0)
                    {
                        Prix -= item.mt_remise;
                    }

                    //double PrixOrigin = item.PUHT + item.TVA;
                    string PrixDescrip = "";

                    if (item.mt_remise != 0)
                    {
                        PrixDescrip = $"Remise de {item.mt_remise}FCFA";
                    }

                    if (item.quantite == 1)
                    {
                        data2 = $"{item.produit_type_mesure.produit.nom}{tab}{item.tax}{Prix}";
                    }
                    else
                    {
                        data2 = $"{item.produit_type_mesure.produit.nom}{tab}{item.tax}{Prix}*{item.quantite.ToString().Replace(',', '.')}";
                    }

                    data2 = ReplaceSpecialChar(data2);

                    if (!string.IsNullOrEmpty(item.libellets) && item.ts != null)
                    {
                        data2 += $";{item.ts},{item.libellets}";
                    }

                    if (Prix != PrixOrigin)
                    {
                        data2 += $"{tab}{PrixOrigin},{PrixDescrip}";
                    }

                    byte[] ba2 = Encoding.Default.GetBytes(data2);
                    var hex2 = BitConverter.ToString(ba2);
                    var hexString2 = hex2.Replace("-", "");

                    string trame_31 = Trame.Command(hex2, seq++, "31", hexString2);
                    string trh_response = spHandler.ExecuteCommand(trame_31);
                }

                // Command 33h
                var cmd_33 = Trame.Command("", seq++, "33", "");
                string total_response = spHandler.ExecuteCommand(cmd_33);

                if (total_response != "\u0015")
                {
                    // Récuperer les données
                    string val = total_response.Split('\u0001')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.MV = val2[0].Substring(3, val2[0].Length - 3);
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
                }

                // Command 35h
                var cmd_35 = Trame.Command("", seq++, "35", "");
                string tth_response = spHandler.ExecuteCommand(cmd_35);

                // Command 38h
                var cmd_38 = Trame.Command("", seq++, "38", "");
                string t8h_response = spHandler.ExecuteCommand(cmd_38);

                if (t8h_response != "\u0015")
                {
                    // Récuperer les données
                    //string val = t8h_response.Split('&')[1];
                    string val = t8h_response.Split('\u0001')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.FC_ = val2[0];
                    GlobalVars.TC_ = val2[1];
                    GlobalVars.FT_ = val2[2];
                    GlobalVars.DT_ = val2[3];

                    if (val2[6] != null)
                    {
                        GlobalVars.SIG_ = val2[6];
                    }
                    else
                    {
                        GlobalVars.SIG_ = "";
                    }
                }
                else
                {
                    throw new InvalidOperationException("Une erreur est survenue au cours de l'annulation de la facture !");
                }

                // QR Code: {F};{NIM};{SIG};{IFU};{DT}
                string code = $"F;{GlobalVars.NIM};{GlobalVars.SIG_};{GlobalVars.IFU};{GlobalVars.DT_}";

                facture_avoir facture_Mecef_Avoir = new facture_avoir()
                {
                    id_vente = id,
                    id_facture = facture.id,
                    QRCode_Mecef_Avoir = code,
                    Code_Mecef_Avoir = GlobalVars.SIG_,
                    Compteur_Total_Mecef_Avoir = GlobalVars.TC,
                    Compteur_Type_Facture_Mecef_Avoir = GlobalVars.FC,
                    Date_Mecef_Avoir = GlobalVars.DT_,
                    IFU = GlobalVars.IFU,
                    NIM_Avoir = NIM,
                };

                sale.avec_facture = false;
                db.factures_avoir.Add(facture_Mecef_Avoir);
                db.SaveChanges();

                return facture_Mecef_Avoir;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Echec de la normalisation ! Machine non connectée.");
            }
        }

    }
    #endregion facture d'avoir

}
