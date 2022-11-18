using System;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.IO;
using ItCommerce.Business.Entities;
using System.Globalization;
using ItCommerce.Reporting.Factory;

namespace ItCommerce.Reporting.Reports
{
    /// <summary>
    /// Creates the invoice form.
    /// </summary>
    public class SalePOSForm
    {

        //specifil variables
        private ProductSale sale;
        private int mainFontSize = 9;

        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document document;

        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        readonly XmlDocument invoice;

        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>
        readonly XPathNavigator navigator;

        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table table;
        string printPath = ""; string resourcePath = ""; Company company;
        //logo
        private string logoPath = "";
        private Section _section;

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public SalePOSForm()
        {
            this.invoice = new XmlDocument();
            this.invoice.LoadXml(ParamsRepository.reportXml);
            this.navigator = this.invoice.CreateNavigator();
        }

        public SalePOSForm(ProductSale _sale, string _printPath, string _resourcePath, Company _company)
        {
            this.invoice = new XmlDocument();
            this.invoice.LoadXml(ParamsRepository.reportXml);
            this.navigator = this.invoice.CreateNavigator();
            this.sale = _sale;
            printPath = _printPath;
            company = _company;
            logoPath = Path.Combine(_resourcePath, company.getLogo());
        }

        /// <summary>
        /// generate a pdf file
        /// </summary>
        /// <param name="printPath"></param>
        /// <returns></returns>
        public string generatePdf()
        {
            // Create a MigraDoc document
            Document document = this.CreateDocument();
            document.UseCmykColor = true;

            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

            // Set the MigraDoc document
            pdfRenderer.Document = document;

            // Create the PDF document
            pdfRenderer.RenderDocument();

            // Save the PDF document...
            string filename = "";

            // ...
            filename = "Vente-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";

            //final path
            string finalPath = Path.Combine(printPath, filename);

            pdfRenderer.Save(finalPath);

            return filename;
        }


        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        private Document CreateDocument()
        {
            // Create a new MigraDoc document
            this.document = new Document();
            this.document.Info.Title = "Vente";
            this.document.Info.Subject = "Votre vente";
            this.document.Info.Author = company.name;

            DefineStyles();

            CreatePage();

            FillContent();

            return this.document;
        }


        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        private void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("1mm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("1mm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = mainFontSize;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "2mm";
            style.ParagraphFormat.SpaceAfter = "2mm";
            style.ParagraphFormat.TabStops.AddTabStop("5cm", TabAlignment.Right);
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        private void CreatePage()
        {
            PageSetup pageSetup = this.document.DefaultPageSetup;
            pageSetup.LeftMargin = Unit.FromMillimeter(2); pageSetup.RightMargin = Unit.FromMillimeter(2);
            pageSetup.PageWidth = Unit.FromMillimeter(70);
            pageSetup.PageHeight = Unit.FromMillimeter(110);
            pageSetup.TopMargin = Unit.FromMillimeter(3);
            pageSetup.BottomMargin = Unit.FromMillimeter(3);

            // Each MigraDoc document needs at least one section.
            _section = this.document.AddSection();
            _section.PageSetup = pageSetup.Clone();


            //header company name
            string headerTextCompanyName = company.name;
            Paragraph headerCompanyName = _section.Headers.Primary.AddParagraph();
            headerCompanyName.AddText(headerTextCompanyName);
            headerCompanyName.Format.Font.Size = mainFontSize;
            headerCompanyName.Format.Alignment = ParagraphAlignment.Left;

            //header company services
            string headerTextCompanyServices = company.description;
            Paragraph headerCompanyServices = _section.Headers.Primary.AddParagraph();
            headerCompanyServices.AddText(headerTextCompanyServices);
            headerCompanyServices.Format.Font.Name = "Times New Roman";
            headerCompanyServices.Format.Font.Size = mainFontSize-2;
            headerCompanyServices.Format.SpaceAfter = 3;
            headerCompanyServices.Format.Alignment = ParagraphAlignment.Left;

            //header company address
            string headerTextCompanyAddress = string.Format("Tel: {0} - {1}", company.phone, company.address);
            Paragraph headerCompanyAddress = _section.Headers.Primary.AddParagraph();
            headerCompanyAddress.AddText(headerTextCompanyAddress);
            headerCompanyAddress.Format.Font.Name = "Times New Roman";
            headerCompanyAddress.Format.Font.Size = mainFontSize-2;
            headerCompanyAddress.Format.SpaceAfter = 3;
            headerCompanyAddress.Format.Alignment = ParagraphAlignment.Left;


            //add invoice number
            Paragraph paragraph = _section.Headers.Primary.AddParagraph();
            //invoice number
            paragraph.AddText("Ticket N°: " + sale.id.ToString().PadLeft(8, '0') + " du ");
            paragraph.AddDateField("dd/MM/yyyy HH:mm");
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;

            // Add 
            //paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            //paragraph.Format.SpaceBefore = "5mm";
            //paragraph.Format.Alignment = ParagraphAlignment.Left;
            //paragraph.Style = "Reference";

            if (sale.customer != null )
            {
                paragraph = _section.Headers.Primary.AddParagraph(); // _section.AddParagraph();
                //paragraph.Format.SpaceBefore = "10mm";
                //paragraph.Format.SpaceAfter = "2mm";
                paragraph.Format.Font.Size = mainFontSize-2;
                paragraph.Format.Font.Color = new Color(204, 9, 9);
                string customerDetails = string.Format("{0} {1} - Tel: {2}", sale.customer.nom, sale.customer.prenom,
                sale.customer.telephone);
                paragraph.AddFormattedText("Client: ", TextFormat.NotBold);
                paragraph.AddFormattedText(string.Format("{0}", customerDetails), TextFormat.Italic);
            }

            //vendeur
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            //paragraph.Format.SpaceBefore = "14mm";
            //paragraph.Format.SpaceAfter = "2mm";
            string seller = (sale.agent == null || sale.agent == null) ? "" : sale.agent.user.firstname;
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Font.Color = new Color(204, 9, 9);
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("Vendeur: " + seller);

            // Add 
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.SpaceBefore = "3mm";
            paragraph.Format.SpaceAfter = "0mm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Borders.Bottom.Visible = true;
            string titleReportText = string.Format("Nombre de produits: {0}", (sale.lines!= null)? sale.lines.Count: 0);
            paragraph.AddText(titleReportText);

            // Create footer
            Paragraph paragraphFooter = _section.Footers.Primary.AddParagraph();
            //msg
            paragraphFooter.Format.Font.Size = mainFontSize-2;
            paragraphFooter.Format.Font.Color = new Color(204, 9, 9);
            paragraphFooter.Format.Alignment = ParagraphAlignment.Center;
            paragraphFooter.AddFormattedText("MERCI DE VOTRE VISITE");

        }

        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContent()
        {
            string numberFormat = "# ### ###";
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            // Fill address in address text frame
            Paragraph paragraph = null; // _section.Headers.Primary.AddParagraph(); //this._section.AddParagraph();

            // Iterate the invoice items
            double totalExtendedPrice = 0;

            if (sale.lines != null)
            {
                #region order lines
                for (int i = 0; i < sale.lines.Count; i++)
                {
                    ProductLine productLine = sale.lines[i];

                    //each product
                
                    //add a line for each product
                    paragraph = _section.Headers.Primary.AddParagraph();  //_section.AddParagraph();
                    paragraph.Format.Font.Size = mainFontSize-2;
                    //paragraph.Format.SpaceBefore = "3mm";
                    //paragraph.Format.SpaceAfter = "1mm";
                    paragraph.Format.Alignment = ParagraphAlignment.Center;

                    paragraph.AddText((i + 1).ToString());
                    paragraph.AddSpace(2);
                    string productName = productLine.product.product.name;
                    if (productName.Length > 30) productName = productName.Substring(0, 27) + "...";
                    paragraph.AddText(productName);
                    int countRemaingProdSpaces = 30 - productName.Length;
                    paragraph.AddSpace(countRemaingProdSpaces);
                    paragraph.AddText(productLine.p_vente.ToString(numberFormat, formatProvider));
                    paragraph.AddSpace(5);
                    paragraph.AddText(productLine.quantite.ToString());
                    paragraph.AddSpace(5);
                    paragraph.AddText((productLine.quantite * productLine.p_vente).ToString(numberFormat, formatProvider));

                    double extendedPrice = (productLine.quantite!= 0)? 
                        (productLine.quantite * productLine.p_vente) :
                        (productLine.quantite * productLine.p_vente)     ;//calc
                    totalExtendedPrice += extendedPrice;

                }
                #endregion sales
            }
            //line for separation
            paragraph.Format.Borders.Bottom.Visible = true;
            paragraph = _section.Headers.Primary.AddParagraph();

            //total sale
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            //paragraph.Format.SpaceBefore = "3mm";
            //paragraph.Format.SpaceAfter = "1mm";
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("TOTAL");
            paragraph.AddSpace(29);
            paragraph.AddText(": ");
            paragraph.AddSpace(6- (totalExtendedPrice).ToString(numberFormat, formatProvider).Length);
            paragraph.AddText((totalExtendedPrice).ToString(numberFormat, formatProvider));

            //remise
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("REMISE");
            paragraph.AddSpace(27);
            paragraph.AddText(": ");
            paragraph.AddSpace(6 - (0).ToString(numberFormat, formatProvider).Length);
            paragraph.AddText((0).ToString(numberFormat, formatProvider));

            //net a payer
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("NET A PAYER");
            paragraph.AddSpace(19);
            paragraph.AddText(": ");
            paragraph.AddSpace(6 - (totalExtendedPrice).ToString(numberFormat, formatProvider).Length);
            paragraph.AddText((totalExtendedPrice).ToString(numberFormat, formatProvider));

            //mode de reglement
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("MODE DE REGLEMENT");
            paragraph.AddSpace(6);
            paragraph.AddText(": ESPECES");

            //montant espece donnne
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("MONTANT ESPECE DONNE");
            paragraph.AddSpace(20 - "MONTANT ESPECE DONNE".Length);
            paragraph.AddText(": ");
            paragraph.AddSpace(6 - (sale.amount_paid).ToString(numberFormat, formatProvider).Length);
            paragraph.AddText((sale.amount_paid).ToString(numberFormat, formatProvider));

            //net a payer
            paragraph = _section.Headers.Primary.AddParagraph(); //_section.AddParagraph();
            paragraph.Format.Font.Size = mainFontSize-2;
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText("MONNAIE RENDUE");
            paragraph.AddSpace(11);
            paragraph.AddText(": ");
            paragraph.AddSpace(6 - (sale.amount_paid - totalExtendedPrice).ToString(numberFormat, formatProvider).Length);
            paragraph.AddText((sale.amount_paid - totalExtendedPrice).ToString(numberFormat, formatProvider));

        }

        /// <summary>
        /// Selects a subtree in the XML data.
        /// </summary>

        // Some pre-defined colors
#if true
        // RGB colors
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
                // CMYK colors
                readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
                readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
                readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
    }

}
