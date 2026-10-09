<%@ Page Title="CIPLTMS - RPB2 Order Intake" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="RPB2OrderIntake.aspx.cs" Inherits="FINANCE_RPB2_RPB2OrderIntake" %>

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
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function pageLoad() {
            document.getElementById('<%=txtYearMonthSearch.ClientID %>').value = document.getElementById('<%=hdYearMonthSearch.ClientID %>').value;
        }

        function clientChangedYearMonthSearch(sender, args) {
            document.getElementById('<%=hdYearMonthSearch.ClientID %>').value = document.getElementById('<%=txtYearMonthSearch.ClientID %>').value;
        }

        function clientChangedYearMonthCompare(sender, args) {
            document.getElementById('<%=hdYearMonthCompare.ClientID %>').value = document.getElementById('<%=txtYearMonthCompare.ClientID %>').value;
        }

        function ValidateYearMonthSearch() {
            var YearMonthSearch = document.getElementById('<%=txtYearMonthSearch.ClientID %>').value;
            if (YearMonthSearch == '') {
                document.getElementById('<%=txtYearMonthSearch.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtYearMonthSearch.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateDateRange()) {
                check = false;
            }

            return check;
        }

        function ValidateAlltoPost() {
            var check = true;

            if (ValidateDateRange()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to post Rpb2 report?")) {
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


    <script type="text/Javascript">
        function ValidateDateRange() {

            var dateSearch = document.getElementById('<%=hdYearMonthSearch.ClientID %>').value.split("-");
            var dateCompare = document.getElementById('<%=hdYearMonthCompare.ClientID %>').value.split("-");

            var dateSearchString = dateSearch + "/01";
            var dateCompareString = dateCompare + "/01";

            let mS = dateSearchString.substring(0, 2)
            let yS = dateSearchString.substring(3, 7)

            let mC = dateCompareString.substring(0, 2)
            let yC = dateCompareString.substring(3, 7)

            var dtSearch = new Date(yS, mS, 01); //Year, Month, Date
            var dtCompare = new Date(yC, mC, 01); //Year, Month, Date

            if (dtSearch < dtCompare) {
                alert("Compared month must be equal or less than Search month!!!");
                return true;
            }
        }
    </script>




    <script type="text/javascript" language="javascript">
        function onCalendarShownYearMonthSearch() {
            var cal = $find("calendarYearMonthSearch");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callYearMonthSearch);
                    }
                }
            }
        }

        function onCalendarHiddenYearMonthSearch() {
            var cal = $find("calendarYearMonthSearch");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callYearMonthSearch);
                    }
                }
            }
        }

        function callYearMonthSearch(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarYearMonthSearch");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <script type="text/javascript" language="javascript">
        function onCalendarShownYearMonthCompare() {
            var cal = $find("calendarYearMonthCompare");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callYearMonthCompare);
                    }
                }
            }
        }

        function onCalendarHiddenYearMonthCompare() {
            var cal = $find("calendarYearMonthCompare");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callYearMonthCompare);
                    }
                }
            }
        }

        function callYearMonthCompare(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarYearMonthCompare");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>

    <script type="text/javascript">
        var GridId = "<%=gvRPB2Report.ClientID %>";
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Search By Month:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox ID="txtYearMonthSearch" runat="server" CssClass="form-control"
                                    onkeydown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:HiddenField ID="hdYearMonthSearch" runat="server" />
                                <asp:HiddenField ID="hdYearMonthSearchFull" runat="server" />
                                <asp:CalendarExtender ID="calendarYearMonthSearch" runat="server" OnClientHidden="onCalendarHiddenYearMonthSearch"
                                    PopupButtonID="imgbtnYearMonthSearch" OnClientShown="onCalendarShownYearMonthSearch" Format="MM/yyyy"
                                    BehaviorID="calendarYearMonthSearch" TargetControlID="txtYearMonthSearch" OnClientDateSelectionChanged="clientChangedYearMonthSearch">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtYearMonthSearch"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnYearMonthSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Year Month To Search Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="A35" Value="1" />
                        <asp:ListItem Text="GNU" Value="4" />
                    </asp:DropDownList>


                    <label>Revenue Type:</label>
                    <asp:DropDownList ID="ddlRevenueType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="POC" Value="1" />
                        <asp:ListItem Text="Non-POC" Value="2" />
                    </asp:DropDownList>

                    <label>Company Type:</label>
                    <asp:DropDownList ID="ddlCompanyType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Inter Company" Value="1" />
                        <asp:ListItem Text="Third Party" Value="2" />
                    </asp:DropDownList>

                    <label>BU</label>
                    <asp:TextBox ID="txtBU" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Order Number:</label>
                    <asp:TextBox ID="txtJobNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Compare By Month:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox ID="txtYearMonthCompare" runat="server" CssClass="form-control"
                                    onkeydown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:HiddenField ID="hdYearMonthCompare" runat="server" />
                                <asp:HiddenField ID="hdYearMonthCompareFull" runat="server" />
                                <asp:CalendarExtender ID="calendarYearMonthCompare" runat="server" OnClientHidden="onCalendarHiddenYearMonthCompare"
                                    PopupButtonID="imgbtnYearMonthCompare" OnClientShown="onCalendarShownYearMonthCompare" Format="MM/yyyy"
                                    BehaviorID="calendarYearMonthCompare" TargetControlID="txtYearMonthCompare" OnClientDateSelectionChanged="clientChangedYearMonthCompare">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtYearMonthCompare"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnYearMonthCompare" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Year Month To Compare Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                <asp:Button ID="tbnPost" CssClass="button" Width="100%" runat="server" Text="Post"
                    OnClientClick="return ValidateAlltoPost();"
                    OnClick="tbnPost_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                    OnClick="btnExport_Click" />

                <label>Year Month (Search):</label>
                <asp:TextBox ID="txtYearMonthSearchToShow" runat="server"
                    CssClass="form-control"
                    Enabled="false"
                    Font-Bold="true"></asp:TextBox>

                <label>Year Month (Compared):</label>
                <asp:TextBox ID="txtYearMonthComparedToShow" runat="server"
                    CssClass="form-control"
                    Enabled="false"
                    Font-Bold="true"></asp:TextBox>

                <label>Total OI Margin:</label>
                <asp:TextBox ID="txtTotalOIMarginToShow" runat="server"
                    CssClass="form-control"
                    Enabled="false"
                    Font-Bold="true"></asp:TextBox>

                <label>Total OI Margin (Compared):</label>
                <asp:TextBox ID="txtTotalOIMarginComparedToShow" runat="server"
                    CssClass="form-control"
                    Enabled="false"
                    Font-Bold="true"></asp:TextBox>

                <label>Delta value:</label>
                <asp:TextBox ID="txtDeltaValueToShow" runat="server"
                    CssClass="form-control"
                    Enabled="false"
                    Font-Bold="true"></asp:TextBox>

            </div>


        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>RPB2 Order Intake:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvRPB2Report" runat="server"
                    AutoGenerateColumns="false" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%"
                    HorizontalAlign="Center"
                    OnRowDataBound="gvRPB2Report_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitFid" runat="server" Visible="false" Text='<%# Eval("Unit_Fid") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("Location") %>' />
                                <asp:Label ID="lblYear" runat="server" Visible="true" Text='<%# Eval("Year") %>' />
                                <asp:Label ID="lblMonth" runat="server" Visible="false" Text='<%# Eval("Month") %>' />
                                <asp:Label ID="lblYearMonth" runat="server" Visible="false" Text='<%# Eval("Year_Month") %>' />
                                <asp:Label ID="lblRevenueTypeFid" runat="server" Visible="false" Text='<%# Eval("Revenue_Type_Fid") %>' />
                                <asp:Label ID="lblRevenueType" runat="server" Visible="false" Text='<%# Eval("Revenue_Type") %>' />
                                <asp:Label ID="lblBu" runat="server" Visible="false" Text='<%# Eval("Bu") %>' />
                                <asp:Label ID="lblCompanyTypeFid" runat="server" Visible="false" Text='<%# Eval("Company_Type_Fid") %>' />
                                <asp:Label ID="lblCompanyType" runat="server" Visible="false" Text='<%# Eval("Company_Type") %>' />
                                <asp:Label ID="lblIcCode" runat="server" Visible="false" Text='<%# Eval("Ic_Code") %>' />
                                <asp:Label ID="lblEntryTypeFid" runat="server" Visible="false" Text='<%# Eval("Entry_Type_Fid") %>' />
                                <asp:Label ID="lblEntryType" runat="server" Visible="false" Text='<%# Eval("Entry_Type") %>' />
                                <asp:Label ID="lblOrderNo" runat="server" Visible="false" Text='<%# Eval("Order_No") %>' />
                                <asp:Label ID="lblCurrencyFid" runat="server" Visible="false" Text='<%# Eval("Currency_Fid") %>' />
                                <asp:Label ID="lblCurrency" runat="server" Visible="false" Text='<%# Eval("Currency") %>' />
                                <asp:Label ID="lblRate" runat="server" Visible="false" Text='<%# Eval("Rate") %>' />
                                <asp:Label ID="lblFcOrderValue" runat="server" Visible="false" Text='<%# Eval("Fc_Order_Value") %>' />
                                <asp:Label ID="lblInrOrderValue" runat="server" Visible="false" Text='<%# Eval("Inr_Order_Value") %>' />
                                <asp:Label ID="lblOiMargin" runat="server" Visible="false" Text='<%# Eval("Oi_Margin") %>' />
                                <asp:Label ID="lblComparedYearMonth" runat="server" Visible="false" Text='<%# Eval("Compared_Year_Month") %>' />
                                <asp:Label ID="lblComparedOiMargin" runat="server" Visible="false" Text='<%# Eval("Compared_Oi_Margin") %>' />
                                <asp:Label ID="lblDeltaValue" runat="server" Visible="false" Text='<%# Eval("Delta_Value") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="Month" DataField="Month" />
                        <asp:BoundField HeaderText="Revenue Type" DataField="Revenue_Type" />
                        <asp:BoundField HeaderText="Bu" DataField="Bu" />
                        <asp:BoundField HeaderText="Company Type" DataField="Company_Type" />
                        <asp:BoundField HeaderText="Ic Code" DataField="Ic_Code" />
                        <asp:BoundField HeaderText="Entry Type" DataField="Entry_Type" />
                        <asp:BoundField HeaderText="Order No" DataField="Order_No" />
                        <asp:BoundField HeaderText="Currency" DataField="Currency" />
                        <asp:BoundField HeaderText="Rate" DataField="Rate" />
                        <asp:BoundField HeaderText="Fc Order Value" DataField="Fc_Order_Value" />
                        <asp:BoundField HeaderText="Inr Order Value" DataField="Inr_Order_Value" />
                        <asp:BoundField HeaderText="Oi Margin" DataField="Oi_Margin" />
                        <asp:BoundField HeaderText="Compared Year Month" DataField="Compared_Year_Month" />
                        <asp:BoundField HeaderText="Compared Oi Margin" DataField="Compared_Oi_Margin" />
                        <asp:BoundField HeaderText="Delta Value" DataField="Delta_Value" />
                        <asp:BoundField HeaderText="Location" DataField="Location" />

                    </Columns>
                </asp:GridView>

            </div>

        </fieldset>

    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
