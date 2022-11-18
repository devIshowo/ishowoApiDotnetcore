using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.DbMethods;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.Business.Extra;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Data.SqlClient;
using System.IO;
using ItCommerce.DTO.Factory;
using Utils.IwajuTech.Business.Factories;
using API.Business.Extra;
using API.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using ZXing.Aztec.Internal;
using API.Controllers;

namespace ItCommerce.Business.Actions
{
    public class OfficeParams : IOffice
    {
        #region prestations
        /// <summary>
        /// liste services
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Prestation> getListPrestations()
        {
            List<Prestation> _listBsList = Prestation.CreateFromList(DtoParams.loadServicesList());
            return _listBsList;
        }//fin getListSuppliers

        public static List<Prestation> getPrestation(int id)
        {
            List<Prestation> _list = Prestation.CreateFromList(DtoParams.loadService(id));
            return _list;
        }//fin getPrestation

        public static Prestation createPrestation(Prestation item)
        {
            service result = DtoParams.createService(item.loadDto());
            return Prestation.Create(result);
        }//fin createPrestation
        public static Prestation updatePrestation(Prestation item)
        {
            service result = DtoParams.updateService(item.loadDto());
            return Prestation.Create(result);
        }//fin updatePrestation
        public static Prestation deletePrestation(int id)
        {
            service result = DtoParams.deleteService(id);
            return new Prestation();
        }//fin deletePrestation

        #endregion prestations

        #region customers
        /// <summary>
        /// liste clients
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Customer> getListCustomers()
        {
            List<Customer> _listBsList = Customer.CreateFromList(DtoParams.loadClientsList());
            return _listBsList;
        }//fin getListCustomers
        public static Customer createCustomer(Customer item)
        {
            client result = DtoParams.createCustomer(item.loadDto());
            return Customer.Create(result);
        }//fin creatCustomer
        public static Customer updateCustomer(Customer item)
        {
            client result = DtoParams.updateCustomer(item.loadDto());
            return Customer.Create(result);
        }//fin updatCustomer
        public static Customer deleteCustomer(int id)
        {
            client result = DtoParams.deleteCustomer(id);
            return new Customer();
        }//fin deletCustomer

        #endregion customers

        #region groupes
        /// <summary>
        /// list groupes
        public static Group createGroup(Group obj, List<Law> rolesList, int idProfil)
        {
            List<role> _dtoRolesList = new List<role>();
            foreach (Law item in rolesList)
            {
                _dtoRolesList.Add(item.loadDto());
            }
            groupe result = DtoParams.createGroup(obj.loadDto(), _dtoRolesList, idProfil);
            return Group.Create(result);
        }//fin createGroup

        public static Group updateGroup(Group obj, List<Law> rolesList, int idProfil)
        {
            List<role> _dtoRolesList = new List<role>();
            foreach (Law item in rolesList)
            {
                _dtoRolesList.Add(item.loadDto());
            }
            groupe result = DtoParams.updateGroup(obj.loadDto(), _dtoRolesList, idProfil);
            return Group.Create(result);
        }//fin createGroup

        //List groups with Roles
        public static List<GroupHandler> getListGroups()
        {
            List<GroupHandler> _Listing = new List<GroupHandler>();
            List<Group> _groupsList = Group.CreateFromList(DtoParams.loadGroupesList());

            foreach (Group item in _groupsList)
            {
                List<Law> _ListingLaw = new List<Law>();
                List<Group_Laws> _groupsLawList = Group_Laws.CreateFromList(DtoParams.loadGroupRolesList(item.id));
                foreach (Group_Laws groupe_Role in _groupsLawList)
                {
                    Law _CustomLaw = Law.Create(DtoParams.loadRolesListbyId(groupe_Role.id_law));
                    _CustomLaw.ischecked = groupe_Role.status;

                    _ListingLaw.Add(_CustomLaw);
                }
                GroupHandler _saveLine = new GroupHandler();
                _saveLine.group = item;
                _saveLine.laws = _ListingLaw;
                _Listing.Add(_saveLine);
            }
            return _Listing;
        }//fin getListGroupes with Roles

        //List groups without Roles
        public static List<Group> getListGroupsWithoutRoles()
        {
            List<Group> _listBsList = Group.CreateFromList(DtoParams.loadGroupesList());
            return _listBsList;
        }//fin getListGroupes without Roles

        //group Roles
        public static GroupHandler getGroupRolesRef(int idGroup)
        {
            Group _group = Group.Create(DtoParams.loadGroup(idGroup));

            List<Law> _ListingLaw = new List<Law>();
            List<Group_Laws> _groupsLawList = Group_Laws.CreateFromList(DtoParams.loadGroupRolesList(_group.id));
            foreach (Group_Laws groupe_Role in _groupsLawList)
            {
                Law _CustomLaw = Law.Create(DtoParams.loadRolesListbyId(groupe_Role.id_law));
                _CustomLaw.ischecked = groupe_Role.status;
                _ListingLaw.Add(_CustomLaw);
            }
            GroupHandler _saveLine = new GroupHandler();
            _saveLine.group = _group;
            _saveLine.laws = _ListingLaw;

            return _saveLine;
        }//fin getGroupRolesRef

        #endregion groupes

        #region roles
        public static List<Law> getListRoles()
        {
            List<Law> _listBsList = Law.CreateFromList(DtoParams.loadRolesList());
            return _listBsList;
        }//fin getListGroupes
        #endregion roles

        #region param-mecef
        public static List<ParamMecef> loadParamsMecef()
        {
            List<ParamMecef> _listBsList = ParamMecef.CreateFromList(DtoParams.loadParamMecefList());
            return _listBsList;
        }//fin getListGroupes


        public static UpdateMecef updateParamMecef(UpdateMecef item)
        {
            update_mecef result = DtoParams.updateParamMecef(item.loadDto());
            return UpdateMecef.Create(result);
        }//fin updatCustomer

        #endregion param-mecef

        #region groupe_roles
        public static List<Group_Laws> getListGroupRoles(int idGroup)
        {
            List<Group_Laws> _listBsList = Group_Laws.CreateFromList(DtoParams.loadGroupRolesList(idGroup));
            return _listBsList;
        }//fin getListGroupes
        #endregion groupe_roles

        #region agence
        /*zone agence*/
        /// <summary>
        /// liste agences
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Agency> getListAgencies(int id)
        {
            List<Agency> _listBsList = Agency.CreateFromList(DtoParams.loadAgencesList(id));
            return _listBsList;
        }//fin getListAgencies
        public static Agency createAgency(Agency item)
        {
            agence result = DtoParams.createAgence(item.loadDto(), item.agent.id);
            return Agency.Create(result);
        }//fin createCategory
        public static Agency updateAgency(Agency item)
        {
            agence result = DtoParams.updateAgence(item.loadDto());
            return Agency.Create(result);
        }//fin updateCategory
        public static Agency deleteAgency(int id)
        {
            agence result = DtoParams.deleteAgence(id);
            return new Agency();
        }//fin deleteAgency

        /*fin zone agence*/
        #endregion agence

        #region categorie
        /*zone categorie*/
        /// <summary>
        /// liste categories
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Category> getListCategories(int id)
        {
            List<Category> _listBsList = Category.CreateFromList(DtoParams.loadCategorieProduitList(id));
            return _listBsList;
        }//fin getListCategories

        public static Category createCategory(Category item)
        {
            categ_produit result = DtoParams.createCategory(item.loadDto(), item.agent.id);
            return Category.Create(result);
        }//fin createCategory
        public static Category updateCategory(Category item)
        {
            categ_produit result = DtoParams.updateCategory(item.loadDto());
            return Category.Create(result);
        }//fin updateCategory
        public static Category deleteCategory(int id)
        {
            categ_produit result = DtoParams.deleteCategory(id);
            return new Category();
        }//fin deleteCategory

        /*fin zone categorie*/
        #endregion categorie


        #region types mesure
        public static List<ProdMeasureType> getListProductsMeasureTypes(int id)
        {
            List<ProdMeasureType> _listBsList = ProdMeasureType.CreateFromList(DtoParams.loadProduitsTypesMesureList(id));
            return _listBsList;
        }//fin getListProductsMeasureTypes

        /*debut types mesure*/
        /// <summary>
        /// liste types mesure
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<MeasureType> getListMeasureTypes(int id)
        {
            List<MeasureType> _listBsList = MeasureType.CreateFromList(DtoParams.loadTypesMesureList(id));
            return _listBsList;
        }//fin getListMeasureTypes
        public static MeasureType createMeasureType(MeasureType item)
        {
            type_mesure result = DtoParams.createMeasureType(item.loadDto(), item.agent.id);
            return MeasureType.Create(result);
        }//fin createMeasureType
        public static MeasureType updateMeasureType(MeasureType item)
        {
            type_mesure result = DtoParams.updateMeasureType(item.loadDto());
            return MeasureType.Create(result);
        }//fin updateMeasureType
        public static MeasureType deleteMeasureType(int id)
        {
            type_mesure result = DtoParams.deleteMeasureType(id);
            return new MeasureType();
        }//fin deleteCategory

        /*fin types mesure*/
        #endregion types mesure

        #region Suppliers
        /// <summary>
        /// liste fournisseurs
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Supplier> getListSuppliers(int id)
        {
            List<Supplier> _listBsList = Supplier.CreateFromList(DtoParams.loadFournisseursList(id));
            return _listBsList;
        }//fin getListSuppliers
        public static Supplier createSupplier(Supplier item)
        {
            fournisseur result = DtoParams.createSupplier(item.loadDto(), item.agent.id);
            return Supplier.Create(result);
        }//fin createMeasureType
        public static Supplier updateSupplier(Supplier item)
        {
            fournisseur result = DtoParams.updateSupplier(item.loadDto());
            return Supplier.Create(result);
        }//fin updateMeasureType
        public static Supplier deleteSupplier(int id)
        {
            fournisseur result = DtoParams.deleteSupplier(id);
            return new Supplier();
        }//fin deleteCategory

        #endregion Suppliers


        #region compartments

        /// <summary>
        /// liste rayons
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Compartment> getListCompartments(int id)
        {
            List<Compartment> _listBsList = Compartment.CreateFromList(DtoParams.loadRayonsList(id));
            return _listBsList;
        }//fin getListCompartments
        public static Compartment createCompartment(Compartment item)
        {
            rayon result = DtoParams.createCompartment(item.loadDto(), item.agent.id);
            return Compartment.Create(result);
        }//fin createCompartment
        public static Compartment updateCompartment(Compartment item)
        {
            rayon result = DtoParams.updateCompartment(item.loadDto());
            return Compartment.Create(result);
        }//fin updateCompartment



        public static Compartment deleteCompartment(int id)
        {
            rayon result = DtoParams.deleteCompartment(id);
            return new Compartment();
        }//fin deleteCompartment


        #endregion compartments


        #region products

        /// <summary>
        /// liste produit
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Product> getListProducts(int id)
        {
            List<Product> _listBsList = Product.CreateFromList(DtoParams.loadProduitsList(id));
            return _listBsList;
        }//fin getListProduits

        /// <summary>
        /// liste produit
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Product> searchProducts(int id, string name, string code)
        {
            List<Product> _listBsList = Product.CreateFromList(DtoParams.searchProduitsList(id, name, code));
            return _listBsList;
        }//fin getListProduits

        public static List<ProductInStock> getListProductsFromStock(int id)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoParams.loadProduitsStockList(id));
            return _listBsList;
        }//fin getListProductsFromStock

        public static Product createProduct(Product item)
        {
            //create product
            produit productCreated = DtoParams.createProduct(item.loadDto(), item.agent.id);

            //get measure types
            if (productCreated == null) return null;
            List<produit_type_mesure> prodMeasures = productCreated.produit_type_mesure.ToList<produit_type_mesure>();

            return Product.Create(productCreated);
        }//fin createProduct

        public static Product updateProduct(Product item)
        {
            //update product
            produit productUpdated = DtoParams.updateProduct(item.loadDto());

            //get measure types
            List<produit_type_mesure> prodMeasures = new List<produit_type_mesure>();
            foreach (ProdMeasureType itemMT in item.measure_types)
            {
                produit_type_mesure pTM = itemMT.loadDto();
                pTM.id_produit = productUpdated.id;
                prodMeasures.Add(pTM);
            }
            //productUpdated.produit_type_mesure.ToList<produit_type_mesure>();

            //update measure types
            List<produit_type_mesure> prodMeasuresResult = DtoParams.updateMeasureTypes(prodMeasures);

            //extract associations
            foreach (MeasureAssociation itemMA in item.measure_associations)
            {
                if (itemMA.bulk.id == 0)
                {
                    produit_type_mesure parent = prodMeasuresResult.Find(x => x.id_type_mesure == itemMA.bulk.measure_type.id);
                    itemMA.bulk = ProdMeasureType.Create(parent);
                }
                if (itemMA.retail.id == 0)
                {
                    produit_type_mesure enfant = prodMeasuresResult.Find(x => x.id_type_mesure == itemMA.retail.measure_type.id);
                    itemMA.retail = ProdMeasureType.Create(enfant);
                }
            }
            List<produit_corresp_mesure> prodAssocList = item.loadAssociationDto();

            #region a
            //var indexCm = 0;
            //foreach (produit_corresp_mesure itemCM in prodAssocList)
            //{
            //    int idProdMeasEnfant = 0;
            //    if(itemCM.id_produit_mesure_enfant == 0)
            //    {
            //        idProdMeasEnfant = item.measure_associations.Find(x => x.retail.id == )
            //    }
            //    indexCm++;
            //}

            //update with associations
            //foreach (MeasureAssociation itemMA in item.measure_associations)
            //{
            //    produit_corresp_mesure prodCorrespondance = new produit_corresp_mesure();
            //    //measure type parent
            //    produit_type_mesure parent = prodMeasuresResult.Find(x => x.id_type_mesure == itemMA.bulk.measure_type.id);
            //    //prodCorrespondance.id_produit_mesure_parent = parent.id;

            //    //measure type enfant
            //    produit_type_mesure enfant = prodMeasuresResult.Find(x => x.id_type_mesure == itemMA.retail.measure_type.id);
            //    //prodCorrespondance.id_produit_mesure_enfant = enfant.id;

            //    //quantite
            //    //prodCorrespondance.quantite = itemMA.quantity;

            //    //find the dto index in orginal prodAssocList from l 250
            //    produit_corresp_mesure searchCM = prodAssocList.FindIndex(x => x.id_produit_mesure_parent == 

            //    prodAssocList.Add(prodCorrespondance);
            //}
            #endregion


            bool resultAssoc = DtoParams.updateAssociations(prodAssocList);

            return Product.Create(productUpdated);
        }//fin updateProduct

        public static Product deleteProduct(int id)
        {
            produit result = DtoParams.deleteProduct(id);
            return new Product();
        }//fin deleteProduct

        public static Product deleteProdMeasureType(ProdMeasureType obj)
        {
            produit_type_mesure result = DtoParams.deleteProductMeasureType(obj.loadDto());
            return new Product();
        }//fin deleteProdMeasureType

        public static Product deleteProdAssociation(MeasureAssociation obj)
        {
            produit result = DtoParams.deleteProductAssociation(obj.loadDto());
            return new Product();
        }//fin deleteProdAssociation

        #endregion endproducts

        #region objectif_vente
        /*zone objectif_vente*/

        /// <summary>
        /// liste objectif_vente
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<SaleTarget> getListSaleTarget(int id)
        {
            List<SaleTarget> _listBsList = SaleTarget.CreateFromList(DtoParams.loadObjectifVentesList(id));
            return _listBsList;
        }//fin getListSaleTarget

        public static List<SaleTarget> getListSaleTarget(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<SaleTarget> _listBsList = SaleTarget.CreateFromList(DtoParams.loadObjectifVentesList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListSaleTarget

        public static SaleTarget createSaleTarget(SaleTarget item)
        {
            item.start_date = item.start_date.AddHours(1); item.end_date = item.end_date.AddHours(1);
            item.start_date = new DateTime(item.start_date.Year, item.start_date.Month, item.start_date.Day, 0, 0, 0);
            objectif_vente result = DtoParams.createObjectifVente(item.loadDto(), item.agent.id);
            return SaleTarget.Create(result);
        }//fin createSaleTarget

        public static SaleTarget updateSaleTarget(SaleTarget item)
        {
            item.start_date = item.start_date.AddHours(1); item.end_date = item.end_date.AddHours(1);
            item.start_date = new DateTime(item.start_date.Year, item.start_date.Month, item.start_date.Day, 0, 0, 0);
            objectif_vente result = DtoParams.updateObjectifVente(item.loadDto());
            return SaleTarget.Create(result);
        }//fin updateSaleTarget

        public static SaleTarget deleteSaleTarget(int id)
        {
            objectif_vente result = DtoParams.deleteObjectifVente(id);
            return new SaleTarget();
        }//fin deleteSaleTarget

        /*fin zone objectif_vente*/
        #endregion objectif_vente

        #region cryptage Mot de Passe
        public static string cryptUserPassword(string password)
        {
            string Result = SecurityFactory.cryptAnyWord(password, LocalSecu.AskerSalt, LocalSecu.AskerParam);

            return Result;
        }
        #endregion cryptage Mot de Passe

        #region banques

        /// <summary>
        /// liste banques
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Bank> getListBanks(int id)
        {
            List<Bank> _listBsList = Bank.CreateFromList(DtoParams.loadBanquesList(id));
            return _listBsList;
        }//fin getListBanks

        public static Company getCompany(int idCompany)
        {
            Company _company = Company.Create(DtoParams.loadUserCompany(idCompany));
            return _company;
        }//fin getListBanks

        public static Bank createBank(Bank item)
        {
            banque result = DtoParams.createBanque(item.loadDto(), item.agent.id);
            return Bank.Create(result);
        }//fin createBank

        public static Bank updateBank(Bank item)
        {
            banque result = DtoParams.updateBanque(item.loadDto());
            return Bank.Create(result);
        }//fin updateBank

        public static Bank deleteBank(int id)
        {
            banque result = DtoParams.deleteBanque(id);
            return new Bank();
        }//fin deleteBank


        #endregion banques

        #region account types
        public static List<AccountType> getListAccountTypes(int id)
        {
            List<AccountType> _listBsList = AccountType.CreateFromList(DtoParams.loadTypeCompteList(id));
            return _listBsList;
        }//fin getListAccountTypes
        #endregion

        #region key concerns
        //generate unique keys
        public static string getUniqueKey()
        {
            int maxSize = 8;
            //int minSize = 5;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }//end getUniqueKey

        /// <summary>
        /// returns machien fingerprint
        /// </summary>
        /// <returns></returns>
        public static string getTestMachineKey()
        {
            return FingerPrint.Value();
        }

        //check if a specified servie is running
        public static bool isServiceRunning()
        {
            //service list
            //List<ServiceController> services = ServiceController.GetServices().ToList();

            ////search concerned
            //ServiceController myService = services.Find(x => x.ServiceName.ToLower().Equals("accenture"));
            //if (myService == null) { return false; }
            //else
            //{
            //    if (myService.Status == ServiceControllerStatus.Running) return true;
            //    else return false;
            //}
            return true;
        }//fin isServiceRunning

        #endregion

        /// <summary>
        /// backup the database
        /// </summary>
        public static bool backUpDB(string bckFolder)
        {
            return true;
            //if (!Directory.Exists(bckFolder)) Directory.CreateDirectory(bckFolder);

            ////get actual connection string password
            //string appConnTring = LocalFileObject.ReadContent(Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PaPath));
            //string[] tabConParts = appConnTring.Split(';');
            //string password = "";
            //foreach (string itemCon in tabConParts)
            //{
            //    if (itemCon.Trim().StartsWith("Password"))
            //    {
            //        string[] passParts = itemCon.Split('=');
            //        password = passParts[1];
            //    }
            //}
            //if (password.Trim().Equals(string.Empty)) password = "sa";

            ////create connexion string
            //string cn = string.Format("Data source={0};Persist Security Info=True;Initial Catalog={1};User ID={2};Password={3};Connection Timeout={4}", LocalSecu.DbInstanceName, "IT_COMMERCE", "sa", password, 1000);
            //string fileName = string.Format("BACKUP_{0}_{1}.bak", "IT_COMMERCE", DateTime.Now.Ticks);


            //using (SqlConnection con = new SqlConnection(cn))
            //{
            //    try
            //    {
            //        //ouverture connexion
            //        con.Open();
            //        string database = "IT_COMMERCE";
            //        string path = Path.Combine(bckFolder, fileName);
            //        string query = "Backup database " + database + " to disk='" + path + "'";
            //        SqlCommand cmd = new SqlCommand(query, con);
            //        cmd.ExecuteNonQuery();
            //    }
            //catch (SqlException ex)
            //{
            //    throw ex;
            //}
            //catch (System.IO.IOException ex)
            //{
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //}
            //}

            //    return true;
        }//fin backUpDB

        #region logs
        /// <summary>
        /// last logs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<LogItem> getListLogs(int id)
        {
            List<LogItem> _listBsList = LogItem.CreateFromList(DtoParams.loadLogList(id));
            return _listBsList;
        }//fin getListLogs

        public static List<LogItem> getListLogs(PeriodParam obj)
        {
            int idAgent = 0; if (obj.agent == null) { idAgent = 0; } else { idAgent = obj.agent.id; }
            List<LogItem> _listBsList = LogItem.CreateFromList(DtoParams.loadLogList(obj.startDate, obj.endDate, idAgent));
            return _listBsList;
        }//fin getListLogs


        public static List<string> getListErrors()
        {
            List<string> _listBsList = new List<string>();
            string month = (DateTime.Now.Month.ToString().Length < 2) ? ("0" + DateTime.Now.Month) : DateTime.Now.Month.ToString();
            string day = (DateTime.Now.Day.ToString().Length < 2) ? ("0" + DateTime.Now.Day) : DateTime.Now.Day.ToString();

            string filePath = string.Format("Logs/ISHOWO{0}{1}{2}.txt", DateTime.Now.Year, month, day);
            string[] content = File.ReadAllLines(filePath);
            foreach (string item in content)
            {
                _listBsList.Add(item);
            }

            return _listBsList;
        }//fin getListLogs

        #endregion

        public static string getTestValue()
        {
            return IOffice.ConnString;
        }

        #region licences related

            /// <summary>
        ///get list modules
        /// <returns></returns>
        public static List<ModuleRequest> GetModuleList()
        {
            List<ModuleRequest> _listModules = new List<ModuleRequest>();
            _listModules.Add(new ModuleRequest(){ id = 1, nom = "MODULE_DEMO", description = "Utilisez l'application pendant 30 jours gratuitement pour essayer les fonctionnalitées.", montant = 0, duree = 30 });
            _listModules.Add(new ModuleRequest(){ id = 2, nom = "MODULE STANDARD", description = "Obtenez la licence de l'application avec tous les modules et bénéficiez en premier des récentes mises à jour.", montant = 70000, duree = 365 });
            //_listModules.Add(new ModuleRequest(){ id = 2, nom = "MODULE 1", description = "Effectuez un abonnement annuel de 60 000F avec 0 F de frais d'installation et de formation la première année.", montant = 45000, duree = 365 });
            //_listModules.Add(new ModuleRequest(){ id = 3, nom = "MODULE 2", description = "Effectuez un abonnement annuel de 84 000F avec 0 F de frais d'installation et de formation la première année.", montant = 55000, duree = 365 });
            return _listModules;
        }//fin GetModuleList

        /// <summary>
        ///get list formules
        /// <returns></returns>
        public static List<FormuleRequest> GetFormuleList()
        {
            List<FormuleRequest> _listFormules = new List<FormuleRequest>();
            //_listFormules.Add(new FormuleRequest(){ id=2, module_id="2", duree =90, montant =15000, description ="module trimestrielle", type ="trimestrielle" });
            ////_listFormules.Add(new FormuleRequest(){ id =5, module_id="3", duree=90, montant =20000, description="module trimestrielle", type="trimestrielle" });
            //_listFormules.Add(new FormuleRequest(){ id=3, module_id="2", duree=180, montant=25000, description="module semestrielle", type="semestrielle" });
            _listFormules.Add(new FormuleRequest(){ id=8, module_id="2", duree=90, montant=25000, description="module trimestrielle", type="trimestrielle" });
            ////_listFormules.Add(new FormuleRequest(){ id=6, module_id="3", duree=180, montant=35000, description="module semestrielle", type="semestrielle" });
            //_listFormules.Add(new FormuleRequest(){ id=1, module_id="2", duree=365, montant=45000, description="module annuel", type="annuelle" });
            _listFormules.Add(new FormuleRequest(){ id=9, module_id="2", duree=180, montant=45000, description="module semestrielle", type="semestrielle" });
            //_listFormules.Add(new FormuleRequest(){ id=4, module_id="3", duree=365, montant=55000, description="module annuel", type="annuelle" });
            _listFormules.Add(new FormuleRequest(){ id=7, module_id="2", duree=365, montant=70000, description="module annuel", type="annuelle" });
            return _listFormules;
        }//fin GetFormuleList


        #endregion


      
    }
}
    