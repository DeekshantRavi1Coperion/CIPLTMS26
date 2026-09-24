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

public partial class VOUCHER_AUTH_POV_VouchersReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsVouchersDetails = new DataSet();
    DataSet _dsAttachedDocs = new DataSet();
    DataSet _dsVouchersType = new DataSet();
    DataSet _dsAttachedBy = new DataSet();
    DataSet _dsVoucherCreatedBy = new DataSet();

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

    public int VoucherTypeId
    {
        get
        {
            if (ddlVoucherType.SelectedIndex > 0)
            {
                _voucherTypeId = Convert.ToInt32(ddlVoucherType.SelectedValue);
            }
            else
            {
                _voucherTypeId = 0;
            }

            return _voucherTypeId;
        }
    }

    public string VoucherNumber
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVoucherNumberToS.Text))
                _voucherNumber = Convert.ToString(txtVoucherNumberToS.Text);
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

        set
        {
            _createdById = value;
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


    //public int AttachedById
    //{
    //    get
    //    {
    //        if (ddlAttachedBy.SelectedIndex > 0)
    //            _attachedById = Convert.ToInt32(ddlAttachedBy.SelectedValue);
    //        else _attachedById = 0;

    //        return _attachedById;
    //    }
    //}

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



    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["VOUCHERS"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                GetVouchersCreatedByList();
                BindVouchersType();

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
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvVouchersList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["VOUCHERS"];
            ToCSVNew01(dt);
        }
        else
        {
            ExceptionMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindVouchersType()
    {
        try
        {
            _dsVouchersType = objVouchersAuthorization.GetVouchersType();
            if (_dsVouchersType.Tables.Count > 0 && _dsVouchersType.Tables[0].Rows.Count > 0)
            {
                ddlVoucherType.DataSource = _dsVouchersType.Tables[0];
                ddlVoucherType.DataTextField = "NAME";
                ddlVoucherType.DataValueField = "PID";
                ddlVoucherType.DataBind();
                ddlVoucherType.Items.Insert(0, "All");
                ddlVoucherType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetVouchersCreatedByList()
    {
        try
        {
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList((int)VoucherStatusTypes.EnumVoucherTypes.POV);
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
            throw ex;
        }
    }

    private void GetVouchersList()
    {
        try
        {
            //dsVouchersDetails = objVouchersAuthorization.GetVouchersReport
            //    (
            //          StartDate
            //        , EndDate
            //        , VoucherNumber
            //        , VoucherTypeId
            //        , VoucherCreatedBy
            //    );
            if (dsVouchersDetails.Tables.Count > 0 && dsVouchersDetails.Tables[0].Rows.Count > 0)
            {
                Session["VOUCHERS"]= dsVouchersDetails.Tables[0];
                gvVouchersList.DataSource = dsVouchersDetails.Tables[0];
                gvVouchersList.DataBind();
            }
            else
            {
                Session["VOUCHERS"] = null;
                gvVouchersList.DataSource = null;
                gvVouchersList.DataBind();
            }

            //lblRecords.Text = "Records[" + dsVouchersDetails.Tables[0].Rows.Count + "]";
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

            string fileName = "VouchersReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
