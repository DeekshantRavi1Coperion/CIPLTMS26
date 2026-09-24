using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_MENU_MenuList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    DataSet dsParentMenu = new DataSet();
    DataSet dsMenuList = new DataSet();

    int menuID = 0;
    string menuName = string.Empty;
    int parentMenuID = 0;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindEmpMenu();
                GetMenuList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetMenuList();
    }

    protected void gvMenuList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMenuList.PageIndex = e.NewPageIndex;
        GetMenuList();
    }

    protected void gvMenuList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                ImageButton img = (ImageButton)gvMenuList.Rows[rowindex].Cells[4].FindControl("imgProperties");

                Label lblMenuID = gvMenuList.Rows[rowindex].FindControl("lblMenuID") as Label;
                Label lblMenuName = gvMenuList.Rows[rowindex].FindControl("lblMenuName") as Label;
                Label lblURL = gvMenuList.Rows[rowindex].FindControl("lblURL") as Label;
                Label lblParentMenuID = gvMenuList.Rows[rowindex].FindControl("lblParentMenuID") as Label;
                Label lblSerialNo = gvMenuList.Rows[rowindex].FindControl("lblSerialNo") as Label;

                string menuID = lblMenuID.Text;
                string menuName = lblMenuName.Text; // gvMenuList.Rows[rowindex].Cells[1].Text;
                string url = lblURL.Text; //gvMenuList.Rows[rowindex].Cells[2].Text;                
                string parentID = lblParentMenuID.Text;
                string serialNo = lblSerialNo.Text;

                Response.Redirect("~/ADMIN/MENU/AddMenu.aspx?menuid=" + menuID + "&menuname=" + menuName + "&url=" + url + "&parentid=" + parentID + "&serialno=" + serialNo);
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvMenuList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/MENU/AddMenu.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void BindEmpMenu()
    {
        try
        {
            dsParentMenu = objCommon.GetAssignedMenuList(0);
            if (dsParentMenu.Tables.Count > 0 && dsParentMenu.Tables[0].Rows.Count > 0)
            {
                ddlParentMenu.DataSource = dsParentMenu.Tables[0];
                ddlParentMenu.DataTextField = "MENU_NAME";
                ddlParentMenu.DataValueField = "MENU_ID";
                ddlParentMenu.DataBind();
                ddlParentMenu.Items.Insert(0, "Select");
                ddlParentMenu.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetMenuList()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtMenuName.Text))
                menuName = txtMenuName.Text.Trim();
            else
                menuName = string.Empty;

            if (ddlParentMenu.SelectedIndex > 0)
                parentMenuID = Convert.ToInt32(ddlParentMenu.SelectedValue);
            else
                parentMenuID = 0;

            dsMenuList = objCommon.GetMenuList(menuName, parentMenuID);
            if (dsMenuList.Tables.Count > 0 && dsMenuList.Tables[0].Rows.Count > 0)
            {
                gvMenuList.DataSource = dsMenuList.Tables[0];
                gvMenuList.DataBind();
            }
            else
            {
                gvMenuList.DataSource = null;
                gvMenuList.DataBind();
            }
            lblRecords.Text = "Records[" + dsMenuList.Tables[0].Rows.Count + "]";
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
