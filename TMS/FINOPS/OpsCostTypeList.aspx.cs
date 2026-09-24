using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_OpsCostTypeList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    DataSet dsOpsCpstType = new DataSet();

    int opsCostTypeID = 0;
    string opsCostType = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetOpsCostTypeList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOpsCostTypeList();
    }

    protected void gvOpsCostTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;

                Label lblCostTypeID = gvOpsCostTypeList.Rows[rowindex].FindControl("lblCostTypeID") as Label;
                Label lblCostType = gvOpsCostTypeList.Rows[rowindex].FindControl("lblCostType") as Label;

                ViewState["costTypeID"] = lblCostTypeID.Text;
                txtOpsCostTypeToEdit.Text = lblCostType.Text;
                modalPopupExtenderOpsType.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOpsCostTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        Response.Redirect("~/FINOPS/AddOpsCostType.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void GetOpsCostTypeList()
    {
        try
        {
            opsCostType = string.Empty;
         
            if (!string.IsNullOrEmpty(txtOpsCostType.Text))
                opsCostType = Convert.ToString(txtOpsCostType.Text);

            dsOpsCpstType = objFinOps.GetOpsCostTypeList(opsCostType);
            if (dsOpsCpstType.Tables.Count > 0 && dsOpsCpstType.Tables[0].Rows.Count > 0)
            {
                gvOpsCostTypeList.DataSource = dsOpsCpstType.Tables[0];
                gvOpsCostTypeList.DataBind();
            }
            else
            {
                gvOpsCostTypeList.DataSource = null;
                gvOpsCostTypeList.DataBind();
            }
            lblRecords.Text = "Records[" + dsOpsCpstType.Tables[0].Rows.Count + "]";
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
            opsCostTypeID = 0;
            opsCostType = string.Empty;

            if (Convert.ToInt32(ViewState["costTypeID"]) > 0)
                opsCostTypeID = Convert.ToInt32(ViewState["costTypeID"]);

            if (!string.IsNullOrEmpty(txtOpsCostTypeToEdit.Text))
                opsCostType = Convert.ToString(txtOpsCostTypeToEdit.Text);

            int value = objFinOps.AddUpdateOpsCostType(opsCostTypeID, opsCostType, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Ops Type updated successfully...!!");
                GetOpsCostTypeList();
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
