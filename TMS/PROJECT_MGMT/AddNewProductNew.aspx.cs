using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

public partial class PROJECT_MGMT_AddNewProductNew : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsProject = new DataSet();
    DataSet dsProductUnit = new DataSet();
    DataSet dsEstimatedProduct = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsGroup = new DataSet();
    DataSet dsSubgroup = new DataSet();

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                HidePanel();
                Session["dt"] = null;
                BindProjectNo();
                BindUnit();

                ddlEstimatedProduct.Items.Insert(0, "Select");
                ddlEstimatedProduct.SelectedIndex = 0;

                ddlGroup.Items.Insert(0, "Select");
                ddlGroup.SelectedIndex = 0;
                
                ddlSubgroup.Items.Insert(0, "Select");
                ddlSubgroup.SelectedIndex = 0;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        HidePanel();
        ddlProjectNo.Enabled = false;
        BindNewProductNewOne();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        HidePanel();
        SaveProduct();
    }

    protected void ddlProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        HidePanel();
        BindEstimatedProduct();
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        HidePanel();
        if (ddlUnit.SelectedIndex > 0)
        {
            BindGroup();
        }
        else
        {
            ddlGroup.Items.Clear();
            ddlGroup.Items.Insert(0, "Select");
            ddlGroup.SelectedIndex = 0;

            ddlSubgroup.Items.Clear();
            ddlSubgroup.Items.Insert(0, "Select");
            ddlSubgroup.SelectedIndex = 0;
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        HidePanel();
        if (ddlGroup.SelectedIndex > 0)
        {
            BindSubgroup();
        }
        else
        {
            ddlSubgroup.Items.Clear();
            ddlSubgroup.Items.Insert(0, "Select");
            ddlSubgroup.SelectedIndex = 0;
        }
    }

    protected void btnProductList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
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

    protected void gvProductList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                if (e.CommandArgument == "REMOVE")
                {
                    GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    rowindex = rowSelect.RowIndex;
                }

                Label lblSerialNo = gvProductList.Rows[rowindex].FindControl("lblSerialNo") as Label;

                if (e.CommandArgument == "REMOVE")
                {
                    DataTable dt = (DataTable)Session["dt"];

                    RemoveRecord(Convert.ToInt32(lblSerialNo.Text));

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

    #endregion


    #region METHODS[============================]

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

    private void BindEstimatedProduct()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ddlProjectNo.SelectedValue)) && Convert.ToString(ddlProjectNo.SelectedValue) != "Select")
            {
                dsEstimatedProduct = objProject.GetEstimatedProduct(Convert.ToString(ddlProjectNo.SelectedValue));
                if (dsEstimatedProduct.Tables.Count > 0 && dsEstimatedProduct.Tables[0].Rows.Count > 0)
                {
                    ddlEstimatedProduct.DataSource = dsEstimatedProduct.Tables[0];
                    ddlEstimatedProduct.DataTextField = "ESTIMATED_ITEM";
                    ddlEstimatedProduct.DataValueField = "ESTIMATED_PROJECT_ID";
                    ddlEstimatedProduct.DataBind();
                    ddlEstimatedProduct.Items.Insert(0, "Select");
                    ddlEstimatedProduct.SelectedIndex = 0;
                }
                else
                {
                    ddlEstimatedProduct.Items.Clear();
                    ddlEstimatedProduct.Items.Insert(0, "Select");
                    ddlEstimatedProduct.SelectedIndex = 0;
                }
            }
            else
            {
                ddlEstimatedProduct.Items.Clear();
                ddlEstimatedProduct.Items.Insert(0, "Select");
                ddlEstimatedProduct.SelectedIndex = 0;
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

    private void BindGroup()
    {
        try
        {
            dsGroup = objProject.GetProjectGroupNew(Convert.ToInt32(ddlUnit.SelectedValue));
            if (dsGroup.Tables.Count > 0 && dsGroup.Tables[0].Rows.Count > 0)
            {
                ddlGroup.DataSource = dsGroup.Tables[0];
                ddlGroup.DataTextField = "DESCRIPT";
                ddlGroup.DataValueField = "GROUP_ID";
                ddlGroup.DataBind();
                ddlGroup.Items.Insert(0, "Select");
                ddlGroup.SelectedIndex = 0;
            }
            else
            {
                ddlGroup.Items.Clear();
                ddlGroup.Items.Insert(0, "Select");
                ddlGroup.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindSubgroup()
    {
        try
        {
            int unitID = 0;
            string groupID = string.Empty;

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);

            if (ddlGroup.SelectedIndex > 0)
                groupID = Convert.ToString(ddlGroup.SelectedValue);

            if (unitID > 0 && !string.IsNullOrEmpty(groupID))
            {
                dsSubgroup = objProject.GetProjectSubgroupNew(unitID, groupID);
                if (dsSubgroup.Tables.Count > 0 && dsSubgroup.Tables[0].Rows.Count > 0)
                {
                    ddlSubgroup.DataSource = dsSubgroup.Tables[0];
                    ddlSubgroup.DataTextField = "DESCRIPT";
                    ddlSubgroup.DataValueField = "SUBGRP_ID";
                    ddlSubgroup.DataBind();
                    ddlSubgroup.Items.Insert(0, "Select");
                    ddlSubgroup.SelectedIndex = 0;
                }
                else
                {
                    ddlSubgroup.Items.Clear();
                    ddlSubgroup.Items.Insert(0, "Select");
                    ddlSubgroup.SelectedIndex = 0;
                }
            }
            else
            {
                ddlSubgroup.Items.Clear();
                ddlSubgroup.Items.Insert(0, "Select");
                ddlSubgroup.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindNewProductNewOne()
    {
        try
        {
            int count = 1;
            DataTable dt = new DataTable();
            if (gvProductList.Rows.Count > 0)
            {
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("SERIAL_NO", typeof(string));
                dtNew.Columns.Add("PROJECT_NO", typeof(string));
                dtNew.Columns.Add("ESTIMATED_ITEM", typeof(string));
                dtNew.Columns.Add("TYPE", typeof(string));
                dtNew.Columns.Add("CATEGORY", typeof(string));
                dtNew.Columns.Add("PRODUCT_CODE", typeof(string));
                dtNew.Columns.Add("DESCRIPTION", typeof(string));
                dtNew.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
                dtNew.Columns.Add("GST_HSN", typeof(string));
                dtNew.Columns.Add("GST_RATE", typeof(string));
                dtNew.Columns.Add("PURCHASE_UNIT", typeof(string));
                dtNew.Columns.Add("STOCK_UNIT", typeof(string));
                dtNew.Columns.Add("SALE_UNIT", typeof(string));
                dtNew.Columns.Add("TAG_NO", typeof(string));
                dtNew.Columns.Add("MODEL_NO", typeof(string));
                dtNew.Columns.Add("ADDITIONAL_INFORMATION", typeof(string));
                dtNew.Columns.Add("TECHNICAL_SPECIFICATION", typeof(string));

                dtNew.Columns.Add("UNIT_ID", typeof(string));
                dtNew.Columns.Add("UNIT", typeof(string));

                dtNew.Columns.Add("GROUP_ID", typeof(string));
                dtNew.Columns.Add("GROUP", typeof(string));
                dtNew.Columns.Add("SUBGROUP_ID", typeof(string));
                dtNew.Columns.Add("SUBGROUP", typeof(string));

                foreach (GridViewRow gr in gvProductList.Rows)
                {
                    DataRow dr = dtNew.NewRow();
                    Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                    Label lblEstimatedItem = (Label)gr.FindControl("lblEstimatedItem");
                    DropDownList ddlType = (DropDownList)gr.FindControl("ddlType");
                    DropDownList ddlCategory = (DropDownList)gr.FindControl("ddlCategory");
                    TextBox txtProductCode = (TextBox)gr.FindControl("txtProductCode");
                    TextBox txtDescription = (TextBox)gr.FindControl("txtDescription");
                    TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                    TextBox txtGSTHSN = (TextBox)gr.FindControl("txtGSTHSN");
                    TextBox txtGSTRate = (TextBox)gr.FindControl("txtGSTRate");
                    DropDownList ddlPurchaseUnit = (DropDownList)gr.FindControl("ddlPurchaseUnit");
                    DropDownList ddlStockUnit = (DropDownList)gr.FindControl("ddlStockUnit");
                    DropDownList ddlSaleUnit = (DropDownList)gr.FindControl("ddlSaleUnit");
                    TextBox txtTagNo = (TextBox)gr.FindControl("txtTagNo");
                    TextBox txtModelNo = (TextBox)gr.FindControl("txtModelNo");
                    TextBox txtAdditionalInformation = (TextBox)gr.FindControl("txtAdditionalInformation");
                    TextBox txtTechnicalSpecification = (TextBox)gr.FindControl("txtTechnicalSpecification");

                    Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                    Label lblUnit = (Label)gr.FindControl("lblUnit");

                    Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                    Label lblGroup = (Label)gr.FindControl("lblGroup");
                    Label lblSubgroupID = (Label)gr.FindControl("lblSubgroupID");
                    Label lblSubgroup = (Label)gr.FindControl("lblSubgroup");




                    dr["SERIAL_NO"] = Convert.ToString(count++);

                    if (!string.IsNullOrEmpty(lblProjectNo.Text))
                        dr["PROJECT_NO"] = lblProjectNo.Text;
                    else
                        dr["PROJECT_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblEstimatedItem.Text))
                        dr["ESTIMATED_ITEM"] = lblEstimatedItem.Text;
                    else
                        dr["PROJECT_NO"] = string.Empty;

                    if (ddlType.SelectedIndex > 0)
                        dr["TYPE"] = Convert.ToString(ddlType.SelectedValue);
                    else
                        dr["TYPE"] = "0";

                    if (ddlCategory.SelectedIndex > 0)
                        dr["CATEGORY"] = Convert.ToString(ddlCategory.SelectedValue);
                    else
                        dr["CATEGORY"] = "0";

                    if (!string.IsNullOrEmpty(txtProductCode.Text))
                        dr["PRODUCT_CODE"] = txtProductCode.Text;
                    else
                        dr["PRODUCT_CODE"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtDescription.Text))
                        dr["DESCRIPTION"] = txtDescription.Text;
                    else
                        dr["DESCRIPTION"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                        dr["ADDITIONAL_DESCRIPTION"] = txtAdditionalDescription.Text;
                    else
                        dr["ADDITIONAL_DESCRIPTION"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtGSTHSN.Text))
                        dr["GST_HSN"] = txtGSTHSN.Text;
                    else
                        dr["GST_HSN"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtGSTRate.Text))
                        dr["GST_RATE"] = txtGSTRate.Text;
                    else
                        dr["GST_RATE"] = "0";

                    if (ddlPurchaseUnit.SelectedIndex > 0)
                        dr["PURCHASE_UNIT"] = Convert.ToString(ddlPurchaseUnit.SelectedValue);
                    else
                        dr["PURCHASE_UNIT"] = "0";

                    if (ddlStockUnit.SelectedIndex > 0)
                        dr["STOCK_UNIT"] = Convert.ToString(ddlStockUnit.SelectedValue);
                    else
                        dr["STOCK_UNIT"] = "0";

                    if (ddlSaleUnit.SelectedIndex > 0)
                        dr["SALE_UNIT"] = Convert.ToString(ddlSaleUnit.SelectedValue);
                    else
                        dr["SALE_UNIT"] = "0";

                    if (!string.IsNullOrEmpty(txtTagNo.Text))
                        dr["TAG_NO"] = txtTagNo.Text;
                    else
                        dr["TAG_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtModelNo.Text))
                        dr["MODEL_NO"] = txtModelNo.Text;
                    else
                        dr["MODEL_NO"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtAdditionalInformation.Text))
                        dr["ADDITIONAL_INFORMATION"] = txtAdditionalInformation.Text;
                    else
                        dr["ADDITIONAL_INFORMATION"] = string.Empty;

                    if (!string.IsNullOrEmpty(txtTechnicalSpecification.Text))
                        dr["TECHNICAL_SPECIFICATION"] = txtTechnicalSpecification.Text;
                    else
                        dr["TECHNICAL_SPECIFICATION"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblGroupID.Text))
                        dr["GROUP_ID"] = lblGroupID.Text;
                    else
                        dr["GROUP_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblGroup.Text))
                        dr["GROUP"] = lblGroup.Text;
                    else
                        dr["GROUP"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblUnitID.Text))
                        dr["UNIT_ID"] = lblUnitID.Text;
                    else
                        dr["UNIT_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblUnit.Text))
                        dr["UNIT"] = lblUnit.Text;
                    else
                        dr["UNIT"] = string.Empty;

                    if (!string.IsNullOrEmpty(lblSubgroupID.Text))
                        dr["SUBGROUP_ID"] = lblSubgroupID.Text;
                    else
                        dr["SUBGROUP_ID"] = "0";

                    if (!string.IsNullOrEmpty(lblSubgroup.Text))
                        dr["SUBGROUP"] = lblSubgroup.Text;
                    else
                        dr["SUBGROUP"] = string.Empty;


                    dtNew.Rows.Add(dr);
                }

                DataRow drNewRow = dtNew.NewRow();

                drNewRow["SERIAL_NO"] = Convert.ToString(gvProductList.Rows.Count + 1);
                drNewRow["PROJECT_NO"] = Convert.ToString(ddlProjectNo.SelectedValue);
                drNewRow["ESTIMATED_ITEM"] = Convert.ToString(ddlEstimatedProduct.SelectedItem.Text);
                drNewRow["TYPE"] = "0";
                drNewRow["CATEGORY"] = "0";
                drNewRow["PRODUCT_CODE"] = string.Empty;
                drNewRow["DESCRIPTION"] = string.Empty;
                drNewRow["ADDITIONAL_DESCRIPTION"] = string.Empty;
                drNewRow["GST_HSN"] = string.Empty;
                drNewRow["GST_RATE"] = string.Empty;
                drNewRow["PURCHASE_UNIT"] = "0";
                drNewRow["STOCK_UNIT"] = "0";
                drNewRow["SALE_UNIT"] = "0";
                drNewRow["TAG_NO"] = string.Empty;
                drNewRow["MODEL_NO"] = string.Empty;
                drNewRow["ADDITIONAL_INFORMATION"] = string.Empty;
                drNewRow["TECHNICAL_SPECIFICATION"] = string.Empty;


                if (ddlUnit.SelectedIndex > 0)
                {
                    drNewRow["UNIT_ID"] = Convert.ToString(ddlUnit.SelectedValue);
                    drNewRow["UNIT"] = Convert.ToString(ddlUnit.SelectedItem.Text);
                }
                else
                {
                    drNewRow["UNIT_ID"] = "0";
                    drNewRow["UNIT"] = string.Empty;
                }

                if (ddlGroup.SelectedIndex > 0)
                {
                    drNewRow["GROUP_ID"] = Convert.ToString(ddlGroup.SelectedValue);
                    drNewRow["GROUP"] = Convert.ToString(ddlGroup.SelectedItem.Text);
                }
                else
                {
                    drNewRow["GROUP_ID"] = "0";
                    drNewRow["GROUP"] = string.Empty;
                }

                if (ddlSubgroup.SelectedIndex > 0)
                {
                    drNewRow["SUBGROUP_ID"] = Convert.ToString(ddlSubgroup.SelectedValue);
                    drNewRow["SUBGROUP"] = Convert.ToString(ddlSubgroup.SelectedItem.Text);
                }
                else
                {
                    drNewRow["SUBGROUP_ID"] = "0";
                    drNewRow["SUBGROUP"] = string.Empty;
                }


                dtNew.Rows.Add(drNewRow);
                dt = dtNew;
            }
            else
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("SERIAL_NO", typeof(string));
                dtTemp.Columns.Add("PROJECT_NO", typeof(string));
                dtTemp.Columns.Add("ESTIMATED_ITEM", typeof(string));
                dtTemp.Columns.Add("TYPE", typeof(string));
                dtTemp.Columns.Add("CATEGORY", typeof(string));
                dtTemp.Columns.Add("PRODUCT_CODE", typeof(string));
                dtTemp.Columns.Add("DESCRIPTION", typeof(string));
                dtTemp.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
                dtTemp.Columns.Add("GST_HSN", typeof(string));
                dtTemp.Columns.Add("GST_RATE", typeof(string));
                dtTemp.Columns.Add("PURCHASE_UNIT", typeof(string));
                dtTemp.Columns.Add("STOCK_UNIT", typeof(string));
                dtTemp.Columns.Add("SALE_UNIT", typeof(string));
                dtTemp.Columns.Add("TAG_NO", typeof(string));
                dtTemp.Columns.Add("MODEL_NO", typeof(string));
                dtTemp.Columns.Add("ADDITIONAL_INFORMATION", typeof(string));
                dtTemp.Columns.Add("TECHNICAL_SPECIFICATION", typeof(string));

                dtTemp.Columns.Add("UNIT_ID", typeof(string));
                dtTemp.Columns.Add("UNIT", typeof(string));

                dtTemp.Columns.Add("GROUP_ID", typeof(string));
                dtTemp.Columns.Add("GROUP", typeof(string));
                dtTemp.Columns.Add("SUBGROUP_ID", typeof(string));
                dtTemp.Columns.Add("SUBGROUP", typeof(string));


                DataRow drTemp = dtTemp.NewRow();
                drTemp["SERIAL_NO"] = "1";
                drTemp["PROJECT_NO"] = Convert.ToString(ddlProjectNo.SelectedValue);
                drTemp["ESTIMATED_ITEM"] = Convert.ToString(ddlEstimatedProduct.SelectedItem.Text);
                drTemp["TYPE"] = "0";
                drTemp["CATEGORY"] = "0";
                drTemp["PRODUCT_CODE"] = string.Empty;
                drTemp["DESCRIPTION"] = string.Empty;
                drTemp["ADDITIONAL_DESCRIPTION"] = string.Empty;
                drTemp["GST_HSN"] = string.Empty;
                drTemp["GST_RATE"] = string.Empty;
                drTemp["PURCHASE_UNIT"] = "0";
                drTemp["STOCK_UNIT"] = "0";
                drTemp["SALE_UNIT"] = "0";
                drTemp["TAG_NO"] = string.Empty;
                drTemp["MODEL_NO"] = string.Empty;
                drTemp["ADDITIONAL_INFORMATION"] = string.Empty;
                drTemp["TECHNICAL_SPECIFICATION"] = string.Empty;

                if (ddlUnit.SelectedIndex > 0)
                {
                    drTemp["UNIT_ID"] = Convert.ToString(ddlUnit.SelectedValue);
                    drTemp["UNIT"] = Convert.ToString(ddlUnit.SelectedItem.Text);
                }
                else
                {
                    drTemp["UNIT_ID"] = "0";
                    drTemp["UNIT"] = string.Empty;
                }

                if (ddlGroup.SelectedIndex > 0)
                {
                    drTemp["GROUP_ID"] = Convert.ToString(ddlGroup.SelectedValue);
                    drTemp["GROUP"] = Convert.ToString(ddlGroup.SelectedItem.Text);
                }
                else
                {
                    drTemp["GROUP_ID"] = "0";
                    drTemp["GROUP"] = string.Empty;
                }

                if (ddlSubgroup.SelectedIndex > 0)
                {
                    drTemp["SUBGROUP_ID"] = Convert.ToString(ddlSubgroup.SelectedValue);
                    drTemp["SUBGROUP"] = Convert.ToString(ddlSubgroup.SelectedItem.Text);
                }
                else
                {
                    drTemp["SUBGROUP_ID"] = "0";
                    drTemp["SUBGROUP"] = string.Empty;
                }


                dtTemp.Rows.Add(drTemp);
                dt = dtTemp;
            }

            if (dt.Rows.Count > 0)
            {
                Session["dt"] = dt;
                gvProductList.DataSource = dt;
                gvProductList.DataBind();
            }
            else
            {
                Session["dt"] = null;
                gvProductList.DataSource = null;
                gvProductList.DataBind();
            }

            lblRecords.Text = "[" + gvProductList.Rows.Count + "]";

        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void SaveProduct()
    {

        try
        {
            bool chk = false;
            int serialNo = 0;
            string projectNo = string.Empty;
            int estimatedProdctID = 0;
            int typeID = 0;
            int categoryID = 0;
            string productCode = string.Empty;
            string description = string.Empty;
            string additionalDescription = string.Empty;
            string gSTHSN = string.Empty;
            double gSTRate = 0;
            int purchaseUnitID = 0;
            int stockUnitID = 0;
            int saleUnitID = 0;
            string tagNo = string.Empty;
            string modelNo = string.Empty;
            string additionalInformation = string.Empty;
            string technicalSpecification = string.Empty;
            int unitID = 0;
            string groupID = string.Empty;
            string subGroupID = string.Empty;


            DataTable dt = (DataTable)Session["dt"];

            if (gvProductList.Rows.Count > 0)
            {
                foreach (GridViewRow gr in gvProductList.Rows)
                {
                    chk = true;

                    Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                    Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                    Label lblEstimatedItem = (Label)gr.FindControl("lblEstimatedItem");
                    DropDownList ddlType = (DropDownList)gr.FindControl("ddlType");
                    DropDownList ddlCategory = (DropDownList)gr.FindControl("ddlCategory");
                    TextBox txtProductCode = (TextBox)gr.FindControl("txtProductCode");
                    TextBox txtDescription = (TextBox)gr.FindControl("txtDescription");
                    TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                    TextBox txtGSTHSN = (TextBox)gr.FindControl("txtGSTHSN");
                    TextBox txtGSTRate = (TextBox)gr.FindControl("txtGSTRate");
                    DropDownList ddlPurchaseUnit = (DropDownList)gr.FindControl("ddlPurchaseUnit");
                    DropDownList ddlStockUnit = (DropDownList)gr.FindControl("ddlStockUnit");
                    DropDownList ddlSaleUnit = (DropDownList)gr.FindControl("ddlSaleUnit");
                    TextBox txtTagNo = (TextBox)gr.FindControl("txtTagNo");
                    TextBox txtModelNo = (TextBox)gr.FindControl("txtModelNo");
                    TextBox txtAdditionalInformation = (TextBox)gr.FindControl("txtAdditionalInformation");
                    TextBox txtTechnicalSpecification = (TextBox)gr.FindControl("txtTechnicalSpecification");

                    Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                    Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                    Label lblSubgroupID = (Label)gr.FindControl("lblSubgroupID");


                    serialNo = Convert.ToInt32(lblSerialNo.Text);

                    if (!string.IsNullOrEmpty(lblProjectNo.Text))
                    {
                        projectNo = lblProjectNo.Text;
                    }
                    else
                        projectNo = string.Empty;

                    if (ddlEstimatedProduct.SelectedIndex > 0)
                        estimatedProdctID = Convert.ToInt32(ddlEstimatedProduct.SelectedValue);
                    else
                        estimatedProdctID = 0;

                    if (ddlType.SelectedIndex > 0)
                        typeID = Convert.ToInt32(ddlType.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select Type..!");
                        return;
                    }

                    if (ddlCategory.SelectedIndex > 0)
                        categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select Category..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtProductCode.Text))
                        productCode = txtProductCode.Text.Trim();
                    else
                    {
                        chk = false;
                        ExceptionMessage("Product Code is empty please select Type and Category..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtDescription.Text))
                        description = txtDescription.Text;
                    else
                        description = string.Empty;

                    if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                        additionalDescription = txtAdditionalDescription.Text;
                    else
                        additionalDescription = string.Empty;

                    if (!string.IsNullOrEmpty(txtGSTHSN.Text))
                        gSTHSN = txtGSTHSN.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter GST HSN code..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtGSTRate.Text) && Convert.ToDouble(txtGSTRate.Text) > 0)
                        gSTRate = Convert.ToDouble(txtGSTRate.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter GST Rate..!");
                        return;
                    }

                    if (ddlPurchaseUnit.SelectedIndex > 0)
                        purchaseUnitID = Convert.ToInt32(ddlPurchaseUnit.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select purchase unit..!");
                        return;
                    }

                    if (ddlStockUnit.SelectedIndex > 0)
                        stockUnitID = Convert.ToInt32(ddlStockUnit.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select stock unit..!");
                        return;
                    }

                    if (ddlSaleUnit.SelectedIndex > 0)
                        saleUnitID = Convert.ToInt32(ddlSaleUnit.SelectedValue);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select sale unit..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtTagNo.Text))
                        tagNo = txtTagNo.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter Tag No..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtModelNo.Text))
                        modelNo = txtModelNo.Text;
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please enter Model No..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtAdditionalInformation.Text))
                        additionalInformation = txtAdditionalInformation.Text;
                    else
                        additionalInformation = string.Empty;

                    if (!string.IsNullOrEmpty(txtTechnicalSpecification.Text))
                        technicalSpecification = txtTechnicalSpecification.Text;
                    else
                        technicalSpecification = string.Empty;

                    if (!string.IsNullOrEmpty(lblUnitID.Text) && Convert.ToInt32(lblUnitID.Text) > 0)
                        unitID = Convert.ToInt32(lblUnitID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select Unit..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(lblGroupID.Text) && Convert.ToInt32(lblGroupID.Text) > 0)
                        groupID = Convert.ToString(lblGroupID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select Group ..!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(lblSubgroupID.Text) && Convert.ToInt32(lblSubgroupID.Text) > 0)
                        subGroupID = Convert.ToString(lblSubgroupID.Text);
                    else
                    {
                        chk = false;
                        ExceptionMessage("Please select Subgroup ..!");
                        return;
                    }

                    if (chk)
                    {
                        int value = objProject.AddUpdateNewProductNew(0, projectNo, estimatedProdctID, typeID, categoryID, productCode, description,
                                                                    additionalDescription, gSTHSN, gSTRate, purchaseUnitID, stockUnitID, saleUnitID,
                                                                    tagNo, modelNo, additionalInformation, technicalSpecification,
                                                                    unitID, groupID, subGroupID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
                        if (value < 0)
                        {
                            ExceptionMessage("Product code already exists..!");
                            return;
                        }
                        else if (value > 0)
                        {
                            RemoveRecord(serialNo);
                        }
                    }
                }
            }
            else
            {
                ddlProjectNo.Enabled = true;
                ExceptionMessage("No data found..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ddlProjectNo.Enabled = true;
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void RemoveRecord(int serialNo)
    {
        try
        {
            HidePanel();

            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SERIAL_NO", typeof(string));
            dtNew.Columns.Add("PROJECT_NO", typeof(string));
            dtNew.Columns.Add("ESTIMATED_ITEM", typeof(string));
            dtNew.Columns.Add("TYPE", typeof(string));
            dtNew.Columns.Add("CATEGORY", typeof(string));
            dtNew.Columns.Add("PRODUCT_CODE", typeof(string));
            dtNew.Columns.Add("DESCRIPTION", typeof(string));
            dtNew.Columns.Add("ADDITIONAL_DESCRIPTION", typeof(string));
            dtNew.Columns.Add("GST_HSN", typeof(string));
            dtNew.Columns.Add("GST_RATE", typeof(string));
            dtNew.Columns.Add("PURCHASE_UNIT", typeof(string));
            dtNew.Columns.Add("STOCK_UNIT", typeof(string));
            dtNew.Columns.Add("SALE_UNIT", typeof(string));
            dtNew.Columns.Add("TAG_NO", typeof(string));
            dtNew.Columns.Add("MODEL_NO", typeof(string));
            dtNew.Columns.Add("ADDITIONAL_INFORMATION", typeof(string));
            dtNew.Columns.Add("TECHNICAL_SPECIFICATION", typeof(string));

            dtNew.Columns.Add("UNIT_ID", typeof(string));
            dtNew.Columns.Add("UNIT", typeof(string));

            dtNew.Columns.Add("GROUP_ID", typeof(string));
            dtNew.Columns.Add("GROUP", typeof(string));
            dtNew.Columns.Add("SUBGROUP_ID", typeof(string));
            dtNew.Columns.Add("SUBGROUP", typeof(string));

            foreach (GridViewRow gr in gvProductList.Rows)
            {
                DataRow dr = dtNew.NewRow();

                Label lblSerialNo = (Label)gr.FindControl("lblSerialNo");
                Label lblProjectNo = (Label)gr.FindControl("lblProjectNo");
                Label lblEstimatedItem = (Label)gr.FindControl("lblEstimatedItem");
                DropDownList ddlType = (DropDownList)gr.FindControl("ddlType");
                DropDownList ddlCategory = (DropDownList)gr.FindControl("ddlCategory");
                TextBox txtProductCode = (TextBox)gr.FindControl("txtProductCode");
                TextBox txtDescription = (TextBox)gr.FindControl("txtDescription");
                TextBox txtAdditionalDescription = (TextBox)gr.FindControl("txtAdditionalDescription");
                TextBox txtGSTHSN = (TextBox)gr.FindControl("txtGSTHSN");
                TextBox txtGSTRate = (TextBox)gr.FindControl("txtGSTRate");
                DropDownList ddlPurchaseUnit = (DropDownList)gr.FindControl("ddlPurchaseUnit");
                DropDownList ddlStockUnit = (DropDownList)gr.FindControl("ddlStockUnit");
                DropDownList ddlSaleUnit = (DropDownList)gr.FindControl("ddlSaleUnit");
                TextBox txtTagNo = (TextBox)gr.FindControl("txtTagNo");
                TextBox txtModelNo = (TextBox)gr.FindControl("txtModelNo");
                TextBox txtAdditionalInformation = (TextBox)gr.FindControl("txtAdditionalInformation");
                TextBox txtTechnicalSpecification = (TextBox)gr.FindControl("txtTechnicalSpecification");

                Label lblUnitID = (Label)gr.FindControl("lblUnitID");
                Label lblUnit = (Label)gr.FindControl("lblUnit");

                Label lblGroupID = (Label)gr.FindControl("lblGroupID");
                Label lblGroup = (Label)gr.FindControl("lblGroup");
                Label lblSubgroupID = (Label)gr.FindControl("lblSubgroupID");
                Label lblSubgroup = (Label)gr.FindControl("lblSubgroup");

                if (!string.IsNullOrEmpty(lblSerialNo.Text))
                    dr["SERIAL_NO"] = lblSerialNo.Text;
                else
                    dr["SERIAL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblProjectNo.Text))
                    dr["PROJECT_NO"] = lblProjectNo.Text;
                else
                    dr["PROJECT_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(lblEstimatedItem.Text))
                    dr["ESTIMATED_ITEM"] = lblEstimatedItem.Text;
                else
                    dr["PROJECT_NO"] = string.Empty;

                if (ddlType.SelectedIndex > 0)
                    dr["TYPE"] = Convert.ToString(ddlType.SelectedValue);
                else
                    dr["TYPE"] = "0";

                if (ddlCategory.SelectedIndex > 0)
                    dr["CATEGORY"] = Convert.ToString(ddlCategory.SelectedValue);
                else
                    dr["CATEGORY"] = "0";

                if (!string.IsNullOrEmpty(txtProductCode.Text))
                    dr["PRODUCT_CODE"] = txtProductCode.Text;
                else
                    dr["PRODUCT_CODE"] = string.Empty;

                if (!string.IsNullOrEmpty(txtDescription.Text))
                    dr["DESCRIPTION"] = txtDescription.Text;
                else
                    dr["DESCRIPTION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                    dr["ADDITIONAL_DESCRIPTION"] = txtAdditionalDescription.Text;
                else
                    dr["ADDITIONAL_DESCRIPTION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtGSTHSN.Text))
                    dr["GST_HSN"] = txtGSTHSN.Text;
                else
                    dr["GST_HSN"] = string.Empty;

                if (!string.IsNullOrEmpty(txtGSTRate.Text))
                    dr["GST_RATE"] = txtGSTRate.Text;
                else
                    dr["GST_RATE"] = "0";

                if (ddlPurchaseUnit.SelectedIndex > 0)
                    dr["PURCHASE_UNIT"] = Convert.ToString(ddlPurchaseUnit.SelectedValue);
                else
                    dr["PURCHASE_UNIT"] = "0";

                if (ddlStockUnit.SelectedIndex > 0)
                    dr["STOCK_UNIT"] = Convert.ToString(ddlStockUnit.SelectedValue);
                else
                    dr["STOCK_UNIT"] = "0";

                if (ddlSaleUnit.SelectedIndex > 0)
                    dr["SALE_UNIT"] = Convert.ToString(ddlSaleUnit.SelectedValue);
                else
                    dr["SALE_UNIT"] = "0";

                if (!string.IsNullOrEmpty(txtTagNo.Text))
                    dr["TAG_NO"] = txtTagNo.Text;
                else
                    dr["TAG_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtModelNo.Text))
                    dr["MODEL_NO"] = txtModelNo.Text;
                else
                    dr["MODEL_NO"] = string.Empty;

                if (!string.IsNullOrEmpty(txtAdditionalInformation.Text))
                    dr["ADDITIONAL_INFORMATION"] = txtAdditionalInformation.Text;
                else
                    dr["ADDITIONAL_INFORMATION"] = string.Empty;

                if (!string.IsNullOrEmpty(txtTechnicalSpecification.Text))
                    dr["TECHNICAL_SPECIFICATION"] = txtTechnicalSpecification.Text;
                else
                    dr["TECHNICAL_SPECIFICATION"] = string.Empty;

                if (!string.IsNullOrEmpty(lblUnitID.Text))
                    dr["UNIT_ID"] = lblUnitID.Text;
                else
                    dr["UNIT_ID"] = "0";

                if (!string.IsNullOrEmpty(lblUnit.Text))
                    dr["UNIT"] = lblUnit.Text;
                else
                    dr["UNIT"] = string.Empty;                

                if (!string.IsNullOrEmpty(lblGroupID.Text))
                    dr["GROUP_ID"] = lblGroupID.Text;
                else
                    dr["GROUP_ID"] = "0";

                if (!string.IsNullOrEmpty(lblGroup.Text))
                    dr["GROUP"] = lblGroup.Text;
                else
                    dr["GROUP"] = string.Empty;

                if (!string.IsNullOrEmpty(lblSubgroupID.Text))
                    dr["SUBGROUP_ID"] = lblSubgroupID.Text;
                else
                    dr["SUBGROUP_ID"] = "0";

                if (!string.IsNullOrEmpty(lblSubgroup.Text))
                    dr["SUBGROUP"] = lblSubgroup.Text;
                else
                    dr["SUBGROUP"] = string.Empty;


                dtNew.Rows.Add(dr);
            }


            if (dtNew.Rows.Count > 0)
            {
                foreach (DataRow drremove in dtNew.Select("SERIAL_NO='" + serialNo + "'"))
                {
                    dtNew.Rows.Remove(drremove);
                }

                if (dtNew.Rows.Count > 0)
                {
                    Session["dt"] = dtNew;
                    gvProductList.DataSource = dtNew;
                    gvProductList.DataBind();
                }
                else
                {
                    Session["dt"] = null;
                    gvProductList.DataSource = null;
                    gvProductList.DataBind();
                }
            }
            else
            {
                Session["dt"] = null;
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