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

public partial class FINANCE_PaymentVoucherDetailInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]

    string factVoucherNumber = string.Empty;
    string unit = string.Empty;
    string status = string.Empty;

    BAL.Finance objFinance = new Finance();
    DataSet dsDetail = new DataSet();
    PaymentVoucherHtmlForPDF objPaymentVoucherHtmlForPDF = new PaymentVoucherHtmlForPDF();

    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            factVoucherNumber = string.Empty;
            unit = string.Empty;
            status = string.Empty;

            if (Request.QueryString["factvouchernumber"] != null)
            {
                if (Request.QueryString["factvouchernumber"] != null)
                    factVoucherNumber = Convert.ToString(Request.QueryString["factvouchernumber"]);

                if (Request.QueryString["unit"] != null)
                    unit = Convert.ToString(Request.QueryString["unit"]);

                if (Request.QueryString["status"] != null)
                    status = Convert.ToString(Request.QueryString["status"]);

                Session["dsDetail"] = null;
                GetDetail(factVoucherNumber, unit, status);
                ltTable.Text = GetPDFDetailAndReturnHTML(factVoucherNumber, unit, status, 1);
            }
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        factVoucherNumber = string.Empty;
        unit = string.Empty;
        status = string.Empty;

        if (Request.QueryString["factvouchernumber"] != null)
        {
            if (Request.QueryString["factvouchernumber"] != null)
                factVoucherNumber = Convert.ToString(Request.QueryString["factvouchernumber"]);

            if (Request.QueryString["unit"] != null)
                unit = Convert.ToString(Request.QueryString["unit"]);

            if (Request.QueryString["status"] != null)
                status = Convert.ToString(Request.QueryString["status"]);

            GeneratePDF(factVoucherNumber, unit, status);
        }
        else
            Response.Redirect("~/Login.aspx");


    }

    #endregion


    #region METHODS[=============]

    private void GetDetail(string factVoucherNumber, string unit, string status)
    {
        dsDetail = objFinance.GetEPaymentVoucher(0, "", "", "", unit, "", "", factVoucherNumber, status, "", "", "", "");
        if (dsDetail.Tables.Count > 0)
            Session["dsDetail"] = dsDetail;
        else
            Session["dsDetail"] = null;
    }

    public string GetPDFDetailAndReturnHTML(string factVoucherNumber, string unit, string status, int hiddenFlag)
    {
        try
        {
            if (Session["dsDetail"] != null)
                dsDetail = (DataSet)Session["dsDetail"];
            else
            {
                dsDetail = objFinance.GetEPaymentVoucher(0, "", "", "", unit, "", "", factVoucherNumber, status, "", "", "", "");
            }

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                htmlText = objPaymentVoucherHtmlForPDF.GetHtmlForPDF(dsDetail, factVoucherNumber, 0, imagePath, hiddenFlag);
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

    public void GeneratePDF(string factVoucherNumber, string unit, string status)
    {
        try
        {
            string fileName = string.Empty;
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(factVoucherNumber, unit, status, 0);

            if (!string.IsNullOrEmpty(htmlTxt))
            {
                fileName = factVoucherNumber + "_Voucher_Detail_" + DateTime.Now.ToString("dd-MMM-yyyy");

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