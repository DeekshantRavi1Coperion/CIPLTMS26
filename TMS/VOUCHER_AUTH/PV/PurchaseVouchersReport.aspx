<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PurchaseVouchersReport.aspx.cs" Inherits="VOUCHER_AUTH_PV_PurchaseVouchersReport" %>

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

            document.getElementById('<%=ddlUnitToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlStatusToS.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=ddlCreatedByToS.ClientID %>').selectedIndex = 0;

            document.getElementById('<%=ddlSearchByToS.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtSearchTextToS.ClientID %>').value = "";

            document.getElementById('<%=txtVendorCodeToS.ClientID %>').value = "";
            document.getElementById('<%=txtVendorNameToS.ClientID %>').value = "";

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
                <legend>Purchase Vouchers Report:
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
                                <td style="width: 70%;">
                                    <asp:DropDownList ID="ddlDateTypeToS" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Voucher Date" Value="VOUCHER_DATE"></asp:ListItem>
                                        <asp:ListItem Text="Purchase Order Date" Value="PURCHASE_ORDER_DATE"></asp:ListItem>
                                        <asp:ListItem Text="Challan Date" Value="CHALLAN_DATE"></asp:ListItem>
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
                                    CssClass="form-control"></asp:TextBox>
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

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatusToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnitToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Created By:</label>
                    <asp:DropDownList ID="ddlCreatedByToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Search By:</label>
                    <asp:DropDownList ID="ddlSearchByToS" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Voucher No." Value="VOUCHER_NO"></asp:ListItem>
                        <asp:ListItem Text="Purchase Order No." Value="PURCHASE_ORDER_NO"></asp:ListItem>
                        <asp:ListItem Text="Challan No." Value="CHALLAN_NO"></asp:ListItem>
                    </asp:DropDownList>

                    <div class="full-width">
                        <label>Search Text:</label>
                        <asp:TextBox ID="txtSearchTextToS" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="4">
                        </asp:TextBox>
                    </div>

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCodeToS" runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorNameToS" runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

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
                                        onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 35%">
                                    <asp:TextBox ID="txtAmountTwoS" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
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

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export to Excel"
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
                ID="gvVouchersList"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowDataBound="gvVouchersList_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:BoundField DataField="SR_NO" HeaderText="Sr No." />
                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:BoundField DataField="VOUCHER_DATE" HeaderText="Voucher Date" />
                    <asp:BoundField DataField="CHALLAN_NO" HeaderText="Challan No" />
                    <asp:BoundField DataField="CHALLAN_DATE" HeaderText="Challan Date" />
                    <asp:BoundField DataField="PURCHASE_ORDER_NO" HeaderText="Purchase Order No" />
                    <asp:BoundField DataField="PURCHASE_ORDER_DATE" HeaderText="Purchase Order Date" />
                    <asp:BoundField DataField="VENDOR_CODE" HeaderText="Vendor Code" />
                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                    <asp:BoundField DataField="GLCODE" HeaderText="GL Code" />
                    <asp:BoundField DataField="GL_DESCRIPTION" HeaderText="GL Description" />
                    <asp:BoundField DataField="CLASS" HeaderText="Class" />
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                    <asp:BoundField DataField="PRODUCT_DESCRIPTION" HeaderText="Product Description" />
                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                    <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />

                    <asp:BoundField DataField="CURRENCY_DESC" HeaderText="Currency Desc" />
                    <asp:BoundField DataField="CURRENCY_RATE" HeaderText="Currency Rate" />
                    <asp:BoundField DataField="FC_RATE" HeaderText="FC Rate" />
                    <asp:BoundField DataField="FC_BASIC_AMOUNT" HeaderText="FC Basic Amount" />
                    <asp:BoundField DataField="FC_OTHER_AMOUNT" HeaderText="FC Other Amount" />
                    <asp:BoundField DataField="INR_BASIC_AMOUNT" HeaderText="INR Basic Amount" />
                    <asp:BoundField DataField="INR_GST_AMOUNT" HeaderText="INR GST Amount" />
                    <asp:BoundField DataField="INR_OTHER_AMOUNT" HeaderText="INR Other Amount" />

                    <asp:BoundField DataField="NET_AMT" HeaderText="Net Amount" />

                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                    <asp:BoundField DataField="VOUCHER_CREATED_BY" HeaderText="Voucher Created By" />
                    <asp:BoundField DataField="AUTHORIZED_BY" HeaderText="Authorized By" />
                    <asp:BoundField DataField="AUTHORIZED_ON" HeaderText="Authorized On" />
                    <asp:BoundField DataField="AUTHORIZED_REMARKS" HeaderText="Authorized Remarks" />
                    <asp:BoundField DataField="APPROVED_BY" HeaderText="Approved By" />
                    <asp:BoundField DataField="APPROVED_ON" HeaderText="Approved On" />
                    <asp:BoundField DataField="APPROVED_REMARKS" HeaderText="Approved Remarks" />

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
