using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class REPORTS_PROJECT_MGMT_UnpostedPOListOne : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Purchase objPurchase = new BAL.Purchase();

    DataSet dsJobNo = new DataSet();
    DataSet dsPivotGroup = new DataSet();
    DataSet dsPivotGroupByJob = new DataSet();
    DataSet dsPoPostingStatus = new DataSet();
    DataSet dsUnpostedPOList = new DataSet();

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

                hdPostingMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtPostingMonth.Text = Convert.ToString(hdPostingMonth.Value);

                dsPoPostingStatus = objProject.GetDetailsBySP("sp_get_po_posting_status");
                Session["dtPoPostingStatus"] = dsPoPostingStatus.Tables[0];

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
                Label lblPendingAmount = (Label)e.Row.FindControl("lblPendingAmount");

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


                lblPendingAmount.Text = Convert.ToString(Convert.ToDouble(lblBudgetedAmount.Text) - (Convert.ToDouble(lblPOAmount.Text) + Convert.ToDouble(txtPostingValue.Text)));


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

            dsUnpostedPOList = objPurchase.GetUnpostedPOListOne(jobNo, pivotGroup);

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
                            RemoveRecord(serialNo);
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

    private void RemoveRecord(int serialNo)
    {
        try
        {
            HidePanel();

            DataTable dtNew = new DataTable();

            dtNew.Columns.Add("SERIAL_NO", typeof(string));
            dtNew.Columns.Add("PURCHASE_ID", typeof(string));
            dtNew.Columns.Add("EXISTING_PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("PO_NO", typeof(string));
            dtNew.Columns.Add("PO_DATE", typeof(string));
            dtNew.Columns.Add("VENDOR_CODE", typeof(string));
            dtNew.Columns.Add("VENDOR_NAME", typeof(string));
            dtNew.Columns.Add("PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE", typeof(string));
            dtNew.Columns.Add("TOTAL_PO_VALUE_INR", typeof(string));
            dtNew.Columns.Add("DOC_CLASS", typeof(string));
            dtNew.Columns.Add("PO_STATUS", typeof(string));
            dtNew.Columns.Add("LOCATION", typeof(string));
            dtNew.Columns.Add("LAST_DELIVERY_DATE", typeof(string));
            dtNew.Columns.Add("EXPECTED_DELIVERY_DATE", typeof(string));
            dtNew.Columns.Add("NO_OF_MONTHS_TO_DELIVER", typeof(string));
            dtNew.Columns.Add("NO_OF_MONTHS_BASED_UPON", typeof(string));
            dtNew.Columns.Add("PAYMENT_TERM", typeof(string));
            dtNew.Columns.Add("IS_ORDER_SUBJECT_TO", typeof(string));
            dtNew.Columns.Add("SALES_PIVOT_GROUP", typeof(string));

            foreach (GridViewRow gr in gvUnpostedPOList.Rows)
            {
                DataRow dr = dtNew.NewRow();
                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblPurchaseID = (Label)gr.FindControl("lblPurchaseID");
                Label lblExistingPoValueINR = (Label)gr.FindControl("lblExistingPoValueINR");
                Label lblPONo = (Label)gr.FindControl("lblPONo");
                Label lblPODate = (Label)gr.FindControl("lblPODate");
                Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                Label lblPOValueINR = (Label)gr.FindControl("lblPOValueINR");
                TextBox txtFraightOrCustomLoading = (TextBox)gr.FindControl("txtFraightOrCustomLoading");
                TextBox txtTotalPOValueINR = (TextBox)gr.FindControl("txtTotalPOValueINR");
                Label lblDocClass = (Label)gr.FindControl("lblDocClass");
                Label lblPOStatus = (Label)gr.FindControl("lblPOStatus");
                Label lblLocation = (Label)gr.FindControl("lblLocation");
                TextBox txtLastDeliveryDate = (TextBox)gr.FindControl("txtLastDeliveryDate");

                TextBox txtExpectedDeliveryDate = (TextBox)gr.FindControl("txtExpectedDeliveryDate");
                TextBox txtNoOfMonthsToDeliver = (TextBox)gr.FindControl("txtNoOfMonthsToDeliver");
                TextBox txtNoOfMonthsBasedUpon = (TextBox)gr.FindControl("txtNoOfMonthsBasedUpon");
                TextBox txtPaymentTerm = (TextBox)gr.FindControl("txtPaymentTerm");
                DropDownList ddlIsOrderSubjectTo = (DropDownList)gr.FindControl("ddlIsOrderSubjectTo");
                DropDownList ddlPivotGroup = (DropDownList)gr.FindControl("ddlPivotGroup");



                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;
                else
                    dr["SERIAL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPurchaseID.Text))
                    dr["PURCHASE_ID"] = lblPurchaseID.Text;
                else
                    dr["PURCHASE_ID"] = "0";

                if (!string.IsNullOrEmpty(lblExistingPoValueINR.Text))
                    dr["EXISTING_PO_VALUE_INR"] = lblExistingPoValueINR.Text;
                else
                    dr["EXISTING_PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(lblPONo.Text))
                    dr["PO_NO"] = lblPONo.Text;
                else
                    dr["PO_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPODate.Text))
                    dr["PO_DATE"] = Convert.ToDateTime(lblPODate.Text).ToString("dd-MMM-yyyy");
                else
                    dr["PO_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    dr["VENDOR_CODE"] = lblVendorCode.Text;
                else
                    dr["VENDOR_CODE"] = string.Empty;

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    dr["VENDOR_NAME"] = lblVendorName.Text;
                else
                    dr["VENDOR_NAME"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPOValueINR.Text))
                    dr["PO_VALUE_INR"] = Convert.ToString(lblPOValueINR.Text);
                else
                    dr["PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(txtFraightOrCustomLoading.Text))
                    dr["FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE"] = Convert.ToString(txtFraightOrCustomLoading.Text);
                else
                    dr["FRAIGHT_OR_CUSTOM_LOADING_IN_PERCENTAGE"] = "0";

                if (!string.IsNullOrEmpty(txtTotalPOValueINR.Text))
                    dr["TOTAL_PO_VALUE_INR"] = Convert.ToString(txtTotalPOValueINR.Text);
                else
                    dr["TOTAL_PO_VALUE_INR"] = "0";

                if (!string.IsNullOrEmpty(lblDocClass.Text))
                    dr["DOC_CLASS"] = Convert.ToString(lblDocClass.Text);
                else
                    dr["DOC_CLASS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblPOStatus.Text))
                    dr["PO_STATUS"] = lblPOStatus.Text;
                else
                    dr["PO_STATUS"] = string.Empty;

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    dr["LOCATION"] = lblLocation.Text;
                else
                    dr["LOCATION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtLastDeliveryDate.Text))
                    dr["LAST_DELIVERY_DATE"] = txtLastDeliveryDate.Text;
                else
                    dr["LAST_DELIVERY_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
                    dr["EXPECTED_DELIVERY_DATE"] = txtExpectedDeliveryDate.Text;
                else
                    dr["EXPECTED_DELIVERY_DATE"] = string.Empty;

                if (!string.IsNullOrEmpty(txtNoOfMonthsToDeliver.Text))
                    dr["NO_OF_MONTHS_TO_DELIVER"] = txtNoOfMonthsToDeliver.Text;
                else
                    dr["NO_OF_MONTHS_TO_DELIVER"] = string.Empty;

                if (!string.IsNullOrEmpty(txtNoOfMonthsBasedUpon.Text))
                    dr["NO_OF_MONTHS_BASED_UPON"] = txtNoOfMonthsBasedUpon.Text;
                else
                    dr["NO_OF_MONTHS_BASED_UPON"] = string.Empty;

                if (!string.IsNullOrEmpty(txtPaymentTerm.Text))
                    dr["PAYMENT_TERM"] = txtPaymentTerm.Text;
                else
                    dr["PAYMENT_TERM"] = string.Empty;

                if (ddlIsOrderSubjectTo.SelectedIndex > 0)
                    dr["IS_ORDER_SUBJECT_TO"] = Convert.ToString(ddlIsOrderSubjectTo.SelectedValue);
                else
                    dr["IS_ORDER_SUBJECT_TO"] = "0";

                if (ddlPivotGroup.SelectedIndex > 0)
                    dr["PIVOT_GROUP"] = Convert.ToString(ddlPivotGroup.SelectedValue);
                else
                    dr["PIVOT_GROUP"] = "0";

                dtNew.Rows.Add(dr);
            }


            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + serialNo + "'"))
                {
                    dtNew.Rows.Remove(drremove);
                }

                if (dtNew.Rows.Count > 0)
                {
                    Session["PO_REPORT"] = dtNew;
                    gvUnpostedPOList.DataSource = dtNew;
                    gvUnpostedPOList.DataBind();
                }
                else
                {
                    Session["PO_REPORT"] = null;
                    gvUnpostedPOList.DataSource = null;
                    gvUnpostedPOList.DataBind();
                }
            }
            else
            {
                Session["PO_REPORT"] = null;
            }
            lblRecords.Text = "Records[" + dtNew.Rows.Count + "]";
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