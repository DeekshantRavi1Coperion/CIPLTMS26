using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WorkerTimesheetListNewTwo : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.Common objCommon = new BAL.Common();
    BAL.Timesheet objTimesheet = new BAL.Timesheet();

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

                GetTimesheetList();
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

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectSupervisorList();
        GetProjectWorkerList();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }

    protected void ddlProjectSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTimesheetList();
    }

    protected void gvWorkerTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblAllocationHours = (Label)e.Row.FindControl("lblAllocationHours");
                Label lblAllocatedHours = (Label)e.Row.FindControl("lblAllocatedHours");

                if (Convert.ToInt32(lblAllocationHours.Text) != Convert.ToInt32(lblAllocatedHours.Text))
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    }
                }

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

    protected void gvWorkerTimesheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblEmployeeCode = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblEmployeeCode") as Label;
                Label lblEntryDate = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblEntryDate") as Label;

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    iframeEditTimesheetList.Attributes.Add("src", "UpdateWorkerTimesheetListTwo.aspx?employeecode=" + Convert.ToString(lblEmployeeCode.Text) + "&entrydate=" + Convert.ToString(lblEntryDate.Text));
                    this.ModalPopupExtender1.Show();

                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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

    private void GetTimesheetList()
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

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTimesheet = objTimesheet.GetWorkerTimesheetListNewOne(startDate, endDate, jobNo, unitID, supervisorID, projectWorkerID, empRecordID);
            if (dsTimesheet.Tables.Count > 0 && dsTimesheet.Tables[0].Rows.Count > 0)
            {
                gvWorkerTimesheetList.DataSource = dsTimesheet.Tables[0];
                gvWorkerTimesheetList.DataBind();
            }
            else
            {
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