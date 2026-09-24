using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class REPORTS_EnggHoursReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Reports objReports = new BAL.Reports();

    DataSet dsTimesheetList = new DataSet();
    DataSet dsSubordinate = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;
    int subordinateID = 0;
    int empRecordID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["TIMESHEET_LIST"] = null;

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                hdStartDateSearch.Value = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                txtStartDateSearch.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");

                var endDate = startDate.AddMonths(1).AddDays(-1);
                hdEndDateSearch.Value = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                txtEndDateSearch.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");


                //hdStartDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtStartDateSearch.Text = Convert.ToString(hdStartDateSearch.Value);

                //hdEndDateSearch.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtEndDateSearch.Text = Convert.ToString(hdEndDateSearch.Value);

                BindSubordinate();
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTimesheetList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["TIMESHEET_LIST"];
            ToCSVNew01(dt);
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindSubordinate()
    {
        try
        {
            dsSubordinate = objReports.GetSubordinates(Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsSubordinate.Tables.Count > 0 && dsSubordinate.Tables[0].Rows.Count > 0)
            {
                ddlSubordinate.DataSource = dsSubordinate.Tables[0];
                ddlSubordinate.DataTextField = "EMPLOYEE_NAME";
                ddlSubordinate.DataValueField = "EMP_RECORD_ID";
                ddlSubordinate.DataBind();
                ddlSubordinate.Items.Insert(0, "All");
                ddlSubordinate.SelectedIndex = 0;
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

            jobNo = string.Empty;
            subordinateID = 0;
            empRecordID = 0;
            
            if (chkSelectDates.Checked)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                    startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
                else
                    startDate = string.Empty;

                if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                    endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
                else
                    endDate = string.Empty;
            }
            else
            {
                startDate = string.Empty;
                endDate = string.Empty;
            }


            /////////////////////////////////////////////////////////////////////

            //if (!string.IsNullOrEmpty(txtJobNo.Text))
            //{
            //    string[] jobNoTxt = txtJobNo.Text.Replace(Environment.NewLine, "").TrimEnd(',').Split(',');
            //    foreach (string item in jobNoTxt)
            //    {
            //        if (!string.IsNullOrEmpty(item))
            //        {
            //            //jobNo +=  item ;
            //            jobNo += "'" + item + "',";
            //        }
            //    }
            //    jobNo = jobNo.TrimEnd(',');
            //}
            //else
            //    jobNo = string.Empty;



            //////////////////////////////////////////////////////////////////////
            ///string jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                // Replace newlines with spaces and split by comma
                string[] jobNoTxt = txtJobNo.Text.Replace(Environment.NewLine, " ").Split(',');

                // Use StringBuilder for better performance
                StringBuilder jobNoBuilder = new StringBuilder();

                foreach (string item in jobNoTxt)
                {
                    // Trim spaces around each item and check if not empty
                    string trimmedItem = item.Trim();
                    if (!string.IsNullOrEmpty(trimmedItem))
                    {
                        jobNoBuilder.Append("'").Append(trimmedItem).Append("',");
                    }
                }

                // Remove the trailing comma if there's any item appended
                if (jobNoBuilder.Length > 0)
                {
                    jobNoBuilder.Length--; // This removes the last comma
                }

                jobNo = jobNoBuilder.ToString();
            }
            else
            {
                jobNo = string.Empty;
            }

            // Now jobNo can be passed to the SP as a parameter


            ///////////////////////////////////////////////////////////////////////



            if (ddlSubordinate.SelectedIndex > 0)
                subordinateID = Convert.ToInt32(ddlSubordinate.SelectedValue);
            else subordinateID = 0;

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTimesheetList = objReports.GetEnggHoursReport(startDate, endDate, jobNo, subordinateID, empRecordID);
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                Session["TIMESHEET_LIST"] = dsTimesheetList.Tables[0];
                gvTimesheetList.DataSource = dsTimesheetList.Tables[0];
                gvTimesheetList.DataBind();
            }
            else
            {
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

            string fileName = "Engg_Hours_Report" + DateTime.Now.ToString("dd_MMM_yyyy");
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

    #endregion



}
