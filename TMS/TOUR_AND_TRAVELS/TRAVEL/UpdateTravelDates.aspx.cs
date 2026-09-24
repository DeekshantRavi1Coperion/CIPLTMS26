using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TOUR_AND_TRAVELS_TRAVEL_UpdateTravelDates : System.Web.UI.Page
{

    #region VARIABLES[==================]

    BAL.TourAndTravels objTourAndTravels = new BAL.TourAndTravels();
    DataTable dtTourSanctionDetails = new DataTable();
    DataSet dsSanctionNo = new DataSet();

    int tourID = 0;
    string tourSanctionNo = string.Empty;
    string startDate = string.Empty;
    string endDate = string.Empty;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[=====================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                hdConfirmValue.Value = "0";

                hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStartDate.Text = Convert.ToString(hdStartDate.Value);

                hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToString(hdEndDate.Value);

                Session["SANCTION_NO_DETAILS"] = null;
                BindSanctionNo();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }

    protected void ddlSanctionNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTourSantionDetails();
    }

    protected void btnTourList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TOUR_AND_TRAVELS/TRAVEL/TravelStatementList.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdConfirmValue.Value) > 0)
        {
            UpdateTravelDates();
        }
    }

    #endregion


    #region METHODS[====================]

    private void BindSanctionNo()
    {
        try
        {
            string teamMemberID = string.Empty;
            DataTable dtTeamMembers = new DataTable();
            if (Session["TEAMMEMBERS"] != null)
            {
                dtTeamMembers = (DataTable)Session["TEAMMEMBERS"];
                foreach (DataRow dr in dtTeamMembers.Rows)
                {
                    teamMemberID += "," + Convert.ToString(dr["EMP_RECORD_ID"]);
                }
                teamMemberID = Convert.ToString(Session["EMP_RECORD_ID"]) + "," + teamMemberID.TrimStart(',');
            }
            else
            {
                teamMemberID = Convert.ToString(Session["EMP_RECORD_ID"]);
            }

            dsSanctionNo = objTourAndTravels.GetTourSanctionDetails(teamMemberID);

            if (dsSanctionNo.Tables.Count > 0 && dsSanctionNo.Tables[0].Rows.Count > 0)
            {
                Session["SANCTION_NO_DETAILS"] = dsSanctionNo.Tables[0];

                ddlSanctionNo.DataSource = dsSanctionNo.Tables[0];
                ddlSanctionNo.DataTextField = "TOUR_SANCTION_NO";
                ddlSanctionNo.DataValueField = "TOUR_ID";
                ddlSanctionNo.DataBind();
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }
            else
            {
                Session["SANCTION_NO_DETAILS"] = null;
                ddlSanctionNo.Items.Insert(0, "Select");
                ddlSanctionNo.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTourSantionDetails()
    {
        try
        {
            pnlExceptionMsg.Visible = false;
            pnlSuccessMsg.Visible = false;
            if (ddlSanctionNo.SelectedIndex > 0)
            {
                dtTourSanctionDetails = (DataTable)Session["SANCTION_NO_DETAILS"];
                if (dtTourSanctionDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTourSanctionDetails.Select("TOUR_SANCTION_NO='" + Convert.ToString(ddlSanctionNo.SelectedItem.Text) + "'"))
                    {
                        hdTourID.Value = Convert.ToString(dr["TOUR_ID"]);
                        txtTourNo.Text = Convert.ToString(dr["TOUR_NO"]);
                        txtCustVendName.Text = Convert.ToString(dr["CUST_VEND_NAME"]);
                        txtPlaceOfVisit.Text = Convert.ToString(dr["PLACE_OF_VISIT"]);

                        hdStartDate.Value = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");
                        txtStartDate.Text = Convert.ToString(hdStartDate.Value);

                        hdEndDate.Value = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");
                        txtEndDate.Text = Convert.ToString(hdEndDate.Value);

                        //txtDays.Text = (Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartDate.Text)).Days.ToString(); 
                    }
                }
            }
            else
            {
                Reset();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateTravelDates()
    {
        try
        {
            tourID = Convert.ToInt32(hdTourID.Value);
            tourSanctionNo = Convert.ToString(ddlSanctionNo.SelectedItem.Text);
            startDate = Convert.ToString(hdStartDate.Value);
            endDate = Convert.ToString(hdEndDate.Value);
            remarks = string.Empty;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;

            int value = objTourAndTravels.UpdateTravelDates(tourID, tourSanctionNo, startDate, endDate, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                BindSanctionNo();
                Reset();
                SuccessMessage("Dates updated successfully.");
                return;
            }
            else
            {
                ExceptionMessage("Please try again.");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        try
        {
            ddlSanctionNo.SelectedIndex = 0;
            txtTourNo.Text = string.Empty;
            txtCustVendName.Text = string.Empty;
            txtPlaceOfVisit.Text = string.Empty;

            hdStartDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtStartDate.Text = Convert.ToString(hdStartDate.Value);

            hdEndDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtEndDate.Text = Convert.ToString(hdEndDate.Value);
            txtRemarks.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SuccessMessage(string message)
    {
        pnlSuccessMsg.Visible = true;
        lblSuccessMsg.Text = message;
        lblSuccessMsg.ForeColor = System.Drawing.Color.Green;
    }

    private void ExceptionMessage(string message)
    {
        pnlExceptionMsg.Visible = true;
        lblExceptionMsg.Text = message;
        lblExceptionMsg.ForeColor = System.Drawing.Color.Red;
    }

    #endregion

}
