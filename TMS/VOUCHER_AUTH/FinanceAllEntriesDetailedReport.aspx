<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="FinanceAllEntriesDetailedReport.aspx.cs" Inherits="VOUCHER_AUTH_FinanceAllEntriesDetailedReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../Images/Icons/Icon04.png" />

    <script src="../../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
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

    <script type="text/javascript">
        function ClearAllFilters() {

            document.getElementById('<%=ddlDateSignToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlDateTypeToS.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=ddlUnitToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlStatusToS.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=ddlCreatedByToS.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=ddlMonthToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtVoucherNoToS.ClientID %>').value = "";

            document.getElementById('<%=ddlVoucherTypeToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtPartyNameToS.ClientID %>').value = "";
            document.getElementById('<%=txtGLCodeToS.ClientID %>').value = "";
            document.getElementById('<%=txtGLDescriptionToS.ClientID %>').value = "";

            document.getElementById('<%=txtDOCClassToS.ClientID %>').value = "";

            document.getElementById('<%=ddlAmountSign.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=txtAmountOneS.ClientID %>').value = "";
            document.getElementById('<%=txtAmountTwoS.ClientID %>').value = "";

            return false;
        }

    </script>

    <script type="text/javascript">

        function EnableEndDateSearch() {
            var sign = document.getElementById('<%=ddlDateSignToS.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 0) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = false;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = false;

                if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }
        }



        function EnableAmount2() {
            var sign = document.getElementById('<%=ddlAmountSign.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtAmountOneS.ClientID %>').value = "";
            document.getElementById('<%=txtAmountTwoS.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 5) {
                document.getElementById('<%=txtAmountTwoS.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtAmountTwoS.ClientID %>').disabled = true;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" Value="0" />
    <%-- <asp:UpdatePanel ID="uppanel" runat="server">
        <contenttemplate>--%>

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Finance All Entries Detailed Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                    <asp:ImageButton ID="imgBtnExportToExcel" runat="server"
                        ImageUrl="~/Images/excel.png"
                        Width="30px" Height="30px"
                        OnClick="imgBtnExportToExcel_Click"
                        ToolTip="Export to CSV" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Select/Unselect Dates:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 45%;">
                                <asp:CheckBox ID="chkSelectDates" runat="server"
                                    Checked="true"
                                    onchange="EnableDisableDates()" Enabled="False" />

                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 45%;" align="right">
                                <asp:ImageButton ID="imgBtnClearAllFilters" runat="server"
                                    ImageUrl="~/Images/NEWICONS/clear1.png"
                                    Width="40px" Height="40px"
                                    ToolTip="Clear Filters"
                                    OnClientClick="return ClearAllFilters();" />
                            </td>
                        </tr>
                    </table>

                    <div class="full-width">

                        <label>Date Filter:</label>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:DropDownList ID="ddlDateSignToS" runat="server"
                                        CssClass="form-control"
                                        onchange="EnableEndDateSearch()">
                                        <asp:ListItem Text="Between" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 75%;">
                                    <asp:DropDownList ID="ddlDateTypeToS" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Voucher Date" Value="VOUCHER_DATE"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>
                        </table>
                    </div>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
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
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Voucher No.:</label>
                    <asp:TextBox ID="txtVoucherNoToS" runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                    <label>Month:</label>
                    <asp:DropDownList ID="ddlMonthToS" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">ALL</asp:ListItem>
                        <asp:ListItem Value="1">JAN</asp:ListItem>
                        <asp:ListItem Value="2">FEB</asp:ListItem>
                        <asp:ListItem Value="3">MAR</asp:ListItem>
                        <asp:ListItem Value="4">APR</asp:ListItem>
                        <asp:ListItem Value="5">MAY</asp:ListItem>
                        <asp:ListItem Value="6">JUN</asp:ListItem>
                        <asp:ListItem Value="7">JUL</asp:ListItem>
                        <asp:ListItem Value="8">AUG</asp:ListItem>
                        <asp:ListItem Value="9">SEP</asp:ListItem>
                        <asp:ListItem Value="10">OCT</asp:ListItem>
                        <asp:ListItem Value="11">NOV</asp:ListItem>
                        <asp:ListItem Value="12">DEC</asp:ListItem>
                    </asp:DropDownList>


                    <label>Voucher Type:</label>
                    <asp:DropDownList ID="ddlVoucherTypeToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Party Name:</label>
                    <asp:TextBox ID="txtPartyNameToS" runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                    <div class="full-width">
                        <label>GL Code:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtGLCodeToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                                <td style="width: 80%;">
                                    <asp:TextBox ID="txtGLDescriptionToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>


                    <label>DOC Class:</label>
                    <asp:TextBox ID="txtDOCClassToS" runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                    <div class="full-width">
                        <label>Product Code:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtProductCodeToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                                <td style="width: 80%;">
                                    <asp:TextBox ID="txtProductDescriptionToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatusToS" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnitToS" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Created By:</label>
                    <asp:DropDownList ID="ddlCreatedByToS" runat="server" CssClass="form-control">
                    </asp:DropDownList>


                    <div class="full-width">
                        <label>Amount:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <asp:DropDownList ID="ddlAmountSign" runat="server"
                                        CssClass="form-control"
                                        onchange="EnableAmount2()">
                                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                                        <asp:ListItem Text="BETWEEN" Value="BETWEEN"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 35%">
                                    <asp:TextBox ID="txtAmountOneS" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 35%">
                                    <asp:TextBox ID="txtAmountTwoS" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClick="btnSearch_Click" />
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
                ID="gvVouchersList"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvVouchersList_RowCommand"
                OnRowDataBound="gvVouchersList_RowDataBound">

                <rowstyle backcolor="#E3EAEB" horizontalalign="Left" />
                <rowstyle backcolor="#E3EAEB" horizontalalign="Left" />
                <footerstyle backcolor="#1C5E55" forecolor="White" font-bold="True" />
                <pagerstyle backcolor="#666666" forecolor="White" horizontalalign="Center" />
                <selectedrowstyle backcolor="#C5BBAF" font-bold="True" forecolor="#333333" />
                <headerstyle backcolor="#1C5E55" font-bold="True" forecolor="White" />
                <editrowstyle backcolor="#7C6F57" />
                <alternatingrowstyle backcolor="White" />
                <columns>

                    <asp:BoundField DataField="SR_NO" HeaderText="Sl No." />

                    <asp:TemplateField HeaderText="Zoom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <itemtemplate>

                            <asp:ImageButton ID="btnViewVoucherDetail" Height="30px" Width="30px"
                                CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/viewdetails.png"
                                ToolTip="View Details" />


                            <asp:Label ID="lblVoucherId" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VOUCHER_NO") %>' Visible="false" />
                            <asp:Label ID="lblVoucherDate" runat="server" Text='<%# Eval("VOUCHER_DATE") %>' Visible="false" />
                            <asp:Label ID="lblChallanNo" runat="server" Text='<%# Eval("CHALLAN_NO") %>' Visible="false" />
                            <asp:Label ID="lblGLCode" runat="server" Text='<%# Eval("GL_CODE") %>' Visible="false" />
                            <asp:Label ID="lblGLDescription" runat="server" Text='<%# Eval("GL_DESCRIPTION") %>' Visible="false" />

                            <asp:Label ID="lblPartyCode" runat="server" Text='<%# Eval("PARTY_CODE") %>' Visible="false" />
                            <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("PARTY_NAME") %>' Visible="false" />
                            <asp:Label ID="lblCreditDebitType" runat="server" Text='<%# Eval("CREDIT_DEBIT_TYPE") %>' Visible="false" />
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' Visible="false" />
                            <asp:Label ID="lblDOCClass" runat="server" Text='<%# Eval("DOC_CLASS") %>' Visible="false" />

                            <asp:Label ID="lblVoucherType" runat="server" Text='<%# Eval("VOUCHER_TYPE") %>' Visible="false" />
                            <asp:Label ID="lblVoucherTypeId" runat="server" Text='<%# Eval("TYPE_ID") %>' Visible="false" />

                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                            <asp:Label ID="lblProductDescription" runat="server" Text='<%# Eval("PRODUCT_DESCRIPTION") %>' Visible="false" />

                            <asp:Label ID="lblVoucherCreatedBy" runat="server" Text='<%# Eval("VOUCHER_CREATED_BY") %>' Visible="false" />
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' Visible="false" />
                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UNIT_NAME") %>' Visible="false" />

                            <asp:Label ID="lblAuthorizedBy" runat="server" Text='<%# Eval("AUTHORIZED_BY") %>' Visible="false" />
                            <asp:Label ID="lblAuthorizedOn" runat="server" Text='<%# Eval("AUTHORIZED_ON") %>' Visible="false" />
                            <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("APPROVED_BY") %>' Visible="false" />
                            <asp:Label ID="lblApprovedOn" runat="server" Text='<%# Eval("APPROVED_ON") %>' Visible="false" />

                            <asp:Label ID="lblSRPNo" runat="server" Text='<%# Eval("SRP_FLAG") %>' Visible="false" />

                        </itemtemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        HeaderText="Download"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <itemtemplate>
                            <asp:ImageButton ID="imgBtnDownloadAllDocuments"
                                CommandArgument="DOWNLOAD_ALL_DOCUMENTS"
                                runat="server"
                                ImageUrl="~/Images/Download/download3.png"
                                Height="35px"
                                Width="35px"
                                ToolTip="Download All Documents" />
                        </itemtemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                    <asp:BoundField DataField="VOUCHER_DATE" HeaderText="Voucher Date" />
                    <asp:BoundField DataField="GL_CODE" HeaderText="GL Code" />
                    <asp:BoundField DataField="GL_DESCRIPTION" HeaderText="GL Description" />

                    <asp:BoundField DataField="PARTY_CODE" HeaderText="Party code" />
                    <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />

                    <asp:BoundField DataField="CREDIT_DEBIT_TYPE" HeaderText="Credit/Debit Type" />
                    <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                    <asp:BoundField DataField="DOC_CLASS" HeaderText="DOC Class" />

                    <asp:BoundField DataField="VOUCHER_MONTH" HeaderText="Voucher Month" />
                    <asp:BoundField DataField="VOUCHER_TYPE" HeaderText="Voucher Type" />
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                    <asp:BoundField DataField="PRODUCT_DESCRIPTION" HeaderText="Product Description" />
                    <asp:BoundField DataField="VOUCHER_CREATED_BY" HeaderText="Voucher Created By" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />

                    <asp:BoundField DataField="AUTHORIZED_BY" HeaderText="Authorized By" />
                    <asp:BoundField DataField="AUTHORIZED_ON" HeaderText="Authorized oN" />
                    <asp:BoundField DataField="APPROVED_BY" HeaderText="Approved By" />
                    <asp:BoundField DataField="APPROVED_ON" HeaderText="Approved On" />
                </columns>
                <footerstyle backcolor="#1C5E55" forecolor="White" font-bold="True" />
                <pagerstyle backcolor="#666666" forecolor="White" horizontalalign="Center" />
                <selectedrowstyle backcolor="#C5BBAF" font-bold="True" forecolor="#333333" />
                <headerstyle backcolor="#1C5E55" font-bold="True" forecolor="White" />
                <editrowstyle backcolor="#7C6F57" />
                <alternatingrowstyle backcolor="White" />
            </asp:GridView>

        </div>

    </div>



    <%-- SHOW & AUTHORIZE VOUCHER START--%>
    <asp:Button ID="btnShowAuthorizeVoucher" runat="server" Style="display: none" />
    <asp:ModalPopupExtender
        ID="mpeAuthorizeVoucher"
        runat="server"
        TargetControlID="btnShowAuthorizeVoucher"
        BehaviorID="mpeAuthorizeVoucherBID"
        PopupControlID="pnlViewAuthorizeVoucherPopup"
        CancelControlID="imgBtnCancelAuthorizeVoucher"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewAuthorizeVoucherPopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAuthorizeVoucher" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblAuthorizeVoucherDetailsLegend" runat="server" />
                        [Voucher Details]
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Voucher No.:</label>
                        <asp:TextBox ID="txtVoucherNoToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Voucher Date:</label>
                        <asp:TextBox ID="txtVoucherDateToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Voucher Type:</label>
                        <asp:TextBox ID="txtVoucherTypeToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Party Code:</label>
                        <asp:TextBox ID="txtPartyCodeToAS" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Party Name:</label>
                        <asp:TextBox ID="txtPartyNameToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Unit:</label>
                        <asp:TextBox ID="txtUnitToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>GL Code:</label>
                        <table cssclass="form-control">
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtGLCodeToAS" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGLDescriptionToAS" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                            </tr>
                        </table>

                        <label>Status:</label>
                        <asp:TextBox ID="txtStatusToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Product Code:</label>
                        <table cssclass="form-control">
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtProductCodeToAS" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProductDescriptionToAS" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                            </tr>
                        </table>

                        <label>Amount:</label>
                        <asp:TextBox ID="txtAmountToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Voucher Created By:</label>
                        <asp:TextBox ID="txtVoucherCreatedByToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Authorized By:</label>
                        <asp:TextBox ID="txtAuthorizedByToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Authorized On:</label>
                        <asp:TextBox ID="txtAuthorizedOnToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Approved By:</label>
                        <asp:TextBox ID="txtApprovedByToAS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Approved On:</label>
                        <asp:TextBox ID="txtApprovedOnToAS" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">
                <table width="100%" runat="server">
                    <tr>
                        <td id="tdVoucherFiles" style="width: 45%;">
                            <fieldset style="width: 97%; margin-left: 1%; margin-top: 10px;">
                                <legend style="text-align: center;">
                                    <asp:Label ID="lblAttachedFilesLegend" runat="server" />
                                    &nbsp; Files
                                </legend>
                                <div id="divAttachedFiles" runat="server"
                                    style='overflow-y: auto; overflow-x: auto; width: 100%; height: 200px; border: 1px solid lightgray;'
                                    onscroll="SetDivPosition()">

                                    <asp:GridView
                                        CssClass="popup-grid"
                                        ID="gvAttachedFiles"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        CellPadding="4"
                                        ForeColor="#333333" GridLines="Both"
                                        Width="100%"
                                        HorizontalAlign="Center"
                                        OnRowDataBound="gvAttachedFiles_RowDataBound"
                                        OnRowCommand="gvAttachedFiles_RowCommand">
                                        <rowstyle backcolor="#E3EAEB" horizontalalign="Left" />

                                        <columns>

                                            <asp:TemplateField HeaderText="Sl No."
                                                HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <itemtemplate>
                                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                                                    <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />

                                                    <asp:Label ID="lblAttachedFileName" runat="server" Visible="false" Text='<%# Eval("FILE_NAME") %>' />

                                                </itemtemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">

                                                <itemtemplate>
                                                    <asp:ImageButton ID="imgBtnAttachment1"
                                                        Height="20px"
                                                        Width="20px"
                                                        CommandArgument="ViewATTACHMENT1"
                                                        runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
                                            <asp:BoundField DataField="ATTACHED_BY" HeaderText="Attached By" />
                                            <asp:BoundField DataField="ATTACHED_ON" HeaderText="Attached On" />

                                        </columns>


                                        <footerstyle backcolor="#1C5E55" forecolor="White" font-bold="True" />
                                        <pagerstyle backcolor="#666666" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#C5BBAF" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#1C5E55" font-bold="True" forecolor="White" />
                                        <editrowstyle backcolor="#7C6F57" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>

                                </div>
                            </fieldset>
                        </td>
                        <td id="tdOtherVoucherFiles" style="width: 45%;">
                            <fieldset style="width: 97%; margin-left: 1%; margin-top: 10px;">
                                <legend style="text-align: center;">
                                    <asp:Label ID="lblAttachedOtherFilesLegend" runat="server" />
                                    &nbsp; Files
                                </legend>
                                <div id="divAttachedOtherFiles" runat="server"
                                    style='overflow-y: auto; overflow-x: auto; width: 100%; height: 200px; border: 1px solid lightgray;'
                                    onscroll="SetDivPosition()">

                                    <asp:GridView
                                        CssClass="popup-grid"
                                        ID="gvAttachedOtherFiles"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        CellPadding="4"
                                        ForeColor="#333333" GridLines="Both"
                                        Width="100%"
                                        HorizontalAlign="Center"
                                        OnRowDataBound="gvAttachedOtherFiles_RowDataBound"
                                        OnRowCommand="gvAttachedOtherFiles_RowCommand">
                                        <rowstyle backcolor="#E3EAEB" horizontalalign="Left" />

                                        <columns>

                                            <asp:TemplateField HeaderText="Sl No."
                                                HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <itemtemplate>
                                                    <asp:Label ID="lblSlNoO" runat="server" Text='<%# Eval("SR_NO") %>' />
                                                    <asp:Label ID="lblPIDO" runat="server" Text='<%# Eval("PID") %>' Visible="false" />

                                                    <asp:Label ID="lblAttachedFileNameO" runat="server" Visible="false" Text='<%# Eval("FILE_NAME") %>' />

                                                </itemtemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField
                                                HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">

                                                <itemtemplate>
                                                    <asp:ImageButton ID="imgBtnAttachment1O"
                                                        Height="20px"
                                                        Width="20px"
                                                        CommandArgument="ViewATTACHMENT1O"
                                                        runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
                                            <asp:BoundField DataField="ATTACHED_BY" HeaderText="Attached By" />
                                            <asp:BoundField DataField="ATTACHED_ON" HeaderText="Attached On" />

                                        </columns>


                                        <footerstyle backcolor="#1C5E55" forecolor="White" font-bold="True" />
                                        <pagerstyle backcolor="#666666" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#C5BBAF" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#1C5E55" font-bold="True" forecolor="White" />
                                        <editrowstyle backcolor="#7C6F57" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>

                                </div>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>

        </div>

    </asp:Panel>
    <%-- SHOW & AUTHORIZE VOUCHER END--%>



    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender
        ID="ModalPopupExtender2"
        runat="server"
        TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup"
        CancelControlID="imgBtnCancelImgFile"
        BackgroundCssClass="modalBackground">
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
        <div class="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>



    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender
        ID="ModalPopupExtender3"
        runat="server"
        TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup"
        CancelControlID="imgBtnCancelPDFFile"
        BackgroundCssClass="modalBackground">
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
            class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server"></iframe>
    </asp:Panel>



    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
