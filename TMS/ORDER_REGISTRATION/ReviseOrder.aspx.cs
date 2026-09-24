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

public partial class ORDER_REGISTRATION_ReviseOrder : System.Web.UI.Page
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

            tdRevisionOne.Visible = false;
            tdRevisionTwo.Visible = false;
            tdRevisionThree.Visible = false;
            tdRevisionFour.Visible = false;
            tdRevisionFive.Visible = false;
            tdRevisionSix.Visible = false;
            tdRevisionSeven.Visible = false;
            tdRevisionEight.Visible = false;
            tdRevisionNine.Visible = false;
            tdRevisionTen.Visible = false;


            if (dsOrderReviseList.Tables.Count > 0 && dsOrderReviseList.Tables[0].Rows.Count > 0)
            {
                for (int i = 1; i <= dsOrderReviseList.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 0)
                    {
                        BindRevisionZero(i, dsOrderReviseList);
                    }
                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 1)
                    {
                        BindRevisionOne(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 2)
                    {
                        BindRevisionTwo(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 3)
                    {
                        BindRevisionThree(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 4)
                    {
                        BindRevisionFour(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 5)
                    {
                        BindRevisionFive(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 6)
                    {
                        BindRevisionSix(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 7)
                    {
                        BindRevisionSeven(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 8)
                    {
                        BindRevisionEight(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 9)
                    {
                        BindRevisionNine(i, dsOrderReviseList);
                    }

                    if (Convert.ToInt32(dsOrderReviseList.Tables[0].Rows[i]["REVISION_NO"]) == 10)
                    {
                        BindRevisionTen(i, dsOrderReviseList);
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

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageZero.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);
            
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

    private void BindRevisionOne(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionOne.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINROne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateOne.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueOne.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionTwo(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionTwo.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateTwo.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueTwo.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionThree(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionThree.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateThree.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueThree.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionFour(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionFour.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateFour.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueFour.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionFive(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionFive.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateFive.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueFive.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionSix(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionSix.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencySix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateSix.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueSix.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionSeven(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionSeven.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencySeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateSeven.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueSeven.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionEight(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionEight.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINREight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateEight.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueEight.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionNine(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionNine.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateNine.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueNine.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

    private void BindRevisionTen(int i, DataSet dsOrderReviseList)
    {
        try
        {
            tdRevisionTen.Visible = true;

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"] != DBNull.Value)
                txtBasicValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"] != DBNull.Value)
                txtCurrencyTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CURRENCY_CODE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"] != DBNull.Value)
                txtExchangeRateTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"] != DBNull.Value)
                txtBasicValueINRTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["BASIC_VALUE_INR"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                txtExchangeRateDateTen.Text = Convert.ToDateTime(dsOrderReviseList.Tables[0].Rows[i]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

            if (dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"] != DBNull.Value)
                txtMaterialCostTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["MATERIAL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"] != DBNull.Value)
                txtGrossMarginTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                txtGrossMarginPercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                txtGrossMarginValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["GROSS_MARGIN_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"] != DBNull.Value)
                txtCIDEnggHoursTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                txtCIDEnggHoursRateTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGGHOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCIDEnggHoursValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CID_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"] != DBNull.Value)
                txtCWGEnggHoursTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                txtCWGEnggHoursRateTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_RATE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                txtCWGEnggHoursValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["CWG_ENGG_HOURS_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                txtEstimatedTravelCostTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ESTIMATED_TRAVEL_COST"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"] != DBNull.Value)
                txtSupervisionTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["SUPERVISION"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                txtPurchaseRatePercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                txtPurchaseRateValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["PURCHASE_RATE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                txtWarrantyCostPercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"] != DBNull.Value)
                txtWarrantyCostValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["WARRANTY_COST_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                txtRoyaltyPercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"] != DBNull.Value)
                txtRoyaltyValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["ROYALTY_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                txtInsurancePercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"] != DBNull.Value)
                txtInsuranceValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["INSURANCE_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"] != DBNull.Value)
                txtFraightTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["FRAIGHT"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                txtCommision1PercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"] != DBNull.Value)
                txtCommision1ValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION1_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                txtCommision2PercentageTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_PERCENTAGE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"] != DBNull.Value)
                txtCommision2ValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["COMMISION2_VALUE"]);

            if (dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"] != DBNull.Value)
                txtTotalValueTen.Text = Convert.ToString(dsOrderReviseList.Tables[0].Rows[i]["TOTAL_VALUE"]);

        }
        catch (Exception ex)
        {
            //
        }
    }

}
