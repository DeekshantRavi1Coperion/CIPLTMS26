<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PrintEPaymentVoucher.aspx.cs"
    Inherits="FINANCE_PrintEPaymentVoucher" 
    Title="CIPLTMS - Print E Payment Voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxcenter {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxright {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            height: 26px;
            /*background-color: transparent;*/
            border-color: lightblue;
            /*background-color: #D8D8D8;*/
        }

        .textboxleft {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: transparent;*/
            border-color: lightblue;
        }
    </style>


    <script type="text/javascript">

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

    <script type="text/javascript" language="javascript">

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

        function ValidateAllSearch() {

            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }

    </script>

    <script type="text/javascript">

        function ValidateCopies() {
            var Copies = document.getElementById('<%=txtCopies.ClientID %>').value;
            if (Copies == '' || parseInt(Copies) == 0) {
                document.getElementById('<%=txtCopies.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCopies.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidatePrinter() {
            var Printer = document.getElementById('<%=ddlPrinter.ClientID %>').selectedIndex;
            if (Printer == '' || Printer == 0) {
                document.getElementById('<%=ddlPrinter.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPrinter.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePageSize() {
            var paper = document.getElementById('<%=ddlPaper.ClientID %>').selectedIndex;
            if (paper == '' || paper == 0) {
                document.getElementById('<%=ddlPaper.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPaper.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ConfirmPrint() {
            var check = true;

            if (ValidateCopies()) {
                check = false;
            }

            if (ValidatePrinter()) {
                check = false;
            }

            if (ValidatePageSize()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to print payment voucher?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }



        function ConfirmGenerate() {
            var check = true;

            if (ValidatePageSize()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to generate payment voucher?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }

    </script>

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDesignResponsibleEnggFlag" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Date Filter:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlOnWhichDateMainSearch" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="Request Date" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Order Date" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Generated Date" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSignMainSearch" runat="server"
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
                        </tr>
                    </table>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
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
                            <td style="width:90%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Company:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control"></asp:TextBox>



                    <label>FACT Voucher No.:</label>
                    <asp:TextBox ID="txtFACTVoucherNo" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Order No.:</label>
                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Invoice No.:</label>
                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Party Bill No.:</label>
                    <asp:TextBox ID="txtPartyBillNo" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Bank Name:</label>
                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Generated" Value="1" />
                        <asp:ListItem Text="BA Generated" Value="2" />
                        <asp:ListItem Text="Open" Value="3" />
                    </asp:DropDownList>


                    <div class="full-width">
                        <label>Printer:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:DropDownList ID="ddlPrinter" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 30%;">
                                    <asp:DropDownList ID="ddlPaper" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Select" Value="Select" />
                                        <asp:ListItem Text="A2" Value="A2" />
                                        <asp:ListItem Text="A3" Value="A3" />
                                        <asp:ListItem Text="A4" Value="A4" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAllSearch();" OnClick="btnSearch_Click" />

                <asp:Button ID="btnExportPDF" CssClass="button" Width="100%" runat="server" Text="Generate Voucher"
                    OnClientClick="return ConfirmGenerate();" OnClick="btnExportPDF_Click" />

                <label>Copies:</label>
                <asp:TextBox ID="txtCopies" runat="server" CssClass="form-control"
                    Text="1" onkeypress="return inNumberKey(this, event);" MaxLength="2" />

                <asp:Button ID="btnPrintPDF" CssClass="button" Width="100%" runat="server" Text="Print Voucher"
                    OnClientClick="return ConfirmPrint();" OnClick="btnPrintPDF_Click" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Print E-Payment Voucher:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPaymentVoucherList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPaymentVoucherList_RowDataBound"
                    OnRowCommand="gvPaymentVoucherList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="30px" Width="30px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/pdficon3.png" ToolTip="View LOT Detail in PDF" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Details" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewVoucherDetail" Height="30px" Width="30px" CommandArgument="ViewVoucherDETAIL"
                                    runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View Voucher Details" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Fact Voucher Number">
                            <ItemTemplate>
                                <asp:Label ID="lblFactVoucherNumber" runat="server" Text='<%# Eval("FACT_VoucherNumber") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Request Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("RequestDate") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vendor Code">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("VendorCode") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vendor Name">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bank Name">
                            <ItemTemplate>
                                <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("Branch") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RTGS/IFSC Code">
                            <ItemTemplate>
                                <asp:Label ID="lblRTGS_IFSCcode" runat="server" Text='<%# Eval("RTGS/IFSCcode") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bank Account Number">
                            <ItemTemplate>
                                <asp:Label ID="lblBankAccountNumber" runat="server" Text='<%# Eval("BankAccountNumber") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>





                        <asp:TemplateField HeaderText="Generated By">
                            <ItemTemplate>
                                <asp:Label ID="lblGeneratedBy" runat="server" Text='<%# Eval("GeneratedBy") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Generated On">
                            <ItemTemplate>
                                <asp:Label ID="lblGeneratedOn" runat="server" Text='<%# Eval("GeneratedOn") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="HOD">
                            <ItemTemplate>
                                <asp:Label ID="lblHOD" runat="server" Text='<%# Eval("HOD") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="HOD Approved On">
                            <ItemTemplate>
                                <asp:Label ID="lblHOD_ApprovedOn" runat="server" Text='<%# Eval("HOD_ApprovedOn") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Accounts Approved By">
                            <ItemTemplate>
                                <asp:Label ID="lblAccountsApprovedBy" runat="server" Text='<%# Eval("AccountsApprovedBy") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Accounts Approved On">
                            <ItemTemplate>
                                <asp:Label ID="lblAccounts_ApprovedOn" runat="server" Text='<%# Eval("Accounts_ApprovedOn") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="BA Generated By">
                            <ItemTemplate>
                                <asp:Label ID="lblBAGeneratedBy" runat="server" Text='<%# Eval("BAGeneratedBy") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date BA Generated">
                            <ItemTemplate>
                                <asp:Label ID="lblDate_BAGenerated" runat="server" Text='<%# Eval("Date_BAGenerated") %>' Visible="true" />
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

        </fieldset>

    </div>


    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeViewInPDF" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
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
            class="popup-iframe"
            id="iframePaymentVoucherDetailsInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>


    <%-- SHOW VOUCHER DETAIL START--%>
    <asp:Button ID="btnShowVoucherDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeVoucherDetail" runat="server" TargetControlID="btnShowVoucherDetail" BehaviorID="mpeVoucherDetailBID"
        PopupControlID="pnlViewVoucherDetailPopup" CancelControlID="imgBtnCancelVoucherDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewVoucherDetailPopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelVoucherDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <asp:Panel ID="pnlViewVouchers" runat="server" Visible="true">

            <div class="popup-layout">

                <div class="form-grid-container">
                    <fieldset class="filter-card">
                        <legend>
                            <asp:Label ID="lblVouchersSIRerords" runat="server" Text="Vouchers Records[0]" />
                        </legend>

                        <div class="form-grid form-grid-3">

                            <label>Fact Voucher Number:</label>
                            <asp:TextBox ID="txtFactVoucherNumberInDetails" runat="server" CssClass="form-control" Enabled="false" />

                        </div>
                    </fieldset>
                </div>

                <div class="popup-grid-container">
                    <asp:GridView
                        CssClass="popup-grid"
                        ID="gvVouchersDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                        OnRowDataBound="gvVouchersDetail_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# Eval("SrNo") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# Eval("InvoiceNumber") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Invoice Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Invoice Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDueDate" runat="server" Text='<%# Eval("InvoiceDueDate") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Party Bill Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyBillNumber" runat="server" Text='<%# Eval("PartyBillNumber") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Order Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNumber" runat="server" Text='<%# Eval("OrderNumber") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Order Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderDate") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="InvoiceNet Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNetAmount" runat="server" Text='<%# Eval("InvoiceNetAmount") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Outstanding Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblOutstandingAmount" runat="server" Text='<%# Eval("OutstandingAmount") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproved_Amount" runat="server" Text='<%# Eval("Approved_Amount") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FACT Voucher Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblFACT_VoucherStatus" runat="server" Text='<%# Eval("FACT_VoucherStatus") %>' Visible="true" />
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

        </asp:Panel>

    </asp:Panel>
    <%-- SHOW SUBITEM DETAIL END--%>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
