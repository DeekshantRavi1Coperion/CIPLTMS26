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

public partial class VOUCHER_AUTH_CV_ApproveCVouchers : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsVouchersList = new DataSet();
    DataSet dsVoucherDetails = new DataSet();
    DataSet _dsAttachedDocs = new DataSet();
    DataSet _dsVouchersType = new DataSet();
    DataSet _dsAttachedBy = new DataSet();
    DataSet _dsVoucherCreatedBy = new DataSet();
    DataSet _dsStatus = new DataSet();
    DataSet _dsUnit = new DataSet();


    int _unitIdS = 0;

    int _voucherTypeId = 0;
    string _startDateS = "";
    string _endDateS = "";

    string _searchTextS = "";
    int _createdById = 0;
    int _attachedById = 0;




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
            //if (chkSelectDates.Checked)
            //    _startDateS = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            //else _startDateS = "";

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
            //if (chkSelectDates.Checked)
            //    _endDateS = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            //else _endDateS = "";

            if (!string.IsNullOrEmpty(hdEndDateSearch.Value))
                _endDateS = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else _endDateS = "";

            return _endDateS;
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

    public int StatusIdS
    {
        get
        {
            if (ddlStatusToS.SelectedIndex > 0)
            {
                _statusIdToS = Convert.ToInt32(ddlStatusToS.SelectedValue);
            }
            else
            {
                _statusIdToS = -1;
            }

            return _statusIdToS;
        }
    }


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

    public string TypeIdS
    {
        get
        {
            //_typeIdToS = Convert.ToString(rdTypeToS.SelectedValue);
            return _typeIdToS;
        }
    }

    public string VoucherCreatedByS
    {
        get
        {
            if (ddlCreatedByToS.SelectedIndex > 0)
                _voucherCreatedByS = Convert.ToString(ddlCreatedByToS.Text);
            else _voucherCreatedByS = "";

            return _voucherCreatedByS;
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

    public string AmountSignS
    {
        get
        {
            _amountSignS = Convert.ToString(ddlAmountSign.Text);
            return _amountSignS;
        }
    }

    public decimal AmountOneS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAmountOneS.Text))
                _amountOneS = Convert.ToDecimal(txtAmountOneS.Text);
            else _amountOneS = 0;

            return _amountOneS;
        }
    }

    public decimal AmountTwoS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtAmountTwoS.Text))
                _amountTwoS = Convert.ToDecimal(txtAmountTwoS.Text);
            else _amountTwoS = 0;

            return _amountTwoS;
        }
    }



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

    public string VoucherDateToAS
    {
        get
        {
            if (ViewState["VoucherNo"] != null)
                _voucherDateToAS = Convert.ToString(ViewState["VoucherDate"]);
            else _voucherDateToAS = "";

            return _voucherDateToAS;
        }
    }

    public decimal AmountToAS
    {
        get
        {
            if (ViewState["Amount"] != null)
                _amountToAS = Convert.ToDecimal(ViewState["Amount"]);
            else _amountToAS = 0;

            return _amountToAS;
        }
    }

    public string ParticularToAS
    {
        get
        {
            if (ViewState["Particular"] != null)
                _particularToAS = Convert.ToString(ViewState["Particular"]);
            else _particularToAS = "";

            return _particularToAS;
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

    public bool AddNewDOCToAS
    {
        get
        {
            if (ViewState["Add_New_DOC"] != null)
                _addNewDOCToAS = Convert.ToBoolean(ViewState["Add_New_DOC"]);
            else _addNewDOCToAS = false;

            return _addNewDOCToAS;
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
                    _FileBytesToAS3 = GetFileBytes(uploadFileAttachmentToAS3.PostedFile.FileName, uploadFileAttachmentToAS2.PostedFile.InputStream);
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
    int _unitFidToI;
    string _poNoToI;
    string _poDateToI;
    string _customerCodeToI;
    string _jobNoToI;
    string _attachment1NameToI;
    Byte[] _attachment1DocToI = null;
    string _attachment1RemarksToI;
    int _createdByToI;
    private int _VoucherTypeIdToAS;
    private string _voucherCreatedByToAS;
    private string _classToAS;
    private string _particularToAS;
    private decimal _amountToAS;
    private string _voucherDateToAS;
    private string _voucherNoToAS;
    private string _fileNameToAS;
    private byte[] _FileBytesToAS;
    private string _remarksToAS;
    private string _voucherCreatedByS;
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


    private string _fileNameToAS6;
    private string _fileNameToAS7;
    private string _fileNameToAS8;
    private string _fileNameToAS9;
    private string _fileNameToAS10;
    private byte[] _FileBytesToAS6;
    private byte[] _FileBytesToAS7;
    private byte[] _FileBytesToAS8;
    private byte[] _FileBytesToAS9;
    private byte[] _FileBytesToAS10;




    private string _dateSignS;
    private string _dateTypeS;
    private string _searchByS;
    private string _customerCodeS;
    private string _customerNameS;
    private string _amountSignS;
    private decimal _amountOneS;
    private decimal _amountTwoS;
    private int _statusIdToS;
    private string _warehouseIdToS;
    private bool _addNewDOCToAS;
    private string _typeIdToS;


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
                GetVouchersCreatedByList();
                //BindVouchersType();

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
            Label lblPID = (Label)e.Row.FindControl("lblPID");
            Label lblDocumentsCount = (Label)e.Row.FindControl("lblDocumentsCount");
            Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
            Label lblApproverID = (Label)e.Row.FindControl("lblApproverID");

            ImageButton imgBtnDownloadAllDocuments = (ImageButton)e.Row.FindControl("imgBtnDownloadAllDocuments");
            ImageButton imgBtnApprove = (ImageButton)e.Row.FindControl("imgBtnApprove");

            CheckBox chkSelectVoucher = (CheckBox)e.Row.FindControl("chkSelectVoucher");
            chkSelectVoucher.Visible = false;

            string approverIdsTxt = lblApproverID.Text;

            //if (Convert.ToInt32(lblStatusID.Text) == 1)
            //{
            //    imgBtnApprove.ImageUrl = "~/Images/VC/vc-authorized.png";
            //    imgBtnApprove.ToolTip = "Approve voucher";
            //    imgBtnApprove.Enabled = true;

            //    if (Convert.ToInt32(lblApproverID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
            //    {
            //        imgBtnApprove.Visible = true;
            //        chkSelectVoucher.Visible = true;
            //    }
            //    else
            //    {
            //        imgBtnApprove.Visible = false;
            //    }

            //}
            //else if (Convert.ToInt32(lblStatusID.Text) == 2)
            //{
            //    imgBtnApprove.ImageUrl = "~/Images/VC/vc-approved.png";
            //    imgBtnApprove.ToolTip = "Voucher approved";
            //    imgBtnApprove.Enabled = false;
            //}

            if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.OPEN)
            {
                imgBtnApprove.ImageUrl = "~/Images/VC/vc-pending.png";
                imgBtnApprove.ToolTip = "Unauthorized voucher";
                imgBtnApprove.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.AUTHORIZED)
            {
                imgBtnApprove.ImageUrl = "~/Images/VC/vc-authorized.png";
                imgBtnApprove.ToolTip = "Approve voucher";
                imgBtnApprove.Enabled = true;

                //if (Convert.ToInt32(lblApproverID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                //{
                //    imgBtnApprove.Visible = true;
                //    chkSelectVoucher.Visible = true;
                //}
                //else
                //{
                //    imgBtnApprove.Visible = false;
                //}

                imgBtnApprove.Visible = false;
                if (!string.IsNullOrEmpty(approverIdsTxt))
                {
                    string[] approverIds = approverIdsTxt.Split(',');
                    if (approverIds.Length > 0)
                    {
                        foreach (string approverId in approverIds)
                        {
                            if (Convert.ToInt32(approverId) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                            {
                                imgBtnApprove.Visible = true;
                                chkSelectVoucher.Visible = true;
                            }
                        }
                    }
                }

            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.APPROVED)
            {
                imgBtnApprove.ImageUrl = "~/Images/VC/vc-approved.png";
                imgBtnApprove.ToolTip = "Voucher approved";
                imgBtnApprove.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZATION)
            {
                imgBtnApprove.ImageUrl = "~/Images/VC/vc-reauthorization.png";
                imgBtnApprove.ToolTip = "Reauthorization voucher";
                imgBtnApprove.Enabled = false;
            }
            else if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZED)
            {
                imgBtnApprove.ImageUrl = "~/Images/VC/vc-reauthorized.png";
                imgBtnApprove.ToolTip = "Approve reauthorized voucher";
                imgBtnApprove.Enabled = true;

                //if (Convert.ToInt32(lblApproverID.Text) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                //{
                //    imgBtnApprove.Visible = true;
                //    chkSelectVoucher.Visible = true;
                //}
                //else
                //{
                //    imgBtnApprove.Visible = false;
                //}

                imgBtnApprove.Visible = false;
                if (!string.IsNullOrEmpty(approverIdsTxt))
                {
                    string[] approverIds = approverIdsTxt.Split(',');
                    if (approverIds.Length > 0)
                    {
                        foreach (string approverId in approverIds)
                        {
                            if (Convert.ToInt32(approverId) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                            {
                                imgBtnApprove.Visible = true;
                                chkSelectVoucher.Visible = true;
                            }
                        }
                    }
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
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "VIEW_VOUCHER_PDF" ||
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "APPROVE" ||
                    Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvVouchersList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblStatusID = gvVouchersList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblVoucherNo = gvVouchersList.Rows[rowindex].FindControl("lblVoucherNo") as Label;
                Label lblVoucherDate = gvVouchersList.Rows[rowindex].FindControl("lblVoucherDate") as Label;

                Label lblType = gvVouchersList.Rows[rowindex].FindControl("lblType") as Label;
                Label lblTypeFull = gvVouchersList.Rows[rowindex].FindControl("lblTypeFull") as Label;
                Label lblClass = gvVouchersList.Rows[rowindex].FindControl("lblClass") as Label;

                Label lblGLCode = gvVouchersList.Rows[rowindex].FindControl("lblGLCode") as Label;
                Label lblGLDescription = gvVouchersList.Rows[rowindex].FindControl("lblGLDescription") as Label;

                Label lblCurrencyDesc = gvVouchersList.Rows[rowindex].FindControl("lblCurrencyDesc") as Label;
                Label lblAmount = gvVouchersList.Rows[rowindex].FindControl("lblAmount") as Label;

                Label lblReceiptAmountFC = gvVouchersList.Rows[rowindex].FindControl("lblReceiptAmountFC") as Label;
                Label lblPaymentAmountFC = gvVouchersList.Rows[rowindex].FindControl("lblPaymentAmountFC") as Label;
                Label lblReceiptAmountINR = gvVouchersList.Rows[rowindex].FindControl("lblReceiptAmountINR") as Label;
                Label lblPaymentAmountINR = gvVouchersList.Rows[rowindex].FindControl("lblPaymentAmountINR") as Label;

                Label lblPaidToOrReceivedFrom = gvVouchersList.Rows[rowindex].FindControl("lblPaidToOrReceivedFrom") as Label;

                Label lblChequeOrReferenceNo = gvVouchersList.Rows[rowindex].FindControl("lblChequeOrReferenceNo") as Label;
                Label lblChequeDated = gvVouchersList.Rows[rowindex].FindControl("lblChequeDated") as Label;
                Label lblBankTransactionRefNoOrUTR = gvVouchersList.Rows[rowindex].FindControl("lblBankTransactionRefNoOrUTR") as Label;
                Label lblReconcileDate = gvVouchersList.Rows[rowindex].FindControl("lblReconcileDate") as Label;
                Label lblNarration = gvVouchersList.Rows[rowindex].FindControl("lblNarration") as Label;

                Label lblCreatedBy = gvVouchersList.Rows[rowindex].FindControl("lblCreatedBy") as Label;
                Label lblUnitId = gvVouchersList.Rows[rowindex].FindControl("lblUnitId") as Label;
                Label lblUnitName = gvVouchersList.Rows[rowindex].FindControl("lblUnitName") as Label;

                ViewState["PID"] = lblPID.Text;
                ViewState["VoucherNo"] = lblVoucherNo.Text;
                ViewState["VoucherDate"] = lblVoucherDate.Text;
                ViewState["UnitId"] = lblUnitId.Text;

                ViewState["Add_New_DOC"] = false;

                btnApprove.Visible = false;
                btnSendToReauthorization.Visible = false;
                pnlRemarksToAS.Visible = false;
                pnlAttachmentToAS.Visible = false;
                //pnlAttachedMRNFilesAu.Visible = true;

                txtRemarksToAS.Text = string.Empty;
                ////pnlViewAuthorizeVoucherPopup.Height = 900;
                //dvViewAuthorizeVoucherPopup.Style["height"] = "800px";

                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                {
                    lblAuthorizeVoucherDetailsLegend.Text = "View Voucher Details";

                    divAttachedFiles.Visible = true;

                    if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                    {
                        ////pnlViewAuthorizeVoucherPopup.Height = 670;
                        //dvViewAuthorizeVoucherPopup.Style["height"] = "600px";
                    }

                    if (Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                    {

                        ViewState["Add_New_DOC"] = true;

                        btnApprove.Visible = true;
                        pnlAttachmentToAS.Visible = true;

                        //pnlAttachedMRNFilesAu.Visible = false;

                        lblAuthorizeVoucherDetailsLegend.Text = "Add New Documents";
                        btnApprove.Text = "Save Attachments";
                        ////pnlViewAuthorizeVoucherPopup.Height = 830;
                        //dvViewAuthorizeVoucherPopup.Style["height"] = "730px";
                    }

                    lblAuthorizeVoucherDetailsLegend.Text = "View Voucher Details";

                    lblVoucherNoToAS.Text = lblVoucherNo.Text;
                    //txtTypeToAS.Text = lblTypeFull.Text;
                    //txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    //txtCreatedByToAS.Text = lblCreatedBy.Text;
                    //txtUnitToAS.Text = lblUnitName.Text;


                    txtVoucherNoToAS.Text = lblVoucherNo.Text;
                    txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    //txtTypeToAS.Text = lblTypeFull.Text;
                    //txtDocClassToAS.Text = lblClass.Text;
                    txtCurrencyDescToAS.Text = lblCurrencyDesc.Text;
                    txtAmountToAS.Text = lblAmount.Text;
                    //txtReceiptAmountInrToAS.Text = lblReceiptAmountINR.Text;
                    //txtPaymentAmountInrToAS.Text = lblPaymentAmountINR.Text;
                    //txtPaidToOrReceivedFromToAS.Text = lblPaidToOrReceivedFrom.Text;
                    txtChequeOrReferenceNoToAS.Text = lblChequeOrReferenceNo.Text;
                    txtChequeDatedToAS.Text = lblChequeDated.Text;
                    //txtBankTransactionRefNoOrUtrToAS.Text = lblBankTransactionRefNoOrUTR.Text;
                    txtNarrationToAS.Text = lblNarration.Text;
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
                            , (int)VoucherStatusTypes.EnumVoucherTypes.CV
                            , "");
                }

                else if (Convert.ToString(e.CommandArgument) == "APPROVE")
                {
                    pnlAttachmentToAS.Visible = true;

                    //if (Convert.ToInt32(lblMRNPidD.Text) > 0)
                    //{
                    //    pnlMRNAttachmentToAS.Visible = true;
                    //}

                    pnlRemarksToAS.Visible = true;

                    if (Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.AUTHORIZED ||
                        Convert.ToInt32(lblStatusID.Text) == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZED)
                    {
                        btnApprove.Visible = true;
                        btnSendToReauthorization.Visible = true;
                    }

                    lblAuthorizeVoucherDetailsLegend.Text = "Approve Voucher";
                    btnApprove.Text = "Approve Voucher";

                    lblVoucherNoToAS.Text = lblVoucherNo.Text;
                    //txtTypeToAS.Text = lblTypeFull.Text;
                    //txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    //txtCreatedByToAS.Text = lblCreatedBy.Text;
                    //txtUnitToAS.Text = lblUnitName.Text;

                    txtVoucherNoToAS.Text = lblVoucherNo.Text;
                    txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    //txtTypeToAS.Text = lblTypeFull.Text;
                    //txtDocClassToAS.Text = lblClass.Text;
                    txtCurrencyDescToAS.Text = lblCurrencyDesc.Text;
                    txtAmountToAS.Text = lblAmount.Text;
                    //txtReceiptAmountInrToAS.Text = lblReceiptAmountINR.Text;
                    //txtPaymentAmountInrToAS.Text = lblPaymentAmountINR.Text;
                    //txtPaidToOrReceivedFromToAS.Text = lblPaidToOrReceivedFrom.Text;
                    txtChequeOrReferenceNoToAS.Text = lblChequeOrReferenceNo.Text;
                    txtChequeDatedToAS.Text = lblChequeDated.Text;
                    //txtBankTransactionRefNoOrUtrToAS.Text = lblBankTransactionRefNoOrUTR.Text;
                    txtNarrationToAS.Text = lblNarration.Text;
                    txtCreatedByToAS.Text = lblCreatedBy.Text;


                    GetVouchersDetails(lblVoucherNo.Text, Convert.ToInt32(lblUnitId.Text));

                    BindAttachedDocs(Convert.ToInt32(lblPID.Text));

                    mpeAuthorizeVoucher.Show();

                    //btnApprove.Visible = true;
                }

                else if (Convert.ToString(e.CommandArgument) == "VIEW_VOUCHER_PDF")
                {
                    mpeViewVoucherInPDF.Show();
                    iframeVoucherInPDF.Attributes.Add("src", "VouchersPDF.aspx?pid=" + Convert.ToString(lblPID.Text));
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

    protected void gvAttachedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvAttachedFiles.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblAttachedFileName = gvAttachedFiles.Rows[rowindex].FindControl("lblAttachedFileName") as Label;


                ViewState["PID"] = lblPID.Text;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedFileName.Text).Trim()
                                   , (int)VoucherStatusTypes.EnumVoucherTypes.CV);
                    mpeAuthorizeVoucher.Show();
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


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //mpeAuthorizeVoucher.Show();
        ApproveVoucher((int)VoucherStatusTypes.EnumStatus.APPROVED, (int)VoucherStatusTypes.EnumVoucherTypes.CV);
    }

    protected void btnSendToReauthorization_Click(object sender, EventArgs e)
    {
        ApproveVoucher((int)VoucherStatusTypes.EnumStatus.REAUTHORIZATION, (int)VoucherStatusTypes.EnumVoucherTypes.CV);
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

    protected void btnBulkApprove_Click(object sender, EventArgs e)
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
            BulkApproveVoucher();
        }
        else
        {
            ExceptionMessage("Please select at-least 1 voucher!");
            return;
        }
    }


    

    protected void rdTypeToS_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetVouchersList();
    }

    #endregion


    #region METHODS[=======================]


    private void BindStatusList()
    {
        try
        {
            _dsStatus = objVouchersAuthorization.GetVouchersStatusList(0);
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

    private void GetVouchersCreatedByList()
    {
        try
        {
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList((int)VoucherStatusTypes.EnumVoucherTypes.CV);
            if (_dsVoucherCreatedBy.Tables.Count > 0 && _dsVoucherCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlCreatedByToS.DataSource = _dsVoucherCreatedBy.Tables[0];
                ddlCreatedByToS.DataTextField = "VOUCHER_CREATED_BY";
                ddlCreatedByToS.DataValueField = "VOUCHER_CREATED_BY";
                ddlCreatedByToS.DataBind();
                ddlCreatedByToS.Items.Insert(0, "All");
                ddlCreatedByToS.SelectedIndex = 0;
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
            dsVouchersList = objVouchersAuthorization.GetCVouchersListToApprove
                (
                        DateSignS
                    , DateTypeS
                    , StartDateS
                    , EndDateS
                    , WarehouseIdS
                    , TypeIdS
                    , UnitIdS
                    , StatusIdS
                    , VoucherCreatedByS
                    , SearchByS
                    , SearchTextS
                    , AmountSignS
                    , AmountOneS
                    , AmountTwoS
                );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
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

    private void ApproveVoucher(int statusId, int typeId)
    {
        int val = objVouchersAuthorization.ApproveVoucher
            (
                Pid
            , typeId
            , statusId
            , AddNewDOCToAS
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
            if (statusId == (int)VoucherStatusTypes.EnumStatus.APPROVED)
                SuccessMessage("Voucher Number: " + VoucherNoToAS + " approved successfully!");
            else if (statusId == (int)VoucherStatusTypes.EnumStatus.REAUTHORIZATION)
            {
                //Send email
                Helper helper = new Helper();
                bool mailSent = helper.ProcessSendMail(Pid, typeId);
                string mailSentMsg = "";
                if (mailSent) mailSentMsg = "and mail sent";

                SuccessMessage("Voucher Number: " + VoucherNoToAS + " sent to reauthorization " + mailSentMsg + " successfully!");
            }

            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
            return;
        }
    }

    private void SendVoucherToReauthorization()
    {
        int val = objVouchersAuthorization.SendVoucherToReauthorization
            (
                Pid
            , (int)VoucherStatusTypes.EnumVoucherTypes.CV
            , AddNewDOCToAS
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
            SuccessMessage("Voucher Number: " + VoucherNoToAS + " sent to reauthorization successfully!");
            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
            return;
        }
    }

    private void BulkApproveVoucher()
    {
        DataTable dtVoucherPID = new DataTable();
        dtVoucherPID.Columns.Add("PID", typeof(int));

        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {
                CheckBox chkSelectVoucher = (CheckBox)gr.FindControl("chkSelectVoucher");

                if (chkSelectVoucher.Checked)
                {
                    Label lblPID = (Label)gr.FindControl("lblPID");

                    DataRow dr = dtVoucherPID.NewRow();
                    dr["PID"] = lblPID.Text;
                    dtVoucherPID.Rows.Add(dr);
                }
            }
        }


        int val = objVouchersAuthorization.BulkApproveVoucher
            (
              (int)VoucherStatusTypes.EnumVoucherTypes.CV
            , dtVoucherPID
            , CreatedById
            );

        if (val > 0)
        {
            SuccessMessage("Vouchers approved successfully!");
            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
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

    private void BindAttachedDocs(int PId)
    {
        try
        {
            _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(PId, (int)VoucherStatusTypes.EnumVoucherTypes.CV, "");

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
            dsVoucherDetails = objVouchersAuthorization.GetCVoucherDetails(voucherNo, unitID);
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

    //protected void ExportZip(int Pid, string voucherNo)
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        byte[] pdfCopy = GeneratePDF(Pid, voucherNo);
    //        string pdfCopyName = voucherNo.Replace("/", "_") + "_COPY.pdf";

    //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;

    //        DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCFiles(Pid, (int)VoucherStatusTypes.EnumVoucherTypes.CV);

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
            DataSet dsDetail = objVouchersAuthorization.GetCVoucherDetailsForPDF(PID);

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                CVouchersHtmlForPDF objVouchersHtmlForPDF = new CVouchersHtmlForPDF();
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
