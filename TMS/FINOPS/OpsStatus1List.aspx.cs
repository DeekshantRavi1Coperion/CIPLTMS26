using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_OpsStatus1List : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    DataSet dsOpsCpstType = new DataSet();

    int opsStatus1ID = 0;
    string opsStatus1 = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetOpsStatus1List();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOpsStatus1List();
    }

    protected void gvOpsStatus1List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;

                Label lblStatus1ID = gvOpsStatus1List.Rows[rowindex].FindControl("lblStatus1ID") as Label;
                Label lblStatus1 = gvOpsStatus1List.Rows[rowindex].FindControl("lblStatus1") as Label;

                ViewState["Status1ID"] = lblStatus1ID.Text;
                txtOpsStatus1ToEdit.Text = lblStatus1.Text;
                modalPopupExtenderOpsType.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOpsStatus1List_RowDataBound(object sender, GridViewRowEventArgs e)
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
        Response.Redirect("~/FINOPS/AddOpsStatus1.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void GetOpsStatus1List()
    {
        try
        {
            opsStatus1 = string.Empty;
         
            if (!string.IsNullOrEmpty(txtOpsStatus1.Text))
                opsStatus1 = Convert.ToString(txtOpsStatus1.Text);

            dsOpsCpstType = objFinOps.GetFinOpsStatus1List(opsStatus1);
            if (dsOpsCpstType.Tables.Count > 0 && dsOpsCpstType.Tables[0].Rows.Count > 0)
            {
                gvOpsStatus1List.DataSource = dsOpsCpstType.Tables[0];
                gvOpsStatus1List.DataBind();
            }
            else
            {
                gvOpsStatus1List.DataSource = null;
                gvOpsStatus1List.DataBind();
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
            opsStatus1ID = 0;
            opsStatus1 = string.Empty;

            if (Convert.ToInt32(ViewState["Status1ID"]) > 0)
                opsStatus1ID = Convert.ToInt32(ViewState["Status1ID"]);

            if (!string.IsNullOrEmpty(txtOpsStatus1ToEdit.Text))
                opsStatus1 = Convert.ToString(txtOpsStatus1ToEdit.Text);

            int value = objFinOps.AddUpdateOpsStatus1(opsStatus1ID, opsStatus1, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Ops Type updated successfully...!!");
                GetOpsStatus1List();
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
