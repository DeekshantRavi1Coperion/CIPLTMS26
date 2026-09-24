using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html;
using System.Collections;
using System.Net;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Text;

public partial class REPORTS_COMBINED_PROC_PROD_DESIGN_REPORT_CombinedProcProdDesignViewReportStatusWise : System.Web.UI.Page
{

    #region VARIABLES[=======================]
    CombinedReportTypes objCombinedReportTypes = new CombinedReportTypes();
    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet(); 
    DataSet dsReport = new DataSet();

    string unit = string.Empty;
    string JOBNo = string.Empty;
    int typeID = 0;
    string status = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindUnit();
                Session["dtProcReport"] = null;
                Session["dtProdReport"] = null;
                Session["dtDesignReport"] = null;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hdTotalPOValueINR.Value = "0";
        hdTotalQuantity.Value = "0";
        hdTotalSpentHours.Value = "0";
        hdTotalSpentMins.Value = "0";

        GetReport();
    }


    protected void gvProcurementViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPOValueINR = (Label)e.Row.FindControl("lblPOValueINR");
            hdTotalPOValueINR.Value = Convert.ToString(Convert.ToDouble(hdTotalPOValueINR.Value) + Convert.ToDouble(lblPOValueINR.Text));
        }

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    protected void gvProductionViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
            hdTotalQuantity.Value = Convert.ToString(Convert.ToDouble(hdTotalQuantity.Value) + Convert.ToDouble(lblQuantity.Text));
        }

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    protected void gvDesignViewReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int h = 0;
            int m = 0;

            Label lblTotalHoursSpent = (Label)e.Row.FindControl("lblTotalHoursSpent");
            if (!string.IsNullOrEmpty(Convert.ToString(lblTotalHoursSpent.Text)))
            {
                h = Convert.ToInt32(Convert.ToString(lblTotalHoursSpent.Text).Split(':')[0]);
                m = Convert.ToInt32(Convert.ToString(lblTotalHoursSpent.Text).Split(':')[1]);

                hdTotalSpentHours.Value = Convert.ToString(Convert.ToInt32(hdTotalSpentHours.Value) + h);
                hdTotalSpentMins.Value = Convert.ToString(Convert.ToInt32(hdTotalSpentMins.Value) + m);
            }
        }

        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }


    protected void btnExportCombinedReport_Click(object sender, EventArgs e)
    {
        DataTable dtProc = new DataTable();
        DataTable dtProd = new DataTable();
        DataTable dtDesign = new DataTable();

        if (gvProcurementViewReport.Rows.Count > 0)
        {
            if (Session["dtProcReport"] != null)
                dtProc = (DataTable)Session["dtProcReport"];
            else dtProc = null;
        }
        else dtProc = null;

        if (gvProductionViewReport.Rows.Count > 0)
        {
            if (Session["dtProdReport"] != null)
                dtProd = (DataTable)Session["dtProdReport"];
            else dtProd = null;
        }
        else dtProd = null;

        if (gvDesignViewReport.Rows.Count > 0)
        {
            if (Session["dtDesignReport"] != null)
                dtDesign = (DataTable)Session["dtDesignReport"];
            else dtDesign = null;
        }
        else dtDesign = null;

        GeneratePDF(dtProc, dtProd, dtDesign, txtJOBNo.Text.Trim().ToUpper());
    }




    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnitForCombinedReport();
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetReport()
    {
        try
        {
            unit = string.Empty;
            JOBNo = string.Empty;
            typeID = 0;
            status = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unit = Convert.ToString(ddlCompany.SelectedItem.Text);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedValue);

            dsReport = objReports.GetCombinedProcProdDesignReportStatusWise(unit,JOBNo, typeID, status);

            gvProcurementViewReport.DataSource = null;
            gvProcurementViewReport.DataBind();
            lblProcurementViewRecords.Text = "Procurement View Records [0]";

            gvProductionViewReport.DataSource = null;
            gvProductionViewReport.DataBind();
            lblProductionViewRecords.Text = "Production View Records [0]";

            gvDesignViewReport.DataSource = null;
            gvDesignViewReport.DataBind();
            lblDesignViewRecords.Text = "Design View Records [0]";

            Session["dtProcReport"] = null;
            Session["dtProdReport"] = null;
            Session["dtDesignReport"] = null;

            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    if (dsReport.Tables.Count > 0)
                    {
                        if (dsReport.Tables[0].Rows.Count > 0)
                        {
                            Session["dtProcReport"] = dsReport.Tables[0];
                            gvProcurementViewReport.DataSource = dsReport.Tables[0];
                            gvProcurementViewReport.DataBind();
                            lblProcurementViewRecords.Text = "Procurement View Records [" + dsReport.Tables[0].Rows.Count + "]";
                        }
                        else
                        {
                            Session["dtProcReport"] = null;
                            gvProcurementViewReport.DataBind();
                            lblProcurementViewRecords.Text = "Procurement View Records [0]";
                        }
                    }
                }

                else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    if (dsReport.Tables.Count > 0)
                    {
                        if (dsReport.Tables[0].Rows.Count > 0)
                        {
                            Session["dtProdReport"] = dsReport.Tables[0];
                            gvProductionViewReport.DataSource = dsReport.Tables[0];
                            gvProductionViewReport.DataBind();
                            lblProductionViewRecords.Text = "Production View Records [" + dsReport.Tables[0].Rows.Count + "]";
                        }
                        else
                        {
                            Session["dtProdReport"] = null;
                            gvProductionViewReport.DataBind();
                            lblProductionViewRecords.Text = "Production View Records [0]";
                        }
                    }
                }

                else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                {
                    if (dsReport.Tables.Count > 0)
                    {
                        if (dsReport.Tables[0].Rows.Count > 0)
                        {
                            Session["dtDesignReport"] = dsReport.Tables[0];
                            gvDesignViewReport.DataSource = dsReport.Tables[0];
                            gvDesignViewReport.DataBind();
                            lblDesignViewRecords.Text = "Design View Records [" + dsReport.Tables[0].Rows.Count + "]";
                        }
                        else
                        {
                            Session["dtDesignReport"] = null;
                            gvDesignViewReport.DataBind();
                            lblDesignViewRecords.Text = "Design View Records [0]";
                        }
                    }
                }
            }
            else
            {
                if (dsReport.Tables.Count > 0)
                {
                    for (int i = 0; i < dsReport.Tables.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (dsReport.Tables[i].Rows.Count > 0)
                            {
                                Session["dtProcReport"] = dsReport.Tables[i];
                                gvProcurementViewReport.DataSource = dsReport.Tables[i];
                                gvProcurementViewReport.DataBind();
                                lblProcurementViewRecords.Text = "Procurement View Records [" + dsReport.Tables[i].Rows.Count + "]";
                            }
                            else
                            {
                                Session["dtProcReport"] = null;
                                gvProcurementViewReport.DataBind();
                                lblProcurementViewRecords.Text = "Procurement View Records [0]";
                            }
                        }

                        if (i == 1)
                        {
                            if (dsReport.Tables[i].Rows.Count > 0)
                            {
                                Session["dtProdReport"] = dsReport.Tables[i];
                                gvProductionViewReport.DataSource = dsReport.Tables[i];
                                gvProductionViewReport.DataBind();
                                lblProductionViewRecords.Text = "Production View Records [" + dsReport.Tables[i].Rows.Count + "]";
                            }
                            else
                            {
                                Session["dtProdReport"] = null;
                                gvProductionViewReport.DataBind();
                                lblProductionViewRecords.Text = "Production View Records [0]";
                            }
                        }

                        if (i == 2)
                        {
                            if (dsReport.Tables[i].Rows.Count > 0)
                            {
                                Session["dtDesignReport"] = dsReport.Tables[i];
                                gvDesignViewReport.DataSource = dsReport.Tables[i];
                                gvDesignViewReport.DataBind();
                                lblDesignViewRecords.Text = "Design View Records [" + dsReport.Tables[i].Rows.Count + "]";
                            }
                            else
                            {
                                Session["dtDesignReport"] = null;
                                gvDesignViewReport.DataBind();
                                lblDesignViewRecords.Text = "Design View Records [0]";
                            }
                        }
                    }
                }
                else
                {
                    Session["dsReport"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    public void GeneratePDF(DataTable dtProc, DataTable dtProd, DataTable dtDesign, string JOBNo)
    {
        try
        {
            string fileName = "Combined_Report_" + DateTime.Now.ToString("dd_MMM-yyyy");
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(dtProc, dtProd, dtDesign, JOBNo);

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
                    var document = new Document(PageSize.A2);
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

    public string GetPDFDetailAndReturnHTML(DataTable dtProc, DataTable dtProd, DataTable dtDesign, string JOBNo)
    {
        try
        {
            string htmlText = string.Empty;

            CombinedProcProdDesignReportHtmlForPDF objPDF = new CombinedProcProdDesignReportHtmlForPDF();

            string totalPOValueINR = string.Empty;
            string totalQuantity = string.Empty;
            string totalSpentEnggHours = string.Empty;

            int totalMins = 0;
            int totalSpentH = 0;
            int totalSpentM = 0;

            string totalSpentHours = string.Empty;
            string totalSpentMunits = string.Empty;

            if (Convert.ToDouble(hdTotalPOValueINR.Value) > 0)
                totalPOValueINR = Convert.ToString(hdTotalPOValueINR.Value);
            else totalPOValueINR = "0";

            if (Convert.ToDouble(hdTotalQuantity.Value) > 0)
                totalQuantity = Convert.ToString(hdTotalQuantity.Value);
            else totalQuantity = "0";

            totalMins = (Convert.ToInt32(hdTotalSpentHours.Value) * 60) + Convert.ToInt32(hdTotalSpentMins.Value);

            totalSpentH = (totalMins / 60);
            totalSpentM = (totalMins % 60);

            if (totalSpentH > 0)
            {
                if (totalSpentH < 10)
                    totalSpentHours = Convert.ToString("0" + totalSpentH);
                else totalSpentHours = Convert.ToString(totalSpentH);
            }
            else totalSpentHours = "00";

            if (totalSpentM > 0)
            {
                if (totalSpentM < 10)
                    totalSpentMunits = Convert.ToString("0" + totalSpentM);
                else totalSpentMunits = Convert.ToString(totalSpentM);
            }
            else totalSpentMunits = "00";


            totalSpentEnggHours = totalSpentHours + ":" + totalSpentMunits;

            if (!string.IsNullOrEmpty(Convert.ToString(hdTotalSpentHours.Value)))
                totalSpentHours = Convert.ToString(hdTotalSpentHours.Value);
            else totalSpentHours = "00:00";

            htmlText = objPDF.GetHtmlForPDF(dtProc, dtProd, dtDesign, JOBNo, totalPOValueINR, totalQuantity, totalSpentEnggHours, Convert.ToString(CombinedReportTypes.EnumType.Complete));

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



    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty; ;
    }

    #endregion

}
