using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PROJECT_MGMT_UpdateProductNewOne : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Project objProject = new BAL.Project();
    BAL.Common objCommon = new BAL.Common();

    DataSet dsProject = new DataSet();
    DataSet dsGroup = new DataSet();
    DataSet dsSubgroup = new DataSet();
    DataSet dsProductUnit = new DataSet();
    DataSet dsEstimatedItem = new DataSet();
    DataSet dsUnit = new DataSet();
    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindProjectNo();
                BindProjectUnit();
                BindUnit();
                if (Request.QueryString["productID"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["productID"])))                
                    BindDetails(Convert.ToInt32(Request.QueryString["productID"]));                
                else                
                    Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");                
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateProduct();
    }

    protected void ddlProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemCode();
        BindEstimatedItem();
        BindGroup();
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedIndex > 0)
        {
            BindGroup();
        }
        else
        {
            ddlGroup.Items.Clear();
            ddlGroup.Items.Insert(0, "Select");
            ddlGroup.SelectedIndex = 0;            
        }
        ddlSubgroup.Items.Clear();
        ddlSubgroup.Items.Insert(0, "Select");
        ddlSubgroup.SelectedIndex = 0;
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemCode();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemCode();
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
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetItemCode()
    {
        try
        {
            string projectNo = string.Empty;
            string type = string.Empty;
            string category = string.Empty;

            if (ddlProjectNo.SelectedIndex > 0)
                projectNo = Convert.ToString(ddlProjectNo.SelectedItem.Text).Substring(5, 7);
            else
                projectNo = string.Empty;


            if (ddlType.SelectedIndex > 0)
                type = Convert.ToString(ddlType.SelectedItem.Text).Substring(0, 1);
            else
                type = string.Empty;


            if (ddlCategory.SelectedIndex > 0)
                category = Convert.ToString(ddlCategory.SelectedItem.Text).Substring(0, 1);
            else
                category = string.Empty;

            txtProductCode.Text = type + category + projectNo;

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

    private void BindEstimatedItem()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ddlProjectNo.SelectedValue)) && Convert.ToString(ddlProjectNo.SelectedValue) != "Select")
            {
                dsEstimatedItem = objProject.GetEstimatedProduct(Convert.ToString(ddlProjectNo.SelectedValue));
                if (dsEstimatedItem.Tables.Count > 0)
                {
                    if (dsEstimatedItem.Tables[0].Rows.Count > 0)
                    {
                        ddlEstimatedItem.DataSource = dsEstimatedItem.Tables[0];
                        ddlEstimatedItem.DataTextField = "ESTIMATED_ITEM";
                        ddlEstimatedItem.DataValueField = "ESTIMATED_PROJECT_ID";
                        ddlEstimatedItem.DataBind();
                        ddlEstimatedItem.Items.Insert(0, "Select");
                        ddlEstimatedItem.SelectedIndex = 0;

                    }
                    else
                    {
                        ddlEstimatedItem.Items.Clear();
                        ddlEstimatedItem.Items.Insert(0, "Select");
                        ddlEstimatedItem.SelectedIndex = 0;
                    }

                    if (dsEstimatedItem.Tables[1].Rows.Count > 0)
                    {

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                ddlEstimatedItem.Items.Clear();
                ddlEstimatedItem.Items.Insert(0, "Select");
                ddlEstimatedItem.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProjectUnit()
    {
        try
        {
            dsProductUnit = objProject.GetDetailsBySP("sp_get_product_unit");
            if (dsProductUnit.Tables.Count > 0 && dsProductUnit.Tables[0].Rows.Count > 0)
            {
                ddlPurchaseUnit.DataSource = dsProductUnit.Tables[0];
                ddlPurchaseUnit.DataTextField = "PRODUCT_UNIT";
                ddlPurchaseUnit.DataValueField = "PRODUCT_UNIT_ID";
                ddlPurchaseUnit.DataBind();
                ddlPurchaseUnit.Items.Insert(0, "Select");
                ddlPurchaseUnit.SelectedIndex = 0;

                ddlStockUnit.DataSource = dsProductUnit.Tables[0];
                ddlStockUnit.DataTextField = "PRODUCT_UNIT";
                ddlStockUnit.DataValueField = "PRODUCT_UNIT_ID";
                ddlStockUnit.DataBind();
                ddlStockUnit.Items.Insert(0, "Select");
                ddlStockUnit.SelectedIndex = 0;

                ddlSaleUnit.DataSource = dsProductUnit.Tables[0];
                ddlSaleUnit.DataTextField = "PRODUCT_UNIT";
                ddlSaleUnit.DataValueField = "PRODUCT_UNIT_ID";
                ddlSaleUnit.DataBind();
                ddlSaleUnit.Items.Insert(0, "Select");
                ddlSaleUnit.SelectedIndex = 0;
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

    private void BindDetails(int productID)
    {
        try
        {
            DataSet dsProductDetail = new DataSet();
            dsProductDetail = objProject.GetProductDetail(productID);
            if (dsProductDetail.Tables.Count > 0 && dsProductDetail.Tables[0].Rows.Count > 0)
            {

                if (dsProductDetail.Tables[0].Rows[0]["PROJECT_NO"] != DBNull.Value)
                    ddlProjectNo.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["PROJECT_NO"]);

                BindEstimatedItem();
                if (dsProductDetail.Tables[0].Rows[0]["ESTIMATED_PROJECT_ITEM_ID"] != DBNull.Value)
                    ddlEstimatedItem.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["ESTIMATED_PROJECT_ITEM_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["TYPE_ID"] != DBNull.Value)
                    ddlType.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["TYPE_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["CATEGORY_ID"] != DBNull.Value)
                    ddlCategory.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["CATEGORY_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["PRODUCT_CODE"] != DBNull.Value)
                    txtProductCode.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["PRODUCT_CODE"]);

                if (dsProductDetail.Tables[0].Rows[0]["DESCRIPTION"] != DBNull.Value)
                    txtDescription.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["DESCRIPTION"]);

                if (dsProductDetail.Tables[0].Rows[0]["ADDITIONAL_DESCRIPTION"] != DBNull.Value)
                    txtAdditionalDescription.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["ADDITIONAL_DESCRIPTION"]);

                if (dsProductDetail.Tables[0].Rows[0]["GST_HSN"] != DBNull.Value)
                    txtGSTHSN.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["GST_HSN"]);

                if (dsProductDetail.Tables[0].Rows[0]["GST_RATE"] != DBNull.Value)
                    txtGSTRate.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["GST_RATE"]);

                if (dsProductDetail.Tables[0].Rows[0]["PURCHASE_UNIT_ID"] != DBNull.Value)
                    ddlPurchaseUnit.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["PURCHASE_UNIT_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["SALE_UNIT_ID"] != DBNull.Value)
                    ddlSaleUnit.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["SALE_UNIT_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["STOCK_UNIT_ID"] != DBNull.Value)
                    ddlStockUnit.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["STOCK_UNIT_ID"]);

                if (dsProductDetail.Tables[0].Rows[0]["TAG_NO"] != DBNull.Value)
                    txtTagNo.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["TAG_NO"]);

                if (dsProductDetail.Tables[0].Rows[0]["MODEL_NO"] != DBNull.Value)
                    txtModelNo.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["MODEL_NO"]);

                if (dsProductDetail.Tables[0].Rows[0]["ADDITIONAL_INFORMATION"] != DBNull.Value)
                    txtAdditionalInformation.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["ADDITIONAL_INFORMATION"]);

                if (dsProductDetail.Tables[0].Rows[0]["TECHNICAL_SPECIFICATION"] != DBNull.Value)
                    txtTechnicalSpecification.Text = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["TECHNICAL_SPECIFICATION"]);

                BindUnit();
                if (dsProductDetail.Tables[0].Rows[0]["UNIT_ID"] != DBNull.Value)
                    ddlUnit.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["UNIT_ID"]);

                BindGroup();
                if (dsProductDetail.Tables[0].Rows[0]["GROUP_ID"] != DBNull.Value)
                    ddlGroup.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["GROUP_ID"]);

                BindSubgroup();
                if (dsProductDetail.Tables[0].Rows[0]["SUBGROUP_ID"] != DBNull.Value)
                    ddlSubgroup.SelectedValue = Convert.ToString(dsProductDetail.Tables[0].Rows[0]["SUBGROUP_ID"]);

            }
            else
            {
                Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void UpdateProduct()
    {
        try
        {
            int productID = 0;
            string projectNo = string.Empty;
            int typeID = 0;
            int estimatedProdctID = 0;
            int categoryID = 0;
            string productCode = string.Empty;
            string description = string.Empty;
            string additionalDescription = string.Empty;
            string gSTHsn = string.Empty;
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

            if (Request.QueryString["productID"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["productID"])))
            {
                productID = Convert.ToInt32(Request.QueryString["productID"]);
            }
            else
            {
                productID = 0;
                return;
            }


            if (ddlProjectNo.SelectedIndex > 0)
                projectNo = Convert.ToString(ddlProjectNo.SelectedValue);
            else
                projectNo = string.Empty;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);
            else
                typeID = 0;

            if (ddlEstimatedItem.SelectedIndex > 0)
                estimatedProdctID = Convert.ToInt32(ddlEstimatedItem.SelectedValue);
            else
                estimatedProdctID = 0;

            if (ddlCategory.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                categoryID = 0;

            if (!string.IsNullOrEmpty(txtProductCode.Text))
                productCode = txtProductCode.Text;
            else
                productCode = string.Empty;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;
            else
                description = string.Empty;

            if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                additionalDescription = txtAdditionalDescription.Text;
            else
                additionalDescription = string.Empty;

            if (!string.IsNullOrEmpty(txtGSTHSN.Text))
                gSTHsn = txtGSTHSN.Text;
            else
                gSTHsn = string.Empty;

            if (!string.IsNullOrEmpty(txtGSTRate.Text))
                gSTRate = Convert.ToDouble(txtGSTRate.Text);
            else
                gSTRate = 0;

            if (ddlPurchaseUnit.SelectedIndex > 0)
                purchaseUnitID = Convert.ToInt32(ddlPurchaseUnit.SelectedValue);
            else
                purchaseUnitID = 0;

            if (ddlStockUnit.SelectedIndex > 0)
                stockUnitID = Convert.ToInt32(ddlStockUnit.SelectedValue);
            else
                stockUnitID = 0;

            if (ddlSaleUnit.SelectedIndex > 0)
                saleUnitID = Convert.ToInt32(ddlSaleUnit.SelectedValue);
            else
                saleUnitID = 0;

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = Convert.ToString(txtTagNo.Text);
            else
                tagNo = string.Empty;

            if (!string.IsNullOrEmpty(txtModelNo.Text))
                modelNo = Convert.ToString(txtModelNo.Text);
            else
                modelNo = string.Empty;

            if (!string.IsNullOrEmpty(txtTechnicalSpecification.Text))
                technicalSpecification = Convert.ToString(txtTechnicalSpecification.Text);
            else
                technicalSpecification = string.Empty;

            if (!string.IsNullOrEmpty(txtAdditionalInformation.Text))
                additionalInformation = Convert.ToString(txtAdditionalInformation.Text);
            else
                additionalInformation = string.Empty;

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
            else
                unitID = 0;

            if (ddlGroup.SelectedIndex > 0)
                groupID = Convert.ToString(ddlGroup.SelectedValue);
            else
                groupID = string.Empty;

            if (ddlSubgroup.SelectedIndex > 0)
                subGroupID = Convert.ToString(ddlSubgroup.SelectedValue);
            else
                subGroupID = string.Empty;

            int value = objProject.AddUpdateNewProductNew(productID, projectNo, estimatedProdctID, typeID, categoryID, productCode, description,
                                                        additionalDescription, gSTHsn, gSTRate, purchaseUnitID, stockUnitID, saleUnitID,
                                                        tagNo, modelNo, additionalInformation, technicalSpecification,
                                                        unitID, groupID, subGroupID, Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value < 0)
            {
                SuccessMessage("Product code already exists..!");
                return;
            }
            else if (value == 0)
            {
                ExceptionMessage("Please try again..!");
                return;
            }
            else
            {
                Reset();
                SuccessMessage("Product code updated successfully..!");
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void Reset()
    {
        ddlProjectNo.SelectedIndex = 0;
        ddlEstimatedItem.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        txtAdditionalDescription.Text = string.Empty;
        txtGSTHSN.Text = string.Empty;
        txtGSTRate.Text = string.Empty;
        ddlPurchaseUnit.SelectedIndex = 0;
        ddlStockUnit.SelectedIndex = 0;
        ddlSaleUnit.SelectedIndex = 0;
        txtTagNo.Text = string.Empty;
        txtModelNo.Text = string.Empty;
        txtAdditionalInformation.Text = string.Empty;
        ddlGroup.SelectedIndex = 0;
        ddlSubgroup.SelectedIndex = 0;
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
