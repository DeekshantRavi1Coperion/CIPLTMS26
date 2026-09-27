<%@ Page Title="CIPLTMS-Transmittal To Factory List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTTransmittalFactoryTransferredLOTList.aspx.cs"
    Inherits="PROJECT_LOT_LOTTransmittalFactoryTransferredLOTList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

        <%--function ValidateTFNo() {
            var TFNo = document.getElementById('<%=txtTFNoToEdit.ClientID %>').value;
            if (TFNo == '') {
                document.getElementById('<%=txtTFNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTFNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

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
                if (confirm("Would you like to save linked production order details on LOT?")) {
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


        function ValidateAddSubitem() {
            var check = true;

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



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Transferred LOT List:
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
                                <ajax:CalendarExtender ID="calendarStartDateSearch"
                                    PopupButtonID="imgbtnStartDateSearch"
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

                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch"
                                    PopupButtonID="imgbtnEndDateSearch"
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
                    <asp:TextBox ID="txtTFNo" runat="server" CssClass="form-control" />

                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" />

                    <label>Status.</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control" />

                    <label>LOT For</label>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainSubitems" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Tag Number</label>
                    <asp:TextBox ID="txtTagNumber" runat="server" CssClass="form-control" />

                    <label>Drawing Number</label>
                    <asp:TextBox ID="txtDrawingNumber" runat="server" CssClass="form-control" />

                    <label>Transfer Status</label>
                    <asp:DropDownList ID="ddlTransferStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="1">Not Transferred</asp:ListItem>
                        <asp:ListItem Value="2">Transferred</asp:ListItem>
                        <%--<asp:ListItem Value="3">Passed By Store</asp:ListItem>--%>
                    </asp:DropDownList>

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
                ID="gvLOTTFList" runat="server" AutoGenerateColumns="False"
                CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvLOTTFList_RowCommand" OnRowDataBound="gvLOTTFList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="Link"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
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

                            <asp:Label ID="lblIsTransferred" runat="server" Visible="false" Text='<%# Eval("IS_TRANSFERRED") %>' />
                            <%--<asp:Label ID="lblIsHoldByStore" runat="server" Visible="false" Text='<%# Eval("IS_HOLD_BY_STORE") %>' />--%>

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
                                ImageUrl="~/Images/LOT/link.png" Height="20px" Width="20px"
                                ToolTip="Link Production order number details" />
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


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>TF No. [<asp:Label ID="lblTFNo" runat="server" />] Details</legend>

                <div class="form-grid form-grid-2">

                    <label>TF No.</label>
                    <asp:TextBox ID="txtTFNoToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Company</label>
                    <asp:HiddenField ID="hdCompanyToEdit" runat="server" />
                    <asp:TextBox ID="txtCompanyToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>JOB Number</label>
                    <asp:TextBox ID="txtJOBNoToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control" />



                    <label>Customer PO Number</label>
                    <asp:TextBox ID="txtPONoToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control" />


                    <label>Customer Name</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustomerNameToEdit" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCodeToEdit" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Date</label>
                    <asp:TextBox ID="txtDateToEdit" runat="server" onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control"></asp:TextBox>



                    <%--<label>TF Number</label>
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
                    </table>--%>


                    <%--<div class="full-width">--%>

                    <label>Item</label>
                    <asp:TextBox ID="txtItemNameToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <%--</div>--%>


                    <%--<table style="width: 100%; visibility: hidden;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAddSubitem" runat="server" Width="25%"
                                    Text="Add Subitem"
                                    CssClass="button"
                                    OnClick="btnAddSubitem_Click" />
                            </td>
                        </tr>
                    </table>--%>


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

                                        <asp:TemplateField HeaderText="Link"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
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
                                                    ToolTip="Link production order number details"
                                                    ImageUrl="~/Images/LOT/link.png" Height="20px" Width="20px" />
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


                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarksToEdit" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />

                    <%-- <label>Attachment</label>
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
                    </table>--%>




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
                    <asp:TextBox ID="txtProductionOrderDateToEdit" runat="server"
                        Enabled="false" CssClass="form-control" />


                    <label>Product Code</label>
                    <asp:DropDownList ID="ddlProductCodeToEdit" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProductCodeToEdit_SelectedIndexChanged" />


                    <label>UOM</label>
                    <asp:TextBox ID="txtUOMToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <label>Product Description</label>
                    <asp:TextBox ID="txtProductDescToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Completion Required By</label>
                    <asp:TextBox ID="txtExpectedCompletionDateToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Quantity</label>
                    <asp:HiddenField ID="hdQuantityToEdit" runat="server" />
                    <asp:TextBox ID="txtQuantityToEdit" runat="server"
                        CssClass="form-control"
                        ReadOnly="true"
                        onkeypress="return inNumberKey(this, event);" />

                    <label>Description</label>
                    <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="2"
                        runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>Tag No.</label>
                    <asp:TextBox ID="txtTagNoToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />

                    <div class="full-width">

                        <asp:Panel ID="pnlAttachFiles" runat="server">

                            <label>Drawing (.pdf)</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%;">
                                        <asp:HiddenField ID="hdSiDrawingToEdit1" runat="server" Value="0" />
                                        <asp:TextBox ID="txtSiDrawingToEdit1" runat="server"
                                            Enabled="false"
                                            CssClass="form-control" /></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <asp:ImageButton ID="imgBtnViewSiDrawingToEdit1" runat="server" Height="20px" Width="20px"
                                            ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit1_Click" ToolTip="View" />
                                    </td>
                                </tr>
                            </table>

                            <label>Drawing (.dwg/.dxf)</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%;">
                                        <asp:HiddenField ID="hdSiDrawingToEdit2" runat="server" Value="0" />
                                        <asp:TextBox ID="txtSiDrawingToEdit2" runat="server"
                                            Enabled="false"
                                            CssClass="form-control" /></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <asp:ImageButton ID="imgBtnViewSiDrawingToEdit2" runat="server" Height="20px" Width="20px"
                                            ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit2_Click" ToolTip="View" />
                                    </td>
                                </tr>
                            </table>

                            <label>Drawing 3</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%;">
                                        <asp:HiddenField ID="hdSiDrawingToEdit3" runat="server" Value="0" />
                                        <asp:TextBox ID="txtSiDrawingToEdit3" runat="server"
                                            Enabled="false"
                                            CssClass="form-control" /></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <asp:ImageButton ID="imgBtnViewSiDrawingToEdit3" runat="server" Height="20px" Width="20px"
                                            ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit3_Click" ToolTip="View" />
                                    </td>
                                </tr>
                            </table>

                            <label>Drawing 4</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%;">
                                        <asp:HiddenField ID="hdSiDrawingToEdit4" runat="server" Value="0" />
                                        <asp:TextBox ID="txtSiDrawingToEdit4" runat="server"
                                            Enabled="false" CssClass="form-control" /></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <asp:ImageButton ID="imgBtnViewSiDrawingToEdit4" runat="server" Height="20px" Width="20px"
                                            ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit4_Click" ToolTip="View" />
                                    </td>
                                </tr>
                            </table>


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


        <div class="popup-layout">

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

            <div class="popup-grid-container">
                <div align="center">
                    <asp:Label ID="lblProductonOrderNoMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="popup-grid"
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

        <div class="popup-layout">

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

            <div class="popup-grid-container">
                <div align="center">
                    <asp:Label ID="lblDMSDrawingNoListMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="popup-grid"
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
            id="iframeViewTravelStatementInPDF"
            runat="server"></iframe>
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


        <div class="popup-layout">

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

            <div class="popup-grid-container">
                <asp:GridView
                    CssClass="popup-grid"
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
