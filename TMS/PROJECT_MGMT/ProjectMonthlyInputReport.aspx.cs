using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_ProjectMonthlyInputReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();

    DataSet dsJobNo = new DataSet();
    DataSet dsProjectMonthlyInput = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["MONTHLY_INPUT_REPORT"] = null;                
                HidePanel();
                hdMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtMonth.Text = Convert.ToString(hdMonth.Value);

                ddlJOBNo.Items.Clear();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
                BindJobNo();

                GetProjectmonthlyReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        pnlMsg.Visible = false;
        GetProjectmonthlyReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvProjectMonthlyInputReport.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["MONTHLY_INPUT_REPORT"];
            ToCSVNew01(dt);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void gvProjectMonthlyInputReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindJobNo()
    {
        try
        {
            dsJobNo = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJobNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NO";
                ddlJOBNo.DataValueField = "JOB_NO";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetProjectmonthlyReport()
    {
        try
        {
            string jobNo = string.Empty;
            string month = string.Empty;


            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(hdMonth.Value))
                month = Convert.ToDateTime(hdMonth.Value).ToString("yyyy-MM");
            else
                month = string.Empty;

            dsProjectMonthlyInput = objProject.GetProjectmonthlyReport(jobNo, month);

            if (dsProjectMonthlyInput.Tables.Count > 0 && dsProjectMonthlyInput.Tables[0].Rows.Count > 0)
            {
                Session["MONTHLY_INPUT_REPORT"] = dsProjectMonthlyInput.Tables[0];
                gvProjectMonthlyInputReport.DataSource = dsProjectMonthlyInput.Tables[0];
                gvProjectMonthlyInputReport.DataBind();
            }
            else
            {
                Session["MONTHLY_INPUT_REPORT"] = null;
                gvProjectMonthlyInputReport.DataSource = null;
                gvProjectMonthlyInputReport.DataBind();
            }
            lblRecords.Text = "Records[" + gvProjectMonthlyInputReport.Rows.Count + "]";
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

            string fileName = "MonthlyInpurtReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}