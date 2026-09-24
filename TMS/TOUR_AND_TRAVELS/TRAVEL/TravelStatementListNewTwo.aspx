<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="TravelStatementListNewTwo.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TravelStatementListNewTwo" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;

            if (document.getElementById('<%=hdAccDated.ClientID %>') != null) {
                document.getElementById('<%=txtAccDated.ClientID %>').value = document.getElementById('<%=hdAccDated.ClientID %>').value;
            }

            if (document.getElementById('<%=hdDNDate.ClientID %>') != null) {
                document.getElementById('<%=txtDNDate.ClientID %>').value = document.getElementById('<%=hdDNDate.ClientID %>').value;
            }

            if (document.getElementById('<%=hdVoucherDate.ClientID %>') != null) {
                document.getElementById('<%=txtVoucherDate.ClientID %>').value = document.getElementById('<%=hdVoucherDate.ClientID %>').value;
            }
        }

        function clientChangedAccDated(sender, args) {
            document.getElementById('<%=hdAccDated.ClientID %>').value = document.getElementById('<%=txtAccDated.ClientID %>').value;
        }

        function clientChangedDNDate(sender, args) {
            document.getElementById('<%=hdDNDate.ClientID %>').value = document.getElementById('<%=txtDNDate.ClientID %>').value;
        }

        function clientChangedVoucherDate(sender, args) {
            document.getElementById('<%=hdVoucherDate.ClientID %>').value = document.getElementById('<%=txtVoucherDate.ClientID %>').value;
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdStartDateSearch.ClientID %>').value = document.getElementById('<%=txtStartDateSearch.ClientID %>').value;
            document.getElementById('<%=hdEndDateSearch.ClientID %>').value = document.getElementById('<%=txtEndDateSearch.ClientID %>').value;


            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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
        function checkDecNew(el) {
            var advanceObtained = '0';
            var Airfare = '0';
            var mobile = '0';
            var lodging = '0';
            var tips = '0';
            var meals = '0';
            var visaFee = '0';
            var groundTransport = '0';
            var dailyAllowance = '0';
            var entertainment = '0';
            var other = '0';
            var gifts = '0';


            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value != '') {
                    alert(el.value);
                }
                else {
                    alert('null');
                }
            }
            else {
                if (document.getElementById('<%=txtAirfare.ClientID %>').value != '') {
                    Airfare = document.getElementById('<%=txtAirfare.ClientID %>').value;
                }
                else {
                    Airfare = '0';
                }

                if (document.getElementById('<%=txtMobile.ClientID %>').value != '') {
                    mobile = document.getElementById('<%=txtMobile.ClientID %>').value;
                }
                else {
                    mobile = '0';
                }

                if (document.getElementById('<%=txtLodging.ClientID %>').value != '') {
                    lodging = document.getElementById('<%=txtLodging.ClientID %>').value;
                }
                else {
                    lodging = '0';
                }

                if (document.getElementById('<%=txtTips.ClientID %>').value != '') {
                    tips = document.getElementById('<%=txtTips.ClientID %>').value;
                }
                else {
                    tips = '0';
                }

                if (document.getElementById('<%=txtMeals.ClientID %>').value != '') {
                    meals = document.getElementById('<%=txtMeals.ClientID %>').value;
                }
                else {
                    meals = '0';
                }

                if (document.getElementById('<%=txtVisaFee.ClientID %>').value != '') {
                    visaFee = document.getElementById('<%=txtVisaFee.ClientID %>').value;
                }
                else {
                    visaFee = '0';
                }

                if (document.getElementById('<%=txtGroundTransport.ClientID %>').value != '') {
                    groundTransport = document.getElementById('<%=txtGroundTransport.ClientID %>').value;
                }
                else {
                    groundTransport = '0';
                }

                if (document.getElementById('<%=txtDailyAllowance.ClientID %>').value != '') {
                    dailyAllowance = document.getElementById('<%=txtDailyAllowance.ClientID %>').value;
                }
                else {
                    dailyAllowance = '0';
                }

                if (document.getElementById('<%=txtEntertainment.ClientID %>').value != '') {
                    entertainment = document.getElementById('<%=txtEntertainment.ClientID %>').value;
                }
                else {
                    entertainment = '0';
                }

                if (document.getElementById('<%=txtOther.ClientID %>').value != '') {
                    other = document.getElementById('<%=txtOther.ClientID %>').value;
                }
                else {
                    other = '0';
                }

                if (document.getElementById('<%=txtGifts.ClientID %>').value != '') {
                    gifts = document.getElementById('<%=txtGifts.ClientID %>').value;
                }
                else {
                    gifts = '0';
                }

                document.getElementById('<%=txtTotal.ClientID %>').value = parseFloat(Airfare) + parseFloat(mobile) + parseFloat(lodging) +
                    parseFloat(tips) + parseFloat(meals) + parseFloat(visaFee) +
                    parseFloat(groundTransport) + parseFloat(dailyAllowance) +
                    parseFloat(entertainment) + parseFloat(other) + parseFloat(gifts);

                if (document.getElementById('<%=txtAdvanceObtained.ClientID %>').value != '') {
                    advanceObtained = document.getElementById('<%=txtAdvanceObtained.ClientID %>').value;
                }
                else {
                    advanceObtained = '0';
                };

              <%--  document.getElementById('<%=txtDeltaAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value) - parseFloat(advanceObtained);


                document.getElementById('<%=txtFinalAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtDeltaAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtAdjustedAmount.ClientID %>').value);--%>



                if (parseFloat(document.getElementById('<%=txtFinalAmount.ClientID %>').value) < 0) {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
                }
                else {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
                }
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
            var advanceObtained = '0';
            var Airfare = '0';
            var mobile = '0';
            var lodging = '0';
            var tips = '0';
            var meals = '0';
            var visaFee = '0';
            var groundTransport = '0';
            var dailyAllowance = '0';
            var entertainment = '0';
            var other = '0';
            var gifts = '0';

            if (document.getElementById('<%=txtAirfare.ClientID %>').value != '') {
                Airfare = document.getElementById('<%=txtAirfare.ClientID %>').value;
            }
            else {
                Airfare = '0';
            }

            if (document.getElementById('<%=txtMobile.ClientID %>').value != '') {
                mobile = document.getElementById('<%=txtMobile.ClientID %>').value;
            }
            else {
                mobile = '0';
            }

            if (document.getElementById('<%=txtLodging.ClientID %>').value != '') {
                lodging = document.getElementById('<%=txtLodging.ClientID %>').value;
            }
            else {
                lodging = '0';
            }

            if (document.getElementById('<%=txtTips.ClientID %>').value != '') {
                tips = document.getElementById('<%=txtTips.ClientID %>').value;
            }
            else {
                tips = '0';
            }

            if (document.getElementById('<%=txtMeals.ClientID %>').value != '') {
                meals = document.getElementById('<%=txtMeals.ClientID %>').value;
            }
            else {
                meals = '0';
            }

            if (document.getElementById('<%=txtVisaFee.ClientID %>').value != '') {
                visaFee = document.getElementById('<%=txtVisaFee.ClientID %>').value;
            }
            else {
                visaFee = '0';
            }

            if (document.getElementById('<%=txtGroundTransport.ClientID %>').value != '') {
                groundTransport = document.getElementById('<%=txtGroundTransport.ClientID %>').value;
            }
            else {
                groundTransport = '0';
            }

            if (document.getElementById('<%=txtDailyAllowance.ClientID %>').value != '') {
                dailyAllowance = document.getElementById('<%=txtDailyAllowance.ClientID %>').value;
            }
            else {
                dailyAllowance = '0';
            }

            if (document.getElementById('<%=txtEntertainment.ClientID %>').value != '') {
                entertainment = document.getElementById('<%=txtEntertainment.ClientID %>').value;
            }
            else {
                entertainment = '0';
            }

            if (document.getElementById('<%=txtOther.ClientID %>').value != '') {
                other = document.getElementById('<%=txtOther.ClientID %>').value;
            }
            else {
                other = '0';
            }

            if (document.getElementById('<%=txtGifts.ClientID %>').value != '') {
                gifts = document.getElementById('<%=txtGifts.ClientID %>').value;
            }
            else {
                gifts = '0';
            }

            document.getElementById('<%=hdTotal.ClientID %>').value = Math.round((parseFloat(Airfare) + parseFloat(mobile) + parseFloat(lodging) +
                parseFloat(tips) + parseFloat(meals) + parseFloat(visaFee) +
                parseFloat(groundTransport) + parseFloat(dailyAllowance) +
                parseFloat(entertainment) + parseFloat(other) + parseFloat(gifts)) * 100) / 100;


            document.getElementById('<%=txtTotal.ClientID %>').value = document.getElementById('<%=hdTotal.ClientID %>').value;

            if (document.getElementById('<%=txtAdvanceObtained.ClientID %>').value != '') {
                advanceObtained = document.getElementById('<%=txtAdvanceObtained.ClientID %>').value;
            }
            else {
                advanceObtained = '0';
            };


          <%--  document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value = Math.round(parseFloat(advanceObtained) - parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value));
            document.getElementById('<%=txtAdjustedAmount.ClientID %>').value = document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value--%>


<%--            if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) == 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = '';
            }
            else if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
            }
            else {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
            }--%>
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
        function checkDecNew5(el) {
            var ex = /^[0-9]+\*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/javascript">

        <%-- function ValidateEmployee() {
            var Employee = document.getElementById('<%=ddlEmployee.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        function ValidateEmployee() {
            var Employee = document.getElementById('<%=txtEmployeeName.ClientID %>').value;
             if (Employee == '') {
                 document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                 document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "";
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
            var PurposeOfVisit = document.getElementById('<%=txtPurposeOfVisit.ClientID %>').value;
            if (PurposeOfVisit == '') {
                document.getElementById('<%=txtPurposeOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPurposeOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBusinessSegment() {
            var BusinessSegment = document.getElementById('<%=txtBusinessSegment.ClientID %>').value;
            if (BusinessSegment == '') {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateddlVisitSummaryAttached() {
            var visitSummaryAttached = document.getElementById('<%=ddlVisitSummaryAttached.ClientID %>').selectedIndex;
            document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').value = '';
            document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').value = '';
            document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').value = '';
            if (visitSummaryAttached == '' || visitSummaryAttached == '0') {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').disabled = true;
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').disabled = true;
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').disabled = false;
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').disabled = false;
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').disabled = false;
            }
            ValidatefileUploadVisitSummary1();
            ValidatefileUploadVisitSummary2();
            ValidatefileUploadVisitSummary3();
        }

        function ValidatefileUploadVisitSummary1() {

            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary1 = document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').value;
            var divfileUploadVisitSummary1 = document.getElementById("divfileUploadVisitSummary1");
            var lblfileUploadVisitSummary1 = document.getElementById('<%=lblfileUploadVisitSummary1.ClientID %>');
            var visitSummaryAttached = document.getElementById('<%=ddlVisitSummaryAttached.ClientID %>').selectedIndex;


            if (visitSummaryAttached != '' || visitSummaryAttached != '0') {
                if (fileUploadVisitSummary1 == '') {
                    document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary1.style.display = "none";
                    lblfileUploadVisitSummary1.innerHTML = "";
                    return true;
                }
                else {
                    if (!regex.test(fileUploadVisitSummary1.toLowerCase())) {
                        document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "#F7627F";
                        divfileUploadVisitSummary1.style.display = "block";
                        lblfileUploadVisitSummary1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "";
                        divfileUploadVisitSummary1.style.display = "none";
                        lblfileUploadVisitSummary1.innerHTML = "";
                        return false;
                    }
                }
            }
            else {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary1.style.display = "none";
                lblfileUploadVisitSummary1.innerHTML = "";
                return false;
            }
        }

        function ValidatefileUploadVisitSummary2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary2 = document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').value;
            var divfileUploadVisitSummary2 = document.getElementById("divfileUploadVisitSummary2");
            var lblfileUploadVisitSummary2 = document.getElementById('<%=lblfileUploadVisitSummary2.ClientID %>');

            if (fileUploadVisitSummary2 == '') {
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary2.style.display = "none";
                lblfileUploadVisitSummary2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitSummary2.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary2.style.display = "block";
                    lblfileUploadVisitSummary2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "";
                    divfileUploadVisitSummary2.style.display = "none";
                    lblfileUploadVisitSummary2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileUploadVisitSummary3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary3 = document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').value;
            var divfileUploadVisitSummary3 = document.getElementById("divfileUploadVisitSummary3");
            var lblfileUploadVisitSummary3 = document.getElementById('<%=lblfileUploadVisitSummary3.ClientID %>');

            if (fileUploadVisitSummary3 == '') {
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary3.style.display = "none";
                lblfileUploadVisitSummary3.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitSummary3.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary3.style.display = "block";
                    lblfileUploadVisitSummary3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "";
                    divfileUploadVisitSummary3.style.display = "none";
                    lblfileUploadVisitSummary3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateVoucherNo() {
            if (document.getElementById('<%=txtVoucherNo.ClientID %>') != null) {
                var VoucherNo = document.getElementById('<%=txtVoucherNo.ClientID %>').value;

                if (VoucherNo == '') {
                    document.getElementById('<%=txtVoucherNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtVoucherNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ValidateVoucherType() {

            if (document.getElementById('<%=ddlVoucherType.ClientID %>') != null) {
                var VoucherType = document.getElementById('<%=ddlVoucherType.ClientID %>').selectedIndex;
                if (VoucherType == '' || VoucherType == '0') {
                    document.getElementById('<%=ddlVoucherType.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlVoucherType.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                return false;
            }
        }

    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            var updationFlag = document.getElementById('<%=hdUpdationFlag.ClientID %>').value

            if (ValidateEmployee()) {
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
            
            if (updationFlag == 1) {
                if (ValidatefileUploadVisitSummary1()) {
                    check = false;
                }

                if (ValidatefileUploadVisitSummary2()) {
                    check = false;
                }

                if (ValidatefileUploadVisitSummary3()) {
                    check = false;
                }
            }

            if (ValidateVoucherNo()) {
                check = false;
            }

            if (ValidateVoucherType()) {
                check = false;
            }

            

            if (check) {
                if (confirm("Would you like to update?")) {
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

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdUpdationFlag" runat="server" />
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 60%">
            <legend style="text-align: center;">Travel Statement List</legend>
            <table width="100%">
                <tr>
                    <td align="right">Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                        runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" Width="20px" /></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">Sanction No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSanctionNo" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">Status.:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">Employee Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                        OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnAddNewTravelStmt" CssClass="button" Width="100%" runat="server"
                                        Text="Add New" OnClick="btnAddNewTravelStmt_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 50%; border: 1px solid lightgray;'>
                <asp:GridView ID="gvTravelList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                    AllowPaging="True" OnPageIndexChanging="gvTravelList_PageIndexChanging" OnRowCommand="gvTravelList_RowCommand"
                    OnRowDataBound="gvTravelList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="STATEMENT_ID" HeaderText="STATEMENT_ID" Visible="False" />
                        <asp:TemplateField HeaderText="VIEW">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/pdficon1.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PASSPORT">
                            <ItemTemplate>
                                <asp:Label ID="lblPassportCopyName" runat="server" Visible="false" Text='<%# Eval("PASSPORT_COPY_NAME") %>' />
                                <asp:ImageButton ID="btnPassportCopy" Height="20px" Width="20px" ImageUrl="~/Images/NEWICONS/PASSPORT_01.png"
                                    CommandArgument="VIEWPASSPORT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FILE_1">
                            <ItemTemplate>
                                <asp:Label ID="lblVisitRptSummaryOneName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_ONE_NAME") %>' />
                                <asp:ImageButton ID="btnVisitRptSummaryOneName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FILE_2">
                            <ItemTemplate>
                                <asp:Label ID="lblVisitRptSummaryTwoName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_TWO_NAME") %>' />
                                <asp:ImageButton ID="btnVisitRptSummaryTwoName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FILE_3">
                            <ItemTemplate>
                                <asp:Label ID="lblVisitRptSummaryThreeName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_THREE_NAME") %>' />
                                <asp:ImageButton ID="btnVisitRptSummaryThreeName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SEND_MAIL" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnSendMail" CommandArgument="SEND_MAIL" runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="APPROVE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnApproval" CommandArgument="APPROVE" ToolTip="Approve Tour" runat="server"
                                    Text="Approve" CssClass="cancelbutton" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="SANCTION_NO" />
                        <asp:TemplateField HeaderText="STATUS">
                            <ItemTemplate>
                                <asp:Label ID="lblStatementID" runat="server" Visible="false" Text='<%# Eval("STATEMENT_ID") %>' />
                                <asp:Label ID="lblStatementNo" runat="server" Visible="false" Text='<%# Eval("STATEMENT_NO") %>' />
                                <asp:Label ID="lblTourID" runat="server" Visible="false" Text='<%# Eval("TOUR_ID") %>' />
                                <asp:Label ID="lblTourNo" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                                <asp:Label ID="lblSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                                <asp:Label ID="lblCurrentStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                                <asp:Label ID="lblCurrentStatus" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                                <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                <asp:Label ID="lblTeamLeaderID" runat="server" Visible="false" Text='<%# Eval("TEAMLEADER_ID") %>' />
                                <asp:Label ID="lblAdvanceAmt" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMT") %>' />
                                <asp:Label ID="lblAdvanceCurrencyID" runat="server" Visible="false" Text='<%# Eval("ADVANCE_CURRENCY") %>' />
                                <asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNT") %>' />
                                <asp:Label ID="lblIsApprovalMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVAL_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsCheckedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CHECKED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsPassedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PASSED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsSettledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_SETTLED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDMENT_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedCehckedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_CHECKED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedPassedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_PASSED_MAIL_SENT") %>' />
                                <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                        <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                        <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                        <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                        <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                        <asp:BoundField DataField="VISIT_TYPE" HeaderText="VISIT_TYPE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="AIRFARE_AMT" HeaderText="AIRFARE_AMT" />
                        <asp:BoundField DataField="TELEPHONE_MOBILE_AMT" HeaderText="TELEPHONE_MOBILE_AMT" />
                        <asp:BoundField DataField="LODGING_AMT" HeaderText="LODGING_AMT" />
                        <asp:BoundField DataField="TIPS_AMT" HeaderText="TIPS_AMT" />
                        <asp:BoundField DataField="MEALS_AMT" HeaderText="MEALS_AMT" />
                        <asp:BoundField DataField="VISAFEE_AMT" HeaderText="VISAFEE_AMT" />
                        <asp:BoundField DataField="GROUND_TRANSPORT_AMT" HeaderText="GROUND_TRANSPORT_AMT" />
                        <asp:BoundField DataField="DAILY_ALLOWANCE_AMT" HeaderText="DAILY_ALLOWANCE_AMT" />
                        <asp:BoundField DataField="ENTERTAINMENT_AMT" HeaderText="ENTERTAINMENT_AMT" />
                        <asp:BoundField DataField="OTHER_AMT" HeaderText="OTHER_AMT" />
                        <asp:BoundField DataField="GIFTS_AMT" HeaderText="GIFTS_AMT" />
                        <asp:BoundField DataField="TOTAL_AMT" HeaderText="TOTAL_AMT" />
                        <asp:BoundField DataField="TOTAL_CURRENCY" HeaderText="TOTAL_CURRENCY" />
                        <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_AMT" />
                        <asp:BoundField DataField="ADV_CURRENCY" HeaderText="ADV_CURRENCY" />
                        <asp:BoundField DataField="ADJUSTED_AMT" HeaderText="ADJUSTED_AMT" />
                        <asp:BoundField DataField="ADJUSTED_AMT_CURRENCY" HeaderText="ADJUSTED_AMT_CURRENCY" />
                        <asp:BoundField DataField="IS_COST_RECOVERABLE" HeaderText="IS_COST_RECOVERABLE" />
                        <asp:BoundField DataField="DN_NO" HeaderText="DN_NO" />
                        <asp:BoundField DataField="AMOUNT_DATED" HeaderText="AMOUNT_DATED" />
                        <asp:BoundField DataField="FINAL_AMT" HeaderText="FINAL_AMT" />
                        <asp:BoundField DataField="FINAL_AMT_CURRENCY" HeaderText="FINAL_AMT_CURRENCY" />
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="1000px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 23px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" />
            </legend>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td colspan="5" align="center">
                            <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
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
                        <td>&nbsp;
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
                        <td>&nbsp;
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
                        <td>&nbsp;
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
                        <td>&nbsp;
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
                        <td>&nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAttachments" runat="server" Visible="false">
                        <tr>
                            <td>Visit Report Summary Attached?:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVisitSummaryAttached" runat="server" Width="100%" Height="26px"
                                    onChange="return ValidateddlVisitSummaryAttached();" Enabled="true">
                                    <asp:ListItem Text="No" Value="0" />
                                    <asp:ListItem Text="Yes" Value="1" />
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Attachment1:
                            </td>
                            <td>
                                <asp:FileUpload ID="fileUploadVisitSummary1" runat="server" Enabled="false" Width="100%"
                                    Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td colspan="3">&nbsp;
                            </td>
                            <td>
                                <div id="divfileUploadVisitSummary1" style="display: none;">
                                    <asp:Label ID="lblfileUploadVisitSummary1" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Attachment2:
                            </td>
                            <td>
                                <asp:FileUpload ID="fileUploadVisitSummary2" runat="server" Enabled="false" Width="100%"
                                    Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary2();" />
                            </td>
                            <td></td>
                            <td>Attachment3:
                            </td>
                            <td>
                                <asp:FileUpload ID="fileUploadVisitSummary3" runat="server" Enabled="false" Width="100%"
                                    Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary3();" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <div id="divfileUploadVisitSummary2" style="display: none;">
                                    <asp:Label ID="lblfileUploadVisitSummary2" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td>
                                <div id="divfileUploadVisitSummary3" style="display: none;">
                                    <asp:Label ID="lblfileUploadVisitSummary3" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlViewAttachments" runat="server">
                        <tr>
                            <td>Attachment 1:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Attachment 2:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment2" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment2" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment2_Click" />
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
                            <td>Attachment 3:
                            </td>
                            <td style="width: 30%;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment3" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment3" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="2">
                            <b>Expense Statement</b>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Airfare:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAirfare" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                                        <asp:TextBox ID="txtMobile" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtMobileGL" runat="server" Width="100%" Enabled="false" Text="52" />
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
                        <td>Lodging:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtLodging" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                                        <asp:TextBox ID="txtTips" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtTipsGL" runat="server" Width="100%" Enabled="false" Text="54" />
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
                        <td>Meals:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtMeals" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                                        <asp:TextBox ID="txtVisaFee" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtVisaFeeGL" runat="server" Width="100%" Enabled="false" Text="56" />
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
                        <td>Ground Transport:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtGroundTransport" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                                        <asp:TextBox ID="txtDailyAllowance" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Entertainment:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtEntertainment" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
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
                                        <asp:TextBox ID="txtOther" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtOtherGL" runat="server" Width="100%" Enabled="false" Text="60" />
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
                        <td>Gifts:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtGifts" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:TextBox ID="txtGiftsGL" runat="server" Width="100%" Enabled="false" Text="50" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Total Submitted Bill Amount:
                        </td>
                        <td>
                            <table style="width: 100%; height: 22px;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtTotal" runat="server" Width="100%" Text="0.00" Enabled="false" />
                                        <asp:HiddenField ID="hdTotal" runat="server" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlTotalCurrency" Enabled="false" runat="server" Width="100%"
                                            Height="26px" />
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
                        <td>Advance Obtained:
                        </td>
                        <td>
                            <table style="width: 100%; height: 22px;">
                                <tr>
                                    <td style="width: 75%;">
                                        <asp:TextBox ID="txtAdvanceObtained" runat="server" Width="100%" Text="0.00" Enabled="false"
                                            onkeyup="checkDec(this);" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlAdvanceObtainedCurreny" runat="server" Width="100%" Height="26px"
                                            Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Tour Cost Recoverable From Client:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTourCostRecoverable" runat="server" Width="100%" Height="26px"
                                Enabled="false">
                                <asp:ListItem Text="No" Value="0" />
                                <asp:ListItem Text="Yes" Value="1" />
                            </asp:DropDownList>
                        </td>
                        <%--<td>Delta Amount:
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 75%;">                                        
                                        <asp:TextBox ID="txtDeltaAmount" runat="server" Width="100%" Text="0" Enabled="false" />
                                        <asp:HiddenField ID="hdAdjustmentAmt" runat="server" />
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:DropDownList ID="ddlAdjustmentAmtCurrency" runat="server" Width="100%" Height="26px"
                                            Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAcc" runat="server" Visible="false">
                        <tr>
                            <td>Adjusted Amount:</td>
                            <td>
                                <asp:TextBox ID="txtAdjustedAmount" runat="server" Width="100%" Visible="true"
                                    onkeypress="return inNumberKeyWithDecimal(this, event);" onkeyup="checkDec(this);"
                                    onpaste="return false" /></td>
                            <td>&nbsp;
                            </td>
                            <td>Final Amount:</td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:TextBox ID="txtFinalAmount" runat="server" Width="100%" Visible="true"
                                                Enabled="false" />
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="ddlFinalAmountCurrency" runat="server" Enabled="false" Width="100%"
                                                Height="26px" />
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
                            <td>Adjustment Type:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdjustmentAmtType" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>Dated:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtAccDated" runat="server" ReadOnly="true" Enabled="false" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdAccDated" runat="server" />
                                            <ajax:CalendarExtender ID="calendarAccDated" PopupButtonID="imgBtnAccDated" runat="server"
                                                TargetControlID="txtAccDated" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedAccDated">
                                            </ajax:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnAccDated" runat="server" Enabled="false" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Calendar" />
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
                            <td>DN No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDNNo" runat="server" Width="100%" Visible="true" />
                            </td>
                            <td>&nbsp;</td>
                            <td>DN Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDNDate" runat="server" ReadOnly="true" Enabled="false" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdDNDate" runat="server" />
                                            <ajax:CalendarExtender ID="calendarDNDate" PopupButtonID="imgBtnDNDate" runat="server"
                                                TargetControlID="txtDNDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedDNDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnDNDate" runat="server" Enabled="false" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Calendar" />
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
                            <td>Voucher No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtVoucherNo" runat="server" Width="100%" Visible="true" />
                            </td>
                            <td>&nbsp;</td>
                            <td>Voucher Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtVoucherDate" runat="server" ReadOnly="true" Enabled="false" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdVoucherDate" runat="server" />
                                            <ajax:CalendarExtender ID="calendarVoucherDate" PopupButtonID="imgBtnVoucherDate" runat="server"
                                                TargetControlID="txtVoucherDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedVoucherDate">
                                            </ajax:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnVoucherDate" runat="server" Enabled="false" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Calendar" />
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
                            <td>Voucher Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVoucherType" runat="server" Width="100%" Height="26px">
                                </asp:DropDownList>
                            </td>
                            <%--<td>&nbsp;
                        </td>
                        <td>Adjustment Type:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdjustmentAmtType" runat="server" Width="100%" Enabled="false" />
                        </td>--%>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlTravellerRemarks" runat="server" Visible="false">
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
                    <asp:Panel ID="pnlApprovedRemarks" runat="server" Visible="false">
                        <tr>
                            <td>HOD Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtApprovedRemarks" runat="server" Width="100%" Enabled="false"
                                    TextMode="MultiLine" Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlCheckRemarks" runat="server" Visible="false">
                        <tr>
                            <td>Check Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCheckRemarks" runat="server" Width="100%" Enabled="false" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlAccPassAmdRemarks" runat="server" Visible="false">
                        <tr>
                            <td>Pass/Amendment Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAccPassAmdRemarks" runat="server" Width="100%" Enabled="false"
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
                                Rows="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="center">
                            <asp:Panel ID="pnlExceptionMsg" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="lblExceptionMsg" runat="server" Font-Bold="True" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                                OnClick="btnSubmit_Click" Width="100%" />
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
                            <asp:Button ID="btnAmendment" OnClick="btnAmendment_Click" CssClass="button" runat="server"
                                OnClientClick="return ValidateAll();" Text="Send For Amendment" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
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
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewTravelStatementInPDF"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
