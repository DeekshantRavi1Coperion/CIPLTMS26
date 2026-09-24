using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_WorkerTimesheetReportNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsTimesheetList = new DataSet();
    DataSet dsEmployee = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["TIMESHEET_REPORT"] = null;
                HidePanel();
                BindWorkerList();
                GetTimesheetList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    
    protected void gvTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        HidePanel();
        gvTimesheetList.PageIndex = e.NewPageIndex;
        GetTimesheetList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetTimesheetList();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTimesheetList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["TIMESHEET_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }
    
    #endregion


    #region METHODS[=======================]

    private void BindWorkerList()
    {
        try
        {
            dsEmployee = objCommon.GetWorkerList();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTimesheetList()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string jobNo = string.Empty;
            int empRecordID = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text.Trim();
            else
                jobNo = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            dsTimesheetList = objTimesheet.GetWorkerTimesheetReportNew(startDate, endDate, jobNo, empRecordID);
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                Session["TIMESHEET_REPORT"] = dsTimesheetList;
                gvTimesheetList.DataSource = dsTimesheetList.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
                Session["TIMESHEET_REPORT"] = null;
                gvTimesheetList.DataSource = null;
                gvTimesheetList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTimesheetList.Tables[0].Rows.Count + "]";
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

            string fileName = "WorkerTimesheetReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
        lblMsg.Visible = false;
    }

    #endregion

}

