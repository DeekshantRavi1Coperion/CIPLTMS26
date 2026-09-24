using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_ProductList : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Project objProject = new BAL.Project();
    DataSet dsEstimatedItem = new DataSet();
    DataSet dsProject = new DataSet();
    DataSet dsItemList = new DataSet();

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
                GetProductList();
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
        GetProductList();
    }

    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/AddNewProductNew.aspx");
    }

    protected void gvProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        HidePanel();
        gvProductList.PageIndex = e.NewPageIndex;
        GetProductList();
    }

    protected void gvProductList_RowCommand(object sender, GridViewCommandEventArgs e)
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

                Label lblProductID = gvProductList.Rows[rowindex].FindControl("lblProductID") as Label;

                if (e.CommandArgument == "PROPERTIES")
                {
                    ModalPopupExtender1.Show();
                    iframeUpdateProduct.Attributes.Add("src", "UpdateProductNewOne.aspx?productID=" + Convert.ToString(lblProductID.Text));                    
                    //Response.Redirect("~/PROJECT_MGMT/UpdateProduct.aspx?productID=" + Convert.ToString(lblProductID.Text));
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

    protected void gvProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HidePanel();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");

                e.Row.ToolTip = lblProductCode.Text;

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
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetProductList()
    {
        try
        {
            string startDate = string.Empty;
            string endDate = string.Empty;
            string projectNo = string.Empty;
            string productCode = string.Empty;

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

            dsItemList = objProject.GetProductList(startDate, endDate, projectNo, productCode);
            if (dsItemList.Tables.Count > 0 && dsItemList.Tables[0].Rows.Count > 0)
            {
                gvProductList.DataSource = dsItemList.Tables[0];
                gvProductList.DataBind();
            }
            else
            {
                gvProductList.DataSource = null;
                gvProductList.DataBind();
            }
            lblRecords.Text = "Records[" + gvProductList.Rows.Count + "]";
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