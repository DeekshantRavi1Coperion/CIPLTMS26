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

public partial class PROJECT_LOT_LOTMainSubitemsList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsUnit = new DataSet();
    DataSet dsLOTMainItems = new DataSet();
    DataSet dsManager = new DataSet();

    DataSet dsSubitems = new DataSet();

    DataSet dsUnitToEdit = new DataSet();
    DataSet dsLOTMainItemsToEdit = new DataSet();

    int LOTMainSubitemID = 0;
    int companyID = 0;
    int LOTMainItemID = 0;
    string LOTMainSubitem = string.Empty;


    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {

                Session["DB_DETAILS"] = objCommon.GetDBDetails();
                BindCompany();
                BindLOTMainItem();

                GetLOTMainSubitemsList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLOTMainSubitemsList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetLOTMainSubitemsList();
    }

    protected void gvLOTMainSubitems_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblLOTMainSubitemID = gvLOTMainSubitems.Rows[rowindex].FindControl("lblLOTMainSubitemID") as Label;
                Label lblCompanyID = gvLOTMainSubitems.Rows[rowindex].FindControl("lblCompanyID") as Label;
                Label lblLOTMainItemID = gvLOTMainSubitems.Rows[rowindex].FindControl("lblLOTMainItemID") as Label;
                Label lblLOTMainSubitem = gvLOTMainSubitems.Rows[rowindex].FindControl("lblLOTMainSubitem") as Label;


                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblLOTMainSubitemID.Text))
                        ViewState["LOT_MAIN_SUBITEM_ID"] = Convert.ToInt32(lblLOTMainSubitemID.Text);
                    else
                        ViewState["LOT_MAIN_SUBITEM_ID"] = 0;

                    BindCompanyToEdit();
                    ddlCompanyToEdit.SelectedValue = Convert.ToString(lblCompanyID.Text);

                    BindLOTMainItemToEdit();
                    ddlLOTMainItemsToEdit.SelectedValue = Convert.ToString(lblLOTMainItemID.Text);

                    txtLOTMainSubitemToEdit.Text = Convert.ToString(lblLOTMainSubitem.Text);

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

    protected void btnAddNewSubitem_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/AddLOTMainSubitem.aspx");
    }








    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateLOTmainSubitem();
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
                ddlCompany.DataSource = dsUnit.Tables[0];
                ddlCompany.DataTextField = "UNIT_NAME";
                ddlCompany.DataValueField = "UNIT_ID";
                ddlCompany.DataBind();
            }
            else
            {
                ddlCompany.DataSource = null;
                ddlCompany.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItem()
    {
        try
        {
            dsLOTMainItems = objProject.GetLotMainItems();
            if (dsLOTMainItems.Tables.Count > 0 && dsLOTMainItems.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItems.DataSource = dsLOTMainItems.Tables[0];
                ddlLOTMainItems.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItems.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItems.DataBind();
                ddlLOTMainItems.Items.Insert(0, "Select");
                ddlLOTMainItems.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainItems.DataSource = null;
                ddlLOTMainItems.Items.Clear();
                ddlLOTMainItems.Items.Insert(0, "Select");
                ddlLOTMainItems.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetLOTMainSubitemsList()
    {
        try
        {
            companyID = 0;
            LOTMainItemID = 0;
            LOTMainSubitem = string.Empty;


            companyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (ddlLOTMainItems.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItems.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTMainSubitem.Text))
                LOTMainSubitem = txtLOTMainSubitem.Text.Trim();

            dsSubitems = objProject.GetLOTMainSubitemList(companyID, LOTMainItemID, LOTMainSubitem);

            if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[0].Rows.Count > 0)
            {
                gvLOTMainSubitems.DataSource = dsSubitems.Tables[0];
                gvLOTMainSubitems.DataBind();
            }
            else
            {
                gvLOTMainSubitems.DataSource = null;
                gvLOTMainSubitems.DataBind();
            }
            lblRecords.Text = "Records[" + dsSubitems.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }







    private void BindCompanyToEdit()
    {
        try
        {
            dsUnitToEdit = objCommon.GetUnit();
            if (dsUnitToEdit.Tables.Count > 0 && dsUnitToEdit.Tables[0].Rows.Count > 0)
            {
                ddlCompanyToEdit.DataSource = dsUnitToEdit.Tables[0];
                ddlCompanyToEdit.DataTextField = "UNIT_NAME";
                ddlCompanyToEdit.DataValueField = "UNIT_ID";
                ddlCompanyToEdit.DataBind();
            }
            else
            {
                ddlCompanyToEdit.DataSource = null;
                ddlCompanyToEdit.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLOTMainItemToEdit()
    {
        try
        {
            dsLOTMainItemsToEdit = objProject.GetLotMainItems();
            if (dsLOTMainItemsToEdit.Tables.Count > 0 && dsLOTMainItemsToEdit.Tables[0].Rows.Count > 0)
            {
                ddlLOTMainItemsToEdit.DataSource = dsLOTMainItemsToEdit.Tables[0];
                ddlLOTMainItemsToEdit.DataTextField = "LOT_MAIN_ITEM";
                ddlLOTMainItemsToEdit.DataValueField = "LOT_MAIN_ITEM_ID";
                ddlLOTMainItemsToEdit.DataBind();
                ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainItemsToEdit.SelectedIndex = 0;
            }
            else
            {
                ddlLOTMainItemsToEdit.DataSource = null;
                ddlLOTMainItemsToEdit.Items.Clear();
                ddlLOTMainItemsToEdit.Items.Insert(0, "Select");
                ddlLOTMainItemsToEdit.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateLOTmainSubitem()
    {
        try
        {
            LOTMainSubitemID = 0;
            companyID = 0;
            LOTMainItemID = 0;
            LOTMainSubitem = string.Empty;

            if (ViewState["LOT_MAIN_SUBITEM_ID"] != null && Convert.ToInt32(ViewState["LOT_MAIN_SUBITEM_ID"]) > 0)
                LOTMainSubitemID = Convert.ToInt32(ViewState["LOT_MAIN_SUBITEM_ID"]);

            companyID = Convert.ToInt32(ddlCompanyToEdit.SelectedValue);

            if (ddlLOTMainItemsToEdit.SelectedIndex > 0)
                LOTMainItemID = Convert.ToInt32(ddlLOTMainItemsToEdit.SelectedValue);

            if (!string.IsNullOrEmpty(txtLOTMainSubitemToEdit.Text))
                LOTMainSubitem = txtLOTMainSubitemToEdit.Text.Trim();

            int value = 0;

            if (LOTMainSubitemID > 0)
            {
                value = objProject.AddUpdateMainSubitem(LOTMainSubitemID, companyID, LOTMainItemID, LOTMainSubitem, 0, 0, Convert.ToInt32(Session["EMP_RECORD_ID"]));

                if (value > 0)
                {
                    SuccessMessage("LOT Main Subitem details updated successfully..!!");
                    ResetToEdit();
                    GetLOTMainSubitemsList();
                }
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
                return;
            }

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void ResetToEdit()
    {
        ddlLOTMainItemsToEdit.SelectedIndex = 0;
        txtLOTMainSubitemToEdit.Text = string.Empty;
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