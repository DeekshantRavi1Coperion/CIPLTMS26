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

public partial class VOUCHER_AUTH_MRN_MRNVouchersReport : System.Web.UI.Page
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


    private string _voucherCreatedBy;
    
    private int _statusId;


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
            ToCSVNew01(dt,"MRNVouchersReport_");
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
            dsVouchersList = objVouchersAuthorization.GetMRNVouchersReport
                (
                      StartDate
                    , EndDate
                    , UnitId
                    , StatusId
                    , VoucherNumber
                    , VoucherCreatedBy
                );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
                Session["VOUCHERS"]= dsVouchersList.Tables[0];
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

    private void ToCSVNew01(DataTable dt,string fileNameS)
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

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
