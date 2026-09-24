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

public partial class REPORTS_LogReport : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsUnit = new DataSet();
    DataSet dsUser = new DataSet();
    DataSet dsLogReport = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string unitName = string.Empty;
    string reportType = string.Empty;
    string userName = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["LOG_REPORT"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;

                BindUnit();

                ddlUser.Items.Clear();
                ddlUser.Items.Insert(0, "ALL");
                ddlUser.SelectedIndex = 0;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["LOG_REPORT"] = null;
        gvLogReport.DataSource = null;
        gvLogReport.DataBind();
        lblTotal.Text = Convert.ToString(gvLogReport.Rows.Count);
        lblRecords.Text = "Records[" + gvLogReport.Rows.Count + "]";

        if (ddlReportType.SelectedIndex > 0)
        {
            BindUser();
        }
        else
        {
            ddlUser.Items.Clear();
            ddlUser.Items.Insert(0, "All");
            ddlUser.SelectedIndex = 0;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["LOG_REPORT"] = null;
        gvLogReport.DataSource = null;
        gvLogReport.DataBind();
        lblTotal.Text = Convert.ToString(gvLogReport.Rows.Count);
        lblRecords.Text = "Records[" + gvLogReport.Rows.Count + "]";
        BindUser();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblTotal.Text = "0";
        lblMsg.Text = string.Empty;
        pnlMsg.Visible = false;
        GetLogReport();
    }

    protected void gvLogReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            //    totalAmount += Convert.ToDouble(lblAmount.Text);
            //    lblTotalAmount.Text = Convert.ToString(totalAmount);
            //    lblTotalAmount.ForeColor = System.Drawing.Color.Green;
            //}

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
        if (gvLogReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["LOG_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "All");
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUser()
    {
        try
        {
            string reportType = string.Empty;
            string unitName = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;

            if (ddlReportType.SelectedIndex > 0)
                reportType = Convert.ToString(ddlReportType.SelectedItem.Text);
            else
                reportType = string.Empty;

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;


            dsUser = objReports.BindUserForLogReport(fromDate, toDate, reportType, unitName);
            if (dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
            {
                ddlUser.DataSource = dsUser.Tables[0];
                ddlUser.DataTextField = "FUSER";
                ddlUser.DataValueField = "FUSER";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, "ALL");
                ddlUser.SelectedIndex = 0;
            }
            else
            {
                ddlUser.Items.Clear();
                ddlUser.Items.Insert(0, "ALL");
                ddlUser.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetLogReport()
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

            if (ddlCompany.SelectedIndex > 0)
                unitName = Convert.ToString(ddlCompany.SelectedItem.Text);
            else
                unitName = string.Empty;

            if (ddlReportType.SelectedIndex > 0)
                reportType = Convert.ToString(ddlReportType.SelectedItem.Text);
            else
                reportType = string.Empty;

            if (ddlUser.SelectedIndex > 0)
                userName = Convert.ToString(ddlUser.SelectedValue);
            else
                userName = string.Empty;

            dsLogReport = objReports.GetLogReport(fromDate, toDate, unitName, reportType, userName);

            if (dsLogReport.Tables.Count > 0 && dsLogReport.Tables[0].Rows.Count > 0)
            {
                Session["LOG_REPORT"] = dsLogReport;
                gvLogReport.DataSource = dsLogReport.Tables[0];
                gvLogReport.DataBind();
            }
            else
            {
                Session["LOG_REPORT"] = null;
                gvLogReport.DataSource = null;
                gvLogReport.DataBind();
            }

            lblTotal.Text = Convert.ToString(gvLogReport.Rows.Count);
            lblRecords.Text = "Records[" + gvLogReport.Rows.Count + "]";
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

            string fileName = "LOG_REPORT_From_" + txtStartDateSearch.Text + "_To_" + txtEndDateSearch.Text;
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
