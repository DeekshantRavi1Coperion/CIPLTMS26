<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TravelStatementReport.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TravelStatementReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Travel Statement Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate"
                                    runat="server"
                                    ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDate" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate" runat="server"
                                    TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox
                                    ID="txtEndDate"
                                    runat="server"
                                    ReadOnly="true"
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

                    <label>Sanction No</label>
                    <asp:TextBox ID="txtSanctionNo" runat="server" CssClass="form-control" />

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Employee Name</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    &nbsp;
                    &nbsp;
                 <asp:Button ID="btnSearch"
                     OnClick="btnSearch_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                    &nbsp;
                    &nbsp;
                 <asp:Button ID="btnExport"
                     OnClick="btnExport_Click"
                     runat="server"
                     Text="Export"
                     CssClass="button" />

                </div>
            </fieldset>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView ID="gvTourList"
                CssClass="employee-grid"
                runat="server"
                AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" PageSize="18" Width="100%" HorizontalAlign="Center"
                AllowPaging="True" OnPageIndexChanging="gvTourList_PageIndexChanging" OnRowDataBound="gvTourList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="TOUR_SANCTION_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                            <asp:Label ID="lblTourSanctionNo" runat="server" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS" />
                    <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                    <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                    <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                    <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                    <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                    <asp:BoundField DataField="VISIT_TYPE" HeaderText="VISIT_TYPE" />

                    <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />

                    <asp:BoundField DataField="Booked_By_Reception-Air_Ticket" HeaderText="Booked_By_Reception-Air_Ticket" />
                    <asp:BoundField DataField="Booked_By_Reception-Hotel" HeaderText="Booked_By_Reception-Hotel" />
                    <asp:BoundField DataField="Booked_By_Reception-Taxi" HeaderText="Booked_By_Reception-Taxi" />
                    <asp:BoundField DataField="Booked_By_Reception-Others" HeaderText="Booked_By_Reception-Others" />



                    <asp:BoundField DataField="AIRFARE_AMT" HeaderText="AIRFARE_AMT" />
                    <asp:BoundField DataField="TELEPHONE_MOBILE_AMT" HeaderText="TELEPHONE_MOBILE_AMT" />
                    <asp:BoundField DataField="LODGING_AMT" HeaderText="LODGING_AMT" />
                    <asp:BoundField DataField="TIPS_AMT" HeaderText="TIPS_AMT" />
                    <asp:BoundField DataField="MEALS_AMT" HeaderText="MEALS_AMT" />
                    <asp:BoundField DataField="VISAFEE_AMT" HeaderText="VISAFEE_AMT" />
                    <asp:BoundField DataField="GROUND_TRANSPORT_AMT" HeaderText="GROUND_TRANSPORT_AMT" />
                    <asp:BoundField DataField="DAILY_ALLOWANCE_AMT" HeaderText="DAILY_ALLOWANCE_AMT" />
                    <asp:BoundField DataField="ENTERTAINMENT_AMT" HeaderText="ENTERTAINMENT_AMT" />
                    <asp:BoundField DataField="OTHER_AMT" HeaderText="OTHER_AMT" />

                    <asp:BoundField DataField="GIFTS_AMT" HeaderText="GIFTS_AMT" />
                    <asp:BoundField DataField="Total_Expense" HeaderText="Total_Expense" />

                    <%--<asp:BoundField DataField="TOTAL_AMT" HeaderText="TOTAL_AMT" />--%>
                    <asp:BoundField DataField="TOTAL_CURRENCY" HeaderText="TOTAL_CURRENCY" />
                    <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_AMT" />
                    <asp:BoundField DataField="ADV_CURRENCY" HeaderText="ADV_CURRENCY" />
                    <asp:BoundField DataField="ADJUSTED_AMT" HeaderText="ADJUSTED_AMT" />
                    <asp:BoundField DataField="ADJUSTED_AMT_CURRENCY" HeaderText="ADJUSTED_AMT_CURRENCY" />
                    <asp:BoundField DataField="IS_COST_RECOVERABLE" HeaderText="IS_COST_RECOVERABLE" />
                    <asp:BoundField DataField="AMOUNT_DATED" HeaderText="AMOUNT_DATED" />
                    <asp:BoundField DataField="FINAL_AMT" HeaderText="FINAL_AMT" />
                    <asp:BoundField DataField="AMOUNT_DATED" HeaderText="AMOUNT_DATED" />
                    <asp:BoundField DataField="FINAL_AMT_CURRENCY" HeaderText="FINAL_AMT_CURRENCY" />
                    <asp:BoundField DataField="IS_COST_RECOVERABLE" HeaderText="IS_COST_RECOVERABLE" />

                    <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY"></asp:BoundField>
                    <asp:BoundField DataField="CREATED_ON" HeaderText="CREATED_ON"></asp:BoundField>
                    <asp:BoundField DataField="CREATED_REMARKS" HeaderText="CREATED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="APPROVED_BY" HeaderText="APPROVED_BY"></asp:BoundField>
                    <asp:BoundField DataField="APPROVED_ON" HeaderText="APPROVED_ON"></asp:BoundField>
                    <asp:BoundField DataField="APPROVED_REMARKS" HeaderText="APPROVED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="CHECKED_BY" HeaderText="CHECKED_BY"></asp:BoundField>
                    <asp:BoundField DataField="CHECKED_ON" HeaderText="CHECKED_ON"></asp:BoundField>
                    <asp:BoundField DataField="CHECKED_REMARKS" HeaderText="CHECKED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="PASSED_BY" HeaderText="PASSED_BY"></asp:BoundField>
                    <asp:BoundField DataField="PASSED_ON" HeaderText="PASSED_ON"></asp:BoundField>
                    <asp:BoundField DataField="PASSED_REMARKS" HeaderText="PASSED_REMARKS"></asp:BoundField>



                    <asp:BoundField DataField="AMENDMENT_BY" HeaderText="AMENDMENT_BY"></asp:BoundField>
                    <asp:BoundField DataField="AMENDMENT_ON" HeaderText="AMENDMENT_ON"></asp:BoundField>
                    <asp:BoundField DataField="AMENDMENT_REMARKS" HeaderText="AMENDMENT_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="AMENDED_BY" HeaderText="AMENDED_BY"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_ON" HeaderText="AMENDED_ON"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_REMARKS" HeaderText="AMENDED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="AMENDED_APPROVED_BY" HeaderText="AMENDED_APPROVED_BY"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_APPROVED_ON" HeaderText="AMENDED_APPROVED_ON"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_APPROVED_REMARKS" HeaderText="AMENDED_APPROVED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="AMENDED_CHECKED_BY" HeaderText="AMENDED_CHECKED_BY"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_CHECKED_ON" HeaderText="AMENDED_CHECKED_ON"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_CHECKED_REMARKS" HeaderText="AMENDED_CHECKED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="AMENDED_PASSED_BY" HeaderText="AMENDED_PASSED_BY"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_PASSED_ON" HeaderText="AMENDED_PASSED_ON"></asp:BoundField>
                    <asp:BoundField DataField="AMENDED_PASSED_REMARKS" HeaderText="AMENDED_PASSED_REMARKS"></asp:BoundField>


                    <asp:BoundField DataField="SETTLED_BY" HeaderText="SETTLED_BY"></asp:BoundField>
                    <asp:BoundField DataField="SETTLED_ON" HeaderText="SETTLED_ON"></asp:BoundField>
                    <asp:BoundField DataField="SETTLED_REMARKS" HeaderText="SETTLED_REMARKS"></asp:BoundField>

                    <asp:BoundField DataField="CANCELLED_BY" HeaderText="CANCELLED_BY"></asp:BoundField>
                    <asp:BoundField DataField="CANCELLED_ON" HeaderText="CANCELLED_ON"></asp:BoundField>
                    <asp:BoundField DataField="CANCELLED_REMARKS" HeaderText="CANCELLED_REMARKS"></asp:BoundField>
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

</asp:Content>
