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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class LOTPrintDocument
{
    Project objProject = new Project();
    DataSet dsDrawings = new DataSet();

    Byte[] fileBytes1 = null;
    Byte[] fileBytes2 = null;
    Byte[] fileBytes3 = null;
    Byte[] fileBytes4 = null;

    public bool PrintPDF(int LOTTFID, string LOTTFSubItemIDs, string printer, string paperName, int copies)
    {
        try
        {
            dsDrawings = objProject.GetDrawingsToPrint(LOTTFID, LOTTFSubItemIDs);
            if (dsDrawings.Tables.Count > 0 && dsDrawings.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDrawings.Tables[0].Rows)
                {
                    fileBytes1 = null;
                    fileBytes2 = null;
                    fileBytes3 = null;
                    fileBytes4 = null;


                    if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT1_NAME"])) && dr["SI_ATTACHMENT1_DOC"] != DBNull.Value)
                        fileBytes1 = (byte[])dr["SI_ATTACHMENT1_DOC"];

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT2_NAME"])) && dr["SI_ATTACHMENT2_DOC"] != DBNull.Value)
                        fileBytes2 = (byte[])dr["SI_ATTACHMENT2_DOC"];

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT3_NAME"])) && dr["SI_ATTACHMENT3_DOC"] != DBNull.Value)
                        fileBytes3 = (byte[])dr["SI_ATTACHMENT3_DOC"];

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["SI_ATTACHMENT4_NAME"])) && dr["SI_ATTACHMENT4_DOC"] != DBNull.Value)
                        fileBytes4 = (byte[])dr["SI_ATTACHMENT4_DOC"];




                    if (fileBytes1 != null)
                    {
                        Stream stream1 = new MemoryStream(fileBytes1);
                        Print(stream1, printer, copies, paperName);
                    }

                    if (fileBytes2 != null)
                    {
                        Stream stream2 = new MemoryStream(fileBytes2);
                        Print(stream2, printer, copies, paperName);
                    }

                    if (fileBytes3 != null)
                    {
                        Stream stream3 = new MemoryStream(fileBytes3);
                        Print(stream3, printer, copies, paperName);
                    }

                    if (fileBytes4 != null)
                    {
                        Stream stream4 = new MemoryStream(fileBytes4);
                        Print(stream4, printer, copies, paperName);
                    }
                }

                return true;
            }
            else
                return false;
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



    public void Print1(Control ctrl, string Script)

    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
        }
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }





    public void GetAllPrinterList()
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

}