<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="UnpostedPOListSix.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_UnpostedPOListSix" Title="Unposted PO List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
        .aligntxtcenter
        {
            text-align: center;
        }
        .aligntxtleft
        {
            text-align: left;
        }
        .aligntxtright
        {
            text-align: right;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
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


        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
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

        function ValidateJOBNo() {
            var jobNo = document.getElementById('<%=ddlJOBNo.ClientID %>').selectedIndex;
            if (jobNo == '' && jobNo == '0') {

                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            if (ValidateJOBNo()) {
                return false;
            }
            return true;
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

    <script type="text/Javascript">

        function checkDec1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;

            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {


                    var lblBudgetedAmount = row.cells[6].innerText;
                    var lblPOAmount = row.cells[7].innerText;
                    var txtPendingAmount = row.cells[8].getElementsByTagName("input")[0].value;
                    var txtSavingAmount = row.cells[9].getElementsByTagName("input")[0].value;

                    var BudgetedAmount = 0;
                    var POAmount = 0;
                    var PendingAmount = 0;

                    if (lblBudgetedAmount != '' && parseFloat(lblBudgetedAmount) > '0') {
                        BudgetedAmount = parseFloat(lblBudgetedAmount);
                    }
                    else {
                        BudgetedAmount = 0;
                    }

                    if (lblPOAmount != '' && parseFloat(lblPOAmount) > '0') {
                        POAmount = parseFloat(lblPOAmount);
                    }
                    else {
                        POAmount = 0;
                    }

                    if (txtPendingAmount != '' && parseFloat(txtPendingAmount) > '0') {
                        PendingAmount = parseFloat(txtPendingAmount);
                    }
                    else {
                        PendingAmount = 0;
                    }

                    if (PendingAmount < (BudgetedAmount - POAmount)) {
                        row.cells[9].getElementsByTagName("input")[0].value = parseFloat(BudgetedAmount - (POAmount + PendingAmount));
                    }
                    else {
                        row.cells[8].getElementsByTagName("input")[0].value = parseFloat(BudgetedAmount - POAmount);
                        row.cells[9].getElementsByTagName("input")[0].value = '0';
                    }




                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblBudgetedAmount = row.cells[6].innerText;
                var lblPOAmount = row.cells[7].innerText;
                var txtPendingAmount = row.cells[8].getElementsByTagName("input")[0].value;
                var txtSavingAmount = row.cells[9].getElementsByTagName("input")[0].value;

                var BudgetedAmount = 0;
                var POAmount = 0;
                var PendingAmount = 0;

                if (lblBudgetedAmount != '' && parseFloat(lblBudgetedAmount) > '0') {
                    BudgetedAmount = parseFloat(lblBudgetedAmount);
                }
                else {
                    BudgetedAmount = 0;
                }

                if (lblPOAmount != '' && parseFloat(lblPOAmount) > '0') {
                    POAmount = parseFloat(lblPOAmount);
                }
                else {
                    POAmount = 0;
                }

                if (txtPendingAmount != '' && parseFloat(txtPendingAmount) > '0') {
                    PendingAmount = parseFloat(txtPendingAmount);
                }
                else {
                    PendingAmount = 0;
                }

                if (PendingAmount < (BudgetedAmount - POAmount)) {
                    row.cells[9].getElementsByTagName("input")[0].value = parseFloat(BudgetedAmount - (POAmount + PendingAmount));
                }
                else {
                    row.cells[8].getElementsByTagName("input")[0].value = parseFloat(BudgetedAmount - POAmount);
                    row.cells[9].getElementsByTagName("input")[0].value = '0';
                }



            }
        }           
    </script>

    <script type="text/javascript">
        window.onload = function() {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function() {
                div_position.value = div.scrollTop;
            };
        };
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRangeNew(el) {
            var row = el.parentNode.parentNode;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");

            var dateItems = row.cells[11].getElementsByTagName("input")[0].value.split("-");
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

            var endDateItems = row.cells[12].getElementsByTagName("input")[0].value.split("-");

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



            var monthdiff = (endDateItems[endYearIndex] - dateItems[yearIndex]) * 12 + (endMonth - month);

            if (endFormatedDate < formatedDate) {
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[12].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                row.cells[12].getElementsByTagName("input")[0].value = "";
                row.cells[13].getElementsByTagName("input")[0].value = "0";
                alert("Invalid Date Range");
                return true;
            }
            else {
                row.cells[11].getElementsByTagName("input")[0].style.borderColor = "";
                row.cells[12].getElementsByTagName("input")[0].style.borderColor = "";
            }




            if (monthdiff >= 0) {
                row.cells[13].getElementsByTagName("input")[0].value = monthdiff;
                row.cells[13].getElementsByTagName("input")[0].style.borderColor = "";
            }
            else {
                row.cells[13].getElementsByTagName("input")[0].value = monthdiff;
                row.cells[13].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                return true;
            }
        }
    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 90%">
                    <legend style="text-align: center;">Unposted PO List </legend>
                    <table width="90%">
                        <tr>
                            <td>
                                Start Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                End Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Company:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Vendor:
                            </td>
                            <td>
                                <asp:TextBox ID="txtVendorName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                JOB No.:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJOBNo" runat="server" Width="200px" Height="25px" onblur="return ValidateJOBNo();"
                                    OnSelectedIndexChanged="ddlJOBNo_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Pivot Group:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPivotGroup" runat="server" Width="250px" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Posting Month:
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
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
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
            <div align="center">
                <fieldset style="width: 95%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                    <div id="dvScroll" style='overflow: scroll; width: 100%; height: 400px; border: 1px solid lightgray;'>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:GridView ID="gvUnpostedPOList" runat="server" CellPadding="4" ForeColor="#333333"
                                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvUnpostedPOList_RowDataBound"
                                    OnRowCommand="gvUnpostedPOList_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="JOB_No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                                <asp:Label ID="lblPODate" runat="server" Visible="false" Text='<%# Eval("PO_DATE") %>' />
                                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                                <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                                <asp:Label ID="lblPostedJOBNO" runat="server" Visible="false" Text='<%# Eval("POSTED_JOB_NO") %>' />
                                                <asp:Label ID="lblDeltaPOAmount" runat="server" Visible="false" Text='<%# Eval("DELTA_PO_AMOUNT") %>' />
                                                <asp:Label ID="lblDeltaSavingAmount" runat="server" Visible="false" Text='<%# Eval("DELTA_SAVING_AMOUNT") %>' />
                                                <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JOB_NO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Vendor_Code" DataField="VENDOR_CODE" />
                                        <asp:BoundField HeaderText="Vendor_Name" DataField="VENDOR_NAME" />
                                        <asp:BoundField HeaderText="PO_No" DataField="PO_NO" />
                                        <asp:BoundField HeaderText="PO_Date" DataField="PO_DATE" />
                                        <asp:TemplateField HeaderText="Pivot_Group">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlPivotGroup" runat="server" Width="100%" OnSelectedIndexChanged="ddlPivotGroup_SelectedIndexChanged"
                                                    AutoPostBack="true" Enabled="false">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted_Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBudgetedAmount" runat="server" Text='<%# Eval("BUDGETED_AMOUNT") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO_Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOAmount" runat="server" Text='<%# Eval("PO_AMOUNT") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO_Budgeted_Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPOBudgetedAmount" runat="server" Width="100%" CssClass="aligntxtright"
                                                    Text='<%# Eval("PO_BUDGETED_AMOUNT") %>' onKeyUp="checkDec1(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Saving_Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSavingAmount" runat="server" Width="100%" CssClass="aligntxtright"
                                                    Text='<%# Eval("SAVING_AMOUNT") %>' onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last_Delivery_Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLastDeliveryDate" runat="server" Width="80%" onkeyDown="javascript:preventInput(event);"
                                                    Text='<%# Eval("LAST_DELIVERY_DATE") %>' onchange="ValidateDateRangeNew(this)"></asp:TextBox>
                                                <asp:CalendarExtender ID="calendarLastDeliveryDate" PopupButtonID="imgbtnLastDeliveryDate"
                                                    runat="server" TargetControlID="txtLastDeliveryDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                                <asp:ImageButton ID="imgbtnLastDeliveryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Last Delivery Date Calendar" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expected_Delivery_Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" Width="80%" onkeyDown="javascript:preventInput(event);"
                                                    onchange="ValidateDateRangeNew(this)" Text='<%# Eval("EXPECTED_DELIVERY_DATE") %>'></asp:TextBox>
                                                <asp:CalendarExtender ID="calendarExpectedDeliveryDate" PopupButtonID="imgbtnExpectedDeliveryDate"
                                                    runat="server" TargetControlID="txtExpectedDeliveryDate" Format="dd-MMM-yyyy">
                                                </asp:CalendarExtender>
                                                <asp:ImageButton ID="imgbtnExpectedDeliveryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Expected Delivery Date Calendar" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months_To_Deliver">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNoOfMonthsToDeliver" runat="server" Width="100%" onkeyup="checkDec(this);"
                                                    CssClass="aligntxtcenter" onkeyDown="javascript:preventInput(event);" Text='<%# Eval("NO_OF_MONTHS_TO_DELIVER") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Months_Based_Upon">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNoOfMonthsBasedUpon" runat="server" Width="100%" onkeyup="checkDec(this);"
                                                    CssClass="aligntxtcenter" Text='<%# Eval("NO_OF_MONTHS_BASED_UPON") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment_Term">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaymentTerm" runat="server" Width="100%" Text='<%# Eval("PAYMENT_TERM") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is_Order_Subject_To">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlIsOrderSubjectTo" runat="server" Width="100%">
                                                    <asp:ListItem Text="SELECT" Value='0' />
                                                    <asp:ListItem Text="YES" Value='YES' />
                                                    <asp:ListItem Text="NO" Value='NO' />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is_Billable">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlIsBillable" runat="server" Width="100%">
                                                    <asp:ListItem Text="SELECT" Value='0' />
                                                    <asp:ListItem Text="YES" Value='YES' />
                                                    <asp:ListItem Text="NO" Value='NO' />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="150PX" Text='<%# Eval("REMARKS") %>'
                                                    Rows="1" Columns="5" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UDF1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUDF1" runat="server" Width="150PX" Text='<%# Eval("UDF1") %>'
                                                    Rows="1" Columns="5" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UDF2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUDF2" runat="server" Width="150PX" Text='<%# Eval("UDF2") %>'
                                                    Rows="1" Columns="15" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Location" DataField="LOCATION" />
                                        <asp:TemplateField HeaderText="Posting_Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPostingMonth" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Post">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPost" CssClass="button" Width="100%" Height="30px" runat="server"
                                                    Text="Post" CommandArgument="POST" Visible="true" />
                                                <asp:Label ID="lblPOAmountNew" runat="server" Text='<%# Eval("POSTED_PO_AMOUNT") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="PO_Amount_New">
                                            <ItemTemplate>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <input type="hidden" id="div_position" name="div_position" />
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
