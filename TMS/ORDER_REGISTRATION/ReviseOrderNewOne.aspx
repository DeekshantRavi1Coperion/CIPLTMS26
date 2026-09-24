<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviseOrderNewOne.aspx.cs" Inherits="ORDER_REGISTRATION_ReviseOrderNew" %>

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
                                                    <asp:TextBox ID="txtGrossMarginZero" runat="server" Width="100%" Enabled="false" />
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
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueZero" runat="server" Width="100%" Enabled="false" />
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
                <td id="tdRevisionLast" runat="server">
                    <div id="dvReviseLast" style="width: 600px; height: 100%; background-color: #CCCCFF;">
                        <fieldset>
                            <legend style="text-align: center;">Last Revision</legend>
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
                                                    <asp:TextBox ID="txtBasicValueLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCurrencyLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRateLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasicValueINRLast" runat="server" Width="100%" Enabled="false" />
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
                                        <asp:TextBox ID="txtExchangeRateDateLast" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Material Cost:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialCostLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtGrossMarginLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    %:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginPercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossMarginValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtCIDEnggHoursLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursRateLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCIDEnggHoursValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtCWGEnggHoursLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Rate:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursRateLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCWGEnggHoursValueLast" runat="server" Width="100%" Enabled="false" />
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
                                        <asp:TextBox ID="txtEstimatedTravelCostLast" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Supervision:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupervisionLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtPurchaseRatePercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPurchaseRateValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtWarrantyCostPercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWarrantyCostValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtRoyaltyPercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRoyaltyValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtInsurancePercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInsuranceValueLast" runat="server" Width="100%" Enabled="false" />
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
                                        <asp:TextBox ID="txtFraightLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtCommision1PercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision1ValueLast" runat="server" Width="100%" Enabled="false" />
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
                                                    <asp:TextBox ID="txtCommision2PercentageLast" runat="server" Width="100%" Enabled="false" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCommision2ValueLast" runat="server" Width="100%" Enabled="false" />
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
                                        <asp:TextBox ID="txtTotalValueLast" runat="server" Width="100%" Enabled="false" />
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
