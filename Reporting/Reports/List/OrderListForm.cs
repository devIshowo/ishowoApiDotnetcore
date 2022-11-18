using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.IO;
using ItCommerce.Business.Entities;
using System.Globalization;
using ItCommerce.Reporting.Factory;

namespace ItCommerce.Reporting.Reports
{
    public class OrderListForm
    {
        //specific variables
        private List<Order> dataList = new List<Order>();

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

        //dates
        private DateTime startDate; private DateTime endDate;

        //logo
        private string logoPath =  "";

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public OrderListForm()
        {
            this.invoice = new XmlDocument();
            this.invoice.LoadXml(ParamsRepository.reportXml);
            this.navigator = this.invoice.CreateNavigator();
        }

        public OrderListForm(List<Order> _dataList, DateTime _startDate, DateTime _endDate, string _printPath, string _resourcePath, Company _company)
        {
            this.invoice = new XmlDocument();
            this.invoice.LoadXml(ParamsRepository.reportXml);
            this.navigator = this.invoice.CreateNavigator();
            this.dataList = _dataList;
            startDate = _startDate; endDate = _endDate;
            printPath = _printPath;
            company = _company;
            logoPath = Path.Combine(_resourcePath, company.getLogo());
        }
        

        public void setLogo(string _logoPath)
        {
            logoPath = _logoPath;
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

            // 
            filename = "ListeCommandes-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";

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
            this.document.Info.Title = "Liste Commandes";
            this.document.Info.Subject = "Liste Commandes";
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
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        private void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();

            //set defaut size
            Unit width, height; PageSetup.GetPageSize(PageFormat.A4, out width, out height);
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = Unit.FromMillimeter(3); section.PageSetup.RightMargin = Unit.FromMillimeter(3);
            section.PageSetup.PageWidth = width; section.PageSetup.PageHeight = height;
            section.PageSetup.TopMargin = Unit.FromMillimeter(5); section.PageSetup.BottomMargin = Unit.FromMillimeter(5);

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage(logoPath);
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            //header company name
            string headerTextCompanyName = company.name; 
            Paragraph headerCompanyName = section.Headers.Primary.AddParagraph();
            headerCompanyName.AddText(headerTextCompanyName);
            headerCompanyName.Format.Font.Size = 9;
            headerCompanyName.Format.Alignment = ParagraphAlignment.Left;

            //header company services
            string headerTextCompanyServices = company.description;
            Paragraph headerCompanyServices = section.Headers.Primary.AddParagraph();
            headerCompanyServices.AddText(headerTextCompanyServices);
            headerCompanyServices.Format.Font.Name = "Times New Roman";
            headerCompanyServices.Format.Font.Size = 7;
            headerCompanyServices.Format.SpaceAfter = 3;
            headerCompanyServices.Format.Alignment = ParagraphAlignment.Left;

            //header company address
            string headerTextCompanyAddress = string.Format("{0}/{1}", company.address, company.phone);
            Paragraph headerCompanyAddress = section.Headers.Primary.AddParagraph();
            headerCompanyAddress.AddText(headerTextCompanyAddress);
            headerCompanyAddress.Format.Font.Name = "Times New Roman";
            headerCompanyAddress.Format.Font.Size = 7;
            headerCompanyAddress.Format.SpaceAfter = 3;
            headerCompanyAddress.Format.Alignment = ParagraphAlignment.Left;


            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Imprimé, le ");
            paragraph.AddDateField("dd/MM/yyyy HH:mm");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "5.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "2cm";
            paragraph.Format.SpaceAfter = "0.5cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Borders.Bottom.Visible = true;
            //paragraph.AddTab();
            string titleReportText = string.Format("Liste des ventes du {0} au {1}", startDate.ToShortDateString(), endDate.ToShortDateString());
            paragraph.AddText(titleReportText);

            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = this.table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;

            row.Cells[0].AddParagraph("#");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            
            row.Cells[1].AddParagraph("Date");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;

            //row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[2].AddParagraph("Fournisseur");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].AddParagraph("Montant");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;

            row.Cells[4].AddParagraph("Agent");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;

            this.table.SetEdge(0, 0, 5, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContent()
        {
            string numberFormat = "# ### ###";
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            // Fill address in address text frame
            Paragraph paragraph = this.addressFrame.AddParagraph();

            // Iterate the invoice items
            double totalValue = 0;

            if (dataList != null)
            {
                #region sale lines
                for (int i = 0; i < dataList.Count; i++)
                {
                    Order orderLine = dataList[i];

                    // Each item fills two rows
                    Row row1 = this.table.AddRow();
                    row1.TopPadding = 1.5;
                    row1.Cells[0].Shading.Color = TableGray;
                    row1.Cells[3].Shading.Color = TableGray;

                    row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row1.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                    row1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                    row1.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;
                    row1.Cells[4].Format.Alignment = ParagraphAlignment.Left;

                    row1.Cells[0].AddParagraph((i + 1).ToString());

                    //
                    row1.Cells[1].AddParagraph(orderLine.date.ToString("dd/MM/yyyy HH:mm"));

                    string supplierName = (orderLine.supplier != null) ? (orderLine.supplier.name ) : "";
                    row1.Cells[2].AddParagraph(supplierName);

                    row1.Cells[3].AddParagraph(orderLine.amount.ToString(numberFormat, formatProvider) + " FCFA");
                    string sellerName = (orderLine.agent != null) ? (orderLine.agent.user.firstname + " " + orderLine.agent.user.lastname) : "";

                    row1.Cells[4].AddParagraph(sellerName);

                    totalValue += orderLine.amount;

                    this.table.SetEdge(0, this.table.Rows.Count - 2, 5, 1, Edge.Box, BorderStyle.Single, 0.75);
                }
                #endregion sales
            }


            // Add an invisible row as a space line to the table
            Row row = this.table.AddRow();
            row.Borders.Visible = false;

            // Add the total amount of operation cost
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Montant total");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 3;
            row.Cells[4].AddParagraph(totalValue.ToString(numberFormat, formatProvider) + " FCFA");

            //// Add the number
            //row = this.table.AddRow();
            //row.Cells[0].AddParagraph("Reliquat");
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 3;
            //totalValue += 0; // .18 * totalExtendedPrice;
            //row.Cells[4].AddParagraph(dataList.Count.ToString());

            // Set the borders of the specified cell range
            this.table.SetEdge(4, this.table.Rows.Count - 3, 1, 3, Edge.Box, BorderStyle.Single, 0.75);

            //this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

            // Add the notes paragraph
            //paragraph = this.document.LastSection.AddParagraph();
            //paragraph.Format.SpaceBefore = "1cm";
            //paragraph.Format.Borders.Width = 0.75;
            //paragraph.Format.Borders.Distance = 3;
            //paragraph.Format.Borders.Color = TableBorder;
            //paragraph.Format.Shading.Color = TableGray;
            ////item = SelectItem("/invoice");
            //paragraph.AddText("my notes");
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
