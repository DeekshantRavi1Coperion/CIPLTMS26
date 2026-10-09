<%@ Page Title="CIPLTMS- FC Vendor Payment List" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="FCVendorPaymentsList.aspx.cs"
    Inherits="FINANCE_FC_PAYMENT_FCVendorPaymentsList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <%--<link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;

            document.getElementById('<%=txtCutOffDateTOU.ClientID %>').value = document.getElementById('<%=hdCutOffDateTOU.ClientID %>').value;
            document.getElementById('<%=txtVendorInvoiceDateTOU.ClientID %>').value = document.getElementById('<%=hdVendorInvoiceDateTOU.ClientID %>').value;

            document.getElementById('<%=txtDateOfPaymentReleasedUS.ClientID %>').value = document.getElementById('<%=hdDateOfPaymentReleasedUS.ClientID %>').value;
        }



        function clientChangedCutOffDateTOU(sender, args) {
            document.getElementById('<%=hdCutOffDateTOU.ClientID %>').value = document.getElementById('<%=txtCutOffDateTOU.ClientID %>').value;
        }

        function clientChangedVendorInvoiceDateTOU(sender, args) {
            document.getElementById('<%=hdVendorInvoiceDateTOU.ClientID %>').value = document.getElementById('<%=txtVendorInvoiceDateTOU.ClientID %>').value;
        }

        function clientChangedDateOfPaymentReleasedUS(sender, args) {
            document.getElementById('<%=hdDateOfPaymentReleasedUS.ClientID %>').value = document.getElementById('<%=txtDateOfPaymentReleasedUS.ClientID %>').value;
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

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
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

                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
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

                    <label>Request No.:</label>
                    <asp:TextBox ID="txtRequestNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>PO No.:</label>
                    <asp:TextBox ID="txtPONo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCode" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="OPEN" Value="1"></asp:ListItem>
                        <asp:ListItem Text="CLOSE" Value="2"></asp:ListItem>
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Vendor Payments:
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
                    ID="gvVendorPaymentList"
                    runat="server"
                    AutoGenerateColumns="false"
                    CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%"
                    HorizontalAlign="Center"
                    OnRowCommand="gvVendorPaymentList_RowCommand"
                    OnRowDataBound="gvVendorPaymentList_RowDataBound">

                    <Columns>

                        <asp:TemplateField HeaderText="VIEW" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail"
                                    Height="20px"
                                    Width="20px"
                                    CommandArgument="ViewDETAIL"
                                    runat="server"
                                    ImageUrl="~/Images/pdficon1.png" />

                                <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />


                                <asp:Label ID="lblPaymentRequesNo" runat="server" Visible="false" Text='<%# Eval("PAYMENT_REQUEST_NO") %>' />
                                <asp:Label ID="lblDateOfRequest" runat="server" Visible="false" Text='<%# Eval("DATE_OF_REQUEST") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                                <asp:Label ID="lblVendorAddress" runat="server" Visible="false" Text='<%# Eval("VENDOR_ADDRESS") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblAmountToBeReleased" runat="server" Visible="false" Text='<%# Eval("AMOUNT_TO_BE_RELEASED") %>' />
                                <asp:Label ID="lblPaymentTypeID" runat="server" Visible="false" Text='<%# Eval("PAYMENT_TYPE_FID") %>' />
                                <asp:Label ID="lblPaymentType" runat="server" Visible="false" Text='<%# Eval("PAYMENT_TYPE") %>' />
                                <asp:Label ID="lblPOAmount" runat="server" Visible="false" Text='<%# Eval("PO_AMOUNT") %>' />
                                <asp:Label ID="lblCurrency" runat="server" Visible="false" Text='<%# Eval("PO_CURRENCY") %>' />

                                <asp:Label ID="lblInvoiceNo" runat="server" Visible="false" Text='<%# Eval("INVOICE_NO") %>' />
                                <asp:Label ID="lblInvoiceDate" runat="server" Visible="false" Text='<%# Eval("INVOICE_DATE") %>' />
                                <asp:Label ID="lblCutOffDate" runat="server" Visible="false" Text='<%# Eval("CUT_OFF_DATE") %>' />

                                <asp:Label ID="lblBankName" runat="server" Visible="false" Text='<%# Eval("BANK_NAME") %>' />
                                <asp:Label ID="lblBankBranch" runat="server" Visible="false" Text='<%# Eval("BANK_BRANCH") %>' />
                                <asp:Label ID="lblSwftCode" runat="server" Visible="false" Text='<%# Eval("SWIFT_CODE") %>' />
                                <asp:Label ID="lblBankAccountNo" runat="server" Visible="false" Text='<%# Eval("BANK_ACCOUNT_NO") %>' />


                                <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_FID") %>' />
                                <asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNT") %>' />

                                <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                                <asp:Label ID="lblApprovedBy" runat="server" Visible="false" Text='<%# Eval("APPROVED_BY") %>' />
                                <asp:Label ID="lblSentToAmendmentBy" runat="server" Visible="false" Text='<%# Eval("SENT_TO_AMENDMENT_BY") %>' />
                                <asp:Label ID="lblAmendedBy" runat="server" Visible="false" Text='<%# Eval("AMENDED_BY") %>' />
                                <asp:Label ID="lblApprovedAmendedBy" runat="server" Visible="false" Text='<%# Eval("APPROVED_AMENDED_BY") %>' />
                                <asp:Label ID="lblPaymentReleasedBy" runat="server" Visible="false" Text='<%# Eval("PAYMENT_RELEASED_BY") %>' />

                                <asp:Label ID="lblCreatedRemarks" runat="server" Visible="false" Text='<%# Eval("CREATED_REMARKS") %>' />
                                <asp:Label ID="lblAmendedRemarks" runat="server" Visible="false" Text='<%# Eval("AMENDED_REMARKS") %>' />
                                <asp:Label ID="lblApprovedRemarks" runat="server" Visible="false" Text='<%# Eval("APPROVED_REMARKS") %>' />
                                <asp:Label ID="lblApprovedAmendedRemarks" runat="server" Visible="false" Text='<%# Eval("APPROVED_AMENDED_REMARKS") %>' />
                                <asp:Label ID="lblSentToAmendmentRemarks" runat="server" Visible="false" Text='<%# Eval("SENT_TO_AMENDMENT_REMARKS") %>' />
                                <asp:Label ID="lblPaymentReleasedRemarks" runat="server" Visible="false" Text='<%# Eval("PAYMENT_RELEASED_REMARKS") %>' />

                                <asp:Label ID="lblIsCreatedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CREATED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsAmendedApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_AMENDED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsSentToAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_SENT_TO_AMENDMENT_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsPaymentReleasedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PAYMENT_RELEASED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsCancelledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CANCELLED_MAIL_SENT") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Invoice File" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceFileName" runat="server" Visible="false" Text='<%# Eval("INVOICE_NO_FILE_NAME") %>' />
                                <asp:ImageButton ID="imgBtnInvoiceFileName"
                                    Height="30px"
                                    Width="30px"
                                    CommandArgument="VIEW_INVOICE_FILE"
                                    runat="server"
                                    ImageUrl="~/Images/pdficon4.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment File" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPaymentFileName" runat="server" Visible="false" Text='<%# Eval("PAYMENT_FILE_NAME") %>' />
                                <asp:ImageButton ID="imgBtnPaymentFileName"
                                    Height="30px"
                                    Width="30px"
                                    CommandArgument="VIEW_PAYMENT_FILE"
                                    ImageUrl="~/Images/pdficon4.png"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnEdit" CommandArgument="EDIT" runat="server" ImageUrl="~/Images/LOT/edit5.png"
                                    Height="35px" Width="35px" ToolTip="Edit Request" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cancel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnCancel" CommandArgument="CANCEL" runat="server" ImageUrl="~/Images/cancelled_img.png"
                                    Height="35px" Width="35px" ToolTip="Cancel Request" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnApproveRequest" CommandArgument="APPROVE_REQUEST" ToolTip="Approve" runat="server"
                                                Text="Approve" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAmend" CommandArgument="AMEND_REQUEST" ToolTip="Amend" runat="server"
                                                Text="Amend" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid"
                                                BackColor="LightPink"
                                                BorderWidth="2px" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnReleaseAmount" CommandArgument="RELEASE_AMOUNT" ToolTip="Release Amount" runat="server"
                                                Text="Release Amount" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="2px"
                                                BackColor="Blue"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSendToAmendment" CommandArgument="SEND_TO_AMENDMENT" ToolTip="Send To Amendment" runat="server"
                                                Text="Send To Amendment" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px"
                                                BackColor="Orange"
                                                Width="100%" />
                                        </td>
                                    </tr>
                                </table>

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Send Mail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnSendCreatedMail" CommandArgument="SEND_CREATED_MAIL" ToolTip="Send Created Mail" runat="server"
                                    Text="Send Created Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnSendAmendedMail" CommandArgument="SEND_AMENDED_MAIL" ToolTip="Send Amended Mail" runat="server"
                                    Text="Send Amended Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnSendApprovedMail" CommandArgument="SEND_APPROVED_MAIL" ToolTip="Send Amended Mail" runat="server"
                                    Text="Send Amended Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnSendPaymentRelesedMail" CommandArgument="SEND_AMOUNT_RELEASED_MAIL" ToolTip="Send Payment Relesed Mail" runat="server"
                                    Text="Send Payment Relesed Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnSendAmendmentMail" CommandArgument="SEND_AMENDMENT_MAIL" ToolTip="Send Amendment Mail" runat="server"
                                    Text="Send Amendment Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnSendCancelledMail" CommandArgument="SEND_CANCELLED_MAIL" ToolTip="Send Cancelled Mail" runat="server"
                                    Text="Send Cancelled Mail" CssClass="cancelbutton" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Request No." DataField="PAYMENT_REQUEST_NO" />
                        <asp:BoundField HeaderText="Date of Request" DataField="DATE_OF_REQUEST" />

                        <asp:TemplateField HeaderText="Status"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnStatus" runat="server" Height="40px" Width="40px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="JOB No." DataField="JOB_NO" />
                        <asp:BoundField HeaderText="Vendor Code" DataField="VENDOR_CODE" />
                        <asp:BoundField HeaderText="Vendor Name" DataField="VENDOR_NAME" />
                        <asp:BoundField HeaderText="Vendor Address" DataField="VENDOR_ADDRESS" />

                        <asp:BoundField HeaderText="PO No." DataField="PO_NO" />

                        <asp:BoundField HeaderText="PO Amount" DataField="PO_AMOUNT" />
                        <asp:BoundField HeaderText="Currency" DataField="PO_CURRENCY" />

                        <asp:BoundField HeaderText="Total Amount To Be Released" DataField="AMOUNT_TO_BE_RELEASED" />

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

        </fieldset>
    </div>



    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeViewInPDFPopup" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
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
            id="iframeViewFCVendorPaymentInPDF"
            runat="server"></iframe>
    </asp:Panel>


    <%--EDIT/AMEND START--%>
    <asp:Button ID="btnShowPopupEditAmend" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeEditAmend" runat="server" TargetControlID="btnShowPopupEditAmend"
        PopupControlID="pnlPopupEditAmend" CancelControlID="imgBtnCancelEditAmend" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupEditAmend" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelEditAmend" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Update Payment Request</legend>

                <div class="form-grid form-grid-2">

                    <label>Payment Request No.:</label>
                    <asp:TextBox ID="txtPaymentRequestNoTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Job No. / Project No.:</label>
                    <asp:TextBox ID="txtJOBNoTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCodeTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorNameTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Address:</label>
                    <asp:TextBox ID="txtVendorAddressTOU" runat="server"
                        CssClass="form-control"
                        Rows="3" Enabled="false" />


                    <label>CID PO No. to Vendor:</label>
                    <asp:TextBox ID="txtPONoTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Total PO Amount:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%">
                                <asp:TextBox ID="txtTotalPOAmountTOU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" Text="0.000" />
                            </td>

                            <td style="width: 30%">
                                <asp:TextBox ID="txtPOAmountCurrencyTOU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Released Amount:</label>
                    <asp:TextBox ID="txtReleasedAmountTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" Text="0.000" />

                    <label>Payment Release Cut-Off Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCutOffDateTOU" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdCutOffDateTOU" runat="server" />
                                <asp:CalendarExtender ID="calendarCutOffDateTOU" PopupButtonID="imgBtnCutOffDateTOU" runat="server"
                                    TargetControlID="txtCutOffDateTOU" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedCutOffDateTOU">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnCutOffDateTOU" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>Balance Amount:</label>
                    <asp:TextBox ID="txtBalanceAmountTOU" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" Text="0.000" />

                    <label>Requested Amount to be Released:</label>
                    <asp:TextBox ID="txtRequestedAmountToBeReleasedTOU" runat="server"
                        CssClass="form-control"
                        Text="0.000"
                        onkeyup="checkDec(this);"
                        onpaste="return false"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Type of Payment:</label>
                    <asp:DropDownList ID="ddlPaymentTypeTOU"
                        runat="server"
                        CssClass="form-control" />

                    <asp:Label ID="lblAmountToBeReleasedMsgTOU" runat="server" ForeColor="Red" />

                </div>
                <div class="form-grid form-grid-2">
                    <label>Vendor Invoice No.:</label>
                    <asp:TextBox ID="txtVendorInvoiceNoTOU" runat="server"
                        CssClass="form-control" />

                    <label>Vendor Invoice Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtVendorInvoiceDateTOU"
                                    runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdVendorInvoiceDateTOU" runat="server" />
                                <asp:CalendarExtender ID="calendarVendorInvoiceDateTOU"
                                    PopupButtonID="imgBtnVendorInvoiceDateTOU" runat="server"
                                    TargetControlID="txtVendorInvoiceDateTOU" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedVendorInvoiceDateTOU">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnVendorInvoiceDateTOU" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>Attachment:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachmentInvoiceTOU" runat="server"
                                    CssClass="form-control"
                                    BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachmentInvoiceTOU" style="display: none;">
                                    <asp:Label ID="lblfileAttachmentInvoiceTOU" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                    <label>Bank Name:</label>
                    <asp:TextBox ID="txtVendorBankNameTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>Bank Branch:</label>
                    <asp:TextBox ID="txtBankVendorBranchTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Swift Code:</label>
                    <asp:TextBox ID="txtVendorSwiftCodeTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Account Number:</label>
                    <asp:TextBox ID="txtBankAccountNumberTOU" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Remarks:</label>
                    <asp:TextBox ID="txtRemarksTOU" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />

                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnSaveTOU" runat="server" Width="100%" Text="Update" CssClass="button"
                    OnClick="btnSaveTOU_Click" OnClientClick="return ValidateAll();" />

            </div>

            <div class="full-width">
                <asp:Panel ID="pnlEditAmend" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblEditAmendMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>


    </asp:Panel>
    <%--EDIT/AMEND END--%>


    <%--UPDATE STATUS START--%>
    <asp:Button ID="btnShowPopupUpdateStatus" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeUpdateStatus" runat="server" TargetControlID="btnShowPopupUpdateStatus"
        PopupControlID="pnlPopupUpdateStatus" CancelControlID="imgBtnCancelUpdateStatus" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupUpdateStatus" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelUpdateStatus" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-grid-container">
            <fieldset class="form-card">
                <legend>Update Status Of Payment Request</legend>

                <div class="form-grid form-grid-2">

                    <label>Payment Request No.:</label>
                    <asp:TextBox ID="txtPaymentRequestNoUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Job No. / Project No.:</label>
                    <asp:TextBox ID="txtJOBNoUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>Vendor Name:</label>
                    <table>
                        <tr>
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtVendorNameUS" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 20%;">
                                <asp:TextBox ID="txtVendorCodeUS" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Vendor Address:</label>
                    <asp:TextBox ID="txtVendorAddressUS" runat="server"
                        CssClass="form-control"
                        Rows="3" Enabled="false" />

                    <label>CID PO No. to Vendor:</label>
                    <asp:TextBox ID="txtPONoUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Total PO Amount:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%">
                                <asp:TextBox ID="txtTotalPOAmountUS" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" Text="0.000" />
                            </td>

                            <td style="width: 30%">
                                <asp:TextBox ID="txtPOAmountCurrencyUS" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Requested Amount:</label>
                    <asp:TextBox ID="txtReleasedAmountUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" Text="0.000" />

                    <label>Payment Release Cut-Off Date:</label>
                    <asp:TextBox ID="txtCutOffDateUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Type of Payment:</label>
                    <asp:TextBox ID="txtPaymentTypeUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Invoice No.:</label>
                    <asp:TextBox ID="txtVendorInvoiceNoUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Invoice Date:</label>
                    <asp:TextBox ID="txtVendorInvoiceDateUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <asp:Panel ID="pnlViewInvoiceFile" runat="server" Visible="false">
                        <asp:Label ID="lblInvoiceFileUS" runat="server" Font-Bold="True" Font-Size="Large" />
                        <asp:ImageButton ID="imgBtnViewInvoiceFile" Height="30px" Width="30px" runat="server"
                            ImageUrl="~/Images/pdficon4.png" OnClick="imgBtnViewInvoiceFile_Click" />
                    </asp:Panel>

                    <label>Bank Name:</label>
                    <asp:TextBox ID="txtVendorBankNameUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Bank Branch:</label>
                    <asp:TextBox ID="txtBankVendorBranchUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Swift Code:</label>
                    <asp:TextBox ID="txtVendorSwiftCodeUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Account Number:</label>
                    <asp:TextBox ID="txtBankAccountNumberUS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Created / Amended Remarks:</label>
                    <asp:TextBox ID="txtCreatedOrAmendedRemarksUS" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" Enabled="false" />

                    <asp:Panel ID="pnlApprovedOrAmendedApprovedRemarksUS" runat="server" Visible="false">
                        <asp:Label ID="lblApprovedOrAmendedApprovedRemarksUS" runat="server" />
                        <asp:TextBox ID="txtApprovedOrAmendedApprovedRemarksUS" runat="server" Width="100%" TextMode="MultiLine"
                            Rows="2" Enabled="false" />
                    </asp:Panel>

                    <asp:Panel ID="pnlPaymentReleaseRemarksUS" runat="server" Visible="false">

                        <label>Bank Advice:</label>
                        <asp:TextBox ID="txtBankAdviceUS" runat="server"
                            CssClass="form-control" />

                        <label>Date of Payment Released:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDateOfPaymentReleasedUS"
                                        runat="server"
                                        onkeyDown="javascript:preventInput(event);"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdDateOfPaymentReleasedUS" runat="server" />
                                    <asp:CalendarExtender ID="calendarDateOfPaymentReleasedUS"
                                        PopupButtonID="imgBtnDateOfPaymentReleasedUS" runat="server"
                                        TargetControlID="txtDateOfPaymentReleasedUS" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedDateOfPaymentReleasedUS">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnDateOfPaymentReleasedUS" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Calendar" />
                                </td>
                            </tr>
                        </table>

                        <label>Attach Payment File:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="uploadFileAttachmentPaymentFileUS" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileAttachmentPaymentFileUS" style="display: none;">
                                        <asp:Label ID="lblfileAttachmentPaymentFileUS" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <asp:Label ID="lblPaymentReleaseRemarksUS" runat="server" />
                        <asp:TextBox ID="txtPaymentReleaseRemarksUS" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" Enabled="false" />
                    </asp:Panel>

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdateSatus" runat="server" Width="100%" Text="Update" CssClass="button"
                    OnClick="btnUpdateSatus_Click" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlUpdateStatus" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblUpdateStatusMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>
    <%--UPDATE STATUS END--%>


    <%--CANCEL START--%>
    <asp:Button ID="btnShowPopupCancel" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeCancel" runat="server" TargetControlID="btnShowPopupCancel"
        PopupControlID="pnlPopupCancel" CancelControlID="imgBtnCancelCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupCancel" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-grid-container">
            <fieldset class="form-card">

                <legend>Cancel Payment Request</legend>

                <div class="form-grid form-grid-2">

                    <label>Payment Request No.:</label>
                    <asp:TextBox ID="txtPaymentRequestCAN" runat="server"
                        CssClass="form-control"
                        Enabled="false" />


                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCodeCAN" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorNameCAN" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Remarks:</label>
                    <asp:TextBox ID="txtCancelledRemarksCAN" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnCancelRequest" runat="server" Width="100%" Text="Cancel Request" CssClass="button"
                    OnClick="btnCancelRequest_Click" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlCancel" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblCancel" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
