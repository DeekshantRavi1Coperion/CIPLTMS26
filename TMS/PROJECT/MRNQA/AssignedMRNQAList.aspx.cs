using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

public partial class PROJECT_MRNQA_AssignedMRNQAList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsEmployee = new DataSet();
    DataSet dsMRNList = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsMRNStatus = new DataSet();
    DataSet dsMailInfo = new DataSet();

    int recordID = 0;
    int actID = 0;
    int newStatusID = 0;
    string newStatus = string.Empty;

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string mrnNo = string.Empty;
    string poNo = string.Empty;
    string mrnDate = string.Empty;
    string vendorCode = string.Empty;
    string vendorName = string.Empty;
    string productCode = string.Empty;
    string productDesc = string.Empty;
    string quantity = string.Empty;
    string uom = string.Empty;
    string unitName = string.Empty;
    int unitID = 0;
    int statusID = 0;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["MRN_LIST"] = null;

                //hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtStartDateSearch.Text = hdStartDateSearch.Value;

                //hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtEndDateSearch.Text = hdEndDateSearch.Value;

                Session["dsEmployee"] = objProject.GetQualityChecker();

                BindUnit();
                BindStatus();
                GetAssignedMRNList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetAssignedMRNList();
    }

    protected void gvMRNList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;



                //Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                //Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
                Label lblAssignToID = (Label)e.Row.FindControl("lblAssignToID");
                Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");
                Label lblIsNewMailSent = (Label)e.Row.FindControl("lblIsNewMailSent");
                Label lblIsCheckedByID = (Label)e.Row.FindControl("lblIsCheckedByID");
                Label lblIsCheckedMailSent = (Label)e.Row.FindControl("lblIsCheckedMailSent");
                Label lblRejectedByID = (Label)e.Row.FindControl("lblRejectedByID");
                Label lblIsRejectedByMailSent = (Label)e.Row.FindControl("lblIsRejectedByMailSent");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                Label lblMRNNo = (Label)e.Row.FindControl("lblMRNNo");
                //Label lblMRNDate = (Label)e.Row.FindControl("lblMRNDate");
                //Label lblPONo = (Label)e.Row.FindControl("lblPONo");

                //Label lblVendorCode = (Label)e.Row.FindControl("lblVendorCode");
                //Label lblVendorName = (Label)e.Row.FindControl("lblVendorName");
                //Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                //Label lblProductDesc = (Label)e.Row.FindControl("lblProductDesc");
                //TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                //Label lblUOM = (Label)e.Row.FindControl("lblUOM");
                //Label lblUnit = (Label)e.Row.FindControl("lblUnit");

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgbtnSendMail = (ImageButton)e.Row.FindControl("imgbtnSendMail");
                Button btnCheck = (Button)e.Row.FindControl("btnCheck");

                btnCheck.Visible = false;
                imgbtnSendMail.Visible = false;
                imgStatus.Enabled = false;


                string currentStatus = lblStatus.Text;
                int currentStatusID = Convert.ToInt32(lblStatusID.Text);
                int empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

                //New
                if (currentStatus == "New" || currentStatusID == 1)
                {
                    imgStatus.ImageUrl = "~/Images/NEWICONS/New02.png";
                    imgStatus.ToolTip = "New";
                    if (Convert.ToInt32(lblCreatedByID.Text) == empRecordID)
                    {
                        if (Convert.ToInt32(lblIsNewMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Send mail for quality check/reject.";
                        }
                    }
                    if (Convert.ToInt32(lblAssignToID.Text) == empRecordID)
                    {
                        imgStatus.Enabled = true;
                        imgStatus.ToolTip = "Check/Reject MRN No.-: " + lblMRNNo.Text;

                        btnCheck.Visible = true;
                        btnCheck.ToolTip = "Check/Reject MRN No.-: " + lblMRNNo.Text;
                    }
                }

                //Checked
                else if (currentStatus == "Checked" || currentStatusID == 2)
                {
                    imgStatus.ImageUrl = "~/Images/Icons/yes3.png";
                    imgStatus.ToolTip = "Checked by the quality";

                    if (Convert.ToInt32(lblIsCheckedByID.Text) == empRecordID)
                    {
                        if (Convert.ToInt32(lblIsCheckedMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Send checked Mail.";
                        }
                    }
                }

                //Rejected
                else if (currentStatus == "Rejected" || currentStatusID == 2)
                {
                    imgStatus.ImageUrl = "~/Images/Icons/no2.png";
                    imgStatus.ToolTip = "Rejected by the quality";

                    if (Convert.ToInt32(lblRejectedByID.Text) == empRecordID)
                    {
                        if (Convert.ToInt32(lblIsRejectedByMailSent.Text) == 0)
                        {
                            imgbtnSendMail.Visible = true;
                            imgbtnSendMail.ToolTip = "Send rejected Mail.";
                        }
                    }
                }

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

    protected void gvMRNList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int recordID = 0;
                int rowindex = 0;


                mrnNo = string.Empty;
                mrnDate = string.Empty;
                poNo = string.Empty;
                vendorCode = string.Empty;
                vendorName = string.Empty;
                productCode = string.Empty;
                productDesc = string.Empty;
                quantity = string.Empty;
                uom = string.Empty;
                unitName = string.Empty;

                if (Convert.ToString(e.CommandArgument) == "SEND_MAIL" || Convert.ToString(e.CommandArgument) == "STATUS")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "CHECK")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }



                Label lblMRNNo = gvMRNList.Rows[rowindex].FindControl("lblMRNNo") as Label;
                Label lblMRNDate = gvMRNList.Rows[rowindex].FindControl("lblMRNDate") as Label;
                Label lblPONo = gvMRNList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblVendorCode = gvMRNList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvMRNList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblProductCode = gvMRNList.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDesc = gvMRNList.Rows[rowindex].FindControl("lblProductDesc") as Label;
                TextBox txtQuantity = gvMRNList.Rows[rowindex].FindControl("txtQuantity") as TextBox;
                Label lblUOM = gvMRNList.Rows[rowindex].FindControl("lblUOM") as Label;
                Label lblUnit = gvMRNList.Rows[rowindex].FindControl("lblUnit") as Label;


                Label lblRecordID = gvMRNList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblStatusID = gvMRNList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblUnitID = gvMRNList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblAssignToID = gvMRNList.Rows[rowindex].FindControl("lblAssignToID") as Label;
                Label lblCreatedByID = gvMRNList.Rows[rowindex].FindControl("lblCreatedByID") as Label;
                Label lblIsNewMailSent = gvMRNList.Rows[rowindex].FindControl("lblIsNewMailSent") as Label;
                Label lblIsCheckedByID = gvMRNList.Rows[rowindex].FindControl("lblIsCheckedByID") as Label;
                Label lblIsCheckedMailSent = gvMRNList.Rows[rowindex].FindControl("lblIsCheckedMailSent") as Label;
                Label lblRejectedByID = gvMRNList.Rows[rowindex].FindControl("lblRejectedByID") as Label;
                Label lblRejectedByMailSent = gvMRNList.Rows[rowindex].FindControl("lblRejectedByMailSent") as Label;

                Label lblStatus = gvMRNList.Rows[rowindex].FindControl("lblStatus") as Label;




                if (!string.IsNullOrEmpty(lblMRNNo.Text))
                    mrnNo = lblMRNNo.Text.Trim();

                if (!string.IsNullOrEmpty(lblMRNDate.Text))
                    mrnDate = Convert.ToDateTime(lblMRNDate.Text).ToString("dd-MMM-yyyy");

                if (!string.IsNullOrEmpty(lblVendorCode.Text))
                    vendorCode = lblVendorCode.Text.Trim();

                if (!string.IsNullOrEmpty(lblVendorName.Text))
                    vendorName = lblVendorName.Text.Trim();

                if (!string.IsNullOrEmpty(lblProductCode.Text))
                    productCode = lblProductCode.Text.Trim();

                if (!string.IsNullOrEmpty(lblProductDesc.Text))
                    productDesc = lblProductDesc.Text.Trim();

                if (!string.IsNullOrEmpty(txtQuantity.Text))
                    quantity = txtQuantity.Text;

                if (!string.IsNullOrEmpty(lblUOM.Text))
                    uom = lblUOM.Text.Trim();

                if (!string.IsNullOrEmpty(lblUnit.Text))
                    unitName = lblUnit.Text.Trim();



                recordID = Convert.ToInt32(lblRecordID.Text);
                ViewState["recordID"] = recordID;
                ViewState["MRN_NO"] = Convert.ToString(lblMRNNo.Text);
                ViewState["CURRENT_STATUS_ID"] = Convert.ToInt32(lblStatusID.Text);
                ViewState["CURRENT_STATUS"] = Convert.ToString(lblStatus.Text);
                ViewState["CREATED_BY_ID"] = Convert.ToInt32(lblCreatedByID.Text);

                if (Convert.ToString(e.CommandArgument) == "STATUS" || Convert.ToString(e.CommandArgument) == "CHECK")
                {
                    //New [For Check]
                    if (lblStatusID.Text == "1" || lblStatus.Text == "New")
                    {
                        ViewState["ACT_ID"] = 2;
                        ViewState["NEW_STATUS_ID"] = 2;
                    }

                    BindMRNDetails(mrnNo, mrnDate, poNo, vendorCode, vendorName, productCode, productDesc, quantity, uom, unitName);
                    this.ModalPopupExtender1.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "SEND_MAIL")
                {
                    int value = SendMail(recordID);
                    if (value > 0)
                    {
                        objProject.UpdateMRNMailSentStatus(recordID, Convert.ToInt32(lblStatusID.Text), Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        SuccessMessage("Mail sent successfully.");
                        GetAssignedMRNList();
                    }
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


    protected void btnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["recordID"] != null && Convert.ToInt32(ViewState["recordID"]) > 0)
                recordID = Convert.ToInt32(ViewState["recordID"]);

            if (ViewState["ACT_ID"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["ACT_ID"])))
                actID = Convert.ToInt32(ViewState["ACT_ID"]);

            if (ViewState["NEW_STATUS_ID"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["NEW_STATUS_ID"])))
                newStatusID = Convert.ToInt32(ViewState["NEW_STATUS_ID"]);


            if (Convert.ToInt32(hdConfirmValue.Value) > 0)
            {
                UpdateMRNStatus(recordID, txtMRNNoToEdit.Text, actID, newStatusID);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["ACT_ID"] = 3;
            ViewState["NEW_STATUS_ID"] = 3;

            if (ViewState["recordID"] != null && Convert.ToInt32(ViewState["recordID"]) > 0)
                recordID = Convert.ToInt32(ViewState["recordID"]);

            if (ViewState["ACT_ID"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["ACT_ID"])))
                actID = Convert.ToInt32(ViewState["ACT_ID"]);

            if (ViewState["NEW_STATUS_ID"] != null && !string.IsNullOrEmpty(Convert.ToString(ViewState["NEW_STATUS_ID"])))
                newStatusID = Convert.ToInt32(ViewState["NEW_STATUS_ID"]);

            if (Convert.ToInt32(hdConfirmValue.Value) > 0)
            {
                UpdateMRNStatus(recordID, txtMRNNoToEdit.Text, actID, newStatusID);
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

    private void BindStatus()
    {
        try
        {
            dsMRNStatus = objProject.GetMRNStatus();
            if (dsMRNStatus.Tables.Count > 0 && dsMRNStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsMRNStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetAssignedMRNList()
    {
        try
        {
            fromDate = string.Empty;
            toDate = string.Empty;
            unitID = 0;
            mrnNo = string.Empty;
            poNo = string.Empty;
            statusID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");

            if (ddlCompany.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtMRNNo.Text))
                mrnNo = txtMRNNo.Text.Trim();

            if (!string.IsNullOrEmpty(txtPOno.Text))
                poNo = txtPOno.Text.Trim();

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);



            dsMRNList = objProject.GetAssignedMRNList(fromDate, toDate, unitID, mrnNo, poNo, statusID);

            if (dsMRNList.Tables.Count > 0 && dsMRNList.Tables[0].Rows.Count > 0)
            {
                Session["MRN_LIST"] = dsMRNList;
                gvMRNList.DataSource = dsMRNList.Tables[0];
                gvMRNList.DataBind();
            }
            else
            {
                Session["MRN_LIST"] = null;
                gvMRNList.DataSource = null;
                gvMRNList.DataBind();
            }

            lblRecords.Text = "Records[" + gvMRNList.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindMRNDetails(string mrnNo, string mrnDate, string poNo, string vendorCode, string vendorName, string productCode, string productDesc, string quantity, string uom, string unitName)
    {
        try
        {
            txtMRNNoToEdit.Text = mrnNo;
            txtMRNDateToEdit.Text = mrnDate;
            txtPONoToEdit.Text = poNo;
            txtVendorCodeToEdit.Text = vendorCode;
            txtVendorNameToEdit.Text = vendorName;
            txtProductCodeToEdit.Text = productCode;
            txtProductDescToEdit.Text = productDesc;
            txtQuantityToEdit.Text = quantity;
            txtUOMToEdit.Text = uom;
            txtUnitNameToEdit.Text = unitName;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateMRNStatus(int recordID, string mrnNo, int actID, int newStatusID)
    {
        try
        {
            remarks = string.Empty;

            if (!string.IsNullOrEmpty(txtRemarksToEdit.Text))
                remarks = txtRemarksToEdit.Text;


            int value = objProject.UpdateMRNStatus(recordID, actID, newStatusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                int sendMailValue = SendMail(recordID);
                if (sendMailValue > 0)
                {
                    int sentMailStatusVal = objProject.UpdateMRNMailSentStatus(recordID, newStatusID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                }

                if (sendMailValue > 0)
                {
                    if (newStatusID == 2)
                        SuccessMessage(mrnNo + " checked and mail sent successfully...!!!");
                    else if (newStatusID == 3)
                        SuccessMessage(mrnNo + " rejected and mail sent successfully...!!!");
                }
                else
                {
                    if (newStatusID == 2)
                        SuccessMessage(mrnNo + " checked successfully...!!!");
                    else if (newStatusID == 3)
                        SuccessMessage(mrnNo + " rejected successfully...!!!");
                }
                GetAssignedMRNList();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(int recordID)
    {
        try
        {
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string fileName = string.Empty;


            mrnNo = string.Empty;
            mrnDate = string.Empty;
            poNo = string.Empty;
            vendorCode = string.Empty;
            vendorName = string.Empty;
            productCode = string.Empty;
            productDesc = string.Empty;
            quantity = string.Empty;
            uom = string.Empty;
            unitName = string.Empty;
            newStatusID = 0;
            newStatus = string.Empty;

            string fromName = string.Empty;
            string toName = string.Empty;

            string createdByName = string.Empty;
            string createdByEmail = string.Empty;
            string createdOn = string.Empty;
            string createdRemarks = string.Empty;

            string assignedToName = string.Empty;
            string assignedToEmail = string.Empty;
            string assignedOn = string.Empty;
            string assignedToRemarks = string.Empty;

            string checkedByName = string.Empty;
            string checkedByEmail = string.Empty;
            string checkedOn = string.Empty;
            string checkedRemarks = string.Empty;

            string rejectedByName = string.Empty;
            string rejectedByEmail = string.Empty;
            string rejectedOn = string.Empty;
            string rejectedRemarks = string.Empty;


            dsMailInfo = objProject.GetMRNSentMailInfo(recordID);

            if (dsMailInfo.Tables.Count > 0 && dsMailInfo.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsMailInfo.Tables[0].Rows)
                {
                    if (dr["STATUS_ID"] != DBNull.Value)
                        newStatusID = Convert.ToInt32(dr["STATUS_ID"]);

                    if (dr["STATUS_NAME"] != DBNull.Value)
                        newStatus = Convert.ToString(dr["STATUS_NAME"]);

                    if (dr["MRN_NO"] != DBNull.Value)
                        mrnNo = Convert.ToString(dr["MRN_NO"]);

                    if (dr["MRN_DATE"] != DBNull.Value)
                        mrnDate = Convert.ToDateTime(dr["MRN_DATE"]).ToString("dd-MMM-yyyy");

                    if (dr["PO_NO"] != DBNull.Value)
                        poNo = Convert.ToString(dr["PO_NO"]);

                    if (dr["VENDOR_CODE"] != DBNull.Value)
                        vendorCode = Convert.ToString(dr["VENDOR_CODE"]);

                    if (dr["VENDOR_NAME"] != DBNull.Value)
                        vendorName = Convert.ToString(dr["VENDOR_NAME"]);

                    if (dr["PRODUCT_CODE"] != DBNull.Value)
                        productCode = Convert.ToString(dr["PRODUCT_CODE"]);

                    if (dr["PRODUCT_DESC"] != DBNull.Value)
                        productDesc = Convert.ToString(dr["PRODUCT_DESC"]);

                    if (dr["QUANTITY"] != DBNull.Value)
                        quantity = Convert.ToString(dr["QUANTITY"]);

                    if (dr["UOM"] != DBNull.Value)
                        uom = Convert.ToString(dr["UOM"]);

                    if (dr["UNIT_NAME"] != DBNull.Value)
                        unitName = Convert.ToString(dr["UNIT_NAME"]);



                    if (dr["CREATED_BY"] != DBNull.Value)
                        createdByName = Convert.ToString(dr["CREATED_BY"]);

                    if (dr["CREATED_BY_EMAIL"] != DBNull.Value)
                        createdByEmail = Convert.ToString(dr["CREATED_BY_EMAIL"]);

                    if (dr["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToString(dr["CREATED_ON"]);

                    if (dr["ASSIGNED_TO_REMARKS"] != DBNull.Value)
                        createdRemarks = Convert.ToString(dr["ASSIGNED_TO_REMARKS"]);


                    if (dr["ASSIGNED_TO"] != DBNull.Value)
                        assignedToName = Convert.ToString(dr["ASSIGNED_TO"]);

                    if (dr["ASSIGNED_TO_EMAIL"] != DBNull.Value)
                        assignedToEmail = Convert.ToString(dr["ASSIGNED_TO_EMAIL"]);

                    if (dr["CREATED_ON"] != DBNull.Value)
                        assignedOn = Convert.ToString(dr["CREATED_ON"]);

                    if (dr["ASSIGNED_TO_REMARKS"] != DBNull.Value)
                        assignedToRemarks = Convert.ToString(dr["ASSIGNED_TO_REMARKS"]);


                    if (dr["CHECKED_BY"] != DBNull.Value)
                        checkedByName = Convert.ToString(dr["CHECKED_BY"]);

                    if (dr["CHECKED_BY_EMAIL"] != DBNull.Value)
                        checkedByEmail = Convert.ToString(dr["CHECKED_BY_EMAIL"]);

                    if (dr["CHECKED_ON"] != DBNull.Value)
                        checkedOn = Convert.ToString(dr["CHECKED_ON"]);

                    if (dr["CHECKED_REMARKS"] != DBNull.Value)
                        checkedRemarks = Convert.ToString(dr["CHECKED_REMARKS"]);




                    if (dr["REJECTED_BY"] != DBNull.Value)
                        rejectedByName = Convert.ToString(dr["REJECTED_BY"]);

                    if (dr["REJECTED_BY_EMAIL"] != DBNull.Value)
                        rejectedByEmail = Convert.ToString(dr["REJECTED_BY_EMAIL"]);

                    if (dr["REJECTED_ON"] != DBNull.Value)
                        rejectedOn = Convert.ToString(dr["REJECTED_ON"]);

                    if (dr["REJECTED_REMARKS"] != DBNull.Value)
                        rejectedRemarks = Convert.ToString(dr["REJECTED_REMARKS"]);

                }
            }

            //New
            if (newStatusID == 1 || newStatus == "New")
            {
                fromName = createdByName;
                from = createdByEmail;

                toName = assignedToName;
                to = assignedToEmail;

                subject = "MRN No.- " + mrnNo + " for Quality Check";
                fileName = "~/PROJECT/MRNQA/EMAIL_FORMATS/NewMRNMail.htm";
            }

            //Checked
            else if (newStatusID == 2 || newStatus == "Checked")
            {
                fromName = checkedByName;
                from = checkedByEmail;

                toName = createdByName;
                to = createdByEmail;

                subject = "MRN No.- " + mrnNo + " checked by Quality";
                fileName = "~/PROJECT/MRNQA/EMAIL_FORMATS/CheckedMRNMail.htm";
            }

            //Rejected
            else if (newStatusID == 3 || newStatus == "Rejected")
            {
                fromName = rejectedByName;
                from = rejectedByEmail;

                toName = createdByName;
                to = createdByEmail;

                subject = "MRN No.- " + mrnNo + " rejected by Quality";
                fileName = "~/PROJECT/MRNQA/EMAIL_FORMATS/RejectedMRNMail.htm";
            }

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
                    mail.To.Add(item);
                }
            }

            mail.IsBodyHtml = true;

            using (StreamReader reader = new StreamReader(Server.MapPath(fileName)))
            {
                body = reader.ReadToEnd();
            }

            //urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);
            //link = "'" + urlTxt + "/Login.aspx?mrnno=" + mrnNo + "&productcode=" + productCode + "'";
            //href = "<a href=" + link + ">Please Check MRN</a>";

            body = body.Replace("{#unit#}", unitName);
            body = body.Replace("{#mrnno#}", mrnNo);
            body = body.Replace("{#mrndate#}", mrnDate);
            body = body.Replace("{#pono#}", poNo);
            body = body.Replace("{#vendorname#}", ("[" + vendorCode + "]-" + vendorName));
            body = body.Replace("{#checkedremarks#}", checkedRemarks);
            body = body.Replace("{#rejectedremarks#}", rejectedRemarks);
            body = body.Replace("{#toname#}", toName);
            body = body.Replace("{#fromname#}", fromName);

            mail.Body = body;
            try
            {
                if (!string.IsNullOrEmpty(to))
                {
                    SmtpServer.Send(mail);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string exMsg = ex.ToString();
                if (exMsg.Contains("Mailbox unavailable. The server response was: 5.7.1 Unable to relay"))
                    return 1;

                else
                    return 0;
            }
        }
        catch (Exception ex)
        {
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
