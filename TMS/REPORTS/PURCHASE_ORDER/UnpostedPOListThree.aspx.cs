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

public partial class REPORTS_PURCHASE_ORDER_UnpostedPOListThree : System.Web.UI.Page
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

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string jobNo = string.Empty;
    string pivotGroup = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
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
                double budgetedAmount = 0;
                double poAmount = 0;
                double postingValue = 0;

                DataTable dtUnpostedPOList = new DataTable();
                DataTable dtPivotGroup = new DataTable();
                DataTable dtPoPostingStatus = new DataTable();

                Label lblPONo = (Label)e.Row.FindControl("lblPONo");

                Label lblBudgetedAmount = (Label)e.Row.FindControl("lblBudgetedAmount");
                Label lblPOAmount = (Label)e.Row.FindControl("lblPOAmount");
                TextBox txtPostingValue = (TextBox)e.Row.FindControl("txtPostingValue");
                TextBox txtPendingAmount = (TextBox)e.Row.FindControl("txtPendingAmount");

                DropDownList ddlPostingStatus = (DropDownList)e.Row.FindControl("ddlPostingStatus");
                DropDownList ddlPivotGroup = (DropDownList)e.Row.FindControl("ddlPivotGroup");

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



                if (dtPoPostingStatus.Rows.Count > 0)
                {
                    ddlPostingStatus.DataSource = dtPoPostingStatus;
                    ddlPostingStatus.DataTextField = "STATUS_NAME";
                    ddlPostingStatus.DataValueField = "STATUS_ID";
                    ddlPostingStatus.DataBind();
                    ddlPostingStatus.Items.Insert(0, "SELECT");
                    ddlPostingStatus.SelectedIndex = 0;
                }
                else
                {
                    ddlPostingStatus.Items.Clear();
                    ddlPostingStatus.Items.Insert(0, "SELECT");
                    ddlPostingStatus.SelectedIndex = 0;
                }

                if (Convert.ToDouble(lblBudgetedAmount.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(lblBudgetedAmount.Text)))
                    budgetedAmount = Convert.ToDouble(lblBudgetedAmount.Text);
                else
                    budgetedAmount = 0;

                if (Convert.ToDouble(lblPOAmount.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(lblPOAmount.Text)))
                    poAmount = Convert.ToDouble(lblPOAmount.Text);
                else
                    poAmount = 0;

                if (Convert.ToDouble(txtPostingValue.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(txtPostingValue.Text)))
                    postingValue = Convert.ToDouble(txtPostingValue.Text);
                else
                    postingValue = 0;

                if (budgetedAmount > 0)
                {
                    txtPendingAmount.Text = Convert.ToString(budgetedAmount - (poAmount + postingValue));
                }
                else
                    txtPendingAmount.Text = "0";
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


    DataSet dsBudAmt = new DataSet();   
    protected void gvUnpostedPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                string jobNo = string.Empty;
                string pivotGroup = string.Empty;
                int pivotGroupID = 0;
                int rowindex = 0;

                if (e.CommandArgument == "GBA" || e.CommandArgument == "POST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblJobNo = gvUnpostedPOList.Rows[rowindex].FindControl("lblJobNo") as Label;
                DropDownList ddlPivotGroup = gvUnpostedPOList.Rows[rowindex].FindControl("ddlPivotGroup") as DropDownList;
                Label lblBudgetedAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblBudgetedAmount") as Label;
                Label lblPOAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblPOAmount") as Label;
                TextBox txtPendingAmount = gvUnpostedPOList.Rows[rowindex].FindControl("txtPendingAmount") as TextBox;
                TextBox txtPostingValue = gvUnpostedPOList.Rows[rowindex].FindControl("txtPostingValue") as TextBox;

                double pendingAmt = 0;
                if (ddlPivotGroup.SelectedIndex > 0)
                {
                    jobNo = Convert.ToString(lblJobNo.Text);
                    pivotGroup = Convert.ToString(ddlPivotGroup.SelectedItem.Text);
                    pivotGroupID = Convert.ToInt32(ddlPivotGroup.SelectedValue);

                    pendingAmt = GetNextBudgetedAmount(pivotGroup, pivotGroupID, jobNo, rowindex);                   
                }
                else
                {
                    pivotGroup = string.Empty;
                    pivotGroupID = 0;
                    lblBudgetedAmount.Text = "0";
                }


                if (Convert.ToDouble(pendingAmt) > 0)
                    txtPendingAmount.Text = Convert.ToString(pendingAmt);
                else
                    txtPendingAmount.Text = "0";                
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

    protected void btnPost_Click(object sender, EventArgs e)
    {
        HidePanel();
        PostUnpostedPO();
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
            ddlPivotGroup.DataValueField = "RECORD_ID";
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

            dsPivotGroup = objProject.GetDetailsBySP("sp_get_pivot_group");
            if (dsPivotGroup.Tables.Count > 0 && dsPivotGroup.Tables[0].Rows.Count > 0)
                Session["dtPivotGroup"] = dsPivotGroup.Tables[0];
            else
                Session["dtPivotGroup"] = null;

            dsUnpostedPOList = objPurchase.GetUnpostedPOListTwo(fromDate, toDate, jobNo, pivotGroup);

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
            lblRecords.Text = "Records[" + gvUnpostedPOList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void PostUnpostedPO()
    {
        try
        {
            bool chk = false;
            int serialNo = 0;
            int recordID = 0;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            double poValueINR = 0;
            double poPostingValueINR = 0;
            string postingStatus = string.Empty;
            string docClass = string.Empty;
            int salesPivotGroup = 0;
            string location = string.Empty;

            int count = 0;
            if (gvUnpostedPOList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvUnpostedPOList.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                    Label lblPONo = (Label)gr.FindControl("lblPONo");
                    Label lblPODate = (Label)gr.FindControl("lblPODate");
                    Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                    Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                    Label lblPOValueINR = (Label)gr.FindControl("lblPOValueINR");
                    TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                    Label lblPostedValue = (Label)gr.FindControl("lblPostedValue");
                    Label lblPendingPostingValue = (Label)gr.FindControl("lblPendingPostingValue");
                    DropDownList ddlPostingStatus = (DropDownList)gr.FindControl("ddlPostingStatus");
                    Label lblDocClass = (Label)gr.FindControl("lblDocClass");
                    DropDownList ddlPivotGroup = (DropDownList)gr.FindControl("ddlPivotGroup");
                    Label lblLocation = (Label)gr.FindControl("lblLocation");


                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)))
                        recordID = Convert.ToInt32(lblRecordID.Text);
                    else
                        recordID = 0;

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

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPOValueINR.Text)))
                        poValueINR = Convert.ToDouble(lblPOValueINR.Text);
                    else
                        poValueINR = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPostingValue.Text)))
                        poPostingValueINR = Convert.ToDouble(txtPostingValue.Text);
                    else
                        poPostingValueINR = 0;

                    postingStatus = Convert.ToString(ddlPostingStatus.SelectedValue);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblDocClass.Text)))
                        docClass = Convert.ToString(lblDocClass.Text);
                    else
                        docClass = string.Empty;

                    if (ddlPivotGroup.SelectedIndex > 0)
                        salesPivotGroup = Convert.ToInt32(ddlPivotGroup.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select sales pivot group.!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                        location = Convert.ToString(lblLocation.Text);
                    else
                        location = string.Empty;

                    if (chk)
                    {
                        int value = objPurchase.PostUnpostedPO(poNo, poDate, vendorCode, vendorName, poValueINR, poPostingValueINR, postingStatus,
                                                                docClass, salesPivotGroup, location, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (value > 0)
                        {
                            count++;
                            //RemoveRecord(serialNo);
                        }
                    }
                }

                if (Session["PO_REPORT"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["PO_REPORT"];
                    gvUnpostedPOList.DataSource = dt;
                    gvUnpostedPOList.DataBind();

                    SuccessMessage(count + " Records imported. Rest " + gvUnpostedPOList.Rows.Count + " Records already existed with different PO Value INR, would you like to update..?");
                }
                else
                {
                    SuccessMessage("All records imported successfully..!");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    
    private double GetNextBudgetedAmount(string pivotGroup, int pivotGroupID, string jobNo, int currentRowIndex)
    {
        try
        {
            double pvtBudgetAmount = 0;
            double totalPOAmount = 0;
            double totalPostingValue = 0;
            double pendingAmount = 0;

            if (gvUnpostedPOList.Rows.Count > 0)
            {
                DropDownList ddlPivotGroup = (DropDownList)gvUnpostedPOList.Rows[currentRowIndex].FindControl("ddlPivotGroup");
                Label lblBudgetedAmount = (Label)gvUnpostedPOList.Rows[currentRowIndex].FindControl("lblBudgetedAmount");
                Label lblPOAmount = (Label)gvUnpostedPOList.Rows[currentRowIndex].FindControl("lblPOAmount");
                TextBox txtPendingAmount = (TextBox)gvUnpostedPOList.Rows[currentRowIndex].FindControl("txtPendingAmount");

                if (ddlPivotGroup.SelectedIndex > 0)
                {
                    dsBudAmt = objPurchase.GetBudgetedAmountByPivotGroupOne(jobNo, pivotGroup, pivotGroupID);
                    if (dsBudAmt.Tables.Count > 0 && dsBudAmt.Tables[0].Rows.Count > 0)
                    {
                        if (dsBudAmt.Tables[0].Rows[0]["BUDGETED_AMOUNT"] != DBNull.Value && Convert.ToDouble(dsBudAmt.Tables[0].Rows[0]["BUDGETED_AMOUNT"]) > 0)
                            pvtBudgetAmount = Convert.ToDouble(dsBudAmt.Tables[0].Rows[0]["BUDGETED_AMOUNT"]);
                        else
                            pvtBudgetAmount = 0;
                    }
                }
                else
                {
                    pvtBudgetAmount = 0;
                    return 0;
                }

                double currentBudgetAmt = 0;
                foreach (GridViewRow grv in gvUnpostedPOList.Rows)
                {
                    DropDownList ddlPivotGroupGr = (DropDownList)grv.FindControl("ddlPivotGroup");
                    Label lblJobNo = (Label)grv.FindControl("lblJobNo");
                    Label lblBudgetedAmountGr = grv.FindControl("lblBudgetedAmount") as Label;
                    if (jobNo == lblJobNo.Text && pivotGroup == Convert.ToString(ddlPivotGroupGr.SelectedItem.Text))
                    {
                        if (Convert.ToDouble(lblBudgetedAmountGr.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(lblBudgetedAmountGr.Text)))
                        {
                            currentBudgetAmt = Convert.ToDouble(lblBudgetedAmountGr.Text);
                            break;
                        }
                    }
                }

                if (currentBudgetAmt == 0)
                    lblBudgetedAmount.Text = Convert.ToString(pvtBudgetAmount);

                foreach (GridViewRow grv in gvUnpostedPOList.Rows)
                {
                    DropDownList ddlPivotGroupGr = (DropDownList)grv.FindControl("ddlPivotGroup");
                    Label lblJobNo = (Label)grv.FindControl("lblJobNo");
                    Label lblBudgetedAmountGr = grv.FindControl("lblBudgetedAmount") as Label;
                    Label lblPOAmountGr = grv.FindControl("lblPOAmount") as Label;
                    TextBox txtPostingValueGr = grv.FindControl("txtPostingValue") as TextBox;

                    if (jobNo == lblJobNo.Text && pivotGroup == Convert.ToString(ddlPivotGroupGr.SelectedItem.Text))
                    {
                        if (Convert.ToDouble(lblBudgetedAmountGr.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(lblBudgetedAmountGr.Text)))
                        {
                            if (Convert.ToDouble(lblPOAmountGr.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(lblPOAmountGr.Text)))
                                totalPOAmount += Convert.ToDouble(lblPOAmountGr.Text);

                            if (Convert.ToDouble(txtPostingValueGr.Text) > 0 && !string.IsNullOrEmpty(Convert.ToString(txtPostingValueGr.Text)))
                                totalPostingValue += Convert.ToDouble(txtPostingValueGr.Text);
                        }
                    }
                }

                pendingAmount = (pvtBudgetAmount - (totalPOAmount + totalPostingValue));
                if (currentBudgetAmt > 0)
                {
                    lblBudgetedAmount.Text = Convert.ToString(pendingAmount);
                    //pendingAmount = pendingAmount - Convert.ToDouble(lblPOAmount.Text);
                }

                txtPendingAmount.Text = Convert.ToString(pendingAmount);

                return pendingAmount;
            }
            return pendingAmount;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return 0;
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