using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_AddUpdateProject : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsJobNo = new DataSet();
    DataSet dsStatus = new DataSet();
    DataSet dsCustomer = new DataSet();

    BAL.EnggHours objEnggHours = new BAL.EnggHours();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsDBDetails = new DataSet();

    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    int projectID = 0;
    string jobNo = string.Empty;
    string customerName = string.Empty;
    string custCode = string.Empty;
    string budgetedHours = string.Empty;
    int statusID = 0;
    string remarks = string.Empty;

    DataSet dsProjectDetail = new DataSet();

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["DB_DETAILS"] = objCommon.GetDBDetails();

                ddlCustomerName.Items.Insert(0, "Select");
                ddlCustomerName.SelectedIndex = 0;
                GetJobNoNew();
                //GetJobNo();
                BindStatus();

                if (Convert.ToInt32(Request.QueryString["projectid"]) > 0)
                {
                    dsProjectDetail = objEnggHours.GetProjectDetail(Convert.ToInt32(Request.QueryString["projectid"]));
                    if (dsProjectDetail.Tables.Count > 0 && dsProjectDetail.Tables[0].Rows.Count > 0)
                    {

                        projectID = Convert.ToInt32(dsProjectDetail.Tables[0].Rows[0]["PROJECT_ID"]);
                        ddlJobNo.SelectedValue = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["JOB_NO"]);
                        //GetCustomer();
                        GetCustomerNew();
                        ddlCustomerName.SelectedValue = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["CUST_CODE"]);
                        txtCustomerID.Text = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["CUST_CODE"]);
                        txtBudgetedHours.Text = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["BUDGETED_HOURS"]);
                        ddlStatus.SelectedValue = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["STATUS_ID"]);
                        txtRemarks.Text = Convert.ToString(dsProjectDetail.Tables[0].Rows[0]["REMARKS"]);
                    }
                    else
                    {
                        ExceptionMessage("Please select job no.!");
                        return;
                    }
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCustomerNew();

        //GetCustomer();
    }

    private void GetCustomerNew()
    {
        try
        {
            SuccessMessage("Processing...");
            txtCustomerName.Text = string.Empty;

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

            txtCustomerID.Text = string.Empty;
            if (ddlJobNo.SelectedIndex > 0)
            {
                dsCustomer = objEnggHours.GetCustomer("", dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ);

                foreach (DataRow dr in dsCustomer.Tables[0].Select("JOB_NO='" + Convert.ToString(ddlJobNo.SelectedValue).Trim() + "'"))
                {
                    txtCustomerName.Text = Convert.ToString(dr["NAME"]);
                    txtCustomerID.Text = Convert.ToString(dr["CUSTCODE"]);
                }
            }

            HidePanel();
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomerName.SelectedIndex > 0)
        {
            txtCustomerID.Text = Convert.ToString(ddlCustomerName.SelectedValue).Trim();
        }
        else
        {
            txtCustomerID.Text = string.Empty;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        InsertUpdateProject();
    }

    protected void btnProjectList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/ProjectList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void GetJobNo()
    {
        try
        {
            dsJobNo = objEnggHours.GetJobNo("sp_get_jobno");
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                ddlJobNo.DataSource = dsJobNo.Tables[0];
                ddlJobNo.DataTextField = "JOB_NO";
                ddlJobNo.DataValueField = "JOB_NO";
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, "Select");
                ddlJobNo.SelectedIndex = 0;
            }
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


            txtCustomerID.Text = string.Empty;
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

    private void BindStatus()
    {
        try
        {
            dsStatus = objEnggHours.GetStatus("sp_get_status");
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void GetCustomer()
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

            txtCustomerID.Text = string.Empty;
            ddlCustomerName.Items.Clear();
            if (ddlJobNo.SelectedIndex > 0)
            {
                dsCustomer = objEnggHours.GetCustomer(Convert.ToString(ddlJobNo.SelectedValue).Trim(), dbNameA35, dbNameDLH, dbNameGNU, dbNameSEZ);
                if (dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
                {
                    ddlCustomerName.Enabled = true;
                    ddlCustomerName.DataSource = dsCustomer.Tables[0];
                    ddlCustomerName.DataTextField = "NAME";
                    ddlCustomerName.DataValueField = "CUSTCODE";
                    ddlCustomerName.DataBind();
                    ddlCustomerName.Items.Insert(0, "Select");
                    ddlCustomerName.SelectedIndex = 0;
                }
                else
                {
                    ddlCustomerName.Enabled = false;
                    ddlCustomerName.Items.Insert(0, "Select");
                    ddlCustomerName.SelectedIndex = 0;
                }
            }
            else
            {
                ddlCustomerName.Enabled = false;
                ddlCustomerName.Items.Insert(0, "Select");
                ddlCustomerName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void InsertUpdateProject()
    {
        try
        {
            if (Convert.ToInt32(Request.QueryString["projectid"]) > 0)
            {
                projectID = Convert.ToInt32(Request.QueryString["projectid"]);
            }
            else
            {
                projectID = 0;
            }
            jobNo = Convert.ToString(ddlJobNo.SelectedValue).Trim();
            //customerName = Convert.ToString(ddlCustomerName.SelectedItem.Text).Trim();
            customerName = txtCustomerName.Text;
            custCode = txtCustomerID.Text;
            budgetedHours = txtBudgetedHours.Text;
            statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            remarks = txtRemarks.Text;

            int value = objEnggHours.InsertUpdateProject(projectID, jobNo, customerName, custCode, budgetedHours, statusID, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                if (projectID > 0)
                {
                    SuccessMessage("Project updated successfully");
                }
                else
                {
                    SuccessMessage("Project added successfully");
                }

                Reset();
            }
            else
            {
                ExceptionMessage("Please try again!");
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
            ddlJobNo.SelectedIndex = 0;
            ddlCustomerName.SelectedIndex = 0;
            ddlCustomerName.Enabled = false;
            txtCustomerName.Text = string.Empty;
            txtCustomerID.Text = string.Empty;
            txtBudgetedHours.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
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
