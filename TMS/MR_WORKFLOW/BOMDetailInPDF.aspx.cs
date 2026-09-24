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

public partial class MR_WORKFLOW_BOMDetailInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]

    BAL.PROJECT_MANAGEMENT.ProjectManagement objProjectM = new BAL.PROJECT_MANAGEMENT.ProjectManagement();
    DataSet dsDetail = new DataSet();
    BOMHtmlForPDF objBOMHtmlForPDF = new BOMHtmlForPDF();

    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["recordid"] != null)
            {
                Session["dsDetail"] = null;
                GetDetail(Convert.ToInt32(Request.QueryString["recordid"]));
                ltTable.Text = GetPDFDetailAndReturnHTML();
            }
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        GeneratePDF();
    }

    #endregion


    #region METHODS[=============]

    private void GetDetail(int recordID)
    {
        //dsDetail = objProject.GetDMSDrawingDetailForPDF(recordID);
        dsDetail = objProjectM.GetBomMailInfo(recordID);
        if (dsDetail.Tables.Count > 0)
            Session["dsDetail"] = dsDetail;
        else
            Session["dsDetail"] = null;
    }

    public string GetPDFDetailAndReturnHTML()
    {
        try
        {
            if (Session["dsDetail"] != null)
                dsDetail = (DataSet)Session["dsDetail"];
            else
            {
                dsDetail = objProjectM.GetBomMailInfo(Convert.ToInt32(Request.QueryString["recordid"]));
            }

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                htmlText = objBOMHtmlForPDF.GetHtmlForPDF(dsDetail.Tables[0], dsDetail.Tables[1]);
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
                //if (ViewState["TF_NO"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["TF_NO"])))
                //    fileName = Convert.ToString(ViewState["TF_NO"]);
                //else
                //    fileName = "Drawing_Detail_" + DateTime.Now.ToString("dd-MMM-yyyy");

                fileName = "Drawing_Detail_" + DateTime.Now.ToString("dd-MMM-yyyy");

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