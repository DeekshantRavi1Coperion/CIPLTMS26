using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PROJECT_LOT_SubitemDetail : System.Web.UI.Page
{

    #region EVENTS[======================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsSubitems = new DataSet();
    DataSet dsLOTCategoryForFactory = new DataSet();

    int LOTTFSubitemID = 0;
    int LOTTFID = 0;
    string LOTTFNo = string.Empty;
    string description = string.Empty;
    string DRGNo = string.Empty;
    string categoryID = string.Empty;
    int revisionNo = 0;
    int noOfCopies = 0;

    #endregion



    #region EVENTS[======================]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLOTCategoryForFactory();

            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                GetSubitemList();
            else
                Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GetSubitemList();
    }

    protected void gvSubItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                hdLOTTFSubitemID.Value = "0";
                int rowindex = 0;
                if (e.CommandArgument == "PROPERTIES")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblLOTTFSubitemID = gvSubItem.Rows[rowindex].FindControl("lblLOTTFSubitemID") as Label;
                Label lblDescription = gvSubItem.Rows[rowindex].FindControl("lblDescription") as Label;
                Label lblDrgOrDOCNo = gvSubItem.Rows[rowindex].FindControl("lblDrgOrDOCNo") as Label;
                Label lblRevNo = gvSubItem.Rows[rowindex].FindControl("lblRevNo") as Label;
                Label lblCategoryID = gvSubItem.Rows[rowindex].FindControl("lblCategoryID") as Label;
                Label lblCategory = gvSubItem.Rows[rowindex].FindControl("lblCategory") as Label;
                Label lblNoOfCopies = gvSubItem.Rows[rowindex].FindControl("lblNoOfCopies") as Label;

                foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryTEdit.Items)
                {
                    item.Selected = false;
                }

                if (e.CommandArgument == "PROPERTIES")
                {
                    hdLOTTFSubitemID.Value = lblLOTTFSubitemID.Text;
                    txtDescriptionToEdit.Text = lblDescription.Text;
                    txtDrgOrDOCNoToEdit.Text = lblDrgOrDOCNo.Text;
                    txtRevNoToEdit.Text = lblRevNo.Text;

                    string[] str = lblCategoryID.Text.Split(',');
                    foreach (string item in str)
                    {
                        chkLstCategoryTEdit.Items[Convert.ToInt32(item) - 1].Selected = true;
                    }

                    txtNoOfCopiesToEdit.Text = lblNoOfCopies.Text;
                    modalPopupExtenderSubItemDetail.Show();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvSubItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;
                ImageButton imgProperties = (ImageButton)e.Row.FindControl("imgProperties");
                imgProperties.Visible = false;
                
                if (Convert.ToInt32(Request.QueryString["currentStatusID"]) == 1 && Convert.ToInt32(Request.QueryString["cb"]) == Convert.ToInt32(Session["EMP_RECORD_ID"]))
                    imgProperties.Visible = true;


                string txt = string.Empty;
                string categoryTxt = string.Empty;

                Label lblCategoryID = (Label)e.Row.FindControl("lblCategoryID");
                Label lblCategory = (Label)e.Row.FindControl("lblCategory");


                if (!string.IsNullOrEmpty(lblCategoryID.Text))
                {
                    string[] srtCategoryID = lblCategoryID.Text.Split(',');
                    foreach (string i in srtCategoryID)
                    {
                        if (Convert.ToInt32(i) > 0)
                        {
                            if (Convert.ToInt32(i) == 1)
                                txt = "Fabrication";
                            else if (Convert.ToInt32(i) == 2)
                                txt = "Inspection";
                            else if (Convert.ToInt32(i) == 3)
                                txt = "Information";
                        }

                        categoryTxt += txt + ",";
                    }
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    categoryTxt = categoryTxt.TrimEnd(',');
                }

                if (!string.IsNullOrEmpty(categoryTxt))
                {
                    lblCategory.Text = categoryTxt;
                }

            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateSubitems();
    }

    #endregion



    #region METHODS[======================]    

    private void BindLOTCategoryForFactory()
    {
        try
        {
            dsLOTCategoryForFactory = objProject.GetFactoryCategory();
            if (dsLOTCategoryForFactory.Tables.Count > 0 && dsLOTCategoryForFactory.Tables[0].Rows.Count > 0)
            {
                chkLstCategoryTEdit.DataSource = dsLOTCategoryForFactory.Tables[0];
                chkLstCategoryTEdit.DataTextField = "LOT_CATEGORY_NAME";
                chkLstCategoryTEdit.DataValueField = "LOT_CATEGORY_ID";
                chkLstCategoryTEdit.DataBind();
            }
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void GetSubitemList()
    {
        try
        {
            LOTTFID = 0;
            description = string.Empty;
            DRGNo = string.Empty;

            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["LOTTFID"]);

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;

            if (!string.IsNullOrEmpty(txtDrgNo.Text))
                DRGNo = txtDrgNo.Text;

            dsSubitems = objProject.GetLOTSubitems(LOTTFID, description, DRGNo);
            if (dsSubitems.Tables.Count > 0 && dsSubitems.Tables[0].Rows.Count > 0)
            {
                gvSubItem.DataSource = dsSubitems.Tables[0];
                gvSubItem.DataBind();
            }
            else
            {
                gvSubItem.DataSource = null;
                gvSubItem.DataBind();
            }

            lblSubitemsRecords.Text = "Records[" + gvSubItem.Rows.Count + "]";

        }
        catch (Exception ex)
        {

        }
    }

    private void UpdateSubitems()
    {
        try
        {
            LOTTFSubitemID = 0;
            LOTTFID = 0;
            LOTTFNo = string.Empty;
            description = string.Empty;
            DRGNo = string.Empty;
            categoryID = string.Empty;
            revisionNo = 0;
            noOfCopies = 0;


            LOTTFSubitemID = Convert.ToInt32(hdLOTTFSubitemID.Value);

            if (Convert.ToInt32(Request.QueryString["LOTTFID"]) > 0)
                LOTTFID = Convert.ToInt32(Request.QueryString["LOTTFID"]);

            if (!string.IsNullOrEmpty(txtDescriptionToEdit.Text))
                description = txtDescriptionToEdit.Text;

            if (!string.IsNullOrEmpty(txtDrgOrDOCNoToEdit.Text))
                DRGNo = txtDrgOrDOCNoToEdit.Text;

            foreach (System.Web.UI.WebControls.ListItem item in chkLstCategoryTEdit.Items)
            {
                if (item.Selected)
                    categoryID += item.Value + ",";
            }

            if (!string.IsNullOrEmpty(categoryID))
                categoryID = categoryID.TrimEnd(',');

            if (!string.IsNullOrEmpty(txtRevNoToEdit.Text))
                revisionNo = Convert.ToInt32(txtRevNoToEdit.Text);

            if (!string.IsNullOrEmpty(txtNoOfCopiesToEdit.Text))
                noOfCopies = Convert.ToInt32(txtNoOfCopiesToEdit.Text);

            int value = objProject.UpdateSubitem(LOTTFSubitemID, LOTTFID, description, DRGNo, categoryID, revisionNo, noOfCopies, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                GetSubitemList();
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

}