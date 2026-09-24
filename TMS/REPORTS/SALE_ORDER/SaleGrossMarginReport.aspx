<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="SaleGrossMarginReport.aspx.cs" Inherits="REPORTS_SALE_ORDER_SaleGrossMarginReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:toolkitscriptmanager id="ScriptManager2" runat="server">
    </asp:toolkitscriptmanager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Sale Gross Margin Report</legend>
            <table width="100%">
                <tr>
                    <td align="right">Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:textbox id="txtStartDateSearch" runat="server" onkeydown="javascript:preventInput(event);"
                                        width="100%" />
                                    <asp:hiddenfield id="hdStartDateSearch" runat="server" />
                                    <asp:calendarextender id="calendarStartDateSearch" popupbuttonid="imgbtnStartDateSearch"
                                        runat="server" targetcontrolid="txtStartDateSearch" format="dd-MMM-yyyy" onclientdateselectionchanged="clientChangedSearch">
                                    </asp:calendarextender>
                                </td>
                                <td align="right">
                                    <asp:imagebutton id="imgbtnStartDateSearch" runat="server" imageurl="~/Images/Calendar2.png"
                                        tooltip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:textbox id="txtEndDateSearch" runat="server" onkeydown="javascript:preventInput(event);"
                                        width="100%" />
                                    <asp:hiddenfield id="hdEndDateSearch" runat="server" />
                                    <asp:calendarextender id="calendarEndDateSearch" popupbuttonid="imgbtnEndDateSearch"
                                        runat="server" targetcontrolid="txtEndDateSearch" format="dd-MMM-yyyy" onclientdateselectionchanged="clientChangedSearch">
                                    </asp:calendarextender>
                                </td>
                                <td align="right">
                                    <asp:imagebutton id="imgbtnEndDateSearch" runat="server" imageurl="~/Images/Calendar2.png"
                                        tooltip="End Date Calendar" width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">Company:
                    </td>
                    <td>
                        <asp:dropdownlist id="ddlCompany" runat="server" width="100%" height="26px" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">
                        <asp:button id="btnSearch" cssclass="button" width="100%" runat="server" text="Search"
                            onclientclick="return ValidateAll();" onclick="btnSearch_Click" on />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:button id="btnExport" cssclass="button" width="100%" runat="server" text="Export"
                            onclick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <div align="center">
        <asp:panel id="pnlMsg" visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:panel>
    </div>
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:label id="lblRecords" runat="server" text="Records[0]" />
            </legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:gridview id="gvSaleMargin" runat="server" autogeneratecolumns="False" cellpadding="4"
                    forecolor="#333333" gridlines="Vertical" width="100%" horizontalalign="Center"
                    onrowdatabound="gvSaleMargin_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="OA_NO" HeaderText="OA_NO" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="WARRANTY" HeaderText="WARRANTY" />
                        <asp:BoundField DataField="MO" HeaderText="MO" />
                        <asp:BoundField DataField="BILL_DATE" HeaderText="BILL_DATE" />
                        <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
                        <asp:BoundField DataField="CURR_DESC" HeaderText="CURR_DESC" />
                        <asp:BoundField DataField="RATE" HeaderText="RATE" />
                        <asp:BoundField DataField="F_CURRENCY" HeaderText="F_CURRENCY" />
                        <asp:BoundField DataField="AMOUNT_INR" HeaderText="AMOUNT_INR" />
                        <asp:BoundField DataField="BILL_NO" HeaderText="BILL_NO" />
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="GM_PERCENTAGE" HeaderText="GM_PERCENTAGE" />
                        <asp:BoundField DataField="PERCENTAGE" HeaderText="PERCENTAGE" />
                        <asp:BoundField DataField="COST_AS_PER_ORDER" HeaderText="COST_AS_PER_ORDER" />
                        <asp:BoundField DataField="MARGIN_AS_PER_ORDER" HeaderText="MARGIN_AS_PER_ORDER" />                        
                        <asp:BoundField DataField="POC_NONPOC" HeaderText="POC/NONPOC" />



                        <%--<asp:BoundField DataField="MONTH" HeaderText="MONTH" />
                        <asp:BoundField DataField="CLASS1" HeaderText="CLASS1" />
                        <asp:BoundField DataField="TYPE" HeaderText="TYPE" />
                        <asp:BoundField DataField="DOC" HeaderText="DOC" />
                        <asp:BoundField DataField="PERCENTAGE" HeaderText="PERCENTAGE" />
                        <asp:BoundField DataField="PERCENTAGE_AMOUNT" HeaderText="PERCENTAGE_AMOUNT" />
                        <asp:BoundField DataField="GLCODE" HeaderText="GLCODE" />
                        <asp:BoundField DataField="GROSS_MARGIN" HeaderText="GROSS_MARGIN" />
                        <asp:BoundField DataField="GROSS_MARGIN_VALUE" HeaderText="GROSS_MARGIN_VALUE" />
                        <asp:BoundField DataField="END_MARKET" HeaderText="END_MARKET" />
                        <asp:BoundField DataField="GEOGROPHY" HeaderText="GEOGROPHY" />--%>

                    </Columns>
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:gridview>
            </div>
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
