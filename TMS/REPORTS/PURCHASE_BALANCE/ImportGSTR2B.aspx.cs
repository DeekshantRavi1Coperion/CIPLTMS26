using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

public partial class REPORTS_PURCHASE_BALANCE_ImportGSTR2B : System.Web.UI.Page
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
            ImportRecords();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    protected void btnViewGSTR2BList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/REPORTS/PURCHASE_BALANCE/ImportedGSTR2BCleanList.aspx");
    }





    #endregion


    #region METHODS[=========================]

    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv = "GSTIN_OF_SUPPLIER" + ',';
            csv += "TRADE_OR_LEGAL_NAME" + ',';
            csv += "INVOICE_NO" + ',';
            csv += "INVOICE_DATE (yyyy-MM-dd)" + ',';
            csv += "INVOICE_VALUE" + ',';
            csv += "TAXABLE_VALUE" + ',';

            csv += "\r\n";

            string fileName = "GSTR2B_" + DateTime.Now;
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
           
            // Create DataTable structure
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("SR_NO", typeof(string));
            dtTemp.Columns.Add("GSTIN_OF_SUPPLIER", typeof(string));
            dtTemp.Columns.Add("TRADE_OR_LEGAL_NAME", typeof(string));
            dtTemp.Columns.Add("INVOICE_NO", typeof(string));
            dtTemp.Columns.Add("INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("INVOICE_VALUE", typeof(string));
            dtTemp.Columns.Add("TAXABLE_VALUE", typeof(string));
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

                        string gstinOfSupplier = GetSafeValue(csv, 0).ToUpper();
                        string tradeOrLegalName = GetSafeValue(csv, 1);
                        string invoiceNo = GetSafeValue(csv, 2);
                        string rawDate = GetSafeValue(csv, 3).ToUpper();
                        string invoiceValue = GetSafeValue(csv, 4).ToUpper();
                        string taxableValue = GetSafeValue(csv, 5);

                        if (string.IsNullOrEmpty(invoiceValue)) invoiceValue = "0";
                        if (string.IsNullOrEmpty(taxableValue)) taxableValue = "0";

                        // Format date (from dd/MM/yyyy or dd-MM-yyyy → yyyy-MM-dd)
                        string formattedDate = "";
                        if (!string.IsNullOrEmpty(rawDate))
                        {
                            if (rawDate.Contains("/"))
                            {
                                string[] parts1 = rawDate.Split('/');
                                if (parts1.Length == 3)
                                    formattedDate = parts1[2] + "-" + parts1[0] + "-" + parts1[1];
                            }
                            else if (rawDate.Contains("-"))
                            {
                                string[] parts2 = rawDate.Split('-');
                                if (parts2.Length == 3)
                                    formattedDate = parts2[0] + "-" + parts2[1] + "-" + parts2[2];
                            }

                            //string[] parts = rawDate.Contains("/") ? rawDate.Split('/') : rawDate.Split('-');
                            //if (parts.Length == 3)
                            //    formattedDate = parts[2] + "-" + parts[1] + "-" + parts[0];
                        }

                        dr["SR_NO"] = count;
                        dr["GSTIN_OF_SUPPLIER"] = gstinOfSupplier;
                        dr["TRADE_OR_LEGAL_NAME"] = tradeOrLegalName;
                        dr["INVOICE_NO"] = invoiceNo;
                        dr["INVOICE_DATE"] = formattedDate;
                        dr["INVOICE_VALUE"] = invoiceValue;
                        dr["TAXABLE_VALUE"] = taxableValue;
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

    private void ImportRecords()
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

            dtTemp.Columns.Add("GSTIN_OF_SUPPLIER", typeof(string));
            dtTemp.Columns.Add("TRADE_OR_LEGAL_NAME", typeof(string));
            dtTemp.Columns.Add("INVOICE_NO", typeof(string));
            dtTemp.Columns.Add("INVOICE_DATE", typeof(string));
            dtTemp.Columns.Add("INVOICE_VALUE", typeof(string));
            dtTemp.Columns.Add("TAXABLE_VALUE", typeof(string));
            dtTemp.Columns.Add("MONTH", typeof(string));
            dtTemp.Columns.Add("YEAR", typeof(string));

            #endregion


            bool check = true;

            foreach (GridViewRow gr in gvDesignDetails.Rows)
            {
                Label lblGstinOfSupplier = (Label)gr.FindControl("lblGstinOfSupplier");
                Label lblTradeOrLegalName = (Label)gr.FindControl("lblTradeOrLegalName");
                Label lblInvoiceNo = (Label)gr.FindControl("lblInvoiceNo");
                Label lblInvoiceDate = (Label)gr.FindControl("lblInvoiceDate");
                Label lblInvoiceValue = (Label)gr.FindControl("lblInvoiceValue");
                Label lblTaxableValue = (Label)gr.FindControl("lblTaxableValue");
                Label lblMonth = (Label)gr.FindControl("lblMonth");
                Label lblYear = (Label)gr.FindControl("lblYear");

                DataRow dr = dtTemp.NewRow();
                dr["GSTIN_OF_SUPPLIER"] = lblGstinOfSupplier.Text;
                dr["TRADE_OR_LEGAL_NAME"] = lblTradeOrLegalName.Text;
                dr["INVOICE_NO"] = lblInvoiceNo.Text;
                dr["INVOICE_DATE"] = lblInvoiceDate.Text;
                dr["INVOICE_VALUE"] = lblInvoiceValue.Text;
                dr["TAXABLE_VALUE"] = lblTaxableValue.Text;
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
                value = objPurchaseBalanceReports.ImportGSTR2B(dtTemp, year, month);
            }

            if (value > 0)
            {
                cleanRecords = value;
                //gvDesignDetails.DataSource = null;
                //gvDesignDetails.DataBind();
                SuccessMessage(totalRecords + " total record(s) imported successfully!");
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
