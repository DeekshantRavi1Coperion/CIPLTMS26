<%@ Page Title="CIPLTMS - HR Ticket Report" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TicketReport.aspx.cs" Inherits="HR_TICKET_TicketReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
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


    <script type="text/javascript">
        var GridId = "<%=gvTicketList.ClientID %>";
        var ScrollHeight = 450;
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
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>



    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Date Type:</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Created On" Value="CREATED_ON" />
                        <asp:ListItem Text="Allocated On" Value="ALLOCATED_ON" />
                        <asp:ListItem Text="Closed On" Value="CLOSED_ON" />
                        <asp:ListItem Text="Cancelled On" Value="CANCELLED_ON" />
                    </asp:DropDownList>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch"
                                    PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
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
                            <td style="width:80%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbtnEndDateSearch" runat="server"
                                                ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="End Date Calendar" Width="20px" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkSelectDates" runat="server"
                                                Checked="true"
                                                onchange="EnableDisableDates()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <label>Ticket No.:</label>
                    <asp:TextBox ID="txtTicketNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Ticket Type:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlTicketType"
                                    runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlTicketType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTicketSubtype" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <label>Ticket Status.:</label>
                    <asp:DropDownList ID="ddlTicketStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Created By:</label>
                    <asp:DropDownList ID="ddlCreatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Allocated To:</label>
                    <asp:DropDownList ID="ddlAllocatedTo" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" runat="server" Width="100%" Text="Search"
                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />

                <asp:Button ID="btnExportToExcel" CssClass="button" runat="server" Width="100%" Text="Export To Excel"
                    OnClick="btnExportToExcel_Click" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Ticket Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

            <div class="employee-grid-container">

                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>

                    <asp:GridView
                        ID="gvTicketList" runat="server"
                        AutoGenerateColumns="false" CellPadding="4"
                        CssClass="employee-grid"
                        ForeColor="#333333" GridLines="Both" Width="100%"
                        HorizontalAlign="Center"
                        OnRowCommand="gvTicketList_RowCommand"
                        OnRowDataBound="gvTicketList_RowDataBound">

                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="View"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnViewDetail" CommandArgument="VIEW_DETAIL"
                                        runat="server" ImageUrl="~/Images/pdficon1.png" Height="30PX" Width="30PX" />


                                    <asp:Label ID="lblTicketID" runat="server" Visible="false" Text='<%# Eval("PID") %>'></asp:Label>
                                    <asp:Label ID="lblTicketNO" runat="server" Visible="false" Text='<%# Eval("TICKET_NUMBER") %>'></asp:Label>
                                    <asp:Label ID="lblTicketStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_FID") %>'></asp:Label>

                                    <asp:Label ID="lblTicketType" runat="server" Visible="false" Text='<%# Eval("TICKET_TYPE") %>'></asp:Label>

                                    <asp:Label ID="lblTicketSubtype" runat="server" Visible="false" Text='<%# Eval("TICKET_SUBTYPE") %>'></asp:Label>

                                    <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>

                                    <asp:Label ID="lblCreatedOn" runat="server" Visible="false" Text='<%# Eval("CREATED_ON") %>'></asp:Label>
                                    <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("CREATED_REMARKS") %>'></asp:Label>

                                    <asp:Label ID="lblAllocatedOn" runat="server" Visible="false" Text='<%# Eval("ALLOCATED_ON") %>'></asp:Label>
                                    <asp:Label ID="lblAllocatedRemarks" runat="server" Visible="false" Text='<%# Eval("ALLOCATED_REMARKS") %>'></asp:Label>

                                    <asp:Label ID="lblClosedOn" runat="server" Visible="false" Text='<%# Eval("CLOSED_ON") %>'></asp:Label>
                                    <asp:Label ID="lblClosedRemarks" runat="server" Visible="false" Text='<%# Eval("CLOSED_REMARKS") %>'></asp:Label>

                                    <asp:Label ID="lblCancelledOn" runat="server" Visible="false" Text='<%# Eval("CANCELLED_ON") %>'></asp:Label>
                                    <asp:Label ID="lblCancelledRemarks" runat="server" Visible="false" Text='<%# Eval("CANCELLED_REMARKS") %>'></asp:Label>

                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("COMBINED_STATUS_MULTIPLE_LINES") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachment"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileOneName" runat="server" Visible="false" Text='<%# Eval("FILE_NAME1") %>' />
                                    <asp:ImageButton ID="btnFileOneName" Height="20px" Width="20px"
                                        CommandArgument="ViewATTACHMENT1"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachment2"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileTwoName" runat="server" Visible="false" Text='<%# Eval("FILE_NAME2") %>' />
                                    <asp:ImageButton ID="btnFileTwoName" Height="20px" Width="20px"
                                        CommandArgument="ViewATTACHMENT2"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Attachment3"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileThreeName" runat="server" Visible="false" Text='<%# Eval("FILE_NAME3") %>' />
                                    <asp:ImageButton ID="btnFileThreeName" Height="20px" Width="20px"
                                        CommandArgument="ViewATTACHMENT3"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ClosingAttachment1"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingFileOneName" runat="server" Visible="false" Text='<%# Eval("FILECLOSING_NAME1") %>' />
                                    <asp:ImageButton ID="btnClosingFileName" Height="20px" Width="20px"
                                        CommandArgument="ViewCLOSINGATTACHMENT"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ClosingAttachment2"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingFileTwoName" runat="server" Visible="false" Text='<%# Eval("FILECLOSING_NAME2") %>' />
                                    <asp:ImageButton ID="btnClosingFileTwoName" Height="20px" Width="20px"
                                        CommandArgument="ViewCLOSINGATTACHMENT2"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ClosingAttachment3"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingFileThreeName" runat="server" Visible="false" Text='<%# Eval("FILECLOSING_NAME3") %>' />
                                    <asp:ImageButton ID="btnClosingFileThreeName" Height="20px" Width="20px"
                                        CommandArgument="ViewCLOSINGATTACHMENT3"
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="TICKET_NUMBER" HeaderText="Ticket Number"></asp:BoundField>
                            <asp:BoundField DataField="STATUS_NAME" HeaderText="Status"></asp:BoundField>
                            <asp:BoundField DataField="TICKET_TYPE" HeaderText="Ticket Type"></asp:BoundField>
                            <asp:BoundField DataField="TICKET_SUBTYPE" HeaderText="Ticket Subtype"></asp:BoundField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description"></asp:BoundField>

                            <asp:BoundField DataField="CREATED_BY" HeaderText="Created By"></asp:BoundField>
                            <asp:BoundField DataField="CREATED_ON" HeaderText="Created On"></asp:BoundField>
                            <asp:BoundField DataField="CREATED_REMARKS" HeaderText="Created Remarks"></asp:BoundField>

                            <asp:BoundField DataField="ALLOCATED_BY" HeaderText="Allocated By"></asp:BoundField>
                            <asp:BoundField DataField="ALLOCATED_ON" HeaderText="Allocated On"></asp:BoundField>
                            <asp:BoundField DataField="ALLOCATED_REMARKS" HeaderText="Allocated Remarks"></asp:BoundField>


                            <asp:BoundField DataField="ALLOCATED_TO" HeaderText="Allocated To"></asp:BoundField>

                            <asp:BoundField DataField="CLOSED_BY" HeaderText="Closed By"></asp:BoundField>
                            <asp:BoundField DataField="CLOSED_ON" HeaderText="Closed On"></asp:BoundField>
                            <asp:BoundField DataField="CLOSED_REMARKS" HeaderText="Closed Remarks"></asp:BoundField>

                            <asp:BoundField DataField="CANCELLED_BY" HeaderText="Cancelled By"></asp:BoundField>
                            <asp:BoundField DataField="CANCELLED_ON" HeaderText="Cancelled On"></asp:BoundField>
                            <asp:BoundField DataField="CANCELLED_REMARKS" HeaderText="Cancelled Remarks"></asp:BoundField>

                            <asp:TemplateField HeaderText="Status Details"
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtFormatedStatus"
                                        Text='<%# Eval("COMBINED_STATUS_MULTIPLE_LINES") %>'
                                        TextMode="MultiLine"
                                        Columns="10" Rows="5" Width="250px" ReadOnly="true"
                                        CssClass="form-control" />
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

        </fieldset>

    </div>



    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div cssclass="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>


    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            cssclass="popup-iframe"
            id="iframeViewPDFFile"
            runat="server"></iframe>
    </asp:Panel>


    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeViewInPDF" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            cssclass="popup-iframe"
            id="iframeViewDrawingDetailsInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>



    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
