<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="SaleOrderOutstandingReportThree.aspx.cs" Inherits="REPORTS_SALE_ORDER_SaleOrderOutstandingReportThree" %>

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
        var GridId = "<%=gvSaleOrder.ClientID %>";
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Sale Order Backlog/Forecast Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Posting Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" 
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);"></asp:TextBox>
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
                ID="gvSaleOrder" runat="server" AutoGenerateColumns="true" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvSaleOrder_RowDataBound">
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


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
