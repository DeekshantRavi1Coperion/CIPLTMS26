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

public partial class REPORTS_PURCHASE_ORDER_POReportCostControl : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsPOReport = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;

    string poNo = string.Empty;
    string vendorName = string.Empty;
    string vendorCode = string.Empty;
    string JOBNo = string.Empty;
    double amount1 = 0;
    double amount2 = 0;
    string status = string.Empty;
    string unitName = string.Empty;
    string sign = string.Empty;
    int excludeCIDF = 0;

    double totalQty = 0;
    double totalCostsCalculated = 0;
    double totalCostsCommitted = 0;
    double totalCostsDeviation = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["PO_REPORT"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindUnit();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {        
        txtTotalCostsCalculated.Text = "0";
        txtTotalCostsCommitted.Text = "0";
        txtTotalCostsDeviation.Text = "0";

        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPOReportSummery();

        if (ddlSign.SelectedIndex == 5)
        {
            txtAmountTwo.Enabled = true;
        }
        else
        {
            txtAmountTwo.Text = string.Empty;
            txtAmountTwo.Enabled = false;
        }
    }

    //protected void gvPOReportSummery_PreRender(object sender, EventArgs e)
    //{
    //    GridViewRow LastRow = gvPOReportSummery.Rows[gvPOReportSummery.Rows.Count - 1];

    //    LastRow.BackColor = System.Drawing.Color.Gainsboro;
    //    LastRow.Font.Bold = true;
    //    //LastRow.Font.Italic = true;
    //}


    

    protected void gvPOReportSummery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               

                Label lblSrNo = (Label)e.Row.FindControl("lblSrNo"); 
                Label lblIsTotal = (Label)e.Row.FindControl("lblIsTotal");

                Label lblQty = (Label)e.Row.FindControl("lblQty");
                Label lblCostsCalculated = (Label)e.Row.FindControl("lblCostsCalculated");
                Label lblCostsCommitted = (Label)e.Row.FindControl("lblCostsCommitted");
                Label lblCostsDeviation = (Label)e.Row.FindControl("lblCostsDeviation");


                
                TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
                TextBox txtCostsCalculated = (TextBox)e.Row.FindControl("txtCostsCalculated");
                TextBox txtCostsCommitted = (TextBox)e.Row.FindControl("txtCostsCommitted");
                TextBox txtCostDeviationCommittedVersusCalculated = (TextBox)e.Row.FindControl("txtCostDeviationCommittedVersusCalculated");

                if (Convert.ToInt32(lblIsTotal.Text) > 0)
                {
                    totalQty = Convert.ToDouble(lblQty.Text);
                    totalQty = Math.Round(totalQty, 2);
                    txtTotalQty.Text = Convert.ToString(totalQty);
                    txtTotalQty.ForeColor = System.Drawing.Color.Green;

                    totalCostsCalculated = Convert.ToDouble(lblCostsCalculated.Text);
                    totalCostsCalculated = Math.Round(totalCostsCalculated, 2);
                    txtTotalCostsCalculated.Text = Convert.ToString(totalCostsCalculated);
                    txtTotalCostsCalculated.ForeColor = System.Drawing.Color.Green;

                    totalCostsCommitted = Convert.ToDouble(lblCostsCommitted.Text);
                    totalCostsCommitted = Math.Round(totalCostsCommitted, 2);
                    txtTotalCostsCommitted.Text = Convert.ToString(totalCostsCommitted);
                    txtTotalCostsCommitted.ForeColor = System.Drawing.Color.Green;


                    totalCostsDeviation = Convert.ToDouble(lblCostsDeviation.Text);
                    totalCostsDeviation = Math.Round(totalCostsDeviation, 2);
                    txtTotalCostsDeviation.Text = Convert.ToString(totalCostsDeviation);
                    txtTotalCostsDeviation.ForeColor = System.Drawing.Color.Green;
                }


                if (Convert.ToInt32(lblIsTotal.Text)>0)
                {
                    lblSrNo.Font.Bold = true;
                    txtQty.Font.Bold = true;
                    txtCostsCalculated.Font.Bold = true;
                    txtCostsCommitted.Font.Bold = true;
                    txtCostDeviationCommittedVersusCalculated.Font.Bold = true;
                }
            }






            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvPOReportSummery_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                    Label lblPONo = gvPOReportSummery.Rows[rowindex].FindControl("lblPONo") as Label;
                    Label lblDocClass = gvPOReportSummery.Rows[rowindex].FindControl("lblDocClass") as Label;
                    Label lblLocation = gvPOReportSummery.Rows[rowindex].FindControl("lblLocation") as Label;

                    ModalPopupExtender1.Show();
                    iframePOHeaderDetail.Attributes.Add("src", "POHeaderDetail.aspx?pono=" + Convert.ToString(lblPONo.Text) + "&docclass=" + Convert.ToString(lblDocClass.Text) + "&unit=" + Convert.ToString(lblLocation.Text));
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPOReportSummery.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["PO_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPOReportSummery()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text.ToUpper();
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = txtVendorName.Text;
            else
                vendorName = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorCode.Text))
                vendorCode = txtVendorCode.Text;
            else
                vendorCode = string.Empty;

            //if (!string.IsNullOrEmpty(txtJOBNo.Text))
            //    JOBNo = txtJOBNo.Text.ToUpper();
            //else
            //    JOBNo = string.Empty;


            //DOC_CLASS LIKE '%OS18001%' OR
            if (!string.IsNullOrEmpty(txtJOBNo.Text))
            {
                string[] jobNoTxt = txtJOBNo.Text.Replace(Environment.NewLine, "").TrimEnd(',').Split(',');
                foreach (string item in jobNoTxt)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        JOBNo += "LTRIM(RTRIM(PD.CLASS)) LIKE '%" + item + "%' OR ";
                    }
                }
                JOBNo = JOBNo.TrimEnd(' ');
                JOBNo = JOBNo.TrimEnd('R');
                JOBNo = JOBNo.TrimEnd('O');
            }
            else JOBNo = string.Empty;



            if (!string.IsNullOrEmpty(txtAmountOne.Text))
                amount1 = Convert.ToDouble(txtAmountOne.Text);
            else
                amount2 = 0;

            if (!string.IsNullOrEmpty(txtAmountTwo.Text))
                amount2 = Convert.ToDouble(txtAmountTwo.Text);
            else
                amount2 = 0;

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedItem.Text);
            else
                status = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            sign = Convert.ToString(ddlSign.SelectedItem.Text);

            if (chkExcludeCIDF.Checked)
                excludeCIDF = 1;
            else
                excludeCIDF = 0;

            dsPOReport = objReports.GetPOReportCostControl(fromDate, toDate, poNo, vendorName, vendorCode, unitName, status
                                                         , JOBNo, amount1, amount2, sign, excludeCIDF);

            if (dsPOReport.Tables.Count > 0 && dsPOReport.Tables[0].Rows.Count > 0)
            {                
                double totalQtyCSV = 0;
                double totalCostsCalculatedCSV = 0;
                double totalCostsCommittedCSV = 0;
                double totalCostsDeviationCSV = 0;


                foreach (DataRow dr in dsPOReport.Tables[0].Rows)
                {
                    totalQtyCSV += Convert.ToDouble(dr["Qty"]);
                    totalCostsCalculatedCSV += Convert.ToDouble(dr["Costs_Calculated"]);
                    totalCostsCommittedCSV += Convert.ToDouble(dr["Costs_Committed"]);
                    totalCostsDeviationCSV += Convert.ToDouble(dr["Cost_Deviation_Committed_Versus_Calculated"]);
                }

                totalQtyCSV = Math.Round(totalQtyCSV, 2);
                totalCostsCalculatedCSV = Math.Round(totalCostsCalculatedCSV, 2);
                totalCostsCommittedCSV = Math.Round(totalCostsCommittedCSV, 2);
                totalCostsDeviationCSV = Math.Round(totalCostsDeviationCSV, 2);

                foreach (DataRow dr in dsPOReport.Tables[0].Select("IsTotal=1"))
                {
                    dr["Qty"] = totalQtyCSV;
                    dr["Costs_Calculated"] = totalCostsCalculatedCSV;
                    dr["Costs_Committed"] = totalCostsCommittedCSV;
                    dr["Cost_Deviation_Committed_Versus_Calculated"] = totalCostsDeviationCSV;
                }


                Session["PO_REPORT"] = dsPOReport;
                gvPOReportSummery.DataSource = dsPOReport.Tables[0];
                gvPOReportSummery.DataBind();
            }
            else
            {
                Session["PO_REPORT"] = null;
                gvPOReportSummery.DataSource = null;
                gvPOReportSummery.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                csv += column.ColumnName + ',';
            }
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "PO_Report_Cost_Control_From_" + txtStartDateSearch.Text + "_To_" + txtEndDateSearch.Text;
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

    private void SuccessMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
