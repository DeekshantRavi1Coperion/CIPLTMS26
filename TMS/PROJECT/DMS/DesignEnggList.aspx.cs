using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class PROJECT_DMS_DesignEnggList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsDept = new DataSet();
    DataSet dsEmp = new DataSet();

    #endregion



    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanels();
            if (!IsPostBack)
            {
                BindDepartmentForDMS();
                BindDesignEnggList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDesignEnggList();
    }

    protected void gvEnggList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/DMS/AddDesignEngg.aspx");
    }


    #endregion


    #region METHODS[=======================]

    private void BindDepartmentForDMS()
    {
        try
        {
            dsDept = objProject.GetDepartmentForDMS();
            if (dsDept.Tables.Count > 0 && dsDept.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDept.Tables[0];
                ddlDepartment.DataTextField = "DEPARTMENT_NAME";
                ddlDepartment.DataValueField = "DEPARTMENT_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "All");
                ddlDepartment.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDesignEnggList()
    {
        try
        {
            string enggName = string.Empty;
            int deptID = 0;

            if (!string.IsNullOrEmpty(txtEnggName.Text))
                enggName = txtEnggName.Text;

            if (ddlDepartment.SelectedIndex > 0)
                deptID = Convert.ToInt32(ddlDepartment.SelectedValue);
            
            dsEmp = objProject.GetDesignResponsibleEnggList(enggName, deptID);
            if (dsEmp.Tables.Count > 0 && dsEmp.Tables[0].Rows.Count > 0)
            {
                gvEnggList.DataSource = dsEmp.Tables[0];
                gvEnggList.DataBind();
            }
            else
            {
                gvEnggList.DataSource = null;
                gvEnggList.DataBind();
            }

            lblRecords.Text = "Records[" + gvEnggList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
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
    }
    #endregion

}
