using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Management;
using iTextSharp.text;
using System.IO;
//using Spire.Pdf;
using System.Data;
using BAL;

public class PaymentVoucherPrintDocument
{
    public bool PrintPDF(Byte[] fileBytes, string printer, string paperName, int copies)
    {
        try
        {
            Stream stream1 = new MemoryStream(fileBytes);
            bool check = Print(stream1, printer, copies, paperName);
            return check;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    private bool Print(Stream stream, string printer, int copies, string paperName)
    {
        try
        {
            //PdfDocument pdfdocument = new PdfDocument();
            //pdfdocument.LoadFromStream(stream);

            //if (pdfdocument != null)
            //{
            //    pdfdocument.PrintSettings.PrinterName = printer;
            //    pdfdocument.PrintSettings.Copies = (short)copies;

            //    PrinterSettings ps = new PrinterSettings();
            //    IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();

            //    if (paperName == "A2")
            //    {
            //        PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A2);
            //        pdfdocument.PrintSettings.PaperSize = paperSize;
            //    }
            //    else if (paperName == "A3")
            //    {
            //        PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A3);
            //        pdfdocument.PrintSettings.PaperSize = paperSize;
            //    }
            //    else if (paperName == "A4")
            //    {
            //        PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
            //        pdfdocument.PrintSettings.PaperSize = paperSize;
            //    }
            //    else if (paperName == "A5")
            //    {
            //        PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5);
            //        pdfdocument.PrintSettings.PaperSize = paperSize;
            //    }
            //    else if (paperName == "A6")
            //    {
            //        PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A6);
            //        pdfdocument.PrintSettings.PaperSize = paperSize;
            //    }

            //    pdfdocument.Print();
            //    pdfdocument.Dispose();
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}