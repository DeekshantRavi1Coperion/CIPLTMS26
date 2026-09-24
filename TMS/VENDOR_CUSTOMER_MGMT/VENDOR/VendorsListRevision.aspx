<%--<%@ Page Title="CIPLTMS-Transmittal To Factory List" Language="C#"
    MasterPageFile="~/HOME.master" AutoEventWireup="true"
    Page.MaintainScrollPositionOnPostBack ="true"
    EnableViewState="true" CodeFile="VendorsList.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_VENDOR_VendorsList" %>--%>

<%@ Page Title="CIPLTMS-Revise Vendors"
    Language="C#"
    MasterPageFile="~/HOME.master"
    AutoEventWireup="true"
    EnableViewState="true"
    CodeFile="VendorsListRevision.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_VENDOR_VendorsListRevision" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

        .textboxtstatustext {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxdrawings {
            padding: 5px 2px 5px 5px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightgreen;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxleft {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxcenter {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxright {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }



        .myGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .myGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .myGrid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .myGrid .alt {
                background-color: #EFEFEF;
            }

        .chkHorizontal td {
            padding-right: 25px;
        }



        .btn-add-show {
            border: 3px solid green;
            padding: 5px;
            display: inline-block;
            border-radius: 15px;
        }
    </style>

    <%--/*Supporting Functions=================================================================*/--%>

    <script type="text/javascript" language="javascript">

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function pageLoad() {
            GetRevisionFor();



            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
        }

        function clientChangedDateTimeofConfirmationContactdetailsToS(sender, args) {
            document.getElementById('<%=hdDateTimeofConfirmationContactdetailsToS.ClientID %>').value = document.getElementById('<%=txtDateTimeofConfirmationContactdetailsToS.ClientID %>').value;
            $find('<%= mpeUpdateLOT.ClientID %>').show();
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


        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }

        document.onkeypress = stopEnterKey;


        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);

    </script>


    <%--/*Validation Functions=================================================================*/--%>
    <script type="text/Javascript">

        /*Vendor Details=================================================================*/

        function ValidateRevisionsFor() {
            var chkList = document.getElementById('<%= chkListRevisionsForToS.ClientID %>');
            var checkboxes = chkList.getElementsByTagName('input');

            var isChecked = false;

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isChecked = true;
                    break;
                }
            }

            if (!isChecked) {
                chkList.style.border = "1px solid #F7627F";
                return true; // validation failed
            } else {
                chkList.style.border = "";
                return false; // validation passed
            }
        }




        function ValidateName() {
            var val = document.getElementById('<%=txtNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCode() {
            var val = document.getElementById('<%=txtCodeToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtCodeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCodeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidatePANNumber() {

            var country = document.getElementById('<%= ddlCountryToS.ClientID %>').value;
            var val = document.getElementById('<%=txtPANNumberToS.ClientID %>').value.toUpperCase();

            var panPattern = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
            if (country == '95') {
                if (val == '') {
                    document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    if (!panPattern.test(val)) {
                        alert("Invalid PAN Number");
                        document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "";
                        return false;
                    }
                }
            }

        }



        function ValidateCategory() {
            var val = document.getElementById('<%=ddlCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMSMEStatus() {
            var val = document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Billing Address=================================================================*/

        function ValidateGSTIn() {
            var PAN = document.getElementById('<%=txtPANNumberToS.ClientID %>').value.toUpperCase();
            var val = document.getElementById('<%=txtGSTInToS.ClientID %>').value.toUpperCase();



            var gstPattern = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;

            if (val == '') {
                <%--document.getElementById('<%=txtGSTInToS.ClientID %>').style.borderColor = "#F7627F";
                return true;--%>
                return false;
            }
            else {

                var PANText = val.substring(2, 12);

                if (!gstPattern.test(val)) {
                    alert("Invalid GSTIN");
                    return true;
                }
                else if (PANText != PAN) {
                    alert("PAN number does not match the PAN embedded in GSTIN.");
                    return true;
                }
                document.getElementById('<%=txtGSTInToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAddress1() {
            var val = document.getElementById('<%=txtAddress1ToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtAddress1ToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAddress1ToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCity() {
            var val = document.getElementById('<%=txtCityToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtCityToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCityToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        <%--function ValidateState() {
            var val = document.getElementById('<%=txtStateToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtStateToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtStateToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        <%--function ValidateCountry() {
            var val = document.getElementById('<%=txtCountryToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtCountryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCountryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>



        function ValidateCountry() {
            var val = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;
            if (val == '') {
                document.getElementById('<%=ddlCountryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateState() {
            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=ddlStateToS.ClientID %>').selectedIndex;

            if (countryId == '95') {
                if (val == '' || val == 0) {
                    document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOtherState() {
            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=txtOtherStateToS.ClientID %>').value;

            if (countryId != 95) {
                if (val == '') {
                    document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePhone() {
            var val = document.getElementById('<%=txtPhoneToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtPhoneToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPhoneToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmail() {
            var val = document.getElementById('<%=txtEmailToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtEmailToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmailToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Contact Person Details=================================================================*/

        function ValidateContactPersonName() {
            var val = document.getElementById('<%=txtContactPersonNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonMobile() {
            var val = document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonPhone() {
            var val = document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonEmail() {
            var val = document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Bank Details=================================================================*/

        function ValidateBankName() {
            var val = document.getElementById('<%=txtBankNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBankNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBankNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBranch() {
            var val = document.getElementById('<%=txtBranchToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBranchToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBranchToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSWIFTCode() {

            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').value;

            if (countryId != 95) {
                if (val == '') {
                    document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "";
                return false;
            }

        }

        function ValidateAccountNumber() {
            var val = document.getElementById('<%=txtAccountNumberToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtAccountNumberToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAccountNumberToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRTGSOrIFSC() {
            var val = document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateISBN() {
            var val = document.getElementById('<%=txtISBNToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtISBNToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtISBNToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Other Details=================================================================*/

        function ValidateResponsible() {
            var val = document.getElementById('<%=ddlResponsibleToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlResponsibleToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlResponsibleToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRelationType() {
            var val = document.getElementById('<%=ddlRelationTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlRelationTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRelationTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemCategory() {
            var val = document.getElementById('<%=ddlItemCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemSubCategory() {
            var val = document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Attachment=================================================================*/

        function ValidatefileAttachment() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachment.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachment");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachment.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        <%--function ValidatefileAttachmentVRF() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentVRF");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentVRF.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentGSTIN() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentGSTIN");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentGSTIN.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentPAN() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentPAN");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentPAN.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentCancelledCheque() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentCancelledCheque");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentCancelledCheque.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther1");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther1.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther2");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther2.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther3");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther3.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther4");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther4.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }--%>

    </script>


    <%--/*Apply Validation Functions===========================================================*/--%>
    <script type="text/Javascript">

        <%--function ValidateAll() {
            var check = true;

            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            if (ValidateGSTIn()) { check = false; }
            if (ValidateCategory()) { check = false; }
            if (ValidateMSMEStatus()) { check = false; }

            if (ValidateAddress1()) { check = false; }
            if (ValidateCity()) { check = false; }
            if (ValidateState()) { check = false; }
            if (ValidateCountry()) { check = false; }
            if (ValidateOtherState()) { check = false; }
            if (ValidatePhone()) { check = false; }
            if (ValidateEmail()) { check = false; }

            if (ValidateContactPersonName()) { check = false; }
            if (ValidateContactPersonMobile()) { check = false; }
            if (ValidateContactPersonPhone()) { check = false; }
            if (ValidateContactPersonEmail()) { check = false; }

            if (ValidateBankName()) { check = false; }
            if (ValidateBranch()) { check = false; }
            //if (ValidateSWIFTCode()) { check = false; }
            if (ValidateAccountNumber()) { check = false; }
            if (ValidateRTGSOrIFSC()) { check = false; }
            //if (ValidateISBN()) { check = false; }

            if (ValidateResponsible()) { check = false; }
            if (ValidateRelationType()) { check = false; }
            if (ValidateItemCategory()) { check = false; }
            if (ValidateItemSubCategory()) { check = false; }


            if (ValidatefileAttachmentVRF()) { check = false; }
            if (ValidatefileAttachmentGSTIN()) { check = false; }
            if (ValidatefileAttachmentPAN()) { check = false; }
            if (ValidatefileAttachmentCancelledCheque()) { check = false; }
            if (ValidatefileAttachmentOther1()) { check = false; }
            if (ValidatefileAttachmentOther2()) { check = false; }
            if (ValidatefileAttachmentOther3()) { check = false; }
            if (ValidatefileAttachmentOther4()) { check = false; }

            if (check) {
                if (confirm("Would you like to save Vendor?")) {
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

            return check;
        }--%>



        function ValidateBillingAddress() {
            var check = true;

            if (ValidateGSTIn()) { check = false; }
            if (ValidateAddress1()) { check = false; }
            if (ValidateCity()) { check = false; }
            if (ValidateState()) { check = false; }
            if (ValidateCountry()) { check = false; }
            if (ValidateOtherState()) { check = false; }
            if (ValidatePhone()) { check = false; }
            if (ValidateEmail()) { check = false; }

            if (check) {

                return true;
            }
            else {
                return false;
            }

            return check;
        }

        function ValidateBillingAddressGrid() {

            document.getElementById('<%= lblBillingAddressMsg.ClientID %>').innerText = ''
            var check = false;
            var grid = document.getElementById('<%= gvBillingAddress.ClientID %>');
            if (grid != null) {

                var rows = grid.getElementsByTagName('tr');
                if (rows.length > 1) {
                    check = true;
                }
            }

            if (check) {
                return true;
            }
            else {
                return false;
                document.getElementById('<%= lblBillingAddressMsg.ClientID %>').innerText = 'Please add at least one billing address.'
            }

            return false;
        }


        function ValidateContactPerson() {
            var check = true;

            if (ValidateContactPersonName()) { check = false; }
            if (ValidateContactPersonMobile()) { check = false; }
            if (ValidateContactPersonPhone()) { check = false; }
            if (ValidateContactPersonEmail()) { check = false; }

            if (check) {
                return true;
            }
            else {
                return false;
            }

            return check;
        }

        function ValidateContactPersonGrid() {

            displayNoneAllPanels();

            document.getElementById('<%= lblContactPersonMsg.ClientID %>').innerText = ''
            var check = false;
            var grid = document.getElementById('<%= gvContactPerson.ClientID %>');
            if (grid != null) {

                var rows = grid.getElementsByTagName('tr');
                if (rows.length > 1) {
                    check = true;
                }
            }

            if (check) {
                return true;
            }
            else {
                return false;
                document.getElementById('<%= lblContactPersonMsg.ClientID %>').innerText = 'Please add at least one contact person.'
            }

            return false;
        }




        function ValidateAllAttachment() {
            var check = true;

            if (ValidatefileAttachment()) { check = false; }

            //if (ValidatefileAttachmentVRF()) { check = false; }
            //if (ValidatefileAttachmentGSTIN()) { check = false; }
            //if (ValidatefileAttachmentPAN()) { check = false; }
            //if (ValidatefileAttachmentCancelledCheque()) { check = false; }
            //if (ValidatefileAttachmentOther1()) { check = false; }
            //if (ValidatefileAttachmentOther2()) { check = false; }
            //if (ValidatefileAttachmentOther3()) { check = false; }
            //if (ValidatefileAttachmentOther4()) { check = false; }

            return check;
        }



        function ValidateAll() {
            var check = true;

            if (ValidateRevisionsFor()) { check = false; }
            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            if (ValidateCategory()) { check = false; }

            if (ValidateBankName()) { check = false; }
            if (ValidateBranch()) { check = false; }
            if (ValidateAccountNumber()) { check = false; }

            if (ValidateResponsible()) { check = false; }
            if (ValidateRelationType()) { check = false; }
            if (ValidateItemCategory()) { check = false; }
            if (ValidateItemSubCategory()) { check = false; }

            if (check) {
                if (confirm("Would you like to save Vendor?")) {
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

            return check;
        }




        function ValidateCodeAll() {
            var check = true;

            if (ValidateCode()) { check = false; }

            if (check) {
                if (confirm("Would you like to register vendor in FACT?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }







    </script>


    <%--/*Paneling Functions===================================================================*/--%>
    <script type="text/Javascript">

        function ShowBillingAddressPopup() {

            var addressCounts= document.getElementById('<%= hdBillingAddressCounts.ClientID %>').value;

            if (addressCounts >= 1) {
                document.getElementById('<%= chkIsDefaultBillingAddressToS.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%= chkIsDefaultBillingAddressToS.ClientID %>').disabled = true;
            }

            var panNo = document.getElementById('<%= txtPANNumberToS.ClientID %>').value;

            if (panNo != '') {
                document.getElementById('<%= txtPANToSS.ClientID %>').value = panNo;

                //document.getElementById('<%= ddlCountryToS.ClientID %>').value = "95";

                document.getElementById('<%= hdBillingAddressUpdationFlag.ClientID %>').value = '0';
                document.getElementById('<%= btnAddUpdateAddressToList.ClientID %>').value = 'Add Address';
                $find('<%= mpeAddBillingAddress.ClientID %>').show();
            }
            else {
                alert('Please enter PAN No. !')
            }


        }

        function ShowContactPersonPopup() {
            document.getElementById('<%= hdContactPersonUpdationFlag.ClientID %>').value = '0';
            document.getElementById('<%= btnAddUpdateContactPersonToList.ClientID %>').value = 'Add Contact Person';
            $find('<%= mpeAddContactPerson.ClientID %>').show();
        }

        function GenerateGSTIN() {

            var panNo = document.getElementById('<%= txtPANToSS.ClientID %>').value.trim();
            var runningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>').value.trim();

            var ddlState = document.getElementById('<%= ddlStateToS.ClientID %>');
            var state = ddlState.options[ddlState.selectedIndex].text;

            var stateCode = state.substring(state.indexOf('[') + 1, state.indexOf(']'));

            if (!stateCode || !panNo || !runningNo) {
                return;
            }

            document.getElementById('<%= txtGSTInToS.ClientID %>').value = stateCode + panNo + runningNo;
        }

        function FillPANNo() {
            var country = document.getElementById('<%= ddlCountryToS.ClientID %>').value;
            if (country == '95') {
                document.getElementById('<%= ddlStateToS.ClientID %>').disabled = false;

                var otherState = document.getElementById('<%= txtOtherStateToS.ClientID %>');
                otherState.disabled = true;
                otherState.value = '';


                var GSTInRunningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>');
                GSTInRunningNo.disabled = false;
                GSTInRunningNo.value = '';

                var GSTIn = document.getElementById('<%= txtGSTInToS.ClientID %>');
                GSTIn.disabled = false;
                GSTIn.value = '';



                document.getElementById('<%= txtPANNumberToS.ClientID %>').value = '';
            }
            else {
                var ddlState = document.getElementById('<%= ddlStateToS.ClientID %>');
                ddlState.disabled = true;
                ddlState.selectedIndex = 0; // Optional: reset selection

                var otherState = document.getElementById('<%= txtOtherStateToS.ClientID %>');
                otherState.disabled = false;
                otherState.value = '';


                var GSTInRunningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>');
                GSTInRunningNo.disabled = true;
                GSTInRunningNo.value = '';

                var GSTIn = document.getElementById('<%= txtGSTInToS.ClientID %>');
                GSTIn.disabled = true;
                GSTIn.value = '';



                document.getElementById('<%= txtPANNumberToS.ClientID %>').value = 'PANNOTAVBL';
            }
        }


        function ResetFillingControls() {

            toggleBillingAddressActions(false);
            toggleContactPersonActions(false);
            toggleBankDetailsActions(false);

            document.getElementById('<%= imgBtnShowPopupAddBillingAddress.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddBillingAddress").classList.remove('btn-add-show');

            document.getElementById('<%= imgBtnShowPopupAddContactPerson.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddContactPerson").classList.remove('btn-add-show');

            <%--document.getElementById('<%= imgBtnShowPopupAddBankDetails.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddBankDetails").classList.remove('btn-add-show');--%>


            document.getElementById('<%= txtCreditDaysToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtCreditLimitToS.ClientID %>').disabled = true;
            document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').disabled = true;


            document.getElementById('<%= txtBankNameToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtBranchToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtRTGSOrIFSCToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtAccountNumberToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtSWIFTCodeToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtISBNToS.ClientID %>').disabled = true;

        }


        function HoldCreditLimit(txt) {
            document.getElementById('<%= hdCreditLimitToS.ClientID %>').value = txt.value;
        }

        function HoldCreditDays(txt) {
            document.getElementById('<%= hdCreditDaysToS.ClientID %>').value = txt.value;
        }

        function HoldMSMEStatus(txt) {
            document.getElementById('<%= hdMSMEStatusToS.ClientID %>').value = txt.value;
        }



        function GetRevisionFor() {



            document.getElementById('<%= hdEditBillingAddressOrGST.ClientID %>').value = "0";
            document.getElementById('<%= hdEditContactPerson.ClientID %>').value = "0";
            document.getElementById('<%= hdEditBankDetails.ClientID %>').value = "0";

            toggleBillingAddressActions(false);
            toggleContactPersonActions(false);
            toggleBankDetailsActions(false);

            document.getElementById('<%= imgBtnShowPopupAddBillingAddress.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddBillingAddress").classList.remove('btn-add-show');

            document.getElementById('<%= imgBtnShowPopupAddContactPerson.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddContactPerson").classList.remove('btn-add-show');

            <%--document.getElementById('<%= imgBtnShowPopupAddBankDetails.ClientID %>').disabled = true;
            document.getElementById("divShowPopupAddBankDetails").classList.remove('btn-add-show');--%>

            document.getElementById('<%= txtCreditDaysToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtCreditLimitToS.ClientID %>').disabled = true;
            document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').disabled = true;


            <%--document.getElementById('<%= txtCreditDaysToS.ClientID %>').value = document.getElementById('<%= hdCreditDaysToS.ClientID %>').value;
            document.getElementById('<%= txtCreditLimitToS.ClientID %>').value = document.getElementById('<%= hdCreditLimitToS.ClientID %>').value;
            document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').value = document.getElementById('<%= hdMSMEStatusToS.ClientID %>').value;--%>



            document.getElementById("dvDeclarationCD").style.display = "none";
            //document.getElementById("dvDeclarationBD").style.display = "none";


            var chkList = document.getElementById('<%= chkListRevisionsForToS.ClientID %>');
            var checkboxes = chkList.getElementsByTagName('input');

            for (var i = 0; i < checkboxes.length; i++) {

                if (i == 0 || i == 2) {
                    if (checkboxes[i].checked) {
                        document.getElementById('<%= imgBtnShowPopupAddBillingAddress.ClientID %>').disabled = false;
                        document.getElementById("divShowPopupAddBillingAddress").classList.add('btn-add-show');
                    }
                }

                if (i == 1 || i == 3) {
                    if (checkboxes[i].checked) {
                        document.getElementById('<%= hdEditBillingAddressOrGST.ClientID %>').value = i;
                        toggleBillingAddressActions(true);
                    }
                }

                if (i == 4) {
                    if (checkboxes[i].checked) {
                        document.getElementById("dvDeclarationCD").style.display = "block";
                        document.getElementById('<%= imgBtnShowPopupAddContactPerson.ClientID %>').disabled = false;
                        document.getElementById("divShowPopupAddContactPerson").classList.add('btn-add-show');
                    }
                }

                if (i == 5) {
                    if (checkboxes[i].checked) {
                        //document.getElementById("dvDeclarationBD").style.display = "block";
                        document.getElementById('<%= hdEditBankDetails.ClientID %>').value = i;
                        toggleBankDetailsActions(true);
                    }
                }

                if (i == 6) {
                    if (checkboxes[i].checked) {
                        document.getElementById('<%= txtCreditDaysToS.ClientID %>').disabled = false;
                        document.getElementById('<%= txtCreditLimitToS.ClientID %>').disabled = false;

                        document.getElementById('<%= txtCreditDaysToS.ClientID %>').value = document.getElementById('<%= hdCreditDaysToS.ClientID %>').value;
                        document.getElementById('<%= txtCreditLimitToS.ClientID %>').value = document.getElementById('<%= hdCreditLimitToS.ClientID %>').value;
                    }
                    else {
                        document.getElementById('<%= txtCreditDaysToS.ClientID %>').value = "0";
                        document.getElementById('<%= txtCreditLimitToS.ClientID %>').value = "0.00";
                    }
                }

                if (i == 7) {
                    if (checkboxes[i].checked) {
                        document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').disabled = false;
                        document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').value = document.getElementById('<%= hdMSMEStatusToS.ClientID %>').value;
                    }
                    else {
                        document.getElementById('<%= ddlMSMEStatusToS.ClientID %>').value = 0;
                    }
                }
            }
        }


        function toggleBillingAddressActions(show) {

            var val = document.getElementById('<%= hdEditBillingAddressOrGST.ClientID %>').value;
            if (parseInt(val) > 0) show = true;

            var grid = document.getElementById('<%= gvBillingAddress.ClientID %>');

            if (!grid) return;

            for (var i = 0; i < grid.rows.length; i++) {
                grid.rows[i].cells[0].style.display = show ? '' : 'none';
            }
        }

        function toggleContactPersonActions(show) {

            var val = document.getElementById('<%= hdEditContactPerson.ClientID %>').value;
            if (parseInt(val) > 0) show = true;

            var grid = document.getElementById('<%= gvContactPerson.ClientID %>');

            if (!grid) return;

            for (var i = 0; i < grid.rows.length; i++) {
                grid.rows[i].cells[0].style.display = show ? '' : 'none';
            }
        }

        function toggleBankDetailsActions(show) {

            document.getElementById('<%= txtBankNameToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtBranchToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtRTGSOrIFSCToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtAccountNumberToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtSWIFTCodeToS.ClientID %>').disabled = true;
            document.getElementById('<%= txtISBNToS.ClientID %>').disabled = true;


            document.getElementById('<%= txtBankNameToS.ClientID %>').value = document.getElementById('<%= hdBankNameToS.ClientID %>').value;
            document.getElementById('<%= txtBranchToS.ClientID %>').value = document.getElementById('<%= hdBranchToS.ClientID %>').value;
            document.getElementById('<%= txtRTGSOrIFSCToS.ClientID %>').value = document.getElementById('<%= hdRTGSOrIFSCToS.ClientID %>').value;
            document.getElementById('<%= txtAccountNumberToS.ClientID %>').value = document.getElementById('<%= hdAccountNumberToS.ClientID %>').value;
            document.getElementById('<%= txtSWIFTCodeToS.ClientID %>').value = document.getElementById('<%= hdSWIFTCodeToS.ClientID %>').value;
            document.getElementById('<%= txtISBNToS.ClientID %>').value = document.getElementById('<%= hdISBNToS.ClientID %>').value;


            if (show) {
                document.getElementById('<%= txtBankNameToS.ClientID %>').disabled = false;
                document.getElementById('<%= txtBranchToS.ClientID %>').disabled = false;
                document.getElementById('<%= txtRTGSOrIFSCToS.ClientID %>').disabled = false;
                document.getElementById('<%= txtAccountNumberToS.ClientID %>').disabled = false;
                document.getElementById('<%= txtSWIFTCodeToS.ClientID %>').disabled = false;
                document.getElementById('<%= txtISBNToS.ClientID %>').disabled = false;
            }
        }


    </script>




    <script type="text/javascript">

        window.addEventListener('load', function () {

            var div = document.getElementById("scrollDiv");

            // Restore scroll position
            var scrollPos = sessionStorage.getItem("divScroll");

            if (scrollPos !== null) {
                div.scrollTop = scrollPos;
            }

            // Save scroll position
            div.onscroll = function () {
                sessionStorage.setItem("divScroll", div.scrollTop);
            };

        });

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>

    <asp:HiddenField ID="hfScroll" runat="server" />

    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdIsRevised" runat="server" Value="0" />
    <asp:HiddenField ID="hdIsEdited" runat="server" Value="0" />

    <asp:HiddenField ID="hdEditBillingAddressOrGST" runat="server" Value="0" />
    <asp:HiddenField ID="hdEditContactPerson" runat="server" Value="0" />
    <asp:HiddenField ID="hdEditBankDetails" runat="server" Value="0" />

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Vouchers List For Revision:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" /></td>
                            <td align="right">
                                <asp:CheckBox ID="chkClearDates" runat="server"
                                    AutoPostBack="true"
                                    ToolTip="Clear dates"
                                    OnCheckedChanged="chkClearDates_CheckedChanged" />

                                <%--OnChange="ClearDates();" --%>
                            </td>
                        </tr>
                    </table>


                    <div class="full-width">
                        <label>Vendor Code/Name:</label>
                        <table>
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtVendorCodeS" runat="server"
                                        CssClass="form-control" />
                                </td>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtVendorNameS" runat="server"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatusS" runat="server" Enabled="false"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Responsible:</label>
                    <asp:DropDownList ID="ddlResponsibleS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Type:</label>
                    <asp:DropDownList ID="ddlRelationTypeS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>PAN:</label>
                    <asp:TextBox ID="txtPANS" runat="server"
                        CssClass="form-control" />

                    <label>Category:</label>
                    <asp:DropDownList ID="ddlCategoryS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Organization Type:</label>
                    <asp:DropDownList ID="ddlOrganizationTypeS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <asp:CheckBox ID="chkOnlyFACTVendors" runat="server" Text="FACT Vendprs" Visible="false" />

                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" runat="server"
                    Text="Search" Width="100%" OnClick="btnSearch_Click" />
            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView ID="gvVendorsList"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                CssClass="employee-grid"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvVendorsList_RowCommand"
                OnRowDataBound="gvVendorsList_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />

                <Columns>

                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="30px" Width="30px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/VCM/pdf.png" ToolTip="View Vedor Detail in PDF" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDownloadDOCs" Height="30px" Width="30px" CommandArgument="DOWNLOAD_DOCS"
                                    runat="server" ImageUrl="~/Images/VCM/download.png" ToolTip="Download DOCs" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnRevise" Height="30px" Width="30px" CommandArgument="REVISE"
                                runat="server" ImageUrl="~/Images/VCM/revise.png" ToolTip="Revise Vendor" />

                            <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />
                            <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("NAME") %>' />
                            <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("CODE") %>' />
                            <asp:Label ID="lblStatusId" runat="server" Visible="false" Text='<%# Eval("STATUS_FID") %>' />
                            <asp:Label ID="lblRequestedBy" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                            <%--<asp:Label ID="lblIsRequestedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_REQUESTED_MAIL_SENT") %>' />
                                <asp:Label ID="lblHodApprovedBy" runat="server" Visible="false" Text='<%# Eval("HOD_APPROVED_BY") %>' />
                                <asp:Label ID="lblIsHodApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_HOD_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblAccountsLevel1ApprovedBy" runat="server" Visible="false" Text='<%# Eval("ACCOUNTS_LEVEL1_APPROVED_BY") %>' />
                                <asp:Label ID="lblIsAccountsLevel1ApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ACCOUNTS_LEVEL1_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblAccountsLevel2ApprovedBy" runat="server" Visible="false" Text='<%# Eval("ACCOUNTS_LEVEL2_APPROVED_BY") %>' />
                                <asp:Label ID="lblIsAccountsLevel2ApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ACCOUNTS_LEVEL2_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblRegisteredBy" runat="server" Visible="false" Text='<%# Eval("REGISTERED_BY") %>' />
                                <asp:Label ID="lblIsRegisteredMailSent" runat="server" Visible="false" Text='<%# Eval("IS_REGISTERED_MAIL_SENT") %>' />--%>

                            <%--<asp:Label ID="lblRevisedBy" runat="server" Visible="false" Text='<%# Eval("REVISED_BY") %>' />--%>
                            <%--<asp:Label ID="lblIsRevisedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_REVISED_MAIL_SENT") %>' />--%>

                            <%--<asp:Label ID="lblIsAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDMENT_MAIL_SENT") %>' />--%>
                            <%--<asp:Label ID="lblIsAmendedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' />--%>
                            <%--<asp:Label ID="lblIsMSMED" runat="server" Visible="false" Text='<%# Eval("IS_MSMED") %>' />--%>
                            <asp:Label ID="lblIsISOCertified" runat="server" Visible="false" Text='<%# Eval("IS_ISO_CERTIFIED") %>' />
                            <asp:Label ID="lblIsTechnicalDetailsReceived" runat="server" Visible="false" Text='<%# Eval("IS_TECHNICAL_DETAILS_RECEIVED") %>' />
                            <asp:Label ID="lblIsVisitByQa" runat="server" Visible="false" Text='<%# Eval("IS_VISIT_BY_QA") %>' />
                            <asp:Label ID="lblIsGovernment" runat="server" Visible="false" Text='<%# Eval("IS_GOVERNMENT") %>' />
                            <%--<asp:Label ID="lblIsSentForApproval" runat="server" Visible="false" Text='<%# Eval("IS_SENT_FOR_APPROVAL") %>' />--%>
                            <asp:Label ID="lblIsOneTime" runat="server" Visible="false" Text='<%# Eval("IS_ONE_TIME") %>' />

                            <asp:Label ID="lblPIDVRF" runat="server" Visible="false" Text='<%# Eval("PID_VRF") %>' />
                            <asp:Label ID="lblPIDGSTIN" runat="server" Visible="false" Text='<%# Eval("PID_GSTIN") %>' />
                            <asp:Label ID="lblPIDPAN" runat="server" Visible="false" Text='<%# Eval("PID_PAN") %>' />
                            <asp:Label ID="lblPIDCC" runat="server" Visible="false" Text='<%# Eval("PID_CANCELLED_CHEQUE") %>' />
                            <asp:Label ID="lblPIDOther1" runat="server" Visible="false" Text='<%# Eval("PID_OTHER1") %>' />
                            <asp:Label ID="lblPIDOther2" runat="server" Visible="false" Text='<%# Eval("PID_OTHER2") %>' />
                            <asp:Label ID="lblPIDOther3" runat="server" Visible="false" Text='<%# Eval("PID_OTHER3") %>' />
                            <asp:Label ID="lblPIDOther4" runat="server" Visible="false" Text='<%# Eval("PID_OTHER4") %>' />

                            <asp:Label ID="lblFileNameVRF" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_VRF") %>' />
                            <asp:Label ID="lblFileNameGSTIN" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_GSTIN") %>' />
                            <asp:Label ID="lblFileNamePAN" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_PAN") %>' />
                            <asp:Label ID="lblFileNameCC" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_CANCELLED_CHEQUE") %>' />
                            <asp:Label ID="lblFileNameOther1" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_OTHER1") %>' />
                            <asp:Label ID="lblFileNameOther2" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_OTHER2") %>' />
                            <asp:Label ID="lblFileNameOther3" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_OTHER3") %>' />
                            <asp:Label ID="lblFileNameOther4" runat="server" Visible="false" Text='<%# Eval("FILE_NAME_OTHER4") %>' />

                            <%--<asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNTS") %>' />--%>

                            <asp:Label ID="lblAddressCount" runat="server" Visible="false" Text='<%# Eval("ADDRESS_COUNTS") %>' />
                            <asp:Label ID="lblContactPersonCount" runat="server" Visible="false" Text='<%# Eval("CONTACT_PERSON_COUNTS") %>' />
                            <asp:Label ID="lblBankDetailsCount" runat="server" Visible="false" Text='<%# Eval("BANK_DETAILS_COUNTS") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="NAME" HeaderText="Name" />
                    <asp:BoundField DataField="CODE" HeaderText="Code" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:BoundField DataField="CATEGORY" HeaderText="Category" />
                    <%--<asp:BoundField DataField="GSTIN" HeaderText="Gstin" />--%>
                    <asp:BoundField DataField="PAN" HeaderText="Pan" />
                    <asp:BoundField DataField="CREDIT_DAYS" HeaderText="Credit Days" />
                    <asp:BoundField DataField="CREDIT_LIMIT" HeaderText="Credit Limit" />

                    <asp:TemplateField HeaderText="Is Iso Certified" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsIsoCertified" runat="server" Height="20px" Width="20px" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is Technical Details Received" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsTechnicalDetailsReceived" runat="server" Height="20px" Width="20px" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is Visit By Qa" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsVisitByQa" runat="server" Height="20px" Width="20px" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is Government" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsGovernment" runat="server" Height="20px" Width="20px" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is One Time" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsOneTime" runat="server" Height="20px" Width="20px" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="RESPONSIBLE" HeaderText="Responsible" />
                    <asp:BoundField DataField="VENDOR_TYPE" HeaderText="Vendor Type" />

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgFACT" runat="server" Height="30px" Width="30px" Enabled="false" Visible="false"
                                ImageUrl="~/Images/VCM/fact.png" ToolTip="FACT Vendor" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

        </div>

    </div>




    <%-- EDIT/UPDATE STATUS START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdateLOT" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Vendor Details</legend>

                <div class="form-grid form-grid-2">

                    <div class="full-width">

                        <fieldset class="form-card">
                            <legend>Revision For</legend>

                            <div class="full-width">

                                <asp:CheckBoxList
                                    ID="chkListRevisionsForToS"
                                    runat="server"
                                    RepeatDirection="Horizontal"
                                    RepeatColumns="4"
                                    CssClass="chkHorizontal"
                                    onchange="GetRevisionFor();">
                                </asp:CheckBoxList>

                            </div>

                        </fieldset>
                    </div>

                    <div class="full-width">

                        <fieldset class="form-card">
                            <legend>Basic Details</legend>

                            <div class="form-grid form-grid-2">

                                <div class="full-width">
                                    <label>Name:</label>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 70%">
                                                <asp:TextBox ID="txtNameToS" runat="server" CssClass="form-control" />
                                            </td>

                                            <td style="width: 30%">
                                                <asp:TextBox ID="txtCodeToS" runat="server" CssClass="form-control"
                                                    Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <label>Country:</label>
                                <asp:DropDownList ID="ddlCountryToS" runat="server"
                                    CssClass="form-control"
                                    onchange="FillPANNo();">
                                </asp:DropDownList>

                                <label>PAN Number:</label>
                                <asp:TextBox ID="txtPANNumberToS" runat="server"
                                    CssClass="form-control" />


                                <label>Credit Days:</label>
                                <asp:HiddenField ID="hdCreditDaysToS" runat="server" />
                                <asp:TextBox ID="txtCreditDaysToS" runat="server"
                                    CssClass="form-control"
                                    Text="0"
                                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                                    onchange="HoldCreditDays(this);" />


                                <label>Credit Limit:</label>
                                <asp:HiddenField ID="hdCreditLimitToS" runat="server" />
                                <asp:TextBox ID="txtCreditLimitToS" runat="server"
                                    CssClass="form-control"
                                    Text="0"
                                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                                    onchange="HoldCreditLimit(this);" />

                                <label>Category:</label>
                                <asp:DropDownList ID="ddlCategoryToS" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>

                                <label>MSME Status:</label>
                                <asp:HiddenField ID="hdMSMEStatusToS" runat="server" Value="0" />
                                <asp:DropDownList ID="ddlMSMEStatusToS" runat="server"
                                    CssClass="form-control"
                                    onchange="HoldMSMEStatus(this);">
                                </asp:DropDownList>

                                <div class="full-width button-group">
                                    <label>Do you have any relation work in our company as employee?</label>
                                    <asp:RadioButtonList ID="rdAssociatedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:RadioButtonList>

                                    <label>Is One Time Vendor?</label>
                                    <asp:RadioButtonList ID="rdIsOneTimeVendorToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>


                            </div>
                        </fieldset>
                    </div>

                    <br />

                    <div class="full-width">

                        <asp:HiddenField ID="hdBillingAddressSRNo" runat="server" />
                        <asp:HiddenField ID="hdBillingAddressUpdationFlag" runat="server" />
                        <asp:HiddenField ID="hdBillingAddressLastSUID" runat="server" />
                        <asp:HiddenField ID="hdBillingAddressCounts" runat="server" />

                        <div class="employee-grid-container">

                            <fieldset class="filter-card">
                                <legend>Billing Address:
                                <asp:Label ID="lblBillingAddressRecords" runat="server" Text="[0]" />
                                </legend>

                                <div id="divShowPopupAddBillingAddress">
                                    <asp:ImageButton ImageUrl="~/Images/VCM/add.png"
                                        ID="imgBtnShowPopupAddBillingAddress"
                                        runat="server"
                                        OnClientClick="ShowBillingAddressPopup(); return false;"
                                        ToolTip="Add new billing address"
                                        Width="25px" Height="25px" />
                                </div>

                                <asp:GridView ID="gvBillingAddress"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CellPadding="4"
                                    CssClass="employee-grid"
                                    ForeColor="#333333" GridLines="Both" Width="100%"
                                    HorizontalAlign="Center"
                                    OnRowCommand="gvBillingAddress_RowCommand"
                                    OnRowDataBound="gvBillingAddress_RowDataBound">

                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />


                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server"
                                                    ToolTip="Edit" ImageUrl="~/Images/VCM/edit.png"
                                                    Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server"
                                                    ToolTip="Remove" ImageUrl="~/Images/VCM/delete.png"
                                                    Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                                                <asp:Label ID="lblGSTIN" runat="server" Text='<%# Eval("GSTIN") %>' Visible="false" />
                                                <asp:Label ID="lblAddress1" runat="server" Text='<%# Eval("ADDRESS_LINE1") %>' Visible="false" />
                                                <asp:Label ID="lblAddress2" runat="server" Text='<%# Eval("ADDRESS_LINE2") %>' Visible="false" />
                                                <asp:Label ID="lblAddress3" runat="server" Text='<%# Eval("ADDRESS_LINE3") %>' Visible="false" />
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>' Visible="false" />
                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE") %>' Visible="false" />
                                                <asp:Label ID="lblOtherState" runat="server" Text='<%# Eval("OTHER_STATE") %>' Visible="false" />
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>' Visible="false" />
                                                <asp:Label ID="lblPINCode" runat="server" Text='<%# Eval("PIN_CODE") %>' Visible="false" />
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("PHONE") %>' Visible="false" />
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAIL") %>' Visible="false" />
                                                <asp:Label ID="lblIsDefault" runat="server" Text='<%# Eval("IS_DEFAULT") %>' Visible="false" />

                                                <asp:Label ID="lblStateID" runat="server" Text='<%# Eval("STATE_ID") %>' Visible="false" />
                                                <asp:Label ID="lblCountryID" runat="server" Text='<%# Eval("COUNTRY_ID") %>' Visible="false" />

                                                <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="ADDRESS_LINE1" HeaderText="Address1" />
                                        <asp:BoundField DataField="ADDRESS_LINE2" HeaderText="Address2" />
                                        <asp:BoundField DataField="ADDRESS_LINE3" HeaderText="Address3" />
                                        <asp:BoundField DataField="CITY" HeaderText="City" />
                                        <asp:BoundField DataField="COUNTRY" HeaderText="Country" />
                                        <asp:BoundField DataField="STATE" HeaderText="State" />
                                        <asp:BoundField DataField="OTHER_STATE" HeaderText="Other State" />
                                        <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" />
                                        <asp:BoundField DataField="PIN_CODE" HeaderText="PIN Code" />

                                        <asp:BoundField DataField="PHONE" HeaderText="Phone" />
                                        <asp:BoundField DataField="EMAIL" HeaderText="Email" />


                                        <asp:TemplateField HeaderText="Is Default?">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

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

                        <div align="center">
                            <asp:Label ID="lblBillingAddressMsg" runat="server" ForeColor="Red" />
                        </div>

                    </div>

                    <br />

                    <div class="full-width">

                        <asp:HiddenField ID="hdContactPersonSRNo" runat="server" />
                        <asp:HiddenField ID="hdContactPersonUpdationFlag" runat="server" />
                        <asp:HiddenField ID="hdContactPersonLastSUID" runat="server" />

                        <div class="employee-grid-container">

                            <fieldset class="filter-card">
                                <legend>Contact Person Details:
                                <asp:Label ID="lblContactPersonRecords" runat="server" Text="[0]" />

                                </legend>

                                <div id="divShowPopupAddContactPerson">
                                    <asp:ImageButton ImageUrl="~/Images/VCM/add.png"
                                        ID="imgBtnShowPopupAddContactPerson"
                                        runat="server"
                                        OnClientClick="ShowContactPersonPopup(); return false;"
                                        ToolTip="Add new contact person"
                                        Width="25px" Height="25px" />
                                </div>

                                <asp:GridView ID="gvContactPerson"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CellPadding="4"
                                    CssClass="employee-grid"
                                    ForeColor="#333333" GridLines="Both" Width="100%"
                                    HorizontalAlign="Center"
                                    OnRowCommand="gvContactPerson_RowCommand"
                                    OnRowDataBound="gvContactPerson_RowDataBound">

                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />

                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES"
                                                    runat="server" ToolTip="Edit" ImageUrl="~/Images/VCM/edit.png"
                                                    Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server"
                                                    ToolTip="Remove" ImageUrl="~/Images/VCM/delete.png"
                                                    Width="30px" Height="30px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                                                <asp:Label ID="lblContactPersonName" runat="server" Text='<%# Eval("NAME") %>' Visible="false" />
                                                <asp:Label ID="lblContactPersonMobile" runat="server" Text='<%# Eval("MOBILE_NO") %>' Visible="false" />
                                                <asp:Label ID="lblContactPersonPhone" runat="server" Text='<%# Eval("PHONE") %>' Visible="false" />
                                                <asp:Label ID="lblContactPersonEmail" runat="server" Text='<%# Eval("EMAIL") %>' Visible="false" />
                                                <asp:Label ID="lblIsDefault" runat="server" Text='<%# Eval("IS_DEFAULT") %>' Visible="false" />


                                                <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="NAME" HeaderText="Name" />
                                        <asp:BoundField DataField="MOBILE_NO" HeaderText="Mobile No." />
                                        <asp:BoundField DataField="PHONE" HeaderText="Phone No." />
                                        <asp:BoundField DataField="EMAIL" HeaderText="Email" />

                                        <asp:TemplateField HeaderText="Is Default?">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

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

                        <div class="form-grid form-grid-2">
                            <div class="full-width">

                                <div id="dvDeclarationCD" style="display: none;">

                                    <div class="full-width">
                                        <h5><i>Declaration in case of change in Contact details</i></h5>
                                    </div>


                                    <label>Declaration:</label>
                                    <asp:TextBox ID="txtDeclarationContactdetailsToS" runat="server"
                                        CssClass="form-control"
                                        TextMode="MultiLine" Rows="6"
                                        Enabled="false">                                       
                                    </asp:TextBox>

                                    <div class="form-grid form-grid-2">

                                        <label>Name of Person Contacted:</label>
                                        <asp:TextBox ID="txtNameofPersonContactedContactdetailsToS" runat="server"
                                            CssClass="form-control" />

                                        <label>Contact Number:</label>
                                        <asp:TextBox ID="txtContactNumberContactdetailsToS" runat="server"
                                            CssClass="form-control" />

                                        <label>Date & Time of Confirmation</label>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDateTimeofConfirmationContactdetailsToS" runat="server" ReadOnly="true"
                                                        CssClass="form-control"></asp:TextBox>
                                                    <asp:HiddenField ID="hdDateTimeofConfirmationContactdetailsToS" runat="server" />
                                                    <ajax:CalendarExtender ID="calendarDateTimeofConfirmationContactdetailsToS"
                                                        PopupButtonID="imgbtnDateTimeofConfirmationContactdetailsToS"
                                                        runat="server" TargetControlID="txtDateTimeofConfirmationContactdetailsToS" Format="dd-MMM-yyyy"
                                                        OnClientDateSelectionChanged="clientChangedDateTimeofConfirmationContactdetailsToS">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgbtnDateTimeofConfirmationContactdetailsToS" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Date & Time of Confirmation" />
                                                </td>
                                            </tr>
                                        </table>

                                        <label>Contacted by</label>
                                        <asp:TextBox ID="txtContactedbyContactdetailsToS" runat="server"
                                            CssClass="form-control" />

                                    </div>
                                </div>

                            </div>
                        </div>

                        <div align="center">
                            <asp:Label ID="lblContactPersonMsg" runat="server" ForeColor="Red" />
                        </div>

                    </div>

                </div>

                <br />

                <div class="full-width">

                    <asp:HiddenField ID="hdBankDetailsPID" runat="server" />
                    <asp:HiddenField ID="hdBankNameToS" runat="server" />
                    <asp:HiddenField ID="hdBranchToS" runat="server" />
                    <asp:HiddenField ID="hdRTGSOrIFSCToS" runat="server" />
                    <asp:HiddenField ID="hdAccountNumberToS" runat="server" />
                    <asp:HiddenField ID="hdSWIFTCodeToS" runat="server" />
                    <asp:HiddenField ID="hdISBNToS" runat="server" />

                    <fieldset class="form-card">
                        <legend>Bank Details</legend>

                        <div class="form-grid form-grid-2">

                            <label>Bank Name:</label>
                            <asp:TextBox ID="txtBankNameToS" runat="server" CssClass="form-control" />

                            <label>Branch:</label>
                            <asp:TextBox ID="txtBranchToS" runat="server" CssClass="form-control" />


                            <label>RTGS/IFSC:</label>
                            <asp:TextBox ID="txtRTGSOrIFSCToS" runat="server" CssClass="form-control" />

                            <label>Account Number:</label>
                            <asp:TextBox ID="txtAccountNumberToS" runat="server" CssClass="form-control" />

                            <label>SWIFT Code:</label>
                            <asp:TextBox ID="txtSWIFTCodeToS" runat="server" CssClass="form-control" Enabled="false" />

                            <label>ISBN:</label>
                            <asp:TextBox ID="txtISBNToS" runat="server" CssClass="form-control" Enabled="false" />

                        </div>
                    </fieldset>

                </div>

                <br />

                <div class="full-width">

                    <fieldset class="form-card">
                        <legend>Other Details</legend>

                        <div class="form-grid form-grid-2">

                            <label>Is Technical Details Received?</label>
                            <asp:RadioButtonList ID="rdIsTechnicalDetailsReceivedToS" runat="server"
                                RepeatDirection="Horizontal" Width="30%">
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>

                            <label>Is ISO Certified?</label>
                            <asp:RadioButtonList ID="rdIsISOCertifiedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>

                            <label>Is QA Visited?</label>
                            <asp:RadioButtonList ID="rdIsQAVisitedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>

                            <label>Is Government?</label>
                            <asp:RadioButtonList ID="rdIsGovernmentToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                            </asp:RadioButtonList>

                            <label>Responsible:</label>
                            <asp:DropDownList ID="ddlResponsibleToS" runat="server" CssClass="form-control">
                            </asp:DropDownList>

                            <label>Type:</label>
                            <asp:DropDownList ID="ddlRelationTypeToS" runat="server" CssClass="form-control">
                            </asp:DropDownList>

                            <label>Item Category:</label>
                            <asp:DropDownList ID="ddlItemCategoryToS" runat="server"
                                CssClass="form-control"
                                OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <label>Item Sub-Category:</label>
                            <asp:DropDownList ID="ddlItemSubCategoryToS" runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>

                        </div>
                    </fieldset>

                </div>

                <br />

                <div class="full-width">

                    <div class="form-grid form-grid-2">

                        <div class="full-width button-group">

                            <label>Attachment Type:</label>
                            <asp:DropDownList ID="ddlAttachmentType" runat="server" CssClass="form-control"></asp:DropDownList>

                            <label>Brownse:</label>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fileUploadAttachment" runat="server"
                                            CssClass="form-control"
                                            BorderStyle="Groove" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divfileAttachment" style="display: none;">
                                            <asp:Label ID="lblfileAttachment" runat="server" ForeColor="Red" />
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <asp:ImageButton ImageUrl="~/Images/VCM/add.png" runat="server"
                                ID="btnAddUpdateAttachmentToList"
                                OnClick="btnAddUpdateAttachmentToList_Click"
                                OnClientClick="return ValidateAllAttachment();"
                                ToolTip="Add new attachment" Height="30px" Width="30px" />

                        </div>

                    </div>

                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>Attachments:
                                <asp:Label ID="lblAttachmentsRecords" runat="server" Text="[0]" /></legend>


                            <asp:GridView ID="gvAttachments"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="employee-grid"
                                ForeColor="#333333" GridLines="Both" Width="100%"
                                HorizontalAlign="Center"
                                OnRowCommand="gvAttachments_RowCommand"
                                OnRowDataBound="gvAttachments_RowDataBound">

                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />


                                <Columns>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                                ImageUrl="~/Images/VCM/delete.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgViewDOC" CommandArgument="VIEW_DOC"
                                                runat="server" ToolTip="View Attachment" ImageUrl="~/Images/VCM/pdf.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSrInNo" runat="server" Text='<%# Eval("SR_NO_IN") %>' Visible="false" />
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                            <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                                            <asp:Label ID="lblDOCTypeFID" runat="server" Text='<%# Eval("DOC_TYPE_FID") %>' Visible="false" />
                                            <asp:Label ID="lblDOCName" runat="server" Text='<%# Eval("DOC_NAME") %>' Visible="false" />

                                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:BoundField DataField="DOC_TYPE" HeaderText="Type" />
                                    <asp:BoundField DataField="DOC_NAME" HeaderText="Name" />

                                </Columns>
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>


                            <asp:Label ID="lblAttachmentsMsg" runat="server" ForeColor="Red" />


                        </fieldset>
                    </div>

                </div>

                <br />

                <div class="full-width">

                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>View Remarks</legend>

                            <asp:GridView ID="gvRemarks"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="employee-grid"
                                ForeColor="#333333" GridLines="Both" Width="100%"
                                HorizontalAlign="Center"
                                OnRowDataBound="gvRemarks_RowDataBound">

                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />

                                <Columns>
                                    <asp:BoundField DataField="SR_NO" HeaderText="Sr No." />
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="SENT_BY" HeaderText="Action By" />
                                    <asp:BoundField DataField="SENT_ON" HeaderText="Action On" />
                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

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

                <br />

                <div class="full-width">
                    <label>Remarks:</label>
                    <asp:TextBox ID="txtRemarksToS" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" />
                </div>

            </fieldset>

            <div class="full-width button-group">

                <label>Checker:</label>
                <asp:DropDownList ID="ddlCheckerToS" runat="server" CssClass="form-control">
                </asp:DropDownList>

                <asp:Button ID="btnReviseVendor" runat="server" Width="100%" Text="Save Vendor Revision" CssClass="button"
                    OnClick="btnReviseVendor_Click" OnClientClick="return ValidateAllRevision();" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlUpdateMsg" Visible="true" runat="server" Height="50px">
                    <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>
    <%-- EDIT/UPDATE STATUS END --%>




    <%--ADD BILLING ADDRESS START--%>
    <asp:Button ID="btnShowPopupAddBillingAddress" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddBillingAddress" runat="server" TargetControlID="btnShowPopupAddBillingAddress"
        PopupControlID="pnlPopupAddBillingAddress" CancelControlID="imgBtnCancelAddBillingAddress" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddBillingAddress" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddBillingAddress" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="HiddenField3" runat="server" />

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Billing Address</legend>

                <div class="form-grid form-grid-2">

                    <label>Address Line 1:</label>
                    <asp:TextBox ID="txtAddress1ToS" runat="server"
                        CssClass="form-control" />

                    <label>Address Line 2:</label>
                    <asp:TextBox ID="txtAddress2ToS" runat="server"
                        CssClass="form-control" />

                    <label>Address Line 3:</label>
                    <asp:TextBox ID="txtAddress3ToS" runat="server"
                        CssClass="form-control" />

                    <label>State:</label>
                    <asp:DropDownList ID="ddlStateToS" runat="server"
                        CssClass="form-control"
                        onchange="GenerateGSTIN();">
                    </asp:DropDownList>

                    <label>Other State:</label>
                    <asp:TextBox ID="txtOtherStateToS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>City:</label>
                    <asp:TextBox ID="txtCityToS" runat="server"
                        CssClass="form-control" />

                    <label>PIN Code:</label>
                    <asp:TextBox ID="txtPINCodeToS" runat="server"
                        CssClass="form-control" />

                    <label>Phone:</label>
                    <asp:TextBox ID="txtPhoneToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Email:</label>
                    <asp:TextBox ID="txtEmailToS" runat="server"
                        CssClass="form-control" />

                    <label>PAN:</label>
                    <asp:TextBox ID="txtPANToSS" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                    <label>GSTIn Running No.:</label>
                    <asp:TextBox ID="txtGSTInRunningNoToS" runat="server"
                        CssClass="form-control"
                        onkeyup="GenerateGSTIN();" />

                    <label>GSTIn:</label>
                    <asp:TextBox ID="txtGSTInToS" runat="server"
                        CssClass="form-control" />

                    <label>Is Default?:</label>
                    <asp:CheckBox ID="chkIsDefaultBillingAddressToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateAddressToList" CssClass="button" runat="server" Text="Add Address"
                    Width="100%" OnClick="btnAddUpdateAddressToList_Click"
                    OnClientClick="return ValidateBillingAddress();" />
            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Label ID="lblBillingAddressWindowMsg" runat="server" ForeColor="Red" />
                </div>
            </div>
        </div>



    </asp:Panel>
    <%--ADD BILLING ADDRESS END--%>


    <%--ADD CONTACT PERSON START--%>
    <asp:Button ID="btnShowPopupAddContactPerson" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddContactPerson" runat="server" TargetControlID="btnShowPopupAddContactPerson"
        PopupControlID="pnlPopupAddContactPerson" CancelControlID="imgBtnCancelAddContactPerson" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddContactPerson" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddContactPerson" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:HiddenField ID="HiddenField5" runat="server" />
        <asp:HiddenField ID="HiddenField6" runat="server" />

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Add Contact Person</legend>

                <div class="form-grid form-grid-2">

                    <label>Name:</label>
                    <asp:TextBox ID="txtContactPersonNameToS" runat="server"
                        CssClass="form-control" />

                    <label>Mobile:</label>
                    <asp:TextBox ID="txtContactPersonMobileToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Phone:</label>
                    <asp:TextBox ID="txtContactPersonPhoneToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Email:</label>
                    <asp:TextBox ID="txtContactPersonEmailToS" runat="server"
                        CssClass="form-control" />

                    <label>Is Default?:</label>
                    <asp:CheckBox ID="chkIsDefaultContactPersonToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateContactPersonToList" CssClass="button"
                    runat="server" Text="Add Contact Person"
                    Width="100%" OnClick="btnAddUpdateContactPersonToList_Click"
                    OnClientClick="return ValidateContactPerson();" />
            </div>

        </div>



    </asp:Panel>
    <%--ADD CONTACT PERSON END--%>


    <%-- SHOW IMAGE FILE START --%>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowImageFile" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <%-- SHOW IMAGE FILE END --%>

    <%-- SHOW PDF FILE START--%>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowPDFFile" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
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
            class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server"></iframe>
    </asp:Panel>
    <%-- SHOW PDF FILE END--%>

    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            class="popup-iframe"
            id="iframeViewEntityInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>


    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


