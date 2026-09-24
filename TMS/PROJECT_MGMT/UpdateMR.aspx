<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateMR.aspx.cs" Inherits="PROJECT_MGMT_UpdateMR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 90%;">
                    <legend style="text-align: center;">[<asp:Label ID="lblMRNo" runat="server"></asp:Label>]</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project No.:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectNo" runat="server" Width="100%" Height="25px" onblur="return ValidateProject();"
                                    Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Product Code:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProductCode" runat="server" Width="100%" Height="25px" onblur="return ValidateProductCode();"
                                    Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" Width="100%" Height="25px" onblur="return ValidateType();"
                                    Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="25px" onblur="return ValidateUnit();"
                                    Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Expected PO Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtExpectedPODate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdExpectedPODate" runat="server" />
                                            <asp:CalendarExtender ID="calendarExpectedPODate" PopupButtonID="imgbtnExpectedPODate"
                                                runat="server" TargetControlID="txtExpectedPODate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnExpectedPODate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Start Date Calendar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                Delivery Required By:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDeliveryRequiredBy" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdDeliveryRequiredBy" runat="server" />
                                            <asp:CalendarExtender ID="calendarDeliveryRequiredBy" PopupButtonID="imgbtnDeliveryRequiredBy"
                                                runat="server" TargetControlID="txtDeliveryRequiredBy" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnDeliveryRequiredBy" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="End Date Calendar" Width="20px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Document Class:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumentClass" runat="server" Width="100%" onblur="return ValidateDocumentClass();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                UOM:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUOM" runat="server" Width="100%" Height="25px" onblur="return ValidateUOM();">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Quantity:
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="100%" onblur="return ValidateQuantity();"
                                    onkeyup="checkDec(this);" onpaste="return false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Additional Description:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAdditionalDescription" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AV1:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAV1" runat="server" Width="100%" onblur="return ValidateAV1();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                AV2:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAV2" runat="server" Width="100%" onblur="return ValidateAV2();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AV3:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAV3" runat="server" Width="100%" onblur="return ValidateAV3();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                AV4:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAV4" runat="server" Width="100%" onblur="return ValidateAV4();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AV5:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAV5" runat="server" Width="100%" onblur="return ValidateAV5();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Budget:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBudget" runat="server" Width="100%" onblur="return ValidateBudget();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" />
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
                                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                    OnClick="btnSubmit_Click" Width="100%" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>
                    <br />
                    <br />
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtExpectedPODate.ClientID %>').value = document.getElementById('<%=hdExpectedPODate.ClientID %>').value;
            document.getElementById('<%=txtDeliveryRequiredBy.ClientID %>').value = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value;           
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdExpectedPODate.ClientID %>').value = document.getElementById('<%=txtExpectedPODate.ClientID %>').value;
            document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value = document.getElementById('<%=txtDeliveryRequiredBy.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdExpectedPODate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value.split("-");
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
                alert("Invalid Date Range");
                return false;
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdExpectedPODate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value.split("-");
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
                alert("Invalid Date Range");
                return true;
            }
        }
    </script>

    <script type="text/javascript">

        function ValidateProject() {
            var Project = document.getElementById('<%=ddlProjectNo.ClientID %>').selectedIndex;
            if (Project== '' || Project== '0') {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
         function ValidateProductCode() {
            var ProductCode = document.getElementById('<%=ddlProductCode.ClientID %>').selectedIndex;
            if (ProductCode == '') {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateType() {
            var Type = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Type== '' || Type== '0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                        
        function ValidateDocumentClass() {
            var DocumentClass = document.getElementById('<%=txtDocumentClass.ClientID %>').value;
            if (DocumentClass== '') {
                document.getElementById('<%=txtDocumentClass.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDocumentClass.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit== '' || Unit=='0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                               
        function ValidateUOM() {
            var UOM = document.getElementById('<%=ddlUOM.ClientID %>').selectedIndex;
            if (UOM== '' || UOM== '0') {
                document.getElementById('<%=ddlUOM.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUOM.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                     
       function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value;
            if (Quantity== '' || parseFloat(Quantity)==0) {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                              
        function ValidateAV1() {
            var AV1 = document.getElementById('<%=txtAV1.ClientID %>').value;
            if (AV1== '') {
                document.getElementById('<%=txtAV1.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAV1.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateAV2() {
            var AV2 = document.getElementById('<%=txtAV2.ClientID %>').value;
            if (AV2== '') {
                document.getElementById('<%=txtAV2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAV2.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
         function ValidateAV3() {
            var AV3 = document.getElementById('<%=txtAV3.ClientID %>').value;
            if (AV3== '') {
                document.getElementById('<%=txtAV3.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAV3.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateAV4() {
            var AV4 = document.getElementById('<%=txtAV4.ClientID %>').value;
            if (AV4== '') {
                document.getElementById('<%=txtAV4.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAV4.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateAV5() {
            var AV5 = document.getElementById('<%=txtAV5.ClientID %>').value;
            if (AV5== '') {
                document.getElementById('<%=txtAV5.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAV5.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                        
        function ValidateBudget() {
            var Budget = document.getElementById('<%=txtBudget.ClientID %>').value;
            if (Budget== '' || parseFloat(Budget)==0) {
                document.getElementById('<%=txtBudget.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBudget.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                                                                                
        function ValidateAll() {
            var check = true;            
            if (ValidateProject()) {return false;}
            if (ValidateProductCode()) {return false;}
            if (ValidateType()) {return false;}
            if (ValidateDocumentClass()) {return false;}            
            if (ValidateUnit()) {return false;}
            if (ValidateUOM()) {return false;}
            if (ValidateQuantity()) {return false;}
            if (ValidateAV1()) {return false;}
            if (ValidateAV2()) {return false;}
            if (ValidateAV3()) {return false;}
            if (ValidateAV4()) {return false;}
            if (ValidateAV5()) {return false;}
            if (ValidateBudget()) {return false;}  
            if (ValidateDateRange()) {return false;}              
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

    </form>
</body>
</html>
