using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class HOME : System.Web.UI.MasterPage
{
    DataSet dsMenu = new DataSet();
    BAL.Common objCommon = new BAL.Common();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-480));
                Response.Cache.SetNoStore();

                lblEmployeeName.Text = Convert.ToString(Session["EMPLOYEE_NAME"]);
                lblEmployeeInitial.Text = lblEmployeeName.Text.Substring(0,1);

                if (string.IsNullOrEmpty(Convert.ToString(Session["LOGIN_TIME"])))
                {
                    //Session["LOGIN_TIME"] = DateTime.Now.ToString("hh:mm tt");
                    Session["LOGIN_TIME"] = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "India Standard Time").ToString("hh:mm tt");

                }
                lblLoginTime.Text = Convert.ToString(Session["LOGIN_TIME"]);
                lblPunchIn.Text = Convert.ToString(Session["PUNCH_IN"]);

                if (string.IsNullOrEmpty(lblPunchIn.Text))
                    lblPunchIn.Text = lblLoginTime.Text;



                BindMenuNew();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    //private void BindMenuNew()
    //{
    //    try
    //    {
    //        dsMenu = objCommon.GetAssignedMenuList(Convert.ToInt32(Session["EMP_RECORD_ID"]));
    //        if (dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow drMenu in dsMenu.Tables[0].Rows)
    //            {
    //                int menuID = Convert.ToInt32(drMenu["MENU_ID"]);
    //                if (Convert.ToInt32(drMenu["PARENT_ID"]) == 0)
    //                {
    //                    MenuItem MainMenu = new MenuItem(Convert.ToString(drMenu["MENU_NAME"]));
    //                    MainMenu.NavigateUrl = Convert.ToString(drMenu["URL"]);
    //                    Menu1.Items.Add(MainMenu);

    //                    foreach (DataRow drChildMenu in dsMenu.Tables[0].Select("PARENT_ID='" + menuID + "'"))
    //                    {
    //                        MenuItem ChildMenu = new MenuItem(Convert.ToString(drChildMenu["MENU_NAME"]));
    //                        ChildMenu.NavigateUrl = Convert.ToString(drChildMenu["URL"]);
    //                        MainMenu.ChildItems.Add(ChildMenu);

    //                        foreach (DataRow drChildMenuOne in dsMenu.Tables[0].Select("PARENT_ID='" + Convert.ToInt32(drChildMenu["MENU_ID"]) + "'"))
    //                        {
    //                            MenuItem ChildMenuOne = new MenuItem(Convert.ToString(drChildMenuOne["MENU_NAME"]));
    //                            ChildMenuOne.NavigateUrl = Convert.ToString(drChildMenuOne["URL"]);
    //                            ChildMenu.ChildItems.Add(ChildMenuOne);

    //                            foreach (DataRow drChildMenuTwo in dsMenu.Tables[0].Select("PARENT_ID='" + Convert.ToInt32(drChildMenuOne["MENU_ID"]) + "'"))
    //                            {
    //                                MenuItem ChildMenuTwo = new MenuItem(Convert.ToString(drChildMenuTwo["MENU_NAME"]));
    //                                ChildMenuTwo.NavigateUrl = Convert.ToString(drChildMenuTwo["URL"]);
    //                                ChildMenuOne.ChildItems.Add(ChildMenuTwo);

    //                                foreach (DataRow drChildMenuThree in dsMenu.Tables[0].Select("PARENT_ID='" + Convert.ToInt32(drChildMenuTwo["MENU_ID"]) + "'"))
    //                                {
    //                                    MenuItem ChildMenuThree = new MenuItem(Convert.ToString(drChildMenuThree["MENU_NAME"]));
    //                                    ChildMenuThree.NavigateUrl = Convert.ToString(drChildMenuThree["URL"]);
    //                                    ChildMenuTwo.ChildItems.Add(ChildMenuThree);

    //                                    foreach (DataRow drChildMenuFour in dsMenu.Tables[0].Select("PARENT_ID='" + Convert.ToInt32(drChildMenuThree["MENU_ID"]) + "'"))
    //                                    {
    //                                        MenuItem ChildMenuFour = new MenuItem(Convert.ToString(drChildMenuFour["MENU_NAME"]));
    //                                        ChildMenuFour.NavigateUrl = Convert.ToString(drChildMenuFour["URL"]);
    //                                        ChildMenuThree.ChildItems.Add(ChildMenuFour);

    //                                        foreach (DataRow drChildMenuFive in dsMenu.Tables[0].Select("PARENT_ID='" + Convert.ToInt32(drChildMenuFour["MENU_ID"]) + "'"))
    //                                        {
    //                                            MenuItem ChildMenuFive = new MenuItem(Convert.ToString(drChildMenuFive["MENU_NAME"]));
    //                                            ChildMenuFive.NavigateUrl = Convert.ToString(drChildMenuFive["URL"]);
    //                                            ChildMenuFour.ChildItems.Add(ChildMenuFive);
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //
    //    }
    //}



    private void BindMenuNew()
    {
        try
        {
            int employeeId = Convert.ToInt32(Session["EMP_RECORD_ID"]);

            dsMenu = objCommon.GetAssignedMenuList(employeeId);

            if (dsMenu == null ||
                dsMenu.Tables.Count == 0 ||
                dsMenu.Tables[0].Rows.Count == 0)
            {
                MenuContainer.InnerHtml = string.Empty;
                return;
            }

            DataTable dtMenu = dsMenu.Tables[0];

            StringBuilder sb = new StringBuilder();

            sb.Append("<ul class='menu-tree'>");

            DataRow[] rootMenus = dtMenu.Select("PARENT_ID = 0");

            foreach (DataRow row in rootMenus)
            {
                BuildMenuItem(sb, dtMenu, row);
            }

            sb.Append("</ul>");

            MenuContainer.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            // Log exception
        }
    }

    private void BuildMenuItem(
    StringBuilder sb,
    DataTable dtMenu,
    DataRow menuRow)
    {
        int menuId = Convert.ToInt32(menuRow["MENU_ID"]);

        string menuName = Convert.ToString(menuRow["MENU_NAME"]);
        string url = Convert.ToString(menuRow["TRIMMED_URL"]);
        
        DataRow[] children = dtMenu.Select(
            "PARENT_ID = " + menuId
        );

        bool hasChildren = children.Length > 0;

        sb.Append("<li");

        if (hasChildren)
        {
            sb.Append(" class='has-children'");
        }

        sb.Append(">");

        // ==============================
        // MENU HEADER
        // ==============================

        if (hasChildren)
        {
            sb.Append("<div class='menu-link menu-toggle'>");

            sb.Append("<span class='menu-text'>");
            sb.Append(HttpUtility.HtmlEncode(menuName));
            sb.Append("</span>");

            sb.Append("<span class='menu-arrow'>");
            sb.Append("›");
            sb.Append("</span>");

            sb.Append("</div>");
        }
        else
        {
            sb.Append("<a class='menu-link' href='");
            sb.Append(HttpUtility.HtmlAttributeEncode(url));
            sb.Append("'>");

            sb.Append("<span class='menu-text'>");
            sb.Append(HttpUtility.HtmlEncode(menuName));
            sb.Append("</span>");

            sb.Append("</a>");
        }

        // ==============================
        // CHILDREN
        // ==============================

        if (hasChildren)
        {
            sb.Append("<ul class='submenu'>");

            foreach (DataRow childRow in children)
            {
                BuildMenuItem(sb, dtMenu, childRow);
            }

            sb.Append("</ul>");
        }

        sb.Append("</li>");
    }
}
