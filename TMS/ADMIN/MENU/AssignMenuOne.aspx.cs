using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ADMIN_MENU_AssignMenuOne : System.Web.UI.Page
{

    #region VARIABLES[======================]

    DataSet dsEmployee = new DataSet();
    DataSet dsParentMenu = new DataSet();
    DataSet dsChildMenu = new DataSet();
    DataSet dsEmpMenu = new DataSet();
    BAL.Common objCommon = new BAL.Common();

    int menuID = 0;
    int childNodeOneID = 0;
    int childNodeTwoID = 0;
    int childNodeThreeID = 0;
    int childNodeFourID = 0;
    int childNodeFiveID = 0;
    int childNodeSixID = 0;

    #endregion


    #region EVENTS[=========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                Session["Employee"] = null;
                BindEmployee();
                BindMainMenu();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkSelectAll.Checked = false;
        ClearEmpMenu();
        if (ddlEmployeeName.SelectedIndex > 0)
        {
            BindEmpMenu();
        }
    }

    protected void tvMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (tvMenu.Nodes.Count > 0)
        {
            for (int i = 0; i < tvMenu.Nodes.Count; i++)
            {
                if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                {
                    for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                    {
                        if (tvMenu.Nodes[i].Checked)
                            tvMenu.Nodes[i].ChildNodes[j].Checked = true;

                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                        {
                            for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;

                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                {
                                    for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                    {
                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                            tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = true;

                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                        {
                                            for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                            {
                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked = true;

                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                {
                                                    for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; n++)
                                                    {
                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked)
                                                            tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked = true;

                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count > 0)
                                                        {
                                                            for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[n].ChildNodes.Count; o++)
                                                            {
                                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked)
                                                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            if (chkSelectAll.Checked)
            {
                if (tvMenu.Nodes.Count > 0)
                {
                    for (int i = 0; i < tvMenu.Nodes.Count; i++)
                    {
                        tvMenu.Nodes[i].Checked = true;

                        if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                            {
                                tvMenu.Nodes[i].ChildNodes[j].Checked = true;

                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                                {
                                    for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                    {
                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;

                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                        {
                                            for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                            {
                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = true;

                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                                {
                                                    for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                                    {
                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked = true;

                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                        {
                                                            for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count; n++)
                                                            {
                                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked = true;

                                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                                {
                                                                    for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count; o++)
                                                                    {
                                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked = true;                                                                            
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (tvMenu.Nodes.Count > 0)
                {
                    for (int i = 0; i < tvMenu.Nodes.Count; i++)
                    {
                        tvMenu.Nodes[i].Checked = false;

                        if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                            {
                                tvMenu.Nodes[i].ChildNodes[j].Checked = false;

                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                                {
                                    for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                    {
                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;

                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                        {
                                            for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                            {
                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = false;

                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                                {
                                                    for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                                    {
                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked = false;

                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                        {
                                                            for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count; n++)
                                                            {
                                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked = false;

                                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                                {
                                                                    for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count; o++)
                                                                    {
                                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked = false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        MenuAssignment();
        ClearEmpMenu();
        BindEmpMenu();
    }

    #endregion


    #region METHODS[========================]

    private void BindEmployee()
    {
        try
        {
            dsEmployee = objCommon.GetEmployeeByEmpRecordID(0);
            if (dsEmployee.Tables.Count > 0 && dsEmployee.Tables[0].Rows.Count > 0)
            {
                Session["Employee"] = dsEmployee.Tables[0];
                ddlEmployeeName.DataSource = dsEmployee.Tables[0];
                ddlEmployeeName.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeName.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeName.DataBind();
                ddlEmployeeName.Items.Insert(0, "Select");
                ddlEmployeeName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindMainMenu()
    {
        try
        {
            dsParentMenu = objCommon.GetAssignedMenuList(0);
            if (dsParentMenu.Tables.Count > 0 && dsParentMenu.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drMainMenu in dsParentMenu.Tables[0].Select("PARENT_ID=0"))
                {
                    TreeNode node = new TreeNode();
                    node.Text = Convert.ToString(drMainMenu["MENU_NAME"]);
                    node.Value = Convert.ToString(drMainMenu["MENU_ID"]);
                    tvMenu.Nodes.Add(node);

                    foreach (DataRow firstChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(drMainMenu["MENU_ID"]) + "'"))
                    {
                        TreeNode firstChildNode = new TreeNode();
                        firstChildNode.Text = Convert.ToString(firstChildMenu["MENU_NAME"]);
                        firstChildNode.Value = Convert.ToString(firstChildMenu["MENU_ID"]);
                        node.ChildNodes.Add(firstChildNode);

                        foreach (DataRow secondChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(firstChildMenu["MENU_ID"]) + "'"))
                        {
                            TreeNode secondChildNode = new TreeNode();
                            secondChildNode.Text = Convert.ToString(secondChildMenu["MENU_NAME"]);
                            secondChildNode.Value = Convert.ToString(secondChildMenu["MENU_ID"]);
                            firstChildNode.ChildNodes.Add(secondChildNode);

                            foreach (DataRow thirdChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(secondChildMenu["MENU_ID"]) + "'"))
                            {
                                TreeNode thirdChildNode = new TreeNode();
                                thirdChildNode.Text = Convert.ToString(thirdChildMenu["MENU_NAME"]);
                                thirdChildNode.Value = Convert.ToString(thirdChildMenu["MENU_ID"]);
                                secondChildNode.ChildNodes.Add(thirdChildNode);

                                foreach (DataRow fourthChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(thirdChildMenu["MENU_ID"]) + "'"))
                                {
                                    TreeNode fourthChildNode = new TreeNode();
                                    fourthChildNode.Text = Convert.ToString(fourthChildMenu["MENU_NAME"]);
                                    fourthChildNode.Value = Convert.ToString(fourthChildMenu["MENU_ID"]);
                                    thirdChildNode.ChildNodes.Add(fourthChildNode);

                                    foreach (DataRow fifthChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(fourthChildMenu["MENU_ID"]) + "'"))
                                    {
                                        TreeNode fifthChildNode = new TreeNode();
                                        fifthChildNode.Text = Convert.ToString(fifthChildMenu["MENU_NAME"]);
                                        fifthChildNode.Value = Convert.ToString(fifthChildMenu["MENU_ID"]);
                                        fourthChildNode.ChildNodes.Add(fifthChildNode);

                                        foreach (DataRow sixthChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(fifthChildMenu["MENU_ID"]) + "'"))
                                        {
                                            TreeNode sixthChildNode = new TreeNode();
                                            sixthChildNode.Text = Convert.ToString(sixthChildMenu["MENU_NAME"]);
                                            sixthChildNode.Value = Convert.ToString(sixthChildMenu["MENU_ID"]);
                                            fifthChildNode.ChildNodes.Add(sixthChildNode);


                                            foreach (DataRow seventhChildMenu in dsParentMenu.Tables[0].Select("PARENT_ID='" + Convert.ToString(sixthChildMenu["MENU_ID"]) + "'"))
                                            {
                                                TreeNode seventhChildNode = new TreeNode();
                                                seventhChildNode.Text = Convert.ToString(seventhChildMenu["MENU_NAME"]);
                                                seventhChildNode.Value = Convert.ToString(seventhChildMenu["MENU_ID"]);
                                                sixthChildNode.ChildNodes.Add(seventhChildNode);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void MenuAssignment()
    {
        try
        {
            string typeA = "A";
            string typeU = "U";
            int value = 0;

            if (tvMenu.Nodes.Count > 0)
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    //Main Menu Start-----------------------------
                    string menuname = Convert.ToString(tvMenu.Nodes[i].Text);
                    if (tvMenu.Nodes[i].Checked)
                    {
                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                        if (menuID > 0)
                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    }
                    else
                    {
                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                        if (menuID > 0)
                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                    }
                    //Main Menu End-----------------------------



                    if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                    {
                        for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                        {
                            //First Child Start-----------------------------
                            string childnodeone = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].Text);
                            if (tvMenu.Nodes[i].Checked)
                            {
                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                if (menuID > 0)
                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            }
                            else
                            {
                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                if (menuID > 0)
                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            }


                            if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                            {
                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                if (childNodeOneID > 0)
                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            }
                            else
                            {
                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                if (childNodeOneID > 0)
                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                            }

                            //First Child End-----------------------------



                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                            {
                                for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                {
                                    //Second Child Start-----------------------------
                                    string childnodetwo = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Text);
                                    if (tvMenu.Nodes[i].Checked)
                                    {
                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                        if (menuID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }
                                    else
                                    {
                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                        if (menuID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }

                                    if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                    {
                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                        if (childNodeOneID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }
                                    else
                                    {
                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                        if (childNodeOneID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }

                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                    {
                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                        if (childNodeTwoID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }
                                    else
                                    {
                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                        if (childNodeTwoID > 0)
                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                    }
                                    //Second Child End-----------------------------




                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                    {
                                        for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                        {
                                            //Third Child Start-----------------------------
                                            string childnodethree = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Text);
                                            if (tvMenu.Nodes[i].Checked)
                                            {
                                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                if (menuID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }
                                            else
                                            {
                                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                if (menuID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }

                                            if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                            {
                                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                if (childNodeOneID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }
                                            else
                                            {
                                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                if (childNodeOneID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }

                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                            {
                                                childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                if (childNodeTwoID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }
                                            else
                                            {
                                                childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                if (childNodeTwoID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }

                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                            {
                                                childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                if (childNodeThreeID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }
                                            else
                                            {
                                                childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                if (childNodeThreeID > 0)
                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                            }
                                            //Third Child End-----------------------------




                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                            {
                                                for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                                {
                                                    //Fourth Child Start-----------------------------
                                                    string childnodefour = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Text);
                                                    if (tvMenu.Nodes[i].Checked)
                                                    {
                                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                        if (menuID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }
                                                    else
                                                    {
                                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                        if (menuID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }

                                                    if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                                    {
                                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                        if (childNodeOneID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }
                                                    else
                                                    {
                                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                        if (childNodeOneID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }

                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                                    {
                                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                        if (childNodeTwoID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }
                                                    else
                                                    {
                                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                        if (childNodeTwoID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }

                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                                    {
                                                        childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                        if (childNodeThreeID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }
                                                    else
                                                    {
                                                        childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                        if (childNodeThreeID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }

                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked)
                                                    {
                                                        childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                        if (childNodeFourID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                    }
                                                    else
                                                    {
                                                        childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                        if (childNodeFourID > 0)
                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                                                    }
                                                    //Fourth Child End-----------------------------



                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                    {
                                                        for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; n++)
                                                        {
                                                            //Fifth Child Start-----------------------------
                                                            string childnodefive = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Text);
                                                            if (tvMenu.Nodes[i].Checked)
                                                            {
                                                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                                if (menuID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                                if (menuID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }

                                                            if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                                            {
                                                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                                if (childNodeOneID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                                if (childNodeOneID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }

                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                                            {
                                                                childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                                if (childNodeTwoID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                                if (childNodeTwoID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }

                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                                            {
                                                                childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                                if (childNodeThreeID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                                if (childNodeThreeID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }

                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked)
                                                            {
                                                                childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                                if (childNodeFourID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                                if (childNodeFourID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                                                            }

                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked)
                                                            {
                                                                childNodeFiveID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                if (childNodeFiveID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFiveID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            else
                                                            {
                                                                childNodeFiveID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                if (childNodeFiveID > 0)
                                                                    value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFiveID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                            }
                                                            //Fifth Child End-----------------------------



                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count > 0)
                                                            {
                                                                for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[n].ChildNodes.Count; o++)
                                                                {
                                                                    //Sixth Child Start-----------------------------
                                                                    string childnodesix = Convert.ToString(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Text);
                                                                    if (tvMenu.Nodes[i].Checked)
                                                                    {
                                                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                                        if (menuID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        menuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                                                        if (menuID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), menuID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                                                    {
                                                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                                        if (childNodeOneID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                                                        if (childNodeOneID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeOneID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                                                    {
                                                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                                        if (childNodeTwoID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                                        if (childNodeTwoID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeTwoID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                                                    {
                                                                        childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                                        if (childNodeThreeID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                                        if (childNodeThreeID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeThreeID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked)
                                                                    {
                                                                        childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                                        if (childNodeFourID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                                        if (childNodeFourID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFourID, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked)
                                                                    {
                                                                        childNodeFiveID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                        if (childNodeFiveID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFiveID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeFiveID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                        if (childNodeFiveID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeFiveID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }

                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked)
                                                                    {
                                                                        childNodeSixID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Value);
                                                                        if (childNodeSixID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeA, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeSixID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    else
                                                                    {
                                                                        childNodeSixID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Value);
                                                                        if (childNodeSixID > 0)
                                                                            value = objCommon.AssignUnassignEmployeeMenu(typeU, Convert.ToInt32(ddlEmployeeName.SelectedValue), childNodeSixID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                                                                    }
                                                                    //Sixth Child End-----------------------------
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    
                }
                if (value > 0)
                {
                    SuccessMessage("Menu assigned successfully..!");                    
                }
                else
                {
                    ExceptionMessage("Please try again..!");                    
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEmpMenu()
    {
        try
        {
            if (ddlEmployeeName.SelectedIndex > 0)
            {
                DataSet dsEmpMenuList = new DataSet();
                dsEmpMenuList = objCommon.GetEmpMenuList(Convert.ToInt32(ddlEmployeeName.SelectedValue));

                if (dsEmpMenuList.Tables.Count > 0 && dsEmpMenuList.Tables[0].Rows.Count > 0)
                {
                    int menuID = 0;
                    foreach (DataRow dr in dsEmpMenuList.Tables[0].Rows)
                    {
                        menuID = Convert.ToInt32(dr["MENU_ID"]);

                        if (tvMenu.Nodes.Count > 0)
                        {
                            for (int i = 0; i < tvMenu.Nodes.Count; i++)
                            {
                                int tvNodeID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                                if (menuID == tvNodeID)
                                {
                                    tvMenu.Nodes[i].Checked = true;
                                }

                                if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                                {
                                    for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                                    {
                                        int tvChildNodeOneID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                                        if (menuID == tvChildNodeOneID)
                                            tvMenu.Nodes[i].ChildNodes[j].Checked = true;

                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                                        {
                                            for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                            {
                                                int tvChildNodeTwoID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value);
                                                if (menuID == tvChildNodeTwoID)
                                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = true;

                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                                {
                                                    for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                                    {
                                                        int tvChildNodeThreeID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value);
                                                        if (menuID == tvChildNodeThreeID)
                                                            tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = true;

                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                                        {
                                                            for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                                            {
                                                                int tvChildNodeFourID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value);
                                                                if (menuID == tvChildNodeFourID)
                                                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked = true;

                                                                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                                {
                                                                    for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count; n++)
                                                                    {
                                                                        int tvChildNodeFiveID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                        if (menuID == tvChildNodeFiveID)
                                                                            tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked = true;

                                                                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                                        {
                                                                            for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count; o++)
                                                                            {
                                                                                int tvChildNodeSixID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value);
                                                                                if (menuID == tvChildNodeSixID)
                                                                                    tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked = true;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ClearEmpMenu()
    {
        try
        {
            if (tvMenu.Nodes.Count > 0)
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    if (tvMenu.Nodes[i].Checked)
                        tvMenu.Nodes[i].Checked = false;

                    if (tvMenu.Nodes[i].ChildNodes.Count > 0)
                    {
                        for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
                        {
                            if (tvMenu.Nodes[i].ChildNodes[j].Checked)
                                tvMenu.Nodes[i].ChildNodes[j].Checked = false;

                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                            {
                                for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                                {
                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked)
                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked = false;

                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                    {
                                        for (int l = 0; l < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                        {
                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked)
                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked = false;

                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count > 0)
                                            {
                                                for (int m = 0; m < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                                {
                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked)
                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked = false;

                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                    {
                                                        for (int n = 0; n < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count; n++)
                                                        {
                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked)
                                                                tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked = false;

                                                            if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count > 0)
                                                            {
                                                                for (int o = 0; o < tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count; o++)
                                                                {
                                                                    if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked)
                                                                        tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
