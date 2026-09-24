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

public partial class ORDER_REGISTRATION_OrderRegistration : System.Web.UI.Page
{

    #region VARIABLES[===================]

    BAL.Common objCommon = new BAL.Common();
    BAL.Order objOrder = new BAL.Order();

    DataSet dsCustomerAddressDetails = new DataSet();
    DataSet dsDBDetails = new DataSet();
    DataSet dsCurrency = new DataSet();

    DataSet dsDivision = new DataSet();
    DataSet dsBusinessUnit = new DataSet();
    DataSet dsTargetGroup = new DataSet();
    DataSet dsProjectType = new DataSet();
    DataSet dsCustomerCharacter = new DataSet();
    DataSet dsLocation = new DataSet();
    DataSet dsDeliveryTerms = new DataSet();

    DataSet dsProjectAndSalesTeam = new DataSet();

    #endregion


    #region EVENTS[======================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EMP_RECORD_ID"] != null)
        {
            if (!IsPostBack)
            {
                hdExchangeRateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtExchangeRateDate.Text = hdExchangeRateDate.Value.ToString();

                hdDeliveryDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDeliveryDate.Text = hdDeliveryDate.Value.ToString();

                hdOrderRegistrationDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                txtOrderRegistrationDate.Text = hdOrderRegistrationDate.Value.ToString();

                BindCurrency();

                BindDivision();
                BindBusinessUnit();
                BindTargetGroup();
                BindProjectType();
                BindCustomerCharacter();
                BindLocation();
                BindDeleryTerms();

                BindProjectAndSalesTeam();

                Session["DB_DETAILS"] = objCommon.GetDBDetails();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnGetCustomer_Click(object sender, EventArgs e)
    {
        txtPONo.Text = string.Empty;
        ModalPopupExtender1.Show();
    }

    protected void btnSearchCustomer_Click(object sender, EventArgs e)
    {

        ModalPopupExtender1.Show();
        GetCustomerList();
    }

    protected void gvCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (Session["EMP_RECORD_ID"] != null)
            {
                int rowindex = 0;
                GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                rowindex = rowSelect.RowIndex;

                Label lblCustomerCode = gvCustomerList.Rows[rowindex].FindControl("lblCustomerCode") as Label;
                Label lblCustomerName = gvCustomerList.Rows[rowindex].FindControl("lblCustomerName") as Label;
                Label lblPONo = gvCustomerList.Rows[rowindex].FindControl("lblPONo") as Label;
                Label lblPoDate = gvCustomerList.Rows[rowindex].FindControl("lblPoDate") as Label;

                txtCustomerName.Text = Convert.ToString(lblCustomerName.Text).Trim();
                txtCustomerCode.Text = Convert.ToString(lblCustomerCode.Text).Trim();
                txtPONo.Text = Convert.ToString(lblPONo.Text).Trim();
                hdPODate.Value = Convert.ToString(lblPoDate.Text).Trim();
                txtPODate.Text = hdPODate.Value.ToString();
                txtEndUser.Text = Convert.ToString(lblCustomerName.Text).Trim();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertOrderRegistration();
    }

    protected void btnOrderList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ORDER_REGISTRATION/OrderList.aspx?");
    }

    #endregion


    #region METHODS[=====================]

    private void BindDivision()
    {
        try
        {
            dsDivision = objOrder.GetPrimaryDetails("sp_get_division");
            if (dsDivision.Tables.Count > 0 && dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataSource = dsDivision.Tables[0];
                ddlDivision.DataTextField = "DIVISION_NAME";
                ddlDivision.DataValueField = "DIVISION_ID";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindBusinessUnit()
    {
        try
        {
            dsBusinessUnit = objOrder.GetPrimaryDetails("sp_get_business_unit");
            if (dsBusinessUnit.Tables.Count > 0 && dsBusinessUnit.Tables[0].Rows.Count > 0)
            {
                ddlBusinessUnit.DataSource = dsBusinessUnit.Tables[0];
                ddlBusinessUnit.DataTextField = "BUSINESS_UNIT";
                ddlBusinessUnit.DataValueField = "BUSINESS_UNIT_ID";
                ddlBusinessUnit.DataBind();
                ddlBusinessUnit.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindTargetGroup()
    {
        try
        {
            dsTargetGroup = objOrder.GetPrimaryDetails("sp_get_target_group");
            if (dsTargetGroup.Tables.Count > 0 && dsTargetGroup.Tables[0].Rows.Count > 0)
            {
                ddlTargetGroupName.DataSource = dsTargetGroup.Tables[0];
                ddlTargetGroupName.DataTextField = "TARGET_GROUP";
                ddlTargetGroupName.DataValueField = "TARGET_GROUP_ID";
                ddlTargetGroupName.DataBind();
                ddlTargetGroupName.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProjectType()
    {
        try
        {
            dsProjectType = objOrder.GetPrimaryDetails("sp_get_project_type");
            if (dsProjectType.Tables.Count > 0 && dsProjectType.Tables[0].Rows.Count > 0)
            {
                ddlTypeOfProject.DataSource = dsProjectType.Tables[0];
                ddlTypeOfProject.DataTextField = "PROJECT_TYPE";
                ddlTypeOfProject.DataValueField = "PROJECT_TYPE_ID";
                ddlTypeOfProject.DataBind();
                ddlTypeOfProject.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindCustomerCharacter()
    {
        try
        {
            dsCustomerCharacter = objOrder.GetPrimaryDetails("sp_get_customer_character");
            if (dsProjectType.Tables.Count > 0 && dsCustomerCharacter.Tables[0].Rows.Count > 0)
            {
                ddlCustomerCharacter.DataSource = dsCustomerCharacter.Tables[0];
                ddlCustomerCharacter.DataTextField = "CUSTOMER_CHARACTER";
                ddlCustomerCharacter.DataValueField = "CUSTOMER_CHARACTER_ID";
                ddlCustomerCharacter.DataBind();
                ddlCustomerCharacter.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindLocation()
    {
        try
        {
            dsLocation = objOrder.GetPrimaryDetails("sp_get_order_booking_location");
            if (dsLocation.Tables.Count > 0 && dsLocation.Tables[0].Rows.Count > 0)
            {
                ddlOrderBookingLocation.DataSource = dsLocation.Tables[0];
                ddlOrderBookingLocation.DataTextField = "UNIT_NAME";
                ddlOrderBookingLocation.DataValueField = "UNIT_ID";
                ddlOrderBookingLocation.DataBind();
                ddlOrderBookingLocation.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindProjectAndSalesTeam()
    {
        try
        {
            dsProjectAndSalesTeam = objOrder.GetEmployeesForOrderReg();
            if (dsProjectAndSalesTeam.Tables.Count > 0 && dsProjectAndSalesTeam.Tables[0].Rows.Count > 0)
            {
                if (dsProjectAndSalesTeam.Tables[0].Rows.Count > 0)
                {
                    ddlProjectManager.DataSource = dsProjectAndSalesTeam.Tables[0];
                    ddlProjectManager.DataTextField = "EMPLOYEE_NAME";
                    ddlProjectManager.DataValueField = "EMP_RECORD_ID";
                    ddlProjectManager.DataBind();
                    ddlProjectManager.Items.Insert(0, "Select");


                    ddlProjectEngineer.DataSource = dsProjectAndSalesTeam.Tables[0];
                    ddlProjectEngineer.DataTextField = "EMPLOYEE_NAME";
                    ddlProjectEngineer.DataValueField = "EMP_RECORD_ID";
                    ddlProjectEngineer.DataBind();
                    ddlProjectEngineer.Items.Insert(0, "Select");
                }

                if (dsProjectAndSalesTeam.Tables[1].Rows.Count > 0)
                {
                    ddlSalesManager.DataSource = dsProjectAndSalesTeam.Tables[1];
                    ddlSalesManager.DataTextField = "EMPLOYEE_NAME";
                    ddlSalesManager.DataValueField = "EMP_RECORD_ID";
                    ddlSalesManager.DataBind();
                    ddlSalesManager.Items.Insert(0, "Select");


                    ddlSalesEngineer.DataSource = dsProjectAndSalesTeam.Tables[1];
                    ddlSalesEngineer.DataTextField = "EMPLOYEE_NAME";
                    ddlSalesEngineer.DataValueField = "EMP_RECORD_ID";
                    ddlSalesEngineer.DataBind();
                    ddlSalesEngineer.Items.Insert(0, "Select");
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void BindDeleryTerms()
    {
        try
        {
            dsDeliveryTerms = objOrder.GetPrimaryDetails("sp_get_delivery_terms");
            if (dsLocation.Tables.Count > 0 && dsDeliveryTerms.Tables[0].Rows.Count > 0)
            {
                ddlDeliveryTerms.DataSource = dsDeliveryTerms.Tables[0];
                ddlDeliveryTerms.DataTextField = "DELIVERY_TERM";
                ddlDeliveryTerms.DataValueField = "DELIVERY_TERM_ID";
                ddlDeliveryTerms.DataBind();
                ddlDeliveryTerms.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void GetCustomerList()
    {
        try
        {
            string customerName = string.Empty;
            string customerCode = string.Empty;
            string dbNameA35 = string.Empty;
            string dbNameDLH = string.Empty;
            string dbNameSEZ = string.Empty;
            string dbNameGNU = string.Empty;

            dsDBDetails = (DataSet)Session["DB_DETAILS"];
            if (dsDBDetails.Tables.Count > 0 && dsDBDetails.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDBDetails.Tables[0].Rows)
                {
                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "A35")
                        dbNameA35 = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && (Convert.ToString(dr["UNIT_NAME"]) == "DLH" || Convert.ToString(dr["UNIT_NAME"]) == "DELHI"))
                        dbNameDLH = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "SEZ")
                        dbNameSEZ = Convert.ToString(dr["DATABASE_NAME"]);

                    if (dr["UNIT_NAME"] != DBNull.Value && Convert.ToString(dr["UNIT_NAME"]) == "GNU")
                        dbNameGNU = Convert.ToString(dr["DATABASE_NAME"]);
                }
            }

            if (!string.IsNullOrEmpty(txtCustmerNameSearch.Text))
                customerName = txtCustmerNameSearch.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCodeSearch.Text))
                customerCode = txtCustomerCodeSearch.Text;
            else
                customerCode = string.Empty;

            lblCustomerMsg.Visible = false;
            dsCustomerAddressDetails = objOrder.GetCustomerListForOrder(customerName, customerCode, dbNameA35, dbNameDLH, dbNameSEZ, dbNameGNU);
            if (dsCustomerAddressDetails.Tables.Count > 0 && dsCustomerAddressDetails.Tables[0].Rows.Count > 0)
            {
                gvCustomerList.DataSource = dsCustomerAddressDetails.Tables[0];
                gvCustomerList.DataBind();
            }
            else
            {
                lblCustomerMsg.Visible = true;
                lblCustomerMsg.Text = "No Data found!";
                gvCustomerList.DataSource = null;
                gvCustomerList.DataBind();
            }
            lblRecords.Text = "Records[" + gvCustomerList.Rows.Count + "]";
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
        }
    }

    private void BindCurrency()
    {
        try
        {
            dsCurrency = objOrder.GetPrimaryDetails("sp_get_currency_list");
            if (dsCurrency.Tables.Count > 0 && dsCurrency.Tables[0].Rows.Count > 0)
            {
                ddlCurrency.DataSource = dsCurrency.Tables[0];
                ddlCurrency.DataTextField = "CURRENCY_CODE";
                ddlCurrency.DataValueField = "CURRENCY_ID";
                ddlCurrency.DataBind();
                ddlCurrency.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage(ex.ToString());
            return;
        }
    }

    private void InsertOrderRegistration()
    {
        try
        {
            string orderNo = string.Empty;
            string GSSNo = string.Empty;
            string customerName = string.Empty;
            string customerCode = string.Empty;
            string endUserName = string.Empty;
            string poNo = string.Empty;
            string poDate = string.Empty;
            string hsnCode = string.Empty;
            string description = string.Empty;
            double basicValue = 0;
            int basicValueCurrencyId = 0;
            double exchangeRate = 0;
            double basicValueINR = 0;
            string exchangeRateDate = string.Empty;
            double materialCost = 0;
            double grossMarginValue = 0;
            double grossMarginPercentage = 0;
            double cidEngghours = 0;
            double cidEngghoursRate = 0;
            double cidEngghoursValue = 0;
            double cwgEngghours = 0;
            double cwgEngghoursRate = 0;
            double cwgEngghoursValue = 0;
            double estimatedTravelCost = 0;
            double supervision = 0;
            double purchaseRatePercentage = 0;
            double purchaseRateValue = 0;
            double warrantyCostPercentage = 0;
            double warrantyCostValue = 0;
            double royaltyPercentage = 0;
            double royaltyValue = 0;
            double insurancePercentage = 0;
            double insuranceValue = 0;
            double fraight = 0;
            double commision1Percentage = 0;
            double commision1Value = 0;
            double commision2Percentage = 0;
            double commision2Value = 0;
            double totalValue = 0;
            int divisionID = 0;
            int businessUnitID = 0;
            int targetGroupID = 0;
            string industryCode = string.Empty;
            int projectTypeID = 0;
            int customerCharacterID = 0;
            int orderBookingLocationID = 0;
            int projectManagerID = 0;
            int projectEngineerID = 0;
            int salesManagerID = 0;
            int salesEngineerID = 0;
            string quotationNo = string.Empty;
            int estimateAttached = 0;
            string paymentTerms = string.Empty;
            int creditDays = 0;
            int deliveryTermsID = 0;
            int ld = 0;
            string deliveryDate = string.Empty;
            string orderRegistrationDate = string.Empty;
            string remarks = string.Empty;


            if (!string.IsNullOrEmpty(txtOrderNo.Text))
                orderNo = txtOrderNo.Text;
            else
                orderNo = string.Empty;

            if (!string.IsNullOrEmpty(txtGSSNo.Text))
                GSSNo = txtGSSNo.Text;
            else
                GSSNo = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerName.Text))
                customerName = txtCustomerName.Text;
            else
                customerName = string.Empty;

            if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                customerCode = txtCustomerCode.Text;
            else
                customerCode = string.Empty;

            if (!string.IsNullOrEmpty(txtEndUser.Text))
                endUserName = txtEndUser.Text;
            else
                endUserName = string.Empty;

            if (!string.IsNullOrEmpty(txtPONo.Text))
                poNo = txtPONo.Text;
            else
                poNo = string.Empty;

            if (!string.IsNullOrEmpty(txtPODate.Text))
                poDate = Convert.ToDateTime(txtPODate.Text).ToString("yyyy-MM-dd");
            else
                poDate = string.Empty;

            if (!string.IsNullOrEmpty(txtHSNCode.Text))
                hsnCode = txtHSNCode.Text;
            else
                hsnCode = string.Empty;

            if (!string.IsNullOrEmpty(txtDescription.Text))
                description = txtDescription.Text;
            else
                description = string.Empty;

            if (!string.IsNullOrEmpty(txtBasicValue.Text))
                basicValue = Convert.ToDouble(txtBasicValue.Text);
            else
                basicValue = 0;

            if (ddlCurrency.SelectedIndex > 0)
                basicValueCurrencyId = Convert.ToInt32(ddlCurrency.SelectedValue);
            else
                basicValueCurrencyId = 0;

            if (!string.IsNullOrEmpty(txtExchangeRate.Text))
                exchangeRate = Convert.ToDouble(txtExchangeRate.Text);
            else
                exchangeRate = 0;

            if (Convert.ToDouble(hdBasicValueINR.Value) > 0)
                basicValueINR = Convert.ToDouble(hdBasicValueINR.Value);
            else
                basicValueINR = 0;

            exchangeRateDate = Convert.ToDateTime(hdExchangeRateDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtMaterialCost.Text))
                materialCost = Convert.ToDouble(txtMaterialCost.Text);
            else
                materialCost = 0;

            if (Convert.ToDouble(hdGrossMarginValue.Value) > 0)
                grossMarginValue = Convert.ToDouble(hdGrossMarginValue.Value);
            else
                grossMarginValue = 0;

            if (Convert.ToDouble(hdGrossMarginPercentage.Value) > 0)
                grossMarginPercentage = Convert.ToDouble(hdGrossMarginPercentage.Value);
            else
                grossMarginPercentage = 0;

            if (!string.IsNullOrEmpty(txtCIDEnggHours.Text))
                cidEngghours = Convert.ToDouble(txtCIDEnggHours.Text);
            else
                cidEngghours = 0;

            if (!string.IsNullOrEmpty(txtCIDEnggHoursRate.Text))
                cidEngghoursRate = Convert.ToDouble(txtCIDEnggHoursRate.Text);
            else
                cidEngghoursRate = 0;

            if (Convert.ToDouble(hdCIDEnggHoursValue.Value) > 0)
                cidEngghoursValue = Convert.ToDouble(hdCIDEnggHoursValue.Value);
            else
                cidEngghoursValue = 0;

            if (!string.IsNullOrEmpty(txtCWGEnggHours.Text))
                cwgEngghours = Convert.ToDouble(txtCWGEnggHours.Text);
            else
                cwgEngghours = 0;

            if (!string.IsNullOrEmpty(txtCWGEnggHoursRate.Text))
                cwgEngghoursRate = Convert.ToDouble(txtCWGEnggHoursRate.Text);
            else
                cwgEngghoursRate = 0;

            if (Convert.ToDouble(hdCWGEnggHoursValue.Value) > 0)
                cwgEngghoursValue = Convert.ToDouble(hdCWGEnggHoursValue.Value);
            else
                cwgEngghoursValue = 0;

            if (!string.IsNullOrEmpty(txtEstimatedTravelCost.Text))
                estimatedTravelCost = Convert.ToDouble(txtEstimatedTravelCost.Text);
            else
                estimatedTravelCost = 0;

            if (!string.IsNullOrEmpty(txtSupervision.Text))
                supervision = Convert.ToDouble(txtSupervision.Text);
            else
                supervision = 0;

            if (!string.IsNullOrEmpty(txtPurchaseRatePercentage.Text))
                purchaseRatePercentage = Convert.ToDouble(txtPurchaseRatePercentage.Text);
            else
                purchaseRatePercentage = 0;

            if (Convert.ToDouble(hdPurchaseRateValue.Value) > 0)
                purchaseRateValue = Convert.ToDouble(hdPurchaseRateValue.Value);
            else
                purchaseRateValue = 0;

            if (!string.IsNullOrEmpty(txtWarrantyCostPercentage.Text))
                warrantyCostPercentage = Convert.ToDouble(txtWarrantyCostPercentage.Text);
            else
                warrantyCostPercentage = 0;

            if (!string.IsNullOrEmpty(hdWarrantyCostValue.Value))
                warrantyCostValue = Convert.ToDouble(hdWarrantyCostValue.Value);
            else
                warrantyCostValue = 0;

            if (!string.IsNullOrEmpty(txtRoyaltyPercentage.Text))
                royaltyPercentage = Convert.ToDouble(txtRoyaltyPercentage.Text);
            else
                royaltyPercentage = 0;

            if (!string.IsNullOrEmpty(hdRoyaltyValue.Value))
                royaltyValue = Convert.ToDouble(hdRoyaltyValue.Value);
            else
                royaltyValue = 0;

            if (!string.IsNullOrEmpty(txtInsurancePercentage.Text))
                insurancePercentage = Convert.ToDouble(txtInsurancePercentage.Text);
            else
                insurancePercentage = 0;

            if (!string.IsNullOrEmpty(hdInsuranceValue.Value))
                insuranceValue = Convert.ToDouble(hdInsuranceValue.Value);
            else
                insuranceValue = 0;

            if (!string.IsNullOrEmpty(txtFraight.Text))
                fraight = Convert.ToDouble(txtFraight.Text);
            else
                fraight = 0;

            if (!string.IsNullOrEmpty(txtCommision1Percentage.Text))
                commision1Percentage = Convert.ToDouble(txtCommision1Percentage.Text);
            else
                commision1Percentage = 0;

            if (!string.IsNullOrEmpty(hdCommision1Value.Value))
                commision1Value = Convert.ToDouble(hdCommision1Value.Value);
            else
                commision1Value = 0;

            if (!string.IsNullOrEmpty(txtCommision2Percentage.Text))
                commision2Percentage = Convert.ToDouble(txtCommision2Percentage.Text);
            else
                commision2Percentage = 0;

            if (!string.IsNullOrEmpty(hdCommision2Value.Value))
                commision2Value = Convert.ToDouble(hdCommision2Value.Value);
            else
                commision2Value = 0;

            if (!string.IsNullOrEmpty(hdTotalValue.Value))
                totalValue = Convert.ToDouble(hdTotalValue.Value);
            else
                totalValue = 0;


            if (ddlDivision.SelectedIndex > 0)
                divisionID = Convert.ToInt32(ddlDivision.SelectedValue);
            else
                divisionID = 0;

            if (ddlBusinessUnit.SelectedIndex > 0)
                businessUnitID = Convert.ToInt32(ddlBusinessUnit.SelectedValue);
            else
                businessUnitID = 0;

            if (ddlTargetGroupName.SelectedIndex > 0)
                targetGroupID = Convert.ToInt32(ddlTargetGroupName.SelectedValue);
            else
                targetGroupID = 0;

            if (!string.IsNullOrEmpty(txtIndustryCode.Text))
                industryCode = Convert.ToString(txtIndustryCode.Text);
            else
                industryCode = string.Empty;

            if (ddlTypeOfProject.SelectedIndex > 0)
                projectTypeID = Convert.ToInt32(ddlTypeOfProject.SelectedValue);
            else
                projectTypeID = 0;

            if (ddlCustomerCharacter.SelectedIndex > 0)
                customerCharacterID = Convert.ToInt32(ddlCustomerCharacter.SelectedValue);
            else
                customerCharacterID = 0;

            if (ddlOrderBookingLocation.SelectedIndex > 0)
                orderBookingLocationID = Convert.ToInt32(ddlOrderBookingLocation.SelectedValue);
            else
                orderBookingLocationID = 0;

            if (ddlProjectManager.SelectedIndex > 0)
                projectManagerID = Convert.ToInt32(ddlProjectManager.SelectedValue);
            else
                projectManagerID = 0;

            if (ddlProjectEngineer.SelectedIndex > 0)
                projectEngineerID = Convert.ToInt32(ddlProjectEngineer.SelectedValue);
            else
                projectEngineerID = 0;

            if (ddlSalesManager.SelectedIndex > 0)
                salesManagerID = Convert.ToInt32(ddlSalesManager.SelectedValue);
            else
                salesManagerID = 0;

            if (ddlSalesEngineer.SelectedIndex > 0)
                salesEngineerID = Convert.ToInt32(ddlSalesEngineer.SelectedValue);
            else
                salesEngineerID = 0;

            if (!string.IsNullOrEmpty(txtQuotationNo.Text))
                quotationNo = Convert.ToString(txtQuotationNo.Text);
            else
                quotationNo = string.Empty;

            if (ddlEstimateAttached.SelectedIndex > 0)
                estimateAttached = Convert.ToInt32(ddlEstimateAttached.SelectedValue);
            else
                estimateAttached = 0;

            if (!string.IsNullOrEmpty(txtPaymentTerms.Text))
                paymentTerms = Convert.ToString(txtPaymentTerms.Text);
            else
                paymentTerms = string.Empty;

            if (!string.IsNullOrEmpty(txtCraditDays.Text))
                creditDays = Convert.ToInt32(txtCraditDays.Text);
            else
                creditDays = 0;

            if (ddlDeliveryTerms.SelectedIndex > 0)
                deliveryTermsID = Convert.ToInt32(ddlDeliveryTerms.SelectedValue);
            else
                deliveryTermsID = 0;

            if (ddlLd.SelectedIndex > 0)
                ld = Convert.ToInt32(ddlLd.SelectedValue);
            else
                ld = 0;

            deliveryDate = Convert.ToDateTime(hdDeliveryDate.Value).ToString("yyyy-MM-dd");
            orderRegistrationDate = Convert.ToDateTime(hdOrderRegistrationDate.Value).ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(txtRemarks.Text))
                remarks = Convert.ToString(txtRemarks.Text);
            else
                remarks = string.Empty;

            int value = objOrder.InsertOrderRegistration(0, orderNo, GSSNo, customerName, customerCode, endUserName, poNo, poDate, hsnCode, description,
                                    divisionID, businessUnitID, targetGroupID, industryCode, projectTypeID, customerCharacterID, orderBookingLocationID,
                                    projectManagerID, projectEngineerID, salesManagerID, salesEngineerID, quotationNo, estimateAttached, paymentTerms,
                                    creditDays, deliveryTermsID, ld, deliveryDate, orderRegistrationDate, basicValue, basicValueINR, basicValueCurrencyId, exchangeRate,
                                    exchangeRateDate, materialCost, grossMarginValue, grossMarginPercentage, cidEngghours, cidEngghoursRate,
                                    cidEngghoursValue, cwgEngghours, cwgEngghoursRate, cwgEngghoursValue, estimatedTravelCost, supervision,
                                    purchaseRatePercentage, purchaseRateValue, warrantyCostPercentage, warrantyCostValue, royaltyPercentage, royaltyValue,
                                    insurancePercentage, insuranceValue, fraight, commision1Percentage, commision1Value, commision2Percentage,
                                    commision2Value, totalValue, remarks, Convert.ToInt32(Session["EMP_RECORD_ID"]));


            if (value > 0)
            {
                SuccessMessage("Order No.: " + orderNo + " registerd successfully.");
                Reset();
                return;
            }
            else
            {
                ExceptionMessage("Please try again");
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
        try
        {
            txtOrderNo.Text = string.Empty;
            txtGSSNo.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtEndUser.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtPODate.Text = string.Empty;
            txtHSNCode.Text = string.Empty;
            txtDescription.Text = string.Empty;

            txtBasicValue.Text = string.Empty;
            txtExchangeRate.Text = "1";
            ddlCurrency.SelectedValue = "68";
            txtBasicValueINR.Text = string.Empty;
            txtExchangeRateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtMaterialCost.Text = string.Empty;
            txtGrossMarginValue.Text = string.Empty;
            txtGrossMarginPercentage.Text = string.Empty;
            txtCIDEnggHours.Text = string.Empty;
            txtCIDEnggHoursRate.Text = string.Empty;
            txtCIDEnggHoursValue.Text = string.Empty;
            txtCWGEnggHours.Text = string.Empty;
            txtCWGEnggHoursRate.Text = string.Empty;
            txtCWGEnggHoursValue.Text = string.Empty;
            txtEstimatedTravelCost.Text = string.Empty;
            txtSupervision.Text = string.Empty;
            txtPurchaseRatePercentage.Text = string.Empty;
            txtPurchaseRateValue.Text = string.Empty;
            txtWarrantyCostPercentage.Text = string.Empty;
            txtWarrantyCostValue.Text = string.Empty;
            txtRoyaltyPercentage.Text = string.Empty;
            txtRoyaltyValue.Text = string.Empty;
            txtInsurancePercentage.Text = string.Empty;
            txtInsuranceValue.Text = string.Empty;
            txtFraight.Text = string.Empty;
            txtCommision1Percentage.Text = string.Empty;
            txtCommision1Value.Text = string.Empty;
            txtCommision2Percentage.Text = string.Empty;
            txtCommision2Value.Text = string.Empty;
            txtTotalValue.Text = string.Empty;

            ddlDivision.SelectedIndex = 0;
            ddlBusinessUnit.SelectedIndex = 0;
            ddlTargetGroupName.SelectedIndex = 0;
            txtIndustryCode.Text = string.Empty;
            ddlTypeOfProject.SelectedIndex = 0;
            ddlCustomerCharacter.SelectedIndex = 0;
            ddlOrderBookingLocation.SelectedIndex = 0;

            ddlProjectManager.SelectedIndex = 0;
            ddlProjectEngineer.SelectedIndex = 0;
            ddlSalesManager.SelectedIndex = 0;
            ddlSalesEngineer.SelectedIndex = 0;
            txtQuotationNo.Text = string.Empty;
            ddlEstimateAttached.SelectedIndex = 0;

            txtPaymentTerms.Text = string.Empty;
            txtCraditDays.Text = string.Empty;
            ddlDeliveryTerms.SelectedIndex = 0;
            ddlLd.SelectedIndex = 0;
            txtDeliveryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtOrderRegistrationDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtRemarks.Text = string.Empty;

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
    
    #endregion

}
