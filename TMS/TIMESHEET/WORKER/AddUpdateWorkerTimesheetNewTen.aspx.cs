using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_AddUpdateWorkerTimesheetNewTen : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsWorker = new DataSet();
    DataSet dsJOBNo = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEmployeeList = new DataSet();
    DataTable dt = new DataTable();

    DataSet dsProjectHead = new DataSet();
    DataSet dsProjectSupervisor = new DataSet();
    bool chk = false;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HideMessagePanel();
                Session["dsEmployeeList"] = null;
                Session["dtChanged"] = null;
                Session["dtTemp"] = null;
                //txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                hdDate.Value = txtDate.Text;
                hdCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                BindJOBNo();
                BindUnit();

                dsEmployeeList = objCommon.GetEmployeeByEmpRecordID(0);
                Session["dsEmployeeList"] = dsEmployeeList;

                ddlProjectHead.Items.Insert(0, "All");
                ddlProjectHead.SelectedIndex = 0;

                ddlProjectSupervisor.Items.Insert(0, "All");
                ddlProjectSupervisor.SelectedIndex = 0;

                ddlJOBNo.SelectedIndex = 1;
                txtDate.Text = Convert.ToDateTime("12-01-2018").ToString("dd-MMM-yyyy");
                hdDate.Value = txtDate.Text;
                hdCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
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

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        BindWorkerTimesheet();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        AddWorkerTimesheetNewOne();
    }

    protected void gvWorkerTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int hrs = 0;
            int mns = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerialNo = (Label)e.Row.FindControl("lblSerialNo");
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Button btnAddNewRecord = (Button)e.Row.FindControl("btnAddNewRecord");
                TextBox txtWorkingHours = (TextBox)e.Row.FindControl("txtWorkingHours");
                DropDownList ddlJOBNos = (DropDownList)e.Row.FindControl("ddlJOBNo");

                Label lblAdjustedWorkingHours = (Label)e.Row.FindControl("lblAdjustedWorkingHours");
                DropDownList ddlHours = (DropDownList)e.Row.FindControl("ddlHours");
                DropDownList ddlMins = (DropDownList)e.Row.FindControl("ddlMins");

                DataSet dsJOBNo = (DataSet)Session["dsJOBNo"];
                if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
                {
                    ddlJOBNos.DataSource = dsJOBNo.Tables[0];
                    ddlJOBNos.DataTextField = "JOB_NUMBER";
                    ddlJOBNos.DataValueField = "JOB_NUMBER";
                    ddlJOBNos.DataBind();
                    ddlJOBNos.Items.Insert(0, "SELECT");
                }



                if (!string.IsNullOrEmpty(lblAdjustedWorkingHours.Text) && Convert.ToString(lblAdjustedWorkingHours.Text) != "00:00" && Convert.ToString(lblAdjustedWorkingHours.Text) != "0:0")
                {
                    string[] srtWorkingHours = Convert.ToString(lblAdjustedWorkingHours.Text).Split(':');

                    if (Convert.ToInt32(srtWorkingHours[0]) > 0 && !string.IsNullOrEmpty(Convert.ToString(srtWorkingHours[0])))
                        hrs = Convert.ToInt32(srtWorkingHours[0]);
                    else
                        hrs = 0;

                    if (hrs > 0)
                        ddlHours.SelectedValue = Convert.ToString(hrs);
                    else
                        ddlHours.SelectedValue = "0";

                    if (Convert.ToInt32(srtWorkingHours[1]) > 0 && !string.IsNullOrEmpty(Convert.ToString(srtWorkingHours[1])))
                        mns = Convert.ToInt32(srtWorkingHours[1]);
                    else
                        mns = 0;

                    if (mns > 0)
                        ddlMins.SelectedValue = Convert.ToString(mns);
                    else
                        ddlMins.SelectedValue = "0";
                }
                else
                {
                    ddlHours.SelectedValue = "0";
                    ddlMins.SelectedValue = "0";
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
            DataTable dt = new DataTable();
            hdTotalMinuts.Value = "0";
            dt.Columns.Add("SERIAL_NO", typeof(string));
            dt.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dt.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dt.Columns.Add("UNIT", typeof(string));
            dt.Columns.Add("JOB_NO", typeof(string));
            dt.Columns.Add("ENTRY_DATE", typeof(string));
            dt.Columns.Add("IN_TIME", typeof(string));
            dt.Columns.Add("OUT_TIME", typeof(string));
            dt.Columns.Add("WORKING_HOURS", typeof(string));
            dt.Columns.Add("WORKING_MINUTS", typeof(string));
            dt.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dt.Columns.Add("OT_HOURS", typeof(string));
            dt.Columns.Add("BAL_WRK_HRS", typeof(string));

            dt.Columns.Add("REMARKS", typeof(string));

            int rindex = 0;
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "ADD" || e.CommandArgument == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                #region ADD[===================]

                if (e.CommandArgument == "ADD")
                {
                    int hoursOfCurrentRowAdjustedHours = 0;
                    int minutsOfCurrentRowAdjustedHours = 0;
                    int totalMinutsOfCurrentRowAdjustedHours = 0;

                    TextBox txtCurrentRowAdjustedWorkingHours = (TextBox)gvWorkerTimesheetList.Rows[rowindex].FindControl("txtAdjustedWorkingHours");
                    ImageButton imgBtnAddNewRecord = (ImageButton)gvWorkerTimesheetList.Rows[rowindex].FindControl("imgBtnAddNewRecord");
                    if (!string.IsNullOrEmpty(txtCurrentRowAdjustedWorkingHours.Text))
                    {
                        if (txtCurrentRowAdjustedWorkingHours.Text.Contains(':'))
                        {
                            string[] strCurrentRowAdjustedHours = txtCurrentRowAdjustedWorkingHours.Text.Split(':');

                            if (Convert.ToInt32(strCurrentRowAdjustedHours[0]) > 0)
                                hoursOfCurrentRowAdjustedHours = Convert.ToInt32(strCurrentRowAdjustedHours[0]);
                            else
                                hoursOfCurrentRowAdjustedHours = 0;

                            if (Convert.ToInt32(strCurrentRowAdjustedHours[1]) > 0)
                                minutsOfCurrentRowAdjustedHours = Convert.ToInt32(strCurrentRowAdjustedHours[1]);
                            else
                                minutsOfCurrentRowAdjustedHours = 0;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtCurrentRowAdjustedWorkingHours.Text) && Convert.ToInt32(txtCurrentRowAdjustedWorkingHours.Text) > 0)
                                hoursOfCurrentRowAdjustedHours = Convert.ToInt32(txtCurrentRowAdjustedWorkingHours.Text);
                            else
                                hoursOfCurrentRowAdjustedHours = 0;
                        }
                    }
                    else
                    {
                        hoursOfCurrentRowAdjustedHours = 0;
                        minutsOfCurrentRowAdjustedHours = 0;
                    }

                    totalMinutsOfCurrentRowAdjustedHours = (hoursOfCurrentRowAdjustedHours * 60 + minutsOfCurrentRowAdjustedHours);

                    if (totalMinutsOfCurrentRowAdjustedHours > 0)
                    {
                        foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                        {
                            DataRow dr = dt.NewRow();
                            rindex++;

                            Label lblEmployeeName = (Label)gr.FindControl("lblEmployeeName");
                            Label lblEmpCode = (Label)gr.FindControl("lblEmpCode");
                            Label lblUnit = (Label)gr.FindControl("lblUnit");
                            DropDownList ddlJOBNo = (DropDownList)gr.FindControl("ddlJOBNo");
                            Label lblEntryDate = (Label)gr.FindControl("lblEntryDate");
                            Label lblInTime = (Label)gr.FindControl("lblInTime");
                            Label lblOutTime = (Label)gr.FindControl("lblOutTime");
                            Label lblWorkingHours = (Label)gr.FindControl("lblWorkingHours");
                            Label lblWorkingMinuts = (Label)gr.FindControl("lblWorkingMinuts");
                            TextBox txtAdjustedWorkingHours = (TextBox)gr.FindControl("txtAdjustedWorkingHours");
                            Label lblOTHours = (Label)gr.FindControl("lblOTHours");
                            Label lblBalanceWorkingHours = (Label)gr.FindControl("lblBalanceWorkingHours");
                            DropDownList ddlHours = (DropDownList)gr.FindControl("ddlHours");
                            DropDownList ddlMins = (DropDownList)gr.FindControl("ddlMins");


                            TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");

                            dr["SERIAL_NO"] = Convert.ToString(rindex);

                            if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                                dr["EMPLOYEE_NAME"] = lblEmployeeName.Text;

                            if (!string.IsNullOrEmpty(lblEmpCode.Text))
                                dr["EMPLOYEE_CODE"] = lblEmpCode.Text;

                            if (!string.IsNullOrEmpty(lblUnit.Text))
                                dr["UNIT"] = lblUnit.Text;

                            if (ddlJOBNo.SelectedIndex > 0)
                                dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);

                            if (!string.IsNullOrEmpty(lblEntryDate.Text))
                                dr["ENTRY_DATE"] = lblEntryDate.Text;

                            if (!string.IsNullOrEmpty(lblInTime.Text))
                                dr["IN_TIME"] = lblInTime.Text;

                            if (!string.IsNullOrEmpty(lblOutTime.Text))
                                dr["OUT_TIME"] = lblOutTime.Text;

                            if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                                dr["WORKING_HOURS"] = lblWorkingHours.Text;

                            if (!string.IsNullOrEmpty(lblWorkingMinuts.Text))
                                dr["WORKING_MINUTS"] = lblWorkingMinuts.Text;

                            if (!string.IsNullOrEmpty(txtAdjustedWorkingHours.Text))
                                dr["ADJUSTED_WORKING_HOURS"] = txtAdjustedWorkingHours.Text;

                            if (!string.IsNullOrEmpty(lblOTHours.Text))
                                dr["OT_HOURS"] = lblOTHours.Text;

                            if (!string.IsNullOrEmpty(txtRemarks.Text))
                                dr["REMARKS"] = txtRemarks.Text;

                            dt.Rows.Add(dr);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            int totalWorkingMinuts = 0;

                            DataRow drnew = dt.NewRow();

                            drnew["SERIAL_NO"] = Convert.ToString(Convert.ToInt32(dt.Rows[rowindex]["SERIAL_NO"]) + Convert.ToInt32(gvWorkerTimesheetList.Rows.Count));
                            drnew["EMPLOYEE_NAME"] = Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_NAME"]);
                            drnew["EMPLOYEE_CODE"] = Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_CODE"]);
                            drnew["WORKING_HOURS"] = Convert.ToString(dt.Rows[rowindex]["WORKING_HOURS"]);
                            drnew["WORKING_MINUTS"] = Convert.ToString(dt.Rows[rowindex]["WORKING_MINUTS"]);

                            int adjustedWorkingHours = 0;
                            int adjustedWorkingMins = 0;
                            int adjustedWorkingTotalMins = 0;

                            foreach (DataRow dr in dt.Select("EMPLOYEE_NAME='" + Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_NAME"]) + "' AND EMPLOYEE_CODE='" + Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_CODE"]) + "'"))
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr["ADJUSTED_WORKING_HOURS"])))
                                {
                                    if (Convert.ToString(dr["ADJUSTED_WORKING_HOURS"]).Contains(':'))
                                    {
                                        string[] strAdjustedWorkingHours = Convert.ToString(dr["ADJUSTED_WORKING_HOURS"]).Split(':');

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
                                        adjustedWorkingHours = Convert.ToInt32(Convert.ToString(dr["ADJUSTED_WORKING_HOURS"]));
                                        adjustedWorkingMins = 0;
                                    }
                                }
                                else
                                {
                                    adjustedWorkingHours = 0;
                                    adjustedWorkingMins = 0;
                                }

                                adjustedWorkingTotalMins += adjustedWorkingHours * 60 + adjustedWorkingMins;
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(drnew["WORKING_MINUTS"])) && Convert.ToInt32(drnew["WORKING_MINUTS"]) > 0)
                                totalWorkingMinuts = Convert.ToInt32(drnew["WORKING_MINUTS"]);
                            else
                                totalWorkingMinuts = 0;


                            int balanceWorkingMinuts = 0;

                            balanceWorkingMinuts = totalWorkingMinuts - adjustedWorkingTotalMins;

                            if (!string.IsNullOrEmpty(Convert.ToString(balanceWorkingMinuts)))
                            {
                                string balWorkingHrs = string.Empty;
                                string balWorkingMins = string.Empty;

                                if (Convert.ToInt32(balanceWorkingMinuts / 60) < 10)
                                    balWorkingHrs = Convert.ToString("0" + Convert.ToInt32(balanceWorkingMinuts / 60));
                                else
                                    balWorkingHrs = Convert.ToString(balanceWorkingMinuts / 60);

                                if (Convert.ToInt32(balanceWorkingMinuts % 60) < 10)
                                    balWorkingMins = Convert.ToString("0" + Convert.ToInt32(balanceWorkingMinuts % 60));
                                else
                                    balWorkingMins = Convert.ToString(balanceWorkingMinuts % 60);

                                drnew["ADJUSTED_WORKING_HOURS"] = (balWorkingHrs + ":" + balWorkingMins);
                            }
                            else
                            {
                                drnew["ADJUSTED_WORKING_HOURS"] = string.Empty;
                            }

                            if (balanceWorkingMinuts > 0)
                            {
                                dt.Rows.InsertAt(drnew, rowindex + 1);
                            }


                            imgBtnAddNewRecord.Visible = false;
                            if (dt.Rows.Count > 0)
                            {
                                chk = true;
                                Session["dtChanged"] = dt;
                                gvWorkerTimesheetList.DataSource = dt.DefaultView;
                                gvWorkerTimesheetList.DataBind();
                            }
                            else
                            {
                                chk = false;
                                Session["dtChanged"] = null;
                                gvWorkerTimesheetList.DataSource = null;
                                gvWorkerTimesheetList.DataBind();
                            }
                        }
                    }
                    else
                    {
                        //
                    }
                }

                #endregion

                #region REMOVE[================]

                else if (e.CommandArgument == "REMOVE")
                {
                    Label lblSerialNo = (Label)gvWorkerTimesheetList.Rows[rowindex].FindControl("lblSerialNo");
                    Label lblWorkingMinuts = (Label)gvWorkerTimesheetList.Rows[rowindex].FindControl("lblWorkingMinuts");
                    Label lblAdjustedWorkingHours = (Label)gvWorkerTimesheetList.Rows[rowindex].FindControl("lblAdjustedWorkingHours");
                    TextBox txtAdjustedWorkingHours = (TextBox)gvWorkerTimesheetList.Rows[rowindex].FindControl("txtAdjustedWorkingHours");

                    foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        rindex++;

                        Label lblEmployeeName = (Label)gr.FindControl("lblEmployeeName");
                        Label lblEmpCode = (Label)gr.FindControl("lblEmpCode");
                        Label lblUnit = (Label)gr.FindControl("lblUnit");
                        DropDownList ddlJOBNo = (DropDownList)gr.FindControl("ddlJOBNo");
                        Label lblEntryDate = (Label)gr.FindControl("lblEntryDate");
                        Label lblInTime = (Label)gr.FindControl("lblInTime");
                        Label lblOutTime = (Label)gr.FindControl("lblOutTime");
                        Label lblWorkingHours = (Label)gr.FindControl("lblWorkingHours");
                        Label lblWorkingMinutsGR = (Label)gr.FindControl("lblWorkingMinuts");
                        TextBox txtAdjustedWorkingHoursGR = (TextBox)gr.FindControl("txtAdjustedWorkingHours");
                        Label lblOTHours = (Label)gr.FindControl("lblOTHours");
                        Label lblBalanceWorkingHours = (Label)gr.FindControl("lblBalanceWorkingHours");
                        DropDownList ddlHours = (DropDownList)gr.FindControl("ddlHours");
                        DropDownList ddlMins = (DropDownList)gr.FindControl("ddlMins");


                        TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");

                        dr["SERIAL_NO"] = Convert.ToString(rindex);

                        if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                            dr["EMPLOYEE_NAME"] = lblEmployeeName.Text;

                        if (!string.IsNullOrEmpty(lblEmpCode.Text))
                            dr["EMPLOYEE_CODE"] = lblEmpCode.Text;

                        if (!string.IsNullOrEmpty(lblUnit.Text))
                            dr["UNIT"] = lblUnit.Text;

                        if (ddlJOBNo.SelectedIndex > 0)
                            dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);

                        if (!string.IsNullOrEmpty(lblEntryDate.Text))
                            dr["ENTRY_DATE"] = lblEntryDate.Text;

                        if (!string.IsNullOrEmpty(lblInTime.Text))
                            dr["IN_TIME"] = lblInTime.Text;

                        if (!string.IsNullOrEmpty(lblOutTime.Text))
                            dr["OUT_TIME"] = lblOutTime.Text;

                        if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                            dr["WORKING_HOURS"] = lblWorkingHours.Text;

                        if (!string.IsNullOrEmpty(lblWorkingMinutsGR.Text))
                            dr["WORKING_MINUTS"] = lblWorkingMinutsGR.Text;

                        if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text))
                            dr["ADJUSTED_WORKING_HOURS"] = txtAdjustedWorkingHoursGR.Text;

                        if (!string.IsNullOrEmpty(lblOTHours.Text))
                            dr["OT_HOURS"] = lblOTHours.Text;

                        if (!string.IsNullOrEmpty(txtRemarks.Text))
                            dr["REMARKS"] = txtRemarks.Text;

                        dt.Rows.Add(dr);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        Session["dtChanged"] = dt;
                        RemoveRecords((DataTable)Session["dtChanged"], Convert.ToString(lblSerialNo.Text));
                    }
                    else
                    {
                        Session["dtChanged"] = null;
                    }
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlHours_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

            string balanceWorkingHrs = string.Empty;
            string hours = string.Empty;
            string mins = string.Empty;
            DropDownList ddlHours = (DropDownList)gvr.FindControl("ddlHours");
            DropDownList ddlMins = (DropDownList)gvr.FindControl("ddlMins");
            TextBox txtAdjustedWorkingHours = (TextBox)gvr.FindControl("txtAdjustedWorkingHours");
            Label lblSerialNo = (Label)gvr.FindControl("lblSerialNo");

            if (Convert.ToInt32(ddlHours.SelectedValue) < 10)
                hours = "0" + Convert.ToString(ddlHours.SelectedValue);
            else
                hours = Convert.ToString(ddlHours.SelectedValue);

            if (Convert.ToInt32(ddlMins.SelectedValue) < 10)
                mins = "0" + Convert.ToString(ddlMins.SelectedValue);
            else
                mins = Convert.ToString(ddlMins.SelectedValue);

            txtAdjustedWorkingHours.Text = hours + ":" + mins;

            Label lblWorkingHours = (Label)gvr.FindControl("lblWorkingHours");
            Label lblEmployeeName = (Label)gvr.FindControl("lblEmployeeName");
            Label lblEmpCode = (Label)gvr.FindControl("lblEmpCode");
            Label lblBalanceWorkingHours = (Label)gvr.FindControl("lblBalanceWorkingHours");

            balanceWorkingHrs = Convert.ToString(lblBalanceWorkingHours.Text);

            int wrkHrs = 0;
            int wrkMins = 0;
            int wrkTotalTime = 0;

            if (!string.IsNullOrEmpty(lblWorkingHours.Text))
            {
                if (lblWorkingHours.Text.Contains(':'))
                {
                    string[] srtWrkHours = lblWorkingHours.Text.Split(':');

                    if (Convert.ToInt32(srtWrkHours[0]) > 0)
                        wrkHrs = Convert.ToInt32(srtWrkHours[0]);
                    else
                        wrkHrs = 0;

                    if (Convert.ToInt32(srtWrkHours[1]) > 0)
                        wrkMins = Convert.ToInt32(srtWrkHours[1]);
                    else
                        wrkMins = 0;
                }
                else
                {
                    if (!string.IsNullOrEmpty(lblWorkingHours.Text) && Convert.ToInt32(lblWorkingHours.Text) > 0)
                        wrkHrs = Convert.ToInt32(lblWorkingHours.Text);
                    else
                        wrkHrs = 0;
                }
            }
            else
            {
                wrkHrs = 0;
                wrkMins = 0;
            }
            wrkTotalTime = (wrkHrs * 60) + wrkMins;




            int adjHrs = 0;
            int adjMins = 0;
            int adjTotalHrs = 0;
            int adjTotalMins = 0;
            int adjTotalTime = 0;


            int adjCurrentHrs = 0;
            int adjCurrentMins = 0;
            int adjCurrentTotalTime = 0;

            foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
            {
                Label lblSerialNoGR = (Label)gr.FindControl("lblSerialNo");
                Label lblEmployeeNameGR = (Label)gr.FindControl("lblEmployeeName");
                Label lblEmpCodeGR = (Label)gr.FindControl("lblEmpCode");
                TextBox txtAdjustedWorkingHoursGR = (TextBox)gr.FindControl("txtAdjustedWorkingHours");
                Label lblBalanceWorkingHoursGR = (Label)gr.FindControl("lblBalanceWorkingHours");

                lblBalanceWorkingHoursGR.Text = string.Empty;

                if (lblEmployeeName.Text == lblEmployeeNameGR.Text && lblEmpCode.Text == lblEmpCodeGR.Text)
                {
                    if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text))
                    {
                        if (txtAdjustedWorkingHoursGR.Text.Contains(':'))
                        {
                            string[] srtAdjustedHours = txtAdjustedWorkingHoursGR.Text.Split(':');

                            if (Convert.ToInt32(srtAdjustedHours[0]) > 0)
                                adjHrs = Convert.ToInt32(srtAdjustedHours[0]);
                            else
                                adjHrs = 0;

                            if (Convert.ToInt32(srtAdjustedHours[1]) > 0)
                                adjMins = Convert.ToInt32(srtAdjustedHours[1]);
                            else
                                adjMins = 0;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text) && Convert.ToInt32(txtAdjustedWorkingHoursGR.Text) > 0)
                                adjHrs = Convert.ToInt32(txtAdjustedWorkingHoursGR.Text);
                            else
                                adjHrs = 0;
                        }
                    }
                    else
                    {
                        adjHrs = 0;
                        adjMins = 0;
                    }

                    adjTotalHrs += adjHrs;
                    adjTotalMins += adjMins;



                    if (lblSerialNo.Text == lblSerialNoGR.Text)
                    {
                        if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text))
                        {
                            if (txtAdjustedWorkingHoursGR.Text.Contains(':'))
                            {
                                string[] srtAdjustedHours = txtAdjustedWorkingHoursGR.Text.Split(':');

                                if (Convert.ToInt32(srtAdjustedHours[0]) > 0)
                                    adjCurrentHrs = Convert.ToInt32(srtAdjustedHours[0]);
                                else
                                    adjCurrentHrs = 0;

                                if (Convert.ToInt32(srtAdjustedHours[1]) > 0)
                                    adjCurrentMins = Convert.ToInt32(srtAdjustedHours[1]);
                                else
                                    adjCurrentMins = 0;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text) && Convert.ToInt32(txtAdjustedWorkingHoursGR.Text) > 0)
                                    adjCurrentHrs = Convert.ToInt32(txtAdjustedWorkingHoursGR.Text);
                                else
                                    adjCurrentHrs = 0;
                            }
                        }
                        else
                        {
                            adjCurrentHrs = 0;
                            adjCurrentMins = 0;
                        }
                    }
                    adjCurrentTotalTime = (adjCurrentHrs * 60) + adjCurrentMins;
                }
            }
            adjTotalTime = (adjTotalHrs * 60) + adjTotalMins;

            int newBalanceTime = 0;
            int completeBalanceTime = 0;

            string balHrs = string.Empty;
            string balMins = string.Empty;

            completeBalanceTime = wrkTotalTime - adjTotalTime;

            if (wrkTotalTime >= adjTotalTime)
            {
                if (Convert.ToInt32(completeBalanceTime / 60) < 10)
                    balHrs = Convert.ToString("0" + Convert.ToInt32(completeBalanceTime / 60));
                else
                    balHrs = Convert.ToString(completeBalanceTime / 60);

                if (Convert.ToInt32(completeBalanceTime % 60) < 10)
                    balMins = Convert.ToString("0" + Convert.ToInt32(completeBalanceTime % 60));
                else
                    balMins = Convert.ToString(completeBalanceTime % 60);

                lblBalanceWorkingHours.Text = balHrs + ":" + balMins;
                lblBalanceWorkingHours.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                if (completeBalanceTime < 0)
                    completeBalanceTime = completeBalanceTime * (-1);

                newBalanceTime = adjCurrentTotalTime - completeBalanceTime;

                if (Convert.ToInt32(newBalanceTime / 60) < 10)
                    balHrs = Convert.ToString("0" + Convert.ToInt32(newBalanceTime / 60));
                else
                    balHrs = Convert.ToString(newBalanceTime / 60);

                if (Convert.ToInt32(newBalanceTime % 60) < 10)
                    balMins = Convert.ToString("0" + Convert.ToInt32(newBalanceTime % 60));
                else
                    balMins = Convert.ToString(newBalanceTime % 60);

                ddlHours.SelectedValue = Convert.ToString(Convert.ToInt32(balHrs));
                ddlMins.SelectedValue = Convert.ToString(Convert.ToInt32(balMins));

                txtAdjustedWorkingHours.Text = balHrs + ":" + balMins;
                lblBalanceWorkingHours.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlMins_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

            string balanceWorkingHrs = string.Empty;
            string hours = string.Empty;
            string mins = string.Empty;
            DropDownList ddlHours = (DropDownList)gvr.FindControl("ddlHours");
            DropDownList ddlMins = (DropDownList)gvr.FindControl("ddlMins");
            TextBox txtAdjustedWorkingHours = (TextBox)gvr.FindControl("txtAdjustedWorkingHours");
            Label lblSerialNo = (Label)gvr.FindControl("lblSerialNo");

            if (Convert.ToInt32(ddlHours.SelectedValue) < 10)
                hours = "0" + Convert.ToString(ddlHours.SelectedValue);
            else
                hours = Convert.ToString(ddlHours.SelectedValue);

            if (Convert.ToInt32(ddlMins.SelectedValue) < 10)
                mins = "0" + Convert.ToString(ddlMins.SelectedValue);
            else
                mins = Convert.ToString(ddlMins.SelectedValue);

            txtAdjustedWorkingHours.Text = hours + ":" + mins;

            Label lblWorkingHours = (Label)gvr.FindControl("lblWorkingHours");
            Label lblEmployeeName = (Label)gvr.FindControl("lblEmployeeName");
            Label lblEmpCode = (Label)gvr.FindControl("lblEmpCode");
            Label lblBalanceWorkingHours = (Label)gvr.FindControl("lblBalanceWorkingHours");

            balanceWorkingHrs = Convert.ToString(lblBalanceWorkingHours.Text);

            int wrkHrs = 0;
            int wrkMins = 0;
            int wrkTotalTime = 0;

            if (!string.IsNullOrEmpty(lblWorkingHours.Text))
            {
                if (lblWorkingHours.Text.Contains(':'))
                {
                    string[] srtWrkHours = lblWorkingHours.Text.Split(':');

                    if (Convert.ToInt32(srtWrkHours[0]) > 0)
                        wrkHrs = Convert.ToInt32(srtWrkHours[0]);
                    else
                        wrkHrs = 0;

                    if (Convert.ToInt32(srtWrkHours[1]) > 0)
                        wrkMins = Convert.ToInt32(srtWrkHours[1]);
                    else
                        wrkMins = 0;
                }
                else
                {
                    if (!string.IsNullOrEmpty(lblWorkingHours.Text) && Convert.ToInt32(lblWorkingHours.Text) > 0)
                        wrkHrs = Convert.ToInt32(lblWorkingHours.Text);
                    else
                        wrkHrs = 0;
                }
            }
            else
            {
                wrkHrs = 0;
                wrkMins = 0;
            }
            wrkTotalTime = (wrkHrs * 60) + wrkMins;




            int adjHrs = 0;
            int adjMins = 0;
            int adjTotalHrs = 0;
            int adjTotalMins = 0;
            int adjTotalTime = 0;


            int adjCurrentHrs = 0;
            int adjCurrentMins = 0;
            int adjCurrentTotalTime = 0;

            foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
            {
                Label lblSerialNoGR = (Label)gr.FindControl("lblSerialNo");
                Label lblEmployeeNameGR = (Label)gr.FindControl("lblEmployeeName");
                Label lblEmpCodeGR = (Label)gr.FindControl("lblEmpCode");
                TextBox txtAdjustedWorkingHoursGR = (TextBox)gr.FindControl("txtAdjustedWorkingHours");
                Label lblBalanceWorkingHoursGR = (Label)gr.FindControl("lblBalanceWorkingHours");

                lblBalanceWorkingHoursGR.Text = string.Empty;

                if (lblEmployeeName.Text == lblEmployeeNameGR.Text && lblEmpCode.Text == lblEmpCodeGR.Text)
                {
                    if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text))
                    {
                        if (txtAdjustedWorkingHoursGR.Text.Contains(':'))
                        {
                            string[] srtAdjustedHours = txtAdjustedWorkingHoursGR.Text.Split(':');

                            if (Convert.ToInt32(srtAdjustedHours[0]) > 0)
                                adjHrs = Convert.ToInt32(srtAdjustedHours[0]);
                            else
                                adjHrs = 0;

                            if (Convert.ToInt32(srtAdjustedHours[1]) > 0)
                                adjMins = Convert.ToInt32(srtAdjustedHours[1]);
                            else
                                adjMins = 0;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text) && Convert.ToInt32(txtAdjustedWorkingHoursGR.Text) > 0)
                                adjHrs = Convert.ToInt32(txtAdjustedWorkingHoursGR.Text);
                            else
                                adjHrs = 0;
                        }
                    }
                    else
                    {
                        adjHrs = 0;
                        adjMins = 0;
                    }

                    adjTotalHrs += adjHrs;
                    adjTotalMins += adjMins;



                    if (lblSerialNo.Text == lblSerialNoGR.Text)
                    {
                        if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text))
                        {
                            if (txtAdjustedWorkingHoursGR.Text.Contains(':'))
                            {
                                string[] srtAdjustedHours = txtAdjustedWorkingHoursGR.Text.Split(':');

                                if (Convert.ToInt32(srtAdjustedHours[0]) > 0)
                                    adjCurrentHrs = Convert.ToInt32(srtAdjustedHours[0]);
                                else
                                    adjCurrentHrs = 0;

                                if (Convert.ToInt32(srtAdjustedHours[1]) > 0)
                                    adjCurrentMins = Convert.ToInt32(srtAdjustedHours[1]);
                                else
                                    adjCurrentMins = 0;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtAdjustedWorkingHoursGR.Text) && Convert.ToInt32(txtAdjustedWorkingHoursGR.Text) > 0)
                                    adjCurrentHrs = Convert.ToInt32(txtAdjustedWorkingHoursGR.Text);
                                else
                                    adjCurrentHrs = 0;
                            }
                        }
                        else
                        {
                            adjCurrentHrs = 0;
                            adjCurrentMins = 0;
                        }
                    }
                    adjCurrentTotalTime = (adjCurrentHrs * 60) + adjCurrentMins;
                }
            }
            adjTotalTime = (adjTotalHrs * 60) + adjTotalMins;

            int newBalanceTime = 0;
            int completeBalanceTime = 0;

            string balHrs = string.Empty;
            string balMins = string.Empty;

            completeBalanceTime = wrkTotalTime - adjTotalTime;

            if (wrkTotalTime >= adjTotalTime)
            {
                if (Convert.ToInt32(completeBalanceTime / 60) < 10)
                    balHrs = Convert.ToString("0" + Convert.ToInt32(completeBalanceTime / 60));
                else
                    balHrs = Convert.ToString(completeBalanceTime / 60);

                if (Convert.ToInt32(completeBalanceTime % 60) < 10)
                    balMins = Convert.ToString("0" + Convert.ToInt32(completeBalanceTime % 60));
                else
                    balMins = Convert.ToString(completeBalanceTime % 60);

                lblBalanceWorkingHours.Text = balHrs + ":" + balMins;
                lblBalanceWorkingHours.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                if (completeBalanceTime < 0)
                    completeBalanceTime = completeBalanceTime * (-1);

                newBalanceTime = adjCurrentTotalTime - completeBalanceTime;

                if (Convert.ToInt32(newBalanceTime / 60) < 10)
                    balHrs = Convert.ToString("0" + Convert.ToInt32(newBalanceTime / 60));
                else
                    balHrs = Convert.ToString(newBalanceTime / 60);

                if (Convert.ToInt32(newBalanceTime % 60) < 10)
                    balMins = Convert.ToString("0" + Convert.ToInt32(newBalanceTime % 60));
                else
                    balMins = Convert.ToString(newBalanceTime % 60);

                ddlHours.SelectedValue = Convert.ToString(Convert.ToInt32(balHrs));
                ddlMins.SelectedValue = Convert.ToString(Convert.ToInt32(balMins));

                txtAdjustedWorkingHours.Text = balHrs + ":" + balMins;
                lblBalanceWorkingHours.Text = string.Empty;
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

    private void BindJOBNo()
    {
        try
        {
            dsJOBNo = objTimesheet.GetDetailsBySP("sp_get_distinct_job_no");
            if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
            {
                Session["dsJOBNo"] = dsJOBNo;
                ddlJOBNo.DataSource = dsJOBNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NUMBER";
                ddlJOBNo.DataValueField = "JOB_NUMBER";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "Select");
            }
            else
            {
                Session["dsJOBNo"] = null;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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
        dsProjectSupervisor = objTimesheet.GetProjectSupervisorList(Convert.ToInt32(ddlProjectHead.SelectedValue),0);
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

    private void BindWorkerTimesheet()
    {
        try
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("SERIAL_NO", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtTemp.Columns.Add("UNIT", typeof(string));
            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("ENTRY_DATE", typeof(string));
            dtTemp.Columns.Add("IN_TIME", typeof(string));
            dtTemp.Columns.Add("OUT_TIME", typeof(string));
            dtTemp.Columns.Add("WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("WORKING_MINUTS", typeof(string));
            dtTemp.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("OT_HOURS", typeof(string));
            dtTemp.Columns.Add("BAL_WRK_HRS", typeof(string));

            dtTemp.Columns.Add("REMARKS", typeof(string));

            string entryDate = string.Empty;
            int unitID = 0;
            int projectHeadID = 0;
            int supervisorID = 0;

            entryDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

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

            //dsWorker = objTimesheet.GetWorkerListAll(entryDate, unitID, projectHeadID, supervisorID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drWorder in dsWorker.Tables[0].Rows)
                {
                    DataRow drTemp = dtTemp.NewRow();

                    drTemp["SERIAL_NO"] = drWorder["SERIAL_NO"];
                    drTemp["EMPLOYEE_NAME"] = drWorder["EMPLOYEE_NAME"];
                    drTemp["EMPLOYEE_CODE"] = drWorder["EMPLOYEE_CODE"];
                    drTemp["UNIT"] = drWorder["UNIT_NAME"];
                    drTemp["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);
                    drTemp["ENTRY_DATE"] = drWorder["ENTRY_DATE"];
                    drTemp["IN_TIME"] = drWorder["IN_TIME"];
                    drTemp["OUT_TIME"] = drWorder["OUT_TIME"];
                    drTemp["WORKING_HOURS"] = drWorder["WORKING_HOURS"];
                    drTemp["WORKING_MINUTS"] = drWorder["WORKING_MINUTS"];
                    drTemp["ADJUSTED_WORKING_HOURS"] = drWorder["ADJUSTED_WORKING_HOURS"];
                    drTemp["OT_HOURS"] = drWorder["OT_HOURS"];
                    drTemp["BAL_WRK_HRS"] = drWorder["BAL_WRK_HRS"];

                    drTemp["REMARKS"] = drWorder["REMARKS"];

                    dtTemp.Rows.Add(drTemp);
                }

                if (dtTemp.Rows.Count > 0)
                {
                    Session["dtTemp"] = dtTemp;
                    Session["dtChanged"] = dtTemp;
                    gvWorkerTimesheetList.DataSource = dtTemp.DefaultView;
                    gvWorkerTimesheetList.DataBind();

                }
                else
                {
                    Session["dtTemp"] = null;
                    Session["dtChanged"] = null;
                    gvWorkerTimesheetList.DataSource = null;
                    gvWorkerTimesheetList.DataBind();

                }
                lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
            }
            else
            {
                Session["dtTemp"] = null;
                gvWorkerTimesheetList.DataSource = null;
                gvWorkerTimesheetList.DataBind();
                lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddWorkerTimesheetNewOne()
    {
        try
        {
            string employeeCode = string.Empty;
            string jobNo = string.Empty;
            string entryDate = string.Empty;
            string inTime = string.Empty;
            string outTime = string.Empty;
            string hours = string.Empty;
            string workingHours = string.Empty;
            string workingHoursh = string.Empty;
            string workingHoursm = string.Empty;
            string remarks = string.Empty;
            int count = 0;
            string serialNos = string.Empty;
            string serialNo = string.Empty;

            DataTable dt = (DataTable)Session["dtTemp"];
            DataTable dtTextChanged = (DataTable)Session["dtChanged"];

            string dtEmployeeName = string.Empty;
            string dtEmployeeCode = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string insertQuery = string.Empty;
                    if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                        dtEmployeeName = Convert.ToString(dr["EMPLOYEE_NAME"]);

                    if (dr["EMPLOYEE_CODE"] != DBNull.Value)
                        dtEmployeeCode = Convert.ToString(dr["EMPLOYEE_CODE"]);

                    if (gvWorkerTimesheetList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                        {
                            Label lblSerialNo = gr.FindControl("lblSerialNo") as Label;
                            Label lblEmpCode = gr.FindControl("lblEmpCode") as Label;
                            Label lblEmployeeName = gr.FindControl("lblEmployeeName") as Label;
                            DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;
                            Label lblEntryDate = gr.FindControl("lblEntryDate") as Label;
                            Label lblInTime = gr.FindControl("lblInTime") as Label;
                            Label lblOutTime = gr.FindControl("lblOutTime") as Label;
                            Label lblWorkingHours = gr.FindControl("lblWorkingHours") as Label;
                            TextBox txtAdjustedWorkingHours = gr.FindControl("txtAdjustedWorkingHours") as TextBox;

                            Label lblBalanceWorkingHours = gr.FindControl("lblBalanceWorkingHours") as Label;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            if (lblEmpCode.Text == dtEmployeeCode && lblEmployeeName.Text == dtEmployeeName)
                            {
                                if (!string.IsNullOrEmpty(lblEmpCode.Text))
                                    employeeCode = Convert.ToString(lblEmpCode.Text);
                                else
                                    employeeCode = string.Empty;

                                if (!string.IsNullOrEmpty(lblEntryDate.Text))
                                    entryDate = Convert.ToDateTime(lblEntryDate.Text).ToString("yyyy-MM-dd");
                                else
                                    entryDate = string.Empty;

                                if (ddlJOBNo.SelectedIndex > 0)
                                    jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
                                else
                                    jobNo = string.Empty;

                                if (!string.IsNullOrEmpty(lblInTime.Text))
                                    inTime = Convert.ToString(lblInTime.Text);
                                else
                                    inTime = string.Empty;

                                if (!string.IsNullOrEmpty(lblOutTime.Text))
                                    outTime = Convert.ToString(lblOutTime.Text);
                                else
                                    outTime = string.Empty;

                                if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                                    hours = Convert.ToString(lblWorkingHours.Text);
                                else
                                    hours = string.Empty;

                                if (!string.IsNullOrEmpty(txtAdjustedWorkingHours.Text))
                                {
                                    if (Convert.ToString(txtAdjustedWorkingHours.Text).Contains(':'))
                                    {
                                        string[] strtext = Convert.ToString(txtAdjustedWorkingHours.Text).Split(':');
                                        if (Convert.ToInt32(strtext[0]) > 0)
                                            workingHoursh = Convert.ToString(strtext[0]);
                                        else
                                            workingHoursh = "00";

                                        if (Convert.ToInt32(strtext[1]) > 0)
                                            workingHoursm = Convert.ToString(strtext[1]);
                                        else
                                            workingHoursm = "00";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(txtAdjustedWorkingHours.Text) > 0)
                                            workingHoursh = Convert.ToString(txtAdjustedWorkingHours.Text);
                                        else
                                            workingHoursh = "00";

                                        workingHoursm = "00";
                                    }
                                    workingHours = workingHoursh + ":" + workingHoursm;
                                }
                                else
                                    workingHours = "00:00";

                                if (!string.IsNullOrEmpty(txtRemarks.Text))
                                    remarks = Convert.ToString(txtRemarks.Text);
                                else
                                    remarks = string.Empty;

                                if (!string.IsNullOrEmpty(employeeCode) && !string.IsNullOrEmpty(workingHours) && workingHours != "00:00")
                                {
                                    insertQuery += "('" + employeeCode + "','" + entryDate + "','" + jobNo + "','" + inTime + "','" + outTime + "','" + hours + "','" + workingHours + "','" + remarks + "'," + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",GETDATE()),";
                                    serialNo += lblSerialNo.Text + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(insertQuery))
                        {
                            insertQuery = insertQuery.TrimEnd(',');
                            int value = objTimesheet.AddWorkerTimesheet(employeeCode, entryDate, insertQuery);
                            if (value > 0)
                            {
                                count++;
                                serialNos = serialNo.TrimEnd(',');
                            }
                        }
                        else
                        {
                            ExceptionMessage("Please try again..!");
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(serialNos))
                {
                    serialNos = serialNos.TrimEnd(',');
                    RemoveRecords(dtTextChanged, serialNos);
                }

            }
            else
            {
                ExceptionMessage("Please try again..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddWorkerTimesheetNew()
    {
        try
        {

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SERIAL_NO", typeof(string));
            dtNew.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtNew.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtNew.Columns.Add("UNIT", typeof(string));
            dtNew.Columns.Add("JOB_NO", typeof(string));
            dtNew.Columns.Add("ENTRY_DATE", typeof(string));
            dtNew.Columns.Add("IN_TIME", typeof(string));
            dtNew.Columns.Add("OUT_TIME", typeof(string));
            dtNew.Columns.Add("WORKING_HOURS", typeof(string));
            dtNew.Columns.Add("WORKING_MINUTS", typeof(string));
            dtNew.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dtNew.Columns.Add("REMARKS", typeof(string));

            string employeeCode = string.Empty;
            string jobNo = string.Empty;
            string entryDate = string.Empty;
            string inTime = string.Empty;
            string outTime = string.Empty;
            string hours = string.Empty;
            string workingHours = string.Empty;
            string workingHoursh = string.Empty;
            string workingHoursm = string.Empty;
            string remarks = string.Empty;
            int count = 0;
            string serialNos = string.Empty;
            string serialNo = string.Empty;

            DataTable dt = (DataTable)Session["dtTemp"];
            DataTable dtTextChanged = (DataTable)Session["dtChanged"];

            string dtEmployeeName = string.Empty;
            string dtEmployeeCode = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string insertQuery = string.Empty;
                    if (dr["EMPLOYEE_NAME"] != DBNull.Value)
                        dtEmployeeName = Convert.ToString(dr["EMPLOYEE_NAME"]);

                    if (dr["EMPLOYEE_CODE"] != DBNull.Value)
                        dtEmployeeCode = Convert.ToString(dr["EMPLOYEE_CODE"]);

                    if (gvWorkerTimesheetList.Rows.Count > 0)
                    {
                        foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                        {
                            Label lblSerialNo = gr.FindControl("lblSerialNo") as Label;
                            Label lblEmpCode = gr.FindControl("lblEmpCode") as Label;
                            Label lblEmployeeName = gr.FindControl("lblEmployeeName") as Label;
                            DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;
                            Label lblEntryDate = gr.FindControl("lblEntryDate") as Label;
                            Label lblInTime = gr.FindControl("lblInTime") as Label;
                            Label lblOutTime = gr.FindControl("lblOutTime") as Label;
                            Label lblWorkingHours = gr.FindControl("lblWorkingHours") as Label;
                            TextBox txtAdjustedWorkingHours = gr.FindControl("txtAdjustedWorkingHours") as TextBox;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            if (lblEmpCode.Text == dtEmployeeCode && lblEmployeeName.Text == dtEmployeeName)
                            {
                                if (!string.IsNullOrEmpty(lblEmpCode.Text))
                                    employeeCode = Convert.ToString(lblEmpCode.Text);
                                else
                                    employeeCode = string.Empty;

                                if (!string.IsNullOrEmpty(lblEntryDate.Text))
                                    entryDate = Convert.ToDateTime(lblEntryDate.Text).ToString("yyyy-MM-dd");
                                else
                                    entryDate = string.Empty;

                                if (ddlJOBNo.SelectedIndex > 0)
                                    jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
                                else
                                    jobNo = string.Empty;

                                if (!string.IsNullOrEmpty(lblInTime.Text))
                                    inTime = Convert.ToString(lblInTime.Text);
                                else
                                    inTime = string.Empty;

                                if (!string.IsNullOrEmpty(lblOutTime.Text))
                                    outTime = Convert.ToString(lblOutTime.Text);
                                else
                                    outTime = string.Empty;

                                if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                                    hours = Convert.ToString(lblWorkingHours.Text);
                                else
                                    hours = string.Empty;

                                if (!string.IsNullOrEmpty(txtAdjustedWorkingHours.Text))
                                {
                                    if (Convert.ToString(txtAdjustedWorkingHours.Text).Contains(':'))
                                    {
                                        string[] strtext = Convert.ToString(txtAdjustedWorkingHours.Text).Split(':');
                                        if (Convert.ToInt32(strtext[0]) > 0)
                                            workingHoursh = Convert.ToString(strtext[0]);
                                        else
                                            workingHoursh = "00";

                                        if (Convert.ToInt32(strtext[1]) > 0)
                                            workingHoursm = Convert.ToString(strtext[1]);
                                        else
                                            workingHoursm = "00";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(txtAdjustedWorkingHours.Text) > 0)
                                            workingHoursh = Convert.ToString(txtAdjustedWorkingHours.Text);
                                        else
                                            workingHoursh = "00";

                                        workingHoursm = "00";
                                    }
                                    workingHours = workingHoursh + ":" + workingHoursm;
                                }
                                else
                                    workingHours = "00:00";

                                if (!string.IsNullOrEmpty(txtRemarks.Text))
                                    remarks = Convert.ToString(txtRemarks.Text);
                                else
                                    remarks = string.Empty;

                                if (!string.IsNullOrEmpty(employeeCode) && !string.IsNullOrEmpty(workingHours) && workingHours != "00:00")
                                {
                                    insertQuery += "('" + employeeCode + "','" + entryDate + "','" + jobNo + "','" + inTime + "','" + outTime + "','" + hours + "','" + workingHours + "','" + remarks + "'," + Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",GETDATE()),";
                                    serialNo += lblSerialNo.Text + ",";
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(insertQuery))
                        {
                            insertQuery = insertQuery.TrimEnd(',');
                            int value = objTimesheet.AddWorkerTimesheet(employeeCode, entryDate, insertQuery);
                            if (value > 0)
                            {
                                count++;
                                serialNos = serialNo.TrimEnd(',');
                            }
                        }
                        else
                        {
                            ExceptionMessage("Please try again..!");
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(serialNos))
                {
                    serialNos = serialNos.TrimEnd(',');
                    RemoveRecords(dtTextChanged, serialNos);
                }

            }
            else
            {
                ExceptionMessage("Please try again..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecords(DataTable dtTextChanged, string serialNos)
    {
        string[] strSerialNo = serialNos.Split(',');
        if (dtTextChanged.Rows.Count > 0)
        {
            foreach (string sr in strSerialNo)
            {
                if (!string.IsNullOrEmpty(sr))
                {
                    foreach (DataRow drremove in dtTextChanged.Select("SERIAL_NO='" + sr + "'"))
                    {
                        dtTextChanged.Rows.Remove(drremove);
                    }
                }
            }

            if (dtTextChanged.Rows.Count > 0)
            {
                Session["dtChanged"] = dtTextChanged;
                gvWorkerTimesheetList.DataSource = dtTextChanged;
                gvWorkerTimesheetList.DataBind();
            }
            else
            {
                Session["dtChanged"] = null;
                gvWorkerTimesheetList.DataSource = null;
                gvWorkerTimesheetList.DataBind();
            }
        }
        else
        {
            Session["dtChanged"] = null;
        }
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
    }

    private void BindNotAddedList(DataTable dtNew)
    {
        try
        {
            if (dtNew.Rows.Count > 0)
            {
                Session["dtTemp"] = dtNew;
                gvWorkerTimesheetList.DataSource = dtNew.DefaultView;
                gvWorkerTimesheetList.DataBind();
                lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
            }
            else
            {
                gvWorkerTimesheetList.DataSource = null;
                gvWorkerTimesheetList.DataBind();
                lblRecords.Text = "Records[0]";

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

    private void HideMessagePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}