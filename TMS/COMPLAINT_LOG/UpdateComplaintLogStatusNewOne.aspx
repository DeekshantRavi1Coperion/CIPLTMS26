<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateComplaintLogStatusNewOne.aspx.cs"
    Inherits="COMPLAINT_LOG_UpdateComplaintLogStatusNewOne" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Update Complaint Status</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
        table
        {
            border: 1px;
            border-collapse: collapse;
        }
        td, th
        {
            padding: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <%-- <asp:UpdatePanel runat="server" ID="uppanel">
            <ContentTemplate>--%>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="700px" Width="1050px"
            Style="display: block">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server"
                            OnClientClick="javascript:window.close();" />
                    </td>
                </tr>
            </table>
            <fieldset style="width: 96%; margin-left: 9px; margin-top: 0px; height: 300px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLegend" runat="server" />
                    <asp:HiddenField ID="hdStatusID" runat="server" />
                    <asp:HiddenField ID="hdComplaintNo" runat="server" />
                    <asp:HiddenField ID="hdPropertyValue" runat="server" />
                    <asp:HiddenField ID="hdRespDeptID" runat="server" />
                </legend>
                <div style='overflow: auto; width: 99%; height: 600px; border: 1px solid lightgray;
                    margin-left: 10px;'>
                    <table width="95%" style="margin-left: 20px;">
                        <tr>
                            <td colspan="5" align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlNewComplaint" runat="server" Enabled="false">
                            <tr>
                                <td>
                                    Date of Complaint Received:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComplanitRcvdDate" runat="server" ReadOnly="true" Width="100%"
                                        Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Location:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Name:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtCustomerNameNew" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="3" runat="server" Width="100%"
                                        Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Plant:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPlant" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    JOB No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer PO Number:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPONumber" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Item Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItemName" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Model No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtModelNo" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Type Of Service:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTypeOfService" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Complaint Description:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtComplaintDescription" runat="server" Width="100%" TextMode="MultiLine"
                                        Rows="2" Enabled="false" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlAssignment" runat="server">
                            <tr>
                                <td>
                                    Responsible Department:
                                </td>
                                <td style="width: 30%;">
                                    <asp:DropDownList ID="ddlResponsibleDepartment" runat="server" Width="100%" OnSelectedIndexChanged="ddlResponsibleDepartment_SelectedIndexChanged"
                                        onblur="return ValidateResponsibleDepartment();" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Responsible Person:
                                </td>
                                <td style="width: 30%;">
                                    <asp:DropDownList ID="ddlResponsiblePerson" runat="server" Width="100%" onblur="return ValidateResponsiblePerson();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTargetDate" runat="server" Text="Target Completion Date:"></asp:Label>
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtTargetCompletionDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                <asp:HiddenField ID="hdTargetCompletionDate" runat="server" />
                                                <asp:CalendarExtender ID="calendarTargetCompletionDate" PopupButtonID="imgbtnTargetCompletionDate"
                                                    runat="server" TargetControlID="txtTargetCompletionDate" Format="dd-MMM-yyyy"
                                                    OnClientDateSelectionChanged="clientChangedTargetDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgbtnTargetCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Start Date Calendar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlResolve" runat="server">
                            <tr>
                                <td>
                                    Business Unit:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBusinessUnit" runat="server" Width="100%" Height="25px"
                                        onblur="return ValidateBusinessUnit();" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Equip. Manufacturer Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEquipmentManufacturerName" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Proposed Actions:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtProposedActions" runat="server" Width="100%" TextMode="MultiLine"
                                        Rows="2" onblur="return ValidateProposedActions();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Root Cause:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtRootCause" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                        Rows="2" onblur="return ValidateRootCause();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Actual Completion Date:
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtActualCompletionDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                <asp:HiddenField ID="hdActualCompletionDate" runat="server" />
                                                <asp:CalendarExtender ID="calendarActualCompletionDate" PopupButtonID="imgBtnActualCompletionDate"
                                                    runat="server" TargetControlID="txtActualCompletionDate" Format="dd-MMM-yyyy"
                                                    OnClientDateSelectionChanged="clientChangedActualDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="imgBtnActualCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Calendar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Corrective Action:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtCorrectiveAction" runat="server" Width="100%" TextMode="MultiLine"
                                        Rows="2" onblur="return ValidateCorrectiveAction();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Lesson Learnt(Resp. Person):
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtRespPersonLessonLearnt" runat="server" Width="100%" />
                                </td>
                            </tr>
                            <asp:Panel ID="pnlVisitReportSummary" runat="server">
                                <tr>
                                    <td>
                                        Sanction No.:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSanctionNo" runat="server" Width="100%" OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Visit Report Summary 1:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fileUploadVisitReport1" runat="server" Width="100%" Height="29px"
                                            BorderStyle="Groove" onblur="return ValidatefileUploadVisitReport1();" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <div id="divfileUploadVisitReport1" style="display: none;">
                                            <asp:Label ID="lblfileUploadVisitReport1" runat="server" ForeColor="Red" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Visit Report Summary 2:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fileUploadVisitReport2" runat="server" Width="100%" Height="29px"
                                            BorderStyle="Groove" onblur="return ValidatefileUploadVisitReport2();" Enabled="false" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Visit Report Summary 3:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fileUploadVisitReport3" runat="server" Width="100%" Height="29px"
                                            BorderStyle="Groove" onblur="return ValidatefileUploadVisitReport3();" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <div id="divfileUploadVisitReport2" style="display: none;">
                                            <asp:Label ID="lblfileUploadVisitReport2" runat="server" ForeColor="Red" />
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <div id="divfileUploadVisitReport3" style="display: none;">
                                            <asp:Label ID="lblfileUploadVisitReport3" runat="server" ForeColor="Red" />
                                        </div>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlAttachFiles" runat="server">
                            <tr>
                                <td>
                                    Other Attachment 1:
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileUploadAttachment1" runat="server" Width="100%" Height="29px"
                                        BorderStyle="Groove" onblur="return ValidatefileUploadAttachment1();" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    Other Attachment 2:
                                </td>
                                <td>
                                    <asp:FileUpload ID="fileUploadAttachment2" runat="server" Width="100%" Height="29px"
                                        BorderStyle="Groove" onblur="return ValidatefileUploadAttachment2();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <div id="divfileUploadAttachment1" style="display: none;">
                                        <asp:Label ID="lblfileUploadAttachment1" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td>
                                    <div id="divfileUploadAttachment2" style="display: none;">
                                        <asp:Label ID="lblfileUploadAttachment2" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlViewComplaintFiles" runat="server">
                            <tr>
                                <td>
                                    View Compaint File 1:
                                </td>
                                <td style="width: 30%;">
                                    <table width="100%">
                                        <tr>
                                            <td width="90%">
                                                <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                                    OnClick="btnViewAttachment1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    View Compaint File 2:
                                </td>
                                <td style="width: 30%;">
                                    <table width="100%">
                                        <tr>
                                            <td width="90%">
                                                <asp:TextBox ID="txtViewAttachment2" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btnViewAttachment2" Height="20px" Width="20px" runat="server"
                                                    OnClick="btnViewAttachment2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlViewRelovedFiles" runat="server">
                            <tr>
                                <td>
                                    View Other Attachment 1:
                                </td>
                                <td style="width: 30%;">
                                    <table width="100%">
                                        <tr>
                                            <td width="90%">
                                                <asp:TextBox ID="txtViewAttachment3" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btnViewAttachment3" Height="20px" Width="20px" runat="server"
                                                    OnClick="btnViewAttachment3_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    View Other Attachment 2:
                                </td>
                                <td style="width: 30%;">
                                    <table width="100%">
                                        <tr>
                                            <td width="90%">
                                                <asp:TextBox ID="txtViewAttachment4" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="btnViewAttachment4" Height="20px" Width="20px" runat="server"
                                                    OnClick="btnViewAttachment4_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlRespDeptHODLessonLearnt" runat="server">
                            <tr>
                                <td>
                                    Lesson Learnt(Resp. Dept. HOD):
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtRespDeptHODLessonLearnt" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlServiceDeptHODLessonLearnt" runat="server">
                            <tr>
                                <td>
                                    Lesson Learnt(Service Dept. HOD):
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtServiceDeptHODLessonLearnt" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td align="center" style="width: 45%">
                                            <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                                                Width="100%" OnClick="btnSubmit_Click" />
                                        </td>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                        <td align="center" style="width: 45%">
                                            <asp:Button ID="btnComplaintLogList" CssClass="button" runat="server" Text="Complaint Log List"
                                                Width="100%" OnClick="btnComplaintLogList_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
        <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
            PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
            Width="1050px" Style="display: block">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                    </td>
                </tr>
            </table>
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                margin-left: 25px;'>
                <asp:Image ID="imgFile" runat="server" />
            </div>
        </asp:Panel>
        <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
            PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
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
                <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                    margin-left: 25px;'>
                </div>
            </iframe>
        </asp:Panel>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>

    <script type="text/javascript" language="javascript">
        function pageLoad() {                                    
        
            if(document.getElementById('<%=hdTargetCompletionDate.ClientID %>')!=null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>')!=null)
            {                
               document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value;
                document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value = document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value;
                document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value;
            }    
            
            if(document.getElementById('<%=hdActualCompletionDate.ClientID %>')!=null)
            {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').value = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value;                
                document.getElementById('<%=hdActualCompletionDate.ClientID %>').value = document.getElementById('<%=txtActualCompletionDate.ClientID %>').value;
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').value = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value;
            }
        }
                
        function clientChangedTargetDate(sender, args) {
            document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value = document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value;
         
         var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);


            if(document.getElementById('<%=hdTargetCompletionDate.ClientID %>')!=null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>')!=null)
            {
                if (endFormatedDate < formatedDate) {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "#F7627F";  
                    alert("Target completion date must be equal or greater than complaint received date!");
                    return true;
                }                           
                else
                {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "";
                    return false;
                }                   
            }                        
         }
        
        
        function clientChangedActualDate(sender, args) {
            document.getElementById('<%=hdActualCompletionDate.ClientID %>').value = document.getElementById('<%=txtActualCompletionDate.ClientID %>').value;
        
        
        var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                alert("Actual completion date must be equal or greater than target completion date!");
                return true;
            }                       
            else
            {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
        
        }
        
          
    </script>

    <script type="text/Javascript">
        function ValidateTargetDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");            
            var dateItems = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);


            if(document.getElementById('<%=hdTargetCompletionDate.ClientID %>')!=null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>')!=null)
            {
                if (endFormatedDate < formatedDate) {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "#F7627F";  
                    alert("Target completion date must be equal or greater than complaint received date!");
                    return true;
                }
                else
                {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateActualDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                alert("Actual completion date must be equal or greater than target completion date!");
                return true;
            }                                 
            else
            {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
            
        }
    </script>

    <script type="text/javascript">
                                                             
        function ValidateResponsibleDepartment() {
            var hdStatusID = document.getElementById('<%=hdStatusID.ClientID %>').value;                                
            var ResponsibleDepartment = document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').selectedIndex;                                       
            if(hdStatusID=='1')
            {
                if (ResponsibleDepartment == '' || ResponsibleDepartment == '0') {
                    document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                    return false;
                } 
            }
             else {
                document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                return false;
            }          
        }
        
        function ValidateResponsiblePerson() {
            var hdStatusID = document.getElementById('<%=hdStatusID.ClientID %>').value; 
            var hdPropertyValue = document.getElementById('<%=hdPropertyValue.ClientID %>').value;  
            var ResponsibleDepartment = document.getElementById('<%=ddlResponsibleDepartment.ClientID %>');
            
            var ResponsibleDepartmentID=ResponsibleDepartment.options[ResponsibleDepartment.selectedIndex].value;
            
            var ResponsiblePerson = document.getElementById('<%=ddlResponsiblePerson.ClientID %>').selectedIndex;
                
                
                if(hdStatusID=='2' || ResponsibleDepartmentID=='8' || ResponsibleDepartmentID=='18')
                {
                    if (ResponsiblePerson == '' || ResponsiblePerson == '0') {
                    document.getElementById('<%=ddlResponsiblePerson.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                    }
                    else {
                        document.getElementById('<%=ddlResponsiblePerson.ClientID %>').style.borderColor = "";
                        return false;
                    }
                }
                 else {
                    document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                    return false;
                }
        }                                                                  
    </script>

    <script type="text/javascript" language="javascript">
       
       function ValidateBusinessUnit() {
            var BusinessUnit = document.getElementById('<%=ddlBusinessUnit.ClientID %>').selectedIndex;
            if (BusinessUnit == '' || BusinessUnit == '0') {
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                                                                        
        function ValidateProposedActions() {
            var ProposedActions = document.getElementById('<%=txtProposedActions.ClientID %>').value;
            if (ProposedActions == '') {
                document.getElementById('<%=txtProposedActions.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProposedActions.ClientID %>').style.borderColor = "";
                return false;
            }
        }  
        
        function ValidateRootCause () {
            var RootCause = document.getElementById('<%=txtRootCause.ClientID %>').value;
            if (RootCause == '') {
                document.getElementById('<%=txtRootCause.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRootCause.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        
           function ValidateCorrectiveAction () {
            var CorrectiveAction = document.getElementById('<%=txtCorrectiveAction.ClientID %>').value;
            if (CorrectiveAction == '') {
                document.getElementById('<%=txtCorrectiveAction.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCorrectiveAction.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidatefileUploadVisitReport1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport1 = document.getElementById('<%=fileUploadVisitReport1.ClientID %>').value;
            var divfileUploadVisitReport1 = document.getElementById("divfileUploadVisitReport1");
            var lblfileUploadVisitReport1 = document.getElementById('<%=lblfileUploadVisitReport1.ClientID %>');

            if (fileUploadVisitReport1 == '') {
                document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport1.style.display = "none";
                lblfileUploadVisitReport1.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport1.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport1.style.display = "block";
                    lblfileUploadVisitReport1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport1.style.display = "none";
                    lblfileUploadVisitReport1.innerHTML = "";
                    return false;
                }
            }
        }
        
        
         function ValidatefileUploadVisitReport2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport2 = document.getElementById('<%=fileUploadVisitReport2.ClientID %>').value;
            var divfileUploadVisitReport2 = document.getElementById("divfileUploadVisitReport2");
            var lblfileUploadVisitReport2 = document.getElementById('<%=lblfileUploadVisitReport2.ClientID %>');

            if (fileUploadVisitReport2 == '') {
                document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport2.style.display = "none";
                lblfileUploadVisitReport2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport2.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport2.style.display = "block";
                    lblfileUploadVisitReport2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport2.style.display = "none";
                    lblfileUploadVisitReport2.innerHTML = "";
                    return false;
                }
            }
        }
        
        
       function ValidatefileUploadVisitReport3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport3 = document.getElementById('<%=fileUploadVisitReport3.ClientID %>').value;
            var divfileUploadVisitReport3 = document.getElementById("divfileUploadVisitReport3");
            var lblfileUploadVisitReport3 = document.getElementById('<%=lblfileUploadVisitReport3.ClientID %>');

            if (fileUploadVisitReport3 == '') {
                document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport3.style.display = "none";
                lblfileUploadVisitReport3.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport3.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport3.style.display = "block";
                    lblfileUploadVisitReport3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport3.style.display = "none";
                    lblfileUploadVisitReport3.innerHTML = "";
                    return false;
                }
            }
        }
        

        function ValidatefileUploadAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment1 = document.getElementById('<%=fileUploadAttachment1.ClientID %>').value;
            var divfileUploadAttachment1 = document.getElementById("divfileUploadAttachment1");
            var lblfileUploadAttachment1 = document.getElementById('<%=lblfileUploadAttachment1.ClientID %>');

            if (fileUploadAttachment1 == '') {
                document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                divfileUploadAttachment1.style.display = "none";
                lblfileUploadAttachment1.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment1.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadAttachment1.style.display = "block";
                    lblfileUploadAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment1.style.display = "none";
                    lblfileUploadAttachment1.innerHTML = "";
                    return false;
                }
            }
        }
        
        function ValidatefileUploadAttachment2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment2 = document.getElementById('<%=fileUploadAttachment2.ClientID %>').value;
            var divfileUploadAttachment2 = document.getElementById("divfileUploadAttachment2");
            var lblfileUploadAttachment2 = document.getElementById('<%=lblfileUploadAttachment2.ClientID %>');

            if (fileUploadAttachment2 == '') {
                document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "";
                divfileUploadAttachment2.style.display = "none";
                lblfileUploadAttachment2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment2.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadAttachment2.style.display = "block";
                    lblfileUploadAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment2.style.display = "none";
                    lblfileUploadAttachment2.innerHTML = "";
                    return false;
                }
            }
        }
                        
    </script>

    <script type="text/javascript" language="javascript">
    
    function ValidateAll() {
            
            var check = true;
                                                                        
            if (document.getElementById('<%=ddlResponsibleDepartment.ClientID %>') !=null)           
            {                               
                if (ValidateResponsibleDepartment()) {               
                    return false;
                }                
                
                if (ValidateResponsiblePerson()) {
                    return false;
                }   
                
                 if (ValidateTargetDateRange()) {                
                    return false;
                } 
            }
            
            if (document.getElementById('<%=ddlBusinessUnit.ClientID %>') !=null) 
            {
                if (ValidateBusinessUnit()) {
                    return false;
                }
                
                if (ValidateProposedActions()) {
                    return false;
                }
                
                 if (ValidateRootCause()) {
                    return false;
                }
                                                         
                if (ValidateCorrectiveAction()) {
                    return false;
                }
                
                if (ValidateActualDateRange()) {
                    return false;
                }
                
                if (document.getElementById('<%=fileUploadVisitReport1.ClientID %>') !=null)  
                {                
                    if (ValidatefileUploadVisitReport1()) {
                        return false;
                    }
                    
                    if (ValidatefileUploadVisitReport2()) {
                        return false;
                    }
                    
                    if (ValidatefileUploadVisitReport3()) {
                        return false;
                    }
                }
                                                                                                                
                if (document.getElementById('<%=fileUploadAttachment1.ClientID %>') !=null)  
                {
                    if (ValidatefileUploadAttachment1()) {
                        return false;
                    }
                    
                    if (ValidatefileUploadAttachment2()) {
                        return false;
                    }
                }  
            }
                                                                                               
            return check;
        }
    </script>

</body>
</html>
