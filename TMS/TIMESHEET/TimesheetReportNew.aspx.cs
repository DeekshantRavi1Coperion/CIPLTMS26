using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TIMESHEET_TimesheetReportNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    BAL.Reports objReports = new BAL.Reports();
    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();



    DataSet dsEmployee = new DataSet();
    DataSet dsTimesheetList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;
    string status = string.Empty;
    int employeeID = 0;
    string teamMemberID = string.Empty;
    string selectedTeamMemberID = string.Empty;

    DataSet dsTimesheetListTemp = new DataSet();
    DataTable dtTimesheetListTemp = new DataTable();
    string employeeNames = string.Empty;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {


                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                Session["TIMESHEET_LIST"] = null;
                Session["DT_TIMESHEET_LIST_TEMP"] = null;
                BindEmployee();
                GetTimesheetList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetTimesheetList();
        //GetSelectedData();
    }

    protected void gvTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTimesheetList.PageIndex = e.NewPageIndex;
        GetTimesheetList();
        //GetSelectedData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTimesheetList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["DT_TIMESHEET_LIST_TEMP"];
            ToCSVNew01(dt);
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

    #endregion


    #region METHODS[=======================]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objReports.GetSubordinates(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                chklbEmployee.DataSource = dsEmployee.Tables[0];
                chklbEmployee.DataTextField = "EMPLOYEE_NAME";
                chklbEmployee.DataValueField = "EMP_RECORD_ID";
                chklbEmployee.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetSelectedData()
    {
        if (Session["TIMESHEET_LIST"] != null)
        {
            dsTimesheetListTemp = (DataSet)Session["TIMESHEET_LIST"];
            for (int i = 0; i < chklbEmployee.Items.Count; i++)
            {
                if (chklbEmployee.Items[i].Selected)
                    employeeNames += "'" + chklbEmployee.Items[i].Text + "',";
            }
            employeeNames = employeeNames.TrimEnd(',');
            if (employeeNames != string.Empty)
            {
                DataRow[] result = dsTimesheetListTemp.Tables[0].Select("EMPLOYEE_NAME IN(" + employeeNames + ")");
                if (result != null && result.Count() > 0)
                {
                    dtTimesheetListTemp = result.CopyToDataTable();
                    Session["DT_TIMESHEET_LIST_TEMP"] = dtTimesheetListTemp;
                    gvTimesheetList.DataSource = dtTimesheetListTemp;
                    gvTimesheetList.DataBind();
                }
                else
                {
                    Session["DT_TIMESHEET_LIST_TEMP"] = null;
                    gvTimesheetList.DataSource = null;
                    gvTimesheetList.DataBind();
                }
                lblRecords.Text = "Records[" + dtTimesheetListTemp.Rows.Count + "]";
            }
            else
            {
                GetTimesheetList();
            }
        }
        else
        {
            GetTimesheetList();
        }
    }

    private void GetTimesheetList()
    {
        try
        {
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



            //if (Session["TEAMMEMBERS"] != null)
            //{
            //    dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
            //    foreach (DataRow dr in dtTeamMembers.Rows)
            //    {
            //        teamMemberID += "," + Convert.ToString(dr["EMP_RECORD_ID"]);
            //    }
            //    teamMemberID = Convert.ToString(Session["EMP_RECORD_ID"]) + "," + teamMemberID.TrimStart(',');
            //}
            //else            
            //    teamMemberID = Convert.ToString(Session["EMP_RECORD_ID"]);            

            
            for (int i = 0; i < chklbEmployee.Items.Count; i++)
            {
                if (chklbEmployee.Items[i].Selected)               
                    selectedTeamMemberID += chklbEmployee.Items[i].Value + ",";                
            }

            selectedTeamMemberID = selectedTeamMemberID.TrimEnd(',');

            if (!string.IsNullOrEmpty(selectedTeamMemberID))
                teamMemberID = selectedTeamMemberID;

            employeeID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTimesheetList = objProject.GetTimesheetReport(startDate, endDate, jobNo, status, employeeID, teamMemberID);
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                Session["DT_TIMESHEET_LIST_TEMP"] = dsTimesheetList.Tables[0];
                Session["TIMESHEET_LIST"] = dsTimesheetList;
                gvTimesheetList.DataSource = dsTimesheetList.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
                Session["DT_TIMESHEET_LIST_TEMP"] = null;
                Session["TIMESHEET_LIST"] = null;
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

    private void ToCSVNew01(DataTable dt)
    {
        try
        {
            string csv = string.Empty;
            if (dt.Rows.Count > 0)
            {


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

                string fileName = "Timesheet_List_" + DateTime.Now.ToString("dd_MMM_yyyy");
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
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

    #endregion


}
