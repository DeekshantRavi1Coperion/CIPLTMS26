<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PostPOReportPivotGroup.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_PIVOT_GROUP_PostPOReportPivotGroup" Title="Post Pivot Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />


    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <style type="text/css">
        .textbox {
            width: 120px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
        }

        .textboxcenter {
            width: 70px;
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
            width: 120px;
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
            background-color: transparent;
        }
    </style>

    <script type="text/javascript">

        function pageLoad() {

            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;


            <%--document.getElementById('<%=txtLikelyDeliveryDateToPost.ClientID %>').value = document.getElementById('<%=hdLikelyDeliveryDateToPost.ClientID %>').value;
            document.getElementById('<%=txtPlanDateofProcurementToPost.ClientID %>').value = document.getElementById('<%=hdPlanDateofProcurementToPost.ClientID %>').value;--%>

            if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        <%--function clientChangedLikelyDeliveryDateToPost(sender, args) {
            document.getElementById('<%=hdLikelyDeliveryDateToPost.ClientID %>').value = document.getElementById('<%=txtLikelyDeliveryDateToPost.ClientID %>').value;
        }

        function clientChangedPlanDateofProcurementToPost(sender, args) {
            document.getElementById('<%=hdPlanDateofProcurementToPost.ClientID %>').value = document.getElementById('<%=txtPlanDateofProcurementToPost.ClientID %>').value;
        }--%>


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

        function ValidateJOBNoMainSearch() {
            var jobNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (jobNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllToSearch() {

            if (ValidateDateRange()) {
                return false;
            }

            if (ValidateJOBNoMainSearch()) {
                return false;
            }
            return true;
        }
    </script>


    <script type="text/Javascript">
        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectDates.ClientID %>').checked;
            if (chk == true) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
        }

    </script>

    <script type="text/Javascript">       
        function GetSaving(el) {
            var row = el.parentNode.parentNode;

            var typid = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;

            var saleEstimateBudget = 0;
            var deltaAsPerSaleEstimate = 0;
            var pendingCost = 0;
            var saving = 0;
            var savingNew = 0;
            var savingOfEstimatePercentage = 0;


            if (typid == 0) {
                saleEstimateBudget = parseFloat(row.cells[5].getElementsByTagName("input")[0].value).toFixed(3);
                deltaAsPerSaleEstimate = parseFloat(row.cells[8].getElementsByTagName("input")[0].value).toFixed(3);

                if (isNaN(parseFloat(row.cells[12].getElementsByTagName("input")[0].value).toFixed(3))) {
                    row.cells[12].getElementsByTagName("input")[0].value = '0';
                }

                pendingCost = parseFloat(row.cells[12].getElementsByTagName("input")[0].value).toFixed(3);
                row.cells[13].getElementsByTagName("input")[0].value = parseFloat(deltaAsPerSaleEstimate - pendingCost).toFixed(3);;

                if (parseFloat(row.cells[13].getElementsByTagName("input")[0].value) > 0) {
                    savingNew = parseFloat(row.cells[13].getElementsByTagName("input")[0].value).toFixed(3);
                }
                else {
                    savingNew = 0;
                }

                if (savingNew > 0) {
                    savingOfEstimatePercentage = parseFloat((savingNew / saleEstimateBudget) * 100).toFixed(3);
                }
                else {
                    savingOfEstimatePercentage = 0;
                }

                row.cells[14].getElementsByTagName("input")[0].value = savingOfEstimatePercentage;
            }
            else {
                saleEstimateBudget = parseFloat(row.cells[4].getElementsByTagName("input")[0].value).toFixed(3);
                deltaAsPerSaleEstimate = parseFloat(row.cells[6].getElementsByTagName("input")[0].value).toFixed(3);

                if (isNaN(parseFloat(row.cells[10].getElementsByTagName("input")[0].value).toFixed(3))) {
                    row.cells[10].getElementsByTagName("input")[0].value = '0';
                }

                pendingCost = parseFloat(row.cells[10].getElementsByTagName("input")[0].value).toFixed(3);
                row.cells[11].getElementsByTagName("input")[0].value = parseFloat(deltaAsPerSaleEstimate - pendingCost).toFixed(3);;

                if (parseFloat(row.cells[11].getElementsByTagName("input")[0].value) > 0) {
                    savingNew = parseFloat(row.cells[11].getElementsByTagName("input")[0].value).toFixed(3);
                }
                else {
                    savingNew = 0;
                }

                if (savingNew > 0) {
                    savingOfEstimatePercentage = parseFloat((savingNew / saleEstimateBudget) * 100).toFixed(3);
                }
                else {
                    savingOfEstimatePercentage = 0;
                }

                row.cells[12].getElementsByTagName("input")[0].value = savingOfEstimatePercentage;
            }


        }
    </script>

    <script type="text/Javascript">       
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">


        function ConfirmPosting() {
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;
            var GVRowCountNew = 0;

            if (GVRowCount == '')
                GVRowCountNew = 0;
            else
                GVRowCountNew = parseInt(GVRowCount);

            if (GVRowCountNew > 0) {

                if (confirm("Would you like to post pivot group?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }


    </script>


    <script type="text/javascript" language="javascript">
        function onCalendarShown() {
            var cal = $find("calendarPostingMonth");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendarPostingMonth");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarPostingMonth");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <script type="text/javascript">



        function ValidatePONo() {
            var pono = document.getElementById('<%=txtPONoToU.ClientID %>').value;
            if (pono == '') {
                document.getElementById('<%=txtPONoToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONoToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJOBNo() {
            var jobno = document.getElementById('<%=txtJOBNoToU.ClientID %>').value;
            if (jobno == '') {
                document.getElementById('<%=txtJOBNoToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOldPivotGroup() {
            var oldPivotGroup = document.getElementById('<%=txtOldPivotGroupToU.ClientID %>').value;
            if (oldPivotGroup == '') {
                document.getElementById('<%=txtOldPivotGroupToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOldPivotGroupToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOldPivotGroupDesc() {
            var oldPivotGroup = document.getElementById('<%=txtOldPivotGroupDescToU.ClientID %>').value;
            if (oldPivotGroup == '') {
                document.getElementById('<%=txtOldPivotGroupDescToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOldPivotGroupDescToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateNewPivotGroup() {
            var newPivotGroup = document.getElementById('<%=txtNewPivotGroupToU.ClientID %>').value;
            if (newPivotGroup == '') {
                document.getElementById('<%=txtNewPivotGroupToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNewPivotGroupToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateNewPivotGroupDesc() {
            var newPivotGroup = document.getElementById('<%=txtNewPivotGroupDescToU.ClientID %>').value;
            if (newPivotGroup == '') {
                document.getElementById('<%=txtNewPivotGroupDescToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNewPivotGroupDescToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSaleEstimateBudgetToU() {
            var SaleEstimateBudgetToU = document.getElementById('<%=txtSaleEstimateBudgetToU.ClientID %>').value;
            if (SaleEstimateBudgetToU == '' || parseFloat(SaleEstimateBudgetToU) == 0) {
                document.getElementById('<%=txtSaleEstimateBudgetToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSaleEstimateBudgetToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateSaleEstimateBudgetToPost() {
            var SaleEstimateBudgetToPost = document.getElementById('<%=txtSaleEstimateBudgetToPost.ClientID %>').value;
            if (SaleEstimateBudgetToPost == '' || parseFloat(SaleEstimateBudgetToPost) == 0) {
                document.getElementById('<%=txtSaleEstimateBudgetToPost.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSaleEstimateBudgetToPost.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAllToPost() {
            var check = true;

            if (ValidateSaleEstimateBudgetToPost()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to post pivot group?")) {
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


        function ValidateAllToUpdate() {
            var check = true;

            if (ValidateJOBNo()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateNewPivotGroup()) {
                check = false;
            }

            if (ValidateNewPivotGroupDesc()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to save pivot group?")) {
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


        function ValidateJOBNoAll() {
            var check = true;

            if (ValidateJOBNo()) {
                check = false;
            }

            return check;
        }
    </script>


    <script type="text/Javascript">

        function Calculation(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    CalCaluateValues();
                }
            }
            else {
                CalCaluateValues();
            }
        }

        function CalCaluateValues() {
            var poValueINR = 0;
            var saleEstimateBudget = 0;
            var pendingCost = 0;

            if (document.getElementById('<%=txtPOValueINRToPost.ClientID %>').value != '') {
                poValueINR = parseFloat(document.getElementById('<%=txtPOValueINRToPost.ClientID %>').value);
            }
            else {
                poValueINR = 0;
            }

            if (document.getElementById('<%=txtSaleEstimateBudgetToPost.ClientID %>').value != '') {
                saleEstimateBudget = parseFloat(document.getElementById('<%=txtSaleEstimateBudgetToPost.ClientID %>').value);
            }
            else {
                saleEstimateBudget = 0;
            }

            if (document.getElementById('<%=txtPendingCostToPost.ClientID %>').value != '') {
                pendingCost = parseFloat(document.getElementById('<%=txtPendingCostToPost.ClientID %>').value);
            }
            else {
                pendingCost = 0;
            }

            document.getElementById('<%=txtDeltaAsPerSaleEstimateBudgetToPost.ClientID %>').value = (saleEstimateBudget - poValueINR).toFixed(3);

            document.getElementById('<%=txtSavingCostToPost.ClientID %>').value = (parseFloat(document.getElementById('<%=txtDeltaAsPerSaleEstimateBudgetToPost.ClientID %>').value) - pendingCost).toFixed(3);

            if (parseFloat(saleEstimateBudget) > 0) {
                document.getElementById('<%=txtSavingofOriginalEstimatePercToPost.ClientID %>').value = (parseFloat((document.getElementById('<%=txtSavingCostToPost.ClientID %>').value) / saleEstimateBudget) * 100).toFixed(3)
            }
            else {
                document.getElementById('<%=txtSavingofOriginalEstimatePercToPost.ClientID %>').value = '0';
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
    <asp:HiddenField ID="hdGVRowCount" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Post PO Pivot Group:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>PO Date- Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
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
                    <label>PO Date- End Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td style="width: 20%;" align="center">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>

                            <td>
                                <asp:CheckBox ID="chkSelectDates" runat="server" onchange="EnableDisableDates()" Checked="true" />
                            </td>

                        </tr>
                    </table>


                    <label>Posting Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                <ajax:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                </ajax:CalendarExtender>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    TargetControlID="txtPostingMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Posting Month Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Report Type:</label>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Text="Both" Value="0" />
                        <asp:ListItem Text="As Per PO Budget" Value="1" />
                        <asp:ListItem Text="As Per Sale Estimate Budget" Value="2" Selected="True" />
                    </asp:DropDownList>

                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAllToSearch();" OnClick="btnSearch_Click" />

                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />



                    <div class="full-width button-group">
                        <table width="100%">

                            <tr runat="server" id="trAsPerPOBudget">
                                <td>Total PO Budget[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total PO Value[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValueINR1" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total Delta As Per PO Budget[INR]:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtTotalDeltaAsPerPOBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr runat="server" id="trAsPerSaleEstimateBudget">
                                <td>Total Sale Estimate Budget[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalSaleEstimateBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total PO Value[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValueINR2" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total Delta As Per Sale Estimate Budget[INR]:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtTotalDeltaAsPerSaleEstimateBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                    </div>

                    <div class="full-width button-group">
                        <label>Select/Deselect All:</label>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true"
                            OnCheckedChanged="chkSelectAll_SelectedIndexChanged" />

                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                            Text="Post" OnClick="btnSave_Click" OnClientClick="return ConfirmPosting();" />

                        <label>Dates.:</label>
                        <asp:Label ID="lblStartDate" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />
                        <asp:Label ID="lblEndDate" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />

                        <label>JOB No.:</label>
                        <asp:Label ID="lblJOBNo" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />

                        <label>Posting Month:</label>
                        <asp:Label ID="lblPostingMonth" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />

                    </div>
                </div>
            </fieldset>
            <div class="full-width button-group">
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
                ID="gvPGReport" runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvPGReport_RowDataBound"
                OnRowCommand="gvPGReport_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordIDInList" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblJOBNoInList" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblTableNameInList" runat="server" Visible="false" Text='<%# Eval("TABLE_NAME") %>' />
                            <asp:Label ID="lblPivotGroupCounts" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP_COUNTS") %>' />
                            <asp:TextBox ID="txtSrNoInList" runat="server" Text='<%# Eval("SR_NO") %>'
                                CssClass="textboxcenter" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pivot Group Desc">
                        <ItemTemplate>
                            <asp:Label ID="lblPivotGroupDescInList" runat="server" Visible="true" Text='<%# Eval("PIVOT_GROUP_DESC") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pivot Group">
                        <ItemTemplate>
                            <asp:Label ID="lblPivotGroupInList" runat="server" Visible="true" Text='<%# Eval("PIVOT_GROUP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO Budget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudgetINRInList" runat="server" Visible="false" Text='<%# Eval("PO_BUDGET") %>' />
                            <asp:TextBox ID="txtTotalBudgetINRInList" runat="server" Text='<%# Eval("PO_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sale Estimate Budget">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleEstimateBudgetINRInList" runat="server" Visible="false" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>' />
                            <asp:TextBox ID="txtSaleEstimateBudgetINRInList" runat="server" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO Value[INR]">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTotalPOValueINR" runat="server" Visible="true" Text='<%# Eval("PO_VALUE_INR") %>' />--%>
                            <asp:TextBox ID="txtTotalPOValueINRInList" runat="server" Text='<%# Eval("PO_VALUE_INR") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delta As Per PO Budget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalDeltaAsPerPOBudgetINRInList" runat="server" Visible="false" Text='<%# Eval("DELTA_AS_PER_PO_BUDGET") %>' />
                            <asp:TextBox ID="txtTotalDeltaAsPerPOBudgetINRInList" runat="server" Text='<%# Eval("DELTA_AS_PER_PO_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delta As Per Sale Estimate Budget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalDeltaAsPerSaleEstimateBudgetINRInList" runat="server" Visible="false" Text='<%# Eval("DELTA_AS_PER_SALE_ESTIMATE_BUDGET") %>' />
                            <asp:TextBox ID="txtTotalDeltaAsPerSaleEstimateBudgetINRInList" runat="server" Text='<%# Eval("DELTA_AS_PER_SALE_ESTIMATE_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Zoom [PO Budget]" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetailInListPOBudget" Height="20px" Width="20px" CommandArgument="ViewDETAILPOBudget" runat="server"
                                ImageUrl="~/Images/viewdetails.png" ToolTip="View PO details" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Zoom [Sale Estimate]" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetailInListSaleEstimate" Height="20px" Width="20px" CommandArgument="ViewDETAILSaleEstimate" runat="server"
                                ImageUrl="~/Images/viewdetails.png" ToolTip="View pivot group details" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatusInList" runat="server" Width="90px" Height="26px" Text='<%# Eval("STATUS") %>'>
                                <asp:ListItem Text="Open" Value="OPEN" />
                                <asp:ListItem Text="Close" Value="CLOSE" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pending Cost">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPendingCostInList" runat="server" onKeyUp="GetSaving(this)" Text='<%# Eval("PENDING_COST") %>'
                                CssClass="textbox" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Saving">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSavingInList" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("SAVING_COST") %>' CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Saving Of Original Estimate[%]">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSavingOfOriginalEstimatePercentageInList" runat="server"
                                Text='<%# Eval("SAVING_OF_ORIGINAL_ESTIMATE_PERCENTAGE") %>' CssClass="textboxrightsmall"
                                onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VPOC">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlVPOCInList" runat="server" Width="80px" Height="26px" Text='<%# Eval("IS_VPOC_ID") %>'>
                                <asp:ListItem Text="No" Value="0" />
                                <asp:ListItem Text="Yes" Value="1" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Likely Delivery Date">
                        <ItemTemplate>
                            <asp:Label ID="lblLikelyDeliveryDateInList" runat="server" Visible="true" Text='<%# Eval("LIKELY_DELIVERY_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plan Date Of Procurement">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanDateOfProcurementInList" runat="server" Visible="true" Text='<%# Eval("PLAN_DATE_OF_PROCUREMENT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

        </div>

    </div>



    <asp:Button ID="btnShowPOListPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePOList" runat="server" TargetControlID="btnShowPOListPopup"
        PopupControlID="pnlPOListPopup" CancelControlID="imgBtnPOListCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPOListPopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPOListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>PO List:
                    <asp:Label ID="lblPOListRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Pivot Group:</label>
                        <asp:TextBox ID="txtPivotGroupInPOList" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total Budget [INR]:</label>
                        <asp:TextBox ID="txtTotalBudgetInPOList" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total PO Value [INR]:</label>
                        <asp:TextBox ID="txtTotalPOValueINRInPOList" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total Delta [INR]:</label>
                        <asp:TextBox ID="txtTotalDeltaInPOList" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnRefreshPOList" CssClass="button" Width="100%" runat="server" Text="Refresh"
                        OnClick="btnRefreshPOList_Click" />

                    <asp:Button ID="btnExportPOList" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportPOList_Click" />

                    <asp:Button ID="btnAddPivotGroupLog" CssClass="button" Width="100%" runat="server" Text="Add Pivot Group"
                        OnClick="btnAddPivotGroupLog_Click" />

                </div>
            </div>

            <div class="employee-grid-container">
                <asp:Panel ID="pnlMsgPOList" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblMsgPOList" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPOList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPOList_RowDataBound"
                    OnRowCommand="gvPOList_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Zoom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View PO Detail" />
                                <asp:Label ID="lblSrNo" runat="server" Visible="false" Text='<%# Eval("SR_NO") %>' />
                                <asp:Label ID="lblAmount" runat="server" Visible="false" Text='<%# Eval("PO_VALUE_INR") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblDocClass" runat="server" Visible="false" Text='<%# Eval("DOC_CLASS") %>' />

                                <asp:Label ID="lblOldPivotGroup" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP") %>' />

                                <asp:Label ID="lblBudget" runat="server" Visible="false" Text='<%# Eval("BUDGET") %>' />
                                <asp:Label ID="lblDelta" runat="server" Visible="false" Text='<%# Eval("DELTA") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnUpdatePivotGroup" Height="20px" Width="20px" CommandArgument="UPDATE"
                                    runat="server" ImageUrl="~/Images/LOT/edit.jpg" ToolTip="Update Pivot Group" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Post" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPostPivotGroup" Height="20px" Width="20px" CommandArgument="POST"
                                    runat="server" ImageUrl="~/Images/LOT/edit5.png" ToolTip="Post Pivot Group" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="Pivot Group" DataField="PIVOT_GROUP" />
                        <asp:BoundField HeaderText="PO No" DataField="PO_NO" />
                        <asp:BoundField HeaderText="PO Date" DataField="PO_DATE" />
                        <asp:BoundField HeaderText="Expected Date" DataField="EXPECTED_DATE" />
                        <asp:BoundField HeaderText="Vendor Code" DataField="VENDOR_CODE" />
                        <asp:BoundField HeaderText="Vendor Name" DataField="VENDOR_NAME" />

                        <asp:BoundField HeaderText="PO Value[FC]" DataField="PO_VALUE" />
                        <asp:BoundField HeaderText="Curr Desc" DataField="CURR_DESC" />
                        <asp:BoundField HeaderText="Curr Rate" DataField="CURR_RATE" />

                        <asp:BoundField HeaderText="Budget" DataField="BUDGET" />
                        <asp:BoundField HeaderText="PO Value[INR]" DataField="PO_VALUE_INR" />
                        <asp:BoundField HeaderText="Delta" DataField="DELTA" />

                        <asp:BoundField HeaderText="Doc Class" DataField="DOC_CLASS" />
                        <asp:BoundField HeaderText="PO Status" DataField="PO_STATUS" />
                        <asp:BoundField HeaderText="Revision" DataField="REVISION" />

                        <asp:BoundField HeaderText="Location" DataField="LOCATION" />
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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


    <asp:Button ID="btnShowPOItemListPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePOItemList" runat="server" TargetControlID="btnShowPOItemListPopup"
        PopupControlID="pnlPOItemListPopup" CancelControlID="imgBtnPOItemListCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPOItemListPopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPOItemListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>PO Detail:
                    <asp:Label ID="lblPOItemListRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>PO No.:</label>
                        <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" Enabled="false" />

                        <label>PO Date:</label>
                        <asp:TextBox ID="txtPODate" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Vendor:</label>
                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" Enabled="false" />
                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total PO Value:</label>
                        <asp:TextBox ID="txtTotalPOValue" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total Invoice Value:</label>
                        <asp:TextBox ID="txtTotalInvoiceValue" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total PO Quantity:</label>
                        <asp:TextBox ID="txtTotalPOQuantity" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total MRN Quantity:</label>
                        <asp:TextBox ID="txtTotalMRNQuantity" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnExportPOItemList" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportPOItemList_Click" />

                </div>
            </div>

            <div class="employee-grid-container">

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPOItemList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="true" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPOItemList_RowDataBound  ">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPOValue" runat="server" Visible="false" Text='<%# Eval("PO_VALUE") %>' />
                                <asp:Label ID="lblPOQuantity" runat="server" Visible="false" Text='<%# Eval("PO_QUANTITY") %>' />
                                <asp:Label ID="lblMRNQuantity" runat="server" Visible="false" Text='<%# Eval("MRN_QUANTITY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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


    <asp:Button ID="btnShowPivotGroupDetailsPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePivotGroupDetails" runat="server" TargetControlID="btnShowPivotGroupDetailsPopup"
        PopupControlID="pnlPivotGroupDetailsPopup" CancelControlID="imgBtnPivotGroupDetailsCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPivotGroupDetailsPopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPivotGroupDetailsCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblPivotGroupDetailsLegend" runat="server" />:
                        <asp:Label ID="lblPivotGroupDetailsRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Total Qty:</label>
                        <asp:TextBox ID="txtTotalQty" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Unit Rate Indian Imported1:</label>
                        <asp:TextBox ID="txtTotalUnitRateIndianImported1" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total Unit Rate Indian Imported2:</label>
                        <asp:TextBox ID="txtTotalUnitRateIndianImported2" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Unit Rate Indian Imported:</label>
                        <asp:TextBox ID="txtTotalUnitRateIndianImported" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Indian Cost Raised 5%:</label>
                        <asp:TextBox ID="txtTotalIndianCostRaised5Perc" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Imported Fob Price:</label>
                        <asp:TextBox ID="txtTotalImportedFobPrice" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Equiv Rupee Price:</label>
                        <asp:TextBox ID="txtTotalEquivRupeePrice" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Full Customs Duty On Imports 10%:</label>
                        <asp:TextBox ID="txtTotalFullCustomsDutyOnImports10Perc" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Cost INR:</label>
                        <asp:TextBox ID="txtTotalCostINR" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Cost Euro:</label>
                        <asp:TextBox ID="txtTotalCostEuro" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnExportPivotGroupDetail" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportPivotGroupDetail_Click" />

                </div>
            </div>

            <div class="employee-grid-container">

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPivotGroupDetails" runat="server" CellPadding="4" ForeColor="#333333"
                    Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPivotGroupDetails_RowDataBound  ">
                    <Columns>
                        <asp:BoundField HeaderText="Fact Code" DataField="FACT_CODE" />
                        <asp:BoundField HeaderText="Factory Items Assembly Bought Out" DataField="FACTORY_ITEMS_ASSEMBLY_BOUGHT_OUT" />
                        <asp:BoundField HeaderText="System No" DataField="SYSTEM_NO" />
                        <asp:BoundField HeaderText="Assembly" DataField="ASSEMBLY" />
                        <asp:BoundField HeaderText="Equipment" DataField="EQUIPMENT" />
                        <asp:BoundField HeaderText="Codes" DataField="CODES" />
                        <asp:BoundField HeaderText="Pos No" DataField="POS_NO" />
                        <asp:BoundField HeaderText="Category" DataField="CATEGORY" />
                        <asp:BoundField HeaderText="Qty Line1" DataField="QTY_LINE1" />
                        <asp:BoundField HeaderText="Qty Line2" DataField="QTY_LINE2" />
                        <asp:BoundField HeaderText="Qty Line3" DataField="QTY_LINE3" />
                        <asp:BoundField HeaderText="Qty" DataField="QTY" />
                        <asp:BoundField HeaderText="Item" DataField="ITEM" />
                        <asp:BoundField HeaderText="Tag No" DataField="TAG_NO" />
                        <asp:BoundField HeaderText="Description" DataField="DESCRIPTION" />
                        <asp:BoundField HeaderText="Note" DataField="NOTE" />
                        <asp:BoundField HeaderText="Currency" DataField="CURRENCY" />
                        <asp:BoundField HeaderText="Unit Rate Indian Imported1" DataField="UNIT_RATE_INDIAN_IMPORTED1" />
                        <asp:BoundField HeaderText="Unit Rate Indian Imported2" DataField="UNIT_RATE_INDIAN_IMPORTED2" />
                        <asp:BoundField HeaderText="Total Unit Rate Indian Imported" DataField="TOTAL_UNIT_RATE_INDIAN_IMPORTED" />
                        <asp:BoundField HeaderText="Indian Cost Raised 5 Perc" DataField="INDIAN_COST_RAISED_5_PERC" />
                        <asp:BoundField HeaderText="Imported Fob Price" DataField="IMPORTED_FOB_PRICE" />
                        <asp:BoundField HeaderText="Equiv Rupee Price" DataField="EQUIV_RUPEE_PRICE" />
                        <asp:BoundField HeaderText="Full Customs Duty On Imports 10 Perc" DataField="FULL_CUSTOMS_DUTY_ON_IMPORTS_10_PERC" />
                        <asp:BoundField HeaderText="Total Cost Inr" DataField="TOTAL_COST_INR" />
                        <asp:BoundField HeaderText="Total Cost Euro" DataField="TOTAL_COST_EURO" />
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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


    <asp:Button ID="btnShowUpdatePivotGroupPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdatePivotGroup" runat="server" TargetControlID="btnShowUpdatePivotGroupPopup"
        PopupControlID="pnlUpdatePivotGroupPopup" CancelControlID="imgBtnUpdatePivotGroupCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlUpdatePivotGroupPopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnUpdatePivotGroupCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Update Pivot Group</legend>

                <div class="form-grid form-grid-2">

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnitToU" runat="server" CssClass="form-control" Enabled="false" />

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNoToU" runat="server" CssClass="form-control" Enabled="false" />


                    <label>PO No:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:TextBox ID="txtPONoToU" runat="server" CssClass="form-control" Enabled="false" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetPONoToU" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClientClick="return ValidateJOBNoAll();" OnClick="btnGetPONoToU_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Sale Estimate Budget:</label>
                    <asp:TextBox ID="txtSaleEstimateBudgetToU" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Old Pivot Group:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtOldPivotGroupDescToU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtOldPivotGroupToU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>


                    <label>New Pivot Group:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtNewPivotGroupDescToU" runat="server" CssClass="form-control" Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtNewPivotGroupToU" runat="server" CssClass="form-control" Enabled="false" />
                            </td>
                            <td style="width: 10%">
                                <asp:Button ID="btnGetNewPivotGroupToU" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetNewPivotGroupToU_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Likely Delivery Date:</label>
                    <asp:TextBox ID="txtLikelyDeliveryDateToU" runat="server" Width="100%" Enabled="false" />

                    <label>Planned Date of Procurement:</label>
                    <asp:TextBox ID="txtPlannedDateOfProcurementToU" runat="server" Width="100%" Enabled="false" />

                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnUpdatePivotGroup" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClientClick="return ValidateAllToUpdate();" OnClick="btnUpdatePivotGroup_Click" />

            </div>

            <div class="full-width">
                <asp:Panel ID="pnlUpdatePivotGroup" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblUpdatePivotGroup" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>


    </asp:Panel>


    <%--PO DETAIL-OLD PIVOT GROUP START--%>
    <asp:Button ID="btnShowPopupOldPODetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeOldPODetail" runat="server" TargetControlID="btnShowPopupOldPODetail"
        PopupControlID="pnlPopupOldPODetail" CancelControlID="imgBtnCancelOldPODetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupOldPODetail" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelOldPODetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblPORecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.:</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <label>PO No.:</label>
                        <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSearchOldPivotGroupPONo" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchOldPivotGroupPONo_Click" />
                </div>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="lblPOMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPOOldPivotGroupDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvPOOldPivotGroupDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get PO Detail">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                <asp:Label ID="lblOldPivotGroup" runat="server" Visible="false" Text='<%# Eval("OLD_PIVOT_GROUP") %>' />
                                <asp:Label ID="lblOldPivotGroupDesc" runat="server" Visible="false" Text='<%# Eval("OLD_PIVOT_GROUP_DESC") %>' />
                                <asp:Button ID="btnGetOldPODetail" CommandArgument="GET" ToolTip="Get PO Detail" Width="100%"
                                    runat="server" Text="Get PO Detail" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="Job No." />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO No." />
                        <asp:BoundField DataField="OLD_PIVOT_GROUP" HeaderText="Old Pivot Group" />
                        <asp:BoundField DataField="OLD_PIVOT_GROUP_DESC" HeaderText="Old Pivot Group Desc" />
                        <asp:BoundField DataField="UNIT" HeaderText="Unit" />
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
    <%--PO DETAIL-OLD PIVOT GROUP END--%>


    <%--NEW PIVOT GROUP DETAIL START--%>
    <asp:Button ID="btnShowPopupPONewPGDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePONewPGDetail" runat="server" TargetControlID="btnShowPopupPONewPGDetail"
        PopupControlID="pnlPopupPONewPGDetail" CancelControlID="imgBtnCancelPONewPGDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupPONewPGDetail" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPONewPGDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblPONewPGRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Pivot Group:</label>
                        <asp:TextBox ID="txtPivotGroupSearch" runat="server" CssClass="form-control" />

                        <label>Pivot Group Desc:</label>
                        <asp:TextBox ID="txtPivotGroupDescSearch" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnSearchPONoNewPG" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchPONoNewPG_Click" />

                </div>
            </div>

            <div class="employee-grid-container">
                <asp:Label ID="lblPONewPGMsg" runat="server" />

                <asp:Panel ID="pnlNewPGDetail" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblNewPGDetail" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPONewPGDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvPONewPGDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get Pivot Group">
                            <ItemTemplate>
                                <asp:Label ID="lblNewPivotGroup" runat="server" Visible="false" Text='<%# Eval("NEW_PIVOT_GROUP") %>' />
                                <asp:Label ID="lblNewPivotGroupDesc" runat="server" Visible="false" Text='<%# Eval("NEW_PIVOT_GROUP_DESC") %>' />
                                <asp:Label ID="lblLikelyDeliveryDate" runat="server" Visible="false" Text='<%# Eval("LIKELY_DELIVERY_DATE") %>' />
                                <asp:Label ID="lblPlanDateOfProcurement" runat="server" Visible="false" Text='<%# Eval("PLAN_DATE_OF_PROCUREMENT") %>' />
                                <asp:Button ID="btnGetPivotGroupetail" CommandArgument="GET" ToolTip="Get New Pviot Group" Width="100%"
                                    runat="server" Text="Get New Pivot Group" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NEW_PIVOT_GROUP" HeaderText="New Pivot Group" />
                        <asp:BoundField DataField="NEW_PIVOT_GROUP_DESC" HeaderText="New Pivot Group Desc" />
                        <asp:TemplateField HeaderText="Sale Estimate Budget">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSaleEstimateBudgetInList" Width="100%" runat="server" onKeyUp="GetSaving(this)" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>'
                                    CssClass="textbox" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LIKELY_DELIVERY_DATE" HeaderText="Likely Delivery Date" />
                        <asp:BoundField DataField="PLAN_DATE_OF_PROCUREMENT" HeaderText="Plan Date Of Procurement" />
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
    <%--NEW PIVOT GROUP DETAIL END--%>



    <%--POST PIVOT GROUP START--%>
    <asp:Button ID="btnShowPostPivotGroupPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePostPivotGroup" runat="server" TargetControlID="btnShowPostPivotGroupPopup"
        PopupControlID="pnlPostPivotGroupPopup" CancelControlID="imgBtnPostPivotGroupCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPostPivotGroupPopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPostPivotGroupCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Post Pivot Group</legend>

                <div class="form-grid form-grid-2">


                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNoToPost" runat="server" Width="100%" Enabled="false" />


                    <td style="width: 15%;">Posting Month:</td>
                    <asp:TextBox ID="txtPostingMonthToPost" runat="server" Width="100%" Enabled="false" />

                    <td>Pivot Group:</td>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtPivotGroupDescToPost" runat="server" Width="100%" Enabled="false" onblur="return ValidateNewPivotGroupDesc();" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtPivotGroupToPost" runat="server" Width="100%" Enabled="false" onblur="return ValidateNewPivotGroup();" />
                            </td>
                        </tr>
                    </table>


                    <label>PO Budget:</label>
                    <asp:TextBox ID="txtPOBudgetToPost" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Sale Estimate Budget:</label>
                    <asp:TextBox ID="txtSaleEstimateBudgetToPost" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="Calculation(this)" />

                    <label>PO Value[INR]:</label>
                    <asp:TextBox ID="txtPOValueINRToPost" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Delta As Per PO Budget:</label>
                    <asp:TextBox ID="txtDeltaAsPerPOBudgetToPost" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Delta As Per Sale Estimate Budget:</label>
                    <asp:TextBox ID="txtDeltaAsPerSaleEstimateBudgetToPost" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Pending Cost:</label>
                    <asp:TextBox ID="txtPendingCostToPost" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="Calculation(this)" />

                    <label>Saving Cost:</label>
                    <asp:TextBox ID="txtSavingCostToPost" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Saving of Original Estimate (%):</label>
                    <asp:TextBox ID="txtSavingofOriginalEstimatePercToPost" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Likely Delivery Date:</label>
                    <asp:TextBox ID="txtLikelyDeliveryDateToPost" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <label>Plan Date of Procurement:</label>
                    <asp:TextBox ID="txtPlannedDateOfProcurementToPost" runat="server" Enabled="false"
                        CssClass="form-control" />


                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnSavePostedPivotGroup" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClientClick="return ValidateAllToPost();" OnClick="btnSavePostedPivotGroup_Click" />

            </div>

            <div class="full-width">
                <asp:Panel ID="pnlPostPivotGroup" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblPostPivotGroup" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>


    </asp:Panel>
    <%--POST PIVOT GROUP END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
