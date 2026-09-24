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

public partial class PO_ViewPoDocuments : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsPoDetails = new DataSet();
    DataSet _dsAttachedPoDocs = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsAttachedBy = new DataSet();
    DataSet _dsCreatedBy = new DataSet();
    int _unitId = 0;

    string _dateType = "";
    string _startDate = "";
    string _endDate = "";

    string _vendorCode = "";
    string _vendorName = "";

    int _docAttachedFilterId = 0;
    string _poNumber = "";
    string _jobNumber = "";
    string _mrNumber = "";

    int _attachedById = 0;
    string _CheckedBy = "";

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

    public string DateType
    {
        get
        {
            _dateType = ddlDateType.SelectedValue.ToString();
            return _dateType;
        }
    }

    public int UnitId
    {
        get
        {
            if (ddlUnit.SelectedIndex > 0)
                _unitId = Convert.ToInt32(ddlUnit.SelectedValue);
            else _unitId = 0;

            return _unitId;
        }
    }

    public int DocumentAttachedFilterId
    {
        get
        {
            if (ddlDocumentAttached.SelectedIndex > 0)
                _docAttachedFilterId= Convert.ToInt32(ddlDocumentAttached.SelectedValue);
            else _docAttachedFilterId = 0;

            return _docAttachedFilterId;
        }
    }

    public string VendorCode
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVendorCode.Text))
                _vendorCode = Convert.ToString(txtVendorCode.Text);
            else _vendorCode = "";

            return _vendorCode;
        }
    }

    public string VendorName
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVendorName.Text))
                _vendorName = Convert.ToString(txtVendorName.Text);
            else _vendorName = "";

            return _vendorName;
        }
    }

    public string PoNumber
    {
        //get
        //{
        //    if (!string.IsNullOrEmpty(txtPoNumber.Text))
        //        _poNumber = Convert.ToString(txtPoNumber.Text);
        //    else _poNumber = "";

        //    return _poNumber;
        //}

        get 
        {
            if (!string.IsNullOrEmpty(txtPoNumber.Text))
            {
                // Replace newlines with spaces and split by comma
                string[] poNoTxt = txtPoNumber.Text.Replace(Environment.NewLine, " ").Split(',');

                // Use StringBuilder for better performance
                StringBuilder poNoBuilder = new StringBuilder();

                foreach (string item in poNoTxt)
                {
                    // Trim spaces around each item and check if not empty
                    string trimmedItem = item.Trim();
                    if (!string.IsNullOrEmpty(trimmedItem))
                    {
                        poNoBuilder.Append("'").Append(trimmedItem).Append("',");
                    }
                }

                // Remove the trailing comma if there's any item appended
                if (poNoBuilder.Length > 0)
                {
                    poNoBuilder.Length--; // This removes the last comma
                }

                _poNumber = poNoBuilder.ToString();
                 }
            else
                {
                _poNumber = string.Empty;
                }
            return _poNumber;
        }



    }

    public string JobNumber
    {
        get
        {
            if (!string.IsNullOrEmpty(txtJobNumber.Text))
                _jobNumber = Convert.ToString(txtJobNumber.Text);
            else _jobNumber = "";

            return _jobNumber;
        }
    }

    public string MRNumber
    {
        get
        {
            if (!string.IsNullOrEmpty(txtMRNumber.Text))
                _mrNumber = Convert.ToString(txtMRNumber.Text);
            else _mrNumber = "";

            return _mrNumber;
        }
    }

    public int AttachedById
    {
        get
        {
            if (ddlAttachedBy.SelectedIndex > 0)
                _attachedById = Convert.ToInt32(ddlAttachedBy.SelectedValue);
            else _attachedById = 0;

            return _attachedById;
        }
    }

    public string CheckedBy
    {
        get
        {
            if (ddlCheckedBy.SelectedIndex > 0)
                _CheckedBy = Convert.ToString(ddlCheckedBy.SelectedValue);
            else _CheckedBy = "";

            return _CheckedBy;
        }
    }
    public int PidToI
    {
        get
        {
            if (ViewState["PID"] != null)
                _pidToI = Convert.ToInt32(ViewState["PID"]);
            else _pidToI = 0;

            return _pidToI;
        }

        set
        {
            _pidToI = value;
        }
    }

    public int UnitFidToI
    {
        get
        {
            if (ViewState["UNIT_FID"] != null)
                _unitFidToI = Convert.ToInt32(ViewState["UNIT_FID"]);
            else _unitFidToI = 0;

            return _unitFidToI;
        }

        set
        {
            _unitFidToI = value;
        }
    }

    public string PoNoToI
    {
        get
        {
            _poNoToI = txtPoNoToS.Text.Trim().ToUpper();
            return _poNoToI;
        }

        set
        {
            _poNoToI = value;
        }
    }

    public string PoDateToI
    {
        get
        {
            _poDateToI = Convert.ToDateTime(txtPoDateToS.Text).ToString("yyyy-MM-dd");
            return _poDateToI;
        }

        set
        {
            _poDateToI = value;
        }
    }

    public string VendorCodeToI
    {
        get
        {
            _vendorCodeToI = txtVendorCodeToS.Text.Trim().ToUpper();
            return _vendorCodeToI;
        }

        set
        {
            _vendorCodeToI = value;
        }
    }

    public string JobNoToI
    {
        get
        {
            _jobNoToI = txtJobNoToS.Text.Trim().ToUpper();
            return _jobNoToI;
        }

        set
        {
            _jobNoToI = value;
        }
    }



    public int CreatedByToI
    {
        get
        {
            _createdByToI = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            return _createdByToI;
        }

        set
        {
            _createdByToI = value;
        }
    }

    int _pidToI;
    int _unitFidToI;
    string _poNoToI;
    string _poDateToI;
    string _vendorCodeToI;
    string _jobNoToI;
    string _attachment1NameToI;
    Byte[] _attachment1DocToI = null;
    string _attachment1RemarksToI;
    int _createdByToI;



    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["MRNReport"] = null;
                Session["PO_Details"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                BindUnit();
                BindAttachedBy();
                BindCreatedBy();
                //ddlDateType.SelectedIndex = 1;

                GetPoList();
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
        GetPoList();
    }

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string attachment1Extn = string.Empty;
            Label lblAttachment1 = (Label)e.Row.FindControl("lblAttachment1");
            ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");

            HtmlTable tblViewDetail = (HtmlTable)e.Row.FindControl("tblViewDetail");
            ImageButton imgBtnDownloadAllDocuments = (ImageButton)e.Row.FindControl("imgBtnDownloadAllDocuments");


            imgBtnAttachment1.Visible = false;

            tblViewDetail.Visible = false;
            imgBtnDownloadAllDocuments.Visible = false;


            if (!string.IsNullOrEmpty(lblAttachment1.Text))
            {
                imgBtnAttachment1.Visible = true;
                imgBtnAttachment1.ToolTip = lblAttachment1.Text;

                tblViewDetail.Visible = true;
                imgBtnDownloadAllDocuments.Visible = true;

                string attachment1Extn = Convert.ToString(lblAttachment1.Text).Split('.').Last();
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
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                }
                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                }
                else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                }
                else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
                    imgBtnAttachment1.ToolTip = lblAttachment1.Text;
                }
            }


            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvPOList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "VIEW_PRODUCT_LIST" ||
                    Convert.ToString(e.CommandArgument) == "ViewDETAIL" ||
                    Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1" ||
                    Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvPOList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblPoFID = gvPOList.Rows[rowindex].FindControl("lblPoFID") as Label;
                Label lblDocumentPID = gvPOList.Rows[rowindex].FindControl("lblDocumentPID") as Label;
                Label lblUnitID = gvPOList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblUnitName = gvPOList.Rows[rowindex].FindControl("lblUnitName") as Label;
                Label lblPoNo = gvPOList.Rows[rowindex].FindControl("lblPoNo") as Label;
                Label lblPoDate = gvPOList.Rows[rowindex].FindControl("lblPoDate") as Label;
                Label lblJobNo = gvPOList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblVendorCode = gvPOList.Rows[rowindex].FindControl("lblVendorCode") as Label;
                Label lblVendorName = gvPOList.Rows[rowindex].FindControl("lblVendorName") as Label;
                Label lblAttachment1 = gvPOList.Rows[rowindex].FindControl("lblAttachment1") as Label;


                ViewState["PID"] = lblPID.Text;
                ViewState["UNIT_FID"] = lblUnitID.Text;

                Session["MRNReport"] = null;
                if (Convert.ToString(e.CommandArgument) == "ViewDETAIL")
                {
                    txtUnitToS.Text = lblUnitName.Text;
                    txtPoNoToS.Text = lblPoNo.Text;
                    txtPoDateToS.Text = lblPoDate.Text;
                    txtJobNoToS.Text = lblJobNo.Text;
                    txtVendorCodeToS.Text = lblVendorCode.Text;
                    txtVendorNameToS.Text = lblVendorName.Text;

                    BindAttachedPOList(Convert.ToInt32(lblPID.Text));

                    mpePoDetail.Show();

                }

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    ViewAttachedFiles(Convert.ToInt32(lblDocumentPID.Text), Convert.ToString(lblAttachment1.Text).Trim());
                }
                else if (Convert.ToString(e.CommandArgument) == "DOWNLOAD_ALL_DOCUMENTS")
                {
                    ExportZip(Convert.ToInt32(lblPID.Text));
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

    protected void gvAttachedPoList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblIsDefault = e.Row.FindControl("lblIsDefault") as Label;
            CheckBox chkIsDefault = e.Row.FindControl("chkIsDefault") as CheckBox;

            chkIsDefault.Checked = false;
            if (Convert.ToInt32(lblIsDefault.Text) > 0)
            {
                chkIsDefault.Checked = true;
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                }
            }
            else
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Transparent;
                }
            }


            Label lblAttachedAttachment1 = (Label)e.Row.FindControl("lblAttachedAttachment1");
            ImageButton imgBtnAttachment1 = (ImageButton)e.Row.FindControl("imgBtnAttachment1");

            imgBtnAttachment1.Visible = false;

            if (!string.IsNullOrEmpty(lblAttachedAttachment1.Text))
            {
                imgBtnAttachment1.Visible = true;
                imgBtnAttachment1.ToolTip = lblAttachedAttachment1.Text;

                string attachment1Extn = Convert.ToString(lblAttachedAttachment1.Text).Split('.').Last();
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
                    imgBtnAttachment1.ToolTip = lblAttachedAttachment1.Text;
                }
                else if (attachment1Extn == "pdf" || attachment1Extn == "PDF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/pdficon1.png";
                    imgBtnAttachment1.ToolTip = lblAttachedAttachment1.Text;
                }
                else if (attachment1Extn == "dxf" || attachment1Extn == "DXF")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dxf.png";
                    imgBtnAttachment1.ToolTip = lblAttachedAttachment1.Text;
                }
                else if (attachment1Extn == "dwg" || attachment1Extn == "DWG")
                {
                    imgBtnAttachment1.ImageUrl = "~/Images/LOT/dwg.png";
                    imgBtnAttachment1.ToolTip = lblAttachedAttachment1.Text;
                }
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void gvAttachedPoList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblPID = gvAttachedPoList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblAttachedAttachment1 = gvAttachedPoList.Rows[rowindex].FindControl("lblAttachedAttachment1") as Label;


                ViewState["PID"] = lblPID.Text;

                if (Convert.ToString(e.CommandArgument) == "ViewATTACHMENT1")
                {
                    //ViewDrawingFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedAttachment1.Text).Trim());
                    ViewAttachedFiles(Convert.ToInt32(lblPID.Text), Convert.ToString(lblAttachedAttachment1.Text).Trim());
                    mpePoDetail.Show();
                }

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    RemovePoDocuments(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(lblPID.Text));
                    GetPoList();
                    //mpePoDetail.Show();
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

    #endregion


    #region METHODS[=======================]

    private void BindUnit()
    {
        try
        {
            _dsUnit = objCommon.GetUnit();
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

    private void BindAttachedBy()
    {
        try
        {
            _dsAttachedBy = objReports.GetAttachedByList();
            if (_dsAttachedBy.Tables.Count > 0 && _dsAttachedBy.Tables[0].Rows.Count > 0)
            {
                ddlAttachedBy.DataSource = _dsAttachedBy.Tables[0];
                ddlAttachedBy.DataTextField = "ATTACHMENT_BY_NAME";
                ddlAttachedBy.DataValueField = "ATTACHMENT_BY_ID";
                ddlAttachedBy.DataBind();
                ddlAttachedBy.Items.Insert(0, "All");
                ddlAttachedBy.SelectedIndex = 0;
            }
            else
            {
                ddlAttachedBy.Items.Insert(0, "All");
                ddlAttachedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCreatedBy()
    {
        try
        {
            _dsCreatedBy = objReports.GetCreatedByList();
            if (_dsCreatedBy.Tables.Count > 0 && _dsCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlCheckedBy.DataSource = _dsCreatedBy.Tables[0];
                ddlCheckedBy.DataTextField = "CREATED_BY";
                ddlCheckedBy.DataValueField = "CREATED_BY";
                ddlCheckedBy.DataBind();
                ddlCheckedBy.Items.Insert(0, "All");
                ddlCheckedBy.SelectedIndex = 0;
            }
            else
            {
                ddlCheckedBy.Items.Insert(0, "All");
                ddlCheckedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetPoList()
    {
        try
        {
            dsPoDetails = objReports.GetPoDocumentsList
                (
                      UnitId
                    , DateType
                    , StartDate
                    , EndDate
                    , VendorCode
                    , VendorName
                    , PoNumber
                    , JobNumber
                    , MRNumber
                    , AttachedById
                    , CheckedBy
                );

            DataTable dt = dsPoDetails.Tables[0];
            DataView dv = new DataView(dsPoDetails.Tables[0]);

            if(DocumentAttachedFilterId == 1)
            {
                dv.RowFilter = "DOCUMENTS_COUNT IS NOT NULL AND DOCUMENTS_COUNT > 0";
            }
            else if(DocumentAttachedFilterId == 2)
            {
                dv.RowFilter = "DOCUMENTS_COUNT IS NULL OR DOCUMENTS_COUNT = 0";
            }


            if (dsPoDetails.Tables.Count > 0 && dsPoDetails.Tables[0].Rows.Count > 0)
            {
                Session["PO_Details"] = dsPoDetails.Tables[0];
                //gvPOList.DataSource = dsPoDetails.Tables[0];
                //gvPOList.DataBind();
            }
            else
            {
                Session["PO_Details"] = null;
                //gvPOList.DataSource = null;
                //gvPOList.DataBind();
            }
            if (dv.Count > 0)
            {
                gvPOList.DataSource = dv;
                gvPOList.DataBind();
            }
            else
            {
                gvPOList.DataSource = null;
                gvPOList.DataBind();
            }

            //lblRecords.Text = "Records[" + dsPoDetails.Tables[0].Rows.Count + "]";
            lblRecords.Text = "Records[" + dv.Count + "]";
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


    private void BindAttachedPOList(int PId)
    {
        try
        {
            _dsAttachedPoDocs = objReports.GetAttachedPoDocumentsList(PId);

            if (_dsAttachedPoDocs.Tables.Count > 0 && _dsAttachedPoDocs.Tables[0].Rows.Count > 0)
            {
                Session["_dtAttachedPoDocs"] = _dsAttachedPoDocs.Tables[0];
                gvAttachedPoList.DataSource = _dsAttachedPoDocs.Tables[0];
                gvAttachedPoList.DataBind();
            }
            else
            {
                Session["_dtAttachedPoDocs"] = null;
                gvAttachedPoList.DataSource = null;
                gvAttachedPoList.DataBind();
            }

            lblMRNDetailRecors.Text = "PO Documents Records[" + _dsAttachedPoDocs.Tables[0].Rows.Count + "]";
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

    private void ViewDrawingFiles(int Pid, string fileName)
    {
        try
        {
            string extn = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {

                ExportDWGFile(Pid);
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

    private void ExportDWGFile(int Pid)
    {
        try
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            DataSet dsFiles = new DataSet();
            dsFiles = objReports.GetAttachedPoDocumentFile(Pid);

            if (dsFiles.Tables.Count > 0 && dsFiles.Tables[0].Rows.Count > 0)
            {
                bytes = (byte[])dsFiles.Tables[0].Rows[0]["ATTACHMENT1_DOC"];
                fileName = Convert.ToString(dsFiles.Tables[0].Rows[0]["ATTACHMENT1_NAME"]);

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
                ExceptionMessage("The process of downloading is too longer, please try again...!!!");
                //mpeAddSubitems.Show();
                return;
            }
            else
            {
                throw;
            }
        }
    }


    private void RemovePoDocuments(int Pid, int DocId)
    {
        int val = objReports.RemovePoDocuments
            (
                  Pid
                , DocId
                , CreatedByToI
            );

        if (val > 0)
        {
            mpePoDetail.Show();
            BindAttachedPOList(Pid);
            GetPoList();
        }
    }

    
    protected void ExportZip(int Pid)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;

            DataSet dsAttachedFiles = objReports.GetAttachedPoDocumentFiles(Pid);

            int count = 0;
            foreach (DataRow row in dsAttachedFiles.Tables[0].Rows)
            {
                count++;

                string name = count + Convert.ToString(row["ATTACHMENT1_NAME"]);
                byte[] bytes = (byte[])row["ATTACHMENT1_DOC"];
                zip.AddEntry(name, bytes);
            }


            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
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
