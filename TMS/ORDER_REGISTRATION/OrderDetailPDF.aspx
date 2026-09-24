<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetailPDF.aspx.cs" EnableEventValidation="false"
    Inherits="ORDER_REGISTRATION_OrderDetailPDF" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../../viewpdftablecss.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="~/Images/pdficon3.png"
                    OnClick="imgBtnExport_Click" Width="20px" Height="20px" ToolTip="Save as PDF" />
            </td>
        </tr>
    </table>
    <div>
        <h3>
            <u>Order Registration</u></h3>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td colspan="5" align="center">
                    <u><b>Commercial Summary</b></u>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Order No:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    GSS No:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblGSSNo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Customer:
                </td>
                <td colspan="2" style="width: 70%">
                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    <asp:Label ID="lblCustomerCode" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    End User:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblEndUser" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    PO No.:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblPONo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    PO Date:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblPODate" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    HSN Code:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblHSNCode" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Description:
                </td>
                <td colspan="3" style="width: 85%">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td colspan="5" align="center">
                    <u><b>Customer And Product Summary</b></u>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Division:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblDivision" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Business Unit:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblBusinessUnit" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Target Group:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblTargetGroup" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Industry Code:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblIndustryCode" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Type of Project:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblTypeofProject" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Customer Character:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblCustomerCharacter" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Order Booking Location:
                </td>
                <td colspan="5" style="width: 85%">
                    <asp:Label ID="lblOrderBookingLocation" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="Table1" style="width: 100%" border="1" cellpadding="2" cellspacing="0"
            runat="server">
            <tr>
                <td colspan="5" align="center">
                    <u><b>Responsible Department Summary</b></u>
                </td>
            </tr>
        </table>
        <table id="Table2" style="width: 100%" border="1" cellpadding="2" cellspacing="0"
            runat="server">
            <tr>
                <td style="width: 15%">
                    Project Manager:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Project Engineer:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblProjectEngineer" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="Table3" style="width: 100%" border="1" cellpadding="2" cellspacing="0"
            runat="server">
            <tr>
                <td style="width: 15%">
                    Sales Manager:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblSalesManager" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Sales Engineer:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblSalesEngineer" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="Table4" style="width: 100%" border="1" cellpadding="2" cellspacing="0"
            runat="server">
            <tr>
                <td style="width: 15%">
                    Quotation No.:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblQuotationNo" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Estimate Attached:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblEstimateAttached" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td colspan="5" align="center">
                    <u><b>Payment Summary</b></u>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Payment Terms:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblPaymentTerms" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Cradit Days
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblCraditDays" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Delivery Terms:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblDeliveryTerms" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    LD:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblLD" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 15%">
                    Delivery Date:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>
                </td>
                <td style="width: 15%">
                    Date Of Order Registration:
                </td>
                <td style="width: 35%">
                    <asp:Label ID="lblDateOfOrderRegistration" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tblOrderList" style="width: 100%" border="1" cellpadding="2" cellspacing="0"
            runat="server">
            <tr>
                <td colspan="12" align="center">
                    <u><b>Quotation Summary</b></u>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    New
                </td>
                <td>
                    Rev.-1
                </td>
                <td>
                    Rev.-2
                </td>
                <td>
                    Rev.-3
                </td>
                <td>
                    Rev.-4
                </td>
                <td>
                    Rev.-5
                </td>
                
            </tr>
            <tr>
                <td>
                    Revsie Value
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblRevsieValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRevsieValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRevsieValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRevsieValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRevsieValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Basic Value
                </td>
                <td>
                    <asp:Label ID="lblBasicValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Currency
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueCurrencyFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Exchange Rate
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Basic Value INR
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINRNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINROne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINRTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINRThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINRFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblBasicValueINRFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Exchange Rate Date
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblExchangeRateDateFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Material Cost
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMaterialCostFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Gross Margin Value
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Gross Margin(%)
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblGrossMarginPercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CID Engg. Hours
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CID Engg. Hours Rate
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursRateFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CID Engg. Hours Value
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCIDEnggHoursValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CWG Engg. Hours
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CWG Engg. Hours Rate
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursRateFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    CWG Engg. Hours Value
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCWGEnggHoursValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Estimated Travel Cost
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblEstimatedTravelCostFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Supervision
                </td>
                <td>
                    <asp:Label ID="lblSupervisionNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblSupervisionOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblSupervisionTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblSupervisionThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblSupervisionFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblSupervisionFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Purchase Overhead/Mtrl Rate(%)
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseOverheadMtrlRateFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Purchase Rate Value
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblPurchaseRateValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Warranty Cost(%)
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostPercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Warranty Cost Value
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblWarrantyCostValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Royalty(%)
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyPercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Royalty Value
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblRoyaltyValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Insurance(%)
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsurancePercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Insurance Value
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInsuranceValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Fraight
                </td>
                <td>
                    <asp:Label ID="lblFraightNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFraightOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFraightTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFraightThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFraightFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFraightFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Commision1(%)
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1PercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Commision1 Value
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision1ValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Commision2(%)
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2PercentageFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Commision2 Value
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblCommision2ValueFive" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Total
                </td>
                <td>
                    <asp:Label ID="lblTotalNew" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblTotalOne" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblTotalTwo" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblTotalThree" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblTotalFour" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblTotalFive" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 100%" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;" align="center">
                    Sales
                </td>
                <td style="width: 20%;" align="center">
                    Projects
                </td>
                <td style="width: 20%;" align="center">
                    Order Registered By
                </td>
                <td style="width: 20%;" align="center">
                    Authorized By
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
