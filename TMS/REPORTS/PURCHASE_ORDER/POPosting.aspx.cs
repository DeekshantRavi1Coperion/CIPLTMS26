using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class REPORTS_PURCHASE_ORDER_POPosting : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Purchase objPurchase = new BAL.Purchase();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsCreatedBy = new DataSet();
    DataSet dsCheckedBy = new DataSet();
    DataSet dsPOList = new DataSet();
    DataSet dsPODetailList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsImportMode = new DataSet();
    DataSet dsDBDetails = new DataSet();

    int dateTypeID = 0;
    string fromDate = string.Empty;
    string toDate = string.Empty;

    string poNo = string.Empty;
    string vendorName = string.Empty;
    string JOBNo = string.Empty;
    double amount1 = 0;
    double amount2 = 0;
    string unitName = string.Empty;
    string sign = string.Empty;
    int excludeCIDF = 0;
    int excludeEngineeringService = 0;
    string isDoneOrPending = string.Empty;
    string followupBy = string.Empty;
    string followupDate = string.Empty;
    string postingStatus = string.Empty;
    string mrNo = string.Empty;
    string mrCreatedBy = string.Empty;
    string checkedBy = string.Empty;

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    string fileName = string.Empty;
    string body = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string toName = string.Empty;
    string cc = string.Empty;
    string bcc = string.Empty;
    string mailSentDate = string.Empty;

    double totalInvoiceINR = 0;
    double totalQuantity = 0;
    double totalRecQuantity = 0;
    double totalBalQuantity = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdPostingConfirmValue.Value = "0";
                hdDeletionConfirmValue.Value = "0";

                Session["dtUnitList"] = null;
                Session["dtImportModeList"] = null;

                Session["dtPMList"] = null;
                Session["dtOthPMList"] = null;
                Session["dtPODetailList"] = null;
                Session["dtPOList"] = null;


                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
                BindCreatedBy();
                BindCheckedBy();
                BindUnit();
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                ddlPostingStatusSearch.SelectedValue = "Open";
            }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "afterpostback();", true);
            //}
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        lblTotalInvoiceINR.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;

        //GetImportModeList();

        GetPOList();

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

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataTable dtImportList = new DataTable();
                if (Session["dtImportModeList"] != null)
                    dtImportList = (DataTable)Session["dtImportModeList"];
                else
                {
                    GetImportModeList();
                    dtImportList = (DataTable)Session["dtImportModeList"];
                }
                DropDownList ddlImportMode = (DropDownList)e.Row.FindControl("ddlImportMode");
                Label lblImportMode = (Label)e.Row.FindControl("lblImportMode");

                if (dtImportList.Rows.Count > 0)
                {
                    ddlImportMode.DataSource = dtImportList;
                    ddlImportMode.DataTextField = "IMPORT_MODE_BUFFER_DAYS";
                    ddlImportMode.DataValueField = "IMPORT_MODE_ID";
                    ddlImportMode.DataBind();
                    //ddlImportMode.Items.Insert(0, "Local Vendors [10 Days]");
                    ddlImportMode.SelectedIndex = 0;

                    if (Convert.ToInt32(lblImportMode.Text) > 0)
                    {
                        ddlImportMode.SelectedValue = lblImportMode.Text;
                    }
                }


                Label lblFollowUpDays = (Label)e.Row.FindControl("lblFollowUpDays");
                Label lblPostingStatus = (Label)e.Row.FindControl("lblPostingStatus");
                Label lblEnggApprovalStatus = (Label)e.Row.FindControl("lblEnggApprovalStatus");
                Label lblPONo = (Label)e.Row.FindControl("lblPONo");

                Label lblPODeliveryDate = (Label)e.Row.FindControl("lblPODeliveryDate");

                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtBalQuantity = (TextBox)e.Row.FindControl("txtBalQuantity");
                TextBox txtPOFirstItem = (TextBox)e.Row.FindControl("txtPOFirstItem");
                TextBox txtItemName = (TextBox)e.Row.FindControl("txtItemName");
                TextBox txtPresentStatus = (TextBox)e.Row.FindControl("txtPresentStatus");
                TextBox txtEdOfInspection = (TextBox)e.Row.FindControl("txtEdOfInspection");
                TextBox txtLastStatus = (TextBox)e.Row.FindControl("txtLastStatus");
                TextBox txtNextFollowupDate = (TextBox)e.Row.FindControl("txtNextFollowupDate");

                Label lblNextFollowupDate = (Label)e.Row.FindControl("lblNextFollowupDate");

                TextBox txtFinalDeliveryDateAsPerAgreedTerms = (TextBox)e.Row.FindControl("txtFinalDeliveryDateAsPerAgreedTerms");

                if (string.IsNullOrEmpty(txtFinalDeliveryDateAsPerAgreedTerms.Text))
                {
                    txtFinalDeliveryDateAsPerAgreedTerms.Text = Convert.ToString(lblPODeliveryDate.Text);
                }

                if (lblNextFollowupDate.Text == "01-Jan-1900")
                {
                    lblNextFollowupDate.Text = string.Empty;
                    txtNextFollowupDate.Text = string.Empty;
                }


                e.Row.ToolTip = Convert.ToString(lblPONo.Text);
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");

                TextBox txtPOValueINR = (TextBox)e.Row.FindControl("txtPOValueINR");
                TextBox txtBudget = (TextBox)e.Row.FindControl("txtBudget");

                totalInvoiceINR += Convert.ToDouble(txtPOValueINR.Text);
                lblTotalInvoiceINR.Text = Convert.ToString(totalInvoiceINR);
                lblTotalInvoiceINR.ForeColor = System.Drawing.Color.Green;

                DropDownList ddlEnggApprovalStatus = (DropDownList)e.Row.FindControl("ddlEnggApprovalStatus");
                if (!string.IsNullOrEmpty(lblEnggApprovalStatus.Text))
                {
                    ddlEnggApprovalStatus.SelectedValue = lblEnggApprovalStatus.Text;
                }
                else
                {
                    ddlEnggApprovalStatus.SelectedIndex = 0;
                }



                DropDownList ddlPostingStatus = (DropDownList)e.Row.FindControl("ddlPostingStatus");
                if (!string.IsNullOrEmpty(lblPostingStatus.Text))
                {
                    ddlPostingStatus.SelectedValue = lblPostingStatus.Text;
                }
                else
                {
                    ddlPostingStatus.SelectedIndex = 0;
                }

                if (Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    txtItemName.Enabled = false;
                    txtItemName.BackColor = System.Drawing.Color.LightGreen;


                    txtPOFirstItem.BackColor = System.Drawing.Color.LightGreen;
                    txtQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtBalQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtPOValueINR.BackColor = System.Drawing.Color.LightGreen;
                    txtBudget.BackColor = System.Drawing.Color.LightGreen;

                    txtPresentStatus.BackColor = System.Drawing.Color.LightGreen;
                    txtEdOfInspection.BackColor = System.Drawing.Color.LightGreen;
                    txtLastStatus.BackColor = System.Drawing.Color.LightGreen;
                    txtNextFollowupDate.BackColor = System.Drawing.Color.LightGreen;


                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else
                {
                    txtItemName.Enabled = true;
                    txtItemName.BackColor = System.Drawing.Color.LightYellow;

                    txtPOFirstItem.BackColor = System.Drawing.Color.LightYellow;
                    txtQuantity.BackColor = System.Drawing.Color.LightYellow;
                    txtBalQuantity.BackColor = System.Drawing.Color.LightYellow;
                    txtPOValueINR.BackColor = System.Drawing.Color.LightYellow;
                    txtBudget.BackColor = System.Drawing.Color.LightYellow;

                    txtPresentStatus.BackColor = System.Drawing.Color.LightYellow;
                    txtEdOfInspection.BackColor = System.Drawing.Color.LightYellow;
                    txtLastStatus.BackColor = System.Drawing.Color.LightYellow;
                    txtNextFollowupDate.BackColor = System.Drawing.Color.LightYellow;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
                    }
                }


                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                if (lblPostingStatus.Text == "Close" && lblPostingStatus.Text != "Open")
                {
                    //headerSelect.Visible = false;
                    chkSelect.Visible = false;
                    txtItemName.Enabled = false;
                    ddlPostingStatus.Enabled = false;

                    //txtItemName.BackColor = System.Drawing.Color.LightPink;
                    //txtPOFirstItem.BackColor = System.Drawing.Color.LightPink;
                    //txtQuantity.BackColor = System.Drawing.Color.LightPink;
                    //txtBalQuantity.BackColor = System.Drawing.Color.LightPink;
                    //txtPOValueINR.BackColor = System.Drawing.Color.LightPink;
                    //txtBudget.BackColor = System.Drawing.Color.LightPink;

                    //txtPresentStatus.BackColor = System.Drawing.Color.LightPink;
                    //txtEdOfInspection.BackColor = System.Drawing.Color.LightPink;
                    //txtLastStatus.BackColor = System.Drawing.Color.LightPink;
                    //txtNextFollowupDate.BackColor = System.Drawing.Color.LightPink;



                    txtItemName.BackColor = System.Drawing.Color.LightGreen;
                    txtPOFirstItem.BackColor = System.Drawing.Color.LightGreen;
                    txtQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtBalQuantity.BackColor = System.Drawing.Color.LightGreen;
                    txtPOValueINR.BackColor = System.Drawing.Color.LightGreen;
                    txtBudget.BackColor = System.Drawing.Color.LightGreen;

                    txtPresentStatus.BackColor = System.Drawing.Color.LightGreen;
                    txtEdOfInspection.BackColor = System.Drawing.Color.LightGreen;
                    txtLastStatus.BackColor = System.Drawing.Color.LightGreen;
                    txtNextFollowupDate.BackColor = System.Drawing.Color.LightGreen;

                    //for (int i = 0; i < e.Row.Cells.Count; i++)
                    //{
                    //    e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    //}

                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else
                {
                    //headerSelect.Visible = true;
                    chkSelect.Visible = true;
                    ddlPostingStatus.Enabled = true;
                }


                if (lblPostingStatus.Text == "Close" && lblPostingStatus.Text != "Open")
                {
                    if (Convert.ToInt32(lblFollowUpDays.Text) > 14)
                    {
                        txtItemName.Enabled = true;
                        txtItemName.BackColor = System.Drawing.Color.Transparent;

                        txtPOFirstItem.BackColor = System.Drawing.Color.Transparent;
                        txtQuantity.BackColor = System.Drawing.Color.Transparent;
                        txtBalQuantity.BackColor = System.Drawing.Color.Transparent;
                        txtPOValueINR.BackColor = System.Drawing.Color.Transparent;
                        txtBudget.BackColor = System.Drawing.Color.Transparent;

                        txtPresentStatus.BackColor = System.Drawing.Color.Transparent;
                        txtEdOfInspection.BackColor = System.Drawing.Color.Transparent;
                        txtLastStatus.BackColor = System.Drawing.Color.Transparent;
                        txtNextFollowupDate.BackColor = System.Drawing.Color.Transparent;

                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Transparent;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(lblFollowUpDays.Text) > 14)
                    {
                        txtItemName.Enabled = true;
                        txtItemName.BackColor = System.Drawing.Color.LightSkyBlue;

                        txtPOFirstItem.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtQuantity.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtBalQuantity.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtPOValueINR.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtBudget.BackColor = System.Drawing.Color.LightSkyBlue;

                        txtPresentStatus.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtEdOfInspection.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtLastStatus.BackColor = System.Drawing.Color.LightSkyBlue;
                        txtNextFollowupDate.BackColor = System.Drawing.Color.LightSkyBlue;

                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.LightSkyBlue;
                        }
                    }
                }



                //if (!string.IsNullOrEmpty(Convert.ToString(lblPODeliveryDate.Text)) && !string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
                //{
                //    if (Convert.ToDateTime(lblPODeliveryDate.Text) < Convert.ToDateTime(txtEdOfInspection.Text))
                //    {
                //        txtPOFirstItem.BackColor = System.Drawing.Color.LightPink;
                //        txtQuantity.BackColor = System.Drawing.Color.LightPink;
                //        txtBalQuantity.BackColor = System.Drawing.Color.LightPink;
                //        txtPOValueINR.BackColor = System.Drawing.Color.LightPink;
                //        txtBudget.BackColor = System.Drawing.Color.LightPink;

                //        txtPresentStatus.BackColor = System.Drawing.Color.LightPink;
                //        txtEdOfInspection.BackColor = System.Drawing.Color.LightPink;
                //        txtLastStatus.BackColor = System.Drawing.Color.LightPink;
                //        txtNextFollowupDate.BackColor = System.Drawing.Color.LightPink;

                //        for (int i = 0; i < e.Row.Cells.Count; i++)
                //        {
                //            e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                //        }
                //    }
                //}

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

    protected void gvPODetailsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtRecQuantity = (TextBox)e.Row.FindControl("txtRecQuantity");
                TextBox txtBalQuantity = (TextBox)e.Row.FindControl("txtBalQuantity");

                totalQuantity += Convert.ToDouble(txtQuantity.Text);
                txtTotalQuantity.Text = Convert.ToString(totalQuantity);
                txtTotalQuantity.ForeColor = System.Drawing.Color.Green;


                totalRecQuantity += Convert.ToDouble(txtRecQuantity.Text);
                txtTotalRecQuantity.Text = Convert.ToString(totalRecQuantity);
                txtTotalRecQuantity.ForeColor = System.Drawing.Color.Green;

                totalBalQuantity += Convert.ToDouble(txtBalQuantity.Text);
                txtTotalBalQuantity.Text = Convert.ToString(totalBalQuantity);
                txtTotalBalQuantity.ForeColor = System.Drawing.Color.Green;
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

    protected void gvPOList_RowCommand(object sender, GridViewCommandEventArgs e)
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
                }



                Label lblPONo = gvPOList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblLocation = gvPOList.Rows[rowindex].FindControl("lblLocation") as Label;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    Session["dtPODetailList"] = null;
                    BindPODetails(Convert.ToString(lblPONo.Text), Convert.ToString(lblLocation.Text));
                    mpeDetail.Show();
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
        if (gvPOList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPOList"];
            ExportToExcel(dt);
            //ExportToExcelNew();
            //ExportToExcelNewOne(dt);
        }
    }

    protected void btnExportPODetails_Click(object sender, EventArgs e)
    {
        if (gvPODetailsList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtPODetailList"];
            ExportToExcelPODetails(dt);
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        HidePanel();
        if (gvPOList.Rows.Count > 0)
        {
            if (chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvPOList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    chkSelect.Checked = true;
                }
            }
            if (!chkSelectAll.Checked)
            {
                foreach (GridViewRow gr in gvPOList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    chkSelect.Checked = false;
                }
            }
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdPostingConfirmValue.Value) > 0)
        {
            DataTable dtUnitList = new DataTable();
            DataTable dtPMList = new DataTable();
            DataTable dtOthPMList = new DataTable();

            if (Session["dtPMList"] != null)
            {
                dtPMList = (DataTable)Session["dtPMList"];
            }

            if (Session["dtPMList"] != null)
            {
                dtOthPMList = (DataTable)Session["dtOthPMList"];
            }

            if (Session["dtUnitList"] != null)
            {
                dtUnitList = (DataTable)Session["dtUnitList"];
            }

            PostPOList(dtPMList, dtOthPMList, dtUnitList);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdDeletionConfirmValue.Value) > 0)
        {
            DeletePO();
        }
    }

    protected void txtDateOfApprovedDrawingSentToVendor_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtDateOfApprovedDrawingSentToVendor = (TextBox)sender;
        GridViewRow gr = (GridViewRow)txtDateOfApprovedDrawingSentToVendor.Parent.Parent;

        string finalDate = "";
        int weeks = 0;

        Label lblPODeliveryDate = (Label)gr.FindControl("lblPODeliveryDate");
        TextBox txtDeliveryTermsAgreedWithSupplierInWeeks = (TextBox)gr.FindControl("txtDeliveryTermsAgreedWithSupplierInWeeks");
        TextBox txtFinalDeliveryDateAsPerAgreedTerms = (TextBox)gr.FindControl("txtFinalDeliveryDateAsPerAgreedTerms");

        if (!string.IsNullOrEmpty(txtDeliveryTermsAgreedWithSupplierInWeeks.Text))
            weeks = Convert.ToInt32(txtDeliveryTermsAgreedWithSupplierInWeeks.Text);

        if (!string.IsNullOrEmpty(txtDateOfApprovedDrawingSentToVendor.Text) && weeks > 0)
            finalDate = Convert.ToDateTime(txtDateOfApprovedDrawingSentToVendor.Text).AddDays(weeks * 7).ToString("dd-MMM-yyyy");
        else finalDate = lblPODeliveryDate.Text;

        txtFinalDeliveryDateAsPerAgreedTerms.Text = finalDate;
    }

    protected void txtDeliveryTermsAgreedWithSupplierInWeeks_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtDeliveryTermsAgreedWithSupplierInWeeks = (TextBox)sender;
        GridViewRow gr = (GridViewRow)txtDeliveryTermsAgreedWithSupplierInWeeks.Parent.Parent;

        string finalDate = "";
        int weeks = 0;

        Label lblPODeliveryDate = (Label)gr.FindControl("lblPODeliveryDate");
        TextBox txtDateOfApprovedDrawingSentToVendor = (TextBox)gr.FindControl("txtDateOfApprovedDrawingSentToVendor");
        TextBox txtFinalDeliveryDateAsPerAgreedTerms = (TextBox)gr.FindControl("txtFinalDeliveryDateAsPerAgreedTerms");

        if (!string.IsNullOrEmpty(txtDeliveryTermsAgreedWithSupplierInWeeks.Text))
            weeks = Convert.ToInt32(txtDeliveryTermsAgreedWithSupplierInWeeks.Text);

        if (!string.IsNullOrEmpty(txtDateOfApprovedDrawingSentToVendor.Text) && weeks > 0)
            finalDate = Convert.ToDateTime(txtDateOfApprovedDrawingSentToVendor.Text).AddDays(weeks * 7).ToString("dd-MMM-yyyy");
        else finalDate = lblPODeliveryDate.Text;

        txtFinalDeliveryDateAsPerAgreedTerms.Text = finalDate;
    }

    #endregion


    #region METHODS[=========================]

    private void BindCreatedBy()
    {
        try
        {
            dsCreatedBy = objPurchase.GetMRCreatedBy();
            if (dsCreatedBy.Tables.Count > 0 && dsCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlMRCreatedBy.DataSource = dsCreatedBy.Tables[0];
                ddlMRCreatedBy.DataTextField = "MR_CREATED_BY";
                ddlMRCreatedBy.DataValueField = "MR_CREATED_BY";
                ddlMRCreatedBy.DataBind();
                ddlMRCreatedBy.Items.Insert(0, "ALL");
                ddlMRCreatedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCheckedBy()
    {
        try
        {
            dsCheckedBy = objPurchase.GetCheckedBy();
            if (dsCheckedBy.Tables.Count > 0 && dsCheckedBy.Tables[0].Rows.Count > 0)
            {
                ddlCheckedBy.DataSource = dsCheckedBy.Tables[0];
                ddlCheckedBy.DataTextField = "CHECKED_BY";
                ddlCheckedBy.DataValueField = "CHECKED_BY";
                ddlCheckedBy.DataBind();
                ddlCheckedBy.Items.Insert(0, "ALL");
                ddlCheckedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                Session["dtUnitList"] = dsUnit.Tables[0];
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
            else
            {
                Session["dtUnitList"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetImportModeList()
    {
        try
        {
            if (Session["dtImportModeList"] != null) return;

            dsImportMode = objPurchase.GetImportModeList();
            if (dsImportMode.Tables.Count > 0 && dsImportMode.Tables[0].Rows.Count > 0)
                Session["dtImportModeList"] = dsImportMode.Tables[0];
            else
                Session["dtImportModeList"] = null;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPOList()
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

            dateTypeID = Convert.ToInt32(ddlDateType.SelectedValue);

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

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text.ToUpper();
            else
                JOBNo = string.Empty;

            if (!string.IsNullOrEmpty(txtAmountOne.Text))
                amount1 = Convert.ToDouble(txtAmountOne.Text);
            else
                amount2 = 0;

            if (!string.IsNullOrEmpty(txtAmountTwo.Text))
                amount2 = Convert.ToDouble(txtAmountTwo.Text);
            else
                amount2 = 0;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            sign = Convert.ToString(ddlSign.SelectedItem.Text);

            if (chkExcludeCIDF.Checked)
                excludeCIDF = 1;
            else
                excludeCIDF = 0;

            if (chkExcludeEngineeringService.Checked)
                excludeEngineeringService = 1;
            else
                excludeEngineeringService = 0;

            if (ddlFollowUp.SelectedIndex > 0)
                isDoneOrPending = Convert.ToString(ddlFollowUp.SelectedValue);
            else
                isDoneOrPending = string.Empty;


            if (ddlFollowUpBy.SelectedIndex > 0)
                followupBy = Convert.ToString(ddlFollowUpBy.SelectedValue);
            else
                followupBy = string.Empty;


            if (!string.IsNullOrEmpty(Convert.ToString(hdFollowUpDateSearch.Value)))
                followupDate = Convert.ToDateTime(hdFollowUpDateSearch.Value).ToString("yyyy-MM-dd");
            else
                followupDate = string.Empty;


            if (ddlPostingStatusSearch.SelectedIndex > 0)
                postingStatus = Convert.ToString(ddlPostingStatusSearch.SelectedValue);
            else
                postingStatus = string.Empty;

            if (!string.IsNullOrEmpty(txtMRNo.Text))
                mrNo = Convert.ToString(txtMRNo.Text);
            else
                mrNo = string.Empty;

            if (ddlMRCreatedBy.SelectedIndex > 0)
                mrCreatedBy = Convert.ToString(ddlMRCreatedBy.SelectedValue);
            else
                mrCreatedBy = string.Empty;

            if (ddlCheckedBy.SelectedIndex > 0)
                checkedBy = Convert.ToString(ddlCheckedBy.SelectedValue);
            else
                checkedBy = string.Empty;

            dsPOList = objPurchase.GetPOListForPosting(dateTypeID, fromDate, toDate, dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ, poNo,
                                                        vendorName, unitName, JOBNo, amount1, amount2, sign, excludeCIDF, excludeEngineeringService,
                                                        isDoneOrPending, followupBy, followupDate, postingStatus, mrNo, mrCreatedBy, checkedBy);

            if (dsPOList.Tables.Count > 0 && dsPOList.Tables[0].Rows.Count > 0)
            {
                Session["dtPOList"] = dsPOList.Tables[0];
                gvPOList.DataSource = dsPOList.Tables[0];

                if (dsPOList.Tables.Count > 0 && dsPOList.Tables[1].Rows.Count > 0)
                {
                    Session["dtPMList"] = dsPOList.Tables[1];
                }

                if (dsPOList.Tables.Count > 0 && dsPOList.Tables[2].Rows.Count > 0)
                {
                    Session["dtOthPMList"] = dsPOList.Tables[2];
                }

                gvPOList.DataBind();
            }
            else
            {
                Session["dtPMList"] = null;
                Session["dtPOList"] = null;
                Session["dtOthPMList"] = null;
                gvPOList.DataSource = null;
                gvPOList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindPODetails(string PONo, string unitName)
    {
        try
        {
            dsPODetailList = objPurchase.GetPODetailList(PONo, unitName);
            if (dsPODetailList.Tables.Count > 0 && dsPODetailList.Tables[0].Rows.Count > 0)
            {
                Session["dtPODetailList"] = dsPODetailList.Tables[0];
                gvPODetailsList.DataSource = dsPODetailList.Tables[0];
                gvPODetailsList.DataBind();
            }
            else
            {
                Session["dtPODetailList"] = null;
            }
            lblPODetailRerords.Text = "Record[" + dsPODetailList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelNew()
    {
        try
        {
            string csv = string.Empty;

            csv = "SR_NO,";

            for (int i = 1; i < gvPOList.Columns.Count; i++)
            {
                if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "PO_DELIVERY_DATE")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",VENDOR_CODE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "UOM")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",SERVICE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "FOLLOW_UP_BY")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",MR_CREATED_BY,";
                }
                //else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "POSTING_STATUS")
                //{
                //    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",NEXT_FOLLOWUP_DATE,";
                //}
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",FOLLOW_UP_DAYS,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "SELECT")
                {
                    //
                }
                else
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ',';
                }

            }
            csv += "\r\n";

            int srNo = 0;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string poDeliveryDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            string itemName = string.Empty;
            string quantity = string.Empty;
            string balQuantity = string.Empty;
            string uom = string.Empty;
            string service = string.Empty;
            string poValueInr = string.Empty;
            string jobNo = string.Empty;
            string budget = string.Empty;
            string followUpBy = string.Empty;
            string mrCreatedBy = string.Empty;
            string location = string.Empty;
            string poFirstItem = string.Empty;
            string enggApprovalStatus = string.Empty;
            string presentStatus = string.Empty;
            string edOfInspComp = string.Empty;
            string postingStatus = string.Empty;
            string nextFollowupDate = string.Empty;
            string lastStatus = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string lastFollowUpDate = string.Empty;
            string oaReceived = string.Empty;
            string dateOfDrawingReceivedFromVendor = string.Empty;
            string dateOfApprovedDrawingSentToVendor = string.Empty;
            string followUpDays = string.Empty;


            foreach (GridViewRow gr in gvPOList.Rows)
            {
                srNo++;
                poNo = string.Empty;
                poDate = string.Empty;
                poDeliveryDate = string.Empty;
                vendorCode = string.Empty;
                vendorName = string.Empty;
                itemName = string.Empty;
                quantity = "0";
                balQuantity = "0";
                uom = string.Empty;
                service = string.Empty;
                poValueInr = "0";
                jobNo = string.Empty;
                budget = "0";
                followUpBy = string.Empty;
                mrCreatedBy = string.Empty;
                location = string.Empty;
                poFirstItem = string.Empty;
                enggApprovalStatus = string.Empty;
                presentStatus = string.Empty;
                edOfInspComp = string.Empty;
                postingStatus = string.Empty;
                nextFollowupDate = string.Empty;
                lastStatus = string.Empty;
                lastEdOfInspComp = string.Empty;
                lastFollowUpDate = string.Empty;
                oaReceived = string.Empty;
                dateOfDrawingReceivedFromVendor = string.Empty;
                dateOfApprovedDrawingSentToVendor = string.Empty;
                followUpDays = "0";


                Label lblPONo = gr.FindControl("lblPONo") as Label;
                Label lblPODate = gr.FindControl("lblPODate") as Label;
                Label lblPODeliveryDate = gr.FindControl("lblPODeliveryDate") as Label;
                Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                Label lblVendorName = gr.FindControl("lblVendorName") as Label;
                TextBox txtItemName = gr.FindControl("txtItemName") as TextBox;
                TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                TextBox txtBalQuantity = gr.FindControl("txtBalQuantity") as TextBox;
                Label lblUOM = gr.FindControl("lblUOM") as Label;
                //Label lblService = gr.FindControl("lblService") as Label;
                TextBox txtPOValueINR = gr.FindControl("txtPOValueINR") as TextBox;
                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                TextBox txtBudget = gr.FindControl("txtBudget") as TextBox;
                Label lblFollowUpBy = gr.FindControl("lblFollowUpBy") as Label;
                Label lblMrCreatedBy = gr.FindControl("lblMrCreatedBy") as Label;
                Label lblLocation = gr.FindControl("lblLocation") as Label;
                TextBox txtPOFirstItem = gr.FindControl("txtPOFirstItem") as TextBox;
                Label lblEnggApprovalStatus = gr.FindControl("lblEnggApprovalStatus") as Label;
                Label lblPresentStatus = gr.FindControl("lblPresentStatus") as Label;
                Label lblEdOfInspection = gr.FindControl("lblEdOfInspection") as Label;
                Label lblPostingStatus = gr.FindControl("lblPostingStatus") as Label;
                Label lblNextFollowupDate = gr.FindControl("lblNextFollowupDate") as Label;
                TextBox txtLastStatus = gr.FindControl("txtLastStatus") as TextBox;
                Label lblLastEdOfInspection = gr.FindControl("lblLastEdOfInspection") as Label;
                Label lblLastModifiedDate = gr.FindControl("lblLastModifiedDate") as Label;
                Label lblOAReceived = gr.FindControl("lblOAReceived") as Label;
                Label lblDateOfDrawingReceivedFromVendor = gr.FindControl("lblDateOfDrawingReceivedFromVendor") as Label;
                Label lblDateOfApprovedDrawingSentToVendor = gr.FindControl("lblDateOfApprovedDrawingSentToVendor") as Label;
                Label lblFollowUpDays = gr.FindControl("lblFollowUpDays") as Label;

                if (!string.IsNullOrEmpty(lblPONo.Text))
                    poNo = lblPONo.Text.Replace("\n", " ").Replace(",", "&").Trim();


                if (poNo == "AM220002")
                {

                }

                if (!string.IsNullOrEmpty(lblPODate.Text))
                    poDate = lblPODate.Text;

                if (!string.IsNullOrEmpty(lblPODeliveryDate.Text))
                    poDeliveryDate = lblPODeliveryDate.Text;

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    vendorCode = lblVendorCode.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    vendorName = lblVendorName.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtItemName.Text))
                    itemName = txtItemName.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Trim();

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    quantity = txtQuantity.Text;

                if (!string.IsNullOrEmpty(txtBalQuantity.Text))
                    balQuantity = txtBalQuantity.Text;

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    uom = lblUOM.Text.Replace("\n", " ").Replace(",", "&").Trim();

                //if (!string.IsNullOrEmpty(lblService.Text) && Convert.ToBoolean(lblService.Text) == true)
                //    service = "Yes";
                //else service = "No";

                if (!string.IsNullOrEmpty(txtPOValueINR.Text))
                    poValueInr = txtPOValueINR.Text;

                if (!string.IsNullOrEmpty(lblJOBNo.Text))
                    jobNo = lblJOBNo.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtBudget.Text))
                    budget = txtBudget.Text;

                if (!string.IsNullOrEmpty(lblFollowUpBy.Text))
                    followUpBy = lblFollowUpBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblMrCreatedBy.Text))
                    mrCreatedBy = lblMrCreatedBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    location = lblLocation.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtPOFirstItem.Text))
                    poFirstItem = txtPOFirstItem.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEnggApprovalStatus.Text))
                    enggApprovalStatus = lblEnggApprovalStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblPresentStatus.Text))
                    presentStatus = lblPresentStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEdOfInspection.Text))
                    edOfInspComp = lblEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblPostingStatus.Text))
                    postingStatus = lblPostingStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblNextFollowupDate.Text))
                    nextFollowupDate = lblNextFollowupDate.Text;

                if (!string.IsNullOrEmpty(txtLastStatus.Text))
                    lastStatus = txtLastStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblLastEdOfInspection.Text))
                    lastEdOfInspComp = lblLastEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblLastModifiedDate.Text))
                    lastFollowUpDate = lblLastModifiedDate.Text;

                if (!string.IsNullOrEmpty(lblOAReceived.Text))
                    oaReceived = lblOAReceived.Text;

                if (!string.IsNullOrEmpty(lblDateOfDrawingReceivedFromVendor.Text))
                    dateOfDrawingReceivedFromVendor = lblDateOfDrawingReceivedFromVendor.Text;

                if (!string.IsNullOrEmpty(lblDateOfApprovedDrawingSentToVendor.Text))
                    dateOfApprovedDrawingSentToVendor = lblDateOfApprovedDrawingSentToVendor.Text;

                if (!string.IsNullOrEmpty(lblFollowUpDays.Text))
                    followUpDays = lblFollowUpDays.Text;


                csv += srNo + "," +
                        poNo + "," +
                        poDate + "," +
                        poDeliveryDate + "," +
                        vendorCode + "," +
                        vendorName + "," +
                        itemName + "," +
                        quantity + "," +
                        balQuantity + "," +
                        uom + "," +
                        service + "," +
                        poValueInr + "," +
                        jobNo + "," +
                        budget + "," +
                        followUpBy + "," +
                        mrCreatedBy + "," +
                        location + "," +
                        poFirstItem + "," +
                        enggApprovalStatus + "," +
                        presentStatus + "," +
                        edOfInspComp + "," +
                        postingStatus + "," +
                        nextFollowupDate + "," +
                        lastStatus + "," +
                        lastEdOfInspComp + "," +
                        lastFollowUpDate + "," +
                        oaReceived + "," +
                        dateOfDrawingReceivedFromVendor + "," +
                        dateOfApprovedDrawingSentToVendor + "," +
                        followUpDays;


                csv += "\r\n";
            }

            string fileName = "Procurement_Status-Procurement_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExportToExcelNewOne(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            csv = "SR_NO,";

            for (int i = 1; i < gvPOList.Columns.Count; i++)
            {
                if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "PO_DELIVERY_DATE")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",VENDOR_CODE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "UOM")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",SERVICE,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "FOLLOW_UP_BY")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",MR_CREATED_BY,";
                }
                //else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "POSTING_STATUS")
                //{
                //    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",NEXT_FOLLOWUP_DATE,";
                //}
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR")
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ",FOLLOW_UP_DAYS,";
                }
                else if (Convert.ToString(gvPOList.Columns[i].HeaderText) == "SELECT")
                {
                    //
                }
                else
                {
                    csv += Convert.ToString(gvPOList.Columns[i].HeaderText) + ',';
                }

            }
            csv += "\r\n";

            int srNo = 0;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string poDeliveryDate = string.Empty;
            string vendorCode = string.Empty;
            string vendorName = string.Empty;
            string itemName = string.Empty;
            string quantity = string.Empty;
            string balQuantity = string.Empty;
            string uom = string.Empty;
            string service = string.Empty;
            string poValueInr = string.Empty;
            string jobNo = string.Empty;
            string budget = string.Empty;
            string followUpBy = string.Empty;
            string mrCreatedBy = string.Empty;
            string location = string.Empty;
            string poFirstItem = string.Empty;
            string enggApprovalStatus = string.Empty;
            string presentStatus = string.Empty;
            string edOfInspComp = string.Empty;
            string postingStatus = string.Empty;
            string nextFollowupDate = string.Empty;
            string lastStatus = string.Empty;
            string lastEdOfInspComp = string.Empty;
            string lastFollowUpDate = string.Empty;
            string oaReceived = string.Empty;
            string dateOfDrawingReceivedFromVendor = string.Empty;
            string dateOfApprovedDrawingSentToVendor = string.Empty;
            string followUpDays = string.Empty;


            foreach (GridViewRow gr in gvPOList.Rows)
            {
                srNo++;
                poNo = string.Empty;
                poDate = string.Empty;
                poDeliveryDate = string.Empty;
                vendorCode = string.Empty;
                vendorName = string.Empty;
                itemName = string.Empty;
                quantity = "0";
                balQuantity = "0";
                uom = string.Empty;
                service = string.Empty;
                poValueInr = "0";
                jobNo = string.Empty;
                budget = "0";
                followUpBy = string.Empty;
                mrCreatedBy = string.Empty;
                location = string.Empty;
                poFirstItem = string.Empty;
                enggApprovalStatus = string.Empty;
                presentStatus = string.Empty;
                edOfInspComp = string.Empty;
                postingStatus = string.Empty;
                nextFollowupDate = string.Empty;
                lastStatus = string.Empty;
                lastEdOfInspComp = string.Empty;
                lastFollowUpDate = string.Empty;
                oaReceived = string.Empty;
                dateOfDrawingReceivedFromVendor = string.Empty;
                dateOfApprovedDrawingSentToVendor = string.Empty;
                followUpDays = "0";


                Label lblPONo = gr.FindControl("lblPONo") as Label;
                Label lblPODate = gr.FindControl("lblPODate") as Label;
                Label lblPODeliveryDate = gr.FindControl("lblPODeliveryDate") as Label;
                Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                Label lblVendorName = gr.FindControl("lblVendorName") as Label;
                TextBox txtItemName = gr.FindControl("txtItemName") as TextBox;
                TextBox txtQuantity = gr.FindControl("txtQuantity") as TextBox;
                TextBox txtBalQuantity = gr.FindControl("txtBalQuantity") as TextBox;
                Label lblUOM = gr.FindControl("lblUOM") as Label;
                Label lblService = gr.FindControl("lblService") as Label;
                TextBox txtPOValueINR = gr.FindControl("txtPOValueINR") as TextBox;
                Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                TextBox txtBudget = gr.FindControl("txtBudget") as TextBox;
                Label lblFollowUpBy = gr.FindControl("lblFollowUpBy") as Label;
                Label lblMrCreatedBy = gr.FindControl("lblMrCreatedBy") as Label;
                Label lblLocation = gr.FindControl("lblLocation") as Label;
                TextBox txtPOFirstItem = gr.FindControl("txtPOFirstItem") as TextBox;
                Label lblEnggApprovalStatus = gr.FindControl("lblEnggApprovalStatus") as Label;
                Label lblPresentStatus = gr.FindControl("lblPresentStatus") as Label;
                Label lblEdOfInspection = gr.FindControl("lblEdOfInspection") as Label;
                Label lblPostingStatus = gr.FindControl("lblPostingStatus") as Label;
                Label lblNextFollowupDate = gr.FindControl("lblNextFollowupDate") as Label;
                TextBox txtLastStatus = gr.FindControl("txtLastStatus") as TextBox;
                Label lblLastEdOfInspection = gr.FindControl("lblLastEdOfInspection") as Label;
                Label lblLastModifiedDate = gr.FindControl("lblLastModifiedDate") as Label;
                Label lblOAReceived = gr.FindControl("lblOAReceived") as Label;
                Label lblDateOfDrawingReceivedFromVendor = gr.FindControl("lblDateOfDrawingReceivedFromVendor") as Label;
                Label lblDateOfApprovedDrawingSentToVendor = gr.FindControl("lblDateOfApprovedDrawingSentToVendor") as Label;
                Label lblFollowUpDays = gr.FindControl("lblFollowUpDays") as Label;

                if (!string.IsNullOrEmpty(lblPONo.Text))
                    poNo = lblPONo.Text.Replace("\n", " ").Replace(",", "&").Trim();


                if (poNo == "AM220002")
                {

                }

                if (!string.IsNullOrEmpty(lblPODate.Text))
                    poDate = lblPODate.Text;

                if (!string.IsNullOrEmpty(lblPODeliveryDate.Text))
                    poDeliveryDate = lblPODeliveryDate.Text;

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    vendorCode = lblVendorCode.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    vendorName = lblVendorName.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtItemName.Text))
                    itemName = txtItemName.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Trim();

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    quantity = txtQuantity.Text;

                if (!string.IsNullOrEmpty(txtBalQuantity.Text))
                    balQuantity = txtBalQuantity.Text;

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    uom = lblUOM.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblService.Text) && Convert.ToBoolean(lblService.Text) == true)
                    service = "Yes";
                else service = "No";

                if (!string.IsNullOrEmpty(txtPOValueINR.Text))
                    poValueInr = txtPOValueINR.Text;

                if (!string.IsNullOrEmpty(lblJOBNo.Text))
                    jobNo = lblJOBNo.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtBudget.Text))
                    budget = txtBudget.Text;

                if (!string.IsNullOrEmpty(lblFollowUpBy.Text))
                    followUpBy = lblFollowUpBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblMrCreatedBy.Text))
                    mrCreatedBy = lblMrCreatedBy.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(lblLocation.Text))
                    location = lblLocation.Text.Replace("\n", " ").Replace(",", "&").Trim();

                if (!string.IsNullOrEmpty(txtPOFirstItem.Text))
                    poFirstItem = txtPOFirstItem.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEnggApprovalStatus.Text))
                    enggApprovalStatus = lblEnggApprovalStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblPresentStatus.Text))
                    presentStatus = lblPresentStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblEdOfInspection.Text))
                    edOfInspComp = lblEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblPostingStatus.Text))
                    postingStatus = lblPostingStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblNextFollowupDate.Text))
                    nextFollowupDate = lblNextFollowupDate.Text;

                if (!string.IsNullOrEmpty(txtLastStatus.Text))
                    lastStatus = txtLastStatus.Text.Replace("\n", " ").Replace(",", "&").Replace("'", " Foot ").Replace("\"", " Inch ").Replace("Foot  Foot", "Foot").Replace("Inch  Inch", "Inch").Replace("\r", " ").Trim();

                if (!string.IsNullOrEmpty(lblLastEdOfInspection.Text))
                    lastEdOfInspComp = lblLastEdOfInspection.Text;

                if (!string.IsNullOrEmpty(lblLastModifiedDate.Text))
                    lastFollowUpDate = lblLastModifiedDate.Text;

                if (!string.IsNullOrEmpty(lblOAReceived.Text))
                    oaReceived = lblOAReceived.Text;

                if (!string.IsNullOrEmpty(lblDateOfDrawingReceivedFromVendor.Text))
                    dateOfDrawingReceivedFromVendor = lblDateOfDrawingReceivedFromVendor.Text;

                if (!string.IsNullOrEmpty(lblDateOfApprovedDrawingSentToVendor.Text))
                    dateOfApprovedDrawingSentToVendor = lblDateOfApprovedDrawingSentToVendor.Text;

                if (!string.IsNullOrEmpty(lblFollowUpDays.Text))
                    followUpDays = lblFollowUpDays.Text;


                csv += srNo + "," +
                        poNo + "," +
                        poDate + "," +
                        poDeliveryDate + "," +
                        vendorCode + "," +
                        vendorName + "," +
                        itemName + "," +
                        quantity + "," +
                        balQuantity + "," +
                        uom + "," +
                        service + "," +
                        poValueInr + "," +
                        jobNo + "," +
                        budget + "," +
                        followUpBy + "," +
                        mrCreatedBy + "," +
                        location + "," +
                        poFirstItem + "," +
                        enggApprovalStatus + "," +
                        presentStatus + "," +
                        edOfInspComp + "," +
                        postingStatus + "," +
                        nextFollowupDate + "," +
                        lastStatus + "," +
                        lastEdOfInspComp + "," +
                        lastFollowUpDate + "," +
                        oaReceived + "," +
                        dateOfDrawingReceivedFromVendor + "," +
                        dateOfApprovedDrawingSentToVendor + "," +
                        followUpDays;


                csv += "\r\n";
            }

            string fileName = "Procurement_Status-Procurement_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 1; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 1; k < dt.Columns.Count - 1; k++)
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


            string fileName = "Procurement_Status-Procurement_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcelPODetails(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
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


            string fileName = "PO_Detail_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void PostPOList(DataTable dtPMList, DataTable dtOthPMList, DataTable dtUnitList)
    {
        try
        {

            #region MyRegion TEMP DATATABLES

            DataTable dtPostedSrNo = new DataTable();
            dtPostedSrNo.Columns.Add("SR_NO", typeof(int));
            dtPostedSrNo.Columns.Add("PO_NO", typeof(string));

            DataTable dtTempPOListAttachment = new DataTable();
            dtTempPOListAttachment.Columns.Add("SR_NO", typeof(int));
            dtTempPOListAttachment.Columns.Add("PO_NO", typeof(string));
            dtTempPOListAttachment.Columns.Add("PO_DATE", typeof(string));
            dtTempPOListAttachment.Columns.Add("PO_DELIVERY_DATE", typeof(string));
            dtTempPOListAttachment.Columns.Add("VENDOR_CODE", typeof(string));
            dtTempPOListAttachment.Columns.Add("VENDOR_NAME", typeof(string));
            dtTempPOListAttachment.Columns.Add("ITEM_NAME", typeof(string));
            dtTempPOListAttachment.Columns.Add("JOB_NO", typeof(string));
            //dtTempPOList.Columns.Add("COMPLETE_JOB_NO", typeof(string));
            dtTempPOListAttachment.Columns.Add("MR_CREATED_BY", typeof(string));
            dtTempPOListAttachment.Columns.Add("FOLLOW_UP_BY", typeof(string));
            dtTempPOListAttachment.Columns.Add("LOCATION", typeof(string));
            dtTempPOListAttachment.Columns.Add("PO_FIRST_ITEM", typeof(string));
            dtTempPOListAttachment.Columns.Add("ENGG_APPROVAL_STATUS", typeof(string));
            dtTempPOListAttachment.Columns.Add("PRESENT_STATUS", typeof(string));
            dtTempPOListAttachment.Columns.Add("ED_OF_INSP_COMP", typeof(string));
            dtTempPOListAttachment.Columns.Add("NEXT_FOLLOWUP_DATE", typeof(string));

            dtTempPOListAttachment.Columns.Add("GAD_GIVEN_1", typeof(string));
            dtTempPOListAttachment.Columns.Add("GAD_RECEIVED_1", typeof(string));
            dtTempPOListAttachment.Columns.Add("GAD_GIVEN_2", typeof(string));
            dtTempPOListAttachment.Columns.Add("GAD_RECEIVED_2", typeof(string));
            dtTempPOListAttachment.Columns.Add("GAD_GIVEN_3", typeof(string));
            dtTempPOListAttachment.Columns.Add("GAD_RECEIVED_3", typeof(string));

            dtTempPOListAttachment.Columns.Add("OA_RECEIVED", typeof(string));
            dtTempPOListAttachment.Columns.Add("DATE_OF_DRAWING_RECEIVED_FROM_VENDOR", typeof(string));
            dtTempPOListAttachment.Columns.Add("DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR", typeof(string));
            dtTempPOListAttachment.Columns.Add("DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS", typeof(int));
            dtTempPOListAttachment.Columns.Add("FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS", typeof(string));
            dtTempPOListAttachment.Columns.Add("NON_NEGOTIABLE_OFFERED_PRICE", typeof(double));
            dtTempPOListAttachment.Columns.Add("CHECKED_BY", typeof(string));

            //dtTempPOListAttachment.Columns.Add("UNIT_ID", typeof(int));

            DataTable dtTempToInsert = new DataTable();
            dtTempToInsert.Columns.Add("PO_NO", typeof(string));
            dtTempToInsert.Columns.Add("PO_DATE", typeof(string));
            dtTempToInsert.Columns.Add("PO_DELIVERY_DATE", typeof(string));
            dtTempToInsert.Columns.Add("VENDOR_CODE", typeof(string));
            dtTempToInsert.Columns.Add("ITEM_NAME", typeof(string));
            dtTempToInsert.Columns.Add("QUANTITY", typeof(double));
            dtTempToInsert.Columns.Add("UOM", typeof(string));
            dtTempToInsert.Columns.Add("PO_VALUE_INR", typeof(double));
            dtTempToInsert.Columns.Add("JOB_NO", typeof(string));
            dtTempToInsert.Columns.Add("BUDGET", typeof(double));
            dtTempToInsert.Columns.Add("POSTING_STATUS", typeof(string));
            dtTempToInsert.Columns.Add("PO_FIRST_ITEM", typeof(string));
            dtTempToInsert.Columns.Add("ENGG_APPROVAL_STATUS", typeof(string));
            dtTempToInsert.Columns.Add("FOLLOW_UP_BY", typeof(string));
            dtTempToInsert.Columns.Add("PRESENT_STATUS", typeof(string));
            dtTempToInsert.Columns.Add("ED_OF_INSP_COMP", typeof(string));
            dtTempToInsert.Columns.Add("NEXT_FOLLOWUP_DATE", typeof(string));

            dtTempToInsert.Columns.Add("GAD_GIVEN_1", typeof(string));
            dtTempToInsert.Columns.Add("GAD_RECEIVED_1", typeof(string));
            dtTempToInsert.Columns.Add("GAD_GIVEN_2", typeof(string));
            dtTempToInsert.Columns.Add("GAD_RECEIVED_2", typeof(string));
            dtTempToInsert.Columns.Add("GAD_GIVEN_3", typeof(string));
            dtTempToInsert.Columns.Add("GAD_RECEIVED_3", typeof(string));



            dtTempToInsert.Columns.Add("OA_RECEIVED", typeof(string));
            dtTempToInsert.Columns.Add("DATE_OF_DRAWING_RECEIVED_FROM_VENDOR", typeof(string));
            dtTempToInsert.Columns.Add("DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR", typeof(string));
            dtTempToInsert.Columns.Add("DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS", typeof(int));
            dtTempToInsert.Columns.Add("FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS", typeof(string));
            dtTempToInsert.Columns.Add("UNIT_ID", typeof(int));
            dtTempToInsert.Columns.Add("IMPORT_MODE_ID", typeof(int));
            dtTempToInsert.Columns.Add("NON_NEGOTIABLE_OFFERED_PRICE", typeof(double));
            dtTempToInsert.Columns.Add("CHECKED_BY", typeof(string));

            DataTable dtTempInsertLog = new DataTable();
            dtTempInsertLog.Columns.Add("PO_POSTED_RECORD_ID", typeof(int));
            dtTempInsertLog.Columns.Add("OLD_ENGG_APPROVAL_STATUS", typeof(string));
            dtTempInsertLog.Columns.Add("OLD_PRESENT_STATUS", typeof(string));
            dtTempInsertLog.Columns.Add("OLD_ED_OF_INSP_COMP", typeof(string));
            dtTempInsertLog.Columns.Add("NEW_ENGG_APPROVAL_STATUS", typeof(string));
            dtTempInsertLog.Columns.Add("NEW_PRESENT_STATUS", typeof(string));
            dtTempInsertLog.Columns.Add("NEW_ED_OF_INSP_COMP", typeof(string));
            dtTempInsertLog.Columns.Add("NON_NEGOTIABLE_OFFERED_PRICE", typeof(double));
            dtTempInsertLog.Columns.Add("CHECKED_BY", typeof(string));

            #endregion

            int srNo = 0;
            int recordID = 0;
            string PONo = string.Empty;
            string PODate = string.Empty;
            string PODeliveryDate = string.Empty;
            string vendorCode = string.Empty;
            string itemName = string.Empty;

            double quantity = 0;
            string UOM = string.Empty;
            //string currencyCode = string.Empty;
            //double FCAmount = 0;
            double POValueINR = 0;
            string JOBNo = string.Empty;
            double budget = 0;
            string POPostingStatus = string.Empty;
            string POFirstItem = string.Empty;
            string enggApprovalStatus = string.Empty;
            string followUpBy = string.Empty;
            string presentStatus = string.Empty;
            string EDOfInspection = string.Empty;
            string nextFollowupDate = string.Empty;

            string gadrec1 = string.Empty;
            string gadsent1 = string.Empty;
            string gadrec2 = string.Empty;
            string gadsent2 = string.Empty;
            string gadrec3 = string.Empty;
            string gadsent3 = string.Empty;

            string onReceived = string.Empty;
            string dateOfDrawingReceivedFromVendor = string.Empty;
            string dateOfApprovedDrawingSentToVendor = string.Empty;
            int deliveryTermsAgreedWithSupplierInWeeks = 0;
            string finalDeliveryDateAsPerAgreedTerms = string.Empty;
            int unitId = 0;
            int importModeId = 0;
            double nonNegotiableOfferedPrice = 0;
            string checkedBy = string.Empty;
            string tableName = string.Empty;


            string oldEnggApprovalStatus = string.Empty;
            string oldPresentStatus = string.Empty;
            string oldEDOfInspection = string.Empty;
            string updateQuery = string.Empty;

            int count = 0;

            if (gvPOList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPOList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {
                        count++;


                        Label lblSrNo = (Label)gr.FindControl("lblSrNo");
                        Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                        Label lblPONo = (Label)gr.FindControl("lblPONo");
                        Label lblPODate = (Label)gr.FindControl("lblPODate");
                        Label lblPODeliveryDate = (Label)gr.FindControl("lblPODeliveryDate");
                        Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                        TextBox txtItemName = (TextBox)gr.FindControl("txtItemName");
                        TextBox txtQuantity = (TextBox)gr.FindControl("txtQuantity");
                        Label lblUOM = (Label)gr.FindControl("lblUOM");
                        TextBox txtPOValueINR = (TextBox)gr.FindControl("txtPOValueINR");
                        Label lblJOBNo = (Label)gr.FindControl("lblJOBNo");
                        TextBox txtBudget = (TextBox)gr.FindControl("txtBudget");
                        DropDownList ddlPostingStatus = (DropDownList)gr.FindControl("ddlPostingStatus");
                        TextBox txtPOFirstItem = (TextBox)gr.FindControl("txtPOFirstItem");
                        Label lblFollowUpBy = (Label)gr.FindControl("lblFollowUpBy");
                        Label lblEnggApprovalStatus = (Label)gr.FindControl("lblEnggApprovalStatus");
                        DropDownList ddlEnggApprovalStatus = (DropDownList)gr.FindControl("ddlEnggApprovalStatus");
                        Label lblLastWeekStatus = (Label)gr.FindControl("lblLastWeekStatus");
                        Label lblLastWeekEdOfInspection = (Label)gr.FindControl("lblLastWeekEdOfInspection");
                        Label lblPresentStatus = (Label)gr.FindControl("lblPresentStatus");
                        Label lblEdOfInspection = (Label)gr.FindControl("lblEdOfInspection");
                        TextBox txtPresentStatus = (TextBox)gr.FindControl("txtPresentStatus");
                        TextBox txtEdOfInspection = (TextBox)gr.FindControl("txtEdOfInspection");
                        TextBox txtNextFollowupDate = (TextBox)gr.FindControl("txtNextFollowupDate");

                        TextBox txtGADsent1 = (TextBox)gr.FindControl("txtGADsent1");
                        TextBox txtGADrec1 = (TextBox)gr.FindControl("txtGADrec1");
                        TextBox txtGADsent2 = (TextBox)gr.FindControl("txtGADsent2");
                        TextBox txtGADrec2 = (TextBox)gr.FindControl("txtGADrec2");
                        TextBox txtGADsent3 = (TextBox)gr.FindControl("txtGADsent3");
                        TextBox txtGADrec3 = (TextBox)gr.FindControl("txtGADrec3");

                        TextBox txtOnReceived = (TextBox)gr.FindControl("txtOnReceived");
                        TextBox txtDateOfDrawingReceivedFromVendor = (TextBox)gr.FindControl("txtDateOfDrawingReceivedFromVendor");
                        TextBox txtDateOfApprovedDrawingSentToVendor = (TextBox)gr.FindControl("txtDateOfApprovedDrawingSentToVendor");
                        TextBox txtDeliveryTermsAgreedWithSupplierInWeeks = (TextBox)gr.FindControl("txtDeliveryTermsAgreedWithSupplierInWeeks");
                        TextBox txtFinalDeliveryDateAsPerAgreedTerms = (TextBox)gr.FindControl("txtFinalDeliveryDateAsPerAgreedTerms");
                        Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                        DropDownList ddlImportMode = (DropDownList)gr.FindControl("ddlImportMode");
                        TextBox txtNonNegotiableOfferedPrice = (TextBox)gr.FindControl("txtNonNegotiableOfferedPrice");
                        Label lblCheckedBy = (Label)gr.FindControl("lblCheckedBy");


                        Label lblTableName = (Label)gr.FindControl("lblTableName");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblSrNo.Text)) && Convert.ToInt32(lblSrNo.Text) > 0)
                            srNo = Convert.ToInt32(lblSrNo.Text);
                        else
                            srNo = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)) && Convert.ToInt32(lblRecordID.Text) > 0)
                            recordID = Convert.ToInt32(lblRecordID.Text);
                        else
                            recordID = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text).Trim()))
                            PONo = Convert.ToString(lblPONo.Text).Trim();
                        else
                            PONo = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPODate.Text).Trim()))
                            PODate = Convert.ToDateTime(lblPODate.Text).ToString("yyyy-MM-dd");
                        else
                            PODate = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPODeliveryDate.Text).Trim()))
                            PODeliveryDate = Convert.ToDateTime(lblPODeliveryDate.Text).ToString("yyyy-MM-dd");
                        else
                            PODeliveryDate = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblVendorCode.Text).Trim()))
                            vendorCode = Convert.ToString(lblVendorCode.Text).Trim();
                        else
                            vendorCode = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtItemName.Text).Trim()))
                        {
                            itemName = Convert.ToString(txtItemName.Text);
                            //txtItemName.BackColor = System.Drawing.Color.LightYellow;
                        }
                        else
                        {
                            itemName = string.Empty;
                            //txtItemName.Focus();
                            //txtItemName.BackColor = System.Drawing.Color.LightPink;
                            //ExceptionMessage("Please enter item name...!!!");
                            //return;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtQuantity.Text).Trim()))
                            quantity = Convert.ToDouble(txtQuantity.Text);
                        else
                            quantity = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblUOM.Text).Trim()))
                            UOM = Convert.ToString(lblUOM.Text);
                        else
                            UOM = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtPOValueINR.Text).Trim()))
                            POValueINR = Convert.ToDouble(txtPOValueINR.Text);
                        else
                            POValueINR = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                            JOBNo = Convert.ToString(lblJOBNo.Text);
                        else
                            JOBNo = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtBudget.Text).Trim()))
                            budget = Convert.ToDouble(txtBudget.Text);
                        else
                            budget = 0;

                        POPostingStatus = Convert.ToString(ddlPostingStatus.SelectedValue);

                        if (!string.IsNullOrEmpty(Convert.ToString(txtPOFirstItem.Text)))
                            POFirstItem = Convert.ToString(txtPOFirstItem.Text);
                        else
                            POFirstItem = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblEnggApprovalStatus.Text).Trim()))
                            oldEnggApprovalStatus = Convert.ToString(lblEnggApprovalStatus.Text);
                        else
                            oldEnggApprovalStatus = string.Empty;

                        enggApprovalStatus = Convert.ToString(ddlEnggApprovalStatus.SelectedValue);

                        if (!string.IsNullOrEmpty(Convert.ToString(lblFollowUpBy.Text).Trim()))
                            followUpBy = Convert.ToString(lblFollowUpBy.Text);
                        else
                            followUpBy = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPresentStatus.Text).Trim()))
                            oldPresentStatus = Convert.ToString(lblPresentStatus.Text);
                        else
                            oldPresentStatus = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblEdOfInspection.Text).Trim()))
                            oldEDOfInspection = Convert.ToString(lblEdOfInspection.Text);
                        else
                            oldEDOfInspection = string.Empty;

                       

                        if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
                        {
                            presentStatus = Convert.ToString(txtPresentStatus.Text);
                            txtPresentStatus.BackColor = System.Drawing.Color.LightYellow;
                        }
                        else
                        {
                            txtPresentStatus.BackColor = System.Drawing.Color.LightPink;
                            ExceptionMessage("Please enter present status...!!!");
                            presentStatus = string.Empty;
                            return;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text)))
                        {
                            EDOfInspection = Convert.ToDateTime(txtEdOfInspection.Text).ToString("yyyy-MM-dd");
                            txtEdOfInspection.BackColor = System.Drawing.Color.LightYellow;
                            //if (!string.IsNullOrEmpty(oldEDOfInspection))
                            //{
                            //    if (Convert.ToDateTime(EDOfInspection).Date < Convert.ToDateTime(oldEDOfInspection).Date)
                            //    {
                            //        ExceptionMessage("Present ED of Insp/Comp date must be greater or equal to last ED of Insp/Comp date...!!!");
                            //        EDOfInspection = string.Empty;
                            //        return;
                            //    }
                            //}
                        }
                        else
                        {
                            txtEdOfInspection.BackColor = System.Drawing.Color.LightPink;
                            ExceptionMessage("Please enter ED of Insp/Comp date...!!!");
                            EDOfInspection = string.Empty;
                            return;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtNextFollowupDate.Text).Trim()))
                            nextFollowupDate = Convert.ToString(txtNextFollowupDate.Text);
                        else
                            nextFollowupDate = string.Empty;


                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent1.Text).Trim()))
                            gadsent1 = Convert.ToString(txtGADsent1.Text);
                        else
                            gadsent1 = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec1.Text).Trim()))
                            gadrec1 = Convert.ToString(txtGADrec1.Text);
                        else
                            gadrec1 = string.Empty;

                        // GAD 2
                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent2.Text).Trim()))
                            gadsent2 = Convert.ToString(txtGADsent2.Text);
                        else
                            gadsent2 = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec2.Text).Trim()))
                            gadrec2 = Convert.ToString(txtGADrec2.Text);
                        else
                            gadrec2 = string.Empty;

                        // GAD 3
                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent3.Text).Trim()))
                            gadsent3 = Convert.ToString(txtGADsent3.Text);
                        else
                            gadsent3 = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec3.Text).Trim()))
                            gadrec3 = Convert.ToString(txtGADrec3.Text);
                        else
                            gadrec3 = string.Empty;



                        if (!string.IsNullOrEmpty(Convert.ToString(txtOnReceived.Text).Trim()))
                            onReceived = Convert.ToDateTime(txtOnReceived.Text).ToString("yyyy-MM-dd");
                        else
                            onReceived = string.Empty;


                        if (!string.IsNullOrEmpty(Convert.ToString(txtDateOfDrawingReceivedFromVendor.Text).Trim()))
                            dateOfDrawingReceivedFromVendor = Convert.ToDateTime(txtDateOfDrawingReceivedFromVendor.Text).ToString("yyyy-MM-dd");
                        else
                            dateOfDrawingReceivedFromVendor = string.Empty;


                        if (!string.IsNullOrEmpty(Convert.ToString(txtDateOfApprovedDrawingSentToVendor.Text).Trim()))
                            dateOfApprovedDrawingSentToVendor = Convert.ToDateTime(txtDateOfApprovedDrawingSentToVendor.Text).ToString("yyyy-MM-dd");
                        else
                            dateOfApprovedDrawingSentToVendor = string.Empty;


                        if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryTermsAgreedWithSupplierInWeeks.Text).Trim()))
                            deliveryTermsAgreedWithSupplierInWeeks = Convert.ToInt32(txtDeliveryTermsAgreedWithSupplierInWeeks.Text);
                        else
                            deliveryTermsAgreedWithSupplierInWeeks = 0;


                        //if (!string.IsNullOrEmpty(Convert.ToString(txtFinalDeliveryDateAsPerAgreedTerms.Text).Trim()))
                        //    finalDeliveryDateAsPerAgreedTerms = Convert.ToDateTime(txtFinalDeliveryDateAsPerAgreedTerms.Text).ToString("yyyy-MM-dd");
                        //else finalDeliveryDateAsPerAgreedTerms = string.Empty;

                        if (!string.IsNullOrEmpty(dateOfApprovedDrawingSentToVendor) && deliveryTermsAgreedWithSupplierInWeeks > 0)
                            finalDeliveryDateAsPerAgreedTerms = Convert.ToDateTime(dateOfApprovedDrawingSentToVendor)
                                .AddDays(deliveryTermsAgreedWithSupplierInWeeks * 7).ToString("yyyy-MM-dd");
                        else finalDeliveryDateAsPerAgreedTerms = Convert.ToDateTime(lblPODeliveryDate.Text).ToString("yyyy-MM-dd");


                        //if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryTermsAgreedWithSupplierInWeeks.Text).Trim()) &&
                        //    string.IsNullOrEmpty(Convert.ToString(txtDateOfApprovedDrawingSentToVendor.Text).Trim()))
                        //{
                        //    txtDateOfApprovedDrawingSentToVendor.Focus();
                        //    ExceptionMessage("Please enter Date Of Approved Drawing Sent To Vendor");
                        //    return;
                        //}

                        if (Convert.ToInt32(lblUnitID.Text) > 0)
                            unitId = Convert.ToInt32(lblUnitID.Text);
                        else
                            unitId = 0;


                        //if (Convert.ToInt32(ddlImportMode.SelectedIndex) > 0)
                        //    importModeId = Convert.ToInt32(ddlImportMode.SelectedValue);
                        //else
                        //    importModeId = 0;

                        importModeId = Convert.ToInt32(ddlImportMode.SelectedValue);


                        if (!string.IsNullOrEmpty(Convert.ToString(txtNonNegotiableOfferedPrice.Text)) &&
                            Convert.ToDouble(txtNonNegotiableOfferedPrice.Text) > 0)
                            nonNegotiableOfferedPrice = Convert.ToDouble(txtNonNegotiableOfferedPrice.Text);
                        else
                            nonNegotiableOfferedPrice = 0;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblCheckedBy.Text).Trim()))
                            checkedBy = Convert.ToString(lblCheckedBy.Text).Trim();
                        else
                            checkedBy = string.Empty;

                        if (!string.IsNullOrEmpty(Convert.ToString(lblTableName.Text).Trim()))
                            tableName = Convert.ToString(lblTableName.Text).Trim();
                        else
                            tableName = string.Empty;


                        if (recordID > 0)
                        {
                            //insert into log table

                            DataRow drn2 = dtTempInsertLog.NewRow();
                            drn2["PO_POSTED_RECORD_ID"] = recordID;
                            drn2["OLD_ENGG_APPROVAL_STATUS"] = oldEnggApprovalStatus;
                            drn2["OLD_PRESENT_STATUS"] = oldPresentStatus;
                            drn2["OLD_ED_OF_INSP_COMP"] = oldEDOfInspection;
                            drn2["NEW_ENGG_APPROVAL_STATUS"] = enggApprovalStatus;
                            drn2["NEW_PRESENT_STATUS"] = presentStatus;
                            drn2["NEW_ED_OF_INSP_COMP"] = EDOfInspection;
                            drn2["NON_NEGOTIABLE_OFFERED_PRICE"] = nonNegotiableOfferedPrice;
                            drn2["CHECKED_BY"] = checkedBy;

                            dtTempInsertLog.Rows.Add(drn2);


                            //update main table
                            updateQuery += "UPDATE " + tableName + " SET ENGG_APPROVAL_STATUS='" + enggApprovalStatus + "', " +
                                                                        "POSTING_STATUS='" + POPostingStatus + "', " +
                                                                        "PRESENT_STATUS='" + presentStatus + "', " +
                                                                        "ED_OF_INSP_COMP='" + EDOfInspection + "', " +
                                                                        "NEXT_FOLLOWUP_DATE='" + nextFollowupDate + "', " +

                                                                        "GAD_GIVEN_1='" + gadsent1 + "', " +
                                                                        "GAD_RECEIVED_1='" + gadrec1 + "', " +
                                                                        "GAD_GIVEN_2='" + gadsent2 + "', " +
                                                                        "GAD_RECEIVED_2='" + gadrec2 + "', " +
                                                                        "GAD_GIVEN_3='" + gadsent3 + "', " +
                                                                        "GAD_RECEIVED_3='" + gadrec3 + "', " +


                                                                        "OA_RECEIVED='" + onReceived + "', " +
                                                                        "DATE_OF_DRAWING_RECEIVED_FROM_VENDOR='" + dateOfDrawingReceivedFromVendor + "', " +
                                                                        "DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR='" + dateOfApprovedDrawingSentToVendor + "', " +

                                                                        "DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS=" + deliveryTermsAgreedWithSupplierInWeeks + ", " +
                                                                        "FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS='" + finalDeliveryDateAsPerAgreedTerms + "', " +
                                                                        "UNIT_ID=" + unitId + ", " +
                                                                        "IMPORT_MODE_ID=" + importModeId + ", " +
                                                                        "NON_NEGOTIABLE_OFFERED_PRICE= '" + nonNegotiableOfferedPrice + "', " +
                                                                        "CHECKED_BY= '" + checkedBy + "', " +


                                                                        "MODIFIED_BY=" + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ", " +
                                                                        "MODIFIED_ON=GETDATE() " +
                                                                        "WHERE RECORD_ID=" + recordID + ";" + Environment.NewLine;


                            DataRow drp = dtPostedSrNo.NewRow();
                            drp["SR_NO"] = srNo;
                            drp["PO_NO"] = PONo;
                            dtPostedSrNo.Rows.Add(drp);

                        }
                        else
                        {
                            DataRow drn1 = dtTempToInsert.NewRow();

                            drn1["PO_NO"] = PONo;
                            drn1["PO_DATE"] = PODate;
                            drn1["PO_DELIVERY_DATE"] = PODeliveryDate;
                            drn1["VENDOR_CODE"] = vendorCode;
                            drn1["ITEM_NAME"] = itemName;
                            drn1["QUANTITY"] = quantity;
                            drn1["UOM"] = UOM;
                            drn1["PO_VALUE_INR"] = POValueINR;
                            drn1["JOB_NO"] = JOBNo;
                            drn1["BUDGET"] = budget;
                            drn1["POSTING_STATUS"] = POPostingStatus;
                            drn1["PO_FIRST_ITEM"] = POFirstItem;
                            drn1["ENGG_APPROVAL_STATUS"] = enggApprovalStatus;
                            drn1["FOLLOW_UP_BY"] = followUpBy;
                            drn1["PRESENT_STATUS"] = presentStatus;
                            drn1["ED_OF_INSP_COMP"] = EDOfInspection;
                            drn1["NEXT_FOLLOWUP_DATE"] = nextFollowupDate;


                            drn1["GAD_GIVEN_1"] = gadsent1;
                            drn1["GAD_RECEIVED_1"] = gadrec1;
                            drn1["GAD_GIVEN_2"] = gadsent2;
                            drn1["GAD_RECEIVED_2"] = gadrec1;
                            drn1["GAD_GIVEN_3"] = gadsent3;
                            drn1["GAD_RECEIVED_3"] = gadrec1;

                            drn1["OA_RECEIVED"] = onReceived;
                            drn1["DATE_OF_DRAWING_RECEIVED_FROM_VENDOR"] = dateOfDrawingReceivedFromVendor;
                            drn1["DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR"] = dateOfApprovedDrawingSentToVendor;

                            drn1["DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS"] = deliveryTermsAgreedWithSupplierInWeeks;
                            drn1["FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS"] = finalDeliveryDateAsPerAgreedTerms;
                            drn1["UNIT_ID"] = unitId;
                            drn1["IMPORT_MODE_ID"] = importModeId;
                            drn1["NON_NEGOTIABLE_OFFERED_PRICE"] = nonNegotiableOfferedPrice;
                            drn1["CHECKED_BY"] = checkedBy;

                            dtTempToInsert.Rows.Add(drn1);

                            DataRow drp = dtPostedSrNo.NewRow();
                            drp["SR_NO"] = srNo;
                            drp["PO_NO"] = PONo;
                            dtPostedSrNo.Rows.Add(drp);

                        }
                    }
                }

                int srCount = 0;

                if (count > 0)
                {
                    if (!string.IsNullOrEmpty(updateQuery))
                        updateQuery = updateQuery.TrimEnd('\n', ' ');

                    int value = objPurchase.PostPOList(dtTempToInsert, dtTempInsertLog, updateQuery, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {
                        if (dtPostedSrNo.Rows.Count > 0)
                        {
                            foreach (DataRow drp in dtPostedSrNo.Rows)
                            {
                                foreach (GridViewRow gr in gvPOList.Rows)
                                {
                                    Label lblSrNo = gr.FindControl("lblSrNo") as Label;
                                    Label lblPONo = gr.FindControl("lblPONo") as Label;
                                    Label lblPODate = gr.FindControl("lblPODate") as Label;
                                    Label lblPODeliveryDate = gr.FindControl("lblPODeliveryDate") as Label;
                                    Label lblVendorCode = gr.FindControl("lblVendorCode") as Label;
                                    Label lblVendorName = gr.FindControl("lblVendorName") as Label;
                                    TextBox txtItemName = gr.FindControl("txtItemName") as TextBox;
                                    Label lblJOBNo = gr.FindControl("lblJOBNo") as Label;
                                    Label lblMRCreatedBy = gr.FindControl("lblMRCreatedBy") as Label;
                                    Label lblFollowUpBy = gr.FindControl("lblFollowUpBy") as Label;
                                    Label lblLocation = gr.FindControl("lblLocation") as Label;
                                    TextBox txtPOFirstItem = gr.FindControl("txtPOFirstItem") as TextBox;
                                    DropDownList ddlEnggApprovalStatus = gr.FindControl("ddlEnggApprovalStatus") as DropDownList;
                                    TextBox txtPresentStatus = gr.FindControl("txtPresentStatus") as TextBox;
                                    TextBox txtEdOfInspection = gr.FindControl("txtEdOfInspection") as TextBox;
                                    TextBox txtNextFollowupDate = gr.FindControl("txtNextFollowupDate") as TextBox;

                                    TextBox txtGADsent1 = gr.FindControl("txtGADsent1") as TextBox;
                                    TextBox txtGADrec1 = gr.FindControl("txtGADrec1") as TextBox;
                                    TextBox txtGADsent2 = gr.FindControl("txtGADsent2") as TextBox;
                                    TextBox txtGADrec2 = gr.FindControl("txtGADrec2") as TextBox;
                                    TextBox txtGADsent3 = gr.FindControl("txtGADsent3") as TextBox;
                                    TextBox txtGADrec3 = gr.FindControl("txtGADrec3") as TextBox;


                                    TextBox txtOnReceived = gr.FindControl("txtOnReceived") as TextBox;
                                    TextBox txtDateOfDrawingReceivedFromVendor = gr.FindControl("txtDateOfDrawingReceivedFromVendor") as TextBox;
                                    TextBox txtDateOfApprovedDrawingSentToVendor = gr.FindControl("txtDateOfApprovedDrawingSentToVendor") as TextBox;
                                    TextBox txtDeliveryTermsAgreedWithSupplierInWeeks = gr.FindControl("txtDeliveryTermsAgreedWithSupplierInWeeks") as TextBox;
                                    TextBox txtFinalDeliveryDateAsPerAgreedTerms = gr.FindControl("txtFinalDeliveryDateAsPerAgreedTerms") as TextBox;
                                    TextBox txtNonNegotiableOfferedPrice = gr.FindControl("txtNonNegotiableOfferedPrice") as TextBox;
                                    Label lblCheckedBy = gr.FindControl("lblCheckedBy") as Label;


                                    if ((Convert.ToInt32(lblSrNo.Text) == Convert.ToInt32(drp["SR_NO"])) && (Convert.ToString(lblPONo.Text).Trim() == Convert.ToString(drp["PO_NO"]).Trim()))
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(lblPODeliveryDate.Text)) && Convert.ToDateTime(txtEdOfInspection.Text) > Convert.ToDateTime(lblPODeliveryDate.Text))
                                        {
                                            srCount++;

                                            DataRow drPOn = dtTempPOListAttachment.NewRow();

                                            drPOn["SR_NO"] = srCount;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text).Trim()))
                                                drPOn["PO_NO"] = Convert.ToString(lblPONo.Text).Trim();
                                            else drPOn["PO_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblPODate.Text).Trim()))
                                                drPOn["PO_DATE"] = Convert.ToString(lblPODate.Text).Trim();
                                            else drPOn["PO_DATE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblPODeliveryDate.Text).Trim()))
                                                drPOn["PO_DELIVERY_DATE"] = Convert.ToString(lblPODeliveryDate.Text).Trim();
                                            else drPOn["PO_DELIVERY_DATE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblVendorCode.Text).Trim()))
                                                drPOn["VENDOR_CODE"] = Convert.ToString(lblVendorCode.Text).Trim();
                                            else drPOn["VENDOR_CODE"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblVendorName.Text).Trim()))
                                                drPOn["VENDOR_NAME"] = Convert.ToString(lblVendorName.Text).Trim();
                                            else drPOn["VENDOR_NAME"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtItemName.Text).Trim()))
                                                drPOn["ITEM_NAME"] = Convert.ToString(txtItemName.Text).Trim();
                                            else drPOn["ITEM_NAME"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblJOBNo.Text).Trim()))
                                                drPOn["JOB_NO"] = Convert.ToString(lblJOBNo.Text).Trim();
                                            else drPOn["PO_NO"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblMRCreatedBy.Text).Trim()))
                                                drPOn["MR_CREATED_BY"] = Convert.ToString(lblMRCreatedBy.Text).Trim();
                                            else drPOn["MR_CREATED_BY"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblFollowUpBy.Text).Trim()))
                                                drPOn["FOLLOW_UP_BY"] = Convert.ToString(lblFollowUpBy.Text).Trim();
                                            else drPOn["FOLLOW_UP_BY"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(lblLocation.Text).Trim()))
                                                drPOn["LOCATION"] = Convert.ToString(lblLocation.Text).Trim();
                                            else drPOn["LOCATION"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtPOFirstItem.Text)))
                                                drPOn["PO_FIRST_ITEM"] = Convert.ToString(txtPOFirstItem.Text);
                                            else drPOn["PO_FIRST_ITEM"] = string.Empty;

                                            drPOn["ENGG_APPROVAL_STATUS"] = Convert.ToString(ddlEnggApprovalStatus.SelectedValue);

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtPresentStatus.Text).Trim()))
                                                drPOn["PRESENT_STATUS"] = Convert.ToString(txtPresentStatus.Text).Trim();
                                            else drPOn["PRESENT_STATUS"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtEdOfInspection.Text).Trim()))
                                                drPOn["ED_OF_INSP_COMP"] = Convert.ToString(txtEdOfInspection.Text).Trim();
                                            else drPOn["ED_OF_INSP_COMP"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtNextFollowupDate.Text).Trim()))
                                                drPOn["NEXT_FOLLOWUP_DATE"] = Convert.ToString(txtNextFollowupDate.Text).Trim();
                                            else drPOn["NEXT_FOLLOWUP_DATE"] = string.Empty;


                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent1.Text).Trim()))
                                                drPOn["GAD_GIVEN_1"] = Convert.ToString(txtGADsent1.Text).Trim();
                                            else drPOn["GAD_GIVEN_1"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec1.Text).Trim()))
                                                drPOn["GAD_RECEIVED_1"] = Convert.ToString(txtGADrec1.Text).Trim();
                                            else drPOn["GAD_RECEIVED_1"] = string.Empty;

                                            // GAD 2
                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent2.Text).Trim()))
                                                drPOn["GAD_GIVEN_2"] = Convert.ToString(txtGADsent2.Text).Trim();
                                            else drPOn["GAD_GIVEN_2"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec2.Text).Trim()))
                                                drPOn["GAD_RECEIVED_2"] = Convert.ToString(txtGADrec2.Text).Trim();
                                            else drPOn["GAD_RECEIVED_2"] = string.Empty;

                                            // GAD 3
                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADsent3.Text).Trim()))
                                                drPOn["GAD_GIVEN_3"] = Convert.ToString(txtGADsent3.Text).Trim();
                                            else drPOn["GAD_GIVEN_3"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtGADrec3.Text).Trim()))
                                                drPOn["GAD_RECEIVED_3"] = Convert.ToString(txtGADrec3.Text).Trim();
                                            else drPOn["GAD_RECEIVED_3"] = string.Empty;



                                            if (!string.IsNullOrEmpty(Convert.ToString(txtOnReceived.Text).Trim()))
                                                drPOn["OA_RECEIVED"] = Convert.ToDateTime(txtOnReceived.Text).ToString("dd-MMM-yyyy");
                                            else drPOn["OA_RECEIVED"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtDateOfDrawingReceivedFromVendor.Text).Trim()))
                                                drPOn["DATE_OF_DRAWING_RECEIVED_FROM_VENDOR"] = Convert.ToDateTime(txtDateOfDrawingReceivedFromVendor.Text).ToString("dd-MMM-yyyy");
                                            else drPOn["DATE_OF_DRAWING_RECEIVED_FROM_VENDOR"] = string.Empty;

                                            if (!string.IsNullOrEmpty(Convert.ToString(txtDateOfApprovedDrawingSentToVendor.Text).Trim()))
                                                drPOn["DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR"] = Convert.ToDateTime(txtDateOfApprovedDrawingSentToVendor.Text).ToString("dd-MMM-yyyy");
                                            else drPOn["DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR"] = string.Empty;



                                            if (!string.IsNullOrEmpty(Convert.ToString(txtDeliveryTermsAgreedWithSupplierInWeeks.Text).Trim()))
                                                drPOn["DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS"] = Convert.ToInt32(txtDeliveryTermsAgreedWithSupplierInWeeks.Text);
                                            else drPOn["DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS"] = 0;


                                            //if (!string.IsNullOrEmpty(Convert.ToString(txtFinalDeliveryDateAsPerAgreedTerms.Text).Trim()))
                                            //    drPOn["FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS"] = Convert.ToDateTime(txtFinalDeliveryDateAsPerAgreedTerms.Text).AddDays(deliveryTermsAgreedWithSupplierInWeeks * 7).ToString("yyyy-MM-dd");
                                            //else drPOn["FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS"] = string.Empty;


                                            if (!string.IsNullOrEmpty(Convert.ToString(txtDateOfApprovedDrawingSentToVendor.Text).Trim()) &&
                                                Convert.ToInt32(drPOn["DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS"]) > 0)
                                                drPOn["FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS"] = Convert.ToDateTime(drPOn["DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR"])
                                                    .AddDays(Convert.ToInt32(drPOn["DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS"]) * 7).ToString("dd-MMM-yyyy");
                                            else finalDeliveryDateAsPerAgreedTerms = Convert.ToString(drPOn["PO_DELIVERY_DATE"]);


                                            if (!string.IsNullOrEmpty(Convert.ToString(txtNonNegotiableOfferedPrice.Text)) &&
                                                Convert.ToDouble(txtNonNegotiableOfferedPrice.Text) > 0)
                                                drPOn["NON_NEGOTIABLE_OFFERED_PRICE"] = Convert.ToDouble(txtNonNegotiableOfferedPrice.Text);
                                            else drPOn["NON_NEGOTIABLE_OFFERED_PRICE"] = 0;


                                            if (!string.IsNullOrEmpty(Convert.ToString(lblCheckedBy.Text).Trim()))
                                                drPOn["CHECKED_BY"] = Convert.ToString(lblCheckedBy.Text);
                                            else drPOn["CHECKED_BY"] = string.Empty;

                                            dtTempPOListAttachment.Rows.Add(drPOn);
                                        }
                                    }
                                }
                            }
                        }


                        if (dtTempPOListAttachment.Rows.Count > 0)
                        {
                            CreateAttachmentsAndSendMail(dtTempPOListAttachment, dtPMList, dtOthPMList, dtUnitList);
                        }

                        SuccessMessage(count + " records posted successfully...!!!");
                        GetPOList();
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please select atleast 1 record...!!!");
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

    private int CreateAttachmentsAndSendMail(DataTable dtPOList, DataTable dtPMList, DataTable dtOthPMList, DataTable dtUnitList)
    {
        try
        {
            int sendMailValue = 0;
            int retVal = 0;
            string jobNo = string.Empty;
            string unit = string.Empty;
            string mrCreatedBy = string.Empty;
            Stream streamProcStmtProjectView = null;

            if (dtPOList.Rows.Count > 0)
            {
                foreach (DataRow drul in dtUnitList.Rows)
                {
                    unit = Convert.ToString(drul["UNIT_NAME"]);
                    foreach (DataRow drpm in dtPMList.Select("UNIT_NAME='" + unit + "'"))
                    {
                        streamProcStmtProjectView = null;
                        body = string.Empty;
                        subject = string.Empty;
                        from = string.Empty;
                        to = string.Empty;
                        cc = string.Empty;
                        bcc = string.Empty;
                        toName = string.Empty;
                        mailSentDate = string.Empty;
                        from = "ithelpdesk@coperion.com";

                        to = Convert.ToString(drpm["EMAIL_ID"]);
                        toName = Convert.ToString(drpm["PM"]);
                        jobNo = Convert.ToString(drpm["JOB_NO"]);


                        DataTable dtProc = new DataTable();
                        dtProc = dtPOList.Clone();

                        //foreach (DataRow dr2 in dtPOList.Select("COMPLETE_JOB_NO='" + jobNo + "'"))
                        foreach (DataRow dr2 in dtPOList.Select("JOB_NO='" + jobNo + "' AND LOCATION='" + unit + "'"))
                        {
                            dtProc.ImportRow(dr2);
                        }

                        if (dtProc.Rows.Count > 0)
                        {
                            mailSentDate = DateTime.Now.ToString("dd-MMM-yyyy");
                            subject = "Alert of Procurement Status (Job Number : " + jobNo + ")";

                            int othCount = 0;
                            foreach (DataRow dro in dtProc.Select("JOB_NO='OP21053'"))
                            {
                                othCount++;
                                //mrCreatedBy = Convert.ToString(dro["MR_CREATED_BY"]);                                
                            }

                            //if (mrCreatedBy == "LAKSHMI")
                            //{
                            //    to = string.Empty;
                            //    toName = string.Empty;

                            //    foreach (DataRow dr2 in dtOthPMList.Rows)
                            //    {
                            //        to += Convert.ToString(dr2["EMAIL_ID"]) + ";";
                            //    }
                            //    toName = " Team";
                            //}

                            if (othCount > 0)
                            {
                                to = string.Empty;
                                toName = string.Empty;

                                foreach (DataRow dr2 in dtOthPMList.Rows)
                                {
                                    to += Convert.ToString(dr2["EMAIL_ID"]) + ";";
                                }
                                toName = " Team";
                            }


                            if (!string.IsNullOrEmpty(to))
                                to = to.TrimEnd(';');

                            if (!string.IsNullOrEmpty(cc))
                                cc = cc.TrimEnd(';');

                            if (!string.IsNullOrEmpty(bcc))
                                bcc = bcc.TrimEnd(';');

                            streamProcStmtProjectView = CreateAttachment(dtProc);

                            int chk = 0;
                            if (streamProcStmtProjectView == null)
                            {
                                chk = 0;
                            }
                            else
                            {
                                chk = 1;
                            }

                            if (chk > 0)
                            {
                                sendMailValue = SendMail(subject, from, to, toName, cc, bcc, streamProcStmtProjectView, mailSentDate, jobNo);
                            }
                        }
                    }
                }
            }
            else
                retVal = 0;

            if (sendMailValue > 0)
                retVal = 1;

            return retVal;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private Stream CreateAttachment(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;

                    rowTxt = Convert.ToString(rowTxt).Replace("\n", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace("\r", " ");
                    rowTxt = Convert.ToString(rowTxt).Replace(",", "") + ',';

                    csv += Convert.ToString(rowTxt);
                }
                csv += "\r\n";
            }


            byte[] byteArray = Encoding.ASCII.GetBytes(csv);
            MemoryStream stream = new MemoryStream(byteArray);

            return stream;
        }
        catch (Exception)
        {
            return null;
        }
    }


    private int SendMail(string subject, string from, string to, string toName, string cc, string bcc,
                            Stream streamProcStmtProjectView,
                            string sentDate, string jobNo)
    {
        body = string.Empty;
        int returnVal = 0;
        SmtpClient SmtpServer = new SmtpClient();
        SmtpServer.Host = "eusmtp.hi.corp";
        SmtpServer.Port = 25;
        SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

        MailMessage mail = new MailMessage();

        mail.Subject = subject;
        mail.From = new MailAddress(from);

        if (!string.IsNullOrEmpty(to))
        {
            to = to.TrimEnd(';');
            string[] strTo = to.Split(';');
            foreach (string item in strTo)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.To.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(cc))
        {
            cc = cc.TrimEnd(';');
            string[] strCC = cc.Split(';');
            foreach (string item in strCC)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.CC.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(bcc))
        {
            bcc = bcc.TrimEnd(';');
            string[] strBcc = bcc.Split(';');
            foreach (string item in strBcc)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    mail.Bcc.Add(item);
                }
            }
        }

        if (!string.IsNullOrEmpty(to))
        {
            if (streamProcStmtProjectView != null)
            {
                mail.Attachments.Add(new Attachment(streamProcStmtProjectView, "Procurement_Status_Report_" + sentDate + ".csv", "text/csv"));
            }


            mail.IsBodyHtml = true;

            fileName = "~/REPORTS/PURCHASE_ORDER/StatusReportMail.html";
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#toname#}", toName);
            body = body.Replace("{#jobno#}", jobNo);
            body = body.Replace("{#sentdate#}", sentDate);


            mail.Body = body;

            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    returnVal = 1;
                }
                else
                    returnVal = 0;
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    returnVal = 1;

                else
                    returnVal = 0;
            }
        }
        else
            returnVal = 0;


        return returnVal;
    }


    private void DeletePO()
    {
        try
        {
            DataTable dtDeleteTemp = new DataTable();

            dtDeleteTemp.Columns.Add("PO_NO", typeof(string));

            string PONo = string.Empty;


            int count = 0;

            if (gvPOList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvPOList.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");

                    if (chkSelect.Checked)
                    {
                        count++;

                        Label lblPONo = (Label)gr.FindControl("lblPONo");

                        if (!string.IsNullOrEmpty(Convert.ToString(lblPONo.Text).Trim()))
                            PONo = Convert.ToString(lblPONo.Text).Trim();
                        else
                            PONo = string.Empty;

                        DataRow drn1 = dtDeleteTemp.NewRow();

                        drn1["PO_NO"] = PONo;

                        dtDeleteTemp.Rows.Add(drn1);
                    }
                }

                if (count > 0)
                {
                    int value = objPurchase.DeletePO(dtDeleteTemp, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    if (value > 0)
                    {
                        SuccessMessage(count + " records deleted successfully...!!!");
                        GetPOList();
                        return;
                    }
                }
                else
                {
                    ExceptionMessage("Please select atleast 1 record...!!!");
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
