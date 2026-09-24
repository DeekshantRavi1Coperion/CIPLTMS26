using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_DMS_AddDesignChecker : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();
    DataSet dsDept = new DataSet();
    DataSet dsEmployee = new DataSet();

    int isExisted = 0;
    int employeeRecordID = 0;

    #endregion


    #region EVNETS[==========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                ddlEmployees.Items.Clear();
                ddlEmployees.Items.Insert(0, "Select");
                ddlEmployees.SelectedIndex = 0;

                BindEmployee(0);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddDesignChecker();
    }

    protected void btnCheckerList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/DesignCheckerList.aspx");
    }

    #endregion


    #region METHODS[=========================]
    
    private void BindEmployee(int departmentID)
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByDepartmentID(departmentID);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                ddlEmployees.DataSource = dsEmployee.Tables[0];
                ddlEmployees.DataTextField = "EMPLOYEE_NAME";
                ddlEmployees.DataValueField = "EMP_RECORD_ID";
                ddlEmployees.DataBind();
                ddlEmployees.Items.Insert(0, "Select");
                ddlEmployees.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void AddDesignChecker()
    {
        try
        {
            employeeRecordID = 0;

            if (ddlEmployees.SelectedIndex > 0)
                employeeRecordID = Convert.ToInt32(ddlEmployees.SelectedValue);


            int value = objProject.AddDesignChecker(0, employeeRecordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                SuccessMessage("Design checker added successfully");
                ddlEmployees.SelectedIndex = 0;
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Design checker already exists...!!");
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
