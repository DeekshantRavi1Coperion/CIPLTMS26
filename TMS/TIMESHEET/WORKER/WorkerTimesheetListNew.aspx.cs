using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_WorkerTimesheetListNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsTimesheetList = new DataSet();
    DataSet dsJOBNo = new DataSet();
    DataSet dsEmployee = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();

                txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                hdEntryDate.Value = txtEntryDate.Text;
                BindJOBNo(string.Empty);


                BindWorkerList();

                GetTimesheetList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvTimesheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            HidePanel();
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvTimesheetList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblEmployeeName = gvTimesheetList.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblEmployeeCode = gvTimesheetList.Rows[rowindex].FindControl("lblEmployeeCode") as Label;
                Label lblUnit = gvTimesheetList.Rows[rowindex].FindControl("lblUnit") as Label;
                Label lblJobNo = gvTimesheetList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblEntryDate = gvTimesheetList.Rows[rowindex].FindControl("lblEntryDate") as Label;
                Label lblInTime = gvTimesheetList.Rows[rowindex].FindControl("lblInTime") as Label;
                Label lblHours = gvTimesheetList.Rows[rowindex].FindControl("lblHours") as Label;

                PublicValuesForWorkerTimesheetListNew.rcordID = Convert.ToInt32(lblRecordID.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    pnlUpdateMsg.Visible = false;

                    txtEmployeeName.Text = Convert.ToString(lblEmployeeName.Text);
                    txtEmployeeCode.Text = Convert.ToString(lblEmployeeCode.Text);
                    txtUnit.Text = Convert.ToString(lblUnit.Text);
                    BindJOBNo(Convert.ToString(lblJobNo.Text));
                    hdEntryDate.Value = Convert.ToString(lblEntryDate.Text);
                    txtEntryDate.Text = Convert.ToDateTime(hdEntryDate.Value).ToString("dd-MMM-yyyy");
                    txtInTime.Text = Convert.ToString(lblInTime.Text);
                    txtHours.Text = Convert.ToString(lblHours.Text);

                    btnSubmit.Text = "Update";
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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        HidePanel();
        Response.Redirect("~/TIMESHEET/AddUpdateWorkerTimesheetNew.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HidePanel();
        UpdateWorkerTimesheet();
    }

    #endregion


    #region METHODS[=======================]

    private void BindJOBNo(string jobNo)
    {
        try
        {
            dsJOBNo = objTimesheet.GetDetailsBySP("sp_get_distinct_job_no");
            if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJOBNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NUMBER";
                ddlJOBNo.DataValueField = "JOB_NUMBER";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "Select");

                if (!string.IsNullOrEmpty(jobNo))
                {
                    ddlJOBNo.SelectedValue = jobNo;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

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

            dsTimesheetList = objTimesheet.GetWorkerTimesheetListNew(startDate, endDate, jobNo, empRecordID);
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                gvTimesheetList.DataSource = dsTimesheetList.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
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

    private void UpdateWorkerTimesheet()
    {
        try
        {
            int recordID = 0;
            string employeeCode = string.Empty;
            string jobNo = string.Empty;
            string entryDate = string.Empty;
            string inTime = string.Empty;
            string hours = string.Empty;
            bool inTimeCheck = false;
            bool hoursCheck = false;

            recordID = PublicValuesForWorkerTimesheetListNew.rcordID;

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
                employeeCode = txtEmployeeCode.Text;
            else
                employeeCode = string.Empty;

            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
            else
                jobNo = string.Empty;


            entryDate = Convert.ToDateTime(hdEntryDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtInTime.Text))
            {
                inTime = Convert.ToString(txtInTime.Text);

                if (inTime.Contains(':'))
                {
                    inTime = inTime + ":00";
                }

                string[] strtext = inTime.Split(':');
                int inTimeHours = Convert.ToInt32(strtext[0]);
                int inTimeMins = Convert.ToInt32(strtext[1]);

                if ((inTimeHours == 0 && (inTimeMins > 0 && inTimeMins <= 59)) || (inTimeHours > 0 && inTimeMins <= 59))
                    inTimeCheck = true;
                else
                    inTimeCheck = false;
            }
            else
                inTime = string.Empty;

            if (!string.IsNullOrEmpty(txtHours.Text))
            {
                hours = Convert.ToString(txtHours.Text);

                if (!hours.Contains(':'))
                {
                    hours = hours + ":00";
                }

                string[] strtext = hours.Split(':');
                int hoursHours = Convert.ToInt32(strtext[0]);
                int hoursMins = Convert.ToInt32(strtext[1]);
                
                if ((hoursHours == 0 && (hoursMins > 0 && hoursMins <= 59)) || (hoursHours > 0 && hoursMins <= 59))
                    hoursCheck = true;
                else
                {
                    hoursCheck = false;
                    ModalPopupExtender1.Show();
                    ExceptionUpdateMessage("Please enter correct format of hours!");
                    hours = string.Empty;
                }
            }
            else
            {
                hoursCheck = false;
                ModalPopupExtender1.Show();
                ExceptionUpdateMessage("Please enter hours!");
                return;
            }

            if (inTimeCheck == true && hoursCheck == true)
            {
                int value = objTimesheet.AddUpdateWorkerTimesheetNew(recordID, employeeCode, jobNo, entryDate, inTime, hours, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    GetTimesheetList();
                    SuccessMessage("Updated successfully");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again!");
                    return;
                }
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

    private void ExceptionUpdateMessage(string message)
    {
        pnlUpdateMsg.Visible = true;
        lblUpdateMsg.Text = message;
        lblUpdateMsg.ForeColor = System.Drawing.Color.Red;
    }

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
        lblMsg.Visible = false;
    }
    #endregion

}
public static class PublicValuesForWorkerTimesheetListNew
{
    public static int rcordID = 0;
}

