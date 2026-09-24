<%@ Page Title="GST Reconciliation  Report By GSTR2B" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PurchaseBalanceReportByGSTR2B.aspx.cs" Inherits="REPORTS_PURCHASE_BALANCE_PurchaseBalanceReportByGSTR2B" %>

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

            document.getElementById('<%=txtGSTINToS.ClientID %>').value = "";
            document.getElementById('<%=txtVendorCodeToS.ClientID %>').value = "";
            document.getElementById('<%=txtVendorNameToS.ClientID %>').value = "";
            document.getElementById('<%=txtInvoiceNoToS.ClientID %>').value = "";

            document.getElementById('<%=chkIsGSTInvoiceNoAmountMatched.ClientID %>').checked = false;
            document.getElementById('<%=chkIsGSTInvoiceDateAmountMatched.ClientID %>').checked = false;
            <%--document.getElementById('<%=chkIsGSTAmountMatched.ClientID %>').checked = false;--%>
            document.getElementById('<%=chkIsFullyUnmatched.ClientID %>').checked = false;
            document.getElementById('<%=chkIsGSTNotIn.ClientID %>').checked = false;


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
    <asp:HiddenField ID="hdConfirmValue" runat="server" Value="0" />
    <%-- <asp:UpdatePanel ID="uppanel" runat="server">
        <contenttemplate>--%>

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>GST Reconciliation  Report By GSTR2B:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
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
                                <td style="width: 30%;">
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
                                <td style="width: 70%;">
                                    <asp:DropDownList ID="ddlDateTypeToS" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Invoice Date" Value="B2B_INVOICE_DATE"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>
                        </table>
                    </div>

                    <label>Posted Year:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlPostedYearToS" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="2020-2021" Value="2020-2021" />
                                    <asp:ListItem Text="2021-2022" Value="2021-2022" />
                                    <asp:ListItem Text="2022-2023" Value="2022-2023" />
                                    <asp:ListItem Text="2023-2024" Value="2023-2024" />
                                    <asp:ListItem Text="2024-2025" Value="2024-2025" />
                                    <asp:ListItem Text="2025-2026" Value="2025-2026" />
                                    <asp:ListItem Text="2026-2027" Value="2026-2027" />
                                    <asp:ListItem Text="2027-2028" Value="2027-2028" />
                                    <asp:ListItem Text="2028-2029" Value="2028-2029" />
                                    <asp:ListItem Text="2029-2030" Value="2029-2030" />
                                    <asp:ListItem Text="2030-2031" Value="2030-2031" />
                                </asp:DropDownList>
                            </td>
                            <asp:Panel runat="server" Visible="false">
                                <td>
                                    <asp:DropDownList ID="ddlPostedMonthToS" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="All" Value="" />
                                        <asp:ListItem Text="Jan" Value="1" />
                                        <asp:ListItem Text="Feb" Value="2" />
                                        <asp:ListItem Text="Mar" Value="3" />
                                        <asp:ListItem Text="Apr" Value="4" />
                                        <asp:ListItem Text="May" Value="5" />
                                        <asp:ListItem Text="Jun" Value="6" />
                                        <asp:ListItem Text="Jul" Value="7" />
                                        <asp:ListItem Text="Aug" Value="8" />
                                        <asp:ListItem Text="Sep" Value="9" />
                                        <asp:ListItem Text="Oct" Value="10" />
                                        <asp:ListItem Text="Nov" Value="11" />
                                        <asp:ListItem Text="Dec" Value="12" />
                                    </asp:DropDownList>
                                </td>
                            </asp:Panel>
                        </tr>
                    </table>


                    <asp:Panel runat="server" Visible="false">
                        <label>Start Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
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

                        <label>End Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
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

                    </asp:Panel>

                    <label>GSTIN:</label>
                    <asp:TextBox ID="txtGSTINToS" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Vendor:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtVendorCodeToS" runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVendorNameToS" runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </table>


                    <label>Invoice No.:</label>
                    <asp:TextBox ID="txtInvoiceNoToS" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Is GST & Invoice No & Amount Matched?</label>
                    <asp:CheckBox ID="chkIsGSTInvoiceNoAmountMatched" runat="server" />

                    <label>Is GST & Invoice Date & Amount Matched?</label>
                    <asp:CheckBox ID="chkIsGSTInvoiceDateAmountMatched" runat="server" />

                    <label>Is Fully Unmatched?</label>
                    <asp:CheckBox ID="chkIsFullyUnmatched" runat="server" />

                    <label>GST Not in BOOKS</label>
                    <asp:CheckBox ID="chkIsGSTNotIn" runat="server" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export to Excel"
                    OnClick="btnExport_Click" />
            </div>


            <fieldset class="filter-card">
                <legend>Stats</legend>
                <div class="form-grid form-grid-3">

                    <label>Imported:</label>
                    <asp:TextBox ID="txtImportedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>Processed:</label>
                    <asp:TextBox ID="txtProcessedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>M1 Matched:</label>
                    <asp:TextBox ID="txtM1MatchedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>M1 Unmatched:</label>
                    <asp:TextBox ID="txtM1UnmatchedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>M2 Matched:</label>
                    <asp:TextBox ID="txtM2MatchedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>M2 Unmatched:</label>
                    <asp:TextBox ID="txtM2UnmatchedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>Fully Unmatched:</label>
                    <asp:TextBox ID="txtFullyUnmatchedCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                    <label>Not In BOOKS:</label>
                    <asp:TextBox ID="txtNotInGSTR2BCounts" runat="server" ReadOnly="true"
                        CssClass="form-control"
                        Text="0" />

                </div>

            </fieldset>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView ID="gvReport"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                CssClass="employee-grid"
                ForeColor="#333333" GridLines="Both"
                Width="100%"
                HorizontalAlign="Center"
                OnRowDataBound="gvReport_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:BoundField DataField="POSTED_YEAR" HeaderText="Posted_Year" />
                    <asp:BoundField DataField="POSTED_MONTH" HeaderText="Posted_Month" />
                    <asp:BoundField DataField="GSTR2B_GSTIN_OF_SUPPLIER" HeaderText="Gstr2B_Gstin_Of_Supplier" />
                    <asp:BoundField DataField="GSTR2B_VENDOR_CODE" HeaderText="Gstr2B_Vendor_Code" />
                    <asp:BoundField DataField="GSTR2B_VENDOR_NAME" HeaderText="Gstr2B_Vendor_Name" />
                    <asp:BoundField DataField="GSTR2B_INVOICE_NUMBER" HeaderText="Gstr2B_Invoice_Number" />
                    <asp:BoundField DataField="GSTR2B_INVOICE_DATE" HeaderText="Gstr2B_Invoice_Date" />
                    <asp:BoundField DataField="GSTR2B_TAXABLE_VALUE" HeaderText="Gstr2B_Taxable_Value" />
                    <asp:BoundField DataField="B2B_M1_GSTIN_OF_SUPPLIER" HeaderText="B2B_M1_Gstin_Of_Supplier" />
                    <asp:BoundField DataField="B2B_M1_INVOICE_NO" HeaderText="B2B_M1_Invoice_No" />
                    <asp:BoundField DataField="B2B_M1_TAXABLE_VALUE" HeaderText="B2B_M1_Taxable_Value" />
                    <asp:BoundField DataField="B2B_M1_GST_INVOICE_NO_AMOUNT_MATCHED" HeaderText="B2B_M1_Gst_Invoice_No_Amount_Matched" />
                    <asp:BoundField DataField="B2B_M2_GSTIN_OF_SUPPLIER" HeaderText="B2B_M2_Gstin_Of_Supplier" />
                    <asp:BoundField DataField="B2B_M2_INVOICE_DATE" HeaderText="B2B_M2_Invoice_Date" />
                    <asp:BoundField DataField="B2B_M2_TAXABLE_VALUE" HeaderText="B2B_M2_Taxable_Value" />
                    <asp:BoundField DataField="B2B_M2_GST_INVOICE_DATE_AMOUNT_MATCHED" HeaderText="B2B_M2_Gst_Invoice_Date_Amount_Matched" />
                    <asp:BoundField DataField="FULLY_UNMATCHED_GSTIN" HeaderText="Fully_Unmatched_Gstin" />
                    <asp:BoundField DataField="GSTIN_NOT_IN_GSTR2B" HeaderText="Gstin_Not_In_Gstr2B" />

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


    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
