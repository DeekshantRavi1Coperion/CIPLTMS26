<%@ Page Title="CIPLTMS - Export Posted BOM List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ExportPostedBOMList.aspx.cs" Inherits="MR_WORKFLOW_ExportPostedBOMList" %>

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
        function ValidateAllSearch() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>



    <script type="text/javascript">

        <%--var GridId = "<%=gvPostedBomList.ClientID %>";
        var ScrollHeight = 600;
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
        }--%>

        function ClearAllFilters() {

            document.getElementById('<%=txtMrNo.ClientID %>').value = "";
            document.getElementById('<%=txtBOMNo.ClientID %>').value = "";
            document.getElementById('<%=txtJobNo.ClientID %>').value = "";

            document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlMrCreatedBy.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlPivotGroup.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlResponsibleFor.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlIsTCRequired.ClientID %>').selectedIndex = 0;

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

    <style type="text/css">
        .textboxtstatustext {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxdrawings {
            padding: 5px 2px 5px 5px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightgreen;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
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


    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Post BOM List Filters:</legend>

                <div class="form-filter-grid">

                    <div class="full-width">
                        <label>Select/Unselect Dates</label>
                        <asp:CheckBox ID="chkSelectDates" runat="server"
                            Checked="true"
                            onchange="EnableDisableDates()" />

                        <asp:ImageButton ID="imgBtnClearAllFilters" runat="server"
                            ImageUrl="~/Images/NEWICONS/clear1.png"
                            Width="20px" Height="20px"
                            ToolTip="Clear Filters"
                            OnClientClick="return ClearAllFilters();" />

                    </div>

                    <label>Date Type</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="BOM Date" Value="BOM_DATE"></asp:ListItem>
                        <asp:ListItem Text="Mr date" Value="MR_DATE"></asp:ListItem>
                        <asp:ListItem Text="Delivery Required By" Value="DELIVERY_REQUIRED_BY"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch"
                                    PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server"
                                    ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch"
                                    PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server"
                                    ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Mr No.</label>
                    <asp:TextBox ID="txtMrNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Mr Created By</label>
                    <asp:DropDownList ID="ddlMrCreatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>BOM No.</label>
                    <asp:TextBox ID="txtBOMNo" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Job No.</label>
                    <asp:TextBox ID="txtJobNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Type</label>
                    <asp:DropDownList ID="ddlType" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Pivot Group</label>
                    <asp:DropDownList ID="ddlPivotGroup" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Responsible For</label>
                    <asp:DropDownList ID="ddlResponsibleFor" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Is TC Required:</label>
                    <asp:DropDownList ID="ddlIsTCRequired" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>

            </fieldset>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>

            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClientClick="return ValidateAllSearch();"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export"
                    OnClick="btnExport_Click" />

            </div>

        </div>



        <fieldset class="employee-grid-fieldset">

            <div class="employee-grid-container">

                <fieldset class="employee-grid-fieldset">
                    <legend>
                        <asp:Label ID="lblPurchaseIndentHeaderRecords" runat="server" Text="Records[0]" />
                    </legend>


                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvPurchaseIndentHeader" runat="server"
                        AutoGenerateColumns="false" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%"
                        HorizontalAlign="Center"
                        OnRowDataBound="gvPurchaseIndentHeader_RowDataBound">

                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Number">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqNumber" runat="server" Text='<%# Eval("Purchase Indent/Req Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqDate" runat="server" Text='<%# Eval("Purchase Indent/Req Date") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vendor Code">
                                <ItemTemplate>

                                    <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("Vendor Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Document Class Code">
                                <ItemTemplate>

                                    <asp:Label ID="lblDocumentClassCode" runat="server" Text='<%# Eval("Document Class Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>

                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expected PO Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblExpectedPODate" runat="server" Text='<%# Eval("Expected PO Date") %>' />
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

                </fieldset>
                <br />

                <fieldset class="employee-grid-fieldset">
                    <legend>
                        <asp:Label ID="lblPurchaseIndentUdfRecords" runat="server" Text="Records[0]" />
                    </legend>


                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvPurchaseIndentUdf" runat="server"
                        AutoGenerateColumns="false" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%"
                        HorizontalAlign="Center"
                        OnRowDataBound="gvPurchaseIndentUdf_RowDataBound">

                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Number">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqNumberUdf" runat="server" Text='<%# Eval("Purchase Indent/Req Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqDateUdf" runat="server" Text='<%# Eval("Purchase Indent/Req Date") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Acceptable Vendor 1">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcceptableVendor1" runat="server" Text='<%# Eval("Acceptable Vendor 1") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acceptable Vendor 2">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcceptableVendor2" runat="server" Text='<%# Eval("Acceptable Vendor 2") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acceptable Vendor 3">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcceptableVendor3" runat="server" Text='<%# Eval("Acceptable Vendor 3") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acceptable Vendor 4">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcceptableVendor4" runat="server" Text='<%# Eval("Acceptable Vendor 4") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acceptable Vendor 5">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcceptableVendor5" runat="server" Text='<%# Eval("Acceptable Vendor 5") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Revision Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevisionNumber" runat="server" Text='<%# Eval("Revision Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Budgeted Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblBudgetedCost" runat="server" Text='<%# Eval("Budgeted Cost") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estimated Cost">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstimatedCost" runat="server" Text='<%# Eval("Estimated Cost") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cost Related Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblCostRelatedRemarks" runat="server" Text='<%# Eval("Cost Related Remarks") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Pivot Group">
                                <ItemTemplate>
                                    <asp:Label ID="lblPivotGroup" runat="server" Text='<%# Eval("Pivot Group") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Responsible for PO">
                                <ItemTemplate>
                                    <asp:Label ID="lblResponsibleForPO" runat="server" Text='<%# Eval("Responsible for PO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Is TC Required?">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsTCRequired" runat="server" Text='<%# Eval("Is TC Required?") %>' />
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

                </fieldset>
                <br />

                <fieldset class="employee-grid-fieldset">
                    <legend>
                        <asp:Label ID="lblPurchaseIndentDetailRecords" runat="server" Text="Records[0]" />
                    </legend>


                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvPurchaseIndentDetail" runat="server"
                        AutoGenerateColumns="false" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%"
                        HorizontalAlign="Center"
                        OnRowDataBound="gvPurchaseIndentDetail_RowDataBound">

                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Number">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqNumberD" runat="server" Text='<%# Eval("Purchase Indent/Req Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Purchase Indent/Req Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblPurchaseIndentReqDateD" runat="server" Text='<%# Eval("Purchase Indent/Req Date") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Product Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("Product Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Converted Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblConvertedQuantity" runat="server" Text='<%# Eval("Converted Quantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Double Quantity – Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoubleQuantity" runat="server" Text='<%# Eval("Double Quantity - Quantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductUOM" runat="server" Text='<%# Eval("Product UOM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Double Quantity – UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoubleQuantityUOM" runat="server" Text='<%# Eval("Double Quantity - UOM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item Serial Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemSerialNumber" runat="server" Text='<%# Eval("Item Serial Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Additional Product Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdditionalProductDescription" runat="server" Text='<%# Eval("Additional Product Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Document Class Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocumentClassCodeD" runat="server" Text='<%# Eval("Document Class Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Manufacturer Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblManufacturerCode" runat="server" Text='<%# Eval("Manufacturer Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bar Code Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblBarCodeNumber" runat="server" Text='<%# Eval("Bar Code Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Scheme Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeCode" runat="server" Text='<%# Eval("Scheme Code") %>' />
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

                </fieldset>
            </div>

        </fieldset>


    </div>
</asp:Content>









