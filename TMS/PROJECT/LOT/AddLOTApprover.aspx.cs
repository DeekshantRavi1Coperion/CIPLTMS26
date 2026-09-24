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

public partial class PROJECT_LOT_AddLOTApprover : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsEmployee = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsJobNo = new DataSet();

    string dbName = string.Empty;

    int jobUnitID = 0;
    int companyID = 0;
    string jobNo = string.Empty;
    string custCode = string.Empty;
    string poNo = string.Empty;

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
                Session["DB_DETAILS"] = objCommon.GetDBDetails();
                BindCompany();
                BindLOTApprover();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    // JOB DETAILS-------
    protected void btnGetJOBNo_Click(object sender, EventArgs e)
    {
        modalPopupExtenderJOBDetail.Show();
        GetJOBDetail();
    }

    protected void btnSearchJOBNo_Click(object sender, EventArgs e)
    {
        modalPopupExtenderJOBDetail.Show();
        GetJOBDetail();
    }

    protected void gvJOBDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblJOBNo = gvJOBDetail.Rows[rowindex].FindControl("lblJOBNo") as Label;

                txtJOBNo.Text = Convert.ToString(lblJOBNo.Text).Trim();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    //--------------------


    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewLOTApprover();
    }

    protected void btnApproverList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTApproverList.aspx");
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
                //ddlCompany.Items.Insert(0, "Select");

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

    private void GetJOBDetail()
    {
        try
        {
            companyID = 0;
            custCode = string.Empty;
            jobNo = string.Empty;
            poNo = string.Empty;


            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                custCode = txtCustomerCodeSearch.Text;
            else
                custCode = string.Empty;


            if (!string.IsNullOrEmpty(txtJOBNoSearch.Text))
                jobNo = txtJOBNoSearch.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPONoSearch.Text))
                poNo = txtPONoSearch.Text;
            else
                poNo = string.Empty;

            dsJobNo = objProject.GetJOBDetailsForLOT(companyID, custCode, jobNo, poNo);
            if (dsJobNo.Tables.Count > 0 && dsJobNo.Tables[0].Rows.Count > 0)
            {
                lblJOBMsg.Visible = false;
                lblJOBMsg.Text = string.Empty;
                gvJOBDetail.DataSource = dsJobNo.Tables[0];
                gvJOBDetail.DataBind();
                Session["PE_DETAILS"] = dsJobNo.Tables[1];
                Session["PM_DETAILS"] = dsJobNo.Tables[2];

            }
            else
            {
                lblJOBMsg.Visible = true;
                lblJOBMsg.Text = "No data found!";
                gvJOBDetail.DataSource = null;
                gvJOBDetail.DataBind();
                Session["PE_DETAILS"] = null;
                Session["PM_DETAILS"] = null;
            }
            lblJOBRecords.Text = "Records[" + gvJOBDetail.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
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
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Approvers already exists...!!!");
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
            txtJOBNo.Text = string.Empty;
            gvJOBDetail.DataSource = null;
            gvJOBDetail.DataBind();

            ddlProjectManager.SelectedIndex = 0;
            ddlProjectEngineer.SelectedIndex = 0;
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
