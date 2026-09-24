<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="POCPhasing.aspx.cs" Inherits="REPORTS_SALE_ORDER_POCPhasing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        function ValidatePostingMonth() {
            var PostingMonth = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
            if (PostingMonth == '') {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidatePostingMonth()) { return false; }
            return true;
        }
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
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
        var GridId = "<%=gvPOCList.ClientID %>";
        var ScrollHeight = 335;
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

    <script type="text/javascript">
        function checkDec1(el, val) {
            var row = el.parentNode.parentNode;

            var lblFinalBacklog = row.cells[20].innerText;
            var txtMonthFirst = row.cells[24].getElementsByTagName("input")[0].value;
            var txtMonthSecond = row.cells[25].getElementsByTagName("input")[0].value;
            var txtMonthThird = row.cells[26].getElementsByTagName("input")[0].value;
            var txtMonthFourth = row.cells[27].getElementsByTagName("input")[0].value;
            var txtMonthFifth = row.cells[28].getElementsByTagName("input")[0].value;
            var txtMonthSixth = row.cells[29].getElementsByTagName("input")[0].value;
            var txtMonthSeventh = row.cells[30].getElementsByTagName("input")[0].value;
            var txtMonthEighth = row.cells[31].getElementsByTagName("input")[0].value;
            var txtMonthNinth = row.cells[32].getElementsByTagName("input")[0].value;
            var txtMonthTenth = row.cells[33].getElementsByTagName("input")[0].value;
            var txtMonthEleventh = row.cells[34].getElementsByTagName("input")[0].value;
            var txtMonthTwelfth = row.cells[35].getElementsByTagName("input")[0].value;
            var txtNextYears = row.cells[36].getElementsByTagName("input")[0].value;


            var FinalBacklog = 0;
            var MonthFirst = 0;
            var MonthSecond = 0;
            var MonthThird = 0;
            var MonthFourth = 0;
            var MonthFifth = 0;
            var MonthSixth = 0;
            var MonthSeventh = 0;
            var MonthEighth = 0;
            var MonthNinth = 0;
            var MonthTenth = 0;
            var MonthEleventh = 0;
            var MonthTwelfth = 0;
            var NextYears = 0;



            if (lblFinalBacklog != '' && parseFloat(lblFinalBacklog) > '0') {
                FinalBacklog = parseFloat(lblFinalBacklog);
            }
            else {
                FinalBacklog = 0;
            }

            if (txtMonthFirst != '' && parseFloat(txtMonthFirst) > '0') {
                MonthFirst = parseFloat(txtMonthFirst);
            }
            else {
                MonthFirst = 0;
            }

            if (txtMonthSecond != '' && parseFloat(txtMonthSecond) > '0') {
                MonthSecond = parseFloat(txtMonthSecond);
            }
            else {
                MonthSecond = 0;
            }

            if (txtMonthThird != '' && parseFloat(txtMonthThird) > '0') {
                MonthThird = parseFloat(txtMonthThird);
            }
            else {
                MonthThird = 0;
            }

            if (txtMonthFourth != '' && parseFloat(txtMonthFourth) > '0') {
                MonthFourth = parseFloat(txtMonthFourth);
            }
            else {
                MonthFourth = 0;
            }

            if (txtMonthFifth != '' && parseFloat(txtMonthFifth) > '0') {
                MonthFifth = parseFloat(txtMonthFifth);
            }
            else {
                MonthFifth = 0;
            }

            if (txtMonthSixth != '' && parseFloat(txtMonthSixth) > '0') {
                MonthSixth = parseFloat(txtMonthSixth);
            }
            else {
                MonthSixth = 0;
            }

            if (txtMonthSeventh != '' && parseFloat(txtMonthSeventh) > '0') {
                MonthSeventh = parseFloat(txtMonthSeventh);
            }
            else {
                MonthSeventh = 0;
            }

            if (txtMonthEighth != '' && parseFloat(txtMonthEighth) > '0') {
                MonthEighth = parseFloat(txtMonthEighth);
            }
            else {
                MonthEighth = 0;
            }

            if (txtMonthNinth != '' && parseFloat(txtMonthNinth) > '0') {
                MonthNinth = parseFloat(txtMonthNinth);
            }
            else {
                MonthNinth = 0;
            }

            if (txtMonthTenth != '' && parseFloat(txtMonthTenth) > '0') {
                MonthTenth = parseFloat(txtMonthTenth);
            }
            else {
                MonthTenth = 0;
            }

            if (txtMonthEleventh != '' && parseFloat(txtMonthEleventh) > '0') {
                MonthEleventh = parseFloat(txtMonthEleventh);
            }
            else {
                MonthEleventh = 0;
            }

            if (txtMonthTwelfth != '' && parseFloat(txtMonthTwelfth) > '0') {
                MonthTwelfth = parseFloat(txtMonthTwelfth);
            }
            else {
                MonthTwelfth = 0;
            }

            if (txtNextYears != '' && parseFloat(txtNextYears) > '0') {
                NextYears = parseFloat(txtNextYears);
            }
            else {
                NextYears = 0;
            }


            if (val == 24) {
                var OtherThanFirst = (MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthFirst <= (FinalBacklog - OtherThanFirst))
                    row.cells[24].getElementsByTagName("input")[0].value = MonthFirst;
                else
                    row.cells[24].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanFirst);

            }
            else if (val == 25) {
                var OtherThanSecond = (MonthFirst + MonthThird + MonthFourth + MonthFifth + MonthSixth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthSecond <= (FinalBacklog - OtherThanSecond))
                    row.cells[25].getElementsByTagName("input")[0].value = MonthSecond;
                else
                    row.cells[25].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanSecond);

            }
            else if (val == 26) {
                var OtherThanThird = (MonthFirst + MonthSecond + MonthFourth + MonthFifth + MonthSixth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthThird <= (FinalBacklog - OtherThanThird))
                    row.cells[26].getElementsByTagName("input")[0].value = MonthThird;
                else
                    row.cells[26].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanThird);

            }
            else if (val == 27) {
                var OtherThanFourth = (MonthFirst + MonthSecond + MonthThird + MonthFifth + MonthSixth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthFourth <= (FinalBacklog - OtherThanFourth))
                    row.cells[27].getElementsByTagName("input")[0].value = MonthFourth;
                else
                    row.cells[27].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanFourth);
            }
            else if (val == 28) {
                var OtherThanFifth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthSixth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthFifth <= (FinalBacklog - OtherThanFifth))
                    row.cells[28].getElementsByTagName("input")[0].value = MonthFifth;
                else
                    row.cells[28].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanFifth);
            }
            else if (val == 29) {
                var OtherThanSixth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSeventh +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthSixth <= (FinalBacklog - OtherThanSixth))
                    row.cells[29].getElementsByTagName("input")[0].value = MonthSixth;
                else
                    row.cells[29].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanSixth);
            }
            else if (val == 30) {
                var OtherThanSeventh = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthSeventh <= (FinalBacklog - OtherThanSeventh))
                    row.cells[30].getElementsByTagName("input")[0].value = MonthSeventh;
                else
                    row.cells[30].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanSeventh);
            }
            else if (val == 31) {
                var OtherThanEighth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthEighth <= (FinalBacklog - OtherThanEighth))
                    row.cells[31].getElementsByTagName("input")[0].value = MonthEighth;
                else
                    row.cells[31].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanEighth);
            }
            else if (val == 32) {
                var OtherThanNinth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthEighth + MonthTenth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthNinth <= (FinalBacklog - OtherThanNinth))
                    row.cells[32].getElementsByTagName("input")[0].value = MonthNinth;
                else
                    row.cells[32].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanNinth);
            }
            else if (val == 33) {
                var OtherThanTenth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthEighth + MonthNinth + MonthEleventh + MonthTwelfth + NextYears);

                if (MonthTenth <= (FinalBacklog - OtherThanTenth))
                    row.cells[33].getElementsByTagName("input")[0].value = MonthTenth;
                else
                    row.cells[33].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanTenth);
            }
            else if (val == 34) {
                var OtherThanEleventh = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthEighth + MonthNinth + MonthTenth + MonthTwelfth + NextYears);

                if (MonthEleventh <= (FinalBacklog - OtherThanEleventh))
                    row.cells[34].getElementsByTagName("input")[0].value = MonthEleventh;
                else
                    row.cells[34].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanEleventh);
            }
            else if (val == 35) {
                var OtherThanTwelfth = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthEighth + MonthNinth + MonthTenth + MonthEleventh + NextYears);

                if (MonthTwelfth <= (FinalBacklog - OtherThanTwelfth))
                    row.cells[35].getElementsByTagName("input")[0].value = MonthTwelfth;
                else
                    row.cells[35].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanTwelfth);
            }
            else if (val == 36) {
                var OtherThanNextYears = (MonthFirst + MonthSecond + MonthThird + MonthFourth + MonthFifth + MonthSixth +
                    MonthSeventh + MonthEighth + MonthNinth + MonthTenth + MonthEleventh + MonthTwelfth);

                if (NextYears <= (FinalBacklog - OtherThanNextYears))
                    row.cells[36].getElementsByTagName("input")[0].value = NextYears;
                else
                    row.cells[36].getElementsByTagName("input")[0].value = (FinalBacklog - OtherThanNextYears);
            }



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
                <legend>POC Phasing:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Posting Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" 
                                    CssClass="form-control"
                                    onkeydown="javascript:preventInput(event);"></asp:TextBox>
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

                    <label>JOB No:</label>
                    <asp:TextBox ID="txtJobNo" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>Customer Name:</label>
                    <asp:TextBox ID="txtCustomerName" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>Customer Type:</label>
                    <asp:DropDownList ID="ddlCustomerType" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Inter Company" Value="1" />
                        <asp:ListItem Text="Third Party" Value="2" />
                    </asp:DropDownList>

                    <label>BU:</label>
                    <asp:TextBox ID="txtBU" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>Currency:</label>
                    <asp:DropDownList ID="ddlCurrency" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="SELECT" Value="0" />
                        <asp:ListItem Text="IC" Value="1" />
                        <asp:ListItem Text="THIRD_PARTY" Value="2" />
                    </asp:DropDownList>
                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
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
                ID="gvPOCList" runat="server" AutoGenerateColumns="false" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvPOCList_RowDataBound"
                OnRowCommand="gvPOCList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="JOB_No" HeaderText="JOB_No" />
                    <asp:BoundField DataField="Customer_Code" HeaderText="Customer_Code" />
                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer_Name" />
                    <asp:BoundField DataField="Customer_Type" HeaderText="Customer_Type" />
                    <asp:BoundField DataField="DOC_Class" HeaderText="DOC_Class" />
                    <asp:BoundField DataField="BU" HeaderText="BU" />
                    <asp:BoundField DataField="Order_Currency" HeaderText="Order_Currency" />
                    <asp:BoundField DataField="Order_Curr_Rate" HeaderText="Order_Curr_Rate" />
                    <asp:BoundField DataField="Order_Amt_FC" HeaderText="Order_Amt_FC" />
                    <asp:BoundField DataField="INV_Amt_FC" HeaderText="INV_Amt_FC" />
                    <asp:BoundField DataField="Delta_FC" HeaderText="Delta_FC" />
                    <asp:BoundField DataField="Order_Amt_INR" HeaderText="Order_Amt_INR" />
                    <asp:BoundField DataField="INV_Amt_INR" HeaderText="INV_Amt_INR" />
                    <asp:BoundField DataField="Delta_INR" HeaderText="Delta_INR" />
                    <asp:BoundField DataField="Current_FC_Rate" HeaderText="Current_FC_Rate" />
                    <asp:BoundField DataField="Order_Amt_INR_On_Current_Rate" HeaderText="Order_Amt_INR_On_Current_Rate" />
                    <asp:BoundField DataField="INV_Amt_INR_On_Current_Rate" HeaderText="INV_Amt_INR_On_Current_Rate" />
                    <asp:BoundField DataField="Delta_On_Curr_Rate" HeaderText="Delta_On_Curr_Rate" />
                    <asp:BoundField DataField="POC_Billing_Amt" HeaderText="POC_Billing_Amt" />
                    <asp:BoundField DataField="TAX_Billing_Amt" HeaderText="TAX_Billing_Amt" />

                    <asp:TemplateField HeaderText="Final_Backlog">
                        <ItemTemplate>
                            <asp:Label ID="lblFinalBacklog" runat="server" Visible="true" Text='<%# Eval("Final_Backlog") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Adjustment" HeaderText="Adjustment" />
                    <asp:BoundField DataField="Expected_Date" HeaderText="Expected_Date" />

                    <asp:TemplateField HeaderText="Posting_Month">
                        <ItemTemplate>
                            <asp:Label ID="lblPostingMonth" runat="server" Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--24--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthFirst" Width="100PX" runat="server" Text='<%#Eval("MONTH_01") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,24)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthSecond" Width="100PX" runat="server" Text='<%#Eval("MONTH_02") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,25)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthThird" Width="100PX" runat="server" Text='<%#Eval("MONTH_03") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,26)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthFourth" Width="100PX" runat="server" Text='<%#Eval("MONTH_04") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,27)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthFifth" Width="100PX" runat="server" Text='<%#Eval("MONTH_05") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,28)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthSixth" Width="100PX" runat="server" Text='<%#Eval("MONTH_06") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,29)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthSeventh" Width="100PX" runat="server" Text='<%#Eval("MONTH_07") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,30)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthEighth" Width="100PX" runat="server" Text='<%#Eval("MONTH_08") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,31)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthNinth" Width="100PX" runat="server" Text='<%#Eval("MONTH_09") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,32)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthTenth" Width="100PX" runat="server" Text='<%#Eval("MONTH_10") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,33)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthEleventh" Width="100PX" runat="server" Text='<%#Eval("MONTH_11") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,34)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtMonthTwelfth" Width="100PX" runat="server" Text='<%#Eval("MONTH_12") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,35)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NEXT_YEARS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNextYears" Width="100PX" runat="server" Text='<%#Eval("NEXT_YEARS") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this,36)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POST" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_No") %>' />
                            <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("Customer_Code") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("Customer_Name") %>' />
                            <asp:Label ID="lblCustomerType" runat="server" Visible="false" Text='<%# Eval("Customer_Type") %>' />
                            <asp:Label ID="lblDOCClass" runat="server" Visible="false" Text='<%# Eval("DOC_Class") %>' />
                            <asp:Label ID="lblBU" runat="server" Visible="false" Text='<%# Eval("BU") %>' />
                            <asp:Label ID="lblOrderCurrency" runat="server" Visible="false" Text='<%# Eval("Order_Currency") %>' />
                            <asp:Label ID="lblOrderCurrRate" runat="server" Visible="false" Text='<%# Eval("Order_Curr_Rate") %>' />
                            <asp:Label ID="lblOrderAmtFC" runat="server" Visible="false" Text='<%# Eval("Order_Amt_FC") %>' />
                            <asp:Label ID="lblINVAmtFC" runat="server" Visible="false" Text='<%# Eval("INV_Amt_FC") %>' />
                            <asp:Label ID="lblDeltaFC" runat="server" Visible="false" Text='<%# Eval("Delta_FC") %>' />
                            <asp:Label ID="lblOrderAmtINR" runat="server" Visible="false" Text='<%# Eval("Order_Amt_INR") %>' />
                            <asp:Label ID="lblINVAmtINR" runat="server" Visible="false" Text='<%# Eval("INV_Amt_INR") %>' />
                            <asp:Label ID="lblDeltaINR" runat="server" Visible="false" Text='<%# Eval("Delta_INR") %>' />
                            <asp:Label ID="lblCurrentFCRate" runat="server" Visible="false" Text='<%# Eval("Current_FC_Rate") %>' />
                            <asp:Label ID="lblOrderAmtINROnCurrentRate" runat="server" Visible="false" Text='<%# Eval("Order_Amt_INR_On_Current_Rate") %>' />
                            <asp:Label ID="lblINVAmtINROnCurrentRate" runat="server" Visible="false" Text='<%# Eval("INV_Amt_INR_On_Current_Rate") %>' />
                            <asp:Label ID="lblDeltaOnCurrRate" runat="server" Visible="false" Text='<%# Eval("Delta_On_Curr_Rate") %>' />
                            <asp:Label ID="lblPOCBillingAmt" runat="server" Visible="false" Text='<%# Eval("POC_Billing_Amt") %>' />
                            <asp:Label ID="lblTAXBillingAmt" runat="server" Visible="false" Text='<%# Eval("TAX_Billing_Amt") %>' />
                            <asp:Label ID="lblAdjustment" runat="server" Visible="false" Text='<%# Eval("Adjustment") %>' />
                            <asp:Label ID="lblExpectedDate" runat="server" Visible="false" Text='<%# Eval("Expected_Date") %>' />
                            <asp:Label ID="lblPOC" runat="server" Visible="false" Text='<%# Eval("POC") %>' />
                            <asp:Button ID="btnPost" CommandArgument="POST" runat="server" Text="Post" CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>

    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
