using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_OpsTypeList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    DataSet dsFinOpsType = new DataSet();

    int typeID = 0;
    string type = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetFinOpsTypeList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetFinOpsTypeList();
    }

    protected void gvOpsTypeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;

                Label lbltypeID = gvOpsTypeList.Rows[rowindex].FindControl("lbltypeID") as Label;
                Label lblType = gvOpsTypeList.Rows[rowindex].FindControl("lblType") as Label;

                ViewState["typeID"] = lbltypeID.Text;
                txtTypeToEdit.Text = lblType.Text;
                modalPopupExtenderType.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOpsTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        Response.Redirect("~/FINOPS/AddOpsType.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void GetFinOpsTypeList()
    {
        try
        {
            type = string.Empty;
         
            if (!string.IsNullOrEmpty(txtType.Text))
                type = Convert.ToString(txtType.Text);


            dsFinOpsType = objFinOps.GetFinOpsTypeList(type);
            if (dsFinOpsType.Tables.Count > 0 && dsFinOpsType.Tables[0].Rows.Count > 0)
            {
                gvOpsTypeList.DataSource = dsFinOpsType.Tables[0];
                gvOpsTypeList.DataBind();
            }
            else
            {
                gvOpsTypeList.DataSource = null;
                gvOpsTypeList.DataBind();
            }
            lblRecords.Text = "Records[" + dsFinOpsType.Tables[0].Rows.Count + "]";
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
            typeID = 0;
            type = string.Empty;

            if (Convert.ToInt32(ViewState["typeID"]) > 0)
                typeID = Convert.ToInt32(ViewState["typeID"]);

            if (!string.IsNullOrEmpty(txtTypeToEdit.Text))
                type = Convert.ToString(txtTypeToEdit.Text);

            int value = objFinOps.AddUpdateFinOpsType(typeID, type, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Type updated successfully...!!");
                GetFinOpsTypeList();
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
