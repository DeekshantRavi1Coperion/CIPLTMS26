using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TIMESHEET_AddUpdateTimesheetNew : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUserTimeDetails = new DataSet();

    DataSet dsCategory = new DataSet();
    DataSet dsProjectNumber = new DataSet();
    DataSet dsSerialNumber = new DataSet();
    DataSet dsSize = new DataSet();
    DataSet dsRev = new DataSet();
    DataSet dsTypeOfDrawing = new DataSet();

    int empRecordID = 0;
    //string entryDate = string.Empty;
    string projectCategory = string.Empty;
    string projectNumber = string.Empty;
    string jobNumber = string.Empty;

    string drawingCategory = string.Empty;
    string serialNumber = string.Empty;
    string size = string.Empty;
    string rev = string.Empty;
    int sheets = 0;
    string drawingID = string.Empty;
    int drawingTypeID = 0;

    string startTime = string.Empty;
    string endTime = string.Empty;
    string timeSpent = string.Empty;
    string title = string.Empty;

    string timesheetDate = string.Empty;

    string remarks = string.Empty;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdConfirmValue.Value = "0";

                hdTimesheetDeptID.Value = Convert.ToString(Session["TIMESHEET_DEPT_ID"]);

                hdTimesheetDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTimesheetDate.Text = Convert.ToString(hdTimesheetDate.Value);



                BindCategory();
                BindProjectNumber();
                BindSerialNumber();
                BindSize();
                BindRev();
                BindTypeOfDrawing();


                GetCurrentTimesheetList();

                if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 100 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 56 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 23 ||
                    Convert.ToInt32(Session["EMP_RECORD_ID"]) == 104)
                {
                    lblTimesheetDate.Visible = true;
                    txtTimesheetDate.Visible = true;
                    btnTimesheet.Visible = true;
                }
                else
                {
                    lblTimesheetDate.Visible = false;
                    txtTimesheetDate.Visible = false;
                    imgbtnTimesheetDate.Visible = false;
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            InsertTimeSheet();
        }
    }

    protected void btnTimesheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TIMESHEET/TimesheetListNew.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void GetCurrentTimesheetList()
    {
        try
        {
            timesheetDate = Convert.ToDateTime(hdTimesheetDate.Value).ToString("yyyy-MM-dd");
            dsUserTimeDetails = objProject.GetUserLastEndTime(Convert.ToInt32(Session["EMP_RECORD_ID"]), timesheetDate);
            if (dsUserTimeDetails.Tables.Count > 0 && dsUserTimeDetails.Tables[0].Rows.Count > 0)
            {
                Session["dsUserTimeDetails"] = dsUserTimeDetails;
                gvTimesheetList.DataSource = dsUserTimeDetails.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
                Session["dsUserTimeDetails"] = null;
            }
            lblRecords.Text = "Records[" + gvTimesheetList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertTimeSheet()
    {
        try
        {
            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            projectCategory = Convert.ToString(ddlProjectCategory.SelectedItem.Text);
            projectNumber = Convert.ToString(ddlProjectNumber.SelectedItem.Text);
            jobNumber = projectCategory + projectNumber;


            if (ddlCategory.SelectedIndex > 0)
                drawingCategory = Convert.ToString(ddlCategory.SelectedItem.Text);
            else drawingCategory = string.Empty;

            if (ddlSerialNumber.SelectedIndex > 0)
                serialNumber = Convert.ToString(ddlSerialNumber.SelectedItem.Text);
            else serialNumber = string.Empty;

            if (ddlSize.SelectedIndex > 0)
                size = Convert.ToString(ddlSize.SelectedItem.Text);
            else size = string.Empty;

            if (ddlRev.SelectedIndex > 0)
                rev = Convert.ToString(ddlRev.SelectedItem.Text);
            else rev = string.Empty;

            if (!string.IsNullOrEmpty(txtSheets.Text))
                sheets = Convert.ToInt32(txtSheets.Text);
            else sheets = 0;

            if (!string.IsNullOrEmpty(hdDrawingID.Value))
                drawingID = Convert.ToString(hdDrawingID.Value);
            else drawingID = string.Empty;

            if (ddlTypeOfDrawing.SelectedIndex > 0)
                drawingTypeID = Convert.ToInt32(ddlTypeOfDrawing.SelectedValue);
            else drawingTypeID = 0;


            startTime = txtStartTime.Text;
            endTime = txtEndTime.Text;

            timeSpent = Convert.ToString(hdTimeSpent.Value);

            title = txtTitle.Text;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;

            int value = 0;
            string lastEndTime = string.Empty;

            bool checkForValidTime = false;

            timesheetDate = Convert.ToDateTime(hdTimesheetDate.Value).ToString("yyyy-MM-dd");



            if (Convert.ToInt32(Session["EMP_RECORD_ID"]) == 100 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 56 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 23 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 104)
            {
                dsUserTimeDetails = objProject.GetUserLastEndTime(Convert.ToInt32(Session["EMP_RECORD_ID"]), timesheetDate);
                Session["dsUserTimeDetails"] = dsUserTimeDetails;
                gvTimesheetList.DataSource = dsUserTimeDetails.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
                if (Session["dsUserTimeDetails"] != null)
                    dsUserTimeDetails = (DataSet)Session["dsUserTimeDetails"];
                else
                    dsUserTimeDetails = objProject.GetUserLastEndTime(Convert.ToInt32(Session["EMP_RECORD_ID"]), timesheetDate);

            }




            if (dsUserTimeDetails.Tables.Count > 0 && dsUserTimeDetails.Tables[0].Rows.Count > 0)
            {
                if (dsUserTimeDetails.Tables[0].Rows.Count == 1)
                {
                    if (Convert.ToDateTime(endTime).TimeOfDay <= Convert.ToDateTime(dsUserTimeDetails.Tables[0].Rows[0]["START_TIME"]).TimeOfDay)
                        checkForValidTime = true;
                    else if (Convert.ToDateTime(startTime).TimeOfDay >= Convert.ToDateTime(dsUserTimeDetails.Tables[0].Rows[0]["END_TIME"]).TimeOfDay)
                        checkForValidTime = true;
                    else
                        checkForValidTime = false;
                }
                else
                {
                    for (int i = 0; i < dsUserTimeDetails.Tables[0].Rows.Count; i++)
                    {
                        if (i == dsUserTimeDetails.Tables[0].Rows.Count - 1)
                        {
                            if (Convert.ToDateTime(dsUserTimeDetails.Tables[0].Rows[i]["END_TIME"]).TimeOfDay <= Convert.ToDateTime(startTime).TimeOfDay)
                            {
                                checkForValidTime = true;
                                break;
                            }
                            else
                                checkForValidTime = false;
                        }
                        else
                        {
                            //prev end_time  >= startTime and endTime <= next start_time

                            if (Convert.ToDateTime(dsUserTimeDetails.Tables[0].Rows[i]["END_TIME"]).TimeOfDay >= Convert.ToDateTime(startTime).TimeOfDay &&
                                Convert.ToDateTime(endTime).TimeOfDay <= Convert.ToDateTime(dsUserTimeDetails.Tables[0].Rows[i + 1]["START_TIME"]).TimeOfDay)
                            {
                                checkForValidTime = true;
                                break;
                            }
                            else
                                checkForValidTime = false;
                        }
                    }
                }

                //lastEndTime = Convert.ToString(dsUserTimeDetails.Tables[0].Rows[0]["END_TIME"]);
            }
            else
            {
                checkForValidTime = true;
                //lastEndTime = string.Empty;
            }


            if (checkForValidTime)
            {
                value = objProject.InsertUpdateTimesheet(0, empRecordID, projectCategory, projectNumber,
                                                            jobNumber, drawingCategory, serialNumber, size, rev, sheets, drawingID, drawingTypeID,
                                                            startTime, endTime, timeSpent, title, timesheetDate, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    Reset();
                    SuccessMessage("Timesheet details added successfully...!!!");
                    GetCurrentTimesheetList();
                    return;
                }
                else
                {
                    ExceptionMessage("Please try again...!!!");
                    return;
                }
            }
            else
            {
                ExceptionMessage("Timesheet already exists between " + startTime + " and " + endTime + "...!!!");
                return;
            }


            //if (!string.IsNullOrEmpty(lastEndTime))
            //{
            //    if (Convert.ToDateTime(lastEndTime).TimeOfDay < Convert.ToDateTime(startTime).TimeOfDay)
            //    {
            //        value = objProject.InsertUpdateTimesheet(0, empRecordID, projectCategory, projectNumber,
            //                                                jobNumber, drawingCategory, serialNumber, size, rev, sheets, drawingID, drawingTypeID,
            //                                                startTime, endTime, timeSpent, title, timesheetDate, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            //        if (value > 0)
            //        {
            //            Reset();
            //            SuccessMessage("Timesheet details added successfully");
            //        }
            //        else
            //        {
            //            ExceptionMessage("Please try again.");
            //        }
            //    }
            //    else
            //    {
            //        ExceptionMessage("Start time is shuld be greater than last end time!");
            //    }
            //}
            //else
            //{
            //    value = objProject.InsertUpdateTimesheet(0, empRecordID, projectCategory, projectNumber,
            //                                  jobNumber, drawingCategory, serialNumber, size, rev, sheets, drawingID, drawingTypeID,
            //                                  startTime, endTime, timeSpent, title, timesheetDate, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            //    if (value > 0)
            //    {
            //        Reset();
            //        SuccessMessage("Timesheet details added successfully");
            //    }
            //    else
            //    {
            //        ExceptionMessage("Please try again.");
            //    }
            //}


        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        try
        {
            ddlProjectCategory.SelectedIndex = 0;
            ddlProjectNumber.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlSerialNumber.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            ddlRev.SelectedIndex = 0;
            txtSheets.Text = string.Empty;
            txtDrawingID.Text = string.Empty;
            ddlTypeOfDrawing.SelectedIndex = 0;
            txtStartTime.Text = "00:00";
            txtEndTime.Text = "00:00";
            txtTimeSpent.Text = "00:00";
            txtTitle.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCategory()
    {
        try
        {
            dsCategory = objProject.GetDetailsBySP("sp_get_project_category");
            if (dsCategory.Tables.Count > 0)
            {
                //Project
                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    ddlProjectCategory.DataSource = dsCategory.Tables[0];
                    ddlProjectCategory.DataTextField = "CATEGORY_NAME";
                    ddlProjectCategory.DataValueField = "CATEGORY_ID";
                    ddlProjectCategory.DataBind();
                    ddlProjectCategory.Items.Insert(0, "Select");
                    ddlProjectCategory.SelectedIndex = 0;
                }

                //Drawing
                if (dsCategory.Tables[1].Rows.Count > 0)
                {
                    ddlCategory.DataSource = dsCategory.Tables[1];
                    ddlCategory.DataTextField = "CATEGORY_NAME";
                    ddlCategory.DataValueField = "CATEGORY_ID";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, "Select");
                    ddlCategory.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProjectNumber()
    {
        try
        {
            dsProjectNumber = objProject.GetDetailsBySP("sp_get_project_number");
            if (dsProjectNumber.Tables.Count > 0 && dsProjectNumber.Tables[0].Rows.Count > 0)
            {
                ddlProjectNumber.DataSource = dsProjectNumber.Tables[0];
                ddlProjectNumber.DataTextField = "PROJECT_NUMBER";
                ddlProjectNumber.DataValueField = "RECORD_ID";
                ddlProjectNumber.DataBind();
                ddlProjectNumber.Items.Insert(0, "Select");
                ddlProjectNumber.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindSerialNumber()
    {
        try
        {
            dsSerialNumber = objProject.GetDetailsBySP("sp_get_serial_number");
            if (dsSerialNumber.Tables.Count > 0 && dsSerialNumber.Tables[0].Rows.Count > 0)
            {
                ddlSerialNumber.DataSource = dsSerialNumber.Tables[0];
                ddlSerialNumber.DataTextField = "SERIAL_NUMBER";
                ddlSerialNumber.DataValueField = "RECORD_ID";
                ddlSerialNumber.DataBind();
                ddlSerialNumber.Items.Insert(0, "Select");
                ddlSerialNumber.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindSize()
    {
        try
        {
            dsSize = objProject.GetDetailsBySP("sp_get_project_size");
            if (dsSize.Tables.Count > 0 && dsSize.Tables[0].Rows.Count > 0)
            {
                ddlSize.DataSource = dsSize.Tables[0];
                ddlSize.DataTextField = "SIZE";
                ddlSize.DataValueField = "RECORD_ID";
                ddlSize.DataBind();
                ddlSize.Items.Insert(0, "Select");
                ddlSize.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindRev()
    {
        try
        {
            dsRev = objProject.GetDetailsBySP("sp_get_project_rev");
            if (dsRev.Tables.Count > 0 && dsRev.Tables[0].Rows.Count > 0)
            {
                ddlRev.DataSource = dsRev.Tables[0];
                ddlRev.DataTextField = "REV";
                ddlRev.DataValueField = "RECORD_ID";
                ddlRev.DataBind();
                ddlRev.Items.Insert(0, "Select");
                ddlRev.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTypeOfDrawing()
    {
        try
        {
            dsTypeOfDrawing = objProject.GetDetailsBySP("sp_get_drawing_type");
            if (dsTypeOfDrawing.Tables.Count > 0 && dsTypeOfDrawing.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfDrawing.DataSource = dsTypeOfDrawing.Tables[0];
                ddlTypeOfDrawing.DataTextField = "DRAWING_TYPE";
                ddlTypeOfDrawing.DataValueField = "DRAWING_TYPE_ID";
                ddlTypeOfDrawing.DataBind();
                ddlTypeOfDrawing.Items.Insert(0, "Select");
                ddlTypeOfDrawing.SelectedIndex = 0;
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

    #endregion

}
