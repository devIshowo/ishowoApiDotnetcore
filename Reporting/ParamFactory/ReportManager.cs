using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using DinkToPdf;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using Serilog;
using Wkhtmltopdf.NetCore;
using ItCommerce.Reporting.ViewModels;

namespace ItCommerce.Reporting.Factory
{

    public class ReportManager
    {
         IGeneratePdf _generatePdf;

        public async Task<IActionResult> generator(List<BarcodeViewModel> _viewModel)
        {
          return await _generatePdf.GetPdf("Views/BarcodeList.cshtml", _viewModel);
        }

        //methode de generation dun pdf a partir dune template html fourni en parametre
        public static async Task<byte[]> GenerateReportFile<TViewModel>(ITemplateService _templateService, IConverter _converter,
         string _reportTemplate, TViewModel _viewModel)
        {
            //init vars
            

            var vm = new { teste = "" };
            string documentContent = await _templateService.RenderTemplateAsync(_reportTemplate, _viewModel);

            var doc = new HtmlToPdfDocument()
            {
                       GlobalSettings = {
                                            PaperSize = PaperKind.A4,
                                            Orientation = Orientation.Portrait, //Landscape
                                            DPI = 380
                                        },

                             Objects = {
                                            new ObjectSettings()
                                            {
                                                PagesCount = true,
                                                HtmlContent = documentContent,
                                                WebSettings = { DefaultEncoding = "utf-8"},
                                                HeaderSettings = { FontSize = 9, Right = "Page [page] / [toPage]", Line = true, Spacing = 2.812}
                                            }
                                        }
            };

            byte[] pdf = _converter.Convert(doc);
            return pdf;
        }//fin GenerateReportFile

        //methode de generation dun pdf a partir dune template html fourni en parametre
        public static async Task<byte[]> GenerateReportFileInvoice<TViewModel>(ITemplateService _templateService, IConverter _converter,
                 string _reportTemplate, TViewModel _viewModel)
        {
            //init vars
            // dimensions for sale ticket
            PechkinPaperSize invoiceSize = new PechkinPaperSize("70mm", "110mm");

            //
            var vm = new { teste = "" };
            string documentContent = await _templateService.RenderTemplateAsync(
                _reportTemplate, _viewModel);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                                            PaperSize = invoiceSize, // PaperKind.A4,
                                            Orientation = Orientation.Portrait,
                                            DPI = 380,
                                             Margins = new MarginSettings() { Top = 0, Left = 0, Right = 0 },
                                        },

                Objects = {
                                            new ObjectSettings()
                                            {
                                                PagesCount = true,
                                                HtmlContent = documentContent,
                                                WebSettings = { DefaultEncoding = "utf-8" },
                                                // HeaderSettings = { FontSize = 9, Right = "Page [page] / [toPage]", Line = true, Spacing = 2.812 },
                                                FooterSettings = { FontSize = 9, Center = "MERCI DE VOTRE AIMABLE VISITE", Line = true, Spacing = 2.812 }

                                            }
                                        }
            };

            byte[] pdf = _converter.Convert(doc);
            return pdf;
        }//fin GenerateReportFile

    }



}