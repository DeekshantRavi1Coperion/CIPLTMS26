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
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using BAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class ORDER_REGISTRATION_OrderDetailPDF : System.Web.UI.Page
{

    #region VARIABLES[=====================]

    #endregion


    #region EVENTS[========================]

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["OrderDetail"] = null;
            BindOrderDetail();
        }
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string orderNo = string.Empty;
            DataSet ds = (DataSet)Session["OrderDetail"];
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ORDER_NO"] != DBNull.Value)
                    orderNo = Convert.ToString(ds.Tables[0].Rows[0]["ORDER_NO"]);
            }

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=OrderNo_" + orderNo + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 30f, 0f);//(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }

    #endregion


    #region METHODS[=======================]

    private void CreateTable()
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();

        cell.ColSpan = 6;
        cell.Align = "center";
        cell.InnerText = "Quotation Summary";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Basic Value";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        Label lblBasivValue = new Label();
        lblBasivValue.Text = "100";
        cell.Controls.Add(lblBasivValue);
        row.Cells.Add(cell);

        tblOrderList.Rows.Add(row);


        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Exchange Rate";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Basic Value INR";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Exchange Rate Date";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Material Cost";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Gross Margin";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Gross Margin(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Gross Margin Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CID Engg. Hours";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);


        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CID Engg. Hours Rate";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CID Engg. Hours Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CWG Engg. Hours";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);


        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CWG Engg. Hours Rate";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "CWG Engg. Hours Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);




        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Estimated Travel Cost";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Supervision";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Purchase Overhead/Mtrl Rate(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Purchase Rate Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Warranty Cost(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Warranty Cost Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Royalty(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Royalty Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);


        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Insurance(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Insurance Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Fraight";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Commision1(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Commision1 Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Commision2(%)";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Commision2 Value";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);

        row = new HtmlTableRow();
        cell = new HtmlTableCell();
        cell.InnerText = "Total";
        row.Cells.Add(cell);
        tblOrderList.Rows.Add(row);


    }

    private void BindOrderDetail()
    {
        try
        {
            BAL.Order objOrder = new BAL.Order();
            DataSet dsOrderList = new DataSet();

            int orderID = 0;

            if (Request.QueryString["orderID"] != null && Convert.ToInt32(Request.QueryString["orderID"]) > 0)
            {
                orderID = Convert.ToInt32(Request.QueryString["orderID"]);

                dsOrderList = objOrder.GetOrderDetailForPDF(orderID);
                if (dsOrderList.Tables.Count > 0)
                {
                    Session["OrderDetail"] = dsOrderList;
                    if (dsOrderList.Tables[0].Rows.Count > 0)
                    {
                        if (dsOrderList.Tables[0].Rows[0]["ORDER_NO"] != DBNull.Value)
                            lblOrderNo.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["ORDER_NO"]);

                        if (dsOrderList.Tables[0].Rows[0]["GSS_NO"] != DBNull.Value)
                            lblGSSNo.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["GSS_NO"]);

                        if (dsOrderList.Tables[0].Rows[0]["CUSTOMER_NAME"] != DBNull.Value)
                            lblCustomerName.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["CUSTOMER_NAME"]);

                        if (dsOrderList.Tables[0].Rows[0]["CUSTOMER_CODE"] != DBNull.Value)
                            lblCustomerCode.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["CUSTOMER_CODE"]);

                        if (dsOrderList.Tables[0].Rows[0]["END_USER_NAME"] != DBNull.Value)
                            lblEndUser.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["END_USER_NAME"]);

                        if (dsOrderList.Tables[0].Rows[0]["PO_NO"] != DBNull.Value)
                            lblPONo.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PO_NO"]);

                        if (dsOrderList.Tables[0].Rows[0]["PO_DATE"] != DBNull.Value)
                            lblPODate.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PO_DATE"]);

                        if (dsOrderList.Tables[0].Rows[0]["HSN_CODE"] != DBNull.Value)
                            lblHSNCode.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["HSN_CODE"]);

                        if (dsOrderList.Tables[0].Rows[0]["DESCRIPTION"] != DBNull.Value)
                            lblDescription.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["DESCRIPTION"]);

                        if (dsOrderList.Tables[0].Rows[0]["DIVISION_NAME"] != DBNull.Value)
                            lblDivision.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["DIVISION_NAME"]);

                        if (dsOrderList.Tables[0].Rows[0]["BUSINESS_UNIT"] != DBNull.Value)
                            lblBusinessUnit.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["BUSINESS_UNIT"]);

                        if (dsOrderList.Tables[0].Rows[0]["TARGET_GROUP"] != DBNull.Value)
                            lblTargetGroup.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["TARGET_GROUP"]);

                        if (dsOrderList.Tables[0].Rows[0]["INDUSTRY_CODE"] != DBNull.Value)
                            lblIndustryCode.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["INDUSTRY_CODE"]);

                        if (dsOrderList.Tables[0].Rows[0]["PROJECT_TYPE"] != DBNull.Value)
                            lblTypeofProject.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PROJECT_TYPE"]);

                        if (dsOrderList.Tables[0].Rows[0]["CUSTOMER_CHARACTER"] != DBNull.Value)
                            lblCustomerCharacter.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["CUSTOMER_CHARACTER"]);

                        if (dsOrderList.Tables[0].Rows[0]["UNIT_NAME"] != DBNull.Value)
                            lblOrderBookingLocation.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["UNIT_NAME"]);

                        if (dsOrderList.Tables[0].Rows[0]["PROJECT_MANAGER"] != DBNull.Value)
                            lblProjectManager.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PROJECT_MANAGER"]);

                        if (dsOrderList.Tables[0].Rows[0]["PROJECT_ENGINEER"] != DBNull.Value)
                            lblProjectEngineer.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PROJECT_ENGINEER"]);

                        if (dsOrderList.Tables[0].Rows[0]["SALES_MANAGER"] != DBNull.Value)
                            lblSalesManager.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["SALES_MANAGER"]);

                        if (dsOrderList.Tables[0].Rows[0]["SALES_ENGINEER"] != DBNull.Value)
                            lblSalesEngineer.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["SALES_ENGINEER"]);

                        if (dsOrderList.Tables[0].Rows[0]["QUOTATION_NO"] != DBNull.Value)
                            lblQuotationNo.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["QUOTATION_NO"]);

                        if (dsOrderList.Tables[0].Rows[0]["ESTIMATE_ATTACHED"] != DBNull.Value)
                            lblEstimateAttached.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["ESTIMATE_ATTACHED"]);

                        if (dsOrderList.Tables[0].Rows[0]["PAYMENT_TERMS"] != DBNull.Value)
                            lblPaymentTerms.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["PAYMENT_TERMS"]);

                        if (dsOrderList.Tables[0].Rows[0]["CREDIT_DAYS"] != DBNull.Value)
                            lblCraditDays.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["CREDIT_DAYS"]);

                        if (dsOrderList.Tables[0].Rows[0]["DELIVERY_TERM"] != DBNull.Value)
                            lblDeliveryTerms.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["DELIVERY_TERM"]);

                        if (dsOrderList.Tables[0].Rows[0]["LD"] != DBNull.Value)
                            lblLD.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["LD"]);

                        if (dsOrderList.Tables[0].Rows[0]["DELIVERY_DATE"] != DBNull.Value)
                            lblDeliveryDate.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["DELIVERY_DATE"]);

                        if (dsOrderList.Tables[0].Rows[0]["ORDER_REGISTRATION_DATE"] != DBNull.Value)
                            lblDateOfOrderRegistration.Text = Convert.ToString(dsOrderList.Tables[0].Rows[0]["ORDER_REGISTRATION_DATE"]);

                    }


                    if (dsOrderList.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsOrderList.Tables[1].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                if (dsOrderList.Tables[1].Rows[0]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[0]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[0]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINRNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[0]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateNew.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[0]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[0]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[0]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[0]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[0]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[0]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[0]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["FRAIGHT"] != DBNull.Value)
                                    lblFraightNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[0]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[0]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[0]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalNew.Text = Convert.ToString(dsOrderList.Tables[1].Rows[0]["TOTAL_VALUE"]);



                            }

                            else if (i == 1)
                            {
                                if (dsOrderList.Tables[1].Rows[1]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[1]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[1]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINROne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[1]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateOne.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[1]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[1]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[1]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[1]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[1]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[1]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[1]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["FRAIGHT"] != DBNull.Value)
                                    lblFraightOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[1]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[1]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[1]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalOne.Text = Convert.ToString(dsOrderList.Tables[1].Rows[1]["TOTAL_VALUE"]);
                            }

                            else if (i == 2)
                            {
                                if (dsOrderList.Tables[1].Rows[2]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[2]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[2]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINRTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[2]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateTwo.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[2]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[2]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[2]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[2]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[2]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[2]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[2]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["FRAIGHT"] != DBNull.Value)
                                    lblFraightTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[2]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[2]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[2]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalTwo.Text = Convert.ToString(dsOrderList.Tables[1].Rows[2]["TOTAL_VALUE"]);
                            }

                            else if (i == 3)
                            {
                                if (dsOrderList.Tables[1].Rows[3]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[3]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[3]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINRThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[3]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateThree.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[3]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[3]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[3]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[3]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[3]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[3]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[3]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["FRAIGHT"] != DBNull.Value)
                                    lblFraightThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[3]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[3]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[3]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalThree.Text = Convert.ToString(dsOrderList.Tables[1].Rows[3]["TOTAL_VALUE"]);
                            }

                            else if (i == 4)
                            {
                                if (dsOrderList.Tables[1].Rows[4]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[4]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[4]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINRFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[4]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateFour.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[4]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[4]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[4]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[4]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[4]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[4]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[4]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["FRAIGHT"] != DBNull.Value)
                                    lblFraightFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[4]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[4]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[4]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalFour.Text = Convert.ToString(dsOrderList.Tables[1].Rows[4]["TOTAL_VALUE"]);
                            }

                            else if (i == 5)
                            {
                                if (dsOrderList.Tables[1].Rows[5]["BASIC_VALUE"] != DBNull.Value)
                                    lblBasicValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["BASIC_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["CURRENCY"] != DBNull.Value)
                                    lblBasicValueCurrencyFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CURRENCY"]);

                                if (dsOrderList.Tables[1].Rows[5]["EXCHANGE_RATE"] != DBNull.Value)
                                    lblExchangeRateFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["EXCHANGE_RATE"]);

                                if (dsOrderList.Tables[1].Rows[5]["BASIC_VALUE_INR"] != DBNull.Value)
                                    lblBasicValueINRFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["BASIC_VALUE_INR"]);

                                if (dsOrderList.Tables[1].Rows[5]["EXCHANGE_RATE_DATE"] != DBNull.Value)
                                    lblExchangeRateDateFive.Text = Convert.ToDateTime(dsOrderList.Tables[1].Rows[5]["EXCHANGE_RATE_DATE"]).ToString("dd-MMM-yyyy");

                                if (dsOrderList.Tables[1].Rows[5]["MATERIAL_COST"] != DBNull.Value)
                                    lblMaterialCostFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["MATERIAL_COST"]);

                                if (dsOrderList.Tables[1].Rows[5]["GROSS_MARGIN_VALUE"] != DBNull.Value)
                                    lblGrossMarginValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["GROSS_MARGIN_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["GROSS_MARGIN_PERCENTAGE"] != DBNull.Value)
                                    lblGrossMarginPercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["GROSS_MARGIN_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["CID_ENGG_HOURS"] != DBNull.Value)
                                    lblCIDEnggHoursFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CID_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[5]["CID_ENGGHOURS_RATE"] != DBNull.Value)
                                    lblCIDEnggHoursRateFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CID_ENGGHOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[5]["CID_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCIDEnggHoursValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CID_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS"] != DBNull.Value)
                                    lblCWGEnggHoursFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS"]);

                                if (dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS_RATE"] != DBNull.Value)
                                    lblCWGEnggHoursRateFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS_RATE"]);

                                if (dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS_VALUE"] != DBNull.Value)
                                    lblCWGEnggHoursValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["CWG_ENGG_HOURS_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["ESTIMATED_TRAVEL_COST"] != DBNull.Value)
                                    lblEstimatedTravelCostFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["ESTIMATED_TRAVEL_COST"]);

                                if (dsOrderList.Tables[1].Rows[5]["SUPERVISION"] != DBNull.Value)
                                    lblSupervisionFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["SUPERVISION"]);

                                if (dsOrderList.Tables[1].Rows[5]["PURCHASE_RATE_PERCENTAGE"] != DBNull.Value)
                                    lblPurchaseOverheadMtrlRateFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["PURCHASE_RATE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["PURCHASE_RATE_VALUE"] != DBNull.Value)
                                    lblPurchaseRateValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["PURCHASE_RATE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["WARRANTY_COST_PERCENTAGE"] != DBNull.Value)
                                    lblWarrantyCostPercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["WARRANTY_COST_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["WARRANTY_COST_VALUE"] != DBNull.Value)
                                    lblWarrantyCostValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["WARRANTY_COST_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["ROYALTY_PERCENTAGE"] != DBNull.Value)
                                    lblRoyaltyPercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["ROYALTY_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["ROYALTY_VALUE"] != DBNull.Value)
                                    lblRoyaltyValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["ROYALTY_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["INSURANCE_PERCENTAGE"] != DBNull.Value)
                                    lblInsurancePercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["INSURANCE_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["INSURANCE_VALUE"] != DBNull.Value)
                                    lblInsuranceValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["INSURANCE_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["FRAIGHT"] != DBNull.Value)
                                    lblFraightFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["FRAIGHT"]);

                                if (dsOrderList.Tables[1].Rows[5]["COMMISION1_PERCENTAGE"] != DBNull.Value)
                                    lblCommision1PercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["COMMISION1_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["COMMISION1_VALUE"] != DBNull.Value)
                                    lblCommision1ValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["COMMISION1_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["COMMISION2_PERCENTAGE"] != DBNull.Value)
                                    lblCommision2PercentageFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["COMMISION2_PERCENTAGE"]);

                                if (dsOrderList.Tables[1].Rows[5]["COMMISION2_VALUE"] != DBNull.Value)
                                    lblCommision2ValueFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["COMMISION2_VALUE"]);

                                if (dsOrderList.Tables[1].Rows[5]["TOTAL_VALUE"] != DBNull.Value)
                                    lblTotalFive.Text = Convert.ToString(dsOrderList.Tables[1].Rows[5]["TOTAL_VALUE"]);
                            }
                        }
                    }
                }
                else
                {
                    Session["OrderDetail"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            //ExceptionMessage(ex.ToString());
            //return;
        }
    }

    #endregion

}
