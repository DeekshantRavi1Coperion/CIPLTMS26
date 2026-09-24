using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_ApproversList : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();
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

                GetEntityTypes();
                BindEntityTypes();

                GetApproverTypes();
                BindApproverTypes();

                
                BindApprovers();
                BindApproversToEdit();

                GetApproversList();
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
        GetApproversList();
    }

    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }

                Label lblIsDeleted = (Label)e.Row.FindControl("lblIsDeleted");
                ImageButton imgIsActive = (ImageButton)e.Row.FindControl("imgIsActive");

                if (Convert.ToInt32(lblIsDeleted.Text) == 0)
                {
                    imgIsActive.ImageUrl = "~/Images/VCM/on.png";
                    imgIsActive.ToolTip = "Active";
                }
                else
                {
                    imgIsActive.ImageUrl = "~/Images/VCM/off.png";
                    imgIsActive.ToolTip = "Inactive";
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void gvEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblPID = gvEmployeeList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblEntityTypeFID = gvEmployeeList.Rows[rowindex].FindControl("lblEntityTypeFID") as Label;
                Label lblApproverTypeFID = gvEmployeeList.Rows[rowindex].FindControl("lblApproverTypeFID") as Label;
                Label lblApproverID = gvEmployeeList.Rows[rowindex].FindControl("lblApproverID") as Label;
                Label lblIsDeleted = gvEmployeeList.Rows[rowindex].FindControl("lblIsDeleted") as Label;

                ViewState["PID"] = Convert.ToInt32(lblPID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    BindEntityTypesToS();
                    ddlEntityTypeToS.SelectedValue = Convert.ToString(lblEntityTypeFID.Text);

                    BindApproverTypesToS();
                    ddlApproverTypeToS.SelectedValue = Convert.ToString(lblApproverTypeFID.Text);

                    BindApproversToS();
                    ddlApproverToS.SelectedValue = Convert.ToString(lblApproverID.Text);

                    chkIsActiveToS.Checked = false;

                    if (Convert.ToInt32(lblIsDeleted.Text) == 0)
                    {
                        chkIsActiveToS.Checked = true;
                    }


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
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/AddApprovers.aspx");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateApprover();
    }


    #endregion


    #region METHODS[=======================]

    private void GetEntityTypes()
    {
        try
        {
            dsEntityType = objVCM.GetEntityTypes();
            if (dsEntityType.Tables.Count > 0 && dsEntityType.Tables[0].Rows.Count > 0)
            {
                Session["dtEntityType"] = dsEntityType.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    private void BindEntityTypes()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtEntityType"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlEntityType.DataSource = dtDropdown;
                ddlEntityType.DataTextField = "NAME";
                ddlEntityType.DataValueField = "PID";
                ddlEntityType.DataBind();
                ddlEntityType.Items.Insert(0, "All");
                ddlEntityType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }
    private void BindEntityTypesToS()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtEntityType"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlEntityTypeToS.DataSource = dtDropdown;
                ddlEntityTypeToS.DataTextField = "NAME";
                ddlEntityTypeToS.DataValueField = "PID";
                ddlEntityTypeToS.DataBind();
                ddlEntityTypeToS.Items.Insert(0, "Select");
                ddlEntityTypeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void GetApproverTypes()
    {
        try
        {
            dsApproverType = objVCM.GettApproverTypes();
            if (dsApproverType.Tables.Count > 0 && dsApproverType.Tables[0].Rows.Count > 0)
            {
                Session["dtApproverType"] = dsApproverType.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindApproverTypes()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtApproverType"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlApproverType.DataSource = dtDropdown;
                ddlApproverType.DataTextField = "NAME";
                ddlApproverType.DataValueField = "PID";
                ddlApproverType.DataBind();
                ddlApproverType.Items.Insert(0, "All");
                ddlApproverType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindApproverTypesToS()
    {
        try
        {
            DataTable dtDropdown = (DataTable)Session["dtApproverType"];
            if (dtDropdown.Rows.Count > 0)
            {
                ddlApproverTypeToS.DataSource = dtDropdown;
                ddlApproverTypeToS.DataTextField = "NAME";
                ddlApproverTypeToS.DataValueField = "PID";
                ddlApproverTypeToS.DataBind();
                ddlApproverTypeToS.Items.Insert(0, "Select");
                ddlApproverTypeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    //private void GetApprovers()
    //{
    //    try
    //    {
    //        Session["dtEmployees"] = null;
    //        dsEmployees = objVCM.GetApprovers(0);
    //        if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
    //        {
    //            Session["dtEmployees"] = dsEmployees.Tables[0];
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionMessage(ex.ToString());
    //        return;
    //    }
    //}

    private void BindApprovers()
    {
        try
        {
            DataSet dsApprovers = new DataSet();
            dsApprovers = objVCM.GetApprovers(0);

            if (dsApprovers.Tables.Count > 0 && dsApprovers.Tables[0].Rows.Count > 0)
            {
                ddlApprover.DataSource = dsApprovers.Tables[0];
                ddlApprover.DataTextField = "EMPLOYEE_NAME";
                ddlApprover.DataValueField = "EMP_RECORD_FID";
                ddlApprover.DataBind();
                ddlApprover.Items.Insert(0, "All");
                ddlApprover.SelectedIndex = 0;
            }
            else
            {
                ddlApprover.Items.Insert(0, "All");
                ddlApprover.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void BindApproversToEdit()
    {
        try
        {
            dsEmployees = objCommon.GetTeamLeadersByID(0);
            if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
            {
                ddlApproverToS.DataSource = dsEmployees.Tables[0];
                ddlApproverToS.DataTextField = "EMPLOYEE_NAME";
                ddlApproverToS.DataValueField = "EMP_RECORD_ID";
                ddlApproverToS.DataBind();
                ddlApproverToS.Items.Insert(0, "Select");
                ddlApproverToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindApproversToS()
    {
        try
        {
            DataTable dtDropdown = new DataTable();
            if (Session["dtEmployees"] != null)
                dtDropdown = (DataTable)Session["dtEmployees"];

            if (dtDropdown.Rows.Count > 0)
            {
                ddlApproverToS.DataSource = dtDropdown;
                ddlApproverToS.DataTextField = "EMPLOYEE_NAME";
                ddlApproverToS.DataValueField = "EMP_RECORD_FID";
                ddlApproverToS.DataBind();
                ddlApproverToS.Items.Insert(0, "Select");
                ddlApproverToS.SelectedIndex = 0;
            }
            else
            {
                ddlApprover.Items.Insert(0, "Select");
                ddlApprover.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetApproversList()
    {
        try
        {
            int entityTypeId = 0;
            int approverTypeId = 0;
            int approverId = 0;

            if (ddlEntityType.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityType.SelectedValue);
            if (ddlApproverType.SelectedIndex > 0) approverTypeId = Convert.ToInt32(ddlApproverType.SelectedValue);
            if (ddlApprover.SelectedIndex > 0) approverId = Convert.ToInt32(ddlApprover.SelectedValue);

            dsList = objVCM.GetApproversList(entityTypeId, approverTypeId, approverId);
            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                Session["dtList"] = dsList.Tables[0];
                gvEmployeeList.DataSource = dsList.Tables[0];
                gvEmployeeList.DataBind();
            }
            else
            {
                Session["dtList"] = null;
                gvEmployeeList.DataSource = null;
                gvEmployeeList.DataBind();
            }
            lblRecords.Text = "Records[" + dsList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateApprover()
    {
        try
        {
            int PID = Convert.ToInt32(ViewState["PID"]);
            int entityTypeId = 0;
            int approverTypeId = 0;
            int approverId = 0;
            int isDeleted = 1;

            if (ddlEntityTypeToS.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityTypeToS.SelectedValue);
            if (ddlApproverTypeToS.SelectedIndex > 0) approverTypeId = Convert.ToInt32(ddlApproverTypeToS.SelectedValue);
            if (ddlApproverToS.SelectedIndex > 0) approverId = Convert.ToInt32(ddlApproverToS.SelectedValue);
            if (chkIsActiveToS.Checked) isDeleted = 0;

            int value = objVCM.AddUpdateApprover(PID, entityTypeId, approverTypeId, approverId, isDeleted, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                GetApproversList();
                SuccessMessage("Approver updated successfully");
                GetApproversList();
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
    #endregion

}
