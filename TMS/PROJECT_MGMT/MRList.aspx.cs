using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_MRList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsEstimatedItem = new DataSet();
    DataSet dsProject = new DataSet();
    DataSet dsMRList = new DataSet();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();
                
                
                BindProjectNo();



                GetMRList();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        HidePanel();
        pnlMsg.Visible = false;
        GetMRList();
    }

    protected void btnAddNewMR_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/AddNewMR.aspx");
    }

    protected void gvMRList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        HidePanel();
        gvMRList.PageIndex = e.NewPageIndex;
        GetMRList();
    }

    protected void gvMRList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            HidePanel();
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblMRID = gvMRList.Rows[rowindex].FindControl("lblMRID") as Label;

                if (e.CommandArgument == "PROPERTIES")
                {
                    ModalPopupExtender1.Show();
                    iframeUpdateMR.Attributes.Add("src", "UpdateMR.aspx?mrID=" + Convert.ToString(lblMRID.Text));
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

    protected void gvMRList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMRCode = (Label)e.Row.FindControl("lblMRCode");

                e.Row.ToolTip = lblMRCode.Text;

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap;");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    #endregion


    #region METHODS[=======================]

    private void BindProjectNo()
    {
        try
        {
            dsProject = objProject.GetDetailsBySP("get_estimated_projects");
            if (dsProject.Tables.Count > 0 && dsProject.Tables[0].Rows.Count > 0)
            {
                ddlProjectNo.DataSource = dsProject.Tables[0];
                ddlProjectNo.DataTextField = "PROJECT_NO";
                ddlProjectNo.DataValueField = "PROJECT_NO";
                ddlProjectNo.DataBind();
                ddlProjectNo.Items.Insert(0, "Select");
                ddlProjectNo.SelectedIndex = 0;
            }
            else
            {
                ddlProjectNo.Items.Clear();
                ddlProjectNo.Items.Insert(0, "Select");
                ddlProjectNo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetMRList()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string projectNo = string.Empty;
            string productCode = string.Empty;
            string mRNo = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdStartDateSearch.Value)))
                startDate = Convert.ToDateTime(hdStartDateSearch.Value).ToString("yyyy-MM-dd");
            else
                startDate = string.Empty;

            if (!string.IsNullOrEmpty(Convert.ToString(hdEndDateSearch.Value)))
                endDate = Convert.ToDateTime(hdEndDateSearch.Value).ToString("yyyy-MM-dd");
            else
                endDate = string.Empty;

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text.Trim();
            else
                productCode = string.Empty;

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text.Trim();
            else
                productCode = string.Empty;

            if (!string.IsNullOrEmpty(txtMRNo.Text))
                mRNo = txtMRNo.Text.Trim();
            else
                mRNo = string.Empty;

            dsMRList = objProject.GetMRList(startDate, endDate, projectNo, productCode, mRNo);
            if (dsMRList.Tables.Count > 0 && dsMRList.Tables[0].Rows.Count > 0)
            {
                gvMRList.DataSource = dsMRList.Tables[0];
                gvMRList.DataBind();
            }
            else
            {
                gvMRList.DataSource = null;
                gvMRList.DataBind();
            }
            lblRecords.Text = "Records[" + gvMRList.Rows.Count + "]";
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