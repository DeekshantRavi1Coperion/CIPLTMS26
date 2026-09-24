<%@ Page Title="CIPLTMS" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ImportOldTimesheetDetails.aspx.cs" Inherits="TIMESHEET_ImportOldTimesheetDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

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

    <script type="text/javascript">
        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 85%">
            <legend style="text-align: center;">Timesheet List</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Start Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                        <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                        <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                            runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                            ToolTip="Start Date Calendar" />
                    </td>
                    <td align="right">
                        End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                        <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                        <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                            runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                            ToolTip="End Date Calendar" Width="20px" />
                    </td>
                    <td align="right">
                        JOB No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Status.:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        Name:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUser" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td align="right">
                        <table width="100%">
                            <tr>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                </td>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnImport" CssClass="button" Width="100%" runat="server" Text="Import"
                                        OnClick="btnImport_Click" />
                                </td>
                            </tr>
                        </table>
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
            <div style='overflow: scroll; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvTimesheetList" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="Vertical" PageSize="20" Width="100%" Height="100%" HorizontalAlign="Center"
                    AllowPaging="false" AutoGenerateColumns="False" OnRowDataBound="gvTimesheetList_RowDataBound"
                    OnPageIndexChanging="gvTimesheetList_PageIndexChanging">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>' />
                                <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                <asp:Label ID="lblDrawingTypeID" runat="server" Visible="false" Text='<%# Eval("DRAWING_TYPE_ID") %>' />
                                <asp:Label ID="lblUserName" runat="server" Visible="true" Text='<%# Eval("USER_NAME") %>' />
                                <asp:Label ID="lblIsImported" runat="server" Visible="false" Text='<%# Eval("IS_IMPORTED") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                        <asp:BoundField DataField="ENTRY_DATE" HeaderText="ENTRY_DATE" />
                        <asp:BoundField DataField="PROJECT_CATETORY" HeaderText="PROJECT_CATETORY" />
                        <asp:BoundField DataField="PROJECT_NUMBER" HeaderText="PROJECT_NUMBER" />
                        <asp:BoundField DataField="JOB_NUMBER" HeaderText="JOB_NUMBER" />
                        <asp:BoundField DataField="DRAWING_CATEGORY" HeaderText="DRAWING_CATEGORY" />
                        <asp:BoundField DataField="SERIAL_NUMBER" HeaderText="SERIAL_NUMBER" />
                        <asp:BoundField DataField="SIZE" HeaderText="SIZE" />
                        <asp:BoundField DataField="REV" HeaderText="REV" />
                        <asp:BoundField DataField="SHEETS" HeaderText="SHEETS" />
                        <asp:BoundField DataField="DRAWING_ID" HeaderText="DRAWING_ID" />
                        <asp:BoundField DataField="DRAWING_TYPE" HeaderText="DRAWING_TYPE" />
                        <asp:BoundField DataField="START_TIME" HeaderText="START_TIME" />
                        <asp:BoundField DataField="END_TIME" HeaderText="END_TIME" />
                        <asp:BoundField DataField="TIME_SPENT" HeaderText="TIME_SPENT" />
                        <asp:BoundField DataField="TITLE" HeaderText="TITLE" />
                        <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
