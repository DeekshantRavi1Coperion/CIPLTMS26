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

public partial class PROJECT_LOT_LOTTransmittalFactoryInPDF : System.Web.UI.Page
{

    #region VARIABLES[===========]
    int _pdfType = 0;
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
                ViewState["TF_NO"] = null;
                Session["dsDetail"] = null;
                if (Request.QueryString["pdfType"] != null)
                    _pdfType = Convert.ToInt32(Request.QueryString["pdfType"]);

                GetDetail(Convert.ToInt32(Request.QueryString["LOTTFID"]), Convert.ToString(Request.QueryString["LOTTFSubitemIDs"]));
                ltTable.Text = GetPDFDetailAndReturnHTML();
            }
            else
            {
                if (Request.QueryString["LOTTFID"] != null && Convert.ToInt32(Request.QueryString["LOTTFID"]) == 0 && Session["dtLOT"] != null)
                {
                    GetDetail(0, "");
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

    private void GetDetail(int LOTTFID, string LOTTFSubitemIDs)
    {
        if (LOTTFID > 0)
        {
            dsDetail = objProject.GetLOTTFDetailsThree(LOTTFID, LOTTFSubitemIDs);
            //dsDetail = objProject.GetJOBMailInfoTwo(LOTTFID);
            if (dsDetail.Tables.Count > 0)
                Session["dsDetail"] = dsDetail;
            else
                Session["dsDetail"] = null;
        }
        else
        {
            dsDetail.Tables.Clear();
            DataTable dtLOT1 = new DataTable();
            DataTable dtSubitems1 = new DataTable();           
            DataTable dtQuantityDetails1 = new DataTable();
            DataTable dtEditedOrAmended1 = new DataTable();

            if (Session["dtLOT"] != null)
            {
                dtLOT1 = (DataTable)Session["dtLOT"];
            }

            if (Session["dtSubitems"] != null)
            {
                dtSubitems1 = (DataTable)Session["dtSubitems"];
            }

            if (Session["dtQuantityDetails"] != null)
            {
                dtQuantityDetails1 = (DataTable)Session["dtQuantityDetails"];
            }

            if (Session["dtEditedOrAmended"] != null)
            {
                dtEditedOrAmended1 = (DataTable)Session["dtEditedOrAmended"];
            }

            if (dtLOT1 != null && dtLOT1.Rows.Count > 0)
            {
                //dsDetail.Tables.Add(dtLOT1);

                DataTable dtCopy = dtLOT1.Copy();
                dsDetail.Tables.Add(dtCopy);

            }

            if (dtSubitems1 != null && dtSubitems1.Rows.Count > 0)
            {
                //dsDetail.Tables.Add(dtSubitems1);

                DataTable dtCopy = dtSubitems1.Copy();
                dsDetail.Tables.Add(dtCopy);
            }

           

            if (dtQuantityDetails1 != null && dtQuantityDetails1.Rows.Count > 0)
            {
                //dsDetail.Tables.Add(dtQuantityDetails1);

                DataTable dtCopy = dtQuantityDetails1.Copy();
                dsDetail.Tables.Add(dtCopy);
            }

            if (dtSubitems1 != null && dtSubitems1.Rows.Count > 0)
            {
                DataTable dtCopy = dtEditedOrAmended1.Copy();
                dsDetail.Tables.Add(dtCopy);
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
            if (Session["dsDetail"] != null)
                dsDetail = (DataSet)Session["dsDetail"];
            else
            {
                dsDetail = objProject.GetLOTTFDetailsThree(Convert.ToInt32(Request.QueryString["LOTTFID"]), Convert.ToString(Request.QueryString["LOTTFSubitemIDs"]));
                //dsDetail = objProject.GetJOBMailInfoTwo(Convert.ToInt32(Request.QueryString["LOTTFID"]));
            }

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                ViewState["TF_NO"] = Convert.ToString(dsDetail.Tables[0].Rows[0]["TF_NO"]);
                //htmlText = objLOTHtmlForPDF.GetHtmlForPDF(dsDetail.Tables[0], dsDetail.Tables[1]);

                LOTHtmlForPDFApproval objLOTHtmlForPDFApproval = new LOTHtmlForPDFApproval();
                htmlText = objLOTHtmlForPDFApproval.GetHtmlForPDF(dsDetail.Tables[0], dsDetail.Tables[1], dsDetail.Tables[2], dsDetail.Tables[3], _pdfType);
            }
            else
            {
                ViewState["TF_NO"] = null;
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
                if (ViewState["TF_NO"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["TF_NO"])))
                    fileName = Convert.ToString(ViewState["TF_NO"]);
                else
                    fileName = "LOT_" + DateTime.Now.ToString("dd-MMM-yyyy");

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