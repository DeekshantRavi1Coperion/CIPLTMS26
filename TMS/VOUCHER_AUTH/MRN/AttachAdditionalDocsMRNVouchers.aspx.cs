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

public partial class VOUCHER_AUTH_MRN_AttachAdditionalDocsMRNVouchers : System.Web.UI.Page
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

    public int StatusId
    {
        get
        {
            if (ddlStatus.SelectedIndex > 0)
            {
                _statusId = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            else
            {
                _statusId = -1;
            }

            return _statusId;
        }
    }

    public string WarehouseId
    {
        get
        {
            if (ddlWarehouse.SelectedIndex > 0)
            {
                _warehouseId = Convert.ToString(ddlWarehouse.SelectedValue);
            }
            else
            {
                _warehouseId = "";
            }

            return _warehouseId;
        }
    }

    public int UnitId
    {
        get
        {
            if (ddlUnit.SelectedIndex > 0)
            {
                _unitId = Convert.ToInt32(ddlUnit.SelectedValue);
            }
            else
            {
                _unitId = 0;
            }

            return _unitId;
        }
    }

    public string VoucherNumber
    {
        get
        {

            if (!string.IsNullOrEmpty(txtVoucherNumberToS.Text))
            {
                var texts = txtVoucherNumberToS.Text.Split(',');
                string txt = "";
                foreach (var item in texts)
                {
                    string dd = item.Replace('\r', ' ').Replace('\n', ' ').Trim();
                    txt += "'" + dd + "',";
                }
                _voucherNumber = txt.TrimEnd(',');
            }
            else _voucherNumber = "";

            return _voucherNumber;
        }
    }

    public int CreatedById
    {
        get
        {
            _createdById = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _createdById;
        }
    }

    public string VoucherCreatedBy
    {
        get
        {
            if (ddlCreatedBy.SelectedIndex > 0)
                _voucherCreatedBy = Convert.ToString(ddlCreatedBy.Text);
            else _voucherCreatedBy = "";

            return _voucherCreatedBy;
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


    public int IsMRNClosedToAS
    {
        get
        {
            if (chkCloseMRNToAS.Checked)
                _isMRNClosedToAS = 1;
            else
                _isMRNClosedToAS = 0;

            return _isMRNClosedToAS;
        }
    }

    //public int UnitFidToI
    //{
    //    get
    //    {
    //        if (ViewState["UNIT_FID"] != null)
    //            _unitFidToI = Convert.ToInt32(ViewState["UNIT_FID"]);
    //        else _unitFidToI = 0;

    //        return _unitFidToI;
    //    }

    //    set
    //    {
    //        _unitFidToI = value;
    //    }
    //}

    //public string PoNoToI
    //{
    //    get
    //    {
    //        _poNoToI = txtPoNoToS.Text.Trim().ToUpper();
    //        return _poNoToI;
    //    }

    //    set
    //    {
    //        _poNoToI = value;
    //    }
    //}

    //public string PoDateToI
    //{
    //    get
    //    {
    //        _poDateToI = Convert.ToDateTime(txtPoDateToS.Text).ToString("yyyy-MM-dd");
    //        return _poDateToI;
    //    }

    //    set
    //    {
    //        _poDateToI = value;
    //    }
    //}

    //public string VendorCodeToI
    //{
    //    get
    //    {
    //        _vendorCodeToI = txtVendorCodeToS.Text.Trim().ToUpper();
    //        return _vendorCodeToI;
    //    }

    //    set
    //    {
    //        _vendorCodeToI = value;
    //    }
    //}

    //public string JobNoToI
    //{
    //    get
    //    {
    //        _jobNoToI = txtJobNoToS.Text.Trim().ToUpper();
    //        return _jobNoToI;
    //    }

    //    set
    //    {
    //        _jobNoToI = value;
    //    }
    //}

    //public string Attachment1NameToI
    //{
    //    get
    //    {
    //        if (uploadFileAttachment1.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
    //            {
    //                string[] str = uploadFileAttachment1.PostedFile.FileName.Split('\\');
    //                int length = str.Length;
    //                _attachment1NameToI = str[str.Length - 1];
    //            }
    //        }
    //        else _attachment1NameToI = "";

    //        return _attachment1NameToI;
    //    }

    //    set
    //    {
    //        _attachment1NameToI = value;
    //    }
    //}

    //public byte[] Attachment1DocToI
    //{
    //    get
    //    {
    //        if (uploadFileAttachment1.HasFile)
    //        {
    //            if (!string.IsNullOrEmpty(uploadFileAttachment1.PostedFile.FileName))
    //            {
    //                _attachment1DocToI = GetFileBytes(uploadFileAttachment1.PostedFile.FileName, uploadFileAttachment1.PostedFile.InputStream);
    //            }
    //        }
    //        else _attachment1DocToI = null;

    //        return _attachment1DocToI;
    //    }

    //    set
    //    {
    //        _attachment1DocToI = value;
    //    }
    //}

    //public string Attachment1RemarksToI
    //{
    //    get
    //    {
    //        if (!string.IsNullOrEmpty(txtRemarksToS.Text))
    //            _attachment1RemarksToI = txtRemarksToS.Text;
    //        else _attachment1RemarksToI = "";

    //        return _attachment1RemarksToI;
    //    }

    //    set
    //    {
    //        _attachment1RemarksToI = value;
    //    }
    //}

    //public int CreatedByToI
    //{
    //    get
    //    {
    //        _createdByToI = Convert.ToInt32(Session["EMP_RECORD_ID"]);
    //        return _createdByToI;
    //    }

    //    set
    //    {
    //        _createdByToI = value;
    //    }
    //}

    int _pid;
    int _unitFidToI;
    string _poNoToI;
    string _poDateToI;
    string _vendorCodeToI;
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
    private string _voucherCreatedBy;
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
    private int _statusId;
    private string _warehouseId;
    private int _isMRNClosedToAS;



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
            ImageButton imgBtnDownloadAllDocuments = (ImageButton)e.Row.FindControl("imgBtnDownloadAllDocuments");

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
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvVouchersList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblVoucherNo = gvVouchersList.Rows[rowindex].FindControl("lblVoucherNo") as Label;
                Label lblVoucherDate = gvVouchersList.Rows[rowindex].FindControl("lblVoucherDate") as Label;
                Label lblPurchaseOrderNo = gvVouchersList.Rows[rowindex].FindControl("lblPurchaseOrderNo") as Label;
                Label lblPurchaseOrderDate = gvVouchersList.Rows[rowindex].FindControl("lblPurchaseOrderDate") as Label;

                ViewState["PID"] = lblPID.Text;
                

                pnlAttachmentToAS.Visible = false;
                btnSaveAttachments.Visible = false;
                //pnlViewAuthorizeVoucherPopup.Height = 900;


                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                {
                    lblAuthorizeVoucherDetailsLegend.Text = "View Voucher Details";

                    divAttachedFiles.Visible = true;

                    if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                    {
                        //pnlViewAuthorizeVoucherPopup.Height = 670;
                    }

                    if (Convert.ToString(e.CommandArgument) == "ADD_NEW_DOCUMENT")
                    {
                        pnlAttachmentToAS.Visible = true;
                        btnSaveAttachments.Visible = true;
                        lblAuthorizeVoucherDetailsLegend.Text = "Add New Documents";
                        //pnlViewAuthorizeVoucherPopup.Height = 830;
                    }

                    lblAuthorizeVoucherDetailsLegend.Text = "View Voucher Details";

                    lblVoucherNoToAS.Text = lblVoucherNo.Text;
                    txtVoucherDateToAS.Text = lblVoucherDate.Text;
                    txtPurchaseOrderNoToAS.Text = lblPurchaseOrderNo.Text;
                    txtPurchaseOrderDateToAS.Text = lblPurchaseOrderDate.Text;

                    BindAttachedDocs(Convert.ToInt32(lblPID.Text));

                    mpeAuthorizeVoucher.Show();

                }

                else if (Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    //ExportZip(Convert.ToInt32(lblPID.Text), lblVoucherNo.Text);
                    ExportZip(Convert.ToInt32(lblPID.Text)
                            , lblVoucherNo.Text.Trim().ToUpper()
                            , (int)VoucherStatusTypes.EnumVoucherTypes.MRN
                            , "");
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


                //ViewState["PID"] = lblPID.Text;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedFileName.Text).Trim());
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


    protected void btnSaveAttachments_Click(object sender, EventArgs e)
    {
        //mpeAuthorizeVoucher.Show();
        ApproveVoucher();
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
                ddlStatus.DataSource = _dsStatus.Tables[0];
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

    private void BindUnits()
    {
        try
        {
            _dsUnit = objVouchersAuthorization.GetUnits();
            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = _dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
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
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList((int)VoucherStatusTypes.EnumVoucherTypes.MRN);
            if (_dsVoucherCreatedBy.Tables.Count > 0 && _dsVoucherCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlCreatedBy.DataSource = _dsVoucherCreatedBy.Tables[0];
                ddlCreatedBy.DataTextField = "VOUCHER_CREATED_BY";
                ddlCreatedBy.DataValueField = "VOUCHER_CREATED_BY";
                ddlCreatedBy.DataBind();
                ddlCreatedBy.Items.Insert(0, "All");
                ddlCreatedBy.SelectedIndex = 0;
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
            dsVouchersList = objVouchersAuthorization.GetMRNVouchersListToAttachAdditionalDocs
                (
                      StartDate
                    , EndDate
                    , StatusId
                    , WarehouseId
                    , UnitId
                    , VoucherNumber
                    , CreatedById
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
            _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(PId, (int)VoucherStatusTypes.EnumVoucherTypes.MRN,"");

            if (_dsAttachedDocs.Tables.Count > 0 && _dsAttachedDocs.Tables[0].Rows.Count > 0)
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
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ViewAttachedFiles(int docID, string fileName)
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
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?docID=" + docID);// + "&fileType=" + fileType) ;
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

    //        DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCFiles(Pid, (int)VoucherStatusTypes.EnumVoucherTypes.MRN);

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
            DataSet dsDetail = objVouchersAuthorization.GetMRNVoucherDetailsForPDF(PID);

            string htmlText = string.Empty;
            if (dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                MRNVouchersHtmlForPDF objVouchersHtmlForPDF = new MRNVouchersHtmlForPDF();
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

    private void ApproveVoucher()
    {
        int val = objVouchersAuthorization.AttachMRNAdditionalDocs
            (
                Pid
            , (int)VoucherStatusTypes.EnumVoucherTypes.MRN
            , IsMRNClosedToAS
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
            SuccessMessage("Voucher Number: " + VoucherNoToAS + " approved successfully!");
            GetVouchersList();
        }
        else
        {
            ExceptionMessage("Please try again.");
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
