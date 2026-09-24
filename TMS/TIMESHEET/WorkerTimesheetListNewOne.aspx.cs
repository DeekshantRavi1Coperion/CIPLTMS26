using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WorkerTimesheetListNewOne : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.Common objCommon = new BAL.Common();
    BAL.Timesheet objTimesheet = new BAL.Timesheet();

    DataSet dsJOBNo = new DataSet();
    DataSet dsTimesheet = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsProjectHead = new DataSet();
    DataSet dsProjectSupervisor = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindUnit();
                BindJOBNo();
                ddlProjectHead.Items.Insert(0, "All");
                ddlProjectHead.SelectedIndex = 0;

                ddlProjectSupervisor.Items.Insert(0, "All");
                ddlProjectSupervisor.SelectedIndex = 0;

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
        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";



        if (ddlUnit.SelectedIndex > 0)
        {
            GetProjectHeadList();
        }
        else
        {
            ddlProjectHead.Items.Clear();
            ddlProjectHead.Items.Insert(0, "All");
            ddlProjectHead.SelectedIndex = 0;

            ddlProjectSupervisor.Items.Clear();
            ddlProjectSupervisor.Items.Insert(0, "All");
            ddlProjectSupervisor.SelectedIndex = 0;
        }
    }

    protected void ddlProjectHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        if (ddlProjectHead.SelectedIndex > 0)
        {
            GetProjectSupervisorList();
        }
        else
        {
            ddlProjectSupervisor.Items.Clear();
            ddlProjectSupervisor.Items.Insert(0, "All");
            ddlProjectSupervisor.SelectedIndex = 0;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTimesheetList();
    }

    protected void gvWorkerTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWorkerTimesheetList.PageIndex = e.NewPageIndex;
        GetTimesheetList();
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

    protected void gvWorkerTimesheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES" || e.CommandArgument == "DELETE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblRecordID = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblRecordID") as Label;
                Label lblEmployeeName = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblEmployeeName") as Label;
                Label lblEmployeeCode = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblEmployeeCode") as Label;
                Label lblEntryDate = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblEntryDate") as Label;
                Label lblJOBNo = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblJOBNo") as Label;
                Label lblInTime = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblInTime") as Label;
                Label lblOutTime = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblOutTime") as Label;
                Label lblHours = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblHours") as Label;
                Label lblWorkingHours = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblWorkingHours") as Label;
                Label lblRemarks = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblRemarks") as Label;

                //PublicValuesForWorkerTimesheetUpdations.recordID = Convert.ToInt32(lblRecordID.Text);
                ViewState["RecordID"] = Convert.ToInt32(lblRecordID.Text);

                txtEmployeeName.Text = Convert.ToString(lblEmployeeName.Text);
                txtEmployeeCode.Text = Convert.ToString(lblEmployeeCode.Text);
                txtEntryDate.Text = Convert.ToString(lblEntryDate.Text);
                ddlJOBNoForUpdation.SelectedValue = Convert.ToString(lblJOBNo.Text);
                txtInTime.Text = Convert.ToString(lblInTime.Text);
                txtOutTime.Text = Convert.ToString(lblOutTime.Text);
                txtHours.Text = Convert.ToString(lblHours.Text);
                txtWorkingHours.Text = Convert.ToString(lblWorkingHours.Text);
                txtRemarks.Text = Convert.ToString(lblRemarks.Text);

                int adjustedWorkingHours = 0;
                int adjustedWorkingMins = 0;
                int adjustedWorkingTotalMins = 0;

                int currentRowAdjustedWorkingHours = 0;
                int currentRowAdjustedWorkingMins = 0;
                int currentRowAdjustedWorkingTotalMins = 0;

                if (e.CommandArgument == "PROPERTIES")
                {
                    HideUpdateMessage();
                    this.ModalPopupExtender1.Show();

                    if (gvWorkerTimesheetList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                        {
                            Label lblEmployeeNameNew = (Label)gr.FindControl("lblEmployeeName");
                            Label lblEmpCodeNew = (Label)gr.FindControl("lblEmployeeCode");
                            Label lblWorkingHoursNew = (Label)gr.FindControl("lblWorkingHours");
                            if (lblEmployeeNameNew.Text == Convert.ToString(lblEmployeeName.Text) && lblEmpCodeNew.Text == Convert.ToString(lblEmployeeCode.Text))
                            {
                                if (!string.IsNullOrEmpty(lblWorkingHoursNew.Text))
                                {
                                    if (Convert.ToString(lblWorkingHoursNew.Text).Contains(':'))
                                    {
                                        string[] strAdjustedWorkingHours = Convert.ToString(lblWorkingHoursNew.Text).Split(':');

                                        if (Convert.ToInt32(strAdjustedWorkingHours[0]) > 0)
                                            adjustedWorkingHours = Convert.ToInt32(strAdjustedWorkingHours[0]);
                                        else
                                            adjustedWorkingHours = 0;

                                        if (Convert.ToInt32(strAdjustedWorkingHours[1]) > 0)
                                            adjustedWorkingMins = Convert.ToInt32(strAdjustedWorkingHours[1]);
                                        else
                                            adjustedWorkingMins = 0;
                                    }
                                    else
                                    {
                                        adjustedWorkingHours = Convert.ToInt32(lblWorkingHoursNew.Text);
                                        adjustedWorkingMins = 0;
                                    }
                                }
                            }
                            else
                            {
                                adjustedWorkingHours = 0;
                                adjustedWorkingMins = 0;
                            }

                            adjustedWorkingTotalMins += adjustedWorkingHours * 60 + adjustedWorkingMins;
                        }


                        if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                        {
                            if (lblWorkingHours.Text.Contains(':'))
                            {
                                string[] strAdjustedWorkingHours = lblWorkingHours.Text.Split(':');

                                if (Convert.ToInt32(strAdjustedWorkingHours[0]) > 0)
                                    currentRowAdjustedWorkingHours = Convert.ToInt32(strAdjustedWorkingHours[0]);
                                else
                                    currentRowAdjustedWorkingHours = 0;

                                if (Convert.ToInt32(strAdjustedWorkingHours[1]) > 0)
                                    currentRowAdjustedWorkingMins = Convert.ToInt32(strAdjustedWorkingHours[1]);
                                else
                                    currentRowAdjustedWorkingMins = 0;
                            }
                            else
                            {
                                currentRowAdjustedWorkingHours = Convert.ToInt32(lblWorkingHours.Text);
                                currentRowAdjustedWorkingMins = 0;
                            }
                        }
                        else
                        {
                            currentRowAdjustedWorkingHours = 0;
                            currentRowAdjustedWorkingMins = 0;
                        }

                        currentRowAdjustedWorkingTotalMins = currentRowAdjustedWorkingHours * 60 + currentRowAdjustedWorkingMins;

                        int hoursHours = 0;
                        int hoursMins = 0;
                        int hoursTotalMins = 0;

                        if (!string.IsNullOrEmpty(txtHours.Text))
                        {
                            if (txtHours.Text.Contains(':'))
                            {
                                string[] strAdjustedWorkingHours = txtHours.Text.Split(':');

                                if (Convert.ToInt32(strAdjustedWorkingHours[0]) > 0)
                                    hoursHours = Convert.ToInt32(strAdjustedWorkingHours[0]);
                                else
                                    hoursHours = 0;

                                if (Convert.ToInt32(strAdjustedWorkingHours[1]) > 0)
                                    hoursMins = Convert.ToInt32(strAdjustedWorkingHours[1]);
                                else
                                    hoursMins = 0;
                            }
                            else
                            {
                                hoursHours = Convert.ToInt32(txtHours.Text);
                                hoursMins = 0;
                            }
                        }
                        else
                        {
                            hoursHours = 0;
                            hoursMins = 0;
                        }
                        hoursTotalMins = hoursHours * 60 + hoursMins;

                        hdHoursMins.Value = Convert.ToString(hoursTotalMins);
                        hdTotalWorkingMins.Value = Convert.ToString(adjustedWorkingTotalMins);
                        hdBalanceworkingMins.Value = Convert.ToString(Convert.ToInt32(hdTotalWorkingMins.Value) - Convert.ToInt32(currentRowAdjustedWorkingTotalMins));
                    }
                }
                else if (e.CommandArgument == "DELETE")
                {
                    //int value = objTimesheet.UpdateWorkerTimesheet(1, PublicValuesForWorkerTimesheetUpdations.recordID, string.Empty, string.Empty, string.Empty, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    int value = objTimesheet.UpdateWorkerTimesheet(1, Convert.ToInt32(ViewState["RecordID"]), string.Empty, string.Empty, string.Empty, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    if (value > 0)
                    {
                        SuccessMessage("Record deleted successfully..!");
                        GetTimesheetList();
                        return;
                    }
                    else
                    {
                        ExceptionMessage("Please try again..!");
                        return;
                    }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateWorkerTimesheet();
    }

    #endregion


    #region METHODS[=======================]

    private void BindJOBNo()
    {
        try
        {
            dsJOBNo = objTimesheet.GetDetailsBySP("sp_get_distinct_job_no");
            if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNoForUpdation.DataSource = dsJOBNo.Tables[0];
                ddlJOBNoForUpdation.DataTextField = "JOB_NUMBER";
                ddlJOBNoForUpdation.DataValueField = "JOB_NUMBER";
                ddlJOBNoForUpdation.DataBind();
                ddlJOBNoForUpdation.Items.Insert(0, "Select");
            }
            else
            {
                ddlJOBNoForUpdation.Items.Clear();
                ddlJOBNoForUpdation.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetProjectHeadList()
    {
        dsProjectHead = objTimesheet.GetProjectHeadList(Convert.ToInt32(ddlUnit.SelectedValue));
        if (dsProjectHead.Tables.Count > 0 && dsProjectHead.Tables[0].Rows.Count > 0)
        {
            ddlProjectHead.DataSource = dsProjectHead.Tables[0];
            ddlProjectHead.DataTextField = "EMPLOYEE_NAME";
            ddlProjectHead.DataValueField = "EMP_RECORD_ID";
            ddlProjectHead.DataBind();
            ddlProjectHead.Items.Insert(0, "All");
            ddlProjectHead.SelectedIndex = 0;
        }
        else
        {
            ddlProjectHead.Items.Clear();
            ddlProjectHead.Items.Insert(0, "All");
            ddlProjectHead.SelectedIndex = 0;
        }
    }

    private void GetProjectSupervisorList()
    {
        dsProjectSupervisor = objTimesheet.GetProjectSupervisorList(Convert.ToInt32(ddlProjectHead.SelectedValue), 0);
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
                ddlUnit.Items.Insert(0, "Select");
                ddlUnit.SelectedIndex = 0;
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
            int unitID = 0;
            int empRecordID = 0;
            int projectHeadID = 0;
            int supervisorID = 0;
            string teamMembers = string.Empty;


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

            if (ddlProjectHead.SelectedIndex > 0)
                projectHeadID = Convert.ToInt32(ddlProjectHead.SelectedValue);
            else
                projectHeadID = 0;

            if (ddlProjectSupervisor.SelectedIndex > 0)
                supervisorID = Convert.ToInt32(ddlProjectSupervisor.SelectedValue);
            else
                supervisorID = 0;

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTimesheet = objTimesheet.GetWorkerTimesheetListNewOne(startDate, endDate, jobNo, unitID, projectHeadID, supervisorID, empRecordID);
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

    private void UpdateWorkerTimesheet()
    {
        try
        {
            int recordID = 0;
            string JOBNo = string.Empty;
            string workingHours = string.Empty;
            string remarks = string.Empty;

            int currentRowAdjustedWorkingHours = 0;
            int currentRowAdjustedWorkingMins = 0;
            int currentRowAdjustedWorkingTotalMins = 0;

            recordID = Convert.ToInt32(ViewState["RecordID"]);

            if (ddlJOBNoForUpdation.SelectedIndex > 0)
                JOBNo = Convert.ToString(ddlJOBNoForUpdation.SelectedValue);
            else
                JOBNo = string.Empty;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;


            if (!string.IsNullOrEmpty(txtWorkingHours.Text))
            {
                if (txtWorkingHours.Text.Contains(':'))
                {
                    string[] strAdjustedWorkingHours = txtWorkingHours.Text.Split(':');

                    if (Convert.ToInt32(strAdjustedWorkingHours[0]) > 0)
                        currentRowAdjustedWorkingHours = Convert.ToInt32(strAdjustedWorkingHours[0]);
                    else
                        currentRowAdjustedWorkingHours = 0;

                    if (Convert.ToInt32(strAdjustedWorkingHours[1]) > 0)
                        currentRowAdjustedWorkingMins = Convert.ToInt32(strAdjustedWorkingHours[1]);
                    else
                        currentRowAdjustedWorkingMins = 0;
                }
                else
                {
                    currentRowAdjustedWorkingHours = Convert.ToInt32(txtWorkingHours.Text);
                    currentRowAdjustedWorkingMins = 0;
                }
            }
            else
            {
                currentRowAdjustedWorkingHours = 0;
                currentRowAdjustedWorkingMins = 0;
            }

            currentRowAdjustedWorkingTotalMins = currentRowAdjustedWorkingHours * 60 + currentRowAdjustedWorkingMins;

            if ((Convert.ToInt32(hdBalanceworkingMins.Value) + Convert.ToInt32(currentRowAdjustedWorkingTotalMins)) <= Convert.ToInt32(hdHoursMins.Value))
            {
                workingHours = Convert.ToString(Convert.ToInt32(currentRowAdjustedWorkingTotalMins) / 60 + ":" + Convert.ToInt32(currentRowAdjustedWorkingTotalMins) % 60);
                int value = objTimesheet.UpdateWorkerTimesheet(0, recordID, JOBNo, workingHours, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                if (value > 0)
                {
                    HideUpdateMessage();
                    SuccessMessage("Record updated successfully..!");
                    GetTimesheetList();
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again..!");
                    return;
                }
            }
            else
            {
                ModalPopupExtender1.Show();
                ExceptionUpdateMessage("Invalid working hours..!");
                return;
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

    private void HideUpdateMessage()
    {
        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;
    }


    #endregion

}

//public static class PublicValuesForWorkerTimesheetUpdations
//{
//    public static int recordID = 0;
//}