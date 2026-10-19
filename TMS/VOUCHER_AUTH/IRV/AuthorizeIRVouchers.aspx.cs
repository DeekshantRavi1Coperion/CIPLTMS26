using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class VOUCHER_AUTH_IRV_AuthorizeIRVouchers : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsVouchersList = new DataSet();
    DataSet _dsAttachedDocs = new DataSet();
    DataSet _dsVouchersType = new DataSet();
    DataSet _dsAttachedBy = new DataSet();
    DataSet dsVoucherDetails = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsStatus = new DataSet();

    int _unitId = 0;

    int _voucherTypeId = 0;
    string _startDate = "";
    string _endDate = "";

    string _voucherNumber = "";
    int _createdById = 0;
    int _attachedById = 0;


    public string StartDate
    {
        get
        {
            if (chkSelectDates.Checked)
                _startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else _startDate = "";

            return _startDate;
        }
    }

    public string EndDate
    {
        get
        {
            if (chkSelectDates.Checked)
                _endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else _endDate = "";

            return _endDate;
        }
    }

    public string DateSignS
    {
        get
        {
            _dateSignS = Convert.ToString(ddlDateSignToS.Text);
            return _dateSignS;
        }
    }

    public string DateTypeS
    {
        get
        {
            _dateTypeS = Convert.ToString(ddlDateTypeToS.Text);
            return _dateTypeS;
        }
    }

    public string StartDateS
    {
        get
        {
            if (!string.IsNullOrEmpty(hdStartDateSearch.Value))
                _startDateS = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else _startDateS = "";

            return _startDateS;
        }
    }

    public string EndDateS
    {
        get
        {
            if (!string.IsNullOrEmpty(hdEndDateSearch.Value))
                _endDateS = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else _endDateS = "";

            return _endDateS;
        }
    }


    public int StatusIdS
    {
        get
        {
            if (ddlStatusToS.SelectedIndex > 0)
            {
                _statusIdS = Convert.ToInt32(ddlStatusToS.SelectedValue);
            }
            else
            {
                _statusIdS = -1;
            }

            return _statusIdS;
        }
    }

    public string WarehouseIdS
    {
        get
        {
            if (ddlWarehouseToS.SelectedIndex > 0)
            {
                _warehouseIdToS = Convert.ToString(ddlWarehouseToS.SelectedValue);
            }
            else
            {
                _warehouseIdToS = "";
            }

            return _warehouseIdToS;
        }
    }

    //public string TypeIdS
    //{
    //    get
    //    {
    //        if (ddlTypeToS.SelectedIndex > 0)
    //        {
    //            _typeIdToS = Convert.ToString(ddlTypeToS.SelectedValue);
    //        }
    //        else
    //        {
    //            _typeIdToS = "";
    //        }

    //        return _typeIdToS;
    //    }
    //}

    public int UnitIdS
    {
        get
        {
            if (ddlUnitToS.SelectedIndex > 0)
                _unitIdS = Convert.ToInt32(ddlUnitToS.SelectedValue);
            else _unitIdS = 0;

            return _unitIdS;
        }
    }

    public string SearchByS
    {
        get
        {
            _searchByS = Convert.ToString(ddlSearchByToS.Text);
            return _searchByS;
        }
    }

    public string SearchTextS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtSearchTextToS.Text))
            {
                var texts = txtSearchTextToS.Text.Split(',');
                string txt = "";
                foreach (var item in texts)
                {
                    string dd = item.Replace('\r', ' ').Replace('\n', ' ').Trim();
                    txt += "'" + dd + "',";
                }
                _searchTextS = txt.TrimEnd(',');
            }
            else _searchTextS = "";

            return _searchTextS;
        }
    }

    //public string VendorCodeS
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(txtVendorCodeToS.Text))
    //            _vendorCodeS = Convert.ToString(txtVendorCodeToS.Text);
    //        else _vendorCodeS = "";

    //        return _vendorCodeS;
    //    }
    //}

    //public string VendorNameS
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(txtVendorNameToS.Text))
    //            _vendorNameS = Convert.ToString(txtVendorNameToS.Text);
    //        else _vendorNameS = "";

    //        return _vendorNameS;
    //    }
    //}


    //public string AmountSearchByS
    //{
    //    get
    //    {
    //        _amountSearchByS = Convert.ToString(ddlAmountSearchByS.SelectedValue);
    //        return _amountSearchByS;
    //    }
    //}

    //public string AmountSignS
    //{
    //    get
    //    {
    //        _amountSignS = Convert.ToString(ddlAmountSign.Text);
    //        return _amountSignS;
    //    }
    //}

    //public decimal AmountOneS
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(txtAmountOneS.Text))
    //            _amountOneS = Convert.ToDecimal(txtAmountOneS.Text);
    //        else _amountOneS = 0;

    //        return _amountOneS;
    //    }
    //}

    //public decimal AmountTwoS
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(txtAmountTwoS.Text))
    //            _amountTwoS = Convert.ToDecimal(txtAmountTwoS.Text);
    //        else _amountTwoS = 0;

    //        return _amountTwoS;
    //    }
    //}






    public int CreatedById
    {
        get
        {
            _createdById = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _createdById;
        }

        set
        {
            _createdById = value;
        }
    }

    public int Pid
    {
        get
        {
            if (ViewState["PID"] != null)
                _pid = Convert.ToInt32(ViewState["PID"]);
            else _pid = 0;

            return _pid;
        }
    }

    public int ActIdToAS
    {
        get
        {
            if (ViewState["ACT_ID"] != null)
                _actId = Convert.ToInt32(ViewState["ACT_ID"]);
            else _actId = 0;

            return _actId;
        }
    }

    public string VoucherNoToAS
    {
        get
        {
            if (ViewState["VoucherNo"] != null)
                _voucherNoToAS = Convert.ToString(ViewState["VoucherNo"]);
            else _voucherNoToAS = "";

            return _voucherNoToAS;
        }
    }

    //public string TypeIdToAS
    //{
    //    get
    //    {
    //        if (ViewState["TypeId"] != null)
    //            _typeIdToAS = Convert.ToString(ViewState["TypeId"]);
    //        else _typeIdToAS = "";

    //        return _typeIdToAS;
    //    }
    //}

    //public decimal AmountToAS
    //{
    //    get
    //    {
    //        if (ViewState["Amount"] != null)
    //            _amountToAS = Convert.ToDecimal(ViewState["Amount"]);
    //        else _amountToAS = 0;

    //        return _amountToAS;
    //    }
    //}

    public decimal ReceiptAmountFCToAS
    {
        get
        {
            if (ViewState["ReceiptAmountFC"] != null)
                _receiptAmountFCToAS = Convert.ToDecimal(ViewState["ReceiptAmountFC"]);
            else _receiptAmountFCToAS = 0;

            return _receiptAmountFCToAS;
        }
    }

    public decimal PaymentAmountFCToAS
    {
        get
        {
            if (ViewState["PaymentAmountFC"] != null)
                _paymentAmountFCToAS = Convert.ToDecimal(ViewState["PaymentAmountFC"]);
            else _paymentAmountFCToAS = 0;

            return _paymentAmountFCToAS;
        }
    }

    public decimal ReceiptAmountINRToAS
    {
        get
        {
            if (ViewState["ReceiptAmountINR"] != null)
                _receiptAmountINRToAS = Convert.ToDecimal(ViewState["ReceiptAmountINR"]);
            else _receiptAmountINRToAS = 0;

            return _receiptAmountINRToAS;
        }
    }

    public decimal PaymentAmountINRToAS
    {
        get
        {
            if (ViewState["PaymentAmountINR"] != null)
                _paymentAmountINRToAS = Convert.ToDecimal(ViewState["PaymentAmountINR"]);
            else _paymentAmountINRToAS = 0;

            return _paymentAmountINRToAS;
        }
    }

    public string VoucherDateToAS
    {
        get
        {
            if (ViewState["VoucherDate"] != null)
                _voucherDateToAS = Convert.ToString(ViewState["VoucherDate"]);
            else _voucherDateToAS = "";

            return _voucherDateToAS;
        }
    }

    public int UnitIdToAS
    {
        get
        {
            if (ViewState["UnitId"] != null)
                _unitIdToAS = Convert.ToInt32(ViewState["UnitId"]);
            else _unitIdToAS = 0;

            return _unitIdToAS;
        }
    }

    public string ChallanNoToAS
    {
        get
        {
            if (ViewState["ChallanNo"] != null)
                _challanNoToAS = Convert.ToString(ViewState["ChallanNo"]);
            else _challanNoToAS = "";

            return _challanNoToAS;
        }
    }

    public string ChallanDateToAS
    {
        get
        {
            if (ViewState["ChallanDate"] != null)
                _challanDateToAS = Convert.ToString(ViewState["ChallanDate"]);
            else _challanDateToAS = "";

            return _challanDateToAS;
        }
    }

    public string OANoToAS
    {
        get
        {
            if (ViewState["PurchaseOrderNo"] != null)
                _oANoToAS = Convert.ToString(ViewState["PurchaseOrderNo"]);
            else _oANoToAS = "";

            return _oANoToAS;
        }
    }

    public string OADateToAS
    {
        get
        {
            if (ViewState["PurchaseOrderDate"] != null)
                _oADateToAS = Convert.ToString(ViewState["PurchaseOrderDate"]);
            else _oADateToAS = "";

            return _oADateToAS;
        }
    }

    public string VendorCodeToAS
    {
        get
        {
            if (ViewState["VendorCode"] != null)
                _vendorCodeToAS = Convert.ToString(ViewState["VendorCode"]);
            else _vendorCodeToAS = "";

            return _vendorCodeToAS;
        }
    }

    public string VendorNameToAS
    {
        get
        {
            if (ViewState["VendorName"] != null)
                _vendorNameToAS = Convert.ToString(ViewState["VendorName"]);
            else _vendorNameToAS = "";

            return _vendorNameToAS;
        }
    }

    public decimal NetAmountToAS
    {
        get
        {
            if (ViewState["NetAmount"] != null)
                _netAmountToAS = Convert.ToDecimal(ViewState["NetAmount"]);
            else _netAmountToAS = 0;

            return _netAmountToAS;
        }
    }

    public string CurrencyDescToAS
    {
        get
        {
            if (ViewState["CurrencyDesc"] != null)
                _currencyDescToAS = Convert.ToString(ViewState["CurrencyDesc"]);
            else _currencyDescToAS = "";

            return _currencyDescToAS;
        }
    }

    public decimal CurrencyRateToAS
    {
        get
        {
            if (ViewState["CurrencyRate"] != null)
                _currencyRateToAS = Convert.ToDecimal(ViewState["CurrencyRate"]);
            else _currencyRateToAS = 0;

            return _currencyRateToAS;
        }
    }

    public decimal INRBasicAmountToAS
    {
        get
        {
            if (ViewState["INRBasicAmount"] != null)
                _INRBasicAmountToAS = Convert.ToDecimal(ViewState["INRBasicAmount"]);
            else _INRBasicAmountToAS = 0;

            return _INRBasicAmountToAS;
        }
    }

    public decimal INROtherAmountToAS
    {
        get
        {
            if (ViewState["INROtherAmount"] != null)
                _INROtherAmountToAS = Convert.ToDecimal(ViewState["INROtherAmount"]);
            else _INROtherAmountToAS = 0;

            return _INROtherAmountToAS;
        }
    }

    public decimal FCBasicAmountToAS
    {
        get
        {
            if (ViewState["FCBasicAmount"] != null)
                _FCBasicAmountToAS = Convert.ToDecimal(ViewState["FCBasicAmount"]);
            else _FCBasicAmountToAS = 0;

            return _FCBasicAmountToAS;
        }
    }

    public decimal FCOtherAmountToAS
    {
        get
        {
            if (ViewState["FCOtherAmount"] != null)
                _FCOtherAmountToAS = Convert.ToDecimal(ViewState["FCOtherAmount"]);
            else _FCOtherAmountToAS = 0;

            return _FCOtherAmountToAS;
        }
    }

    public string ClassToAS
    {
        get
        {
            if (ViewState["Class"] != null)
                _classToAS = Convert.ToString(ViewState["Class"]);
            else _classToAS = "";

            return _classToAS;
        }
    }

    public string ReferenceVoucherNoToAS
    {
        get
        {
            if (ViewState["ReferenceVoucherNo"] != null)
                _referenceVoucherNoToAS = Convert.ToString(ViewState["ReferenceVoucherNo"]);
            else _referenceVoucherNoToAS = "";

            return _referenceVoucherNoToAS;
        }
    }

    public string ReferenceVoucherDateToAS
    {
        get
        {
            if (ViewState["ReferenceVoucherDate"] != null)
                _referenceVoucherDateToAS = Convert.ToString(ViewState["ReferenceVoucherDate"]);
            else _referenceVoucherDateToAS = "";

            return _referenceVoucherDateToAS;
        }
    }


    public string VoucherCreatedByToAS
    {
        get
        {
            if (ViewState["VoucherCreatedBy"] != null)
                _voucherCreatedByToAS = Convert.ToString(ViewState["VoucherCreatedBy"]);
            else _voucherCreatedByToAS = "";

            return _voucherCreatedByToAS;
        }
    }

    public int VoucherTypeIdToAS
    {
        get
        {
            if (ViewState["VoucherTypeId"] != null)
                _VoucherTypeIdToAS = Convert.ToInt32(ViewState["VoucherTypeId"]);
            else _VoucherTypeIdToAS = 0;

            return _VoucherTypeIdToAS;
        }
    }


    public string FileNameToAS1
    {
        get
        {
            if (uploadFileAttachmentToAS1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS1.PostedFile.FileName))
                {
                    _fileNameToAS1 = uploadFileAttachmentToAS1.PostedFile.FileName;
                }
                else _fileNameToAS1 = string.Empty;
            }
            else _fileNameToAS1 = string.Empty;

            return _fileNameToAS1;
        }
    }

    public string FileNameToAS2
    {
        get
        {
            if (uploadFileAttachmentToAS2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS2.PostedFile.FileName))
                {
                    _fileNameToAS2 = uploadFileAttachmentToAS2.PostedFile.FileName;
                }
                else _fileNameToAS2 = string.Empty;
            }
            else _fileNameToAS2 = string.Empty;

            return _fileNameToAS2;
        }
    }

    public string FileNameToAS3
    {
        get
        {
            if (uploadFileAttachmentToAS3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS3.PostedFile.FileName))
                {
                    _fileNameToAS3 = uploadFileAttachmentToAS3.PostedFile.FileName;
                }
                else _fileNameToAS3 = string.Empty;
            }
            else _fileNameToAS3 = string.Empty;

            return _fileNameToAS3;
        }
    }

    public string FileNameToAS4
    {
        get
        {
            if (uploadFileAttachmentToAS4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS4.PostedFile.FileName))
                {
                    _fileNameToAS4 = uploadFileAttachmentToAS4.PostedFile.FileName;
                }
                else _fileNameToAS4 = string.Empty;
            }
            else _fileNameToAS4 = string.Empty;

            return _fileNameToAS4;
        }
    }

    public string FileNameToAS5
    {
        get
        {
            if (uploadFileAttachmentToAS5.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS5.PostedFile.FileName))
                {
                    _fileNameToAS5 = uploadFileAttachmentToAS5.PostedFile.FileName;
                }
                else _fileNameToAS5 = string.Empty;
            }
            else _fileNameToAS5 = string.Empty;

            return _fileNameToAS5;
        }
    }


    public Byte[] FileBytesToAS1
    {
        get
        {

            if (uploadFileAttachmentToAS1.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS1.PostedFile.FileName))
                {
                    _FileBytesToAS1 = GetFileBytes(uploadFileAttachmentToAS1.PostedFile.FileName, uploadFileAttachmentToAS1.PostedFile.InputStream);
                }
                else _FileBytesToAS1 = null;
            }
            else _FileBytesToAS1 = null;

            return _FileBytesToAS1;
        }
    }

    public Byte[] FileBytesToAS2
    {
        get
        {

            if (uploadFileAttachmentToAS2.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS2.PostedFile.FileName))
                {
                    _FileBytesToAS2 = GetFileBytes(uploadFileAttachmentToAS2.PostedFile.FileName, uploadFileAttachmentToAS2.PostedFile.InputStream);
                }
                else _FileBytesToAS2 = null;
            }
            else _FileBytesToAS2 = null;

            return _FileBytesToAS2;
        }
    }

    public Byte[] FileBytesToAS3
    {
        get
        {

            if (uploadFileAttachmentToAS3.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS3.PostedFile.FileName))
                {
                    _FileBytesToAS3 = GetFileBytes(uploadFileAttachmentToAS3.PostedFile.FileName, uploadFileAttachmentToAS3.PostedFile.InputStream);
                }
                else _FileBytesToAS3 = null;
            }
            else _FileBytesToAS3 = null;

            return _FileBytesToAS3;
        }
    }

    public Byte[] FileBytesToAS4
    {
        get
        {

            if (uploadFileAttachmentToAS4.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS4.PostedFile.FileName))
                {
                    _FileBytesToAS4 = GetFileBytes(uploadFileAttachmentToAS4.PostedFile.FileName, uploadFileAttachmentToAS4.PostedFile.InputStream);
                }
                else _FileBytesToAS4 = null;
            }
            else _FileBytesToAS4 = null;

            return _FileBytesToAS4;
        }
    }

    public Byte[] FileBytesToAS5
    {
        get
        {

            if (uploadFileAttachmentToAS5.HasFile)
            {
                if (!string.IsNullOrEmpty(uploadFileAttachmentToAS5.PostedFile.FileName))
                {
                    _FileBytesToAS5 = GetFileBytes(uploadFileAttachmentToAS5.PostedFile.FileName, uploadFileAttachmentToAS5.PostedFile.InputStream);
                }
                else _FileBytesToAS5 = null;
            }
            else _FileBytesToAS5 = null;

            return _FileBytesToAS5;
        }
    }


    public string RemarksToAS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtRemarksToAS.Text))
                _remarksToAS = txtRemarksToAS.Text;
            else _remarksToAS = "";

            return _remarksToAS;
        }
    }





    int _pid;
    private int _VoucherTypeIdToAS;
    private string _voucherCreatedByToAS;
    private string _classToAS;

    private decimal _receiptAmountFCToAS;
    private decimal _paymentAmountFCToAS;
    private decimal _receiptAmountINRToAS;
    private decimal _paymentAmountINRToAS;

    private string _voucherDateToAS;
    private string _voucherNoToAS;
    private string _remarksToAS;

    private string _fileNameToAS1;
    private string _fileNameToAS2;
    private string _fileNameToAS3;
    private string _fileNameToAS5;
    private string _fileNameToAS4;
    private byte[] _FileBytesToAS1;
    private byte[] _FileBytesToAS2;
    private byte[] _FileBytesToAS3;
    private byte[] _FileBytesToAS4;
    private byte[] _FileBytesToAS5;

    private int _unitIdToAS;
    private string _challanNoToAS;
    private string _challanDateToAS;
    private string _oANoToAS;
    private string _oADateToAS;
    private string _vendorCodeToAS;
    private string _vendorNameToAS;
    private decimal _netAmountToAS;
    private string _dateSignS;
    private string _dateTypeS;
    private string _startDateS;
    private string _endDateS;
    private int _unitIdS;
    private string _searchByS;
    private string _searchTextS;
    private string _amountSignS;
    private decimal _amountOneS;
    private decimal _amountTwoS;
    private int _statusIdS;
    private int _actId;
    private string _currencyDescToAS;
    private decimal _currencyRateToAS;

    private decimal _INRBasicAmountToAS;
    private decimal _INROtherAmountToAS;
    private decimal _FCBasicAmountToAS;
    private decimal _FCOtherAmountToAS;
    private string _warehouseIdToS;
    private string _amountSearchByS;
    private string _factorToAS;
    private string _referenceVoucherNoToAS;
    private string _referenceVoucherDateToAS;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //Session["MRNReport"] = null;
                //Session["PO_Details"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, 1, 1);//DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var currentDate = new DateTime(now.Year, now.Month, 1);
                var endDate = currentDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindStatusList();
                BindUnits();
                //BindVouchersType();
                //BindAttachedBy();
                //ddlDateType.SelectedIndex = 1;

                GetVouchersList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetVouchersList();
    }

    protected void gvVouchersList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string attachment1Extn = string.Empty;
            Label lblPID = (Label)e.Row.FindControl("lblPID");
            Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
            Label lblDocumentsCount = (Label)e.Row.FindControl("lblDocumentsCount");
            ImageButton imgBtnAddNewDocument = (ImageButton)e.Row.FindControl("imgBtnAddNewDocument");
            ImageButton btnViewPDFCopy = (ImageButton)e.Row.FindControl("btnViewPDFCopy");

            HtmlTable tblViewDetail = (HtmlTable)e.Row.FindControl("tblViewDetail");
            ImageButton imgBtnDownloadAllDocuments = (ImageButton)e.Row.FindControl("imgBtnDownloadAllDocuments");
            ImageButton imgBtnAuthorize = (ImageButton)e.Row.FindControl("imgBtnAuthorize");


            tblViewDetail.Visible = false;
            btnViewPDFCopy.Visible = false;
            imgBtnAddNewDocument.Visible = false;

            //if (Convert.ToInt32(lblStatusID.Text) == 0)
            //{
            //    imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-pending.png";
            //    imgBtnAuthorize.ToolTip = "Unauthorized voucher";
            //    //imgBtnAuthorize.Enabled = false;
            //}
            //else if (Convert.ToInt32(lblStatusID.Text) == 1)
            //{
            //    if (Convert.ToInt32(lblPID.Text) > 0)
            //    {
            //        tblViewDetail.Visible = true;
            //        btnViewPDFCopy.Visible = true;
            //        imgBtnAddNewDocument.Visible = true;
            //        imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-authorized.png";
            //        imgBtnAuthorize.ToolTip = "Voucher authorized";
            //        imgBtnAuthorize.Enabled = false;
            //    }
            //    else
            //    {
            //        imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-authorized.png";
            //        imgBtnAuthorize.ToolTip = "Authorize voucher";
            //        imgBtnAuthorize.Enabled = true;
            //    }
            //}
            //else if (Convert.ToInt32(lblStatusID.Text) == 2)
            //{
            //    tblViewDetail.Visible = true;
            //    btnViewPDFCopy.Visible = true;
            //    imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-approved.png";
            //    imgBtnAuthorize.ToolTip = "Voucher approved";
            //    imgBtnAuthorize.Enabled = false;
            //}

            if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.OPEN)
            {
                imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-pending.png";
                imgBtnAuthorize.ToolTip = "Unauthorized voucher";
                //imgBtnAuthorize.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.AUTHORIZED)
            {
                if (Convert.ToInt32(lblPID.Text) > 0)
                {
                    tblViewDetail.Visible = true;
                    btnViewPDFCopy.Visible = true;
                    imgBtnAddNewDocument.Visible = true;
                    imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-authorized.png";
                    imgBtnAuthorize.ToolTip = "Voucher authorized";
                    imgBtnAuthorize.Enabled = false;
                }
                else
                {
                    imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-authorized.png";
                    imgBtnAuthorize.ToolTip = "Authorize voucher";
                    imgBtnAuthorize.Enabled = true;
                }
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.APPROVED)
            {
                tblViewDetail.Visible = true;
                btnViewPDFCopy.Visible = true;
                imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-approved.png";
                imgBtnAuthorize.ToolTip = "Voucher approved";
                imgBtnAuthorize.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZATION)
            {
                imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-reauthorization.png";
                imgBtnAuthorize.ToolTip = "Reauthorization voucher";
                //imgBtnAuthorize.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZED)
            {
                tblViewDetail.Visible = true;
                btnViewPDFCopy.Visible = true;
                imgBtnAddNewDocument.Visible = true;
                imgBtnAuthorize.ImageUrl = "~/Images/VC/vc-reauthorized.png";
                imgBtnAuthorize.ToolTip = "Voucher reauthorized";
                imgBtnAuthorize.Enabled = false;
            }

            Label lblFolderDocumentsCount = (Label)e.Row.FindControl("lblFolderDocumentsCount");
            ImageButton btnViewFolderDocuments = (ImageButton)e.Row.FindControl("btnViewFolderDocuments");
            
            CheckBox chkSelectVoucher = (CheckBox)e.Row.FindControl("chkSelectVoucher");
            CheckBox chkSelectVoucherWithoutDOCS = (CheckBox)e.Row.FindControl("chkSelectVoucherWithoutDOCS");

            btnViewFolderDocuments.Visible = false;
            chkSelectVoucher.Visible = false;
            chkSelectVoucherWithoutDOCS.Visible = false;

            if (Convert.ToInt32(lblPID.Text) == 0)
            {
                chkSelectVoucherWithoutDOCS.Visible = true;

                if (Convert.ToInt32(lblFolderDocumentsCount.Text) > 0)
                {
                    chkSelectVoucher.Visible = true;
                    btnViewFolderDocuments.Visible = true;
                }
            }


            imgBtnDownloadAllDocuments.Visible = false;
            if (Convert.ToInt32(lblDocumentsCount.Text) > 0)
            {
                imgBtnDownloadAllDocuments.Visible = true;
            }


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvVouchersList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_VOUCHER_PDF" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT" ||
                    Convert.ToString(e.CommandArgument) == "AUTHORIZE" ||
                    Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS" ||
                    Convert.ToString(e.CommandArgument) == "ViewFolderDocuments")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvVouchersList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblVoucherNo = gvVouchersList.Rows[rowindex].FindControl("lblVoucherNo") as Label;
                Label lblVoucherDate = gvVouchersList.Rows[rowindex].FindControl("lblVoucherDate") as Label;
                Label lblClass = gvVouchersList.Rows[rowindex].FindControl("lblClass") as Label;

                Label lblReferenceVoucherNo = gvVouchersList.Rows[rowindex].FindControl("lblReferenceVoucherNo") as Label;
                Label lblReferenceVoucherDate = gvVouchersList.Rows[rowindex].FindControl("lblReferenceVoucherDate") as Label;

                Label lblCreatedBy = gvVouchersList.Rows[rowindex].FindControl("lblCreatedBy") as Label;
                Label lblUnitId = gvVouchersList.Rows[rowindex].FindControl("lblUnitId") as Label;
                Label lblUnitName = gvVouchersList.Rows[rowindex].FindControl("lblUnitName") as Label;

                ViewState["PID"] = lblPID.Text;
                ViewState["VoucherNo"] = lblVoucherNo.Text;
                ViewState["VoucherDate"] = lblVoucherDate.Text;
                ViewState["ReferenceVoucherNo"] = lblReferenceVoucherNo.Text;
                ViewState["ReferenceVoucherDate"] = lblReferenceVoucherDate.Text;
                ViewState["UnitId"] = lblUnitId.Text;
                ViewState["VoucherCreatedBy"] = lblCreatedBy.Text;
                ViewState["Class"] = lblClass.Text;

                btnAuthorize.Visible = false;
                pnlRemarksToAS.Visible = false;
                pnlAttachmentToAS.Visible = false;
                divAttachedFiles.Visible = false;

                txtRemarksToAS.Text = string.Empty;

                btnAuthorize.Text = "Authorize Voucher";

                ////pnlViewAuthorizeVoucherPopup.Height = 900;
                //dvViewAuthorizeVoucherPopup.Style["height"] = "800px";

                ViewState["ACT_ID"] = 0;

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                {
                    lblAuthorizeVoucherDetailsLegend.Text = "View Voucher Details";
                    divAttachedFiles.Visible = true;

                    if (Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                    {
                        ViewState["ACT_ID"] = (int)VoucherStatusTypes.EnumVoucherActions.AddDoc;
                        btnAuthorize.Visible = true;
                        pnlAttachmentToAS.Visible = true;
                        lblAuthorizeVoucherDetailsLegend.Text = "Add New Documents";
                        btnAuthorize.Text = "Save Attachments";
                        ////pnlViewAuthorizeVoucherPopup.Height = 830;
                        //dvViewAuthorizeVoucherPopup.Style["height"] = "730px";
                    }


                    lblVoucherNoToAS.Text = lblVoucherNo.Text;
                    txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    txtUnitToAS.Text = lblUnitName.Text;
                    txtCreatedByToAS.Text = lblCreatedBy.Text;

                    GetVouchersDetails(lblVoucherNo.Text, Convert.ToInt32(lblUnitId.Text));

                    BindAttachedDocs(Convert.ToInt32(lblPID.Text));

                    mpeAuthorizeVoucher.Show();

                }
                else if (Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    //ExportZip(Convert.ToInt32(lblPID.Text), lblVoucherNo.Text);
                    ExportZip(Convert.ToInt32(lblPID.Text)
                            , lblVoucherNo.Text.Trim().ToUpper()
                            , (int)VoucherStatusTypes.EnumVoucherTypes.IRV
                            , "");
                }
                else if (Convert.ToString(e.CommandArgument) == "AUTHORIZE")
                {
                    //dvViewAuthorizeVoucherPopup.Style["height"] = "600px";

                    pnlRemarksToAS.Visible = true;
                    pnlAttachmentToAS.Visible = true;
                    btnAuthorize.Visible = true;
                    divAttachedFiles.Visible = false;
                    lblAuthorizeVoucherDetailsLegend.Text = "Authorize Voucher";

                    lblVoucherNoToAS.Text = lblVoucherNo.Text;
                    txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    txtUnitToAS.Text = lblUnitName.Text;
                    txtCreatedByToAS.Text = lblCreatedBy.Text;

                    GetVouchersDetails(lblVoucherNo.Text, Convert.ToInt32(lblUnitId.Text));

                    BindAttachedDocs(Convert.ToInt32(lblPID.Text));

                    mpeAuthorizeVoucher.Show();
                    ////pnlViewAuthorizeVoucherPopup.Height = 700;
                }
                else if (Convert.ToString(e.CommandArgument) == "VIEW_VOUCHER_PDF")
                {
                    mpeViewVoucherInPDF.Show();
                    iframeVoucherInPDF.Attributes.Add("src", "VouchersPDF.aspx?pid=" + Convert.ToString(lblPID.Text));
                }
                else if (Convert.ToString(e.CommandArgument) == "ViewFolderDocuments")
                {
                    BindFolderAttachedDocs(lblVoucherNo.Text);
                    mpeFolderDocsVoucher.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }


    protected void gvAttachedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblIsDefault = e.Row.FindControl("lblIsDefault") as Label;
            //CheckBox chkIsDefault = e.Row.FindControl("chkIsDefault") as CheckBox;

            //chkIsDefault.Checked = false;
            //if (Convert.ToInt32(lblIsDefault.Text) > 0)
            //{
            //    chkIsDefault.Checked = true;
            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Transparent;
            //    }
            //}


            Label lblAttachedFileName = (Label)e.Row.FindControl("lblAttachedFileName");
            ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");

            imgBtnAttachment1.Visible = false;

            if (!string.IsNullOrEmpty(lblAttachedFileName.Text))
            {
                imgBtnAttachment1.Visible = true;
                imgBtnAttachment1.ToolTip = lblAttachedFileName.Text;

                string attachment1Extn = Convert.ToString(lblAttachedFileName.Text).Split('.').Last();
                if (attachment1Extn == "jpg" ||
                    attachment1Extn == "jepg" ||
                    attachment1Extn == "bmp" ||
                    attachment1Extn == "png" ||
                    attachment1Extn == "gif" ||
                    attachment1Extn == "JPG" ||
                    attachment1Extn == "JPEG" ||
                    attachment1Extn == "BMP" ||
                    attachment1Extn == "PNG" ||
                    attachment1Extn == "GIF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/imgicon1.png";
                    imgBtnAttachment1.ToolTip = lblAttachedFileName.Text;
                }
                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    imgBtnAttachment1.ToolTip = lblAttachedFileName.Text;
                }
                else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
                    imgBtnAttachment1.ToolTip = lblAttachedFileName.Text;
                }
                else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
                    imgBtnAttachment1.ToolTip = lblAttachedFileName.Text;
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvAttachedFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "REMOVE" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvAttachedFiles.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblAttachedFileName = gvAttachedFiles.Rows[rowindex].FindControl("lblAttachedFileName") as Label;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedFileName.Text).Trim(), (int)VoucherStatusTypes.EnumVoucherTypes.IRV);
                    mpeAuthorizeVoucher.Show();
                }


                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    //RemovePoDocuments(Convert.ToInt32(lblPID.Text));
                    //GetVouchersList();

                    int val = RemovePoDocuments(Convert.ToInt32(lblPID.Text));
                    if (val > 0)
                    {
                        GetVouchersList();
                        BindAttachedDocs(Pid);

                        mpeAuthorizeVoucher.Show();
                    }
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    protected void gvVoucherDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }


    protected void gvFolderDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvFolderDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewFolderDocument")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSlNoFD = gvFolderDocuments.Rows[rowindex].FindControl("lblSlNoFD") as Label;
                Label lblVoucherNoFD = gvFolderDocuments.Rows[rowindex].FindControl("lblVoucherNoFD") as Label;
                Label lblFileNameFD = gvFolderDocuments.Rows[rowindex].FindControl("lblFileNameFD") as Label;

                if (Convert.ToString(e.CommandArgument) == "ViewFolderDocument")
                {
                    ViewFolderAttachedFiles(Convert.ToInt32(lblSlNoFD.Text), Convert.ToString(lblFileNameFD.Text));
                    mpeFolderDocsVoucher.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    //protected void btnAuthorize_Click(object sender, EventArgs e)
    //{
    //    Authorize();
    //}

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ViewState["StatusID"]) == (int)VoucherStatusTypes.EnumStatus.OPEN)
        {
            Authorize();
        }
        else
        {
            ReAuthorize(Pid, (int)VoucherStatusTypes.EnumVoucherTypes.IRV);
        }
    }

    private void ReAuthorize(int Pid, int voucherTypeId)
    {
        int val = objVouchersAuthorization.ReauthorizeVoucher
            (
                    Pid
                , voucherTypeId
                , RemarksToAS
                , FileNameToAS1
                , FileBytesToAS1

                , FileNameToAS2
                , FileBytesToAS2

                , FileNameToAS3
                , FileBytesToAS3

                , FileNameToAS4
                , FileBytesToAS4

                , FileNameToAS5
                , FileBytesToAS5
                , CreatedById
            );

        if (val > 0)
        {
            SuccessMessage("Voucher Number: " + VoucherNoToAS + " reauthorized successfully!");
            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
            return;
        }
    }

    protected void btnBulkAuthorize_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {
                CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucher");
                if (chkSelectVoucher.Checked)
                {
                    count++;
                    if (count > 0)
                    {
                        break;
                    }
                }
            }
        }

        if (count > 0)
        {
            BulkAuthorize();
        }
        else
        {
            ExceptionMessage("Please select at-least 1 voucher!");
            return;
        }
    }

    protected void btnBulkAuthorizeWithoutDOCS_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {
                CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucherWithoutDOCS");
                if (chkSelectVoucher.Checked)
                {
                    count++;
                    if (count > 0)
                    {
                        break;
                    }
                }
            }
        }

        if (count > 0)
        {
            BulkAuthorizeWithoutDOCS();
        }
        else
        {
            ExceptionMessage("Please select at-least 1 voucher!");
            return;
        }
    }


    protected void chkSelectDeselectVouchers_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectDeselectVouchers.Checked)
        {
            if (gvVouchersList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvVouchersList.Rows)
                {
                    CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucher");
                    if (chkSelectVoucher.Visible == true)
                    {
                        chkSelectVoucher.Checked = true;
                    }
                }
            }
        }
        else
        {
            if (gvVouchersList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvVouchersList.Rows)
                {
                    CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucher");

                    chkSelectVoucher.Checked = false;
                }
            }
        }

    }


    protected void chkSelectDeselectVouchersWithoutDOCS_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectDeselectVouchersWithoutDOCS.Checked)
        {
            if (gvVouchersList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvVouchersList.Rows)
                {
                    CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucherWithoutDOCS");
                    if (chkSelectVoucher.Visible == true)
                    {
                        chkSelectVoucher.Checked = true;
                    }
                }
            }
        }
        else
        {
            if (gvVouchersList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvVouchersList.Rows)
                {
                    CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucherWithoutDOCS");

                    chkSelectVoucher.Checked = false;
                }
            }
        }

    }


    protected void chkLoadFolderDocuments_CheckedChanged(object sender, EventArgs e)
    {
        txtFolderPathSS.Enabled = false;
        if (chkLoadFolderDocuments.Checked)
        {
            GetFolderPath();
            txtFolderPathSS.Enabled = true;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void GetFolderPath()
    {
        txtFolderPathSS.Text = string.Empty;

        DataTable dt = CollectFiles.GetFilePath(Convert.ToInt32(Session["EMP_RECORD_ID"]), (int)VoucherStatusTypes.EnumVoucherTypes.IRV);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtFolderPathSS.Text = Convert.ToString(dt.Rows[0]["DIRECTORY_PATH"]);
        }
    }

    private void BindStatusList()
    {
        try
        {
            _dsStatus = objVouchersAuthorization.GetVouchersStatusList(1);
            if (_dsStatus.Tables.Count > 0 && _dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatusToS.DataSource = _dsStatus.Tables[0];
                ddlStatusToS.DataTextField = "NAME";
                ddlStatusToS.DataValueField = "PID";
                ddlStatusToS.DataBind();
                ddlStatusToS.Items.Insert(0, "All");
                ddlStatusToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnits()
    {
        try
        {
            _dsUnit = objVouchersAuthorization.GetUnits();
            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnitToS.DataSource = _dsUnit.Tables[0];
                ddlUnitToS.DataTextField = "UNIT_NAME";
                ddlUnitToS.DataValueField = "UNIT_ID";
                ddlUnitToS.DataBind();
                ddlUnitToS.Items.Insert(0, "All");
                ddlUnitToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetVouchersList()
    {
        try
        {
            dsVouchersList = objVouchersAuthorization.GetIRVouchersListToAutorize
                (
                      DateSignS
                    , DateTypeS
                    , StartDateS
                    , EndDateS
                    , UnitIdS
                    , StatusIdS
                    , WarehouseIdS
                    , SearchByS
                    , SearchTextS
                    , CreatedById
                );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
                if (chkLoadFolderDocuments.Checked)
                {
                    string filePath = string.Empty;
                    filePath = txtFolderPathSS.Text;

                    DataView view = new DataView(dsVouchersList.Tables[0]);
                    view.RowFilter = "PID = 0";
                    DataTable dtFilteredVoucherNo = view.ToTable(false);

                    List<DataTable> dtfilesList = CollectFiles.GetLocalFilesList(CreatedById, (int)VoucherStatusTypes.EnumVoucherTypes.SOV
                        , dtFilteredVoucherNo, filePath);

                    if (dtfilesList.Count > 0)
                    {
                        DataTable dtfiles = dtfilesList[0];
                        DataTable dtVoucherCounts = dtfilesList[1];

                        if (dtfiles.Rows.Count > 0)
                        {
                            Session["dtfiles"] = dtfiles;

                            foreach (DataRow drF in dtVoucherCounts.Rows)
                            {
                                string vn = Convert.ToString(drF["VOUCHER_NO"]);

                                foreach (DataRow drV in dsVouchersList.Tables[0].Select("VOUCHER_NO = '" + vn + "'"))
                                {
                                    drV["FOLDER_DOCUMENTS_COUNT"] = drF["COUNT"];
                                }
                            }
                        }
                        else
                        {
                            Session["dtfiles"] = null;
                        }
                    }
                }

                gvVouchersList.DataSource = dsVouchersList.Tables[0];
                gvVouchersList.DataBind();
            }
            else
            {
                gvVouchersList.DataSource = null;
                gvVouchersList.DataBind();
            }

            lblRecords.Text = "Records[" + dsVouchersList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindFolderAttachedDocs(string voucherNo)
    {
        try
        {
            DataTable dtFiles = Session["dtfiles"] as DataTable;

            // Defensive check
            if (dtFiles == null || dtFiles.Rows.Count == 0)
            {
                gvFolderDocuments.DataSource = null;
                gvFolderDocuments.DataBind();
                lblShowFolderDocumentsCount.Text = "0";
                return;
            }

            // Filter matching rows
            DataRow[] matchedRows = dtFiles.Select("VOUCHER_NO='" + voucherNo + "'");

            // Clone schema and import rows
            DataTable dtVoucherFiles = dtFiles.Clone();
            foreach (DataRow row in matchedRows)
            {
                dtVoucherFiles.ImportRow(row);
            }

            // Bind to GridView
            gvFolderDocuments.DataSource = dtVoucherFiles;
            gvFolderDocuments.DataBind();

            lblShowFolderDocumentsCount.Text = dtVoucherFiles.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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

    private void BindAttachedDocs(int PId)
    {
        try
        {
            _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(PId, (int)VoucherStatusTypes.EnumVoucherTypes.IRV, "");

            if (_dsAttachedDocs.Tables.Count > 0)
            {
                if (_dsAttachedDocs.Tables[0].Rows.Count > 0)
                {
                    gvAttachedFiles.DataSource = _dsAttachedDocs.Tables[0];
                    gvAttachedFiles.DataBind();
                }
                else
                {
                    gvAttachedFiles.DataSource = null;
                    gvAttachedFiles.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetVouchersDetails(string voucherNo, int unitID)
    {
        try
        {
            dsVoucherDetails = objVouchersAuthorization.GetIRVoucherDetails(voucherNo, unitID);
            if (dsVoucherDetails.Tables.Count > 0 && dsVoucherDetails.Tables[0].Rows.Count > 0)
            {
                gvVoucherDetails.DataSource = dsVoucherDetails.Tables[0];
                gvVoucherDetails.DataBind();
            }
            else
            {
                gvVoucherDetails.DataSource = null;
                gvVoucherDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewAttachedFiles(int docID, string fileName, int fileType)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" ||
                    extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewAttachedImageFile.ashx?docID=" + docID;// + "&fileType=" + fileType;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?docID=" + docID + "&fileType=" + fileType);
                    this.ModalPopupExtender3.Show();
                }
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

    private void ViewFolderAttachedFiles(int docID, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                extn = fileName.Split('.').Last();
                if (extn == "jpg" || extn == "jpeg" || extn == "bmp" || extn == "png" || extn == "gif" ||
                    extn == "JPG" || extn == "JPEG" || extn == "BMP" || extn == "PNG" || extn == "GIF")
                {
                    imgFile.ImageUrl = "ViewAttachedFolderPDFFile.ashx?docID=" + docID;
                    ModalPopupExtender2.Show();
                }
                else if (extn == "pdf" || extn == "PDF")
                {
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedFolderPDFFile.aspx?docID=" + docID);
                    //mpeFolderDocsVoucher.Show();
                    ModalPopupExtender3.Show();
                }
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



    private int RemovePoDocuments(int Pid)
    {
        int val = objVouchersAuthorization.RemoveVoucherDocuments(Pid, CreatedById);

        return val;

        //if (val > 0)
        //{
        //    BindAttachedDocs(Pid);
        //    GetVouchersList();
        //}
    }

    //protected void ExportZip(int Pid, string voucherNo)
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        byte[] pdfCopy = GeneratePDF(Pid, voucherNo);
    //        string pdfCopyName = voucherNo.Replace("/", "_") + "_COPY.pdf";

    //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;

    //        DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCFiles(Pid, (int)VoucherStatusTypes.EnumVoucherTypes.IRV);

    //        int count = 0;
    //        foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
    //        {
    //            count++;

    //            string name = count + Convert.ToString(row["FILE_NAME"]);
    //            byte[] bytes = (byte[])row["FILE_BYTES"];
    //            zip.AddEntry(name, bytes);
    //        }

    //        if (pdfCopy != null)
    //        {
    //            zip.AddEntry(pdfCopyName, pdfCopy);
    //        }

    //        Response.Clear();
    //        Response.BufferOutput = false;
    //        string zipName = String.Format(voucherNo + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
    //        Response.ContentType = "application/zip";
    //        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
    //        zip.Save(Response.OutputStream);
    //        Response.End();
    //    }
    //}

    //protected void ExportZip(int Pid, string voucherNo, int voucherTypeId, string challanNo)
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        byte[] pdfCopy = GeneratePDF(Pid, voucherNo);
    //        string pdfCopyName = voucherNo.Replace("/", "_") + "_COPY.pdf";

    //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;

    //        //DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCFiles(Pid, (int)VoucherStatusTypes.EnumVoucherTypes.PV);

    //        DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCS(Pid, voucherTypeId, challanNo);

    //        int count = 0;

    //        if (dsAttachedFiles.Tables.Count > 0)
    //        {
    //            if (dsAttachedFiles.Tables[0].Rows.Count > 0)
    //            {
    //                foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
    //                {
    //                    count++;
    //                    string name = count + Convert.ToString(row["FILE_NAME"]);
    //                    byte[] bytes = (byte[])row["FILE_BYTES"];
    //                    zip.AddEntry(name, bytes);
    //                }
    //            }



    //            if (dsAttachedFiles.Tables[1].Rows.Count > 0)
    //            {
    //                foreach (DataRow row in dsAttachedFiles.Tables[1].Rows)
    //                {
    //                    count++;
    //                    string name = count + Convert.ToString(row["FILE_NAME"]);
    //                    byte[] bytes = (byte[])row["FILE_BYTES"];
    //                    zip.AddEntry(name, bytes);
    //                }
    //            }

    //        }

    //        //int count = 0;
    //        //foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
    //        //{
    //        //    count++;

    //        //    string name = count + Convert.ToString(row["FILE_NAME"]);
    //        //    byte[] bytes = (byte[])row["FILE_BYTES"];
    //        //    zip.AddEntry(name, bytes);
    //        //}

    //        if (count > 0)
    //        {
    //            if (pdfCopy != null)
    //            {
    //                zip.AddEntry(pdfCopyName, pdfCopy);
    //            }

    //            Response.Clear();
    //            Response.BufferOutput = false;
    //            string zipName = String.Format(voucherNo + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
    //            Response.ContentType = "application/zip";
    //            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
    //            zip.Save(Response.OutputStream);
    //            Response.End();
    //        }
    //        else
    //        {
    //            ExceptionMessage("No files found to download!");
    //            return;
    //        }


    //    }
    //}

    //protected void ExportZip(int Pid, string voucherNo, int voucherTypeId, string challanNo)
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        byte[] pdfCopy = GeneratePDF(Pid, voucherNo);
    //        string pdfCopyName = voucherNo.Replace("/", "_") + "_COPY.pdf";

    //        Helper helper = new Helper();
    //        var retuls = helper.ExportByetes(Pid, voucherNo, voucherTypeId, challanNo);

    //        if (retuls != null)
    //        {
    //            if (pdfCopy != null)
    //            {
    //                zip.AddEntry(pdfCopyName, pdfCopy);
    //            }

    //            if (retuls.mainBytes != null)
    //            {
    //                zip.AddEntry(retuls.mainFileName, retuls.mainBytes);
    //            }

    //            if (retuls.supportingBytes != null)
    //            {
    //                zip.AddEntry(retuls.supportingFileName, retuls.supportingBytes);
    //            }


    //            Response.Clear();
    //            Response.BufferOutput = false;
    //            string zipName = String.Format(voucherNo + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
    //            Response.ContentType = "application/zip";
    //            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
    //            zip.Save(Response.OutputStream);
    //            Response.End();

    //        }
    //        else
    //        {
    //            ExceptionMessage("No files found to download!");
    //            return;
    //        }
    //    }
    //}

    protected void ExportZip(int Pid, string voucherNo, int voucherTypeId, string challanNo)
    {
        using (ZipFile zip = new ZipFile())
        {
            byte[] pdfCopy = GeneratePDF(Pid, voucherNo);
            string pdfCopyName = voucherNo.Replace("/", "_") + "_COPY.pdf";

            List<string> fileNames = new List<string>();

            Helper helper = new Helper();

            var result = helper.ExportByetes(Pid, voucherNo, voucherTypeId, challanNo);

            if (result != null)
            {
                if (pdfCopy != null)
                {
                    zip.AddEntry(pdfCopyName, pdfCopy);
                }

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if (item.mainBytes != null && !fileNames.Contains(item.mainFileName))
                        {
                            zip.AddEntry(item.mainFileName, item.mainBytes);
                            fileNames.Add(item.mainFileName);
                        }

                        if (item.supportingBytes != null && !fileNames.Contains(item.mainFileName))
                        {
                            zip.AddEntry(item.supportingFileName, item.supportingBytes);
                            fileNames.Add(item.supportingFileName);
                        }
                    }
                }

                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format(voucherNo + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();

            }
            else
            {
                ExceptionMessage("No files found to download!");
                return;
            }
        }
    }

    public byte[] GeneratePDF(int Pid, string voucherNo)
    {
        try
        {
            string fileName = string.Empty;
            byte[] pdf;
            var cssText = File.ReadAllText(MapPath("~/Styles/LOT.css"));

            string htmlTxt = string.Empty;
            StringBuilder sb = new StringBuilder();

            htmlTxt = GetPDFDetailAndReturnHTML(Pid, voucherNo);

            if (!string.IsNullOrEmpty(htmlTxt))
            {
                if (!string.IsNullOrEmpty(voucherNo))
                    fileName = Convert.ToString(voucherNo);
                else
                    fileName = "Voucher_" + DateTime.Now.ToString("dd-MMM-yyyy");

                sb.Append("<html>\n");
                sb.Append("<body>\n");
                sb.Append(htmlTxt + "\n");
                sb.Append("</body>\n");
                sb.Append("</html>\n");
            }

            var html = sb.ToString();
            if (!string.IsNullOrEmpty(Convert.ToString(html)))
            {
                string imagePath = Server.MapPath("\\Images\\COPERION") + "\\logo2.png";
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                img.Alignment = Element.ALIGN_LEFT;
                img.ScaleToFit(180f, 250f);


                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document(PageSize.A4);
                    var writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    document.Add(img);
                    using (var cssMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(cssText)))
                    {
                        using (var htmlMemoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlMemoryStream, cssMemoryStream);
                        }
                    }

                    document.Close();
                    pdf = memoryStream.GetBuffer();

                    return pdf;
                }
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string GetPDFDetailAndReturnHTML(int PID, string voucherNo)
    {
        try
        {
            DataSet dsDetail = objVouchersAuthorization.GetIRVoucherDetailsForPDF(PID);

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                IRVouchersHtmlForPDF objVouchersHtmlForPDF = new IRVouchersHtmlForPDF();
                htmlText = objVouchersHtmlForPDF.GetHtmlForPDF(dsDetail);
            }
            else
            {
                htmlText = string.Empty;
            }

            if (!string.IsNullOrEmpty(htmlText))
                return htmlText;
            else
                return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private void Authorize()
    {
        DataTable dtVoucherDetails = new DataTable();

        dtVoucherDetails.Columns.Add("DOC_CLASS", typeof(string));
        dtVoucherDetails.Columns.Add("PRODUCT_CODE", typeof(string));
        dtVoucherDetails.Columns.Add("PRODUCT_DESCRIPTION", typeof(string));
        dtVoucherDetails.Columns.Add("RETURNED_PRODUCT_CODE", typeof(string));
        dtVoucherDetails.Columns.Add("RETURNED_PRODUCT_DESCRIPTION", typeof(string));
        dtVoucherDetails.Columns.Add("COST_CENTER_CODE", typeof(string));
        dtVoucherDetails.Columns.Add("COST_CENTER_NAME", typeof(string));
        dtVoucherDetails.Columns.Add("QUANTITY", typeof(decimal));
        dtVoucherDetails.Columns.Add("UOM", typeof(string));

        if (gvVoucherDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVoucherDetails.Rows)
            {
                Label lblDOCClassD = (Label)gr.FindControl("lblDOCClassD");
                Label lblProductCodeD = (Label)gr.FindControl("lblProductCodeD");
                Label lblProductDescriptionD = (Label)gr.FindControl("lblProductDescriptionD");
                Label lblReturnedProductCodeD = (Label)gr.FindControl("lblReturnedProductCodeD");
                Label lblReturnedProductDescriptionD = (Label)gr.FindControl("lblReturnedProductDescriptionD");
                Label lblCostCenterCodeD = (Label)gr.FindControl("lblCostCenterCodeD");
                Label lblCostCenterNameD = (Label)gr.FindControl("lblCostCenterNameD");
                Label lblQuantityD = (Label)gr.FindControl("lblQuantityD");
                Label lblUOMD = (Label)gr.FindControl("lblUOMD");

                DataRow dr = dtVoucherDetails.NewRow();

                dr["DOC_CLASS"] = lblDOCClassD.Text;
                dr["PRODUCT_CODE"] = lblProductCodeD.Text;
                dr["PRODUCT_DESCRIPTION"] = lblProductDescriptionD.Text;
                dr["RETURNED_PRODUCT_CODE"] = lblReturnedProductCodeD.Text;
                dr["RETURNED_PRODUCT_DESCRIPTION"] = lblReturnedProductDescriptionD.Text;
                dr["COST_CENTER_CODE"] = lblCostCenterCodeD.Text;
                dr["COST_CENTER_NAME"] = lblCostCenterNameD.Text;
                dr["QUANTITY"] = lblQuantityD.Text;
                dr["UOM"] = lblUOMD.Text;

                dtVoucherDetails.Rows.Add(dr);
            }
        }




        int val = objVouchersAuthorization.AuthorizeIRVoucher
            (
                 Pid
               , VoucherNoToAS
               , Convert.ToDateTime(VoucherDateToAS).ToString("yyyy-MM-dd")
               , ReferenceVoucherNoToAS
               , Convert.ToDateTime(ReferenceVoucherDateToAS).ToString("yyyy-MM-dd")
               , ClassToAS
               , UnitIdToAS
               , VoucherCreatedByToAS
               , RemarksToAS

               , FileNameToAS1
               , FileBytesToAS1

               , FileNameToAS2
               , FileBytesToAS2

               , FileNameToAS3
               , FileBytesToAS3

               , FileNameToAS4
               , FileBytesToAS4

               , FileNameToAS5
               , FileBytesToAS5
               , CreatedById
               , dtVoucherDetails
            );

        if (val > 0)
        {
            SuccessMessage("Voucher Number: " + VoucherNoToAS + " saved successfully!");
            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
            return;
        }

    }


    private void BulkAuthorize()
    {
        string filePath = txtFolderPathSS.Text;

        DataTable dtVouchers = new DataTable();
        dtVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtVouchers.Columns.Add("VOUCHER_DATE", typeof(string));
        dtVouchers.Columns.Add("REFERENCE_VOUCHER_NO", typeof(string));
        dtVouchers.Columns.Add("REFERENCE_VOUCHER_DATE", typeof(string));
        dtVouchers.Columns.Add("DOC_CLASS", typeof(string));
        dtVouchers.Columns.Add("UNIT_ID", typeof(int));
        dtVouchers.Columns.Add("VOUCHER_CREATED_BY", typeof(string));

        DataTable dtVoucherFiles = new DataTable();
        dtVoucherFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtVoucherFiles.Columns.Add("FILE_NAME", typeof(string));
        dtVoucherFiles.Columns.Add("FILE_BYTES", typeof(byte[]));


        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {
                CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucher");

                if (chkSelectVoucher.Checked)
                {
                    Label lblVoucherNo = (Label)gr.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)gr.FindControl("lblVoucherDate");
                    Label lblReferenceVoucherNo = (Label)gr.FindControl("lblReferenceVoucherNo");
                    Label lblReferenceVoucherDate = (Label)gr.FindControl("lblReferenceVoucherDate");
                    Label lblClass = (Label)gr.FindControl("lblClass");
                    Label lblCreatedBy = (Label)gr.FindControl("lblCreatedBy");
                    Label lblUnitId = (Label)gr.FindControl("lblUnitId");

                    DataRow dr = dtVouchers.NewRow();

                    dr["VOUCHER_NO"] = lblVoucherNo.Text;
                    dr["VOUCHER_DATE"] = lblVoucherDate.Text;
                    dr["REFERENCE_VOUCHER_NO"] = lblReferenceVoucherNo.Text;
                    dr["REFERENCE_VOUCHER_DATE"] = lblReferenceVoucherDate.Text;
                    dr["DOC_CLASS"] = lblClass.Text;
                    dr["UNIT_ID"] = lblUnitId.Text;
                    dr["VOUCHER_CREATED_BY"] = lblCreatedBy.Text;

                    dtVouchers.Rows.Add(dr);

                }
            }
        }

        if (dtVouchers.Rows.Count > 0)
        {
            if (Session["dtfiles"] != null)
            {
                DataTable dtfiles = (DataTable)Session["dtfiles"];

                foreach (DataRow drV in dtVouchers.Rows)
                {
                    foreach (var drVD in dtfiles.Select("VOUCHER_NO='" + drV["VOUCHER_NO"] + "'"))
                    {
                        DataRow dr = dtVoucherFiles.NewRow();

                        dr["VOUCHER_NO"] = drVD["VOUCHER_NO"];
                        dr["FILE_NAME"] = drVD["FILE_NAME"];
                        dr["FILE_BYTES"] = drVD["FILE_BYTES"];

                        dtVoucherFiles.Rows.Add(dr);
                    }
                }
            }

        }


        if (dtVoucherFiles.Rows.Count > 0)
        {
            int val = objVouchersAuthorization.BulkAuthorizeIRVoucher
            (
                 CreatedById
               , dtVouchers
               , dtVoucherFiles
               , filePath
            );

            if (val > 0)
            {
                SuccessMessage("Vouchers authorized successfully!");
                GetVouchersList();
            }
            else
            {
                ExceptionMessage("Please try again.");
                return;
            }
        }
        else
        {
            ExceptionMessage("No files found for authorization!");
            return;
        }

    }

    private void BulkAuthorizeWithoutDOCS()
    {
        string filePath = txtFolderPathSS.Text;

        DataTable dtVouchers = new DataTable();
        dtVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtVouchers.Columns.Add("VOUCHER_DATE", typeof(string));
        dtVouchers.Columns.Add("REFERENCE_VOUCHER_NO", typeof(string));
        dtVouchers.Columns.Add("REFERENCE_VOUCHER_DATE", typeof(string));
        dtVouchers.Columns.Add("DOC_CLASS", typeof(string));
        dtVouchers.Columns.Add("UNIT_ID", typeof(int));
        dtVouchers.Columns.Add("VOUCHER_CREATED_BY", typeof(string));


        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {
                CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucherWithoutDOCS");

                if (chkSelectVoucher.Checked)
                {
                    Label lblVoucherNo = (Label)gr.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)gr.FindControl("lblVoucherDate");
                    Label lblReferenceVoucherNo = (Label)gr.FindControl("lblReferenceVoucherNo");
                    Label lblReferenceVoucherDate = (Label)gr.FindControl("lblReferenceVoucherDate");
                    Label lblClass = (Label)gr.FindControl("lblClass");
                    Label lblCreatedBy = (Label)gr.FindControl("lblCreatedBy");
                    Label lblUnitId = (Label)gr.FindControl("lblUnitId");

                    DataRow dr = dtVouchers.NewRow();

                    dr["VOUCHER_NO"] = lblVoucherNo.Text;
                    dr["VOUCHER_DATE"] = lblVoucherDate.Text;
                    dr["REFERENCE_VOUCHER_NO"] = lblReferenceVoucherNo.Text;
                    dr["REFERENCE_VOUCHER_DATE"] = lblReferenceVoucherDate.Text;
                    dr["DOC_CLASS"] = lblClass.Text;
                    dr["UNIT_ID"] = lblUnitId.Text;
                    dr["VOUCHER_CREATED_BY"] = lblCreatedBy.Text;

                    dtVouchers.Rows.Add(dr);

                }
            }
        }

       
        if (dtVouchers.Rows.Count > 0)
        {
            int val = objVouchersAuthorization.BulkAuthorizeIRVoucherWithoutDOCS(CreatedById, dtVouchers);

            if (val > 0)
            {
                SuccessMessage("Vouchers authorized successfully!");
                GetVouchersList();
            }
            else
            {
                ExceptionMessage("Please try again.");
                return;
            }
        }
        else
        {
            ExceptionMessage("No vouchers found for authorization!");
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

    #endregion

}
