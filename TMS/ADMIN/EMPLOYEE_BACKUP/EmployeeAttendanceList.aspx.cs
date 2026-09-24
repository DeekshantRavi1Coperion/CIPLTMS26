using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_EMPLOYEE_EmployeeAttendanceList : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    DataSet dsAttendanceList = new DataSet();

    string fromDate = string.Empty;
    string toDate = string.Empty;
    string name = string.Empty;
    string employeeCode = string.Empty;


    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtAttendanceList"] = null;

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        GetAttendanceList();
    }

    protected void gvAttendanceList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (gvAttendanceList.Rows.Count > 0)
        {
            DataTable dt = (DataTable)Session["dtAttendanceList"];
            ToCSVNew01(dt);
        }
    }

    #endregion


    #region METHODS[=========================]

    private void GetAttendanceList()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                fromDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                fromDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                toDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                toDate = string.Empty;


            if (!string.IsNullOrEmpty(txtName.Text))
                name = Convert.ToString(txtName.Text);
            else
                name = string.Empty;

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
                employeeCode = Convert.ToString(txtEmployeeCode.Text);
            else
                employeeCode = string.Empty;

            dsAttendanceList = objCommon.GetEmployeeAttendanceList(fromDate, toDate, name, employeeCode);

            if (dsAttendanceList.Tables.Count > 0 && dsAttendanceList.Tables[0].Rows.Count > 0)
            {
                Session["dtAttendanceList"] = dsAttendanceList.Tables[0];
                gvAttendanceList.DataSource = dsAttendanceList.Tables[0];
                gvAttendanceList.DataBind();
            }
            else
            {
                Session["dtAttendanceList"] = null;
                gvAttendanceList.DataSource = null;
                gvAttendanceList.DataBind();
            }
            lblRecords.Text = "Records[" + dsAttendanceList.Tables[0].Rows.Count + "]";
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

            string fileName = "SaleOrderReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
