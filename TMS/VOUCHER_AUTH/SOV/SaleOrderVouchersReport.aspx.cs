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

public partial class VOUCHER_AUTH_SOV_SaleOrderVouchersReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsVouchersList = new DataSet();
    DataSet _dsVoucherCreatedBy = new DataSet();
    DataSet _dsUnit = new DataSet();
    DataSet _dsStatus = new DataSet();


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

    public string CustomerCodeS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtCustomerCodeToS.Text))
                _customerCodeS = Convert.ToString(txtCustomerCodeToS.Text);
            else _customerCodeS = "";

            return _customerCodeS;
        }
    }

    public string CustomerNameS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtCustomerNameToS.Text))
                _customerNameS = Convert.ToString(txtCustomerNameToS.Text);
            else _customerNameS = "";

            return _customerNameS;
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
            ToCSVNew01(dt, "SaleOrderVoucherReport_");
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
            _dsVoucherCreatedBy = objVouchersAuthorization.GetVouchersCreatedByList((int)VoucherStatusTypes.EnumVoucherTypes.SOV);
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
            dsVouchersList = objVouchersAuthorization.GetSaleOrderVouchersReport
                (
                      DateSignS, DateTypeS, StartDateS, EndDateS, UnitIdS, StatusIdS
                      , VoucherCreatedByS, SearchByS, SearchTextS, CustomerCodeS, CustomerNameS
                      , AmountSignS, AmountOneS, AmountTwoS
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
