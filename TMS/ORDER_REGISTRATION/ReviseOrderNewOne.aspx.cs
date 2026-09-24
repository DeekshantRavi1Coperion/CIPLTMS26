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

public partial class ORDER_REGISTRATION_ReviseOrderNew : System.Web.UI.Page
{

    BAL.Order objOrder = new BAL.Order();
    DataSet dsOrderReviseList = new DataSet();
    DataSet dsCurrency = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdExchangeRateDate.Value = DateTime.Now.ToString("dd-MMM-yyyy");
            txtExchangeRateDate.Text = hdExchangeRateDate.Value.ToString();

            BindCurrency();
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

                ddlRevisionValueCurrency.DataSource = dsCurrency.Tables[0];
                ddlRevisionValueCurrency.DataTextField = "CURRENCY_CODE";
                ddlRevisionValueCurrency.DataValueField = "CURRENCY_ID";
                ddlRevisionValueCurrency.DataBind();
                ddlRevisionValueCurrency.SelectedValue = "68";
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            //return;
        }
    }


    private void GetAndBindDetails()
    {
        try
        {
            int orderID = Convert.ToInt32(Request.QueryString["orderid"]);

            dsOrderReviseList = objOrder.GetOrderReviseList(orderID);
            
            if (dsOrderReviseList.Tables.Count > 0 && dsOrderReviseList.Tables[0].Rows.Count > 0)
            {
                for (int i = 1; i <= dsOrderReviseList.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 0)
                    {
                        BindRevisionZero(i, dsOrderReviseList);
                    }
                    
                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 10)
                    {
                        BindRevisionLast(i, dsOrderReviseList);
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void BindRevisionZero(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionZero.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateZero.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionLast(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionLast.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateLast.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueLast.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

}
