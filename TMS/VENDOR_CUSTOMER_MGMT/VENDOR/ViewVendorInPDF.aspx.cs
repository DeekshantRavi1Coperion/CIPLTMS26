using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using BAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;

public partial class VENDOR_CUSTOMER_MGMT_VENDOR_ViewVendorInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]

    //int _pdfType = 0;
    BAL.VendorCustomerManagement objVCM = new VendorCustomerManagement();
    DataSet dsDetail = new DataSet();


    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["pid"] != null)
            {
                //ViewState["TF_NO"] = null;
                Session["dsDetail"] = null;
                //if (Request.QueryString["pdfType"] != null)
                //    _pdfType = Convert.ToInt32(Request.QueryString["pdfType"]);

                GetDetail(Convert.ToInt32(Request.QueryString["pid"]));
                ltTable.Text = GetPDFDetailAndReturnHTML();
            }
            else
            {
                if (Request.QueryString["pid"] != null && Convert.ToInt32(Request.QueryString["pid"]) == 0)
                {
                    GetDetail(0);
                    ltTable.Text = GetPDFDetailAndReturnHTML();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }

        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        GeneratePDF();
    }

    #endregion


    #region METHODS[=============]

    private void GetDetail(int pid)
    {
        if (pid > 0)
        {
            dsDetail = objVCM.GetVendorForPDF(pid);
            if (dsDetail.Tables.Count > 0)
                Session["dsDetail"] = dsDetail;
            else
                Session["dsDetail"] = null;
        }
        else
        {
            dsDetail.Tables.Clear();

            if (Session["dtVendorDetails"] != null)
            {
                DataTable dtVendorDetails = (DataTable)Session["dtVendorDetails"];
                DataTable dtCopy0 = dtVendorDetails.Copy();
                dtCopy0.TableName = "VendorDetails";
                dsDetail.Tables.Add(dtCopy0);
            }

            if (Session["dtBillingAddress"] != null)
            {
                DataTable dtBillingAddress = (DataTable)Session["dtBillingAddress"];
                DataTable dtCopy1 = dtBillingAddress.Copy();
                dtCopy1.TableName = "BillingAddress";
                dsDetail.Tables.Add(dtCopy1);
            }

            if (Session["dtContactPerson"] != null)
            {
                DataTable dtContactPerson = (DataTable)Session["dtContactPerson"];
                DataTable dtCopy2 = dtContactPerson.Copy();
                dtCopy2.TableName = "ContactPerson";
                dsDetail.Tables.Add(dtCopy2);
            }

            if (Session["dtBankDetails"] != null)
            {
                DataTable dtBankDetails = (DataTable)Session["dtBankDetails"];
                DataTable dtCopy3 = dtBankDetails.Copy();
                dtCopy3.TableName = "BankDetails";
                dsDetail.Tables.Add(dtCopy3);
            }


            if (Session["dtAmendments"] != null)
            {
                DataTable dtAmendments = (DataTable)Session["dtAmendments"];
                DataTable dtCopy4 = dtAmendments.Copy();
                dtCopy4.TableName = "Amendments";
                dsDetail.Tables.Add(dtCopy4);
            }


            if (dsDetail != null && dsDetail.Tables.Count > 0)
            {
                Session["dsDetail"] = dsDetail;
            }
            else
            {
                Session["dsDetail"] = null;
            }

        }
    }

    public string GetPDFDetailAndReturnHTML()
    {
        try
        {
            int pid = Convert.ToInt32(Request.QueryString["pid"]);

            if (Session["dsDetail"] != null)
                dsDetail = (DataSet)Session["dsDetail"];
            else
            {
                dsDetail = objVCM.GetVendorForPDF(pid);
            }

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                ViewState["NAME"] = Convert.ToString(dsDetail.Tables[0].Rows[0]["NAME"]);

                VendorHtmlForPDF objPDF = new VendorHtmlForPDF();
                htmlText = objPDF.GetHtmlForPDF(pid, dsDetail);
            }
            else
            {
                //ViewState["TF_NO"] = null;
                htmlText = string.Empty;
            }

            if (!string.IsNullOrEmpty(htmlText))
                return htmlText;
            else
                return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public void GeneratePDF()
    {
        try
        {
            string fileName = string.Empty;
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML();

            if (!string.IsNullOrEmpty(htmlTxt))
            {
                if (ViewState["NAME"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["NAME"])))
                    fileName = Convert.ToString(ViewState["NAME"]);
                else
                    fileName = "Vendor_" + DateTime.Now.ToString("dd-MMM-yyyy");

                sb.Append("<html>\n");
                sb.Append("<body>\n");
                sb.Append(htmlTxt + "\n");
                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_LEFT;
                img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    document.Add(img);
                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                    {
                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                        }
                    }

                    document.Close();
                    pdf = memoryStream.GetBuffer();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
                    Response.OutputStream.Write(pdf, 0, pdf.Length);
                    Response.End();
                }
            }
            else
            {
                //
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #endregion
}