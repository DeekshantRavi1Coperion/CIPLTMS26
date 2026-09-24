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

public partial class REPORTS_EnggHoursReportAllDepts : System.Web.UI.Page
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
    string JOBNo = string.Empty;

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
            dsTimesheetDept = objProject.GetTimesheetDeptListForAllDeptsReport();
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
            string columnTxt = string.Empty;

            string timesheetDeptIDTxt = string.Empty;
            string timesheetDeptNameTxt = string.Empty;
            string totalTimeSpentTxt = string.Empty;
            string typeTxt = string.Empty;

            string timesheetDeptName = string.Empty;
            string totalTimeSpent = string.Empty;

            string type = string.Empty;

            string jobNo = string.Empty;
            string typeMtd = string.Empty;
            string totalTimeSpentMtd = string.Empty;

            string typeCumulative = string.Empty;
            string totalTimeSpentCumulative = string.Empty;


            string columnQuery = string.Empty;
            string createQuery = string.Empty;
            string insertQueryTxt = string.Empty;
            string insertQuery = string.Empty;
            string insertQueryValue = string.Empty;
            string insertQueryValueMore = string.Empty;
            string insertQueryFinal = string.Empty;

            string deptName = string.Empty;
            string JOBNo = string.Empty;

            string drawingTimeSpentMtd = string.Empty;
            string eAndITimeSpentMtd = string.Empty;
            string projectTimeSpentMtd = string.Empty;
            string docTimeSpentMtd = string.Empty;

            string drawingTimeSpentCumulative = string.Empty;
            string eAndITimeSpentCumulative = string.Empty;
            string projectTimeSpentCumulative = string.Empty;
            string docTimeSpentCumulative = string.Empty;

            month = string.Empty;
            year = 0;
            deptID = 0;
            JOBNo = string.Empty;

            DataSet dsTempHoursList = new DataSet();

            month = Convert.ToString(ddlMonth.SelectedValue);
            year = Convert.ToInt32(ddlYear.SelectedValue);

            if (ddlDepartment.SelectedIndex > 0)
                deptID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (!string.IsNullOrEmpty(txtJOBNo.Text))
                JOBNo = txtJOBNo.Text;


            dsTimesheetDetails = objReports.GetTimehsheetOfAllDepts(month, year, deptID, JOBNo);

            if (dsTimesheetDetails.Tables.Count > 0 && dsTimesheetDetails.Tables[0].Rows.Count > 0)
            {

                if (dsTimesheetDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTimesheetDetails.Tables[0].Rows)
                    {
                        columnTxt = Convert.ToString(dr["TIMESHEET_DEPT_NAME"]);
                        columnQuery += "[" + columnTxt + "] [varchar](100),";
                    }
                }

                createQuery = "IF (SELECT count(name) FROM sysobjects WHERE  name=N'tblTempengghoursAllDepts')>0 begin drop table tblTempengghoursAllDepts end; CREATE TABLE [dbo].[tblTempengghoursAllDepts]( [JOB_NUMBER] [varchar](100), " + columnQuery + "" +
                                "[TYPE -MTD] [varchar](10), " +
                                "[TOTAL_TIME_SPENT -MTD] [varchar](100), " +
                                "[TYPE -Cumulative] [varchar](10), " +
                                "[TOTAL_TIME_SPENT -Cumulative] [varchar](100)) ";

                int insertionCount = 0;
                if (dsTimesheetDetails.Tables[0].Rows.Count > 0)
                {
                    insertQueryTxt = "INSERT INTO tblTempengghoursAllDepts([JOB_NUMBER],[TYPE -MTD],[TOTAL_TIME_SPENT -MTD],[TYPE -Cumulative],[TOTAL_TIME_SPENT -Cumulative])values";
                    foreach (DataRow dr in dsTimesheetDetails.Tables[1].Rows)
                    {
                        insertionCount++;
                        if (dr["JOB_NUMBER"] != DBNull.Value)
                            jobNo = "'" + Convert.ToString(dr["JOB_NUMBER"]) + "'";
                        else jobNo = "''";


                        if (dr["TYPE -MTD"] != DBNull.Value)
                            typeMtd = "'" + Convert.ToString(dr["TYPE -MTD"]) + "'";
                        else typeMtd = "''";

                        if (dr["TOTAL_TIME_SPENT -MTD"] != DBNull.Value)
                            totalTimeSpentMtd = "'" + Convert.ToString(dr["TOTAL_TIME_SPENT -MTD"]) + "'";
                        else totalTimeSpentMtd = "''";


                        if (dr["TYPE -Cumulative"] != DBNull.Value)
                            typeCumulative = "'" + Convert.ToString(dr["TYPE -Cumulative"]) + "'";
                        else typeCumulative = "''";

                        if (dr["TOTAL_TIME_SPENT -Cumulative"] != DBNull.Value)
                            totalTimeSpentCumulative = "'" + Convert.ToString(dr["TOTAL_TIME_SPENT -Cumulative"]) + "'";
                        else totalTimeSpentCumulative = "''";



                        insertQueryValue = "(" + jobNo + "," + typeMtd + "," + totalTimeSpentMtd + "," + typeCumulative + "," + totalTimeSpentCumulative + ");" + Environment.NewLine;
                        insertQuery += insertQueryTxt + insertQueryValue;
                    }
                }


                if (!string.IsNullOrEmpty(insertQuery))
                    insertQuery = insertQuery.TrimEnd(',');

                int count = 0;
                string createInsertQuery = createQuery + ";" + insertQuery;

                createInsertQuery = createInsertQuery.Trim().TrimEnd(',') + Environment.NewLine;
                int createValue = objReports.CreateTimehsheetAllDeptsTempTable(createInsertQuery);

                string updateQueryMtd = string.Empty;
                string updateQueryCumulative = string.Empty;
                string updateQuery = string.Empty;
                string updateQueryFinal = string.Empty;

                if (createValue > 0)
                {
                    dsTempHoursList = objReports.GetTimehsheetAllDeptsTempTableDetails();
                    if (dsTempHoursList.Tables.Count > 0 && dsTempHoursList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTemp in dsTempHoursList.Tables[0].Rows)
                        {
                            //MTD

                            updateQueryMtd = string.Empty;
                            updateQueryCumulative = string.Empty;

                            JOBNo = Convert.ToString(drTemp["JOB_NUMBER"]);

                            bool chkDrMtd = false;
                            bool chkEIMtd = false;
                            bool chkPrMtd = false;
                            bool chkDoMtd = false;


                            //CUMU
                            bool chkDrCumulative = false;
                            bool chkEICumulative = false;
                            bool chkPrCumulative = false;
                            bool chkDoCumulative = false;

                            foreach (DataRow drdr in dsTimesheetDetails.Tables[2].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='M' AND TIMESHEET_DEPT_NAME='Drawing -MTD'"))
                            {
                                chkDrMtd = true;
                                if (drdr["TIME_SPENT -MTD"] != DBNull.Value)
                                    drawingTimeSpentMtd = Convert.ToString(drdr["TIME_SPENT -MTD"]);
                                else drawingTimeSpentMtd = string.Empty;
                            }


                            foreach (DataRow drei in dsTimesheetDetails.Tables[2].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='M' AND TIMESHEET_DEPT_NAME='E&I -MTD'"))
                            {
                                chkEIMtd = true;
                                if (drei["TIME_SPENT -MTD"] != DBNull.Value)
                                    eAndITimeSpentMtd = Convert.ToString(drei["TIME_SPENT -MTD"]);
                                else eAndITimeSpentMtd = string.Empty;
                            }

                            foreach (DataRow drpr in dsTimesheetDetails.Tables[2].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='M' AND TIMESHEET_DEPT_NAME='Project -MTD'"))
                            {
                                chkPrMtd = true;
                                if (drpr["TIME_SPENT -MTD"] != DBNull.Value)
                                    projectTimeSpentMtd = Convert.ToString(drpr["TIME_SPENT -MTD"]);
                                else projectTimeSpentMtd = string.Empty;
                            }

                            foreach (DataRow drdo in dsTimesheetDetails.Tables[2].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='M' AND TIMESHEET_DEPT_NAME='DOC -MTD'"))
                            {
                                chkDoMtd = true;
                                if (drdo["TIME_SPENT -MTD"] != DBNull.Value)
                                    docTimeSpentMtd = Convert.ToString(drdo["TIME_SPENT -MTD"]);
                                else docTimeSpentMtd = string.Empty;
                            }


                            if (chkDrMtd == false)
                                drawingTimeSpentMtd = string.Empty;

                            if (chkEIMtd == false)
                                eAndITimeSpentMtd = string.Empty;

                            if (chkPrMtd == false)
                                projectTimeSpentMtd = string.Empty;

                            if (chkDoMtd == false)
                                docTimeSpentMtd = string.Empty;

                            updateQueryMtd = "update tblTempengghoursAllDepts " +
                                                           "set [Drawing -MTD]='" + drawingTimeSpentMtd + "' ," +
                                                               "[E&I -MTD] = '" + eAndITimeSpentMtd + "' ," +
                                                               "[Project -MTD] = '" + projectTimeSpentMtd + "' ," +
                                                               "[DOC -MTD] = '" + docTimeSpentMtd + "' " +
                                                           "where JOB_NUMBER='" + JOBNo + "' AND " +
                                                           "[TYPE -MTD]='M'" + Environment.NewLine;


                            //Cumulative                           

                            foreach (DataRow drdr in dsTimesheetDetails.Tables[3].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='C' AND TIMESHEET_DEPT_NAME='Drawing -Cumulative'"))
                            {
                                chkDrCumulative = true;
                                if (drdr["TIME_SPENT -Cumulative"] != DBNull.Value)
                                    drawingTimeSpentCumulative = Convert.ToString(drdr["TIME_SPENT -Cumulative"]);
                                else drawingTimeSpentCumulative = string.Empty;
                            }

                            foreach (DataRow drei in dsTimesheetDetails.Tables[3].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='C' AND TIMESHEET_DEPT_NAME='E&I -Cumulative'"))
                            {
                                chkEICumulative = true;
                                if (drei["TIME_SPENT -Cumulative"] != DBNull.Value)
                                    eAndITimeSpentCumulative = Convert.ToString(drei["TIME_SPENT -Cumulative"]);
                                else eAndITimeSpentCumulative = string.Empty;
                            }

                            foreach (DataRow drpr in dsTimesheetDetails.Tables[3].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='C' AND TIMESHEET_DEPT_NAME='Project -Cumulative'"))
                            {
                                chkPrCumulative = true;
                                if (drpr["TIME_SPENT -Cumulative"] != DBNull.Value)
                                    projectTimeSpentCumulative = Convert.ToString(drpr["TIME_SPENT -Cumulative"]);
                                else projectTimeSpentCumulative = string.Empty;
                            }

                            foreach (DataRow drdo in dsTimesheetDetails.Tables[3].Select("JOB_NUMBER='" + Convert.ToString(drTemp["JOB_NUMBER"]) + "' AND TYPE='C' AND TIMESHEET_DEPT_NAME='DOC -Cumulative'"))
                            {
                                chkDoCumulative = true;
                                if (drdo["TIME_SPENT -Cumulative"] != DBNull.Value)
                                    docTimeSpentCumulative = Convert.ToString(drdo["TIME_SPENT -Cumulative"]);
                                else docTimeSpentCumulative = string.Empty;
                            }

                            if (chkDrCumulative == false)
                                drawingTimeSpentCumulative = string.Empty;

                            if (chkEICumulative == false)
                                eAndITimeSpentCumulative = string.Empty;

                            if (chkPrCumulative == false)
                                projectTimeSpentCumulative = string.Empty;

                            if (chkDoCumulative == false)
                                docTimeSpentCumulative = string.Empty;


                            updateQueryCumulative = "update tblTempengghoursAllDepts " +
                                           "set [Drawing -Cumulative]='" + drawingTimeSpentCumulative + "' ," +
                                               "[E&I -Cumulative] = '" + eAndITimeSpentCumulative + "' ," +
                                               "[Project -Cumulative] = '" + projectTimeSpentCumulative + "' ," +
                                               "[DOC -Cumulative] = '" + docTimeSpentCumulative + "' " +
                                           "where JOB_NUMBER='" + JOBNo + "' AND " +
                                           "[TYPE -Cumulative]='C'" + Environment.NewLine;



                            updateQuery = (updateQueryMtd + updateQueryCumulative);
                            updateQueryFinal += updateQuery;

                        }


                        int updateValue = objReports.UpdateTempTable(updateQueryFinal);
                        if (updateValue > 0)
                        {
                            dsTempHoursList = objReports.GetTimehsheetAllDeptsTempTableDetails();

                            if (dsTempHoursList.Tables.Count > 0 && dsTempHoursList.Tables[0].Rows.Count > 0)
                            {
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
