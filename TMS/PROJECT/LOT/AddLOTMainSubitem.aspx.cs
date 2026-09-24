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

public partial class PROJECT_LOT_AddLOTMainSubitem : System.Web.UI.Page
{

    #region VARIABLES[=======================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsManager = new DataSet();

    int companyID = 0;
    int departmentID = 0;
    int LOTMainItemID = 0;
    string LOTMainSubitem = string.Empty;
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
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewMainSubitem();
    }

    protected void btnSubitemList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/LOTMainSubitemsList.aspx");
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

    private void BindManager()
    {
        try
        {
            dsManager = objProject.GetEmployeesToAddApprover();
            if (dsManager.Tables.Count > 0 && dsManager.Tables[0].Rows.Count > 0)
            {

                ddlManager.DataSource = dsManager.Tables[0];
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

    private void AddNewMainSubitem()
    {
        try
        {
            companyID = 0;
            departmentID = 0;
            LOTMainItemID = 0;
            LOTMainSubitem = string.Empty;
            managerID = 0;


            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (ddlLOTMainItems.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTMainSubitem.Text))
                LOTMainSubitem = txtLOTMainSubitem.Text.Trim();

            if (ddlManager.SelectedIndex > 0)
                managerID = Convert.ToInt32(ddlManager.SelectedValue);


            int value = 0;
            value = objProject.AddUpdateMainSubitem(0, companyID, LOTMainItemID, LOTMainSubitem, departmentID, managerID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("LOT main subitem added successfully..!!");
                Reset();
                return;
            }
            else if (value < 0)
            {
                ExceptionMessage("LOT main subitem already exists...!!!");
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
            ddlCompany.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            ddlLOTMainItems.SelectedIndex = 0;
            txtLOTMainSubitem.Text = string.Empty;
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
