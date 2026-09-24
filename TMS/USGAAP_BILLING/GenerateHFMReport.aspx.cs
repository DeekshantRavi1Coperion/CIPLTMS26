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

public partial class USGAAP_BILLING_GenerateHFMReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Posting objPosting = new BAL.Posting();

    DataSet dsHFMReport = new DataSet();
    string fromDate = string.Empty;
    string toDate = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["HFM_REPORT"] = null;

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
        pnlMsg.Visible = false;
        lblMsg.Visible = false;
        lblMsg.Text = string.Empty;
        GenerateHFMReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvHFMReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["HFM_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void gvHFMReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GenerateHFMReport()
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


            dsHFMReport = objPosting.GenerateHFMReport(fromDate, toDate, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsHFMReport.Tables.Count > 0 && dsHFMReport.Tables[0].Rows.Count > 0)
            {
                Session["HFM_REPORT"] = dsHFMReport;
                gvHFMReport.DataSource = dsHFMReport.Tables[0];
                gvHFMReport.DataBind();
            }
            else
            {
                Session["HFM_REPORT"] = null;
                gvHFMReport.DataSource = null;
                gvHFMReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsHFMReport.Tables[0].Rows.Count + "]";

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

            string fileName = "HFM_Report_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
