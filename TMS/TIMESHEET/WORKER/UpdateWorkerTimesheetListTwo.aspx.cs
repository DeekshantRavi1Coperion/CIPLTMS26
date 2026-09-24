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

public partial class TIMESHEET_WORKER_UpdateWorkerTimesheetListTwo : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    DataSet dsWorker = new DataSet();
    DataSet dsJOBNo = new DataSet();
    DataTable dt = new DataTable();

    bool chkJobNo = false;
    bool chkBalHours = false;

    string employeeCode = string.Empty;
    string entryDate = string.Empty;
    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HideMessagePanel();
                hdRemovedRecordIDs.Value = string.Empty;
                Session["dsJOBNo"] = null;
                Session["dtSubitems"] = null;
                Session["dtChanged"] = null;
                Session["dtTemp"] = null;


                if (Request.QueryString["employeecode"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["employeecode"])))
                    employeeCode = Convert.ToString(Request.QueryString["employeecode"]);
                else
                    employeeCode = string.Empty;

                if (Request.QueryString["entrydate"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["entrydate"])))
                    entryDate = Convert.ToDateTime(Request.QueryString["entrydate"]).ToString("yyyy-MMM-dd");
                else
                    entryDate = string.Empty;

                if (!string.IsNullOrEmpty(employeeCode) && !string.IsNullOrEmpty(entryDate))
                {
                    hdRemovedRecordIDs.Value = string.Empty;
                    GetWorkerTimesheetListDetail();
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["employeecode"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["employeecode"])))
            employeeCode = Convert.ToString(Request.QueryString["employeecode"]);
        else
            employeeCode = string.Empty;

        if (Request.QueryString["entrydate"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["entrydate"])))
            entryDate = Convert.ToDateTime(Request.QueryString["entrydate"]).ToString("yyyy-MMM-dd");
        else
            entryDate = string.Empty;

        if (!string.IsNullOrEmpty(employeeCode) && !string.IsNullOrEmpty(entryDate))
        {
            hdRemovedRecordIDs.Value = string.Empty;
            GetWorkerTimesheetListDetail();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HideMessagePanel();
        UpdateWorkerTimesheet();
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
                Label lblEmpCode = (Label)e.Row.FindControl("lblEmpCode");

                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                Label lblUnitID = (Label)e.Row.FindControl("lblUnitID");
                Button btnAddNewRecord = (Button)e.Row.FindControl("btnAddNewRecord");
                TextBox txtWorkingHours = (TextBox)e.Row.FindControl("txtWorkingHours");
                DropDownList ddlJOBNos = (DropDownList)e.Row.FindControl("ddlJOBNo");

                TextBox txtJOBNo = (TextBox)e.Row.FindControl("txtJOBNo");
                TextBox txtItemName = (TextBox)e.Row.FindControl("txtItemName");

                Label lblLOTTFSubitem = (Label)e.Row.FindControl("lblLOTTFSubitem");
                DropDownList ddlSubitems = (DropDownList)e.Row.FindControl("ddlSubitems");
                TextBox txtActivityMatrix = (TextBox)e.Row.FindControl("txtActivityMatrix");

                Label lblAdjustedWorkingHours = (Label)e.Row.FindControl("lblAdjustedWorkingHours");
                DropDownList ddlHours = (DropDownList)e.Row.FindControl("ddlHours");
                DropDownList ddlMins = (DropDownList)e.Row.FindControl("ddlMins");

                ImageButton imgBtnRemoveRecord = (ImageButton)e.Row.FindControl("imgBtnRemoveRecord");
                if (Convert.ToInt32(e.Row.RowIndex) == 0)
                {
                    imgBtnRemoveRecord.Visible = false;
                }

                ImageButton imgBtnDeleteRecord = (ImageButton)e.Row.FindControl("imgBtnDeleteRecord");

                imgBtnDeleteRecord.Visible = false;

                if (!string.IsNullOrEmpty(Convert.ToString(lblRecordID.Text)) && Convert.ToInt32(lblRecordID.Text) > 0)
                {
                    imgBtnDeleteRecord.Visible = true;
                }

                DataTable dtChanged = (DataTable)Session["dtChanged"];

                DataSet dsJOBNo = new DataSet();

                if (Session["dsJOBNo"] == null)
                {
                    dsJOBNo = objTimesheet.GetLOTJOBs(Convert.ToInt32(lblUnitID.Text));
                    Session["dsJOBNo"] = dsJOBNo;
                }
                else
                {
                    dsJOBNo = (DataSet)Session["dsJOBNo"];
                }

                if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[1].Rows.Count > 0)
                {
                    Session["dtSubitems"] = dsJOBNo.Tables[1];
                }
                else
                {
                    Session["dtSubitems"] = null;
                }

                DataTable dtSubitems = (DataTable)Session["dtSubitems"];

                if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
                {
                    ddlJOBNos.DataSource = dsJOBNo.Tables[0];
                    ddlJOBNos.DataTextField = "JOB_NO";
                    ddlJOBNos.DataValueField = "JOB_NO";
                    ddlJOBNos.DataBind();
                    ddlJOBNos.Items.Insert(0, "SELECT");

                    foreach (DataRow dr in dtChanged.Select("SERIAL_NO='" + Convert.ToString(lblSerialNo.Text) + "' AND EMPLOYEE_CODE='" + Convert.ToString(lblEmpCode.Text) + "'"))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                            ddlJOBNos.SelectedValue = Convert.ToString(dr["JOB_NO"]);
                    }
                }


                ddlSubitems.Items.Clear();
                ddlSubitems.Items.Insert(0, "SELECT");
                ddlSubitems.SelectedIndex = 0;

                if (dtSubitems.Rows.Count > 0)
                {
                    if (ddlJOBNos.SelectedIndex > 0)
                    {
                        DataTable dtNew = new DataTable();
                        dtNew.Columns.Add("SUBITEM_DESC", typeof(string));

                        foreach (DataRow drs in dtSubitems.Select("JOB_NO='" + Convert.ToString(ddlJOBNos.SelectedValue) + "'"))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(drs["SUBITEM_DESC"])))
                            {
                                DataRow drn = dtNew.NewRow();
                                drn["SUBITEM_DESC"] = drs["SUBITEM_DESC"];
                                dtNew.Rows.Add(drn);
                            }
                        }

                        if (dtNew.Rows.Count > 0)
                        {
                            ddlSubitems.DataSource = dtNew;
                            ddlSubitems.DataTextField = "SUBITEM_DESC";
                            ddlSubitems.DataValueField = "SUBITEM_DESC";
                            ddlSubitems.DataBind();
                            ddlSubitems.Items.Insert(0, "SELECT");

                            if (!string.IsNullOrEmpty(lblLOTTFSubitem.Text))
                            {
                                ddlSubitems.SelectedValue = Convert.ToString(lblLOTTFSubitem.Text);
                            }
                            else
                            {
                                ddlSubitems.SelectedIndex = 0;
                            }
                        }
                    }




                    foreach (DataRow dr in dtChanged.Select("SERIAL_NO='" + Convert.ToString(lblSerialNo.Text) + "' AND EMPLOYEE_CODE='" + Convert.ToString(lblEmpCode.Text) + "'"))
                    {
                        ddlSubitems.SelectedValue = Convert.ToString(dr["SUBITEM_DESC"]);
                        txtActivityMatrix.Text = Convert.ToString(dr["ACTIVITY_MATRIX"]);

                        if (ddlJOBNos.SelectedIndex == 0)
                        {
                            txtJOBNo.Text = Convert.ToString(dr["JOB_NO1"]);
                            txtItemName.Text = Convert.ToString(dr["ITEM_NAME"]);

                            txtJOBNo.Enabled = true;
                            txtItemName.Enabled = true;
                        }
                        else
                        {
                            txtJOBNo.Text = string.Empty;
                            txtItemName.Text = string.Empty;

                            txtJOBNo.Enabled = false;
                            txtItemName.Enabled = false;
                        }

                    }
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
            dt.Columns.Add("RECORD_ID", typeof(string));
            dt.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dt.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dt.Columns.Add("UNIT", typeof(string));
            dt.Columns.Add("UNIT_ID", typeof(int));
            dt.Columns.Add("JOB_NO", typeof(string));
            dt.Columns.Add("JOB_NO1", typeof(string));
            dt.Columns.Add("ITEM_NAME", typeof(string));

            dt.Columns.Add("SUBITEM_DESC", typeof(string));
            dt.Columns.Add("ACTIVITY_MATRIX", typeof(string));

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
                if (Convert.ToString(e.CommandArgument) == "ADD" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE" ||
                    Convert.ToString(e.CommandArgument) == "DELETE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }


                #region ADD[===================]

                if (Convert.ToString(e.CommandArgument) == "ADD")
                {
                    int hoursOfCurrentRowAdjustedHours = 0;
                    int minutsOfCurrentRowAdjustedHours = 0;
                    int totalMinutsOfCurrentRowAdjustedHours = 0;

                    TextBox txtCurrentRowAdjustedWorkingHours = (TextBox)gvWorkerTimesheetList.Rows[rowindex].FindControl("txtAdjustedWorkingHours");
                    //ImageButton imgBtnAddNewRecord = (ImageButton)gvWorkerTimesheetList.Rows[rowindex].FindControl("imgBtnAddNewRecord");
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

                            Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                            Label lblEmployeeName = (Label)gr.FindControl("lblEmployeeName");
                            Label lblEmpCode = (Label)gr.FindControl("lblEmpCode");
                            Label lblUnit = (Label)gr.FindControl("lblUnit");
                            Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                            DropDownList ddlJOBNo = (DropDownList)gr.FindControl("ddlJOBNo");

                            TextBox txtJOBNo = (TextBox)gr.FindControl("txtJOBNo");
                            TextBox txtItemName = (TextBox)gr.FindControl("txtItemName");

                            DropDownList ddlSubitems = (DropDownList)gr.FindControl("ddlSubitems");
                            TextBox txtActivityMatrix = (TextBox)gr.FindControl("txtActivityMatrix");

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

                            if (!string.IsNullOrEmpty(lblRecordID.Text))
                                dr["RECORD_ID"] = lblRecordID.Text;

                            if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                                dr["EMPLOYEE_NAME"] = lblEmployeeName.Text;

                            if (!string.IsNullOrEmpty(lblEmpCode.Text))
                                dr["EMPLOYEE_CODE"] = lblEmpCode.Text;

                            if (!string.IsNullOrEmpty(lblUnit.Text))
                                dr["UNIT"] = lblUnit.Text;

                            if (!string.IsNullOrEmpty(lblUnitID.Text))
                                dr["UNIT_ID"] = lblUnitID.Text;

                            if (ddlJOBNo.SelectedIndex > 0)
                                dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);

                            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                                dr["JOB_NO1"] = Convert.ToString(txtJOBNo.Text).ToUpper().Trim();

                            if (!string.IsNullOrEmpty(txtItemName.Text))
                                dr["ITEM_NAME"] = Convert.ToString(txtItemName.Text).Trim();


                            if (ddlSubitems.SelectedIndex > 0)
                                dr["SUBITEM_DESC"] = Convert.ToString(ddlSubitems.SelectedValue);

                            if (!string.IsNullOrEmpty(txtActivityMatrix.Text))
                                dr["ACTIVITY_MATRIX"] = txtActivityMatrix.Text;


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
                            drnew["ENTRY_DATE"] = Convert.ToString(dt.Rows[rowindex]["ENTRY_DATE"]);
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

                            if (dt.Rows.Count > 0)
                            {
                                chkJobNo = true;
                                Session["dtChanged"] = dt;
                                gvWorkerTimesheetList.DataSource = dt.DefaultView;
                                gvWorkerTimesheetList.DataBind();
                                lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
                            }
                            else
                            {
                                chkJobNo = false;
                                Session["dtChanged"] = null;
                                gvWorkerTimesheetList.DataSource = null;
                                gvWorkerTimesheetList.DataBind();
                                lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
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

                else if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    DataTable dtChanged = (DataTable)Session["dtChanged"];
                    Label lblRecordID = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblRecordID") as Label;
                    Label lblSerialNo = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblSerialNo") as Label;
                    hdRemovedRecordIDs.Value = Convert.ToString(hdRemovedRecordIDs.Value) + "," + Convert.ToString(lblRecordID.Text) + ",";
                    RemoveRecords(dtChanged, Convert.ToString(lblSerialNo.Text));
                }

                #endregion



                #region DELETE[================]

                else if (Convert.ToString(e.CommandArgument) == "DELETE")
                {
                    //hdDeleteRecordID.Value = "0";

                    DataTable dtPrimaryDetails = new DataTable();

                    dtPrimaryDetails.Columns.Add("EMPLOYEE_NAME", typeof(string));
                    dtPrimaryDetails.Columns.Add("UNIT", typeof(string));
                    dtPrimaryDetails.Columns.Add("ENTRY_DATE", typeof(string));
                    dtPrimaryDetails.Columns.Add("WORKING_HOURS", typeof(string));
                    dtPrimaryDetails.Columns.Add("IN_TIME", typeof(string));
                    dtPrimaryDetails.Columns.Add("OUT_TIME", typeof(string));
                    dtPrimaryDetails.Columns.Add("OT_HOURS", typeof(string));
                    dtPrimaryDetails.Columns.Add("SAVED_TIMESHEET_HOURS", typeof(string));
                    dtPrimaryDetails.Columns.Add("BAL_TIMESHEET_HRS", typeof(string));

                    DataRow dr0 = dtPrimaryDetails.NewRow();

                    dr0["EMPLOYEE_NAME"] = Convert.ToString(lblLegend.Text);
                    dr0["UNIT"] = Convert.ToString(txtUnit.Text);
                    dr0["ENTRY_DATE"] = Convert.ToString(txtEntryDate.Text);
                    dr0["WORKING_HOURS"] = Convert.ToString(txtWorkingHours.Text);
                    dr0["IN_TIME"] = Convert.ToString(txtInTime.Text);
                    dr0["OUT_TIME"] = Convert.ToString(txtOutTime.Text);
                    dr0["OT_HOURS"] = Convert.ToString(txtOTHours.Text);
                    dr0["SAVED_TIMESHEET_HOURS"] = Convert.ToString(txtSavedTimesheetHours.Text);
                    dr0["BAL_TIMESHEET_HRS"] = Convert.ToString(txtBalanceTimesheetHours.Text);

                    dtPrimaryDetails.Rows.Add(dr0);


                    DataTable dtChanged = (DataTable)Session["dtChanged"];
                    Label lblRecordID = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblRecordID") as Label;
                    DeleteRecords(dtChanged, dtPrimaryDetails, Convert.ToInt32(lblRecordID.Text));

                    if (gvWorkerTimesheetList.Rows.Count > 0)
                    {
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        ExceptionMessage("No data found! Please close window and click on search button to refresh worker timesheet list...!!!");
                        return;
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

    protected void ddlJOBNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

            DropDownList ddlJOBNos = (DropDownList)gvr.FindControl("ddlJOBNo");
            DropDownList ddlSubitems = (DropDownList)gvr.FindControl("ddlSubitems");

            TextBox txtJOBNo = (TextBox)gvr.FindControl("txtJOBNo");
            TextBox txtItemName = (TextBox)gvr.FindControl("txtItemName");

            DataTable dtSubitems = (DataTable)Session["dtSubitems"];

            ddlSubitems.Items.Clear();
            ddlSubitems.Items.Insert(0, "SELECT");
            ddlSubitems.SelectedIndex = 0;

            if (dtSubitems.Rows.Count > 0)
            {
                if (ddlJOBNos.SelectedIndex > 0)
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("SUBITEM_DESC", typeof(string));

                    foreach (DataRow dr in dtSubitems.Select("JOB_NO='" + Convert.ToString(ddlJOBNos.SelectedValue) + "'"))
                    {
                        DataRow drn = dtNew.NewRow();
                        drn["SUBITEM_DESC"] = dr["SUBITEM_DESC"];
                        dtNew.Rows.Add(drn);
                    }

                    if (dtNew.Rows.Count > 0)
                    {
                        ddlSubitems.DataSource = dtNew;
                        ddlSubitems.DataTextField = "SUBITEM_DESC";
                        ddlSubitems.DataValueField = "SUBITEM_DESC";
                        ddlSubitems.DataBind();
                        ddlSubitems.Items.Insert(0, "SELECT");
                        ddlSubitems.SelectedIndex = 0;
                    }
                }
            }

            txtJOBNo.Enabled = true;
            txtItemName.Enabled = true;

            if (ddlJOBNos.SelectedIndex > 0)
            {
                txtJOBNo.Text = string.Empty;
                txtItemName.Text = string.Empty;
                txtJOBNo.Enabled = false;
                txtItemName.Enabled = false;
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

            ImageButton imgBtnRemoveRecord = (ImageButton)gvr.FindControl("imgBtnRemoveRecord");
            imgBtnRemoveRecord.Visible = false;
            if (ddlHours.SelectedIndex == 0)
            {
                imgBtnRemoveRecord.Visible = true;
            }


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

    private void GetWorkerTimesheetListDetail()
    {
        try
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("SERIAL_NO", typeof(string));
            dtTemp.Columns.Add("RECORD_ID", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtTemp.Columns.Add("UNIT", typeof(string));
            dtTemp.Columns.Add("UNIT_ID", typeof(int));
            dtTemp.Columns.Add("JOB_NO", typeof(string));

            dtTemp.Columns.Add("JOB_NO1", typeof(string));
            dtTemp.Columns.Add("ITEM_NAME", typeof(string));

            dtTemp.Columns.Add("SUBITEM_DESC", typeof(string));
            dtTemp.Columns.Add("ACTIVITY_MATRIX", typeof(string));

            dtTemp.Columns.Add("ENTRY_DATE", typeof(string));
            dtTemp.Columns.Add("IN_TIME", typeof(string));
            dtTemp.Columns.Add("OUT_TIME", typeof(string));
            dtTemp.Columns.Add("WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("WORKING_MINUTS", typeof(string));
            dtTemp.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("OT_HOURS", typeof(string));
            dtTemp.Columns.Add("BAL_WRK_HRS", typeof(string));

            dtTemp.Columns.Add("REMARKS", typeof(string));

            if (Request.QueryString["employeecode"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["employeecode"])))
                employeeCode = Convert.ToString(Request.QueryString["employeecode"]);
            else
                employeeCode = string.Empty;

            if (Request.QueryString["entrydate"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["entrydate"])))
                entryDate = Convert.ToDateTime(Request.QueryString["entrydate"]).ToString("yyyy-MM-dd");
            else
                entryDate = string.Empty;


            dsWorker = objTimesheet.GetWorkerTimesheetListDetail(employeeCode, entryDate);

            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                if (dsWorker.Tables[0].Rows.Count > 0)
                {
                    DataRow dr0 = dsWorker.Tables[0].Rows[0];

                    if (dr0["EMPLOYEE_NAME"] != DBNull.Value)
                        lblLegend.Text = Convert.ToString(dr0["EMPLOYEE_NAME"]) + " [" + Convert.ToString(dr0["EMPLOYEE_CODE"]) + "]";
                    else
                        lblLegend.Text = string.Empty;

                    if (dr0["UNIT"] != DBNull.Value)
                        txtUnit.Text = Convert.ToString(dr0["UNIT"]);
                    else
                        txtUnit.Text = string.Empty;

                    if (dr0["ENTRY_DATE"] != DBNull.Value)
                        txtEntryDate.Text = Convert.ToString(dr0["ENTRY_DATE"]);
                    else
                        txtEntryDate.Text = string.Empty;

                    if (dr0["WORKING_HOURS"] != DBNull.Value)
                        txtWorkingHours.Text = Convert.ToString(dr0["WORKING_HOURS"]);
                    else
                        txtWorkingHours.Text = "00:00";


                    if (dr0["IN_TIME"] != DBNull.Value)
                        txtInTime.Text = Convert.ToString(dr0["IN_TIME"]);
                    else
                        txtInTime.Text = "00:00";

                    if (dr0["OUT_TIME"] != DBNull.Value)
                        txtOutTime.Text = Convert.ToString(dr0["OUT_TIME"]);
                    else
                        txtOutTime.Text = "00:00";

                    if (dr0["OT_HOURS"] != DBNull.Value)
                        txtOTHours.Text = Convert.ToString(dr0["OT_HOURS"]);
                    else
                        txtOTHours.Text = "0";

                    if (dr0["SAVED_TIMESHEET_HOURS"] != DBNull.Value)
                        txtSavedTimesheetHours.Text = Convert.ToString(dr0["SAVED_TIMESHEET_HOURS"]);
                    else
                        txtSavedTimesheetHours.Text = "00:00";

                    if (dr0["BAL_TIMESHEET_HRS"] != DBNull.Value)
                        txtBalanceTimesheetHours.Text = Convert.ToString(dr0["BAL_TIMESHEET_HRS"]);
                    else
                        txtBalanceTimesheetHours.Text = "00:00";
                }

                if (dsWorker.Tables[1].Rows.Count > 0)
                {
                    Session["dtTemp"] = dsWorker.Tables[1];
                    Session["dtChanged"] = dsWorker.Tables[1];
                    gvWorkerTimesheetList.DataSource = dsWorker.Tables[1].DefaultView;
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

    private void UpdateWorkerTimesheet()
    {
        try
        {
            int value = 0;
            int count = 0;

            int recordID = 0;
            string employeeCode = string.Empty;
            string jobNo = string.Empty;
            string entryDate = string.Empty;

            string subitemDesc = string.Empty;
            string activityMatrix = string.Empty;

            string remarks = string.Empty;

            string inTime = string.Empty;
            string outTime = string.Empty;

            string hours = string.Empty;
            string workingHours = string.Empty;
            string workingHoursh = string.Empty;
            string workingHoursm = string.Empty;
            string otHours = string.Empty;

            string serialNos = string.Empty;
            string serialNo = string.Empty;
            string recordIDSToRemove = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdRemovedRecordIDs.Value)))
                recordIDSToRemove = Convert.ToString(hdRemovedRecordIDs.Value).TrimEnd(',');


            //if (gvWorkerTimesheetList.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
            //    {
            //        TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
            //        DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;

            //        if (ddlJOBNo.SelectedIndex > 0 || !string.IsNullOrEmpty(txtJOBNo.Text))
            //            chkJobNo = true;
            //        else
            //        {
            //            chkJobNo = false;
            //            break;
            //        }
            //    }

            //    foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
            //    {
            //        Label lblBalanceWorkingHours = gr.FindControl("lblBalanceWorkingHours") as Label;

            //        if (string.IsNullOrEmpty(Convert.ToString(lblBalanceWorkingHours.Text)) || Convert.ToString(lblBalanceWorkingHours.Text) == "00:00" || Convert.ToString(lblBalanceWorkingHours.Text) == "")
            //            chkBalHours = true;
            //        else
            //        {
            //            chkBalHours = false;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    ExceptionMessage("No data found...!");
            //    return;
            //}


            //if (chkJobNo == false)
            //{
            //    ExceptionMessage("Please select or enter JOB No..!");
            //    return;
            //}

            //if (chkBalHours == false)
            //{
            //    ExceptionMessage("Balance hours must be zero..!");
            //    return;
            //}

            if (gvWorkerTimesheetList.Rows.Count > 0)
            {
                int h = 0;
                int m = 0;
                string hs = string.Empty;
                string ms = string.Empty;

                foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                {
                    DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;
                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;

                    DropDownList ddlHours = gr.FindControl("ddlHours") as DropDownList;
                    DropDownList ddlMins = gr.FindControl("ddlMins") as DropDownList;

                    Label lblBalanceWorkingHours = gr.FindControl("lblBalanceWorkingHours") as Label;

                    ddlJOBNo.BackColor = System.Drawing.Color.Transparent;
                    ddlHours.BackColor = System.Drawing.Color.Transparent;
                    ddlMins.BackColor = System.Drawing.Color.Transparent;

                    if (ddlJOBNo.SelectedIndex > 0 || !string.IsNullOrEmpty(txtJOBNo.Text))
                    {
                        //
                    }
                    else
                    {
                        jobNo = string.Empty;
                        ExceptionMessage("Please select or enter JOB no...!!");
                        ddlJOBNo.BackColor = System.Drawing.Color.LightPink;
                        ddlJOBNo.Focus();
                        return;
                    }


                    if (ddlHours.SelectedIndex > 0 | ddlMins.SelectedIndex > 0)
                    {
                        //                                    
                    }
                    else
                    {
                        ExceptionMessage("Please select adjustment hours or adjustment minuts...!!");
                        workingHours = "00:00";
                        ddlHours.BackColor = System.Drawing.Color.LightPink;
                        ddlMins.BackColor = System.Drawing.Color.LightPink;
                        ddlHours.Focus();
                        ddlMins.Focus();
                        return;
                    }


                    //if (string.IsNullOrEmpty(Convert.ToString(lblBalanceWorkingHours.Text)) ||
                    //    Convert.ToString(lblBalanceWorkingHours.Text) == "00:00" ||
                    //    Convert.ToString(lblBalanceWorkingHours.Text) == "")
                    //{
                    //    //
                    //}
                    //else
                    //{
                    //    ExceptionMessage("Balance hours must be zero..!");
                    //    return;
                    //}

                }



                foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                {
                    Label lblSerialNo = gr.FindControl("lblSerialNo") as Label;
                    Label lblRecordID = gr.FindControl("lblRecordID") as Label;
                    Label lblEmpCode = gr.FindControl("lblEmpCode") as Label;
                    Label lblEmployeeName = gr.FindControl("lblEmployeeName") as Label;
                    DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;

                    DropDownList ddlHours = gr.FindControl("ddlHours") as DropDownList;
                    DropDownList ddlMins = gr.FindControl("ddlMins") as DropDownList;

                    TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                    TextBox txtItemName = gr.FindControl("txtItemName") as TextBox;

                    DropDownList ddlSubitems = gr.FindControl("ddlSubitems") as DropDownList;
                    TextBox txtActivityMatrix = gr.FindControl("txtActivityMatrix") as TextBox;

                    Label lblEntryDate = gr.FindControl("lblEntryDate") as Label;
                    Label lblInTime = gr.FindControl("lblInTime") as Label;
                    Label lblOutTime = gr.FindControl("lblOutTime") as Label;
                    Label lblWorkingHours = gr.FindControl("lblWorkingHours") as Label;
                    Label lblOTHours = gr.FindControl("lblOTHours") as Label;
                    TextBox txtAdjustedWorkingHours = gr.FindControl("txtAdjustedWorkingHours") as TextBox;
                    Label lblBalanceWorkingHours = gr.FindControl("lblBalanceWorkingHours") as Label;
                    TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                    ddlJOBNo.BackColor = System.Drawing.Color.Transparent;
                    ddlHours.BackColor = System.Drawing.Color.Transparent;
                    ddlMins.BackColor = System.Drawing.Color.Transparent;

                    if (!string.IsNullOrEmpty(lblSerialNo.Text))
                        serialNo = Convert.ToString(lblSerialNo.Text);
                    else
                        serialNo = "0";

                    if (!string.IsNullOrEmpty(lblRecordID.Text))
                        recordID = Convert.ToInt32(lblRecordID.Text);
                    else
                        recordID = 0;

                    if (!string.IsNullOrEmpty(lblEmpCode.Text))
                        employeeCode = Convert.ToString(lblEmpCode.Text);
                    else
                        employeeCode = string.Empty;

                    if (!string.IsNullOrEmpty(lblEntryDate.Text))
                        entryDate = Convert.ToDateTime(lblEntryDate.Text).ToString("yyyy-MM-dd");
                    else
                        entryDate = string.Empty;


                    if (ddlJOBNo.SelectedIndex > 0 || !string.IsNullOrEmpty(txtJOBNo.Text))
                    {
                        if (ddlJOBNo.SelectedIndex > 0)
                        {
                            jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
                        }
                        else if (!string.IsNullOrEmpty(txtJOBNo.Text))
                        {
                            jobNo = Convert.ToString(txtJOBNo.Text).ToUpper().Trim();
                        }

                    }
                    //else
                    //{
                    //    jobNo = string.Empty;
                    //    ExceptionMessage("Please select or enter JOB no...!!");
                    //    ddlJOBNo.BackColor = System.Drawing.Color.LightPink;
                    //    ddlJOBNo.Focus();
                    //    return;
                    //}

                    if (ddlSubitems.SelectedIndex > 0)
                        subitemDesc = Convert.ToString(ddlSubitems.SelectedItem.Text);
                    else if (!string.IsNullOrEmpty(txtItemName.Text))
                        subitemDesc = Convert.ToString(txtItemName.Text);
                    else
                        subitemDesc = string.Empty;



                    if (!string.IsNullOrEmpty(txtActivityMatrix.Text))
                        activityMatrix = Convert.ToString(txtActivityMatrix.Text);
                    else
                        activityMatrix = string.Empty;


                    //if (!string.IsNullOrEmpty(lblInTime.Text))
                    //    inTime = Convert.ToString(lblInTime.Text);
                    //else
                    //    inTime = string.Empty;

                    //if (!string.IsNullOrEmpty(lblOutTime.Text))
                    //    outTime = Convert.ToString(lblOutTime.Text);
                    //else
                    //    outTime = string.Empty;


                    if (!string.IsNullOrEmpty(txtInTime.Text))
                        inTime = Convert.ToString(txtInTime.Text);
                    else
                        inTime = string.Empty;

                    if (!string.IsNullOrEmpty(txtOutTime.Text))
                        outTime = Convert.ToString(txtOutTime.Text);
                    else
                        outTime = string.Empty;

                    if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                        hours = Convert.ToString(lblWorkingHours.Text);
                    else
                        hours = string.Empty;

                    //if (!string.IsNullOrEmpty(lblOTHours.Text))
                    //    otHours = Convert.ToString(lblOTHours.Text);
                    //else
                    //    otHours = string.Empty;

                    if (!string.IsNullOrEmpty(txtOTHours.Text))
                        otHours = Convert.ToString(txtOTHours.Text);
                    else
                        otHours = string.Empty;


                    if (ddlHours.SelectedIndex > 0 | ddlMins.SelectedIndex > 0)
                    {
                        h = (ddlHours.SelectedIndex > 0) ? (Convert.ToInt32(ddlHours.SelectedValue)) : 0;
                        m = (ddlMins.SelectedIndex > 0) ? (Convert.ToInt32(ddlMins.SelectedValue)) : 0;

                        hs = (h < 10) ? ("0" + Convert.ToString(h)) : (Convert.ToString(h));
                        ms = (m < 10) ? ("0" + Convert.ToString(m)) : (Convert.ToString(m));

                        workingHours = hs + ":" + ms;
                    }
                    //else
                    //{
                    //    ExceptionMessage("Please select adjustment hours or adjustment minuts...!!");
                    //    workingHours = "00:00";
                    //    ddlHours.BackColor = System.Drawing.Color.LightPink;
                    //    ddlMins.BackColor = System.Drawing.Color.LightPink;
                    //    ddlHours.Focus();
                    //    ddlMins.Focus();
                    //    return;
                    //}



                    if (!string.IsNullOrEmpty(txtRemarks.Text))
                        remarks = Convert.ToString(txtRemarks.Text);
                    else
                        remarks = string.Empty;

                    if (workingHours != "00:00")
                    {
                        //value = objTimesheet.UpdateWorkerTimesheet(recordID, recordIDSToRemove, employeeCode, entryDate, jobNo, subitemDesc, activityMatrix,
                        //                                     inTime, outTime, hours, otHours, workingHours, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));



                        value = objTimesheet.UpdateWorkerTimesheet(recordID, recordIDSToRemove, employeeCode, entryDate,
                                                                        jobNo,
                                                                        "", "", "", "", "", 0,
                                                                        inTime, outTime, hours, otHours, workingHours, remarks,
                                                                        Convert.ToInt32(Session["EMP_RECORD_ID"]));

                    }


                    if (value > 0)
                    {
                        count++;
                        serialNos += serialNo + ",";
                    }
                }
            }


            if (!string.IsNullOrEmpty(serialNos))
            {
                serialNos = serialNos.TrimEnd(',');
                RemoveRecords((DataTable)Session["dtChanged"], serialNos);
                SuccessMessage(count + " Records updated successfully..!");
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

            lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
        }
        else
        {
            Session["dtChanged"] = null;
        }
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
    }


    private void DeleteRecords(DataTable dtTextChanged, DataTable dtPrimaryDetails, int recordID)
    {
        int value = objTimesheet.DeleteWorkerTimesheet(recordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

        if (value > 0)
        {
            GetWorkerTimesheetListPrimaryDetail(dtPrimaryDetails);

            if (dtTextChanged.Rows.Count > 0)
            {
                foreach (DataRow drremove in dtTextChanged.Select("RECORD_ID=" + recordID + ""))
                {
                    dtTextChanged.Rows.Remove(drremove);
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
        }
        else
        {
            Session["dtChanged"] = null;
        }

        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
    }

    private void GetWorkerTimesheetListPrimaryDetail(DataTable dtPrimaryDetails)
    {
        try
        {
            if (Request.QueryString["employeecode"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["employeecode"])))
                employeeCode = Convert.ToString(Request.QueryString["employeecode"]);
            else
                employeeCode = string.Empty;

            if (Request.QueryString["entrydate"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["entrydate"])))
                entryDate = Convert.ToDateTime(Request.QueryString["entrydate"]).ToString("yyyy-MM-dd");
            else
                entryDate = string.Empty;


            dsWorker = objTimesheet.GetWorkerTimesheetListDetail(employeeCode, entryDate);

            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                DataRow dr0 = dsWorker.Tables[0].Rows[0];

                if (dr0["EMPLOYEE_NAME"] != DBNull.Value)
                    lblLegend.Text = Convert.ToString(dr0["EMPLOYEE_NAME"]) + " [" + Convert.ToString(dr0["EMPLOYEE_CODE"]) + "]";
                else
                    lblLegend.Text = string.Empty;

                if (dr0["UNIT"] != DBNull.Value)
                    txtUnit.Text = Convert.ToString(dr0["UNIT"]);
                else
                    txtUnit.Text = string.Empty;

                if (dr0["ENTRY_DATE"] != DBNull.Value)
                    txtEntryDate.Text = Convert.ToString(dr0["ENTRY_DATE"]);
                else
                    txtEntryDate.Text = string.Empty;

                if (dr0["WORKING_HOURS"] != DBNull.Value)
                    txtWorkingHours.Text = Convert.ToString(dr0["WORKING_HOURS"]);
                else
                    txtWorkingHours.Text = "00:00";


                if (dr0["IN_TIME"] != DBNull.Value)
                    txtInTime.Text = Convert.ToString(dr0["IN_TIME"]);
                else
                    txtInTime.Text = "00:00";

                if (dr0["OUT_TIME"] != DBNull.Value)
                    txtOutTime.Text = Convert.ToString(dr0["OUT_TIME"]);
                else
                    txtOutTime.Text = "00:00";

                if (dr0["OT_HOURS"] != DBNull.Value)
                    txtOTHours.Text = Convert.ToString(dr0["OT_HOURS"]);
                else
                    txtOTHours.Text = "0";

                if (dr0["SAVED_TIMESHEET_HOURS"] != DBNull.Value)
                    txtSavedTimesheetHours.Text = Convert.ToString(dr0["SAVED_TIMESHEET_HOURS"]);
                else
                    txtSavedTimesheetHours.Text = "00:00";

                if (dr0["BAL_TIMESHEET_HRS"] != DBNull.Value)
                    txtBalanceTimesheetHours.Text = Convert.ToString(dr0["BAL_TIMESHEET_HRS"]);
                else
                    txtBalanceTimesheetHours.Text = "00:00";
            }
            else
            {
                if (gvWorkerTimesheetList.Rows.Count > 0)
                {
                    DataRow dr0 = dtPrimaryDetails.Rows[0];

                    if (dr0["EMPLOYEE_NAME"] != DBNull.Value)
                        lblLegend.Text = Convert.ToString(dr0["EMPLOYEE_NAME"]);
                    else
                        lblLegend.Text = string.Empty;

                    if (dr0["UNIT"] != DBNull.Value)
                        txtUnit.Text = Convert.ToString(dr0["UNIT"]);
                    else
                        txtUnit.Text = string.Empty;

                    if (dr0["ENTRY_DATE"] != DBNull.Value)
                        txtEntryDate.Text = Convert.ToString(dr0["ENTRY_DATE"]);
                    else
                        txtEntryDate.Text = string.Empty;

                    if (dr0["WORKING_HOURS"] != DBNull.Value)
                        txtWorkingHours.Text = Convert.ToString(dr0["WORKING_HOURS"]);
                    else
                        txtWorkingHours.Text = "00:00";


                    if (dr0["IN_TIME"] != DBNull.Value)
                        txtInTime.Text = Convert.ToString(dr0["IN_TIME"]);
                    else
                        txtInTime.Text = "00:00";

                    if (dr0["OUT_TIME"] != DBNull.Value)
                        txtOutTime.Text = Convert.ToString(dr0["OUT_TIME"]);
                    else
                        txtOutTime.Text = "00:00";

                    if (dr0["OT_HOURS"] != DBNull.Value)
                        txtOTHours.Text = Convert.ToString(dr0["OT_HOURS"]);
                    else
                        txtOTHours.Text = "0";

                    txtSavedTimesheetHours.Text = "00:00";

                    TimeSpan duration = DateTime.Parse(txtOutTime.Text).Subtract(DateTime.Parse(txtInTime.Text));
                    int totalMinutes = Convert.ToInt32(duration.TotalMinutes);
                    if (totalMinutes > 0)
                    {
                        if (totalMinutes >= 360)
                            totalMinutes = totalMinutes - 30;

                        txtBalanceTimesheetHours.Text = Convert.ToDateTime(totalMinutes / 60 + ":" + totalMinutes % 60).ToString("HH:mm");
                    }
                    else txtBalanceTimesheetHours.Text = "00:00";
                }
                else
                {
                    if (gvWorkerTimesheetList.Rows.Count > 0)
                    {
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        ExceptionMessage("No data found! Please close window and click on search button to refresh worker timesheet list...!!!");
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void DeleteRecords(string serialNos)
    {
        int recordID = 0;
        int value = 0;

        DataTable dt = new DataTable();
        hdTotalMinuts.Value = "0";
        dt.Columns.Add("SERIAL_NO", typeof(string));
        dt.Columns.Add("RECORD_ID", typeof(string));
        dt.Columns.Add("EMPLOYEE_NAME", typeof(string));
        dt.Columns.Add("EMPLOYEE_CODE", typeof(string));
        dt.Columns.Add("UNIT", typeof(string));
        dt.Columns.Add("JOB_NO", typeof(string));

        dt.Columns.Add("JOB_NO1", typeof(string));
        dt.Columns.Add("ITEM_NAME", typeof(string));

        dt.Columns.Add("ENTRY_DATE", typeof(string));
        dt.Columns.Add("IN_TIME", typeof(string));
        dt.Columns.Add("OUT_TIME", typeof(string));
        dt.Columns.Add("WORKING_HOURS", typeof(string));
        dt.Columns.Add("WORKING_MINUTS", typeof(string));
        dt.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
        dt.Columns.Add("OT_HOURS", typeof(string));
        dt.Columns.Add("BAL_WRK_HRS", typeof(string));

        dt.Columns.Add("REMARKS", typeof(string));

        if (gvWorkerTimesheetList.Rows.Count > 0)
        {
            foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
            {
                DataRow dr = dt.NewRow();

                Label lblRecordID = (Label)gr.FindControl("lblRecordID");
                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblEmployeeName = (Label)gr.FindControl("lblEmployeeName");
                Label lblEmpCode = (Label)gr.FindControl("lblEmpCode");
                Label lblUnit = (Label)gr.FindControl("lblUnit");
                Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                DropDownList ddlJOBNo = (DropDownList)gr.FindControl("ddlJOBNo");

                TextBox txtJOBNo = (TextBox)gr.FindControl("txtJOBNo");
                TextBox txtItemName = (TextBox)gr.FindControl("txtItemName");

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

                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;

                if (!string.IsNullOrEmpty(lblRecordID.Text))
                    dr["RECORD_ID"] = lblRecordID.Text;

                if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                    dr["EMPLOYEE_NAME"] = lblEmployeeName.Text;

                if (!string.IsNullOrEmpty(lblEmpCode.Text))
                    dr["EMPLOYEE_CODE"] = lblEmpCode.Text;

                if (!string.IsNullOrEmpty(lblUnit.Text))
                    dr["UNIT"] = lblUnit.Text;

                if (!string.IsNullOrEmpty(lblUnitID.Text))
                    dr["UNIT_ID"] = lblUnit.Text;

                if (ddlJOBNo.SelectedIndex > 0)
                    dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);


                if (!string.IsNullOrEmpty(txtJOBNo.Text))
                    dr["JOB_NO1"] = Convert.ToString(txtJOBNo.Text).ToUpper().Trim();

                if (!string.IsNullOrEmpty(txtItemName.Text))
                    dr["ITEM_NAME"] = Convert.ToString(txtItemName.Text).ToUpper();

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
                if (!string.IsNullOrEmpty(serialNos))
                {
                    foreach (DataRow drremove in dt.Select("SERIAL_NO='" + serialNos.TrimEnd(',') + "'"))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(drremove["RECORD_ID"])))
                            ViewState["RECORD_IDS"] += Convert.ToString(drremove["RECORD_ID"]) + ",";
                        //else
                        //    recordID = 0;

                        //if (recordID > 0)
                        //{
                        //    value = objTimesheet.RemoveTimesheetRecord(recordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        //}

                        //if (value > 0 || recordID == 0)


                        dt.Rows.Remove(drremove);
                        if (dt.Rows.Count > 0)
                        {
                            Session["dtChanged"] = dt;
                            gvWorkerTimesheetList.DataSource = dt;
                            gvWorkerTimesheetList.DataBind();
                        }
                        else
                        {
                            Session["dtChanged"] = null;
                            gvWorkerTimesheetList.DataSource = null;
                            gvWorkerTimesheetList.DataBind();
                        }

                        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
                    }
                }
            }
            else
            {
                Session["dtChanged"] = null;
                lblRecords.Text = "Records[0]";
            }
        }

        //string[] strSerialNo = serialNos.Split(',');
        //if (dtTextChanged.Rows.Count > 0)
        //{
        //    foreach (string sr in strSerialNo)
        //    {
        //        if (!string.IsNullOrEmpty(sr))
        //        {
        //            foreach (DataRow drremove in dtTextChanged.Select("SERIAL_NO='" + sr + "'"))
        //            {
        //                dtTextChanged.Rows.Remove(drremove);
        //            }
        //        }
        //    }

        //    if (dtTextChanged.Rows.Count > 0)
        //    {
        //        Session["dtChanged"] = dtTextChanged;
        //        gvWorkerTimesheetList.DataSource = dtTextChanged;
        //        gvWorkerTimesheetList.DataBind();
        //    }
        //    else
        //    {
        //        Session["dtChanged"] = null;
        //        gvWorkerTimesheetList.DataSource = null;
        //        gvWorkerTimesheetList.DataBind();
        //    }

        //    lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";
        //}
        //else
        //{
        //    Session["dtChanged"] = null;
        //}
        //lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

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
