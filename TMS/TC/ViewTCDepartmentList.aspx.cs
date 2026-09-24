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

public partial class TC_ViewTCDepartmentList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.TC objTC = new BAL.TC();

    DataSet dsDepartment = new DataSet();
    DataSet dsUnit = new DataSet();

    int departmentID = 0;
    int unitID = 0;
    string department = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                BindCompany();
                GetDepartmentList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDepartmentList();
    }

    protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblDepartmentID = gvDepartment.Rows[rowindex].FindControl("lblDepartmentID") as Label;
                Label lblDepartment = gvDepartment.Rows[rowindex].FindControl("lblDepartment") as Label;
                Label lblUnitID = gvDepartment.Rows[rowindex].FindControl("lblUnitID") as Label;


                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblDepartmentID.Text))
                        ViewState["TC_DEPARTMENT_ID"] = Convert.ToInt32(lblDepartmentID.Text);
                    else
                        ViewState["TC_DEPARTMENT_ID"] = 0;

                    ddlUnitToUpdate.SelectedValue = Convert.ToString(lblUnitID.Text);

                    if (!string.IsNullOrEmpty(lblDepartment.Text))
                        txtDepartmentToUpdate.Text = lblDepartment.Text;
                    else
                        txtDepartmentToUpdate.Text = string.Empty;

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

    protected void btnAddNewDepartment_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TC/AddTCDepartment.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateLOTMainItem();
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
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;

                ddlUnitToUpdate.DataSource = dsUnit.Tables[0];
                ddlUnitToUpdate.DataTextField = "UNIT_NAME";
                ddlUnitToUpdate.DataValueField = "UNIT_ID";
                ddlUnitToUpdate.DataBind();
                ddlUnitToUpdate.Items.Insert(0, "Select");
                ddlUnitToUpdate.SelectedIndex = 0;
            }
            else
            {
                ddlUnit.DataSource = null;
                ddlUnit.Items.Clear();

                ddlUnitToUpdate.DataSource = null;
                ddlUnitToUpdate.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetDepartmentList()
    {
        try
        {
            unitID = 0;
            department = string.Empty;

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (!string.IsNullOrEmpty(txtDepartment.Text))
                department = txtDepartment.Text;

            dsDepartment = objTC.GetTCDepartmentList(unitID, department);

            if (dsDepartment.Tables.Count > 0 && dsDepartment.Tables[0].Rows.Count > 0)
            {
                gvDepartment.DataSource = dsDepartment.Tables[0];
                gvDepartment.DataBind();
            }
            else
            {
                gvDepartment.DataSource = null;
                gvDepartment.DataBind();
            }
            lblRecords.Text = "Records[" + dsDepartment.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateLOTMainItem()
    {
        try
        {
            departmentID = 0;
            unitID = 0;
            department = string.Empty;

            if (ViewState["TC_DEPARTMENT_ID"] != null)
                departmentID = Convert.ToInt32(ViewState["TC_DEPARTMENT_ID"]);

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (!string.IsNullOrEmpty(Convert.ToString(txtDepartment.Text)))
                department = Convert.ToString(txtDepartment.Text);


            int value = 0;
            if (departmentID > 0)
            {
                value = objTC.AddUpdateDepartment(departmentID, unitID, department, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            }


            if (value > 0)
            {
                SuccessMessage("Department updated successfully...!!!");
                GetDepartmentList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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

    private void HidePanel()
    {
        pnlMsg.Visible = false;
        lblMsg.Text = string.Empty;
    }

    #endregion

}