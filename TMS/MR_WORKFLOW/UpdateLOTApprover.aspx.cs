using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Net.Mime;


public partial class MR_WORKFLOW_UpdateLOTApprover : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();
    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();

    int jobUnitID = 0;
    string jobNo = string.Empty;
    int pmID = 0;
    int peID = 0;

    #endregion


    #region EVENTS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindCompany();
                if (Request.QueryString["jobNo"] != null)
                {
                    txtJOBNo.Text = Convert.ToString(Request.QueryString["jobNo"]);                    
                    ddlCompany.SelectedValue= Convert.ToString(Request.QueryString["unitid"]);
                }
                else
                {
                    txtJOBNo.Text = string.Empty;
                    ddlCompany.SelectedIndex = 0;
                    ddlCompany.Enabled = true;
                    Response.Redirect("~/Login.aspx");
                }

                BindLOTApprover();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewLOTApprover();
    }

    #endregion


    #region METHODS[=========================]

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTApprover()
    {
        try
        {
            dsEmployee = objProject.GetEmployeesToAddApprover();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {

                ddlProjectManager.DataSource = dsEmployee.Tables[0];
                ddlProjectManager.DataTextField = "EMPLOYEE_NAME";
                ddlProjectManager.DataValueField = "EMP_RECORD_ID";
                ddlProjectManager.DataBind();
                ddlProjectManager.Items.Insert(0, "Select");
                ddlProjectManager.SelectedIndex = 0;

                ddlProjectEngineer.DataSource = dsEmployee.Tables[0];
                ddlProjectEngineer.DataTextField = "EMPLOYEE_NAME";
                ddlProjectEngineer.DataValueField = "EMP_RECORD_ID";
                ddlProjectEngineer.DataBind();
                ddlProjectEngineer.Items.Insert(0, "Select");
                ddlProjectEngineer.SelectedIndex = 0;
            }
            else
            {
                ddlProjectManager.DataSource = null;
                ddlProjectManager.Items.Clear();

                ddlProjectEngineer.DataSource = null;
                ddlProjectEngineer.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddNewLOTApprover()
    {
        try
        {
            jobUnitID = 0;
            jobNo = string.Empty;
            pmID = 0;
            peID = 0;

            if (ddlCompany.SelectedIndex > 0)
                jobUnitID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(txtJOBNo.Text)))
                jobNo = Convert.ToString(txtJOBNo.Text);

            if (ddlProjectManager.SelectedIndex > 0)
                pmID = Convert.ToInt32(ddlProjectManager.SelectedValue);

            if (ddlProjectEngineer.SelectedIndex > 0)
                peID = Convert.ToInt32(ddlProjectEngineer.SelectedValue);

            int value = 0;
            value = objProject.AddUpdateLOTApprover(0, jobUnitID, jobNo, pmID, peID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("LOT approvers added successfully..!!");
            }
            else if (value < 0)
            {
                ExceptionMessage("Approvers already existed...!!!");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}