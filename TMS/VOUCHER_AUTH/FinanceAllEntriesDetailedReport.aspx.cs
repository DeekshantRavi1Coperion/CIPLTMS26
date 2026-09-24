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
using System.Linq;

public partial class VOUCHER_AUTH_FinanceAllEntriesDetailedReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsVouchersList = new DataSet();
    DataSet _dsVoucherCreatedBy = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsStatus = new DataSet();
    DataSet _dsVouchersType = new DataSet();
    DataSet dsVouchersDetails = new DataSet();
    DataSet _dsAttachedDocs = new DataSet();

    int _unitIdS = 0;

    string _startDateS = "";
    string _endDateS = "";
    string _searchTextS = "";


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

    public int MonthS
    {
        get
        {
            if (ddlMonthToS.SelectedIndex > 0)
            {
                _monthS = Convert.ToInt32(ddlMonthToS.SelectedValue);
            }
            else
            {
                _monthS = 0;
            }

            return _monthS;
        }
    }

    public string DocumentNumberS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtVoucherNoToS.Text))
            {
                _documentNumberS = txtVoucherNoToS.Text.Trim().ToUpper();
            }
            else _documentNumberS = "";

            return _documentNumberS;
        }
    }


    public int DocumentTypeIdS
    {
        get
        {
            if (ddlVoucherTypeToS.SelectedIndex > 0)
            {
                _documentTypeIdS = Convert.ToInt32(ddlVoucherTypeToS.SelectedValue);
            }
            else
            {
                _documentTypeIdS = 0;
            }

            return _documentTypeIdS;
        }
    }

    public string PartyNameS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtPartyNameToS.Text))
            {
                _partyNameS = txtPartyNameToS.Text.Trim();
            }
            else _partyNameS = "";

            return _partyNameS;
        }
    }

    public string GLCodeS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtGLCodeToS.Text))
            {
                _GLCodeS = txtGLCodeToS.Text.Trim().ToUpper();
            }
            else _GLCodeS = "";

            return _GLCodeS;
        }
    }


    public string GLDescriptionS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtGLDescriptionToS.Text))
            {
                _GLDescriptionS = txtGLDescriptionToS.Text.Trim();
            }
            else _GLDescriptionS = "";

            return _GLDescriptionS;
        }
    }


    public string DOCClassS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtDOCClassToS.Text))
            {
                _DOCClassS = txtDOCClassToS.Text.Trim();
            }
            else _DOCClassS = "";

            return _DOCClassS;
        }
    }


    public string ProductCodeS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtProductCodeToS.Text))
            {
                _ProductCodeS = txtProductCodeToS.Text.Trim().ToUpper();
            }
            else _ProductCodeS = "";

            return _ProductCodeS;
        }
    }


    public string ProductDescriptionS
    {
        get
        {

            if (!string.IsNullOrEmpty(txtProductDescriptionToS.Text))
            {
                _ProductDescriptionS = txtProductDescriptionToS.Text.Trim();
            }
            else _ProductDescriptionS = "";

            return _ProductDescriptionS;
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

    public string CreatedByIdS
    {
        get
        {
            if (ddlCreatedByToS.SelectedIndex > 0)
            {
                _createdById = Convert.ToString(ddlCreatedByToS.SelectedValue).ToUpper();
            }
            else
            {
                _createdById = "";
            }

            return _createdById;
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

    private string _voucherCreatedByS;
    private string _dateSignS;
    private string _dateTypeS;
    private string _searchByS;
    private string _customerCodeS;
    private string _customerNameS;
    private string _amountSignS;
    private decimal _amountOneS;
    private decimal _amountTwoS;
    private int _statusIdS;
    private int _monthS;
    private string _documentNumberS;
    private int _documentTypeIdS;
    private string _partyNameS;
    private string _GLCodeS;
    private string _GLDescriptionS;
    private string _createdById;
    private string _ProductCodeS;
    private string _ProductDescriptionS;
    private string _DOCClassS;



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
                BindVouchersType();

                //GetVouchersList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["VoucherTypeid"] = null;
        ViewState["VoucherTypeidO"] = null;
        pnlMsg.Visible = false;
        GetVouchersList();
    }

    protected void gvVouchersList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblSRPNo = (Label)e.Row.FindControl("lblSRPNo");
            ImageButton btnViewVoucherDetail = (ImageButton)e.Row.FindControl("btnViewVoucherDetail");

            //btnViewVoucherDetail.Visible = false;


            //if (Convert.ToInt32(lblSRPNo.Text) > 0)
            //{
            //    btnViewVoucherDetail.Visible = true;
            //}

            if (lblStatus.Text.ToUpper() == Constants.PENDING)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = "<span style='color:red;'>" + e.Row.Cells[i].Text + "</span>";
                }
            }


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }


    private void DisableZoomButton()
    {
        if (gvVouchersList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvVouchersList.Rows)
            {

                Label lblSRPNo = gr.FindControl("lblSRPNo") as Label;
                Label lblStatus = gr.FindControl("lblStatus") as Label;
                ImageButton btnViewVoucherDetail = gr.FindControl("btnViewVoucherDetail") as ImageButton;

                btnViewVoucherDetail.Visible = true;

                //int srpN0 = 0;
                //if (string.IsNullOrEmpty(lblSRPNo.Text)) srpN0 = 0;
                //else srpN0 = Convert.ToInt32(lblSRPNo.Text);

                //if (srpN0 == 0)
                //{
                //    btnViewVoucherDetail.Visible = false;
                //}

                if (lblStatus.Text == "PENDING" || string.IsNullOrEmpty(lblStatus.Text))
                {
                    btnViewVoucherDetail.Visible = false;
                }
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
                    Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                DisableZoomButton();

                ViewState["VoucherTypeid"] = null;
                ViewState["VoucherTypeidO"] = null;

                Label lblVoucherId = gvVouchersList.Rows[rowindex].FindControl("lblVoucherId") as Label;
                Label lblVoucherTypeId = gvVouchersList.Rows[rowindex].FindControl("lblVoucherTypeId") as Label;

                Label lblVoucherNo = gvVouchersList.Rows[rowindex].FindControl("lblVoucherNo") as Label;
                Label lblVoucherDate = gvVouchersList.Rows[rowindex].FindControl("lblVoucherDate") as Label;
                Label lblChallanNo = gvVouchersList.Rows[rowindex].FindControl("lblChallanNo") as Label;
                Label lblVoucherType = gvVouchersList.Rows[rowindex].FindControl("lblVoucherType") as Label;
                Label lblPartyCode = gvVouchersList.Rows[rowindex].FindControl("lblPartyCode") as Label;
                Label lblPartyName = gvVouchersList.Rows[rowindex].FindControl("lblPartyName") as Label;
                Label lblGLCode = gvVouchersList.Rows[rowindex].FindControl("lblGLCode") as Label;
                Label lblGLDescription = gvVouchersList.Rows[rowindex].FindControl("lblGLDescription") as Label;

                Label lblProductCode = gvVouchersList.Rows[rowindex].FindControl("lblProductCode") as Label;
                Label lblProductDescription = gvVouchersList.Rows[rowindex].FindControl("lblProductDescription") as Label;

                Label lblAmount = gvVouchersList.Rows[rowindex].FindControl("lblAmount") as Label;
                Label lblVoucherCreatedBy = gvVouchersList.Rows[rowindex].FindControl("lblVoucherCreatedBy") as Label;
                Label lblStatus = gvVouchersList.Rows[rowindex].FindControl("lblStatus") as Label;
                Label lblUnitName = gvVouchersList.Rows[rowindex].FindControl("lblUnitName") as Label;

                Label lblAuthorizedBy = gvVouchersList.Rows[rowindex].FindControl("lblAuthorizedBy") as Label;
                Label lblAuthorizedOn = gvVouchersList.Rows[rowindex].FindControl("lblAuthorizedOn") as Label;
                Label lblApprovedBy = gvVouchersList.Rows[rowindex].FindControl("lblApprovedBy") as Label;
                Label lblApprovedOn = gvVouchersList.Rows[rowindex].FindControl("lblApprovedOn") as Label;


                ViewState["VoucherTypeid"] = lblVoucherTypeId.Text;

                txtVoucherNoToAS.Text = lblVoucherNo.Text;
                txtVoucherDateToAS.Text = lblVoucherDate.Text;
                txtVoucherTypeToAS.Text = lblVoucherType.Text;
                txtPartyCodeToAS.Text = lblPartyCode.Text;
                txtPartyNameToAS.Text = lblPartyName.Text;
                txtGLCodeToAS.Text = lblGLCode.Text;
                txtGLDescriptionToAS.Text = lblGLDescription.Text;

                txtProductCodeToAS.Text = lblProductCode.Text;
                txtProductDescriptionToAS.Text = lblProductDescription.Text;

                txtAmountToAS.Text = lblAmount.Text;
                txtVoucherCreatedByToAS.Text = lblVoucherCreatedBy.Text;
                txtStatusToAS.Text = lblStatus.Text;
                txtUnitToAS.Text = lblUnitName.Text;

                txtAuthorizedByToAS.Text = lblAuthorizedBy.Text;
                txtAuthorizedOnToAS.Text = lblAuthorizedOn.Text;
                txtApprovedByToAS.Text = lblApprovedBy.Text;
                txtApprovedOnToAS.Text = lblApprovedOn.Text;

                //DisableVoucherDetailsGridPanels();

                //tdOtherVoucherFiles.Visible = false;

                //if (Convert.ToInt32(lblVoucherTypeId.Text) == (int)VoucherStatusTypes.EnumVoucherTypes.PV)
                //{
                //    tdOtherVoucherFiles.Visible = true;
                //}

                //string voucherType = lblVoucherType.Text.Trim().ToUpper();
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    //GetVouchersDetails(Convert.ToInt32(lblVoucherTypeId.Text), Convert.ToInt32(lblVoucherId.Text));
                    BindAttachedDocs(Convert.ToInt32(lblVoucherId.Text), Convert.ToInt32(lblVoucherTypeId.Text), lblVoucherType.Text);

                    mpeAuthorizeVoucher.Show();

                }

                else if (Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    ExportZip(Convert.ToInt32(lblVoucherId.Text), lblVoucherNo.Text.Trim().ToUpper()
                            , Convert.ToInt32(lblVoucherTypeId.Text), lblChallanNo.Text);
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

                int voucherTypeId = 0;
                if (ViewState["VoucherTypeid"] != null) voucherTypeId = Convert.ToInt32(ViewState["VoucherTypeid"]);

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedFileName.Text).Trim(), voucherTypeId);
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




    protected void gvAttachedOtherFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAttachedFileNameO = (Label)e.Row.FindControl("lblAttachedFileNameO");
            ImageButton imgBtnAttachment1O = (ImageButton)e.Row.FindControl("imgBtnAttachment1O");

            imgBtnAttachment1O.Visible = false;

            if (!string.IsNullOrEmpty(lblAttachedFileNameO.Text))
            {
                imgBtnAttachment1O.Visible = true;
                imgBtnAttachment1O.ToolTip = lblAttachedFileNameO.Text;

                string attachment1Extn = Convert.ToString(lblAttachedFileNameO.Text).Split('.').Last();
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
                    imgBtnAttachment1O.ImageUrl = "~/Images/imgicon1.png";
                    imgBtnAttachment1O.ToolTip = lblAttachedFileNameO.Text;
                }
                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                {
                    imgBtnAttachment1O.ImageUrl = "~/Images/pdficon1.png";
                    imgBtnAttachment1O.ToolTip = lblAttachedFileNameO.Text;
                }
                else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                {
                    imgBtnAttachment1O.ImageUrl = "~/Images/LOT/dxf.png";
                    imgBtnAttachment1O.ToolTip = lblAttachedFileNameO.Text;
                }
                else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                {
                    imgBtnAttachment1O.ImageUrl = "~/Images/LOT/dwg.png";
                    imgBtnAttachment1O.ToolTip = lblAttachedFileNameO.Text;
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvAttachedOtherFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1O")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPIDO = gvAttachedOtherFiles.Rows[rowindex].FindControl("lblPIDO") as Label;
                Label lblAttachedFileNameO = gvAttachedOtherFiles.Rows[rowindex].FindControl("lblAttachedFileNameO") as Label;

                int voucherTypeId = 0;
                if (ViewState["VoucherTypeidO"] != null) voucherTypeId = Convert.ToInt32(ViewState["VoucherTypeidO"]);

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1O")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblPIDO.Text), Convert.ToString(lblAttachedFileNameO.Text).Trim(), voucherTypeId);
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



    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    if (gvVouchersList.Rows.Count > 0)
    //    {
    //        DataTable dt = (DataTable)Session["VOUCHERS"];
    //        ToCSVNew01(dt, "FinanceAllEntriesDetailedReport_");
    //    }
    //    else
    //    {
    //        ExceptionMessage("No data found!");
    //    }
    //}


    protected void imgBtnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (gvVouchersList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["VOUCHERS"];
            ToCSVNew01(dt, "FinanceAllEntriesDetailedReport_");
        }
        else
        {
            ExceptionMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

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

    private void GetVouchersCreatedByList()
    {
        try
        {
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList(0);
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

    private void BindVouchersType()
    {
        try
        {
            _dsVouchersType = objVouchersAuthorization.GetVouchersType();
            if (_dsVouchersType.Tables.Count > 0 && _dsVouchersType.Tables[0].Rows.Count > 0)
            {
                ddlVoucherTypeToS.DataSource = _dsVouchersType.Tables[0];
                ddlVoucherTypeToS.DataTextField = "NAME";
                ddlVoucherTypeToS.DataValueField = "PID";
                ddlVoucherTypeToS.DataBind();
                ddlVoucherTypeToS.Items.Insert(0, "All");
                ddlVoucherTypeToS.SelectedIndex = 0;
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
            dsVouchersList = objVouchersAuthorization.GetFinanceAllEntriesDetailedReport
                (
                      DateSignS
                    , StartDateS
                    , EndDateS
                    , MonthS
                    , DocumentNumberS
                    , DocumentTypeIdS
                    , PartyNameS
                    , GLCodeS
                    , GLDescriptionS
                    , DOCClassS
                    , ProductCodeS
                    , ProductDescriptionS
                    , UnitIdS
                    , StatusIdS
                    , AmountSignS
                    , AmountOneS
                    , AmountTwoS
                    , CreatedByIdS
                );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
                Session["VOUCHERS"] = dsVouchersList.Tables[0];
                gvVouchersList.DataSource = dsVouchersList.Tables[0];
                gvVouchersList.DataBind();
            }
            else
            {
                Session["VOUCHERS"] = null;
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

    //private void GetVouchersDetails(int VoucherTypeId, int VoucherId)
    //{
    //    try
    //    {
    //        //ClearVoucherDetailsGrids();

    //        dsVouchersDetails = objVouchersAuthorization.GetFinanceAllEntriesReportVoucherDetails(VoucherTypeId, VoucherId);

    //        if (dsVouchersDetails.Tables.Count > 0 && dsVouchersDetails.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dtVoucherDetails = dsVouchersDetails.Tables[0];

    //            //EnableVoucherDetailsGridPanels(VoucherTypeId);
    //            //ViewVoucherDetailsGrids(VoucherTypeId, dtVoucherDetails);
    //        }


    //        //lblRecords.Text = "Records[" + dsVouchersDetails.Tables[0].Rows.Count + "]";
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}


    //private void BindAttachedDocs(int voucherId, int voucherTypeId)
    //{
    //    try
    //    {
    //        _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(voucherId, voucherTypeId, "");

    //        if (_dsAttachedDocs.Tables.Count > 0 && _dsAttachedDocs.Tables[0].Rows.Count > 0)
    //        {
    //            gvAttachedFiles.DataSource = _dsAttachedDocs.Tables[0];
    //            gvAttachedFiles.DataBind();
    //        }
    //        else
    //        {
    //            gvAttachedFiles.DataSource = null;
    //            gvAttachedFiles.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void BindAttachedDocs(int voucherId, int voucherTypeId, string voucherType)
    {
        try
        {
            tdOtherVoucherFiles.Visible = false;

            lblAttachedFilesLegend.Text = voucherType;
            if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.PV)
            {
                ViewState["VoucherTypeidO"] = (int)VoucherStatusTypes.EnumVoucherTypes.MRN;
                lblAttachedOtherFilesLegend.Text = "MRN Voucher";
            }

            _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(voucherId, voucherTypeId, "");


            gvAttachedFiles.DataSource = null;
            gvAttachedFiles.DataBind();
            gvAttachedOtherFiles.DataSource = null;
            gvAttachedOtherFiles.DataBind();

            if (_dsAttachedDocs.Tables.Count > 0)
            {
                if (_dsAttachedDocs.Tables[0].Rows.Count > 0)
                {
                    gvAttachedFiles.DataSource = _dsAttachedDocs.Tables[0];
                    gvAttachedFiles.DataBind();
                }

                if (_dsAttachedDocs.Tables.Count > 1 && _dsAttachedDocs.Tables[1].Rows.Count > 0)
                {
                    tdOtherVoucherFiles.Visible = true;
                    gvAttachedOtherFiles.DataSource = _dsAttachedDocs.Tables[1];
                    gvAttachedOtherFiles.DataBind();
                }

            }

            //if (_dsAttachedDocs.Tables.Count > 0 && _dsAttachedDocs.Tables[0].Rows.Count > 0)
            //{
            //    gvAttachedFiles.DataSource = _dsAttachedDocs.Tables[0];
            //    gvAttachedFiles.DataBind();
            //}
            //else
            //{
            //    gvAttachedFiles.DataSource = null;
            //    gvAttachedFiles.DataBind();
            //}
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindOtherAttachedDocs(int voucherId, int voucherTypeId)
    {
        try
        {
            _dsAttachedDocs = objVouchersAuthorization.GetVoucherDOCS(voucherId, voucherTypeId, "");

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

    //private void EnableVoucherDetailsGridPanels(int voucherTypeId)
    //{
    //    if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.JV) pnlGVJV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.PV) pnlGVPV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.MRN) pnlGVMRN.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SV) pnlGVSV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SOV) pnlGVSOV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SIV) pnlGVSIV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VRPV) pnlGVVRPV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CRPV) pnlGVCRPV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CBV) pnlGVCBV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CV) pnlGVCV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.MR) pnlGVMR.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.POV) pnlGVPOV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.IIV) pnlGVIIV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.IRV) pnlGVIRV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VDNV) pnlGVVDNV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VCNV) pnlGVVCNV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CDNV) pnlGVCDNV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CCNV) pnlGVCCNV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.STV) pnlGVSTV.Visible = true;
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SAV) pnlGVSAV.Visible = true;

    //}

    //private void DisableVoucherDetailsGridPanels()
    //{
    //    pnlGVJV.Visible = false;
    //    pnlGVPV.Visible = false;
    //    pnlGVMRN.Visible = false;
    //    pnlGVSV.Visible = false;
    //    pnlGVSOV.Visible = false;
    //    pnlGVSIV.Visible = false;
    //    pnlGVVRPV.Visible = false;
    //    pnlGVCRPV.Visible = false;
    //    pnlGVCBV.Visible = false;
    //    pnlGVCV.Visible = false;
    //    pnlGVMR.Visible = false;
    //    pnlGVPOV.Visible = false;
    //    pnlGVIIV.Visible = false;
    //    pnlGVIRV.Visible = false;
    //    pnlGVVDNV.Visible = false;
    //    pnlGVVCNV.Visible = false;
    //    pnlGVCDNV.Visible = false;
    //    pnlGVCCNV.Visible = false;
    //    pnlGVSTV.Visible = false;
    //    pnlGVSAV.Visible = false;
    //}


    //private int GetVoucherTypeId(string voucherType)
    //{
    //    int voucherTypeId = 0;

    //    if (voucherType == Constants.JOURNAL_VOUCHER_JV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.JV;
    //    else if (voucherType == Constants.PURCHASE_VOUCHER_PV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.PV;
    //    else if (voucherType == Constants.MRN_MRN) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.MRN;
    //    else if (voucherType == Constants.SERVICE_VOUCHER_SV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.SV;
    //    else if (voucherType == Constants.SALE_ORDER_VOUCHER_SOV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.SOV;
    //    else if (voucherType == Constants.SALE_INVOICE_VOUCHER_SIV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.SIV;
    //    else if (voucherType == Constants.VENDOR_RECEIPT_PAYMENT_VOUCHER_VRPV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.VRPV;
    //    else if (voucherType == Constants.CUSTOMER_RECEIPT_PAYMENT_VOUCHER_CRPV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.CRPV;
    //    else if (voucherType == Constants.CASH_BANK_VOUCHER_CBV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.CBV;
    //    else if (voucherType == Constants.CONTRA_VOUCHERS_CV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.CV;
    //    else if (voucherType == Constants.MR_MR) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.MR;
    //    else if (voucherType == Constants.PURCHASE_ORDER_VOUCHER_POV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.POV;
    //    else if (voucherType == Constants.INVENTORY_ISSUE_VOUCHER_IIV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.IIV;
    //    else if (voucherType == Constants.INVENTORY_RETURN_VOUCHER_IRV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.IRV;
    //    else if (voucherType == Constants.VENDOR_DEBIT_NOTE_VOUCHER_VDNV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.VDNV;
    //    else if (voucherType == Constants.VENDOR_CREDIT_NOTE_VOUCHER_VCNV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.VCNV;
    //    else if (voucherType == Constants.CUSTOMER_DEBIT_NOTE_VOUCHER_CDNV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.CDNV;
    //    else if (voucherType == Constants.CUSTOMER_CREDIT_NOTE_VOUCHER_CCNV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.CCNV;
    //    else if (voucherType == Constants.STOCK_TRANSFER_VOUCHER_STV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.STV;
    //    else if (voucherType == Constants.STOCK_ADJUSTMENT_VOUCHER_SAV) voucherTypeId = (int)VoucherStatusTypes.EnumVoucherTypes.SAV;

    //    return voucherTypeId;
    //}

    //private void ViewVoucherDetailsGrids(int voucherTypeId, DataTable dtVoucherDetails)
    //{

    //    if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.JV)
    //    {
    //        pnlGVJV.Visible = true;
    //        gvJVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvJVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.PV)
    //    {
    //        pnlGVPV.Visible = true;
    //        gvPVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvPVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.MRN)
    //    {
    //        pnlGVMRN.Visible = true;
    //        gvMRNVoucherDetails.DataSource = dtVoucherDetails;
    //        gvMRNVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SV)
    //    {
    //        pnlGVSV.Visible = true;
    //        gvSVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvSVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SOV)
    //    {
    //        pnlGVSOV.Visible = true;
    //        gvSOVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvSOVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SIV)
    //    {
    //        pnlGVSIV.Visible = true;
    //        gvSIVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvSIVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VRPV)
    //    {
    //        pnlGVVRPV.Visible = true;
    //        gvVRPVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvVRPVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CRPV)
    //    {
    //        pnlGVCRPV.Visible = true;
    //        gvCRPVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvCRPVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CBV)
    //    {
    //        pnlGVCBV.Visible = true;
    //        gvCBVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvCBVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CV)
    //    {
    //        pnlGVCV.Visible = true;
    //        gvCVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvCVVoucherDetails.DataBind();
    //    }

    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.MR)
    //    {
    //        pnlGVMR.Visible = true;
    //        gvMRVoucherDetails.DataSource = dtVoucherDetails;
    //        gvMRVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.POV)
    //    {
    //        pnlGVPOV.Visible = true;
    //        gvPOVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvPOVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.IIV)
    //    {
    //        pnlGVIIV.Visible = true;
    //        gvIIVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvIIVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.IRV)
    //    {
    //        pnlGVIRV.Visible = true;
    //        gvIRVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvIRVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VDNV)
    //    {
    //        pnlGVVDNV.Visible = true;
    //        gvVDNVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvVDNVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.VCNV)
    //    {
    //        pnlGVVCNV.Visible = true;
    //        gvVCNVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvVCNVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CDNV)
    //    {
    //        pnlGVCDNV.Visible = true;
    //        gvCDNVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvCDNVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.CCNV)
    //    {
    //        pnlGVCCNV.Visible = true;
    //        gvCCNVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvCCNVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.STV)
    //    {
    //        pnlGVSTV.Visible = true;
    //        gvSTVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvSTVVoucherDetails.DataBind();
    //    }
    //    else if (voucherTypeId == (int)VoucherStatusTypes.EnumVoucherTypes.SAV)
    //    {
    //        pnlGVSAV.Visible = true;
    //        gvSAVVoucherDetails.DataSource = dtVoucherDetails;
    //        gvSAVVoucherDetails.DataBind();
    //    }
    //}

    //private void ClearVoucherDetailsGrids()
    //{
    //    gvJVVoucherDetails.DataSource = null;
    //    gvJVVoucherDetails.DataBind();

    //    gvPVVoucherDetails.DataSource = null;
    //    gvPVVoucherDetails.DataBind();

    //    gvMRNVoucherDetails.DataSource = null;
    //    gvMRNVoucherDetails.DataBind();

    //    gvSVVoucherDetails.DataSource = null;
    //    gvSVVoucherDetails.DataBind();

    //    gvSOVVoucherDetails.DataSource = null;
    //    gvSOVVoucherDetails.DataBind();

    //    gvSIVVoucherDetails.DataSource = null;
    //    gvSIVVoucherDetails.DataBind();

    //    gvVRPVVoucherDetails.DataSource = null;
    //    gvVRPVVoucherDetails.DataBind();

    //    gvCRPVVoucherDetails.DataSource = null;
    //    gvCRPVVoucherDetails.DataBind();

    //    gvCBVVoucherDetails.DataSource = null;
    //    gvCBVVoucherDetails.DataBind();

    //    gvCVVoucherDetails.DataSource = null;
    //    gvCVVoucherDetails.DataBind();

    //    gvMRVoucherDetails.DataSource = null;
    //    gvMRVoucherDetails.DataBind();

    //    gvPOVVoucherDetails.DataSource = null;
    //    gvPOVVoucherDetails.DataBind();

    //    gvIIVVoucherDetails.DataSource = null;
    //    gvIIVVoucherDetails.DataBind();

    //    gvIRVVoucherDetails.DataSource = null;
    //    gvIRVVoucherDetails.DataBind();

    //    gvVDNVVoucherDetails.DataSource = null;
    //    gvVDNVVoucherDetails.DataBind();

    //    gvVCNVVoucherDetails.DataSource = null;
    //    gvVCNVVoucherDetails.DataBind();

    //    gvCDNVVoucherDetails.DataSource = null;
    //    gvCDNVVoucherDetails.DataBind();

    //    gvCCNVVoucherDetails.DataSource = null;
    //    gvCCNVVoucherDetails.DataBind();

    //    gvSTVVoucherDetails.DataSource = null;
    //    gvSTVVoucherDetails.DataBind();

    //    gvSAVVoucherDetails.DataSource = null;
    //    gvSAVVoucherDetails.DataBind();
    //}

    private void ViewAttachedFiles(int docID, string fileName, int voucherTypeId)
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
                    iframeViewPDFFile.Attributes.Add("src", "ViewAttachedPDFFile.aspx?docID=" + docID + "&typeID=" + voucherTypeId);// + "&fileType=" + fileType) ;
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

    private void ToCSVNew01(DataTable dt, string fileNameS)
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

            string fileName = fileNameS + DateTime.Now.ToString("dd_MMM_yyyy");
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



    protected void ExportZip(int Pid, string voucherNo, int voucherTypeId, string challanNo)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;

            DataSet dsAttachedFiles = objVouchersAuthorization.GetVoucherDOCS(Pid, voucherTypeId, challanNo);

            int count = 0;

            if (dsAttachedFiles.Tables.Count > 0)
            {
                if (dsAttachedFiles.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
                    {
                        count++;
                        string name = count + Convert.ToString(row["FILE_NAME"]);
                        byte[] bytes = (byte[])row["FILE_BYTES"];
                        zip.AddEntry(name, bytes);
                    }
                }


                if (dsAttachedFiles.Tables.Count > 1 && dsAttachedFiles.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in dsAttachedFiles.Tables[1].Rows)
                    {
                        count++;
                        string name = count + Convert.ToString(row["FILE_NAME"]);
                        byte[] bytes = (byte[])row["FILE_BYTES"];
                        zip.AddEntry(name, bytes);
                    }
                }

                if (count > 0)
                {
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
            else
            {
                ExceptionMessage("No files found to download!");
                return;
            }


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
