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

public partial class TIMESHEET_ImportOldTimesheetDetails : System.Web.UI.Page
{

    #region VARIABLES[====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    BAL.Project objProject = new BAL.Project();

    DataTable dtTeamMembers = new DataTable();
    DataSet dsTimesheetDept = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsOldTimesheetDetails = new DataSet();
    DataSet dsEmployeeList = new DataSet();
    DataSet dsDrawingType = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;
    string status = string.Empty;
    string userName = string.Empty;
    string teamUserNames = string.Empty;
    #endregion


    #region EVENTS[=======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindStatus();
                BindUser();
                Session["dtTempNewTimesheet"] = null;
                Session["dsEmployeeList"] = objTourAndTravels.GetEmployeeList(0);
                Session["dsDrawingType"] = objProject.GetDetailsBySP("sp_get_drawing_type");
                GetOldTimesheetDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void gvTimesheetList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportTimesheet();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOldTimesheetDetails();
    }

    protected void gvTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTimesheetList.PageIndex = e.NewPageIndex;
        GetOldTimesheetDetails();
    }

    #endregion


    #region METHODS[======================]

    private void BindStatus()
    {
        try
        {
            dsTimesheetDept = objProject.GetTimesheetDeptList();
            if (dsTimesheetDept.Tables.Count > 0 && dsTimesheetDept.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsTimesheetDept.Tables[0];
                ddlStatus.DataTextField = "TIMESHEET_DEPT_CODE";
                ddlStatus.DataValueField = "TIMESHEET_DEPT_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindUser()
    {
        try
        {
            dsEmployee = objTourAndTravels.GetEmployeeForTravel(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlUser.DataSource = dsEmployee.Tables[0];
                ddlUser.DataTextField = "USER_NAME";
                ddlUser.DataValueField = "EMP_RECORD_ID";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, "Select");
                ddlUser.SelectedValue = Convert.ToString(Session["EMP_RECORD_ID"]);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetOldTimesheetDetails()
    {
        try
        {
            dsEmployeeList = (DataSet)Session["dsEmployeeList"];
            dsDrawingType = (DataSet)Session["dsDrawingType"];

            DataTable dtTempNewTimesheet = new DataTable();

            dtTempNewTimesheet.Columns.Add("ID", typeof(string));
            dtTempNewTimesheet.Columns.Add("EMP_RECORD_ID", typeof(string));
            dtTempNewTimesheet.Columns.Add("USER_NAME", typeof(string));
            dtTempNewTimesheet.Columns.Add("EMPLOYEE_NAME", typeof(string));
            dtTempNewTimesheet.Columns.Add("STATUS", typeof(string));
            dtTempNewTimesheet.Columns.Add("ENTRY_DATE", typeof(string));
            dtTempNewTimesheet.Columns.Add("PROJECT_CATETORY", typeof(string));
            dtTempNewTimesheet.Columns.Add("PROJECT_NUMBER", typeof(string));
            dtTempNewTimesheet.Columns.Add("JOB_NUMBER", typeof(string));
            dtTempNewTimesheet.Columns.Add("DRAWING_CATEGORY", typeof(string));
            dtTempNewTimesheet.Columns.Add("SERIAL_NUMBER", typeof(string));
            dtTempNewTimesheet.Columns.Add("SIZE", typeof(string));
            dtTempNewTimesheet.Columns.Add("REV", typeof(string));
            dtTempNewTimesheet.Columns.Add("SHEETS", typeof(string));
            dtTempNewTimesheet.Columns.Add("DRAWING_ID", typeof(string));
            dtTempNewTimesheet.Columns.Add("DRAWING_TYPE_ID", typeof(string));
            dtTempNewTimesheet.Columns.Add("DRAWING_TYPE", typeof(string));
            dtTempNewTimesheet.Columns.Add("START_TIME", typeof(string));
            dtTempNewTimesheet.Columns.Add("END_TIME", typeof(string));
            dtTempNewTimesheet.Columns.Add("TIME_SPENT", typeof(string));
            dtTempNewTimesheet.Columns.Add("TITLE", typeof(string));
            dtTempNewTimesheet.Columns.Add("REMARKS", typeof(string));
            dtTempNewTimesheet.Columns.Add("IS_IMPORTED", typeof(string));


            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text.Trim();
            else
                jobNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                status = Convert.ToString(ddlStatus.SelectedItem.Text);
            else
                status = string.Empty;

            if (ddlUser.SelectedIndex > 0)
                userName = Convert.ToString(ddlUser.SelectedItem.Text);
            else
            {
                userName = string.Empty;

                if (Session["TEAMMEMBERS"] != null)
                {
                    dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
                    foreach (DataRow dr in dtTeamMembers.Rows)
                    {
                        teamUserNames += "'" + Convert.ToString(dr["USER_NAME"]) + "',";
                    }
                    teamUserNames = "'" + Convert.ToString(Session["USER_NAME"]) + "'," + teamUserNames.TrimEnd(',');
                }
                else
                {
                    teamUserNames = "'" + Convert.ToString(Session["USER_NAME"]) + "'";
                }
            }

            dsOldTimesheetDetails = objProject.GetOldTimesheetDetails(startDate, endDate, jobNo, status, userName, teamUserNames);

            if (dsOldTimesheetDetails.Tables.Count > 0 && dsOldTimesheetDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOldTimesheetDetails.Tables[0].Rows)
                {
                    DataRow dtr = dtTempNewTimesheet.NewRow();

                    dtr["ID"] = Convert.ToString(dr["ID"]);
                    dtr["USER_NAME"] = Convert.ToString(dr["USER_ID"]);

                    foreach (DataRow dremp in dsEmployeeList.Tables[0].Select("USER_NAME ='" + Convert.ToString(dr["USER_ID"]) + "'"))
                    {
                        string empRecordID = Convert.ToString(dremp["EMP_RECORD_ID"]);
                        if (!string.IsNullOrEmpty(empRecordID))
                        {
                            dtr["EMP_RECORD_ID"] = empRecordID;
                            dtr["EMPLOYEE_NAME"] = Convert.ToString(dremp["EMPLOYEE_NAME"]);
                        }
                        else
                            dtr["EMP_RECORD_ID"] = "0";
                    }

                    dtr["STATUS"] = Convert.ToString(dr["STATUS"]);
                    dtr["ENTRY_DATE"] = Convert.ToDateTime(dr["EntryDate"]).ToString("dd-MMM-yyyy");
                    dtr["PROJECT_CATETORY"] = Convert.ToString(dr["CAT_ID"]);
                    dtr["PROJECT_NUMBER"] = Convert.ToString(dr["PROJ_ID"]);
                    dtr["JOB_NUMBER"] = Convert.ToString(dtr["PROJECT_CATETORY"]) + Convert.ToString(dtr["PROJECT_NUMBER"]);
                    dtr["DRAWING_CATEGORY"] = Convert.ToString(dr["DRW_CATEGORY"]);
                    dtr["SERIAL_NUMBER"] = Convert.ToString(dr["SerialNo"]);
                    dtr["SIZE"] = Convert.ToString(dr["SIZE"]);
                    dtr["REV"] = Convert.ToString(dr["REV_ID"]);
                    dtr["SHEETS"] = Convert.ToString(dr["SHEETS"]);
                    dtr["DRAWING_ID"] = Convert.ToString(dr["DRW_ID"]);

                    foreach (DataRow drdy in dsDrawingType.Tables[0].Select("DRAWING_TYPE='" + Convert.ToString(dr["TypeOfDrawing"]) + "'"))
                    {
                        string drawingTypeId = Convert.ToString(drdy["DRAWING_TYPE_ID"]);
                        if (!string.IsNullOrEmpty(drawingTypeId))
                            dtr["DRAWING_TYPE_ID"] = drawingTypeId;
                        else
                            dtr["DRAWING_TYPE_ID"] = "0";
                    }

                    dtr["DRAWING_TYPE"] = Convert.ToString(dr["TypeOfDrawing"]);
                    dtr["START_TIME"] = Convert.ToString(dr["From_Time"]);
                    dtr["END_TIME"] = Convert.ToString(dr["To_Time"]);
                    dtr["TIME_SPENT"] = Convert.ToString(dr["TimeSpent"]);
                    dtr["TITLE"] = Convert.ToString(dr["TITLE"]);
                    dtr["REMARKS"] = Convert.ToString(dr["REMARKS"]);
                    dtr["IS_IMPORTED"] = Convert.ToString(dr["IS_IMPORTED"]);

                    dtTempNewTimesheet.Rows.Add(dtr);
                    if (dtTempNewTimesheet.Rows.Count > 0)
                    {
                        Session["dtTempNewTimesheet"] = dtTempNewTimesheet;
                        gvTimesheetList.DataSource = dtTempNewTimesheet;
                        gvTimesheetList.DataBind();
                    }
                    else
                    {
                        Session["dtTempNewTimesheet"] = null;
                        ExceptionMessage("No data found!");
                    }
                }
            }
            else
            {
                Session["dtTempNewTimesheet"] = null;
                dtTempNewTimesheet.Rows.Clear();
                gvTimesheetList.DataSource = dtTempNewTimesheet;
                gvTimesheetList.DataBind();
                lblRecords.Text = "Records[" + dtTempNewTimesheet.Rows.Count + "]";
            }
            lblRecords.Text = "Records[" + dtTempNewTimesheet.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ImportTimesheet()
    {
        try
        {
            int val = 0;
            int ID = 0;
            int EMP_RECORD_ID = 0;
            string USER_NAME = string.Empty;
            string ENTRY_DATE = string.Empty;
            string PROJECT_CATETORY = string.Empty;
            string PROJECT_NUMBER = string.Empty;
            string JOB_NUMBER = string.Empty;
            string DRAWING_CATEGORY = string.Empty;
            string SERIAL_NUMBER = string.Empty;
            string SIZE = string.Empty;
            string REV = string.Empty;
            int SHEETS = 0;
            string DRAWING_ID = string.Empty;
            int DRAWING_TYPE_ID = 0;
            string START_TIME = string.Empty;
            string END_TIME = string.Empty;
            string TIME_SPENT = string.Empty;
            string TITLE = string.Empty;
            string REMARKS = string.Empty;
            int IS_IMPORTED = 0;

            DataTable dt = (DataTable)Session["dtTempNewTimesheet"];
            if (Session["dtTempNewTimesheet"] != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            ID = Convert.ToInt32(dr["ID"]);
                        else
                            ID = 0;

                        if (dr["EMP_RECORD_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["EMP_RECORD_ID"])))
                            EMP_RECORD_ID = Convert.ToInt32(dr["EMP_RECORD_ID"]);
                        else
                            EMP_RECORD_ID = 0;

                        if (dr["USER_NAME"] != DBNull.Value)
                            USER_NAME = Convert.ToString(dr["USER_NAME"]);
                        else
                            USER_NAME = string.Empty;

                        if (dr["ENTRY_DATE"] != DBNull.Value)
                            ENTRY_DATE = Convert.ToDateTime(dr["ENTRY_DATE"]).ToString("yyyy-MM-dd");
                        else
                            ENTRY_DATE = string.Empty;

                        if (dr["PROJECT_CATETORY"] != DBNull.Value)
                            PROJECT_CATETORY = Convert.ToString(dr["PROJECT_CATETORY"]);
                        else
                            PROJECT_CATETORY = string.Empty;

                        if (dr["PROJECT_NUMBER"] != DBNull.Value)
                            PROJECT_NUMBER = Convert.ToString(dr["PROJECT_NUMBER"]);
                        else
                            PROJECT_NUMBER = string.Empty;

                        if (dr["JOB_NUMBER"] != DBNull.Value)
                            JOB_NUMBER = Convert.ToString(dr["JOB_NUMBER"]);
                        else
                            JOB_NUMBER = string.Empty;

                        if (dr["DRAWING_CATEGORY"] != DBNull.Value)
                            DRAWING_CATEGORY = Convert.ToString(dr["DRAWING_CATEGORY"]);
                        else
                            DRAWING_CATEGORY = string.Empty;

                        if (dr["SERIAL_NUMBER"] != DBNull.Value)
                            SERIAL_NUMBER = Convert.ToString(dr["SERIAL_NUMBER"]);
                        else
                            SERIAL_NUMBER = string.Empty;

                        if (dr["SIZE"] != DBNull.Value)
                            SIZE = Convert.ToString(dr["SIZE"]);
                        else
                            SIZE = string.Empty;

                        if (dr["REV"] != DBNull.Value)
                            REV = Convert.ToString(dr["REV"]);
                        else
                            REV = string.Empty;

                        if (dr["SHEETS"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["SHEETS"])))
                            SHEETS = Convert.ToInt32(dr["SHEETS"]);
                        else
                            SHEETS = 0;

                        if (dr["DRAWING_ID"] != DBNull.Value)
                            DRAWING_ID = Convert.ToString(dr["DRAWING_ID"]);
                        else
                            DRAWING_ID = string.Empty;

                        if (dr["DRAWING_TYPE_ID"] != DBNull.Value && !string.IsNullOrEmpty(Convert.ToString(dr["DRAWING_TYPE_ID"])))
                            DRAWING_TYPE_ID = Convert.ToInt32(dr["DRAWING_TYPE_ID"]);
                        else
                            DRAWING_TYPE_ID = 0;

                        if (dr["START_TIME"] != DBNull.Value)
                            START_TIME = Convert.ToString(dr["START_TIME"]);
                        else
                            START_TIME = string.Empty;

                        if (dr["END_TIME"] != DBNull.Value)
                            END_TIME = Convert.ToString(dr["END_TIME"]);
                        else
                            END_TIME = string.Empty;

                        if (dr["TIME_SPENT"] != DBNull.Value)
                            TIME_SPENT = Convert.ToString(dr["TIME_SPENT"]);
                        else
                            TIME_SPENT = string.Empty;

                        if (dr["TITLE"] != DBNull.Value)
                            TITLE = Convert.ToString(dr["TITLE"]);
                        else
                            TITLE = string.Empty;

                        if (dr["REMARKS"] != DBNull.Value)
                            REMARKS = Convert.ToString(dr["REMARKS"]);
                        else
                            REMARKS = string.Empty;

                        if (dr["IS_IMPORTED"] != DBNull.Value)
                            IS_IMPORTED = Convert.ToInt32(dr["IS_IMPORTED"]);
                        else
                            IS_IMPORTED = 0;

                        int insertValue = objProject.ImportTimesheet(ID, EMP_RECORD_ID, ENTRY_DATE, PROJECT_CATETORY, PROJECT_NUMBER, JOB_NUMBER, DRAWING_CATEGORY, SERIAL_NUMBER, SIZE, REV, SHEETS, DRAWING_ID, DRAWING_TYPE_ID, START_TIME, END_TIME, TIME_SPENT, TITLE, REMARKS, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                        if (insertValue > 0)
                        {
                            //val = val + value;
                            int updateValue = objProject.UpdateOldTimesheetDetails(ID, IS_IMPORTED);
                            if (updateValue > 0)
                            {
                                val += updateValue;
                            }
                        }
                    }
                    if (val > 0)
                    {
                        SuccessMessage(val + " Records imported successfully.");
                    }
                    GetOldTimesheetDetails();
                }
                else
                {
                    ExceptionMessage("No data found!");
                }
            }
            else
            {
                ExceptionMessage("No data found!");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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
