using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TIMESHEET_TimesheetListNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsTimesheetList = new DataSet();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string jobNo = string.Empty;

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

    protected void gvTimesheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTimesheetList.PageIndex = e.NewPageIndex;
        GetTimesheetList();
    }

    protected void btnAddNewTimeSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TIMESHEET/AddUpdateTimesheetNew.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTimesheetList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["TIMESHEET_LIST"];
            ToCSVNew01(ds.Tables[0]);
        }
    }

    #endregion


    #region METHODS[=======================]

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

            dsTimesheetList = objProject.GetTimesheetList(startDate, endDate, jobNo, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (dsTimesheetList.Tables.Count > 0 && dsTimesheetList.Tables[0].Rows.Count > 0)
            {
                Session["TIMESHEET_LIST"] = dsTimesheetList;
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
}
