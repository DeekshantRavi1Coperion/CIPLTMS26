using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
public partial class REPORTS_GLDataDumpReport : System.Web.UI.Page
{
    BAL.Posting objPosting = new BAL.Posting();
    DataTable dtCSVCostCenter = new DataTable();
    DataTable dtTempCostCenter = new DataTable();
    DataSet dtgetReport = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCostCenterStatus = new DataSet();
    DataSet dsCostCenter = new DataSet();
    int existedRecrdsCount = 0;

    int unitId = 0;
    string unitName = string.Empty;
    string costCenterCodeText = string.Empty;
    string type = string.Empty;
    string PLBS = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindPLBS();
                //gvCostCenter.Columns[4].Visible = false; // PL / BS TYPE
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


    protected void btnGetGLDataDumpReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPLBS.SelectedIndex >= 0)
            {
                PLBS = ddlPLBS.SelectedValue.ToString();
            }
            else
            {
                PLBS = "All";
            }


            dtgetReport = objPosting.GetGLDataDumpReport(PLBS);
            gvCostCenter.Columns
            .Cast<DataControlField>()
            .First(c => c.HeaderText == "PL / BS TYPE")
            .Visible = true;
            Session["DS_COSTCENTER"] = dtgetReport.Tables[0];

            gvCostCenter.DataSource = dtgetReport.Tables[0];
            gvCostCenter.DataBind();

            lblRecords.Text = "Records[" + dtgetReport.Tables[0].Rows.Count + "]";

        }
        catch(Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindPLBS()
    {
        try
        {
            ddlPLBS.Items.Clear();
            ddlPLBS.Items.Add(new ListItem("All", "All"));
            ddlPLBS.Items.Add(new ListItem("P", "P"));
            ddlPLBS.Items.Add(new ListItem("B", "B"));
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
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
            dtCSVCostCenter.Columns.Add("COMPANY_INITIALS", typeof(string));
            dtCSVCostCenter.Columns.Add("DOCUMENT_TYPE", typeof(string));
            dtCSVCostCenter.Columns.Add("DOCUMENT_NUMBER", typeof(string));
            dtCSVCostCenter.Columns.Add("DOCUMENT_DATE", typeof(string));
            dtCSVCostCenter.Columns.Add("VOUCHER_NUMBER", typeof(string));
            dtCSVCostCenter.Columns.Add("GENERAL_LEDGER_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("GENERAL_LEDGER_DESCRIPTION", typeof(string));
            dtCSVCostCenter.Columns.Add("CUSTOMER_VENDOR_COST_CENTER_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("CUSTOMER_VENDOR_COST_CENTER_NAME", typeof(string));
            dtCSVCostCenter.Columns.Add("PRODUCT_CODE", typeof(string));
            dtCSVCostCenter.Columns.Add("PRODUCT_DESCRIPTION", typeof(string));
            dtCSVCostCenter.Columns.Add("NET_AMOUNT_INR", typeof(string));
            dtCSVCostCenter.Columns.Add("PL_OR_BS_TYPE", typeof(string));
            


            #endregion

            if (fileUploadCostCenter.HasFile)
            {
                using (StreamReader myReader = new StreamReader(fileUploadCostCenter.PostedFile.InputStream))
                {
                    string csvData = myReader.ReadToEnd();
                    string[] lines = csvData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        if (line.Contains("VOUCHER_NUMBER")) // Skip header
                            continue;

                        // 🔹 Protect commas inside quoted text
                        string processedLine = Regex.Replace(
                            line,
                            "\"([^\"]*)\"",
                            m => m.Value.Replace(",", "§")
                        );

                        string[] cells = processedLine.Split(',');

                        DataRow newRow = dtCSVCostCenter.NewRow();

                        for (int i = 0; i < dtCSVCostCenter.Columns.Count; i++)
                        {
                            if (i < cells.Length)
                            {
                                newRow[i] = cells[i]
                                    .Replace("§", ",")
                                    .Trim()
                                    .Trim('"');
                            }
                            else
                            {
                                newRow[i] = string.Empty;
                            }
                        }

                        dtCSVCostCenter.Rows.Add(newRow);
                    }
                }

                gvCostCenter.DataSource = dtCSVCostCenter;
                gvCostCenter.DataBind();

                lblRecords.Text = "Records[" + dtCSVCostCenter.Rows.Count + "]";
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

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (gvCostCenter.Rows.Count > 0)
        {

            DataTable dt = (DataTable)Session["DS_COSTCENTER"];
            if(dt.Rows.Count > 0)
            {
                ToCSVNew01(dt);
            }
            else
            {
                
                
            }
            
        }
    }

   
    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                csv += dt.Columns[i].ColumnName + ',';
            }

            csv += "\r\n";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    csv += Convert.ToString(dt.Rows[i][j]).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }

            string fileName = "GL_Data_Dump_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
            string CompanyInitials = string.Empty;
            string DocType = string.Empty;
            string DocNumber = string.Empty;
            //DateTime? DocDate = null;
            //string DocDate = string.Empty;
            string VoucherNumber = string.Empty;
            string GLCode = string.Empty;
            string GLDesc = string.Empty;
            string CustVendorCostCenterCode = string.Empty;
            string CustVendorCostCenterName = string.Empty;
            string ProductCode = string.Empty;
            string ProductDesc = string.Empty;
            string NetAmountInr = string.Empty;

            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            DataTable dtPosting = new DataTable();

            dtPosting.Columns.Add("COMPANY_INITIALS", typeof(string));
            dtPosting.Columns.Add("DOCUMENT_TYPE", typeof(string));
            dtPosting.Columns.Add("DOCUMENT_NUMBER", typeof(string));
            dtPosting.Columns.Add("DOCUMENT_DATE", typeof(DateTime));
            dtPosting.Columns.Add("VOUCHER_NUMBER", typeof(string));
            //dtPosting.Columns.Add("GENERAL_LEDGER_CODE", typeof(string));
            dtPosting.Columns.Add("GENERAL_LEDGER_CODE", typeof(int));
            dtPosting.Columns.Add("GENERAL_LEDGER_DESC", typeof(string));
            dtPosting.Columns.Add("CUSTOMER_VENDOR_COST_CENTER_CODE", typeof(string));
            dtPosting.Columns.Add("CUSTOMER_VENDOR_COST_CENTER_NAME", typeof(string));
            dtPosting.Columns.Add("PRODUCT_CODE", typeof(string));
            dtPosting.Columns.Add("PRODUCT_DESC", typeof(string));
            // dtPosting.Columns.Add("NET_AMOUNT_INR", typeof(string));
            dtPosting.Columns.Add("NET_AMOUNT", typeof(int));
            //dtPosting.Columns.Add("CREATED_BY", typeof(string));
            dtPosting.Columns.Add("CREATED_BY", typeof(int));

            int value = 0;
            foreach (GridViewRow gr in gvCostCenter.Rows)
            {
                DateTime? DocDate = null;
                DataRow dr = dtPosting.NewRow();
                bool rowIsValid = true; // <-- flag to track errors in this row

                Label lblCompanyInitials = (Label)gr.FindControl("lblCompanyInitials");
                Label lblDocType = (Label)gr.FindControl("lblDocType");
                Label lblDocNumber = (Label)gr.FindControl("lblDocNumber");
                Label lblDocumentDate = (Label)gr.FindControl("lblDocumentDate");
                Label lblVoucherNumber = (Label)gr.FindControl("lblVoucherNumber");
                Label lblGLCode = (Label)gr.FindControl("lblGLCode");
                Label lblGLDesc = (Label)gr.FindControl("lblGLDesc");
                Label lblCustVendCostCenterCode = (Label)gr.FindControl("lblCustVendCostCenterCode");
                Label lblCustVendCostCenterName = (Label)gr.FindControl("lblCustVendCostCenterName");
                Label lblProductCode = (Label)gr.FindControl("lblProductCode");
                Label lblProductDesc = (Label)gr.FindControl("lblProductDesc");
                Label lblNetAmountInr = (Label)gr.FindControl("lblNetAmountInr");

                if (!string.IsNullOrEmpty(Convert.ToString(lblCompanyInitials.Text)))
                    CompanyInitials = Convert.ToString(lblCompanyInitials.Text);
                else
                    CompanyInitials = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDocType.Text)))
                    DocType = Convert.ToString(lblDocType.Text);
                else
                    DocType = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblDocNumber.Text)))
                    DocNumber = Convert.ToString(lblDocNumber.Text);
                else
                    DocNumber = string.Empty;


                //if (!string.IsNullOrEmpty(lblDocumentDate.Text))
                //{
                //    DocDate = DateTime.Parse(lblDocumentDate.Text); // works with "2025-11-30"
                //}

                string dateText = Convert.ToString(lblDocumentDate.Text).Trim();

                if (!string.IsNullOrEmpty(dateText))
                {
                    DateTime parsedDate;

                    if (DateTime.TryParseExact(
                            dateText,
                            new string[] { "dd/MM/yy", "dd/MM/yyyy", "MM/dd/yy", "MM/dd/yyyy" },
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out parsedDate))
                    {
                        DocDate = parsedDate;
                    }
                    else
                    {
                        rowIsValid = false;
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(lblVoucherNumber.Text)))
                    VoucherNumber = Convert.ToString(lblVoucherNumber.Text);
                else
                    VoucherNumber = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblGLCode.Text)))
                    GLCode = Convert.ToString(lblGLCode.Text);
                else
                    GLCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblGLDesc.Text)))
                    GLDesc = Convert.ToString(lblGLDesc.Text);
                else
                    GLDesc = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustVendCostCenterCode.Text)))
                    CustVendorCostCenterCode = Convert.ToString(lblCustVendCostCenterCode.Text);
                else
                    CustVendorCostCenterCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblCustVendCostCenterName.Text)))
                    CustVendorCostCenterName = Convert.ToString(lblCustVendCostCenterName.Text);
                else
                    CustVendorCostCenterName = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(lblProductCode.Text)))
                    ProductCode = Convert.ToString(lblProductCode.Text);
                else
                    ProductCode = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(lblProductDesc.Text)))
                    ProductDesc = Convert.ToString(lblProductDesc.Text);
                else
                    ProductDesc = string.Empty;

                //if (!string.IsNullOrEmpty(Convert.ToString(lblNetAmountInr.Text)))
                //    NetAmountInr = Convert.ToString(lblNetAmountInr.Text);
                //else
                //    NetAmountInr = string.Empty;
                int netAmount = 0;

                string amountText = Convert.ToString(lblNetAmountInr.Text)
                                        .Replace(",", "")
                                        .Trim();

                decimal temp;
                if (decimal.TryParse(amountText, out temp))
                {
                    netAmount = Convert.ToInt32(temp);
                }

                if (!string.IsNullOrEmpty(DocNumber) && rowIsValid)
                {
                    count++;
                    srNoForRemoval += srNo + ",";


                    int glCodeInt = 0;
                    int.TryParse(GLCode, out glCodeInt);

                    dr["COMPANY_INITIALS"] = CompanyInitials;
                    dr["DOCUMENT_TYPE"] = DocType;
                    dr["DOCUMENT_NUMBER"] = DocNumber;
                    dr["DOCUMENT_DATE"] = DocDate.HasValue ? (object)DocDate.Value : DBNull.Value;
                    dr["VOUCHER_NUMBER"] = VoucherNumber;
                    dr["GENERAL_LEDGER_CODE"] = glCodeInt;
                    dr["GENERAL_LEDGER_DESC"] = GLDesc;
                    dr["CUSTOMER_VENDOR_COST_CENTER_CODE"] = CustVendorCostCenterCode;
                    dr["CUSTOMER_VENDOR_COST_CENTER_NAME"] = CustVendorCostCenterName;
                    dr["PRODUCT_CODE"] = ProductCode;
                    dr["PRODUCT_DESC"] = ProductDesc;
                    dr["NET_AMOUNT"] = netAmount;
                    dr["CREATED_BY"] = createdBy;


                    dtPosting.Rows.Add(dr);

                }

            }

            value = objPosting.ImportGLDataDump(dtPosting);

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

            csv += "COMPANY_INITIALS" + ',';
            csv += "DOCUMENT_TYPE" + ',';
            csv += "DOCUMENT_NUMBER" + ',';
            csv += "DOCUMENT_DATE" + ',';
            csv += "VOUCHER_NUMBER" + ',';
            csv += "GENERAL_LEDGER_CODE" + ',';
            csv += "GENERAL_LEDGER_DESCRIPTION"+ ',';
            csv += "CUSTOMER_VENDOR_COST_CENTER_CODE" + ',';
            csv += "CUSTOMER_VENDOR_COST_CENTER_NAME" + ',';
            csv += "PRODUCT_CODE" + ',';
            csv += "PRODUCT_DESCRIPTION" + ',';
            csv += "NET_AMOUNT_INR" + ',';
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
           
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvCostCenter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPLOrBS");
            if (lbl != null && lbl.Text == "P")
                lbl.ForeColor = System.Drawing.Color.Green;
            else if (lbl != null && lbl.Text == "B")
                lbl.ForeColor = System.Drawing.Color.Blue;
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