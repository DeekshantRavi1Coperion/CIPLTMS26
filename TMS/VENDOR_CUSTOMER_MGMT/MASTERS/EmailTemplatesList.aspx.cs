using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class VENDOR_CUSTOMER_MGMT_MASTERS_EmailTemplatesList : System.Web.UI.Page
{

    #region VARIABLES[=====================]
    BAL.VendorCustomerManagement objVCM = new BAL.VendorCustomerManagement();
    BAL.Common objCommon = new BAL.Common();


    DataSet dsEntityType = new DataSet();
    DataSet dsStatus = new DataSet();

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


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetEntityTypes();
                BindEntityTypesS();

                GetStatus();
                BindStatusS();

                GetEmailTemplatesList();
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
        GetEmailTemplatesList();
    }

    protected void gvEmailTemplatesList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvEmailTemplatesList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblPID = gvEmailTemplatesList.Rows[rowindex].FindControl("lblPID") as Label;
                Label lblEntityTypeId = gvEmailTemplatesList.Rows[rowindex].FindControl("lblEntityTypeId") as Label;
                Label lblStatusId = gvEmailTemplatesList.Rows[rowindex].FindControl("lblStatusId") as Label;
                Label lblSubject = gvEmailTemplatesList.Rows[rowindex].FindControl("lblSubject") as Label;
                Label lblSalutation = gvEmailTemplatesList.Rows[rowindex].FindControl("lblSalutation") as Label;
                Label lblBodyLine = gvEmailTemplatesList.Rows[rowindex].FindControl("lblBodyLine") as Label;
                Label lblClosingLine = gvEmailTemplatesList.Rows[rowindex].FindControl("lblClosingLine") as Label;
                Label lblLink = gvEmailTemplatesList.Rows[rowindex].FindControl("lblLink") as Label;
                Label lblCC = gvEmailTemplatesList.Rows[rowindex].FindControl("lblCC") as Label;
                Label lblBCC = gvEmailTemplatesList.Rows[rowindex].FindControl("lblBCC") as Label;

                ViewState["PID"] = Convert.ToInt32(lblPID.Text);

                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {

                    BindEntityTypesToS();
                    if (Convert.ToInt32(lblEntityTypeId.Text) > 0)
                    {
                        ddlEntityTypeToS.SelectedValue = Convert.ToString(lblEntityTypeId.Text);
                    }

                    BindStatusToS();
                    if (Convert.ToInt32(lblStatusId.Text) > 0)
                    {
                        ddlStatusToS.SelectedValue = Convert.ToString(lblStatusId.Text);
                    }

                    txtSubjectToS.Text = lblSubject.Text;
                    txtSalutationToS.Text = lblSalutation.Text;
                    txtBodyLineToS.Text = lblBodyLine.Text;
                    txtClosingLineToS.Text = lblClosingLine.Text;
                    txtLinkToS.Text = lblLink.Text;
                    txtCCToS.Text = lblCC.Text;
                    txtBCCToS.Text = lblBCC.Text;

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
        UpdateEmailTemplate();
    }


    #endregion


    #region METHODS[=======================]

    private void GetEntityTypes()
    {
        try
        {

            dsEntityType = objVCM.GetEntityTypes();
            if (dsEntityType.Tables.Count > 0 && dsEntityType.Tables[0].Rows.Count > 0)
            {
                Session["dtEntityType"] = dsEntityType.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEntityTypesS()
    {
        try
        {
            DataTable dtEntityType = new DataTable();

            if (Session["dtEntityType"] != null)
                dtEntityType = (DataTable)Session["dtEntityType"];

            if (dtEntityType.Rows.Count > 0)
            {
                ddlEntityTypeS.DataSource = dtEntityType;
                ddlEntityTypeS.DataTextField = "NAME";
                ddlEntityTypeS.DataValueField = "PID";
                ddlEntityTypeS.DataBind();
                ddlEntityTypeS.Items.Insert(0, "All");
                ddlEntityTypeS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindEntityTypesToS()
    {
        try
        {
            DataTable dtEntityType = new DataTable();
            if (Session["dtEntityType"] != null)
                dtEntityType = (DataTable)Session["dtEntityType"];

            if (dtEntityType.Rows.Count > 0)
            {
                ddlEntityTypeToS.DataSource = dtEntityType;
                ddlEntityTypeToS.DataTextField = "NAME";
                ddlEntityTypeToS.DataValueField = "PID";
                ddlEntityTypeToS.DataBind();
                ddlEntityTypeToS.Items.Insert(0, "Select");
                ddlEntityTypeToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void GetStatus()
    {
        try
        {

            dsStatus = objVCM.GetStatus();
            if (dsStatus.Tables.Count > 0 && dsStatus.Tables[0].Rows.Count > 0)
            {
                Session["dtStatus"] = dsStatus.Tables[0];
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatusS()
    {
        try
        {
            DataTable dtStatus = new DataTable();
            if (Session["dtStatus"] != null)
                dtStatus = (DataTable)Session["dtStatus"];

            if (dtStatus.Rows.Count > 0)
            {
                ddlStatusS.DataSource = dtStatus;
                ddlStatusS.DataTextField = "NAME";
                ddlStatusS.DataValueField = "PID";
                ddlStatusS.DataBind();
                ddlStatusS.Items.Insert(0, "All");
                ddlStatusS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindStatusToS()
    {
        try
        {
            DataTable dtStatus = new DataTable();
            if (Session["dtStatus"] != null)
                dtStatus = (DataTable)Session["dtStatus"];

            if (dtStatus.Rows.Count > 0)
            {
                ddlStatusToS.DataSource = dtStatus;
                ddlStatusToS.DataTextField = "NAME";
                ddlStatusToS.DataValueField = "PID";
                ddlStatusToS.DataBind();
                ddlStatusToS.Items.Insert(0, "Select");
                ddlStatusToS.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }



    private void GetEmailTemplatesList()
    {
        try
        {
            int entityTypeId = 0;
            int statusId = 0;

            if (ddlEntityTypeS.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityTypeS.SelectedValue);
            if (ddlStatusS.SelectedIndex > 0) statusId = Convert.ToInt32(ddlStatusS.SelectedValue);

            dsList = objVCM.GetEmailTemplatesList(entityTypeId, statusId);

            if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                gvEmailTemplatesList.DataSource = dsList.Tables[0];
                gvEmailTemplatesList.DataBind();
            }
            else
            {
                gvEmailTemplatesList.DataSource = null;
                gvEmailTemplatesList.DataBind();
            }
            lblRecords.Text = "Records[" + dsList.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateEmailTemplate()
    {
        try
        {
            int pid = Convert.ToInt32(ViewState["PID"]);
            int entityTypeId = 0;
            int statusId = 0;
            string subject = string.Empty;
            string salutation = string.Empty;
            string bodyLine = string.Empty;
            string closingLine = string.Empty;
            string link = string.Empty;
            string cc = string.Empty;
            string bcc = string.Empty;
            int createdBy = Convert.ToInt32(Session["EMP_RECORD_ID"]);


            if (ddlEntityTypeToS.SelectedIndex > 0) entityTypeId = Convert.ToInt32(ddlEntityTypeToS.SelectedValue);
            if (ddlStatusToS.SelectedIndex > 0) statusId = Convert.ToInt32(ddlStatusToS.SelectedValue);
            if (!string.IsNullOrEmpty(txtSubjectToS.Text)) subject = txtSubjectToS.Text;
            if (!string.IsNullOrEmpty(txtSalutationToS.Text)) salutation = txtSalutationToS.Text;
            if (!string.IsNullOrEmpty(txtBodyLineToS.Text)) bodyLine = txtBodyLineToS.Text;
            if (!string.IsNullOrEmpty(txtClosingLineToS.Text)) closingLine = txtClosingLineToS.Text;
            if (!string.IsNullOrEmpty(txtLinkToS.Text)) link = txtLinkToS.Text;
            if (!string.IsNullOrEmpty(txtCCToS.Text)) cc = txtCCToS.Text;
            if (!string.IsNullOrEmpty(txtBCCToS.Text)) bcc = txtBCCToS.Text;

            int value = objVCM.AddUpdateEmailTemplates(pid, entityTypeId
                                                    , statusId
                                                    , subject
                                                    , bodyLine
                                                    , salutation
                                                    , closingLine
                                                    , link
                                                    , cc
                                                    , bcc
                                                    , createdBy);
            if (value > 0)
            {
                GetEmailTemplatesList();
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
        ddlEntityTypeToS.SelectedIndex = 0;
        ddlStatusToS.SelectedIndex = 0;
        txtSubjectToS.Text = string.Empty;
        txtSalutationToS.Text = string.Empty;
        txtBodyLineToS.Text = string.Empty;
        txtClosingLineToS.Text = string.Empty;
        txtLinkToS.Text = string.Empty;
        txtCCToS.Text = string.Empty;
        txtBCCToS.Text = string.Empty;
    }

    #endregion

}
