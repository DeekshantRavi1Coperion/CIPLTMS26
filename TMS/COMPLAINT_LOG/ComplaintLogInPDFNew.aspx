<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplaintLogInPDFNew.aspx.cs"
    EnableEventValidation="false" Inherits="COMPLAINT_LOG_ComplaintLogInPDFNew" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../viewpdftablecss.css" rel="stylesheet" type="text/css" />--%>
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
        <table style="width: 95%" border="0" cellpadding="2" cellspacing="0">
            <tr>
                <th align="right">
                    <h3>
                        <u>Customer Complaint Form:</u></h3>
                </th>
                <th align="right">
                    <h3>
                        <u>FT/SA/02/02</u></h3>
                </th>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <h3>
            <u>Complaint Detail</u></h3>
        <table style="width: 100%" border="1" cellpadding="2" cellspacing="0">
            <%--<tr>
                <th colspan="4" align="center">
                    Travel Statement
                </th>
            </tr>--%>
            <tr>
                <td>
                    Complaint No.:
                </td>
                <td>
                    <asp:Label ID="lblComplaintNo" runat="server"></asp:Label>
                </td>
                <td>
                    Date Of Complaint Received:
                </td>
                <td>
                    <asp:Label ID="lblDateOfComplaintReceived" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Customer Name:
                </td>
                <td colspan="3">
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 80%;">
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblCustomerCode" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    Addresss:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Location:
                </td>
                <td>
                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                </td>
                <td>
                    Plant:
                </td>
                <td>
                    <asp:Label ID="lblPlant" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Job No.:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblJobNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Customer PO No.:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblCustomerPONo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Item Name:
                </td>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
                <td>
                    Model No:
                </td>
                <td>
                    <asp:Label ID="lblModelNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Type of Service:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblServiceType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Complaint Description:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblComplaintDescription" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <h3>
            <u>Responsible Departartment And Person Detail</u></h3>
        <table style="width: 100%;" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    Responsible Department:
                </td>
                <td>
                    <asp:Label ID="lblResponsibleDepartment" runat="server"></asp:Label>
                </td>
                <td>
                    Responsible Person:
                </td>
                <td>
                    <asp:Label ID="lblResponsiblePerson" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <h3>
            <u>Solution Detail</u></h3>
        <table style="width: 100%;" border="1" cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    Business Unit:
                </td>
                <td>
                    <asp:Label ID="lblBusinessUnit" runat="server"></asp:Label>
                </td>
                <td>
                    Equipment Manufacturer Name:
                </td>
                <td>
                    <asp:Label ID="lblEquipmentManufacturerName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Proposed Actions:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblProposedActions" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Root Cause:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRootCause" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Target Completion Date:
                </td>
                <td>
                    <asp:Label ID="lblTargetCompletionDate" runat="server"></asp:Label>
                </td>
                <td>
                    Actual Completion Date:
                </td>
                <td>
                    <asp:Label ID="lblActualCompletionDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Corrective Action:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblCorrectiveAction" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Lesson Learnt(Resp. Person):
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRespPersonLessonLearnt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Lesson Learnt(Resp. Dept. HOD):
                </td>
                <td colspan="3">
                    <asp:Label ID="lblRespDeptHODLessonLearnt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Lesson Learnt(Service Dept. HOD):
                </td>
                <td colspan="3">
                    <asp:Label ID="lblServiceDeptHODLessonLearnt" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
