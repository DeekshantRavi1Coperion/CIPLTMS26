using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_WorkerTimesheetListNewOne : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.Common objCommon = new BAL.Common();
    BAL.Timesheet objTimesheet = new BAL.Timesheet();

    DataSet dsJOBNo = new DataSet();
    DataSet dsTimesheet = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCompany = new DataSet();
    DataSet dsProjectSupervisor = new DataSet();
    DataSet dsProjectWorker = new DataSet();
    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["TIMESHET"] = null;
                BindUnit();
                BindCompany();

                GetProjectSupervisorList();
                GetProjectWorkerList();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                GetTimesheetReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;

        txtTotalWorkingHours.Text = string.Empty;
        txtTotalAllocatedHours.Text = string.Empty;
        txtTotalOTHours.Text = string.Empty;

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectSupervisorList();
        GetProjectWorkerList();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTotalWorkingHours.Text = string.Empty;
        txtTotalAllocatedHours.Text = string.Empty;
        txtTotalOTHours.Text = string.Empty;

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }

    protected void ddlProjectSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;
        txtTotalWorkingHours.Text = string.Empty;
        txtTotalAllocatedHours.Text = string.Empty;
        txtTotalOTHours.Text = string.Empty;

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtTotalWorkingHours.Text = string.Empty;
        txtTotalAllocatedHours.Text = string.Empty;
        txtTotalOTHours.Text = string.Empty;

        HideUpdateMessage();
        pnlMsg.Visible = false;
        GetTimesheetReport();
    }

    protected void gvWorkerTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWorkerTimesheetList.PageIndex = e.NewPageIndex;
        GetTimesheetReport();
    }

    protected void gvWorkerTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvWorkerTimesheetList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["TIMESHET"];
            ExportToExcel(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCompany()
    {
        try
        {
            dsCompany = objCommon.GetCompany();
            if (dsCompany.Tables.Count > 0 && dsCompany.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsCompany.Tables[0];
                ddlCompany.DataTextField = "COMPANY_NAME";
                ddlCompany.DataValueField = "COMPANY_ID";
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

    private void GetProjectSupervisorList()
    {
        int unitID = 0;
        if (ddlUnit.SelectedIndex > 0)
            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

        dsProjectSupervisor = objTimesheet.GetProjectSupervisorList(Convert.ToInt32(Session["EMP_RECORD_ID"]), unitID);
        if (dsProjectSupervisor.Tables.Count > 0 && dsProjectSupervisor.Tables[0].Rows.Count > 0)
        {
            ddlProjectSupervisor.DataSource = dsProjectSupervisor.Tables[0];
            ddlProjectSupervisor.DataTextField = "EMPLOYEE_NAME";
            ddlProjectSupervisor.DataValueField = "EMP_RECORD_ID";
            ddlProjectSupervisor.DataBind();
            ddlProjectSupervisor.Items.Insert(0, "All");
            ddlProjectSupervisor.SelectedIndex = 0;
        }
        else
        {
            ddlProjectSupervisor.Items.Clear();
            ddlProjectSupervisor.Items.Insert(0, "All");
            ddlProjectSupervisor.SelectedIndex = 0;
        }
    }

    private void GetProjectWorkerList()
    {
        int supervisorID = 0;
        int companyID = 0;
        int unitID = 0;

        if (ddlProjectSupervisor.SelectedIndex > 0)
            supervisorID = Convert.ToInt32(ddlProjectSupervisor.SelectedValue);

        if (ddlCompany.SelectedIndex > 0)
            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

        if (ddlUnit.SelectedIndex > 0)
            unitID = Convert.ToInt32(ddlUnit.SelectedValue);

        dsProjectWorker = objTimesheet.GetProjectWorkerList(Convert.ToInt32(Session["EMP_RECORD_ID"]), supervisorID, companyID, unitID);
        if (dsProjectWorker.Tables.Count > 0 && dsProjectWorker.Tables[0].Rows.Count > 0)
        {
            ddlProjectWorker.DataSource = dsProjectWorker.Tables[0];
            ddlProjectWorker.DataTextField = "EMPLOYEE_NAME";
            ddlProjectWorker.DataValueField = "EMP_RECORD_ID";
            ddlProjectWorker.DataBind();
            ddlProjectWorker.Items.Insert(0, "All");
            ddlProjectWorker.SelectedIndex = 0;
        }
        else
        {
            ddlProjectWorker.Items.Clear();
            ddlProjectWorker.Items.Insert(0, "All");
            ddlProjectWorker.SelectedIndex = 0;
        }
    }

    private void GetTimesheetReport()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string jobNo = string.Empty;
            int unitID = 0;
            int empRecordID = 0;
            int supervisorID = 0;
            int projectWorkerID = 0;
            int companyID = 0;


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


            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
            else
                unitID = 0;

            if (ddlProjectSupervisor.SelectedIndex > 0)
                supervisorID = Convert.ToInt32(ddlProjectSupervisor.SelectedValue);
            else
                supervisorID = 0;

            if (ddlProjectWorker.SelectedIndex > 0)
                projectWorkerID = Convert.ToInt32(ddlProjectWorker.SelectedValue);
            else
                projectWorkerID = 0;

            if (ddlCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            else
                companyID = 0;


            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTimesheet = objTimesheet.GetWorkerTimesheetReportNewOne(startDate, endDate, jobNo, unitID, supervisorID, projectWorkerID, empRecordID, companyID);
            if (dsTimesheet.Tables.Count > 0)
            {
                if (dsTimesheet.Tables[0].Rows.Count > 0)
                {
                    Session["TIMESHET"] = dsTimesheet;
                    gvWorkerTimesheetList.DataSource = dsTimesheet.Tables[0];

                    gvWorkerTimesheetList.DataBind();
                }
                else
                {
                    Session["TIMESHET"] = null;
                    gvWorkerTimesheetList.DataSource = null;
                    gvWorkerTimesheetList.DataBind();
                }


                if (dsTimesheet.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = dsTimesheet.Tables[1].Rows[0];
                    txtTotalWorkingHours.Text = Convert.ToString(dr["PUNCH_HOURS"]);
                    txtTotalAllocatedHours.Text = Convert.ToString(dr["WORKING_HOURS"]);
                    txtTotalOTHours.Text = Convert.ToString(dr["OT_HOURS"]);
                }
                else
                {
                    txtTotalWorkingHours.Text = string.Empty;
                    txtTotalAllocatedHours.Text = string.Empty;
                    txtTotalOTHours.Text = string.Empty;
                }
            }
            else
            {
                Session["TIMESHET"] = null;
                gvWorkerTimesheetList.DataSource = null;
                gvWorkerTimesheetList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTimesheet.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            string csv = string.Empty;

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                csv += Convert.ToString(dt.Columns[i].ColumnName) + ',';
            }

            csv += "\r\n";

            string rowTxt = string.Empty;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count - 1; k++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j][k])) && Convert.ToString(dt.Rows[j][k]) != "&nbsp;")
                        rowTxt = Convert.ToString(dt.Rows[j][k]);
                    else
                        rowTxt = string.Empty;


                    rowTxt = rowTxt.Replace(',', ' ');
                    rowTxt = rowTxt.TrimEnd('\r', ' ');
                    rowTxt = rowTxt.TrimEnd('\n', ' ');
                    rowTxt = rowTxt.Replace('\r', ' ');
                    rowTxt = rowTxt.Replace('\n', ' ');

                    csv += Convert.ToString(rowTxt).Replace(",", ";") + ',';
                }
                csv += "\r\n";
            }


            string fileName = "Procurement_Status-Procurement_View_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

            string fileName = "WorkerTimeSheetReport_From_" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    private void HideUpdateMessage()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}