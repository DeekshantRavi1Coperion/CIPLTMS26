<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviseOrder.aspx.cs" Inherits="ORDER_REGISTRATION_ReviseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <div style="width: 100%; height: 100%;">
        <table width="100%">
            <tr>
                <td id="tdRevisionZero" runat="server">
                    <div id="dvReviseZero" style="width: 600px; height: 100%; background-color: #CCCCFF;">
                        <fieldset>
                            <legend style="text-align: center;">New</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueZero" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueZero" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionOne" runat="server">
                    <div id="dvReviseOne" style="width: 600px; height: 100%; background-color: #DCDCDC;">
                        <fieldset>
                            <legend style="text-align: center;">1st Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINROne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueOne" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueOne" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionTwo" runat="server">
                    <div id="dvReviseTwo" style="background-color: #CCCCFF; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">2nd Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueTwo" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueTwo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionThree" runat="server">
                    <div id="dvReviseThree" style="background-color: #DCDCDC; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">3rd Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueThree" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueThree" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionFour" runat="server">
                    <div id="dvReviseFour" style="background-color: #CCCCFF; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">4th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueFour" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueFour" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionFive" runat="server">
                    <div id="dvReviseFive" style="background-color: #DCDCDC; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">5th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueFive" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueFive" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionSix" runat="server">
                    <div id="dvReviseSix" style="background-color: #CCCCFF; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">6th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencySix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueSix" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueSix" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionSeven" runat="server">
                    <div id="dvReviseSeven" style="background-color: #DCDCDC; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">7th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencySeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueSeven" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueSeven" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionEight" runat="server">
                    <div id="dvReviseEight" style="background-color: #CCCCFF; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">8th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINREight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueEight" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueEight" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionNine" runat="server">
                    <div id="dvReviseNine" style="background-color: #DCDCDC; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">9th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueNine" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueNine" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td id="tdRevisionTen" runat="server">
                    <div id="dvReviseTen" style="background-color: #CCCCFF; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">10th Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRateDateTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCostTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraightTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1PercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2PercentageTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueTen" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValueTen" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td>
                    <div id="dvCurrentRevise" style="background-color: #DCDCDC; width: 600px; height: 100%;">
                        <fieldset>
                            <legend style="text-align: center;">Current Revision</legend>
                            <table width="95%" style="margin-left: 10px;">
                                <tr>
                                    <td>
                                        Revision Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtRevisionValue" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:DropDownList ID="ddlRevisionValueCurrency" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    Rate:
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="txtRevisionValueExchangeRate" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                </td>
                                                <td style="width: 10%">
                                                    Value(INR):
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtRevisionValueINR" runat="server" Width="100%" Enabled="false"
                                                        onblur="return ValidateBasicValueINR();" />
                                                    <asp:HiddenField ID="hdRevisionValueINR" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Basic Value:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtBasicValue" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                </td>
                                                <td style="width: 10%">
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    Rate:
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="txtExchangeRate" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                </td>
                                                <td style="width: 10%">
                                                    Value(INR):
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtBasicValueINR" runat="server" Width="100%" Enabled="false" onblur="return ValidateBasicValueINR();" />
                                                    <asp:HiddenField ID="hdBasicValueINR" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Exchange Rate Date:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                    <asp:HiddenField ID="hdExchangeRateDate" runat="server" />
                                                    <ajax:CalendarExtender ID="calendarExchangeRateDate" PopupButtonID="imgbtnExchangeRateDate"
                                                        runat="server" TargetControlID="txtExchangeRateDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedExchangeRateDate">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgbtnExchangeRateDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Exchange Rate Date Calendar" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCost" runat="server" Width="100%" onblur="return ValidateMaterialCost();"
                                            onpaste="return false" onKeyUp="checkDecNew1(this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gross Margin:
                                    </td>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtGrossMargin" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                    <asp:HiddenField ID="hdGrossMargin" runat="server" />
                                                </td>
                                                <td style="width: 15%">
                                                    Gross Margin(%):
                                                </td>
                                                <td style="width: 15%">
                                                    <asp:TextBox ID="txtGrossMarginPercentage" runat="server" Width="100%" onpaste="return false"
                                                        onKeyUp="checkDecNew1(this)" />
                                                    <asp:HiddenField ID="hdGrossMarginPercentage" runat="server" />
                                                </td>
                                                <td style="width: 10%">
                                                    Value:
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:TextBox ID="txtGrossMarginValue" runat="server" Width="100%" Enabled="false"
                                                        onblur="return ValidateGrossMarginValue();" />
                                                    <asp:HiddenField ID="hdGrossMarginValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CID Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHours" runat="server" Width="100%" onblur="return ValidateCIDEnggHours();"
                                                        onKeyUp="checkDecNew1(this)" onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRate" runat="server" Width="100%" onblur="return ValidateCIDEnggHoursRate();"
                                                        onKeyUp="checkDecNew1(this)" onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdCIDEnggHoursValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        CWG Engg. Hours:
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHours" runat="server" Width="100%" onblur="return ValidateCWGEnggHours();"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRate" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onblur="return ValidateEnggHoursRate();" onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdCWGEnggHoursValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estimated Travel Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEstimatedTravelCost" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                            onpaste="return false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervision" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                            onpaste="return false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Purchase Overhead/Mtrl Rate(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRatePercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdPurchaseRateValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Warranty Cost(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostPercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdWarrantyCostValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Royalty(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyPercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdRoyaltyValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Insurance(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInsurancePercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValue" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdInsuranceValue" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fraight:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFraight" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                            onpaste="return false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Commision1(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1Percentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1Value" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdCommision1Value" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Commision2(%):
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2Percentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                                        onpaste="return false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2Value" runat="server" Width="100%" Enabled="false" />
                                                    <asp:HiddenField ID="hdCommision2Value" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Total:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalValue" runat="server" Width="100%" Enabled="false" />
                                        <asp:HiddenField ID="hdTotalValue" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="4">
                                        <asp:Button ID="btnRevise" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                            Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
                       
            document.getElementById('<%=txtExchangeRateDate.ClientID %>').value = document.getElementById('<%=hdExchangeRateDate.ClientID %>').value;            
            document.getElementById('<%=hdExchangeRateDate.ClientID %>').value = document.getElementById('<%=txtExchangeRateDate.ClientID %>').value;                      
        }
                  
         function clientChangedExchangeRateDate(sender, args) {
            document.getElementById('<%=hdExchangeRateDate.ClientID %>').value = document.getElementById('<%=txtExchangeRateDate.ClientID %>').value;
            document.getElementById('<%=txtExchangeRateDate.ClientID %>').value = document.getElementById('<%=hdExchangeRateDate.ClientID %>').value;            
        }                                      
    </script>

    <script type="text/javascript">
    
    function ValidateRevisionValueINR() {
            var BasicValueINR = document.getElementById('<%=txtRevisionValueINR.ClientID %>').value;
            if (BasicValueINR == '') {
                document.getElementById('<%=txtRevisionValueINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRevisionValueINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
     function ValidateBasicValueINR() {
            var BasicValueINR = document.getElementById('<%=txtBasicValueINR.ClientID %>').value;
            if (BasicValueINR == '') {
                document.getElementById('<%=txtBasicValueINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBasicValueINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
    function ValidateMaterialCost() {
            var MaterialCost = document.getElementById('<%=txtMaterialCost.ClientID %>').value;
            if (MaterialCost == '') {
                document.getElementById('<%=txtMaterialCost.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMaterialCost.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
    function ValidateGrossMarginValue() {
            var GrossMarginValue = document.getElementById('<%=txtGrossMarginValue.ClientID %>').value;
            if (GrossMarginValue == '') {
                document.getElementById('<%=txtGrossMarginValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGrossMarginValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateCIDEnggHours() {
            var CIDEnggHours = document.getElementById('<%=txtCIDEnggHours.ClientID %>').value;
            if (CIDEnggHours == '') {
                document.getElementById('<%=txtCIDEnggHours.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCIDEnggHours.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateCIDEnggHoursRate() {
            var CIDEnggHoursRate = document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').value;
            if (CIDEnggHoursRate == '') {
                document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateCWGEnggHours() {
            var CWGEnggHours = document.getElementById('<%=txtCWGEnggHours.ClientID %>').value;
            if (CWGEnggHours == '') {
                document.getElementById('<%=txtCWGEnggHours.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCWGEnggHours.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateEnggHoursRate() {
            var CWGEnggHoursRate = document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').value;
            if (CWGEnggHoursRate == '') {
                document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').style.borderColor = "";
                return false;
            }
        }                                                
    </script>

    <script type="text/javascript">
    
    function ValidateAll() {
            var check = true;
                                    
            if( ValidateRevisionValueINR()) {return false;}
                                                                                                                 
            if( ValidateBasicValueINR()) {return false;}
                
            if( ValidateMaterialCost()) {return false;}

            if( ValidateGrossMarginValue()) {return false;}
                
            if( ValidateCIDEnggHours()) {return false;}
              
            if( ValidateCIDEnggHoursRate()) {return false;}
                
            if( ValidateCWGEnggHours()) {return false;}
              
            if( ValidateEnggHoursRate()) {return false;}
                                                                                                                                           
            return check;
        }
    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/Javascript">
        function checkDecNew1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    checkDecNew2();
                }
            }
            else {
                checkDecNew2();
            }
        }
        
        function checkDecNew2() {
        
        var RevisionValue= '0';
        var RevisionValueExchangeRate= '0';
        var RevisionValueINR= '0';
        
        var BasicValue= '0';
        var ExchangeRate= '0';
        var BasicValueINR= '0';
        
        var MaterialCost= '0';
        
        var GrossMargin= '0';
        var GrossMarginPercentage= '0';
        var GrossMarginValue= '0';
        
        var CIDEnggHours= '0';
        var CIDEnggHoursRate= '0';
        var CIDEnggHoursValue= '0';
        
        var CWGEnggHours= '0';
        var CWGEnggHoursRate= '0';
        var CWGEnggHoursValue= '0';
        
        var EstimatedTravelCost= '0';
        var Supervision= '0';
        
        var PurchaseRatePercentage= '0';
        var PurchaseRateValue= '0';
        
        var WarrantyCostPercentage= '0';
        var WarrantyCostValue= '0';
        
        var RoyaltyPercentage= '0';
        var RoyaltyValue= '0';
        
        var InsurancePercentage= '0';
        var InsuranceValue= '0';
        
        var Fraight= '0';
        var Commision1Percentage= '0';
        var Commision1Value= '0';
        
        var Commision2Percentage= '0';
        var Commision2Value= '0';
        
        var TotalValue= '0';
        
        
        if (document.getElementById('<%=txtRevisionValue.ClientID %>').value != '') {
                RevisionValue = document.getElementById('<%=txtRevisionValue.ClientID %>').value;
            }
            else {
                RevisionValue = '0';
            }
                
        if (document.getElementById('<%=txtRevisionValueExchangeRate.ClientID %>').value != '') {
                RevisionValueExchangeRate = document.getElementById('<%=txtRevisionValueExchangeRate.ClientID %>').value;
            }
            else {
                RevisionValueExchangeRate = '0';
            }
            
            
        
        RevisionValueINR=parseFloat(RevisionValue)*parseFloat(RevisionValueExchangeRate);               
        document.getElementById('<%=hdRevisionValueINR.ClientID %>').value=Math.round(RevisionValueINR);            
        document.getElementById('<%=txtRevisionValueINR.ClientID %>').value=document.getElementById('<%=hdRevisionValueINR.ClientID %>').value;
                     
                     
                                                        
        if (document.getElementById('<%=txtBasicValue.ClientID %>').value != '') {
                BasicValue = document.getElementById('<%=txtBasicValue.ClientID %>').value;
            }
            else {
                BasicValue = '0';
            }
                
        if (document.getElementById('<%=txtExchangeRate.ClientID %>').value != '') {
                ExchangeRate = document.getElementById('<%=txtExchangeRate.ClientID %>').value;
            }
            else {
                ExchangeRate = '0';
            }
            
        BasicValueINR=parseFloat(BasicValue)*parseFloat(ExchangeRate);
        document.getElementById('<%=hdBasicValueINR.ClientID %>').value=Math.round(BasicValueINR);            
        document.getElementById('<%=txtBasicValueINR.ClientID %>').value=document.getElementById('<%=hdBasicValueINR.ClientID %>').value;
        
        
        if (document.getElementById('<%=txtMaterialCost.ClientID %>').value != '') {
                MaterialCost = Math.round(document.getElementById('<%=txtMaterialCost.ClientID %>').value);
            }
            else {
                MaterialCost= '0';
            }
            
                                                                     
          GrossMargin=parseFloat(BasicValueINR)-(parseFloat(TotalValue)+parseFloat(Commision1Value)+parseFloat(Commision2Value));
          document.getElementById('<%=hdGrossMargin.ClientID %>').value=Math.round(GrossMargin);            
          document.getElementById('<%=txtGrossMargin.ClientID %>').value=document.getElementById('<%=hdGrossMargin.ClientID %>').value ;
            
            
          GrossMarginPercentage=parseFloat(GrossMargin)/parseFloat(BasicValueINR);
          document.getElementById('<%=hdGrossMarginPercentage.ClientID %>').value=Math.round(GrossMarginPercentage);            
          document.getElementById('<%=txtGrossMarginPercentage.ClientID %>').value=document.getElementById('<%=hdGrossMarginPercentage.ClientID %>').value ;    
           
           
        
                            
          GrossMarginValue=(parseFloat(GrossMargin)*parseFloat(GrossMarginPercentage))/100;           
          document.getElementById('<%=hdGrossMarginValue.ClientID %>').value=Math.round(GrossMarginValue);            
          document.getElementById('<%=txtGrossMarginValue.ClientID %>').value=document.getElementById('<%=hdGrossMarginValue.ClientID %>').value ;
          
          
          
          
          if (document.getElementById('<%=txtCIDEnggHours.ClientID %>').value != '') {
                CIDEnggHours = document.getElementById('<%=txtCIDEnggHours.ClientID %>').value;
            }
            else {
                CIDEnggHours= '0';
            }
        
        if (document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').value != '') {
                CIDEnggHoursRate = document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').value;
            }
            else {
                CIDEnggHoursRate= '0';
            }
            
        CIDEnggHoursValue=parseFloat(CIDEnggHours)*parseFloat(CIDEnggHoursRate);
        document.getElementById('<%=hdCIDEnggHoursValue.ClientID %>').value=Math.round(CIDEnggHoursValue);            
        document.getElementById('<%=txtCIDEnggHoursValue.ClientID %>').value=document.getElementById('<%=hdCIDEnggHoursValue.ClientID %>').value  ;
        
        
        
        
        
        
        if (document.getElementById('<%=txtCWGEnggHours.ClientID %>').value != '') {
                CWGEnggHours = document.getElementById('<%=txtCWGEnggHours.ClientID %>').value;
            }
            else {
                CWGEnggHours= '0';
            }
        
        if (document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').value != '') {
                CWGEnggHoursRate = document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').value;
            }
            else {
                CWGEnggHoursRate= '0';
            }
         
        CWGEnggHoursValue=parseFloat(CWGEnggHours)*parseFloat(CWGEnggHoursRate);
        document.getElementById('<%=hdCWGEnggHoursValue.ClientID %>').value=Math.round(CWGEnggHoursValue);            
        document.getElementById('<%=txtCWGEnggHoursValue.ClientID %>').value=document.getElementById('<%=hdCWGEnggHoursValue.ClientID %>').value  ;
        
        
           
           if (document.getElementById('<%=txtEstimatedTravelCost.ClientID %>').value != '') {
                EstimatedTravelCost = Math.round(document.getElementById('<%=txtEstimatedTravelCost.ClientID %>').value);
            }
            else {
                EstimatedTravelCost= '0';
            }
            
            
        
         if (document.getElementById('<%=txtSupervision.ClientID %>').value != '') {
                Supervision =Math.round(document.getElementById('<%=txtSupervision.ClientID %>').value);
            }
            else {
                Supervision= '0';
            }
            
            
                        
        if (document.getElementById('<%=txtPurchaseRatePercentage.ClientID %>').value != '') {
                PurchaseRatePercentage = document.getElementById('<%=txtPurchaseRatePercentage.ClientID %>').value;
            }
            else {
                PurchaseRatePercentage= '0';
            }
                      
        PurchaseRateValue=(parseFloat(MaterialCost)*parseFloat(PurchaseRatePercentage))/100;
        document.getElementById('<%=hdPurchaseRateValue.ClientID %>').value=Math.round(PurchaseRateValue);            
        document.getElementById('<%=txtPurchaseRateValue.ClientID %>').value=document.getElementById('<%=hdPurchaseRateValue.ClientID %>').value
        
        
        
        
        if (document.getElementById('<%=txtWarrantyCostPercentage.ClientID %>').value != '') {
                WarrantyCostPercentage = document.getElementById('<%=txtWarrantyCostPercentage.ClientID %>').value;
            }
            else {
                WarrantyCostPercentage= '0';
            }
                      
        WarrantyCostValue=(parseFloat(BasicValueINR)*parseFloat(WarrantyCostPercentage))/100;
        document.getElementById('<%=hdWarrantyCostValue.ClientID %>').value=Math.round(WarrantyCostValue);            
        document.getElementById('<%=txtWarrantyCostValue.ClientID %>').value=document.getElementById('<%=hdWarrantyCostValue.ClientID %>').value;    
            
        
         if (document.getElementById('<%=txtRoyaltyPercentage.ClientID %>').value != '') {
                RoyaltyPercentage = document.getElementById('<%=txtRoyaltyPercentage.ClientID %>').value;
            }
            else {
                RoyaltyPercentage= '0';
            }
                   
        RoyaltyValue=(parseFloat(BasicValueINR)*parseFloat(RoyaltyPercentage))/100;
        document.getElementById('<%=hdRoyaltyValue.ClientID %>').value=Math.round(RoyaltyValue);            
        document.getElementById('<%=txtRoyaltyValue.ClientID %>').value=document.getElementById('<%=hdRoyaltyValue.ClientID %>').value ;
        
        
        if (document.getElementById('<%=txtInsurancePercentage.ClientID %>').value != '') {
                InsurancePercentage = document.getElementById('<%=txtInsurancePercentage.ClientID %>').value;
            }
            else {
                InsurancePercentage= '0';
            }
                           
        InsuranceValue=(parseFloat(BasicValueINR)*parseFloat(InsurancePercentage))/100;
        document.getElementById('<%=hdInsuranceValue.ClientID %>').value=Math.round(InsuranceValue);            
        document.getElementById('<%=txtInsuranceValue.ClientID %>').value=document.getElementById('<%=hdInsuranceValue.ClientID %>').value
        
        
        if (document.getElementById('<%=txtFraight.ClientID %>').value != '') {
                Fraight = document.getElementById('<%=txtFraight.ClientID %>').value;
            }
            else {
                Fraight= '0';
            }
        
        
        if (document.getElementById('<%=txtCommision1Percentage.ClientID %>').value != '') {
                Commision1Percentage = document.getElementById('<%=txtCommision1Percentage.ClientID %>').value;
            }
            else {
                Commision1Percentage= '0';
            }
                            
        Commision1Value=(parseFloat(ExchangeRate)*parseFloat(Commision1Percentage))/100;
        document.getElementById('<%=hdCommision1Value.ClientID %>').value=Math.round(Commision1Value);            
        document.getElementById('<%=txtCommision1Value.ClientID %>').value=document.getElementById('<%=hdCommision1Value.ClientID %>').value
        
        
        
        if (document.getElementById('<%=txtCommision2Percentage.ClientID %>').value != '') {
                Commision2Percentage = document.getElementById('<%=txtCommision2Percentage.ClientID %>').value;
            }
            else {
                Commision2Percentage = '0';
            }            
       
        Commision2Value=(parseFloat(ExchangeRate)*parseFloat(Commision2Percentage))/100;
        document.getElementById('<%=hdCommision2Value.ClientID %>').value=Math.round(Commision2Value);            
        document.getElementById('<%=txtCommision2Value.ClientID %>').value=document.getElementById('<%=hdCommision2Value.ClientID %>').value
        
        
        
        
        TotalValue=parseFloat(MaterialCost)+
                    parseFloat(CIDEnggHoursValue)+
                    parseFloat(CWGEnggHoursValue)+
                    parseFloat(EstimatedTravelCost)+
                    parseFloat(Supervision)+
                    parseFloat(PurchaseRateValue)+
                    parseFloat(WarrantyCostValue)+
                    parseFloat(RoyaltyValue)+
                    parseFloat(InsuranceValue)+
                    parseFloat(Fraight)+
                    parseFloat(Commision1Value)+
                    parseFloat(Commision2Value);
                    
       document.getElementById('<%=hdTotalValue.ClientID %>').value=Math.round(TotalValue);
       document.getElementById('<%=txtTotalValue.ClientID %>').value=document.getElementById('<%=hdTotalValue.ClientID %>').value;
        }                
    </script>

</body>
</html>
