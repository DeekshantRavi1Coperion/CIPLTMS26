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

public partial class PROJECT_MGMT_AddNewProduct : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Project objProject = new BAL.Project();

    DataSet dsProject = new DataSet();
    DataSet dsProductUnit = new DataSet();
    DataSet dsEstimatedItem = new DataSet();

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
                //ddlEstimatedItem.Items.Insert(0, "Select");
                //ddlEstimatedItem.SelectedIndex = 0;
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddNewProduct();
    }

    protected void ddlProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEstimatedItem();
    }

    protected void btnProductList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PROJECT_MGMT/ProductList.aspx");
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

    private void AddNewProduct()
    {
        try
        {
            string projectNo = string.Empty;
            int typeID = 0;
            int estimatedProjectItemID = 0;
            int categoryID = 0;
            string itemCode = string.Empty;
            string description = string.Empty;
            string additionalDescription = string.Empty;
            string hsn = string.Empty;
            double rate = 0;
            int purchaseUnitID = 0;
            int stockUnitID = 0;
            int saleUnitID = 0;
            string tagNo = string.Empty;
            string modelNo = string.Empty;
            string additionalInformation = string.Empty;
            string technicalSpecification = string.Empty;


            if (ddlProjectNo.SelectedIndex > 0)
                projectNo = Convert.ToString(ddlProjectNo.SelectedValue);
            else
                projectNo = string.Empty;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);
            else
                typeID = 0;

            if (ddlEstimatedItem.SelectedIndex > 0)
                estimatedProjectItemID = Convert.ToInt32(ddlEstimatedItem.SelectedValue);
            else
                estimatedProjectItemID = 0;

            if (ddlCategory.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                categoryID = 0;

            if (!string.IsNullOrEmpty(txtItemCode.Text))
                itemCode = txtItemCode.Text;
            else
                itemCode = string.Empty;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;
            else
                description = string.Empty;

            if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                additionalDescription = txtAdditionalDescription.Text;
            else
                additionalDescription = string.Empty;

            if (!string.IsNullOrEmpty(txtGSTHSN.Text))
                hsn = txtGSTHSN.Text;
            else
                hsn = string.Empty;

            if (!string.IsNullOrEmpty(txtGSTRate.Text))
                rate = Convert.ToDouble(txtGSTRate.Text);
            else
                rate = 0;

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

            if (!string.IsNullOrEmpty(txtTechnicalSpecification.Text))
                technicalSpecification = Convert.ToString(txtTechnicalSpecification.Text);
            else
                technicalSpecification = string.Empty;

            if (!string.IsNullOrEmpty(txtAdditionalInformation.Text))
                additionalInformation = Convert.ToString(txtAdditionalInformation.Text);
            else
                additionalInformation = string.Empty;

            if (!string.IsNullOrEmpty(txtTagNo.Text))
                tagNo = Convert.ToString(txtTagNo.Text);
            else
                tagNo = string.Empty;

            int value = objProject.AddUpdateNewProduct(0, projectNo, typeID, estimatedProjectItemID, categoryID, itemCode, description,
                                                additionalDescription, hsn, rate, purchaseUnitID, stockUnitID, saleUnitID,
                                                tagNo, modelNo, additionalInformation, technicalSpecification,
                                                Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                DataSet dsItemCode = new DataSet();
                dsItemCode = objProject.GetItemCode(value);
                if (dsItemCode.Tables.Count > 0 && dsItemCode.Tables[0].Rows.Count > 0)
                    SuccessMessage("Item Code '" + Convert.ToString(dsItemCode.Tables[0].Rows[0]["ITEM_CODE"]) + "' added successfully.");
                else
                    SuccessMessage("Item Code added successfully.");

                Reset();
            }
            else
            {
                ExceptionMessage("Please try again!");
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