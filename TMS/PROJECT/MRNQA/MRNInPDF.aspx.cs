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

public partial class PROJECT_LOT_LOTTransmittalFactoryInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]

    BAL.Project objProject = new Project();
    DataSet dsDetail = new DataSet();
    LOTHtmlForPDF objLOTHtmlForPDF = new LOTHtmlForPDF();
    
    #endregion


    #region EVENTS[==============]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["LOTTFID"] != null)
            {
                Session["dsDetail"] = null;

                GetDetail(Convert.ToInt32(Request.QueryString["LOTTFID"]));
                ltTable.Text = GetPDFDetailAndReturnHTML();
            }
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GeneratePDF();
        }
        catch (Exception ex)
        {
            //
        }
    }

    #endregion


    #region METHODS[=============]

    private void GetDetail(int LOTTFID)
    {
        dsDetail = objProject.GetLOTTFDetailsOne(LOTTFID);
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
                dsDetail = objProject.GetLOTTFDetailsOne(Convert.ToInt32(Request.QueryString["LOTTFID"]));


            string htmlText = string.Empty;           
            htmlText = objLOTHtmlForPDF.GetHtmlForPDF(dsDetail.Tables[0], dsDetail.Tables[1]);

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
        byte[] pdf;
        var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

        string htmlTxt = string.Empty;
        StringBuilder sb = new StringBuilder();

        htmlTxt = GetPDFDetailAndReturnHTML();

        if (!string.IsNullOrEmpty(htmlTxt))
        {
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
                Response.AddHeader("content-disposition", "attachment;filename=dd.pdf");
                Response.OutputStream.Write(pdf, 0, pdf.Length);
                Response.End();
            }
        }
        else
        {
            //
        }
    }

    #endregion
}