<%@ Page Title="CIPLTMS- Assign Items To Machines" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="MSScheduledReport.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_MSScheduledReport" %>

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



    <script type="text/javascript">

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

    <script type="text/javascript" language="javascript">

        function ValidateAllSearch() {

            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }
    </script>



    <script type="text/javascript">

        function EnableEndDateSearch() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').disabled = false;
            document.getElementById('<%=imgbtnStartDateSearch.ClientID %>').disabled = false;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = false;
            document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = false;

            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            document.getElementById('<%=chkSelectDates.ClientID %>').checked = false;


            var sign = document.getElementById('<%=ddlSignMainSearch.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 0) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = false;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = false;

                if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else if (signText == ">" || signText == ">=" || signindex == 1 || signindex == 2) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }
            else if (signText == "<" || signText == "<=" || signindex == 3 || signindex == 4) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnStartDateSearch.ClientID %>').disabled = true;
            }
            else if (signText == "=" || signindex == 5) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }
            <%--else {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }--%>
        }
    </script>

    <script type="text/Javascript">
        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectDates.ClientID %>').checked;
            var datetype = document.getElementById('<%=ddlSignMainSearch.ClientID %>').selectedIndex;
            if (chk == true) {
                if (datetype == 0) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
        }

    </script>



    <script type="text/javascript">

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

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


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Scheduled Machine- Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width">

                        <label>Date Filter:</label>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlOnWhichDateMainSearch" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Scheduled Date" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSignMainSearch" runat="server"
                                        CssClass="form-control"
                                        onchange="EnableEndDateSearch()">
                                        <asp:ListItem Text="BETWEEN" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </div>

                    <label>Start Date:</label>
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

                    <label>End Date:</label>
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
                            <td style="width: 10%;">
                                <asp:CheckBox ID="chkSelectDates" runat="server" onchange="EnableDisableDates()" />
                            </td>
                        </tr>
                    </table>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />

                    <label>JOB Number:</label>
                    <asp:TextBox ID="txtJOBNo" runat="server"
                        CssClass="form-control" />

                    <label>Tag Number:</label>
                    <asp:TextBox ID="txtTagNo" runat="server"
                        CssClass="form-control" />

                    <label>Item name in LOT:</label>
                    <asp:TextBox ID="txtItemNameInLOT" runat="server"
                        CssClass="form-control" />

                    <label>Allocated Code:</label>
                    <asp:TextBox ID="txtAllocatedCode" runat="server"
                        CssClass="form-control" />

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control" />

                    <label>LOT Number:</label>
                    <asp:TextBox ID="txtLOTNo" runat="server"
                        CssClass="form-control" />

                    <label>Drawing Number:</label>
                    <asp:TextBox ID="txtDrawingNo" runat="server"
                        CssClass="form-control" />

                    <label>Equipment:</label>
                    <asp:TextBox ID="txtEquipment" runat="server"
                        CssClass="form-control" />


                    <label>Machines</label>
                    <asp:DropDownList ID="ddlMachines" runat="server"
                        CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" runat="server" Width="100%" Text="Search" CssClass="button"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" runat="server" Width="100%" Text="Export" CssClass="button"
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
                ID="gvSubitemsList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Widt h="100%"
                HorizontalAlign="Center">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <%--OnRowDataBound="gvSubitemsList_RowDataBound"--%>
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" CssClass="textboxcenter"
                                Text='<%# Eval("SR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Machine Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMachineName" Width="250px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("MACHINE_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:TextBox ID="txtStatus" Width="150px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("STATUS_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUnit" Width="70px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("UNIT_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtJOBNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("JOB_NO") %>' ToolTip='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LOT No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLOTNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("TF_NO") %>' ToolTip='<%# Eval("TF_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Equipment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEquipment" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("EQUIPMENT") %>' ToolTip='<%# Eval("EQUIPMENT") %>' />
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

                    <asp:TemplateField HeaderText="Drawing No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingNo" Width="200px" TextMode="MultiLine" runat="server" Enabled="false"
                                CssClass="textboxleftyellow"
                                Text='<%# Eval("DRAWING_NO") %>'
                                ToolTip='<%# Eval("DRAWING_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Item Given In LOT">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemGivenInLOT" Width="250px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                TextMode="MultiLine"
                                Columns="30"
                                Text='<%# Eval("ITEM_NAME_GIVEN_IN_LOT") %>'
                                ToolTip='<%# Eval("ITEM_NAME_GIVEN_IN_LOT") %>' />
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

                    <asp:TemplateField HeaderText="Activity Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtActivityName" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ACTIVITY_NAME") %>'
                                ToolTip='<%# Eval("ACTIVITY_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Worker Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtWorkerName" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("WORKER_NAME") %>'
                                ToolTip='<%# Eval("WORKER_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Allotment Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAllotmentDate" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ALLOTMENT_DATE") %>'
                                ToolTip='<%# Eval("ALLOTMENT_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Expected Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtExpectedDate" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("EXPECTED_DATE") %>'
                                ToolTip='<%# Eval("EXPECTED_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Scheduled Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtScheduledDate" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("SCHEDULED_DATE") %>'
                                ToolTip='<%# Eval("SCHEDULED_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Scheduled From">
                        <ItemTemplate>
                            <asp:TextBox ID="txtScheduledFrom" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("SCHEDULED_FROM") %>'
                                ToolTip='<%# Eval("SCHEDULED_FROM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Scheduled To">
                        <ItemTemplate>
                            <asp:TextBox ID="txtScheduledTo" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("SCHEDULED_TO") %>'
                                ToolTip='<%# Eval("SCHEDULED_TO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actual Date of Completion">
                        <ItemTemplate>
                            <asp:TextBox ID="txtActualDateofCompletion" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ACTUAL_DATE_OF_COMPLETION") %>'
                                ToolTip='<%# Eval("ACTUAL_DATE_OF_COMPLETION") %>' />
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

