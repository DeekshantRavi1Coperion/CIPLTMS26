using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_MastersList : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();


    DataSet dsItemCategory = new DataSet();
    DataSet dsItemSubcategory = new DataSet();
    DataSet dsDepartment = new DataSet();
    DataSet dsTimesheetDepartment = new DataSet();
    DataSet dsList = new DataSet();

    DataSet dsEmployee = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsBank = new DataSet();
    string empName = string.Empty;
    string empID = string.Empty;
    int departmentID = 0;

    DataSet dsEntityType = new DataSet();
    DataSet dsApproverType = new DataSet();
    DataSet dsEmployees = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["dtEntityType"] = null;
                Session["dtApproverType"] = null;
                Session["dtEmployees"] = null;
                Session["dtList"] = null;
                Session["dtItemCategory"] = null;
                Session["dtItemSubcategory"] = null;

                GetItemCategorys();
                GetItemSubcategorys();

                BindItemCategorys();
                BindItemaSubcategorys();

                GetMastersList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlMsg.Visible = false;
        GetMastersList();
    }

    protected void gvMastersList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvMastersList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                HidePanels();
                int rowindex = 0;
                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblPID = gvMastersList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblItemCategoryFID = gvMastersList.Rows[rowindex].FindControl("lblItemCategoryFID") as Label;
                Label lblTypeFID = gvMastersList.Rows[rowindex].FindControl("lblTypeFID") as Label;
                Label lblName = gvMastersList.Rows[rowindex].FindControl("lblName") as Label;
                Label lblDescription = gvMastersList.Rows[rowindex].FindControl("lblDescription") as Label;

                ViewState["PID"] = Convert.ToInt32(lblPID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    ddlMasterTypeToS.SelectedValue = Convert.ToString(lblTypeFID.Text);

                   
                    if (Convert.ToInt32(lblItemCategoryFID.Text)>0)
                    {
                        BindItemCategorysToS();
                        ddlItemCategoryToS.SelectedValue = Convert.ToString(lblItemCategoryFID.Text);
                    }
                    

                    txtNameToS.Text = lblName.Text;
                    txtDescriptionToS.Text = lblDescription.Text;

                    this.ModalPopupExtender1.Show();
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



    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/AddMasters.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateMaster();
    }


    #endregion


    #region METHODS[=======================]



    private void GetItemCategorys()
    {
        try
        {
            dsItemCategory = objVCM.GetItemCategorys();
            if (dsItemCategory.Tables.Count > 0 && dsItemCategory.Tables[0].Rows.Count > 0)
            {
                Session["dtItemCategory"] = dsItemCategory.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindItemCategorys()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtItemCategory"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlItemCategory.DataSource = dtDropdown;
                ddlItemCategory.DataTextField = "NAME";
                ddlItemCategory.DataValueField = "PID";
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, "All");
                ddlItemCategory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindItemCategorysToS()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtItemCategory"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlItemCategoryToS.DataSource = dtDropdown;
                ddlItemCategoryToS.DataTextField = "NAME";
                ddlItemCategoryToS.DataValueField = "PID";
                ddlItemCategoryToS.DataBind();
                ddlItemCategoryToS.Items.Insert(0, "Select");
                ddlItemCategoryToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetItemSubcategorys()
    {
        try
        {

            dsItemSubcategory = objVCM.GetItemSubCategorys(0);
            if (dsItemSubcategory.Tables.Count > 0 && dsItemSubcategory.Tables[0].Rows.Count > 0)
            {
                Session["dtItemSubcategory"] = dsItemSubcategory.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindItemaSubcategorys()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtItemSubcategory"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlItemSubcategory.DataSource = dtDropdown;
                ddlItemSubcategory.DataTextField = "NAME";
                ddlItemSubcategory.DataValueField = "PID";
                ddlItemSubcategory.DataBind();
                ddlItemSubcategory.Items.Insert(0, "All");
                ddlItemSubcategory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

   

    private void GetMastersList()
    {
        try
        {
            int typeId = 0;
            int itemCategoryId = 0;
            int itemSubcategoryId = 0;
            string name = string.Empty;
            string description = string.Empty;

            typeId = Convert.ToInt32(ddlMasterType.SelectedValue);
            if (ddlItemCategory.SelectedIndex > 0) itemCategoryId = Convert.ToInt32(ddlItemCategory.SelectedValue);
            if (ddlItemSubcategory.SelectedIndex > 0) itemSubcategoryId = Convert.ToInt32(ddlItemSubcategory.SelectedValue);

            if (!string.IsNullOrEmpty(txtName.Text))
                name = Convert.ToString(txtName.Text);

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = Convert.ToString(txtDescription.Text);

            dsList = objVCM.GetMasterTablesList(typeId, itemCategoryId, itemSubcategoryId, name, description);
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                gvMastersList.DataSource = dsList.Tables[0];
                gvMastersList.DataBind();
            }
            else
            {
                gvMastersList.DataSource = null;
                gvMastersList.DataBind();
            }
            lblRecords.Text = "Records[" + dsList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateMaster()
    {
        try
        {
            int pid = Convert.ToInt32(ViewState["PID"]);
            int masterTypeId = 0;
            int itemCategoryId = 0;
            string name = string.Empty;
            string description = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);


            if (ddlMasterTypeToS.SelectedIndex > 0) masterTypeId = Convert.ToInt32(ddlMasterTypeToS.SelectedValue);
            if (ddlItemCategoryToS.SelectedIndex > 0) itemCategoryId = Convert.ToInt32(ddlItemCategoryToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtNameToS.Text)) name = txtNameToS.Text;
            if (!string.IsNullOrEmpty(txtDescriptionToS.Text)) description = txtDescriptionToS.Text;

            int value = objVCM.AddUpdateMasterTables(pid, masterTypeId, name, description, itemCategoryId, createdBy);
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

    private void HidePanels()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;

        pnlUpdateMsg.Visible = false;
        lblUpdateMsg.Text = string.Empty;

    }

    private void Reset()
    {
        ddlMasterTypeToS.SelectedIndex = 0;
        ddlItemCategoryToS.SelectedIndex = 0;
        txtNameToS.Text = string.Empty;
        txtDescriptionToS.Text = string.Empty;
    }

    #endregion

}
