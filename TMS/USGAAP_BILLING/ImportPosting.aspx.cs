using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class USGAAP_BILLING_ImportPosting : System.Web.UI.Page
{
    BAL.Posting objPosting = new BAL.Posting();
    DataTable dtCSVCostCenter = new DataTable();
    DataTable dtTempCostCenter = new DataTable();
    DataSet dsUnit = new DataSet();
    DataSet dsCostCenterStatus = new DataSet();
    DataSet dsCostCenter = new DataSet();
    int existedRecrdsCount = 0;

    int unitId = 0;
    string unitName = string.Empty;
    string costCenterCodeText = string.Empty;
    string type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                Session["dtCostCenterStatus"] = null;
                Session["DS_COSTCENTER"] = null;
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

    protected void btnGetCostCenterFile_Click(object sender, EventArgs e)
    {
        GetCostCenterFile();
    }

    private void GetCostCenterFile()
    {
        try
        {
            #region CREATE_TABLE
            dtCSVCostCenter.Columns.Add("INVOICE_NUMBER", typeof(string));
            dtCSVCostCenter.Columns.Add("COMPANY", typeof(string));
            dtCSVCostCenter.Columns.Add("INVOICE_DATE", typeof(string));
            dtCSVCostCenter.Columns.Add("JOB_NO", typeof(string));
            dtCSVCostCenter.Columns.Add("CUSTOMER_NAME", typeof(string));
            dtCSVCostCenter.Columns.Add("CUSTOMER_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("CUSTOMER_ADDRESS", typeof(string));
            dtCSVCostCenter.Columns.Add("BUSINESS_UNIT", typeof(string));
            dtCSVCostCenter.Columns.Add("PRODUCT_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("REVENUE_ACCOUNT", typeof(string));
            dtCSVCostCenter.Columns.Add("REVENUE_ACCOUNT_DESC", typeof(string));
            dtCSVCostCenter.Columns.Add("REVENUE_ACCOUNT_TYPE", typeof(string));
            dtCSVCostCenter.Columns.Add("QUANTITY", typeof(string));
            dtCSVCostCenter.Columns.Add("PRODUCT_RATE", typeof(string));
            dtCSVCostCenter.Columns.Add("INVOICE_AMOUNT", typeof(string));
            dtCSVCostCenter.Columns.Add("POSTED_VALUE", typeof(string));
            dtCSVCostCenter.Columns.Add("UNPOSTED_VALUE", typeof(string));
            dtCSVCostCenter.Columns.Add("END_MARKET", typeof(string));
            dtCSVCostCenter.Columns.Add("GEOGRAPHY", typeof(string));
            dtCSVCostCenter.Columns.Add("POSTING_MONTH", typeof(string));
            dtCSVCostCenter.Columns.Add("POSTING_YEAR", typeof(string));
            dtCSVCostCenter.Columns.Add("TYPE", typeof(string));
            dtCSVCostCenter.Columns.Add("REVENUE/NOT REVENUE TYPE", typeof(string));
            dtCSVCostCenter.Columns.Add("REV_REC_REDUCTION", typeof(string));
            dtCSVCostCenter.Columns.Add("UDF2", typeof(string));
            dtCSVCostCenter.Columns.Add("UDF3", typeof(string));
            dtCSVCostCenter.Columns.Add("UDF4", typeof(string));
            dtCSVCostCenter.Columns.Add("UDF5", typeof(string));
            dtCSVCostCenter.Columns.Add("CREATED_BY", typeof(string));

            #endregion

            if (fileUploadCostCenter.HasFile)
            {
                using (StreamReader myReader = new StreamReader(fileUploadCostCenter.PostedFile.InputStream))
                {
                    string csvData = myReader.ReadToEnd();
                    string[] lines = csvData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        if (line.Contains("INVOICE_NUMBER")) // Skip header
                            continue;

                        string[] cells = line.Split(',');
                        DataRow newRow = dtCSVCostCenter.NewRow();

                        for (int i = 0; i < dtCSVCostCenter.Columns.Count; i++)
                        {
                            newRow[i] = i < cells.Length ? cells[i].Trim() : string.Empty;
                        }

                        dtCSVCostCenter.Rows.Add(newRow);
                    }
                }

                gvCostCenter.DataSource = dtCSVCostCenter;
                gvCostCenter.DataBind();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvCostCenter.Rows.Count > 0)
        {
            ImportCostCenterFile();
        }
        else
        {
            ExceptionMessage("No data found!");
            return;
        }
    }

    private void ImportCostCenterFile()
    {
        try
        {
            string updateQuery = string.Empty;
            int costCenterID = 0;
            int srNo = 0;
            int unitID = 0;
            int statusID = 0;
            string srNoForRemoval = string.Empty;
            int count = 0;

            string Company = string.Empty;
            string InvoiceNumber = string.Empty;
           //string InvoiceDate = string.Empty;
            DateTime? InvoiceDate = null;
            string JobNo = string.Empty;
            string CustomerName = string.Empty;
            string CustomerCode = string.Empty;
            string CustomerAddress = string.Empty;
            string BusinessUnit = string.Empty;
            string ProductCode = string.Empty;
            string RevenueAccount = string.Empty;
            string RevenueAccountDesc = string.Empty;
            string RevenueAccountType = string.Empty;
            int RevenueAccountTypeInt = 0;
            string Quantity = string.Empty;
            string ProductRate = string.Empty;
            string InvoiceAmount = string.Empty;
            string PostedValue = string.Empty;
            string UnpostedValue = string.Empty;
            string EndMarket = string.Empty;

            string Geography = string.Empty;
            int PostingMonth = 0;

            //string PostingMonth = string.Empty;
            //string PostingYear = string.Empty;
            string Type = string.Empty;
            string RevNonRevType = string.Empty;

            string RevRecReduction = string.Empty;
            string udf2 = string.Empty;
            string udf3 = string.Empty;
            string udf4 = string.Empty;
            string udf5 = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            DataTable dtPosting = new DataTable();

            dtPosting.Columns.Add("INVOICE_NUMBER", typeof(string));
            dtPosting.Columns.Add("COMPANY", typeof(string));
            dtPosting.Columns.Add("INVOICE_DATE", typeof(DateTime));
            dtPosting.Columns.Add("JOB_NO", typeof(string));
            dtPosting.Columns.Add("CUSTOMER_NAME", typeof(string));
            dtPosting.Columns.Add("CUSTOMER_CODE", typeof(string));
            dtPosting.Columns.Add("CUSTOMER_ADDRESS", typeof(string));
            dtPosting.Columns.Add("BUSINESS_UNIT", typeof(string));
            dtPosting.Columns.Add("PRODUCT_CODE", typeof(string));
            dtPosting.Columns.Add("REVENUE_ACCOUNT", typeof(string));
            dtPosting.Columns.Add("REVENUE_ACCOUNT_DESC", typeof(string));
            dtPosting.Columns.Add("REVENUE_ACCOUNT_TYPE", typeof(string));

            dtPosting.Columns.Add("QUANTITY", typeof(decimal));
            dtPosting.Columns.Add("PRODUCT_RATE", typeof(decimal));
            dtPosting.Columns.Add("INVOICE_AMOUNT", typeof(decimal));
            dtPosting.Columns.Add("POSTED_VALUE", typeof(decimal));
            dtPosting.Columns.Add("UNPOSTED_VALUE", typeof(decimal));

            dtPosting.Columns.Add("END_MARKET", typeof(string));
            dtPosting.Columns.Add("GEOGRAPHY", typeof(string));
            dtPosting.Columns.Add("POSTING_MONTH", typeof(int));
            dtPosting.Columns.Add("POSTING_YEAR", typeof(int));
            dtPosting.Columns.Add("TYPE", typeof(string));
            dtPosting.Columns.Add("REVENUE/NOT REVENUE TYPE", typeof(string));
            dtPosting.Columns.Add("REV_REC_REDUCTION", typeof(string));
            dtPosting.Columns.Add("UDF2", typeof(string));
            dtPosting.Columns.Add("UDF3", typeof(string));
            dtPosting.Columns.Add("UDF4", typeof(string));
            dtPosting.Columns.Add("UDF5", typeof(string));
            dtPosting.Columns.Add("CREATED_BY", typeof(string));


            int value = 0;
            foreach (GridViewRow gr in gvCostCenter.Rows)
            {
                DataRow dr = dtPosting.NewRow();
                bool rowIsValid = true; // <-- flag to track errors in this row

                Label lblCompany = (Label)gr.FindControl("lblCompany");
                Label lblInvoiceNumber = (Label)gr.FindControl("lblInvoiceNumber");
                Label lblInvoiceDate = (Label)gr.FindControl("lblInvoiceDate");
                Label lblJobNo = (Label)gr.FindControl("lblJobNo");
                Label lblCustomerName = (Label)gr.FindControl("lblCustomerName");
                Label lblCustomerCode = (Label)gr.FindControl("lblCustomerCode");
                Label lblCustomerAddress = (Label)gr.FindControl("lblCustomerAddress");
                Label lblBusinessUnit = (Label)gr.FindControl("lblBusinessUnit");
                Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                Label lblRevenueAccount = (Label)gr.FindControl("lblRevenueAccount");
                Label lblRevenueAccountDesc = (Label)gr.FindControl("lblRevenueAccountDesc");
                Label lblRevenueAccountType = (Label)gr.FindControl("lblRevenueAccountType");

                Label lblQuantity = (Label)gr.FindControl("lblQuantity");
                Label lblProductRate = (Label)gr.FindControl("lblProductRate");
                Label lblInvoiceAmount = (Label)gr.FindControl("lblInvoiceAmount");
                Label lblPostedValue = (Label)gr.FindControl("lblPostedValue");
                Label lblUnpostedValue = (Label)gr.FindControl("lblUnpostedValue");

                Label lblEndMarket = (Label)gr.FindControl("lblEndMarket");
                Label lblGeography = (Label)gr.FindControl("lblGeography");
                Label lblPostingMonth = (Label)gr.FindControl("lblPostingMonth");
                Label lblPostingYear = (Label)gr.FindControl("lblPostingYear");
                Label lblType = (Label)gr.FindControl("lblType");

                Label lblRevNonRevType = (Label)gr.FindControl("lblRevNonRevType");
                Label lblRevRecReduction = (Label)gr.FindControl("lblRevRecReduction");
                Label lbludf2 = (Label)gr.FindControl("lbludf2");
                Label lbludf3 = (Label)gr.FindControl("lbludf3");
                Label lbludf4 = (Label)gr.FindControl("lbludf4");
                Label lbludf5 = (Label)gr.FindControl("lbludf5");

                if (!string.IsNullOrEmpty(Convert.ToString(lblInvoiceNumber.Text)))
                    InvoiceNumber = Convert.ToString(lblInvoiceNumber.Text);
                else
                    InvoiceNumber = string.Empty;

                if(!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                {
                    string jobno = Convert.ToString(lblJobNo.Text);
                    int check = 0;
                    check = objPosting.CheckJobNo(jobno);
                    if (check > 0)
                    {
                        JobNo = Convert.ToString(lblJobNo.Text);
                    }
                    else
                    {
                        //ExceptionMessage(string.Format("This Job Number " + jobno + " does not exist in the FACT"));
                        //rowIsValid = false;
                        throw new Exception(string.Format(
                        "Invalid Job Number '{0}' in row {1}. Job does not exist in FACT. Import aborted.",
                        jobno, gr.RowIndex + 1
                       ));

                    }

                }
                else {
                    //ExceptionMessage(string.Format("This Job Number does not exist in the FACT"));
                    //rowIsValid = false;
                    throw new Exception(string.Format(
                    "Job Number is missing in row {0}. Import aborted.",
                    gr.RowIndex + 1
                    ));

                }

                string[] validCompanies = { "A35", "GNU", "USGAAP", "CID2527", "USGAAPN2527", "CID_INDIANGAAP2527" };
                string labelValue = Convert.ToString(lblCompany.Text);

                if (!string.IsNullOrEmpty(labelValue))
                {
                    string companyValue = labelValue.ToUpper();

                    if (validCompanies.Contains(companyValue))
                    {
                        Company = labelValue; // valid
                    }
                    else
                    {
                    throw new Exception(string.Format( "Invalid company name '{0}' in row {1}. Must be one of: {2}. Import aborted.",
                     labelValue, gr.RowIndex + 1, string.Join(", ", validCompanies)
                    ));
                    }
                }
                else
                {   throw new Exception(string.Format(
                        "Company name is missing in row {0}. Import aborted.",
                        gr.RowIndex + 1
                    ));
                    rowIsValid = false;
                }

                //if (!string.IsNullOrEmpty(Convert.ToString(lblInvoiceDate.Text)))
                //    InvoiceDate = Convert.ToString(lblInvoiceDate.Text);
                //else
                //    InvoiceDate = string.Empty;

                if (!string.IsNullOrEmpty(lblInvoiceDate.Text))
                {
                    InvoiceDate = DateTime.Parse(lblInvoiceDate.Text); // works with "2025-11-30"
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblJobNo.Text)))
                    JobNo = Convert.ToString(lblJobNo.Text);
                else
                    JobNo = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerName.Text)))
                {
                    string customerName = Convert.ToString(lblCustomerName.Text);
                    int check = 0;
                    check = objPosting.CheckCustomerName(customerName);
                    if (check > 0)
                    {
                        CustomerName = Convert.ToString(lblCustomerName.Text);
                    }
                    else
                    {
                       throw new Exception(string.Format("Invalid Customer Name '{0}' found in row {1}. Import aborted.", customerName, gr.RowIndex + 1));
                    }

                }
                else
                {
                      throw new Exception(string.Format("Customer name is missing in row {0}. Import aborted.", gr.RowIndex + 1));

                }
             
                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerCode.Text)))
                {
                    string customercd = Convert.ToString(lblCustomerCode.Text);
                    int check = 0;
                    check = objPosting.CheckCustomerCode(customercd);
                    if (check > 0)
                    {
                        CustomerCode = Convert.ToString(lblCustomerCode.Text);
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid Customer Code '{0}' found in row {1}. Import aborted.", customercd, gr.RowIndex + 1));
                    }

                }
                else
                {
                    throw new Exception(string.Format("Customer Code is missing in row {0}. Import aborted.", gr.RowIndex + 1));

                }


                if (!string.IsNullOrEmpty(Convert.ToString(lblCustomerAddress.Text)))
                    CustomerAddress = Convert.ToString(lblCustomerAddress.Text);
                else
                    CustomerAddress = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblBusinessUnit.Text)))
                    BusinessUnit = Convert.ToString(lblBusinessUnit.Text);
                else
                    BusinessUnit = string.Empty;

                //if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text)))
                //    ProductCode = Convert.ToString(lblProductCode.Text);
                //else
                //    ProductCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text)))
                {
                    string productCode = Convert.ToString(lblProductCode.Text);
                    int check = 0;
                    check = objPosting.CheckProductCode(productCode);
                    if (check > 0)
                    {
                        ProductCode = Convert.ToString(lblProductCode.Text);
                    }
                    else
                    {
                        //throw new Exception(string.Format("Invalid Product code '{0}' found in row {1}. Import aborted.", productCode, gr.RowIndex + 1));
                        ProductCode = "";
                    }

                }
                else
                {
                    //throw new Exception(string.Format("Product code is missing in row {0}. Import aborted.", gr.RowIndex + 1));
                    ProductCode = "";

                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblRevenueAccount.Text)))
                    RevenueAccount = Convert.ToString(lblRevenueAccount.Text);
                else
                    RevenueAccount = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblRevenueAccountDesc.Text)))
                    RevenueAccountDesc = Convert.ToString(lblRevenueAccountDesc.Text);
                else
                    RevenueAccountDesc = string.Empty;

                //if (!string.IsNullOrEmpty(Convert.ToString(lblRevenueAccountType.Text)))
                //    RevenueAccountType = Convert.ToString(lblRevenueAccountType.Text);
                //else
                //    RevenueAccountType = string.Empty;

                string revAccType = Convert.ToString(lblRevenueAccountType.Text);

                if (!string.IsNullOrEmpty(lblRevenueAccountType.Text))
                {
                    if (revAccType == "P")
                        RevenueAccountTypeInt = 0;
                    else
                        RevenueAccountTypeInt = 1;
                }
                else
                    RevenueAccountTypeInt = 1;

                if (!string.IsNullOrEmpty(Convert.ToString(lblQuantity.Text)))
                    Quantity = Convert.ToString(lblQuantity.Text);
                else
                    Quantity = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblProductRate.Text)))
                    ProductRate = Convert.ToString(lblProductRate.Text);
                else
                    ProductRate = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblInvoiceAmount.Text)))
                    InvoiceAmount = Convert.ToString(lblInvoiceAmount.Text);
                else
                    InvoiceAmount = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(lblPostedValue.Text)))
                    PostedValue = Convert.ToString(lblPostedValue.Text);
                else
                    PostedValue = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblUnpostedValue.Text)))
                    UnpostedValue = Convert.ToString(lblUnpostedValue.Text);
                else
                    UnpostedValue = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblEndMarket.Text)))
                {
                    string endMarket = Convert.ToString(lblEndMarket.Text).Trim();
                    int endMarketCode = objPosting.GetEndMarketCodeByName(endMarket);

                    if (endMarketCode > 0)
                        EndMarket = endMarketCode.ToString();
                    else
                    {
                        ExceptionMessage("Invalid End Market: " + endMarket + " (not found in master table)");
                        EndMarket = string.Empty;
                    }
                }
                else
                {
                    EndMarket = string.Empty;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblGeography.Text)))
                {
                    string geographyName = Convert.ToString(lblGeography.Text).Trim();
                    int geographyCode = objPosting.GetCountryCodeByName(geographyName);

                    if (geographyCode > 0)
                        Geography = geographyCode.ToString();
                    else
                    {
                        ExceptionMessage("Invalid Geography: " + geographyName + " (not found in master table)");
                        Geography = string.Empty;
                    }
                }
                else
                {
                    Geography = string.Empty;
                }


                //if (!string.IsNullOrEmpty(Convert.ToString(lblPostingMonth.Text)))
                //    PostingMonth = Convert.ToString(lblPostingMonth.Text);
                //else
                //    PostingMonth = string.Empty;

                if (!string.IsNullOrEmpty(lblPostingMonth.Text))
                {
                    int.TryParse(lblPostingMonth.Text, out PostingMonth);
                }


                //if (!string.IsNullOrEmpty(Convert.ToString(lblPostingYear.Text)))
                //    PostingYear = Convert.ToString(lblPostingYear.Text);
                //else
                //    PostingYear = string.Empty;

                int? PostingYear = null;

                if (!string.IsNullOrEmpty(lblPostingYear.Text))
                {
                    int temp;
                    if (int.TryParse(lblPostingYear.Text, out temp))
                    {
                        PostingYear = temp;
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblType.Text)))
                {
                    //string type = Convert.ToString(lblType.Text).Trim();
                    type = Convert.ToString(lblType.Text).Trim();

                    int typeCode = objPosting.GetTypeByName(type);

                    if (typeCode > 0)
                        Type = typeCode.ToString();
                    else
                    {
                        ExceptionMessage("Invalid Type: " + type + " (not found in master table)");
                        Type = string.Empty;
                    }
                }
                else
                {
                    Type = string.Empty;
                }


                if (!string.IsNullOrEmpty(Convert.ToString(lblRevNonRevType.Text)))
                {
                    string RevNonRevSubtype = Convert.ToString(lblRevNonRevType.Text).Trim();
                    int RevNotRevtypeCode = 0;

                    //int RevNonRevSubtype = 

                    if (type == "Revenue")
                    {
                        RevNotRevtypeCode = objPosting.GetRevenueTypeByName(type, RevNonRevSubtype);
                    }
                    else if(type == "Not Revenue")
                    {
                        RevNotRevtypeCode = objPosting.GetRevenueTypeByName(type, RevNonRevSubtype);
                    }
                    else
                    {
                        throw new Exception(string.Format("Wrong Revenue/Not Revenue type at", gr.RowIndex + 1));
                    }


                    RevNonRevType = RevNotRevtypeCode.ToString();
                }
                else
                    RevNonRevType = string.Empty;


                if (!string.IsNullOrEmpty(Convert.ToString(lblRevRecReduction.Text)))
                    RevRecReduction = Convert.ToString(lblRevRecReduction.Text);
                else
                    RevRecReduction = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lbludf2.Text)))
                    udf2 = Convert.ToString(lbludf2.Text);
                else
                    udf2 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lbludf3.Text)))
                    udf3 = Convert.ToString(lbludf3.Text);
                else
                    udf3 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lbludf2.Text)))
                    udf4 = Convert.ToString(lbludf4.Text);
                else
                    udf4 = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lbludf2.Text)))
                    udf5 = Convert.ToString(lbludf5.Text);
                else
                    udf5 = string.Empty;

                if (!string.IsNullOrEmpty(InvoiceNumber) && rowIsValid)
                {
                    count++;
                    srNoForRemoval += srNo + ",";

                    dr["INVOICE_NUMBER"] = InvoiceNumber;
                    dr["COMPANY"] = Company;
                    dr["INVOICE_DATE"] = InvoiceDate;
                    dr["JOB_NO"] = JobNo;
                    dr["CUSTOMER_NAME"] = CustomerName;
                    dr["CUSTOMER_CODE"] = CustomerCode;
                    dr["CUSTOMER_ADDRESS"] = CustomerAddress;
                    dr["BUSINESS_UNIT"] = BusinessUnit;
                    dr["PRODUCT_CODE"] = ProductCode;
                    dr["REVENUE_ACCOUNT"] = RevenueAccount;
                    dr["REVENUE_ACCOUNT_DESC"] = RevenueAccountDesc;
                    //dr["REVENUE_ACCOUNT_TYPE"] = RevenueAccountType;
                    dr["REVENUE_ACCOUNT_TYPE"] = RevenueAccountTypeInt;
                    //dr["QUANTITY"] = Quantity;
                    //dr["PRODUCT_RATE"] = ProductRate;
                    //dr["INVOICE_AMOUNT"] = InvoiceAmount;
                    //dr["POSTED_VALUE"] = PostedValue;
                    //dr["UNPOSTED_VALUE"] = UnpostedValue;
                    //dr["QUANTITY"] = string.IsNullOrWhiteSpace(Quantity) ? 0 : Convert.ToDecimal(Quantity);
                    //dr["PRODUCT_RATE"] = string.IsNullOrWhiteSpace(ProductRate) ? 0 : Convert.ToDecimal(ProductRate);
                    //dr["INVOICE_AMOUNT"] = string.IsNullOrWhiteSpace(InvoiceAmount) ? 0 : Convert.ToDecimal(InvoiceAmount);
                    //dr["POSTED_VALUE"] = string.IsNullOrWhiteSpace(PostedValue) ? 0 : Convert.ToDecimal(PostedValue);
                    //dr["UNPOSTED_VALUE"] = string.IsNullOrWhiteSpace(UnpostedValue) ? 0 : Convert.ToDecimal(UnpostedValue);

                    dr["QUANTITY"] = string.IsNullOrEmpty(Quantity.Trim()) ? 0 : Convert.ToDecimal(Quantity);
                    dr["PRODUCT_RATE"] = string.IsNullOrEmpty(ProductRate.Trim()) ? 0 : Convert.ToDecimal(ProductRate);
                    dr["INVOICE_AMOUNT"] = string.IsNullOrEmpty(InvoiceAmount.Trim()) ? 0 : Convert.ToDecimal(InvoiceAmount);
                    dr["POSTED_VALUE"] = string.IsNullOrEmpty(PostedValue.Trim()) ? 0 : Convert.ToDecimal(PostedValue);
                    dr["UNPOSTED_VALUE"] = string.IsNullOrEmpty(UnpostedValue.Trim()) ? 0 : Convert.ToDecimal(UnpostedValue);


                    dr["END_MARKET"] = EndMarket;
                    dr["GEOGRAPHY"] = Geography;
                    dr["POSTING_MONTH"] = PostingMonth;
                    dr["POSTING_YEAR"] = PostingYear;
                    dr["TYPE"] = Type;
                    dr["REVENUE/NOT REVENUE TYPE"] = RevNonRevType;
                    dr["REV_REC_REDUCTION"] = RevRecReduction;
                    dr["UDF2"] = udf2;
                    dr["UDF3"] = udf3;
                    dr["UDF4"] = udf4;
                    dr["UDF5"] = udf5;
                    dr["CREATED_BY"] = createdBy;

                    dtPosting.Rows.Add(dr);

                }

            }

            value = objPosting.ImportPosting(dtPosting);

            //if (value > 0)
            if (value == 1)
            {
                SuccessMessage(count + " Records Imported successfully.");
                if (!string.IsNullOrEmpty(srNoForRemoval))
                    srNoForRemoval = srNoForRemoval.TrimEnd(',');

                if (!string.IsNullOrEmpty(srNoForRemoval))
                    RemoveAndBind(srNoForRemoval);
                else
                {
                    gvCostCenter.DataSource = null;
                    gvCostCenter.DataBind();
                    lblRecords.Text = "Records[" + gvCostCenter.Rows.Count + "]";
                }
            }
            else
            {
                ExceptionMessage("You have entered wrong data");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void DownloadFormat()
    {
        try
        {
            string csv = string.Empty;

            csv += "INVOICE_NUMBER" + ',';
            csv += "COMPANY" + ',';
            csv += "INVOICE_DATE" + ',';
            csv += "JOB_NO" + ',';
            csv += "CUSTOMER_NAME" + ',';
            csv += "CUSTOMER_CODE" + ',';
            csv += "CUSTOMER_ADDRESS" + ',';
            csv += "BUSINESS_UNIT" + ',';
            csv += "PRODUCT_CODE" + ',';
            csv += "REVENUE_ACCOUNT" + ',';
            csv += "REVENUE_ACCOUNT_DESC" + ',';
            csv += "REVENUE_ACCOUNT_TYPE" + ',';
            csv += "QUANTITY" + ',';
            csv += "PRODUCT_RATE" + ',';
            csv += "INVOICE_AMOUNT" + ',';
            csv += "POSTED_VALUE" + ',';
            csv += "UNPOSTED_VALUE" + ',';
            csv += "END_MARKET" + ',';
            csv += "GEOGRAPHY" + ',';
            csv += "POSTING_MONTH" + ',';
            csv += "POSTING_YEAR" + ',';
            csv += "TYPE" + ',';
            csv += "REVENUE/NOT REVENUE TYPE" + ',';
            csv += "REV_REC_REDUCTION" + ',';
            csv += "UDF2" + ',';
            csv += "UDF3" + ',';
            csv += "UDF4" + ',';
            csv += "UDF5" + ',';
            csv += "\r\n";

            string fileName = "Import_Posting_Format";
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

    private void RemoveAndBind(string srNoForRemoval)
    {
        try
        {
            dtTempCostCenter = (DataTable)Session["DS_COSTCENTER"];
            //string[] strNoForRemoval = srNoForRemoval.Split(',');
            //foreach (string item in strNoForRemoval)
            //{
            //    foreach (DataRow dr in dtTempCostCenter.Select("SR_NO='" + item + "'"))
            //    {
            //        dtTempCostCenter.Rows.Remove(dr);
            //    }
            //}
            //if (dtTempCostCenter.Rows.Count > 0)
            //{
            //    gvCostCenter.DataSource = dtTempCostCenter;
            //    gvCostCenter.DataBind();
            //}
            //else
            //{
            //    gvCostCenter.DataSource = null;
            //    gvCostCenter.DataBind();
            //}
            //lblRecords.Text = "Records[" + gvCostCenter.Rows.Count + "]";
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
}