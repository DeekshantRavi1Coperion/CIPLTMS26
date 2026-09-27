<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="VendorRatingReport.aspx.cs" Inherits="REPORTS_VendorRatingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

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


            if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
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

    <script type="text/javascript" language="javascript">
        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        var GridId = "<%=gvVendorRating.ClientID %>";
        var ScrollHeight = 570;
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

        function ClearAllFilters() {
            document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtVendorCode.ClientID %>').value = "";
            document.getElementById('<%=txtVendorName.ClientID %>').value = "";


            document.getElementById('<%=ddlSignNoOfCountsForDelivery.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtNoOfCountsForDelivery1.ClientID %>').value = "";
            document.getElementById('<%=txtNoOfCountsForDelivery2.ClientID %>').value = "";


            document.getElementById('<%=ddlSignNoOfCountsForQuality.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtNoOfCountsForQuality1.ClientID %>').value = "";
            document.getElementById('<%=txtNoOfCountsForQuality2.ClientID %>').value = "";

            document.getElementById('<%=ddlSignNoOfRejectedDeliveries.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtNoOfRejectedDeliveries1.ClientID %>').value = "";
            document.getElementById('<%=txtNoOfRejectedDeliveries2.ClientID %>').value = "";

            document.getElementById('<%=ddlSignQuality.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtQuality1.ClientID %>').value = "";
            document.getElementById('<%=txtQuality2.ClientID %>').value = "";

            document.getElementById('<%=ddlSignDelivery.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtDelivery1.ClientID %>').value = "";
            document.getElementById('<%=txtDelivery2.ClientID %>').value = "";

            document.getElementById('<%=ddlSignNoOfDelays.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtNoOfDelays1.ClientID %>').value = "";
            document.getElementById('<%=txtNoOfDelays2.ClientID %>').value = "";

            document.getElementById('<%=ddlSignCommulative.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtCommulative1.ClientID %>').value = "";
            document.getElementById('<%=txtCommulative2.ClientID %>').value = "";

            return false;
        }

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Vendor Rating Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Select/Unselect Dates:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 45%;">
                                <asp:CheckBox ID="chkSelectDates" runat="server"
                                    Checked="true"
                                    onchange="EnableDisableDates()" />

                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 45%;" align="right">
                                <asp:ImageButton ID="imgBtnClearAllFilters" runat="server"
                                    ImageUrl="~/Images/NEWICONS/clear1.png"
                                    Width="40px" Height="40px"
                                    ToolTip="Clear Filters"
                                    OnClientClick="return ClearAllFilters();" />
                            </td>
                        </tr>
                    </table>

                    <label>Date Type:</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="PO Date" Value="OA_DATE"></asp:ListItem>
                        <asp:ListItem Text="MRN Date" Value="CHALLAN_DT"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
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
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                    </asp:DropDownList>

                    <div class="full-width">
                        <label>Vendor Code/Name:</label>
                        <table width="100%;">
                            <tr>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtVendorCode" runat="server"
                                        CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendorName" runat="server"
                                        CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                    </div>


                    <div class="full-width">
                        <label>No. Of Counts For Delivery:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignNoOfCountsForDelivery" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfCountsForDelivery1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfCountsForDelivery2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="full-width">
                        <label>No. Of Counts For Quality:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignNoOfCountsForQuality" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfCountsForQuality1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfCountsForQuality2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div class="full-width">
                        <label>No. Of Rejected Deliveries:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignNoOfRejectedDeliveries" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfRejectedDeliveries1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfRejectedDeliveries2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="full-width">
                        <label>Quality:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignQuality" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuality1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuality2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="full-width">
                        <label>Delivery:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignDelivery" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDelivery1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDelivery2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div class="full-width">
                        <label>No. Of Delays:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignNoOfDelays" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfDelays1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNoOfDelays2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div class="full-width">
                        <label>Commulative:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlSignCommulative" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text="Greater Or Equal" Value=">="></asp:ListItem>
                                        <asp:ListItem Text="Greater" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="Less Or Equal" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="Less" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="Equal" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommulative1" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommulative2" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export"
                    OnClick="btnExport_Click" />

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
                ID="gvVendorRating" runat="server"
                AutoGenerateColumns="false" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvVendorRating_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:TemplateField HeaderText="Zoom"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton ID="btnViewSubitemDetail" Height="30px"
                                Width="30px"
                                CommandArgument="VIEW_PRODUCT_LIST"
                                runat="server"
                                ImageUrl="~/Images/viewdetails.png"
                                ToolTip="View Details" />

                            <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>' Visible="false" />
                            <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("Vendor_Code") %>' Visible="false" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sl No."
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSlNo" runat="server"
                                Text='<%# Eval("Sl_No") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                    <asp:BoundField DataField="Vendor_Code" HeaderText="Vendor Code" />
                    <asp:BoundField DataField="Vendor_Name" HeaderText="Vendor Name" />

                    <%-- <asp:TemplateField HeaderText="No. of Counts"
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <itemtemplate>
                                            <asp:Label ID="lblNoOfCounts" runat="server"
                                                Text='<%# Eval("No_Of_Counts") %>' />
                                        </itemtemplate>
                                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="No. of Counts For Delivery"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNoOfCountsForDelivery" runat="server"
                                Text='<%# Eval("No_Of_Counts_For_Delivery") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No. of Counts For Quality"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNoOfCountsForQuality" runat="server"
                                Text='<%# Eval("No_Of_Counts_For_Quality") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No. of Rejected Deliveries"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNoOfRejectedDeliveries" runat="server"
                                Text='<%# Eval("No_Of_Rejected_Deliveries") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quality"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblQuality" runat="server"
                                Text='<%# Eval("Quality") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delivery"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDelivery" runat="server"
                                Text='<%# Eval("Delivery") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No. Of Delays"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNoOfDelays" runat="server"
                                Text='<%# Eval("No_Of_Delays") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Commulative"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCommulative" runat="server"
                                Text='<%# Eval("Commulative") %>' />
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


    <%-- SHOW DETAIL START--%>
    <asp:Button ID="btnShowMRNDetailFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeMRNDetail" runat="server" TargetControlID="btnShowMRNDetailFile" BehaviorID="mpeMRNDetailBID"
        PopupControlID="pnlViewMRNDetailPopup" CancelControlID="imgBtnCancelMRNDetailFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewMRNDetailPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelMRNDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblMRNDetailRecors" runat="server" Text="MRN Detailed Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>MRN Start Date:</label>
                        <asp:TextBox ID="txtMRNStartDateToS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>MRN End Date:</label>
                        <asp:TextBox ID="txtMRNEndDateToS" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnExportMrnDetail" CssClass="button" Width="100%" runat="server"
                        Text="Export"
                        OnClick="btnExportMrnDetail_Click" />

                </div>
                <div class="full-width button-group">

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnitToS" runat="server" CssClass="form-control" Enabled="false" />

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCodeToS" runat="server" CssClass="form-control" Enabled="false" />


                    <label>Mrn Counts:</label>
                    <asp:TextBox ID="txtMrnCountsToS" runat="server" CssClass="form-control" Enabled="false" />

                </div>
            </div>

            <div class="popup-grid-container">

                <asp:GridView 
                    CssClass="popup-grid"
                    ID="gvMRNDetailReport" runat="server" AutoGenerateColumns="true" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="20" Width="100%" HorizontalAlign="Center"
                    AllowPaging="false" OnRowDataBound="gvMRNDetailReport_RowDataBound">
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
    <%-- SHOW SUBITEM DETAIL END--%>
</asp:Content>
