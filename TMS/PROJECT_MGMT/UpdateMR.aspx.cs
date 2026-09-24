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

public partial class PROJECT_MGMT_UpdateMR : System.Web.UI.Page
{

    #region VARIABLES[==========================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Project objProject = new BAL.Project();

    DataSet dsProject = new DataSet();
    DataSet dsProduct = new DataSet();
    DataSet dsMRType = new DataSet();
    DataSet dsUnit = new DataSet();
    DataSet dsUOM = new DataSet();

    #endregion


    #region EVENTS[=============================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                BindProjectNo();
                BindProduct();
                BindMRType();
                BindUnit();
                BindUOM();
                if (Request.QueryString["mrID"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["mrID"])))
                {
                    BindDetails(Convert.ToInt32(Request.QueryString["mrID"]));
                }
                else
                {
                    Response.Redirect("~/PROJECT_MGMT/MRList.aspx");
                }
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
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProduct()
    {
        try
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ddlProjectNo.SelectedValue)) && Convert.ToString(ddlProjectNo.SelectedValue) != "Select")
            {
                dsProduct = objProject.GetProductCode(Convert.ToString(ddlProjectNo.SelectedValue), 0);
                if (dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
                {

                    ddlProductCode.DataSource = dsProduct.Tables[0];
                    ddlProductCode.DataTextField = "PRODUCT_CODE";
                    ddlProductCode.DataValueField = "PRODUCT_ID";
                    ddlProductCode.DataBind();
                    ddlProductCode.Items.Insert(0, "Select");
                    ddlProductCode.SelectedIndex = 0;
                }
                else
                {
                    ddlProductCode.Items.Clear();
                    ddlProductCode.Items.Insert(0, "Select");
                    ddlProductCode.SelectedIndex = 0;
                }
            }
            else
            {
                ddlProductCode.Items.Clear();
                ddlProductCode.Items.Insert(0, "Select");
                ddlProductCode.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindMRType()
    {
        try
        {
            dsMRType = objProject.GetDetailsBySP("sp_get_mr_type");
            if (dsMRType.Tables.Count > 0 && dsMRType.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsMRType.Tables[0];
                ddlType.DataTextField = "MR_TYPE";
                ddlType.DataValueField = "MR_TYPE_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "Select");
                ddlType.SelectedIndex = 0;
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

    private void BindUOM()
    {
        try
        {
            dsUOM = objProject.GetDetailsBySP("sp_get_unit_of_measurement");
            if (dsUOM.Tables.Count > 0 && dsUOM.Tables[0].Rows.Count > 0)
            {
                ddlUOM.DataSource = dsUOM.Tables[0];
                ddlUOM.DataTextField = "PRODUCT_UNIT";
                ddlUOM.DataValueField = "PRODUCT_UNIT_ID";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, "All");
                ddlUOM.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDetails(int mrID)
    {
        try
        {
            DataSet dsMRDetail = new DataSet();
            dsMRDetail = objProject.GetMRDetail(mrID);
            if (dsMRDetail.Tables.Count > 0 && dsMRDetail.Tables[0].Rows.Count > 0)
            {
                if (dsMRDetail.Tables[0].Rows[0]["MR_NO"] != DBNull.Value)
                    lblMRNo.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["MR_NO"]);

                if (dsMRDetail.Tables[0].Rows[0]["PROJECT_NO"] != DBNull.Value)
                    ddlProjectNo.SelectedValue = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["PROJECT_NO"]);

                BindProduct();
                if (dsMRDetail.Tables[0].Rows[0]["PRODUCT_ID"] != DBNull.Value)
                    ddlProductCode.SelectedValue = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["PRODUCT_ID"]);

                if (dsMRDetail.Tables[0].Rows[0]["TYPE_ID"] != DBNull.Value)
                    ddlType.SelectedValue = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["TYPE_ID"]);

                if (dsMRDetail.Tables[0].Rows[0]["UNIT_ID"] != DBNull.Value)
                    ddlUnit.SelectedValue = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["UNIT_ID"]);

                if (dsMRDetail.Tables[0].Rows[0]["DOCUMENT_CLASS"] != DBNull.Value)
                    txtDocumentClass.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["DOCUMENT_CLASS"]);

                if (dsMRDetail.Tables[0].Rows[0]["DESCRIPTION"] != DBNull.Value)
                    txtDescription.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["DESCRIPTION"]);

                if (dsMRDetail.Tables[0].Rows[0]["UINT_OF_MEASUREMENT_ID"] != DBNull.Value)
                    ddlUOM.SelectedValue = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["UINT_OF_MEASUREMENT_ID"]);

                if (dsMRDetail.Tables[0].Rows[0]["QUANTITY"] != DBNull.Value)
                    txtQuantity.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["QUANTITY"]);

                if (dsMRDetail.Tables[0].Rows[0]["ADDITIONAL_DESCRIPTION"] != DBNull.Value)
                    txtAdditionalDescription.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["ADDITIONAL_DESCRIPTION"]);

                if (dsMRDetail.Tables[0].Rows[0]["EXPECTED_PO_DATE"] != DBNull.Value)
                {
                    hdExpectedPODate.Value = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["EXPECTED_PO_DATE"]);
                    txtExpectedPODate.Text = Convert.ToString(hdExpectedPODate.Value);
                }

                if (dsMRDetail.Tables[0].Rows[0]["DELIVERY_REQUIRED_BY"] != DBNull.Value)
                {
                    hdDeliveryRequiredBy.Value = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["DELIVERY_REQUIRED_BY"]);
                    txtDeliveryRequiredBy.Text = Convert.ToString(hdDeliveryRequiredBy.Value);
                }

                if (dsMRDetail.Tables[0].Rows[0]["AV1"] != DBNull.Value)
                    txtAV1.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["AV1"]);

                if (dsMRDetail.Tables[0].Rows[0]["AV2"] != DBNull.Value)
                    txtAV2.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["AV2"]);

                if (dsMRDetail.Tables[0].Rows[0]["AV3"] != DBNull.Value)
                    txtAV3.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["AV3"]);

                if (dsMRDetail.Tables[0].Rows[0]["AV4"] != DBNull.Value)
                    txtAV4.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["AV4"]);

                if (dsMRDetail.Tables[0].Rows[0]["AV5"] != DBNull.Value)
                    txtAV5.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["AV5"]);

                if (dsMRDetail.Tables[0].Rows[0]["BUDGET"] != DBNull.Value)
                    txtBudget.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["BUDGET"]);

                if (dsMRDetail.Tables[0].Rows[0]["REMARKS"] != DBNull.Value)
                    txtRemarks.Text = Convert.ToString(dsMRDetail.Tables[0].Rows[0]["REMARKS"]);
            }
            else
            {
                Response.Redirect("~/PROJECT_MGMT/MRList.aspx");
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
            int mRID = 0;
            int productID = 0;
            int typeID = 0;
            int unitID = 0;
            string mRNo = string.Empty;
            string docuementClass = string.Empty;
            string description = string.Empty;
            int uomID = 0;
            double quantity = 0;
            string additionalDescription = string.Empty;
            string av1 = string.Empty;
            string av2 = string.Empty;
            string av3 = string.Empty;
            string av4 = string.Empty;
            string av5 = string.Empty;
            double budget = 0;
            string remarks = string.Empty;

            string expectedPODate = string.Empty;
            string deliveryRequiredBy = string.Empty;

            if (Request.QueryString["mrID"] != null && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["mrID"])))
            {
                mRID = Convert.ToInt32(Request.QueryString["mrID"]);
            }
            else
            {
                mRID = 0;
                return;
            }

            if (ddlProductCode.SelectedIndex > 0)
                productID = Convert.ToInt32(ddlProductCode.SelectedValue);
            else
                productID = 0;

            if (ddlType.SelectedIndex > 0)
                typeID = Convert.ToInt32(ddlType.SelectedValue);
            else
                typeID = 0;

            if (ddlUnit.SelectedIndex > 0)
                unitID = Convert.ToInt32(ddlUnit.SelectedValue);
            else
                unitID = 0;

            mRNo = lblMRNo.Text;

            if (!string.IsNullOrEmpty(txtDocumentClass.Text))
                docuementClass = txtDocumentClass.Text;
            else
                docuementClass = string.Empty;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;
            else
                description = string.Empty;

            if (ddlUOM.SelectedIndex > 0)
                uomID = Convert.ToInt32(ddlUOM.SelectedValue);
            else
                uomID = 0;

            if (!string.IsNullOrEmpty(txtQuantity.Text) && Convert.ToDouble(txtQuantity.Text) > 0)
                quantity = Convert.ToDouble(txtQuantity.Text);
            else
                quantity = 0;

            if (!string.IsNullOrEmpty(txtAdditionalDescription.Text))
                additionalDescription = txtAdditionalDescription.Text;
            else
                additionalDescription = string.Empty;

            if (!string.IsNullOrEmpty(txtAV1.Text))
                av1 = txtAV1.Text;
            else
                av1 = string.Empty;

            if (!string.IsNullOrEmpty(txtAV2.Text))
                av2 = txtAV2.Text;
            else
                av2 = string.Empty;

            if (!string.IsNullOrEmpty(txtAV3.Text))
                av3 = txtAV3.Text;
            else
                av3 = string.Empty;

            if (!string.IsNullOrEmpty(txtAV4.Text))
                av4 = txtAV4.Text;
            else
                av4 = string.Empty;

            if (!string.IsNullOrEmpty(txtAV5.Text))
                av5 = txtAV5.Text;
            else
                av5 = string.Empty;

            if (!string.IsNullOrEmpty(txtBudget.Text) && Convert.ToDouble(txtBudget.Text) > 0)
                budget = Convert.ToDouble(txtBudget.Text);
            else
                budget = 0;

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = txtRemarks.Text;
            else
                remarks = string.Empty;


            int value = objProject.AddUpdateNewMR(mRID, productID, typeID, unitID, mRNo, docuementClass, description, uomID, quantity,
                                                             additionalDescription, av1, av2, av3, av4, av5, budget,
                                                             remarks, expectedPODate, deliveryRequiredBy, "",
                                                             Convert.ToInt32(Session["EMP_RECORD_ID"]));
            if (value > 0)
            {
                Reset();
                SuccessMessage("MR code updated successfully..!");
                return;

            }
            else
            {
                ExceptionMessage("Please try again..!");
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
        ddlProductCode.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        txtDescription.Text = string.Empty;
        txtDocumentClass.Text = string.Empty;
        ddlUOM.SelectedIndex = 0;
        txtQuantity.Text = "0";
        txtAdditionalDescription.Text = string.Empty;
        txtAV1.Text = string.Empty;
        txtAV2.Text = string.Empty;
        txtAV3.Text = string.Empty;
        txtAV4.Text = string.Empty;
        txtAV5.Text = string.Empty;
        txtBudget.Text = "0";
        txtRemarks.Text = string.Empty;
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
