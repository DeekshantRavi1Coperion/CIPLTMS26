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

public partial class PROJECT_LOT_LOTMainItemList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsLOtMainItem = new DataSet();

    int maintItemID = 0;
    string maintItem = string.Empty;

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            HidePanel();
            if (!IsPostBack)
            {
                GetMainItemList();
            }
        }
        else
            Response.Redirect("~/Login.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetMainItemList();
    }

    protected void gvLOTMainItem_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblMainItemID = gvLOTMainItem.Rows[rowindex].FindControl("lblMainItemID") as Label;
                Label lblMainItem = gvLOTMainItem.Rows[rowindex].FindControl("lblMainItem") as Label;


                if (Convert.ToString(e.CommandArgument) == "PROPERTIES")
                {
                    if (!string.IsNullOrEmpty(lblMainItemID.Text))
                        ViewState["LOT_MAIN_ITEM_ID"] = Convert.ToInt32(lblMainItemID.Text);
                    else
                        ViewState["LOT_MAIN_ITEM_ID"] = 0;

                    if (!string.IsNullOrEmpty(lblMainItem.Text))
                        txtMainItemToEdit.Text = lblMainItem.Text;
                    else
                        txtMainItemToEdit.Text = string.Empty;

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

    protected void btnAddNewMainItem_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT/LOT/AddLOTMainItem.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateLOTMainItem();
    }


    #endregion


    #region METHODS[=======================]

    private void GetMainItemList()
    {
        try
        {
            maintItem = string.Empty;

            if (!string.IsNullOrEmpty(txtMainItem.Text))
                maintItem = txtMainItem.Text;

            dsLOtMainItem = objProject.GetLOTMainItemList(maintItem);

            if (dsLOtMainItem.Tables.Count > 0 && dsLOtMainItem.Tables[0].Rows.Count > 0)
            {
                gvLOTMainItem.DataSource = dsLOtMainItem.Tables[0];
                gvLOTMainItem.DataBind();
            }
            else
            {
                gvLOTMainItem.DataSource = null;
                gvLOTMainItem.DataBind();
            }
            lblRecords.Text = "Records[" + dsLOtMainItem.Tables[0].Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }


    private void UpdateLOTMainItem()
    {
        try
        {
            maintItemID = 0;
            maintItem = string.Empty;


            if (Convert.ToInt32(ViewState["LOT_MAIN_ITEM_ID"]) > 0)
                maintItemID = Convert.ToInt32(ViewState["LOT_MAIN_ITEM_ID"]);

            if (!string.IsNullOrEmpty(txtMainItemToEdit.Text))
                maintItem = txtMainItemToEdit.Text;

            int value = 0;
            value = objProject.AddUpdateMainItem(maintItemID, maintItem, Convert.ToInt32(Session["EMP_RECORD_ID"]));

            if (value > 0)
            {
                SuccessMessage("LOT Main Item updated successfully...!!!");
                GetMainItemList();
            }
            else
            {
                ExceptionMessage("Please try again...!!!");
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