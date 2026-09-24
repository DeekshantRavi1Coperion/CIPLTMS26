using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class TIMESHEET_WORKER_AddUpdateWorkerTimesheetNewNine : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Timesheet objTimesheet = new BAL.Timesheet();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsWorker = new DataSet();
    DataSet dsJOBNo = new DataSet();
    DataSet dsSubitems = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsCompany = new DataSet();
    DataSet dsEmployeeList = new DataSet();
    DataTable dt = new DataTable();


    DataSet dsProjectSupervisor = new DataSet();
    DataSet dsProjectWorker = new DataSet();
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
                hdIsNewRecord.Value = "0";
                Session["dtJOBNo"] = null;
                Session["dtSubitems"] = null;
                Session["dsEmployeeList"] = null;
                Session["dtChanged"] = null;
                Session["dtTemp"] = null;

                hdCurrentDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

                hdDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDate.Text = hdDate.Value;

                hdPrevDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtPrevDate.Text = hdPrevDate.Value;


                //BindJOBNo();
                BindUnit();
                BindCompany();
                //dsEmployeeList = objCommon.GetEmployeeByEmpRecordID(0);
                Session["dsEmployeeList"] = dsEmployeeList;

                ddlJOBNo.Items.Insert(0, "Select");
                ddlJOBNo.SelectedIndex = 0;

                ddlProjectSupervisor.Items.Insert(0, "All");
                ddlProjectSupervisor.SelectedIndex = 0;

                ddlProjectWorker.Items.Insert(0, "All");
                ddlProjectWorker.SelectedIndex = 0;
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

        if (ddlUnit.SelectedIndex > 0)
        {
            BindJOBNo(Convert.ToInt32(ddlUnit.SelectedValue));
            GetProjectSupervisorList();
            GetProjectWorkerList();
        }
        else
        {
            ddlJOBNo.Items.Clear();
            ddlJOBNo.Items.Insert(0, "Select");
            ddlJOBNo.SelectedIndex = 0;

            ddlProjectSupervisor.Items.Clear();
            ddlProjectSupervisor.Items.Insert(0, "All");
            ddlProjectSupervisor.SelectedIndex = 0;

            ddlProjectWorker.Items.Clear();
            ddlProjectWorker.Items.Insert(0, "All");
            ddlProjectWorker.SelectedIndex = 0;
        }
    }




    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }



    //protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);

    //        DropDownList ddlOrderType = (DropDownList)gvr.FindControl("ddlOrderType");
    //        DropDownList ddlJOBNos = (DropDownList)gvr.FindControl("ddlJOBNo");
    //        DropDownList ddlSubitems = (DropDownList)gvr.FindControl("ddlSubitems");
    //        TextBox txtJOBNo = (TextBox)gvr.FindControl("txtJOBNo");

    //        DataTable dtSubitems = (DataTable)Session["dtSubitems"];

    //        ddlSubitems.Items.Clear();
    //        ddlSubitems.Items.Insert(0, "SELECT");
    //        ddlSubitems.SelectedIndex = 0;

    //        if (dtSubitems.Rows.Count > 0)
    //        {
    //            if (ddlJOBNos.SelectedIndex > 0)
    //            {
    //                DataTable dtNew = new DataTable();
    //                dtNew.Columns.Add("LOT_TF_SUBITEM_ID", typeof(int));
    //                dtNew.Columns.Add("SUBITEM_DESC", typeof(string));

    //                foreach (DataRow dr in dtSubitems.Select("JOB_NO='" + Convert.ToString(ddlJOBNos.SelectedValue) + "'"))
    //                {
    //                    DataRow drn = dtNew.NewRow();
    //                    drn["LOT_TF_SUBITEM_ID"] = dr["LOT_TF_SUBITEM_ID"];
    //                    drn["SUBITEM_DESC"] = dr["SUBITEM_DESC"];
    //                    dtNew.Rows.Add(drn);
    //                }

    //                if (dtNew.Rows.Count > 0)
    //                {
    //                    ddlSubitems.DataSource = dtNew;
    //                    ddlSubitems.DataTextField = "SUBITEM_DESC";
    //                    ddlSubitems.DataValueField = "LOT_TF_SUBITEM_ID";
    //                    ddlSubitems.DataBind();
    //                    ddlSubitems.Items.Insert(0, "SELECT");
    //                    ddlSubitems.SelectedIndex = 0;
    //                }
    //            }
    //        }

    //        txtJOBNo.Enabled = false;
    //        ddlJOBNos.SelectedIndex = 0;
    //        ddlSubitems.SelectedIndex = 0;
    //        txtJOBNo.Text = string.Empty;

    //        if (ddlOrderType.SelectedIndex > 0)
    //        {
    //            if (Convert.ToInt32(ddlOrderType.SelectedValue) == 1)
    //            {
    //                ddlJOBNos.SelectedIndex = 0;
    //                ddlSubitems.SelectedIndex = 0;
    //                txtJOBNo.Text = string.Empty;
    //            }
    //            else if (Convert.ToInt32(ddlOrderType.SelectedValue) == 1)
    //            {
    //                txtJOBNo.Enabled = true;
    //            }
    //            else if (Convert.ToInt32(ddlOrderType.SelectedValue) == 2)
    //            {
    //                txtJOBNo.Enabled = true;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}


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
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["SUBITEM_DESC"])))
                        {
                            drn["SUBITEM_DESC"] = dr["SUBITEM_DESC"];
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



    protected void ddlProjectSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;

        gvWorkerTimesheetList.DataSource = null;
        gvWorkerTimesheetList.DataBind();
        lblRecords.Text = "Records[" + gvWorkerTimesheetList.Rows.Count + "]";

        GetProjectWorkerList();
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        hdIsNewRecord.Value = "0";
        HideMessagePanel();
        BindWorkerTimesheet();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        hdIsNewRecord.Value = "0";
        HideMessagePanel();
        AddWorkerTimesheetNewOne();
    }

    protected void gvWorkerTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int hrs = 0;
            int mns = 0;

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPunchMissed = (Label)e.Row.FindControl("lblPunchMissed");

                Label lblSerialNo = (Label)e.Row.FindControl("lblSerialNo");
                Label lblEmpCode = (Label)e.Row.FindControl("lblEmpCode");

                Label lblMainRecordFlag = (Label)e.Row.FindControl("lblMainRecordFlag");
                Label lblRecordID = (Label)e.Row.FindControl("lblRecordID");
                ImageButton imgBtnAddNewRecord = (ImageButton)e.Row.FindControl("imgBtnAddNewRecord");
                TextBox txtWorkingHours = (TextBox)e.Row.FindControl("txtWorkingHours");

                //DropDownList ddlOrderType = (DropDownList)e.Row.FindControl("ddlOrderType");
                DropDownList ddlJOBNos = (DropDownList)e.Row.FindControl("ddlJOBNo");
                TextBox txtJOBNo = (TextBox)e.Row.FindControl("txtJOBNo");
                Label lblPrevExisted = (Label)e.Row.FindControl("lblPrevExisted");
                TextBox txtItemName = (TextBox)e.Row.FindControl("txtItemName");

                DropDownList ddlSubitems = (DropDownList)e.Row.FindControl("ddlSubitems");

                //TextBox txtActivityMatrix = (TextBox)e.Row.FindControl("txtActivityMatrix");

                TextBox txtBalTimesheetHours = (TextBox)e.Row.FindControl("txtBalTimesheetHours");
                TextBox txtAdjustedWorkingHours = (TextBox)e.Row.FindControl("txtAdjustedWorkingHours");
                Label lblAdjustedWorkingHours = (Label)e.Row.FindControl("lblAdjustedWorkingHours");

                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                DropDownList ddlHours = (DropDownList)e.Row.FindControl("ddlHours");
                DropDownList ddlMins = (DropDownList)e.Row.FindControl("ddlMins");

                ImageButton imgBtnRemoveRecord = (ImageButton)e.Row.FindControl("imgBtnRemoveRecord");


                imgBtnRemoveRecord.Visible = false;

                DataTable dtJOBNo = (DataTable)Session["dtJOBNo"];
                DataTable dtSubitems = (DataTable)Session["dtSubitems"];
                DataTable dtChanged = (DataTable)Session["dtChanged"];

                if (dtJOBNo.Rows.Count > 0)
                {
                    ddlJOBNos.DataSource = dtJOBNo;
                    ddlJOBNos.DataTextField = "JOB_NO";
                    ddlJOBNos.DataValueField = "JOB_NO";
                    ddlJOBNos.DataBind();
                    ddlJOBNos.Items.Insert(0, "SELECT");

                    foreach (DataRow dr in dtChanged.Select("SERIAL_NO='" + Convert.ToString(lblSerialNo.Text) + "' AND EMPLOYEE_CODE='" + Convert.ToString(lblEmpCode.Text) + "'"))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["JOB_NO"])))
                        {
                            ddlJOBNos.SelectedValue = Convert.ToString(dr["JOB_NO"]);
                        }
                        else
                        {
                            ddlJOBNos.SelectedValue = Convert.ToString(ddlJOBNo.SelectedValue);
                        }
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
                            ddlSubitems.SelectedIndex = 0;
                        }
                    }




                    foreach (DataRow dr in dtChanged.Select("SERIAL_NO='" + Convert.ToString(lblSerialNo.Text) + "' AND EMPLOYEE_CODE='" + Convert.ToString(lblEmpCode.Text) + "'"))
                    {
                        ddlSubitems.SelectedValue = Convert.ToString(dr["SUBITEM_DESC"]);
                        //txtActivityMatrix.Text = Convert.ToString(dr["ACTIVITY_MATRIX"]);
                        txtJOBNo.Text = Convert.ToString(dr["JOB_NO1"]);
                        txtItemName.Text = Convert.ToString(dr["ITEM_NAME"]);
                    }
                }


                //txtJOBNo.Text = string.Empty;

                //if (ddlOrderType.SelectedIndex > 0)
                //{
                //    if (Convert.ToInt32(ddlOrderType.SelectedValue) == 1)
                //    {
                //        ddlJOBNos.SelectedIndex = 0;
                //        ddlSubitems.SelectedIndex = 0;
                //        txtJOBNo.Text = string.Empty;
                //    }
                //    else if (Convert.ToInt32(ddlOrderType.SelectedValue) == 1)
                //    {
                //        txtJOBNo.Enabled = true;
                //    }
                //    else if (Convert.ToInt32(ddlOrderType.SelectedValue) == 2)
                //    {
                //        txtJOBNo.Enabled = true;
                //    }
                //}

                if (!string.IsNullOrEmpty(lblAdjustedWorkingHours.Text) && Convert.ToString(lblAdjustedWorkingHours.Text) != "00:00" && Convert.ToString(lblAdjustedWorkingHours.Text) != "0:0")
                {

                    string[] srtWorkingHours = null;

                    //if (!string.IsNullOrEmpty(txtBalTimesheetHours.Text))
                    //{
                    //    srtWorkingHours = Convert.ToString(txtBalTimesheetHours.Text).Split(':');
                    //    txtAdjustedWorkingHours.Text = txtBalTimesheetHours.Text;

                    //    if (Convert.ToInt32(lblMainRecordFlag.Text) == 1 && Convert.ToInt32(hdIsNewRecord.Value) > 0)
                    //    {
                    //        srtWorkingHours = Convert.ToString(lblAdjustedWorkingHours.Text).Split(':');
                    //    }
                    //}
                    //else
                    //{
                    //    srtWorkingHours = Convert.ToString(lblAdjustedWorkingHours.Text).Split(':');
                    //}


                    srtWorkingHours = Convert.ToString(lblAdjustedWorkingHours.Text).Split(':');
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

                if (ddlHours.SelectedIndex == 0 && ddlMins.SelectedIndex == 0)
                {
                    imgBtnRemoveRecord.Visible = true;
                }

                if (Convert.ToInt32(lblMainRecordFlag.Text) == 1)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSkyBlue;
                    }
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                ddlJOBNos.BackColor = System.Drawing.Color.Transparent;
                ddlSubitems.BackColor = System.Drawing.Color.Transparent;
                ddlHours.BackColor = System.Drawing.Color.Transparent;

                if (ddlJOBNos.SelectedIndex > 0)
                {
                    txtJOBNo.Text = string.Empty;
                    txtItemName.Text = string.Empty;

                    txtJOBNo.Enabled = false;
                    txtItemName.Enabled = false;
                }


                if (Convert.ToInt32(lblPunchMissed.Text) == 1)
                {
                    imgBtnAddNewRecord.Visible = false;
                    ddlJOBNos.Enabled = false;
                    ddlSubitems.Enabled = false;
                    txtJOBNo.Enabled = false;
                    txtItemName.Enabled = false;
                    txtJOBNo.BackColor = System.Drawing.Color.LightPink;
                    txtItemName.BackColor = System.Drawing.Color.LightPink;
                    ddlHours.Enabled = false;
                    ddlHours.BackColor = System.Drawing.Color.Transparent;
                    txtRemarks.Enabled = false;
                    txtRemarks.BackColor = System.Drawing.Color.Transparent;
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                    }


                }
                else
                {
                    ddlJOBNos.Enabled = true;
                    ddlSubitems.Enabled = true;
                    imgBtnAddNewRecord.Visible = true;
                    txtJOBNo.Enabled = true;
                    txtItemName.Enabled = true;
                    txtJOBNo.BackColor = System.Drawing.Color.LightYellow;
                    txtItemName.BackColor = System.Drawing.Color.LightYellow;
                    ddlHours.Enabled = true;
                    txtRemarks.Enabled = true;
                    txtRemarks.BackColor = System.Drawing.Color.LightYellow;
                }

                if (Convert.ToInt32(lblPrevExisted.Text) > 0)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightYellow;
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

    protected void gvWorkerTimesheetList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            hdIsNewRecord.Value = "0";

            DataTable dt = new DataTable();
            hdTotalMinuts.Value = "0";
            dt.Columns.Add("MAIN_RECORD_FLAG", typeof(int));
            dt.Columns.Add("SERIAL_NO", typeof(string));
            dt.Columns.Add("PUNCH_MISSED", typeof(int));
            dt.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dt.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dt.Columns.Add("UNIT", typeof(string));
            dt.Columns.Add("JOB_NO", typeof(string));
            dt.Columns.Add("JOB_NO1", typeof(string));
            dt.Columns.Add("ITEM_NAME", typeof(string));
            dt.Columns.Add("SUBITEM_DESC", typeof(string));
            //dt.Columns.Add("ACTIVITY_MATRIX", typeof(string));

            dt.Columns.Add("ENTRY_DATE", typeof(string));
            dt.Columns.Add("IN_TIME", typeof(string));
            dt.Columns.Add("OUT_TIME", typeof(string));
            dt.Columns.Add("WORKING_HOURS", typeof(string));
            dt.Columns.Add("WORKING_MINUTS", typeof(string));
            dt.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dt.Columns.Add("OT_HOURS", typeof(string));
            dt.Columns.Add("BAL_WRK_HRS", typeof(string));


            dt.Columns.Add("SAVED_TIMESHEET_HOURS", typeof(string));
            dt.Columns.Add("BAL_TIMESHEET_HRS", typeof(string));
            dt.Columns.Add("REMARKS", typeof(string));
            dt.Columns.Add("IS_PREV_EXISTED", typeof(int));

            int rindex = 0;
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "ADD" ||
                    Convert.ToString(e.CommandArgument) == "REMOVE")
                {

                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                if (Convert.ToString(e.CommandArgument) == "ADD")
                {
                    hdIsNewRecord.Value = "1";
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

                            Label lblMainRecordFlag = (Label)gr.FindControl("lblMainRecordFlag");
                            Label lblPunchMissed = (Label)gr.FindControl("lblPunchMissed");
                            Label lblEmployeeName = (Label)gr.FindControl("lblEmployeeName");
                            Label lblEmpCode = (Label)gr.FindControl("lblEmpCode");
                            Label lblUnit = (Label)gr.FindControl("lblUnit");
                            DropDownList ddlJOBNo = (DropDownList)gr.FindControl("ddlJOBNo");
                            TextBox txtJOBNo = (TextBox)gr.FindControl("txtJOBNo");
                            TextBox txtItemName = (TextBox)gr.FindControl("txtItemName");

                            DropDownList ddlSubitems = (DropDownList)gr.FindControl("ddlSubitems");
                            //TextBox txtActivityMatrix = (TextBox)gr.FindControl("txtActivityMatrix");

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


                            TextBox txtSavedTimesheetHours = (TextBox)gr.FindControl("txtSavedTimesheetHours");
                            TextBox txtBalTimesheetHours = (TextBox)gr.FindControl("txtBalTimesheetHours");

                            TextBox txtRemarks = (TextBox)gr.FindControl("txtRemarks");
                            Label lblPrevExisted = (Label)gr.FindControl("lblPrevExisted");

                            dr["MAIN_RECORD_FLAG"] = Convert.ToInt32(lblMainRecordFlag.Text);

                            dr["SERIAL_NO"] = Convert.ToString(rindex);

                            if (!string.IsNullOrEmpty(lblPunchMissed.Text))
                                dr["PUNCH_MISSED"] = lblPunchMissed.Text;

                            if (!string.IsNullOrEmpty(lblEmployeeName.Text))
                                dr["EMPLOYEE_NAME"] = lblEmployeeName.Text;

                            if (!string.IsNullOrEmpty(lblEmpCode.Text))
                                dr["EMPLOYEE_CODE"] = lblEmpCode.Text;

                            if (!string.IsNullOrEmpty(lblUnit.Text))
                                dr["UNIT"] = lblUnit.Text;

                            if (ddlJOBNo.SelectedIndex > 0)
                                dr["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);

                            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                                dr["JOB_NO1"] = Convert.ToString(txtJOBNo.Text).ToUpper().Trim();

                            if (!string.IsNullOrEmpty(txtItemName.Text))
                                dr["ITEM_NAME"] = Convert.ToString(txtItemName.Text).Trim();

                            if (ddlSubitems.SelectedIndex > 0)
                                dr["SUBITEM_DESC"] = Convert.ToString(ddlSubitems.SelectedValue);

                            //if (!string.IsNullOrEmpty(txtActivityMatrix.Text))
                            //    dr["ACTIVITY_MATRIX"] = txtActivityMatrix.Text;

                            if (!string.IsNullOrEmpty(lblEntryDate.Text))
                                dr["ENTRY_DATE"] = lblEntryDate.Text;

                            if (!string.IsNullOrEmpty(lblInTime.Text))
                                dr["IN_TIME"] = lblInTime.Text;

                            if (!string.IsNullOrEmpty(lblOutTime.Text))
                                dr["OUT_TIME"] = lblOutTime.Text;

                            if (!string.IsNullOrEmpty(lblWorkingHours.Text))
                                dr["WORKING_HOURS"] = lblWorkingHours.Text;

                            if (!string.IsNullOrEmpty(txtBalTimesheetHours.Text))
                            {
                                int balTimesheetHours = 0;
                                int balTimesheetMins = 0;
                                int totalMins = 0;

                                balTimesheetHours = Convert.ToInt32(txtBalTimesheetHours.Text.Split(':')[0]);
                                balTimesheetMins = Convert.ToInt32(txtBalTimesheetHours.Text.Split(':')[1]);
                                totalMins = (balTimesheetHours * 60 + balTimesheetMins);

                                dr["WORKING_MINUTS"] = Convert.ToString(totalMins);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(lblWorkingMinuts.Text))
                                    dr["WORKING_MINUTS"] = lblWorkingMinuts.Text;
                            }



                            if (!string.IsNullOrEmpty(txtAdjustedWorkingHours.Text))
                                dr["ADJUSTED_WORKING_HOURS"] = txtAdjustedWorkingHours.Text;

                            if (!string.IsNullOrEmpty(lblOTHours.Text))
                                dr["OT_HOURS"] = lblOTHours.Text;


                            if (!string.IsNullOrEmpty(txtSavedTimesheetHours.Text))
                                dr["SAVED_TIMESHEET_HOURS"] = txtSavedTimesheetHours.Text;

                            if (!string.IsNullOrEmpty(txtBalTimesheetHours.Text))
                                dr["BAL_TIMESHEET_HRS"] = txtBalTimesheetHours.Text;


                            if (!string.IsNullOrEmpty(txtRemarks.Text))
                                dr["REMARKS"] = txtRemarks.Text;

                            if (!string.IsNullOrEmpty(Convert.ToString(lblPrevExisted.Text)))
                                dr["IS_PREV_EXISTED"] = Convert.ToInt32(lblPrevExisted.Text);
                            else dr["IS_PREV_EXISTED"] = 0;

                            dt.Rows.Add(dr);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            int totalWorkingMinuts = 0;

                            DataRow drnew = dt.NewRow();


                            drnew["MAIN_RECORD_FLAG"] = Convert.ToInt32(dt.Rows[rowindex]["MAIN_RECORD_FLAG"]) + 1;
                            drnew["SERIAL_NO"] = Convert.ToString(Convert.ToInt32(dt.Rows[rowindex]["SERIAL_NO"]) + Convert.ToInt32(gvWorkerTimesheetList.Rows.Count));
                            drnew["PUNCH_MISSED"] = Convert.ToString(dt.Rows[rowindex]["PUNCH_MISSED"]);
                            drnew["EMPLOYEE_NAME"] = Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_NAME"]);
                            drnew["EMPLOYEE_CODE"] = Convert.ToString(dt.Rows[rowindex]["EMPLOYEE_CODE"]);
                            drnew["ENTRY_DATE"] = Convert.ToString(dt.Rows[rowindex]["ENTRY_DATE"]);
                            drnew["IN_TIME"] = Convert.ToString(dt.Rows[rowindex]["IN_TIME"]);
                            drnew["OUT_TIME"] = Convert.ToString(dt.Rows[rowindex]["OUT_TIME"]);

                            drnew["WORKING_HOURS"] = Convert.ToString(dt.Rows[rowindex]["WORKING_HOURS"]);

                            //drnew["SAVED_TIMESHEET_HOURS"] = Convert.ToString(dt.Rows[rowindex]["SAVED_TIMESHEET_HOURS"]);
                            drnew["BAL_TIMESHEET_HRS"] = Convert.ToString(dt.Rows[rowindex]["BAL_TIMESHEET_HRS"]);

                            drnew["WORKING_MINUTS"] = Convert.ToString(dt.Rows[rowindex]["WORKING_MINUTS"]);
                            drnew["OT_HOURS"] = Convert.ToString(dt.Rows[rowindex]["OT_HOURS"]);

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

                if (Convert.ToString(e.CommandArgument) == "REMOVE")
                {
                    Label lblSerialNo = gvWorkerTimesheetList.Rows[rowindex].FindControl("lblSerialNo") as Label;
                    DropDownList ddlHours = gvWorkerTimesheetList.Rows[rowindex].FindControl("ddlHours") as DropDownList;
                    int hours = Convert.ToInt32(ddlHours.SelectedValue);
                    if (hours == 0)
                    {
                        DataTable dtTextChanged = (DataTable)Session["dtChanged"];
                        RemoveRecords(dtTextChanged, Convert.ToString(lblSerialNo.Text));
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

    protected void ddlHours_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string workingHours = string.Empty;

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
            if (ddlHours.SelectedIndex == 0 && ddlMins.SelectedIndex == 0)
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
            TextBox txtBalTimesheetHours = (TextBox)gvr.FindControl("txtBalTimesheetHours");

            balanceWorkingHrs = Convert.ToString(lblBalanceWorkingHours.Text);

            if (!string.IsNullOrEmpty(txtBalTimesheetHours.Text))
                workingHours = Convert.ToString(txtBalTimesheetHours.Text);
            else
                workingHours = Convert.ToString(lblWorkingHours.Text);


            int wrkHrs = 0;
            int wrkMins = 0;
            int wrkTotalTime = 0;



            if (!string.IsNullOrEmpty(workingHours))
            {
                if (workingHours.Contains(':'))
                {
                    string[] srtWrkHours = workingHours.Split(':');

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
                    if (!string.IsNullOrEmpty(workingHours) && Convert.ToInt32(workingHours) > 0)
                        wrkHrs = Convert.ToInt32(workingHours);
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


            //if (!string.IsNullOrEmpty(lblWorkingHours.Text))
            //{
            //    if (lblWorkingHours.Text.Contains(':'))
            //    {
            //        string[] srtWrkHours = lblWorkingHours.Text.Split(':');

            //        if (Convert.ToInt32(srtWrkHours[0]) > 0)
            //            wrkHrs = Convert.ToInt32(srtWrkHours[0]);
            //        else
            //            wrkHrs = 0;

            //        if (Convert.ToInt32(srtWrkHours[1]) > 0)
            //            wrkMins = Convert.ToInt32(srtWrkHours[1]);
            //        else
            //            wrkMins = 0;
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(lblWorkingHours.Text) && Convert.ToInt32(lblWorkingHours.Text) > 0)
            //            wrkHrs = Convert.ToInt32(lblWorkingHours.Text);
            //        else
            //            wrkHrs = 0;
            //    }
            //}
            //else
            //{
            //    wrkHrs = 0;
            //    wrkMins = 0;
            //}
            //wrkTotalTime = (wrkHrs * 60) + wrkMins;




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
            string workingHours = string.Empty;

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
            TextBox txtBalTimesheetHours = (TextBox)gvr.FindControl("txtBalTimesheetHours");

            balanceWorkingHrs = Convert.ToString(lblBalanceWorkingHours.Text);

            if (!string.IsNullOrEmpty(txtBalTimesheetHours.Text))
                workingHours = Convert.ToString(txtBalTimesheetHours.Text);
            else
                workingHours = Convert.ToString(lblWorkingHours.Text);


            int wrkHrs = 0;
            int wrkMins = 0;
            int wrkTotalTime = 0;

            if (!string.IsNullOrEmpty(workingHours))
            {
                if (workingHours.Contains(':'))
                {
                    string[] srtWrkHours = workingHours.Split(':');

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
                    if (!string.IsNullOrEmpty(workingHours) && Convert.ToInt32(workingHours) > 0)
                        wrkHrs = Convert.ToInt32(workingHours);
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


    protected void ddlCopyFromDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPrevDate.Visible = false;
        imgbtnPrevDate.Visible = false;
        imgbtnRefreshDate.Visible = false;
        string prevDay = String.Empty;

        prevDay = Convert.ToDateTime(hdDate.Value).DayOfWeek.ToString();
        if (prevDay == "Monday")
            hdPrevDate.Value = Convert.ToDateTime(hdDate.Value).AddDays(-2).ToString("dd-MMM-yyyy");
        else
            hdPrevDate.Value = Convert.ToDateTime(hdDate.Value).AddDays(-1).ToString("dd-MMM-yyyy");

        txtPrevDate.Text = hdPrevDate.Value;

        if (ddlCopyFromDay.SelectedIndex == 1)
        {
            txtPrevDate.Visible = true;
            imgbtnRefreshDate.Visible = true;
        }
        else if (ddlCopyFromDay.SelectedIndex == 2)
        {
            txtPrevDate.Visible = true;
            imgbtnPrevDate.Visible = true;
        }
    }

    protected void imgbtnRefreshDate_Click(object sender, ImageClickEventArgs e)
    {
        string prevDay = Convert.ToDateTime(hdDate.Value).DayOfWeek.ToString();
        if (prevDay == "Monday")
            hdPrevDate.Value = Convert.ToDateTime(hdDate.Value).AddDays(-2).ToString("dd-MMM-yyyy");
        else
            hdPrevDate.Value = Convert.ToDateTime(hdDate.Value).AddDays(-1).ToString("dd-MMM-yyyy");

        txtPrevDate.Text = hdPrevDate.Value;
    }
    #endregion


    #region METHODS[=======================]

    private void BindJOBNo(int unitID)
    {
        try
        {
            //dsJOBNo = objTimesheet.GetDetailsBySP("sp_get_distinct_job_no");
            dsJOBNo = objTimesheet.GetLOTJOBs(unitID);
            if (dsJOBNo.Tables.Count > 0 && dsJOBNo.Tables[0].Rows.Count > 0)
            {
                Session["dtJOBNo"] = dsJOBNo.Tables[0];
                Session["dtSubitems"] = dsJOBNo.Tables[1];

                ddlJOBNo.DataSource = dsJOBNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NO";
                ddlJOBNo.DataValueField = "JOB_NO";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "Select");
            }
            else
            {
                Session["dtJOBNo"] = null;
                Session["dtSubitems"] = null;
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
        dsProjectSupervisor = objTimesheet.GetProjectSupervisorList(Convert.ToInt32(Session["EMP_RECORD_ID"]), Convert.ToInt32(ddlUnit.SelectedValue));
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

        if (ddlProjectSupervisor.SelectedIndex > 0)
            supervisorID = Convert.ToInt32(ddlProjectSupervisor.SelectedValue);

        if (ddlCompany.SelectedIndex > 0)
            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

        dsProjectWorker = objTimesheet.GetProjectWorkerList(Convert.ToInt32(Session["EMP_RECORD_ID"]), supervisorID, companyID, Convert.ToInt32(ddlUnit.SelectedValue));
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

    private void BindWorkerTimesheet()
    {
        try
        {
            DataTable dtTemp = new DataTable();

            dtTemp.Columns.Add("MAIN_RECORD_FLAG", typeof(int));
            dtTemp.Columns.Add("SERIAL_NO", typeof(string));
            dtTemp.Columns.Add("PUNCH_MISSED", typeof(int));
            dtTemp.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtTemp.Columns.Add("EMPLOYEE_CODE", typeof(string));
            dtTemp.Columns.Add("UNIT", typeof(string));
            dtTemp.Columns.Add("JOB_NO", typeof(string));
            dtTemp.Columns.Add("JOB_NO1", typeof(string));
            dtTemp.Columns.Add("ITEM_NAME", typeof(string));

            dtTemp.Columns.Add("SUBITEM_DESC", typeof(string));
            //dtTemp.Columns.Add("ACTIVITY_MATRIX", typeof(string));

            dtTemp.Columns.Add("ENTRY_DATE", typeof(string));
            dtTemp.Columns.Add("IN_TIME", typeof(string));
            dtTemp.Columns.Add("OUT_TIME", typeof(string));
            dtTemp.Columns.Add("WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("WORKING_MINUTS", typeof(string));
            dtTemp.Columns.Add("ADJUSTED_WORKING_HOURS", typeof(string));
            dtTemp.Columns.Add("OT_HOURS", typeof(string));
            dtTemp.Columns.Add("BAL_WRK_HRS", typeof(string));

            dtTemp.Columns.Add("SAVED_TIMESHEET_HOURS", typeof(string));
            dtTemp.Columns.Add("BAL_TIMESHEET_HRS", typeof(string));

            dtTemp.Columns.Add("REMARKS", typeof(string));
            dtTemp.Columns.Add("IS_PREV_EXISTED", typeof(int));

            string entryDate = string.Empty;
            string prevDate = string.Empty;
            int unitID = 0;
            int supervisorID = 0;
            int projectWorkerID = 0;
            int companyID = 0;
            int excludeMissedPunch = 0;

            entryDate = Convert.ToDateTime(hdDate.Value).ToString("yyyy-MM-dd");

            if (ddlCopyFromDay.SelectedIndex > 0)
                prevDate = Convert.ToDateTime(hdPrevDate.Value).ToString("yyyy-MM-dd");


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

            if (chkExcludeMissedPunch.Checked)
                excludeMissedPunch = 1;
            else
                excludeMissedPunch = 0;

            dsWorker = objTimesheet.GetWorkerListAll(entryDate, prevDate, unitID, supervisorID, projectWorkerID, companyID, excludeMissedPunch,
                                                     Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsWorker.Tables.Count > 0 && dsWorker.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drWorder in dsWorker.Tables[0].Rows)
                {
                    DataRow drTemp = dtTemp.NewRow();

                    drTemp["MAIN_RECORD_FLAG"] = 1;
                    drTemp["SERIAL_NO"] = drWorder["SERIAL_NO"];
                    drTemp["PUNCH_MISSED"] = drWorder["PUNCH_MISSED"];
                    drTemp["EMPLOYEE_NAME"] = drWorder["EMPLOYEE_NAME"];
                    drTemp["EMPLOYEE_CODE"] = drWorder["EMPLOYEE_CODE"];
                    drTemp["UNIT"] = drWorder["UNIT_NAME"];

                    if (drWorder["JOB_NO"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drWorder["JOB_NO"])))
                        drTemp["JOB_NO"] = Convert.ToString(drWorder["JOB_NO"]);
                    else
                        drTemp["JOB_NO"] = Convert.ToString(ddlJOBNo.SelectedValue);


                    drTemp["JOB_NO1"] = string.Empty;

                    if (drWorder["ITEM_NAME"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drWorder["ITEM_NAME"])))
                        drTemp["ITEM_NAME"] = Convert.ToString(drWorder["ITEM_NAME"]);
                    else
                        drTemp["ITEM_NAME"] = string.Empty;

                    drTemp["SUBITEM_DESC"] = string.Empty;
                    //drTemp["ACTIVITY_MATRIX"] = string.Empty;

                    drTemp["ENTRY_DATE"] = drWorder["ENTRY_DATE"];
                    drTemp["IN_TIME"] = drWorder["IN_TIME"];
                    drTemp["OUT_TIME"] = drWorder["OUT_TIME"];
                    drTemp["WORKING_HOURS"] = drWorder["WORKING_HOURS"];
                    drTemp["WORKING_MINUTS"] = drWorder["WORKING_MINUTS"];

                    drTemp["BAL_TIMESHEET_HRS"] = drWorder["BAL_TIMESHEET_HRS"];

                    if (drWorder["BAL_TIMESHEET_HRS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drWorder["BAL_TIMESHEET_HRS"])))
                        drTemp["ADJUSTED_WORKING_HOURS"] = drWorder["BAL_TIMESHEET_HRS"];
                    else
                        drTemp["ADJUSTED_WORKING_HOURS"] = drWorder["ADJUSTED_WORKING_HOURS"];

                    drTemp["OT_HOURS"] = drWorder["OT_HOURS"];
                    drTemp["BAL_WRK_HRS"] = drWorder["BAL_WRK_HRS"];

                    drTemp["SAVED_TIMESHEET_HOURS"] = drWorder["SAVED_TIMESHEET_HOURS"];

                    drTemp["REMARKS"] = drWorder["REMARKS"];

                    if (drWorder["IS_PREV_EXISTED"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(drWorder["IS_PREV_EXISTED"])))
                        drTemp["IS_PREV_EXISTED"] = drWorder["IS_PREV_EXISTED"];
                    else drTemp["IS_PREV_EXISTED"] = "0";

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

                SuccessMessage("No attendance found or timesheet hours saved successfully of " + Convert.ToString(hdDate.Value) + "...!!");
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

            string subitemDesc = string.Empty;
            string activityMatrix = string.Empty;

            string entryDate = string.Empty;
            string inTime = string.Empty;
            string outTime = string.Empty;
            string hours = string.Empty;
            string workingHours = string.Empty;
            string workingHoursh = string.Empty;
            string workingHoursm = string.Empty;
            string otHours = string.Empty;
            string remarks = string.Empty;
            int count = 0;
            string serialNos = string.Empty;
            string serialNo = string.Empty;

            DataTable dt = new DataTable();
            DataTable dtTextChanged = new DataTable();

            if (Session["dtTemp"] != null)
                dt = (DataTable)Session["dtTemp"];

            if (Session["dtChanged"] != null)
                dtTextChanged = (DataTable)Session["dtChanged"];

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
                            DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;
                            TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                            Label lblPunchMissed = gr.FindControl("lblPunchMissed") as Label;

                            if (ddlJOBNo.SelectedIndex > 0 || !string.IsNullOrEmpty(txtJOBNo.Text))
                            {
                                ddlJOBNo.BackColor = System.Drawing.Color.Transparent;
                                txtJOBNo.BackColor = System.Drawing.Color.LightYellow;

                            }
                            else
                            {
                                if (Convert.ToInt32(lblPunchMissed.Text) == 0)
                                {
                                    ExceptionMessage("Please select or enter JOB no...!!");
                                    ddlJOBNo.BackColor = System.Drawing.Color.LightPink;
                                    ddlJOBNo.Focus();
                                    return;
                                }
                                else
                                {
                                    ddlJOBNo.BackColor = System.Drawing.Color.Transparent;
                                    txtJOBNo.BackColor = System.Drawing.Color.LightYellow;
                                }
                            }
                        }

                        foreach (GridViewRow gr in gvWorkerTimesheetList.Rows)
                        {
                            Label lblSerialNo = gr.FindControl("lblSerialNo") as Label;
                            Label lblPunchMissed = gr.FindControl("lblPunchMissed") as Label;
                            Label lblEmpCode = gr.FindControl("lblEmpCode") as Label;
                            Label lblEmployeeName = gr.FindControl("lblEmployeeName") as Label;
                            DropDownList ddlJOBNo = gr.FindControl("ddlJOBNo") as DropDownList;
                            TextBox txtJOBNo = gr.FindControl("txtJOBNo") as TextBox;
                            DropDownList ddlHours = gr.FindControl("ddlHours") as DropDownList;
                            DropDownList ddlMins = gr.FindControl("ddlMins") as DropDownList;
                            DropDownList ddlSubitems = gr.FindControl("ddlSubitems") as DropDownList;
                            //TextBox txtActivityMatrix = gr.FindControl("txtActivityMatrix") as TextBox;
                            TextBox txtItemName = gr.FindControl("txtItemName") as TextBox;
                            Label lblEntryDate = gr.FindControl("lblEntryDate") as Label;
                            Label lblInTime = gr.FindControl("lblInTime") as Label;
                            Label lblOutTime = gr.FindControl("lblOutTime") as Label;
                            Label lblWorkingHours = gr.FindControl("lblWorkingHours") as Label;
                            Label lblOTHours = gr.FindControl("lblOTHours") as Label;

                            TextBox txtAdjustedWorkingHours = gr.FindControl("txtAdjustedWorkingHours") as TextBox;

                            Label lblBalanceWorkingHours = gr.FindControl("lblBalanceWorkingHours") as Label;
                            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

                            if (lblEmpCode.Text == dtEmployeeCode && lblEmployeeName.Text == dtEmployeeName && Convert.ToInt32(lblPunchMissed.Text) == 0)
                            {
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
                                    ddlJOBNo.BackColor = System.Drawing.Color.Transparent;
                                }
                                else
                                {
                                    jobNo = string.Empty;
                                    ExceptionMessage("Please select or enter JOB no...!!");
                                    ddlJOBNo.BackColor = System.Drawing.Color.LightPink;
                                    ddlJOBNo.Focus();
                                    return;
                                }



                                if (ddlSubitems.SelectedIndex > 0)
                                    subitemDesc = Convert.ToString(ddlSubitems.SelectedItem.Text);
                                else if (!string.IsNullOrEmpty(txtItemName.Text))
                                    subitemDesc = Convert.ToString(txtItemName.Text);
                                else
                                    subitemDesc = string.Empty;

                                //if (!string.IsNullOrEmpty(txtActivityMatrix.Text))
                                //    activityMatrix = Convert.ToString(txtActivityMatrix.Text);
                                //else
                                //    activityMatrix = string.Empty;

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


                                if (!string.IsNullOrEmpty(lblOTHours.Text))
                                    otHours = Convert.ToString(lblOTHours.Text);
                                else
                                    otHours = string.Empty;

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

                                string nas = string.Empty;
                                int nai = 0;

                                if (ddlHours.SelectedIndex > 0 | ddlMins.SelectedIndex > 0)
                                {
                                    if (!string.IsNullOrEmpty(employeeCode) && !string.IsNullOrEmpty(workingHours) && workingHours != "00:00")
                                    {
                                        //insertQuery += "('" + employeeCode + "','" +
                                        //                      entryDate + "','" +
                                        //                      jobNo + "','" +
                                        //                      subitemDesc + "','" +
                                        //                      activityMatrix + "','" +
                                        //                      inTime + "','" +
                                        //                      outTime + "','" +
                                        //                      hours + "','" +
                                        //                      otHours + "','" +
                                        //                      workingHours + "','" +
                                        //                      remarks + "'," +
                                        //                      Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",GETDATE()),";
                                        //serialNo += lblSerialNo.Text + ",";


                                        insertQuery += "('" + employeeCode + "','" +
                                                              entryDate + "','" +
                                                              jobNo + "','" +
                                                              nas + "','" +
                                                              nas + "','" +
                                                              subitemDesc + "','" +
                                                              nas + "','" +
                                                              nas + "'," +
                                                              nai + ",'" +
                                                              activityMatrix + "','" +
                                                              inTime + "','" +
                                                              outTime + "','" +
                                                              hours + "','" +
                                                              otHours + "','" +
                                                              workingHours + "','" +
                                                              remarks + "'," +

                                                              Convert.ToInt32(Session["EMP_RECORD_ID"]) + ",GETDATE()),";
                                        serialNo += lblSerialNo.Text + ",";


                                    }
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
                        //else
                        //{
                        //    ExceptionMessage("Please try again..!");
                        //    return;
                        //}
                    }
                }

                BindWorkerTimesheet();

                //if (!string.IsNullOrEmpty(serialNos))
                //{
                //    serialNos = serialNos.TrimEnd(',');
                //    RemoveRecords(dtTextChanged, serialNos);
                //}

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