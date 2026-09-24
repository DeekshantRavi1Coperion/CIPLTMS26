<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="UnpostedListByGL.aspx.cs"
    Inherits="USGAAP_BILLING_UnpostedListByGL" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable = no" />
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <script type="text/Javascript">
        function EnableDesableRevenueTypes(el) {
            var row = el.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            row.cells[24].childNodes[1].selectedIndex = '0';
            row.cells[25].childNodes[1].selectedIndex = '0';
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
        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
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
        function checkDec3(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
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

    <script type="text/Javascript">

        function checkDec1(el) {

            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    var row = el.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;

                    var lblAmountInr = row.cells[12].innerHTML;
                    var pendingpostedValue = row.cells[14].innerHTML;
                    var postingValue = row.cells[15].getElementsByTagName("input")[0].value;

                    if (parseFloat(pendingpostedValue) == '0' || pendingpostedValue == '') {
                        unpostedValue = Math.round((parseFloat(lblAmountInr) - parseFloat(postingValue)) * 100) / 100;
                        row.cells[17].getElementsByTagName("input")[0].value = unpostedValue;

                        if (parseFloat(postingValue) == '0' || postingValue == '') {
                            row.cells[17].getElementsByTagName("input")[0].value = lblAmountInr;
                        }

                        if (parseFloat(lblAmountInr) < parseFloat(postingValue)) {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                            document.getElementById('<%=btnPost.ClientID %>').visibility = 'hidden';
                            return true;
                        }
                        else {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                            return false;
                        }
                    }
                    else {

                        unpostedValue = Math.round((parseFloat(pendingpostedValue) - parseFloat(postingValue)) * 100) / 100;
                        row.cells[17].getElementsByTagName("input")[0].value = unpostedValue;

                        if (parseFloat(postingValue) == '0' || postingValue == '') {
                            row.cells[17].getElementsByTagName("input")[0].value = pendingpostedValue;
                        }

                        if (parseFloat(pendingpostedValue) < parseFloat(postingValue)) {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                            return true;
                        }
                        else {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                            return false;
                        }
                    }
                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblAmountInr = row.cells[12].innerHTML;
                var pendingpostedValue = row.cells[14].innerHTML;
                var postingValue = row.cells[15].getElementsByTagName("input")[0].value;

                if (parseFloat(pendingpostedValue) == '0' || pendingpostedValue == '') {
                    unpostedValue = Math.round((parseFloat(lblAmountInr) - parseFloat(postingValue)) * 100) / 100;
                    row.cells[17].getElementsByTagName("input")[0].value = unpostedValue;


                    if (parseFloat(postingValue) == '0' || postingValue == '') {
                        row.cells[17].getElementsByTagName("input")[0].value = lblAmountInr;
                    }

                    if (parseFloat(lblAmountInr) < parseFloat(postingValue)) {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                }
                else {
                    unpostedValue = Math.round((parseFloat(pendingpostedValue) - parseFloat(postingValue)) * 100) / 100;
                    row.cells[17].getElementsByTagName("input")[0].value = unpostedValue;


                    if (parseFloat(postingValue) == '0' || postingValue == '') {
                        row.cells[17].getElementsByTagName("input")[0].value = pendingpostedValue;
                    }

                    if (parseFloat(pendingpostedValue) < parseFloat(postingValue)) {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                }
            }
        }
    </script>

    <script type="text/Javascript">
        function checkDec2(el) {

            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    var row = el.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;

                    var lblAmountInr = row.cells[12].innerHTML;
                    var pendingpostedValue = row.cells[14].innerHTML;
                    var unpostedValue = row.cells[17].getElementsByTagName("input")[0].value;

                    if (parseFloat(pendingpostedValue) == '0' || pendingpostedValue == '') {
                        postingValue = Math.round((parseFloat(lblAmountInr) - parseFloat(unpostedValue)) * 100) / 100;
                        row.cells[15].getElementsByTagName("input")[0].value = postingValue;

                        if (parseFloat(unpostedValue) == '0' || unpostedValue == '') {
                            row.cells[15].getElementsByTagName("input")[0].value = lblAmountInr;
                        }

                        if (parseFloat(lblAmountInr) < parseFloat(row.cells[15].getElementsByTagName("input")[0].value)) {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                            return true;
                        }
                        else {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                            return false;
                        }
                    }
                    else {
                        postingValue = Math.round((parseFloat(lblAmountInr) - parseFloat(unpostedValue)) * 100) / 100;
                        row.cells[15].getElementsByTagName("input")[0].value = postingValue;

                        if (parseFloat(unpostedValue) == '0' || unpostedValue == '') {
                            row.cells[15].getElementsByTagName("input")[0].value = pendingpostedValue;
                        }

                        if (parseFloat(pendingpostedValue) < parseFloat(row.cells[15].getElementsByTagName("input")[0].value)) {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                            return true;
                        }
                        else {
                            row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                            return false;
                        }
                    }
                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblAmountInr = row.cells[12].innerHTML;
                var pendingpostedValue = row.cells[14].innerHTML;
                var unpostedValue = row.cells[17].getElementsByTagName("input")[0].value;

                if (parseFloat(pendingpostedValue) == '0' || pendingpostedValue == '') {
                    postingValue = Math.round((parseFloat(lblAmountInr) - parseFloat(unpostedValue)) * 100) / 100;
                    row.cells[15].getElementsByTagName("input")[0].value = postingValue;

                    if (parseFloat(unpostedValue) == '0' || unpostedValue == '') {
                        row.cells[15].getElementsByTagName("input")[0].value = lblAmountInr;
                    }

                    if (parseFloat(lblAmountInr) < parseFloat(row.cells[15].getElementsByTagName("input")[0].value)) {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                }
                else {
                    postingValue = Math.round((parseFloat(lblAmountInr) - parseFloat(unpostedValue)) * 100) / 100;
                    row.cells[15].getElementsByTagName("input")[0].value = postingValue;

                    if (parseFloat(unpostedValue) == '0' || unpostedValue == '') {
                        row.cells[15].getElementsByTagName("input")[0].value = pendingpostedValue;
                    }

                    if (parseFloat(pendingpostedValue) < parseFloat(row.cells[15].getElementsByTagName("input")[0].value)) {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[15].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                }
            }
        }
    </script>

    <style type="text/css">
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url( '../Images/pageLoader.gif' ) 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
    </style>

    <script type="text/javascript">
        var GridId = "<%=gvPostingList.ClientID %>";
        var ScrollHeight = 490;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>

    <script type="text/javascript">

        function ConfirmPost() {
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;
            if (parseInt(GVRowCount) > 0) {

                if (confirm("Would you like to post?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdGVRowCount" runat="server" />
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Unposted List GL wise:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

                <div class="form-grid form-grid-3">
                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server"
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch"
                                    Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
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
                                <asp:TextBox ID="txtEndDateSearch" runat="server"
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch"
                                    Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />

                    <label>Invoice No.</label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtInvoiceNo"
                                    CssClass="form-control"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Posting Month</label>
                    <asp:DropDownList ID="ddlPostingMonthTop" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlPostingMonthTop_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Jan" />
                        <asp:ListItem Value="2" Text="Feb" />
                        <asp:ListItem Value="3" Text="Mar" />
                        <asp:ListItem Value="4" Text="Apr" />
                        <asp:ListItem Value="5" Text="May" />
                        <asp:ListItem Value="6" Text="Jun" />
                        <asp:ListItem Value="7" Text="Jul" />
                        <asp:ListItem Value="8" Text="Aug" />
                        <asp:ListItem Value="9" Text="Sep" />
                        <asp:ListItem Value="10" Text="Oct" />
                        <asp:ListItem Value="11" Text="Nov" />
                        <asp:ListItem Value="12" Text="Dec" />
                    </asp:DropDownList>

                    <label>Posting Year</label>
                    <asp:DropDownList ID="ddlPostingYearTop" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlPostingYearTop_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="2015" Text="2015" />
                        <asp:ListItem Value="2016" Text="2016" />
                        <asp:ListItem Value="2017" Text="2017" />
                        <asp:ListItem Value="2018" Text="2018" />
                        <asp:ListItem Value="2019" Text="2019" />
                        <asp:ListItem Value="2020" Text="2020" />
                        <asp:ListItem Value="2021" Text="2021" />
                        <asp:ListItem Value="2022" Text="2022" />
                        <asp:ListItem Value="2023" Text="2023" />
                        <asp:ListItem Value="2024" Text="2024" />
                        <asp:ListItem Value="2025" Text="2025" />
                        <asp:ListItem Value="2026" Text="2026" />
                        <asp:ListItem Value="2027" Text="2027" />
                        <asp:ListItem Value="2028" Text="2028" />
                        <asp:ListItem Value="2029" Text="2029" />
                        <asp:ListItem Value="2030" Text="2030" />
                    </asp:DropDownList>

                    <asp:CheckBox ID="chkClearUnclearAll" runat="server" Text="Clear/Unclear All"
                        OnCheckedChanged="chkClearUnclearAll_CheckedChanged"
                        AutoPostBack="true" />

                    <asp:Button
                        ID="btnPost"
                        CssClass="button"
                        Width="100%"
                        runat="server"
                        Text="Post"
                        OnClick="btnPost_Click"
                        OnClientClick="return ConfirmPost();" />

                    <asp:Button
                        ID="btnExport"
                        CssClass="button"
                        Width="100%"
                        runat="server"
                        Text="Export"
                        OnClick="btnExport_Click" />

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
                ID="gvPostingList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" PageSize="18" HorizontalAlign="Center" OnRowDataBound="gvPostingList_RowDataBound"
                OnRowCommand="gvPostingList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                    <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" />
                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="INVOICE_DATE" />
                    <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT" HeaderText="REVENUE_ACCOUNT" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT_DESC" HeaderText="ACCOUNT_DESC" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT_TYPE" HeaderText="ACCOUNT_TYPE" />
                    <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                    <asp:BoundField DataField="PRODUCT_RATE" HeaderText="PRODUCT_RATE" />
                    <asp:BoundField DataField="INVOICE_AMOUNT" HeaderText="INVOICE_AMOUNT" />
                    <asp:BoundField DataField="POSTED_VALUE" HeaderText="POSTED_VALUE" />
                    <asp:BoundField DataField="PENDING_UNPOSTED_VALUE" HeaderText="PENDING_UNPOSTED_VALUE" />
                    <asp:TemplateField HeaderText="POSTING_VALUE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPostingValue" runat="server" onKeyUp="checkDec1(this)" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblInvoiceNo" runat="server" Visible="false" Text='<%# Eval("INVOICE_NO") %>' />
                            <asp:Label ID="lblInvoiceDate" runat="server" Visible="false" Text='<%# Eval("INVOICE_DATE") %>' />
                            <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblBusinessUnit" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT") %>' />
                            <asp:Label ID="lblRevenueAccount" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT") %>' />
                            <asp:Label ID="lblRevenueAccountDesc" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_DESC") %>' />
                            <asp:Label ID="lblRevenueAccountType" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_TYPE") %>' />
                            <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                            <asp:Label ID="lblProductRate" runat="server" Visible="false" Text='<%# Eval("PRODUCT_RATE") %>' />
                            <asp:Label ID="lblInvoiceAmount" runat="server" Visible="false" Text='<%# Eval("INVOICE_AMOUNT") %>' />
                            <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                            <asp:Label ID="lblPostedValue" runat="server" Visible="false" Text='<%# Eval("POSTED_VALUE") %>' />
                            <asp:Label ID="lblPendingUnpostedValue" runat="server" Visible="false" Text='<%# Eval("PENDING_UNPOSTED_VALUE") %>' />
                            <asp:Label ID="lblEndMarket" runat="server" Visible="false" Text='<%# Eval("END_MARKET") %>' />
                            <asp:Label ID="lblCountry" runat="server" Visible="false" Text='<%# Eval("GEOGROPHY") %>' />
                            <asp:Label ID="lblPostingCurrencyID" runat="server" Visible="false" Text='<%# Eval("POSTED_CURRENCY_ID") %>' />
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("STATUS") %>' />
                            <asp:Label ID="lblTypeID" runat="server" Visible="false" Text='<%# Eval("TYPE_ID") %>' />
                            <asp:Label ID="lblRevenueTypeID" runat="server" Visible="false" Text='<%# Eval("REVENUE_TYPE_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSTING_CURRENCY">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlPostingCurrency" runat="server" Width="100%">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNPOSTED_VALUE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUnPostedValue" runat="server" onKeyUp="checkDec(this)" Text="0.00"
                                onkeyDown="javascript:preventInput(event);" onpaste="return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="END_MARKET" HeaderText="END_MARKET" />--%>
                    <asp:TemplateField HeaderText="END_MARKET">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlEndMarket" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="GEOGROPHY" HeaderText="GEOGROPHY" />--%>
                    <asp:TemplateField HeaderText="GEOGROPHY">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCountry" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSTING_MONTH">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlPostingMonth" runat="server" Width="100%" Enabled="true">
                                <asp:ListItem Value="1" Text="Jan" />
                                <asp:ListItem Value="2" Text="Feb" />
                                <asp:ListItem Value="3" Text="Mar" />
                                <asp:ListItem Value="4" Text="Apr" />
                                <asp:ListItem Value="5" Text="May" />
                                <asp:ListItem Value="6" Text="Jun" />
                                <asp:ListItem Value="7" Text="Jul" />
                                <asp:ListItem Value="8" Text="Aug" />
                                <asp:ListItem Value="9" Text="Sep" />
                                <asp:ListItem Value="10" Text="Oct" />
                                <asp:ListItem Value="11" Text="Nov" />
                                <asp:ListItem Value="12" Text="Dec" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POSTING_YEAR">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlPostingYear" runat="server" Width="100%" Enabled="true">
                                <asp:ListItem Value="2015" Text="2015" />
                                <asp:ListItem Value="2016" Text="2016" />
                                <asp:ListItem Value="2017" Text="2017" />
                                <asp:ListItem Value="2018" Text="2018" />
                                <asp:ListItem Value="2019" Text="2019" />
                                <asp:ListItem Value="2020" Text="2020" />
                                <asp:ListItem Value="2021" Text="2021" />
                                <asp:ListItem Value="2022" Text="2022" />
                                <asp:ListItem Value="2023" Text="2023" />
                                <asp:ListItem Value="2024" Text="2024" />
                                <asp:ListItem Value="2025" Text="2025" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TYPE" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" Width="150px" Enabled="true" onchange="EnableDesableRevenueTypes(this)">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REVENUE_TYPE">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlRevenueType" runat="server" Width="100%" Enabled="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOT_REVENUE_TYPE">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlNotRevenueType" runat="server" Width="100%" Enabled="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POST" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnPost" CommandArgument="POST" runat="server" Text="Post" CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REV_REC_REDUCTION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRevRecReduction" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF3" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF4" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF5" runat="server" />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
