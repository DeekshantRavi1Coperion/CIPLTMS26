using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_WorkerTimesheetList : System.Web.UI.Page
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
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                hdDate.Value = txtDate.Text;

                txtDateUpdation.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                hdDateUpdation.Value = txtDateUpdation.Text;
                BindJOBNo(string.Empty);

                dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);
                Session["dsEmployee"] = dsEmployee;
                BindEmployee();
                BindEmployeeUpdation();

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
                Label lblEmpRecordID = gvTimesheetList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;
                Label lblEmployeeCode = gvTimesheetList.Rows[rowindex].FindControl("lblEmployeeCode") as Label;
                Label lblJobNo = gvTimesheetList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblEntryDate = gvTimesheetList.Rows[rowindex].FindControl("lblEntryDate") as Label;
                Label lblInTime = gvTimesheetList.Rows[rowindex].FindControl("lblInTime") as Label;
                Label lblHours = gvTimesheetList.Rows[rowindex].FindControl("lblHours") as Label;

                PublicValuesForWorkerTimesheetList.rcordID = Convert.ToInt32(lblRecordID.Text);

                if (e.CommandArgument == "PROPERTIES")
                {
                    pnlUpdateMsg.Visible = false;
                    ddlEmployeeUpdation.SelectedValue = Convert.ToString(lblEmpRecordID.Text);
                    txtEmployeeCode.Text = Convert.ToString(lblEmployeeCode.Text);
                    BindJOBNo(Convert.ToString(lblJobNo.Text));
                    hdDateUpdation.Value = Convert.ToString(lblEntryDate.Text);
                    txtDateUpdation.Text = Convert.ToDateTime(hdDateUpdation.Value).ToString("dd-MMM-yyyy");
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
    }

    protected void ddlEmployeeUpdation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        DataSet dsEmplist = (DataSet)Session["dsEmployee"];
        if (dsEmplist.Tables.Count > 0 && dsEmplist.Tables[0].Rows.Count > 0)
        {
            if (ddlEmployeeUpdation.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ddlEmployeeUpdation.SelectedValue)))
                {
                    foreach (DataRow dr in dsEmplist.Tables[0].Select("EMP_RECORD_ID='" + Convert.ToString(ddlEmployeeUpdation.SelectedValue) + "'"))
                    {
                        txtEmployeeCode.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                    }
                }
                else
                    txtEmployeeCode.Text = string.Empty;
            }
            else
                txtEmployeeCode.Text = string.Empty;
        }
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

    private void BindEmployee()
    {
        try
        {
            DataSet ds = (DataSet)Session["dsEmployee"];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds.Tables[0];
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

    private void BindEmployeeUpdation()
    {
        try
        {
            DataSet ds = (DataSet)Session["dsEmployee"];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeUpdation.DataSource = ds.Tables[0];
                ddlEmployeeUpdation.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeUpdation.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeUpdation.DataBind();
                ddlEmployeeUpdation.Items.Insert(0, "Select");
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
            string entryDate = string.Empty;
            string jobNo = string.Empty;
            int empRecordID = 0;

            entryDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                jobNo = txtJOBNo.Text.Trim();
            else
                jobNo = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;

            dsTimesheetList = objTimesheet.GetWorkerTimesheetList(entryDate, jobNo, empRecordID);
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
            string entryDate = string.Empty;
            int empRecordID = 0;
            string jobNo = string.Empty;
            string inTime = string.Empty;
            string hours = string.Empty;
            bool inTimeCheck = false;
            bool hoursCheck = false;

            recordID = PublicValuesForWorkerTimesheetList.rcordID;
            entryDate = Convert.ToDateTime(hdDateUpdation.Value).ToString("yyyy-MM-dd");

            if (ddlEmployeeUpdation.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployeeUpdation.SelectedValue);
            else
                empRecordID = 0;

            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtInTime.Text))
            {
                inTime = Convert.ToString(txtInTime.Text);
                string[] strtext = inTime.Split(':');
                int inTimeHours = Convert.ToInt32(strtext[0]);
                int inTimeMins = Convert.ToInt32(strtext[1]);

                if (inTimeHours <= 23 && inTimeMins <= 59)
                    inTimeCheck = true;
                else
                    inTimeCheck = false;
            }
            else
                inTime = string.Empty;

            if (!string.IsNullOrEmpty(txtHours.Text))
            {
                hours = Convert.ToString(txtHours.Text);
                string[] strtext = hours.Split(':');
                int hoursMins = Convert.ToInt32(strtext[1]);

                if (hoursMins <= 59)
                    hoursCheck = true;
                else
                {
                    hoursCheck = false;
                    ModalPopupExtender1.Show();
                    ExceptionUpdateMessage("Please enter correct format of hours!");
                    hours = string.Empty;
                }
            }

            if (inTimeCheck == true && hoursCheck == true)
            {
                int value = objTimesheet.AddUpdateWorkerTimesheet(recordID, entryDate, empRecordID, jobNo, inTime, hours, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    GetTimesheetList();
                    SuccessMessage("Updated successfully");
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again!");

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


public static class PublicValuesForWorkerTimesheetList
{
    public static int rcordID = 0;
}

