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

public partial class TC_AddTCChecker : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.TC objTC = new BAL.TC();

    DataSet dsEmployee = new DataSet();
    DataSet dsDept = new DataSet();
    DataSet dsUnit = new DataSet();

    int unitID = 0;
    int departmentID = 0;
    int empRecordID = 0;

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
                BindChecker();

                ddlDepartment.DataSource = null;
                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Insert(0, "Select");
                ddlDepartment.SelectedIndex = 0;

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex>0)
        {
            BindDepartment();
        }
        else
        {
            ddlDepartment.DataSource = null;
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Insert(0, "Select");
            ddlDepartment.SelectedIndex = 0;
        }        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddChecker();
    }

    protected void btnViewCheckerList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TC/ViewTCCheckerList.aspx");
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
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "Select");
                ddlUnit.SelectedIndex = 0;
            }
            else
            {
                ddlUnit.DataSource = null;
                ddlUnit.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDepartment()
    {
        try
        {
            unitID = 0;
            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);


            dsDept = objTC.GetTCDepartmentList(unitID, "");
            if (dsDept.Tables.Count > 0 && dsDept.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.DataSource = dsDept.Tables[0];
                ddlDepartment.DataTextField = "TC_DEPARTMENT";
                ddlDepartment.DataValueField = "TC_DEPARTMENT_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "Select");
                ddlDepartment.SelectedIndex = 0;
            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Insert(0, "Select");
                ddlDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindChecker()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByDepartmentID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {

                ddlChecker.DataSource = dsEmployee.Tables[0];
                ddlChecker.DataTextField = "EMPLOYEE_NAME";
                ddlChecker.DataValueField = "EMP_RECORD_ID";
                ddlChecker.DataBind();
                ddlChecker.Items.Insert(0, "Select");
                ddlChecker.SelectedIndex = 0;
            }
            else
            {
                ddlChecker.DataSource = null;
                ddlChecker.Items.Clear();
                ddlChecker.Items.Insert(0, "Select");
                ddlChecker.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddChecker()
    {
        try
        {
            departmentID = 0;
            empRecordID = 0;

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (ddlChecker.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlChecker.SelectedValue);

            int value = 0;
            value = objTC.AddUpdateChecker(0, departmentID, empRecordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Checker added successfully..!!");
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("Checker already existed with given department...!!!");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion    
}
