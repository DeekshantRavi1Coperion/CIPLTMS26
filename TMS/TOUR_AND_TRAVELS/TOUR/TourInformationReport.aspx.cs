using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net.Mime;

public partial class TOUR_AND_TRAVELS_TOUR_TourInformationReport : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTourStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTourList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string tourNo = string.Empty;
    int tourStatusID = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                Session["TOUR_INFO_REPORT"] = null;
                BindTourStatus();
                BindEmployee();

                GetTourInformationReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetTourInformationReport();
    }

    protected void gvTourList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTourList.PageIndex = e.NewPageIndex;
        GetTourInformationReport();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvTourList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["TOUR_INFO_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    protected void gvTourList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTourStatus = (Label)e.Row.FindControl("lblTourStatus");

                if (Convert.ToString(lblTourStatus.Text) == "New")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }

                else if (Convert.ToString(lblTourStatus.Text) == "Deleted")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Salmon;
                    }
                }

                else if (Convert.ToString(lblTourStatus.Text) == "HODApproved")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                else if (Convert.ToString(lblTourStatus.Text) == "FinalApproved")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSkyBlue;
                    }
                }

                else if (Convert.ToString(lblTourStatus.Text) == "Cancelled")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                    }
                }

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

    #endregion


    #region METHODS[=======================]

    private void BindTourStatus()
    {
        try
        {

            dsTourStatus = objTourAndTravels.GetTourStatus();
            if (dsTourStatus.Tables.Count > 0 && dsTourStatus.Tables[0].Rows.Count > 0)
            {
                ddlTourStatus.DataSource = dsTourStatus.Tables[0];
                ddlTourStatus.DataTextField = "TOUR_STATUS_NAME";
                ddlTourStatus.DataValueField = "TOUR_STATUS_ID";
                ddlTourStatus.DataBind();
                ddlTourStatus.Items.Insert(0, "All");
                ddlTourStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmployee()
    {
        try
        {
            if (Convert.ToInt32(Session["DEPARTMENT_ID"]) == 4 || 
                Convert.ToInt32(Session["DEPARTMENT_ID"]) == 2 || 
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 117 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 115 ||
                Convert.ToInt32(Session["EMP_RECORD_ID"]) == 17 ||
                Convert.ToString(Session["USER_TYPE"]) == "Admin")
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(0);
            else
                dsEmployee = objTourAndTravels.GetEmployeeForTravel(Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsEmployee.Tables[0];
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetTourInformationReport()
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

            if (!string.IsNullOrEmpty(txtTourNo.Text))
                tourNo = txtTourNo.Text.Trim();
            else
                tourNo = string.Empty;

            if (ddlTourStatus.SelectedIndex > 0)
                tourStatusID = Convert.ToInt32(ddlTourStatus.SelectedValue);
            else
                tourStatusID = 0;


            if (ddlEmployee.SelectedIndex > 0)
            {
                teamMemberID = Convert.ToInt32(ddlEmployee.SelectedValue);
            }
            else
            {
                teamMemberID = 0;
                if (Session["TEAMMEMBERS"] != null)
                {
                    dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
                    foreach (DataRow dr in dtTeamMembers.Rows)
                    {
                        teamMembers += "," + Convert.ToString(dr["EMP_RECORD_ID"]);
                    }
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]) + "," + teamMembers.TrimStart(',');
                }
                else
                {
                    teamMembers = Convert.ToString(Session["EMP_RECORD_ID"]);
                }
            }

            empRecordID = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsTourList = objTourAndTravels.GetTourInformationReport(startDate, endDate, tourNo, tourStatusID, empRecordID, teamMemberID, teamMembers);
            if (dsTourList.Tables.Count > 0 && dsTourList.Tables[0].Rows.Count > 0)
            {
                Session["TOUR_INFO_REPORT"] = dsTourList;
                gvTourList.DataSource = dsTourList.Tables[0];
                gvTourList.DataBind();
            }
            else
            {
                Session["TOUR_INFO_REPORT"] = null;
                gvTourList.DataSource = null;
                gvTourList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTourList.Tables[0].Rows.Count + "]";
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

            string fileName = "TourInformationReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
