using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_MENU_AddMenu : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.Common objCommon = new BAL.Common();

    DataSet dsEmpMenu = new DataSet();
    int menuID = 0;
    string menuName = string.Empty;
    string url = string.Empty;
    int parentMenuID = 0;
    int serialNo = 0;

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindEmpMenu();

                if (Convert.ToInt32(Request.QueryString["menuid"]) > 0)
                {
                    menuName = string.Empty;
                    url = string.Empty;
                    parentMenuID = 0;
                    serialNo = 0;


                    if (Request.QueryString["menuid"] != null && Convert.ToInt32(Request.QueryString["menuid"]) > 0)
                        menuID = Convert.ToInt32(Request.QueryString["menuid"]);


                    lblLegend.Text = "Update Menu";
                    btnSubmit.Text = "Update";


                    if (Request.QueryString["menuname"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["menuname"])))
                        menuName = Convert.ToString(Request.QueryString["menuname"]);

                    if (Request.QueryString["url"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["url"])))
                        url = Convert.ToString(Request.QueryString["url"]);

                    if (Request.QueryString["parentid"] != null && Convert.ToInt32(Request.QueryString["parentid"]) > 0)
                        parentMenuID = Convert.ToInt32(Request.QueryString["parentid"]);

                    if (Request.QueryString["serialno"] != null && Convert.ToInt32(Request.QueryString["serialno"]) > 0)
                        serialNo = Convert.ToInt32(Request.QueryString["serialno"]);




                    txtMenuName.Text = menuName;
                    txtURL.Text = url;
                    ddlParentMenu.SelectedValue = Convert.ToString(parentMenuID);
                    txtSerialNo.Text = Convert.ToString(serialNo);
                }

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HidePannel();
        AddUpdateMenu();
    }

    protected void btnMenuList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADMIN/MENU/MenuList.aspx");
    }

    #endregion


    #region METHODS[=====================]

    private void BindEmpMenu()
    {
        try
        {
            dsEmpMenu = objCommon.GetAssignedMenuList(0);
            if (dsEmpMenu.Tables.Count > 0 && dsEmpMenu.Tables[0].Rows.Count > 0)
            {
                ddlParentMenu.DataSource = dsEmpMenu.Tables[0];
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

    private void AddUpdateMenu()
    {
        try
        {
            if (Convert.ToInt32(Request.QueryString["menuid"]) > 0)
                menuID = Convert.ToInt32(Request.QueryString["menuid"]);


            menuName = txtMenuName.Text;
            url = txtURL.Text;

            if (!url.Contains(".aspx"))
                url = url + ".aspx";


            if (ddlParentMenu.SelectedIndex > 0)
                parentMenuID = Convert.ToInt32(ddlParentMenu.SelectedValue);
            else
                parentMenuID = 0;


            if (!string.IsNullOrEmpty(txtSerialNo.Text))
                serialNo = Convert.ToInt32(txtSerialNo.Text);
            else
                serialNo = 0;

            int value = objCommon.AddUpdateMenu(menuID, menuName, url, parentMenuID, serialNo, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                if (menuID > 0)
                    SuccessMessage("Menu updated successfully");

                else
                    SuccessMessage("Menu added successfully");
            }
            else
            {
                ExceptionMessage("Please try again");
            }

            txtMenuName.Text = string.Empty;
            txtURL.Text = string.Empty;
            ddlParentMenu.SelectedIndex = 0;
            txtSerialNo.Text = "0";

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

    private void HidePannel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

  

    #endregion

}
