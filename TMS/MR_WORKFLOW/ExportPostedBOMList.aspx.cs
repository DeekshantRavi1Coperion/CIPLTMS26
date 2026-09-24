using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;
using System.Text;
using Ionic.Zip;

public partial class MR_WORKFLOW_ExportPostedBOMList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();
    BAL.PROJECT_MANAGEMENT.ProjectManagement objProjM = new BAL.PROJECT_MANAGEMENT.ProjectManagement();

    DataSet _dsStatus = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsBomType = new DataSet();
    DataSet _dsBomMrCreatedBy = new DataSet();
    DataSet _dsBomResponsibleFor = new DataSet();
    DataSet _dsBomPivotGroups = new DataSet();

    DataSet _dsPostedBomList = new DataSet();
    DataSet _dsBomList = new DataSet();

    DataSet _dsBomLineToU = new DataSet();
    DataTable _dtBomLineSelectedToU = new DataTable();

    DataSet _dsPostedBomLineToU = new DataSet();

    int _currentUserId = 0;
    DateTime _currentDate;


    string _dateType = "";
    string _startDate = "";
    string _endDate = "";

    string _mrNo = "";
    int _mrCreatedById = 0;
    string _bomNo = "";
    string _jobNo = "";

    int _unitId = 0;
    int _typeId = 0;
    string _statusId = "0";
    int _pivotGroupId = 0;
    int _responsibleForId = 0;
    int _isTcRequired = 0;


    #endregion


    #region PROPERTIES[====================]

    public int CurrentUserId
    {
        get
        {
            _currentUserId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            return _currentUserId;
        }
    }

    public DateTime CurrentDate
    {
        get
        {
            _currentDate = DateTime.Now;

            return _currentDate;
        }
    }

    public int UnitIdToS
    {
        get
        {
            if (ddlUnit.SelectedIndex > 0)
                _unitId = Convert.ToInt32(ddlUnit.SelectedValue);
            else _unitId = 0;

            return _unitId;
        }

        set
        {
            _unitId = value;
        }
    }

    public string StartDateToS
    {
        get
        {
            if (chkSelectDates.Checked)
                _startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else _startDate = "";

            return _startDate;
        }

        set
        {
            _startDate = value;
        }
    }

    public string EndDateToS
    {
        get
        {
            if (chkSelectDates.Checked)
                _endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else _endDate = "";

            return _endDate;
        }

        set
        {
            _endDate = value;
        }
    }

    public string DateTypeToS
    {
        get
        {
            _dateType = ddlDateType.SelectedValue.ToString();
            return _dateType;
        }

        set
        {
            _dateType = value;
        }
    }


    public int TypeIdToS
    {
        get
        {
            if (ddlType.SelectedIndex > 0)
                _typeId = Convert.ToInt32(ddlType.SelectedValue);
            else _typeId = 0;

            return _typeId;
        }

        set
        {
            _typeId = value;
        }
    }

    public string StatusIdToS
    {
        get
        {
            if (ddlStatus.SelectedIndex > 0)
                _statusId = Convert.ToString(ddlStatus.SelectedValue);
            else _statusId = "0";

            return _statusId;
        }

        set
        {
            _statusId = value;
        }
    }

    public int PivotGroupIdToS
    {
        get
        {
            if (ddlPivotGroup.SelectedIndex > 0)
                _pivotGroupId = Convert.ToInt32(ddlPivotGroup.SelectedValue);
            else _pivotGroupId = 0;

            return _pivotGroupId;
        }

        set
        {
            _pivotGroupId = value;
        }
    }

    public int ResponsibleForIdToS
    {
        get
        {
            if (ddlResponsibleFor.SelectedIndex > 0)
                _responsibleForId = Convert.ToInt32(ddlResponsibleFor.SelectedValue);
            else _responsibleForId = 0;

            return _responsibleForId;
        }

        set
        {
            _responsibleForId = value;
        }
    }

    public int IsTcRequiredToS
    {
        get
        {
            if (ddlIsTCRequired.SelectedIndex > 0)
                _isTcRequired = Convert.ToInt32(ddlIsTCRequired.SelectedValue);
            else _isTcRequired = 0;

            return _isTcRequired;
        }

        set
        {
            _isTcRequired = value;
        }
    }

    public string MrNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtMrNo.Text))
                _mrNo = Convert.ToString(txtMrNo.Text);
            else _mrNo = "";

            return _mrNo;
        }

        set
        {
            _mrNo = value;
        }
    }

    public int MrCreatedByIdToS
    {
        get
        {
            if (ddlMrCreatedBy.SelectedIndex > 0)
                _mrCreatedById = Convert.ToInt32(ddlMrCreatedBy.SelectedValue);
            else _mrCreatedById = 0;

            return _mrCreatedById;
        }

        set
        {
            _mrCreatedById = value;
        }
    }

    public string BomNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtBOMNo.Text))
                _bomNo = Convert.ToString(txtBOMNo.Text);
            else _bomNo = "";

            return _bomNo;
        }

        set
        {
            _bomNo = value;
        }
    }

    public string JobNoToS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
                _jobNo = Convert.ToString(txtJobNo.Text);
            else _jobNo = "";

            return _jobNo;
        }

        set
        {
            _jobNo = value;
        }
    }



    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {

                ClearSessions();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                GetUnit();
                GetBomType();
                GetBomResponsibleFor();
                GetPivotGroups();

                BindStatus();
                BindUnitToS();
                BindBomTypeToS();
                BindBomMrCreatedByToS();
                BindBomResponsibleForToS();
                BindPivotGroupsToS();



                ddlStatus.SelectedValue = Convert.ToString((int)BOMAllTypeEnums.EnumStatus.Approved);
                ddlStatus.Enabled = false;


                GetPostedBOMList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetPostedBOMList();
    }

    protected void gvPurchaseIndentHeader_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    protected void gvPurchaseIndentUdf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    protected void gvPurchaseIndentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }


    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    if (gvPurchaseIndentHeader.Rows.Count > 0)
    //    {

    //        DataTable dtPurchaseIndentHeader = new DataTable();
    //        DataTable dtPurchaseIndentUdf = new DataTable();
    //        DataTable dtPurchaseIndentDetail = new DataTable();

    //        if (Session["dtPurchaseIndentHeader"] != null)
    //            dtPurchaseIndentHeader = (DataTable)Session["dtPurchaseIndentHeader"];

    //        if (Session["dtPurchaseIndentUdf"] != null)
    //            dtPurchaseIndentUdf = (DataTable)Session["dtPurchaseIndentUdf"];

    //        if (Session["dtPurchaseIndentDetail"] != null)
    //            dtPurchaseIndentDetail = (DataTable)Session["dtPurchaseIndentDetail"];


    //        //DataTable dtH = (DataTable)Session["PostedBomList"];

    //        //DataTable dtPurchaseIndentDetail = new DataTable();
    //        //dtPurchaseIndentDetail.TableName = "PURCHASE_INDENT_DETAIL";

    //        //dtPurchaseIndentDetail.Columns.AddRange(new DataColumn[3] {
    //        //                                        new DataColumn("Id", typeof(int)),
    //        //                                        new DataColumn("Name", typeof(string)),
    //        //                                        new DataColumn("Country",typeof(string)) });

    //        //dtPurchaseIndentDetail.Rows.Add(1, "John Hammond", "United States");
    //        //dtPurchaseIndentDetail.Rows.Add(2, "Mudassar Khan", "India");
    //        //dtPurchaseIndentDetail.Rows.Add(3, "Suzanne Mathews", "France");
    //        //dtPurchaseIndentDetail.Rows.Add(4, "Robert Schidner", "Russia");

    //        //DataTable dtPurchaseIndentHeader = new DataTable();
    //        //dtPurchaseIndentHeader.TableName = "PURCHASE_INDENT_HEADER";
    //        //dtPurchaseIndentHeader.Columns.AddRange(new DataColumn[3] {
    //        //                                        new DataColumn("Id", typeof(int)),
    //        //                                        new DataColumn("Name", typeof(string)),
    //        //                                        new DataColumn("Country",typeof(string)) });

    //        //dtPurchaseIndentHeader.Rows.Add(1, "Maria", "Austria");
    //        //dtPurchaseIndentHeader.Rows.Add(2, "Thomas Hardy", "Ireland");
    //        //dtPurchaseIndentHeader.Rows.Add(3, "Laurence Lebihan", "USA");
    //        //dtPurchaseIndentHeader.Rows.Add(4, "Victoria Ashworth", "UK");

    //        //DataTable dtPurchaseIndentUdf = new DataTable();
    //        //dtPurchaseIndentUdf.TableName = "PURCHASE_INDENT_UDF";
    //        //dtPurchaseIndentUdf.Columns.AddRange(new DataColumn[3] {
    //        //                                     new DataColumn("Id", typeof(int)),
    //        //                                     new DataColumn("Name", typeof(string)),
    //        //                                     new DataColumn("Country",typeof(string)) });

    //        //dtPurchaseIndentUdf.Rows.Add(1, "Maria", "Austria");
    //        //dtPurchaseIndentUdf.Rows.Add(2, "Thomas Hardy", "Ireland");
    //        //dtPurchaseIndentUdf.Rows.Add(3, "Laurence Lebihan", "USA");
    //        //dtPurchaseIndentUdf.Rows.Add(4, "Victoria Ashworth", "UK");


    //        ExportZip(dtPurchaseIndentDetail
    //                , dtPurchaseIndentHeader
    //                , dtPurchaseIndentUdf);
    //    }
    //    else
    //    {
    //        ExceptionMessage("No data found...");
    //        return;
    //    }
    //}

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPurchaseIndentHeader.Rows.Count > 0)
        {

            DataTable dtPurchaseIndentHeader = new DataTable();
            DataTable dtPurchaseIndentUdf = new DataTable();
            DataTable dtPurchaseIndentDetail = new DataTable();

            if (Session["dtPurchaseIndentHeader"] != null)
                dtPurchaseIndentHeader = (DataTable)Session["dtPurchaseIndentHeader"];

            if (Session["dtPurchaseIndentUdf"] != null)
                dtPurchaseIndentUdf = (DataTable)Session["dtPurchaseIndentUdf"];

            if (Session["dtPurchaseIndentDetail"] != null)
                dtPurchaseIndentDetail = (DataTable)Session["dtPurchaseIndentDetail"];


            DataTable dtH = dtPurchaseIndentHeader.Copy();
            dtH.TableName = "PURCHASE_INDENT_HEADER";

            DataTable dtU = dtPurchaseIndentUdf.Copy();
            dtU.TableName = "PURCHASE_INDENT_UDF";

            DataTable dtD = dtPurchaseIndentDetail.Copy();
            dtD.TableName = "PURCHASE_INDENT_DETAIL";

            ExportZip(dtH
                    , dtU
                    , dtD);
        }
        else
        {
            ExceptionMessage("No data found...");
            return;
        }
    }


    #endregion


    #region METHODS[=======================]


    private void ClearSessions()
    {
        Session["dtPurchaseIndentHeader"] = null;
        Session["dtPurchaseIndentUdf"] = null;
        Session["dtPurchaseIndentDetail"] = null;
    }

    private void BindStatus()
    {
        try
        {
            _dsStatus = objProjM.GetBomStatus();
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


    private void GetUnit()
    {
        try
        {
            Session["dsUnit"] = null;
            _dsUnit = objCommon.GetUnit();
            if (_dsUnit.Tables.Count > 0 && _dsUnit.Tables[0].Rows.Count > 0)
                Session["dsUnit"] = _dsUnit;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUnitToS()
    {
        try
        {
            if (Session["dsUnit"] != null)
                _dsUnit = (DataSet)Session["dsUnit"];

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


    private void GetBomType()
    {
        try
        {
            Session["dsBomType"] = null;

            _dsBomType = objProjM.GetBomTypes();
            if (_dsBomType.Tables.Count > 0 && _dsBomType.Tables[0].Rows.Count > 0)
                Session["dsBomType"] = _dsBomType;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomTypeToS()
    {
        try
        {
            if (Session["dsBomType"] != null)
                _dsBomType = (DataSet)Session["dsBomType"];

            if (_dsBomType.Tables.Count > 0 && _dsBomType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = _dsBomType.Tables[0];
                ddlType.DataTextField = "NAME";
                ddlType.DataValueField = "PID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "All");
                ddlType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindBomMrCreatedByToS()
    {
        try
        {
            _dsBomMrCreatedBy = objProjM.GetBomMrCreatedBy();

            if (_dsBomMrCreatedBy.Tables.Count > 0 && _dsBomMrCreatedBy.Tables[0].Rows.Count > 0)
            {
                ddlMrCreatedBy.DataSource = _dsBomMrCreatedBy.Tables[0];
                ddlMrCreatedBy.DataTextField = "CREATED_BY";
                ddlMrCreatedBy.DataValueField = "CREATED_BY_FID";
                ddlMrCreatedBy.DataBind();
                ddlMrCreatedBy.Items.Insert(0, "All");
                ddlMrCreatedBy.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetBomResponsibleFor()
    {
        try
        {
            Session["dsBomResponsibleFor"] = null;

            _dsBomResponsibleFor = objProjM.GetBomResponsibleFor();
            if (_dsBomResponsibleFor.Tables.Count > 0 && _dsBomResponsibleFor.Tables[0].Rows.Count > 0)
                Session["dsBomResponsibleFor"] = _dsBomResponsibleFor;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBomResponsibleForToS()
    {
        try
        {
            if (Session["dsBomResponsibleFor"] != null)
                _dsBomResponsibleFor = (DataSet)Session["dsBomResponsibleFor"];

            if (_dsBomResponsibleFor.Tables.Count > 0 && _dsBomResponsibleFor.Tables[0].Rows.Count > 0)
            {
                ddlResponsibleFor.DataSource = _dsBomResponsibleFor.Tables[0];
                ddlResponsibleFor.DataTextField = "NAME";
                ddlResponsibleFor.DataValueField = "PID";
                ddlResponsibleFor.DataBind();
                ddlResponsibleFor.Items.Insert(0, "All");
                ddlResponsibleFor.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void GetPivotGroups()
    {
        try
        {
            Session["dsBomPivotGroups"] = null;

            _dsBomPivotGroups = objProjM.GetPivotGroupList("", "", "");
            if (_dsBomPivotGroups.Tables.Count > 0 && _dsBomPivotGroups.Tables[0].Rows.Count > 0)
                Session["dsBomPivotGroups"] = _dsBomPivotGroups;

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPivotGroupsToS()
    {
        try
        {
            if (Session["dsBomPivotGroups"] != null)
                _dsBomPivotGroups = (DataSet)Session["dsBomPivotGroups"];

            if (_dsBomPivotGroups.Tables.Count > 0 && _dsBomPivotGroups.Tables[0].Rows.Count > 0)
            {
                ddlPivotGroup.DataSource = _dsBomPivotGroups.Tables[0];
                ddlPivotGroup.DataTextField = "PIVOT_GROUP";
                ddlPivotGroup.DataValueField = "PID";
                ddlPivotGroup.DataBind();
                ddlPivotGroup.Items.Insert(0, "All");
                ddlPivotGroup.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void GetPostedBOMList()
    {
        try
        {
            StatusIdToS = StatusIdToS + "," + BOMAllTypeEnums.EnumStatus.AmendedApproved;

            _dsPostedBomList = objProjM.GetPostedBOMListToExport
                          (DateTypeToS
                         , StartDateToS
                         , EndDateToS
                         , UnitIdToS
                         , TypeIdToS
                         , StatusIdToS
                         , PivotGroupIdToS
                         , ResponsibleForIdToS
                         , IsTcRequiredToS
                         , MrNoToS
                         , MrCreatedByIdToS
                         , BomNoToS
                         , JobNoToS);


            if (_dsPostedBomList.Tables.Count > 0)
            {
                if (_dsPostedBomList.Tables[0].Rows.Count > 0)
                {
                    Session["dtPurchaseIndentHeader"] = _dsPostedBomList.Tables[0];
                    gvPurchaseIndentHeader.DataSource = _dsPostedBomList.Tables[0];
                    gvPurchaseIndentHeader.DataBind();
                }

                if (_dsPostedBomList.Tables[1].Rows.Count > 0)
                {
                    Session["dtPurchaseIndentUdf"] = _dsPostedBomList.Tables[1];
                    gvPurchaseIndentUdf.DataSource = _dsPostedBomList.Tables[1];
                    gvPurchaseIndentUdf.DataBind();
                }

                if (_dsPostedBomList.Tables[2].Rows.Count > 0)
                {
                    Session["dtPurchaseIndentDetail"] = _dsPostedBomList.Tables[2];
                    gvPurchaseIndentDetail.DataSource = _dsPostedBomList.Tables[2];
                    gvPurchaseIndentDetail.DataBind();
                }
            }
            else
            {
                Session["dtPurchaseIndentHeader"] = null;
                Session["dtPurchaseIndentUdf"] = null;
                Session["dtPurchaseIndentDetail"] = null;
            }

            lblPurchaseIndentHeaderRecords.Text = "Purchase Indent Header Records[" + _dsPostedBomList.Tables[0].Rows.Count + "]";
            lblPurchaseIndentUdfRecords.Text = "Purchase Indent Udf Records[" + _dsPostedBomList.Tables[1].Rows.Count + "]";
            lblPurchaseIndentDetailRecords.Text = "Purchase Indent Detail Records[" + _dsPostedBomList.Tables[2].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportZip(DataTable dtPurchaseIndentHeader
                         , DataTable dtPurchaseIndentUdf
                         , DataTable dtPurchaseIndentDetail)
    {
        DataSet ds = new DataSet();

        ds.Tables.Add(dtPurchaseIndentDetail);
        ds.Tables.Add(dtPurchaseIndentHeader);
        ds.Tables.Add(dtPurchaseIndentUdf);

        List<ExportFiles> txtDatas = new List<ExportFiles>();
        foreach (DataTable dt in ds.Tables)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(ConvertDataTableToString(dt));
            txtDatas.Add(new ExportFiles() { TableName = dt.TableName, Bytes = bytes });
        }

        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            foreach (ExportFiles txtData in txtDatas)
            {
                zip.AddEntry(txtData.TableName + ".csv", txtData.Bytes);
            }

            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("Zip_Purchase_Indent_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }

    private string ConvertDataTableToString(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        for (int k = 0; k < dt.Columns.Count; k++)
        {
            sb.Append(dt.Columns[k].ColumnName + ',');
        }
        sb.Append("\r\n");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
            }
            sb.Append("\r\n");
        }

        return sb.ToString();
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion







}
//public class PurchaseIndentFiles
//{
//    public string TableName { get; set; }
//    public byte[] Bytes { get; set; }
//}
