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

public partial class PROJECT_LOT_LOTMainSubitemManagersList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();
    
    DataSet dsUnit = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsLOTMainItems = new DataSet();    
    DataSet dsLOTMainSubitems = new DataSet();    
    DataSet dsManagers = new DataSet();


    DataSet dsUnitToEdit = new DataSet();
    DataSet dsDepartmentToEdit = new DataSet();
    DataSet dsLOTMainItemsToEdit = new DataSet();
    DataSet dsLOTMainSubitemsToEdit = new DataSet();
    DataSet dsManagersToEdit = new DataSet();


    int managerRecordID = 0;
    int companyID = 0;
    int departmentID = 0;
    int LOTMainItemID = 0;
    int LOTMainSubitemID = 0;
    int managerID = 0;

    #endregion


    #region EVENTS[========================]

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


                ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

                GetLOTMainSubitemManagerList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTMainSubitemManagerList();
    }

    protected void gvLOTMainSubitemManagers_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblManagerRecordID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblManagerRecordID") as Label;
                Label lblCompanyID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblCompanyID") as Label;
                Label lblDepartmentID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblDepartmentID") as Label;
                Label lblLOTMainItemID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblLOTMainItemID") as Label;

                Label lblLOTMainSubitemID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblLOTMainSubitemID") as Label;
                Label lblManagerID = gvLOTMainSubitemManagers.Rows[rowindex].FindControl("lblManagerID") as Label;


                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblManagerRecordID.Text))
                        ViewState["MANAGER_RECORD_ID"] = Convert.ToInt32(lblManagerRecordID.Text);
                    else
                        ViewState["MANAGER_RECORD_ID"] = 0;

                    BindCompanyToEdit();
                    ddlCompanyToEdit.SelectedValue = Convert.ToString(lblCompanyID.Text);

                    BindDepartmentToEdit();
                    ddlDepartmentToEdit.SelectedValue = Convert.ToString(lblDepartmentID.Text);

                    BindLOTMainItemToEdit();
                    ddlLOTMainItemsToEdit.SelectedValue = Convert.ToString(lblLOTMainItemID.Text);

                    BindLOTMainSubItemsToEdit(Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue));
                    ddlLOTMainSubitemsToEdit.SelectedValue = Convert.ToString(lblLOTMainSubitemID.Text);

                    BindManagerTEdit();
                    ddlManagerToEdit.SelectedValue = Convert.ToString(lblManagerID.Text);

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

    protected void btnAddNewManager_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/AddLOTMainSubitemManagers.aspx");
    }














    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset();
    }

    protected void ddlCompanyToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetToEdit();
        ModalPopupExtender1.Show();
    }
    
    protected void ddlLOTMainItemsToEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
        {
            BindLOTMainSubItemsToEdit(Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue));
        }
        else
        {
            ddlLOTMainSubitemsToEdit.Items.Clear();
            ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
            ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
        }
        ModalPopupExtender1.Show();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        AddNewLOTMainSubitemManager();
    }

    #endregion


    #region METHODS[=======================]




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
            else
            {
                ddlLOTMainItems.DataSource = null;
                ddlLOTMainItems.Items.Clear();
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
            dsManagers = objProject.GetEmployeesToAddApprover();
            if (dsManagers.Tables.Count > 0 && dsManagers.Tables[0].Rows.Count > 0)
            {

                ddlManager.DataSource = dsManagers.Tables[0];
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

    private void Reset()
    {
        ddlDepartment.SelectedIndex = 0;
        ddlLOTMainItems.SelectedIndex = 0;

        ddlLOTMainSubitems.Items.Clear();
        ddlLOTMainSubitems.Items.Insert(0, "Select");
        ddlLOTMainSubitems.SelectedIndex = 0;

        ddlManager.SelectedIndex = 0;
    }

    private void GetLOTMainSubitemManagerList()
    {
        try
        {
            companyID = 0;
            departmentID = 0;
            LOTMainItemID = 0;
            LOTMainSubitemID = 0;
            managerID = 0;



            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlDepartment.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (ddlLOTMainItems.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            if (ddlLOTMainSubitems.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitems.SelectedValue);

            if (ddlManager.SelectedIndex > 0)
                managerID = Convert.ToInt32(ddlManager.SelectedValue);


            dsManagers = objProject.GetLotMainSubItemManagerList(companyID, departmentID, LOTMainItemID, LOTMainSubitemID, managerID);

            if (dsManagers.Tables.Count > 0 && dsManagers.Tables[0].Rows.Count > 0)
            {
                gvLOTMainSubitemManagers.DataSource = dsManagers.Tables[0];
                gvLOTMainSubitemManagers.DataBind();
            }
            else
            {
                gvLOTMainSubitemManagers.DataSource = null;
                gvLOTMainSubitemManagers.DataBind();
            }
            lblRecords.Text = "Records[" + dsManagers.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }







    private void BindCompanyToEdit()
    {
        try
        {
            dsUnitToEdit = objCommon.GetUnit();
            if (dsUnitToEdit.Tables.Count > 0 && dsUnitToEdit.Tables[0].Rows.Count > 0)
            {
                ddlCompanyToEdit.DataSource = dsUnitToEdit.Tables[0];
                ddlCompanyToEdit.DataTextField = "UNIT_NAME";
                ddlCompanyToEdit.DataValueField = "UNIT_ID";
                ddlCompanyToEdit.DataBind();
            }
            else
            {
                ddlCompanyToEdit.DataSource = null;
                ddlCompanyToEdit.Items.Clear();                
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDepartmentToEdit()
    {
        try
        {
            dsDepartmentToEdit = objProject.GetLOTDepartments();
            if (dsDepartmentToEdit.Tables.Count > 0 && dsDepartmentToEdit.Tables[0].Rows.Count > 0)
            {
                ddlDepartmentToEdit.DataSource = dsDepartmentToEdit.Tables[0];
                ddlDepartmentToEdit.DataTextField = "DEPARTMENT_NAME";
                ddlDepartmentToEdit.DataValueField = "DEPARTMENT_ID";
                ddlDepartmentToEdit.DataBind();
                ddlDepartmentToEdit.Items.Insert(0, "Select");
                ddlDepartmentToEdit.SelectedIndex = 0;
            }
            else
            {
                ddlDepartmentToEdit.DataSource = null;
                ddlDepartmentToEdit.Items.Clear();
                ddlDepartmentToEdit.Items.Insert(0, "Select");
                ddlDepartmentToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItemToEdit()
    {
        try
        {
            dsLOTMainItemsToEdit = objProject.GetLotMainItems();
            if (dsLOTMainItemsToEdit.Tables.Count > 0 && dsLOTMainItemsToEdit.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItemsToEdit.DataSource = dsLOTMainItemsToEdit.Tables[0];
                ddlLOTMainItemsToEdit.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItemsToEdit.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItemsToEdit.DataBind();
                ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainItemsToEdit.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainItemsToEdit.DataSource = null;
                ddlLOTMainItemsToEdit.Items.Clear();
                ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainItemsToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainSubItemsToEdit(int LOTMainItemID)
    {
        try
        {
            dsLOTMainSubitemsToEdit = objProject.GetLotMainSubItems(LOTMainItemID, Convert.ToInt32(ddlCompanyToEdit.SelectedValue));
            if (dsLOTMainSubitemsToEdit.Tables.Count > 0 && dsLOTMainSubitemsToEdit.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainSubitemsToEdit.DataSource = dsLOTMainSubitemsToEdit.Tables[0];
                ddlLOTMainSubitemsToEdit.DataTextField = "LOT_MAIN_SUBITEM";
                ddlLOTMainSubitemsToEdit.DataValueField = "LOT_MAIN_SUBITEM_ID";
                ddlLOTMainSubitemsToEdit.DataBind();
                ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainSubitemsToEdit.Items.Clear();
                ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainSubitemsToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindManagerTEdit()
    {
        try
        {
            dsManagersToEdit = objProject.GetEmployeesToAddApprover();
            if (dsManagersToEdit.Tables.Count > 0 && dsManagersToEdit.Tables[0].Rows.Count > 0)
            {

                ddlManagerToEdit.DataSource = dsManagersToEdit.Tables[0];
                ddlManagerToEdit.DataTextField = "EMPLOYEE_NAME";
                ddlManagerToEdit.DataValueField = "EMP_RECORD_ID";
                ddlManagerToEdit.DataBind();
                ddlManagerToEdit.Items.Insert(0, "Select");
                ddlManagerToEdit.SelectedIndex = 0;
            }
            else
            {
                ddlManagerToEdit.DataSource = null;
                ddlManagerToEdit.Items.Clear();
                ddlManagerToEdit.Items.Insert(0, "Select");
                ddlManagerToEdit.SelectedIndex = 0;
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
            managerRecordID = 0;
            companyID = 0;
            departmentID = 0;
            LOTMainSubitemID = 0;
            managerID = 0;

            if (ViewState["MANAGER_RECORD_ID"] != null && Convert.ToInt32(ViewState["MANAGER_RECORD_ID"]) > 0)
                managerRecordID = Convert.ToInt32(ViewState["MANAGER_RECORD_ID"]);

            companyID = Convert.ToInt32(ddlCompanyToEdit.SelectedValue);

            if (ddlDepartmentToEdit.SelectedIndex > 0)
                departmentID = Convert.ToInt32(ddlDepartmentToEdit.SelectedValue);

            if (ddlLOTMainSubitemsToEdit.SelectedIndex > 0)
                LOTMainSubitemID = Convert.ToInt32(ddlLOTMainSubitemsToEdit.SelectedValue);

            if (ddlManagerToEdit.SelectedIndex > 0)
                managerID = Convert.ToInt32(ddlManagerToEdit.SelectedValue);


            int value = 0;

            if (managerRecordID > 0)
            {
                value = objProject.AddNewLOTMainSubitemManager(managerRecordID, companyID, departmentID, LOTMainSubitemID, managerID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    SuccessMessage("LOT Main Subitem Manager details updated successfully..!!");
                    ResetToEdit();
                    GetLOTMainSubitemManagerList();
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }
            
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
  
    private void ResetToEdit()
    {
        ddlDepartmentToEdit.SelectedIndex = 0;
        ddlLOTMainItemsToEdit.SelectedIndex = 0;

        ddlLOTMainSubitemsToEdit.Items.Clear();
        ddlLOTMainSubitemsToEdit.Items.Insert(0, "Select");
        ddlLOTMainSubitemsToEdit.SelectedIndex = 0;

        ddlManagerToEdit.SelectedIndex = 0;
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