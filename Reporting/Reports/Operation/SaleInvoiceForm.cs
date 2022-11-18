﻿using ItCommerce.Business.Entities;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ItCommerce.Reporting.Reports
{
    public class SaleInvoiceForm
    {
        //variables
        PdfDocument document;
        PageSize pageSize;
        PdfPage page;
        XGraphics gfx;
        double pixelTranslate = 0; double partWidth = 0; double partHeight = 0;
        //string filePath = string.Empty;

        //public data
        string printPath = ""; string resourcePath = ""; Company company;
        private ProductSale sale;
        //logo
        private string logoPath = "";

        public SaleInvoiceForm(ProductSale _sale, string _printPath, string _resourcePath, Company _company)
        {
            // Create a new PDF document
            document = new PdfDocument();

            // Create a font
            XFont font = new XFont("Times", 25, XFontStyle.Bold);
            pageSize = PageSize.A4;
            page = document.AddPage();
            page.Size = pageSize; page.Orientation = PageOrientation.Landscape;
            gfx = XGraphics.FromPdfPage(page);

            //param for duplication
            pixelTranslate = page.Width/2;
            partWidth = page.Width / 2;
            partHeight = page.Height;

            //sale
            sale = _sale;
            printPath = _printPath;
            company = _company;
            logoPath = Path.Combine(_resourcePath, company.getLogo());
        }//end

        /// <summary>
        /// generate the invoice for a salelogo
        /// </summary>
        public string generatePdf()
        {        
            //part 1 of invoice
            drawInvoicePart(0, gfx, partWidth, partHeight);

            //part 2 of invoice
            drawInvoicePart(pixelTranslate, gfx, partWidth, partHeight);

            // Save the document...
            string filename = "Facture-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
            string fileGenerated = Path.Combine(printPath, filename);
            document.Save(fileGenerated);

            return filename;
        }

        /// <summary>
        /// draw a part of the invoice
        /// </summary>
        /// <param name="startingPoint"></param>
        private void drawInvoicePart(double startingPoint, XGraphics gfx, double partWidth, double partHeight)
        {
            //left padding
            double leftMargin = 15; double topMargin = 10;

            #region general parts
            //draw a line to separate the two part
            XPen pen = new XPen(XColors.LightGray, 2);
            pen.DashStyle = XDashStyle.Dash;
            gfx.DrawLine(pen, partWidth - 10, 0, partWidth - 10, partHeight);

            //company name
            XFont companyNameFont = new XFont("Times", 12, XFontStyle.Bold);
            string companyNameText = company.name;
            gfx.DrawString(companyNameText, companyNameFont, XBrushes.DarkBlue, new XRect(leftMargin + startingPoint, topMargin, 10, 10), XStringFormats.TopLeft);

            //company activity
            XFont companyActivityFont = new XFont("Times", 11, XFontStyle.Regular);
            string companyActivityText = company.description;
            gfx.DrawString(companyActivityText, companyActivityFont, XBrushes.Black, new XRect(leftMargin + startingPoint, topMargin + 15 , 10, 10), XStringFormats.TopLeft);

            //company address
            XFont companyAddressFont = new XFont("Times", 11, XFontStyle.Regular);
            string companyAddressText = string.Format("{0}/{1}", company.address, company.phone);
            gfx.DrawString(companyAddressText, companyAddressFont, XBrushes.Black, new XRect(leftMargin + startingPoint, topMargin + 30, 10, 10), XStringFormats.TopLeft);

            //company logo
            XImage logoImage = XImage.FromFile(logoPath);
            gfx.DrawImage(logoImage, startingPoint + partWidth - 130, topMargin, 100, 100);

            //invoice number
            XFont invoiceNumberFont = new XFont("Times", 13, XFontStyle.Regular);
            string invoiceLibText = string.Format("FACTURE N° ");
            string invoiceNumberText = string.Format("{0}", sale.id.ToString().PadLeft(8, '0'));
            //lib
            gfx.DrawString(invoiceLibText, invoiceNumberFont, XBrushes.SteelBlue, new XRect(leftMargin + startingPoint, topMargin + 130, 70, 10), XStringFormats.TopLeft);
            //value
            invoiceNumberFont = new XFont("Times", 13, XFontStyle.Bold);
            gfx.DrawString(invoiceNumberText, invoiceNumberFont, XBrushes.SteelBlue, new XRect(leftMargin + startingPoint + 85, topMargin + 130, 10, 10), XStringFormats.TopLeft);
            
            //invoice date
            XFont invoiceDateFont = new XFont("Times", 11, XFontStyle.Italic);
            string invoiceDateText = string.Format("{0}, le {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            gfx.DrawString(invoiceDateText, invoiceDateFont, XBrushes.Black, new XRect(startingPoint + partWidth - 100, topMargin + 130, 10, 10), XStringFormats.Center);

            //customer name
            if (sale.customer != null) //check if customer
            {
                XFont invoiceCustomerFont = new XFont("Times", 13, XFontStyle.Regular);
                string invoiceCustomerLabelText = string.Format("Client: ");
                string invoiceCustomerValueText = string.Format("{0} {1} (Tel: {2})", sale.customer.nom, sale.customer.prenom,
                sale.customer.telephone);
                //lib
                gfx.DrawString(invoiceCustomerLabelText, invoiceCustomerFont, XBrushes.DarkGoldenrod, new XRect(leftMargin + startingPoint, topMargin + 145, 30, 10), XStringFormats.TopLeft);
                //value
                invoiceCustomerFont = new XFont("Times", 13, XFontStyle.Bold);
                gfx.DrawString(invoiceCustomerValueText, invoiceCustomerFont, XBrushes.DarkGoldenrod, new XRect(leftMargin + startingPoint + 40, topMargin + 145, 10, 10), XStringFormats.TopLeft);

            }

            #endregion

            #region summary labels and values
            string numberFormat = "# ### ###";
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            //sale summary labels
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Right;
            XFont summaryLabelsFont = new XFont("Times", 13, XFontStyle.Bold);
            tf.DrawString(" Montant reçu", summaryLabelsFont, XBrushes.Black, new XRect(startingPoint + partWidth - 210, topMargin + 170, 100, 10), XStringFormats.TopLeft);
            tf.DrawString("Montant vente", summaryLabelsFont, XBrushes.Black, new XRect(startingPoint + partWidth - 210, topMargin + 170 + 20, 100, 10), XStringFormats.TopLeft);
            tf.DrawString("     Reliquat", summaryLabelsFont, XBrushes.Black, new XRect(startingPoint + partWidth - 210, topMargin + 170 + 40, 100, 10), XStringFormats.TopLeft);

            //sale summary values
            tf.Alignment = XParagraphAlignment.Right;
            XSolidBrush amountValuesBgBrush = XBrushes.DarkBlue;
            XSolidBrush amountValuesForeColBrush = XBrushes.White;
            XFont summaryValuesFont = new XFont("Times", 13, XFontStyle.Regular);
            double valuesRectWidth = 70; double valuesStartingX = startingPoint + partWidth - 100; double valuesStartingY = topMargin + 170;
            double valuesHeight = 15;

            string montantRecu = string.Format(" {0} ", sale.amount_paid.ToString(numberFormat, formatProvider) + "");
            XRect values1Rect = new XRect(valuesStartingX, valuesStartingY, valuesRectWidth, valuesHeight);
            gfx.DrawRectangle(amountValuesBgBrush, values1Rect);
            tf.DrawString(montantRecu, summaryValuesFont, amountValuesForeColBrush, values1Rect, XStringFormats.TopLeft);

            int totalVente = sale.lines.Sum(x => x.quantite * x.p_vente);
            string montantVente = string.Format(" {0} ", totalVente.ToString(numberFormat, formatProvider) + "");
            XRect values2Rect = new XRect(valuesStartingX, valuesStartingY + 20, valuesRectWidth, valuesHeight);
            gfx.DrawRectangle(amountValuesBgBrush, values2Rect);
            tf.DrawString(montantVente, summaryValuesFont, amountValuesForeColBrush, values2Rect, XStringFormats.TopLeft);

            string reliquat = (sale.amount_paid - totalVente).ToString(numberFormat, formatProvider) + "";
            XRect values3Rect = new XRect(valuesStartingX, valuesStartingY + 40, valuesRectWidth, valuesHeight);
            gfx.DrawRectangle(amountValuesBgBrush, values3Rect);
            tf.DrawString(reliquat, summaryValuesFont, amountValuesForeColBrush, values3Rect, XStringFormats.TopLeft);

            #endregion
            //display the table for products sold
            drawTableLines(startingPoint, gfx, partWidth, partHeight, leftMargin, topMargin);

            //thankToCustomer: footer
            XFont thankToCustomerFont = new XFont("Times", 11, XFontStyle.Italic);
            string thankToCustomerText = string.Format("{0}", company.name + " vous remercie pour votre confiance");
            gfx.DrawString(thankToCustomerText, thankToCustomerFont, XBrushes.Black, new XRect(leftMargin + startingPoint, partHeight - topMargin - 10, partWidth, 10), XStringFormats.Center);
            
        }//fin drawInvoicePart

        //draw  table
        private void drawTableLines(double startingPoint, XGraphics gfx, double partWidth, double partHeight, double leftMargin, double topMargin)
        {
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Right;

            string numberFormat = "# ### ###";
            IFormatProvider formatProvider = CultureInfo.InvariantCulture;

            #region product header
            //display the table for products sold
            XPen penTable = new XPen(XColors.Black, 1);
            penTable.DashStyle = XDashStyle.Solid;
            XFont prodLinesHeaderFont = new XFont("Times", 10, XFontStyle.Bold);
            XFont prodLinesValuesFont = new XFont("Times", 10, XFontStyle.Regular);

            //add headers
            XSolidBrush headerBgBrush = XBrushes.Black;
            XSolidBrush headerForeColBrush = XBrushes.White;
            //column 1
            string column1Label = "#";
            XRect column1Rect = new XRect(leftMargin + startingPoint, topMargin + 240, 20, 15);
            gfx.DrawRectangle(headerBgBrush, column1Rect);
            gfx.DrawString(column1Label, prodLinesHeaderFont, headerForeColBrush, column1Rect, XStringFormats.Center);
           
            //column 2
            string column2Label = "Désignation";
            XRect column2Rect = new XRect(leftMargin + startingPoint + 20, topMargin + 240, 180, 15);
            gfx.DrawRectangle(headerBgBrush, column2Rect);
            gfx.DrawString(column2Label, prodLinesHeaderFont, headerForeColBrush, column2Rect, XStringFormats.Center);
            //column 3
            string column3Label = "Quantité";
            XRect column3Rect = new XRect(leftMargin + startingPoint + 200, topMargin + 240, 50, 15);
            gfx.DrawRectangle(headerBgBrush, column3Rect);
            gfx.DrawString(column3Label, prodLinesHeaderFont, headerForeColBrush, column3Rect, XStringFormats.Center);
            //column 4
            string column4Label = "Prix unitaire";
            XRect column4Rect = new XRect(leftMargin + startingPoint + 250, topMargin + 240, 60, 15);
            gfx.DrawRectangle(headerBgBrush, column4Rect);
            gfx.DrawString(column4Label, prodLinesHeaderFont, headerForeColBrush, column4Rect, XStringFormats.Center);
            //column 5
            string column5Label = "Prix total";
            XRect column5Rect = new XRect(leftMargin + startingPoint + 310, topMargin + 240, 70, 15);
            gfx.DrawRectangle(headerBgBrush, column5Rect);
            gfx.DrawString(column5Label, prodLinesHeaderFont, headerForeColBrush, column5Rect, XStringFormats.Center);

            #endregion

            #region product lines
            //add values
            double unitHeight = 15; int currentIndex = 0;
            if (sale.lines != null)
            {
                for (int i = 0; i < sale.lines.Count; i++)
                {
                    ProductLine productLine = sale.lines[i];

                    double currentLevelHeight = topMargin + 240 + unitHeight * (currentIndex + 1);

                    //column 1
                    tf.Alignment = XParagraphAlignment.Left;
                    column1Label = string.Format(" {0}", (currentIndex + 1).ToString());
                    column1Rect = new XRect(leftMargin + startingPoint, currentLevelHeight, 20, 15);
                    gfx.DrawRectangle(penTable, column1Rect);
                    tf.DrawString(column1Label, prodLinesValuesFont, XBrushes.Black, column1Rect, XStringFormats.TopLeft);

                    //column 2
                    tf.Alignment = XParagraphAlignment.Left;
                    column2Label = string.Format(" {0} ", productLine.product.product.name);
                    column2Rect = new XRect(leftMargin + startingPoint + 20, currentLevelHeight, 180, 15);
                    gfx.DrawRectangle(penTable, column2Rect);
                    tf.DrawString(column2Label, prodLinesValuesFont, XBrushes.Black, column2Rect, XStringFormats.TopLeft);

                    //column 3
                    tf.Alignment = XParagraphAlignment.Center;
                    column3Label = productLine.quantite.ToString();
                    column3Rect = new XRect(leftMargin + startingPoint + 200, currentLevelHeight, 50, 15);
                    gfx.DrawRectangle(penTable, column3Rect);
                    tf.DrawString(column3Label, prodLinesValuesFont, XBrushes.Black, column3Rect, XStringFormats.TopLeft);

                    //column 4
                    tf.Alignment = XParagraphAlignment.Right;
                    column4Label = string.Format(" {0} ", productLine.p_vente.ToString(numberFormat, formatProvider) + " FCFA");
                    column4Rect = new XRect(leftMargin + startingPoint + 250, currentLevelHeight, 60, 15);
                    gfx.DrawRectangle(penTable, column4Rect);
                    tf.DrawString(column4Label, prodLinesValuesFont, XBrushes.Black, column4Rect, XStringFormats.TopLeft);

                    //column 5
                    tf.Alignment = XParagraphAlignment.Right;
                    double extendedPrice = productLine.quantite * productLine.p_vente;
                    column5Label = string.Format(" {0} ", extendedPrice.ToString(numberFormat, formatProvider) + " FCFA");
                    column5Rect = new XRect(leftMargin + startingPoint + 310, currentLevelHeight, 70, 15);
                    gfx.DrawRectangle(penTable, column5Rect);
                    tf.DrawString(column5Label, prodLinesValuesFont, XBrushes.Black, column5Rect, XStringFormats.TopLeft);

                    //index
                    currentIndex++;
                }
            }
            #endregion


        }//fin drawTableLines


    }
}
