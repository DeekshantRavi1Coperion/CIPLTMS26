using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_AddEmailTemplate : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsItemCategory = new DataSet();

    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsEntityType = new DataSet();
    DataSet dsStatus = new DataSet();

    int employeeRecordID = 0;
    string employeeName = string.Empty;
    string employeeID = string.Empty;
    string designation = string.Empty;
    int departmentID = 0;
    int timesheetDeptID = 0;
    string emailID = string.Empty;
    int unitID = 0;
    int teamLeaderID = 0;
    string userName = string.Empty;
    string password = string.Empty;
    string category = string.Empty;
    string userType = string.Empty;
    string timesheetEmpType = string.Empty;
    int bankID = 0;
    string IFSC = string.Empty;
    string accountNo = string.Empty;

    string fileUploadPassportCopyFileName = string.Empty;

    #endregion


    #region EVNETS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindEntityTypes();
                BindStatus();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddEmailTemplate();
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/EmailTemplatesList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void BindEntityTypes()
    {
        try
        {
            dsEntityType = objVCM.GetEntityTypes();
            if (dsEntityType.Tables.Count > 0 && dsEntityType.Tables[0].Rows.Count > 0)
            {
                ddlEntityTypeToS.DataSource = dsEntityType.Tables[0];
                ddlEntityTypeToS.DataTextField = "NAME";
                ddlEntityTypeToS.DataValueField = "PID";
                ddlEntityTypeToS.DataBind();
                ddlEntityTypeToS.Items.Insert(0, "Select");
                ddlEntityTypeToS.SelectedIndex = 0;
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
            dsStatus = objVCM.GetStatus();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatusToS.DataSource = dsStatus.Tables[0];
                ddlStatusToS.DataTextField = "NAME";
                ddlStatusToS.DataValueField = "PID";
                ddlStatusToS.DataBind();
                ddlStatusToS.Items.Insert(0, "Select");
                ddlStatusToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddEmailTemplate()
    {
        try
        {
            int entityTypeId = 0;
            int statusId = 0;
            string subject = string.Empty;
            string salutation = string.Empty;
            string bodyLine = string.Empty;
            string closingLine = string.Empty;
            string link = string.Empty;
            string cc = string.Empty;
            string bcc = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);


            if (ddlEntityTypeToS.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityTypeToS.SelectedValue);
            if (ddlStatusToS.SelectedIndex > 0) statusId = Convert.ToInt32(ddlStatusToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtSubjectToS.Text)) subject = txtSubjectToS.Text;
            if (!string.IsNullOrEmpty(txtSalutationToS.Text)) salutation = txtSalutationToS.Text;
            if (!string.IsNullOrEmpty(txtBodyLineToS.Text)) bodyLine = txtBodyLineToS.Text;
            if (!string.IsNullOrEmpty(txtClosingLineToS.Text)) closingLine = txtClosingLineToS.Text;
            if (!string.IsNullOrEmpty(txtLinkToS.Text)) link = txtLinkToS.Text;
            if (!string.IsNullOrEmpty(txtCCToS.Text)) cc = txtCCToS.Text;
            if (!string.IsNullOrEmpty(txtBCCToS.Text)) bcc = txtBCCToS.Text;

            int value = objVCM.AddUpdateEmailTemplates(0, entityTypeId, statusId, subject, bodyLine, salutation, closingLine, link, cc, bcc, createdBy);
            if (value > 0)
            {
                SuccessMessage("Saved successfully");
                Reset();
            }
            else if (value < 0)
            {
                ExceptionMessage("Email template already exist for select status...!!");
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!");
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
        ddlEntityTypeToS.SelectedIndex = 0;
        ddlStatusToS.SelectedIndex = 0;
        txtSubjectToS.Text = string.Empty;
        txtSalutationToS.Text = string.Empty;
        txtBodyLineToS.Text = string.Empty;
        txtClosingLineToS.Text = string.Empty;
        txtLinkToS.Text = string.Empty;
        txtCCToS.Text = string.Empty;
        txtBCCToS.Text = string.Empty;
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
