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

public partial class PROJECT_LOT_AddLOTMainSubitemManagers : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsLOTMainSubitems = new DataSet();


    int companyID = 0;
    int departmentID = 0;
    int LOTMainSubitemID = 0;
    int managerID = 0;

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
                BindDepartment();
                BindLOTMainItem();
                BindManager();

                ddlLOTMainSubitems.Items.Insert(0, "Select");
                ddlLOTMainSubitems.SelectedIndex = 0;
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

    protected void ddlLOTMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLOTMainItems.SelectedIndex > 0)
        {
            BindLOTMainSubItems(Convert.ToInt32(ddlLOTMainItems.SelectedValue));
        }
        else
        {
            ddlLOTMainSubitems.Items.Clear();
            ddlLOTMainSubitems.Items.Insert(0, "Select");
            ddlLOTMainSubitems.SelectedIndex = 0;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewLOTMainSubitemManager();
    }

    protected void btnMangerList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTMainSubitemManagersList.aspx");
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
            }
            else
            {
                ddlCompany.DataSource = null;
                ddlCompany.Items.Clear();               
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
            dsDepartment = objProject.GetLOTDepartments();
            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDepartment.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME";
                ddlDepartment.DataValueField = "DEPARTMENT_ID";
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


    private void BindLOTMainItem()
    {
        try
        {
            dsLOTMainItems = objProject.GetLotMainItems();
            if (dsLOTMainItems.Tables.Count > 0 && dsLOTMainItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTMainItems.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "Select");
                ddlLOTMainItems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindLOTMainSubItems(int LOTMainItemID)
    {
        try
        {
            dsLOTMainSubitems = objProject.GetLotMainSubItems(LOTMainItemID, Convert.ToInt32(ddlCompany.SelectedValue));
            if (dsLOTMainSubitems.Tables.Count > 0 && dsLOTMainSubitems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainSubitems.DataSource = dsLOTMainSubitems.Tables[0];
                ddlLOTMainSubitems.DataTextField = "LOT_MAIN_SUBITEM";
                ddlLOTMainSubitems.DataValueField = "LOT_MAIN_SUBITEM_ID";
                ddlLOTMainSubitems.DataBind();
                ddlLOTMainSubitems.Items.Insert(0, "Select");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainSubitems.Items.Clear();
                ddlLOTMainSubitems.Items.Insert(0, "Select");
                ddlLOTMainSubitems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindManager()
    {
        try
        {
            dsEmployee = objProject.GetEmployeesToAddApprover();
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {

                ddlManager.DataSource = dsEmployee.Tables[0];
                ddlManager.DataTextField = "EMPLOYEE_NAME";
                ddlManager.DataValueField = "EMP_RECORD_ID";
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, "Select");
                ddlManager.SelectedIndex = 0;
            }
            else
            {
                ddlManager.DataSource = null;
                ddlManager.Items.Clear();
                ddlManager.Items.Insert(0, "Select");
                ddlManager.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void AddNewLOTMainSubitemManager()
    {
        try
        {
            companyID = 0;
            departmentID = 0;
            LOTMainSubitemID = 0;
            managerID = 0;


            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (ddlLOTMainSubitems.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitems.SelectedValue);

            if (ddlManager.SelectedIndex > 0)
                managerID = Convert.ToInt32(ddlManager.SelectedValue);


            int value = 0;
            value = objProject.AddNewLOTMainSubitemManager(0, companyID, departmentID, LOTMainSubitemID, managerID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("LOT manager added successfully...!!!");
                return;
            }
            else if (value<0)
            {
                ExceptionMessage("Manager already exists...!!!");
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
            //ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlLOTMainItems.SelectedIndex = 0;

            ddlLOTMainSubitems.Items.Clear();
            ddlLOTMainSubitems.Items.Insert(0, "Select");
            ddlLOTMainSubitems.SelectedIndex = 0;

            ddlManager.SelectedIndex = 0;
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
