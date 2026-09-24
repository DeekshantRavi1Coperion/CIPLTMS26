using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_AddApprovers : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsEntityType = new DataSet();
    DataSet dsApproverType = new DataSet();
    DataSet dsEmployees = new DataSet();

    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();

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
                BindApproverTypes();
                BindApprovers();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddApprover();
    }

    protected void btnApproversList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/ApproversList.aspx");
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

    private void BindApproverTypes()
    {
        try
        {
            dsApproverType = objVCM.GettApproverTypes();
            if (dsApproverType.Tables.Count > 0 && dsApproverType.Tables[0].Rows.Count > 0)
            {
                ddlApproverTypeToS.DataSource = dsApproverType.Tables[0];
                ddlApproverTypeToS.DataTextField = "NAME";
                ddlApproverTypeToS.DataValueField = "PID";
                ddlApproverTypeToS.DataBind();
                ddlApproverTypeToS.Items.Insert(0, "Select");
                ddlApproverTypeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindApprovers()
    {
        try
        {
            dsEmployees = objCommon.GetTeamLeadersByID(0);
            if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
            {
                ddlApproverToS.DataSource = dsEmployees.Tables[0];
                ddlApproverToS.DataTextField = "EMPLOYEE_NAME";
                ddlApproverToS.DataValueField = "EMP_RECORD_ID";
                ddlApproverToS.DataBind();
                ddlApproverToS.Items.Insert(0, "Select");
                ddlApproverToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddApprover()
    {
        try
        {
            int entityTypeId = 0;
            int approverTypeId = 0;
            int approverId = 0;

            if (ddlEntityTypeToS.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityTypeToS.SelectedValue);
            if (ddlApproverTypeToS.SelectedIndex > 0) approverTypeId = Convert.ToInt32(ddlApproverTypeToS.SelectedValue);
            if (ddlApproverToS.SelectedIndex > 0) approverId = Convert.ToInt32(ddlApproverToS.SelectedValue);

            int value = objVCM.AddUpdateApprover(0, entityTypeId, approverTypeId, approverId,0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Approver added successfully");
                Reset();
            }
            else if (value < 0)
            {
                ExceptionMessage("Approver already exists...!!");
            }
            else
            {
                ExceptionMessage("Please try again...!!");
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
        ddlApproverTypeToS.SelectedIndex = 0;
        ddlApproverToS.SelectedIndex = 0;
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
