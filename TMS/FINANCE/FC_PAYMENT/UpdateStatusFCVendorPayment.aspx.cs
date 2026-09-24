using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Net.Mime;
using iTextSharp.tool.xml;

public partial class FINANCE_FC_PAYMENT_UpdateStatusFCVendorPayment : System.Web.UI.Page
{


    #region VARIABLES[=======================]

    BAL.FCVendorPayment objFCVendorPayment = new BAL.FCVendorPayment();
    FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();

    GetLOTMailTypeAndStatus objGetLOTMailTypeAndStatus = new GetLOTMailTypeAndStatus();
    LOTMailTypeAndStatusProperties objLOTMailTypeAndStatusProperties = new LOTMailTypeAndStatusProperties();
    GetLOTApproverStatus objGetLOTApproverStatus = new GetLOTApproverStatus();

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsSubitems = new DataSet();
    DataTable dtSubitemToAdd = new DataTable();
    DataSet dsApprovers = new DataSet();
    DataSet dsDetails = new DataSet();
    DataSet dsMailInfo = new DataSet();
    DataTable dtNew = new DataTable();

    LOTHtmlForPDF objLOTHtmlForPDF = new LOTHtmlForPDF();

    int actID = 0;
    int amdActID = 0;
    int PID = 0;
    //int LOTMainSubItemID = 0;
    string companyName = string.Empty;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string TFNo = string.Empty;
    string poNo = string.Empty;
    string LOTDate = string.Empty;
    string itemName = string.Empty;
    //string LOTMainItem = string.Empty;
    string impNotes = string.Empty;
    string remarks = string.Empty;

    string drawing1Txt = string.Empty;
    string drawing1Extn = string.Empty;

    string drawing2Txt = string.Empty;
    string drawing2Extn = string.Empty;

    string drawing3Txt = string.Empty;
    string drawing3Extn = string.Empty;

    string drawing4Txt = string.Empty;
    string drawing4Extn = string.Empty;

    int newStatusID = 0;
    int currentStatusID = 0;
    string currentStatus = string.Empty;
    string subject = string.Empty;
    string from = string.Empty;
    string to = string.Empty;
    string cc = string.Empty;
    string fileName = string.Empty;
    string body = string.Empty;


    int createdByID = 0;
    string createrName = string.Empty;
    string createrEmail = string.Empty;

    int amendedByID = 0;
    string amendedByName = string.Empty;
    string amendedByEmail = string.Empty;

    int PEApproverID = 0;
    string PEApproverName = string.Empty;
    string PEApproverEmail = string.Empty;

    string PEApprovedByName = string.Empty;
    string PEApprovedByEmail = string.Empty;

    int PMApproverID = 0;
    string PMApproverName = string.Empty;
    string PMApproverEmail = string.Empty;
    string PMUD = string.Empty;
    string PMPD = string.Empty;

    string PMApprovedByName = string.Empty;
    string PMApprovedByEmail = string.Empty;

    string acceptedByName = string.Empty;
    string acceptedByEmail = string.Empty;

    int productionManagerID = 0;
    string productionManagerName = string.Empty;
    string productionManagerEmail = string.Empty;
    string productionManagerEmailCC = string.Empty;

    int amendmentByID = 0;
    string amendmentByName = string.Empty;

    string completedByName = string.Empty;
    string completedByEmail = string.Empty;

    string amendedPEApprovedByName = string.Empty;
    string amendedPEApprovedByEmail = string.Empty;

    string amendedPMApprovedByName = string.Empty;
    string amendedPMApprovedByEmail = string.Empty;

    string amendedAcceptedByName = string.Empty;
    string amendedAcceptedByEmail = string.Empty;

    string urlTxt = string.Empty;
    string href = string.Empty;
    string link = string.Empty;

    string approveHref = string.Empty;
    string approveLink = string.Empty;

    string amendmentHref = string.Empty;
    string amendmentLink = string.Empty;

    int PEID = 0;
    int PMID = 0;
    int approvedByID = 0;
    int prodMngrID = 0;
    int acceptedByID = 0;
    int amendmentCount = 0;

    int amendedApprovedByID = 0;
    int amendedAcceptedByID = 0;

    int nextStatusID = 0;
    int statusID = 0;
    string subitemDesc = string.Empty;
    string tagNo = string.Empty;

    string LOTMainItem = string.Empty;
    string LOTMainSubItem = string.Empty;
    int LOTMainItemID = 0;
    int LOTTFSubitemID = 0;
    int LOTMainSubitemID = 0;
    string LOTMainSubItemIDs = string.Empty;


    string drgNo = string.Empty;
    int revisionNo = 0;
    int quantity = 0;
    string categoryID = string.Empty;
    string category = string.Empty;

    string subitemAttachment1File = string.Empty;
    string subitemAttachment2File = string.Empty;
    string subitemAttachment3File = string.Empty;
    string subitemAttachment4File = string.Empty;

    Byte[] subitemAttachment1FileBytes = null;
    Byte[] subitemAttachment2FileBytes = null;
    Byte[] subitemAttachment3FileBytes = null;
    Byte[] subitemAttachment4FileBytes = null;


    string attachment1Txt = string.Empty;
    string attachment1Extn = string.Empty;
    string attachment2Txt = string.Empty;
    string attachment2Extn = string.Empty;
    string attachment3Txt = string.Empty;
    string attachment3Extn = string.Empty;
    string attachment4Txt = string.Empty;
    string attachment4Extn = string.Empty;

    string tableName = string.Empty;
    private int _FCStatusID;
    private int _FCVendorPaymentPID;
    private string _Remarks;
    private int _CreatedBy;

    #endregion

    public int FCVendorPaymentPID
    {
        get
        {
            _FCVendorPaymentPID = Convert.ToInt32(Request.QueryString["PID"]);
            return _FCVendorPaymentPID;
        }

        set
        {
            _FCVendorPaymentPID = value;
        }
    }

    public int FCStatusID
    {
        get
        {
            _FCStatusID = Convert.ToInt32(ViewState["StatusID"]);
            return _FCStatusID;
        }

        set
        {
            _FCStatusID = value;
        }
    }

    public string Remarks
    {
        get
        {
            _Remarks = txtApprovedOrAmendedApprovedRemarksUS.Text;
            return _Remarks;
        }

        set
        {
            _Remarks = value;
        }
    }

    public int CreatedBy
    {
        get
        {
            _CreatedBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _CreatedBy;
        }

        set
        {
            _CreatedBy = value;
        }
    }


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Request.QueryString["PID"]) > 0)
            {
                ViewState["Amendmentcount"] = 0;
                ValidateAndLoginAndApprove();
                GetStatusMessage(0);
                //if (!IsPostBack)
                //{
                //    hdConfirmValue.Value = "0";
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }

    protected void imgBtnViewInvoiceFile_Click(object sender, ImageClickEventArgs e)
    {
        ViewAttachmentFiles(FCVendorPaymentPID, "VIEW_INVOICE_FILE", lblInvoiceFileUS.Text);
    }


    protected void btnUpdateSatus_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        //{
        //    UpdatePaymentRequestSatus(Convert.ToInt32(EnumActID.Approve));
        //}
        //else
        //{
        //    Response.Redirect("~/Login.aspx");
        //}

        UpdatePaymentRequestSatus(Convert.ToInt32(EnumActID.Approve));
    }

    protected void btnAmendment_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        //{
        //    UpdatePaymentRequestSatus(Convert.ToInt32(EnumActID.SendToAmendment));
        //}
        //else
        //{
        //    Response.Redirect("~/Login.aspx");
        //}

        UpdatePaymentRequestSatus(Convert.ToInt32(EnumActID.SendToAmendment));
    }

    protected void btnViewList_Click(object sender, EventArgs e)
    {
        PID = 0;
        PMUD = string.Empty;
        PMPD = string.Empty;

        if (Convert.ToInt32(Request.QueryString["PID"]) > 0)
            PID = Convert.ToInt32(Request.QueryString["PID"]);

        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["ud"])))
            PMUD = Convert.ToString(Request.QueryString["ud"]).Trim();

        if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["pd"])))
            PMPD = Convert.ToString(Request.QueryString["pd"]).Trim();

        Response.Redirect("/FINANCE/FC_PAYMENT/FCVendorPaymentsList.aspx?paymentRequestNo=" + txtPaymentRequestNoUS.Text);
    }

    #endregion






    #region METHODS[=========================]

    private void ValidateAndLoginAndApprove()
    {
        try
        {
            DataSet ds = new DataSet();
            string userName = string.Empty;
            string password = string.Empty;
            userName = Convert.ToString(Request.QueryString["ud"]).Trim();
            password = Convert.ToString(Request.QueryString["pd"]).Trim();

            ds = objCommon.ValidateAndLogin(userName, password);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["EMP_RECORD_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EMP_RECORD_ID"]);
                Session["EMPLOYEE_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);
                Session["USER_NAME"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_NAME"]);
                Session["PASSWORD"] = Convert.ToString(ds.Tables[0].Rows[0]["PASSWORD"]);
                Session["EMAIL_ID"] = Convert.ToString(ds.Tables[0].Rows[0]["EMAIL_ID"]);
                Session["USER_TYPE"] = Convert.ToString(ds.Tables[0].Rows[0]["USER_TYPE"]);
                Session["IS_TEAMLEADER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["IS_TEAMLEADER"]);
                Session["TEAMLEADER_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TEAMLEADER_ID"]);
                Session["DEPARTMENT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT_ID"]);
                Session["TIMESHEET_DEPT_ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TIMESHEET_DEPT_ID"]);
                Session["TS_APPROVAL_BY"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TS_APPROVAL_BY"]);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["TEAMMEMBERS"] = ds.Tables[1];
                }

                if (ds.Tables.Count > 0)
                {
                    Session["USERINFO"] = ds;
                }

                BindFCVendorPaymentDetails();
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

    private void UpdatePaymentRequestSatus(int actID)
    {
        try
        {
            int UpdatingSatusID = 0;

            if (actID == (int)EnumActID.Approve)
            {
                if (Convert.ToInt32(ViewState["Amendmentcount"]) > 0)
                {
                    UpdatingSatusID = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED;
                }
                else
                {
                    UpdatingSatusID = (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED;
                }
            }
            else if (actID == (int)EnumActID.SendToAmendment)
            {
                UpdatingSatusID = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT;
            }

            int value = objFCVendorPayment.UpdatePaymentRequestSatus
            (
                FCVendorPaymentPID
              , UpdatingSatusID
              , Remarks
              , ""
              , ""
              , ""
              , null
              , CreatedBy
            );

            if (value > 0)
            {
                FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();
                int mailSentValue = objFCVendorPaymentSendMail.ProcessSendEmail(FCVendorPaymentPID, 0);

                ShowUpdateStatusMessages(mailSentValue, UpdatingSatusID);

                if (mailSentValue > 0)
                {
                    int mailstatusVal = 0;
                    mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(FCVendorPaymentPID, UpdatingSatusID);
                }
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }

            GetStatusMessage(UpdatingSatusID);
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void ShowUpdateStatusMessages(int mailstatusVal, int updatingSatusID)
    {
        if (mailstatusVal == 0)
        {
            if (updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
                updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {
                SuccessMessage("Payment Request approved successfully. Please go to list to send email!");
            }
            else if (updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                SuccessMessage("Payment Request sent to amendment successfully. Please go to list to send email!");
            }
        }
        else
        {
            if (updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
                updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {
                SuccessMessage("Payment Request approved and mail sent successfully.");
            }
            else if (updatingSatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                SuccessMessage("Payment Request sent to amendment and mail sent successfully.");
            }
        }

    }




    private void BindFCVendorPaymentDetails()
    {
        try
        {
            //remarks = string.Empty;

            lblInvoiceFileUS.Text = string.Empty;
            imgBtnViewInvoiceFile.Visible = false;

            //hdTFNo.Value = string.Empty;
            //hdStatusID.Value = "0";
            //hdPEID.Value = "0";
            //hdPMID.Value = "0";
            //hdTableName.Value = string.Empty;
            //createdByID = 0;
            //PEID = 0;
            //PMID = 0;
            //amendmentCount = 0;
            //newStatusID = 0;

            string createdRemarks = string.Empty;
            string amendedRemarks = string.Empty;
            int amendmentcount = 0;

            PID = 0;
            if (Convert.ToInt32(Request.QueryString["PID"]) > 0)
                PID = Convert.ToInt32(Request.QueryString["PID"]);

            dsDetails = objFCVendorPayment.GetVendorPaymentDetailsByPID(PID);

            if (dsDetails.Tables.Count > 0)
            {
                //Session["dsLOTTFDetails"] = dsDetails;

                if (dsDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsDetails.Tables[0].Rows[0]["PAYMENT_REQUEST_NO"] != DBNull.Value)
                        txtPaymentRequestNoUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["PAYMENT_REQUEST_NO"]);

                    if (dsDetails.Tables[0].Rows[0]["STATUS_FID"] != DBNull.Value)
                        ViewState["StatusID"] = Convert.ToString(dsDetails.Tables[0].Rows[0]["STATUS_FID"]);

                    if (dsDetails.Tables[0].Rows[0]["JOB_NO"] != DBNull.Value)
                        txtJOBNoUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["JOB_NO"]);

                    if (dsDetails.Tables[0].Rows[0]["VENDOR_CODE"] != DBNull.Value)
                        txtVendorCodeUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["VENDOR_CODE"]);

                    if (dsDetails.Tables[0].Rows[0]["VENDOR_NAME"] != DBNull.Value)
                        txtVendorNameUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["VENDOR_NAME"]);

                    if (dsDetails.Tables[0].Rows[0]["VENDOR_ADDRESS"] != DBNull.Value)
                        txtVendorAddressUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["VENDOR_ADDRESS"]);

                    if (dsDetails.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                        txtPONoUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["PO_NO"]);

                    if (dsDetails.Tables[0].Rows[0]["PO_AMOUNT"] != DBNull.Value)
                        txtTotalPOAmountUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["PO_AMOUNT"]);

                    if (dsDetails.Tables[0].Rows[0]["PO_CURRENCY"] != DBNull.Value)
                        txtPOAmountCurrencyUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["PO_CURRENCY"]);

                    if (dsDetails.Tables[0].Rows[0]["AMOUNT_TO_BE_RELEASED"] != DBNull.Value)
                        txtReleasedAmountUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["AMOUNT_TO_BE_RELEASED"]);

                    if (dsDetails.Tables[0].Rows[0]["CUT_OFF_DATE"] != DBNull.Value)
                        txtCutOffDateUS.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["CUT_OFF_DATE"]).ToString("dd-MMM-yyyy");

                    if (dsDetails.Tables[0].Rows[0]["PAYMENT_TYPE"] != DBNull.Value)
                        txtPaymentTypeUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["PAYMENT_TYPE"]);

                    if (dsDetails.Tables[0].Rows[0]["INVOICE_NO"] != DBNull.Value)
                        txtVendorInvoiceNoUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["INVOICE_NO"]);

                    if (dsDetails.Tables[0].Rows[0]["INVOICE_DATE"] != DBNull.Value)
                        txtVendorInvoiceDateUS.Text = Convert.ToDateTime(dsDetails.Tables[0].Rows[0]["INVOICE_DATE"]).ToString("dd-MMM-yyyy"); ;

                    if (dsDetails.Tables[0].Rows[0]["BANK_NAME"] != DBNull.Value)
                        txtVendorBankNameUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["BANK_NAME"]);

                    if (dsDetails.Tables[0].Rows[0]["BANK_BRANCH"] != DBNull.Value)
                        txtBankVendorBranchUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["BANK_BRANCH"]);

                    if (dsDetails.Tables[0].Rows[0]["SWIFT_CODE"] != DBNull.Value)
                        txtVendorSwiftCodeUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["SWIFT_CODE"]);

                    if (dsDetails.Tables[0].Rows[0]["BANK_ACCOUNT_NO"] != DBNull.Value)
                        txtBankAccountNumberUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["BANK_ACCOUNT_NO"]);


                    if (dsDetails.Tables[0].Rows[0]["AMENDMENT_COUNT"] != DBNull.Value)
                        amendmentcount = Convert.ToInt32(dsDetails.Tables[0].Rows[0]["AMENDMENT_COUNT"]);

                    if (dsDetails.Tables[0].Rows[0]["CREATED_REMARKS"] != DBNull.Value)
                        createdRemarks = Convert.ToString(dsDetails.Tables[0].Rows[0]["CREATED_REMARKS"]);

                    if (dsDetails.Tables[0].Rows[0]["AMENDED_REMARKS"] != DBNull.Value)
                        amendedRemarks = Convert.ToString(dsDetails.Tables[0].Rows[0]["AMENDED_REMARKS"]);


                    ViewState["Amendmentcount"] = amendmentcount;

                    if (amendmentcount > 0)
                    {
                        if (!string.IsNullOrEmpty(amendedRemarks))
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "CREATED: " + createdRemarks + ";\n" + "AMENDED: " + amendedRemarks;
                        }
                        else
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "CREATED: " + createdRemarks;
                        }
                    }
                    else
                    {
                        txtCreatedOrAmendedRemarksUS.Text = "CREATED: " + createdRemarks;
                    }


                    if (dsDetails.Tables[0].Rows[0]["INVOICE_NO_FILE_NAME"] != DBNull.Value)
                    {
                        lblInvoiceFileUS.Text = Convert.ToString(dsDetails.Tables[0].Rows[0]["INVOICE_NO_FILE_NAME"]);
                        attachment1Txt = lblInvoiceFileUS.Text;
                    }

                    if (!string.IsNullOrEmpty(lblInvoiceFileUS.Text))
                    {
                        imgBtnViewInvoiceFile.Visible = true;
                        attachment1Extn = Convert.ToString(attachment1Txt).Split('.').Last();
                        if (attachment1Extn == "jpg" || attachment1Extn == "jepg" || attachment1Extn == "bmp" || attachment1Extn == "png" || attachment1Extn == "gif" || attachment1Extn == "JPG" || attachment1Extn == "JPEG" || attachment1Extn == "BMP" || attachment1Extn == "PNG" || attachment1Extn == "GIF")
                        {

                            imgBtnViewInvoiceFile.ImageUrl = "~/Images/imgicon1.png";
                            imgBtnViewInvoiceFile.ToolTip = attachment1Txt;
                        }
                        else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                        {
                            imgBtnViewInvoiceFile.ImageUrl = "~/Images/pdficon1.png";
                            imgBtnViewInvoiceFile.ToolTip = attachment1Txt;
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

    private void GetStatusMessage(int updatingSatusID)
    {
        try
        {
            btnUpdateSatus.Visible = false;
            btnAmendment.Visible = false;

            if (updatingSatusID > 0)
            {
                if (updatingSatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.OPEN) ||
                    updatingSatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.AMENDED))
                {
                    btnUpdateSatus.Visible = true;
                    btnAmendment.Visible = true;
                }
                else if (updatingSatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.APPROVED) ||
                         updatingSatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED))
                {
                    SuccessMessage("Payment Request.: [" + txtPaymentRequestNoUS.Text + "] already approved...!!!");
                }
                else if (updatingSatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.RELEASED))
                {
                    SuccessMessage("Payment Request.: [" + txtPaymentRequestNoUS.Text + "] already released...!!!");
                }
            }
            else
            {
                if (FCStatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.OPEN) ||
                FCStatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.AMENDED))
                {
                    btnUpdateSatus.Visible = true;
                    btnAmendment.Visible = true;
                }
                else if (FCStatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.APPROVED) ||
                         FCStatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED))
                {
                    SuccessMessage("Payment Request.: [" + txtPaymentRequestNoUS.Text + "] already approved...!!!");
                }
                else if (FCStatusID == Convert.ToInt32(FCVendorPaymentStatusTypes.EnumStatus.RELEASED))
                {
                    SuccessMessage("Payment Request.: [" + txtPaymentRequestNoUS.Text + "] already released...!!!");
                }
            }



        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void ViewAttachmentFiles(int PID, string fileType, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {

                ExportPDFFFiles(PID, fileType);
            }
            else
            {
                ExceptionMessage("File Doesn't exist!");
                return;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void ExportPDFFFiles(int PID, string fileType)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objFCVendorPayment.GetFiles(PID);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                if (fileType == "VIEW_INVOICE_FILE")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["INVOICE_NO_FILE"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["INVOICE_NO_FILE_NAME"]);
                }
                else if (fileType == "VIEW_PAYMENT_FILE")
                {
                    bytes = (byte[])dsFiles.Tables[0].Rows[0]["PAYMENT_FILE"];
                    fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["PAYMENT_FILE_NAME"]);
                }

                if (bytes != null)
                {
                    string[] stringParts = fileName.Split(new char[] { '.' });
                    string strType = stringParts[1];
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.ContentType = strType;
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }

        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Timeout expired"))
            {
                //ExceptionSubitemsMessage("The process of downloading is too longer, please try again...!!!");
                return;
            }
            else
            {
                throw;
            }
        }
    }




    private void SuccessMessage(string message)
    {
        pnlUpdateStatus.Visible = true;
        lblUpdateStatusMsg.Text = message;
        lblUpdateStatusMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlUpdateStatus.Visible = true;
        lblUpdateStatusMsg.Text = message;
        lblUpdateStatusMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanel()
    {
        pnlUpdateStatus.Visible = false;
        lblUpdateStatusMsg.Text = string.Empty;
    }

    #endregion

}

public enum EnumActID
{
    Approve = 1,
    SendToAmendment = 2
}