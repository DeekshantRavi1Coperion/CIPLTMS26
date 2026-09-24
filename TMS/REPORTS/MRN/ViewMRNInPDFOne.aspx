<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMRNInPDFOne.aspx.cs"
    EnableEventValidation="false" Inherits="REPORTS_MRN_ViewMRNInPDFOne" %>

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
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>
        <div>
            <h4>
                <u>Travel Statement</u></h4>
            <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
                <tr>
                    <td>Tour Sanction No.
                    </td>
                    <td>
                        <asp:Label ID="lblTourSanctionNo" runat="server"></asp:Label>
                    </td>
                    <td>Tour No.
                    </td>
                    <td>
                        <asp:Label ID="lblTourNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Emplyoee Name.
                    </td>
                    <td>
                        <asp:Label ID="lblEmplyoeeName" runat="server"></asp:Label>
                    </td>
                    <td>Employee ID.
                    </td>
                    <td>
                        <asp:Label ID="lblEmployeeID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Designation:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Start Date Of Tour:
                    </td>
                    <td>
                        <asp:Label ID="lblStartDateOfTour" runat="server"></asp:Label>
                    </td>
                    <td>End Date Of Tour:
                    </td>
                    <td>
                        <asp:Label ID="lblEndDateOfTour" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Name Of Customer/Vendor:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblCustVendName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Place Of Visit:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblPlaceOfVisit" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Purpose Of Visit:
                    </td>
                    <td>
                        <asp:Label ID="lblPurposeOfVisit" runat="server"></asp:Label>
                    </td>
                    <td>Job/Enquiry No.:
                    </td>
                    <td>
                        <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Business Segment:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblBusinessSegment" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <h4>
                <u>Expense Statement</u></h4>
            <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
                <tr>
                    <td>Airfare:
                    </td>
                    <td>
                        <asp:Label ID="lblAirfare" runat="server"></asp:Label>
                    </td>
                    <td>Telephone/Mobile:
                    </td>
                    <td>
                        <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Lodging:
                    </td>
                    <td>
                        <asp:Label ID="lblLodging" runat="server"></asp:Label>
                    </td>
                    <td>Tips:
                    </td>
                    <td>
                        <asp:Label ID="lblTips" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Meals:
                    </td>
                    <td>
                        <asp:Label ID="lblMeals" runat="server"></asp:Label>
                    </td>
                    <td>Visa Fee:
                    </td>
                    <td>
                        <asp:Label ID="lblVisaFee" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Ground Transport:
                    </td>
                    <td>
                        <asp:Label ID="lblGroundTransport" runat="server"></asp:Label>
                    </td>
                    <td>Daily Allowance:
                    </td>
                    <td>
                        <asp:Label ID="lblDailyAllowance" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Entertainment:
                    </td>
                    <td>
                        <asp:Label ID="lblEntertainment" runat="server"></asp:Label>
                    </td>
                    <td>Other:
                    </td>
                    <td>
                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Gifts:
                    </td>
                    <td>
                        <asp:Label ID="lblGifts" runat="server"></asp:Label>
                    </td>
                    <td>Total:
                    </td>
                    <td>
                        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td>:
                                </td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblTotalCurrency" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Advanced Obtained:
                    </td>
                    <td>
                        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:Label ID="lblAdvancedObtained" runat="server"></asp:Label>
                                </td>
                                <td>:
                                </td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblAdvancedObtainedCurrency" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>Amount Adjustment:
                    </td>
                    <td>
                        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:Label ID="lblAmountAdjustment" runat="server"></asp:Label>
                                </td>
                                <td>:
                                </td>
                                <td style="width: 20%;">
                                    <asp:Label ID="lblAmountAdjustmentCurrency" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Tour Cost Recoverable:
                    </td>
                    <td>
                        <asp:Label ID="lblTourCostRecoverable" runat="server"></asp:Label>
                    </td>
                    <td>Adjustment Type:
                    </td>
                    <td>
                        <asp:Label ID="lblAdjustmentType" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <h4>
                <u>New Statement</u></h4>
            <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 20%;">Created By
                    </td>
                    <td style="width: 20%;">Approved By
                    </td>
                    <td style="width: 20%;">Checked By
                    </td>
                    <td style="width: 20%;">Passed By
                    </td>
                    <td style="width: 20%;">Settled By
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreatedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblApprovedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCheckedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPassedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSettledBy" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblApprovedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCheckedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPassedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSettledOn" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <h4>
                <u>Amendment Count [<asp:Label ID="lblAmendmentCount" runat="server" Text="0"></asp:Label>]</u></h4>
            <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 16%;">Amendment By
                    </td>
                    <td style="width: 16%;">Amd. By
                    </td>
                    <td style="width: 16%;">Amd. Approved By
                    </td>
                    <td style="width: 16%;">Amd. Checked By
                    </td>
                    <td style="width: 16%;">Amd. Passed By
                    </td>
                    <td style="width: 20%;">Amd. Settled By
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAmendmentBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedApprovedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedCheckedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedPassedBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedSettledBy" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAmendmentOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedApprovedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedCheckedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedPassedOn" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAmendedSettledOn" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
