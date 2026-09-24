using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_AddResponsibles : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    DataSet dsItemCategory = new DataSet();

    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsEmployees = new DataSet();

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
                BindEmployees();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddResponsible();
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/ResponsiblesList.aspx");
    }

    #endregion


    #region METHODS[=========================]

    private void BindEmployees()
    {
        try
        {
            dsEmployees = objCommon.GetEmployeeByEmpRecordID(0);
            if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
            {
                ddlEmployeeToS.DataSource = dsEmployees.Tables[0];
                ddlEmployeeToS.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeToS.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeToS.DataBind();
                ddlEmployeeToS.Items.Insert(0, "Select");
                ddlEmployeeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddResponsible()
    {
        try
        {
            int employeeId = 0;
            string name = string.Empty;
            string description = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);


            if (ddlEmployeeToS.SelectedIndex > 0) employeeId = Convert.ToInt32(ddlEmployeeToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtShortNameToS.Text)) name = txtShortNameToS.Text;
            if (!string.IsNullOrEmpty(txtFullNameToS.Text)) description = txtFullNameToS.Text;

            int value = objVCM.AddUpdateRespnsible(0, employeeId, name, description,0, createdBy);
            if (value > 0)
            {
                SuccessMessage("Saved successfully");
                Reset();
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
        ddlEmployeeToS.SelectedIndex = 0;
        txtShortNameToS.Text = string.Empty;
        txtFullNameToS.Text = string.Empty;
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
