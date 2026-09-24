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
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public partial class REPORTS_PURCHASE_BALANCE_GSTReconSummaryReport : System.Web.UI.Page
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

            _startDateS = "";
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

            _endDateS = "";
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

    //public string IsMatchedS
    //{
    //    get
    //    {
    //        //if (chkIsGSTInvoiceNoAmountMatched.Checked)
    //        //    _isGSTInvoiceNoAmountMatchedS = "M";
    //        //else
    //        //    _isGSTInvoiceNoAmountMatchedS = "";

    //        //if (rdIsMatched.SelectedIndex == 0) _isGSTInvoiceNoAmountMatchedS = "";
    //        //else if (rdIsMatched.SelectedIndex == 1) _isGSTInvoiceNoAmountMatchedS = "M";
    //        //else if (rdIsMatched.SelectedIndex == 2) _isGSTInvoiceNoAmountMatchedS = "U";

    //        _isGSTInvoiceNoAmountMatchedS = rdIsMatched.SelectedValue;

    //        return _isGSTInvoiceNoAmountMatchedS;
    //    }
    //}

    //public string IsGSTInvoiceDateAmountMatchedS
    //{
    //    get
    //    {
    //        if (chkIsGSTInvoiceDateAmountMatched.Checked)
    //            _isGSTInvoiceDateAmountMatchedS = "M";
    //        else
    //            _isGSTInvoiceDateAmountMatchedS = "";

    //        return _isGSTInvoiceDateAmountMatchedS;
    //    }
    //}

    public string IsGSTAmountMatchedS
    {
        get
        {
            //if (chkIsGSTAmountMatched.Checked)
            //    _isGSTAmountMatchedS = "M";
            //else
            //    _isGSTAmountMatchedS = "";

            return _isGSTAmountMatchedS;
        }
    }

    //public string IsFullyUnmatchedS
    //{
    //    get
    //    {
    //        if (chkIsFullyUnmatched.Checked)
    //            _isFullyUnmatchedS = "U";
    //        else
    //            _isFullyUnmatchedS = "";

    //        return _isFullyUnmatchedS;
    //    }
    //}


    //public string IschkIsGSTNotInS
    //{
    //    get
    //    {
    //        if (chkIsGSTNotIn.Checked)
    //            _ischkIsGSTNotInS = "NA";
    //        else
    //            _ischkIsGSTNotInS = "";

    //        return _ischkIsGSTNotInS;
    //    }
    //}



    public int IsBooksS
    {
        get
        {
            if (chkIsBooksS.Checked)
                _isBooksS = 1;
            else
                _isBooksS = 0;

            return _isBooksS;
        }
    }

    public int IsGSTR2BS
    {
        get
        {
            if (chkIsGSTR2BS.Checked)
                _isGSTR2BS = 1;
            else
                _isGSTR2BS = 0;

            return _isGSTR2BS;
        }
    }


    public int GSTINNotInFACTS
    {
        get
        {
            _GSTINNotInFACTS = Convert.ToInt32(chkIsGSTINNotInFACTS.Checked);
            return _GSTINNotInFACTS;
        }
    }

    public int OldInvoiceS
    {
        get
        {
            _oldInvoiceS = Convert.ToInt32(chkIsOldInvoiceS.Checked);
            return _oldInvoiceS;           
        }
    }

    public int RCMS
    {
        get
        {
            _RCMS = Convert.ToInt32(chkIsRCMS.Checked);
            return _RCMS;
        }
    }




    public int PartiallyMatchS
    {
        get
        {
            _partiallyMatchS = Convert.ToInt32(chkIsPartiallyMatchS.Checked);
            return _partiallyMatchS;
        }
    }


    public int MismatchS
    {
        get
        {
            _mismatchS = Convert.ToInt32(chkIsMismatchS.Checked);
            return _mismatchS;
        }
    }

    //public int MismatchTaxableValueS
    //{
    //    get
    //    {
    //        if (chkIsMismatchTaxableValueS.Checked)
    //            _mismatchTaxableValueS = -1;
    //        else
    //            _mismatchTaxableValueS = 0;

    //        return _mismatchTaxableValueS;
    //    }
    //}

    //public int MatchInvoiceDateTaxableValueS
    //{
    //    get
    //    {
    //        if (chkIsMismatchInvoiceDateTaxableValueS.Checked)
    //            _matchInvoiceDateTaxableValueS = 1;
    //        else
    //            _matchInvoiceDateTaxableValueS = 0;

    //        return _matchInvoiceDateTaxableValueS;
    //    }
    //}



    //public int MismatchOldInvoiceS
    //{
    //    get
    //    {
    //        if (chkIsMismatchOldInvoiceS.Checked)
    //            _mismatchOldInvoiceS = 1;
    //        else
    //            _mismatchOldInvoiceS = 0;

    //        return _mismatchOldInvoiceS;
    //    }
    //}

    //public int MismatchRCMS
    //{
    //    get
    //    {
    //        if (chkIsMismatchRCMS.Checked)
    //            _mismatchRCMS = 1;
    //        else
    //            _mismatchRCMS = 0;

    //        return _mismatchRCMS;
    //    }
    //}



    public string PostedYearS
    {
        get
        {
            _postedYearS = Convert.ToString(ddlPostedYearToS.Text);
            return _postedYearS;
        }
    }

    public string PostedMonthS
    {
        get
        {
            _postedMonthS = "";

            if (ddlPostedMonthToS.SelectedIndex > 0)
                _postedMonthS = Convert.ToString(ddlPostedMonthToS.Text);

            return _postedMonthS;
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
    private string _postedYearS;
    private string _postedMonthS;
    private string _ischkIsGSTNotInS;
    private int _GSTINNotInFACTS;
    private int _RCMS;
    private int _oldInvoiceS;
    private int _matchInvoiceNoTaxableValueS;
    private int _partiallyMatchS;
    private int _mismatchS;
    private int _mismatchTaxableValueS;
    private int _matchInvoiceDateTaxableValueS;
    private int _isBooksS;
    private int _isGSTR2BS;
    private int _mismatchOldInvoiceS;
    private int _mismatchRCMS;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["REPORT"] = null;

                ClearStats();

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
        ClearStats();
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
            ToCSVNew01(dt, "GSTReconciliationSummaryReport_");
            //ExportDataTableToCsv(dt, "GSTReconciliationReportByBooksB2B_");
        }
        else
        {
            ExceptionMessage("No data found!");
        }
    }

    protected void chkSelectDeselectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSelectDeselectAll.Checked)
        {
            chkIsBooksS.Checked = true;
            chkIsGSTR2BS.Checked = true;
            chkIsGSTINNotInFACTS.Checked = true;
            chkIsOldInvoiceS.Checked = true;
            chkIsRCMS.Checked = true;
            chkIsPartiallyMatchS.Checked = true;
            chkIsMismatchS.Checked = true;
        }
        else
        {
            chkIsBooksS.Checked = false;
            chkIsGSTR2BS.Checked = false;
            chkIsGSTINNotInFACTS.Checked = false;
            chkIsOldInvoiceS.Checked = false;
            chkIsRCMS.Checked = false;
            chkIsPartiallyMatchS.Checked = false;
            chkIsMismatchS.Checked = false;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void GetReport()
    {
        try
        {
            dsVouchersList.Tables.Clear();
            dsVouchersList = objPurchaseBalanceReports.GetGSTReconSummaryReport
            (
                    DateSignS
                ,   DateTypeS
                ,   StartDateS
                ,   EndDateS
                ,   GSTINS
                ,   VendorCodeS
                ,   VendorNameS
                ,   InvoiceNoS
                ,   PostedYearS
                ,   PostedMonthS

                ,   IsBooksS
                ,   IsGSTR2BS                
                ,   GSTINNotInFACTS
                ,   OldInvoiceS
                ,   RCMS
                ,   PartiallyMatchS
                ,   MismatchS
                
                //,   MismatchTaxableValueS
                //,   MatchInvoiceDateTaxableValueS
                //,   MismatchOldInvoiceS
                //,   MismatchRCMS
            );
            if (dsVouchersList.Tables.Count > 0 && dsVouchersList.Tables[0].Rows.Count > 0)
            {
                if (dsVouchersList.Tables[0].Rows.Count > 0)
                {
                    Session["REPORT"] = dsVouchersList.Tables[0];
                    gvReport.DataSource = dsVouchersList.Tables[0];
                    gvReport.DataBind();
                }

                if (dsVouchersList.Tables[1].Rows.Count > 0)
                {
                    gvBooksImportedAndProcessed.DataSource = dsVouchersList.Tables[1];
                    gvBooksImportedAndProcessed.DataBind();
                }

                if (dsVouchersList.Tables[2].Rows.Count > 0)
                {
                    gvGSTR2BImportedAndProcessed.DataSource = dsVouchersList.Tables[2];
                    gvGSTR2BImportedAndProcessed.DataBind();
                }

                if (dsVouchersList.Tables[3].Rows.Count > 0)
                {
                    gvBothMatchMismatchCounts.DataSource = dsVouchersList.Tables[3];
                    gvBothMatchMismatchCounts.DataBind();
                }

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

    //private void ToCSVNew01(DataTable dt, string fileNameS)
    //{
    //    try
    //    {
    //        string csv = string.Empty;
    //        foreach (DataColumn column in dt.Columns)
    //        {
    //            csv += column.ColumnName + ',';
    //        }
    //        csv += "\r\n";

    //        foreach (DataRow row in dt.Rows)
    //        {
    //            foreach (DataColumn column in dt.Columns)
    //            {
    //                csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
    //            }
    //            csv += "\r\n";
    //        }

    //        string fileName = fileNameS + DateTime.Now.ToString("dd_MMM_yyyy");
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
    //        Response.Charset = "";
    //        Response.ContentType = "application/text";
    //        Response.Output.Write(csv);
    //        Response.Flush();
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void ToCSVNew01(DataTable dt, string fileNameS)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;

            string fileName = fileNameS + DateTime.Now.ToString("dd_MMM_yyyy") + ".csv";

            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "text/csv";

            using (var writer = new StreamWriter(Response.OutputStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration()
            {
                HasHeaderRecord = true
            }))
            {
                // Write header
                foreach (DataColumn column in dt.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write rows
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(row[column]);
                    }
                    csv.NextRecord();
                }

                writer.Flush();
            }

            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void ExportDataTableToCsv(DataTable dt, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, new CsvConfiguration()
        {
            HasHeaderRecord = true
        }))
        {
            // Write header
            foreach (DataColumn column in dt.Columns)
            {
                csv.WriteField(column.ColumnName);
            }
            csv.NextRecord();

            // Write rows
            foreach (DataRow row in dt.Rows)
            {
                foreach (var field in row.ItemArray)
                {
                    csv.WriteField(field);
                }
                csv.NextRecord();
            }
        }
    }

    private void ClearStats()
    {
        gvBooksImportedAndProcessed.DataSource = null;
        gvGSTR2BImportedAndProcessed.DataSource = null;
        gvBothMatchMismatchCounts.DataSource = null;

        gvBooksImportedAndProcessed.DataBind();
        gvGSTR2BImportedAndProcessed.DataBind();
        gvBothMatchMismatchCounts.DataBind();
    }

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion


   
}
