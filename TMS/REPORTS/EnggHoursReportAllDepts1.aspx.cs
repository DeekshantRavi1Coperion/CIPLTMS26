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

public partial class REPORTS_EnggHoursReportAllDepts1 : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Project objProject = new BAL.Project();
    BAL.Reports objReports = new BAL.Reports();
    DataSet dsTimesheetDept = new DataSet();
    DataSet dsTimesheetDetails = new DataSet();
    DataSet dsEnggHours = new DataSet();


    string month = string.Empty;
    int year = 0;
    int deptID = 0;


    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        HidePanel();
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["ENGG_HOURS"] = null;
                GetTimesheetDeptList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetEnggHoursReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvEnggHoursReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["ENGG_HOURS"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    protected void gvEnggHoursReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
        }
    }

    #endregion


    #region METHODS[=========================]


    private void GetTimesheetDeptList()
    {
        try
        {
            dsTimesheetDept = objProject.GetTimesheetDeptList();
            if (dsTimesheetDept.Tables.Count > 0 && dsTimesheetDept.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsTimesheetDept.Tables[0];
                ddlDepartment.DataTextField = "TIMESHEET_DEPT_NAME";
                ddlDepartment.DataValueField = "TIMESHEET_DEPT_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetEnggHoursReport()
    {
        try
        {
            string jobNoTxt = string.Empty;
            string timesheetDeptIDTxt = string.Empty;
            string timesheetDeptNameTxt = string.Empty;
            string totalTimeSpentTxt = string.Empty;
            string typeTxt = string.Empty;

            string timesheetDeptName = string.Empty;
            string totalTimeSpent = string.Empty;
            string type = string.Empty;

            string columnQuery = string.Empty;
            string createQuery = string.Empty;
            string insertQuery = string.Empty;
            string insertQueryValue = string.Empty;

            int deptID = 0;
            string deptName = string.Empty;
            string JOBNo = string.Empty;
            string timeSpent = string.Empty;

            month = string.Empty;
            year = 0;
            deptID = 0;

            DataSet dsTempHoursList = new DataSet();

            month = Convert.ToString(ddlMonth.SelectedValue);
            year = Convert.ToInt32(ddlYear.SelectedValue);

            if (ddlDepartment.SelectedIndex > 0)
                deptID = Convert.ToInt32(ddlDepartment.SelectedValue);


            dsTimesheetDetails = objReports.GetTimehsheetOfAllDepts(month, year, deptID, "");

            if (dsTimesheetDetails.Tables.Count > 0 && dsTimesheetDetails.Tables[0].Rows.Count > 0)
            {
                if (dsTimesheetDetails.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTimesheetDetails.Tables[1].Rows)
                    {
                        jobNoTxt = Convert.ToString(dr["JOB_NUMBER"]);
                        columnQuery += "[" + jobNoTxt + "] [varchar](70),";
                    }
                }

                createQuery = "IF (SELECT count(name) FROM sysobjects WHERE  name=N'tblTempengghoursAllDepts')>0 begin drop table tblTempengghoursAllDepts end; CREATE TABLE [dbo].[tblTempengghoursAllDepts]( [TIMESHEET_DEPT_ID] [int], [TIMESHEET_DEPT_NAME] [varchar](100), [TYPE] [varchar](10)," + columnQuery + "[TOTAL_TIME_SPENT] [varchar](100))";

                if (dsTimesheetDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTimesheetDetails.Tables[0].Rows)
                    {
                        if (dr["TIMESHEET_DEPT_ID"] != DBNull.Value)
                            timesheetDeptIDTxt = "'" + Convert.ToString(dr["TIMESHEET_DEPT_ID"]) + "'";

                        if (dr["TIMESHEET_DEPT_NAME"] != DBNull.Value)
                            timesheetDeptNameTxt = "'" + Convert.ToString(dr["TIMESHEET_DEPT_NAME"]) + "'";

                        if (dr["TOTAL_TIME_SPENT"] != DBNull.Value)
                            totalTimeSpentTxt = "'" + Convert.ToString(dr["TOTAL_TIME_SPENT"]) + "'";

                        if (dr["TYPE"] != DBNull.Value)
                            typeTxt = "'" + Convert.ToString(dr["TYPE"]) + "'";

                        insertQueryValue += "(" + timesheetDeptIDTxt + "," + timesheetDeptNameTxt + "," + totalTimeSpentTxt + "," + typeTxt + ")," + Environment.NewLine;

                    }
                    insertQuery = "INSERT INTO tblTempengghoursAllDepts([TIMESHEET_DEPT_ID],[TIMESHEET_DEPT_NAME],[TOTAL_TIME_SPENT],[TYPE])values" + insertQueryValue.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(insertQuery))
                    insertQuery = insertQuery.TrimEnd(',');

                int count = 0;
                string createInsertQuery = createQuery + ";" + insertQuery;

                createInsertQuery = createInsertQuery.Trim().TrimEnd(',') + Environment.NewLine + "select * from tblTempengghoursAllDepts";
                int createValue = objReports.CreateTimehsheetAllDeptsTempTable(createInsertQuery);

                string updateQuery = string.Empty;

                deptID = 0;
                deptName = string.Empty;
                type = string.Empty;
                JOBNo = string.Empty;
                timeSpent = string.Empty;

                if (createValue > 0)
                {
                    dsTempHoursList = objReports.GetTimehsheetAllDeptsTempTableDetails();
                    if (dsTempHoursList.Tables.Count > 0 && dsTempHoursList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTemp in dsTempHoursList.Tables[0].Rows)
                        {
                            foreach (DataRow dr in dsTimesheetDetails.Tables[2].Select("TIMESHEET_DEPT_ID=" + drTemp["TIMESHEET_DEPT_ID"] + " AND TIMESHEET_DEPT_NAME='" + drTemp["TIMESHEET_DEPT_NAME"] + "' AND TYPE='" + drTemp["TYPE"] + "'"))
                            {
                                deptID = Convert.ToInt32(dr["TIMESHEET_DEPT_ID"]);
                                deptName = Convert.ToString(dr["TIMESHEET_DEPT_NAME"]);
                                type = Convert.ToString(dr["TYPE"]);
                                JOBNo = Convert.ToString(dr["JOB_NUMBER"]);
                                timeSpent = Convert.ToString(dr["TIME_SPENT"]);

                                updateQuery += "update tblTempengghoursAllDepts set [" + JOBNo + "]='" + timeSpent + "' " +
                                                "where TIMESHEET_DEPT_ID=" + deptID + " AND " +
                                                "TIMESHEET_DEPT_NAME='" + deptName + "' AND " +
                                                "TYPE='" + type + "'" + Environment.NewLine;
                            }
                        }


                        int updateValue = objReports.UpdateTempTable(updateQuery);
                        if (updateValue > 0)
                        {
                            count++;
                        }


                        dsTempHoursList = objReports.GetTimehsheetAllDeptsTempTableDetails();

                        if (dsTempHoursList.Tables.Count > 0 && dsTempHoursList.Tables[0].Rows.Count > 0)
                        {
                            dsTempHoursList.Tables[0].Columns.Remove("TIMESHEET_DEPT_ID");
                            dsTempHoursList.Tables[0].Columns.Remove("TYPE");

                            Session["ENGG_HOURS"] = dsTempHoursList;
                            gvEnggHoursReport.DataSource = dsTempHoursList.Tables[0];
                            gvEnggHoursReport.DataBind();
                        }
                        else
                        {
                            Session["ENGG_HOURS"] = null;
                            gvEnggHoursReport.DataSource = null;
                            gvEnggHoursReport.DataBind();
                        }
                        lblRecords.Text = "Records[" + dsTempHoursList.Tables[0].Rows.Count + "]";
                    }
                }
            }
            else
            {
                dsTempHoursList = null;
                Session["ENGG_HOURS"] = null;
                gvEnggHoursReport.DataSource = null;
                gvEnggHoursReport.DataBind();
                lblRecords.Text = "Records[0]";
                ExceptionMessage("Data not found!");
                return;
            }
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

            string fileName = "Timesheet_All_Dept_Reports_" + DateTime.Now.ToString("dd_MMM_yyyy") + "_" + Convert.ToString(ddlDepartment.SelectedItem.Text);
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

    private void ExceptionMessage(string message)
    {
        pnlMsg.Visible = true;
        lblMsg.Text = message;
        lblMsg.ForeColor = System.Drawing.Color.Red;
    }


    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}
