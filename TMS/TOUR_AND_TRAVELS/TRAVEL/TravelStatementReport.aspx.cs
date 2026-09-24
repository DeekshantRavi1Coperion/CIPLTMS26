using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TOUR_AND_TRAVELS_TRAVEL_TravelStatementReport : System.Web.UI.Page
{
    #region VARIABLES[=====================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataSet dsTourStatus = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsTravelStatemetList = new DataSet();
    DataTable dtTeamMembers = new DataTable();
    string startDate = string.Empty;
    string endDate = string.Empty;
    string sanctionNo = string.Empty;
    int statusID = 0;
    int empRecordID = 0;
    int teamMemberID = 0;
    string teamMembers = string.Empty;
    int departmentID = 0;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                Session["TRAVEL_STATEMENT_REPORT"] = null;
                BindStatus();
                BindEmployee();
                //GetTravelStatementReport();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetTravelStatementReport();
    }

    protected void gvTourList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTourList.PageIndex = e.NewPageIndex;
        GetTravelStatementReport();
    }

    protected void gvTourList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                if (Convert.ToString(lblStatus.Text) == "New")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
                else if (Convert.ToString(lblStatus.Text) == "Approved")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Checked")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSeaGreen;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Amendment")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Amended")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Passed")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSkyBlue;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Settled")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                else if (Convert.ToString(lblStatus.Text) == "Inoice-Booked")
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.LightBlue;
                    }
                }

                //else if (Convert.ToString(lblStatus.Text) == "Cancelled")
                //{
                //    for (int i = 0; i < e.Row.Cells.Count; i++)
                //    {
                //        e.Row.Cells[i].BackColor = System.Drawing.Color.LightSalmon;
                //    }
                //}

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
        if (gvTourList.Rows.Count > 0)
        {
            DataSet ds = (DataSet)Session["TRAVEL_STATEMENT_REPORT"];
            ToCSVNew01(ds.Tables[0]);
        }
        else
        {
            SuccessMessage("No data found!");
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindStatus()
    {
        try
        {

            dsTourStatus = objTourAndTravels.GetTravelStatementStatus();
            if (dsTourStatus.Tables.Count > 0 && dsTourStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsTourStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "All");
                ddlStatus.SelectedIndex = 0;
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

    private void GetTravelStatementReport()
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

            if (!string.IsNullOrEmpty(txtSanctionNo.Text))
                sanctionNo = txtSanctionNo.Text.Trim();
            else
                sanctionNo = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;


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

            dsTravelStatemetList = objTourAndTravels.GetTravelStatementReport(startDate, endDate, sanctionNo, statusID, empRecordID, teamMemberID, teamMembers);
            if (dsTravelStatemetList.Tables.Count > 0 && dsTravelStatemetList.Tables[0].Rows.Count > 0)
            {
                Session["TRAVEL_STATEMENT_REPORT"] = dsTravelStatemetList;
                gvTourList.DataSource = dsTravelStatemetList.Tables[0];
                gvTourList.DataBind();
            }
            else
            {
                Session["TRAVEL_STATEMENT_REPORT"] = null;
                gvTourList.DataSource = null;
                gvTourList.DataBind();
            }
            lblRecords.Text = "Records[" + dsTravelStatemetList.Tables[0].Rows.Count + "]";
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

            string fileName = "TravelStatementReport_" + DateTime.Now.ToString("dd_MMM_yyyy");
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
