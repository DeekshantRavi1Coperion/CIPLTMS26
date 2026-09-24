using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_ProjectListMonthWise : System.Web.UI.Page
{

    #region VARIABLES[======================]

    BAL.EnggHours objEnggHours = new BAL.EnggHours();
    DataSet dsProjectList = new DataSet();

    BAL.Common objCommon = new BAL.Common();

    DataSet dsDBDetails = new DataSet();

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    DataSet dsCustomer = new DataSet();
    string jobNo = string.Empty;
    string customerName = string.Empty;

    #endregion


    #region EVENTS[=========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                GetJobNoNew();

                txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                GetProjectList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void imgbtnStartDate_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlStartDate.Visible == false)
        {
            divStartDate.Visible = true;
            pnlStartDate.Visible = true;
        }
        else
        {
            divStartDate.Visible = false;
            pnlStartDate.Visible = false;
        }
    }

    protected void imgbtnEndDate_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlEndDate.Visible == false)
        {
            divEndDate.Visible = true;
            pnlEndDate.Visible = true;
        }
        else
        {
            divEndDate.Visible = false;
            pnlEndDate.Visible = false;
        }
    }

    protected void calendarStartDate_SelectionChanged(object sender, EventArgs e)
    {
        txtStartDate.Text = calendarStartDate.SelectedDate.ToString("dd-MMM-yyyy");
        pnlStartDate.Visible = false;

        if (Convert.ToDateTime(txtStartDate.Text).Date > Convert.ToDateTime(txtEndDate.Text).Date)
        {
            pnlMsg.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Start date must be smaller or equal to end date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            pnlMsg.Visible = false;
            lblMsg.Visible = false;
            lblMsg.Text = string.Empty;
        }
    }

    protected void calendarEndDate_SelectionChanged(object sender, EventArgs e)
    {
        txtEndDate.Text = calendarEndDate.SelectedDate.ToString("dd-MMM-yyyy");
        pnlEndDate.Visible = false;

        if (Convert.ToDateTime(txtStartDate.Text).Date > Convert.ToDateTime(txtEndDate.Text).Date)
        {
            pnlMsg.Visible = true;
            lblMsg.Visible = true;
            lblMsg.Text = "Start date must be smaller or equal to end date.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            pnlMsg.Visible = false;
            lblMsg.Visible = false;
            lblMsg.Text = string.Empty;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetProjectList();
    }

    protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCustomerNew();
    }

    protected void gvProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProjectList.PageIndex = e.NewPageIndex;
        gvProjectList.DataBind();
    }

    #endregion


    #region METHODS[========================]

    private void GetProjectList()
    {
        try
        {
            string startDate = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
            string endDate = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");

            //if (!string.IsNullOrEmpty(txtJobNo.Text))
            //    jobNo = txtJobNo.Text;
            //else
            //    jobNo = string.Empty;

            if (ddlJobNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJobNo.SelectedValue);
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            dsProjectList = objEnggHours.GetProiectListMonthWise(jobNo, customerName, startDate, endDate);
            if (dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                gvProjectList.DataSource = dsProjectList.Tables[0];
                gvProjectList.DataBind();
            }
            else
            {
                gvProjectList.DataSource = null;
                gvProjectList.DataBind();
            }
            lblRecords.Text = "Records[" + dsProjectList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void GetJobNoNew()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }
            else
            {
                ExceptionMessage("No data found...!");
                return;
            }

            txtCustomerName.Text = string.Empty;
            ddlJobNo.Items.Clear();

            dsCustomer = objEnggHours.GetCustomer("", dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ);
            if (dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
            {
                ddlJobNo.DataSource = dsCustomer.Tables[0];
                ddlJobNo.DataTextField = "JOB_NO";
                ddlJobNo.DataValueField = "JOB_NO";
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, "Select");
                ddlJobNo.SelectedIndex = 0;
            }
            else
            {
                ddlJobNo.Items.Insert(0, "Select");
                ddlJobNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCustomerNew()
    {
        try
        {
            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }
            else
            {
                ExceptionMessage("No data found...!");
                return;
            }

            txtCustomerName.Text = string.Empty;
            if (ddlJobNo.SelectedIndex > 0)
            {
                dsCustomer = objEnggHours.GetCustomer("", dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ);

                foreach (DataRow dr in dsCustomer.Tables[0].Select("JOB_NO='" + Convert.ToString(ddlJobNo.SelectedValue).Trim() + "'"))
                {
                    txtCustomerName.Text = Convert.ToString(dr["NAME"]);
                    if (dr["START_DATE"] != DBNull.Value)
                        txtStartDate.Text = Convert.ToDateTime(dr["START_DATE"]).ToString("dd-MMM-yyyy");
                    else
                        txtStartDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                    if (dr["END_DATE"] != DBNull.Value)
                        txtEndDate.Text = Convert.ToDateTime(dr["END_DATE"]).ToString("dd-MMM-yyyy");
                    else
                        txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
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
