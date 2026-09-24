using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
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

public partial class FINANCE_AdvancePaymentVoucherDetailInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]

    string paymentRunNumber = string.Empty;
    string unit = string.Empty;
    string status = string.Empty;


    BAL.Finance objFinance = new Finance();
    DataSet dsDetail = new DataSet();
    AdvancePaymentVoucherHtmlForPDF objAdvancePaymentVoucherHtmlForPDF = new AdvancePaymentVoucherHtmlForPDF();

    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            paymentRunNumber = string.Empty;
            unit = string.Empty;
            status = string.Empty;

            if (Request.QueryString["paymentrunnumber"] != null)
            {
                if (Request.QueryString["paymentrunnumber"] != null)
                    paymentRunNumber = Convert.ToString(Request.QueryString["paymentrunnumber"]);

                if (Request.QueryString["unit"] != null)
                    unit = Convert.ToString(Request.QueryString["unit"]);

                if (Request.QueryString["status"] != null)
                    status = Convert.ToString(Request.QueryString["status"]);


                Session["dsDetail"] = null;
                GetDetail(paymentRunNumber, unit, status);
                ltTable.Text = GetPDFDetailAndReturnHTML(paymentRunNumber, unit, status, 1);
            }
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        paymentRunNumber = string.Empty;
        unit = string.Empty;
        status = string.Empty;

        if (Request.QueryString["paymentrunnumber"] != null)
        {
            if (Request.QueryString["paymentrunnumber"] != null)
                paymentRunNumber = Convert.ToString(Request.QueryString["paymentrunnumber"]);

            if (Request.QueryString["unit"] != null)
                unit = Convert.ToString(Request.QueryString["unit"]);

            if (Request.QueryString["status"] != null)
                status = Convert.ToString(Request.QueryString["status"]);

            GeneratePDF(paymentRunNumber, unit, status);
        }
        else
            Response.Redirect("~/Login.aspx");


    }

    #endregion


    #region METHODS[=============]

    private void GetDetail(string paymentRunNumber, string unit, string status)
    {
        dsDetail = objFinance.GetAdvanceEPaymentVoucher(0, "", "", "", unit, "", "", paymentRunNumber, "", status, "", "", "");
        if (dsDetail.Tables.Count > 0)
            Session["dsDetail"] = dsDetail;
        else
            Session["dsDetail"] = null;
    }

    public string GetPDFDetailAndReturnHTML(string paymentRunNumber, string unit, string status, int hiddenFlag)
    {
        try
        {
            if (Session["dsDetail"] != null)
                dsDetail = (DataSet)Session["dsDetail"];
            else
            {
                dsDetail = objFinance.GetAdvanceEPaymentVoucher(0, "", "", "", unit, "", "", paymentRunNumber, "", status, "", "", "");
            }

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                htmlText = objAdvancePaymentVoucherHtmlForPDF.GetHtmlForPDF(dsDetail, paymentRunNumber, 0, imagePath, hiddenFlag);
            }
            else
            {
                htmlText = string.Empty;
            }

            if (!string.IsNullOrEmpty(htmlText))
                return htmlText;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void GeneratePDF(string paymentRunNumber, string unit, string status)
    {
        try
        {
            string fileName = string.Empty;
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(paymentRunNumber, unit, status, 0);

            if (!string.IsNullOrEmpty(htmlTxt))
            {
                fileName = "Voucher_Detail_of_PRN_" + paymentRunNumber + DateTime.Now.ToString("dd-MMM-yyyy");

                sb.Append("<html>\n");
                sb.Append("<body>\n");
                sb.Append(htmlTxt + "\n");
                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                //string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                //img.Alignment = Element.ALIGN_LEFT;
                //img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    //document.Add(img);
                    
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