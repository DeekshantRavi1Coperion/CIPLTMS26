<%@ Page Title="Tour Information" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TourInformation.aspx.cs" Inherits="TOUR_AND_TRAVELS_TOUR_TourInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/form.css" rel="stylesheet" />

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
            /////

            var label = document.getElementById('<%= lblMessage.ClientID %>');

            if (ModeOfTravelText == "Air - Business") {
                label.style.display = 'block';//show
            }
            else {
                label.style.display = 'none';//hide
            }
            ////////


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

        <%--function ValidateCountry() {
            var Country = document.getElementById('<%=ddlCountry.ClientID %>').selectedIndex;
              if (Country == '' || Country == '0') {
                  document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                  document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "";
                  return false;
              }
          }--%>

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

        function ValidateCountryOfVisit() {
            var CountryOfVisit = document.getElementById('<%=ddlCountry.ClientID %>').selectedIndex;
            if (CountryOfVisit == '' || CountryOfVisit == '0') {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTourBasedOn() {
            var TourBasedOn = document.getElementById('<%=ddlTourBasedOn.ClientID %>').selectedIndex;
            if (TourBasedOn == '' || TourBasedOn == '0') {
                document.getElementById('<%=ddlTourBasedOn.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTourBasedOn.ClientID %>').style.borderColor = "";
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
        //////

        function ValidateTourInitiative() {
            var TourInitiative = document.getElementById('<%=ddlTourInitiative.ClientID %>').selectedIndex;
            if (TourInitiative == '' && TourInitiative != 0) {
                document.getElementById('<%=ddlTourInitiative.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTourInitiative.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        /////
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

            if (ValidateCountryOfVisit()) {
                check = false;
            }

            if (ValidateTourBasedOn()) {
                check = false;
            }
            /// 
            if (ValidateTourInitiative()) {
                check = false;
            }
            //
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

    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>
                <asp:Label ID="lblLegend" runat="server" Text="Tour Information"></asp:Label></legend>

            <div class="form-grid form-grid-2">
                <label>Employee Name</label>
                <asp:DropDownList ID="ddlEmployee"
                    runat="server"
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true" />


                <%--</div>

            <div class="form-grid form-grid-2">--%>

                <label>Employee ID</label>
                <asp:TextBox ID="txtEmployeeID"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control" />


                <label>Designation</label>
                <%--<div class="full-width">--%>
                <asp:TextBox ID="txtDesignation"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control" />
                <%--</div>--%>


                <%--</div>

            <div class="form-grid form-grid-2">--%>

                <asp:Panel ID="pnlViewPassportcopy" runat="server" Visible="false" Width="100%">
                    <label>Passport copy</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtPassportcopy" CssClass="form-control"
                            runat="server" Width="100%" Enabled="false" />

                        <asp:ImageButton ID="imgbtnPassportcopy" ImageUrl="~/Images/NEWICONS/PASSPORT_01.png"
                            runat="server" OnClick="imgbtnPassportcopy_Click" Height="20px" Width="20" />
                    </div>

                </asp:Panel>
                <asp:Panel ID="pnlAddPassportCopy" runat="server" Visible="false" Width="100%">
                    <label>Passport copy</label>
                    <div class="full-width">
                        <asp:FileUpload ID="fileUploadPassportCopy"
                            runat="server"
                            CssClass="form-file" />
                    </div>
                    <div class="full-width" id="divfileUploadPassportCopy" style="display: none;">
                        <asp:Label ID="lblfileUploadPassportCopy" runat="server" ForeColor="Red" />
                    </div>

                </asp:Panel>


            </div>

            <div class="form-grid form-grid-2">


                <label>Starting Date Of Tour</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtStartDate"
                                runat="server" ReadOnly="true"
                                CssClass="form-control"></asp:TextBox>
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

                <label>End Date Of Tour</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEndDate" runat="server"
                                ReadOnly="true"
                                CssClass="form-control"></asp:TextBox>
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
                <asp:Label ID="lblDateMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />




                <label>Name of Customer/Vendor</label>
                <asp:TextBox ID="txtCustVendName"
                    runat="server"
                    Enabled="true"
                    CssClass="form-control" />


                <label>Place of Visit</label>
                <asp:TextBox ID="txtPlaceOfVisit"
                    runat="server"
                    Enabled="true"
                    CssClass="form-control" />

                <label>Purpose of Visit</label>
                <asp:DropDownList ID="ddlPurposeOfVisit"
                    runat="server"
                    CssClass="form-control" />


                <label>Job/Inq No. Where Applicable</label>
                <asp:TextBox ID="txtJobInqNo"
                    runat="server"
                    Enabled="true"
                    CssClass="form-control" />


                <label>Country of Visit</label>
                <asp:DropDownList ID="ddlCountry"
                    runat="server"
                    CssClass="form-control" />

                <label>Tour Based On</label>
                <asp:DropDownList ID="ddlTourBasedOn"
                    runat="server"
                    CssClass="form-control" />

                <label>Business Segment</label>
                <asp:DropDownList ID="ddlBusinessSegment"
                    runat="server"
                    CssClass="form-control"
                    onchange="EnableBusinessSegmentOther()" />

                &nbsp;

                <asp:TextBox ID="txtBusinessSegment"
                    runat="server"
                    Visible="true" Enabled="false"
                    CssClass="form-control" />



                <label>Mode of Travel</label>
                <asp:DropDownList ID="ddlModeOfTravel"
                    runat="server"
                    CssClass="form-control"
                    onchange="EnableModeOfTravel()" />
                <asp:Label ID="lblMessage" runat="server" Style="display: none;" ForeColor="#ff3300"
                    Text="Please ensure that travel duration must be more than 12 hours"></asp:Label>

                &nbsp;
                
                <asp:TextBox ID="txtModeOfTravel"
                    Visible="true" Enabled="false"
                    runat="server"
                    CssClass="form-control" />


                <label>Type of Trip</label>
                <asp:DropDownList ID="ddlTypeOfTrip"
                    runat="server"
                    CssClass="form-control" />

                <label>Tour Initiative</label>
                <asp:DropDownList ID="ddlTourInitiative"
                    runat="server"
                    CssClass="form-control" />

                <label>In Case of Local Travelling</label>
                <asp:DropDownList ID="ddlLocalTravelling"
                    runat="server"
                    CssClass="form-control"
                    onchange="EnableLocalTravelling()" />

                &nbsp;

                <asp:TextBox ID="txtLocalTravelling"
                    Visible="true" Enabled="false"
                    runat="server"
                    CssClass="form-control" />

                <label>Expected Expenditure Of The Trip</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%;">
                            <asp:TextBox ID="txtExpenditureAmt" runat="server"
                                CssClass="form-control"
                                Enabled="true"
                                onkeyup="checkDec(this);"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExpenditureCurrency" runat="server"
                                CssClass="form-control"
                                onchange="ChangeByExpenditureCurrency()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <label>Advance Required</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%;">
                            <asp:TextBox ID="txtAdvanceAmt" runat="server"
                                CssClass="form-control"
                                Enabled="true" onkeyup="checkDec(this);"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdvanceCurrency" runat="server"
                                CssClass="form-control"
                                onchange="ChangeByAdvanceCurrency()">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <%-- </div>

            <div class="form-grid form-grid-2">--%>


                <label>Remarks</label>
                <div class="full-width">

                    <asp:TextBox ID="txtRemarks"
                        Enabled="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>


            </div>
        </fieldset>
        <div class="full-width button-group">
            <asp:Button ID="btnSubmit"
                runat="server"
                Text="Save"
                CssClass="button"
                OnClientClick="return ValidateAll();"
                OnClick="btnSubmit_Click" Width="50%" />

            <asp:Button ID="btnTourList"
                runat="server"
                Text="Tour List"
                CssClass="button"
                OnClick="btnTourList_Click" Width="50%" />
        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>

    </div>





    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div CssClass="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>


    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            CssClass="popup-iframe"
            id="iframeViewPDFFile"
            runat="server">
        </iframe>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <%--OPEN TOURS DETAIL START--%>
    <asp:Button ID="btnShowPopupOpentours" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeOpentours" runat="server" TargetControlID="btnShowPopupOpentours"
        PopupControlID="pnlPopupOpentours" CancelControlID="imgBtnCancelOpentours"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupOpentours" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelOpentours" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="popup-grid-container">

                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblOpenTours" runat="server" Text="Records[0]" /></legend>


                    <div align="center">
                        <asp:Label ID="lblOpenToursMsg" runat="server" />
                    </div>

                    <asp:GridView ID="gvOpenTours"
                        CssClass="popup-grid"
                        runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="TOUR_NO" HeaderText="TOUR_NO" />
                            <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="TOUR_SANCTION_NO" />
                            <asp:BoundField DataField="TOUR_STATUS_NAME" HeaderText="TOUR_STATUS_NAME" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>

                </fieldset>
            </div>

        </div>



        <div align="center">
            <fieldset style="width: 90%; margin-top: 10px;">
                <legend style="text-align: center;"></legend>
                <br />

                <table style="width: 95%;">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <br />
                <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray; margin-left: 5px;'>
                    <div align="center">
                    </div>
                </div>
            </fieldset>
        </div>


    </asp:Panel>
    <%--OPEN TOURS DETAIL END--%>
</asp:Content>
