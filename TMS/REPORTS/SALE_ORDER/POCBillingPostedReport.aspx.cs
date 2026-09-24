using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class REPORTS_SALE_ORDER_POCBillingPostedReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]
    
    BAL.Reports objReports = new BAL.Reports();    
    DataSet dsPOCReport = new DataSet();
   
    string fromDate = string.Empty;
    string toDate = string.Empty;   
    string JOBNo = string.Empty;
    string customerName = string.Empty;
    string customerCode = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["POC_REPORT"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;               
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetPOCPostedBillingReport();
    }

    protected void gvPOCReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPOCReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["POC_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]
   
    private void GetPOCPostedBillingReport()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;
            
            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text;
            else
                JOBNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                customerCode = txtCustomerCode.Text;
            else
                customerCode = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            dsPOCReport = objReports.GetPOCPostedBillingReport(fromDate, toDate, JOBNo, customerCode, customerName);

            if (dsPOCReport.Tables.Count > 0 && dsPOCReport.Tables[0].Rows.Count > 0)
            {
                Session["POC_REPORT"] = dsPOCReport;
                gvPOCReport.DataSource = dsPOCReport.Tables[0];
                gvPOCReport.DataBind();
            }
            else
            {
                Session["POC_REPORT"] = null;
                gvPOCReport.DataSource = null;
                gvPOCReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsPOCReport.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ToCSVNew01(DataTable dt)
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

            string fileName = "POC_Billing_Posted_Report_From_" + txtStartDateSearch.Text + "_To_" + txtEndDateSearch.Text;
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

    #endregion

}
