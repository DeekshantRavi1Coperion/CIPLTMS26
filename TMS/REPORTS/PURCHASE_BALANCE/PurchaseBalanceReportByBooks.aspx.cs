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

public partial class REPORTS_PURCHASE_BALANCE_PurchaseBalanceReportByBooks : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.PurchaseBalanceReports objPurchaseBalanceReports = new BAL.PurchaseBalanceReports();
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


    public string GSTINS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtGSTINToS.Text))
                _GSTINS = Convert.ToString(txtGSTINToS.Text);
            else _GSTINS = "";

            return _GSTINS;
        }
    }

    public string VendorCodeS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVendorCodeToS.Text))
                _vendorCodeS = Convert.ToString(txtVendorCodeToS.Text);
            else _vendorCodeS = "";

            return _vendorCodeS;
        }
    }

    public string VendorNameS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtVendorNameToS.Text))
                _vendorNameS = Convert.ToString(txtVendorNameToS.Text);
            else _vendorNameS = "";

            return _vendorNameS;
        }
    }

    public string InvoiceNoS
    {
        get
        {
            if (!string.IsNullOrEmpty(txtInvoiceNoToS.Text))
                _invoiceNoS = Convert.ToString(txtInvoiceNoToS.Text);
            else _invoiceNoS = "";

            return _invoiceNoS;
        }
    }

    public string IsGSTInvoiceNoAmountMatchedS
    {
        get
        {
            if (chkIsGSTInvoiceNoAmountMatched.Checked)
                _isGSTInvoiceNoAmountMatchedS = "M";
            else
                _isGSTInvoiceNoAmountMatchedS = "";

            return _isGSTInvoiceNoAmountMatchedS;
        }
    }

    public string IsGSTInvoiceDateAmountMatchedS
    {
        get
        {
            if (chkIsGSTInvoiceDateAmountMatched.Checked)
                _isGSTInvoiceDateAmountMatchedS = "M";
            else
                _isGSTInvoiceDateAmountMatchedS = "";

            return _isGSTInvoiceDateAmountMatchedS;
        }
    }

    public string IsGSTAmountMatchedS
    {
        get
        {
            if (chkIsGSTAmountMatched.Checked)
                _isGSTAmountMatchedS = "M";
            else
                _isGSTAmountMatchedS = "";

            return _isGSTAmountMatchedS;
        }
    }

    public string IsFullyUnmatchedS
    {
        get
        {
            if (chkIsFullyUnmatched.Checked)
                _isFullyUnmatchedS = "U";
            else
                _isFullyUnmatchedS = "";

            return _isFullyUnmatchedS;
        }
    }

    private string _voucherCreatedByS;
    private string _dateSignS;
    private string _dateTypeS;
    private string _searchByS;
    private string _vendorCodeS;
    private string _vendorNameS;
    private string _amountSignS;
    private decimal _amountOneS;
    private decimal _amountTwoS;
    private int _statusIdS;
    private string _GSTINS;
    private string _invoiceNoS;
    private string _isGSTInvoiceNoAmountMatchedS;
    private string _isGSTInvoiceDateAmountMatchedS;
    private string _isGSTAmountMatchedS;
    private string _isFullyUnmatchedS;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["REPORT"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, 1, 1);//DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var currentDate = new DateTime(now.Year, now.Month, 1);
                var endDate = currentDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                GetReport();
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
        GetReport();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvReport.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["REPORT"];
            ToCSVNew01(dt, "PurchaseBalanceReportByBooks_");
        }
        else
        {
            ExceptionMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void GetReport()
    {
        try
        {
            dsVouchersList = objPurchaseBalanceReports.GetReportByBooks
                (
                        DateSignS
                    ,   DateTypeS
                    ,   StartDateS
                    ,   EndDateS
                    ,   GSTINS
                    ,   VendorCodeS
                    ,   VendorNameS
                    ,   InvoiceNoS
                    ,   IsGSTInvoiceNoAmountMatchedS
                    ,   IsGSTInvoiceDateAmountMatchedS
                    ,   IsGSTAmountMatchedS
                    ,   IsFullyUnmatchedS
                );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
                Session["REPORT"] = dsVouchersList.Tables[0];
                gvReport.DataSource = dsVouchersList.Tables[0];
                gvReport.DataBind();
            }
            else
            {
                Session["REPORT"] = null;
                gvReport.DataSource = null;
                gvReport.DataBind();
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
