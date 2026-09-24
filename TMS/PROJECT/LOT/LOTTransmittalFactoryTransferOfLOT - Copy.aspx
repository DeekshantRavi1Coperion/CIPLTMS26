<%@ Page Title="CIPLTMS-Transmittal To Factory List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTTransmittalFactoryTransferOfLOT - Copy.aspx.cs" 
    Inherits="PROJECT_LOT_LOTTransmittalFactoryTransferOfLOT" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .style1 {
            width: 10px;
        }
    </style>

    <style type="text/css">
        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            padding: 12px 16px;
            z-index: 1;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>

    <style type="text/css">
        .textboxleft {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxleftgreen {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
        }

        .textboxleftyellow {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightyellow;
        }

        .textboxleftpink {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightpink;
        }

        .textboxcenter {
            width: 50px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxright {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxrightsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: lightyellow;
        }

        .textboxleftsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightyellow;
        }

        .textfiles {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightgreen;
        }
    </style>
    
    
     <style type="text/css">
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
    </style>

    <style type="text/css">
        label {
            display: block;
            font: 1rem 'Fira Sans', sans-serif;
        }

        input,
        label {
            margin: .4rem 0;
        }
    </style>
    
    
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxdrawings {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: whitesmoke;
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
    </style>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
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

    <script type="text/javascript" language="javascript">

        function ValidateAllSearch() {
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>















    <script type="text/javascript">

        function ValidateCustName() {
            var CustName = document.getElementById('<%=txtCustomerNameToEdit.ClientID %>').value;
            if (CustName == '') {
                document.getElementById('<%=txtCustomerNameToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerNameToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustCode() {
            var CustCode = document.getElementById('<%=txtCustomerCodeToEdit.ClientID %>').value;
            if (CustCode == '') {
                document.getElementById('<%=txtCustomerCodeToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerCodeToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJobNo() {
            var JOBNo = document.getElementById('<%=txtJOBNoToEdit.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTFNo() {
            var TFNo = document.getElementById('<%=txtTFNoToEdit.ClientID %>').value;
            if (TFNo == '') {
                document.getElementById('<%=txtTFNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTFNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePONo() {
            var PONo = document.getElementById('<%=txtPONoToEdit.ClientID %>').value;
            if (PONo == '') {
                document.getElementById('<%=txtPONoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemName() {
            var ItemName = document.getElementById('<%=txtItemNameToEdit.ClientID %>').value;
            if (ItemName == '') {
                document.getElementById('<%=txtItemNameToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemNameToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


    </script>

    <script type="text/javascript">

        function ValidateProductionOrderNoAll() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            return check;
        }

    </script>

    <script type="text/javascript">

        function ValidatefileAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment1 = document.getElementById('<%=uploadFileAttachment1ToEdit.ClientID %>').value;
            var divfileAttachment1 = document.getElementById("divfileAttachment1ToEdit");
            var lblfileAttachment1 = document.getElementById('<%=lblfileAttachment1ToEdit.ClientID %>');


            if (fileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachment1ToEdit.ClientID %>').style.borderColor = "";
                divfileAttachment1.style.display = "none";
                lblfileAttachment1.innerHTML = "";
                return false;
            }
            else {

                fileAttachment1 = fileAttachment1.split(" ").join("")
                fileAttachment1 = fileAttachment1.split("(").join("")
                fileAttachment1 = fileAttachment1.split(")").join("")

                if (!regex.test(fileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment1ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment1.style.display = "block";
                    lblfileAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment1ToEdit.ClientID %>').style.borderColor = "";
                    divfileAttachment1.style.display = "none";
                    lblfileAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment2 = document.getElementById('<%=uploadFileAttachment2ToEdit.ClientID %>').value;
            var divfileAttachment2 = document.getElementById("divfileAttachment2ToEdit");
            var lblfileAttachment2 = document.getElementById('<%=lblfileAttachment2ToEdit.ClientID %>');


            if (fileAttachment2 == '') {
                document.getElementById('<%=uploadFileAttachment2ToEdit.ClientID %>').style.borderColor = "";
                divfileAttachment2.style.display = "none";
                lblfileAttachment2.innerHTML = "";
                return false;
            }
            else {

                fileAttachment2 = fileAttachment2.split(" ").join("")
                fileAttachment2 = fileAttachment2.split("(").join("")
                fileAttachment2 = fileAttachment2.split(")").join("")

                if (!regex.test(fileAttachment2.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment2ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment2.style.display = "block";
                    lblfileAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment2ToEdit.ClientID %>').style.borderColor = "";
                    divfileAttachment2.style.display = "none";
                    lblfileAttachment2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment3 = document.getElementById('<%=uploadFileAttachment3ToEdit.ClientID %>').value;
            var divfileAttachment3 = document.getElementById("divfileAttachment3ToEdit");
            var lblfileAttachment3 = document.getElementById('<%=lblfileAttachment3ToEdit.ClientID %>');

            if (fileAttachment3 == '') {
                document.getElementById('<%=uploadFileAttachment3ToEdit.ClientID %>').style.borderColor = "";
                divfileAttachment3.style.display = "none";
                lblfileAttachment3.innerHTML = "";
                return false;
            }
            else {

                fileAttachment3 = fileAttachment3.split(" ").join("")
                fileAttachment3 = fileAttachment3.split("(").join("")
                fileAttachment3 = fileAttachment3.split(")").join("")

                if (!regex.test(fileAttachment3.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment3ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment3.style.display = "block";
                    lblfileAttachment3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment3ToEdit.ClientID %>').style.borderColor = "";
                    divfileAttachment3.style.display = "none";
                    lblfileAttachment3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=uploadFileAttachment4ToEdit.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachment4ToEdit");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachment4ToEdit.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=uploadFileAttachment4ToEdit.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment4ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment4ToEdit.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }
    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateCustName()) {
                check = false;
            }

            if (ValidateCustCode()) {
                check = false;
            }

            if (ValidateJobNo()) {
                check = false;
            }


            if (ValidateTFNo()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateItemName()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to save LOT?")) {
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



        function ValidateJobNoForFT() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            return check;
        }


        function ValidateAllAttachments() {
            var check = true;

            if (ValidatefileAttachment1()) {
                check = false;
            }

            if (ValidatefileAttachment2()) {
                check = false;
            }

            if (ValidatefileAttachment3()) {
                check = false;
            }

            if (ValidatefileAttachment4()) {
                check = false;
            }

            return check;
        }

    </script>



    <script type="text/javascript">

        function ValidateProductionOrderNo() {
            var ProductionOrderNo = document.getElementById('<%=txtProductionOrderNoToEdit.ClientID %>').value;
            if (ProductionOrderNo == '') {
                document.getElementById('<%=txtProductionOrderNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductionOrderNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateProductionOrderDate() {
            var ProductionOrderDate = document.getElementById('<%=txtProductionOrderDateToEdit.ClientID %>').value;
            if (ProductionOrderDate == '') {
                document.getElementById('<%=txtProductionOrderDateToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductionOrderDateToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProductCode() {
            var ProductCode = document.getElementById('<%=ddlProductCodeToEdit.ClientID %>').selectedIndex;
            if (ProductCode == '' || ProductCode == 0) {
                document.getElementById('<%=ddlProductCodeToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProductCodeToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateUOM() {
            var UOM = document.getElementById('<%=txtUOMToEdit.ClientID %>').value;
            if (UOM == '') {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProductDesc() {
            var ProductDesc = document.getElementById('<%=txtProductDescToEdit.ClientID %>').value;
            if (ProductDesc == '') {
                document.getElementById('<%=txtProductDescToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductDescToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateExpectedCompletionDate() {
            var ExpectedCompletionDate = document.getElementById('<%=txtExpectedCompletionDateToEdit.ClientID %>').value;
            if (ExpectedCompletionDate == '') {
                document.getElementById('<%=txtExpectedCompletionDateToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtExpectedCompletionDateToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateLOTMainItems() {
            var LOTMainItems = document.getElementById('<%=ddlLOTMainItemsToEdit.ClientID %>').selectedIndex;
            if (LOTMainItems == '' || LOTMainItems == 0) {
                document.getElementById('<%=ddlLOTMainItemsToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLOTMainItemsToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLOTMainSubItems() {
            var LOTMainSubItems = document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').selectedIndex;
            if (LOTMainSubItems == '' || LOTMainSubItems == 0) {
                document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescriptionToEdit.ClientID %>').value;
            if (Description == '') {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrgNo() {
            var DrgNo = document.getElementById('<%=txtDrgNoToEdit.ClientID %>').value;
            if (DrgNo == '') {
                document.getElementById('<%=txtDrgNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrgNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevNo() {

            var RevNo = document.getElementById('<%=ddlRevNoToEdit.ClientID %>').selectedIndex;
            var oldRevNo = document.getElementById('<%=hdOldRevNo.ClientID %>').value;

            if (parseInt(RevNo) <= parseInt(oldRevNo)) {
                document.getElementById('<%=ddlRevNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=lblRevNoToEditMsg.ClientID %>').innerHTML = "Revision No. " + oldRevNo + " Already exists, please try with higher Revision number...!!!";
                return true;
            }
            else {
                document.getElementById('<%=ddlRevNoToEdit.ClientID %>').style.borderColor = "";
                document.getElementById('<%=lblRevNoToEditMsg.ClientID %>').innerHTML = "";
                return false;
            }
        }

        function ValidateCategory() {
            var Category = document.getElementById('<%=chkLstCategoryToEdit.ClientID %>');
            var chkBox = Category.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < chkBox.length; i++) {
                if (chkBox[i].checked) {
                    counter++;
                }
            }

            if (counter == 0) {
                alert("Please select atleast one category...!!");
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateQuantity() {
            var IsPartOfProduction = document.getElementById('<%=chkIsPartOfProductionOrMainDrawingToEdit.ClientID %>').checked;
            var Quantity = document.getElementById('<%=txtQuantityToEdit.ClientID %>').value;

            if (IsPartOfProduction == true) {
                if (Quantity == '' || parseInt(Quantity) == 0) {
                    document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }




        function ValidateFileSiDrawing1() {

            var SiDrawingToEdit1 = 0;
            var UploadSiDrawingToEdit1 = 0;

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawingToEdit1 = document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').value;
            var divFileSiDrawingToEdit1 = document.getElementById("divFileSiDrawingToEdit1");
            var lblFileSiDrawingToEdit1 = document.getElementById('<%=lblFileSiDrawingToEdit1.ClientID %>');

            if (document.getElementById('<%=hdSiDrawingToEdit1.ClientID %>') != null) {
                SiDrawingToEdit1 = document.getElementById('<%=hdSiDrawingToEdit1.ClientID %>').value;
            }
            else {
                SiDrawingToEdit1 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawingToEdit1.ClientID %>') != null) {
                UploadSiDrawingToEdit1 = document.getElementById('<%=hdUploadSiDrawingToEdit1.ClientID %>').value;
            }
            else {
                UploadSiDrawingToEdit1 = 0;
            }


            var newSubitemUpdationFlag = document.getElementById('<%=hdNewSubitemUpdationFlag.ClientID %>').value;
            var revisedSubitemUpdationFlag = document.getElementById('<%=hdRevisedSubitemUpdationFlag.ClientID %>').value;

            if (parseInt(newSubitemUpdationFlag) == 0 || parseInt(revisedSubitemUpdationFlag) == 0) {
                if (FileSiDrawingToEdit1 == '') {
                    document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawingToEdit1.style.display = "none";
                    lblFileSiDrawingToEdit1.innerHTML = "";
                    return true;
                }
                else {

                    FileSiDrawing1 = FileSiDrawing1.split(" ").join("")
                    FileSiDrawing1 = FileSiDrawing1.split("(").join("")
                    FileSiDrawing1 = FileSiDrawing1.split(")").join("")

                    if (!regex.test(FileSiDrawingToEdit1.toLowerCase())) {
                        document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "#F7627F";
                        divFileSiDrawingToEdit1.style.display = "block";
                        lblFileSiDrawing1.innerHTML = "Please enter only .pdf file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "";
                        divFileSiDrawingToEdit1.style.display = "none";
                        lblFileSiDrawingToEdit1.innerHTML = "";
                        return false;
                    }
                }
            }
            else {

                if (parseInt(SiDrawingToEdit1) == 0 && parseInt(UploadSiDrawingToEdit1) == 1) {
                    if (FileSiDrawingToEdit1 == '') {
                        document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "#F7627F";
                        divFileSiDrawingToEdit1.style.display = "none";
                        lblFileSiDrawingToEdit1.innerHTML = "";
                        return true;
                    }
                    else {

                        FileSiDrawing1 = FileSiDrawing1.split(" ").join("")
                        FileSiDrawing1 = FileSiDrawing1.split("(").join("")
                        FileSiDrawing1 = FileSiDrawing1.split(")").join("")

                        if (!regex.test(FileSiDrawingToEdit1.toLowerCase())) {
                            document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "#F7627F";
                            divFileSiDrawingToEdit1.style.display = "block";
                            lblFileSiDrawing1.innerHTML = "Please enter only .pdf file!";
                            return true;
                        }
                        else {
                            document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "";
                            divFileSiDrawingToEdit1.style.display = "none";
                            lblFileSiDrawingToEdit1.innerHTML = "";
                            return false;
                        }
                    }
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawingToEdit1.ClientID %>').style.borderColor = "";
                    divFileSiDrawingToEdit1.style.display = "none";
                    lblFileSiDrawingToEdit1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawingToEdit2 = document.getElementById('<%=uploadFileSiDrawingToEdit2.ClientID %>').value;
            var divFileSiDrawingToEdit2 = document.getElementById("divFileSiDrawingToEdit2");
            var lblFileSiDrawingToEdit2 = document.getElementById('<%=lblFileSiDrawingToEdit2.ClientID %>');


            if (FileSiDrawingToEdit2 == '') {
                document.getElementById('<%=uploadFileSiDrawingToEdit2.ClientID %>').style.borderColor = "";
                divFileSiDrawingToEdit2.style.display = "none";
                lblFileSiDrawingToEdit2.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing2 = FileSiDrawing2.split(" ").join("")
                FileSiDrawing2 = FileSiDrawing2.split("(").join("")
                FileSiDrawing2 = FileSiDrawing2.split(")").join("")

                if (!regex.test(FileSiDrawingToEdit2.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawingToEdit2.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawingToEdit2.style.display = "block";
                    lblFileSiDrawingToEdit2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawingToEdit2.ClientID %>').style.borderColor = "";
                    divFileSiDrawingToEdit2.style.display = "none";
                    lblFileSiDrawingToEdit2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawingToEdit3 = document.getElementById('<%=uploadFileSiDrawingToEdit3.ClientID %>').value;
            var divFileSiDrawingToEdit3 = document.getElementById("divFileSiDrawingToEdit3");
            var lblFileSiDrawingToEdit3 = document.getElementById('<%=lblFileSiDrawingToEdit3.ClientID %>');

            if (FileSiDrawingToEdit3 == '') {
                document.getElementById('<%=uploadFileSiDrawingToEdit3.ClientID %>').style.borderColor = "";
                divFileSiDrawingToEdit3.style.display = "none";
                lblFileSiDrawingToEdit3.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing3 = FileSiDrawing3.split(" ").join("")
                FileSiDrawing3 = FileSiDrawing3.split("(").join("")
                FileSiDrawing3 = FileSiDrawing3.split(")").join("")

                if (!regex.test(FileSiDrawingToEdit3.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawingToEdit3.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawingToEdit3.style.display = "block";
                    lblFileSiDrawingToEdit3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawingToEdit3.ClientID %>').style.borderColor = "";
                    divFileSiDrawingToEdit3.style.display = "none";
                    lblFileSiDrawingToEdit3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawingToEdit4 = document.getElementById('<%=uploadFileSiDrawingToEdit4.ClientID %>').value;
            var divFileSiDrawingToEdit4 = document.getElementById("divFileSiDrawingToEdit4");
            var lblFileSiDrawingToEdit4 = document.getElementById('<%=lblFileSiDrawingToEdit4.ClientID %>');

            if (FileSiDrawingToEdit4 == '') {
                document.getElementById('<%=uploadFileSiDrawingToEdit4.ClientID %>').style.borderColor = "";
                divFileSiDrawingToEdit4.style.display = "none";
                lblFileSiDrawingToEdit4.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing4 = FileSiDrawing4.split(" ").join("")
                FileSiDrawing4 = FileSiDrawing4.split("(").join("")
                FileSiDrawing4 = FileSiDrawing4.split(")").join("")

                if (!regex.test(FileSiDrawingToEdit4.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawingToEdit4.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawingToEdit4.style.display = "block";
                    lblFileSiDrawingToEdit4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawingToEdit4.ClientID %>').style.borderColor = "";
                    divFileSiDrawingToEdit4.style.display = "none";
                    lblFileSiDrawingToEdit4.innerHTML = "";
                    return false;
                }
            }
        }




        function ValidateTagNo() {
            var TagNo = document.getElementById('<%=txtTagNoToEdit.ClientID %>').value;

            if (TagNo == '') {
                document.getElementById('<%=txtTagNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTagNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAddSubitem() {
            var check = true;

            var SiDrawingToEdit1 = 0;
            var SiDrawingToEdit2 = 0;
            var SiDrawingToEdit3 = 0;
            var SiDrawingToEdit4 = 0;

            var UploadSiDrawingToEdit1 = 0;
            var UploadSiDrawingToEdit2 = 0;
            var UploadSiDrawingToEdit3 = 0;
            var UploadSiDrawingToEdit4 = 0;


            if (document.getElementById('<%=hdSiDrawingToEdit1.ClientID %>') != null) {
                SiDrawingToEdit1 = document.getElementById('<%=hdSiDrawingToEdit1.ClientID %>').value;
            }
            else {
                SiDrawingToEdit1 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawingToEdit1.ClientID %>') != null) {
                UploadSiDrawingToEdit1 = document.getElementById('<%=hdUploadSiDrawingToEdit1.ClientID %>').value;
            }
            else {
                UploadSiDrawingToEdit1 = 0;
            }



            if (document.getElementById('<%=hdSiDrawingToEdit2.ClientID %>') != null) {
                SiDrawingToEdit2 = document.getElementById('<%=hdSiDrawingToEdit2.ClientID %>').value;
            }
            else {
                SiDrawingToEdit2 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawingToEdit2.ClientID %>') != null) {
                UploadSiDrawingToEdit2 = document.getElementById('<%=hdUploadSiDrawingToEdit2.ClientID %>').value;
            }
            else {
                UploadSiDrawingToEdit2 = 0;
            }



            if (document.getElementById('<%=hdSiDrawingToEdit3.ClientID %>') != null) {
                SiDrawingToEdit3 = document.getElementById('<%=hdSiDrawingToEdit3.ClientID %>').value;
            }
            else {
                SiDrawingToEdit3 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawingToEdit3.ClientID %>') != null) {
                UploadSiDrawingToEdit3 = document.getElementById('<%=hdUploadSiDrawingToEdit3.ClientID %>').value;
            }
            else {
                UploadSiDrawingToEdit3 = 0;
            }



            if (document.getElementById('<%=hdSiDrawingToEdit4.ClientID %>') != null) {
                SiDrawingToEdit4 = document.getElementById('<%=hdSiDrawingToEdit4.ClientID %>').value;
            }
            else {
                SiDrawingToEdit4 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawingToEdit4.ClientID %>') != null) {
                UploadSiDrawingToEdit4 = document.getElementById('<%=hdUploadSiDrawingToEdit4.ClientID %>').value;
            }
            else {
                UploadSiDrawingToEdit4 = 0;
            }


            var hdAddSubitemFlag = 0;
            var hdNewSubitemUpdationFlag = 0;
            var hdRevisedSubitemUpdationFlag = 0;
            var hdSubitemReviseFlag = 0;


            hdAddSubitemFlag = document.getElementById('<%=hdAddSubitemFlag.ClientID %>').value;
            hdNewSubitemUpdationFlag = document.getElementById('<%=hdNewSubitemUpdationFlag.ClientID %>').value;
            hdRevisedSubitemUpdationFlag = document.getElementById('<%=hdRevisedSubitemUpdationFlag.ClientID %>').value;
            hdSubitemReviseFlag = document.getElementById('<%=hdSubitemReviseFlag.ClientID %>').value;




            //if (ValidateProductionOrderNo()) {
            //    check = false;
            //}

            //if (ValidateProductionOrderDate()) {
            //    check = false;
            //}

            //if (ValidateProductCode()) {
            //    check = false;
            //}

            //if (ValidateUOM()) {
            //    check = false;
            //}

            //if (ValidateProductDesc()) {
            //    check = false;
            //}


            //if (ValidateExpectedCompletionDate()) {
            //    check = false;
            //}


            if (ValidateLOTMainItems()) {
                check = false;
            }

            if (ValidateLOTMainSubItems()) {
                check = false;
            }

            if (ValidateDescription()) {
                check = false;
            }

            if (ValidateDrgNo()) {
                check = false;
            }

            //if ((parseInt(hdAddSubitemFlag) == 0 ||
            //     parseInt(hdNewSubitemUpdationFlag) == 0) && (parseInt(hdSubitemReviseFlag) > 0 ||
            //     parseInt(hdRevisedSubitemUpdationFlag) > 0)) {
            //    if (ValidateRevNo()) {
            //        check = false;
            //    }
            //}

            if (ValidateCategory()) {
                check = false;
            }

            if (ValidateQuantity()) {
                check = false;
            }

            if (parseInt(SiDrawingToEdit1) == 0 && parseInt(UploadSiDrawingToEdit1) == 1) {
                if (ValidateFileSiDrawing1()) {
                    check = false;
                }
            }

            if (parseInt(SiDrawingToEdit2) == 0 && parseInt(UploadSiDrawingToEdit2) == 1) {
                if (ValidateFileSiDrawing2()) {
                    check = false;
                }
            }

            if (parseInt(SiDrawingToEdit3) == 0 && parseInt(UploadSiDrawingToEdit3) == 1) {
                if (ValidateFileSiDrawing3()) {
                    check = false;
                }
            }

            if (parseInt(SiDrawingToEdit4) == 0 && parseInt(UploadSiDrawingToEdit4) == 1) {
                if (ValidateFileSiDrawing4()) {
                    check = false;
                }
            }




            if (ValidateTagNo()) {
                check = false;
            }


            <%--if (check) {
                var msg;

                if (parseInt(subitemUpdationFlag) == 0) {
                    msg = "Would you like to add subitem?";
                }
                else {
                    msg = "Would you like to update subitem?";
                }

                if (confirm(msg)) {
                    document.getElementById('<%=hdSubitemConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdSubitemConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }--%>

            return check;
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
    <asp:HiddenField ID="hdProductionManagerIDFlag" runat="server" />
    <asp:HiddenField ID="hdOldRevNo" runat="server" />
    <asp:HiddenField ID="hdRemovedSubitemIDs" runat="server" />
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Transfer of LOT List</legend>
            <table width="100%">
                <tr>
                    <td>Start Date:</td>
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

                    <td>&nbsp;</td>

                    <td>End Date:</td>
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
                                        ToolTip="End Date Calendar" /></td>
                            </tr>
                        </table>
                    </td>

                    <td>&nbsp;</td>

                    <td>TF No.:</td>
                    <td>
                        <asp:TextBox ID="txtTFNo" runat="server" Width="100%" />
                    </td>

                    <td>&nbsp;</td>

                    <td>Customer Name:</td>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" />
                    </td>

                </tr>
                
                <tr>
                    <td>Status.:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td>Company:</td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td>JOB No.:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" />
                    </td>

                    <td>&nbsp;</td>

                    <td>LOT For:</td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%;">
                                    <asp:DropDownList ID="ddlLOTMainItems" runat="server" Width="100%" Height="25px" OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:DropDownList ID="ddlLOTMainSubitems" runat="server" Width="100%" Height="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
               
                <tr>
                    <td>Tag Number:</td>
                    <td>
                        <asp:TextBox ID="txtTagNumber" runat="server" Width="100%" />
                    </td>

                    <td>&nbsp;</td>

                    <td>Drawing Number:</td>
                    <td>
                        <asp:TextBox ID="txtDrawingNumber" runat="server" Width="100%" />
                    </td>

                    <td colspan="2">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllSearch();" />
                    </td>

                    <td colspan="2">&nbsp;</td>

                    <td>
                        <asp:Button ID="btnAddNewLOT" CssClass="button" Width="100%" runat="server"
                            Text="Add New" OnClick="btnAddNewLOT_Click" />
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
            <div style='overflow: auto; width: 100%; height: 400px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvLOTTFList" runat="server" AutoGenerateColumns="False" 
                    CssClass="myGrid" CellPadding="4" 
                    ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvLOTTFList_RowCommand" OnRowDataBound="gvLOTTFList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="Transfer" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>

                                <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                                <asp:Label ID="lblTFNo" runat="server" Visible="false" Text='<%# Eval("TF_NO") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblLOTDate" runat="server" Visible="false" Text='<%# Eval("DATE") %>' />
                                <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblItemName" runat="server" Visible="false" Text='<%# Eval("ITEM_NAME") %>' />
                                <asp:Label ID="lblImpNotes" runat="server" Visible="false" Text='<%# Eval("IMP_NOTES") %>' />

                                <asp:Label ID="lblCreatedByID" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                                <asp:Label ID="lblJobPEID" runat="server" Visible="false" Text='<%# Eval("PE_ID") %>' />
                                <asp:Label ID="lblJobPMID" runat="server" Visible="false" Text='<%# Eval("PM_ID") %>' />

                                <%--<asp:Label ID="lblIsApprovalMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVAL_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsPEApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PE_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsPMApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PM_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblLOTAcceptedByID" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_BY") %>' />
                                <asp:Label ID="lblIsAccpetedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ACCEPTED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedAccpetedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_ACCEPTED_MAIL_SENT") %>' />
                                <asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNT") %>' />
                                <asp:Label ID="lblAmendmentByID" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_BY") %>' />
                                <asp:Label ID="lblIsPEAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PE_AMENDMENT_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsPMAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PM_AMENDMENT_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsProdMngrAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PROD_MNGR_AMENDMENT_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedPEApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_PE_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedPMApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_PM_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsCompletedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_COMPLETED_MAIL_SENT") %>' />--%>

                                <asp:ImageButton ID="imgBtnRevise" CommandArgument="REVISE" runat="server" 
                                    ImageUrl="~/Images/LOT/transfer.png" Height="35px" Width="35px" 
                                    ToolTip="Transfer of LOT"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Subitem" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnViewSubitemDetail" Height="30px" Width="30px" CommandArgument="ViewSubitemDETAIL"
                                    runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View Subitem Details" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="30px" Width="30px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/pdficon3.png" ToolTip="View LOT Detail in PDF" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Additional Att." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Att.2" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Att.3" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT3_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Att.4" HeaderStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT4_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment4" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT4"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="TF_NO" HeaderText="TF_No" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                        <asp:BoundField DataField="DATE" HeaderText="Date" />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Customer_Code" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer_Name" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_No" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_No" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item_Name" />
                        <asp:BoundField DataField="IMP_NOTES" HeaderText="Imp_Notes" />

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




    <%-- REVISE LOT STATUS START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeReviseLOT" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="700px" Width="1200px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <fieldset style="width: 97%; margin-left: 17px; margin-top: 10px;">
            <legend style="text-align: center;">TF No. [<asp:Label ID="lblTFNo" runat="server" />] Details</legend>
            <div style='overflow: auto; width: 99%; height: 580px; border: 1px solid lightgray; margin-left: 5px;'>
                <asp:HiddenField ID="hdLOTMainSubitemID" runat="server" />
                <asp:HiddenField ID="hdProductionOrderNo" runat="server" />
                <asp:HiddenField ID="hdPONo" runat="server" />
                <asp:HiddenField ID="hdTFNo" runat="server" />


                <table width="95%" align="center">
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="4" align="center">
                            <asp:Panel ID="pnlReviseMsg" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="lblReviseMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">Old TFNo.:</td>
                        <td style="width: 32%;">
                            <asp:TextBox ID="txtOldTFNoToEdit" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 15%;">Company:</td>
                        <td style="width: 32%;">
                            <asp:HiddenField ID="hdCompanyToEdit" runat="server" />
                            <asp:TextBox ID="txtCompanyToEdit" runat="server" Width="100%" Enabled="false" />
                            <%--<asp:DropDownList ID="ddlCompanyToEdit" runat="server" Width="100%" Height="26px" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyToEdit_SelectedIndexChanged" />--%>                        
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 15%;">JOB Number:</td>
                        <td style="width: 32%;">
                            <asp:TextBox ID="txtJOBNoToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateJOBNo();" />
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 15%;">Customer PO Number:</td>
                        <td style="width: 32%;">
                            <asp:TextBox ID="txtPONoToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidatePONo();" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>Customer Name:</td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtCustomerNameToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustName();" />
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtCustomerCodeToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustCode();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>Date:</td>
                        <td>
                            <asp:TextBox ID="txtDateToEdit" runat="server" onkeyDown="javascript:preventInput(event);" Width="100%"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>TF Number:</td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="txtTFNoToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateTFNo();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Button ID="btnGetTFno" runat="server" Width="100%" Text="Get" CssClass="button"
                                            OnClientClick="return ValidateJobNoForFT();" OnClick="btnGetTFno_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>Item:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtItemNameToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateItemName();" />
                        </td>
                    </tr>

                    
                </table>

                <%--<asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>--%>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAddSubitem" runat="server" Width="25%" Text="Add Subitem" CssClass="button" OnClick="btnAddSubitem_Click" />
                        </td>
                    </tr>
                </table>

                <%--ADD SUBITEM START--%>
                <asp:Button ID="btnShowPopupAddSubitems" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="mpeAddSubitems" runat="server" TargetControlID="btnShowPopupAddSubitems"
                    PopupControlID="pnlPopupAddSubitems" CancelControlID="imgBtnCancelAddSubitems" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlPopupAddSubitems" runat="server" BackColor="White" Height="750px" Width="1200px"
                    Style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnCancelAddSubitems" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <fieldset style="width: 95%; margin-left: 2%; margin-top: 10px;">
                        <legend style="text-align: center;" id="lgSubitemUpdation" runat="server" />
                        <asp:HiddenField ID="hdSRNo" runat="server" />
                        <asp:HiddenField ID="hdNewSubitemUpdationFlag" runat="server" Value="0" />
                        <asp:HiddenField ID="hdRevisedSubitemUpdationFlag" runat="server" Value="0" />
                        <asp:HiddenField ID="hdSubitemReviseFlag" runat="server" Value="0" />
                        <asp:HiddenField ID="hdAddSubitemFlag" runat="server" Value="0" />
                        <div style="width: 100%; margin-left: 0px;">
                            <table style="width: 95%;" align="center">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">Production Order No.:</td>
                                    <td style="width: 33%;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="txtProductionOrderNoToEdit" runat="server" Width="100%" onblur="return ValidateProductionOrderNo();" /></td>
                                                <td style="width: 20%">
                                                    <asp:Button ID="btnGetProductionNumber" runat="server" Width="100%" Text="Get" CssClass="button"
                                                        OnClick="btnGetProductionNumber_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="width: 15%;">Production Order Date:</td>
                                    <td style="width: 33%;">
                                        <asp:TextBox ID="txtProductionOrderDateToEdit" runat="server" Width="100%"
                                            Enabled="false" onblur="return ValidateProductionOrderDate();" />
                                        <%--onkeyDown="javascript:preventInput(event);"--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Product Code:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProductCodeToEdit" runat="server" Width="100%" Height="26px" onblur="return ValidateProductCode();"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlProductCodeToEdit_SelectedIndexChanged" />
                                    </td>
                                    <td>&nbsp;</td>

                                    <td>UOM:</td>
                                    <td>
                                        <asp:TextBox ID="txtUOMToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateUOM();" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>Product Description:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtProductDescToEdit" runat="server" Width="100%" Enabled="false" onblur="return ValidateProductDesc();" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <b>
                                            <label style="color: darkgreen;">Is Part of Production Status Report/Main Drawing?:</label></b>
                                        <asp:CheckBox ID="chkIsPartOfProductionOrMainDrawingToEdit" runat="server" />
                                    </td>
                                    <td>&nbsp;</td>


                                    <td style="width: 15%;">Completion Required By:</td>
                                    <td style="width: 33%;">
                                        <asp:TextBox ID="txtExpectedCompletionDateToEdit" runat="server" Width="100%" onblur="return ValidateExpectedCompletionDate();"
                                            Enabled="false" /><%--onkeyDown="javascript:preventInput(event);"--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 15%;">LOT For:</td>
                                    <td style="width: 30%;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 60%">
                                                    <asp:DropDownList ID="ddlLOTMainItemsToEdit" runat="server" Width="100%" Height="26px" onblur="return ValidateLOTMainItems();"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlLOTMainItemsToEdit_SelectedIndexChanged" />
                                                </td>
                                                <td style="width: 40%">
                                                    <asp:DropDownList ID="ddlLOTMainSubitemsToEdit" runat="server" Width="100%" Height="26px"
                                                        onblur="return ValidateLOTMainSubItems();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp;</td>

                                    <td style="width: 15%;">Drg./Doc No.:</td>
                                    <td style="width: 33%;">

                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 80%">
                                                    <asp:TextBox ID="txtDrgNoToEdit" runat="server" Width="100%" onblur="return ValidateDrgNo();" Style="text-transform: uppercase" />

                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Button ID="btnGetDMSDrawingNo" runat="server" Width="100%" Text="DMS" CssClass="button"
                                                        OnClick="btnGetDMSDrawingNo_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>Description:</td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="2" runat="server" Width="100%"
                                            onblur="return ValidateDescription();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">Rev. No.:</td>
                                    <td style="width: 33%;">
                                        <%--<asp:TextBox ID="txtRevNoToEdit" runat="server" Width="100%" onkeypress="return inNumberKey(this, event);"
                                            onblur="return ValidateRevNo();" Text="0" Enabled="false" />--%>


                                        <asp:DropDownList ID="ddlRevNoToEdit" runat="server" Width="100%" Height="26px">
                                            <asp:ListItem Text="00" Value="0" />
                                            <asp:ListItem Text="01" Value="1" />
                                            <asp:ListItem Text="02" Value="2" />
                                            <asp:ListItem Text="03" Value="3" />
                                            <asp:ListItem Text="04" Value="4" />
                                            <asp:ListItem Text="05" Value="5" />
                                            <asp:ListItem Text="06" Value="6" />
                                            <asp:ListItem Text="07" Value="7" />
                                            <asp:ListItem Text="08" Value="8" />
                                            <asp:ListItem Text="09" Value="9" />
                                            <asp:ListItem Text="10" Value="10" />
                                        </asp:DropDownList>

                                    </td>
                                    <td>&nbsp;</td>
                                    <td>Quantity:</td>
                                    <td>
                                        <asp:HiddenField ID="hdQuantityToEdit" runat="server" />
                                        <asp:TextBox ID="txtQuantityToEdit" runat="server" Width="100%" onblur="return ValidateQuantity();"
                                            onkeypress="return inNumberKey(this, event);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        <asp:Label ID="lblRevNoToEditMsg" runat="server" ForeColor="Red" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Category (For Factory):</td>
                                    <td>
                                        <asp:CheckBoxList ID="chkLstCategoryToEdit" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                                            onblur="return ValidateCategory();" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>Tag No.:</td>
                                    <td>
                                        <asp:TextBox ID="txtTagNoToEdit" runat="server" Width="100%" onblur="return ValidateTagNo();"
                                            Style="text-transform: uppercase" />
                                    </td>
                                </tr>
                                <tr>

                                    <td>&nbsp;</td>

                                </tr>

                                <asp:Panel ID="pnlAttachFiles" runat="server">

                                    <tr>
                                        <td>Drawing (.pdf):</td>
                                        <td>
                                            <div>
                                                <asp:Panel ID="pnlViewSiDrawingToEdit1" runat="server" Visible="false">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 75%;">
                                                                <asp:HiddenField ID="hdSiDrawingToEdit1" runat="server" Value="0" />
                                                                <asp:TextBox ID="txtSiDrawingToEdit1" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                                            <td>&nbsp;</td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit1" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit1_Click" ToolTip="View" />
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit1" runat="server" Height="25px" Width="25px"
                                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit1_Click" ToolTip="Remove" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlUploadSiDrawingToEdit1" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 90%;">
                                                                <asp:HiddenField ID="hdUploadSiDrawingToEdit1" runat="server" Value="0" />
                                                                <asp:FileUpload ID="uploadFileSiDrawingToEdit1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />

                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit1" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit1_Click" ToolTip="Undo" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>

                                        </td>

                                        <td>&nbsp;</td>

                                        <td>Drawing (.dwg/.dxf):</td>
                                        <td>
                                            <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                                            <div>
                                                <asp:Panel ID="pnlViewSiDrawingToEdit2" runat="server" Visible="false">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 75%;">
                                                                <asp:HiddenField ID="hdSiDrawingToEdit2" runat="server" Value="0" />
                                                                <asp:TextBox ID="txtSiDrawingToEdit2" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                                            <td>&nbsp;</td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit2" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit2_Click" ToolTip="View" />
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit2" runat="server" Height="25px" Width="25px"
                                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit2_Click" ToolTip="Remove" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlUploadSiDrawingToEdit2" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 90%;">
                                                                <asp:HiddenField ID="hdUploadSiDrawingToEdit2" runat="server" Value="0" />
                                                                <asp:FileUpload ID="uploadFileSiDrawingToEdit2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />

                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit2" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit2_Click" ToolTip="Undo" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <div id="divFileSiDrawingToEdit1" style="display: none;">
                                                <asp:Label ID="lblFileSiDrawingToEdit1" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                        <td>
                                            <div id="divFileSiDrawingToEdit2" style="display: none;">
                                                <asp:Label ID="lblFileSiDrawingToEdit2" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden;">
                                        <td>Drawing 3:</td>
                                        <td>
                                            <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                                            <div>
                                                <asp:Panel ID="pnlViewSiDrawingToEdit3" runat="server" Visible="false">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 80%;">
                                                                <asp:HiddenField ID="hdSiDrawingToEdit3" runat="server" Value="0" />
                                                                <asp:TextBox ID="txtSiDrawingToEdit3" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                                            <td>&nbsp;</td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit3" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit3_Click" ToolTip="View" />
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit3" runat="server" Height="25px" Width="25px"
                                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit3_Click" ToolTip="Remove" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlUploadSiDrawingToEdit3" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 90%;">
                                                                <asp:HiddenField ID="hdUploadSiDrawingToEdit3" runat="server" Value="0" />
                                                                <asp:FileUpload ID="uploadFileSiDrawingToEdit3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit3" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit3_Click" ToolTip="Undo" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>

                                        </td>

                                        <td>&nbsp;</td>

                                        <td>Drawing 4:</td>
                                        <td>
                                            <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit4" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                                            <div>
                                                <asp:Panel ID="pnlViewSiDrawingToEdit4" runat="server" Visible="false">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 75%;">
                                                                <asp:HiddenField ID="hdSiDrawingToEdit4" runat="server" Value="0" />
                                                                <asp:TextBox ID="txtSiDrawingToEdit4" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                                            <td>&nbsp;</td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit4" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit4_Click" ToolTip="View" />
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit4" runat="server" Height="25px" Width="25px"
                                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit4_Click" ToolTip="Remove" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlUploadSiDrawingToEdit4" runat="server" Visible="true">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 90%;">
                                                                <asp:HiddenField ID="hdUploadSiDrawingToEdit4" runat="server" Value="0" />
                                                                <asp:FileUpload ID="uploadFileSiDrawingToEdit4" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />

                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit4" runat="server" Height="30px" Width="30px"
                                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit4_Click" ToolTip="Undo" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden;">
                                        <td>&nbsp;</td>
                                        <td>
                                            <div id="divFileSiDrawingToEdit3" style="display: none;">
                                                <asp:Label ID="lblFileSiDrawingToEdit3" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                        <td>
                                            <div id="divFileSiDrawingToEdit4" style="display: none;">
                                                <asp:Label ID="lblFileSiDrawingToEdit4" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                    </tr>

                                </asp:Panel>

                                <tr>
                                    <td colspan="5">
                                        <asp:Button ID="btnAddUpdateSubitemToList" CssClass="button" runat="server" Text="Add Subitem"
                                            Width="100%" OnClick="btnAddUpdateSubitemToList_Click" OnClientClick="return ValidateAddSubitem();" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Panel ID="pnlAddUpdatedSubitemsMsg" Visible="false" runat="server" Height="50px">
                                            <asp:TextBox ID="txtAddUpdatedSubitemsMsg" runat="server" Font-Bold="True" Font-Size="Large"
                                                TextMode="MultiLine" Rows="2"
                                                Enabled="false" Width="100%" CssClass="text-center" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </asp:Panel>
                <%--ADD SUBITEM END--%>

                <table width="95%" align="center">
                    <tr>
                        <td>
                            <div align="center">
                                <fieldset style="width: 100%;">
                                    <legend style="text-align: center;">
                                        <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" /></legend>
                                    <div style='overflow-y: scroll; overflow-x: scroll; width: 1100px; height: 300px; border: 1px solid lightgray;'>
                                        <%--<div id="gridContainer" style='overflow-x: scroll; overflow-y: hidden; width: 1150px; height: 250px; border: 1px solid lightgray;'>--%>

                                        <asp:GridView ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            CssClass="myGrid" ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                            OnRowCommand="gvSubItem_RowCommand" OnRowDataBound="gvSubItem_RowDataBound">
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Transfer" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTTFMainSubitemID" runat="server" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblNextStatusID" runat="server" Text='<%# Eval("NEXT_STATUS_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTTFID" runat="server" Text='<%# Eval("LOT_TF_ID") %>' Visible="false" />

                                                        <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblProductionOrderDate" runat="server" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' Visible="false" />
                                                        <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />
                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                                        <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("PRODUCT_DESC") %>' Visible="false" />
                                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />

                                                        <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("TAG_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("SUBITEM_DESC") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTMainItemID" runat="server" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTMainSubitemID" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblLOTFor" runat="server" Text='<%# Eval("LOT_MAIN_ITEM") %>' Visible="false" />
                                                        <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblOldRevNo" runat="server" Text='<%# Eval("OLD_REVISION_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="false" />
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Visible="false" />

                                                        <asp:Label ID="lblIsRevised" runat="server" Text='<%# Eval("IS_REVISED") %>' Visible="false" />

                                                        <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' Visible="false" />
                                                        <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' Visible="false" />
                                                        <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' Visible="false" />
                                                        <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' Visible="false" />

                                                        <asp:ImageButton ID="imgBtnRevise" CommandArgument="REVISE" runat="server" 
                                                            ToolTip="Transfer of LOT"
                                                            ImageUrl="~/Images/LOT/transfer.png" Height="35px" Width="35px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit"
                                                            ImageUrl="~/Images/LOT/edit5.png" Width="35px" Height="35px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                                            ImageUrl="~/Images/Icons/REMOVE03.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                                                <asp:TemplateField HeaderText="Tag No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTagNoInList" runat="server" Text='<%# Eval("TAG_NO") %>' Style="text-transform: uppercase" Width="150PX"
                                                            CssClass="textboxtagno" MaxLength="15"></asp:TextBox>
                                                        <%--onkeyDown="javascript:preventInput(event);"--%>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />
                                                <asp:BoundField DataField="DRAWING_NO" HeaderText="Drg/DOC.No" />

                                                <asp:TemplateField HeaderText="Rev.No.">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Width="40PX"
                                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="CATEGORY" HeaderText="Category" />

                                                <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No." />
                                                <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="Production Order Date" />
                                                <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Completion Required By" />
                                                <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                                                <asp:BoundField DataField="PRODUCT_DESC" HeaderText="Product Desc" />
                                                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="40PX"
                                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Is Part Of Prod. Status Report">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsPartOfProductionStatusReport" runat="server" Text='<%# Eval("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID") %>' Visible="false" />
                                                        <asp:CheckBox ID="chkIsPartOfProductStatusReport" runat="server" Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="SI_ATTACHMENT1_NAME" HeaderText="Drawing (.pdf)" />
                                                <asp:BoundField DataField="SI_ATTACHMENT2_NAME" HeaderText="Drawing (.dwg/.dxf)" />
                                                <asp:BoundField DataField="SI_ATTACHMENT3_NAME" HeaderText="Drawing3" Visible="false" />
                                                <asp:BoundField DataField="SI_ATTACHMENT4_NAME" HeaderText="Drawing4" Visible="false" />

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
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>


                <%--PRODUCTION ORDER NO DETAIL START--%>
                <asp:Button ID="btnShowPopupProductonOrderNoDetail" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="mpeProductonOrderNoDetail" runat="server" TargetControlID="btnShowPopupProductonOrderNoDetail"
                    PopupControlID="pnlPopupProductonOrderNoDetail" CancelControlID="imgBtnCancelProductonOrderNoDetail" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlPopupProductonOrderNoDetail" runat="server" BackColor="White" Height="500px" Width="900px"
                    Style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnCancelProductonOrderNoDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblProductonOrderNoRecords" runat="server" Text="Records[0]" /></legend>
                        <asp:Label ID="lblProductonOrderNoMsg" runat="server" />
                        <table style="width: 95%; margin-left: 20px;">
                            <tr>
                                <td>JOB No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJOBNoSearchPON" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td>Production Order No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProductionOrderNoSearchPON" runat="server" Width="100%" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSearchProductionOrderNo" CssClass="button" runat="server" Text="Search"
                                        Width="100%" OnClick="btnSearchProductionOrderNo_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style='overflow: auto; width: 99%; height: 330px; border: 1px solid lightgray; margin-left: 5px;'>
                            <div align="center">
                                <asp:GridView ID="gvProductonOrderNoDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvProductonOrderNoDetail_RowCommand">
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="GET">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                                <asp:Label ID="lblProductionOrderNo" runat="server" Visible="false" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' />
                                                <asp:Label ID="lblClass" runat="server" Visible="false" Text='<%# Eval("CLASS") %>' />
                                                <asp:Label ID="lblProductionOrderDate" runat="server" Visible="false" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' />
                                                <%--<asp:Label ID="lblExpectedCompletionDate" runat="server" Visible="false" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' />--%>

                                                <asp:Button ID="btnGetProductionOrderNo" CommandArgument="GET" ToolTip="Get Production Order No" Width="100%"
                                                    runat="server" Text="Get" CssClass="cancelbutton" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="PRODUCTION_ORDER_NO" />
                                        <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="PRODUCTION_ORDER_DATE" />
                                        <%--<asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="EXPECTED_COMPLETION_DATE" />--%>
                                        <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
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
                    </fieldset>
                </asp:Panel>
                <%--PRODUCTION ORDER NO DETAIL END--%>


                <%--DMS DRAWING NO LIST START--%>
                <asp:Button ID="btnShowPopupDMSDrawingNoList" runat="server" Style="display: none" />
                <ajax:ModalPopupExtender ID="mpeDMSDrawingNoList" runat="server" TargetControlID="btnShowPopupDMSDrawingNoList"
                    PopupControlID="pnlPopupDMSDrawingNoList" CancelControlID="imgBtnCancelDMSDrawingNoList" BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlPopupDMSDrawingNoList" runat="server" BackColor="White" Height="600px" Width="800px"
                    Style="display: block">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnCancelDMSDrawingNoList" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <div align="center">
                        <fieldset style="width: 90%; margin-top: 10px;">
                            <legend style="text-align: center;">
                                <asp:Label ID="lblDMSDrawingNoListRecords" runat="server" Text="Records[0]" /></legend>
                            <br />
                            <asp:Label ID="lblDMSDrawingNoListMsg" runat="server" />
                            <br />
                            <table style="width: 95%; margin-left: 5%;">
                                <tr>
                                    <td>JOB No.:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJOBNoSearchDMS" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSearchDMSDrawingNo" CssClass="button" runat="server" Text="Search"
                                            Width="100%" OnClick="btnSearchDMSDrawingNo_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray; margin-left: 5px;'>
                                <div align="center">
                                    <asp:GridView ID="gvDMSDrawingNoList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                                        OnRowCommand="gvDMSDrawingNoList_RowCommand">
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="GET">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                                    <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                                    <asp:Button ID="btnGetDMSDrawingNo" CommandArgument="GET" ToolTip="Get DMS Drawing No" Width="100%"
                                                        runat="server" Text="Get" CssClass="cancelbutton" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                                            <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
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
                        </fieldset>
                    </div>

                </asp:Panel>
                <%--DMS DRAWING NO LIST END--%>

                <%--</ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAddUpdateSubitemToList" />
                    </Triggers>
                </asp:UpdatePanel>--%>


                <table width="95%" align="center">
                    <tr>
                        <td style="width: 15%;">Important Notes:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtNotesToEdit" runat="server" Width="100%" TextMode="MultiLine" Rows="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>


                <table width="95%" align="center">
                    <tr>
                        <td style="width: 15%;">Attachment:</td>
                        <td style="width: 30%;">
                            <asp:FileUpload ID="uploadFileAttachment1ToEdit" runat="server" Width="100%"
                                Height="29px" BorderStyle="Groove" />
                        </td>
                        <td>&nbsp;</td>

                        <td colspan="2">
                            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="3">
                            <div id="divfileAttachment1ToEdit" style="display: none;">
                                <asp:Label ID="lblfileAttachment1ToEdit" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>


                <asp:UpdatePanel runat="server" ID="UpdatePanel1" Visible="false">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAddUpdateAttachments" runat="server" Width="25%" Text="Add Additional Attachments" CssClass="button" OnClick="btnAddUpdateAttachments_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                        <%--ADD ATTACHMENTS START--%>
                        <asp:Button ID="btnShowPopupAddAttachments" runat="server" Style="display: none" />
                        <ajax:ModalPopupExtender ID="mpeAddUpdateAttachments" runat="server" TargetControlID="btnShowPopupAddAttachments"
                            PopupControlID="pnlPopupAddAttachments" CancelControlID="imgBtnCancelAddAttachments" BackgroundCssClass="modalBackground">
                        </ajax:ModalPopupExtender>
                        <asp:Panel ID="pnlPopupAddAttachments" runat="server" BackColor="White" Height="300px" Width="1000px"
                            Style="display: block">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnCancelAddAttachments" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <fieldset style="width: 90%; margin-left: 5%; margin-top: 10px;">
                                <legend style="text-align: center;">Add/Update Additional Attachments</legend>

                                <table style="width: 100%;">
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td>Attachment 1:</td>
                                        <td>
                                            <%--<asp:FileUpload ID="uploadFileAttachment1ToEdit" runat="server" Width="100%"
                                                Height="29px" BorderStyle="Groove" />--%>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>Attachment 2:</td>
                                        <td>
                                            <asp:FileUpload ID="uploadFileAttachment2ToEdit" runat="server" Width="100%"
                                                Height="29px" BorderStyle="Groove" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <%--<div id="divfileAttachment1ToEdit" style="display: none;">
                                                <asp:Label ID="lblfileAttachment1ToEdit" runat="server" ForeColor="Red" />
                                            </div>--%>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                        <td>
                                            <div id="divfileAttachment2ToEdit" style="display: none;">
                                                <asp:Label ID="lblfileAttachment2ToEdit" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Attachment 3:</td>
                                        <td>
                                            <asp:FileUpload ID="uploadFileAttachment3ToEdit" runat="server" Width="100%"
                                                Height="29px" BorderStyle="Groove" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>Attachment 4:</td>
                                        <td>
                                            <asp:FileUpload ID="uploadFileAttachment4ToEdit" runat="server" Width="100%"
                                                Height="29px" BorderStyle="Groove" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <div id="divfileAttachment3ToEdit" style="display: none;">
                                                <asp:Label ID="lblfileAttachment3ToEdit" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                        <td>
                                            <div id="divfileAttachment4ToEdit" style="display: none;">
                                                <asp:Label ID="lblfileAttachment4ToEdit" runat="server" ForeColor="Red" />
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnAddUpdateAttachmentsToList" CssClass="button" runat="server" Text="Save Attachments"
                                                Width="100%" OnClick="btnAddUpdateAttachmentsToList_Click" OnClientClick="return ValidateAllAttachments();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                            </fieldset>
                        </asp:Panel>
                        <%--ADD ATTACHMENTS END--%>

                        <table width="95%" align="center">
                            <tr>
                                <td>
                                    <div align="center">
                                        <fieldset style="width: 100%;">
                                            <div style='overflow-y: scroll; overflow-x: scroll; width: 1000px; height: 100%; border: 1px solid lightgray;'>
                                                <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                                    OnRowCommand="gvAttachments_RowCommand" OnRowDataBound="gvAttachments_RowDataBound">
                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("ATTACHMENT1_NAME") %>' Visible="false" />
                                                                <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("ATTACHMENT2_NAME") %>' Visible="false" />
                                                                <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("ATTACHMENT3_NAME") %>' Visible="false" />
                                                                <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("ATTACHMENT4_NAME") %>' Visible="false" />

                                                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove" ImageUrl="~/Images/Icons/REMOVE03.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="ATTACHMENT1_NAME" HeaderText="Attachment1" />
                                                        <asp:BoundField DataField="ATTACHMENT2_NAME" HeaderText="Attachment2" />
                                                        <asp:BoundField DataField="ATTACHMENT3_NAME" HeaderText="Attachment3" />
                                                        <asp:BoundField DataField="ATTACHMENT4_NAME" HeaderText="Attachment4" />
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
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAddUpdateAttachmentsToList" />
                    </Triggers>
                </asp:UpdatePanel>


                <%--<table width="95%" align="center">
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>--%>



                <br />
            </div>
        </fieldset>


    </asp:Panel>
    <%-- REVISE LOT STATUS END --%>






    <%-- SHOW IMAGE FILE START--%>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowImageFile" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="700px" Width="1100px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div style='overflow: auto; width: 1050px; height: 600px; border: 1px solid lightgray; margin-left: 25px;'>
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <%-- SHOW IMAGE FILE END--%>


    <%-- SHOW PDF FILE START--%>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowPDFFile" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="700px"
        Width="1100px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1100px; height: 600px;" id="iframeViewPDFFile"
            runat="server">
            <div style='overflow: auto; width: 1100px; height: 600px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%-- SHOW PDF FILE END--%>


    <%-- VIEW DETAIL IN PDF START--%>
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
    <%-- VIEW DETAIL IN PDF END--%>


    <%-- SHOW SUBITEM DETAIL START--%>
    <asp:Button ID="btnShowSubitemDetailFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeSubitemDetail" runat="server" TargetControlID="btnShowSubitemDetailFile"
        PopupControlID="pnlViewSubitemDetailPopup" CancelControlID="imgBtnCancelSubitemDetailFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewSubitemDetailPopup" runat="server" BackColor="White" Height="600px" Width="1100px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelSubitemDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <%--<div style='overflow: auto; width: 99%; height: 300px; border: 1px solid lightgray; margin-left: 5px;'>--%>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">Subitem Details</legend>
            <table style="width: 80%;" align="center">
                <tr>
                    <td>TF No.:</td>
                    <td>
                        <asp:TextBox ID="txtTFNoSI" runat="server" Width="100%" Enabled="false" />
                    </td>
                    <td>&nbsp;</td>
                    <td>JOB No.:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNoSI" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
            </table>
            <br />

            <div style="margin-left: 10px;">
                <fieldset style="width: 100%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblSubitemsSIRerords" runat="server" Text="Subitems Records[0]" /></legend>
                    <div style='overflow-y: scroll; overflow-x: scroll; width: 100%; height: 370px; border: 1px solid lightgray;'>
                        <asp:GridView ID="gvSubitemsSI" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowCommand="gvSubitemsSI_RowCommand" OnRowDataBound="gvSubitemsSI_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                        <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />
                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />--%>

                                        <asp:Label ID="lblLOTTFID" runat="server" Text='<%# Eval("LOT_TF_ID") %>' Visible="false" />
                                        <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                                        <asp:Label ID="lblLOTTFSubitemID" runat="server" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' Visible="false" />

                                        <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Drawing (.pdf)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                                        <asp:ImageButton ID="imgBtnAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Drawing (.dwg/.dxf)" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                                        <asp:ImageButton ID="imgBtnAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Drawing.3" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' />
                                        <asp:ImageButton ID="imgBtnAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Drawing.4" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' />
                                        <asp:ImageButton ID="imgBtnAttachment4" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT4"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnStatus" CommandArgument="STATUS" runat="server" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                                <asp:TemplateField HeaderText="Tag No.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTagNoInList" runat="server" Text='<%# Eval("TAG_NO") %>' Style="text-transform: uppercase" Width="150PX"
                                            CssClass="textboxtagno" MaxLength="15" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="DRAWING_NO" HeaderText="Drg/DOC.No" />

                                <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />


                                <asp:TemplateField HeaderText="Rev.No.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Width="30px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="30px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No." />
                                <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Completion Required By" />

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
        </fieldset>
        <%--</div>--%>
    </asp:Panel>
    <%-- SHOW SUBITEM DETAIL END--%>





    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
