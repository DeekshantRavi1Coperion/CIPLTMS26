<%@ Page Title="CIPLTMS- Assign Items To Machines" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AssignedItemsListForMS.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_AssignedItemsListForMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>


    <script type="text/javascript">

        function pageLoad() {
            document.getElementById('<%=txtDateToA.ClientID %>').value = document.getElementById('<%=hdDateToA.ClientID %>').value;
        }

        function clientChangedDate(sender, args) {
            document.getElementById('<%=hdDateToA.ClientID %>').value = document.getElementById('<%=txtDateToA.ClientID %>').value;
        }

    </script>



    <script type="text/javascript">


        function ValidateCategory() {
            var Category = document.getElementById('<%=ddlCategoryToA.ClientID %>').selectedIndex;
            if (Category == '' || Category == 0) {
                document.getElementById('<%=ddlCategoryToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateJOBNo() {
            var JOBNo = document.getElementById('<%=txtJOBNoToA.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingNo() {
            var DrawingNo = document.getElementById('<%=txtDrawingNoToA.ClientID %>').value;
            if (DrawingNo == '') {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEquipment() {
            var Equipment = document.getElementById('<%=txtEquipmentToA.ClientID %>').value;
            if (Equipment == '') {
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateTagNo() {
            var TagNo = document.getElementById('<%=txtTagNoToA.ClientID %>').value;
            if (TagNo == '') {
                document.getElementById('<%=txtTagNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTagNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemName() {
            var ItemName = document.getElementById('<%=txtItemNameToA.ClientID %>').value;
            if (ItemName == '') {
                document.getElementById('<%=txtItemNameToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemNameToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateItemDetail() {
            var ItemDetail = document.getElementById('<%=txtItemDetailToA.ClientID %>').value;
            if (ItemDetail == '') {
                document.getElementById('<%=txtItemDetailToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemDetailToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;
            if (Quantity == '' || Quantity == 0) {
                document.getElementById('<%=txtQuantityToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantityToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatefileAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF", ".dxf", ".dwg", ".DXF", ".DWG"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment1 = document.getElementById('<%=uploadFileAttachment1.ClientID %>').value;
            var divfileAttachment1 = document.getElementById("divfileAttachment1");
            var lblfileAttachment1 = document.getElementById('<%=lblfileAttachment1.ClientID %>');


            if (fileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                divfileAttachment1.style.display = "none";
                lblfileAttachment1.innerHTML = "";
                return false;
            }
            else {

                fileAttachment1 = fileAttachment1.split(" ").join("")
                fileAttachment1 = fileAttachment1.split("(").join("")
                fileAttachment1 = fileAttachment1.split(")").join("")

                if (!regex.test(fileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment1.style.display = "block";
                    lblfileAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .dwg, .dxf file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                    divfileAttachment1.style.display = "none";
                    lblfileAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            var categoryVal = document.getElementById('<%=ddlCategoryToA.ClientID %>').selectedIndex;

            if (ValidateJOBNo()) {
                check = false;
            }



            if (ValidateCategory()) {
                check = false;
            }

            if (ValidateDrawingNo()) {
                check = false;
            }

            if (categoryVal == 1) {
                if (ValidateEquipment()) {
                    check = false;
                }

                if (ValidateTagNo()) {
                    check = false;
                }
            }


            if (ValidateQuantity()) {
                check = false;
            }

            if (ValidateItemName()) {
                check = false;
            }

            if (ValidateItemDetail()) {
                check = false;
            }

            if (ValidatefileAttachment1()) {
                check = false;
            }


            <%--if (check) {
                if (confirm("Would you like to save edited details?")) {
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
            }--%>

            return check;
        }


        function ValidateAllAccept() {
            if (confirm("Would you like to accept drawing for machine scheduling?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }

        function ValidateAllReject() {
            if (confirm("Would you like to reject drawing for machine scheduling?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }

        function ValidateAllSchedule() {
            if (confirm("Would you like to schedule machines?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }

        function ValidateAllCompleteScheduling() {
            if (confirm("Would you like to complete scheduling of machines?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }

        function ValidateAllModifyWorker() {
            if (confirm("Would you like to modify worker?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }


        function ValidateInspectionRejectedRemarks() {
            var rejectedQuantity = document.getElementById('<%=txtRejectedQuantityToQaInsp.ClientID %>').value;
            var remarks = document.getElementById('<%=txtInspectedRejectedRemarksToQaInsp.ClientID %>').value;


            if (parseInt(rejectedQuantity) > 0) {
                if (remarks == '') {
                    document.getElementById('<%=txtInspectedRejectedRemarksToQaInsp.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtInspectedRejectedRemarksToQaInsp.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtInspectedRejectedRemarksToQaInsp.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAllSaveInspection() {

            var check = true;

            if (ValidateInspectionRejectedRemarks()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to submit inspection?")) {
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


    </script>


    <script type="text/javascript">

        function ValidateAllJobNo() {
            var check = true;

            if (ValidateJOBNo()) {
                check = false;
            }


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
        window.onload = function () {
            var div = document.getElementById("dvAvailableDatesScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>





    <script type="text/javascript">
        function IncreaseQuantity() {
            var quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;

            document.getElementById('<%=txtQuantityToA.ClientID %>').value = parseInt(quantity) + 1;
        }

        function DecreaseQuantity() {
            var quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;

            if (quantity > 0) {
                document.getElementById('<%=txtQuantityToA.ClientID %>').value = parseInt(quantity) - 1;
            }
            else {
                document.getElementById('<%=txtQuantityToA.ClientID %>').value = "0";
            }

        }



        <%--function EnableDisableDrawingValues() {
            var chkVal = document.getElementById('<%=chkNA.ClientID %>').checked;


            document.getElementById('<%=hdLOTTFSubitemID.ClientID %>').value = "0";
            document.getElementById('<%=txtDrawingNoToA.ClientID %>').value = "";
            document.getElementById('<%=txtEquipmentToA.ClientID %>').value = "";
            document.getElementById('<%=txtQuantityToA.ClientID %>').value = "0";

            if (chkVal) {
                document.getElementById('<%=btnGetDrawingNo.ClientID %>').style.visibility = 'hidden';
            }
            else {
                document.getElementById('<%=btnGetDrawingNo.ClientID %>').style.visibility = 'visible';
            }

        }--%>
    </script>


    <script type="text/javascript">

        function clientChangedScheduleMonth(sender, args) {

            document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value = document.getElementById('<%=txtScheduledFromSchD.ClientID %>').value;
            document.getElementById('<%=hdScheduledToSchD.ClientID %>').value = document.getElementById('<%=txtScheduledToSchD.ClientID %>').value;

           <%-- document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value = "01-" + document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value;
            document.getElementById('<%=hdScheduledToSchD.ClientID %>').value = "01-" + document.getElementById('<%=hdScheduledToSchD.ClientID %>').value;--%>


            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value.split("-");

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
            var endDateItems = document.getElementById('<%=hdScheduledToSchD.ClientID %>').value.split("-");
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

        function clientChangedScheduleDatesToCMSch(sender, args) {

            document.getElementById('<%=hdScheduledFromToCMSch.ClientID %>').value = document.getElementById('<%=txtScheduledFromToCMSch.ClientID %>').value;
            document.getElementById('<%=hdScheduledToToCMSch.ClientID %>').value = document.getElementById('<%=txtScheduledToToCMSch.ClientID %>').value;



            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdScheduledFromToCMSch.ClientID %>').value.split("-");

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
            var endDateItems = document.getElementById('<%=hdScheduledToToCMSch.ClientID %>').value.split("-");
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


        function clientChangedCompletionDateToCMSch(sender, args) {
            document.getElementById('<%=hdCompletionDateToCMSch.ClientID %>').value = document.getElementById('<%=txtCompletionDateToCMSch.ClientID %>').value;
        }

    </script>


    <script type="text/javascript" language="javascript">


        function onCalendarShownFrom() {
            var cal = $find("calendarScheduledFromSchD");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function onCalendarHiddenFrom() {
            var cal = $find("calendarScheduledFromSchD");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function callFrom(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarScheduledFromSchD");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <script type="text/javascript" language="javascript">


        function onCalendarShownTo() {
            var cal = $find("calendarScheduledToSchD");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callTo);
                    }
                }
            }
        }

        function onCalendarHiddenTo() {
            var cal = $find("calendarScheduledToSchD");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callTo);
                    }
                }
            }
        }

        function callTo(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarScheduledToSchD");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>

    <script type="text/javascript">

        <%--function ValidateTypeOfWorkToMSch() {

            var TypeOfWork = document.getElementById('<%=ddlTypeOfWorkToMSch.ClientID %>').selectedIndex;
            if (TypeOfWork == '' || TypeOfWork == 0) {
                document.getElementById('<%=ddlTypeOfWorkToMSch.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfWorkToMSch.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>
        function ValidateActivityToMSch() {

            var Activity = document.getElementById('<%=ddlActivityToMSch.ClientID %>').selectedIndex;
            if (Activity == '' || Activity == 0) {
                document.getElementById('<%=ddlActivityToMSch.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlActivityToMSch.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        <%--function ValidateMachineToMSch() {

            var Machine = document.getElementById('<%=ddlMachineToMSch.ClientID %>').selectedIndex;
            if (Machine == '' || Machine == 0) {
                document.getElementById('<%=ddlMachineToMSch.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMachineToMSch.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        function ValidateAllMachineToMSch() {
            var check = true;


            //if (ValidateTypeOfWorkToMSch()) {
            //    check = false;
            //}

            if (ValidateActivityToMSch()) {
                check = false;
            }

            //if (ValidateMachineToMSch()) {
            //    check = false;
            //}


            return check;
        }




        function ValidateMachineSchD() {

            var Machine = document.getElementById('<%=ddlMachineSchD.ClientID %>').selectedIndex;
            if (Machine == '' || Machine == 0) {
                document.getElementById('<%=ddlMachineSchD.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMachineSchD.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllMachineSchD() {
            var check = true;

            if (clientChangedScheduleMonth()) {
                check = false;
            }

            if (ValidateMachineSchD()) {
                check = false;
            }

            return check;
        }



        function ValidateAllDateRangeToCMSch() {
            var check = true;

            if (clientChangedScheduleDatesToCMSch()) {
                check = false;
            }

            if (ValidateMachineSchD()) {
                check = false;
            }

            return check;
        }


    </script>

    <script type="text/javascript">
        function ClearDates() {

            document.getElementById('<%=hdScheduledFromToCMSch.ClientID %>').value = ""
            document.getElementById('<%=hdScheduledToToCMSch.ClientID %>').value = ""

            document.getElementById('<%=txtScheduledFromToCMSch.ClientID %>').value = ""
            document.getElementById('<%=txtScheduledToToCMSch.ClientID %>').value = ""

        }

        function IncreaseAcceptedQuantity() {
            var allocatedQuantity = document.getElementById('<%=txtAllocatedQuantityToQaInsp.ClientID %>').value;

            var quantity = document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value;

            if (allocatedQuantity > quantity) {
                document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value = parseInt(quantity) + 1;
            }
            else {
                document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value = allocatedQuantity;
            }


            document.getElementById('<%=txtRejectedQuantityToQaInsp.ClientID %>').value = allocatedQuantity - document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value;
        }

        function DecreaseAcceptedQuantity() {
            var allocatedQuantity = document.getElementById('<%=txtAllocatedQuantityToQaInsp.ClientID %>').value;

            var quantity = document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value;

            if (quantity > 0) {
                document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value = parseInt(quantity) - 1;
            }
            else {
                document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value = "0";
            }

            document.getElementById('<%=txtRejectedQuantityToQaInsp.ClientID %>').value = allocatedQuantity - document.getElementById('<%=txtAcceptedQuantityToQaInsp.ClientID %>').value;

        }

        <%--function Resize() {

            var height = document.getElementById('<%=txtHeight.ClientID %>').value;
            var width = document.getElementById('<%=txtWidth.ClientID %>').value;

            alert(height)

            getElementById('dvScroll').style.height = height;
        }--%>

    </script>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:HiddenField ID="hdModifyWorker" runat="server" Value="0" />
    <asp:HiddenField ID="hdViewScheduling" runat="server" Value="0" />
    <asp:HiddenField ID="hdCompleteScheduling" runat="server" Value="0" />--%>
    <asp:HiddenField ID="hdRowCommandFlags" runat="server" Value="0" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />
    <asp:HiddenField ID="hdRemovedIDs" runat="server" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Machine Scheduling- Assigned Items To Machine Shop:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" />

                    <label>JOB Number</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control" />

                    <label>LOT Number</label>
                    <asp:TextBox ID="txtLOTNo" runat="server" CssClass="form-control" />


                    <label>Drawing Number</label>
                    <asp:TextBox ID="txtDrawingNo" runat="server" CssClass="form-control" />

                    <label>Equipment</label>
                    <asp:TextBox ID="txtEquipment" runat="server" CssClass="form-control" />

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" runat="server" Width="100%" Text="Search" CssClass="button"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnAssign" runat="server" Width="100%" Text="Assign" CssClass="button"
                    PostBackUrl="~/PROJECT/MACHINE_SCH/AssignItemsForMS.aspx" />

            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView
                CssClass="employee-grid"
                ID="gvSubitemsList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvSubitemsList_RowCommand"
                OnRowDataBound="gvSubitemsList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>

                            <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" CssClass="textboxcenter" Text='<%# Eval("SR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" CommandArgument="VIEW_DETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png" Height="20PX" Width="20PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />
                            <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_FID") %>' />
                            <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_FID") %>' />
                            <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_FID") %>' />
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_FID") %>' />
                            <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CATEGORY_FID") %>' />
                            <asp:Label ID="lblAdditinoalDrawingFilePath" runat="server" Visible="false" Text='<%# Eval("ADD_DRAWING_FILE_NAME") %>' />
                            <asp:Label ID="lblLOTSIDrawigName1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                            <asp:Label ID="lblLOTSIDrawigName2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                            <asp:Label ID="lblScheduledCounts" runat="server" Visible="false" Text='<%# Eval("SCHEDULED_COUNTS") %>' />
                            <asp:Label ID="lblCompletedCounts" runat="server" Visible="false" Text='<%# Eval("COMPLETED_COUNTS") %>' />
                            <asp:Label ID="lblBalancedCounts" runat="server" Visible="false" Text='<%# Eval("BALANCE_COUNTS") %>' />

                            <asp:Label ID="lblAllocatedQuantity" runat="server" Visible="false" Text='<%# Eval("ALLOCATED_QUANTITY") %>' />
                            <asp:Label ID="lblQaInspectionAcceptedQuantity" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_QUANTITY") %>' />
                            <asp:Label ID="lblQaInspectionRejectedQuantity" runat="server" Visible="false" Text='<%# Eval("REJECTED_QUANTITY") %>' />

                            <asp:Label ID="lblAssignedBy" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_BY") %>' />
                            <asp:Label ID="lblAcceptedBy" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_BY") %>' />
                            <asp:Label ID="lblRejectedBy" runat="server" Visible="false" Text='<%# Eval("REJECTED_BY") %>' />
                            <asp:Label ID="lblScheduledBy" runat="server" Visible="false" Text='<%# Eval("SCHEDULED_BY") %>' />

                            <asp:Label ID="lblIsAssignedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ASSIGNED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAcceptedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ACCEPTED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsRejectedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_REJECTED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsScheduledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_SCHEDULED_MAIL_SENT") %>' />

                            <asp:ImageButton ID="imgBtnEdit" ImageUrl="~/Images/LOT/edit5.png" ToolTip="Edit Record"
                                runat="server" CommandArgument="EDIT" Width="20PX" Height="20PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Additinoal Drawing" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnViewAdditionalDrawing" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewADDDRAWING" Visible="false"
                                Width="20PX" Height="20PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing (.pdf)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnViewDrawing1" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewSIDRAWING1" Visible="false"
                                Width="20PX" Height="20PX" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing (.dwg/.dxf)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnViewDrawing2" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewSIDRAWING2" Visible="false"
                                Width="20PX" Height="20PX" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View Details" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnViewDetails" runat="server" ImageUrl="~/Images/viewdetails.png"
                                Width="20PX" Height="20PX"
                                ToolTip="View scheduling details" CommandArgument="VIEW_SCH_DETAILS" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnStatus" runat="server" ImageUrl="~/Images/pdficon1.png"
                                Enabled="false" Width="30PX" Height="30PX" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Modify" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnModify" ImageUrl="~/Images/LOT/edit.jpg" ToolTip="Modify worker"
                                runat="server" CommandArgument="MODIFY" Width="20PX" Height="20PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:Button ID="btnAcceptDrawings" CommandArgument="ACCEPT" ToolTip="Accept or Reject Drawings" runat="server"
                                Text="Accept/Reject" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            <asp:Button ID="btnSchedule" CommandArgument="SCHEDULING" ToolTip="Schedule machine for drawing" runat="server"
                                Text="Schedule" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px"
                                BackColor="LightCoral" />

                            <asp:Button ID="btnComplete" CommandArgument="COMPLETE" ToolTip="Complete Scheduling" runat="server"
                                Text="Complete" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px"
                                BackColor="#3366ff" />

                            <asp:Button ID="btnInspect" CommandArgument="INSPECT" ToolTip="Inspect Scheduling" runat="server"
                                Text="Inspect" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px"
                                BackColor="#660033" />

                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Drawing No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingNo" Width="200px" TextMode="MultiLine" runat="server" Enabled="false"
                                CssClass="textboxleftyellow"
                                Text='<%# Eval("DRAWING_NO") %>'
                                ToolTip='<%# Eval("DRAWING_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemName" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ITEM_NAME") %>'
                                ToolTip='<%# Eval("ITEM_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Detail">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemDetail" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ITEM_DETAIL") %>'
                                ToolTip='<%# Eval("ITEM_DETAIL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Allocated Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" Width="100%" runat="server" Enabled="false" CssClass="textboxrightsmall"
                                Text='<%# Eval("ALLOCATED_QUANTITY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Assigned Drawing Code">
                        <ItemTemplate>

                            <asp:TextBox ID="txtAssignedDrawingCode" Width="150px" runat="server" Enabled="false"
                                CssClass="textboxleftyellow" Text='<%# Eval("ASSIGNED_DRAWING_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>

                            <asp:TextBox ID="txtUnit" Width="70px" runat="server" Enabled="false" CssClass="textboxleftyellow" Text='<%# Eval("UNIT_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>

                            <asp:TextBox ID="txtCategory" Width="100px" runat="server" Enabled="false"
                                CssClass="textboxleftyellow" Text='<%# Eval("CATEGORY_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LOT No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLOTNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("TF_NO") %>' ToolTip='<%# Eval("TF_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtJOBNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("JOB_NO") %>'
                                ToolTip='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Equipment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEquipment" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("EQUIPMENT") %>'
                                ToolTip='<%# Eval("EQUIPMENT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tag No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTagNo" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("TAG_NO") %>'
                                ToolTip='<%# Eval("TAG_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Expected Date of Completion by Planning/Production">
                        <ItemTemplate>
                            <asp:TextBox ID="txtExpectedDateofComp" runat="server" Width="100%"
                                Text='<%# Eval("EXPECTED_DATE_OF_COMP_BY_PLANNING") %>'
                                Enabled="false" CssClass="textboxleftsmall"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("REMARKS") %>'
                                ToolTip='<%# Eval("REMARKS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--  <asp:TemplateField HeaderText="Accepted Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAcceptedRemarks" Width="200px" runat="server" TextMode="MultiLine"
                                    Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("ACCEPTED_REMARKS") %>'
                                    ToolTip='<%# Eval("ACCEPTED_REMARKS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rejected Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRejectedRemarks" Width="200px" runat="server" TextMode="MultiLine"
                                    Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("REJECTED_REMARKS") %>'
                                    ToolTip='<%# Eval("REJECTED_REMARKS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    --%>
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




    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <label>LOT No.:</label>
                        <asp:TextBox ID="txtLOTNoSearch" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchJOBNo_Click" />
                </div>
            </div>

            <div class="popup-grid-container">
                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblLOTTFIDInList" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                                <asp:Label ID="lblJOBNoInList" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblLOTnoInList" runat="server" Visible="false" Text='<%# Eval("TF_NO") %>' />

                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TF_NO" HeaderText="LOT_NO" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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
    <%--JOB DETAIL END--%>


    <%--DRAWING DETAIL START--%>
    <asp:Button ID="btnShowPopupDrawingDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDrawingDetail" runat="server" TargetControlID="btnShowPopupDrawingDetail"
        PopupControlID="pnlPopupDrawingDetail" CancelControlID="imgBtnCancelDrawingDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupDrawingDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDrawingDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblDrawingRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoInDr" runat="server" CssClass="form-control" Enabled="false" />

                        <label>LOT No.:</label>
                        <asp:TextBox ID="txtLOTNoInDr" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Drawing No.:</label>
                        <asp:TextBox ID="txtDrawingNoInDr" runat="server" CssClass="form-control" />

                        <label>Equipment:</label>
                        <asp:TextBox ID="txtEquipmentInDr" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnSearchDrawingNo" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchDrawingNo_Click" />

                </div>
            </div>

            <div class="popup-grid-container">
                <asp:Label ID="lblDrawingMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvDrawingDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvDrawingDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get">
                            <ItemTemplate>
                                <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' />
                                <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                <asp:Label ID="lblEquipment" runat="server" Visible="false" Text='<%# Eval("EQUIPMENT") %>' />

                                <asp:Button ID="btnGetDrawingDetail" CommandArgument="GET" ToolTip="Get Drawing Details" Width="100%"
                                    runat="server" Text="Get" CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="TF_NO" HeaderText="LOT_NO" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="PRODUCTION_ORDER_NO" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
                        <asp:BoundField DataField="EQUIPMENT" HeaderText="EQUIPMENT" />

                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" Width="100%" runat="server" Enabled="false" CssClass="textboxrightsmall" Text='<%# Eval("QUANTITY") %>' />
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

    </asp:Panel>
    <%--DRAWING DETAIL END--%>



    <%--UPDATE DETAILS START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdateDetails" runat="server" TargetControlID="btnShowPopup"
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


        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegend" runat="server" Text="Update Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlCompanyToA" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyToA_SelectedIndexChanged" />

                    <label>Category:</label>
                    <asp:DropDownList ID="ddlCategoryToA" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategoryToA_SelectedIndexChanged">
                    </asp:DropDownList>


                    <label>JOB Number:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:HiddenField ID="hdLOTTFID" runat="server" />
                                <asp:TextBox ID="txtJOBNoToA" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNo_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Drawing Number:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">

                                <asp:TextBox ID="txtDrawingNoToA" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetDrawingNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClientClick="return ValidateAllJobNo();" OnClick="btnGetDrawingNo_Click" />
                            </td>
                        </tr>
                    </table>



                    <label>Additional Drawing:</label>

                    <asp:Panel ID="pnlViewAddDrawing" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:Label runat="server" Visible="false" ID="lblSRNoToA" Text="0" />
                                    <asp:TextBox ID="txtAddDrawingFilePathToA" runat="server" Enabled="false" CssClass="form-control" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnViewAddDrawing" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/pdficon1.png" ToolTip="View"
                                        OnClick="imgBtnViewAddDrawing_Click" />
                                </td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnRemoveAddDrawing" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/NEWICONS/Deleted01.png" ToolTip="Remove"
                                        OnClick="imgBtnRemoveAddDrawing_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlUploadAddDrawing" runat="server" Visible="true">
                        <table width="100%">
                            <tr>
                                <td style="width: 90%;">


                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="uploadFileAttachment1" runat="server" Width="100%"
                                                    Height="29px" BorderStyle="Groove" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachment1" style="display: none;">
                                                    <asp:Label ID="lblfileAttachment1" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnUndoAddDrawing" runat="server" Height="30px" Width="30px"
                                        ImageUrl="~/Images/Icon05.png" ToolTip="Undo" Visible="false"
                                        OnClick="imgBtnUndoAddDrawing_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>




                    <label>Equipment:</label>
                    <asp:HiddenField ID="hdLOTTFSubitemID" runat="server" />
                    <asp:Label runat="server" ID="lblLOTNo" Visible="false" />
                    <asp:Label runat="server" ID="lblSIDrawing1" Visible="false" />
                    <asp:Label runat="server" ID="lblSIDrawing2" Visible="false" />

                    <asp:TextBox ID="txtEquipmentToA" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Tag No.:</label>
                    <asp:TextBox ID="txtTagNoToA" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemNameToA" runat="server"
                        CssClass="form-control" />

                    <label>Item Detail:</label>
                    <asp:TextBox ID="txtItemDetailToA" runat="server"
                        CssClass="form-control" />


                    <label>ED of Comp. by Planning/Production:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDateToA" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdDateToA" runat="server" />
                                <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate" runat="server"
                                    TargetControlID="txtDateToA" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedDate">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>Allocated Quantity:</label>

                    <table width="100%">
                        <tr>
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtQuantityToA" runat="server"
                                    Text="0"
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/Icons/ADD05.png" onclick="IncreaseQuantity()" style="width: 20px; height: 20px;" alt="" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/Icons/Remove01.png" onclick="DecreaseQuantity()" style="width: 20px; height: 20px;" alt="" />
                            </td>
                        </tr>
                    </table>

                    <label>Remarks:</label>
                    <asp:TextBox ID="txtRemarkstoA" runat="server" Width="100%" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSaveDetails" runat="server" Width="100%" Text="Assign" CssClass="button"
                    OnClientClick="return ValidateAll();" OnClick="btnSaveDetails_Click" />
            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>
        </div>


    </asp:Panel>
    <%--UPDATE DETAILS END--%>


    <%--UPDATE STATUS START--%>
    <asp:Button ID="btnShowPopupUpdateStatus" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdateStatus" runat="server" TargetControlID="btnShowPopupUpdateStatus"
        PopupControlID="pnlPopupUpdateStatus" CancelControlID="imgBtnCancelUpdateStatus" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupUpdateStatus" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelUpdateStatus" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegendUpdateStatus" runat="server" Text="Update Status" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnitToUSt" runat="server" CssClass="form-control" Enabled="false" />

                    <label>JOB Number:</label>
                    <asp:TextBox ID="txtJOBNoToUSt" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Drawing Number:</label>
                    <asp:TextBox ID="txtDrawingNoToUSt" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Additional Drawing:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%;">
                                <asp:TextBox ID="txtAddDrawingFilePathToUSt" runat="server" Enabled="false" CssClass="form-control" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 10%;">
                                <asp:ImageButton ID="imgBtnViewAddDrawingToUSt" runat="server" Height="20px" Width="20px"
                                    ImageUrl="~/Images/pdficon1.png" ToolTip="View"
                                    OnClick="imgBtnViewAddDrawingToSt_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Equipment:</label>
                    <asp:TextBox ID="txtEquipmentToUSt" runat="server" CssClass="form-control"
                        Enabled="false" />

                    <label>Tag No.:</label>
                    <asp:TextBox ID="txtTagNoToUSt" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemNameToUSt" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Item Detail:</label>
                    <asp:TextBox ID="txtItemDetailToUSt" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>ED of Comp. by Planning/Production:</label>
                    <asp:TextBox ID="txtDateToUSt" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Allocated Quantity:</label>
                    <asp:TextBox ID="txtQuantityToUSt" runat="server"
                        CssClass="form-control"
                        Text="0" Enabled="false" />

                    <label>Remarks:</label>
                    <asp:TextBox ID="txtRemarksToUSt" runat="server"
                        CssClass="form-control"
                        Enabled="false"
                        TextMode="MultiLine" Rows="2" />

                    <label>Accepted/Rejected Remarks:</label>
                    <asp:TextBox ID="txtAcceptedOrRejectedRemarksToUst" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />
                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnAccept" runat="server" Width="100%" Text="Accept" CssClass="button"
                    OnClick="btnAccept_Click" OnClientClick="return ValidateAllAccept();" />

                <asp:Button ID="btnReject" runat="server" Width="100%" Text="Reject" CssClass="button"
                    OnClick="btnReject_Click" OnClientClick="return ValidateAllReject();" />

            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlUpdateStatusMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateStatusMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>
        </div>

    </asp:Panel>
    <%--UPDATE STATUS END--%>


    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeViewInPDF" runat="server" TargetControlID="btnViewInPDF"
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
            id="iframeViewDetailsInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END----%>


    <%--MACHINE SCHEDULING START--%>
    <asp:Button ID="btnShowPopupScheduleMachine" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeScheduleMachine" runat="server" TargetControlID="btnShowPopupScheduleMachine"
        PopupControlID="pnlPopupScheduleMachine" CancelControlID="imgBtnCancelScheduleMachine" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupScheduleMachine" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelScheduleMachine" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblLegendScheduleMachine" runat="server" Text="Schedule Machine" />:
                        <asp:Label ID="lblScheduleMachineRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">


                        <label>Unit:</label>
                        <asp:TextBox ID="txtUnitToMSch" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Assigned Drawing Code:</label>
                        <asp:TextBox ID="txtAssignedDrawingCodeToMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoToMSch" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Drawing Number:</label>
                        <asp:TextBox ID="txtDrawingNoToMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Additional Drawing:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 85%;">
                                    <asp:TextBox ID="txtAddDrawingFilePathToMSch" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:ImageButton ID="imgBtnViewAddDrawingToMSch" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/pdficon1.png" ToolTip="View"
                                        OnClick="imgBtnViewAddDrawingToSt_Click" />
                                </td>
                            </tr>
                        </table>

                        <label>Equipment:</label>
                        <asp:TextBox ID="txtEquipmentToMSch" runat="server" CssClass="form-control" Enabled="false" />



                        <label>Tag No.:</label>
                        <asp:TextBox ID="txtTagNoToMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Item Name:</label>
                        <asp:TextBox ID="txtItemNameToMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Item Detail:</label>
                        <asp:TextBox ID="txtItemDetailToMSch" runat="server" CssClass="form-control" Enabled="false" />


                        <label>ED of Comp. by Planning/Production:</label>
                        <asp:TextBox ID="txtDateToMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Allocated Quantity:</label>
                        <asp:TextBox ID="txtQuantityToMSch" runat="server" CssClass="form-control" Text="0" Enabled="false" />


                        <label>Commited Date By Shop Incharge:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%;">
                                    <asp:HiddenField ID="hdCommitedDateByShopInchargeToMSch" runat="server" />
                                    <asp:TextBox ID="txtCommitedDateByShopInchargeToMSch" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <ajax:CalendarExtender ID="calendarCommitedDateByShopInchargeToMSch"
                                        PopupButtonID="imgbtnCommitedDateByShopInchargeToMSch"
                                        runat="server" TargetControlID="txtCommitedDateByShopInchargeToMSch" Format="dd-MMM-yyyy">
                                    </ajax:CalendarExtender>
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:ImageButton ID="imgbtnCommitedDateByShopInchargeToMSch" runat="server" ImageUrl="~/Images/MS/cal2.png"
                                        ToolTip="Commited Date By Shop Incharge Calendar" Width="25px" Height="25PX" />
                                </td>
                            </tr>
                        </table>

                        <label>Date of Receipt of Material:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%;">
                                    <asp:HiddenField ID="hdDateOfReceiptOfMaterialToMSch" runat="server" />
                                    <asp:TextBox ID="txtDateOfReceiptOfMaterialToMSch" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <ajax:CalendarExtender ID="calendarDateOfReceiptOfMaterialToMSch"
                                        PopupButtonID="imgbtnDateOfReceiptOfMaterialToMSch"
                                        runat="server" TargetControlID="txtDateOfReceiptOfMaterialToMSch" Format="dd-MMM-yyyy">
                                    </ajax:CalendarExtender>
                                </td>
                                <td style="width: 10%;" align="right">
                                    <asp:ImageButton ID="imgbtnDateOfReceiptOfMaterialToMSch" runat="server" ImageUrl="~/Images/MS/cal2.png"
                                        ToolTip="Date of Receipt of Material Calendar" Width="25px" Height="25PX" />
                                </td>
                            </tr>
                        </table>

                        <label>Activity:</label>
                        <asp:DropDownList ID="ddlActivityToMSch"
                            CssClass="form-control"
                            runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlActivityToMSch_SelectedIndexChanged" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnGetSchedulingDetails" runat="server" Width="100%" Text="Get Scheduling Hours" CssClass="button"
                        OnClick="btnGetSchedulingDetails_Click" OnClientClick="return ValidateAllMachineToMSch();" />

                    <asp:Button ID="btnSaveScheduledDetails" runat="server" Width="100%" Text="Schedule" CssClass="button"
                        OnClick="btnSaveScheduledDetails_Click" OnClientClick="return ValidateAllSchedule();"
                        BorderColor="Yellow" BorderStyle="Solid"
                        BorderWidth="2px" BackColor="LightCoral" />
                </div>

                <div class="full-width">
                    <div align="center">
                        <asp:Panel ID="pnlScheduleMachineMsg" Visible="false" runat="server">
                            <asp:Label ID="lblScheduleMachineMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="popup-grid-container">

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvMachineSchedulingList" runat="server" AutoGenerateColumns="false" CellPadding="5"
                    AlternatingRowStyle-CssClass="alt"
                    OnRowCommand="gvMachineSchedulingList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <asp:Label ID="lblTypeOfWorkID" runat="server" Visible="false" Text='<%# Eval("TYPE_OF_WORK_ID") %>' />
                                <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_ID") %>' />
                                <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_ID") %>' />
                                <asp:Label ID="lblWorkerID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />

                                <asp:TextBox ID="txtSrNoToMSchInList" runat="server" Enabled="false" CssClass="textboxcenter"
                                    Text='<%# Eval("SR_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                    ImageUrl="~/Images/Icons/REMOVE03.png" Width="35px" Height="35px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnEdit" CommandArgument="EDIT" runat="server" ToolTip="Edit"
                                                ImageUrl="~/Images/LOT/edit5.png" Width="35px" Height="35px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTypeOfWorkToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="150px" Text='<%# Eval("TYPE_OF_WORK") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtActivityToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="150px" Text='<%# Eval("ACTIVITY_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Machine">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMachineToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="250px" Text='<%# Eval("MACHINE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Worker" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtWorkerToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="250px" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledDateToMSchInList" runat="server" Width="100px"
                                    Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("DATE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Scheduled From">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledFromToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_FROM") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Scheduled To">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_TO") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Scheduled Hours">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledHoursToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_HOURS") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <%--<asp:TemplateField HeaderText="Available From">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAvailableFromToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftyellow"
                                                Text='<%# Eval("AVAILABLE_FROM") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Available To">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAvailableToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftyellow"
                                                Text='<%# Eval("AVAILABLE_TO") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Available Hours">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAvailableHoursToMSchInList" runat="server" Width="100%"
                                                Enabled="false" CssClass="textboxleftyellow"
                                                Text='<%# Eval("AVAILABLE_HOURS") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarksToMSchInList" Width="250px" runat="server" TextMode="MultiLine"
                                    Text='<%# Eval("REMARKS") %>' Columns="30" CssClass="textboxleftyellow" />
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






        <div align="center">

            <fieldset style="width: 95%; margin-top: 10px;">
                <legend style="text-align: center;"></legend>

                <div style='overflow: auto; width: 99%; height: 680px; border: 1px solid lightgray; margin-left: 5px;'>

                    <br />

                    <br />
                    <fieldset style="width: 100%;">
                        <legend style="text-align: center;"></legend>
                        <%--id="dvScroll" --%>
                        <div style='overflow: scroll; width: 100%; height: 300px; border: 1px solid lightgray;'>
                        </div>
                        <%--<input type="hidden" id="div_position" name="div_position" />--%>
                    </fieldset>

                </div>
            </fieldset>

        </div>
    </asp:Panel>
    <%--MACHINE SCHEDULING END--%>


    <%--SCHEDULE DATES START--%>
    <asp:Button ID="btnShowPopupScheduleDates" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeScheduleDates" runat="server" TargetControlID="btnShowPopupScheduleDates"
        PopupControlID="pnlPopupScheduleDates" CancelControlID="imgBtnCancelScheduleDates" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupScheduleDates" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelScheduleDates" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblLegendMachineNameSchD" runat="server" Text="Schedule Machine" />:
                        <asp:Label ID="lblAvailableDatesRecords" runat="server" Text="Available Hours[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">


                        <label>From Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtScheduledFromSchD" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdScheduledFromSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarScheduledFromSchD" runat="server"
                                        PopupButtonID="imgbtnScheduledFromSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarScheduledFromSchD" TargetControlID="txtScheduledFromSchD"
                                        OnClientDateSelectionChanged="clientChangedScheduleMonth">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnScheduledFromSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />

                                    <%--Format="MMM-yyyy"--%>
                                    <%--OnClientHidden="onCalendarHiddenFrom"
                                                            OnClientShown="onCalendarShownFrom"--%>
                                </td>
                            </tr>
                        </table>

                        <label>To Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtScheduledToSchD" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdScheduledToSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarScheduledToSchD" runat="server"
                                        PopupButtonID="imgbtnScheduledToSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarScheduledToSchD" TargetControlID="txtScheduledToSchD"
                                        OnClientDateSelectionChanged="clientChangedScheduleMonth">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnScheduledToSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />
                                </td>

                            </tr>
                        </table>

                        <label>Activity:</label>
                        <asp:Label ID="lblActivitySchD" runat="server" Visible="false" />
                        <asp:TextBox ID="txtActivitySchD" runat="server" Enabled="false" Width="100%" />

                        <label>Machine:</label>
                        <asp:DropDownList ID="ddlMachineSchD" Width="100%" Height="26px" runat="server" onblur="ValidateMachineSchD()" />

                        <asp:Button ID="btnCheckAvailibilityOfDates" runat="server" Width="100%" Text="Check Availibility" CssClass="button"
                            OnClick="btnCheckAvailibilityOfDates_Click" OnClientClick="return ValidateAllMachineSchD();" />

                        <label>Select All</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelectAllSchD" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAllSchD_CheckedChanged" />
                                </td>
                                <td>
                                    <asp:Button ID="btnGetScheduledHours" ToolTip="Get Scheduled Hours" runat="server"
                                        Text="Get Scheduled Hours" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid"
                                        BorderWidth="2px" BackColor="LightCoral" OnClick="btnGetScheduledHours_Click"
                                        OnClientClick="return ValidateAllMachineSchD();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlScheduleDatesMsg" Visible="false" runat="server">
                        <asp:Label ID="lblScheduleDatesMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>

                        <asp:GridView
                            CssClass="popup-grid"
                            ID="gvAvailableDatesList" runat="server" AutoGenerateColumns="false" CellPadding="5"
                            AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="gvMachineDatesList_RowDataBound">

                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sr No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeOfWorkID" runat="server" Visible="false" Text='<%# Eval("TYPE_OF_WORK_ID") %>' />
                                        <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_ID") %>' />
                                        <asp:Label ID="lblActivityName" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_NAME") %>' />
                                        <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_ID") %>' />
                                        <asp:Label ID="lblWorkerID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />

                                        <asp:Label ID="lblEndTimeFlag" runat="server" Visible="false" Text='<%# Eval("END_TIME_FLAG") %>' />

                                        <asp:TextBox ID="txtSrNoToMSchInList" runat="server" Enabled="false" CssClass="textboxcenter"
                                            Text='<%# Eval("SR_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelect" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTypeOfWorkToMSchInList" Width="100px" Height="26px" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtActivityToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="150px" Text='<%# Eval("ACTIVITY_NAME") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Machine">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMachineToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                            Width="250px" Text='<%# Eval("MACHINE_NAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Worker" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWorkerMSchInList" Width="250px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledDateToMSchInList" runat="server" Width="100px"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduled From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledFromToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_FROM") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduled To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_TO") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Scheduled Hours">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledHoursToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_HOURS") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Scheduling From dd">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtAvailableFromToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftgreen"
                                                Text='<%# Eval("AVAILABLE_FROM") %>'></asp:TextBox>--%>

                                        <asp:Label ID="lblAvailableFromToMSchInList" runat="server" Visible="false" Text='<%# Eval("AVAILABLE_FROM") %>' />
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlAFH" runat="server" DataTextField="H" DataValueField="H" Width="50px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlAFH_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>H
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAFM" runat="server" DataTextField="M" DataValueField="M" Width="50px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlAFM_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>M
                                                </td>
                                            </tr>
                                        </table>

                                        <%--<label for="appt">Choose a time for your meeting:</label>--%>
                                        <%-- <input type="time" id="appt" name="appt" value='<%# Eval("AVAILABLE_FROM") %>' required />--%>
                                        <%--<cc1:TimeSelector ID="TimeSelector7" runat="server" SelectedTimeFormat="TwentyFour"></cc1:TimeSelector>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduling To dd">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtAvailableToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftgreen"
                                                Text='<%# Eval("AVAILABLE_TO") %>'></asp:TextBox>--%>

                                        <asp:Label ID="lblAvailableToMSchInList" runat="server" Visible="false" Text='<%# Eval("AVAILABLE_TO") %>' />
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlATH" runat="server" DataTextField="H" DataValueField="H" Width="50px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlATH_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>H
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlATM" runat="server" DataTextField="M" DataValueField="M" Width="50px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlATM_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>M
                                                </td>
                                            </tr>
                                        </table>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Available Hours">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLatestScheduledHoursToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftgreen" Text="00:00"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance.. Hours">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkingStartTimeMSchInList" runat="server" Visible="false" Text='<%# Eval("WORKING_START_TIME") %>' />
                                        <asp:Label ID="lblWorkingEndTimeMSchInList" runat="server" Visible="false" Text='<%# Eval("WORKING_END_TIME") %>' />

                                        <asp:TextBox ID="txtAvailableHoursToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftgreen"
                                            Text="00:00"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarksToMSchInList" Width="250px" runat="server" TextMode="MultiLine"
                                            Text='<%# Eval("REMARKS") %>' Columns="30" CssClass="textboxleftgreen" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <input type="hidden" id="div_position" name="div_position" />

            </div>

        </div>

    </asp:Panel>
    <%--SCHEDULE DATES END--%>



    <%--COMPLETION MACHINE SCHEDULING START--%>
    <asp:Button ID="btnShowPopupSchedulingCompletion" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeSchedulingCompletion" runat="server" TargetControlID="btnShowPopupSchedulingCompletion"
        PopupControlID="pnlPopupSchedulingCompletion" CancelControlID="imgBtnCancelSchedulingCompletion" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupSchedulingCompletion" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelSchedulingCompletion" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblLegendSchedulingCompletion" runat="server" Text="Complete Scheduling" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Unit</label>
                        <asp:TextBox ID="txtUnitToCMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Assigned Drawing Code</label>
                        <asp:TextBox ID="txtAssignedDrawingCodeToCMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>JOB Number</label>
                        <asp:TextBox ID="txtJOBNoToCMSch" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Drawing Number:</label>
                        <asp:TextBox ID="txtDrawingNoToCMSch" runat="server" CssClass="form-control" Enabled="false" />


                        <label>From:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtScheduledFromToCMSch" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdScheduledFromToCMSch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarScheduledFromToCMSch" runat="server"
                                        PopupButtonID="imgbtnScheduledFromToCMSch"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarScheduledFromToCMSch" TargetControlID="txtScheduledFromToCMSch"
                                        OnClientDateSelectionChanged="clientChangedScheduleDatesToCMSch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnScheduledFromToCMSch" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                        <label>To:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtScheduledToToCMSch" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdScheduledToToCMSch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarScheduledToToCMSch" runat="server"
                                        PopupButtonID="imgbtnScheduledToToCMSch"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarScheduledToToCMSch" TargetControlID="txtScheduledToToCMSch"
                                        OnClientDateSelectionChanged="clientChangedScheduleDatesToCMSch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnScheduledToToCMSch" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />
                                </td>
                                <td>
                                    <%--<asp:CheckBox ID="chkClearDates" runat="server" OnChange="ClearDates()" Checked="false" />--%>

                                    <input id="btnClearDates" type="button" value="Clear" title="Clear dates"
                                        style="color: white; padding: 8px 25px; text-align: center; text-decoration: none; display: inline-block; font-size: 15px; border-radius: 5px; border-color: Yellow; border: Solid; border-width: 2px; background-color: LightCoral;"
                                        onclick="ClearDates()" />
                                </td>
                            </tr>
                        </table>

                        <label>Activity:</label>
                        <asp:DropDownList ID="ddlActivityToCMSch"
                            CssClass="form-control"
                            runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlActivityToCMSch_SelectedIndexChanged" />

                        <label>Machine:</label>
                        <asp:DropDownList ID="ddlMachineToCMSch"
                            CssClass="form-control"
                            runat="server" />

                        <label>Worker:</label>
                        <asp:DropDownList ID="ddlWorkerToCMSch"
                            CssClass="form-control"
                            runat="server" />


                        <label>Type of Work:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlTypeOfWorkToCMSch"
                                        CssClass="form-control"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Width="100%" Text="Refresh" CssClass="button"
                                        OnClick="btnRefresh_Click" OnClientClick="return ValidateAllDateRangeToCMSch();" />
                                </td>
                            </tr>
                        </table>

                        <asp:Label ID="lblCompletionDateToCMSch" Text="Completion Date:" runat="server" />
                        <table width="100%" id="tblCompletionDateToCMSch" runat="server">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCompletionDateToCMSch" runat="server" Width="100%"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdCompletionDateToCMSch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarCompletionDateToCMSch" runat="server"
                                        PopupButtonID="imgbtnCompletionDateToCMSch"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarCompletionDateToCMSch" TargetControlID="txtCompletionDateToCMSch"
                                        OnClientDateSelectionChanged="clientChangedCompletionDateToCMSch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnCompletionDateToCMSch" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                    </div>
                </fieldset>

                <div class="full-width button-group">

                    <table width="100%" id="tblSelectAllToCMSch" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="lblSelectAllToCMSch" Text="Select All:" runat="server" />
                                <asp:CheckBox ID="chkSelectAllToCMSch" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkSelectAllToCMSch_CheckedChanged" />
                            </td>
                        </tr>
                    </table>

                    <asp:Button ID="btnCompleteScheduling" runat="server" Width="100%" Text="Complete" CssClass="button"
                        OnClick="btnCompleteScheduling_Click" OnClientClick="return ValidateAllCompleteScheduling();"
                        BorderColor="Yellow" BorderStyle="Solid" Visible="false"
                        BorderWidth="2px" BackColor="LightCoral" />

                    <asp:Button ID="btnModifySchedulingDetails" runat="server" Width="100%" Text="Modify" CssClass="button"
                        OnClick="btnModifySchedulingDetails_Click" OnClientClick="return ValidateAllModifyWorker();"
                        BorderColor="Yellow" BorderStyle="Solid" Visible="false"
                        BorderWidth="2px" BackColor="Teal" />

                </div>
            </div>

            <div class="popup-grid-container">

                <div align="center">
                    <asp:Panel ID="pnlSchedulingCompletionMsg" Visible="false" runat="server">
                        <asp:Label ID="lblSchedulingCompletionMsg" runat="server" Font-Bold="true" Font-Size="Large" />:
                        <asp:Label ID="lblScheduledMachineListRecords" runat="server" Text="Records[0]" />
                    </asp:Panel>
                </div>

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvScheduledMachineListForCompletion" runat="server" AutoGenerateColumns="false" CellPadding="5"
                    AlternatingRowStyle-CssClass="alt"
                    OnRowDataBound="gvScheduledMachineListForCompletion_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />
                                <asp:Label ID="lblTypeOfWorkID" runat="server" Visible="false" Text='<%# Eval("TYPE_OF_WORK_FID") %>' />
                                <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_FID") %>' />
                                <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_FID") %>' />
                                <asp:Label ID="lblWorkerID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_FID") %>' />
                                <asp:Label ID="lblCompletedRemarks" runat="server" Visible="false" Text='<%# Eval("COMPLETED_REMARKS") %>' />
                                <asp:Label ID="lblIsCompleted" runat="server" Visible="false" Text='<%# Eval("IS_COMPLETED") %>' />

                                <asp:TextBox ID="txtSrNoToMSchInList" runat="server" Enabled="false" CssClass="textboxcenter"
                                    Text='<%# Eval("SR_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTypeOfWorkToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="150px" Text='<%# Eval("TYPE_OF_WORK") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtActivityToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="150px" Text='<%# Eval("ACTIVITY_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Machine">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMachineToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Width="250px" Text='<%# Eval("MACHINE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Worker" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%--<asp:TextBox ID="txtWorkerToMSchInList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("EMPLOYEE_NAME") %>' />--%>
                                <asp:DropDownList ID="ddlWorkerMSchInList" Width="250px" Height="26px" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledDateToMSchInList" runat="server" Width="100px"
                                    Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("DATE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Scheduled From">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledFromToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_FROM") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Scheduled To">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_TO") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Scheduled Hours">
                            <ItemTemplate>
                                <asp:TextBox ID="txtScheduledHoursToMSchInList" runat="server" Width="100%"
                                    Enabled="false" CssClass="textboxleftgreen"
                                    Text='<%# Eval("SCHEDULED_HOURS") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarksToMSchInList" Width="250px" runat="server" TextMode="MultiLine" Enabled="false"
                                    Text='<%# Eval("REMARKS") %>' Columns="30" CssClass="textboxleftyellow" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Completion/Updation Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCompletionRemarksToMSchInList" Width="250px" runat="server" TextMode="MultiLine"
                                    Columns="30" CssClass="textboxleftgreen" />
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

    </asp:Panel>
    <%--COMPLETION MACHINE SCHEDULING END--%>




    <%--QUALITY INSPECTION START--%>
    <asp:Button ID="btnShowPopupQualityInspection" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeQualityInspection" runat="server" TargetControlID="btnShowPopupQualityInspection"
        PopupControlID="pnlPopupQualityInspection" CancelControlID="imgBtnCancelQualityInspection" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupQualityInspection" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelQualityInspection" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegendQualityInspection" runat="server" Text="Quality Inspection" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnitToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>JOB Number:</label>
                    <asp:TextBox ID="txtJOBNoToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>LOT No:</label>
                    <asp:TextBox ID="txtLOTNoToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Drawing Number:</label>
                    <asp:TextBox ID="txtDrawingNoToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Equipment:</label>
                    <asp:TextBox ID="txtEquipmentToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Tag No.:</label>
                    <asp:TextBox ID="txtTagNoToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemNameToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Item Detail:</label>
                    <asp:TextBox ID="txtItemDetailToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Expected Date of Completion:</label>
                    <asp:TextBox ID="txtExpectedDateofCompletionToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Allocated Quantity:</label>
                    <asp:TextBox ID="txtAllocatedQuantityToQaInsp" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Accepted Quantity:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtAcceptedQuantityToQaInsp" runat="server" CssClass="form-control" Text="0"
                                    onkeyDown="javascript:preventInput(event);" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/MS/UP2.png" onclick="IncreaseAcceptedQuantity()" style="width: 20px; height: 20px;" alt="" title="Increase" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/MS/DOWN1.png" onclick="DecreaseAcceptedQuantity()" style="width: 20px; height: 20px;" alt="" title="Decrese" />
                            </td>
                        </tr>
                    </table>

                    <label>Rejected Quantity:</label>
                    <asp:TextBox ID="txtRejectedQuantityToQaInsp" runat="server" CssClass="form-control" Text="0"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>Completion Remarks:</label>
                    <asp:TextBox ID="txtCompletionRemarksToQaInsp" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Enabled="false" />

                    <label>Accepted Remarks:</label>
                    <asp:TextBox ID="txtInspectedAcceptedRemarksToQaInsp" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />

                    <label>Rejected Remarks:</label>
                    <asp:TextBox ID="txtInspectedRejectedRemarksToQaInsp" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />

                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnSaveInspection" runat="server" Width="100%" Text="Submit Inspection" CssClass="button"
                    OnClick="btnSaveInspection_Click" OnClientClick="return ValidateAllSaveInspection();"
                    BorderColor="Yellow" BorderStyle="Solid"
                    BorderWidth="2px" BackColor="Teal" />

            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlQualityInspectionMsg" Visible="false" runat="server">
                        <asp:Label ID="lblQualityInspectionMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>
        </div>

    </asp:Panel>
    <%--QUALITY INSPECTION END--%>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

