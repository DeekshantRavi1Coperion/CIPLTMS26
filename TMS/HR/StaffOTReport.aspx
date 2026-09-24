<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="StaffOTReport.aspx.cs" Inherits="HR_StaffOTReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link rel="icon" href="../Images/Icon04.png" />
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

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function ValidateAll() {

            if (ValidateDepartment()) {
                return false
            }

            if (ValidateDateRange()) {
                return false;
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
                <legend>Staff OT Hours Report:
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
                                <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate"
                                    runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
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
                                <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDate"
                                    runat="server" TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Department:</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Name:</label>
                    <div style='overflow: auto; width: 100%; height: 400px; border: 1px solid lightgray;'>
                        <asp:CheckBoxList ID="chklbEmployee" runat="server" Width="100%" />
                    </div>

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
                ID="gvOTReport" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvOTReport_RowDataBound"
                OnRowCommand="gvOTReport_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" />
                    <asp:BoundField DataField="TOTAL_OT_HRS" HeaderText="TOTAL_OT_HRS" />
                    <asp:BoundField DataField="NET_OT_PAYBLE" HeaderText="NET_OT_PAYBLE" />
                    <asp:TemplateField HeaderText="DISPUTED">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDisputed" Enabled="false" runat="server" />
                            <asp:Label ID="lblIsDisputed" runat="server" Visible="false" Text='<%# Eval("IS_DISPUTED") %>' />
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnExport" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <asp:Button ID="btnShowDetails" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowDetails"
        PopupControlID="pnlViewShowDetailPopup" CancelControlID="imgBtnCancelDetails" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewShowDetailPopup" runat="server"
        CssClass="popup-edit">
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

                        <asp:Button ID="btnExportDetails" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExportDetails_Click" />

                    </div>
                </fieldset>                
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
                            <asp:BoundField DataField="EXTRA_TIME_SPENT" HeaderText="EXTRA_TIME_SPENT" />
                            <asp:BoundField DataField="ROUNDED_OT_HRS" HeaderText="ROUNDED_OT_HRS" />
                            <asp:BoundField DataField="NET_OT_PAYBLE" HeaderText="NET_OT_PAYBLE" />
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
