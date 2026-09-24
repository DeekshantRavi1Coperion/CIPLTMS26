using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class PROJECT_ProjectList : System.Web.UI.Page
{


    #region VARIABLES[=====================]

    DataSet dsStatus = new DataSet();
    BAL.EnggHours objEnggHours = new BAL.EnggHours();

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {


            if (!IsPostBack)
            {
                BindStatus();
                GetProjectList();

            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void gvProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProjectList.PageIndex = e.NewPageIndex;
        GetProjectList();
    }

    protected void gvProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                string projectid = string.Empty;
                string jobno = string.Empty;
                string customername = string.Empty;
                string custcode = string.Empty;
                string budgetedhours = string.Empty;
                string statusid = string.Empty;
                string remarks = string.Empty;

                int rowindex = 0;

                if (e.CommandArgument == "STATUS" || e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }
                else if (e.CommandArgument == "ADJUST")
                {
                    GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblProjectID = gvProjectList.Rows[rowindex].FindControl("lblProjectID") as Label;
                Label lblJobNo = gvProjectList.Rows[rowindex].FindControl("lblJobNo") as Label;
                Label lblCustCode = gvProjectList.Rows[rowindex].FindControl("lblCustCode") as Label;
                Label lblCustomerName = gvProjectList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblBudgetedHours = gvProjectList.Rows[rowindex].FindControl("lblBudgetedHours") as Label;
                Label lblStatusID = gvProjectList.Rows[rowindex].FindControl("lblStatusID") as Label;
                Label lblRemarks = gvProjectList.Rows[rowindex].FindControl("lblRemarks") as Label;

                projectid = lblProjectID.Text;
                jobno = lblJobNo.Text;
                customername = lblCustomerName.Text;
                custcode = lblCustCode.Text;
                budgetedhours = lblBudgetedHours.Text;
                statusid = lblStatusID.Text;
                remarks = lblRemarks.Text;

                if (e.CommandArgument == "PROPERTIES")
                {
                    if (lblStatusID.Text == "1")
                        Response.Redirect("AddUpdateProject.aspx?projectid=" + projectid);
                }

                else if (e.CommandArgument == "STATUS" || e.CommandArgument == "ADJUST")
                {
                    Response.Redirect("AdjustBudgetedHours.aspx?projectid=" + projectid + "&jobno=" + jobno + "&customername=" + customername);
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

    protected void gvProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ImageButton imgStatus = (ImageButton)e.Row.FindControl("imgStatus");
                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");

                Label lblJobNo = (Label)e.Row.FindControl("lblJobNo");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");

                Button btnAdjust = (Button)e.Row.FindControl("btnAdjust");

                string status = lblStatus.Text;
                int statusID = Convert.ToInt32(lblStatusID.Text);

                if (status == "Open" && statusID == 1)
                {
                    imgStatus.ImageUrl = "~/Images/New02.png";
                    imgStatus.ToolTip = status;
                    btnAdjust.ToolTip = "Adjust Job No.-: " + lblJobNo.Text;
                }

                else if (status == "Closed" && statusID == 2)
                {
                    imgStatus.ImageUrl = "~/Images/Closed01.png";
                    imgStatus.ToolTip = status;
                    imgStatus.Enabled = false;
                    imgProperties.Visible = false;
                    btnAdjust.Visible = false;
                }

                else if (status == "Enquiry" && statusID == 3)
                {
                    imgStatus.ImageUrl = "~/Images/HRApproved03.png";
                    imgStatus.ToolTip = status;
                    btnAdjust.ToolTip = "Adjust Job No.-: " + lblJobNo.Text;
                }

                else if (status == "Warranty" && statusID == 4)
                {
                    imgStatus.ImageUrl = "~/Images/Closed01.png";
                    imgStatus.ToolTip = status;
                    btnAdjust.ToolTip = "Adjust Job No.-: " + lblJobNo.Text;
                }

                else if (status == "RD" && statusID == 5)
                {
                    imgStatus.ImageUrl = "~/Images/Closed01.png";
                    imgStatus.ToolTip = status;
                    btnAdjust.ToolTip = "Adjust Job No.-: " + lblJobNo.Text;
                }

                else if (status == "Misc" && statusID == 6)
                {
                    imgStatus.ImageUrl = "~/Images/Cancelled01.png";
                    imgStatus.ToolTip = status;
                    btnAdjust.ToolTip = "Adjust Job No.-: " + lblJobNo.Text;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetProjectList();

    }

    protected void btnAddNewProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/AddUpdateProject.aspx");
    }

    #endregion


    #region METHODS[=======================]

    private void BindStatus()
    {
        try
        {
            dsStatus = objEnggHours.GetStatus("sp_get_status");
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                ddlStatus.DataSource = dsStatus.Tables[0];
                ddlStatus.DataTextField = "STATUS_NAME";
                ddlStatus.DataValueField = "STATUS_ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "Select");
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void GetProjectList()
    {
        try
        {
            string jobNo = string.Empty;
            string customerName = string.Empty;
            int statusID = 0;

            if (!string.IsNullOrEmpty(txtJobNo.Text))
                jobNo = txtJobNo.Text;
            else
                jobNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (ddlStatus.SelectedIndex > 0)
                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
            else
                statusID = 0;

            DataSet dsProjectList = objEnggHours.GetProiectList(jobNo, customerName, statusID);
            if (dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                gvProjectList.DataSource = dsProjectList.Tables[0];
                gvProjectList.DataBind();
            }
            else
            {
                gvProjectList.DataSource = null;
                gvProjectList.DataBind();
            }
            lblRecords.Text = "Records[" + dsProjectList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            //
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

    #endregion

}
