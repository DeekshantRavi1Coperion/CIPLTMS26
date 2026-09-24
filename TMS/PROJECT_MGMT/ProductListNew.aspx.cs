using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;

public partial class PROJECT_MGMT_ProductListNew : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    BAL.Common objCommon = new BAL.Common();
    DataSet dsDBDetails = new DataSet();
    BAL.Project objProject = new BAL.Project();
    DataSet dsEstimatedItem = new DataSet();
    DataSet dsProject = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsProductList = new DataSet();

    string startDate = string.Empty;
    string endDate = string.Empty;
    string projectNo = string.Empty;
    string productCode = string.Empty;
    int unitId = 0;
    string unitName = string.Empty;
    string dbNameA35 = string.Empty;
    string dbNameDLH = string.Empty;
    string dbNameSEZ = string.Empty;
    string dbNameGNU = string.Empty;

    string unitNameA35 = string.Empty;
    string unitNameDLH = string.Empty;
    string unitNameSEZ = string.Empty;
    string unitNameGNU = string.Empty;

    int unitIDA35 = 0;
    int unitIDDLH = 0;
    int unitIDSEZ = 0;
    int unitIDGNU = 0;

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
                BindUnit();                
                Session["DB_DETAILS"] = objCommon.GetDBDetails();
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

    private void BindUnit()
    {
        try
        {
            dsUnit = objCommon.GetUnit();
            if (dsUnit.Tables.Count > 0 && dsUnit.Tables[0].Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit.Tables[0];
                ddlUnit.DataTextField = "UNIT_NAME";
                ddlUnit.DataValueField = "UNIT_ID";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");
                ddlUnit.SelectedIndex = 0;
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


            dsDBDetails = (DataSet)Session["DB_DETAILS"];

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

            if (rdSingle.Checked == true && rdAll.Checked == false)
            {
                if (ddlUnit.SelectedIndex > 0)
                {
                    unitId = Convert.ToInt32(ddlUnit.SelectedValue);
                }
                dsProductList = objProject.GetProductListNew(startDate, endDate, projectNo, productCode, unitId);
            }

            if (rdSingle.Checked == false && rdAll.Checked == true)
            {
                if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                    {
                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        {
                            dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameA35 = Convert.ToString(dr["UNIT_NAME"]);
                            unitIDA35 = Convert.ToInt32(dr["UNIT_ID"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        {
                            dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameDLH = Convert.ToString(dr["UNIT_NAME"]);
                            unitIDDLH = Convert.ToInt32(dr["UNIT_ID"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        {
                            dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameSEZ = Convert.ToString(dr["UNIT_NAME"]);
                            unitIDSEZ = Convert.ToInt32(dr["UNIT_ID"]);
                        }

                        if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        {
                            dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                            unitNameGNU = Convert.ToString(dr["UNIT_NAME"]);
                            unitIDGNU = Convert.ToInt32(dr["UNIT_ID"]);
                        }
                    }
                }
                dsProductList = objProject.GetProductListAllUnit(startDate, endDate, projectNo, productCode, dbNameA35, unitNameA35, dbNameDLH, unitNameDLH, dbNameSEZ, unitNameSEZ, dbNameGNU, unitNameGNU,unitIDA35,unitIDDLH,unitIDGNU,unitIDSEZ);
            }

            if (dsProductList.Tables.Count > 0 && dsProductList.Tables[0].Rows.Count > 0)
            {
                gvProductList.DataSource = dsProductList.Tables[0];
                gvProductList.DataBind();
            }
            else
            {
                gvProductList.DataSource = null;
                gvProductList.DataBind();
            }
            lblRecords.Text = "Records[" + gvProductList.Rows.Count + "]";

            if (rdSingle.Checked)
            {
                rdSingle.Checked = true;
                rdAll.Checked = false;
                ddlUnit.Enabled = true;
            }
            else
            {
                rdSingle.Checked = false;
                rdAll.Checked = true;
                ddlUnit.Enabled = false;
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