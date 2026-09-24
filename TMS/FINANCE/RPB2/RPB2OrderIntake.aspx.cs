using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINANCE_RPB2_RPB2OrderIntake : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsReport = new DataSet();

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";

                hdYearMonthSearch.Value = DateTime.Now.ToString("MM/yyyy");
                hdYearMonthSearchFull.Value = DateTime.Now.ToString("dd/MM/yyyy");                
                txtYearMonthSearch.Text = Convert.ToString(hdYearMonthSearch.Value);

                hdYearMonthCompare.Value = DateTime.Now.ToString("MM/yyyy");
                hdYearMonthCompareFull.Value = DateTime.Now.ToString("dd/MM/yyyy");
                txtYearMonthCompare.Text = Convert.ToString(hdYearMonthCompare.Value);

                Session["DS_REPORT"] = null;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();

        txtYearMonthSearchToShow.Text = hdYearMonthSearch.Value;
        txtYearMonthComparedToShow.Text = hdYearMonthCompare.Value;

        GetRPB2Report();
    }

    decimal _totalOiMargin = 0;
    decimal _totalComparedOiMargin = 0;
    decimal _totalDeltaValue = 0;

    protected void gvRPB2Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblOiMargin = (Label)e.Row.FindControl("lblOiMargin");
            Label lblComparedOiMargin = (Label)e.Row.FindControl("lblComparedOiMargin");
            Label lblDeltaValue = (Label)e.Row.FindControl("lblDeltaValue");

            _totalOiMargin += Convert.ToDecimal(lblOiMargin.Text);
            _totalComparedOiMargin += Convert.ToDecimal(lblComparedOiMargin.Text);
            _totalDeltaValue += Convert.ToDecimal(lblDeltaValue.Text);

            txtTotalOIMarginToShow.Text = Convert.ToString(_totalOiMargin);
            txtTotalOIMarginComparedToShow.Text = Convert.ToString(_totalComparedOiMargin);
            txtDeltaValueToShow.Text = Convert.ToString(_totalDeltaValue);

            if (_totalDeltaValue >= 0)
                txtDeltaValueToShow.BackColor = System.Drawing.Color.LightGreen;
            else txtDeltaValueToShow.BackColor = System.Drawing.Color.LightPink;


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");

                if (Convert.ToDecimal(lblDeltaValue.Text) >= 0)
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                else
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;

            }
        }
    }

    protected void tbnPost_Click(object sender, EventArgs e)
    {
        if (gvRPB2Report.Rows.Count > 0)
        {
            PostRPB2Report();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvRPB2Report.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["DS_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetRPB2Report()
    {
        try
        {
            int monthSearch = 0;
            int yearSearch = 0;

            int monthCompare = 0;
            int yearCompare = 0;

            string yearMonthSearch = "";
            int unitId = 0;
            int revenueTypeId = 0;
            int companyTypeId = 0;
            string bu = "";
            string orderNumber = "";
            string yearMonthCompare = "";

            monthSearch = Convert.ToInt32(Convert.ToDateTime(hdYearMonthSearch.Value).ToString("MM"));
            yearSearch = Convert.ToInt32(Convert.ToDateTime(hdYearMonthSearch.Value).ToString("yyyy"));

            monthCompare = Convert.ToInt32(Convert.ToDateTime(hdYearMonthCompare.Value).ToString("MM"));
            yearCompare = Convert.ToInt32(Convert.ToDateTime(hdYearMonthCompare.Value).ToString("yyyy"));

            if (monthSearch < 10)
                yearMonthSearch = yearSearch.ToString() + "-0" + monthSearch.ToString();
            else
                yearMonthSearch = yearSearch.ToString() + "-" + monthSearch.ToString();




            if (ddlUnit.SelectedIndex > 0)
                unitId = Convert.ToInt32(ddlUnit.SelectedValue);

            if (ddlRevenueType.SelectedIndex > 0)
                revenueTypeId = Convert.ToInt32(ddlRevenueType.SelectedValue);

            if (ddlCompanyType.SelectedIndex > 0)
                companyTypeId = Convert.ToInt32(ddlCompanyType.SelectedValue);

            if (!string.IsNullOrEmpty(txtBU.Text))
                bu = txtBU.Text.Trim();


            if (!string.IsNullOrEmpty(txtJobNo.Text))
                orderNumber = txtJobNo.Text.ToUpper();

            if (monthCompare < 10)
                yearMonthCompare = yearCompare.ToString() + "-0" + monthCompare.ToString();
            else
                yearMonthCompare = yearCompare.ToString() + "-" + monthCompare.ToString();


            dsReport = objReports.GetRPB2SalesorderIntakeReport
                         (yearMonthSearch
                        , unitId
                        , revenueTypeId
                        , companyTypeId
                        , bu
                        , orderNumber
                        , yearMonthCompare);

            if (dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
            {
                Session["DS_REPORT"] = dsReport;
                gvRPB2Report.DataSource = dsReport.Tables[0];
                gvRPB2Report.DataBind();
            }
            else
            {
                Session["DS_REPORT"] = null;
                gvRPB2Report.DataSource = null;
                gvRPB2Report.DataBind();
            }


            lblRecords.Text = "Records[" + gvRPB2Report.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostRPB2Report()
    {
        DataTable dtRpb2 = new DataTable();
        if (dtRpb2.Columns.Count == 0)
        {
            dtRpb2.Columns.Add("UNIT_FID", typeof(int));
            dtRpb2.Columns.Add("YEAR_MONTH", typeof(string));
            dtRpb2.Columns.Add("REVENUE_TYPE_FID", typeof(int));
            dtRpb2.Columns.Add("BU", typeof(string));
            dtRpb2.Columns.Add("COMPANY_TYPE_FID", typeof(int));
            dtRpb2.Columns.Add("IC_CODE", typeof(string));
            dtRpb2.Columns.Add("ENTRY_TYPE_FID", typeof(int));
            dtRpb2.Columns.Add("ORDER_NO", typeof(string));
            dtRpb2.Columns.Add("CURRENCY_FID", typeof(int));
            dtRpb2.Columns.Add("RATE", typeof(decimal));
            dtRpb2.Columns.Add("FC_ORDER_VALUE", typeof(decimal));
            dtRpb2.Columns.Add("INR_ORDER_VALUE", typeof(decimal));
            dtRpb2.Columns.Add("OI_MARGIN", typeof(decimal));
            dtRpb2.Columns.Add("COMPARED_YEAR_MONTH", typeof(string));
            dtRpb2.Columns.Add("COMPARED_OI_MARGIN", typeof(decimal));
            dtRpb2.Columns.Add("DELTA_VALUE", typeof(decimal));
        }

        foreach (GridViewRow gr in gvRPB2Report.Rows)
        {
            Label lblUnitFid = gr.FindControl("lblUnitFid") as Label;
            Label lblYearMonth = gr.FindControl("lblYearMonth") as Label;
            Label lblRevenueTypeFid = gr.FindControl("lblRevenueTypeFid") as Label;
            Label lblBu = gr.FindControl("lblBu") as Label;
            Label lblCompanyTypeFid = gr.FindControl("lblCompanyTypeFid") as Label;
            Label lblIcCode = gr.FindControl("lblIcCode") as Label;
            Label lblEntryTypeFid = gr.FindControl("lblEntryTypeFid") as Label;
            Label lblOrderNo = gr.FindControl("lblOrderNo") as Label;
            Label lblCurrencyFid = gr.FindControl("lblCurrencyFid") as Label;
            Label lblRate = gr.FindControl("lblRate") as Label;
            Label lblFcOrderValue = gr.FindControl("lblFcOrderValue") as Label;
            Label lblInrOrderValue = gr.FindControl("lblInrOrderValue") as Label;
            Label lblOiMargin = gr.FindControl("lblOiMargin") as Label;
            Label lblComparedYearMonth = gr.FindControl("lblComparedYearMonth") as Label;
            Label lblComparedOiMargin = gr.FindControl("lblComparedOiMargin") as Label;
            Label lblDeltaValue = gr.FindControl("lblDeltaValue") as Label;

            DataRow drn = dtRpb2.NewRow();

            drn["UNIT_FID"] = 0;
            drn["YEAR_MONTH"] = "";
            drn["REVENUE_TYPE_FID"] = 0;
            drn["BU"] = "";
            drn["COMPANY_TYPE_FID"] = 0;
            drn["IC_CODE"] = "";
            drn["ENTRY_TYPE_FID"] = 0;
            drn["ORDER_NO"] = "";
            drn["CURRENCY_FID"] = 0;
            drn["RATE"] = 0;
            drn["FC_ORDER_VALUE"] = 0;
            drn["INR_ORDER_VALUE"] = 0;
            drn["OI_MARGIN"] = 0;
            drn["COMPARED_YEAR_MONTH"] = "";
            drn["COMPARED_OI_MARGIN"] = 0;
            drn["DELTA_VALUE"] = 0;


            if (!string.IsNullOrEmpty(lblUnitFid.Text) && Convert.ToInt32(lblUnitFid.Text) > 0)
                drn["UNIT_FID"] = Convert.ToInt32(lblUnitFid.Text);

            if (!string.IsNullOrEmpty(lblYearMonth.Text))
                drn["YEAR_MONTH"] = Convert.ToString(lblYearMonth.Text);

            if (!string.IsNullOrEmpty(lblRevenueTypeFid.Text) && Convert.ToInt32(lblRevenueTypeFid.Text) > 0)
                drn["REVENUE_TYPE_FID"] = Convert.ToInt32(lblRevenueTypeFid.Text);

            if (!string.IsNullOrEmpty(lblBu.Text))
                drn["BU"] = Convert.ToString(lblBu.Text);

            if (!string.IsNullOrEmpty(lblCompanyTypeFid.Text) && Convert.ToInt32(lblCompanyTypeFid.Text) > 0)
                drn["COMPANY_TYPE_FID"] = Convert.ToInt32(lblCompanyTypeFid.Text);

            if (!string.IsNullOrEmpty(lblIcCode.Text))
                drn["IC_CODE"] = Convert.ToString(lblIcCode.Text);

            if (!string.IsNullOrEmpty(lblEntryTypeFid.Text) && Convert.ToInt32(lblEntryTypeFid.Text) > 0)
                drn["ENTRY_TYPE_FID"] = Convert.ToInt32(lblEntryTypeFid.Text);

            if (!string.IsNullOrEmpty(lblOrderNo.Text))
                drn["ORDER_NO"] = Convert.ToString(lblOrderNo.Text).ToUpper().Trim();

            if (!string.IsNullOrEmpty(lblCurrencyFid.Text) && Convert.ToInt32(lblCurrencyFid.Text) > 0)
                drn["CURRENCY_FID"] = Convert.ToInt32(lblCurrencyFid.Text);

            if (!string.IsNullOrEmpty(lblRate.Text) && Convert.ToDecimal(lblRate.Text) > 0)
                drn["RATE"] = Convert.ToDecimal(lblRate.Text);

            if (!string.IsNullOrEmpty(lblFcOrderValue.Text) && Convert.ToDecimal(lblFcOrderValue.Text) > 0)
                drn["FC_ORDER_VALUE"] = Convert.ToDecimal(lblFcOrderValue.Text);

            if (!string.IsNullOrEmpty(lblInrOrderValue.Text) && Convert.ToDecimal(lblInrOrderValue.Text) > 0)
                drn["INR_ORDER_VALUE"] = Convert.ToDecimal(lblInrOrderValue.Text);

            if (!string.IsNullOrEmpty(lblOiMargin.Text) && Convert.ToDecimal(lblOiMargin.Text) > 0)
                drn["OI_MARGIN"] = Convert.ToDecimal(lblOiMargin.Text);

            if (!string.IsNullOrEmpty(lblComparedYearMonth.Text))
                drn["COMPARED_YEAR_MONTH"] = Convert.ToString(lblComparedYearMonth.Text);

            if (!string.IsNullOrEmpty(lblComparedOiMargin.Text) && Convert.ToDecimal(lblComparedOiMargin.Text) > 0)
                drn["COMPARED_OI_MARGIN"] = Convert.ToDecimal(lblComparedOiMargin.Text);

            if (!string.IsNullOrEmpty(lblDeltaValue.Text) && Convert.ToDecimal(lblDeltaValue.Text) > 0)
                drn["DELTA_VALUE"] = Convert.ToDecimal(lblDeltaValue.Text);

            dtRpb2.Rows.Add(drn);

        }

        if (dtRpb2.Rows.Count > 0)
        {
            int monthSearch = 0;
            int yearSearch = 0;

            int monthCompare = 0;
            int yearCompare = 0;

            string yearMonthSearch = "";
            string yearMonthCompare = "";

            monthSearch = Convert.ToInt32(Convert.ToDateTime(hdYearMonthSearch.Value).ToString("MM"));
            yearSearch = Convert.ToInt32(Convert.ToDateTime(hdYearMonthSearch.Value).ToString("yyyy"));

            monthCompare = Convert.ToInt32(Convert.ToDateTime(hdYearMonthCompare.Value).ToString("MM"));
            yearCompare = Convert.ToInt32(Convert.ToDateTime(hdYearMonthCompare.Value).ToString("yyyy"));

            if (monthSearch < 10)
                yearMonthSearch = yearSearch.ToString() + "-0" + monthSearch.ToString();
            else
                yearMonthSearch = yearSearch.ToString() + "-" + monthSearch.ToString();

            if (monthCompare < 10)
                yearMonthCompare = yearCompare.ToString() + "-0" + monthCompare.ToString();
            else
                yearMonthCompare = yearCompare.ToString() + "-" + monthCompare.ToString();

            int value = objReports.PostRpb2Report(yearMonthSearch, yearMonthCompare, dtRpb2, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                GetRPB2Report();
                SuccessMessage("Rpb2 Report posted successfully.");
                return;
            }
        }
    }

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 7; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 7; j < dt.Columns.Count; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "RPB2_Repprt_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}