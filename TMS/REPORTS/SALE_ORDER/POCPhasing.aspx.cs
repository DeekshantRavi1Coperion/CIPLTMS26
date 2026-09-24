using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class REPORTS_SALE_ORDER_POCPhasing : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsTempTableDetails = new DataSet();
    DataSet dsPOCList = new DataSet();
    DataSet dsCurrency = new DataSet();
    DataSet dsDBDetails = new DataSet();

    string jobNo = string.Empty;
    string customerName = string.Empty;
    string customerType = string.Empty;
    string bu = string.Empty;
    string currency = string.Empty;


    string fromDate = string.Empty;
    string toDate = string.Empty;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                Session["POC_LIST"] = null;
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                BindCurrency();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_LIST"] = null;
        gvPOCList.DataSource = null;
        gvPOCList.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["POC_LIST"] = null;
        gvPOCList.DataSource = null;
        gvPOCList.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetPOCList();
    }

    protected void gvPOCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
        int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));

        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < 12; i++)
            {
                if (i > 0)
                    month = month + 1;

                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }

                if (i == 0)
                    e.Row.Cells[24].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 1)
                    e.Row.Cells[25].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 2)
                    e.Row.Cells[26].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 3)
                    e.Row.Cells[27].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 4)
                    e.Row.Cells[28].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 5)
                    e.Row.Cells[29].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 6)
                    e.Row.Cells[30].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 7)
                    e.Row.Cells[31].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 8)
                    e.Row.Cells[32].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 9)
                    e.Row.Cells[33].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 10)
                    e.Row.Cells[34].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");

                else if (i == 11)
                    e.Row.Cells[35].Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MMM");
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
            Label lblPostingMonth = (Label)e.Row.FindControl("lblPostingMonth");
            Button btnPost = (Button)e.Row.FindControl("btnPost");

            lblPostingMonth.Text = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");

            if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)))
                btnPost.Text = "Update";

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");

                if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)))
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
            }
        }
    }

    protected void gvPOCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
            int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));

            int value = 0;
            int recordID = 0;
            string JOBNo = string.Empty;
            string customerCode = string.Empty;
            string customerName = string.Empty;
            string customerType = string.Empty;
            string DOCClass = string.Empty;
            string BU = string.Empty;
            string orderCurrency = string.Empty;
            double orderCurrRate = 0;
            double orderAmtFC = 0;
            double INVAmtFC = 0;
            double deltaFC = 0;
            double orderAmtINR = 0;
            double INVAmtINR = 0;
            double deltaINR = 0;
            double currentFCRate = 0;
            double orderAmtINROnCurrentRate = 0;
            double INVAmtINROnCurrentRate = 0;
            double deltaOnCurrRate = 0;
            double POCBillingAmt = 0;
            double TAXBillingAmt = 0;
            double finalBacklog = 0;
            double adjustment = 0;
            string expectedDate = string.Empty;
            double monthFirst = 0;
            double monthSecond = 0;
            double monthThird = 0;
            double monthFourth = 0;
            double monthFifth = 0;
            double monthSixth = 0;
            double monthSeventh = 0;
            double monthEighth = 0;
            double monthNinth = 0;
            double monthTenth = 0;
            double monthEleventh = 0;
            double monthTwelfth = 0;
            double nextYears = 0;
            string postingMonth = string.Empty;
            double totalPhasedValue = 0;

            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                Label lblRecordID = gvPOCList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblJOBNo = gvPOCList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblCustomerCode = gvPOCList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvPOCList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblCustomerType = gvPOCList.Rows[rowindex].FindControl("lblCustomerType") as Label;
                Label lblDOCClass = gvPOCList.Rows[rowindex].FindControl("lblDOCClass") as Label;
                Label lblBU = gvPOCList.Rows[rowindex].FindControl("lblBU") as Label;
                Label lblOrderCurrency = gvPOCList.Rows[rowindex].FindControl("lblOrderCurrency") as Label;
                Label lblOrderCurrRate = gvPOCList.Rows[rowindex].FindControl("lblOrderCurrRate") as Label;
                Label lblOrderAmtFC = gvPOCList.Rows[rowindex].FindControl("lblOrderAmtFC") as Label;
                Label lblINVAmtFC = gvPOCList.Rows[rowindex].FindControl("lblINVAmtFC") as Label;
                Label lblDeltaFC = gvPOCList.Rows[rowindex].FindControl("lblDeltaFC") as Label;
                Label lblOrderAmtINR = gvPOCList.Rows[rowindex].FindControl("lblOrderAmtINR") as Label;
                Label lblINVAmtINR = gvPOCList.Rows[rowindex].FindControl("lblINVAmtINR") as Label;
                Label lblDeltaINR = gvPOCList.Rows[rowindex].FindControl("lblDeltaINR") as Label;
                Label lblCurrentFCRate = gvPOCList.Rows[rowindex].FindControl("lblCurrentFCRate") as Label;
                Label lblOrderAmtINROnCurrentRate = gvPOCList.Rows[rowindex].FindControl("lblOrderAmtINROnCurrentRate") as Label;
                Label lblINVAmtINROnCurrentRate = gvPOCList.Rows[rowindex].FindControl("lblINVAmtINROnCurrentRate") as Label;
                Label lblDeltaOnCurrRate = gvPOCList.Rows[rowindex].FindControl("lblDeltaOnCurrRate") as Label;
                Label lblPOCBillingAmt = gvPOCList.Rows[rowindex].FindControl("lblPOCBillingAmt") as Label;
                Label lblTAXBillingAmt = gvPOCList.Rows[rowindex].FindControl("lblTAXBillingAmt") as Label;
                Label lblFinalBacklog = gvPOCList.Rows[rowindex].FindControl("lblFinalBacklog") as Label;
                Label lblAdjustment = gvPOCList.Rows[rowindex].FindControl("lblAdjustment") as Label;
                Label lblExpectedDate = gvPOCList.Rows[rowindex].FindControl("lblExpectedDate") as Label;
                Label lblPostingMonth = gvPOCList.Rows[rowindex].FindControl("lblPostingMonth") as Label;

                TextBox txtMonthFirst = gvPOCList.Rows[rowindex].FindControl("txtMonthFirst") as TextBox;
                TextBox txtMonthSecond = gvPOCList.Rows[rowindex].FindControl("txtMonthSecond") as TextBox;
                TextBox txtMonthThird = gvPOCList.Rows[rowindex].FindControl("txtMonthThird") as TextBox;
                TextBox txtMonthFourth = gvPOCList.Rows[rowindex].FindControl("txtMonthFourth") as TextBox;
                TextBox txtMonthFifth = gvPOCList.Rows[rowindex].FindControl("txtMonthFifth") as TextBox;
                TextBox txtMonthSixth = gvPOCList.Rows[rowindex].FindControl("txtMonthSixth") as TextBox;
                TextBox txtMonthSeventh = gvPOCList.Rows[rowindex].FindControl("txtMonthSeventh") as TextBox;
                TextBox txtMonthEighth = gvPOCList.Rows[rowindex].FindControl("txtMonthEighth") as TextBox;
                TextBox txtMonthNinth = gvPOCList.Rows[rowindex].FindControl("txtMonthNinth") as TextBox;
                TextBox txtMonthTenth = gvPOCList.Rows[rowindex].FindControl("txtMonthTenth") as TextBox;
                TextBox txtMonthEleventh = gvPOCList.Rows[rowindex].FindControl("txtMonthEleventh") as TextBox;
                TextBox txtMonthTwelfth = gvPOCList.Rows[rowindex].FindControl("txtMonthTwelfth") as TextBox;
                TextBox txtNextYears = gvPOCList.Rows[rowindex].FindControl("txtNextYears") as TextBox;


                if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)))
                    recordID = Convert.ToInt32(lblRecordID.Text);
                else
                    recordID = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text)))
                    JOBNo = Convert.ToString(lblJOBNo.Text);
                else
                    JOBNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerCode.Text)))
                    customerCode = Convert.ToString(lblCustomerCode.Text);
                else
                    customerCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                    customerName = Convert.ToString(lblCustomerName.Text);
                else
                    customerName = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerType.Text)))
                    customerType = Convert.ToString(lblCustomerType.Text);
                else
                    customerType = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDOCClass.Text)))
                    DOCClass = Convert.ToString(lblDOCClass.Text);
                else
                    DOCClass = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBU.Text)))
                    BU = Convert.ToString(lblBU.Text);
                else
                    BU = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderCurrency.Text)))
                    orderCurrency = Convert.ToString(lblOrderCurrency.Text);
                else
                    orderCurrency = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderCurrRate.Text)))
                    orderCurrRate = Convert.ToDouble(lblOrderCurrRate.Text);
                else
                    orderCurrRate = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmtFC.Text)))
                    orderAmtFC = Convert.ToDouble(lblOrderAmtFC.Text);
                else
                    orderAmtFC = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblINVAmtFC.Text)))
                    INVAmtFC = Convert.ToDouble(lblINVAmtFC.Text);
                else
                    INVAmtFC = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDeltaFC.Text)))
                    deltaFC = Convert.ToDouble(lblDeltaFC.Text);
                else
                    deltaFC = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmtINR.Text)))
                    orderAmtINR = Convert.ToDouble(lblOrderAmtINR.Text);
                else
                    orderAmtINR = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblINVAmtINR.Text)))
                    INVAmtINR = Convert.ToDouble(lblINVAmtINR.Text);
                else
                    INVAmtINR = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDeltaINR.Text)))
                    deltaINR = Convert.ToDouble(lblDeltaINR.Text);
                else
                    deltaINR = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCurrentFCRate.Text)))
                    currentFCRate = Convert.ToDouble(lblCurrentFCRate.Text);
                else
                    currentFCRate = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblOrderAmtINROnCurrentRate.Text)))
                    orderAmtINROnCurrentRate = Convert.ToDouble(lblOrderAmtINROnCurrentRate.Text);
                else
                    orderAmtINROnCurrentRate = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblINVAmtINROnCurrentRate.Text)))
                    INVAmtINROnCurrentRate = Convert.ToDouble(lblINVAmtINROnCurrentRate.Text);
                else
                    INVAmtINROnCurrentRate = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDeltaOnCurrRate.Text)))
                    deltaOnCurrRate = Convert.ToDouble(lblDeltaOnCurrRate.Text);
                else
                    deltaOnCurrRate = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPOCBillingAmt.Text)))
                    POCBillingAmt = Convert.ToDouble(lblPOCBillingAmt.Text);
                else
                    POCBillingAmt = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblTAXBillingAmt.Text)))
                    TAXBillingAmt = Convert.ToDouble(lblTAXBillingAmt.Text);
                else
                    TAXBillingAmt = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblFinalBacklog.Text)))
                    finalBacklog = Convert.ToDouble(lblFinalBacklog.Text);
                else
                    finalBacklog = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblAdjustment.Text)))
                    adjustment = Convert.ToDouble(lblAdjustment.Text);
                else
                    adjustment = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblExpectedDate.Text)))
                    expectedDate = Convert.ToDateTime(lblExpectedDate.Text).ToString("yyyy-MM-dd");
                else
                    expectedDate = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthFirst.Text)))
                    monthFirst = Convert.ToDouble(txtMonthFirst.Text);
                else
                    monthFirst = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthSecond.Text)))
                    monthSecond = Convert.ToDouble(txtMonthSecond.Text);
                else
                    monthSecond = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthThird.Text)))
                    monthThird = Convert.ToDouble(txtMonthThird.Text);
                else
                    monthThird = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthFourth.Text)))
                    monthFourth = Convert.ToDouble(txtMonthFourth.Text);
                else
                    monthFourth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthFifth.Text)))
                    monthFifth = Convert.ToDouble(txtMonthFifth.Text);
                else
                    monthFifth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthSixth.Text)))
                    monthSixth = Convert.ToDouble(txtMonthSixth.Text);
                else
                    monthSixth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthSeventh.Text)))
                    monthSeventh = Convert.ToDouble(txtMonthSeventh.Text);
                else
                    monthSeventh = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthEighth.Text)))
                    monthEighth = Convert.ToDouble(txtMonthEighth.Text);
                else
                    monthEighth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthNinth.Text)))
                    monthNinth = Convert.ToDouble(txtMonthNinth.Text);
                else
                    monthNinth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthTenth.Text)))
                    monthTenth = Convert.ToDouble(txtMonthTenth.Text);
                else
                    monthTenth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthEleventh.Text)))
                    monthEleventh = Convert.ToDouble(txtMonthEleventh.Text);
                else
                    monthEleventh = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtMonthTwelfth.Text)))
                    monthTwelfth = Convert.ToDouble(txtMonthTwelfth.Text);
                else
                    monthTwelfth = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtNextYears.Text)))
                    nextYears = Convert.ToDouble(txtNextYears.Text);
                else
                    nextYears = 0;

                postingMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");


                totalPhasedValue = monthFirst + monthSecond + monthThird + monthFourth + monthFifth + monthSixth + monthSeventh +
                                   monthEighth + monthNinth + monthTenth + monthEleventh + monthTwelfth + nextYears;


                if (finalBacklog - totalPhasedValue == 0)
                {
                    value = objReports.InsertUpdateNewPOCPhasing(recordID, JOBNo, customerCode, customerName, customerType, DOCClass, BU, orderCurrency,
                                                        orderCurrRate, orderAmtFC, INVAmtFC, deltaFC, orderAmtINR, INVAmtINR, deltaINR,
                                                        currentFCRate, orderAmtINROnCurrentRate, INVAmtINROnCurrentRate, deltaOnCurrRate,
                                                        POCBillingAmt, TAXBillingAmt, finalBacklog, adjustment, expectedDate,
                                                        monthFirst, monthSecond, monthThird, monthFourth, monthFifth, monthSixth,
                                                        monthSeventh, monthEighth, monthNinth, monthTenth, monthEleventh, monthTwelfth,
                                                        nextYears, postingMonth, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (value > 0)
                    {

                        if (recordID > 0)
                            SuccessMessage(JOBNo + " Updated successfully");
                        else
                            SuccessMessage(JOBNo + " Phased successfully");

                        GetPOCList();
                    }
                    else
                    {
                        ExceptionMessage("Please try again!");
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please phase remaining amount of :" + Convert.ToString(finalBacklog - totalPhasedValue) + " /-...!!");
                    return;
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
        if (gvPOCList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["POC_LIST"];
            ToCSVNew01(ds.Tables[1]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindCurrency()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }

            dsCurrency = objReports.GetFactCurrencyList(dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlCurrency.DataSource = dsCurrency.Tables[0];
                ddlCurrency.DataTextField = "CODE";
                ddlCurrency.DataValueField = "CODE";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, "All");
                ddlCurrency.SelectedIndex = 0;
            }
            else
            {
                ddlCurrency.Items.Clear();
                ddlCurrency.Items.Insert(0, "All");
                ddlCurrency.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPOCList()
    {
        try
        {
            string month1 = string.Empty;
            string month2 = string.Empty;
            string month3 = string.Empty;
            string month4 = string.Empty;
            string month5 = string.Empty;
            string month6 = string.Empty;
            string month7 = string.Empty;
            string month8 = string.Empty;
            string month9 = string.Empty;
            string month10 = string.Empty;
            string month11 = string.Empty;
            string month12 = string.Empty;

            int month = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("MM"));
            int year = Convert.ToInt32(Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy"));

            for (int i = 0; i < 12; i++)
            {
                if (i > 0)
                    month = month + 1;

                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }

                if (i == 0)
                    month1 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 1)
                    month2 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 2)
                    month3 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 3)
                    month4 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 4)
                    month5 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 5)
                    month6 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 6)
                    month7 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 7)
                    month8 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 8)
                    month9 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 9)
                    month10 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 10)
                    month11 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
                else if (i == 11)
                    month12 = Convert.ToDateTime(year + "-" + month).ToString("yyyy-MM");
            }

            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.ToUpper();
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (ddlCustomerType.SelectedIndex > 0)
                customerType = ddlCustomerType.SelectedItem.Text;
            else
                customerType = string.Empty;

            if (!string.IsNullOrEmpty(txtBU.Text))
                bu = txtBU.Text;
            else
                bu = string.Empty;

            if (ddlCurrency.SelectedIndex > 0)
                currency = Convert.ToString(ddlCurrency.SelectedValue);
            else
                currency = string.Empty;

            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }
            dsPOCList = objReports.GetPOCList(month1, month2, month3, month4, month5, month6,
                                                                                month7, month8, month9, month10, month11, month12,
                                                                                jobNo, customerName, customerType, bu, currency,
                                                                                dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);

            if (dsPOCList.Tables.Count > 0)
            {
                if (dsPOCList.Tables[0].Rows.Count > 0)
                {
                    if (dsPOCList.Tables[1].Rows.Count > 0)
                    {
                        dsPOCList.Tables[1].Columns.Remove("OM");
                        Session["POC_LIST"] = dsPOCList;
                        gvPOCList.DataSource = dsPOCList.Tables[1];
                        gvPOCList.DataBind();
                    }
                    else
                    {
                        Session["POC_LIST"] = null;
                        gvPOCList.DataSource = null;
                        gvPOCList.DataBind();
                    }
                }
                else
                {
                    Session["POC_LIST"] = null;
                    gvPOCList.DataSource = null;
                    gvPOCList.DataBind();

                    ExceptionMessage("Please enter current FX rate of selected month..!");
                    return;
                }
            }
            else
            {
                Session["POC_LIST"] = null;
                gvPOCList.DataSource = null;
                gvPOCList.DataBind();
            }


            lblRecords.Text = "Records[" + gvPOCList.Rows.Count + "]";
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

            string fileName = "SaleOrderOutstandingReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void EmptyGridview()
    {
        Session["POC_LIST"] = null;
        gvPOCList.DataSource = null;
        gvPOCList.DataBind();
        lblRecords.Text = "Records[" + gvPOCList.Rows.Count + "]";
    }

    #endregion

}