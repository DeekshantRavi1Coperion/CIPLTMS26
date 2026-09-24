<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddUpdateWorkerTimesheetNewNine.aspx.cs"
    Inherits="TIMESHEET_WORKER_AddUpdateWorkerTimesheetNewNine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            width: 100%;
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
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
            document.getElementById('<%=txtPrevDate.ClientID %>').value = document.getElementById('<%=hdPrevDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdCurrentDate.ClientID %>').value.split("-");
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
                alert("Entry Date must be lesser than or equal to current date!");
                return false;
            }
        }

        function clientChangedPrevDate(sender, args) {
            document.getElementById('<%=hdPrevDate.ClientID %>').value = document.getElementById('<%=txtPrevDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdPrevDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdDate.ClientID %>').value.split("-");
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

            var copyFrom = document.getElementById('<%=ddlCopyFromDay.ClientID %>').selectedIndex;

            if (copyFrom == '2') {
                if (endFormatedDate < formatedDate) {
                    alert("Prev Date must be lesser than or equal to entry date!");
                    return false;
                }
            }



        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdCurrentDate.ClientID %>').value.split("-");
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
                alert("Entry Date must be lesser than or equal to current date!");
                return false;
            }
        }

        function ValidateDateRangePrevDate() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdPrevDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdDate.ClientID %>').value.split("-");
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

            var copyFrom = document.getElementById('<%=ddlCopyFromDay.ClientID %>').selectedIndex;

            if (copyFrom == '2') {
                if (endFormatedDate < formatedDate) {
                    alert("Prev Date must be lesser than or equal to entry date!");
                    return false;
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {

            if (ValidateDateRange()) { return false; }

            var copyFrom = document.getElementById('<%=ddlCopyFromDay.ClientID %>').selectedIndex;
            if (copyFrom == '2') {
                if (ValidateDateRangePrevDate()) { return false; }
            }


            return true;
        }
    </script>

    <script type="text/Javascript"> 

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            if (ValidateUnit()) { return false; }
            if (ValidateDateRange()) { return false; }

            var copyFrom = document.getElementById('<%=ddlCopyFromDay.ClientID %>').selectedIndex;

            if (copyFrom == '2') {
                if (ValidateDateRangePrevDate()) { return false; }
            }


            return true;
        }
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function checkDec(el) {

            var ex = /^[0-9]+\:?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);

            }
            else {

            }
        }
    </script>

    <script type="text/Javascript">
        function checkDec1(el) {
            var totalMinuts = '0';
            var row = el.parentNode.parentNode;
            var lblTotalMinuts = row.cells[8].innerText;
            var lblLastTotalAdjustedHours = row.cells[9].innerText;

            var workingHours = row.cells[10].getElementsByTagName("input")[0].value;
            if (String(workingHours) != '') {
                var a = workingHours.split(':');
                var hours = a[0];
                var minuts = a[1];

                if (parseInt(hours) > 0) {
                    hours = hours;
                }
                else {
                    hours = 0;
                }


                if (parseInt(minuts) > 0 && parseInt(minuts) < 60) {
                    minuts = minuts;
                }
                else if (parseInt(minuts) > 59) {
                    row.cells[10].getElementsByTagName("input")[0].value = hours + ':00';
                    minuts = 0;
                }
                else {
                    minuts = 0;
                }

                var totalMinuts = Number((hours * 60) + Number(minuts));
                if (parseFloat(totalMinuts) > 0) {
                    row.cells[9].getElementsByTagName("input")[0].value = Number(row.cells[9].getElementsByTagName("input")[0].value) + Number(totalMinuts);
                }
                else {
                    row.cells[9].getElementsByTagName("input")[0].value = '';
                }
            }
            else {
                row.cells[9].getElementsByTagName("input")[0].value = '';
            }
        }
    </script>

    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
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


        function EnableDays() {

            var copyFrom = document.getElementById('<%=ddlCopyFromDay.ClientID %>').selectedIndex;

            if (copyFrom == '1') {
                document.getElementById('<%=txtPrevDate.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnPrevDate.ClientID %>').disabled = true;
            }
            else if (copyFrom == '2') {
                document.getElementById('<%=txtPrevDate.ClientID %>').disabled = false;
                document.getElementById('<%=imgbtnPrevDate.ClientID %>').disabled = false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Add Worker Timesheet</legend>

            <div class="form-grid form-grid-2">

                <label>Unit</label>
                <asp:DropDownList ID="ddlUnit" runat="server"
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>

                <label>JOB No.</label>
                <asp:DropDownList ID="ddlJOBNo" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Entry Date</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdDate" runat="server" />
                            <asp:HiddenField ID="hdCurrentDate" runat="server" />
                            <asp:CalendarExtender ID="calendarDate" PopupButtonID="imgbtnDate" runat="server"
                                TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                            </asp:CalendarExtender>
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgbtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Start Date Calendar" />
                        </td>
                    </tr>
                </table>

                <label>Exclude Missed Punch</label>
                <asp:CheckBox ID="chkExcludeMissedPunch" runat="server" />

                <label>Copy From</label>
                <div class="full-width button-group">
                    <asp:DropDownList runat="server" ID="ddlCopyFromDay"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlCopyFromDay_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Not Applicable" Value="0" />
                        <asp:ListItem Text="Previous Day" Value="1" />
                        <asp:ListItem Text="Specific Day" Value="2" />
                    </asp:DropDownList>

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPrevDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"
                                    Visible="false"></asp:TextBox>
                                <asp:HiddenField ID="hdPrevDate" runat="server" />
                                <asp:HiddenField ID="hdCurrentPrevDate" runat="server" />
                                <asp:CalendarExtender ID="calendarPrevDate" PopupButtonID="imgbtnPrevDate" runat="server"
                                    TargetControlID="txtPrevDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedPrevDate">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnPrevDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Previous Date Calendar" Visible="false" Height="25PX" Width="25PX" />

                                <asp:ImageButton ID="imgbtnRefreshDate" runat="server" ImageUrl="~/Images/LOT/undo2.png"
                                    ToolTip="Refresh Previous Date" Visible="false" Height="25PX" Width="25PX"
                                    OnClick="imgbtnRefreshDate_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <label>Project Supervisor</label>
                <asp:DropDownList ID="ddlProjectSupervisor" runat="server"
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddlProjectSupervisor_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>

                <label>Company</label>
                <asp:DropDownList ID="ddlCompany" runat="server"
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>

                <label>Project Worker</label>
                <asp:DropDownList ID="ddlProjectWorker" runat="server" W
                    CssClass="form-control">
                </asp:DropDownList>

            </div>
        </fieldset>
        <div class="full-width button-group">
            <asp:Button
                ID="btnAddNewRow"
                CssClass="button"
                Width="50%"
                runat="server"
                Text="Add New"
                OnClick="btnAddNewRow_Click"
                OnClientClick="return ValidateAll();" />

            <asp:Button
                ID="btnSave"
                CssClass="button"
                Width="50%"
                runat="server"
                Text="Save"
                OnClick="btnSave_Click"
                OnClientClick="return ValidateAllNew();" />
        </div>
        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
            </asp:Panel>
        </div>

        <fieldset class="filter-card">
            <asp:HiddenField ID="hdMinuts" runat="server" />
            <asp:HiddenField ID="hdTotalMinuts" runat="server" />

            <legend>
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

            <div class="employee-grid-container">
                <asp:GridView 
                    CssClass="employee-grid"
                    ID="gvWorkerTimesheetList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvWorkerTimesheetList_RowDataBound" OnRowCommand="gvWorkerTimesheetList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPunchMissed" runat="server" Visible="false" Text='<%# Eval("PUNCH_MISSED") %>' />
                                <asp:Label ID="lblMainRecordFlag" runat="server" Visible="false" Text='<%# Eval("MAIN_RECORD_FLAG") %>' />
                                <asp:Label ID="lblPrevExisted" runat="server" Visible="false" Text='<%# Eval("IS_PREV_EXISTED") %>' />
                                <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Code">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Order Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlOrderType" Width="150px" Height="26px" runat="server"
                                            OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="0" />
                                            <asp:ListItem Text="LOT" Value="1" />
                                            <asp:ListItem Text="Stock" Value="2" />
                                            <asp:ListItem Text="Warranty" Value="3" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="JOB No">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlJOBNo" Width="150px" Height="26px" runat="server"
                                    OnSelectedIndexChanged="ddlJOBNo_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSubitems" Width="250px" Height="26px" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JOB No" Visible="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txtJOBNo" Width="150px" runat="server" CssClass="textboxleftgreen" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item Name" Visible="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemName" Width="250px" runat="server" TextMode="MultiLine" Rows="2" Columns="30" CssClass="textboxleftgreen" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Activity Matrix">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActivityMatrix" Width="250px" runat="server" TextMode="MultiLine" Rows="2" Columns="30" CssClass="textboxleftgreen" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                        <asp:TemplateField HeaderText="Entry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("ENTRY_DATE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Time">
                            <ItemTemplate>
                                <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("IN_TIME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Out Time">
                            <ItemTemplate>
                                <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUT_TIME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Working Hours">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkingHours" runat="server" Text='<%# Eval("WORKING_HOURS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OT Hours">
                            <ItemTemplate>
                                <asp:Label ID="lblOTHours" runat="server" Width="100%" Text='<%# Eval("OT_HOURS") %>' CssClass="textboxright" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Saved Timesheet Hours">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSavedTimesheetHours" Width="100%" runat="server" Text='<%# Eval("SAVED_TIMESHEET_HOURS") %>' Enabled="false" CssClass="textboxright" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Balance Timesheet Hours">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBalTimesheetHours" Width="100%" runat="server" Text='<%# Eval("BAL_TIMESHEET_HRS") %>' Enabled="false" CssClass="textboxright" />
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Adjustment Hours">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlHours" runat="server" Width="96%" OnSelectedIndexChanged="ddlHours_SelectedIndexChanged"
                                    AutoPostBack="true">
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
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                    <asp:ListItem Text="13" Value="13" />
                                    <asp:ListItem Text="14" Value="14" />
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="16" Value="16" />
                                    <asp:ListItem Text="17" Value="17" />
                                    <asp:ListItem Text="18" Value="18" />
                                    <asp:ListItem Text="19" Value="19" />
                                    <asp:ListItem Text="20" Value="20" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adjustment Minuts">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMins" runat="server" Width="96%" OnSelectedIndexChanged="ddlMins_SelectedIndexChanged"
                                    BackColor="Transparent" AutoPostBack="true">
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
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                    <asp:ListItem Text="13" Value="13" />
                                    <asp:ListItem Text="14" Value="14" />
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="16" Value="16" />
                                    <asp:ListItem Text="17" Value="17" />
                                    <asp:ListItem Text="18" Value="18" />
                                    <asp:ListItem Text="19" Value="19" />
                                    <asp:ListItem Text="20" Value="20" />
                                    <asp:ListItem Text="21" Value="21" />
                                    <asp:ListItem Text="22" Value="22" />
                                    <asp:ListItem Text="23" Value="23" />
                                    <asp:ListItem Text="24" Value="24" />
                                    <asp:ListItem Text="25" Value="25" />
                                    <asp:ListItem Text="26" Value="26" />
                                    <asp:ListItem Text="27" Value="27" />
                                    <asp:ListItem Text="28" Value="28" />
                                    <asp:ListItem Text="29" Value="29" />
                                    <asp:ListItem Text="30" Value="30" />
                                    <asp:ListItem Text="31" Value="31" />
                                    <asp:ListItem Text="32" Value="32" />
                                    <asp:ListItem Text="33" Value="33" />
                                    <asp:ListItem Text="34" Value="34" />
                                    <asp:ListItem Text="35" Value="35" />
                                    <asp:ListItem Text="36" Value="36" />
                                    <asp:ListItem Text="37" Value="37" />
                                    <asp:ListItem Text="38" Value="38" />
                                    <asp:ListItem Text="39" Value="39" />
                                    <asp:ListItem Text="40" Value="40" />
                                    <asp:ListItem Text="41" Value="41" />
                                    <asp:ListItem Text="42" Value="42" />
                                    <asp:ListItem Text="43" Value="43" />
                                    <asp:ListItem Text="44" Value="44" />
                                    <asp:ListItem Text="45" Value="45" />
                                    <asp:ListItem Text="46" Value="46" />
                                    <asp:ListItem Text="47" Value="47" />
                                    <asp:ListItem Text="48" Value="48" />
                                    <asp:ListItem Text="49" Value="49" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="51" Value="51" />
                                    <asp:ListItem Text="52" Value="52" />
                                    <asp:ListItem Text="53" Value="53" />
                                    <asp:ListItem Text="54" Value="54" />
                                    <asp:ListItem Text="55" Value="55" />
                                    <asp:ListItem Text="56" Value="56" />
                                    <asp:ListItem Text="57" Value="57" />
                                    <asp:ListItem Text="58" Value="58" />
                                    <asp:ListItem Text="59" Value="59" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adjusted Hours">
                            <ItemTemplate>
                                <asp:Label ID="lblWorkingMinuts" runat="server" Text='<%# Eval("WORKING_MINUTS") %>'
                                    Visible="false" />
                                <asp:Label ID="lblAdjustedWorkingHours" runat="server" Text='<%# Eval("ADJUSTED_WORKING_HOURS") %>'
                                    Visible="false" />
                                <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIAL_NO") %>' Visible="false" />
                                <asp:TextBox ID="txtAdjustedWorkingHours" Width="100%" runat="server" Text='<%# Eval("ADJUSTED_WORKING_HOURS") %>'
                                    Enabled="false" CssClass="textboxright" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance Hours">
                            <ItemTemplate>
                                <asp:Label ID="lblBalanceWorkingHours" runat="server" Text='<%# Eval("BAL_WRK_HRS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" Width="250px" runat="server" TextMode="MultiLine" Rows="2"
                                    Columns="50" Text='<%# Eval("REMARKS") %>' CssClass="textboxleftgreen" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ADD" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnAddNewRecord" ImageUrl="~/Images/Icons/ADD05.png" ToolTip="Add New Record"
                                    runat="server" CommandArgument="ADD" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REMOVE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnRemoveRecord" ImageUrl="~/Images/Icons/REMOVE02.png" ToolTip="Remove New Record"
                                    runat="server" CommandArgument="REMOVE" Visible="false" />
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

                <input type="hidden" id="div_position" name="div_position" />
            </div>
        </fieldset>




    </div>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
