<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="FactLogReportDeptWise.aspx.cs"
    Inherits="REPORTS_FACT_LOG_FactLogReportDeptWise" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
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

        function ValidateAll() {

            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">FACT Log Report </legend>
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
                        Department:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%" Height="26px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="ALL" Value="0" />
                            <asp:ListItem Text="FINANCE" Value="1" />
                            <asp:ListItem Text="PURCHASE" Value="2" />
                            <asp:ListItem Text="STORE" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <td>
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
    <div align="center" style="margin-top: 20px;">
        <table width="100%">
            <tr>
                <td>
                    &nbsp
                </td>
                <td>
                    <table width="100%" border="1" style="background-color: #1C5E55;">
                        <tr style="color: White;">
                            <th style="width: 10%">
                                From:
                            </th>
                            <th>
                                <asp:Label ID="lblFromDate" runat="server" />
                            </th>
                            <th style="width: 10%">
                                To:
                            </th>
                            <th>
                                <asp:Label ID="lblToDate" runat="server" />
                            </th>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td align="center" width="33%">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">[Finance] [<asp:Label ID="lblFinanceLogReportRecords"
                            runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 250px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvFinanceLogReport" runat="server" CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvFinanceLogReport_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="USER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Eval("FUSER") %>' />
                                            <asp:Label ID="lblPI" runat="server" Visible="false" Text='<%# Eval("PI") %>' />
                                            <asp:Label ID="lblSI" runat="server" Visible="false" Text='<%# Eval("SI") %>' />
                                            <asp:Label ID="lblJV" runat="server" Visible="false" Text='<%# Eval("JV") %>' />
                                            <asp:Label ID="lblDBNote" runat="server" Visible="false" Text='<%# Eval("DB_NOTE") %>' />
                                            <asp:Label ID="lblCRNote" runat="server" Visible="false" Text='<%# Eval("CR_NOTE") %>' />
                                            <asp:Label ID="lblDBAdj" runat="server" Visible="false" Text='<%# Eval("DB_ADJ") %>' />
                                            <asp:Label ID="lblCRAdj" runat="server" Visible="false" Text='<%# Eval("CR_ADJ") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PI" DataField="PI" />
                                    <asp:BoundField HeaderText="SI" DataField="SI" />
                                    <asp:BoundField HeaderText="JV" DataField="JV" />
                                    <asp:BoundField HeaderText="DB_NOTE" DataField="DB_NOTE" />
                                    <asp:BoundField HeaderText="CR_NOTE" DataField="CR_NOTE" />
                                    <asp:BoundField HeaderText="DB_ADJ" DataField="DB_ADJ" />
                                    <asp:BoundField HeaderText="CR_ADJ" DataField="CR_ADJ" />
                                </Columns>
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <br />
                        </div>
                        <asp:Button ID="btnSendFinanceLogReportMail" CssClass="button" Width="100%" runat="server"
                            Text="Send Mail" OnClick="btnSendFinanceLogReportMail_Click" />
                    </fieldset>
                </td>
                <td align="center" width="33%">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">[Purchase] [<asp:Label ID="lblPurchaseLogReportRecords"
                            runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 250px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvPurchaseLogReport" runat="server" CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvPurchaseLogReport_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="USER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Eval("FUSER") %>' />
                                            <asp:Label ID="lblPO" runat="server" Visible="false" Text='<%# Eval("PO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PO" DataField="PO" />
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
                        <asp:Button ID="btnSendPurchaseLogReportMail" CssClass="button" Width="100%" runat="server"
                            Text="Send Mail" OnClick="btnSendPurchaseLogReportMail_Click" />
                    </fieldset>
                </td>
                <td align="center" width="33%">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">[Store] [<asp:Label ID="lblStoreLogReportRecords"
                            runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 250px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvStoreLogReport" runat="server" CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvStoreLogReport_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="USER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# Eval("FUSER") %>' />
                                            <asp:Label ID="lblMRN" runat="server" Visible="false" Text='<%# Eval("MRN") %>' />
                                            <asp:Label ID="lblIssue" runat="server" Visible="false" Text='<%# Eval("ISS") %>' />
                                            <asp:Label ID="lblFGReceived" runat="server" Visible="false" Text='<%# Eval("FG") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="MRN" DataField="MRN" />
                                    <asp:BoundField HeaderText="ISS" DataField="ISS" />
                                    <asp:BoundField HeaderText="FG" DataField="FG" />
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
                        <asp:Button ID="btnSendStoreLogReportMail" CssClass="button" Width="100%" runat="server"
                            Text="Send Mail" OnClick="btnSendStoreLogReportMail_Click" />
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
