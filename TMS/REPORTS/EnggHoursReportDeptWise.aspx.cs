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

public partial class REPORTS_EnggHoursReportDeptWise : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Reports objReports = new BAL.Reports();
    BAL.Common objCommon = new BAL.Common();
    DataSet dsUserDetails = new DataSet();
    DataSet dsEnggHours = new DataSet();


    string fromDate = string.Empty;
    string toDate = string.Empty;
    int deptID = 0;
    string employeeType = string.Empty;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();
                Session["ENGG_HOURS"] = null;

                hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = hdStartDateSearch.Value;

                hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = hdEndDateSearch.Value;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvEnggHoursReport.DataSource = null;
        gvEnggHoursReport.DataBind();
        lblRecords.Text = "Records[0]";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetEnggHoursReport()
    {
        try
        {
            string jobNoTxt = string.Empty;
            string userNameTxt = string.Empty;
            string columnQuery = string.Empty;
            string createQuery = string.Empty;
            string insertQuery = string.Empty;
            string insertQueryValue = string.Empty;
            string userName = string.Empty;
            string jobNo = string.Empty;
            string timeSpent = string.Empty;
            int totalHours = 0;
            string totalTimeSpent = string.Empty;
            employeeType = string.Empty;


            DataSet dsTempEnggList = new DataSet();

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;


            if (ddlDepartment.SelectedIndex > 0)
                deptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            else
                deptID = 0;

            if (ddlEmployeeType.SelectedIndex > 0)
                employeeType = Convert.ToString(ddlEmployeeType.SelectedValue);
            else
                employeeType = string.Empty;



            dsUserDetails = objReports.GetUserDetailsForEnggHours(fromDate, toDate, deptID, employeeType);

            if (dsUserDetails.Tables.Count > 0)
            {
                if (dsUserDetails.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUserDetails.Tables[1].Rows)
                    {
                        jobNoTxt = Convert.ToString(dr["JOB_NUMBER"]);
                        columnQuery += "[" + jobNoTxt + "] [varchar](70),";
                    }
                }

                createQuery = "IF (SELECT count(name) FROM sysobjects WHERE  name=N'tbltempengghours')>0 begin drop table tbltempengghours end; CREATE TABLE [dbo].[tblTempEnggHours]( [USER_NAME] [varchar](50)," + columnQuery + "[TOTAL] [varchar](70))";

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUserDetails.Tables[0].Rows)
                    {
                        if (dr["USER_NAME"] != DBNull.Value)
                            userNameTxt = "'" + Convert.ToString(dr["USER_NAME"]) + "'";

                        if (dr["TOTAL_TIME_SPENT"] != DBNull.Value)
                            totalTimeSpent = "'" + Convert.ToString(dr["TOTAL_TIME_SPENT"]) + "'";

                        insertQueryValue += "(" + userNameTxt + "," + totalTimeSpent + "),";
                    }
                    insertQuery = "INSERT INTO tbltempengghours(USER_NAME,TOTAL)values" + insertQueryValue.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(insertQuery))
                    insertQuery = insertQuery.TrimEnd(',');

                int count = 0;
                string createInsertQuery = createQuery + ";" + insertQuery;
                int createValue = objReports.CreateTempTable(createInsertQuery);

                string updateQuery = string.Empty;

                if (createValue > 0)
                {
                    dsTempEnggList = objReports.GetTempDetails();
                    if (dsTempEnggList.Tables.Count > 0 && dsTempEnggList.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTemp in dsTempEnggList.Tables[0].Rows)
                        {
                            foreach (DataRow dr in dsUserDetails.Tables[2].Select("USER_NAME='" + drTemp["USER_NAME"] + "'"))
                            {
                                userName = Convert.ToString(dr["USER_NAME"]);
                                jobNo = Convert.ToString(dr["JOB_NUMBER"]);
                                timeSpent = Convert.ToString(dr["TIME_SPENT"]);
                                updateQuery = "update tbltempengghours set [" + jobNo + "]='" + timeSpent + "' where USER_NAME='" + Convert.ToString(drTemp["USER_NAME"]) + "'";
                                int updateValue = objReports.UpdateTempTable(updateQuery);
                                if (updateValue > 0)
                                {
                                    count++;
                                }
                            }
                        }

                        dsTempEnggList = objReports.GetTempDetails();

                        if (dsTempEnggList.Tables.Count > 0 && dsTempEnggList.Tables[0].Rows.Count > 0)
                        {
                            Session["ENGG_HOURS"] = dsTempEnggList;
                            gvEnggHoursReport.DataSource = dsTempEnggList.Tables[0];
                            gvEnggHoursReport.DataBind();
                        }
                        else
                        {
                            Session["ENGG_HOURS"] = null;
                            gvEnggHoursReport.DataSource = null;
                            gvEnggHoursReport.DataBind();
                        }
                        lblRecords.Text = "Records[" + dsTempEnggList.Tables[0].Rows.Count + "]";
                    }
                    else
                    {
                        gvEnggHoursReport.DataSource = null;
                        gvEnggHoursReport.DataBind();
                        ExceptionMessage("Data not found!");
                        ExceptionMessage("Data not found!");
                        return;
                    }
                }
            }
            else
            {
                gvEnggHoursReport.DataSource = null;
                gvEnggHoursReport.DataBind();
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

            string fileName = "EnggHoursDeptWise_" + DateTime.Now.ToString("dd_MMM_yyyy") + "_" + Convert.ToString(ddlDepartment.SelectedItem.Text);
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;        
    }

    #endregion

}
