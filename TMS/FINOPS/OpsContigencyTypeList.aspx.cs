using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_OpsContigencyTypeList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    DataSet dsOpsContigencyType = new DataSet();

    int opsContigencyTypeID = 0;
    string opsContigencyType = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetopsContigencyTypeList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetopsContigencyTypeList();
    }

    protected void gvOpsContigencyTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;

                Label lblContigencyTypeID = gvOpsContigencyTypeList.Rows[rowindex].FindControl("lblContigencyTypeID") as Label;
                Label lblContigencyType = gvOpsContigencyTypeList.Rows[rowindex].FindControl("lblContigencyType") as Label;

                ViewState["contigencyTypeID"] = lblContigencyTypeID.Text;
                txtOpsContigencyTypeToEdit.Text = lblContigencyType.Text;
                modalPopupExtenderOpsType.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOpsContigencyTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateOpsType();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FINOPS/AddopsContigencyType.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void GetopsContigencyTypeList()
    {
        try
        {
            opsContigencyType = string.Empty;
         
            if (!string.IsNullOrEmpty(txtOpsContigencyType.Text))
                opsContigencyType = Convert.ToString(txtOpsContigencyType.Text);

            dsOpsContigencyType = objFinOps.GetOpsContigencyTypeList(opsContigencyType);
            if (dsOpsContigencyType.Tables.Count > 0 && dsOpsContigencyType.Tables[0].Rows.Count > 0)
            {
                gvOpsContigencyTypeList.DataSource = dsOpsContigencyType.Tables[0];
                gvOpsContigencyTypeList.DataBind();
            }
            else
            {
                gvOpsContigencyTypeList.DataSource = null;
                gvOpsContigencyTypeList.DataBind();
            }
            lblRecords.Text = "Records[" + dsOpsContigencyType.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateOpsType()
    {
        try
        {
            opsContigencyTypeID = 0;
            opsContigencyType = string.Empty;

            if (Convert.ToInt32(ViewState["contigencyTypeID"]) > 0)
                opsContigencyTypeID = Convert.ToInt32(ViewState["contigencyTypeID"]);

            if (!string.IsNullOrEmpty(txtOpsContigencyTypeToEdit.Text))
                opsContigencyType = Convert.ToString(txtOpsContigencyTypeToEdit.Text);

            int value = objFinOps.AddUpdateOpsContigencyType(opsContigencyTypeID, opsContigencyType, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Ops Contigency Type updated successfully...!!");
                GetopsContigencyTypeList();
                return;
            }
            else
            {
                ExceptionMessage("Please try again...!!");
                return;
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
