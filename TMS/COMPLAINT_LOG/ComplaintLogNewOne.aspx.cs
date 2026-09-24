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
using System.IO;
using System.Net.Mail;

public partial class COMPLAINT_LOG_ComplaintLogNewOne : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Common objCommon = new BAL.Common();
    BAL.ComplaintLog objComplaintLog = new BAL.ComplaintLog();
    DataSet dsComplaintLog = new DataSet();
    DataSet dsCustomerJOBDetails = new DataSet();
    DataSet dsCustomerAddressDetails = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsComplaintLogDetails = new DataSet();
    DataSet dsTypeOfService = new DataSet();

    int complaintLogId = 0;
    string complaintReveivedDate = string.Empty;
    string complaintLogNo = string.Empty;
    int statusId = 0;
    string customerName = string.Empty;
    string customerCode = string.Empty;
    string address = string.Empty;
    string location = string.Empty;
    string plant = string.Empty;
    string jobNo = string.Empty;
    string poNo = string.Empty;
    string itemName = string.Empty;
    string modelNo = string.Empty;
    int serviceTypeID = 0;
    string complaintDescription = string.Empty;

    string attachmentOneFileName = string.Empty;
    string attachmentTwoFileName = string.Empty;

    //string lessonLearned = string.Empty;

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindTypeofService();

                hdComplanitRcvdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtComplanitRcvdDate.Text = hdComplanitRcvdDate.Value.ToString();

                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                if (Convert.ToInt32(Request.QueryString["complaintlogid"]) > 0)
                {
                    BindComplaintLogDetails();
                    btnSubmit.Text = "Update Complaint Log";
                }

                if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                    btnGetJobNo.Visible = true;
                else
                    btnGetJobNo.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetCustomer_Click(object sender, EventArgs e)
    {
        txtJOBNo.Text = string.Empty;
        txtPONumber.Text = string.Empty;
        ModalPopupExtender1.Show();
    }

    protected void btnGetJobNo_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        GetCustomerJobNoDetails();
    }

    protected void btnSearchCustomer_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Show();
        GetCustomerAddressList();
    }

    protected void btnGetJobNoSearch_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        GetCustomerJobNoDetails();
    }

    protected void gvCustomerAddressList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblCustomerCode = gvCustomerAddressList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvCustomerAddressList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblAddress = gvCustomerAddressList.Rows[rowindex].FindControl("lblAddress") as Label;

                txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                txtCustomerCode.Text = Convert.ToString(lblCustomerCode.Text).Trim();
                txtAddress.Text = Convert.ToString(lblAddress.Text).Trim(',');

                if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                    btnGetJobNo.Visible = true;
                else
                    btnGetJobNo.Visible = false;
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

    protected void gvCustomerJobList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJobNo = gvCustomerJobList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblPONo = gvCustomerJobList.Rows[rowindex].FindControl("lblPONo") as Label;

                txtJOBNo.Text = Convert.ToString(lblJobNo.Text).Trim();
                txtPONumber.Text = Convert.ToString(lblPONo.Text).Trim();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateComplaintLog();
    }

    protected void btnComplaintLogList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/COMPLAINT_LOG/ComplaintLogListNew.aspx");
    }

    #endregion


    #region METHODS[============================]

    private void BindTypeofService()
    {
        try
        {
            dsTypeOfService = objComplaintLog.GetTypeofService();
            if (dsTypeOfService.Tables.Count > 0 && dsTypeOfService.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfService.DataSource = dsTypeOfService.Tables[0];
                ddlTypeOfService.DataTextField = "SERVICE_TYPE";
                ddlTypeOfService.DataValueField = "SERVICE_TYPE_ID";
                ddlTypeOfService.DataBind();
                ddlTypeOfService.Items.Insert(0, "Select");
                ddlTypeOfService.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCustomerAddressList()
    {
        try
        {
            string customerName = string.Empty;
            string customerCode = string.Empty;
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

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

            if (!string.IsNullOrEmpty(txtCustmerNameSearch.Text))
                customerName = txtCustmerNameSearch.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                customerCode = txtCustomerCodeSearch.Text;
            else
                customerCode = string.Empty;

            lblCustomerMsg.Visible = false;
            dsCustomerAddressDetails = objComplaintLog.GetCustomerAddressList(customerName, customerCode, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsCustomerAddressDetails.Tables.Count > 0 && dsCustomerAddressDetails.Tables[0].Rows.Count > 0)
            {
                gvCustomerAddressList.DataSource = dsCustomerAddressDetails.Tables[0];
                gvCustomerAddressList.DataBind();
            }
            else
            {
                lblCustomerMsg.Visible = true;
                lblCustomerMsg.Text = "No Data found!";
                gvCustomerAddressList.DataSource = null;
                gvCustomerAddressList.DataBind();
            }
            lblRecords.Text = "Records[" + gvCustomerAddressList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void GetCustomerJobNoDetails()
    {
        try
        {
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;
            string customerCode = string.Empty;
            string jobNo = string.Empty;
            string poNo = string.Empty;

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

            customerCode = txtCustomerCode.Text;

            if (!string.IsNullOrEmpty(txtJobNoSearch.Text))
                jobNo = txtJobNoSearch.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;
            else
                poNo = string.Empty;

            lblJOBNoMsg.Visible = false;
            dsCustomerJOBDetails = objComplaintLog.GetCustomerJobNoDetails(customerCode, jobNo, poNo, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsCustomerJOBDetails.Tables.Count > 0 && dsCustomerJOBDetails.Tables[0].Rows.Count > 0)
            {
                gvCustomerJobList.DataSource = dsCustomerJOBDetails.Tables[0];
                gvCustomerJobList.DataBind();
            }
            else
            {
                lblJOBNoMsg.Visible = true;
                lblJOBNoMsg.Text = "No Data found!";
                gvCustomerJobList.DataSource = null;
                gvCustomerJobList.DataBind();
            }

            lblRecordsJobNo.Text = "Records[" + dsCustomerJOBDetails.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void BindComplaintLogDetails()
    {
        dsComplaintLog = objComplaintLog.GetComplaintLogInfoNew(Convert.ToInt32(Request.QueryString["complaintlogid"]));
        if (dsComplaintLog.Tables.Count > 0 && dsComplaintLog.Tables[0].Rows.Count > 0)
        {
            if (dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_RECEIVED_ON"] != DBNull.Value)
                txtComplanitRcvdDate.Text = Convert.ToDateTime(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_RECEIVED_ON"]).ToString("dd-MMM-yyyy");

            if (dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value)
                txtCustomerName.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_NAME"]);

            if (dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value)
                txtCustomerCode.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["CUSTOMER_CODE"]);

            if (dsComplaintLog.Tables[0].Rows[0]["LOCATION"] != DBNull.Value)
                txtLocation.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["LOCATION"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ADDRESS"] != DBNull.Value)
                txtAddress.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ADDRESS"]);

            if (dsComplaintLog.Tables[0].Rows[0]["PLANT"] != DBNull.Value)
                txtPlant.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["PLANT"]);

            if (dsComplaintLog.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                txtJOBNo.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["JOB_NO"]);

            if (dsComplaintLog.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                txtPONumber.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["PO_NO"]);

            if (dsComplaintLog.Tables[0].Rows[0]["ITEM_NAME"] != DBNull.Value)
                txtItemName.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["ITEM_NAME"]);

            if (dsComplaintLog.Tables[0].Rows[0]["MODEL_NO"] != DBNull.Value)
                txtModelNo.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["MODEL_NO"]);

            if (dsComplaintLog.Tables[0].Rows[0]["SERVICE_TYPE_ID"] != DBNull.Value)
                ddlTypeOfService.SelectedValue = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["SERVICE_TYPE_ID"]);

            if (dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_DESCRIPTION"] != DBNull.Value)
                txtComplaintDescription.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["COMPLAINT_DESCRIPTION"]);


            //if (dsComplaintLog.Tables[0].Rows[0]["LESSON_LEARNED"] != DBNull.Value)
            //    txtComplaintDescription.Text = Convert.ToString(dsComplaintLog.Tables[0].Rows[0]["LESSON_LEARNED"]);

        }
    }

    private void InsertUpdateComplaintLog()
    {
        try
        {
            if (Convert.ToInt32(Request.QueryString["complaintlogid"]) > 0)
                complaintLogId = Convert.ToInt32(Request.QueryString["complaintlogid"]);
            else
                complaintLogId = 0;

            complaintReveivedDate = Convert.ToDateTime(hdComplanitRcvdDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                customerCode = txtCustomerCode.Text;
            else
                customerCode = string.Empty;

            if (!string.IsNullOrEmpty(txtAddress.Text))
                address = txtAddress.Text;
            else
                address = string.Empty;

            if (!string.IsNullOrEmpty(txtLocation.Text))
                location = txtLocation.Text;
            else
                location = string.Empty;

            if (!string.IsNullOrEmpty(txtPlant.Text))
                plant = txtPlant.Text;
            else
                plant = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONumber.Text))
                poNo = txtPONumber.Text;
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtItemName.Text))
                itemName = txtItemName.Text;
            else
                itemName = string.Empty;

            if (!string.IsNullOrEmpty(txtModelNo.Text))
                modelNo = txtModelNo.Text;
            else
                modelNo = string.Empty;

            if (ddlTypeOfService.SelectedIndex > 0)
                serviceTypeID = Convert.ToInt32(ddlTypeOfService.SelectedValue);
            else
                serviceTypeID = 0;

            if (!string.IsNullOrEmpty(txtComplaintDescription.Text))
                complaintDescription = txtComplaintDescription.Text;
            else
                complaintDescription = string.Empty;

            //if (!string.IsNullOrEmpty(txtLessonLearned.Text))
            //    lessonLearned = txtLessonLearned.Text;
            //else
            //    lessonLearned = string.Empty;

            Byte[] attachmentOneBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment1.PostedFile.FileName))
                {
                    attachmentOneFileName = fileUploadAttachment1.PostedFile.FileName;
                    attachmentOneBytes = GetFileBytes(fileUploadAttachment1.PostedFile.FileName, fileUploadAttachment1.PostedFile.InputStream);
                }
                else
                {
                    attachmentOneFileName = string.Empty;
                    attachmentOneBytes = null;
                }
            }
            else
            {
                attachmentOneFileName = string.Empty;
                attachmentOneBytes = null;
            }

            Byte[] attachmentTwoBytes = null;
            if (fileUploadAttachment1.HasFile)
            {
                if (!string.IsNullOrEmpty(fileUploadAttachment2.PostedFile.FileName))
                {
                    attachmentTwoFileName = fileUploadAttachment2.PostedFile.FileName;
                    attachmentTwoBytes = GetFileBytes(fileUploadAttachment2.PostedFile.FileName, fileUploadAttachment2.PostedFile.InputStream);
                }
                else
                {
                    attachmentTwoFileName = string.Empty;
                    attachmentTwoBytes = null;
                }
            }
            else
            {
                attachmentTwoFileName = string.Empty;
                attachmentTwoBytes = null;
            }



            int value = objComplaintLog.InsertUpdateComplaintLogNewOne(complaintLogId, complaintReveivedDate, customerName, customerCode,
                                        address, location, plant, jobNo, poNo, itemName, modelNo, serviceTypeID, complaintDescription,
                                        attachmentOneFileName, attachmentOneBytes, attachmentTwoFileName, attachmentTwoBytes,
                                        Convert.ToInt32(Session["EMP_RECORD_ID"]));//lessonLearned

            if (value > 0)
            {
                if (complaintLogId > 0)
                {
                    dsComplaintLogDetails = objComplaintLog.GetComplaintLogInfoNew(complaintLogId);
                    int mailSentValue = SendMail(dsComplaintLogDetails);
                    if (mailSentValue > 0)
                    {
                        objComplaintLog.UpdateComplaintLogMailStatus(complaintLogId, 1);
                        if (dsComplaintLogDetails.Tables.Count > 0 && dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                            SuccessMessage("Complaint Log No. '" + Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]) + "' updated successfully and mail sent.");
                        else
                            SuccessMessage("Complaint Log No. updated successfully and mail sent.");
                    }
                    else
                    {
                        if (dsComplaintLogDetails.Tables.Count > 0 && dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                            SuccessMessage("Complaint Log No. '" + Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]) + "' updated successfully, and resend email from Complaint Log List.");
                        else
                            SuccessMessage("Complaint Log No. updated successfully, and resend email from Complaint Log List.");
                    }
                }
                else
                {
                    dsComplaintLogDetails = objComplaintLog.GetComplaintLogInfoNew(value);
                    int mailSentValue = SendMail(dsComplaintLogDetails);
                    if (mailSentValue > 0)
                    {
                        objComplaintLog.UpdateComplaintLogMailStatus(value, 1);
                        if (dsComplaintLogDetails.Tables.Count > 0 && dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                            SuccessMessage("Complaint Log No. '" + Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]) + "' added successfully and mail sent.");
                        else
                            SuccessMessage("Complaint Log No. added successfully and mail sent.");
                    }
                    else
                    {
                        if (dsComplaintLogDetails.Tables.Count > 0 && dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                            SuccessMessage("Complaint Log No. '" + Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]) + "' added successfully, and resend email from Complaint Log List.");
                        else
                            SuccessMessage("Complaint Log No. added successfully, and resend email from Complaint Log List.");
                    }

                }

                Reset();
            }
            else
            {
                ExceptionMessage("Please try again!");
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
   
    private void DeleteDirectory(string path)
    {
        try
        {
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }

            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
        }
        catch (Exception ex)
        {

            throw;
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
                //case ".msg":
                //    GSTContentType = "application/msg";
                //    break;
                //case ".MSG":
                //    GSTContentType = "application/MSG";
                //    break;
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
                ExceptionMessage("GST File format not recognised. Upload Image/PDF formats");
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
        try
        {
            hdComplanitRcvdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtComplanitRcvdDate.Text = hdComplanitRcvdDate.Value;

            txtCustomerName.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtAddress.Text = string.Empty;

            txtLocation.Text = string.Empty;
            txtPlant.Text = string.Empty;
            txtJOBNo.Text = string.Empty;
            txtPONumber.Text = string.Empty;

            txtItemName.Text = string.Empty;
            txtModelNo.Text = string.Empty;
            ddlTypeOfService.SelectedIndex = 0;
            txtComplaintDescription.Text = string.Empty;
            //txtLessonLearned.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private int SendMail(DataSet dsComplaintLogDetails)
    {
        try
        {
            string from = string.Empty;
            string to = string.Empty;
            string cc = string.Empty;

            string createdOn = string.Empty;
            string createdBy = string.Empty;
            string createdByEmail = string.Empty;

            int assignedToEmpRecordID = 0;
            string assignedToName = string.Empty;
            string assignedToEmail = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;

            //string assignHref = string.Empty;
            //string assignLink = string.Empty;

            string href = string.Empty;
            string link = string.Empty;

            if (dsComplaintLogDetails.Tables.Count > 0)
            {
                if (dsComplaintLogDetails.Tables[0].Rows.Count > 0)
                {

                    complaintLogId = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_ID"]);
                    complaintLogNo = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["COMPLAINT_LOG_NO"]);
                    statusId = Convert.ToInt32(dsComplaintLogDetails.Tables[0].Rows[0]["STATUS_ID"]);


                    createdBy = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_NAME"]);
                    createdByEmail = Convert.ToString(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_BY_EMAIL_ID"]);
                    if (dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_ON"] != DBNull.Value)
                        createdOn = Convert.ToDateTime(dsComplaintLogDetails.Tables[0].Rows[0]["CREATED_ON"]).ToString("dd-MMM-yyyy");
                }

                if (dsComplaintLogDetails.Tables[1].Rows.Count > 0)
                {
                    assignedToEmpRecordID = Convert.ToInt32(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMP_RECORD_ID"]);
                    assignedToEmail = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_EMAIL_ID"]);
                    userName = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_USER_NAME"]);
                    password = Convert.ToString(dsComplaintLogDetails.Tables[1].Rows[0]["SERVICE_HEAD_PASSWORD"]);
                }
            }

            from = createdByEmail;
            to = assignedToEmail;            
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(to))
            {
                string[] strTo = to.Split(';');
                foreach (string item in strTo)
                {
                    mail.To.Add(item);
                }
            }

            mail.Subject = "Complaint Log No.: '" + complaintLogNo + "' Created On: " + createdOn;
            mail.IsBodyHtml = true;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/COMPLAINT_LOG/EMAIL_FORMATS/02AssignDeptMail.htm")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{#complaintlogno#}", complaintLogNo);
            body = body.Replace("{#complaintrcvddate#}", Convert.ToDateTime(hdComplanitRcvdDate.Value).ToString("dd-MMM-yyyy"));
            body = body.Replace("{#location#}", txtLocation.Text);
            body = body.Replace("{#customername#}", txtCustomerName.Text);
            body = body.Replace("{#address#}", txtAddress.Text);
            body = body.Replace("{#plant#}", txtPlant.Text);
            body = body.Replace("{#jobno#}", txtJOBNo.Text);
            body = body.Replace("{#pono#}", txtPONumber.Text);
            body = body.Replace("{#itemname#}", txtItemName.Text);
            body = body.Replace("{#modelno#}", txtModelNo.Text);
            body = body.Replace("{#servicetype#}", ddlTypeOfService.SelectedItem.Text);
            body = body.Replace("{#complaintdesc#}", txtComplaintDescription.Text);
            body = body.Replace("{#createdby#}", createdBy);

            string urlTxt = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["URL"]);

            //assignLink = "'" + urlTxt + "/COMPLAINT_LOG/AssignComplaintLogNew.aspx?complaintlogid=" + complaintLogId + "&complaintlogno=" + complaintLogNo + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(assignedToEmpRecordID) + "'";
            //assignHref = "<a href=" + assignLink + ">Assign Complaint Log</a>";

            //link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNew.aspx?complaintlogid=" + complaintLogId + "&complaintlogno=" + complaintLogNo + "&ud=" + userName + "&pd=" + password + "&emprecordid=" + Convert.ToString(assignedToEmpRecordID) + "&actid=2&statusid=" + statusId + "'";
            link = "'" + urlTxt + "/COMPLAINT_LOG/UpdateComplaintLogStatusNewOne.aspx?complaintlogid=" + complaintLogId + "&complaintlogno=" + complaintLogNo + "&ud=" + userName + "&pd=" + password + "&actid=2'";
            href = "<a href=" + link + ">Forward To Responsible Department</a>";

            body = body.Replace("{#link#}", href);
            mail.Body = body;

            SmtpServer.Host = "eusmtp.hi.corp";
            SmtpServer.Port = 25;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                SmtpServer.Send(mail);
                return 1;
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

    #endregion

}
