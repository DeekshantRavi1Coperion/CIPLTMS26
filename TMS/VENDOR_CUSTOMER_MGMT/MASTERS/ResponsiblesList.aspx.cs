using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_ResponsiblesList : System.Web.UI.Page
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
                GetEmployees();
                BindEmployees();

                GetResponsiblesList();
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
        GetResponsiblesList();
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
                Label lblEmployeeRecordId = gvMastersList.Rows[rowindex].FindControl("lblEmployeeRecordId") as Label;
                Label lblName = gvMastersList.Rows[rowindex].FindControl("lblName") as Label;
                Label lblDescription = gvMastersList.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblIsDeleted = gvMastersList.Rows[rowindex].FindControl("lblIsDeleted") as Label;

                ViewState["PID"] = Convert.ToInt32(lblPID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {

                    BindEmployeesToS();
                    if (Convert.ToInt32(lblEmployeeRecordId.Text) > 0)
                    {
                        ddlEmployeeToS.SelectedValue = Convert.ToString(lblEmployeeRecordId.Text);
                    }


                    txtShortNameToS.Text = lblName.Text;
                    txtFullNameToS.Text = lblDescription.Text;

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
        Response.Redirect("~/VENDOR_CUSTOMER_MGMT/MASTERS/AddResponsibles.aspx");
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateResponsible();
    }


    #endregion


    #region METHODS[=======================]

    private void GetEmployees()
    {
        try
        {

            dsEmployees = objCommon.GetEmployeeByEmpRecordID(0);
            if (dsEmployees.Tables.Count > 0 && dsEmployees.Tables[0].Rows.Count > 0)
            {
                Session["dtEmployees"] = dsEmployees.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindEmployees()
    {
        try
        {
            DataTable dtDropdown = new DataTable();
            if (Session["dtEmployees"] != null)
                dtDropdown = (DataTable)Session["dtEmployees"];

            if (dtDropdown.Rows.Count > 0)
            {
                ddlEmployee.DataSource = dtDropdown;
                ddlEmployee.DataTextField = "EMPLOYEE_NAME";
                ddlEmployee.DataValueField = "EMP_RECORD_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, "Select");
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void BindEmployeesToS()
    {
        try
        {
            DataTable dtDropdown = new DataTable();
            if (Session["dtEmployees"] != null)
                dtDropdown = (DataTable)Session["dtEmployees"];

            if (dtDropdown.Rows.Count > 0)
            {
                ddlEmployeeToS.DataSource = dtDropdown;
                ddlEmployeeToS.DataTextField = "EMPLOYEE_NAME";
                ddlEmployeeToS.DataValueField = "EMP_RECORD_ID";
                ddlEmployeeToS.DataBind();
                ddlEmployeeToS.Items.Insert(0, "Select");
                ddlEmployeeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }




    private void GetResponsiblesList()
    {
        try
        {
            int employeId = 0;
            string name = string.Empty;
            string description = string.Empty;

            if (ddlEmployee.SelectedIndex > 0) employeId = Convert.ToInt32(ddlEmployee.SelectedValue);

            if (!string.IsNullOrEmpty(txtShortName.Text))
                name = Convert.ToString(txtShortName.Text);

            if (!string.IsNullOrEmpty(txtFullName.Text))
                description = Convert.ToString(txtFullName.Text);

            dsList = objVCM.GetResponsiblesList(employeId, name, description);
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



    private void UpdateResponsible()
    {
        try
        {
            int pid = Convert.ToInt32(ViewState["PID"]);
            int employeeId = 0;
            string name = string.Empty;
            string description = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);
            int isDeleted = 1;

            if (ddlEmployeeToS.SelectedIndex > 0) employeeId = Convert.ToInt32(ddlEmployeeToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtShortNameToS.Text)) name = txtShortNameToS.Text;
            if (!string.IsNullOrEmpty(txtFullNameToS.Text)) description = txtFullNameToS.Text;
            if (chkIsActiveToS.Checked) isDeleted = 0;

            int value = objVCM.AddUpdateRespnsible(pid, employeeId, name, description, isDeleted, createdBy);
            if (value > 0)
            {
                GetResponsiblesList();
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
        ddlEmployeeToS.SelectedIndex = 0;
        txtFullNameToS.Text = string.Empty;
        txtShortNameToS.Text = string.Empty;
    }

    #endregion

}
