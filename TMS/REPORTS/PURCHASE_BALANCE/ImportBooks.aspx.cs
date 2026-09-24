using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_BALANCE_ImportBooks : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.PurchaseBalanceReports objPurchaseBalanceReports = new BAL.PurchaseBalanceReports();

    //DataSet dsUnit = new DataSet();
    DataSet dsDrawingNo = new DataSet();
    int recordID = 0;
    string jobNo = string.Empty;
    string drawingNo = string.Empty;
    string description = string.Empty;
    int quantity = 0;
    string UOM = string.Empty;
    string rqdDateByProjectTeam = string.Empty;
    string isActive = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //Session["dtUnit"] = null;
                //BindCompany();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetFormat_Click(object sender, EventArgs e)
    {
        DownloadFormat();
    }

    protected void btnGetFile_Click(object sender, EventArgs e)
    {
        GetDetails();
    }

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = new DataTable();

                //Label lblDrawingID = (Label)e.Row.FindControl("lblDrawingID");
                //Label lblDuplicateFlag = (Label)e.Row.FindControl("lblDuplicateFlag");
                //Label lblJOBUnitUD = (Label)e.Row.FindControl("lblJOBUnitUD");
                //DropDownList ddlJOBUnit = (DropDownList)e.Row.FindControl("ddlJOBUnit");

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                //if (Convert.ToInt32(lblDrawingID.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}

                //if (Convert.ToInt32(lblDuplicateFlag.Text) > 0)
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                //    }
                //}


                //if (Session["dtUnit"] != null)
                //{
                //    dt = (DataTable)Session["dtUnit"];
                //}
                //else
                //{
                //    BindCompany();
                //    dt = (DataTable)Session["dtUnit"];
                //}

                //if (dt.Rows.Count > 0)
                //{
                //    ddlJOBUnit.DataSource = dt;
                //    ddlJOBUnit.DataTextField = "UNIT_NAME";
                //    ddlJOBUnit.DataValueField= "UNIT_ID";
                //    ddlJOBUnit.DataBind();
                //    if (Convert.ToInt32(lblJOBUnitUD.Text)>0)
                //    {
                //        ddlJOBUnit.SelectedValue = Convert.ToString(lblJOBUnitUD.Text);
                //    }
                //    else
                //    {
                //        ddlJOBUnit.SelectedIndex = 0;
                //    }                    
                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (gvDesignDetails.Rows.Count > 0)
        {
            ImportDrawings();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    protected void btnViewBooksList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/REPORTS/PURCHASE_BALANCE/ImportedBooksCleanList.aspx");
    }

    protected void btnViewExcludedBooksList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/REPORTS/PURCHASE_BALANCE/ExcludedBooksList.aspx");
    }





    #endregion


    #region METHODS[=========================]

    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "VENDOR_CODE" + ',';
            csv += "VENDOR_NAME" + ',';
            csv += "DOCUMENT_TYPE" + ',';
            csv += "PARTY_INVOICE_NO" + ',';
            csv += "PARTY_INVOICE_DATE (yyyy-MM-dd)" + ',';
            csv += "GSTIN" + ',';
            csv += "CURRENCY_CODE" + ',';
            csv += "TAXABLE_AMOUNT" + ',';
            csv += "COUNTRY" + ',';
            //csv += "MONTH" + ',';
            //csv += "YEAR" + ',';

            csv += "\r\n";

            string fileName = "BOOKS_" + DateTime.Now;
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

    private void GetDetails()
    {
        try
        {
            string year = string.Empty;
            string month = string.Empty;
            jobNo = string.Empty;
            drawingNo = string.Empty;

            // Create DataTable structure
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("SR_NO", typeof(string));
            dtTemp.Columns.Add("VENDOR_CODE", typeof(string));
            dtTemp.Columns.Add("VENDOR_NAME", typeof(string));
            dtTemp.Columns.Add("DOCUMENT_TYPE", typeof(string));
            dtTemp.Columns.Add("PARTY_INVOICE_NO", typeof(string));
            dtTemp.Columns.Add("PARTY_INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("GSTIN", typeof(string));
            dtTemp.Columns.Add("CURRENCY_CODE", typeof(string));
            dtTemp.Columns.Add("TAXABLE_AMOUNT", typeof(string));
            dtTemp.Columns.Add("COUNTRY", typeof(string));
            dtTemp.Columns.Add("MONTH", typeof(string));
            dtTemp.Columns.Add("YEAR", typeof(string));

            // Check for file upload
            if (!fileUploadDrawingFile.HasFile)
            {
                ExceptionMessage("Please upload a valid CSV file!");
                return;
            }

            // Handle type (yearly / monthly)
            int type = Convert.ToInt32(rdType.SelectedValue);
            year = ddlYear.SelectedValue.ToString();
            if (type == 2)
                month = ddlMonth.SelectedValue.ToString();

            // Read CSV
            using (StreamReader reader = new StreamReader(fileUploadDrawingFile.PostedFile.InputStream))
            {
                CsvConfiguration config = new CsvConfiguration();//CultureInfo.InvariantCulture
                config.HasHeaderRecord = true;
                //config.TrimOptions = TrimOptions.Trim;
                //config.MissingFieldFound = null;
                //config.BadDataFound = null;

                using (CsvReader csv = new CsvReader(reader, config))
                {
                    // Read each record (C# 3 compatible)
                    int count = 0;
                    while (csv.Read())
                    {
                        count++;
                        DataRow dr = dtTemp.NewRow();

                        string vendorCode = GetSafeValue(csv, 0).ToUpper();
                        string vendorName = GetSafeValue(csv, 1);
                        string documentType = GetSafeValue(csv, 2);
                        string partyInvoiceNo = GetSafeValue(csv, 3).ToUpper();
                        string rawDate = GetSafeValue(csv, 4);
                        string gstin = GetSafeValue(csv, 5).ToUpper();
                        string currencyCode = GetSafeValue(csv, 6);
                        string taxableAmount = GetSafeValue(csv, 7);
                        string country = GetSafeValue(csv, 8).ToUpper();

                        if (string.IsNullOrEmpty(currencyCode))
                            currencyCode = "INR";

                        if (string.IsNullOrEmpty(taxableAmount))
                            taxableAmount = "0";

                        if (currencyCode == "INR")
                            country = "INDIA";

                        // Format date (from dd/MM/yyyy or dd-MM-yyyy → yyyy-MM-dd)
                        string formattedDate = "";
                        if (!string.IsNullOrEmpty(rawDate))
                        {
                            string[] parts = rawDate.Contains("/") ? rawDate.Split('/') : rawDate.Split('-');
                            if (parts.Length == 3)
                                formattedDate = parts[0] + "-" + parts[1] + "-" + parts[2];
                        }

                        dr["SR_NO"] = count;
                        dr["VENDOR_CODE"] = vendorCode;
                        dr["VENDOR_NAME"] = vendorName;
                        dr["DOCUMENT_TYPE"] = documentType;
                        dr["PARTY_INVOICE_NO"] = partyInvoiceNo;
                        dr["PARTY_INVOICE_DATE"] = formattedDate;
                        dr["GSTIN"] = gstin;
                        dr["CURRENCY_CODE"] = currencyCode;
                        dr["TAXABLE_AMOUNT"] = taxableAmount;
                        dr["COUNTRY"] = country;
                        dr["MONTH"] = month;
                        dr["YEAR"] = year;

                        dtTemp.Rows.Add(dr);
                    }
                }
            }

            // Bind to GridView
            if (dtTemp.Rows.Count > 0)
            {
                gvDesignDetails.DataSource = dtTemp;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[" + dtTemp.Rows.Count + "]";
            }
            else
            {
                gvDesignDetails.DataSource = null;
                gvDesignDetails.DataBind();
                lblRecords.Text = "Records[0]";
                ExceptionMessage("No data found..!!!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.Message);
        }
    }

    private string GetSafeValue(CsvReader csv, int index)
    {
        try
        {
            return csv.GetField(index).Trim();
        }
        catch
        {
            return string.Empty;
        }
    }

    private void ImportDrawings()
    {
        try
        {
            int value = 0;
            int totalRecords = 0;
            int cleanRecords = 0;

            jobNo = string.Empty;
            drawingNo = string.Empty;
            description = string.Empty;
            quantity = 0;
            UOM = string.Empty;
            rqdDateByProjectTeam = string.Empty;

            DataTable dtTemp = new DataTable();
            #region CREATE_TABLE

            dtTemp.Columns.Add("VENDOR_CODE", typeof(string));
            dtTemp.Columns.Add("VENDOR_NAME", typeof(string));
            dtTemp.Columns.Add("DOCUMENT_TYPE", typeof(string));
            dtTemp.Columns.Add("PARTY_INVOICE_NO", typeof(string));
            dtTemp.Columns.Add("PARTY_INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("GSTIN", typeof(string));
            dtTemp.Columns.Add("CURRENCY_CODE", typeof(string));
            dtTemp.Columns.Add("TAXABLE_AMOUNT", typeof(decimal));
            dtTemp.Columns.Add("COUNTRY", typeof(string));
            dtTemp.Columns.Add("MONTH", typeof(string));
            dtTemp.Columns.Add("YEAR", typeof(string));

            #endregion


            bool check = true;

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                Label lblDocumentType = (Label)gr.FindControl("lblDocumentType");
                Label lblPartyInvoiceNo = (Label)gr.FindControl("lblPartyInvoiceNo");
                Label lblPartyInvoiceDate = (Label)gr.FindControl("lblPartyInvoiceDate");
                Label lblGSTIN = (Label)gr.FindControl("lblGSTIN");
                Label lblCurrencyCode = (Label)gr.FindControl("lblCurrencyCode");
                Label lblTaxableAmount = (Label)gr.FindControl("lblTaxableAmount");
                Label lblCountry = (Label)gr.FindControl("lblCountry");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblYear = (Label)gr.FindControl("lblYear");



                DataRow dr = dtTemp.NewRow();
                dr["VENDOR_CODE"] = lblVendorCode.Text;
                dr["VENDOR_NAME"] = lblVendorName.Text;
                dr["DOCUMENT_TYPE"] = lblDocumentType.Text;
                dr["PARTY_INVOICE_NO"] = lblPartyInvoiceNo.Text;
                dr["PARTY_INVOICE_DATE"] = lblPartyInvoiceDate.Text;
                dr["GSTIN"] = lblGSTIN.Text;
                dr["CURRENCY_CODE"] = lblCurrencyCode.Text;
                dr["TAXABLE_AMOUNT"] = lblTaxableAmount.Text;
                dr["COUNTRY"] = lblCountry.Text;
                dr["MONTH"] = lblMonth.Text;
                dr["YEAR"] = lblYear.Text;

                dtTemp.Rows.Add(dr);
            }

            if (dtTemp.Rows.Count > 0)
            {
                totalRecords = dtTemp.Rows.Count;
                string year = "0";
                string month = "0";
                if (Convert.ToInt32(rdType.SelectedValue) == 1)//yearly
                {
                    year = Convert.ToString(ddlYear.SelectedValue);
                }
                if (Convert.ToInt32(rdType.SelectedValue) == 2)//monthly
                {
                    year = Convert.ToString(ddlYear.SelectedValue);
                    month = Convert.ToString(ddlMonth.SelectedValue);
                }
                value = objPurchaseBalanceReports.ImportBooks(dtTemp, year, month);
            }

            if (value > 0)
            {
                cleanRecords = value;
                //gvDesignDetails.DataSource = null;
                //gvDesignDetails.DataBind();
                SuccessMessage(cleanRecords + " clean record(s) imported out of " + totalRecords + " total record(s) successfully!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
