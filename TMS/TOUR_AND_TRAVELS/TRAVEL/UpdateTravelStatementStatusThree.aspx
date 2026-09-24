<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTravelStatementStatusThree.aspx.cs"
    Inherits="TOUR_AND_TRAVELS_TRAVEL_UpdateTravelStatementStatusThree" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Travel Statement</title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>
    <style type="text/css">
        table {
            border: 1px;
            border-collapse: collapse;
        }

        td, th {
            padding: 7px;
        }
    </style>

    <script type="text/javascript">
        function Confirm() {
            if (confirm("Would you like to approve?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }


        }

    </script>

    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;
    </script>

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
        <div align="center" style="margin-top: 5px;">
            <fieldset style="width: 90%; margin-top: 5px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLegend" runat="server" />
                </legend>


                <div style='overflow-y: scroll; overflow-x: hidden; width: 80%; height: 800px; border: 1px solid lightgray; padding: 4%;'>

                    <table style="width: 95%; height: 100%;">
                        <tr>
                            <td colspan="5" align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="20px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>Employee Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" Enabled="false" onblur="return ValidateEmployee();" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Employee ID:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmployeeID" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>Designation:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtDesignation" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>Starting Date Of Tour:
                            </td>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 28%;">
                                            <asp:TextBox ID="txtStartDate" runat="server" Enabled="false" ReadOnly="true" Width="100%"
                                                onblur="return ValidateStartDate();" />
                                        </td>
                                        <td style="width: 19%;">End Date Of Tour:
                                        </td>
                                        <td style="width: 28%;">
                                            <asp:TextBox ID="txtEndDate" runat="server" Enabled="false" ReadOnly="true" Width="100%"
                                                onblur="return ValidateEndDate();" />
                                        </td>
                                        <td style="width: 5%;">Days:
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:TextBox ID="txtDays" runat="server" Width="100%" Enabled="false" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Name of Customer/Vendor:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCustVendName" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustVendName();" />
                            </td>
                        </tr>
                        <tr>
                            <td>Place of Visit:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtPlaceOfVisit" runat="server" Width="100%" Enabled="false" onblur="return ValidatePlaceOfVisit();" />
                            </td>
                        </tr>
                        <tr>
                            <td>Purpose Of Visit:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPurposeOfVisit" runat="server" Width="100%" Enabled="false" onblur="return ValidatePurposeOfVisit();" />
                            </td>
                            <td></td>
                            <td>Job/Inq No. Where Applicable:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJobInqNo" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>Business Segment:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBusinessSegment" runat="server" Width="100%" Visible="true" Enabled="false"
                                    onblur="return ValidateBusinessSegment();" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Attachment1:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;" align="right">
                                            <asp:ImageButton ID="btnViewAttachment1" ImageUrl="~/Images/imgicon1.png" Height="22px"
                                                Width="22px" runat="server" OnClick="btnViewAttachment1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>Attachment2:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:TextBox ID="txtViewAttachment2" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;" align="right">
                                            <asp:ImageButton ID="btnViewAttachment2" ImageUrl="~/Images/imgicon1.png" Height="22px"
                                                Width="22px" runat="server" OnClick="btnViewAttachment2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Attachment3:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:TextBox ID="txtViewAttachment3" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;" align="right">
                                            <asp:ImageButton ID="btnViewAttachment3" ImageUrl="~/Images/imgicon1.png" Height="22px"
                                                Width="22px" runat="server" OnClick="btnViewAttachment3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        
                        <tr>
                            <td colspan="5" align="center">
                                <b>Expense Statement</b>
                            </td>
                        </tr>
                        <tr>
                            <td>Airfare:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtAirfare" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtAirfareGL" runat="server" Width="100%" Enabled="false" Text="40/41" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Telephone/Mobile:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtMobile" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtMobileGL" runat="server" Width="100%" Enabled="false" Text="52" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Lodging:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtLodging" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtLodgingGL" runat="server" Width="100%" Enabled="false" Text="42" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Tips:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtTips" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtTipsGL" runat="server" Width="100%" Enabled="false" Text="54" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Meals:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtMeals" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtMealsGL" runat="server" Width="100%" Enabled="false" Text="44" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Visa Fee:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtVisaFee" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtVisaFeeGL" runat="server" Width="100%" Enabled="false" Text="56" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Ground Transport:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtGroundTransport" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtGroundTransportGL" runat="server" Width="100%" Enabled="false"
                                                Text="46" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Daily Allowance:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtDailyAllowance" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtDailyAllowanceGL" runat="server" Width="100%" Enabled="false"
                                                Text="58" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Entertainment:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtEntertainment" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtEntertainmentGL" runat="server" Width="100%" Enabled="false"
                                                Text="48" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Other:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtOther" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtOtherGL" runat="server" Width="100%" Enabled="false" Text="60" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Gifts:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtGifts" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtGiftsGL" runat="server" Width="100%" Enabled="false" Text="50" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Total:
                            </td>
                            <td>
                                <table style="width: 100%; height: 22px;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtTotal" runat="server" Width="100%" Text="0.00" Enabled="false" />
                                            <asp:HiddenField ID="hdTotal" runat="server" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtTotalCurrency" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Advance Obtained:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtAdvanceObtained" runat="server" Width="100%" Text="0.00" Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtAdvancAmtCurrency" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Amount Adjustment:
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtAdjustmentAmt" runat="server" Width="100%" Text="0.00" Enabled="false" />
                                            <asp:HiddenField ID="hdAdjustmentAmt" runat="server" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:TextBox ID="txtAdjustmentAmtCurrency" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Tour Cost Recoverable From Client:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTourCostRecoverable" runat="server" Width="100%" Height="26px"
                                    Enabled="false">
                                    <asp:ListItem Text="No" Value="0" />
                                    <asp:ListItem Text="Yes" Value="1" />
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Adjustment Type:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdjustmentAmtType" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>

                        <asp:Panel ID="pnlTravellerRemarks" runat="server" Visible="true">
                            <tr>
                                <td>Traveller Remarks:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtTravellerRemarks" runat="server" Width="100%" Enabled="false"
                                        TextMode="MultiLine" Rows="2" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 40%">
                                            <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Approve Travel Statement"
                                                Width="100%" OnClick="btnSubmit_Click" OnClientClick="return Confirm();" />
                                        </td>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Button ID="btnTravelStatementDetails" CssClass="button" runat="server" OnClientClick="return ValidateAll();"
                                                Text="Go To View Details" Width="100%" OnClick="btnTravelStatementDetails_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
                    PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
                    Width="1050px" Style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
                        <asp:Image ID="imgFile" runat="server" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
                    PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="600px"
                    Width="1050px" Style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewPDFFile"
                        runat="server">
                        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
                        </div>
                    </iframe>
                </asp:Panel>
                <br />
            </fieldset>
        </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
