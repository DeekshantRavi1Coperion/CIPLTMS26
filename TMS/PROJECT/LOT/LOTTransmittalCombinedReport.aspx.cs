using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;

using System.Net.Mime;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using System.Xml;
using iTextSharp.tool.xml.css;

public partial class PROJECT_LOT_LOTTransmittalCombinedReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsLOTMainSubItems = new DataSet();
    DataSet dsLOTList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsLOTfor = new DataSet();
    DataSet dsLOTStatus = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string LOTTFNo = string.Empty;
    int statusID = 0;
    int unitID = 0;
    int LOTMainSubitemID = 0;
    string drawingStatus = string.Empty;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    int isPartOfProductionFlag = 0;

    #endregion



    #region EVENTS START[===================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanelReviseList();

            if (!IsPostBack)
            {
                Session["dtLOTReport"] = null;

                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;

                BindCompany();
                BindStatus();
                BindLOTMainItems();

                GetCombinedReport();
            }
        }
        else
        {
            Session["dtLOTReport"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlLOTMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLOTMainItems.SelectedIndex > 0)
        {
            BindLOTMainSubItems(Convert.ToInt32(ddlLOTMainItems.SelectedValue));
        }
        else
        {
            ddlLOTMainSubitems.Items.Clear();
            ddlLOTMainSubitems.Items.Insert(0, "All");
            ddlLOTMainSubitems.SelectedIndex = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetCombinedReport();
    }

    protected void gvCombinedReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");


                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }


                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvCombinedReport.Rows.Count > 0)
        {
            if (Session["dtLOTReport"] != null)
            {
                DataTable dt = (DataTable)Session["dtLOTReport"];
                GeneratePDF(dt);
            }

        }
        else
        {
            //
        }
    }


    #endregion EVENTS END[==================]



    #region METHODS START[==================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindStatus()
    {
        try
        {
            dsLOTStatus = objProject.GetLOTTFStatus();
            if (dsLOTStatus.Tables.Count > 0 && dsLOTStatus.Tables[0].Rows.Count > 0)
            {
                DataTable dtnew = new DataTable();
                dtnew.Columns.Add("STATUS_ID", typeof(int));
                dtnew.Columns.Add("STATUS_NAME", typeof(string));
                for (int i = 0; i <= 5; i++)
                {
                    DataRow dr = dtnew.NewRow();
                    dr["STATUS_ID"] = Convert.ToInt32(dsLOTStatus.Tables[0].Rows[i]["STATUS_ID"]);
                    dr["STATUS_NAME"] = Convert.ToString(dsLOTStatus.Tables[0].Rows[i]["STATUS_NAME"]);
                    dtnew.Rows.Add(dr);
                }

                ddlStatus.DataSource = dtnew;
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;


            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItems()
    {
        try
        {
            dsLOTfor = objProject.GetLotMainItems();
            if (dsLOTfor.Tables.Count > 0 && dsLOTfor.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTfor.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "All");
                ddlLOTMainItems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItems(int LOTMainItemID)
    {
        try
        {
            unitID = 0;

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            dsLOTMainSubItems = objProject.GetLotMainSubItems(LOTMainItemID, unitID);
            if (dsLOTMainSubItems.Tables.Count > 0 && dsLOTMainSubItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainSubitems.DataSource = dsLOTMainSubItems.Tables[0];
                ddlLOTMainSubitems.DataTextField = "LOT_MAIN_SUBITEM";
                ddlLOTMainSubitems.DataValueField = "LOT_MAIN_SUBITEM_ID";
                ddlLOTMainSubitems.DataBind();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "All");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }

    private void GetCombinedReport()
    {
        try
        {
            startDate = string.Empty;
            endDate = string.Empty;
            LOTTFNo = string.Empty;
            statusID = 0;
            unitID = 0;
            jobNo = string.Empty;
            customerName = string.Empty;
            LOTMainSubitemID = 0;
            drawingStatus = string.Empty;
            isPartOfProductionFlag = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtTFNo.Text))
                LOTTFNo = txtTFNo.Text;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;

            if (ddlLOTMainSubitems.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitems.SelectedValue);


            if (chkIsPartofProduction.Checked)
                isPartOfProductionFlag = 1;

            dsLOTList = objProject.GetLOTCombinedReport(startDate, endDate, LOTTFNo, statusID, unitID, jobNo, customerName, LOTMainSubitemID, isPartOfProductionFlag);

            if (dsLOTList.Tables.Count > 0 && dsLOTList.Tables[0].Rows.Count > 0)
            {
                Session["dtLOTReport"] = dsLOTList.Tables[0];
                gvCombinedReport.DataSource = dsLOTList.Tables[0];
                gvCombinedReport.DataBind();
            }
            else
            {
                Session["dtLOTReport"] = null;
                gvCombinedReport.DataSource = null;
                gvCombinedReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsLOTList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessageReviseList(ex.ToString());
            return;
        }
    }




    public void GeneratePDF(DataTable dtReport)
    {
        try
        {
            string fileName = "Combined_Report_" + DateTime.Now.ToString("dd_MMM-yyyy");
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOTCombined.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(dtReport);

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
                    var document = new Document(PageSize.A2.Rotate());
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
            //
        }
    }

    public string GetPDFDetailAndReturnHTML(DataTable dtReport)
    {
        try
        {
            string htmlText = string.Empty;

            CombinedLOTReportPDF objPDF = new CombinedLOTReportPDF();

            htmlText = objPDF.GetHtmlForPDF(dtReport);

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



    //public void GeneratePDF(DataTable dtProc)
    //{
    //    try
    //    {
    //        string fileName = "Combined_LOT_Report_" + DateTime.Now.ToString("dd_MMM-yyyy");
    //        byte[] pdf;
    //        var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

    //        string htmlTxt = string.Empty;
    //        StringBuilder sb = new StringBuilder();

    //        htmlTxt = GetPDFDetailAndReturnHTML(dtProc);

    //        if (!string.IsNullOrEmpty(htmlTxt))
    //        {
    //            sb.Append("<html>\n");
    //            sb.Append("<body>\n");
    //            sb.Append(htmlTxt + "\n");
    //            sb.Append("</body>\n");
    //            sb.Append("</html>\n");
    //        }

    //        var html = sb.ToString();
    //        if (!string.IsNullOrEmpty(Convert.ToString(html)))
    //        {
    //            string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
    //            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
    //            img.Alignment = Element.ALIGN_LEFT;
    //            img.ScaleToFit(180f, 250f);

    //            using (var memoryStream = new MemoryStream())
    //            {
    //                var document = new Document(PageSize.A2);
    //                var writer = PdfWriter.GetInstance(document, memoryStream);
    //                document.Open();
    //                document.Add(img);
    //                using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
    //                {
    //                    using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
    //                    {
    //                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
    //                    }
    //                }

    //                document.Close();
    //                pdf = memoryStream.GetBuffer();

    //                Response.ContentType = "application/pdf";
    //                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
    //                Response.OutputStream.Write(pdf, 0, pdf.Length);
    //                Response.End();
    //            }
    //        }
    //        else
    //        {
    //            //
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //
    //    }
    //}

    //public string GetPDFDetailAndReturnHTML(DataTable dtProc)
    //{
    //    try
    //    {
    //        string htmlText = string.Empty;

    //        CombinedLOTReportPDF objPDF = new CombinedLOTReportPDF();

    //        htmlText = objPDF.GetHtmlForPDF(dtProc);

    //        if (!string.IsNullOrEmpty(htmlText))
    //            return htmlText;
    //        else
    //            return null;
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }
    //}





    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            //for (int i = 1; i < gvCombinedReport.Columns.Count; i++)
            //{
            //    csv += Convert.ToString(gvCombinedReport.Columns[i].HeaderText) + ',';
            //}

            for (int i = 23; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";



            //foreach (GridViewRow gr in gvCombinedReport.Rows)
            //{
            //    for (int j = 1; j < gvCombinedReport.Columns.Count; j++)
            //    {

            //        if (!string.IsNullOrEmpty(Convert.ToString(gr.Cells[j].Text)) && Convert.ToString(gr.Cells[j].Text) != "&nbsp;")
            //            rowTxt = Convert.ToString(gr.Cells[j].Text);
            //        else
            //            rowTxt = string.Empty;

            //        csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';

            //    }
            //    csv += "\r\n";
            //}


            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 23; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');


                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "LOT_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ViewDrawingFiles(int LOTTFID, string fileType, string fileName, int LOTTFSubitemID)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" || extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    //imgFile.ImageUrl = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    //mpeShowImageFile.Show();

                    string url = "ViewAttachedImageFile.ashx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    //iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "");
                    //mpeShowPDFFile.Show();


                    string url = "ViewAttachedPDFFile.aspx?LOTTFID=" + LOTTFID + "&fileType=" + fileType + "&LOTTFSubitemID=" + LOTTFSubitemID + "";
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1200/2);var Mtop = (screen.height/2)-(900/2);window.open( '" + url + "', null, 'height=900,width=1200,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                }
            }
            else
            {
                ExceptionMessageReviseList("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ExceptionMessageReviseList(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanelReviseList()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }


    #endregion METHODS END[=================]

}