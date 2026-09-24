using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
//using System.Web;
using System.Management;
//using iTextSharp.text;
using System.IO;
//using Spire.Pdf;

public class LOTPrintDocument1
{
    public bool PrintPDF(string printer, string paperName, int copies, Stream stream)
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
            //    //
            //}




            //// Create the printer settings for our printer
            //var printerSettings = new PrinterSettings
            //{
            //    PrinterName = printer,
            //    Copies = (short)copies,
            //};

            //// Create our page settings for the paper size selected
            //var pageSettings = new PageSettings(printerSettings)
            //{
            //    Margins = new Margins(0, 0, 0, 0),
            //};
            //foreach (PaperSize paperSize in printerSettings.PaperSizes)
            //{
            //    if (paperSize.PaperName == paperName)
            //    {
            //        pageSettings.PaperSize = paperSize;
            //        break;
            //    }
            //}




            ////Printer Settings
            //PrinterSettings ps = new PrinterSettings();
            //ps.PrinterName = printer;
            //ps.Copies = (short)copies;



            ////Page Size
            //// Create our page settings for the paper size selected
            //var pageSettings = new PageSettings(ps)
            //{
            //    Margins = new Margins(0, 0, 0, 0),
            //};


            //PrintDocument recordDoc = new PrintDocument();
            //recordDoc.PrinterSettings = ps;

            //IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();

            //if (paperName == "A2")
            //{
            //    PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A2);
            //    recordDoc.DefaultPageSettings.PaperSize = paperSize;
            //}
            //else if (paperName == "A3")
            //{
            //    PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A3);
            //    recordDoc.DefaultPageSettings.PaperSize = paperSize;
            //}
            //else if (paperName == "A4")
            //{
            //    PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
            //    recordDoc.DefaultPageSettings.PaperSize = paperSize;
            //}
            //else if (paperName == "A5")
            //{
            //    PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5);
            //    recordDoc.DefaultPageSettings.PaperSize = paperSize;
            //}
            //else if (paperName == "A6")
            //{
            //    PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A6);
            //    recordDoc.DefaultPageSettings.PaperSize = paperSize;
            //}



















            //// B: Load PDF file from Stream

            ////create a pdf document.
            //Spire.Pdf.PdfDocument docFrom = new Spire.Pdf.PdfDocument();


            ////PdfDocument d = new PdfDocument();
            ////PdfWriter.GetInstance(d, stream);

            ////load PDF file from stream
            ////FileStream from_stream = (FileStream)stream;
            //docFrom.LoadFromStream(stream);

            ////var document = PdfiumViewer.PdfDocument.Load(stream);

            //var document = docFrom;

            ////var document = document;
            //var printDocument = document.CreatePrintDocument();

            ////printDocument.PrinterSettings = ps;
            ////printDocument.DefaultPageSettings = pageSettings;
            ////printDocument.PrintController = new StandardPrintController();
            ////printDocument.Print();



            //////save the pdf document
            ////docFrom.SaveToFile("From_stream.pdf", FileFormat.PDF);

            ////System.Diagnostics.Process.Start("From_stream.pdf");



            //////Now print the PDF document
            //////using (var document = PdfiumViewer.PdfDocument.Load(stream))
            //////{
            //////    using (var printDocument = document.CreatePrintDocument())
            //////    {
            //////        printDocument.PrinterSettings = printerSettings;
            //////        printDocument.DefaultPageSettings = pageSettings;
            //////        printDocument.PrintController = new StandardPrintController();
            //////        printDocument.Print();
            //////    }
            //////}


            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    private void GetAllPrinterList()
    {
        string printername = string.Empty;

        ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
        objScope.Connect();

        SelectQuery selectQuery = new SelectQuery();
        selectQuery.QueryString = "Select * from win32_Printer";
        ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
        ManagementObjectCollection MOC = MOS.Get();
        foreach (ManagementObject mo in MOC)
        {
            //lstPrinterList.Items.Add(mo["Name"].ToString());
            printername += (mo["Name"].ToString()) + "," + Environment.NewLine;
        }
    }

    public void PrintDocument(string fileName, byte[] fileBytes, string printerName, string pageSize, int copies)
    {
        try
        {
            string printern = string.Empty;
            //foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    printern += printer + "," + Environment.NewLine;
            //}


            //GetAllPrinterList();



            //Printer Settings
            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printerName;
            ps.Copies = (short)copies;



            //Page Size
            // Create our page settings for the paper size selected
            var pageSettings = new PageSettings(ps)
            {
                Margins = new Margins(0, 0, 0, 0),
            };


            PrintDocument recordDoc = new PrintDocument();
            recordDoc.PrinterSettings = ps;

            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();

            if (pageSize == "A2")
            {
                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A2);
                recordDoc.DefaultPageSettings.PaperSize = paperSize;
            }
            else if (pageSize == "A3")
            {
                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A3);
                recordDoc.DefaultPageSettings.PaperSize = paperSize;
            }
            else if (pageSize == "A4")
            {
                PaperSize paperSize = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
                recordDoc.DefaultPageSettings.PaperSize = paperSize;
            }
           


            //using (var document = Document.Load(filename))
            //{
            //    using (var printDocument = document.CreatePrintDocument())
            //    {
            //        printDocument.PrinterSettings = ps;
            //        printDocument.DefaultPageSettings = pageSettings;
            //        printDocument.PrintController = new StandardPrintController();
            //        printDocument.Print();
            //    }
            //}




        }
        catch (Exception ex)
        {
            //
        }
    }


    public bool PrintPDF(string printer, string paperName, string filename, int copies)
    {
        try
        {
            // Create the printer settings for our printer
            var printerSettings = new PrinterSettings
            {
                PrinterName = printer,
                Copies = (short)copies,
            };

            // Create our page settings for the paper size selected
            var pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0),
            };

            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.PaperName == paperName)
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            // Now print the PDF document
            //using (var document = PdfDocument)
            //{
            //    using (var printDocument = document.CreatePrintDocument())
            //    {
            //        printDocument.PrinterSettings = printerSettings;
            //        printDocument.DefaultPageSettings = pageSettings;
            //        printDocument.PrintController = new StandardPrintController();
            //        printDocument.Print();
            //    }
            //}
            return true;
        }
        catch
        {
            return false;
        }
    }    

}