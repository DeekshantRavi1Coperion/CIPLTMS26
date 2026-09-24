using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections.Generic;

public partial class VENDOR_CUSTOMER_MGMT_VENDOR_RegisterVendor : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    MailService objMailService = new MailService();
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();

    DataTable dtBillingAddress = new DataTable();
    DataTable dtContactPerson = new DataTable();
    DataTable dtBankDetails = new DataTable();
    DataTable dtAttachments = new DataTable();
    DataTable dtAmendments = new DataTable();

    DataSet dsCategory = new DataSet();
    DataSet dsMSMEStatus = new DataSet();
    DataSet dsResponsible = new DataSet();
    DataSet dsRelationType = new DataSet();
    DataSet dsItemCategory = new DataSet();
    DataSet dsItemSubCategory = new DataSet();
    DataSet dsCountry = new DataSet();
    DataSet dsState = new DataSet();

    DataSet dsDOCTypes = new DataSet();
    DataSet dsCheckers = new DataSet();

    DataTable dtTemp;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {



            HidePanel();
            if (!IsPostBack)
            {
                ClearSessions();

                ddlItemSubCategoryToS.Items.Clear();
                ddlItemSubCategoryToS.Items.Insert(0, "Select");
                ddlItemSubCategoryToS.SelectedIndex = 0;

                BindMSMEStatus();
                BindCategory();
                BindResponsible();
                BindRelationType();
                BindItemCategory();
                BindCountrys();
                BindStates();

                BindDOCTypes();
                BindCheckers();

                ddlStateToS.Items.Clear();
                ddlStateToS.Items.Insert(0, "Select");
                ddlStateToS.SelectedIndex = 0;
                //ddlStateToS.Enabled = false;

                txtOtherStateToS.Text = string.Empty;
                txtOtherStateToS.Enabled = true;

                txtSWIFTCodeToS.Text = string.Empty;
                txtSWIFTCodeToS.Enabled = true;

                txtISBNToS.Text = string.Empty;
                txtISBNToS.Enabled = true;



                if (ddlCountryToS.SelectedValue == "95")
                {
                    //ddlStateToS.Enabled = true;
                    txtOtherStateToS.Enabled = false;
                    txtSWIFTCodeToS.Enabled = false;
                    txtISBNToS.Enabled = false;
                    BindStates();
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveVendor();
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemSubCategorys();
    }



    protected void btnAddUpdateAddressToList_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdBillingAddressUpdationFlag.Value) == 0)
        {
            AddBillingAddress();
        }
        else
        {
            UpdateBillingAddress();
        }
    }

    protected void gvBillingAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvBillingAddress.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblGSTIN = gvBillingAddress.Rows[rowindex].FindControl("lblGSTIN") as Label;
                Label lblAddress1 = gvBillingAddress.Rows[rowindex].FindControl("lblAddress1") as Label;
                Label lblAddress2 = gvBillingAddress.Rows[rowindex].FindControl("lblAddress2") as Label;
                Label lblAddress3 = gvBillingAddress.Rows[rowindex].FindControl("lblAddress3") as Label;
                Label lblCity = gvBillingAddress.Rows[rowindex].FindControl("lblCity") as Label;
                Label lblStateID = gvBillingAddress.Rows[rowindex].FindControl("lblStateID") as Label;
                Label lblOtherState = gvBillingAddress.Rows[rowindex].FindControl("lblOtherState") as Label;
                Label lblCountryID = gvBillingAddress.Rows[rowindex].FindControl("lblCountryID") as Label;
                Label lblPINCode = gvBillingAddress.Rows[rowindex].FindControl("lblPINCode") as Label;
                Label lblPhone = gvBillingAddress.Rows[rowindex].FindControl("lblPhone") as Label;
                Label lblEmail = gvBillingAddress.Rows[rowindex].FindControl("lblEmail") as Label;
                Label lblIsDefault = gvBillingAddress.Rows[rowindex].FindControl("lblIsDefault") as Label;

                ViewState["SR_NO"] = Convert.ToInt32(lblSrNo.Text);

                //hdRevNoText.Value = "";
                //hdRevNoTextOld.Value = "";
                //txtRevNoText.Enabled = false;

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {

                    string stateCode = "";
                    if (ddlStateToS.SelectedIndex > 0)
                        stateCode = ddlStateToS.SelectedItem.Text.Substring(ddlStateToS.SelectedItem.Text.Length - 3, 2);

                    btnAddUpdateAddressToList.Text = "Update Address";

                    hdBillingAddressUpdationFlag.Value = "1";

                    if (!string.IsNullOrEmpty(lblSrNo.Text)) hdBillingAddressSRNo.Value = lblSrNo.Text;
                    else hdBillingAddressSRNo.Value = "0";

                    if (!string.IsNullOrEmpty(lblGSTIN.Text))
                    {
                        string panTxt = lblGSTIN.Text.Substring(2, 10);
                        string runningNoTxt = lblGSTIN.Text.Substring(12, 3);
                        if (panTxt == txtPANNumberToS.Text)
                        {
                            txtPANToSS.Text = txtPANNumberToS.Text.ToUpper();
                            txtGSTInToS.Text = lblGSTIN.Text.ToUpper();
                            txtGSTInRunningNoToS.Text = runningNoTxt.ToUpper();
                        }
                        else
                        {

                            txtPANToSS.Text = txtPANNumberToS.Text.ToUpper();
                            txtGSTInRunningNoToS.Text = runningNoTxt.ToUpper();
                            string gstin = stateCode + txtPANNumberToS.Text + runningNoTxt;
                            txtGSTInToS.Text = FormattedString(gstin);
                        }
                    }
                    else
                    {
                        txtGSTInToS.Text = string.Empty;
                        txtGSTInRunningNoToS.Text = string.Empty;
                        txtPANToSS.Text = FormattedString(txtPANNumberToS.Text);
                    }

                    if (!string.IsNullOrEmpty(lblAddress1.Text)) txtAddress1ToS.Text = lblAddress1.Text;
                    else txtAddress1ToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblAddress2.Text)) txtAddress2ToS.Text = lblAddress2.Text;
                    else txtAddress2ToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblAddress3.Text)) txtAddress3ToS.Text = lblAddress3.Text;
                    else txtAddress3ToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblCity.Text)) txtCityToS.Text = lblCity.Text;
                    else txtCityToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblOtherState.Text)) txtOtherStateToS.Text = lblOtherState.Text;
                    else txtOtherStateToS.Text = string.Empty;

                    BindCountrys();
                    if (Convert.ToInt32(lblCountryID.Text) > 0)
                    {
                        ddlCountryToS.SelectedValue = lblCountryID.Text;
                    }

                    BindStates();
                    if (Convert.ToInt32(lblStateID.Text) > 0)
                    {
                        ddlStateToS.SelectedValue = lblStateID.Text;
                    }

                    if (!string.IsNullOrEmpty(lblPINCode.Text)) txtPhoneToS.Text = lblPINCode.Text;
                    else txtPINCodeToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblPhone.Text)) txtPhoneToS.Text = lblPhone.Text;
                    else txtPhoneToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblEmail.Text)) txtEmailToS.Text = lblEmail.Text;
                    else txtEmailToS.Text = string.Empty;



                    if (Convert.ToInt32(lblIsDefault.Text) > 0) chkIsDefaultBillingAddressToS.Checked = true;

                    mpeAddBillingAddress.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveBillingAddress(Convert.ToInt32(lblSrNo.Text));
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

    protected void gvBillingAddress_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Label lblIsDefault = (Label)e.Row.FindControl("lblIsDefault");
                CheckBox chkIsDefault = (CheckBox)e.Row.FindControl("chkIsDefault");

                if (Convert.ToInt32(lblIsDefault.Text) > 0)
                    chkIsDefault.Checked = true;
                else chkIsDefault.Checked = false;

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    protected void btnAddUpdateContactPersonToList_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdContactPersonUpdationFlag.Value) == 0)
        {
            AddContactPerson();
        }
        else
        {
            UpdateContactPerson();
        }
    }

    protected void gvContactPerson_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvContactPerson.Rows[rowindex].FindControl("lblSrNo") as Label;
                Label lblContactPersonName = gvContactPerson.Rows[rowindex].FindControl("lblContactPersonName") as Label;
                Label lblContactPersonMobile = gvContactPerson.Rows[rowindex].FindControl("lblContactPersonMobile") as Label;
                Label lblContactPersonPhone = gvContactPerson.Rows[rowindex].FindControl("lblContactPersonPhone") as Label;
                Label lblContactPersonEmail = gvContactPerson.Rows[rowindex].FindControl("lblContactPersonEmail") as Label;
                Label lblIsDefault = gvContactPerson.Rows[rowindex].FindControl("lblIsDefault") as Label;

                ViewState["SR_NO"] = Convert.ToInt32(lblSrNo.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    btnAddUpdateContactPersonToList.Text = "Update Contact Person";

                    hdContactPersonUpdationFlag.Value = "1";

                    if (!string.IsNullOrEmpty(lblSrNo.Text)) hdContactPersonSRNo.Value = lblSrNo.Text;
                    else hdContactPersonSRNo.Value = "0";

                    if (!string.IsNullOrEmpty(lblContactPersonName.Text)) txtContactPersonNameToS.Text = lblContactPersonName.Text;
                    else txtContactPersonNameToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblContactPersonMobile.Text)) txtContactPersonMobileToS.Text = lblContactPersonMobile.Text;
                    else txtContactPersonMobileToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblContactPersonPhone.Text)) txtContactPersonPhoneToS.Text = lblContactPersonPhone.Text;
                    else txtContactPersonPhoneToS.Text = string.Empty;

                    if (!string.IsNullOrEmpty(lblContactPersonEmail.Text)) txtContactPersonEmailToS.Text = lblContactPersonEmail.Text;
                    else txtContactPersonEmailToS.Text = string.Empty;

                    if (Convert.ToInt32(lblIsDefault.Text) > 0) chkIsDefaultContactPersonToS.Checked = true;

                    mpeAddContactPerson.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveContactPerson(Convert.ToInt32(lblSrNo.Text));
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

    protected void gvContactPerson_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Label lblIsDefault = (Label)e.Row.FindControl("lblIsDefault");
                CheckBox chkIsDefault = (CheckBox)e.Row.FindControl("chkIsDefault");

                if (Convert.ToInt32(lblIsDefault.Text) > 0)
                    chkIsDefault.Checked = true;
                else chkIsDefault.Checked = false;

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }





    protected void btnAddUpdateAttachmentToList_Click(object sender, EventArgs e)
    {
        AddAttachment();
    }

    protected void gvAddAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSrNo = gvAddAttachments.Rows[rowindex].FindControl("lblSrNo") as Label;

                ViewState["SR_NO"] = Convert.ToInt32(lblSrNo.Text);

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemoveAttachments(Convert.ToInt32(lblSrNo.Text));
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

    protected void gvAddAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
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
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    #endregion


    #region METHODS[=========================]

    private void ClearSessions()
    {
        Session["dtBillingAddress"] = null;
        Session["dtContactPerson"] = null;
        Session["dtBankDetails"] = null;
        Session["dtAttachments"] = null;
    }


    private void BindMSMEStatus()
    {
        try
        {
            dsMSMEStatus = objVCM.GetMSMEStatus();
            if (dsMSMEStatus.Tables.Count > 0 && dsMSMEStatus.Tables[0].Rows.Count > 0)
            {
                ddlMSMEStatusToS.DataSource = dsMSMEStatus.Tables[0];
                ddlMSMEStatusToS.DataTextField = "NAME";
                ddlMSMEStatusToS.DataValueField = "PID";
                ddlMSMEStatusToS.DataBind();
                ddlMSMEStatusToS.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCategory()
    {
        try
        {
            dsCategory = objVCM.GetCategorys();
            if (dsCategory.Tables.Count > 0 && dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategoryToS.DataSource = dsCategory.Tables[0];
                ddlCategoryToS.DataTextField = "NAME";
                ddlCategoryToS.DataValueField = "PID";
                ddlCategoryToS.DataBind();
                ddlCategoryToS.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindResponsible()
    {
        try
        {
            dsResponsible = objVCM.GetResponsibles();
            if (dsResponsible.Tables.Count > 0 && dsResponsible.Tables[0].Rows.Count > 0)
            {
                ddlResponsibleToS.DataSource = dsResponsible.Tables[0];
                ddlResponsibleToS.DataTextField = "NAME";
                ddlResponsibleToS.DataValueField = "PID";
                ddlResponsibleToS.DataBind();
                ddlResponsibleToS.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindRelationType()
    {
        try
        {
            dsRelationType = objVCM.GetRelationTypes();
            if (dsRelationType.Tables.Count > 0 && dsRelationType.Tables[0].Rows.Count > 0)
            {
                ddlRelationTypeToS.DataSource = dsRelationType.Tables[0];
                ddlRelationTypeToS.DataTextField = "NAME";
                ddlRelationTypeToS.DataValueField = "PID";
                ddlRelationTypeToS.DataBind();
                ddlRelationTypeToS.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindItemCategory()
    {
        try
        {
            dsItemCategory = objVCM.GetItemCategorys();
            if (dsItemCategory.Tables.Count > 0 && dsItemCategory.Tables[0].Rows.Count > 0)
            {
                ddlItemCategoryToS.DataSource = dsItemCategory.Tables[0];
                ddlItemCategoryToS.DataTextField = "NAME";
                ddlItemCategoryToS.DataValueField = "PID";
                ddlItemCategoryToS.DataBind();
                ddlItemCategoryToS.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindItemSubCategorys()
    {
        try
        {
            ddlItemSubCategoryToS.Items.Clear();
            ddlItemSubCategoryToS.Items.Insert(0, "Select");
            ddlItemSubCategoryToS.SelectedIndex = 0;

            if (ddlItemCategoryToS.SelectedIndex > 0)
            {
                dsItemSubCategory = objVCM.GetItemSubCategorys(Convert.ToInt32(ddlItemCategoryToS.SelectedValue));
                if (dsItemSubCategory.Tables.Count > 0 && dsItemSubCategory.Tables[0].Rows.Count > 0)
                {
                    ddlItemSubCategoryToS.DataSource = dsItemSubCategory.Tables[0];
                    ddlItemSubCategoryToS.DataTextField = "NAME";
                    ddlItemSubCategoryToS.DataValueField = "PID";
                    ddlItemSubCategoryToS.DataBind();
                    ddlItemSubCategoryToS.Items.Insert(0, "Select");
                    ddlItemSubCategoryToS.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCountrys()
    {
        try
        {
            dsCountry = objCommon.GetCountry();
            if (dsCountry.Tables.Count > 0 && dsCountry.Tables[0].Rows.Count > 0)
            {
                ddlCountryToS.DataSource = dsCountry.Tables[0];
                ddlCountryToS.DataTextField = "COUNTRY_NAME";
                ddlCountryToS.DataValueField = "COUNTRY_ID";
                ddlCountryToS.DataBind();
                ddlCountryToS.Items.Insert(0, "Select");
                ddlCountryToS.SelectedValue = "95";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStates()
    {
        try
        {
            ddlStateToS.Items.Clear();
            ddlStateToS.Items.Insert(0, "Select");
            ddlStateToS.SelectedIndex = 0;
            //ddlStateToS.Enabled = false;

            txtOtherStateToS.Text = string.Empty;
            txtOtherStateToS.Enabled = true;

            txtSWIFTCodeToS.Text = string.Empty;
            txtSWIFTCodeToS.Enabled = true;

            txtISBNToS.Text = string.Empty;
            txtISBNToS.Enabled = true;

            if (ddlCountryToS.SelectedIndex > 0 && ddlCountryToS.SelectedValue == "95") //india
            {
                dsState = objCommon.GetStates();
                if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)
                {
                    ddlStateToS.DataSource = dsState.Tables[0];
                    ddlStateToS.DataTextField = "STATE_NAME_GST_CODE";
                    ddlStateToS.DataValueField = "STATE_PID";
                    ddlStateToS.DataBind();
                    ddlStateToS.Items.Insert(0, "Select");
                    ddlStateToS.SelectedIndex = 0;
                    //ddlStateToS.Enabled = true;

                    txtOtherStateToS.Enabled = false;
                    txtSWIFTCodeToS.Enabled = false;
                    txtISBNToS.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDOCTypes()
    {
        try
        {
            dsDOCTypes = objVCM.GetDODTypes((int)StatusAndTypes.EnumEntityType.Vendor);
            if (dsDOCTypes.Tables.Count > 0 && dsDOCTypes.Tables[0].Rows.Count > 0)
            {
                ddlAttachmentType.DataSource = dsDOCTypes.Tables[0];
                ddlAttachmentType.DataTextField = "NAME";
                ddlAttachmentType.DataValueField = "PID";
                ddlAttachmentType.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCheckers()
    {
        try
        {
            dsCheckers = objVCM.GetApproversById((int)StatusAndTypes.EnumApproverType.Checker_1);
            if (dsCheckers.Tables.Count > 0 && dsCheckers.Tables[0].Rows.Count > 0)
            {
                ddlCheckerToS.DataSource = dsCheckers.Tables[0];
                ddlCheckerToS.DataTextField = "EMPLOYEE_NAME";
                ddlCheckerToS.DataValueField = "EMP_RECORD_FID";
                ddlCheckerToS.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }






    private void RemoveBillingAddress(int srNo)
    {
        if (Session["dtBillingAddress"] != null)
            dtTemp = (DataTable)Session["dtBillingAddress"];
        else
            AddTempBillingAddressTable();

        dtTemp = (DataTable)Session["dtBillingAddress"];

        if (dtTemp.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
            {
                dtTemp.Rows.Remove(dr);
            }
        }

        if (dtTemp.Rows.Count > 0)
        {
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dtTemp.Rows[i]["SR_NO"] = i + 1;
            }
        }

        gvBillingAddress.DataSource = dtTemp;
        gvBillingAddress.DataBind();

        lblBillingAddressRecords.Text = "[" + gvBillingAddress.Rows.Count + "]";
    }

    private void AddBillingAddress()
    {
        try
        {
            string GSTIN = string.Empty;
            string Address1 = string.Empty;
            string Address2 = string.Empty;
            string Address3 = string.Empty;
            string City = string.Empty;
            string State = string.Empty;
            string OtherState = string.Empty;
            string Country = string.Empty;
            string PINCode = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int IsDefault = 0;

            int CountryId = 0;
            int StateId = 0;

            if (Session["dtBillingAddress"] != null)
                dtTemp = (DataTable)Session["dtBillingAddress"];
            else
                AddTempBillingAddressTable();

            dtTemp = (DataTable)Session["dtBillingAddress"];


            //if (!string.IsNullOrEmpty(txtGSTInToS.Text))
            //{
            //    GSTIN = Convert.ToString(txtGSTInToS.Text);
            //    if (dtTemp.Rows.Count > 0)
            //    {
            //        foreach (DataRow item in dtTemp.Select("GSTIN='" + GSTIN + "'"))
            //        {
            //            lblBillingAddressWindowMsg.Text = "GSTIN already added to list!";
            //            mpeAddBillingAddress.Show();
            //            return;
            //        }
            //    }
            //}

            if (!string.IsNullOrEmpty(txtGSTInToS.Text))
                GSTIN = Convert.ToString(txtGSTInToS.Text);

            if (!string.IsNullOrEmpty(txtAddress1ToS.Text))
                Address1 = Convert.ToString(txtAddress1ToS.Text);

            if (!string.IsNullOrEmpty(txtAddress2ToS.Text))
                Address2 = Convert.ToString(txtAddress2ToS.Text);

            if (!string.IsNullOrEmpty(txtAddress3ToS.Text))
                Address3 = Convert.ToString(txtAddress3ToS.Text);

            if (!string.IsNullOrEmpty(txtCityToS.Text))
                City = Convert.ToString(txtCityToS.Text);

            if (!string.IsNullOrEmpty(txtOtherStateToS.Text))
                OtherState = Convert.ToString(txtOtherStateToS.Text);

            if (ddlCountryToS.SelectedIndex > 0)
            {
                CountryId = Convert.ToInt32(ddlCountryToS.SelectedValue);
                Country = ddlCountryToS.SelectedItem.Text;
            }

            if (ddlStateToS.SelectedIndex > 0)
            {
                StateId = Convert.ToInt32(ddlStateToS.SelectedValue);
                State = ddlStateToS.SelectedItem.Text;
            }

            if (!string.IsNullOrEmpty(txtPINCodeToS.Text))
                PINCode = Convert.ToString(txtPINCodeToS.Text);

            if (!string.IsNullOrEmpty(txtPhoneToS.Text))
                Phone = Convert.ToString(txtPhoneToS.Text);

            if (!string.IsNullOrEmpty(txtEmailToS.Text))
                Email = Convert.ToString(txtEmailToS.Text);

            if (chkIsDefaultBillingAddressToS.Checked)
            {
                IsDefault = 1;
                foreach (DataRow drt in dtTemp.Rows)
                {
                    drt["IS_DEFAULT"] = 0;
                }
            }


            DataRow dr = dtTemp.NewRow();

            int index = gvBillingAddress.Rows.Count + 1;

            dr["SR_NO"] = index;
            dr["PID"] = 0;
            dr["GSTIN"] = GSTIN;
            dr["ADDRESS_LINE1"] = Address1;
            dr["ADDRESS_LINE2"] = Address2;
            dr["ADDRESS_LINE3"] = Address3;
            dr["CITY"] = City;
            dr["OTHER_STATE"] = OtherState;
            dr["STATE_ID"] = StateId;
            dr["COUNTRY_ID"] = CountryId;
            dr["PIN_CODE"] = PINCode;
            dr["PHONE"] = Phone;
            dr["EMAIL"] = Email;
            dr["IS_DEFAULT"] = IsDefault;
            dr["IS_DELETED"] = 0;
            dr["IS_REVISED"] = 0;

            dr["STATE"] = State;
            dr["COUNTRY"] = Country;

            dtTemp.Rows.Add(dr);

            if (dtTemp.Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow item in dtTemp.Select("IS_DEFAULT=1"))
                {
                    count++;
                    if (count > 0) break;
                }

                if (count == 0)
                {
                    dtTemp.Rows[0]["IS_DEFAULT"] = 1;
                }
            }

            gvBillingAddress.DataSource = dtTemp;
            gvBillingAddress.DataBind();
            lblBillingAddressRecords.Text = "[" + gvBillingAddress.Rows.Count + "]";


            Session["dtBillingAddress"] = dtTemp;


            ResetBillingAddress();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateBillingAddress()
    {
        try
        {
            string GSTIN = string.Empty;
            string Address1 = string.Empty;
            string Address2 = string.Empty;
            string Address3 = string.Empty;
            string City = string.Empty;
            string OtherState = string.Empty;
            string State = string.Empty;
            string Country = string.Empty;
            string PINCode = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int IsDefault = 0;

            int CountryId = 0;
            int StateId = 0;

            if (!string.IsNullOrEmpty(txtGSTInToS.Text))
                GSTIN = Convert.ToString(txtGSTInToS.Text);

            if (!string.IsNullOrEmpty(txtAddress1ToS.Text))
                Address1 = Convert.ToString(txtAddress1ToS.Text);

            if (!string.IsNullOrEmpty(txtAddress2ToS.Text))
                Address2 = Convert.ToString(txtAddress2ToS.Text);

            if (!string.IsNullOrEmpty(txtAddress3ToS.Text))
                Address3 = Convert.ToString(txtAddress3ToS.Text);

            if (!string.IsNullOrEmpty(txtCityToS.Text))
                City = Convert.ToString(txtCityToS.Text);

            if (!string.IsNullOrEmpty(txtOtherStateToS.Text))
                OtherState = Convert.ToString(txtOtherStateToS.Text);

            if (ddlCountryToS.SelectedIndex > 0)
            {
                CountryId = Convert.ToInt32(ddlCountryToS.SelectedValue);
                Country = ddlCountryToS.SelectedItem.Text;
            }

            if (ddlStateToS.SelectedIndex > 0)
            {
                StateId = Convert.ToInt32(ddlStateToS.SelectedValue);
                State = ddlStateToS.SelectedItem.Text;
            }

            if (!string.IsNullOrEmpty(txtPINCodeToS.Text))
                PINCode = Convert.ToString(txtPINCodeToS.Text);

            if (!string.IsNullOrEmpty(txtPhoneToS.Text))
                Phone = Convert.ToString(txtPhoneToS.Text);

            if (!string.IsNullOrEmpty(txtEmailToS.Text))
                Email = Convert.ToString(txtEmailToS.Text);




            if (Session["dtBillingAddress"] != null)
                dtTemp = (DataTable)Session["dtBillingAddress"];
            else
                AddTempBillingAddressTable();

            dtTemp = (DataTable)Session["dtBillingAddress"];


            if (dtTemp.Rows.Count > 0)
            {
                if (chkIsDefaultBillingAddressToS.Checked)
                {
                    IsDefault = 1;
                    foreach (DataRow drt in dtTemp.Rows)
                    {
                        drt["IS_DEFAULT"] = 0;
                    }
                }


                foreach (DataRow d in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdBillingAddressSRNo.Value) + "'"))
                {
                    d["PID"] = "0";
                    d["IS_DELETED"] = 0;
                    d["IS_REVISED"] = 0;

                    if (Convert.ToInt32(hdBillingAddressSRNo.Value) > 0)
                        d["SR_NO"] = Convert.ToString(hdBillingAddressSRNo.Value);
                    else d["SR_NO"] = "0";

                    if (!string.IsNullOrEmpty(GSTIN))
                        d["GSTIN"] = GSTIN;
                    else d["GSTIN"] = string.Empty;

                    if (!string.IsNullOrEmpty(Address1))
                        d["ADDRESS_LINE1"] = Address1;
                    else d["ADDRESS_LINE1"] = string.Empty;

                    if (!string.IsNullOrEmpty(Address2))
                        d["ADDRESS_LINE2"] = Address2;
                    else d["ADDRESS_LINE2"] = string.Empty;

                    if (!string.IsNullOrEmpty(Address3))
                        d["ADDRESS_LINE3"] = Address3;
                    else d["ADDRESS_LINE3"] = string.Empty;

                    if (!string.IsNullOrEmpty(City))
                        d["CITY"] = City;
                    else d["CITY"] = string.Empty;

                    if (!string.IsNullOrEmpty(OtherState))
                        d["OTHER_STATE"] = OtherState;
                    else d["OTHER_STATE"] = string.Empty;

                    if (StateId > 0)
                        d["STATE_ID"] = StateId;
                    else d["STATE_ID"] = string.Empty;

                    if (CountryId > 0)
                        d["COUNTRY_ID"] = CountryId;
                    else d["COUNTRY_ID"] = string.Empty;

                    if (!string.IsNullOrEmpty(PINCode))
                        d["PIN_CODE"] = PINCode;
                    else d["PIN_CODE"] = string.Empty;

                    if (!string.IsNullOrEmpty(Phone))
                        d["PHONE"] = Phone;
                    else d["PHONE"] = string.Empty;

                    if (!string.IsNullOrEmpty(Email))
                        d["EMAIL"] = Email;
                    else d["EMAIL"] = string.Empty;


                    if (!string.IsNullOrEmpty(State))
                        d["STATE"] = State;
                    else d["STATE"] = string.Empty;

                    if (!string.IsNullOrEmpty(Country))
                        d["COUNTRY"] = Country;
                    else d["COUNTRY"] = string.Empty;

                    if (IsDefault > 0)
                        d["IS_DEFAULT"] = 1;
                    else d["IS_DEFAULT"] = 0;
                }
            }

            gvBillingAddress.DataSource = dtTemp;
            gvBillingAddress.DataBind();

            lblBillingAddressRecords.Text = "[" + gvBillingAddress.Rows.Count + "]";

            Session["dtBillingAddress"] = dtTemp;

            ResetBillingAddress();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddTempBillingAddressTable()
    {
        dtBillingAddress.Columns.Add("SR_NO", typeof(int));
        dtBillingAddress.Columns.Add("PID", typeof(int));
        dtBillingAddress.Columns.Add("ADDRESS_LINE1", typeof(string));
        dtBillingAddress.Columns.Add("ADDRESS_LINE2", typeof(string));
        dtBillingAddress.Columns.Add("ADDRESS_LINE3", typeof(string));
        dtBillingAddress.Columns.Add("CITY", typeof(string));
        dtBillingAddress.Columns.Add("OTHER_STATE", typeof(string));
        dtBillingAddress.Columns.Add("PIN_CODE", typeof(string));
        dtBillingAddress.Columns.Add("STATE_ID", typeof(int));
        dtBillingAddress.Columns.Add("COUNTRY_ID", typeof(int));
        dtBillingAddress.Columns.Add("PHONE", typeof(string));
        dtBillingAddress.Columns.Add("EMAIL", typeof(string));
        dtBillingAddress.Columns.Add("GSTIN", typeof(string));
        dtBillingAddress.Columns.Add("IS_DEFAULT", typeof(int));
        dtBillingAddress.Columns.Add("IS_DELETED", typeof(int));
        dtBillingAddress.Columns.Add("IS_REVISED", typeof(int));

        dtBillingAddress.Columns.Add("STATE", typeof(string));
        dtBillingAddress.Columns.Add("COUNTRY", typeof(string));

        Session["dtBillingAddress"] = dtBillingAddress;
    }

    private void ResetBillingAddress()
    {
        txtGSTInToS.Text = string.Empty;
        txtAddress1ToS.Text = string.Empty;
        txtAddress2ToS.Text = string.Empty;
        txtAddress3ToS.Text = string.Empty;
        txtCityToS.Text = string.Empty;
        txtOtherStateToS.Text = string.Empty;
        txtPINCodeToS.Text = string.Empty;
        ddlStateToS.SelectedIndex = 0;
        //ddlCountryToS.SelectedIndex = 0;
        txtPhoneToS.Text = string.Empty;
        txtEmailToS.Text = string.Empty;
        chkIsDefaultBillingAddressToS.Checked = false;
        lblBillingAddressWindowMsg.Text = string.Empty;
    }






    private void RemoveContactPerson(int srNo)
    {
        if (Session["dtContactPerson"] != null)
            dtTemp = (DataTable)Session["dtContactPerson"];
        else
            AddTempContactPersonTable();

        dtTemp = (DataTable)Session["dtContactPerson"];

        if (dtTemp.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTemp.Select("SR_NO='" + srNo + "'"))
            {
                dtTemp.Rows.Remove(dr);
            }
        }

        if (dtTemp.Rows.Count > 0)
        {
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dtTemp.Rows[i]["SR_NO"] = i + 1;
            }
        }

        gvContactPerson.DataSource = dtTemp;
        gvContactPerson.DataBind();

        lblContactPersonRecords.Text = "[" + gvContactPerson.Rows.Count + "]";
    }

    private void AddContactPerson()
    {
        try
        {
            string Name = string.Empty;
            string Mobile = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int IsDefault = 0;

            if (Session["dtContactPerson"] != null)
                dtTemp = (DataTable)Session["dtContactPerson"];
            else
                AddTempContactPersonTable();

            dtTemp = (DataTable)Session["dtContactPerson"];

            if (!string.IsNullOrEmpty(txtContactPersonNameToS.Text))
                Name = Convert.ToString(txtContactPersonNameToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonMobileToS.Text))
                Mobile = Convert.ToString(txtContactPersonMobileToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonPhoneToS.Text))
                Phone = Convert.ToString(txtContactPersonPhoneToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonEmailToS.Text))
                Email = Convert.ToString(txtContactPersonEmailToS.Text);

            if (chkIsDefaultContactPersonToS.Checked)
            {
                IsDefault = 1;
                foreach (DataRow drt in dtTemp.Rows)
                {
                    drt["IS_DEFAULT"] = 0;
                }
            }


            DataRow dr = dtTemp.NewRow();

            int index = gvContactPerson.Rows.Count + 1;

            dr["SR_NO"] = index;
            dr["PID"] = 0;
            dr["NAME"] = Name;
            dr["MOBILE_NO"] = Mobile;
            dr["PHONE"] = Phone;
            dr["EMAIL"] = Email;
            dr["IS_DEFAULT"] = IsDefault;
            dr["IS_DELETED"] = 0;
            dr["IS_REVISED"] = 0;

            dtTemp.Rows.Add(dr);

            if (dtTemp.Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow item in dtTemp.Select("IS_DEFAULT=1"))
                {
                    count++;
                    if (count > 0) break;
                }

                if (count == 0)
                {
                    dtTemp.Rows[0]["IS_DEFAULT"] = 1;
                }
            }

            gvContactPerson.DataSource = dtTemp;
            gvContactPerson.DataBind();
            lblContactPersonRecords.Text = "[" + gvContactPerson.Rows.Count + "]";

            ResetContactPerson();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateContactPerson()
    {
        try
        {
            string Name = string.Empty;
            string Mobile = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            int IsDefault = 0;

            if (!string.IsNullOrEmpty(txtContactPersonNameToS.Text))
                Name = Convert.ToString(txtContactPersonNameToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonMobileToS.Text))
                Mobile = Convert.ToString(txtContactPersonMobileToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonPhoneToS.Text))
                Phone = Convert.ToString(txtContactPersonPhoneToS.Text);

            if (!string.IsNullOrEmpty(txtContactPersonEmailToS.Text))
                Email = Convert.ToString(txtContactPersonEmailToS.Text);


            if (Session["dtContactPerson"] != null)
                dtTemp = (DataTable)Session["dtContactPerson"];
            else
                AddTempContactPersonTable();

            dtTemp = (DataTable)Session["dtContactPerson"];


            if (dtTemp.Rows.Count > 0)
            {
                if (chkIsDefaultContactPersonToS.Checked)
                {
                    IsDefault = 1;
                    foreach (DataRow drt in dtTemp.Rows)
                    {
                        drt["IS_DEFAULT"] = 0;
                    }
                }

                foreach (DataRow d in dtTemp.Select("SR_NO='" + Convert.ToInt32(hdContactPersonSRNo.Value) + "'"))
                {
                    d["PID"] = "0";
                    d["IS_DELETED"] = 0;
                    d["IS_REVISED"] = 0;

                    if (Convert.ToInt32(hdContactPersonSRNo.Value) > 0)
                        d["SR_NO"] = Convert.ToString(hdContactPersonSRNo.Value);
                    else d["SR_NO"] = "0";

                    if (!string.IsNullOrEmpty(Name))
                        d["NAME"] = Name;
                    else d["NAME"] = string.Empty;

                    if (!string.IsNullOrEmpty(Mobile))
                        d["MOBILE_NO"] = Mobile;
                    else d["MOBILE_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(Phone))
                        d["PHONE"] = Phone;
                    else d["PHONE"] = string.Empty;

                    if (!string.IsNullOrEmpty(Email))
                        d["EMAIL"] = Email;
                    else d["EMAIL"] = string.Empty;

                    if (IsDefault > 0)
                        d["IS_DEFAULT"] = 1;
                    else d["IS_DEFAULT"] = 0;
                }
            }

            gvContactPerson.DataSource = dtTemp;
            gvContactPerson.DataBind();

            lblContactPersonRecords.Text = "[" + gvContactPerson.Rows.Count + "]";

            ResetContactPerson();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddTempContactPersonTable()
    {
        dtContactPerson.Columns.Add("SR_NO", typeof(int));
        dtContactPerson.Columns.Add("PID", typeof(int));
        dtContactPerson.Columns.Add("NAME", typeof(string));
        dtContactPerson.Columns.Add("MOBILE_NO", typeof(string));
        dtContactPerson.Columns.Add("PHONE", typeof(string));
        dtContactPerson.Columns.Add("EMAIL", typeof(string));
        dtContactPerson.Columns.Add("IS_DEFAULT", typeof(int));
        dtContactPerson.Columns.Add("IS_DELETED", typeof(int));
        dtContactPerson.Columns.Add("IS_REVISED", typeof(int));

        Session["dtContactPerson"] = dtContactPerson;
    }

    private void ResetContactPerson()
    {
        txtContactPersonNameToS.Text = string.Empty;
        txtContactPersonMobileToS.Text = string.Empty;
        txtContactPersonPhoneToS.Text = string.Empty;
        txtContactPersonEmailToS.Text = string.Empty;
        chkIsDefaultContactPersonToS.Checked = false;
    }






    private void SaveVendor()
    {
        try
        {
            string VendorName = string.Empty;
            int CategoryId = 0;
            //string Gstin = string.Empty;
            string Pan = string.Empty;
            int CreditDays = 0;
            double CreditLimit = 0;
            int MSMEStatusId = 0;
            int IsAssociated = 0;
            int IsIsoCertified = 0;
            int IsTechnicalDetailsReceived = 0;
            int IsVisitByQa = 0;
            int IsGovernment = 0;
            int IsOneTime = 0;
            int ResponsibleId = 0;
            int VendorTypeId = 0;
            int ItemCategoryFid = 0;
            int ItemSubcategoryFid = 0;
            int EntityTypeId = 0;
            int SavingType = 0;
            string Remarks = string.Empty;
            int CreatedBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int checkerId = Convert.ToInt32(ddlCheckerToS.SelectedValue);

            if (!string.IsNullOrEmpty(txtNameToS.Text))
                VendorName = FormattedString(txtNameToS.Text);

            if (ddlCategoryToS.SelectedIndex > 0)
                CategoryId = Convert.ToInt32(ddlCategoryToS.SelectedValue);

            if (ddlMSMEStatusToS.SelectedIndex > 0)
                MSMEStatusId = Convert.ToInt32(ddlMSMEStatusToS.SelectedValue);

            //if (!string.IsNullOrEmpty(txtGSTInToS.Text))
            //    Gstin = FormattedString(txtGSTInToS.Text);

            if (!string.IsNullOrEmpty(txtPANNumberToS.Text))
                Pan = FormattedString(txtPANNumberToS.Text);

            if (!string.IsNullOrEmpty(txtCreditDaysToS.Text))
                CreditDays = Convert.ToInt32(txtCreditDaysToS.Text);

            if (!string.IsNullOrEmpty(txtCreditLimitToS.Text))
                CreditLimit = Convert.ToDouble(txtCreditLimitToS.Text);

            if (!string.IsNullOrEmpty(txtRemarksToS.Text))
                Remarks = txtRemarksToS.Text;

            //IsAssociated = Convert.ToInt32(chkIsAssociatedToS.Checked);
            IsAssociated = Convert.ToInt32(rdAssociatedToS.SelectedValue);

            //IsIsoCertified = Convert.ToInt32(chkIsISOCertifiedToS.Checked);
            //IsTechnicalDetailsReceived = Convert.ToInt32(chkIsTechnicalDetailsReceivedToS.Checked);
            //IsVisitByQa = Convert.ToInt32(chkIsQAVisitedToS.Checked);
            //IsGovernment = Convert.ToInt32(chkIsGovernmentToS.Checked);

            IsIsoCertified = Convert.ToInt32(rdIsISOCertifiedToS.SelectedValue);
            IsTechnicalDetailsReceived = Convert.ToInt32(rdIsTechnicalDetailsReceivedToS.SelectedValue);
            IsVisitByQa = Convert.ToInt32(rdIsQAVisitedToS.SelectedValue);
            IsGovernment = Convert.ToInt32(rdIsGovernmentToS.SelectedValue);
            IsOneTime = Convert.ToInt32(rdIsOneTimeVendorToS.SelectedValue);

            if (ddlResponsibleToS.SelectedIndex > 0)
                ResponsibleId = Convert.ToInt32(ddlResponsibleToS.SelectedValue);

            if (ddlRelationTypeToS.SelectedIndex > 0)
                VendorTypeId = Convert.ToInt32(ddlRelationTypeToS.SelectedValue);

            if (ddlItemCategoryToS.SelectedIndex > 0)
                ItemCategoryFid = Convert.ToInt32(ddlItemCategoryToS.SelectedValue);

            if (ddlItemSubCategoryToS.SelectedIndex > 0)
                ItemSubcategoryFid = Convert.ToInt32(ddlItemSubCategoryToS.SelectedValue);

            EntityTypeId = (int)StatusAndTypes.EnumEntityType.Vendor;
            SavingType = Convert.ToInt32(rdSavingType.SelectedValue);

            DataTable dtAddressTable = new DataTable();
            DataTable dtBankTable = new DataTable();
            DataTable dtContactPersonTable = new DataTable();
            DataTable dtDOCsTable = new DataTable();


            dtAddressTable = GetBillingAddress();
            dtBankTable = GetBankDetails();
            dtContactPersonTable = GetContactPerson();
            dtDOCsTable = GetAttachment();

            if (dtAddressTable == null)
            {
                ExceptionMessage("Please add at least 1 billing address!!!");
                return;
            }
            else
            {
                int count = 0;
                foreach (DataRow dtt in dtAddressTable.Rows)
                {
                    string panTxt = "";
                    if (!string.IsNullOrEmpty(dtt["GSTIN"].ToString()))
                    {
                        panTxt = dtt["GSTIN"].ToString().Substring(2, 10);
                        if (panTxt != txtPANNumberToS.Text)
                        {
                            count++;
                            break;
                        }
                    }
                }

                if (count > 0)
                {
                    ExceptionMessage("1 or more PAN number(s) in GSTIN from Billing Address List are not matching with PAN!!!");
                    return;
                }
            }

            if (dtContactPersonTable == null)
            {
                ExceptionMessage("Please add at least 1 contact person!!!");
                return;
            }

            if (dtBankTable == null)
            {
                ExceptionMessage("Please add at least 1 bank detail!!!");
                return;
            }

            dtAddressTable = RemoveDatatabeColumns(dtAddressTable);
            dtContactPersonTable = RemoveDatatabeColumns(dtContactPersonTable);
            dtBankTable = RemoveDatatabeColumns(dtBankTable);
            dtDOCsTable = RemoveDatatabeColumns(dtDOCsTable);

            BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();

            int savedVendorPID = objVCM.RegisterVendor
                (
                      VendorName
                    , CategoryId
                    , MSMEStatusId
                    //, Gstin
                    , Pan
                    , CreditDays
                    , CreditLimit
                    , IsAssociated
                    , IsIsoCertified
                    , IsTechnicalDetailsReceived
                    , IsVisitByQa
                    , IsGovernment
                    , IsOneTime
                    , ResponsibleId
                    , VendorTypeId
                    , ItemCategoryFid
                    , ItemSubcategoryFid
                    , EntityTypeId
                    , dtAddressTable
                    , dtBankTable
                    , dtContactPersonTable
                    , dtDOCsTable
                    , SavingType
                    , Remarks
                    , CreatedBy
                    , checkerId
                );



            if (savedVendorPID > 0)
            {
                if (SavingType == Convert.ToInt32(StatusAndTypes.EnumSavingType.SaveAndSendForApproval))
                {
                    int sendMailValue = objMailService.ProcessMail(savedVendorPID, (int)StatusAndTypes.EnumStatus.Created_1);

                    if (sendMailValue > 0)
                    {
                        int val = objVCM.UpdateMailStatus(sendMailValue, (int)StatusAndTypes.EnumStatus.Created_1, CreatedBy);
                        SuccessMessage("Vendor created as '" + VendorName + "' and mail sent successfully.");
                        Reset();
                    }
                    else
                    {
                        SuccessMessage("Vendor created as '" + VendorName + "' successfully.");
                        Reset();
                    }
                }
                else
                {
                    SuccessMessage("Vendor requested as '" + VendorName + "' successfully.");
                    Reset();
                }

                ResetAll();

                ClearSessions();

            }
            else if (savedVendorPID < 0)
            {
                ExceptionMessage("Please try again! Vendor already exists with PAN: " + Pan);
                return;
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private byte[] GetFileBytes(string fileName, Stream stream)
    {
        Byte[] GSTbytes = null;
        #region
        try
        {
            string GSTFilePath = fileName;
            string GSTFileName = Path.GetFileName(GSTFilePath);
            string GSText = Path.GetExtension(GSTFileName);
            string GSTContentType = String.Empty;
            switch (GSText)
            {
                case ".jpg":
                    GSTContentType = "image/jpg";
                    break;
                case ".jpeg":
                    GSTContentType = "image/jpeg";
                    break;
                case ".bmp":
                    GSTContentType = "image/bmp";
                    break;
                case ".png":
                    GSTContentType = "image/png";
                    break;
                case ".gif":
                    GSTContentType = "image/gif";
                    break;
                case ".pdf":
                    GSTContentType = "application/pdf";
                    break;
                case ".JPG":
                    GSTContentType = "image/JPG";
                    break;
                case ".JPEG":
                    GSTContentType = "image/JPEG";
                    break;
                case ".BMP":
                    GSTContentType = "image/BMP";
                    break;
                case ".PNG":
                    GSTContentType = "image/PNG";
                    break;
                case ".GIF":
                    GSTContentType = "image/GIF";
                    break;
                case ".PDF":
                    GSTContentType = "application/PDF";
                    break;
                case ".dxf":
                    GSTContentType = "application/dxf";
                    break;
                case ".DXF":
                    GSTContentType = "application/DXF";
                    break;
                case ".dwg":
                    GSTContentType = "application/dwg";
                    break;
                case ".DWG":
                    GSTContentType = "application/DWG";
                    break;
            }
            Stream GSTfs = null;
            BinaryReader GSTbr = null;
            if (GSTContentType != String.Empty)
            {
                try
                {
                    GSTfs = stream;
                    GSTfs.Position = 0;
                    GSTbr = new BinaryReader(GSTfs);
                    GSTbytes = GSTbr.ReadBytes((Int32)GSTfs.Length);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ExceptionMessage("GST File format not recognised. Upload Image/PDF/DXF/DWG formats");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
        #endregion
        return GSTbytes;
    }

    private void Reset()
    {
        //try
        //{
        //    ddlLOTMainItems.SelectedIndex = 0;
        //    ddlLOTMainSubItems.SelectedIndex = 0;
        //    txtCustomerCode.Text = string.Empty;
        //    txtCustomerName.Text = string.Empty;
        //    txtJOBNo.Text = string.Empty;
        //    txtPONo.Text = string.Empty;
        //    txtItemName.Text = string.Empty;
        //    txtTFNo.Text = string.Empty;
        //    txtNotes.Text = string.Empty;

        //    gvJOBDetail.DataSource = null;
        //    Session["dtSubitem"] = null;



        //    dtTemp.Clear();
        //    dtSubitem.Clear();
        //    gvSubItem.DataSource = null;
        //    gvSubItem.DataBind();


        //    Session["dtAttachments"] = null;
        //    dtTempAttachments.Clear();
        //    gvAttachments.DataSource = null;
        //    gvAttachments.DataBind();


        //    ResetSubitems();
        //}
        //catch (Exception ex)
        //{
        //    ExceptionMessage(ex.ToString());
        //    return;
        //}
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

    //private DataTable GetBillingAddress()
    //{
    //    try
    //    {
    //        string Address1 = string.Empty;
    //        string Address2 = string.Empty;
    //        string Address3 = string.Empty;
    //        string City = string.Empty;

    //        int StateId = 0;
    //        int CountryId = 0;
    //        string OtherState = string.Empty;

    //        string Phone = string.Empty;
    //        string Email = string.Empty;
    //        int IsDefault = 0;

    //        if (!string.IsNullOrEmpty(txtAddress1ToS.Text))
    //            Address1 = Convert.ToString(txtAddress1ToS.Text);

    //        if (!string.IsNullOrEmpty(txtAddress2ToS.Text))
    //            Address2 = Convert.ToString(txtAddress2ToS.Text);

    //        if (!string.IsNullOrEmpty(txtAddress3ToS.Text))
    //            Address3 = Convert.ToString(txtAddress3ToS.Text);

    //        if (!string.IsNullOrEmpty(txtCityToS.Text))
    //            City = Convert.ToString(txtCityToS.Text);

    //        if (ddlStateToS.SelectedIndex > 0)
    //            StateId = Convert.ToInt32(ddlStateToS.SelectedValue);

    //        if (ddlCountryToS.SelectedIndex > 0)
    //            CountryId = Convert.ToInt32(ddlCountryToS.SelectedValue);

    //        if (!string.IsNullOrEmpty(txtOtherStateToS.Text))
    //            OtherState = Convert.ToString(txtOtherStateToS.Text);

    //        if (!string.IsNullOrEmpty(txtPhoneToS.Text))
    //            Phone = Convert.ToString(txtPhoneToS.Text);

    //        if (!string.IsNullOrEmpty(txtEmailToS.Text))
    //            Email = Convert.ToString(txtEmailToS.Text);




    //        dtTemp = new DataTable();

    //        dtTemp.Columns.Add("SR_NO", typeof(int));
    //        dtTemp.Columns.Add("PID", typeof(int));
    //        dtTemp.Columns.Add("ADDRESS_LINE1", typeof(string));
    //        dtTemp.Columns.Add("ADDRESS_LINE2", typeof(string));
    //        dtTemp.Columns.Add("ADDRESS_LINE3", typeof(string));
    //        dtTemp.Columns.Add("CITY", typeof(string));
    //        dtTemp.Columns.Add("STATE_FID", typeof(int));
    //        dtTemp.Columns.Add("COUNTRY_FID", typeof(int));
    //        dtTemp.Columns.Add("OTHER_STATE", typeof(string));
    //        dtTemp.Columns.Add("PHONE", typeof(string));
    //        dtTemp.Columns.Add("EMAIL", typeof(string));
    //        dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
    //        dtTemp.Columns.Add("IS_DELETED", typeof(int));
    //        dtTemp.Columns.Add("IS_REVISED", typeof(int));

    //        DataRow dr = dtTemp.NewRow();

    //        int index = dtTemp.Rows.Count + 1;

    //        dr["SR_NO"] = index;
    //        dr["PID"] = 0;
    //        dr["ADDRESS_LINE1"] = Address1;
    //        dr["ADDRESS_LINE2"] = Address2;
    //        dr["ADDRESS_LINE3"] = Address3;
    //        dr["CITY"] = City;

    //        dr["STATE_FID"] = StateId;
    //        dr["COUNTRY_FID"] = CountryId;
    //        dr["OTHER_STATE"] = OtherState;

    //        dr["PHONE"] = Phone;
    //        dr["EMAIL"] = Email;
    //        dr["IS_DEFAULT"] = 1;
    //        dr["IS_DELETED"] = 0;
    //        dr["IS_REVISED"] = 0;

    //        dtTemp.Rows.Add(dr);

    //        return dtTemp;
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return null;
    //    }
    //}

    private DataTable GetBillingAddress()
    {
        try
        {
            DataTable dt = new DataTable();

            if (Session["dtBillingAddress"] != null)
                dt = (DataTable)Session["dtBillingAddress"];
            else return null;

            dtTemp = new DataTable();

            dtTemp.Columns.Add("SR_NO", typeof(int));
            dtTemp.Columns.Add("PID", typeof(int));
            dtTemp.Columns.Add("ADDRESS_LINE1", typeof(string));
            dtTemp.Columns.Add("ADDRESS_LINE2", typeof(string));
            dtTemp.Columns.Add("ADDRESS_LINE3", typeof(string));
            dtTemp.Columns.Add("CITY", typeof(string));
            dtTemp.Columns.Add("STATE_FID", typeof(int));
            dtTemp.Columns.Add("COUNTRY_FID", typeof(int));
            dtTemp.Columns.Add("OTHER_STATE", typeof(string));
            dtTemp.Columns.Add("PIN_CODE", typeof(string));
            dtTemp.Columns.Add("PHONE", typeof(string));
            dtTemp.Columns.Add("EMAIL", typeof(string));
            dtTemp.Columns.Add("GSTIN", typeof(string));
            dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
            dtTemp.Columns.Add("IS_DELETED", typeof(int));
            dtTemp.Columns.Add("IS_REVISED", typeof(int));


            foreach (DataRow dro in dt.Rows)
            {
                DataRow dr = dtTemp.NewRow();

                int index = dtTemp.Rows.Count + 1;

                dr["SR_NO"] = index;
                dr["PID"] = dro["PID"];
                dr["ADDRESS_LINE1"] = dro["ADDRESS_LINE1"];
                dr["ADDRESS_LINE2"] = dro["ADDRESS_LINE2"];
                dr["ADDRESS_LINE3"] = dro["ADDRESS_LINE3"];
                dr["CITY"] = dro["CITY"];

                dr["STATE_FID"] = dro["STATE_ID"];
                dr["COUNTRY_FID"] = dro["COUNTRY_ID"];
                dr["OTHER_STATE"] = dro["OTHER_STATE"];
                dr["PIN_CODE"] = dro["PIN_CODE"];

                dr["PHONE"] = dro["PHONE"];
                dr["EMAIL"] = dro["EMAIL"];
                dr["GSTIN"] = dro["GSTIN"];

                dr["IS_DEFAULT"] = dro["IS_DEFAULT"];
                dr["IS_DELETED"] = 0;
                dr["IS_REVISED"] = 0;

                dtTemp.Rows.Add(dr);
            }

            return dtTemp;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }


    //private DataTable GetContactPerson()
    //{
    //    try
    //    {
    //        string Name = string.Empty;
    //        string Mobile = string.Empty;
    //        string Phone = string.Empty;
    //        string Email = string.Empty;
    //        int IsDefault = 0;


    //        if (!string.IsNullOrEmpty(txtContactPersonNameToS.Text))
    //            Name = Convert.ToString(txtContactPersonNameToS.Text);

    //        if (!string.IsNullOrEmpty(txtContactPersonMobileToS.Text))
    //            Mobile = Convert.ToString(txtContactPersonMobileToS.Text);

    //        if (!string.IsNullOrEmpty(txtContactPersonPhoneToS.Text))
    //            Phone = Convert.ToString(txtContactPersonPhoneToS.Text);

    //        if (!string.IsNullOrEmpty(txtContactPersonEmailToS.Text))
    //            Email = Convert.ToString(txtContactPersonEmailToS.Text);


    //        dtTemp = new DataTable();

    //        dtTemp.Columns.Add("SR_NO", typeof(int));
    //        dtTemp.Columns.Add("PID", typeof(int));
    //        dtTemp.Columns.Add("NAME", typeof(string));
    //        dtTemp.Columns.Add("MOBILE_NO", typeof(string));
    //        dtTemp.Columns.Add("PHONE", typeof(string));
    //        dtTemp.Columns.Add("EMAIL", typeof(string));
    //        dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
    //        dtTemp.Columns.Add("IS_DELETED", typeof(int));
    //        dtTemp.Columns.Add("IS_REVISED", typeof(int));

    //        DataRow dr = dtTemp.NewRow();

    //        int index = dtTemp.Rows.Count + 1;

    //        dr["SR_NO"] = index;
    //        dr["PID"] = 0;
    //        dr["NAME"] = Name;
    //        dr["MOBILE_NO"] = Mobile;
    //        dr["PHONE"] = Phone;
    //        dr["EMAIL"] = Email;
    //        dr["IS_DEFAULT"] = 1;
    //        dr["IS_DELETED"] = 0;
    //        dr["IS_REVISED"] = 0;

    //        dtTemp.Rows.Add(dr);

    //        return dtTemp;
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return null;
    //    }
    //}


    private DataTable GetContactPerson()
    {
        try
        {
            //string Name = string.Empty;
            //string Mobile = string.Empty;
            //string Phone = string.Empty;
            //string Email = string.Empty;
            //int IsDefault = 0;


            //if (!string.IsNullOrEmpty(txtContactPersonNameToS.Text))
            //    Name = Convert.ToString(txtContactPersonNameToS.Text);

            //if (!string.IsNullOrEmpty(txtContactPersonMobileToS.Text))
            //    Mobile = Convert.ToString(txtContactPersonMobileToS.Text);

            //if (!string.IsNullOrEmpty(txtContactPersonPhoneToS.Text))
            //    Phone = Convert.ToString(txtContactPersonPhoneToS.Text);

            //if (!string.IsNullOrEmpty(txtContactPersonEmailToS.Text))
            //    Email = Convert.ToString(txtContactPersonEmailToS.Text);

            DataTable dt = new DataTable();

            if (Session["dtContactPerson"] != null)
                dt = (DataTable)Session["dtContactPerson"];
            else return null;


            dtTemp = new DataTable();

            dtTemp.Columns.Add("SR_NO", typeof(int));
            dtTemp.Columns.Add("PID", typeof(int));
            dtTemp.Columns.Add("NAME", typeof(string));
            dtTemp.Columns.Add("MOBILE_NO", typeof(string));
            dtTemp.Columns.Add("PHONE", typeof(string));
            dtTemp.Columns.Add("EMAIL", typeof(string));
            dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
            dtTemp.Columns.Add("IS_DELETED", typeof(int));
            dtTemp.Columns.Add("IS_REVISED", typeof(int));


            foreach (DataRow dro in dt.Rows)
            {
                DataRow dr = dtTemp.NewRow();

                int index = dtTemp.Rows.Count + 1;

                dr["SR_NO"] = index;
                dr["PID"] = dro["PID"];
                dr["NAME"] = dro["NAME"];
                dr["MOBILE_NO"] = dro["MOBILE_NO"];
                dr["PHONE"] = dro["PHONE"];
                dr["EMAIL"] = dro["EMAIL"];

                dr["IS_DEFAULT"] = dro["IS_DEFAULT"];
                dr["IS_DELETED"] = 0;
                dr["IS_REVISED"] = 0;

                dtTemp.Rows.Add(dr);
            }


            return dtTemp;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    //private DataTable GetBankDetails()
    //{
    //    try
    //    {
    //        string BankName = string.Empty;
    //        string Branch = string.Empty;
    //        string SWIFTCode = string.Empty;
    //        string AccountNumber = string.Empty;
    //        string RTGSOrIFSC = string.Empty;
    //        string ISBN = string.Empty;
    //        int IsDefault = 0;


    //        if (!string.IsNullOrEmpty(txtBankNameToS.Text))
    //            BankName = Convert.ToString(txtBankNameToS.Text);

    //        if (!string.IsNullOrEmpty(txtBranchToS.Text))
    //            Branch = Convert.ToString(txtBranchToS.Text);

    //        if (!string.IsNullOrEmpty(txtSWIFTCodeToS.Text))
    //            SWIFTCode = Convert.ToString(txtSWIFTCodeToS.Text);

    //        if (!string.IsNullOrEmpty(txtAccountNumberToS.Text))
    //            AccountNumber = Convert.ToString(txtAccountNumberToS.Text);

    //        if (!string.IsNullOrEmpty(txtRTGSOrIFSCToS.Text))
    //            RTGSOrIFSC = Convert.ToString(txtRTGSOrIFSCToS.Text);

    //        if (!string.IsNullOrEmpty(txtISBNToS.Text))
    //            ISBN = Convert.ToString(txtISBNToS.Text);


    //        dtTemp = new DataTable();

    //        dtTemp.Columns.Add("SR_NO", typeof(int));
    //        dtTemp.Columns.Add("PID", typeof(int));
    //        dtTemp.Columns.Add("BANK_NAME", typeof(string));
    //        dtTemp.Columns.Add("BRANCH", typeof(string));
    //        dtTemp.Columns.Add("SWIFT_CODE", typeof(string));
    //        dtTemp.Columns.Add("ACCOUNT_NUMBER", typeof(string));
    //        dtTemp.Columns.Add("RTGS_OR_IFSC_CODE", typeof(string));
    //        dtTemp.Columns.Add("ISBN", typeof(string));
    //        dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
    //        dtTemp.Columns.Add("IS_DELETED", typeof(int));
    //        dtTemp.Columns.Add("IS_REVISED", typeof(int));

    //        DataRow dr = dtTemp.NewRow();

    //        int index = dtTemp.Rows.Count + 1;

    //        dr["SR_NO"] = index;
    //        dr["PID"] = 0;
    //        dr["BANK_NAME"] = BankName;
    //        dr["BRANCH"] = Branch;
    //        dr["SWIFT_CODE"] = SWIFTCode;
    //        dr["ACCOUNT_NUMBER"] = AccountNumber;
    //        dr["RTGS_OR_IFSC_CODE"] = RTGSOrIFSC;
    //        dr["ISBN"] = ISBN;
    //        dr["IS_DEFAULT"] = 1;
    //        dr["IS_DELETED"] = 0;
    //        dr["IS_REVISED"] = 0;

    //        dtTemp.Rows.Add(dr);

    //        return dtTemp;
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return null;
    //    }
    //}


    private DataTable GetBankDetails()
    {
        try
        {
            string BankName = string.Empty;
            string Branch = string.Empty;
            string SWIFTCode = string.Empty;
            string AccountNumber = string.Empty;
            string RTGSOrIFSC = string.Empty;
            string ISBN = string.Empty;

            if (!string.IsNullOrEmpty(txtBankNameToS.Text))
                BankName = Convert.ToString(txtBankNameToS.Text);

            if (!string.IsNullOrEmpty(txtBranchToS.Text))
                Branch = Convert.ToString(txtBranchToS.Text);

            if (!string.IsNullOrEmpty(txtSWIFTCodeToS.Text))
                SWIFTCode = Convert.ToString(txtSWIFTCodeToS.Text);

            if (!string.IsNullOrEmpty(txtAccountNumberToS.Text))
                AccountNumber = Convert.ToString(txtAccountNumberToS.Text);

            if (!string.IsNullOrEmpty(txtRTGSOrIFSCToS.Text))
                RTGSOrIFSC = Convert.ToString(txtRTGSOrIFSCToS.Text);

            if (!string.IsNullOrEmpty(txtISBNToS.Text))
                ISBN = Convert.ToString(txtISBNToS.Text);


            dtTemp = new DataTable();

            dtTemp.Columns.Add("SR_NO", typeof(int));
            dtTemp.Columns.Add("PID", typeof(int));
            dtTemp.Columns.Add("BANK_NAME", typeof(string));
            dtTemp.Columns.Add("BRANCH", typeof(string));
            dtTemp.Columns.Add("SWIFT_CODE", typeof(string));
            dtTemp.Columns.Add("ACCOUNT_NUMBER", typeof(string));
            dtTemp.Columns.Add("RTGS_OR_IFSC_CODE", typeof(string));
            dtTemp.Columns.Add("ISBN", typeof(string));
            dtTemp.Columns.Add("IS_DEFAULT", typeof(int));
            dtTemp.Columns.Add("IS_DELETED", typeof(int));
            dtTemp.Columns.Add("IS_REVISED", typeof(int));

            DataRow dr = dtTemp.NewRow();

            dr["SR_NO"] = 1;
            dr["PID"] = 1;
            dr["BANK_NAME"] = BankName;
            dr["BRANCH"] = Branch;
            dr["SWIFT_CODE"] = SWIFTCode;
            dr["ACCOUNT_NUMBER"] = AccountNumber;
            dr["RTGS_OR_IFSC_CODE"] = RTGSOrIFSC;
            dr["ISBN"] = ISBN;
            dr["IS_DEFAULT"] = 1;
            dr["IS_DELETED"] = 0;
            dr["IS_REVISED"] = 0;

            dtTemp.Rows.Add(dr);

            return dtTemp;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    //private DataTable GetAttachment()
    //{
    //    try
    //    {
    //        dtTemp = new DataTable();

    //        dtTemp.Columns.Add("SR_NO", typeof(int));
    //        dtTemp.Columns.Add("PID", typeof(int));
    //        dtTemp.Columns.Add("DOC_TYPE_FID", typeof(int));
    //        dtTemp.Columns.Add("DOC_TYPE", typeof(string));
    //        dtTemp.Columns.Add("DOC_NAME", typeof(string));
    //        dtTemp.Columns.Add("DOC", typeof(byte[]));
    //        dtTemp.Columns.Add("IS_DELETED", typeof(int));
    //        dtTemp.Columns.Add("IS_REVISED", typeof(int));

    //        byte[] FileBytesVRF = null;
    //        byte[] FileBytesGSTIN = null;
    //        byte[] FileBytesPAN = null;
    //        byte[] FileBytesCC = null;
    //        byte[] FileBytesOther1 = null;
    //        byte[] FileBytesOther2 = null;
    //        byte[] FileBytesOther3 = null;
    //        byte[] FileBytesOther4 = null;

    //        string FileNameVRF = string.Empty;
    //        string FileNameGSTIN = string.Empty;
    //        string FileNamePAN = string.Empty;
    //        string FileNameCC = string.Empty;
    //        string FileNameOther1 = string.Empty;
    //        string FileNameOther2 = string.Empty;
    //        string FileNameOther3 = string.Empty;
    //        string FileNameOther4 = string.Empty;




    //        if (fileUploadAttachmentVRF.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentVRF.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentVRF.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameVRF = str[str.Length - 1];
    //                FileBytesVRF = GetFileBytes(fileUploadAttachmentVRF.PostedFile.FileName, fileUploadAttachmentVRF.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentGSTIN.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentGSTIN.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentGSTIN.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameGSTIN = str[str.Length - 1];
    //                FileBytesGSTIN = GetFileBytes(fileUploadAttachmentGSTIN.PostedFile.FileName, fileUploadAttachmentGSTIN.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentPAN.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentPAN.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentPAN.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNamePAN = str[str.Length - 1];
    //                FileBytesPAN = GetFileBytes(fileUploadAttachmentPAN.PostedFile.FileName, fileUploadAttachmentPAN.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentCancelledCheque.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentCancelledCheque.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentCancelledCheque.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameCC = str[str.Length - 1];
    //                FileBytesCC = GetFileBytes(fileUploadAttachmentCancelledCheque.PostedFile.FileName, fileUploadAttachmentCancelledCheque.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentOther1.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentOther1.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentOther1.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameOther1 = str[str.Length - 1];
    //                FileBytesOther1 = GetFileBytes(fileUploadAttachmentOther1.PostedFile.FileName, fileUploadAttachmentOther1.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentOther2.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentOther2.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentOther2.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameOther2 = str[str.Length - 1];
    //                FileBytesOther2 = GetFileBytes(fileUploadAttachmentOther2.PostedFile.FileName, fileUploadAttachmentOther2.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentOther3.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentOther3.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentOther3.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameOther3 = str[str.Length - 1];
    //                FileBytesOther3 = GetFileBytes(fileUploadAttachmentOther3.PostedFile.FileName, fileUploadAttachmentOther3.PostedFile.InputStream);
    //            }
    //        }

    //        if (fileUploadAttachmentOther4.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(fileUploadAttachmentOther4.PostedFile.FileName))
    //            {
    //                string[] str = fileUploadAttachmentOther4.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                FileNameOther4 = str[str.Length - 1];
    //                FileBytesOther4 = GetFileBytes(fileUploadAttachmentOther4.PostedFile.FileName, fileUploadAttachmentOther4.PostedFile.InputStream);
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameVRF)) && FileBytesGSTIN != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 1;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.VRF;
    //            dr["DOC_NAME"] = FileNameVRF;
    //            dr["DOC"] = FileBytesVRF;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameGSTIN)) && FileBytesGSTIN != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 2;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.GSTIn;
    //            dr["DOC_NAME"] = FileNameGSTIN;
    //            dr["DOC"] = FileBytesGSTIN;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNamePAN)) && FileBytesPAN != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 3;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.PAN;
    //            dr["DOC_NAME"] = FileNamePAN;
    //            dr["DOC"] = FileBytesPAN;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameCC)) && FileBytesCC != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 4;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.CancelledCheque;
    //            dr["DOC_NAME"] = FileNameCC;
    //            dr["DOC"] = FileBytesCC;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameOther1)) && FileBytesOther1 != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 5;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.Other1;
    //            dr["DOC_NAME"] = FileNameOther1;
    //            dr["DOC"] = FileBytesOther1;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameOther2)) && FileBytesOther2 != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 6;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.Other2;
    //            dr["DOC_NAME"] = FileNameOther2;
    //            dr["DOC"] = FileBytesOther2;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameOther3)) && FileBytesOther3 != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 7;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.Other3;
    //            dr["DOC_NAME"] = FileNameOther3;
    //            dr["DOC"] = FileBytesOther3;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }

    //        if (!string.IsNullOrEmpty(Convert.ToString(FileNameOther4)) && FileBytesOther4 != null)
    //        {
    //            DataRow dr = dtTemp.NewRow();
    //            dr["SR_NO"] = 8;
    //            dr["PID"] = 0;
    //            dr["DOC_TYPE_FID"] = (int)StatusAndTypes.EnumDOCTypes.Other4;
    //            dr["DOC_NAME"] = FileNameOther4;
    //            dr["DOC"] = FileBytesOther4;
    //            dr["IS_DELETED"] = 0;
    //            dr["IS_REVISED"] = 0;
    //            dtTemp.Rows.Add(dr);
    //        }




    //        return dtTemp;


    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return null;
    //    }
    //}


    private DataTable GetAttachment()
    {
        try
        {

            DataTable dt = new DataTable();

            if (Session["dtAttachments"] != null)
                dt = (DataTable)Session["dtAttachments"];
            else return null;


            dtTemp = new DataTable();

            dtTemp.Columns.Add("SR_NO", typeof(int));
            dtTemp.Columns.Add("PID", typeof(int));
            dtTemp.Columns.Add("DOC_TYPE_FID", typeof(int));
            dtTemp.Columns.Add("DOC_TYPE", typeof(string));
            dtTemp.Columns.Add("DOC_NAME", typeof(string));
            dtTemp.Columns.Add("DOC", typeof(byte[]));
            dtTemp.Columns.Add("IS_DELETED", typeof(int));
            dtTemp.Columns.Add("IS_REVISED", typeof(int));

            foreach (DataRow dro in dt.Rows)
            {
                DataRow dr = dtTemp.NewRow();

                int index = dtTemp.Rows.Count + 1;

                dr["SR_NO"] = index;
                dr["PID"] = dro["PID"];
                dr["DOC_TYPE_FID"] = dro["DOC_TYPE_FID"];
                dr["DOC_NAME"] = dro["DOC_NAME"];
                dr["DOC"] = dro["DOC"];
                dr["IS_DELETED"] = dro["IS_DELETED"];
                dr["IS_REVISED"] = dro["IS_REVISED"];
                dtTemp.Rows.Add(dr);
            }

            return dtTemp;


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return null;
        }
    }

    private string FormattedString(string text)
    {
        if (!string.IsNullOrEmpty(text))
            return text.ToUpper().Trim();

        return "";
    }

    private DataTable RemoveDatatabeColumns(DataTable dataTable)
    {
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            if (dataTable.Columns.Contains("SR_NO"))
                dataTable.Columns.Remove("SR_NO");

            if (dataTable.Columns.Contains("SR_NO_IN"))
                dataTable.Columns.Remove("SR_NO_IN");

            if (dataTable.Columns.Contains("DOC_TYPE"))
                dataTable.Columns.Remove("DOC_TYPE");

            if (dataTable.Columns.Contains("ENTITY_FID"))
                dataTable.Columns.Remove("ENTITY_FID");

            if (dataTable.Columns.Contains("ENTITY_TYPE_FID"))
                dataTable.Columns.Remove("ENTITY_TYPE_FID");
        }

        return dataTable;
    }

    private void ResetAll()
    {
        txtNameToS.Text = string.Empty;
        txtCodeToS.Text = string.Empty;
        txtPANNumberToS.Text = string.Empty;
        txtCreditDaysToS.Text = "0";
        txtCreditLimitToS.Text = "0";
        ddlCategoryToS.SelectedIndex = 0;
        ddlMSMEStatusToS.SelectedIndex = 0;
        //chkIsAssociatedToS.Checked = false;
        rdAssociatedToS.SelectedIndex = 0;
        //rdisven.SelectedIndex = 0;
        ddlCheckerToS.SelectedIndex = 0;


        txtAddress1ToS.Text = string.Empty;
        txtAddress2ToS.Text = string.Empty;
        txtAddress3ToS.Text = string.Empty;
        txtCityToS.Text = string.Empty;
        ddlCountryToS.SelectedIndex = 0;
        ddlStateToS.SelectedIndex = 0;
        txtOtherStateToS.Text = string.Empty;
        txtPhoneToS.Text = string.Empty;
        txtEmailToS.Text = string.Empty;
        txtPANToSS.Text = string.Empty;
        txtGSTInRunningNoToS.Text = string.Empty;
        txtGSTInToS.Text = string.Empty;

        txtContactPersonNameToS.Text = string.Empty;
        txtContactPersonMobileToS.Text = string.Empty;
        txtContactPersonPhoneToS.Text = string.Empty;
        txtContactPersonEmailToS.Text = string.Empty;

        txtBankNameToS.Text = string.Empty;
        txtBranchToS.Text = string.Empty;
        txtRTGSOrIFSCToS.Text = string.Empty;
        txtAccountNumberToS.Text = string.Empty;
        txtSWIFTCodeToS.Text = string.Empty;
        txtISBNToS.Text = string.Empty;


        //chkIsTechnicalDetailsReceivedToS.Checked = false;
        //chkIsISOCertifiedToS.Checked = false;
        //chkIsQAVisitedToS.Checked = false;
        //chkIsGovernmentToS.Checked = false;

        rdIsTechnicalDetailsReceivedToS.SelectedIndex = 0;
        rdIsISOCertifiedToS.SelectedIndex = 0;
        rdIsQAVisitedToS.SelectedIndex = 0;
        rdIsGovernmentToS.SelectedIndex = 0;

        ddlResponsibleToS.SelectedIndex = 0;
        ddlRelationTypeToS.SelectedIndex = 0;
        ddlItemCategoryToS.SelectedIndex = 0;
        ddlItemSubCategoryToS.SelectedIndex = 0;

        txtRemarksToS.Text = string.Empty;


        ClearSessions();

        gvBillingAddress.DataSource = null;
        gvBillingAddress.DataBind();
        lblBillingAddressRecords.Text = "[0]";

        gvContactPerson.DataSource = null;
        gvContactPerson.DataBind();
        lblContactPersonRecords.Text = "[0]";

        gvAddAttachments.DataSource = null;
        gvAddAttachments.DataBind();
        lblAttachmentsRecords.Text = "[0]";

    }



    private void AddAttachment()
    {
        try
        {
            if (Session["dtAttachments"] != null)
                dtTemp = (DataTable)Session["dtAttachments"];
            else
                AddTempAttachmentsTable();

            dtTemp = (DataTable)Session["dtAttachments"];

            Byte[] FileBytes = null;
            string FileName = string.Empty;


            if (fileUploadAttachment.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment.PostedFile.FileName))
                {
                    string[] str = fileUploadAttachment.PostedFile.FileName.Split('\\');
                    int length = str.Length;
                    FileName = str[str.Length - 1];
                    FileBytes = GetFileBytes(fileUploadAttachment.PostedFile.FileName, fileUploadAttachment.PostedFile.InputStream);
                }
            }

            int index = 0;
            //foreach (DataRow dr in dtTemp.Select("DOC_TYPE_FID = " + Convert.ToInt32(ddlAttachmentType.SelectedValue)))
            //{
            //    if (Convert.ToInt32(dr["DOC_TYPE_FID"]) != (int)StatusAndTypes.EnumDOCTypes.Other1)
            //    {
            //        dtTemp.Rows.Remove(dr);
            //    }
            //}

            if (!string.IsNullOrEmpty(Convert.ToString(FileName)) && FileBytes != null)
            {
                index = dtTemp.Rows.Count + 1;
                DataRow dr = dtTemp.NewRow();
                dr["SR_NO"] = index;
                dr["PID"] = 0;
                dr["DOC_TYPE_FID"] = Convert.ToInt32(ddlAttachmentType.SelectedValue);
                dr["DOC_TYPE"] = Convert.ToString(ddlAttachmentType.SelectedItem.Text);
                dr["DOC_NAME"] = FileName;
                dr["DOC"] = FileBytes;
                dr["IS_DELETED"] = 0;
                dr["IS_REVISED"] = 0;
                dtTemp.Rows.Add(dr);
            }

            Session["dtAttachments"] = dtTemp;

            gvAddAttachments.DataSource = dtTemp;
            gvAddAttachments.DataBind();
            lblAttachmentsRecords.Text = "[" + gvAddAttachments.Rows.Count + "]";


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveAttachments(int srNo)
    {
        if (Session["dtAttachments"] != null)
            dtTemp = (DataTable)Session["dtAttachments"];
        else
            AddTempAttachmentsTable();

        dtTemp = (DataTable)Session["dtAttachments"];

        if (dtTemp.Rows.Count > 0)
        {
            DataRow[] dr = dtTemp.Select("SR_NO='" + srNo + "'");
            dtTemp.Rows.Remove(dr[0]);
        }

        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            int count = 0;
            foreach (DataRow drs in dtTemp.Rows)
            {
                count++;
                drs["SR_NO"] = count;
            }
        }


        Session["dtAttachments"] = dtTemp;

        gvAddAttachments.DataSource = dtTemp;
        gvAddAttachments.DataBind();

        lblAttachmentsRecords.Text = "[" + gvAddAttachments.Rows.Count + "]";
    }

    private void AddTempAttachmentsTable()
    {
        dtAttachments.Columns.Add("SR_NO", typeof(int));
        dtAttachments.Columns.Add("PID", typeof(int));
        dtAttachments.Columns.Add("DOC_TYPE_FID", typeof(int));
        dtAttachments.Columns.Add("DOC_TYPE", typeof(string));
        dtAttachments.Columns.Add("DOC_NAME", typeof(string));
        dtAttachments.Columns.Add("DOC", typeof(byte[]));
        dtAttachments.Columns.Add("IS_DELETED", typeof(int));
        dtAttachments.Columns.Add("IS_REVISED", typeof(int));

        Session["dtAttachments"] = dtAttachments;
    }

    #endregion


}