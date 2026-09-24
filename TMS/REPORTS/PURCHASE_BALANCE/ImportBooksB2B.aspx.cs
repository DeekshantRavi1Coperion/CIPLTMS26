using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_BALANCE_ImportBooksB2B : System.Web.UI.Page
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
            ImportBooks();
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

            csv = "GSTIN_OF_SUPPLIER" + ',';
            csv += "INVOICE_NUMBER" + ',';
            csv += "INVOICE_DATE" + ',';
            csv += "INVOICE_VALUE" + ',';
            csv += "PLACE_OF_SUPPLY" + ',';
            csv += "REVERSE_CHARGE" + ',';
            csv += "INVOICE_TYPE" + ',';
            csv += "RATE" + ',';
            csv += "TAXABLE_VALUE" + ',';
            csv += "INTEGRATED_TAX_PAID" + ',';
            csv += "CENTRAL_TAX_PAID" + ',';
            csv += "STATE_UT_TAX_PAID" + ',';
            csv += "CESS_PAID" + ',';
            csv += "ELIGIBILITY_FOR_ITC" + ',';
            csv += "AVAILED_ITC_INTEGRATED_TAX" + ',';
            csv += "AVAILED_ITC_CENTRAL_TAX" + ',';
            csv += "AVAILED_ITC_STATE_UT_TAX" + ',';
            csv += "AVAILED_ITC_CESS" + ',';

            csv += "\r\n";

            string fileName = "BOOKS_B2B_" + DateTime.Now;
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
            dtTemp.Columns.Add("GSTIN_OF_SUPPLIER", typeof(string));
            dtTemp.Columns.Add("INVOICE_NUMBER", typeof(string));
            dtTemp.Columns.Add("INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("INVOICE_VALUE", typeof(string));
            dtTemp.Columns.Add("PLACE_OF_SUPPLY", typeof(string));
            dtTemp.Columns.Add("REVERSE_CHARGE", typeof(string));
            dtTemp.Columns.Add("INVOICE_TYPE", typeof(string));
            dtTemp.Columns.Add("RATE", typeof(string));
            dtTemp.Columns.Add("TAXABLE_VALUE", typeof(string));
            dtTemp.Columns.Add("INTEGRATED_TAX_PAID", typeof(string));
            dtTemp.Columns.Add("CENTRAL_TAX_PAID", typeof(string));
            dtTemp.Columns.Add("STATE_UT_TAX_PAID", typeof(string));
            dtTemp.Columns.Add("CESS_PAID", typeof(string));
            dtTemp.Columns.Add("ELIGIBILITY_FOR_ITC", typeof(string));
            dtTemp.Columns.Add("AVAILED_ITC_INTEGRATED_TAX", typeof(string));
            dtTemp.Columns.Add("AVAILED_ITC_CENTRAL_TAX", typeof(string));
            dtTemp.Columns.Add("AVAILED_ITC_STATE_UT_TAX", typeof(string));
            dtTemp.Columns.Add("AVAILED_ITC_CESS", typeof(string));

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

                        //string vendorCode = GetSafeValue(csv, 0).ToUpper();
                        //string vendorName = GetSafeValue(csv, 1);

                        string GstinOfSupplier = GetSafeValue(csv, 0);
                        string InvoiceNumber = GetSafeValue(csv, 1);
                        string InvoiceDate = GetSafeValue(csv, 2);
                        string InvoiceValue = GetSafeValue(csv, 3);
                        string PlaceOfSupply = GetSafeValue(csv, 4);
                        string ReverseCharge = GetSafeValue(csv, 5);
                        string InvoiceType = GetSafeValue(csv, 6);
                        string Rate = GetSafeValue(csv, 7);
                        string TaxableValue = GetSafeValue(csv, 8);
                        string IntegratedTaxPaid = GetSafeValue(csv, 9);
                        string CentralTaxPaid = GetSafeValue(csv, 10);
                        string StateUtTaxPaid = GetSafeValue(csv, 11);
                        string CessPaid = GetSafeValue(csv, 12);
                        string EligibilityForItc = GetSafeValue(csv, 13);
                        string AvailedItcIntegratedTax = GetSafeValue(csv, 14);
                        string AvailedItcCentralTax = GetSafeValue(csv, 15);
                        string AvailedItcStateUtTax = GetSafeValue(csv, 16);
                        string AvailedItcCess = GetSafeValue(csv, 17);

                        //if (string.IsNullOrEmpty(currencyCode))
                        //    currencyCode = "INR";

                        //if (string.IsNullOrEmpty(taxableAmount))
                        //    taxableAmount = "0";

                        //if (currencyCode == "INR")
                        //    country = "INDIA";

                        //// Format date (from dd/MM/yyyy or dd-MM-yyyy → yyyy-MM-dd)
                        //string formattedDate = "";
                        //if (!string.IsNullOrEmpty(rawDate))
                        //{
                        //    string[] parts = rawDate.Contains("/") ? rawDate.Split('/') : rawDate.Split('-');
                        //    if (parts.Length == 3)
                        //        formattedDate = parts[0] + "-" + parts[1] + "-" + parts[2];
                        //}

                        dr["SR_NO"] = count;
                        dr["VENDOR_CODE"] = "";
                        dr["VENDOR_NAME"] = "";

                        dr["GSTIN_OF_SUPPLIER"] = GstinOfSupplier;
                        dr["INVOICE_NUMBER"] = InvoiceNumber;
                        dr["INVOICE_DATE"] = InvoiceDate;
                        dr["INVOICE_VALUE"] = InvoiceValue;
                        dr["PLACE_OF_SUPPLY"] = PlaceOfSupply;
                        dr["REVERSE_CHARGE"] = ReverseCharge;
                        dr["INVOICE_TYPE"] = InvoiceType;
                        dr["RATE"] = Rate;
                        dr["TAXABLE_VALUE"] = TaxableValue;
                        dr["INTEGRATED_TAX_PAID"] = IntegratedTaxPaid;
                        dr["CENTRAL_TAX_PAID"] = CentralTaxPaid;
                        dr["STATE_UT_TAX_PAID"] = StateUtTaxPaid;
                        dr["CESS_PAID"] = CessPaid;
                        dr["ELIGIBILITY_FOR_ITC"] = EligibilityForItc;
                        dr["AVAILED_ITC_INTEGRATED_TAX"] = AvailedItcIntegratedTax;
                        dr["AVAILED_ITC_CENTRAL_TAX"] = AvailedItcCentralTax;
                        dr["AVAILED_ITC_STATE_UT_TAX"] = AvailedItcStateUtTax;
                        dr["AVAILED_ITC_CESS"] = AvailedItcCess;

                        dr["MONTH"] = month;
                        dr["YEAR"] = year;

                        dtTemp.Rows.Add(dr);
                    }
                }
            }

            // Bind to GridView
            if (dtTemp.Rows.Count > 0)
            {
                DataTable dtGSTINs = new DataTable();
                dtGSTINs.Columns.Add("VALUE", typeof(string));

                foreach (DataRow drg in dtTemp.Rows)
                {
                    DataRow drG = dtGSTINs.NewRow();
                    drG["VALUE"] = drg["GSTIN_OF_SUPPLIER"].ToString();

                    dtGSTINs.Rows.Add(drG);
                }

                DataTable dtVendors = new DataTable();

                if (dtGSTINs != null && dtGSTINs.Rows.Count > 0)
                {
                    dtVendors = objPurchaseBalanceReports.GetVendorByGSTIN(dtGSTINs);
                }

                if (dtVendors != null && dtVendors.Rows.Count > 0)
                {
                    foreach (DataRow b2r in dtTemp.Rows)
                    {
                        string b2GST = b2r["GSTIN_OF_SUPPLIER"].ToString();
                        foreach (DataRow vr in dtVendors.Rows)
                        {
                            if (b2GST == vr["GSTIN"].ToString())
                            {
                                b2r["VENDOR_CODE"] = vr["VENDOR_CODE"];
                                b2r["VENDOR_NAME"] = vr["VENDOR_NAME"];
                            }
                        }
                    }
                }


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

    private void ImportBooks()
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
            dtTemp.Columns.Add("GSTIN_OF_SUPPLIER", typeof(string));
            dtTemp.Columns.Add("INVOICE_NUMBER", typeof(string));
            dtTemp.Columns.Add("INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("INVOICE_VALUE", typeof(decimal));
            dtTemp.Columns.Add("PLACE_OF_SUPPLY", typeof(string));
            dtTemp.Columns.Add("REVERSE_CHARGE", typeof(string));
            dtTemp.Columns.Add("INVOICE_TYPE", typeof(string));
            dtTemp.Columns.Add("RATE", typeof(decimal));
            dtTemp.Columns.Add("TAXABLE_VALUE", typeof(decimal));
            dtTemp.Columns.Add("INTEGRATED_TAX_PAID", typeof(decimal));
            dtTemp.Columns.Add("CENTRAL_TAX_PAID", typeof(decimal));
            dtTemp.Columns.Add("STATE_UT_TAX_PAID", typeof(decimal));
            dtTemp.Columns.Add("CESS_PAID", typeof(decimal));
            dtTemp.Columns.Add("ELIGIBILITY_FOR_ITC", typeof(string));
            dtTemp.Columns.Add("AVAILED_ITC_INTEGRATED_TAX", typeof(decimal));
            dtTemp.Columns.Add("AVAILED_ITC_CENTRAL_TAX", typeof(decimal));
            dtTemp.Columns.Add("AVAILED_ITC_STATE_UT_TAX", typeof(decimal));
            dtTemp.Columns.Add("AVAILED_ITC_CESS", typeof(decimal));
            dtTemp.Columns.Add("MONTH", typeof(string));
            dtTemp.Columns.Add("YEAR", typeof(string));

            #endregion

            bool check = true;

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                Label lblVendorCode = (Label)gr.FindControl("lblVendorCode");
                Label lblVendorName = (Label)gr.FindControl("lblVendorName");
                Label lblGstinOfSupplier = (Label)gr.FindControl("lblGstinOfSupplier");
                Label lblInvoiceNumber = (Label)gr.FindControl("lblInvoiceNumber");
                Label lblInvoiceDate = (Label)gr.FindControl("lblInvoiceDate");
                Label lblInvoiceValue = (Label)gr.FindControl("lblInvoiceValue");
                Label lblPlaceOfSupply = (Label)gr.FindControl("lblPlaceOfSupply");
                Label lblReverseCharge = (Label)gr.FindControl("lblReverseCharge");
                Label lblInvoiceType = (Label)gr.FindControl("lblInvoiceType");
                Label lblRate = (Label)gr.FindControl("lblRate");
                Label lblTaxableValue = (Label)gr.FindControl("lblTaxableValue");
                Label lblIntegratedTaxPaid = (Label)gr.FindControl("lblIntegratedTaxPaid");
                Label lblCentralTaxPaid = (Label)gr.FindControl("lblCentralTaxPaid");
                Label lblStateUtTaxPaid = (Label)gr.FindControl("lblStateUtTaxPaid");
                Label lblCessPaid = (Label)gr.FindControl("lblCessPaid");
                Label lblEligibilityForItc = (Label)gr.FindControl("lblEligibilityForItc");
                Label lblAvailedItcIntegratedTax = (Label)gr.FindControl("lblAvailedItcIntegratedTax");
                Label lblAvailedItcCentralTax = (Label)gr.FindControl("lblAvailedItcCentralTax");
                Label lblAvailedItcStateUtTax = (Label)gr.FindControl("lblAvailedItcStateUtTax");
                Label lblAvailedItcCess = (Label)gr.FindControl("lblAvailedItcCess");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblYear = (Label)gr.FindControl("lblYear");


                DataRow dr = dtTemp.NewRow();

                dr["VENDOR_CODE"] = Convert.ToString(lblVendorCode.Text);
                dr["VENDOR_NAME"] = Convert.ToString(lblVendorName.Text);
                dr["GSTIN_OF_SUPPLIER"] = Convert.ToString(lblGstinOfSupplier.Text);
                dr["INVOICE_NUMBER"] = Convert.ToString(lblInvoiceNumber.Text);
                dr["INVOICE_DATE"] = Convert.ToString(lblInvoiceDate.Text);

                if (!string.IsNullOrEmpty(lblInvoiceValue.Text))
                    dr["INVOICE_VALUE"] = Convert.ToDecimal(lblInvoiceValue.Text);
                else dr["INVOICE_VALUE"] = 0;

                dr["PLACE_OF_SUPPLY"] = Convert.ToString(lblPlaceOfSupply.Text);
                dr["REVERSE_CHARGE"] = Convert.ToString(lblReverseCharge.Text);
                dr["INVOICE_TYPE"] = Convert.ToString(lblInvoiceType.Text);
                
                if (!string.IsNullOrEmpty(lblRate.Text))
                    dr["RATE"] = Convert.ToDecimal(lblRate.Text);
                else dr["RATE"] = 0;

                if (!string.IsNullOrEmpty(lblTaxableValue.Text))
                    dr["TAXABLE_VALUE"] = Convert.ToDecimal(lblTaxableValue.Text);
                else dr["TAXABLE_VALUE"] = 0;

                if (!string.IsNullOrEmpty(lblIntegratedTaxPaid.Text))
                    dr["INTEGRATED_TAX_PAID"] = Convert.ToDecimal(lblIntegratedTaxPaid.Text);
                else dr["INTEGRATED_TAX_PAID"] = 0;

                if (!string.IsNullOrEmpty(lblCentralTaxPaid.Text))
                    dr["CENTRAL_TAX_PAID"] = Convert.ToDecimal(lblCentralTaxPaid.Text);
                else dr["CENTRAL_TAX_PAID"] = 0;

                if (!string.IsNullOrEmpty(lblStateUtTaxPaid.Text))
                    dr["STATE_UT_TAX_PAID"] = Convert.ToDecimal(lblStateUtTaxPaid.Text);
                else dr["STATE_UT_TAX_PAID"] = 0;

                if (!string.IsNullOrEmpty(lblCessPaid.Text))
                    dr["CESS_PAID"] = Convert.ToDecimal(lblCessPaid.Text);
                else dr["CESS_PAID"] = 0;

                dr["ELIGIBILITY_FOR_ITC"] = Convert.ToString(lblEligibilityForItc.Text);

                if (!string.IsNullOrEmpty(lblAvailedItcIntegratedTax.Text))
                    dr["AVAILED_ITC_INTEGRATED_TAX"] = Convert.ToDecimal(lblAvailedItcIntegratedTax.Text);
                else dr["AVAILED_ITC_INTEGRATED_TAX"] = 0;


                if (!string.IsNullOrEmpty(lblAvailedItcCentralTax.Text))
                    dr["AVAILED_ITC_CENTRAL_TAX"] = Convert.ToDecimal(lblAvailedItcCentralTax.Text);
                else dr["AVAILED_ITC_CENTRAL_TAX"] = 0;


                if (!string.IsNullOrEmpty(lblAvailedItcStateUtTax.Text))
                    dr["AVAILED_ITC_STATE_UT_TAX"] = Convert.ToDecimal(lblAvailedItcStateUtTax.Text);
                else dr["AVAILED_ITC_STATE_UT_TAX"] = 0;

                if (!string.IsNullOrEmpty(lblAvailedItcCess.Text))
                    dr["AVAILED_ITC_CESS"] = Convert.ToDecimal(lblAvailedItcCess.Text);
                else dr["AVAILED_ITC_CESS"] = 0;

                dr["MONTH"] = Convert.ToString(lblMonth.Text);
                dr["YEAR"] = Convert.ToString(lblYear.Text);

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
                value = objPurchaseBalanceReports.ImportBooksB2B(dtTemp, year, month);
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
