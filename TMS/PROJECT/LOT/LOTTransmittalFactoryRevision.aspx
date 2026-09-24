<%@ Page Title="CIPLTMS-Transmittal To Factory List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTTransmittalFactoryRevision.aspx.cs" Inherits="PROJECT_LOT_LOTTransmittalFactoryRevision" %>

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


            if (RevNo != 11) {
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




            if (ValidateProductionOrderNo()) {
                check = false;
            }

            if (ValidateProductionOrderDate()) {
                check = false;
            }

            if (ValidateProductCode()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }

            if (ValidateProductDesc()) {
                check = false;
            }


            if (ValidateExpectedCompletionDate()) {
                check = false;
            }


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

            if ((parseInt(hdAddSubitemFlag) == 0 ||
                parseInt(hdNewSubitemUpdationFlag) == 0) &&
                (parseInt(hdSubitemReviseFlag) > 0 ||
                    parseInt(hdRevisedSubitemUpdationFlag) > 0)) {
                if (ValidateRevNo()) {
                    check = false;
                }
            }

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


    <script type="text/javascript">

        function EnableRevisionNoTextboxOther() {
            var hdRevNoText = document.getElementById('<%=hdRevNoTextToEdit.ClientID %>').value;
            var hdRevNoTextOld = document.getElementById('<%=hdRevNoTextOldToEdit.ClientID %>').value;

            var RevNo = document.getElementById('<%=ddlRevNoToEdit.ClientID %>');
            var RevNoIndex = document.getElementById('<%=ddlRevNoToEdit.ClientID %>').selectedIndex;
            var RevNoText = RevNo.options[RevNo.selectedIndex].innerHTML;

            if (RevNoIndex == 11) {
                document.getElementById('<%=txtRevNoTextToEdit.ClientID %>').disabled = false;

                document.getElementById('<%=txtRevNoTextToEdit.ClientID %>').value = hdRevNoTextOld;
            }
            else {
                document.getElementById('<%=txtRevNoTextToEdit.ClientID %>').disabled = true;

                document.getElementById('<%=txtRevNoTextToEdit.ClientID %>').value = RevNoText;
                document.getElementById('<%=hdRevNoTextToEdit.ClientID %>').value = RevNoText;
            }
        }

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



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>LOT Revision:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch"
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date</label>
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
                        </tr>
                    </table>

                    <label>TF No.</label>
                    <asp:TextBox ID="txtTFNo" runat="server"
                        CssClass="form-control" />


                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control" />

                    <label>Status.</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Company:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server"
                        CssClass="form-control" />

                    <label>LOT For</label>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainSubitems" runat="server"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Tag Number</label>
                    <asp:TextBox ID="txtTagNumber" runat="server" CssClass="form-control" />

                    <label>Drawing Number</label>
                    <asp:TextBox ID="txtDrawingNumber" runat="server" CssClass="form-control" />

                    <div class="full-width button-group">

                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllSearch();" />

                    </div>
                </div>
            </fieldset>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView
                CssClass="employee-grid"
                ID="gvLOTTFList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvLOTTFList_RowCommand" OnRowDataBound="gvLOTTFList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="Revise" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
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

                            <asp:ImageButton ID="imgBtnRevise" CommandArgument="REVISE" runat="server" ImageUrl="~/Images/LOT/revise1.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subitem" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnViewSubitemDetail" Height="20px" Width="20px" CommandArgument="ViewSubitemDETAIL"
                                runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View Subitem Details" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/pdficon3.png" ToolTip="View LOT Detail in PDF" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Additional Att." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment1" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.2" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment2" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.3" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT3_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment3" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.4" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT4_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment4" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT4"
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

    </div>




    <%-- REVISE LOT STATUS START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeReviseLOT" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdLOTMainSubitemID" runat="server" />
        <asp:HiddenField ID="hdProductionOrderNo" runat="server" />
        <asp:HiddenField ID="hdPONo" runat="server" />
        <asp:HiddenField ID="hdTFNo" runat="server" />


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend style="text-align: center;">TF No. [<asp:Label ID="lblTFNo" runat="server" />] Details</legend>

                <div class="form-grid form-grid-2">

                    <label>Old TFNo.</label>
                    <asp:TextBox ID="txtOldTFNoToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Company</label>
                    <asp:HiddenField ID="hdCompanyToEdit" runat="server" />
                    <asp:TextBox ID="txtCompanyToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>JOB Number</label>
                    <asp:TextBox ID="txtJOBNoToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" onblur="return ValidateJOBNo();" />


                    <label>Customer PO Number</label>
                    <asp:TextBox ID="txtPONoToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" onblur="return ValidatePONo();" />


                    <label>Customer Name</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustomerNameToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCodeToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Date</label>
                    <asp:TextBox ID="txtDateToEdit" runat="server" onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control"></asp:TextBox>


                    <label>TF Number</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtTFNoToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetTFno" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClientClick="return ValidateJobNoForFT();" OnClick="btnGetTFno_Click" />
                            </td>
                        </tr>
                    </table>


                    <%--<div class="full-width">--%>

                    <label>Item</label>
                    <asp:TextBox ID="txtItemNameToEdit" runat="server" Enabled="false" CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />
                    <%--</div>--%>

                    <%--<label>Tag All</label>
                            <asp:CheckBox ID="chkTagAll" runat="server" AutoPostBack="true"
                                OnCheckedChanged="chkTagAll_CheckedChanged" />--%>

                    <table style="width: 100%; visibility: hidden;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAddSubitem" runat="server"
                                    Text="Add Subitem"
                                    CssClass="button"
                                    OnClick="btnAddSubitem_Click" />
                            </td>
                        </tr>
                    </table>


                    <div class="full-width">

                        <div class="employee-grid-container">

                            <fieldset class="filter-card">
                                <legend>
                                    <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" />
                                </legend>

                                <asp:GridView
                                    CssClass="employee-grid"
                                    ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                    OnRowCommand="gvSubItem_RowCommand" OnRowDataBound="gvSubItem_RowDataBound">
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Transfer"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>

                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("UNIT_ID") %>' Visible="false" />
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

                                                <asp:Label ID="lblProductionManagerID" runat="server" Text='<%# Eval("PRODUCTION_MANAGER_ID") %>' Visible="false" />
                                                <asp:Label ID="lblQualityManagerID" runat="server" Text='<%# Eval("QUALITY_MANAGER_ID") %>' Visible="false" />

                                                <%--<asp:ImageButton ID="imgBtnRevise" CommandArgument="REVISE" runat="server"
                                                            ToolTip="Transfer of LOT"
                                                            ImageUrl="~/Images/LOT/transfer.png" Height="35px" Width="35px" />--%>

                                                <asp:CheckBox ID="chkSelectForTransfer" runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit"
                                                            ImageUrl="~/Images/LOT/edit5.png" Width="35px" Height="35px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                                            ImageUrl="~/Images/Icons/REMOVE03.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                                        <%--<asp:TemplateField HeaderText="Production Manager"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlProductionManager" runat="server" Width="100%" Height="25px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quality Manager"
                                                    HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlQualityManager" runat="server" Width="100%" Height="25px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>



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

                                                <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO_TEXT") %>' Width="40PX"
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

                            </fieldset>


                        </div>
                    </div>

                    <label>Important Notes</label>
                    <asp:TextBox ID="txtNotesToEdit" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />

                    <label>Transfer Remarks</label>
                    <asp:TextBox ID="txtRemarksToEdit" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />

                    <label>Attachment</label>
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment1ToEdit" runat="server"
                                    CssClass="form-control"
                                    BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                                        <asp:Button ID="btnAddUpdateAttachments" runat="server" Width="25%" Text="Add Additional Attachments"
                                            CssClass="button" OnClick="btnAddUpdateAttachments_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>

                            <%--ADD ATTACHMENTS START--%>
                            <%--ADD ATTACHMENTS END--%>

                            <div class="full-width">

                                <div class="employee-grid-container">

                                    <fieldset class="filter-card">

                                        <asp:GridView
                                            CssClass="employee-grid"
                                            ID="gvAttachments" runat="server" AutoGenerateColumns="False" CellPadding="4"
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

                                    </fieldset>

                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAddUpdateAttachmentsToList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSave" runat="server" Width="100%"
                    Text="Transfer" CssClass="button"
                    OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlReviseMsg" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblReviseMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>




    </asp:Panel>
    <%-- REVISE LOT STATUS END --%>


    <%--ADD SUBITEM START--%>
    <asp:Button ID="btnShowPopupAddSubitems" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddSubitems" runat="server" TargetControlID="btnShowPopupAddSubitems"
        PopupControlID="pnlPopupAddSubitems" CancelControlID="imgBtnCancelAddSubitems" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddSubitems" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddSubitems" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdSRNo" runat="server" />
        <asp:HiddenField ID="hdNewSubitemUpdationFlag" runat="server" Value="0" />
        <asp:HiddenField ID="hdRevisedSubitemUpdationFlag" runat="server" Value="0" />
        <asp:HiddenField ID="hdSubitemReviseFlag" runat="server" Value="0" />
        <asp:HiddenField ID="hdAddSubitemFlag" runat="server" Value="0" />

        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend style="text-align: center;" id="lgSubitemUpdation" runat="server" />

                <div class="form-grid form-grid-2">

                    <label>Production Order No.</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtProductionOrderNoToEdit" runat="server"
                                    CssClass="form-control" /></td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetProductionNumber" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetProductionNumber_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                        </tr>
                    </table>

                    <label>Production Order Date</label>
                    <asp:TextBox ID="txtProductionOrderDateToEdit" runat="server" CssClass="form-control"
                        Enabled="false" />

                    <label>Product Code</label>
                    <asp:DropDownList ID="ddlProductCodeToEdit" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProductCodeToEdit_SelectedIndexChanged" />

                    <label>UOM</label>
                    <asp:TextBox ID="txtUOMToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <label>Product Description</label>
                    <asp:TextBox ID="txtProductDescToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <b>
                        <label style="color: darkgreen;">Is Part of Production Status Report/Main Drawing?:</label></b>
                    <asp:CheckBox ID="chkIsPartOfProductionOrMainDrawingToEdit" runat="server" />

                    <label>Completion Required By</label>
                    <asp:TextBox ID="txtExpectedCompletionDateToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" /><%--onkeyDown="javascript:preventInput(event);"--%>


                    <label>LOT For</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                                <asp:DropDownList ID="ddlLOTMainItemsToEdit" runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLOTMainItemsToEdit_SelectedIndexChanged" />
                            </td>
                            <td style="width: 40%">
                                <asp:DropDownList ID="ddlLOTMainSubitemsToEdit" runat="server" Width="100%" Height="26px"
                                    onblur="return ValidateLOTMainSubItems();" />
                            </td>
                        </tr>
                    </table>

                    <label>Drg./Doc No.</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtDrgNoToEdit" runat="server"
                                    CssClass="form-control"
                                    Style="text-transform: uppercase" />

                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetDMSDrawingNo" runat="server" Width="100%" Text="DMS" CssClass="button"
                                    OnClick="btnGetDMSDrawingNo_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                        </tr>
                    </table>

                    <label>Description</label>
                    <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="2" runat="server"
                        CssClass="form-control" />
                    <label>Rev. No.</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%;">

                                <asp:DropDownList ID="ddlRevNoToEdit" runat="server"
                                    onchange="EnableRevisionNoTextboxOther()"
                                    CssClass="form-control">
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
                                    <asp:ListItem Text="99 – Any Other Revision" Value="99" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRevNoTextToEdit"
                                    runat="server"
                                    Width="100%"
                                    Text="00"
                                    onblur="return ValidateRevNo();"
                                    Enabled="false" />
                                <asp:HiddenField ID="hdRevNoTextToEdit" runat="server" />
                                <asp:HiddenField ID="hdRevNoTextOldToEdit" runat="server" />
                            </td>
                        </tr>
                    </table>

                    <label>Quantity</label>
                    <asp:HiddenField ID="hdQuantityToEdit" runat="server" />
                    <asp:TextBox ID="txtQuantityToEdit" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKey(this, event);" />

                    <asp:Label ID="lblRevNoToEditMsg" runat="server" ForeColor="Red" />

                    <label>Category (For Factory)</label>
                    <asp:CheckBoxList ID="chkLstCategoryToEdit" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                        onblur="return ValidateCategory();" />

                    <label>Tag No.</label>
                    <asp:TextBox ID="txtTagNoToEdit" runat="server"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />

                    <div class="full-width">

                        <asp:Panel ID="pnlAttachFiles" runat="server">

                            <label>Drawing (.pdf)</label>
                            <asp:Panel ID="pnlViewSiDrawingToEdit1" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdSiDrawingToEdit1" runat="server" Value="0" />
                                            <asp:TextBox ID="txtSiDrawingToEdit1" runat="server"
                                                Enabled="false" CssClass="textboxdrawings" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewSiDrawingToEdit1" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit1_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit1" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit1_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadSiDrawingToEdit1" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="hdUploadSiDrawingToEdit1" runat="server" Value="0" />
                                                        <asp:FileUpload ID="uploadFileSiDrawingToEdit1" runat="server"
                                                            CssClass="form-control"
                                                            BorderStyle="Groove" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div id="divFileSiDrawingToEdit1" style="display: none;">
                                                            <asp:Label ID="lblFileSiDrawingToEdit1" runat="server" ForeColor="Red" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit1" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit1_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>


                            <label>Drawing (.dwg/.dxf)</label>
                            <asp:Panel ID="pnlViewSiDrawingToEdit2" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdSiDrawingToEdit2" runat="server" Value="0" />
                                            <asp:TextBox ID="txtSiDrawingToEdit2" runat="server"
                                                CssClass="form-control"
                                                Enabled="false" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewSiDrawingToEdit2" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit2_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit2" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit2_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadSiDrawingToEdit2" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="hdUploadSiDrawingToEdit2" runat="server" Value="0" />
                                                        <asp:FileUpload ID="uploadFileSiDrawingToEdit2" runat="server"
                                                            CssClass="form-control"
                                                            BorderStyle="Groove" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div id="divFileSiDrawingToEdit2" style="display: none;">
                                                            <asp:Label ID="lblFileSiDrawingToEdit2" runat="server" ForeColor="Red" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>


                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit2" runat="server" Height="20px" Width="20px"
                                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit2_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>


                            <label>Drawing 3</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawingToEdit3" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 80%;">
                                                <asp:HiddenField ID="hdSiDrawingToEdit3" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawingToEdit3" runat="server" Width="100%"
                                                    Enabled="false" CssClass="textboxdrawings" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit3" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit3_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit3" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit3_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlUploadSiDrawingToEdit3" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">

                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawingToEdit3" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawingToEdit3" runat="server"
                                                                CssClass="form-control"
                                                                BorderStyle="Groove" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawingToEdit3" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawingToEdit3" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit3" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit3_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>


                            <label>Drawing 4</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawingToEdit4" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 75%;">
                                                <asp:HiddenField ID="hdSiDrawingToEdit4" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawingToEdit4" runat="server"
                                                    CssClass="form-control"
                                                    Enabled="false" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawingToEdit4" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit4_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit4" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit4_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlUploadSiDrawingToEdit4" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">

                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawingToEdit4" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawingToEdit4" runat="server"
                                                                CssClass="form-control"
                                                                BorderStyle="Groove" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawingToEdit4" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawingToEdit4" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit4" runat="server" Height="20px" Width="20px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawingToEdit4_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                        </asp:Panel>

                    </div>

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateSubitemToList" CssClass="button" runat="server" Text="Add Subitem"
                    Width="100%" OnClick="btnAddUpdateSubitemToList_Click" OnClientClick="return ValidateAddSubitem();" />
            </div>
            <div class="full-width">
                <asp:Panel ID="pnlAddUpdatedSubitemsMsg" Visible="false" runat="server" Height="50px">
                    <asp:TextBox ID="txtAddUpdatedSubitemsMsg" runat="server" Font-Bold="True" Font-Size="Large"
                        TextMode="MultiLine" Rows="2"
                        Enabled="false" Width="100%" CssClass="text-center" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>
    <%--ADD SUBITEM END--%>




    <%--PRODUCTION ORDER NO DETAIL START--%>
    <asp:Button ID="btnShowPopupProductonOrderNoDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeProductonOrderNoDetail" runat="server" TargetControlID="btnShowPopupProductonOrderNoDetail"
        PopupControlID="pnlPopupProductonOrderNoDetail" CancelControlID="imgBtnCancelProductonOrderNoDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupProductonOrderNoDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelProductonOrderNoDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>Material Requisition List:
                    <asp:Label ID="lblProductonOrderNoRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearchPON" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Production Order No.</label>
                        <asp:TextBox ID="txtProductionOrderNoSearchPON" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchProductionOrderNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchProductionOrderNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblProductonOrderNoMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvProductonOrderNoDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
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

    </asp:Panel>
    <%--PRODUCTION ORDER NO DETAIL END--%>


    <%--DMS DRAWING NO LIST START--%>
    <asp:Button ID="btnShowPopupDMSDrawingNoList" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDMSDrawingNoList" runat="server" TargetControlID="btnShowPopupDMSDrawingNoList"
        PopupControlID="pnlPopupDMSDrawingNoList" CancelControlID="imgBtnCancelDMSDrawingNoList" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupDMSDrawingNoList" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDMSDrawingNoList" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblDMSDrawingNoListRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearchDMS" runat="server" CssClass="form-control" Enabled="false" />

                        <asp:Button ID="btnSearchDMSDrawingNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchDMSDrawingNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblDMSDrawingNoListMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvDMSDrawingNoList" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
    </asp:Panel>
    <%--DMS DRAWING NO LIST END--%>




    <%--ADD ATTACHMENTS START--%>
    <asp:Button ID="btnShowPopupAddAttachments" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddUpdateAttachments" runat="server" TargetControlID="btnShowPopupAddAttachments"
        PopupControlID="pnlPopupAddAttachments" CancelControlID="imgBtnCancelAddAttachments" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddAttachments" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddAttachments" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend style="text-align: center;">Add/Update Additional Attachments</legend>

                <div class="form-grid form-grid-2">

                    <label>Attachment 2</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment2ToEdit" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment2ToEdit" style="display: none;">
                                    <asp:Label ID="lblfileAttachment2ToEdit" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>


                    <label>Attachment 3</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment3ToEdit" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment3ToEdit" style="display: none;">
                                    <asp:Label ID="lblfileAttachment3ToEdit" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>


                    <label>Attachment 4</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment4ToEdit" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment4ToEdit" style="display: none;">
                                    <asp:Label ID="lblfileAttachment4ToEdit" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateAttachmentsToList" CssClass="button" runat="server" Text="Save Attachments"
                    Width="100%" OnClick="btnAddUpdateAttachmentsToList_Click" OnClientClick="return ValidateAllAttachments();" />
            </div>

        </div>
    </asp:Panel>
    <%--ADD ATTACHMENTS END--%>



    <%-- SHOW IMAGE FILE START--%>
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
    <%-- SHOW IMAGE FILE END--%>


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
            runat="server">
        </iframe>
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
            id="iframeViewTravelStatementInPDF"
            runat="server">
        </iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>


    <%-- SHOW SUBITEM DETAIL START--%>
    <asp:Button ID="btnShowSubitemDetailFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeSubitemDetail" runat="server" TargetControlID="btnShowSubitemDetailFile"
        PopupControlID="pnlViewSubitemDetailPopup" CancelControlID="imgBtnCancelSubitemDetailFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewSubitemDetailPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelSubitemDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>Subitem Details:
                    <asp:Label ID="lblSubitemsSIRerords" runat="server" Text="Subitems Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>TF No.</label>
                        <asp:TextBox ID="txtTFNoSI" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSI" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvSubitemsSI" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
                                <asp:ImageButton ID="imgBtnAttachment1" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
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
                                <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO_TEXT") %>' Width="30px"
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

        </div>

        <%--</div>--%>
    </asp:Panel>
    <%-- SHOW SUBITEM DETAIL END--%>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
