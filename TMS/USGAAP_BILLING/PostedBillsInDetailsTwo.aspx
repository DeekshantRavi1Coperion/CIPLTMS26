<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PostedBillsInDetailsTwo.aspx.cs"
    Inherits="USGAAP_BILLING_PostedBillsInDetailsTwo" Title="USGAAP Billing - Posted List In Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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

        function ValidatePostingCurrency() {
            var PostingCurrency = document.getElementById('<%=ddlPostingCurrency.ClientID %>').selectedIndex;
            if (PostingCurrency == '' || PostingCurrency == '0') {
                document.getElementById('<%=ddlPostingCurrency.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPostingCurrency.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateUnpostedValue() {
            if (parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value) < 0) {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                alert("Unposted value must be greater than or equal to zero...!!");
                return true;
            }
            else {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEndMarket() {
            var EndMarket = document.getElementById('<%=ddlEndMarket.ClientID %>').selectedIndex;
            if (EndMarket == '' || EndMarket == '0') {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCountry() {
            var Country = document.getElementById('<%=ddlCountry.ClientID %>').selectedIndex;
            if (Country == '' || Country == '0') {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateType() {
            var Type = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Type == '' || Type == '0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevenueType() {
            var RevenueType = document.getElementById('<%=ddlRevenueType.ClientID %>').selectedIndex;
            if (RevenueType == '' || RevenueType == '0') {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateInvoiceAmtAndUnpostedAmt() {

            var IsNewInserted = document.getElementById('<%=hdIsNewInserted.ClientID %>').value;
            var InvoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            var PostedValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;

            var UnpostedValue = document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            var UnpostingValue = document.getElementById('<%=hdUnpostingValue.ClientID %>').value;
            var ActualUnpostedValue = document.getElementById('<%=hdActualUnpostedValue.ClientID %>').value;

            var pendingAmount = document.getElementById('<%=hdPendingAmount.ClientID %>').value;
            var actualPostedValue = document.getElementById('<%=hdPostedValue.ClientID %>').value;


            if (InvoiceAmount == '' || InvoiceAmount == '-') {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "";

                if (PostedValue == '' || PostedValue == '-') {
                    document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "";

                    if (parseFloat(InvoiceAmount) < parseFloat(PostedValue)) {
                        alert("Invoice amount must be greater then or equal to posted value...!");

                        document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                        document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "";
                        document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "";

                        if ((parseFloat(UnpostedValue) - parseFloat(UnpostingValue)) > parseFloat(ActualUnpostedValue)) {
                            alert("Unposted value can be greater than or equal to: " + (parseFloat(UnpostedValue) - parseFloat(ActualUnpostedValue)) + " only..!");

                            if (parseInt(IsNewInserted) > 0) {
                                document.getElementById('<%=txtPostedValue.ClientID %>').value = parseFloat(InvoiceAmount) - parseFloat(UnpostedValue) + parseFloat(ActualUnpostedValue);
                            }
                            else {
                                document.getElementById('<%=txtPostedValue.ClientID %>').value = parseFloat(pendingAmount) - parseFloat(UnpostedValue) + parseFloat(ActualUnpostedValue);
                            }


                            document.getElementById('<%=hdUnpostingValue.ClientID %>').value = parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value) - parseFloat(ActualUnpostedValue);
                            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostingValue.ClientID %>').value;

                        }
                        return false;
                    }
                }
            }
        }


        function ValidateAllNew() {

            var check = true;

            if (ValidatePostingCurrency()) {
                return false;
            }

            if (ValidateUnpostedValue()) {
                return false;
            }

            if (ValidateEndMarket()) {
                return false;
            }

            if (ValidateCountry()) {
                return false;
            }

            if (ValidateType()) {
                return false;
            }

            if (ValidateRevenueType()) {
                return false;
            }

            if (ValidateInvoiceAmtAndUnpostedAmt()) {
                return false;
            }


            return true;
        }

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

    <script type="text/javascript" language="javascript">

        function chkUnpostedValueNew() {

            var actualInvoiceAmount = 0;
            actualInvoiceAmount = parseFloat(document.getElementById('<%=hdInvoiceAmount.ClientID %>').value);


            var actualPostedValue = 0;
            actualPostedValue = parseFloat(document.getElementById('<%=hdPostedValue.ClientID %>').value);

            var actualUnpostedValue = 0;
            actualUnpostedValue = document.getElementById('<%=hdActualUnpostedValue.ClientID %>').value;


            var revAccount = document.getElementById('<%=txtRevAccount.ClientID %>').value;

            if (parseFloat(document.getElementById('<%=txtInvoiceAmount.ClientID %>').value) < parseFloat(document.getElementById('<%=txtPostedValue.ClientID %>').value)) {

                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
            }
            else {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "";
            }

            var invoiceAmount = 0;
            invoiceAmount = parseFloat(document.getElementById('<%=txtInvoiceAmount.ClientID %>').value);

            var deltaInvoiceAmount = 0;
            deltaInvoiceAmount = parseFloat(invoiceAmount) - parseFloat(actualInvoiceAmount);

            var pendingAmount = 0;
            pendingAmount = parseFloat(document.getElementById('<%=hdPendingAmount.ClientID %>').value);
            document.getElementById('<%=hdPendingAmount.ClientID %>').value = pendingAmount;

            var postingValue = 0;
            postingValue = parseFloat(document.getElementById('<%=txtPostedValue.ClientID %>').value);

            document.getElementById('<%=hdUnpostingValue.ClientID %>').value = (parseFloat(pendingAmount) - parseFloat(postingValue)) + parseFloat(deltaInvoiceAmount);
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostingValue.ClientID %>').value;
        }

    </script>

    <script type="text/javascript">

        function inNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 45) {
                if (txt.value.indexOf('-') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (charCode == 46) {
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var GridId = "<%=gvPostedList.ClientID %>";
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
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Posted List In Detail:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

                <div class="form-grid form-grid-3">
                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
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

                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
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

                    <label>Invoice No.</label>
                    <asp:TextBox ID="txtBillNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Revenue Account</label>
                    <asp:TextBox ID="txtRevenueAccount" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />


                    &nbsp;
                    <asp:Button
                        ID="btnSearch"
                        CssClass="button"
                        Width="100%"
                        runat="server"
                        Text="Search"
                        OnClick="btnSearch_Click" />

                    &nbsp;
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
                ID="gvPostedList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" PageSize="20" HorizontalAlign="Center" OnRowDataBound="gvPostedList_RowDataBound"
                OnRowCommand="gvPostedList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblInvoiceNo" runat="server" Visible="false" Text='<%# Eval("INVOICE_NO") %>' />
                            <asp:Label ID="lblInvoiceDate" runat="server" Visible="false" Text='<%# Eval("INVOICE_DATE") %>' />
                            <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblBusinessUnit" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT") %>' />
                            <asp:Label ID="lblProductCode" runat="server" Visible="false" Text='<%# Eval("PRODUCT_CODE") %>' />
                            <asp:Label ID="lblRevenueAccount" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT") %>' />
                            <asp:Label ID="lblRevenueAccountDesc" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_DESC") %>' />
                            <asp:Label ID="lblRevenueAccountType" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_TYPE") %>' />
                            <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                            <asp:Label ID="lblProductRate" runat="server" Visible="false" Text='<%# Eval("PRODUCT_RATE") %>' />
                            <asp:Label ID="lblInvoiceAmount" runat="server" Visible="false" Text='<%# Eval("INVOICE_AMOUNT") %>' />
                            <asp:Label ID="lblPendingAmount" runat="server" Visible="false" Text='<%# Eval("PENDING_AMOUNT") %>' />
                            <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                            <asp:Label ID="lblPostedValue" runat="server" Visible="false" Text='<%# Eval("POSTED_VALUE") %>' />
                            <asp:Label ID="lblPostedDate" runat="server" Visible="false" Text='<%# Eval("POSTED_DATE") %>' />
                            <asp:Label ID="lblUnpostedValue" runat="server" Visible="false" Text='<%# Eval("UNPOSTED_VALUE") %>' />
                            <asp:Label ID="lblActualUnpostedValue" runat="server" Visible="false" Text='<%# Eval("ACTUAL_UNPOSTED_VALUE") %>' />
                            <asp:Label ID="lblEndMarket" runat="server" Visible="false" Text='<%# Eval("END_MARKET_CODE") %>' />
                            <asp:Label ID="lblGeogrophy" runat="server" Visible="false" Text='<%# Eval("COUNTRY_ISO_CODE") %>' />
                            <asp:Label ID="lblPostingMonth" runat="server" Visible="false" Text='<%# Eval("POSTING_MONTH") %>' />
                            <asp:Label ID="lblPostingYear" runat="server" Visible="false" Text='<%# Eval("POSTING_YEAR") %>' />
                            <asp:Label ID="lblPostingCurrencyID" runat="server" Visible="false" Text='<%# Eval("POSTING_CURRENCY_ID") %>' />
                            <asp:Label ID="lblPostingCurrency" runat="server" Visible="false" Text='<%# Eval("POSTING_CURRENCY") %>' />
                            <asp:Label ID="lblTypeID" runat="server" Visible="false" Text='<%# Eval("TYPE_ID") %>' />
                            <asp:Label ID="lblRevenueTypeID" runat="server" Visible="false" Text='<%# Eval("REVENUE_TYPE_ID") %>' />
                            <asp:Label ID="lblRevRecReduction" runat="server" Visible="false" Text='<%# Eval("REV_REC_REDUCTION") %>' />
                            <asp:Label ID="lblUDF2" runat="server" Visible="false" Text='<%# Eval("UDF2") %>' />
                            <asp:Label ID="lblUDF3" runat="server" Visible="false" Text='<%# Eval("UDF3") %>' />
                            <asp:Label ID="lblUDF4" runat="server" Visible="false" Text='<%# Eval("UDF4") %>' />
                            <asp:Label ID="lblUDF5" runat="server" Visible="false" Text='<%# Eval("UDF5") %>' />
                            <asp:Label ID="lblIsNewInserted" runat="server" Visible="false" Text='<%# Eval("IS_NEW_INSERTED") %>' />
                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                ToolTip="Edit Posted Bill" Width="100%" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                    <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" />
                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="INVOICE_DATE" />
                    <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT" />
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="PRODUCT_CODE" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT" HeaderText="REVENUE_ACCOUNT" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT_DESC" HeaderText="ACCOUNT_DESC" />
                    <asp:BoundField DataField="REVENUE_ACCOUNT_TYPE" HeaderText="ACCOUNT_TYPE" />
                    <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                    <asp:BoundField DataField="PRODUCT_RATE" HeaderText="PRODUCT_RATE" />
                    <asp:BoundField DataField="INVOICE_AMOUNT" HeaderText="INVOICE_AMOUNT" />
                    <asp:BoundField DataField="POSTED_VALUE" HeaderText="POSTED_VALUE" />
                    <asp:BoundField DataField="POSTED_DATE" HeaderText="POSTED_DATE" />
                    <asp:BoundField DataField="UNPOSTED_VALUE" HeaderText="UNPOSTED_VALUE" />
                    <asp:BoundField DataField="END_MARKET" HeaderText="END_MARKET" />
                    <asp:BoundField DataField="GEOGROPHY" HeaderText="GEOGROPHY" />
                    <asp:BoundField DataField="POSTING_MONTH" HeaderText="POSTING_MONTH" />
                    <asp:BoundField DataField="POSTING_YEAR" HeaderText="POSTING_YEAR" />
                    <asp:BoundField DataField="TYPE" HeaderText="TYPE" />
                    <asp:BoundField DataField="REVENUE_TYPE" HeaderText="REVENUE_TYPE" />
                    <asp:BoundField DataField="POSTING_CURRENCY" HeaderText="POSTING_CURRENCY" />
                    <asp:BoundField DataField="REV_REC_REDUCTION" HeaderText="REV_REC_REDUCTION" />
                    <asp:BoundField DataField="UDF2" HeaderText="UDF2" />
                    <asp:BoundField DataField="UDF3" HeaderText="UDF3" />
                    <asp:BoundField DataField="UDF4" HeaderText="UDF4" />
                    <asp:BoundField DataField="UDF5" HeaderText="UDF5" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" CssClass="FrozenHeader" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>


        </div>

    </div>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" CssClass="popup-edit">
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
                    <asp:Label ID="lblLegend" runat="server" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Invoice Date</label>
                    <asp:TextBox ID="txtInvoiceDate" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Job No.</label>
                    <asp:TextBox ID="txtJobNo" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Custoner Name</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustName" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCustCode" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Business Unit</label>
                    <asp:TextBox ID="txtBusinessUnit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Product Code</label>
                    <asp:TextBox ID="txtProductCode" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Revenue Account</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtRevAccount" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtAccountDesc" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 5%">
                                <asp:TextBox ID="txtAccountType" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantity" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Product Rate</label>
                    <asp:TextBox ID="txtProductRate" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Invoice Amount</label>
                    <asp:TextBox ID="txtInvoiceAmount" runat="server"
                        CssClass="form-control"
                        Enabled="true" onkeyup="chkUnpostedValueNew(this);"
                        onkeypress="return inNumberKey(this, event);" />
                    <asp:HiddenField ID="hdInvoiceAmount" runat="server" />
                    <asp:HiddenField ID="hdIsNewInserted" runat="server" />

                    <label>Posted Value</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%">
                                <asp:TextBox ID="txtPostedValue" runat="server"
                                    CssClass="form-control"
                                    onkeyup="chkUnpostedValueNew(this);"
                                    onkeypress="return inNumberKey(this, event);" />
                                <asp:HiddenField ID="hdPostedValue" runat="server" />
                            </td>
                            <td style="width: 30%">
                                <asp:DropDownList ID="ddlPostingCurrency" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <label>Unposted Value</label>
                    <asp:TextBox ID="txtUnpostedValue" runat="server"
                        CssClass="form-control"
                        Enabled="false" />
                    <asp:HiddenField ID="hdUnpostedValue" runat="server" />
                    <asp:HiddenField ID="hdUnpostingValue" runat="server" />
                    <asp:HiddenField ID="hdActualUnpostedValue" runat="server" />
                    <asp:HiddenField ID="hdPendingAmount" runat="server" />

                    <label>End Market</label>
                    <asp:DropDownList ID="ddlEndMarket" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Geogrophy</label>
                    <asp:DropDownList ID="ddlCountry" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Posting Month-Year</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 40%">
                                <asp:DropDownList ID="ddlPostingMonth" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Value="Jan" Text="Jan" />
                                    <asp:ListItem Value="Feb" Text="Feb" />
                                    <asp:ListItem Value="Mar" Text="Mar" />
                                    <asp:ListItem Value="Apr" Text="Apr" />
                                    <asp:ListItem Value="May" Text="May" />
                                    <asp:ListItem Value="Jun" Text="Jun" />
                                    <asp:ListItem Value="Jul" Text="Jul" />
                                    <asp:ListItem Value="Aug" Text="Aug" />
                                    <asp:ListItem Value="Sep" Text="Sep" />
                                    <asp:ListItem Value="Oct" Text="Oct" />
                                    <asp:ListItem Value="Nov" Text="Nov" />
                                    <asp:ListItem Value="Dec" Text="Dec" />
                                </asp:DropDownList>
                            </td>
                            <td style="width: 60%">
                                <asp:DropDownList ID="ddlPostingYear" runat="server"
                                    CssClass="form-control">
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
                            </td>
                        </tr>
                    </table>

                    <label>Type</label>
                    <asp:DropDownList ID="ddlType" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    </asp:DropDownList>

                    <label>Revenue/Not Revenue Type</label>
                    <asp:DropDownList ID="ddlRevenueType" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Rev. Rec. Redction</label>
                    <asp:TextBox ID="txtRevRecReduction" runat="server"
                        CssClass="form-control"
                        Enabled="true" />

                    <label>UDF2</label>
                    <asp:TextBox ID="txtUDF2" runat="server"
                        CssClass="form-control"
                        Enabled="true" />

                    <label>UDF3</label>
                    <asp:TextBox ID="txtUDF3" runat="server"
                        CssClass="form-control"
                        Enabled="true" />

                    <label>UDF4</label>
                    <asp:TextBox ID="txtUDF4" runat="server"
                        CssClass="form-control"
                        Enabled="true" />

                    <label>UDF5</label>
                    <asp:TextBox ID="txtUDF5" runat="server"
                        CssClass="form-control"
                        Enabled="true" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button
                    ID="btnUpdate"
                    CssClass="button"
                    runat="server"
                    Text="Update"
                    OnClientClick="return ValidateAllNew();"
                    Width="100%"
                    OnClick="btnUpdate_Click" />
            </div>

        </div>




    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
