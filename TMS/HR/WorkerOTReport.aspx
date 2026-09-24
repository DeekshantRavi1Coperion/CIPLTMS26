<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="WorkerOTReport.aspx.cs" Inherits="HR_WorkerOTReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
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
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
        }


        function clientChanged(sender, args) {

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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

    <script type="text/javascript">
        var GridId = "<%=gvOTReport.ClientID %>";
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Worker OT Hours:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDate" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate" runat="server"
                                    TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnStartDate" align="right" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDate" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDate" runat="server"
                                    TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Employee Code:</label>
                    <asp:TextBox ID="txtEmployeeCode" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Employee:</label>
                    <asp:DropDownList ID="ddlWorker" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />

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
                ID="gvOTReport" runat="server" AutoGenerateColumns="false" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvOTReport_RowCommand" OnRowDataBound="gvOTReport_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="VIEW">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeID" runat="server" Visible="false" Text='<%# Eval("employee_pid") %>' />
                            <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            <asp:Label ID="lblEmployeeCode" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                            <asp:Label ID="lblNetOTPayble" runat="server" Visible="false" Text='<%# Eval("NET_OT_PAYBLE") %>' />

                            <asp:ImageButton ID="imgViewDetails" CommandArgument="VIEW" ImageUrl="~/Images/search1.png" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" />
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="TOTAL_EARNINGS" HeaderText="TOTAL_EARNINGS" />
                    <asp:BoundField DataField="TOTAL_OT_HRS" HeaderText="TOTAL_OT_HRS" />
                    <asp:BoundField DataField="ESI_DEDUCTED" HeaderText="ESI_DEDUCTED" />
                    <asp:BoundField DataField="WEEKOFF_FOOD" HeaderText="WEEKOFF_FOOD" />
                    <asp:BoundField DataField="WORKING_DAYS" HeaderText="WORKING_DAYS" />
                    <asp:BoundField DataField="WEEK_OFF_WORKING_DAYS" HeaderText="WEEK_OFF_WORKING_DAYS" />
                    <asp:BoundField DataField="TOTAL_WORKING_DAYS" HeaderText="TOTAL_WORKING_DAYS" />
                    <asp:BoundField DataField="CONVENIENCE_AMT" HeaderText="CONVENIENCE_AMT" />
                    <asp:BoundField DataField="NET_OT_PAYBLE" HeaderText="NET_OT_PAYBLE" />
                    <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
                    <asp:TemplateField HeaderText="DISPUTED">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDisputed" Enabled="false" runat="server" />
                            <asp:Label ID="lblIsDisputed" runat="server" Visible="false" Text='<%# Eval("IS_DISPUTED") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
    <asp:Button ID="btnShowDetails" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowDetails"
        PopupControlID="pnlViewShowDetailPopup" CancelControlID="imgBtnCancelDetails" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewShowDetailPopup" runat="server" BackColor="White" Height="650px"
        Width="1300px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDetails" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblLegendDetails" runat="server" />:
                         <asp:Label ID="lblOTDetailsRecords" runat="server" Text="Records[0]"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>From Date:</label>
                        <asp:TextBox ID="txtFromDateDetails" runat="server" 
                            CssClass="form-control"
                            Enabled="false"></asp:TextBox>

                        <label>To Date:</label>
                        <asp:TextBox ID="txtToDateDetails" runat="server" 
                            CssClass="form-control"
                            Enabled="false"></asp:TextBox>

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnExportDetails" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportDetails_Click" />
                </div>
            </div>

            <div class="employee-grid-container">
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvOTDetails" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvOTDetails_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ATTENDANCE_DATE" HeaderText="ATTENDANCE_DATE" />
                        <asp:BoundField DataField="WEEK_DAY" HeaderText="WEEK_DAY" />
                        <asp:BoundField DataField="ATTENDANCE_TYPE" HeaderText="ATTENDANCE_TYPE" />
                        <asp:BoundField DataField="SHIFT" HeaderText="SHIFT" />
                        <asp:BoundField DataField="SHIFT_IN_TIME" HeaderText="SHIFT_IN_TIME" />
                        <asp:BoundField DataField="SHIFT_OUT_TIME" HeaderText="SHIFT_OUT_TIME" />
                        <asp:BoundField DataField="PUNCH_IN_TIME" HeaderText="PUNCH_IN_TIME" />
                        <asp:BoundField DataField="PUNCH_OUT_TIME" HeaderText="PUNCH_OUT_TIME" />
                        <asp:BoundField DataField="EXTRA_SPENT_TIME" HeaderText="EXTRA_SPENT_TIME" />
                        <asp:BoundField DataField="ROUNDED_OT_HRS" HeaderText="ROUNDED_OT_HRS" />
                        <asp:TemplateField HeaderText="DISPUTED">
                            <ItemTemplate>
                                <asp:Label ID="lblIsDisputed" runat="server" Visible="true" Text='<%# Eval("DISPUTED") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
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
</asp:Content>
