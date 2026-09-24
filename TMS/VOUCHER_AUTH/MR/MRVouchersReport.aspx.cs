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

public partial class VOUCHER_AUTH_MR_MRVouchersReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    DataSet dsVouchersDetails = new DataSet();
    DataSet _dsVoucherCreatedBy = new DataSet();
    DataSet _dsStatus = new DataSet();

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
    int _unitId = 0;
    string _startDate = "";
    string _endDate = "";
    string _voucherNumber = "";
    private string _warehouseId;



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

                BindStatusList();

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
            ToCSVNew01(dt, "MRVoucherReport_");
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

    private void GetVouchersCreatedByList()
    {
        try
        {
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList((int)VoucherStatusTypes.EnumVoucherTypes.MR);
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
            dsVouchersDetails = objVouchersAuthorization.GetMRVouchersReport
                (
                      StartDate
                    , EndDate
                    , UnitId
                    , WarehouseId
                    , StatusId
                    , VoucherNumber
                    , VoucherCreatedBy
                );
            if (dsVouchersDetails.Tables.Count > 0 && dsVouchersDetails.Tables[0].Rows.Count > 0)
            {
                Session["VOUCHERS"] = dsVouchersDetails.Tables[0];
                gvVouchersList.DataSource = dsVouchersDetails.Tables[0];
                gvVouchersList.DataBind();
            }
            else
            {
                Session["VOUCHERS"] = null;
                gvVouchersList.DataSource = null;
                gvVouchersList.DataBind();
            }

            lblRecords.Text = "Records[" + dsVouchersDetails.Tables[0].Rows.Count + "]";
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
