<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAdvanceRequestInfoPDF.aspx.cs"
    EnableEventValidation="false" Inherits="TOUR_AND_TRAVELS_TRAVEL_AddAdvanceRequestInfoPDF" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../../viewpdftablecss.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/LOT.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/pdficon3.png"
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>
        <br />
        <div>
            <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/COPERION/logo2.png" Height="70px"/>
        </div>
        <div>
            <asp:Literal ID="ltTable" runat="server" />
        </div>
    </form>




    <%--<form id="form1" runat="server">
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
            <u>Tour Information</u></h3>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            last commented from here<tr>
                <th colspan="4" align="center">
                    Travel Statement
                </th>
            </tr>last commented to here
            <tr>
                <td>
                    Tour No.:
                </td>
                <td>
                    <asp:Label ID="lblTourNo" runat="server"></asp:Label>
                </td>
                <td>
                    Tour Sanction No.:
                </td>
                <td>
                    <asp:Label ID="lblTourSanctionNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Emplyoee Name.
                </td>
                <td>
                    <asp:Label ID="lblEmplyoeeName" runat="server"></asp:Label>
                </td>
                <td>
                    Employee ID.
                </td>
                <td>
                    <asp:Label ID="lblEmployeeID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Designation:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date Of Tour:
                </td>
                <td>
                    <asp:Label ID="lblStartDateOfTour" runat="server"></asp:Label>
                </td>
                <td>
                    End Date Of Tour:
                </td>
                <td>
                    <asp:Label ID="lblEndDateOfTour" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Name Of Customer/Vendor:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblCustVendName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Place Of Visit:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblPlaceOfVisit" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Purpose Of Visit:
                </td>
                <td>
                    <asp:Label ID="lblPurposeOfVisit" runat="server"></asp:Label>
                </td>
                <td>
                    Job/Enquiry No.:
                </td>
                <td>
                    <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Business Segment:
                </td>
                <td>
                    <asp:Label ID="lblBusinessSegment" runat="server"></asp:Label>
                </td>
                <td>
                    Mode Of Travel:
                </td>
                <td>
                    <asp:Label ID="lblModeOfTravel" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Type Of Trip:
                </td>
                <td>
                    <asp:Label ID="lblTypeOfTrip" runat="server"></asp:Label>
                </td>
                <td>
                    In Case Of Local Travelling:
                </td>
                <td>
                    <asp:Label ID="lblLocalTravelling" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Expected Expenditure Of The Trip:
                </td>
                <td>
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 70%;">
                                <asp:Label ID="lblExpectedExpenditure" runat="server"></asp:Label>
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblExpectedExpenditureCurrency" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    Advance Required:
                </td>
                <td>
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 70%;">
                                <asp:Label ID="lblAdvanceRequired" runat="server"></asp:Label>
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblAdvanceRequiredCurrency" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td style="width: 25%;">
                    Created By
                </td>
                <td style="width: 25%;">
                    Approved By
                </td>
                <td style="width: 25%;">
                    Deleted By
                </td>
                <td style="width: 25%;">
                    Cancelled By
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
                    <asp:Label ID="lblDeletedBy" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCancelledBy" runat="server"></asp:Label>
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
                    <asp:Label ID="lblDeletedOn" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCancelledOn" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>--%>
</body>
</html>
