using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class FINANCE_FC_PAYMENT_FCVendorPaymentsList : System.Web.UI.Page
{

    #region VARIABLES[=======================]


    BAL.FCVendorPayment objFCVendorPayment = new BAL.FCVendorPayment();
    DataSet dsPaymentList = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsPaymentType = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string poNo = string.Empty;
    string vendorName = string.Empty;
    string vendorCode = string.Empty;
    string requestNo = string.Empty;
    int statusId = 0;

    DataSet dsSignatories = new DataSet();
    private int _UserID = 0;

    #endregion



    private int _FCVendorPaymentPID;
    private int _FCStatusID;
    private int _AmendmentCount;

    private string _PaymentRequestNo;
    private string _JobNoTOU;
    private string _VendorCodeTOU;
    private string _VendorNameTOU;
    private string _VendorAddressTOU;
    private string _PoNoTOU;
    private string _InvoiceFileNameTOU;
    private Byte[] _InvoiceFileBytesTOU;
    private decimal _RequestedAmountToBeReleasedTOU;
    private int _PaymentTypeIDTOU;
    private string _InvoiceNoTOU;
    private string _InvoiceDateTOU;
    private string _BankNameTOU;
    private string _BankBranchTOU;
    private string _SwiftCodeTOU;
    private string _BankAccountNoTOU;
    private string _CutOffDateTOU;
    private string _PaymentFileNameTOU;
    private Byte[] _PaymentFileBytesTOU;
    private string _RemarksTOU;
    private int _CreatedByTOU;


    private string _BankAdviceUS;
    private string _DateOfPaymentReleasedUS;


    #region PROPERTIES

    //public int FCVendorPaymentPID
    //{
    //    get
    //    {
    //        _FCVendorPaymentPID = Convert.ToInt32(ViewState["PID"]);
    //        return _FCVendorPaymentPID;
    //    }

    //    set
    //    {
    //        _FCVendorPaymentPID = value;
    //    }
    //}

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

    public int AmendmentCount
    {
        get
        {
            _AmendmentCount = Convert.ToInt32(ViewState["AmendmentCount"]);
            return _AmendmentCount;
        }

        set
        {
            _AmendmentCount = value;
        }
    }


    public string PaymentRequestNo
    {
        get
        {
            _PaymentRequestNo = Convert.ToString(ViewState["PaymentRequestNo"]);
            return _PaymentRequestNo;
        }

        set
        {
            _PaymentRequestNo = value;
        }
    }

    public string JobNoTOU
    {
        get
        {
            _JobNoTOU = txtJOBNoTOU.Text;
            return _JobNoTOU;
        }

        set
        {
            _JobNoTOU = value;
        }
    }

    public string VendorCodeTOU
    {
        get
        {
            _VendorCodeTOU = txtVendorCodeTOU.Text;
            return _VendorCodeTOU;
        }

        set
        {
            _VendorCodeTOU = value;
        }
    }

    public string VendorNameTOU
    {
        get
        {
            _VendorNameTOU = txtVendorNameTOU.Text;
            return _VendorNameTOU;
        }

        set
        {
            _VendorNameTOU = value;
        }
    }

    public string VendorAddressTOU
    {
        get
        {
            _VendorAddressTOU = txtVendorAddressTOU.Text;
            return _VendorAddressTOU;
        }

        set
        {
            _VendorAddressTOU = value;
        }
    }

    public string PoNoTOU
    {
        get
        {
            _PoNoTOU = txtPONoTOU.Text;
            return _PoNoTOU;
        }

        set
        {
            _PoNoTOU = value;
        }
    }

    public string InvoiceFileNameTOU
    {
        get
        {
            if (uploadFileAttachmentInvoiceTOU.HasFile)
            {

                if (!string.IsNullOrEmpty(uploadFileAttachmentInvoiceTOU.PostedFile.FileName))
                {
                    _InvoiceFileNameTOU = uploadFileAttachmentInvoiceTOU.PostedFile.FileName;
                }
            }

            return _InvoiceFileNameTOU;
        }

        set
        {
            _InvoiceFileNameTOU = value;
        }
    }

    public Byte[] InvoiceFileBytesTOU
    {
        get
        {
            if (uploadFileAttachmentInvoiceTOU.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentInvoiceTOU.PostedFile.FileName))
                {
                    _InvoiceFileBytesTOU = GetFileBytes(uploadFileAttachmentInvoiceTOU.PostedFile.FileName, uploadFileAttachmentInvoiceTOU.PostedFile.InputStream);
                }
            }

            return _InvoiceFileBytesTOU;

        }

        set
        {
            _InvoiceFileBytesTOU = value;
        }
    }

    public decimal RequestedAmountToBeReleasedTOU
    {
        get
        {
            _RequestedAmountToBeReleasedTOU = Convert.ToDecimal(txtRequestedAmountToBeReleasedTOU.Text);
            return _RequestedAmountToBeReleasedTOU;
        }

        set
        {
            _RequestedAmountToBeReleasedTOU = value;
        }
    }

    public int PaymentTypeIDTOU
    {
        get
        {
            _PaymentTypeIDTOU = Convert.ToInt32(ddlPaymentTypeTOU.SelectedValue);
            return _PaymentTypeIDTOU;
        }

        set
        {
            _PaymentTypeIDTOU = value;
        }
    }

    public string InvoiceNoTOU
    {
        get
        {
            _InvoiceNoTOU = txtVendorInvoiceNoTOU.Text;
            return _InvoiceNoTOU;
        }

        set
        {
            _InvoiceNoTOU = value;
        }
    }

    public string InvoiceDateTOU
    {
        get
        {
            _InvoiceDateTOU = Convert.ToDateTime(txtVendorInvoiceDateTOU.Text).ToString("yyyy-MM-dd");
            return _InvoiceDateTOU;
        }

        set
        {
            _InvoiceDateTOU = value;
        }
    }

    public string BankNameTOU
    {
        get
        {
            _BankNameTOU = txtVendorBankNameTOU.Text;
            return _BankNameTOU;
        }

        set
        {
            _BankNameTOU = value;
        }
    }

    public string BankBranchTOU
    {
        get
        {
            _BankBranchTOU = txtBankVendorBranchTOU.Text;
            return _BankBranchTOU;
        }

        set
        {
            _BankBranchTOU = value;
        }
    }

    public string SwiftCodeTOU
    {
        get
        {
            _SwiftCodeTOU = txtVendorSwiftCodeTOU.Text;
            return _SwiftCodeTOU;
        }

        set
        {
            _SwiftCodeTOU = value;
        }
    }

    public string BankAccountNoTOU
    {
        get
        {
            _BankAccountNoTOU = txtBankAccountNumberTOU.Text;
            return _BankAccountNoTOU;
        }

        set
        {
            _BankAccountNoTOU = value;
        }
    }

    public string CutOffDateTOU
    {
        get
        {
            _CutOffDateTOU = Convert.ToDateTime(txtCutOffDateTOU.Text).ToString("yyyy-MM-dd");
            return _CutOffDateTOU;
        }

        set
        {
            _CutOffDateTOU = value;
        }
    }

    public string PaymentFileName
    {
        get
        {
            if (uploadFileAttachmentPaymentFileUS.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentPaymentFileUS.PostedFile.FileName))
                {
                    _PaymentFileNameTOU = uploadFileAttachmentPaymentFileUS.PostedFile.FileName;
                }
            }

            return _PaymentFileNameTOU;
        }

        set
        {
            _PaymentFileNameTOU = value;
        }
    }

    public Byte[] PaymentFileBytes
    {
        get
        {
            if (uploadFileAttachmentPaymentFileUS.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentPaymentFileUS.PostedFile.FileName))
                {
                    _PaymentFileBytesTOU = GetFileBytes(uploadFileAttachmentPaymentFileUS.PostedFile.FileName, uploadFileAttachmentPaymentFileUS.PostedFile.InputStream);
                }
            }

            return _PaymentFileBytesTOU;

        }

        set
        {
            _PaymentFileBytesTOU = value;
        }
    }

    public string RemarksTOU
    {
        get
        {
            _RemarksTOU = txtRemarksTOU.Text;
            return _RemarksTOU;
        }

        set
        {
            _RemarksTOU = value;
        }
    }

    public int CreatedByTOU
    {
        get
        {
            _CreatedByTOU = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _CreatedByTOU;
        }

        set
        {
            _CreatedByTOU = value;
        }
    }

    public string BankAdvice
    {
        get
        {
            _BankAdviceUS = txtBankAdviceUS.Text;
            return _BankAdviceUS;
        }

        set
        {
            _BankAdviceUS = value;
        }
    }

    public string DateOfPaymentReleased
    {
        get
        {
            if (!string.IsNullOrEmpty(txtDateOfPaymentReleasedUS.Text))
            {
                _DateOfPaymentReleasedUS = Convert.ToDateTime(txtDateOfPaymentReleasedUS.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                _DateOfPaymentReleasedUS = "";
            }

            return _DateOfPaymentReleasedUS;
        }

        set
        {
            _DateOfPaymentReleasedUS = value;
        }
    }


    #endregion

    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["PO_REPORT"] = null;

                hdStartDateSearch.Value = DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                BindStatus();

                dsSignatories = objFCVendorPayment.GetSignatories();

                Session["dsSignatories"] = null;

                GetSignatoriesData();

                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["paymentRequestNo"])))
                {
                    txtRequestNo.Text = Convert.ToString(Request.QueryString["paymentRequestNo"]);
                }

                GetVendorPaymentList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //lblTotalAmount.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;


        GetVendorPaymentList();

        //if (ddlSign.SelectedIndex == 5)
        //{
        //    txtAmountTwo.Enabled = true;
        //}
        //else
        //{
        //    txtAmountTwo.Text = string.Empty;
        //    txtAmountTwo.Enabled = false;
        //}
    }

    double totalAmount = 0;

    protected void gvVendorPaymentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            _UserID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");

                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
                Label lblApprovedBy = (Label)e.Row.FindControl("lblApprovedBy");
                Label lblSentToAmendmentBy = (Label)e.Row.FindControl("lblSentToAmendmentBy");
                Label lblAmendedBy = (Label)e.Row.FindControl("lblAmendedBy");
                Label lblApprovedAmendedBy = (Label)e.Row.FindControl("lblApprovedAmendedBy");
                Label lblPaymentReleasedBy = (Label)e.Row.FindControl("lblPaymentReleasedBy");

                Label lblIsCreatedMailSent = (Label)e.Row.FindControl("lblIsCreatedMailSent");
                Label lblIsApprovedMailSent = (Label)e.Row.FindControl("lblIsApprovedMailSent");
                Label lblIsAmendedMailSent = (Label)e.Row.FindControl("lblIsAmendedMailSent");
                Label lblIsAmendedApprovedMailSent = (Label)e.Row.FindControl("lblIsAmendedApprovedMailSent");
                Label lblIsSentToAmendmentMailSent = (Label)e.Row.FindControl("lblIsSentToAmendmentMailSent");
                Label lblIsPaymentReleasedMailSent = (Label)e.Row.FindControl("lblIsPaymentReleasedMailSent");
                Label lblIsCancelledMailSent = (Label)e.Row.FindControl("lblIsCancelledMailSent");

                Label lblInvoiceFileName = (Label)e.Row.FindControl("lblInvoiceFileName");
                Label lblPaymentFileName = (Label)e.Row.FindControl("lblPaymentFileName");
                ImageButton imgBtnInvoiceFileName = (ImageButton)e.Row.FindControl("imgBtnInvoiceFileName");
                ImageButton imgBtnPaymentFileName = (ImageButton)e.Row.FindControl("imgBtnPaymentFileName");

                ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                ImageButton imgBtnCancel = (ImageButton)e.Row.FindControl("imgBtnCancel");
                ImageButton imgBtnStatus = (ImageButton)e.Row.FindControl("imgBtnStatus");

                Button btnApproveRequest = (Button)e.Row.FindControl("btnApproveRequest");
                Button btnAmend = (Button)e.Row.FindControl("btnAmend");
                Button btnReleaseAmount = (Button)e.Row.FindControl("btnReleaseAmount");
                Button btnSendToAmendment = (Button)e.Row.FindControl("btnSendToAmendment");

                Button btnSendCreatedMail = (Button)e.Row.FindControl("btnSendCreatedMail");
                Button btnSendApprovedMail = (Button)e.Row.FindControl("btnSendApprovedMail");
                Button btnSendAmendedMail = (Button)e.Row.FindControl("btnSendAmendedMail");
                Button btnSendAmendmentMail = (Button)e.Row.FindControl("btnSendAmendmentMail");
                Button btnSendPaymentRelesedMail = (Button)e.Row.FindControl("btnSendPaymentRelesedMail");
                Button btnSendCancelledMail = (Button)e.Row.FindControl("btnSendCancelledMail");



                imgBtnEdit.Visible = false;
                imgBtnCancel.Visible = false;
                imgBtnStatus.Enabled = false;

                imgBtnInvoiceFileName.Visible = false;
                imgBtnPaymentFileName.Visible = false;

                btnApproveRequest.Visible = false;
                btnAmend.Visible = false;
                btnReleaseAmount.Visible = false;
                btnSendToAmendment.Visible = false;

                btnSendCreatedMail.Visible = false;
                btnSendApprovedMail.Visible = false;
                btnSendAmendedMail.Visible = false;
                lblIsAmendedApprovedMailSent.Visible = false;
                btnSendPaymentRelesedMail.Visible = false;
                btnSendAmendmentMail.Visible = false;
                btnSendCancelledMail.Visible = false;

                int statusID = Convert.ToInt32(lblStatusID.Text);

                GetSignatoriesData();

                dsSignatories = (DataSet)Session["dsSignatories"];

                if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN)
                {

                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_open.png";
                    imgBtnStatus.ToolTip = "Open";

                    if (_UserID == Convert.ToInt32(lblCreatedBy.Text))
                    {
                        imgBtnEdit.Visible = true;
                        imgBtnCancel.Visible = true;

                        if (Convert.ToInt32(lblIsCreatedMailSent.Text) == 0)
                        {
                            btnSendCreatedMail.Visible = true;
                        }
                    }

                    if (dsSignatories.Tables[0].Rows.Count > 0)
                    {
                        foreach (var item in dsSignatories.Tables[0].Select("IS_APPROVER = 1 AND EMP_RECORD_ID = " + _UserID))
                        {
                            btnApproveRequest.Visible = true;
                            btnSendToAmendment.Visible = true;
                        }
                    }
                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.CANCELLED)
                {

                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_cancelled.png";
                    imgBtnStatus.ToolTip = "Open";

                    if (_UserID == Convert.ToInt32(lblCreatedBy.Text))
                    {
                        if (Convert.ToInt32(lblIsCancelledMailSent.Text) == 0)
                        {
                            btnSendCancelledMail.Visible = true;
                        }
                    }
                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED)
                {
                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_approved.png";
                    imgBtnStatus.ToolTip = "Approved";

                    if (_UserID == Convert.ToInt32(lblApprovedBy.Text))
                    {
                        if (Convert.ToInt32(lblIsApprovedMailSent.Text) == 0)
                        {
                            btnApproveRequest.Visible = true;
                        }
                    }

                    if (dsSignatories.Tables[0].Rows.Count > 0)
                    {
                        foreach (var item in dsSignatories.Tables[0].Select("IS_RELEASER = 1 AND EMP_RECORD_ID = " + _UserID))
                        {
                            btnReleaseAmount.Visible = true;
                            btnSendToAmendment.Visible = true;
                        }
                    }
                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
                {
                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_amendment.png";
                    imgBtnStatus.ToolTip = "Amendment";

                    if (_UserID == Convert.ToInt32(lblSentToAmendmentBy.Text))
                    {
                        if (Convert.ToInt32(lblIsSentToAmendmentMailSent.Text) == 0)
                        {
                            btnSendAmendmentMail.Visible = true;
                        }
                    }


                    if (_UserID == Convert.ToInt32(lblCreatedBy.Text))
                    {
                        btnAmend.Visible = true;
                    }
                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED)
                {
                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_amended.png";
                    imgBtnStatus.ToolTip = "Amended";

                    if (_UserID == Convert.ToInt32(lblAmendedBy.Text))
                    {
                        if (Convert.ToInt32(lblIsAmendedMailSent.Text) == 0)
                        {
                            btnSendAmendedMail.Visible = true;
                        }
                    }

                    if (dsSignatories.Tables[0].Rows.Count > 0)
                    {
                        foreach (var item in dsSignatories.Tables[0].Select("IS_APPROVER = 1 AND EMP_RECORD_ID = " + _UserID))
                        {
                            btnApproveRequest.Visible = true;
                            btnSendToAmendment.Visible = true;
                        }
                    }

                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
                {
                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_approved.png";
                    imgBtnStatus.ToolTip = "Amended Approved";

                    if (_UserID == Convert.ToInt32(lblApprovedAmendedBy.Text))
                    {
                        if (Convert.ToInt32(lblIsAmendedApprovedMailSent.Text) == 0)
                        {
                            btnSendApprovedMail.Visible = true;
                        }
                    }

                    if (dsSignatories.Tables[0].Rows.Count > 0)
                    {
                        foreach (var item in dsSignatories.Tables[0].Select("IS_RELEASER = 1 AND EMP_RECORD_ID = " + _UserID))
                        {
                            btnReleaseAmount.Visible = true;
                            btnSendToAmendment.Visible = true;
                        }
                    }
                }

                else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED)
                {
                    imgBtnStatus.ImageUrl = "~/Images/FCVP/fcvp_re;eased.png";
                    imgBtnStatus.ToolTip = "Payment Released";

                    if (_UserID == Convert.ToInt32(lblPaymentReleasedBy.Text))
                    {
                        if (Convert.ToInt32(lblIsPaymentReleasedMailSent.Text) == 0)
                        {
                            btnSendPaymentRelesedMail.Visible = true;
                        }
                    }
                }


                if (!string.IsNullOrEmpty(lblInvoiceFileName.Text))
                {
                    imgBtnInvoiceFileName.Visible = true;
                    imgBtnInvoiceFileName.ToolTip = lblInvoiceFileName.Text;
                }

                if (!string.IsNullOrEmpty(lblPaymentFileName.Text))
                {
                    imgBtnPaymentFileName.Visible = true;
                    imgBtnPaymentFileName.ToolTip = lblPaymentFileName.Text;
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

    protected void gvVendorPaymentList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                int PID = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_INVOICE_FILE" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_PAYMENT_FILE" ||
                    Convert.ToString(e.CommandArgument) == "EDIT" ||
                    Convert.ToString(e.CommandArgument) == "CANCEL"
                    )
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (Convert.ToString(e.CommandArgument) == "APPROVE_REQUEST" ||
                         Convert.ToString(e.CommandArgument) == "AMEND_REQUEST" ||
                         Convert.ToString(e.CommandArgument) == "RELEASE_AMOUNT" ||
                         Convert.ToString(e.CommandArgument) == "SEND_TO_AMENDMENT" ||

                         Convert.ToString(e.CommandArgument) == "SEND_CREATED_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_AMENDED_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_APPROVED_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_AMOUNT_RELEASED_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_AMENDMENT_MAIL" ||
                         Convert.ToString(e.CommandArgument) == "SEND_CANCELLED_MAIL"
                         )
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvVendorPaymentList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblStatusID = gvVendorPaymentList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblPaymentRequesNo = gvVendorPaymentList.Rows[rowindex].FindControl("lblPaymentRequesNo") as Label;
                Label lblDateOfRequest = gvVendorPaymentList.Rows[rowindex].FindControl("lblDateOfRequest") as Label;
                Label lblJobNo = gvVendorPaymentList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblVendorCode = gvVendorPaymentList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvVendorPaymentList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblVendorAddress = gvVendorPaymentList.Rows[rowindex].FindControl("lblVendorAddress") as Label;
                Label lblPONo = gvVendorPaymentList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblAmountToBeReleased = gvVendorPaymentList.Rows[rowindex].FindControl("lblAmountToBeReleased") as Label;
                Label lblPaymentTypeID = gvVendorPaymentList.Rows[rowindex].FindControl("lblPaymentTypeID") as Label;
                Label lblPaymentType = gvVendorPaymentList.Rows[rowindex].FindControl("lblPaymentType") as Label;
                Label lblPOAmount = gvVendorPaymentList.Rows[rowindex].FindControl("lblPOAmount") as Label;
                Label lblCurrency = gvVendorPaymentList.Rows[rowindex].FindControl("lblCurrency") as Label;

                Label lblCreatedRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblCreatedRemarks") as Label;
                Label lblAmendedRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblAmendedRemarks") as Label;
                Label lblApprovedRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblApprovedRemarks") as Label;
                Label lblApprovedAmendedRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblApprovedAmendedRemarks") as Label;
                Label lblSentToAmendmentRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblSentToAmendmentRemarks") as Label;
                Label lblPaymentReleasedRemarks = gvVendorPaymentList.Rows[rowindex].FindControl("lblPaymentReleasedRemarks") as Label;

                Label lblInvoiceNo = gvVendorPaymentList.Rows[rowindex].FindControl("lblInvoiceNo") as Label;
                Label lblInvoiceDate = gvVendorPaymentList.Rows[rowindex].FindControl("lblInvoiceDate") as Label;
                Label lblCutOffDate = gvVendorPaymentList.Rows[rowindex].FindControl("lblCutOffDate") as Label;

                Label lblBankName = gvVendorPaymentList.Rows[rowindex].FindControl("lblBankName") as Label;
                Label lblBankBranch = gvVendorPaymentList.Rows[rowindex].FindControl("lblBankBranch") as Label;
                Label lblSwftCode = gvVendorPaymentList.Rows[rowindex].FindControl("lblSwftCode") as Label;
                Label lblBankAccountNo = gvVendorPaymentList.Rows[rowindex].FindControl("lblBankAccountNo") as Label;



                Label lblAmendmentCount = gvVendorPaymentList.Rows[rowindex].FindControl("lblAmendmentCount") as Label;

                Label lblInvoiceFileName = gvVendorPaymentList.Rows[rowindex].FindControl("lblInvoiceFileName") as Label;
                Label lblPaymentFileName = gvVendorPaymentList.Rows[rowindex].FindControl("lblPaymentFileName") as Label;


                PID = Convert.ToInt32(lblPID.Text);
                int statusID = Convert.ToInt32(lblStatusID.Text);

                ViewState["PID"] = PID;
                ViewState["AmendmentCount"] = lblAmendmentCount.Text;

                lblInvoiceFileUS.Text = lblInvoiceFileName.Text;

                txtBankAdviceUS.Text = string.Empty;

                pnlViewInvoiceFile.Visible = false;
                if (!string.IsNullOrEmpty(lblInvoiceFileName.Text))
                {
                    pnlViewInvoiceFile.Visible = true;
                }



                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    mpeViewInPDFPopup.Show();
                    iframeViewFCVendorPaymentInPDF.Attributes.Add("src", "FCVendorPaymenttPDF.aspx?pid=" + Convert.ToString(lblPID.Text));
                }

                else if (Convert.ToString(e.CommandArgument) == "EDIT" ||
                         Convert.ToString(e.CommandArgument) == "AMEND_REQUEST")
                {
                    pnlEditAmend.Visible = false;
                    lblEditAmendMsg.Text = string.Empty;

                    mpeEditAmend.Show();

                    txtPaymentRequestNoTOU.Text = lblPaymentRequesNo.Text;
                    txtJOBNoTOU.Text = lblJobNo.Text;
                    txtVendorCodeTOU.Text = lblVendorCode.Text;
                    txtVendorNameTOU.Text = lblVendorName.Text;
                    txtVendorAddressTOU.Text = lblVendorAddress.Text;

                    txtPONoTOU.Text = lblPONo.Text;
                    txtTotalPOAmountTOU.Text = lblPOAmount.Text;
                    txtPOAmountCurrencyTOU.Text = lblCurrency.Text;
                    txtReleasedAmountTOU.Text = lblAmountToBeReleased.Text;

                    hdCutOffDateTOU.Value = Convert.ToDateTime(lblCutOffDate.Text).ToString("dd-MMM-yyyy");
                    txtCutOffDateTOU.Text = hdCutOffDateTOU.Value;

                    txtBalanceAmountTOU.Text = Convert.ToString(Convert.ToDecimal(txtTotalPOAmountTOU.Text) - Convert.ToDecimal(txtReleasedAmountTOU.Text));
                    txtRequestedAmountToBeReleasedTOU.Text = txtBalanceAmountTOU.Text;

                    BindPaymentType();
                    ddlPaymentTypeTOU.SelectedValue = lblPaymentTypeID.Text;

                    txtVendorInvoiceNoTOU.Text = lblInvoiceNo.Text;

                    hdVendorInvoiceDateTOU.Value = Convert.ToDateTime(lblInvoiceDate.Text).ToString("dd-MMM-yyyy");
                    txtVendorInvoiceDateTOU.Text = hdVendorInvoiceDateTOU.Value;

                    txtVendorBankNameTOU.Text = lblBankName.Text;
                    txtBankVendorBranchTOU.Text = lblBankBranch.Text;
                    txtVendorSwiftCodeTOU.Text = lblSwftCode.Text;
                    txtBankAccountNumberTOU.Text = lblBankAccountNo.Text;

                    txtRemarksTOU.Text = string.Empty;

                    if (Convert.ToString(e.CommandArgument) == "EDIT")
                    {
                        ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN;
                        btnSaveTOU.Text = "Update Payment Request";
                    }
                    else if (Convert.ToString(e.CommandArgument) == "AMEND_REQUEST")
                    {
                        ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED;
                        btnSaveTOU.Text = "Amend Payment Request";
                    }
                }

                else if (Convert.ToString(e.CommandArgument) == "APPROVE_REQUEST" ||
                         Convert.ToString(e.CommandArgument) == "RELEASE_AMOUNT" ||
                         Convert.ToString(e.CommandArgument) == "SEND_TO_AMENDMENT"
                         )
                {
                    pnlUpdateStatus.Visible = false;
                    lblUpdateStatusMsg.Text = string.Empty;


                    if (Convert.ToString(e.CommandArgument) == "APPROVE_REQUEST")
                    {
                        txtApprovedOrAmendedApprovedRemarksUS.Text = string.Empty;
                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED;
                            btnUpdateSatus.Text = "Approve Amended Payment Request";
                            lblApprovedOrAmendedApprovedRemarksUS.Text = "Amended Approve Remarks";
                        }
                        else
                        {
                            ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED;
                            btnUpdateSatus.Text = "Approve Payment Request";
                            lblApprovedOrAmendedApprovedRemarksUS.Text = "Approve Remarks";
                        }
                    }

                    else if (Convert.ToString(e.CommandArgument) == "SEND_TO_AMENDMENT")
                    {

                        txtApprovedOrAmendedApprovedRemarksUS.Text = string.Empty;
                        txtPaymentReleaseRemarksUS.Text = string.Empty;

                        ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT;
                        btnUpdateSatus.Text = "Send To Amendment";
                        lblApprovedOrAmendedApprovedRemarksUS.Text = "Send to Amendment Remarks";
                        lblPaymentReleaseRemarksUS.Text = "Send to Amendment Remarks";
                    }

                    else if (Convert.ToString(e.CommandArgument) == "RELEASE_AMOUNT")
                    {
                        txtPaymentReleaseRemarksUS.Text = string.Empty;

                        ViewState["StatusID"] = (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED;
                        btnUpdateSatus.Text = "Release Payment Request";

                        lblApprovedOrAmendedApprovedRemarksUS.Text = "Approve / Amended Approve Remarks";
                        lblPaymentReleaseRemarksUS.Text = "Payment Release Remarks";
                    }

                    pnlApprovedOrAmendedApprovedRemarksUS.Visible = false;
                    pnlPaymentReleaseRemarksUS.Visible = false;

                    txtApprovedOrAmendedApprovedRemarksUS.Enabled = false;
                    txtPaymentReleaseRemarksUS.Enabled = false;

                    if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN ||
                        statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED)
                    {
                        pnlApprovedOrAmendedApprovedRemarksUS.Visible = true;
                        txtApprovedOrAmendedApprovedRemarksUS.Enabled = true;


                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "Created Remarks: " + lblCreatedRemarks.Text + ";\n"
                                                              + "Amended Remarks: " + lblAmendedRemarks.Text;
                        }
                        else
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "Created Remarks: " + lblCreatedRemarks.Text;
                        }
                    }
                    else if (statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
                        statusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
                    {
                        pnlApprovedOrAmendedApprovedRemarksUS.Visible = true;
                        pnlPaymentReleaseRemarksUS.Visible = true;
                        txtPaymentReleaseRemarksUS.Enabled = true;

                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "Created Remarks: " + lblCreatedRemarks.Text + ";\n"
                                                              + "Amended Remarks: " + lblAmendedRemarks.Text;

                            txtApprovedOrAmendedApprovedRemarksUS.Text = "Approved Remarks: " + lblApprovedRemarks.Text + ";\n"
                                                                       + "Amended Approved Remarks: " + lblApprovedAmendedRemarks.Text;
                        }
                        else
                        {
                            txtCreatedOrAmendedRemarksUS.Text = "Created Remarks: " + lblCreatedRemarks.Text;

                            txtApprovedOrAmendedApprovedRemarksUS.Text = "Approved Remarks: " + lblApprovedRemarks.Text;
                        }
                    }

                    txtPaymentRequestNoUS.Text = lblPaymentRequesNo.Text;
                    txtJOBNoUS.Text = lblJobNo.Text;
                    txtVendorCodeUS.Text = lblVendorCode.Text;
                    txtVendorNameUS.Text = lblVendorName.Text;
                    txtVendorAddressUS.Text = lblVendorAddress.Text;
                    txtPONoUS.Text = lblPONo.Text;
                    txtTotalPOAmountUS.Text = lblPOAmount.Text;
                    txtPOAmountCurrencyUS.Text = lblCurrency.Text;
                    txtReleasedAmountUS.Text = lblAmountToBeReleased.Text;
                    txtCutOffDateUS.Text = lblCutOffDate.Text;

                    txtPaymentTypeUS.Text = lblPaymentType.Text;
                    txtVendorInvoiceNoUS.Text = lblInvoiceNo.Text;
                    txtVendorInvoiceDateUS.Text = lblInvoiceDate.Text;

                    txtVendorBankNameUS.Text = lblBankName.Text;
                    txtBankVendorBranchUS.Text = lblBankBranch.Text;
                    txtVendorSwiftCodeUS.Text = lblSwftCode.Text;
                    txtBankAccountNumberUS.Text = lblBankAccountNo.Text;

                    hdDateOfPaymentReleasedUS.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtDateOfPaymentReleasedUS.Text = hdDateOfPaymentReleasedUS.Value;

                    mpeUpdateStatus.Show();
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_INVOICE_FILE")
                    ViewAttachmentFiles(PID, "VIEW_INVOICE_FILE", Convert.ToString(lblInvoiceFileName.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "VIEW_PAYMENT_FILE")
                    ViewAttachmentFiles(PID, "VIEW_PAYMENT_FILE", Convert.ToString(lblPaymentFileName.Text).Trim());

                else if (Convert.ToString(e.CommandArgument) == "CANCEL")
                {
                    txtPaymentRequestCAN.Text = lblPaymentRequesNo.Text;
                    txtVendorCodeCAN.Text = lblVendorCode.Text;
                    txtVendorNameCAN.Text = lblVendorName.Text;

                    mpeCancel.Show();
                }



                if (Convert.ToString(e.CommandArgument) == "SEND_CREATED_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_AMENDED_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_APPROVED_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_AMOUNT_RELEASED_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_AMENDMENT_MAIL" ||
                    Convert.ToString(e.CommandArgument) == "SEND_CANCELLED_MAIL")
                {
                    int statusIdSM = 0;
                    if (Convert.ToString(e.CommandArgument) == "SEND_CREATED_MAIL")
                    {
                        statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_AMENDED_MAIL")
                    {
                        statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_APPROVED_MAIL")
                    {
                        if (Convert.ToInt32(lblAmendmentCount.Text) > 0)
                        {
                            statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED;
                        }
                        else
                        {
                            statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED;
                        }
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_AMOUNT_RELEASED_MAIL")
                    {
                        statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_AMENDMENT_MAIL")
                    {
                        statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT;
                    }
                    else if (Convert.ToString(e.CommandArgument) == "SEND_CANCELLED_MAIL")
                    {
                        statusIdSM = (int)FCVendorPaymentStatusTypes.EnumStatus.CANCELLED;
                    }


                    FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();
                    int mailSentValue = objFCVendorPaymentSendMail.ProcessSendEmail(PID, statusIdSM);

                    if (mailSentValue > 0)
                    {
                        SuccessMessage("Mail sent successfully.");

                        int mailstatusVal = 0;
                        mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(PID, statusIdSM);
                    }
                    else
                    {
                        ExceptionMessage("Please try again!");
                    }

                    GetVendorPaymentList();

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

    protected void btnSaveTOU_Click(object sender, EventArgs e)
    {
        SaveVendorPayment();

    }

    protected void btnUpdateSatus_Click(object sender, EventArgs e)
    {
        UpdatePaymentRequestSatus();
    }

    protected void btnCancelRequest_Click(object sender, EventArgs e)
    {
        CancelPaymentRequest();
    }

    protected void imgBtnViewInvoiceFile_Click(object sender, EventArgs e)
    {
        _FCVendorPaymentPID = Convert.ToInt32(ViewState["PID"]);

        ViewAttachmentFiles(_FCVendorPaymentPID, "VIEW_INVOICE_FILE", lblInvoiceFileUS.Text);
    }




    private void SaveVendorPayment()
    {
        try
        {
            _FCVendorPaymentPID = Convert.ToInt32(ViewState["PID"]);

            string value = objFCVendorPayment.AddUpdateVendorPaymentRequest
            (
                  _FCVendorPaymentPID
                , JobNoTOU
                , VendorCodeTOU
                , VendorNameTOU
                , VendorAddressTOU
                , PoNoTOU
                , InvoiceFileNameTOU
                , InvoiceFileBytesTOU
                , RequestedAmountToBeReleasedTOU
                , PaymentTypeIDTOU
                , InvoiceNoTOU
                , InvoiceDateTOU
                , BankNameTOU
                , BankBranchTOU
                , SwiftCodeTOU
                , BankAccountNoTOU
                , CutOffDateTOU
                , RemarksTOU
                , CreatedByTOU
            );

            int paymentID = Convert.ToInt32(value.Split(':')[0]);
            string requestNo = Convert.ToString(value.Split(':')[1]);
            if (paymentID > 0)
            {
                FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();
                int mailSentValue = objFCVendorPaymentSendMail.ProcessSendEmail(paymentID, 0);
                ShowUpdateMessages(mailSentValue, AmendmentCount, requestNo);

                if (mailSentValue > 0)
                {
                    int mailstatusVal = 0;
                    if (AmendmentCount == 0)
                    {
                        mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(_FCVendorPaymentPID, (int)FCVendorPaymentStatusTypes.EnumStatus.OPEN);
                    }
                    else
                    {
                        mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(_FCVendorPaymentPID, (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED);
                    }
                }
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }

            GetVendorPaymentList();

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void UpdatePaymentRequestSatus()
    {
        try
        {
            string remarks = "";
            _FCVendorPaymentPID = Convert.ToInt32(ViewState["PID"]);

            if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
               FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {

                remarks = txtApprovedOrAmendedApprovedRemarksUS.Text;
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED)
            {
                remarks = txtPaymentReleaseRemarksUS.Text;
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                if (!string.IsNullOrEmpty(txtApprovedOrAmendedApprovedRemarksUS.Text))
                {
                    remarks = txtApprovedOrAmendedApprovedRemarksUS.Text;
                }
                else if (!string.IsNullOrEmpty(txtPaymentReleaseRemarksUS.Text))
                {
                    remarks = txtPaymentReleaseRemarksUS.Text;
                }
            }

            int value = objFCVendorPayment.UpdatePaymentRequestSatus
            (
                _FCVendorPaymentPID
              , FCStatusID
              , remarks
              , BankAdvice
              , DateOfPaymentReleased
              , PaymentFileName
              , PaymentFileBytes
              , CreatedByTOU
            );

            if (value > 0)
            {
                FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();
                int mailSentValue = objFCVendorPaymentSendMail.ProcessSendEmail(_FCVendorPaymentPID, 0);

                ShowUpdateStatusMessages(mailSentValue);

                if (mailSentValue > 0)
                {
                    int mailstatusVal = 0;
                    mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(_FCVendorPaymentPID, FCStatusID);
                }
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }

            GetVendorPaymentList();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void ShowUpdateMessages(int mailSentValue, int amendmentCount, string requestNo)
    {
        if (mailSentValue > 0)
        {
            if (amendmentCount == 0)
            {
                SuccessMessage("Payment updated with Request No.: '" + requestNo + "' and mail sent successfully.");
            }
            else
            {
                SuccessMessage("Payment amended with Request No.: '" + requestNo + "' and mail sent successfully.");
            }
        }
        else
        {
            if (amendmentCount == 0)
            {
                SuccessMessage("Payment updated with Request No.: '" + requestNo + "'. Please go to list to send email!");
            }
            else
            {
                SuccessMessage("Payment amended with Request No.: '" + requestNo + "'. Please go to list to send email!");
            }
        }
    }

    private void ShowCancelMessages(int mailSentValue, string requestNo)
    {
        if (mailSentValue > 0)
        {
            SuccessMessage("Payment Request No.: '" + requestNo + "' cancelled and mail sent successfully.");
        }
        else
        {
            SuccessMessage("Payment Request No.: '" + requestNo + "' cancelled. Please go to list to send email!");
        }
    }


    private void ShowUpdateStatusMessages(int mailstatusVal)
    {
        if (mailstatusVal == 0)
        {
            if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
                FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] approved successfully. Please go to list to send email!");
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] released successfully. Please go to list to send email!");
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] sent to amendment successfully. Please go to list to send email!");
            }
        }
        else
        {
            if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.APPROVED ||
                FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDED_APPROVED)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] approved and mail sent successfully.");
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.RELEASED)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] released and mail sent successfully.");
            }
            else if (FCStatusID == (int)FCVendorPaymentStatusTypes.EnumStatus.AMENDMENT)
            {
                SuccessMessage("Payment Request [" + txtPaymentRequestNoUS.Text + "] sent to amendment and mail sent successfully.");
            }
        }

    }




    private void CancelPaymentRequest()
    {
        try
        {
            string remarks = "";
            remarks = txtCancelledRemarksCAN.Text;
            _FCVendorPaymentPID = Convert.ToInt32(ViewState["PID"]);

            string value = objFCVendorPayment.CancelVendorPaymentRequest
            (
                  _FCVendorPaymentPID
                , remarks
                , CreatedByTOU
            );

            int paymentID = Convert.ToInt32(value.Split(':')[0]);
            string requestNo = Convert.ToString(value.Split(':')[1]);
            if (paymentID > 0)
            {
                FCVendorPaymentSendMail objFCVendorPaymentSendMail = new FCVendorPaymentSendMail();
                int mailSentValue = objFCVendorPaymentSendMail.ProcessSendEmail(paymentID, 0);

                ShowCancelMessages(mailSentValue, requestNo);

                if (mailSentValue > 0)
                {
                    int mailstatusVal = objFCVendorPayment.UpdateVendorPaymentMailStatus(_FCVendorPaymentPID, (int)FCVendorPaymentStatusTypes.EnumStatus.CANCELLED);
                }
            }
            else
            {
                ExceptionMessage("Please try again!");
                return;
            }

            GetVendorPaymentList();

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    #endregion


    #region METHODS[=========================]

    private void GetSignatoriesData()
    {
        if (Session["dsSignatories"] == null)
        {
            dsSignatories = objFCVendorPayment.GetSignatories();
            Session["dsSignatories"] = dsSignatories;
        }
    }

    //private void BindUnit()
    //{
    //    try
    //    {
    //        dsUnit = objCommon.GetUnit();
    //        if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCompany.DataSource = dsUnit.Tables[0];
    //            ddlCompany.DataTextField = "UNIT_NAME";
    //            ddlCompany.DataValueField = "UNIT_ID";
    //            ddlCompany.DataBind();
    //            ddlCompany.Items.Insert(0, "All");
    //            ddlCompany.SelectedIndex = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}



    private void BindPaymentType()
    {
        try
        {
            dsPaymentType = objFCVendorPayment.GetPaymentTypes();
            if (dsPaymentType.Tables.Count > 0 && dsPaymentType.Tables[0].Rows.Count > 0)
            {
                ddlPaymentTypeTOU.DataSource = dsPaymentType.Tables[0];
                ddlPaymentTypeTOU.DataTextField = "NAME";
                ddlPaymentTypeTOU.DataValueField = "PID";
                ddlPaymentTypeTOU.DataBind();
                ddlPaymentTypeTOU.Items.Insert(0, "Select");
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
            dsStatus = objFCVendorPayment.GetPaymentStatusList();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "NAME";
                ddlStatus.DataValueField = "PID";
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

    private void GetVendorPaymentList()
    {
        try
        {
            //dsDBDetails = (DataSet)Session["DB_DETAILS"];

            //if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
            //    {
            //        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
            //            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

            //        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
            //            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

            //        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
            //            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

            //        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
            //            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
            //    }
            //}


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

            if (!string.IsNullOrEmpty(txtVendorCode.Text))
                vendorCode = txtVendorCode.Text;
            else
                vendorCode = string.Empty;

            if (!string.IsNullOrEmpty(txtRequestNo.Text))
                requestNo = txtRequestNo.Text.ToUpper();
            else
                requestNo = string.Empty;

            //if (!string.IsNullOrEmpty(txtAmountOne.Text))
            //    amount1 = Convert.ToDouble(txtAmountOne.Text);
            //else
            //    amount2 = 0;

            //if (!string.IsNullOrEmpty(txtAmountTwo.Text))
            //    amount2 = Convert.ToDouble(txtAmountTwo.Text);
            //else
            //    amount2 = 0;

            if (ddlStatus.SelectedIndex > 0)
                statusId = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusId = 0;

            //if (ddlCompany.SelectedIndex > 0)
            //    unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            //else
            //    unitName = string.Empty;

            //sign = Convert.ToString(ddlSign.SelectedItem.Text);

            //if (chkExcludeCIDF.Checked)
            //    excludeCIDF = 1;
            //else
            //    excludeCIDF = 0;

            dsPaymentList = objFCVendorPayment.GetVendorPaymentList(fromDate, toDate, poNo, vendorName, vendorCode, statusId, requestNo);

            if (dsPaymentList.Tables.Count > 0 && dsPaymentList.Tables[0].Rows.Count > 0)
            {
                //Session["PO_REPORT"] = dsPaymentList;
                gvVendorPaymentList.DataSource = dsPaymentList.Tables[0];
                gvVendorPaymentList.DataBind();
            }
            else
            {
                //Session["PO_REPORT"] = null;
                gvVendorPaymentList.DataSource = null;
                gvVendorPaymentList.DataBind();
            }
            lblRecords.Text = "Records[" + dsPaymentList.Tables[0].Rows.Count + "]";
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

            string fileName = "PO_Header_From_" + txtStartDateSearch.Text + "_To_" + txtEndDateSearch.Text;
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
        try
        {
            txtJOBNoTOU.Text = string.Empty;
            txtVendorCodeTOU.Text = string.Empty;
            txtVendorNameTOU.Text = string.Empty;
            txtPONoTOU.Text = string.Empty;
            txtTotalPOAmountTOU.Text = "0.000";
            txtPOAmountCurrencyTOU.Text = string.Empty;
            txtReleasedAmountTOU.Text = "0.000";
            txtBalanceAmountTOU.Text = "0.000";
            txtRequestedAmountToBeReleasedTOU.Text = "0.000";

            ddlPaymentTypeTOU.SelectedIndex = 0;
            txtVendorBankNameTOU.Text = string.Empty;
            txtBankVendorBranchTOU.Text = string.Empty;
            txtVendorSwiftCodeTOU.Text = string.Empty;
            txtBankAccountNumberTOU.Text = string.Empty;
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


    private void UpdateSuccessMessage(string message)
    {
        pnlEditAmend.Visible = true;
        lblEditAmendMsg.Text = message;
        lblEditAmendMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void UpdateExceptionMessage(string message)
    {
        pnlEditAmend.Visible = true;
        lblEditAmendMsg.Text = message;
        lblEditAmendMsg.ForeColor = System.Drawing.Color.Red;
    }



    private void UpdateStatusSuccessMessage(string message)
    {
        pnlUpdateStatus.Visible = true;
        lblUpdateStatusMsg.Text = message;
        lblUpdateStatusMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void UpdateStatusExceptionMessage(string message)
    {
        pnlUpdateStatus.Visible = true;
        lblUpdateStatusMsg.Text = message;
        lblUpdateStatusMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
