using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

public partial class PROJECT_MGMT_MonthlyInput : System.Web.UI.Page
{

    #region VARIABLES[==========================]
    
    BAL.Project objProject = new BAL.Project();

    DataSet dsJobNo = new DataSet();
    string jobNo = string.Empty;
    string month = string.Empty;
    double vpoc = 0;
    double icpoc = 0;
    double material = 0;
    double engineering = 0;
    double travelling = 0;
    double otherCost = 0;
    double warranty = 0;
    double commission = 0;
    double royalty = 0;
    double lateDelivery = 0;
    string remarks = string.Empty;

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                //Session["jobnoandrevisionno"] = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");

                hdMonth.Value = DateTime.Now.ToString("MM/yyyy");
                txtMonth.Text = Convert.ToString(hdMonth.Value);

                ddlJOBNo.Items.Clear();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
                BindJobNo();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertProjectMonthlyInput();
    }
    
    protected void btnMonthlyInputList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/ProjectMonthlyInputReport.aspx");
    }

    #endregion


    #region METHODS[============================]

    private void BindJobNo()
    {
        try
        {

            dsJobNo = objProject.GetDetailsBySP("sp_get_job_no_and_revision_no");
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                ddlJOBNo.DataSource = dsJobNo.Tables[0];
                ddlJOBNo.DataTextField = "JOB_NO";
                ddlJOBNo.DataValueField = "JOB_NO";
                ddlJOBNo.DataBind();
                ddlJOBNo.Items.Insert(0, "SELECT");
                ddlJOBNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertProjectMonthlyInput()
    {
        try
        {
            if (ddlJOBNo.SelectedIndex > 0)
                jobNo = Convert.ToString(ddlJOBNo.SelectedValue);
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(hdMonth.Value))
                month = Convert.ToDateTime(hdMonth.Value).ToString("yyyy-MM");
            else
                month = string.Empty;

            if (!string.IsNullOrEmpty(txtVPOC.Text))
                vpoc = Convert.ToDouble(txtVPOC.Text);
            else
                vpoc = 0;

            if (!string.IsNullOrEmpty(txtICPOC.Text))
                icpoc = Convert.ToDouble(txtICPOC.Text);
            else
                icpoc = 0;

            if (!string.IsNullOrEmpty(txtMaterial.Text))
                material = Convert.ToDouble(txtMaterial.Text);
            else
                material = 0;

            if (!string.IsNullOrEmpty(txtEngineering.Text))
                engineering = Convert.ToDouble(txtEngineering.Text);
            else
                engineering = 0;

            if (!string.IsNullOrEmpty(txtTravelling.Text))
                travelling = Convert.ToDouble(txtTravelling.Text);
            else
                travelling = 0;

            if (!string.IsNullOrEmpty(txtOtherCost.Text))
                otherCost = Convert.ToDouble(txtOtherCost.Text);
            else
                otherCost = 0;

            if (!string.IsNullOrEmpty(txtWarranty.Text))
                warranty = Convert.ToDouble(txtWarranty.Text);
            else
                warranty = 0;

            if (!string.IsNullOrEmpty(txtCommission.Text))
                commission = Convert.ToDouble(txtCommission.Text);
            else
                commission = 0;

            if (!string.IsNullOrEmpty(txtRoyalty.Text))
                royalty = Convert.ToDouble(txtRoyalty.Text);
            else
                royalty = 0;

            if (!string.IsNullOrEmpty(txtLateDelivery.Text))
                lateDelivery = Convert.ToDouble(txtLateDelivery.Text);
            else
                lateDelivery = 0;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = Convert.ToString(txtRemarks.Text);
            else
                remarks = string.Empty;



            int value = objProject.InsertProjectMonthlyInput(0, jobNo, month, vpoc, icpoc, material, engineering, travelling, otherCost,
                                                                warranty, commission, royalty, lateDelivery,remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Saved successfully...");
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
        ddlJOBNo.SelectedIndex = 0;
        hdMonth.Value = DateTime.Now.ToString("MM/yyyy");
        txtMonth.Text = Convert.ToString(hdMonth.Value);
        txtVPOC.Text = string.Empty;
        txtICPOC.Text = string.Empty;
        txtMaterial.Text = string.Empty;
        txtEngineering.Text = string.Empty;
        txtTravelling.Text = string.Empty;
        txtOtherCost.Text = string.Empty;
        txtWarranty.Text = string.Empty;
        txtCommission.Text = string.Empty;
        txtRoyalty.Text = string.Empty;
        txtLateDelivery.Text = string.Empty;
        txtRemarks.Text = string.Empty;
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