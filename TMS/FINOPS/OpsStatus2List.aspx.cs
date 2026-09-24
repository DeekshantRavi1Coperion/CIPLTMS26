using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FINOPS_OpsStatus2List : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.FinOps objFinOps = new BAL.FinOps();
    DataSet dsOpsCpstType = new DataSet();

    int opsStatus2ID = 0;
    string opsStatus2 = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetOpsStatus2List();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOpsStatus2List();
    }

    protected void gvOpsStatus2List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "PROPERTIES")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;

                Label lblStatus2ID = gvOpsStatus2List.Rows[rowindex].FindControl("lblStatus2ID") as Label;
                Label lblStatus2 = gvOpsStatus2List.Rows[rowindex].FindControl("lblStatus2") as Label;

                ViewState["Status2ID"] = lblStatus2ID.Text;
                txtOpsStatus2ToEdit.Text = lblStatus2.Text;
                modalPopupExtenderOpsType.Show();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void gvOpsStatus2List_RowDataBound(object sender, GridViewRowEventArgs e)
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
        Response.Redirect("~/FINOPS/AddOpsStatus2.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void GetOpsStatus2List()
    {
        try
        {
            opsStatus2 = string.Empty;
         
            if (!string.IsNullOrEmpty(txtOpsStatus2.Text))
                opsStatus2 = Convert.ToString(txtOpsStatus2.Text);

            dsOpsCpstType = objFinOps.GetFinOpsStatus2List(opsStatus2);
            if (dsOpsCpstType.Tables.Count > 0 && dsOpsCpstType.Tables[0].Rows.Count > 0)
            {
                gvOpsStatus2List.DataSource = dsOpsCpstType.Tables[0];
                gvOpsStatus2List.DataBind();
            }
            else
            {
                gvOpsStatus2List.DataSource = null;
                gvOpsStatus2List.DataBind();
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
            opsStatus2ID = 0;
            opsStatus2 = string.Empty;

            if (Convert.ToInt32(ViewState["Status2ID"]) > 0)
                opsStatus2ID = Convert.ToInt32(ViewState["Status2ID"]);

            if (!string.IsNullOrEmpty(txtOpsStatus2ToEdit.Text))
                opsStatus2 = Convert.ToString(txtOpsStatus2ToEdit.Text);

            int value = objFinOps.AddUpdateOpsStatus2(opsStatus2ID, opsStatus2, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("Ops Type updated successfully...!!");
                GetOpsStatus2List();
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
