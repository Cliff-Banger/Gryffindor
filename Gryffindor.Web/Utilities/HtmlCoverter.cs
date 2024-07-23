using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gryffindor.Web.Utilities
{
    public class HtmlCoverter
    {
        public static string PdfScript
        {
            get
            {
                return "<html><head><style>td,th{line-height:20px;} tr { page-break-inside: avoid }</style><script>function subst() {var vars={};var x=document.location.search.substring(1).split('&');for(var i in x) {var z=x[i].split('=',2);vars[z[0]] = unescape(z[1]);}" +
          "var x=['frompage','topage','page','webpage','section','subsection','subsubsection'];for(var i in x) {var y = document.getElementsByClassName(x[i]);" +
          "for(var j=0; j<y.length; ++j) y[j].textContent = vars[x[i]];}}</script></head><body onload=\"subst()\">";
            }
        }
        public static string ContentTypePdf { get { return "application/pdf"; } }

        public static byte[] ConvertHtmlToPDF(string htmlContent)
        {
            HtmlToPdfConverter nRecohtmltoPdfObj = new HtmlToPdfConverter();
            nRecohtmltoPdfObj.Orientation = PageOrientation.Portrait;
            //nRecohtmltoPdfObj.PageFooterHtml = CreatePDFFooter();
            nRecohtmltoPdfObj.CustomWkHtmlArgs = "--margin-top 35 --header-spacing 0 --margin-left 0 --margin-right 0";
            return nRecohtmltoPdfObj.GeneratePdf(PdfScript + htmlContent + "</body></html>");
        }
    }
}