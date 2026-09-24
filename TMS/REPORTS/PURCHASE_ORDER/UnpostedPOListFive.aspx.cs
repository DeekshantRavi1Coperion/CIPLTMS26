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

public partial class REPORTS_PURCHASE_ORDER_UnpostedPOListFive : System.Web.UI.Page
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

                Label lblBudgetedAmount = (Label)e.Row.FindControl("lblBudgetedAmount");
                Label lblPOAmount = (Label)e.Row.FindControl("lblPOAmount");
                TextBox txtPostingValue = (TextBox)e.Row.FindControl("txtPostingValue");

                DropDownList ddlPostingStatus = (DropDownList)e.Row.FindControl("ddlPostingStatus");
                DropDownList ddlPivotGroup = (DropDownList)e.Row.FindControl("ddlPivotGroup");

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


                if (Convert.ToDouble(lblBudgetedAmount.Text) > 0 && ddlPostingStatus.SelectedIndex > 0)
                    btnPost.Visible = true;
                else
                    btnPost.Visible = false;
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
                double budgetedAmount = 0;
                double poAmount = 0;
                double postingValue = 0;
                double pendingAmount = 0;
                string postingStatus = string.Empty;
                string postingMonth = string.Empty;
                string location = string.Empty;

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
                Label lblBudgetedAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblBudgetedAmount") as Label;
                Label lblPOAmount = gvUnpostedPOList.Rows[rowindex].FindControl("lblPOAmount") as Label;
                TextBox txtPostingValue = gvUnpostedPOList.Rows[rowindex].FindControl("txtPostingValue") as TextBox;
                TextBox txtPendingAmount = gvUnpostedPOList.Rows[rowindex].FindControl("txtPendingAmount") as TextBox;
                DropDownList ddlPostingStatus = gvUnpostedPOList.Rows[rowindex].FindControl("ddlPostingStatus") as DropDownList;
                Label lblLocation = gvUnpostedPOList.Rows[rowindex].FindControl("lblLocation") as Label;




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


                if (!string.IsNullOrEmpty(Convert.ToString(lblBudgetedAmount.Text)))
                    budgetedAmount = Convert.ToDouble(lblBudgetedAmount.Text);
                else
                    budgetedAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(lblPOAmount.Text)))
                    poAmount = Convert.ToDouble(lblPOAmount.Text);
                else
                    poAmount = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtPostingValue.Text)))
                    postingValue = Convert.ToDouble(txtPostingValue.Text);
                else
                    postingValue = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(txtPendingAmount.Text)))
                    pendingAmount = Convert.ToDouble(txtPendingAmount.Text);
                else
                    pendingAmount = 0;


                if (ddlPostingStatus.SelectedIndex > 0)
                {
                    chk = true;
                    postingStatus = Convert.ToString(ddlPostingStatus.SelectedItem.Text);
                }

                else
                {
                    chk = false;
                    postingStatus = string.Empty;
                    ExceptionMessage("Please select posting status..!");
                    return;
                }

                postingMonth = Convert.ToDateTime(txtPostingMonth.Text).ToString("yyyy-MM");

                if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                    location = Convert.ToString(lblLocation.Text);
                else
                    location = string.Empty;

                int value = 0;
                if (chk)
                {
                    value = objPurchase.PostUnpostedPO(jobNo, poNo, poDate, vendorCode, vendorName, pivotGroupID, budgetedAmount, poAmount,
                                                    postingValue, pendingAmount, postingStatus, postingMonth, location, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }
                if (value > 0)
                {
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

    protected void ddlPivotGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string jobNo = string.Empty;
            double poAmount = 0;
            int pivotGroupID = 0;
            string pivotGroup = string.Empty;
            int unitID = 0;
            string unitName = string.Empty;

            DropDownList ddlPivotGroup = (DropDownList)sender;
            GridViewRow gr = (GridViewRow)ddlPivotGroup.NamingContainer;
            Label lblJobNo = (Label)gr.FindControl("lblJobNo");
            Label lblBudgetedAmount = (Label)gr.FindControl("lblBudgetedAmount");
            Label lblPOAmount = (Label)gr.FindControl("lblPOAmount");
            TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
            TextBox txtPendingAmount = (TextBox)gr.FindControl("txtPendingAmount");            

            jobNo = lblJobNo.Text;
            pivotGroupID = Convert.ToInt32(ddlPivotGroup.SelectedValue);
            pivotGroup = Convert.ToString(ddlPivotGroup.SelectedItem.Text);
            if (ddlCompany.SelectedIndex > 0)
            {
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            }

            if (!string.IsNullOrEmpty(lblPOAmount.Text))
                poAmount = Convert.ToDouble(lblPOAmount.Text);
            else
                poAmount = 0;


            dsBudgetedAmt = objPurchase.GetBudgetedAmountByPivotGroupOne(unitID, unitName, jobNo, poAmount, pivotGroupID);

            if (dsBudgetedAmt.Tables.Count > 0 && dsBudgetedAmt.Tables[0].Rows.Count > 0)
            {
                lblBudgetedAmount.Text = Convert.ToString(dsBudgetedAmt.Tables[0].Rows[0]["BUDGETED_AMOUNT"]);
                //txtPendingAmount.Text = Convert.ToString(dsBudgetedAmt.Tables[0].Rows[0]["PENDING_AMOUNT"]);                
            }
            else
            {
                lblBudgetedAmount.Text = "0";
                //txtPendingAmount.Text = "0";                
            }

            txtPendingAmount.Text = Convert.ToString(Convert.ToDouble(lblBudgetedAmount.Text) - (Convert.ToDouble(lblPOAmount.Text) + Convert.ToDouble(txtPostingValue.Text)));
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlPostingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlPostingStatus = (DropDownList)sender;
        GridViewRow gr = (GridViewRow)ddlPostingStatus.NamingContainer;
        Button btnPost = (Button)gr.FindControl("btnPost");
        Label lblBudgetedAmount = (Label)gr.FindControl("lblBudgetedAmount");
        TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");

        if (ddlPostingStatus.SelectedIndex > 0 && Convert.ToDouble(txtPostingValue.Text) > 0)
            btnPost.Visible = true;
        else
            btnPost.Visible = false;
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


            dsDBDetails = (DataSet)Session["DB_DETAILS"];

            if (ddlCompany.SelectedIndex > 0)
            {
                unitId = Convert.ToInt32(ddlCompany.SelectedValue);
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
                dsUnpostedPOList = objPurchase.GetUnpostedPOListFive(fromDate, toDate, unitId, unitName, jobNo, pivotGroup);
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
                dsUnpostedPOList = objPurchase.GetUnpostedPOListAllUnits(fromDate, toDate, unitId, unitName, jobNo, pivotGroup,
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
            string poNo = string.Empty;
            string poDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            string jobNo = string.Empty;
            int pivotGroupID = 0;
            double budgetedAmount = 0;
            double poAmount = 0;
            double poPostingValue = 0;
            string postingStatus = string.Empty;
            string location = string.Empty;

            if (gvUnpostedPOList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvUnpostedPOList.Rows)
                {
                    chk = true;

                    Label lblPONo = (Label)gr.FindControl("lblPONo");
                    Label lblPODate = (Label)gr.FindControl("lblPODate");
                    Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                    Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                    Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                    DropDownList ddlPivotGroup = (DropDownList)gr.FindControl("ddlPivotGroup");
                    Label lblBudgetedAmount = (Label)gr.FindControl("lblBudgetedAmount");
                    Label lblPOAmount = (Label)gr.FindControl("lblPOAmount");
                    TextBox txtPostingValue = (TextBox)gr.FindControl("txtPostingValue");
                    DropDownList ddlPostingStatus = (DropDownList)gr.FindControl("ddlPostingStatus");
                    Label lblLocation = (Label)gr.FindControl("lblLocation");

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

                    if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                        jobNo = Convert.ToString(lblJobNo.Text);
                    else
                        jobNo = string.Empty;

                    if (ddlPivotGroup.SelectedIndex > 0)
                        pivotGroupID = Convert.ToInt32(ddlPivotGroup.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select sales pivot group.!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(lblBudgetedAmount.Text)))
                        budgetedAmount = Convert.ToDouble(lblBudgetedAmount.Text);
                    else
                        budgetedAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(lblPOAmount.Text)))
                        poAmount = Convert.ToDouble(lblPOAmount.Text);
                    else
                        poAmount = 0;

                    if (!string.IsNullOrEmpty(Convert.ToString(txtPostingValue.Text)))
                        poPostingValue = Convert.ToDouble(txtPostingValue.Text);
                    else
                        poPostingValue = 0;

                    postingStatus = Convert.ToString(ddlPostingStatus.SelectedValue);

                    if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text)))
                        location = Convert.ToString(lblLocation.Text);
                    else
                        location = string.Empty;






                    int value = 0;
                    if (chk)
                    {
                        //value = objPurchase.PostUnpostedPO(poNo, poDate, vendorCode, vendorName, jobNo,
                        //                                        pivotGroupID, budgetedAmount, poAmount, poPostingValue, postingStatus,
                        //                                        location, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (value > 0)
                        {
                            GetUnpostedPOList();
                        }
                    }

                }
            }
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