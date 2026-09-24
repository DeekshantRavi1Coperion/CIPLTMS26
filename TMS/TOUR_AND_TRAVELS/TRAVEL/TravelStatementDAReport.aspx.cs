using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TOUR_AND_TRAVELS_TRAVEL_TravelStatementDAReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();

    DataSet dsDAReport = new DataSet();
    DataSet dsEmployee = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    int empRecordID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                Session["DA_REPORT"] = null;

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = hdStartDate.Value;

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = hdEndDate.Value;

                BindEmployee();
                GetTravelStatementDAReport();
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
        GetTravelStatementDAReport();
    }

    protected void gvTravelStatementDAReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTravelStatementDAReport.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["DA_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindEmployee()
    {
        try
        {
            if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4 || Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 || Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117 || Convert.ToString(Session["USER_TYPE"]) == "Admin")
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
            else
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "All");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTravelStatementDAReport()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDate.Value)))
                startDate = Convert.ToDateTime(hdStartDate.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDate.Value)))
                endDate = Convert.ToDateTime(hdEndDate.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (ddlEmployee.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlEmployee.SelectedValue);
            else
                empRecordID = 0;


            dsDAReport = objTourAndTravels.GetTravelStatementDAReport(startDate, endDate, empRecordID);
            if (dsDAReport.Tables.Count > 0 && dsDAReport.Tables[0].Rows.Count > 0)
            {
                Session["DA_REPORT"] = dsDAReport;
                gvTravelStatementDAReport.DataSource = dsDAReport.Tables[0];
                gvTravelStatementDAReport.DataBind();
            }
            else
            {
                Session["DA_REPORT"] = null;
                gvTravelStatementDAReport.DataSource = null;
                gvTravelStatementDAReport.DataBind();
            }
            lblRecords.Text = "Records[" + dsDAReport.Tables[0].Rows.Count + "]";
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

            string fileName = "TravelStatementDAReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
