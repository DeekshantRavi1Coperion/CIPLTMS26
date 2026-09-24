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
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_ORDER_UnpostedPOListSeven : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Purchase objPurchase = new BAL.Purchase();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsJobNo = new DataSet();
    DataSet dsPivotGroup = new DataSet();
    DataSet dsPivotGroupByJob = new DataSet();
    DataSet dsPoPostingStatus = new DataSet();
    DataSet dsUnpostedPOList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsBudgetedAmt = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string jobNo = string.Empty;
    string pivotGroup = string.Empty;
    string vendorName = string.Empty;

    int unitId = 0;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;
    string unitNameA35 = string.Empty;
    string unitNameDLH = string.Empty;
    string unitNameSEZ = string.Empty;
    string unitNameGNU = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                Session["dtUnpostedPOList"] = null;
                Session["dtPivotGroup"] = null;
                Session["dtPoPostingStatus"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                dsPoPostingStatus = objProject.GetDetailsBySP("sp_get_po_posting_status");
                Session["dtPoPostingStatus"] = dsPoPostingStatus.Tables[0];
                BindUnit();
                BindJobNo();

                ddlPivotGroup.Items.Clear();
                ddlPivotGroup.Items.Insert(0, "SELECT");
                ddlPivotGroup.SelectedIndex = 0;
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
        GetUnpostedPOList();
    }

    protected void gvUnpostedPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtUnpostedPOList = new DataTable();
                DataTable dtPivotGroup = new DataTable();
                DataTable dtPoPostingStatus = new DataTable();

                Label lblPONo = (Label)e.Row.FindControl("lblPONo");

                Label lblPVBudgetedAmount = (Label)e.Row.FindControl("lblPVBudgetedAmount");
                Label lblPOAmount = (Label)e.Row.FindControl("lblPOAmount");
                Label lblPOAmountNew = (Label)e.Row.FindControl("lblPOAmountNew");
                TextBox txtPOBudgetedAmount = (TextBox)e.Row.FindControl("txtPOBudgetedAmount");

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                DropDownList ddlPivotGroup = (DropDownList)e.Row.FindControl("ddlPivotGroup");
                DropDownList ddlIsSubjectToVPOC = (DropDownList)e.Row.FindControl("ddlIsSubjectToVPOC");
                DropDownList ddlIsBillable = (DropDownList)e.Row.FindControl("ddlIsBillable");
                Label lblPostingMonth = (Label)e.Row.FindControl("lblPostingMonth");
                Button btnPost = (Button)e.Row.FindControl("btnPost");

                if (Session["dtUnpostedPOList"] != null)
                    dtUnpostedPOList = (DataTable)Session["dtUnpostedPOList"];
                else
                    dtUnpostedPOList = null;

                if (Session["dtPivotGroup"] != null)
                    dtPivotGroup = (DataTable)Session["dtPivotGroup"];
                else
                    dtPivotGroup = null;

                if (Session["dtPoPostingStatus"] != null)
                    dtPoPostingStatus = (DataTable)Session["dtPoPostingStatus"];
                else
                    dtPoPostingStatus = null;

                if (dtPivotGroup.Rows.Count > 0)
                {
                    ddlPivotGroup.DataSource = dtPivotGroup;
                    ddlPivotGroup.DataTextField = "PIVOT_GROUP";
                    ddlPivotGroup.DataValueField = "PIVOT_GROUP_ID";
                    ddlPivotGroup.DataBind();
                    ddlPivotGroup.Items.Insert(0, "SELECT");
                    ddlPivotGroup.SelectedIndex = 0;

                    if (dtUnpostedPOList.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUnpostedPOList.Select("PO_NO='" + Convert.ToString(lblPONo.Text) + "'"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["PIVOT_GROUP_ID"])) && Convert.ToInt32(dr["PIVOT_GROUP_ID"]) > 0 && dr["PIVOT_GROUP_ID"] != DBNull.Value)
                                ddlPivotGroup.SelectedValue = Convert.ToString(dr["PIVOT_GROUP_ID"]);
                            else
                                ddlPivotGroup.SelectedValue = "0";
                        }
                    }
                    else
                        ddlPivotGroup.SelectedValue = "0";
                }
                else
                    ddlPivotGroup.SelectedValue = "0";


                if (ddlPivotGroup.SelectedIndex > 0)
                    btnPost.Visible = true;
                else
                    btnPost.Visible = false;


                if (dtPoPostingStatus.Rows.Count > 0)
                {
                    ddlStatus.DataSource = dtPoPostingStatus;
                    ddlStatus.DataTextField = "STATUS_NAME";
                    ddlStatus.DataValueField = "STATUS_ID";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, "SELECT");
                    ddlStatus.SelectedIndex = 1;
                    ddlStatus.Enabled = false;
                }
                else
                {
                    ddlStatus.Items.Clear();
                    ddlStatus.Items.Insert(0, "SELECT");
                    ddlStatus.SelectedIndex = 0;
                }


                if (dtUnpostedPOList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUnpostedPOList.Select("PO_NO='" + Convert.ToString(lblPONo.Text) + "'"))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["IS_SUBJECT_TO_VPOC"])) && dr["IS_SUBJECT_TO_VPOC"] != DBNull.Value)
                            ddlIsSubjectToVPOC.SelectedValue = Convert.ToString(dr["IS_SUBJECT_TO_VPOC"]);
                        else
                            ddlIsSubjectToVPOC.SelectedValue = "0";
                    }
                }
                else
                    ddlIsSubjectToVPOC.SelectedValue = "0";

                if (dtUnpostedPOList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUnpostedPOList.Select("PO_NO='" + Convert.ToString(lblPONo.Text) + "'"))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["IS_BILLABLE"])) && dr["IS_BILLABLE"] != DBNull.Value)
                            ddlIsBillable.SelectedValue = Convert.ToString(dr["IS_BILLABLE"]);
                        else
                            ddlIsBillable.SelectedValue = "0";
                    }
                }
                else
                    ddlIsBillable.SelectedValue = "0";


                lblPostingMonth.Text = Convert.ToDateTime(hdPostingMonth.Value).ToString("yyyy-MMM");

                if (Convert.ToDouble(lblPOAmountNew.Text) > 0)
                {
                    txtPOBudgetedAmount.Enabled = false;
                    for (int i = 0; i < e.Row.Cells.Count - 1; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                        e.Row.Cells[i].Enabled = false;
                    }
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

    protected void gvUnpostedPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                bool chk = false;
                string jobNo = string.Empty;
                string poNo = string.Empty;
                string poDate = string.Empty;
                string vendorCode = string.Empty;
                string vendorName = string.Empty;
                int pivotGroupID = 0;

                double pVBudgetedAmount = 0;
                double poAmount = 0;
                double pOBudgetedAmount = 0;

                double poSavingAmount = 0;
                string status = string.Empty;
                string lastDeliveryDate = string.Empty;
                string expectedDeliveryDate = string.Empty;
                int monthsToDeliver = 0;
                int monthsBasedUpon = 0;
                string paymentTerms = string.Empty;
                string isSubjectToVPOC = string.Empty;
                string isBillable = string.Empty;

                string remarks = string.Empty;
                string udf1 = string.Empty;
                string udf2 = string.Empty;
                string udf3 = string.Empty;
                string udf4 = string.Empty;
                string udf5 = string.Empty;

                string postingMonth = string.Empty;
                string location = string.Empty;
                string postedJOBNo = string.Empty;
                double deltaPOAmount = 0;
                double deltaSavingAmount = 0;

                int rowindex = 0;
                if (e.CommandArgument == "GBA" || e.CommandArgument == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblJobNo = gvUnpostedPOList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblPONo = gvUnpostedPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblPODate = gvUnpostedPOList.Rows[rowindex].FindControl("lblPODate") as Label;
                Label lblVendorCode = gvUnpostedPOList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvUnpostedPOList.Rows[rowindex].FindControl("lblVendorName") as Label;
                DropDownList ddlPivotGroup = gvUnpostedPOList.Rows[rowindex].FindControl("ddlPivotGroup") as DropDownList;
                Label lblPVBudgetedAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblPVBudgetedAmount") as Label;
                Label lblPOAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblPOAmount") as Label;
                TextBox txtPOBudgetedAmount = gvUnpostedPOList.Rows[rowindex].FindControl("txtPOBudgetedAmount") as TextBox;
                TextBox txtSavingAmount = gvUnpostedPOList.Rows[rowindex].FindControl("txtSavingAmount") as TextBox;
                DropDownList ddlStatus = gvUnpostedPOList.Rows[rowindex].FindControl("ddlStatus") as DropDownList;
                TextBox txtLastDeliveryDate = gvUnpostedPOList.Rows[rowindex].FindControl("txtLastDeliveryDate") as TextBox;
                TextBox txtExpectedDeliveryDate = gvUnpostedPOList.Rows[rowindex].FindControl("txtExpectedDeliveryDate") as TextBox;
                TextBox txtNoOfMonthsToDeliver = gvUnpostedPOList.Rows[rowindex].FindControl("txtNoOfMonthsToDeliver") as TextBox;
                TextBox txtNoOfMonthsBasedUpon = gvUnpostedPOList.Rows[rowindex].FindControl("txtNoOfMonthsBasedUpon") as TextBox;
                TextBox txtPaymentTerm = gvUnpostedPOList.Rows[rowindex].FindControl("txtPaymentTerm") as TextBox;
                DropDownList ddlIsSubjectToVPOC = gvUnpostedPOList.Rows[rowindex].FindControl("ddlIsSubjectToVPOC") as DropDownList;
                DropDownList ddlIsBillable = gvUnpostedPOList.Rows[rowindex].FindControl("ddlIsBillable") as DropDownList;
                TextBox txtRemarks = gvUnpostedPOList.Rows[rowindex].FindControl("txtRemarks") as TextBox;
                TextBox txtUDF1 = gvUnpostedPOList.Rows[rowindex].FindControl("txtUDF1") as TextBox;
                TextBox txtUDF2 = gvUnpostedPOList.Rows[rowindex].FindControl("txtUDF2") as TextBox;
                TextBox txtUDF3 = gvUnpostedPOList.Rows[rowindex].FindControl("txtUDF3") as TextBox;
                TextBox txtUDF4 = gvUnpostedPOList.Rows[rowindex].FindControl("txtUDF4") as TextBox;
                TextBox txtUDF5 = gvUnpostedPOList.Rows[rowindex].FindControl("txtUDF5") as TextBox;


                Label lblLocation = gvUnpostedPOList.Rows[rowindex].FindControl("lblLocation") as Label;
                Label lblPostingMonth = gvUnpostedPOList.Rows[rowindex].FindControl("lblPostingMonth") as Label;
                Label lblPostedJOBNO = gvUnpostedPOList.Rows[rowindex].FindControl("lblPostedJOBNO") as Label;
                Label lblDeltaPOAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblDeltaPOAmount") as Label;
                Label lblDeltaSavingAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblDeltaSavingAmount") as Label;


                if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                    jobNo = Convert.ToString(lblJobNo.Text);
                else
                    jobNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text)))
                    poNo = Convert.ToString(lblPONo.Text);
                else
                    poNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPODate.Text)))
                    poDate = Convert.ToDateTime(lblPODate.Text).ToString("yyyy-MM-dd");
                else
                    poDate = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(lblVendorCode.Text)))
                    vendorCode = Convert.ToString(lblVendorCode.Text);
                else
                    vendorCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblVendorName.Text)))
                    vendorName = Convert.ToString(lblVendorName.Text);
                else
                    vendorName = string.Empty;

                if (ddlPivotGroup.SelectedIndex > 0)
                {
                    chk = true;
                    pivotGroupID = Convert.ToInt32(ddlPivotGroup.SelectedValue);
                }
                else
                {
                    chk = false;
                    pivotGroupID = 0;
                    ExceptionMessage("Please select pivot group..!");
                    return;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblPVBudgetedAmount.Text)))
                    pVBudgetedAmount = Convert.ToDouble(lblPVBudgetedAmount.Text);
                else
                    pVBudgetedAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPOAmount.Text)))
                    poAmount = Convert.ToDouble(lblPOAmount.Text);
                else
                    poAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtPOBudgetedAmount.Text)))
                    pOBudgetedAmount = Convert.ToDouble(txtPOBudgetedAmount.Text);
                else
                    pOBudgetedAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtSavingAmount.Text)))
                    poSavingAmount = Convert.ToDouble(txtSavingAmount.Text);
                else
                    poSavingAmount = 0;

                if (ddlStatus.SelectedIndex > 0)
                {
                    chk = true;
                    status = Convert.ToString(ddlStatus.SelectedItem.Text);
                }

                else
                {
                    chk = false;
                    status = string.Empty;
                    ExceptionMessage("Please select posting status..!");
                    return;
                }

                postingMonth = Convert.ToDateTime(lblPostingMonth.Text).ToString("yyyy-MM");

                if (!string.IsNullOrEmpty(Convert.ToString(txtLastDeliveryDate.Text)))
                    lastDeliveryDate = Convert.ToDateTime(txtLastDeliveryDate.Text).ToString("yyyy-MM-dd");
                else
                    lastDeliveryDate = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtExpectedDeliveryDate.Text)))
                {
                    chk = true;
                    expectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    chk = false;
                    expectedDeliveryDate = string.Empty;
                    ExceptionMessage("Please enter expected date of delivery..!");
                    return;

                }

                if (!string.IsNullOrEmpty(Convert.ToString(txtNoOfMonthsToDeliver.Text)))
                    monthsToDeliver = Convert.ToInt32(txtNoOfMonthsToDeliver.Text);
                else
                    monthsToDeliver = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtNoOfMonthsBasedUpon.Text)))
                    monthsBasedUpon = Convert.ToInt32(txtNoOfMonthsBasedUpon.Text);
                else
                    monthsBasedUpon = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtPaymentTerm.Text)))
                    paymentTerms = Convert.ToString(txtPaymentTerm.Text);
                else
                    paymentTerms = string.Empty;

                if (ddlIsSubjectToVPOC.SelectedIndex > 0)
                {
                    chk = true;
                    isSubjectToVPOC = Convert.ToString(ddlIsSubjectToVPOC.SelectedItem.Text);
                }
                else
                {
                    chk = false;
                    isSubjectToVPOC = string.Empty;
                    ExceptionMessage("Please select is subject to VPOC..!");
                    return;
                }
                if (ddlIsBillable.SelectedIndex > 0)
                {
                    chk = true;
                    isBillable = Convert.ToString(ddlIsBillable.SelectedItem.Text);
                }
                else
                {
                    chk = false;
                    isBillable = string.Empty;
                    ExceptionMessage("Please select is billable..!");
                    return;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(txtRemarks.Text)))
                    remarks = Convert.ToString(txtRemarks.Text);
                else
                    remarks = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtUDF1.Text)))
                    udf1 = Convert.ToString(txtUDF1.Text);
                else
                    udf1 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtUDF2.Text)))
                    udf2 = Convert.ToString(txtUDF2.Text);
                else
                    udf2 = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(txtUDF3.Text)))
                    udf3 = Convert.ToString(txtUDF3.Text);
                else
                    udf3 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtUDF4.Text)))
                    udf4 = Convert.ToString(txtUDF4.Text);
                else
                    udf4 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(txtUDF5.Text)))
                    udf5 = Convert.ToString(txtUDF5.Text);
                else
                    udf5 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                    location = Convert.ToString(lblLocation.Text);
                else
                    location = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(lblPostedJOBNO.Text)))
                    postedJOBNo = Convert.ToString(lblPostedJOBNO.Text);
                else
                    postedJOBNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDeltaPOAmount.Text)))
                    deltaPOAmount = Convert.ToDouble(lblDeltaPOAmount.Text);
                else
                    deltaPOAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDeltaSavingAmount.Text)))
                    deltaSavingAmount = Convert.ToDouble(lblDeltaSavingAmount.Text);
                else
                    deltaSavingAmount = 0;

                int value = 0;
                if (chk)
                {
                    value = objPurchase.PostUnpostedPOSeven(poNo, poDate, vendorCode, vendorName, jobNo,
                                                                pivotGroupID, pVBudgetedAmount, poAmount, pOBudgetedAmount, poSavingAmount, status,
                                                                postingMonth,
                                                                lastDeliveryDate, expectedDeliveryDate, monthsToDeliver, monthsBasedUpon,
                                                                paymentTerms, isSubjectToVPOC, isBillable, remarks, udf1, udf2, udf3, udf4, udf5,
                                                                location, postedJOBNo, deltaPOAmount, deltaSavingAmount, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                }
                if (value > 0)
                {
                    pnlMsg.Visible = false;
                    lblMsg.Text = string.Empty;
                    GetUnpostedPOList();
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

    protected void ddlJOBNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJOBNo.SelectedIndex > 0)
        {
            BindPivotGroupByJob();
        }
        else
        {
            ddlPivotGroup.Items.Clear();
            ddlPivotGroup.Items.Insert(0, "SELECT");
            ddlPivotGroup.SelectedIndex = 0;
        }
    }

    protected void ddlPivotGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string jobNo = string.Empty;
            int pivotGroupID = 0;
            string pivotGroup = string.Empty;
            string unitName = string.Empty;

            DropDownList ddlPivotGroup = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlPivotGroup.NamingContainer;
            Label lblJobNo = (Label)gr.FindControl("lblJobNo");
            Label lblPVBudgetedAmount = (Label)gr.FindControl("lblPVBudgetedAmount");
            Label lblPOAmount = (Label)gr.FindControl("lblPOAmount");
            TextBox txtPOBudgetedAmount = (TextBox)gr.FindControl("txtPOBudgetedAmount");
            TextBox txtSavingAmount = (TextBox)gr.FindControl("txtSavingAmount");
            Button btnPost = (Button)gr.FindControl("btnPost");

            jobNo = lblJobNo.Text;


            if (ddlPivotGroup.SelectedIndex > 0)
            {
                pivotGroupID = Convert.ToInt32(ddlPivotGroup.SelectedValue);
                pivotGroup = Convert.ToString(ddlPivotGroup.SelectedItem.Text);

                dsDBDetails = (DataSet)Session["DB_DETAILS"];

                if (ddlCompany.SelectedIndex > 0)
                {
                    unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                    unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                    dsBudgetedAmt = objPurchase.GetBudgetedAmountByPivotGroupTwo(unitId, unitName, jobNo, pivotGroupID);
                }
                else
                {
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
                    dsBudgetedAmt = objPurchase.GetBudgetedAmountByPivotGroupAllUnits(jobNo, pivotGroupID, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
                }

                if (dsBudgetedAmt.Tables.Count > 0 && dsBudgetedAmt.Tables[0].Rows.Count > 0)
                {
                    lblPVBudgetedAmount.Text = Convert.ToString(dsBudgetedAmt.Tables[0].Rows[0]["PV_BUDGETED_AMOUNT"]);
                }
                else
                {
                    lblPVBudgetedAmount.Text = "0";
                }
                btnPost.Visible = true;
            }
            else
            {
                lblPVBudgetedAmount.Text = "0";
                btnPost.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        GridViewRow gr = (GridViewRow)ddlStatus.NamingContainer;
        Button btnPost = (Button)gr.FindControl("btnPost");
        TextBox txtPOBudgetedAmount = (TextBox)gr.FindControl("txtPOBudgetedAmount");

        //if (ddlStatus.SelectedIndex > 0 && Convert.ToDouble(txtPOBudgetedAmount.Text) > 0)
        //    btnPost.Visible = true;
        //else
        //    btnPost.Visible = false;
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

    private void BindJobNo()
    {
        try
        {
            dsJobNo = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJobNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NO";
                ddlJOBNo.DataValueField = "JOB_NO";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPivotGroupByJob()
    {
        dsPivotGroupByJob = objPurchase.BindPivotGroupByJob(Convert.ToString(ddlJOBNo.SelectedValue));
        if (dsPivotGroupByJob.Tables.Count > 0 && dsPivotGroupByJob.Tables[0].Rows.Count > 0)
        {
            ddlPivotGroup.DataSource = dsPivotGroupByJob.Tables[0];
            ddlPivotGroup.DataTextField = "PIVOT_GROUP";
            ddlPivotGroup.DataValueField = "PIVOT_GROUP_ID";
            ddlPivotGroup.DataBind();
            ddlPivotGroup.Items.Insert(0, "SELECT");
            ddlPivotGroup.SelectedIndex = 0;
        }
        else
        {
            ddlPivotGroup.Items.Clear();
            ddlPivotGroup.Items.Insert(0, "SELECT");
            ddlPivotGroup.SelectedIndex = 0;
        }
    }

    private void GetUnpostedPOList()
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

            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
            else
                jobNo = string.Empty;

            if (ddlPivotGroup.SelectedIndex > 0)
                pivotGroup = Convert.ToString(ddlPivotGroup.SelectedItem.Text);
            else
                pivotGroup = string.Empty;

            if (!string.IsNullOrEmpty(txtVendorName.Text))
                vendorName = Convert.ToString(txtVendorName.Text);
            else
                vendorName = string.Empty;


            dsPivotGroup = objProject.GetDetailsBySP("sp_get_pivot_group");
            if (dsPivotGroup.Tables.Count > 0 && dsPivotGroup.Tables[0].Rows.Count > 0)
                Session["dtPivotGroup"] = dsPivotGroup.Tables[0];
            else
                Session["dtPivotGroup"] = null;


            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                dsUnpostedPOList = objPurchase.GetUnpostedPOListSix(fromDate, toDate, unitId, unitName, jobNo, pivotGroup, vendorName);
            }
            else
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        {
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        {
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        {
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        {
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                        }
                    }
                }
                dsUnpostedPOList = objPurchase.GetUnpostedPOListAllUnitsOne(fromDate, toDate, unitId, unitName, jobNo, pivotGroup, vendorName,
                                                                        dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ,
                                                                        unitNameSEZ, dbNameGNU, unitNameGNU);
            }



            if (dsUnpostedPOList.Tables.Count > 0 && dsUnpostedPOList.Tables[0].Rows.Count > 0)
            {
                Session["dtUnpostedPOList"] = dsUnpostedPOList.Tables[0];
                gvUnpostedPOList.DataSource = dsUnpostedPOList.Tables[0];
                gvUnpostedPOList.DataBind();
            }
            else
            {
                Session["dtUnpostedPOList"] = null;
                gvUnpostedPOList.DataSource = null;
                gvUnpostedPOList.DataBind();
            }

            lblRecords.Text = "Records[" + Convert.ToString(gvUnpostedPOList.Rows.Count) + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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