<%@ Page Title="Tour Information" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddPeriodTypes.aspx.cs" Inherits="FINANCE_BUDGET_MASTER_AddPeriodTypes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            ValidateDateRange();

        }
    </script>

    <script type="text/javascript">
        function EnableBusinessSegmentOther() {
            var BusinessSegment = document.getElementById('<%=ddlBusinessSegment.ClientID %>');
            var busSegmentText = BusinessSegment.options[BusinessSegment.selectedIndex].innerHTML;
            document.getElementById('<%=txtBusinessSegment.ClientID %>').value = "";
            if (busSegmentText == "Other") {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').disabled = true;
            }
        }


        function EnableModeOfTravel() {
            var ModeOfTravel = document.getElementById('<%=ddlModeOfTravel.ClientID %>');
            var ModeOfTravelText = ModeOfTravel.options[ModeOfTravel.selectedIndex].innerHTML;
            document.getElementById('<%=txtModeOfTravel.ClientID %>').value = "";
            if (ModeOfTravelText == "Other") {
                document.getElementById('<%=txtModeOfTravel.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtModeOfTravel.ClientID %>').disabled = true;
            }
        }



        function EnableLocalTravelling() {
            var LocalTravelling = document.getElementById('<%=ddlLocalTravelling.ClientID %>');
            var LocalTravellingText = LocalTravelling.options[LocalTravelling.selectedIndex].innerHTML;
            document.getElementById('<%=txtLocalTravelling.ClientID %>').value = "";
            if (LocalTravellingText == "Other") {
                document.getElementById('<%=txtLocalTravelling.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtLocalTravelling.ClientID %>').disabled = true;
            }
        }

    </script>

    <script type="text/javascript">

        function ValidateEmployee() {
            var Employee = document.getElementById('<%=ddlEmployee.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustVendName() {
            var CustVendName = document.getElementById('<%=txtCustVendName.ClientID %>').value;
            if (CustVendName == '') {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlaceOfVisit() {
            var PlaceOfVisit = document.getElementById('<%=txtPlaceOfVisit.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePurposeOfVisit() {
            var PurposeOfVisit = document.getElementById('<%=ddlPurposeOfVisit.ClientID %>').selectedIndex;
            if (PurposeOfVisit == '' || PurposeOfVisit == '0') {
                document.getElementById('<%=ddlPurposeOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPurposeOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBusinessSegment() {
            var BusinessSegment = document.getElementById('<%=ddlBusinessSegment.ClientID %>').selectedIndex;
            if (BusinessSegment == '' || BusinessSegment == '0') {
                document.getElementById('<%=ddlBusinessSegment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlBusinessSegment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBusinessSegmentOther() {
            var BusinessSegment = document.getElementById('<%=ddlBusinessSegment.ClientID %>');
            var BusinessSegmentOther = document.getElementById('<%=txtBusinessSegment.ClientID %>').value;
            var busSegmentText = BusinessSegment.options[BusinessSegment.selectedIndex].innerHTML;

            if (busSegmentText == "Other") {
                if (BusinessSegmentOther == '') {
                    document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateModeOfTravel() {
            var ModeOfTravel = document.getElementById('<%=ddlModeOfTravel.ClientID %>').selectedIndex;
            if (ModeOfTravel == '' || ModeOfTravel == '0') {
                document.getElementById('<%=ddlModeOfTravel.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlModeOfTravel.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateModeOfTravelOther() {
            var ModeOfTravel = document.getElementById('<%=ddlModeOfTravel.ClientID %>');
            var ModeOfTravelOther = document.getElementById('<%=txtModeOfTravel.ClientID %>').value;
            var ModeOfTravelText = ModeOfTravel.options[ModeOfTravel.selectedIndex].innerHTML;

            if (ModeOfTravelText == "Other") {
                if (ModeOfTravelOther == '') {
                    document.getElementById('<%=txtModeOfTravel.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtModeOfTravel.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtModeOfTravel.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTypeOfTrip() {
            var TypeOfTrip = document.getElementById('<%=ddlTypeOfTrip.ClientID %>').selectedIndex;
            if (TypeOfTrip == '' || TypeOfTrip == '0') {
                document.getElementById('<%=ddlTypeOfTrip.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfTrip.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLocalTravelling() {
            var LocalTravelling = document.getElementById('<%=ddlLocalTravelling.ClientID %>').selectedIndex;
            if (LocalTravelling == '' || LocalTravelling == '0') {
                document.getElementById('<%=ddlLocalTravelling.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLocalTravelling.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLocalTravellingOther() {
            var LocalTravelling = document.getElementById('<%=ddlLocalTravelling.ClientID %>');
            var LocalTravellingOther = document.getElementById('<%=txtLocalTravelling.ClientID %>').value;
            var LocalTravellingText = LocalTravelling.options[LocalTravelling.selectedIndex].innerHTML;

            if (LocalTravellingText == "Other") {
                if (LocalTravellingOther == '') {
                    document.getElementById('<%=txtLocalTravelling.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtLocalTravelling.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtLocalTravelling.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateExpenditureAmt() {
            var ExpenditureAmt = document.getElementById('<%=txtExpenditureAmt.ClientID %>').value;
            if (ExpenditureAmt == '') {
                document.getElementById('<%=txtExpenditureAmt.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtExpenditureAmt.ClientID %>').style.borderColor = "";
                return false;
            }
        }





        function ValidateAll() {

            var check = true;

            if (ValidateEmployee()) {
                check = false;
            }

            if (ValidateDateRange()) {
                check = false;
            }

            if (ValidateCustVendName()) {
                check = false;
            }

            if (ValidatePlaceOfVisit()) {
                check = false;
            }

            if (ValidatePurposeOfVisit()) {
                check = false;
            }



            if (ValidateBusinessSegment()) {
                check = false;
            }

            if (ValidateBusinessSegmentOther()) {
                check = false;
            }

            if (ValidateModeOfTravel()) {
                check = false;
            }

            if (ValidateModeOfTravelOther()) {
                check = false;
            }



            if (ValidateTypeOfTrip()) {
                check = false;
            }

            if (ValidateLocalTravelling()) {
                check = false;
            }

            if (ValidateLocalTravellingOther()) {
                check = false;
            }

            if (ValidateExpenditureAmt()) {
                check = false;
            }




            var AdvanceCurrency = document.getElementById('<%=ddlAdvanceCurrency.ClientID %>');
            var AdvanceCurrencyValue = AdvanceCurrency.options[AdvanceCurrency.selectedIndex].value;
            var AdvanceCurrencyCode = AdvanceCurrency.options[AdvanceCurrency.selectedIndex].innerHtml;

            if (AdvanceCurrencyCode != "INR" && AdvanceCurrencyValue != 68) {

                if (document.getElementById('<%=fileUploadPassportCopy.ClientID %>') != null) {
                    if (ValidatefileUploadPassportCopy()) {
                        check = false;
                    }
                }                
            }



            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
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


    <script type="text/Javascript">
        function ValidatefileUploadPassportCopy() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadPassportCopy = '';

                        
            var fileUploadPassportCopy = document.getElementById('<%=fileUploadPassportCopy.ClientID %>').value;                
            var divfileUploadPassportCopy = document.getElementById("divfileUploadPassportCopy");
            var lblfileUploadPassportCopy = document.getElementById('<%=lblfileUploadPassportCopy.ClientID %>');

            if (fileUploadPassportCopy == '') {
                document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadPassportCopy.style.display = "none";
                lblfileUploadPassportCopy.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadPassportCopy.toLowerCase())) {
                    document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadPassportCopy.style.display = "block";
                    lblfileUploadPassportCopy.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "";
                    divfileUploadPassportCopy.style.display = "none";
                    lblfileUploadPassportCopy.innerHTML = "";
                    return false;
                }
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/javascript">
        function ChangeByExpenditureCurrency() {

            var ExpenditureCurrency = document.getElementById('<%=ddlExpenditureCurrency.ClientID %>');
            var ExpenditureCurrencyValue = ExpenditureCurrency.options[ExpenditureCurrency.selectedIndex].value;
            document.getElementById('<%=ddlAdvanceCurrency.ClientID %>').value = ExpenditureCurrencyValue;
        }

        function ChangeByAdvanceCurrency() {

            var AdvanceCurrency = document.getElementById('<%=ddlAdvanceCurrency.ClientID %>');
            var AdvanceCurrencyValue = AdvanceCurrency.options[AdvanceCurrency.selectedIndex].value;
            document.getElementById('<%=ddlExpenditureCurrency.ClientID %>').value = AdvanceCurrencyValue;
        }
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 2px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" Text="Tour Information"></asp:Label></legend>
            <table width="100%">
                <%--<tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="4" align="center">
                        
                    </td>
                </tr>--%>
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Employee Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="25px" onblur="return ValidateEmployee();"
                            OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
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
                    <td>&nbsp;
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
                    <td>&nbsp;
                    </td>
                </tr>
                <asp:Panel ID="pnlViewPassportcopy" runat="server" Visible="false" Width="100%">
                    <tr>
                        <td>Passport copy:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%;">
                                        <asp:TextBox ID="txtPassportcopy" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td style="width: 10%;" align="right">
                                        <asp:ImageButton ID="imgbtnPassportcopy" ImageUrl="~/Images/NEWICONS/PASSPORT_01.png"
                                            runat="server" OnClick="imgbtnPassportcopy_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlAddPassportCopy" runat="server" Visible="false" Width="100%">
                    <tr>
                        <td>Passport copy:
                        </td>
                        <td colspan="4">
                            <asp:FileUpload ID="fileUploadPassportCopy" runat="server" Width="100%" Height="29px"
                                BorderStyle="Groove" onblur="return ValidatefileUploadPassportCopy();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <td>
                                <div id="divfileUploadPassportCopy" style="display: none;">
                                    <asp:Label ID="lblfileUploadPassportCopy" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>Starting Date Of Tour:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdStartDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>End Date Of Tour:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdEndDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDateSearch" runat="server"
                                        TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="4" align="center">
                        <asp:Label ID="lblDateMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>Name of Customer/Vendor:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtCustVendName" runat="server" Width="100%" Enabled="true" onblur="return ValidateCustVendName();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Place of Visit:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPlaceOfVisit" runat="server" Width="100%" Enabled="true" onblur="return ValidatePlaceOfVisit();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Purpose of Visit:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPurposeOfVisit" runat="server" Width="100%" Height="25px"
                            onblur="return ValidatePurposeOfVisit();">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>Job/Inq No. Where Applicable:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobInqNo" runat="server" Width="100%" Enabled="true" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Business Segment:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBusinessSegment" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateBusinessSegment();" onchange="EnableBusinessSegmentOther()">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusinessSegment" runat="server" Width="100%" Visible="true" Enabled="false"
                            onblur="return ValidateBusinessSegmentOther();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Mode of Travel:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlModeOfTravel" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateModeOfTravel();" onchange="EnableModeOfTravel()">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtModeOfTravel" runat="server" Width="100%" Visible="true" Enabled="false"
                            onblur="return ValidateModeOfTravelOther();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Type of Trip:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTypeOfTrip" runat="server" Width="100%" Height="26px" onblur="return ValidateTypeOfTrip();">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>In Case of Local Travelling:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLocalTravelling" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateLocalTravelling();" onchange="EnableLocalTravelling()">
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocalTravelling" runat="server" Width="100%" Visible="true" Enabled="false"
                            onblur="return ValidateLocalTravellingOther();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Expected Expenditure Of The Trip:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtExpenditureAmt" runat="server" Width="100%" Enabled="true" onblur="return ValidateExpenditureAmt();"
                                        onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExpenditureCurrency" runat="server" Width="100%" Height="26px"
                                        onchange="ChangeByExpenditureCurrency()">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td>Advance Required:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtAdvanceAmt" runat="server" Width="100%" Enabled="true" onkeyup="checkDec(this);"
                                        onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAdvanceCurrency" runat="server" Width="100%" Height="26px"
                                        onchange="ChangeByAdvanceCurrency()">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Remarks:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                            Rows="2" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                        OnClick="btnSubmit_Click" Width="100%" />
                                </td>
                                <td style="width: 10%">&nbsp;
                                </td>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnTourList" CssClass="button" runat="server" Text="Tour Information List"
                                        OnClick="btnTourList_Click" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
    </div>
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
        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
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
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
