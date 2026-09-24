using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using BAL;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.tool.xml;

public partial class TC_ViewTCCheckerList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.TC objTC = new BAL.TC();

    DataSet dsChecker = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsEmployee = new DataSet();
    DataSet dsDept = new DataSet();

    string checker = string.Empty;
    int unitID = 0;
    int checkerRecordID = 0;
    int departmentID = 0;
    int empRecordID = 0;
   
    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                //BindChecker();
                //BindCompany();
                BindDepartment();
                GetCheckerList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetCheckerList();
    }

    protected void ddlUnitToUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        if (ddlUnitToUpdate.SelectedIndex > 0)
        {
            BindDepartmentToUpdate();
        }
        else
        {
            ddlDepartmentToUpdate.DataSource = null;
            ddlDepartmentToUpdate.Items.Clear();
            ddlDepartmentToUpdate.Items.Insert(0, "Select");
            ddlDepartmentToUpdate.SelectedIndex = 0;
        }
    }

    protected void gvCheckerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblCheckerRecordID = gvCheckerList.Rows[rowindex].FindControl("lblCheckerRecordID") as Label;
                Label lblUnitID = gvCheckerList.Rows[rowindex].FindControl("lblUnitID") as Label;
                Label lblDepartmentID = gvCheckerList.Rows[rowindex].FindControl("lblDepartmentID") as Label;
                Label lblEmpRecordID = gvCheckerList.Rows[rowindex].FindControl("lblEmpRecordID") as Label;



                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblCheckerRecordID.Text))
                        ViewState["TC_CHECKER_ID"] = Convert.ToInt32(lblCheckerRecordID.Text);
                    else
                        ViewState["TC_CHECKER_ID"] = 0;

                    BindCompany();
                    ddlUnitToUpdate.SelectedValue = Convert.ToString(lblUnitID.Text);
                    BindDepartmentToUpdate();
                    ddlDepartmentToUpdate.SelectedValue = Convert.ToString(lblDepartmentID.Text);
                    BindChecker();
                    ddlCheckerToUpdate.SelectedValue = Convert.ToString(lblEmpRecordID.Text);
                    
                    ModalPopupExtender1.Show();
                }
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

    protected void btnAddNewChecker_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TC/AddTCChecker.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateChecker();
    }


    #endregion


    #region METHODS[=======================]

    private void BindChecker()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByDepartmentID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {

                ddlCheckerToUpdate.DataSource = dsEmployee.Tables[0];
                ddlCheckerToUpdate.DataTextField = "EMPLOYEE_NAME";
                ddlCheckerToUpdate.DataValueField = "EMP_RECORD_ID";
                ddlCheckerToUpdate.DataBind();
                ddlCheckerToUpdate.Items.Insert(0, "Select");
                ddlCheckerToUpdate.SelectedIndex = 0;
            }
            else
            {
                ddlCheckerToUpdate.DataSource = null;
                ddlCheckerToUpdate.Items.Clear();
                ddlCheckerToUpdate.Items.Insert(0, "Select");
                ddlCheckerToUpdate.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCompany()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {                
                ddlUnitToUpdate.DataSource = dsUnit.Tables[0];
                ddlUnitToUpdate.DataTextField = "UNIT_NAME";
                ddlUnitToUpdate.DataValueField = "UNIT_ID";
                ddlUnitToUpdate.DataBind();
                ddlUnitToUpdate.Items.Insert(0, "Select");
                ddlUnitToUpdate.SelectedIndex = 0;
            }
            else
            {               
                ddlUnitToUpdate.DataSource = null;
                ddlUnitToUpdate.Items.Clear();
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
            dsDept = objTC.GetTCDepartmentList(0, "");
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

    private void BindDepartmentToUpdate()
    {
        try
        {
            unitID = 0;
            if (ddlUnitToUpdate.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnitToUpdate.SelectedValue);


            dsDept = objTC.GetTCDepartmentList(unitID, "");
            if (dsDept.Tables.Count > 0 && dsDept.Tables[0].Rows.Count > 0)
            {                
                ddlDepartmentToUpdate.DataSource = dsDept.Tables[0];
                ddlDepartmentToUpdate.DataTextField = "TC_DEPARTMENT";
                ddlDepartmentToUpdate.DataValueField = "TC_DEPARTMENT_ID";
                ddlDepartmentToUpdate.DataBind();
                ddlDepartmentToUpdate.Items.Insert(0, "Select");
                ddlDepartmentToUpdate.SelectedIndex = 0;
            }
            else
            {                
                ddlDepartmentToUpdate.DataSource = null;
                ddlDepartmentToUpdate.Items.Clear();
                ddlDepartmentToUpdate.Items.Insert(0, "Select");
                ddlDepartmentToUpdate.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCheckerList()
    {
        try
        {
            departmentID = 0;
            checker = string.Empty;

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (!string.IsNullOrEmpty(txtChecker.Text))
                checker = txtChecker.Text;

            dsChecker = objTC.GetCheckerList(checker, departmentID);

            if (dsChecker.Tables.Count > 0 && dsChecker.Tables[0].Rows.Count > 0)
            {
                gvCheckerList.DataSource = dsChecker.Tables[0];
                gvCheckerList.DataBind();
            }
            else
            {
                gvCheckerList.DataSource = null;
                gvCheckerList.DataBind();
            }
            lblRecords.Text = "Records[" + dsChecker.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateChecker()
    {
        try
        {
            checkerRecordID = 0;
            departmentID = 0;
            empRecordID = 0;

            if (ViewState["TC_CHECKER_ID"] != null)
                checkerRecordID = Convert.ToInt32(ViewState["TC_CHECKER_ID"]);

            if (ddlDepartmentToUpdate.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartmentToUpdate.SelectedValue);

            if (ddlCheckerToUpdate.SelectedIndex > 0)
                empRecordID = Convert.ToInt32(ddlCheckerToUpdate.SelectedValue);


            int value = 0;
            if (checkerRecordID > 0)
            {
                value = objTC.AddUpdateChecker(checkerRecordID, departmentID, empRecordID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }


            if (value > 0)
            {
                SuccessMessage("Checker updated successfully...!!!");
                GetCheckerList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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