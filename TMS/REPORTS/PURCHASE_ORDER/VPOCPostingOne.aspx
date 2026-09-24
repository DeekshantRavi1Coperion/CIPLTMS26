<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="VPOCPostingOne.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_VPOCPostingOne" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .aligntxtcenter {
            text-align: center;
        }

        .aligntxtleft {
            text-align: left;
        }

        .aligntxtright {
            text-align: right;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
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
        function ValidateDateRangeBasedUponLDOD(el) {
            var row = el.parentNode.parentNode;



            //PO_DATE
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");

            var dateItems = row.cells[1].getElementsByTagName("input")[0].value.split("-");

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
            //month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);




            //DOD
            var dodFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var dodFormatItems = dodFormatLowerCase.split("-");

            var dodDateItems = row.cells[8].getElementsByTagName("input")[0].value.split("-");

            var dodMonthIndex = dodFormatItems.indexOf("mmm");
            var dodDayIndex = dodFormatItems.indexOf("dd");
            var dodYearIndex = dodFormatItems.indexOf("yyyy");
            var dodMonth;
            if (dodDateItems[dodMonthIndex] == 'Jan') {
                dodMonth = 1;
            }
            else if (dodDateItems[dodMonthIndex] == 'Feb') {
                dodMonth = 2;
            }
            else if (dodDateItems[dodMonthIndex] == 'Mar') {
                dodMonth = 3;
            }
            else if (dodDateItems[dodMonthIndex] == 'Apr') {
                dodMonth = 4;
            }
            else if (dodDateItems[dodMonthIndex] == 'May') {
                dodMonth = 5;
            }
            else if (dodDateItems[dodMonthIndex] == 'Jun') {
                dodMonth = 6;
            }
            else if (dodDateItems[dodMonthIndex] == 'Jul') {
                dodMonth = 7;
            }
            else if (dodDateItems[dodMonthIndex] == 'Aug') {
                dodMonth = 8;
            }
            else if (dodDateItems[dodMonthIndex] == 'Sep') {
                dodMonth = 9;
            }
            else if (dodDateItems[dodMonthIndex] == 'Oct') {
                dodMonth = 10;
            }
            else if (dodDateItems[dodMonthIndex] == 'Nov') {
                dodMonth = 11;
            }
            else if (dodDateItems[dodMonthIndex] == 'Dec') {
                dodMonth = 12;
            }
            //dodMonth -= 1;
            var dodFormatedDate = new Date(dodDateItems[dodYearIndex], dodMonth, dodDateItems[dodDayIndex]);





            //LDOD
            var ldodFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var ldodFormatItems = ldodFormatLowerCase.split("-");

            var ldodDateItems = row.cells[11].getElementsByTagName("input")[0].value.split("-");

            var ldodMonthIndex = ldodFormatItems.indexOf("mmm");
            var ldodDayIndex = ldodFormatItems.indexOf("dd");
            var ldodYearIndex = ldodFormatItems.indexOf("yyyy");
            var ldodMonth;
            if (ldodDateItems[ldodMonthIndex] == 'Jan') {
                ldodMonth = 1;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Feb') {
                ldodMonth = 2;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Mar') {
                ldodMonth = 3;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Apr') {
                ldodMonth = 4;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'May') {
                ldodMonth = 5;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Jun') {
                ldodMonth = 6;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Jul') {
                ldodMonth = 7;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Aug') {
                ldodMonth = 8;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Sep') {
                ldodMonth = 9;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Oct') {
                ldodMonth = 10;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Nov') {
                ldodMonth = 11;
            }
            else if (ldodDateItems[ldodMonthIndex] == 'Dec') {
                ldodMonth = 12;
            }
            //ldodMonth -= 1;
            var ldodFormatedDate = new Date(ldodDateItems[ldodYearIndex], ldodMonth, ldodDateItems[ldodDayIndex]);



            //POSTING_MONTH

            var postingFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var postingFormatItems = postingFormatLowerCase.split("-");

            var postingDate = "01-" + (row.cells[15].getElementsByTagName("input")[0].value);
            var postingDateItems = postingDate.split("-");

            var postingMonthIndex = postingFormatItems.indexOf("mmm");
            var postingDayIndex = postingFormatItems.indexOf("dd");
            var postingYearIndex = postingFormatItems.indexOf("yyyy");

            var postingMonth;
            if (postingDateItems[postingMonthIndex] == 'Jan') {
                postingMonth = 1;
            }
            else if (postingDateItems[postingMonthIndex] == 'Feb') {
                postingMonth = 2;
            }
            else if (postingDateItems[postingMonthIndex] == 'Mar') {
                postingMonth = 3;
            }
            else if (postingDateItems[postingMonthIndex] == 'Apr') {
                postingMonth = 4;
            }
            else if (postingDateItems[postingMonthIndex] == 'May') {
                postingMonth = 5;
            }
            else if (postingDateItems[postingMonthIndex] == 'Jun') {
                postingMonth = 6;
            }
            else if (postingDateItems[postingMonthIndex] == 'Jul') {
                postingMonth = 7;
            }
            else if (postingDateItems[postingMonthIndex] == 'Aug') {
                postingMonth = 8;
            }
            else if (postingDateItems[postingMonthIndex] == 'Sep') {
                postingMonth = 9;
            }
            else if (postingDateItems[postingMonthIndex] == 'Oct') {
                postingMonth = 10;
            }
            else if (postingDateItems[postingMonthIndex] == 'Nov') {
                postingMonth = 11;
            }
            else if (postingDateItems[postingMonthIndex] == 'Dec') {
                postingMonth = 12;
            }
            //postingMonth -= 1;
            var postingFormatedDate = new Date(postingDateItems[postingYearIndex], postingMonth, postingDateItems[postingDayIndex]);




            if (ldodFormatedDate < formatedDate) {
                row.cells[1].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[11].getElementsByTagName("input")[0].value = "";
                row.cells[12].getElementsByTagName("input")[0].value = "0";
                alert("Invalid Date Range between PO Date and LDOD Date");
                return true;
            }
            else {
                row.cells[1].getElementsByTagName("input")[0].style.borderColor = "";
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "";
            }

            if (ldodFormatedDate < dodFormatedDate) {
                row.cells[8].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[11].getElementsByTagName("input")[0].value = "";
                row.cells[12].getElementsByTagName("input")[0].value = "0";
                alert("Invalid Date Range between DOD and LDOD Date");
                return true;
            }
            else {
                row.cells[8].getElementsByTagName("input")[0].style.borderColor = "";
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "";
            }

            var poAmount = 0; 
            var COGSLDOD = 0;

            if (row.cells[6].innerText != null && parseFloat(row.cells[6].innerText) > 0) {
                poAmount =  parseFloat(row.cells[6].innerText);
            }
            else {
                poAmount = 0;
            }

           
            var poMonths = parseInt(month);
            var ldodMonths = parseInt((ldodDateItems[ldodYearIndex] - dateItems[yearIndex]) * 12 + (ldodMonth - month) + month);
            var postingMonths = parseInt((postingDateItems[postingYearIndex] - dateItems[yearIndex]) * 12 + (postingMonth - month) + month);
            var monthsBasedUponDOD;

            if (ldodMonths < postingMonths) {
                monthsBasedUponDOD = parseInt(ldodMonths) - parseInt(poMonths);
            }
            else {
                monthsBasedUponDOD = parseInt(postingMonths) - parseInt(poMonths);
            }

            var monthdiff1 = (ldodDateItems[ldodYearIndex] - dateItems[yearIndex]) * 12 + (ldodMonth - month);
            var monthdiff2 = (ldodDateItems[ldodYearIndex] - dodDateItems[dodYearIndex]) * 12 + (ldodMonth - dodMonth);

            if (monthdiff1 >= 0 && monthdiff2 >= 0) {
                row.cells[12].getElementsByTagName("input")[0].value = monthsBasedUponDOD + 1;
                row.cells[12].getElementsByTagName("input")[0].style.borderColor = "";

                COGSLDOD = parseFloat((poAmount * (postingMonths - poMonths)) / (monthsBasedUponDOD + 1));
                row.cells[13].innerText = COGSLDOD;
                row.cells[12].getElementsByTagName("input")[0].style.borderColor = "";
            }
            else {
                row.cells[12].getElementsByTagName("input")[0].value = 0;
                row.cells[13].innerText = 0;
                row.cells[12].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
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


        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

    </script>

    <script type="text/javascript">
        var GridId = "<%=gvVPOCList.ClientID %>";
        var ScrollHeight = 310;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">VPOC Posting </legend>
            <table width="90%">
                <tr>
                    <td>Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" onkeydown="javascript:preventInput(event);"
                                        Width="100%" />
                                    <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                    <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </asp:CalendarExtender>
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
                    <td>End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" onkeydown="javascript:preventInput(event);"
                                        Width="100%" />
                                    <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                    <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                        runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>PO No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPONo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Vendor Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtVendorName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Status:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="OPEN" Value="1"></asp:ListItem>
                            <asp:ListItem Text="CLOSE" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Total Amount:
                    </td>
                    <td>
                        <asp:Label ID="lblTotalAmount" runat="server" Text="0" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Posting Month:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPostingMonth" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                    <asp:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                        PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                        BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                    </asp:CalendarExtender>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Posting Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnPost" CssClass="button" Width="123%" runat="server" Text="Post All"
                            OnClick="btnPost_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>
            <%--<div style='overflow: auto; width: 100%; height: 370px; border: 1px solid lightgray;'>--%>
            <div id="gridContainer" style="overflow-y: scroll; overflow-y: hidden; width: 100%; height: 360px; border: 1px solid lightgray;">
                <asp:GridView ID="gvVPOCList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvVPOCList_RowDataBound" OnRowCommand="gvVPOCList_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="PO_NO" DataField="PO_NO" />

                        <asp:TemplateField HeaderText="PO_DATE">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPODate" runat="server" Width="150px" onkeyDown="javascript:preventInput(event);"
                                    Text='<%# Eval("PO_DATE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="DOC_CLASS" DataField="DOC_CLASS" />
                        <asp:BoundField HeaderText="VENDOR_CODE" DataField="VENDOR_CODE" />
                        <asp:BoundField HeaderText="VENDOR_NAME" DataField="VENDOR_NAME" />



                        <asp:TemplateField HeaderText="ITEM_CATEGORY">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblPODate" runat="server" Visible="false" Text='<%# Eval("PO_DATE") %>' />
                                <asp:Label ID="lblDOCClass" runat="server" Visible="false" Text='<%# Eval("DOC_CLASS") %>' />
                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                                <asp:Label ID="lblPOValueINR" runat="server" Visible="false" Text='<%# Eval("PO_VALUE_INR") %>' />
                                <asp:Label ID="lblPOStatus" runat="server" Visible="false" Text='<%# Eval("PO_STATUS") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:TextBox ID="txtItemCategory" runat="server" Width="150px" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--6--%>
                        <asp:TemplateField HeaderText="PO_VALUE_INR">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("PO_VALUE_INR") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="PO_STATUS" DataField="PO_STATUS" />

                        <%--8--%>
                        <asp:TemplateField HeaderText="DOD">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDOD" runat="server" Width="150px" Text='<%# Eval("DATE_OF_DELIVERY") %>'
                                    onkeyDown="javascript:preventInput(event);" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--9--%>
                        <asp:TemplateField HeaderText="DOD_MONTHS">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMonthsBasedOponDOD" runat="server" Width="99%"
                                    CssClass="aligntxtcenter" onkeyDown="javascript:preventInput(event);" Text="0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DOD_COGS">
                            <ItemTemplate>
                                <asp:Label ID="lblDODCOGS" runat="server" Text="0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--11--%>
                        <asp:TemplateField HeaderText="LDOD">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLikelyDOD" runat="server" Width="110px" onkeyDown="javascript:preventInput(event);"
                                    onchange="ValidateDateRangeBasedUponLDOD(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="calendarLikelyDOD" PopupButtonID="imgbtnLikelyDOD"
                                    runat="server" TargetControlID="txtLikelyDOD" Format="dd-MMM-yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnLikelyDOD" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Likely Date of Delivery" Width="20px" />
                            </ItemTemplate>
                        </asp:TemplateField>




                        <%--12--%>
                        <asp:TemplateField HeaderText="LDOD_MONTHS">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMonthsBasedOponLDOD" runat="server" Width="99%"
                                    CssClass="aligntxtcenter" onkeyDown="javascript:preventInput(event);" Text="0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--13--%>
                        <asp:TemplateField HeaderText="LDOD_COGS">
                            <ItemTemplate>
                                <asp:Label ID="lblLDODCOGS" runat="server" Text="0" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="LOCATION" DataField="LOCATION" />

                        <%--15--%>
                        <asp:TemplateField HeaderText="POSTING_MONTH">
                            <ItemTemplate>

                                <asp:TextBox ID="txtPostingMonth" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POST">
                            <ItemTemplate>
                                <asp:Button ID="btnPost" CommandArgument="POST" runat="server" Text="Post" CssClass="cancelbutton" />
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
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
