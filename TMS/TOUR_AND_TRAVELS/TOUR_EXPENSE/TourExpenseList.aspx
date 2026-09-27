<%@ Page Title="CIPLTMS-Tour Information List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    AspCompat="true" CodeFile="TourExpenseList.aspx.cs" Inherits="TOUR_AND_TRAVELS_TOUR_TourExpenseList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

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

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            document.getElementById('<%=txtVoucherDateToU.ClientID %>').value = document.getElementById('<%=hdVoucherDateToU.ClientID %>').value;
        }

        function clientChangedVoucherDate(sender, args) {
            document.getElementById('<%=hdVoucherDateToU.ClientID %>').value = document.getElementById('<%=txtVoucherDateToU.ClientID %>').value;
            document.getElementById('<%=txtVoucherDateToU.ClientID %>').value = document.getElementById('<%=hdVoucherDateToU.ClientID %>').value;
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

        function checkDecNew1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    checkDecNew2();
                }
            }
            else {
                checkDecNew2();
            }
        }

        function checkDecNew2() {
            var advanceObtained = '0';
            var AirTicket = '0';
            var Hotel = '0';
            var Taxi = '0';
            var Others = '0';

            if (document.getElementById('<%=txtAirTicketToU.ClientID %>').value != '') {
                AirTicket = document.getElementById('<%=txtAirTicketToU.ClientID %>').value;
            }
            else {
                AirTicket = '0';
            }

            if (document.getElementById('<%=txtHotelToU.ClientID %>').value != '') {
                Hotel = document.getElementById('<%=txtHotelToU.ClientID %>').value;
            }
            else {
                Hotel = '0';
            }

            if (document.getElementById('<%=txtTaxiToU.ClientID %>').value != '') {
                Taxi = document.getElementById('<%=txtTaxiToU.ClientID %>').value;
            }
            else {
                Taxi = '0';
            }

            if (document.getElementById('<%=txtOthersToU.ClientID %>').value != '') {
                Others = document.getElementById('<%=txtOthersToU.ClientID %>').value;
            }
            else {
                Others = '0';
            }


            document.getElementById('<%=hdTotalToU.ClientID %>').value = Math.round(
                (
                    parseFloat(AirTicket)
                    + parseFloat(Hotel)
                    + parseFloat(Taxi)
                    + parseFloat(Others)
                ) * 100) / 100;


            document.getElementById('<%=txtTotalToU.ClientID %>').value = document.getElementById('<%=hdTotalToU.ClientID %>').value;

            if (document.getElementById('<%=txtAdvanceObtainedToU.ClientID %>').value != '') {
                advanceObtained = document.getElementById('<%=txtAdvanceObtainedToU.ClientID %>').value;
            }
            else {
                advanceObtained = '0';
            };


            <%--document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value = Math.round(parseFloat(advanceObtained) - parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value));
            document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value = document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value


            if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) == 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = '';
            }

            else if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
            }
            else {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
            }--%>
        }

    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/Javascript">
        function checkDecNew5(el) {
            var ex = /^[0-9]+\*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/javascript">

        function ValidateSanctionNo() {
            var tourSanctionNo = document.getElementById('<%=txtTourSanctionNoToU.ClientID %>').value;
            if (tourSanctionNo == '') {
                document.getElementById('<%=txtTourSanctionNoToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTourSanctionNoToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function Validatefilea1Upload() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var file1Upload = document.getElementById('<%=file1UploadToU.ClientID %>').value;
            var divfile1Upload = document.getElementById("divfile1UploadToU");
            var lblfile1Upload = document.getElementById('<%=lblfile1UploadToU.ClientID %>');

            if (file1Upload == '') {
                document.getElementById('<%=file1UploadToU.ClientID %>').style.borderColor = "";
                divfile1Upload.style.display = "none";
                lblfile1Upload.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(file1Upload.toLowerCase())) {
                    document.getElementById('<%=file1UploadToU.ClientID %>').style.borderColor = "#F7627F";
                    divfile1Upload.style.display = "block";
                    lblfile1Upload.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=file1UploadToU.ClientID %>').style.borderColor = "";
                    divfile1Upload.style.display = "none";
                    lblfile1Upload.innerHTML = "";
                    return false;
                }
            }
        }



        function Validatefile2Upload() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var file2Upload = document.getElementById('<%=file2UploadToU.ClientID %>').value;
            var divfile2Upload = document.getElementById("divfile2UploadToU");
            var lblfile2Upload = document.getElementById('<%=lblfile2UploadToU.ClientID %>');

            if (file2Upload == '') {
                document.getElementById('<%=file2UploadToU.ClientID %>').style.borderColor = "";
                divfile2Upload.style.display = "none";
                lblfile2Upload.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(file2Upload.toLowerCase())) {
                    document.getElementById('<%=file2UploadToU.ClientID %>').style.borderColor = "#F7627F";
                    divfile2Upload.style.display = "block";
                    lblfile2Upload.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=file2UploadToU.ClientID %>').style.borderColor = "";
                    divfile2Upload.style.display = "none";
                    lblfile2Upload.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateSanctionNo()) {
                check = false;
            }

            if (Validatefile1Upload()) {
                check = false;
            }

            if (Validatefile2Upload()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to save tour expense?")) {
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

        function ValidateVoucherNo() {
            var VoucherNo = document.getElementById('<%=txtVoucherNumberToU.ClientID %>').value;
            if (VoucherNo == '') {
                document.getElementById('<%=txtVoucherNumberToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVoucherNumberToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllStatus() {
            var check = true;

            if (ValidateVoucherNo()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to update tour expense status?")) {
                    document.getElementById('<%=hdConfirmUpdateStatusValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmUpdateStatusValue.ClientID %>').value = "0";
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
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;
    </script>


    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };


        function ClearUnclearDates() {
            var checked = document.getElementById('<%=chkClearUnclearDates.ClientID %>').checked;

            if (checked) {
                document.getElementById('<%=hdStartDateSearch.ClientID %>').value = Date.now;
                document.getElementById('<%=hdEndDateSearch.ClientID %>').value = Date.now;

                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = Date.now;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = Date.now;
            }
            else {
                document.getElementById('<%=hdStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=hdEndDateSearch.ClientID %>').value = "";

                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
        }
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdConfirmUpdateStatusValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Tour Expense List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
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


                    <label>End Date</label>
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
                            <td>
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkClearUnclearDates" runat="server" AutoPostBack="true"
                                    Checked="true"
                                    OnCheckedChanged="chkClearUnclearDates_CheckedChanged" />
                                <%--onchange="ClearUnclearDates()" --%>
                            </td>
                        </tr>
                    </table>

                    <label>Tour Expense No.</label>
                    <asp:TextBox ID="txtTourExpenseNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Sanction No.</label>
                    <asp:TextBox ID="txtSanctionNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Status</label>
                    <asp:DropDownList ID="ddlExpenseStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Employee Name</label>
                    <asp:DropDownList ID="ddlEmployeeName" runat="server"
                        CssClass="form-control">
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
                 <asp:Button ID="btnAddNewTourExpense"
                     OnClick="btnAddNewTourExpense_Click"
                     runat="server"
                     Text="Add New Expense"
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

            <asp:GridView
                CssClass="employee-grid"
                ID="gvTourExpenseList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvTourExpenseList_RowCommand"
                OnRowDataBound="gvTourExpenseList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="VIEW"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px"
                                CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="File1"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment1FileName" runat="server" Visible="false"
                                Text='<%# Eval("ATTACHMENT1_FILE_NAME") %>' />
                            <asp:ImageButton ID="btnAttachment1File" Height="20px" Width="20px"
                                ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="VIEWFILE1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="File2"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment2FileName" runat="server" Visible="false"
                                Text='<%# Eval("ATTACHMENT2_FILE_NAME") %>' />
                            <asp:ImageButton ID="btnAttachment2File" Height="20px" Width="20px"
                                ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="VIEWFILE2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="EDIT"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server"
                                ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="BOOK"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnBook" CommandArgument="BOOK"
                                    ToolTip="Book Tour Expense" runat="server"
                                    Text="Book" CssClass="cancelbutton" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="CLOSE"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnClose" CommandArgument="CLOSE"
                                ToolTip="Close Tour Expense" runat="server"
                                Text="Close" CssClass="cancelbutton"
                                BorderColor="Yellow" BorderStyle="Solid" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEND_MAIL"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnSendMail" CommandArgument="SEND_MAIL" ToolTip="Send Mail"
                                runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TOUR_EXPENSE_NO" HeaderText="TOUR_EXPENSE_NO" />

                    <asp:TemplateField HeaderText="STATUS"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordId" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblTourExpenseNo" runat="server" Visible="false" Text='<%# Eval("TOUR_EXPENSE_NO") %>' />
                            <asp:Label ID="lblTourID" runat="server" Visible="false" Text='<%# Eval("TOUR_ID") %>' />
                            <asp:Label ID="lblTourNo" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                            <asp:Label ID="lblTourSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                            <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            <asp:Label ID="lblEmployeeCode" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                            <asp:Label ID="lblStartDate" runat="server" Visible="false" Text='<%# Eval("START_DATE") %>' />
                            <asp:Label ID="lblEndDate" runat="server" Visible="false" Text='<%# Eval("END_DATE") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblEmployeeDesignation" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_DESIGNATION") %>' />
                            <asp:Label ID="lblPlaceOfVisit" runat="server" Visible="false" Text='<%# Eval("PLACE_OF_VISIT") %>' />
                            <asp:Label ID="lblVisitType" runat="server" Visible="false" Text='<%# Eval("VISIT_TYPE") %>' />
                            <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                            <asp:Label ID="lblStatusName" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                            <asp:Label ID="lblAirTicketAmount" runat="server" Visible="false" Text='<%# Eval("AIR_TICKET_AMOUNT") %>' />
                            <asp:Label ID="lblHotelAmount" runat="server" Visible="false" Text='<%# Eval("HOTEL_AMOUNT") %>' />
                            <asp:Label ID="lblTaxiAmount" runat="server" Visible="false" Text='<%# Eval("TAXI_AMOUNT") %>' />
                            <asp:Label ID="lblOthersAmount" runat="server" Visible="false" Text='<%# Eval("OTHERS_AMOUNT") %>' />
                            <asp:Label ID="lblAdvanceAmount" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMOUNT") %>' />
                            <asp:Label ID="lblTotalAmount" runat="server" Visible="false" Text='<%# Eval("TOTAL_AMOUNT") %>' />
                            <asp:Label ID="lblCurrencyId" runat="server" Visible="false" Text='<%# Eval("CURRENCY_ID") %>' />
                            <asp:Label ID="lblCurrencyCode" runat="server" Visible="false" Text='<%# Eval("CURRENCY_CODE") %>' />
                            <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("CREATED_REMARKS") %>' />
                            <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                            <asp:Label ID="lblClosedBy" runat="server" Visible="false" Text='<%# Eval("CLOSED_BY") %>' />
                            <asp:Label ID="lblClosingPersonID" runat="server" Visible="false" Text='<%# Eval("CLOSING_PERSON") %>' />

                            <asp:Label ID="lblIsOpenMailSent" runat="server" Visible="false" Text='<%# Eval("IS_OPEN_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsBookedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_BOOKED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsClosedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CLOSED_MAIL_SENT") %>' />



                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="TOUR_SANCTION_NO" />
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" />
                    <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                    <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                    <asp:BoundField DataField="EMPLOYEE_DESIGNATION" HeaderText="EMPLOYEE_DESIGNATION" />
                    <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS_NAME" />
                    <asp:BoundField DataField="AIR_TICKET_AMOUNT" HeaderText="AIR_TICKET_AMOUNT" />
                    <asp:BoundField DataField="HOTEL_AMOUNT" HeaderText="HOTEL_AMOUNT" />
                    <asp:BoundField DataField="TAXI_AMOUNT" HeaderText="TAXI_AMOUNT" />
                    <asp:BoundField DataField="OTHERS_AMOUNT" HeaderText="OTHERS_AMOUNT" />
                    <asp:BoundField DataField="ADVANCE_AMOUNT" HeaderText="ADVANCE_AMOUNT" />
                    <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="TOTAL_AMOUNT" />
                    <asp:BoundField DataField="CURRENCY_CODE" HeaderText="CURRENCY_CODE" />


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



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeTourExpense" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>Tour Expense</legend>

                <div class="form-grid form-grid-2">

                    <label>Sanction No.</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtTourSanctionNoToU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetTourSanctionNo" CssClass="button" runat="server" Text="Get"
                                    Width="100%"
                                    OnClick="btnGetTourSanctionNo_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Tour No.</label>
                    <asp:TextBox ID="txtTourNoToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                    <label>Employee Name</label>
                    <asp:TextBox ID="txtEmployeeNameToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                    <label>Employee ID</label>
                    <asp:TextBox ID="txtEmployeeIDToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                </div>


                <div class="form-grid form-grid-2">

                    <label>Tour Dates</label>
                    <div class="full-width">

                        <table width="100%">
                            <tr>
                                <td style="width: 28%;">
                                    <asp:TextBox ID="txtStartDateToU"
                                        runat="server"
                                        Enabled="false"
                                        ReadOnly="true"
                                        CssClass="form-control" />
                                </td>
                                <td style="width: 28%;">
                                    <asp:TextBox ID="txtEndDateToU"
                                        runat="server"
                                        Enabled="false"
                                        ReadOnly="true"
                                        CssClass="form-control" />
                                </td>
                                <td style="width: 5%;">Days:
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtDaysToU" runat="server"
                                        Enabled="false"
                                        ReadOnly="true"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                        </table>

                    </div>


                    <label>Name of Customer/Vendor</label>
                    <asp:TextBox ID="txtCustVendNameToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                    <label>Place of Visit</label>
                    <asp:TextBox ID="txtPlaceOfVisitToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                    <label>Purpose Of Visit</label>
                    <asp:TextBox ID="txtPurposeOfVisitToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                    <label>Job/Inq No. Where Applicable</label>
                    <asp:TextBox ID="txtJobInqNoToU"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false" />

                </div>

                <asp:Panel runat="server" ID="pnlUploadAttachments" Visible="false">
                    <div class="form-grid form-grid-2">

                        <label>Attachment1</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="file1UploadToU" runat="server"
                                        BorderStyle="Groove"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfile1UploadToU" style="display: none;">
                                        <asp:Label ID="lblfile1UploadToU" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <label>Attachment2</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="file2UploadToU" runat="server"
                                        BorderStyle="Groove"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfile2UploadToU" style="display: none;">
                                        <asp:Label ID="lblfile2UploadToU" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                    </div>
                </asp:Panel>

                <div class="form-grid form-grid-2">

                    <label>Air Ticket</label>
                    <asp:TextBox ID="txtAirTicketToU"
                        runat="server"
                        onKeyUp="checkDecNew1(this)"
                        onpaste="return false"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        Text="0"
                        CssClass="form-control" />


                    <label>Hotel</label>
                    <asp:TextBox ID="txtHotelToU"
                        runat="server"
                        onKeyUp="checkDecNew1(this)"
                        onpaste="return false"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        Text="0"
                        CssClass="form-control" />


                    <label>Taxi</label>
                    <asp:TextBox ID="txtTaxiToU"
                        runat="server"
                        onKeyUp="checkDecNew1(this)"
                        onpaste="return false"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        Text="0"
                        CssClass="form-control" />


                    <label>Others</label>
                    <asp:TextBox ID="txtOthersToU"
                        runat="server"
                        onKeyUp="checkDecNew1(this)"
                        onpaste="return false"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        Text="0"
                        CssClass="form-control" />


                    <label>Total Amount</label>
                    <table>
                        <tr>
                            <td style="width: 65%;">
                                <asp:TextBox ID="txtTotalToU" runat="server" Text="0.00" Enabled="false"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdTotalToU" runat="server" />
                            </td>
                            <td style="width: 35%;">
                                <asp:DropDownList ID="ddlTotalCurrencyToU" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Advance Obtained</label>
                    <table>
                        <tr>
                            <td style="width: 65%;">
                                <asp:TextBox ID="txtAdvanceObtainedToU"
                                    runat="server"
                                    Text="0.00"
                                    Enabled="false"
                                    onkeyup="checkDec(this);"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 35%;">
                                <asp:DropDownList ID="ddlAdvanceObtainedCurrenyToU" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Remarks</label>
                    <div class="full-width">

                        <asp:TextBox ID="txtRemarksToU"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <label>Voucher Number</label>
                    <asp:TextBox ID="txtVoucherNumberToU" runat="server" CssClass="form-control" />


                    <label>Voucher Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtVoucherDateToU" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdVoucherDateToU" runat="server" />
                                <asp:CalendarExtender ID="calendarVoucherDateToU" PopupButtonID="imgbtnVoucherDateToU"
                                    runat="server" TargetControlID="txtVoucherDateToU" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedVoucherDate">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnVoucherDateToU" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Voucher Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>Closing Remarks</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtClosingRemarksToU"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>
                </div>
        </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSubmit"
                    runat="server"
                    Text="Save"
                    CssClass="button"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnSubmit_Click" Width="50%" />

                <asp:Button ID="btnUpdateStatus"
                    runat="server"
                    Text="Update Status"
                    CssClass="button"
                    OnClick="btnUpdateStatus_Click" Width="50%" />
            </div>
        <div class="full-width">
            <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
            </asp:Panel>

            <asp:Panel ID="pnlExceptionMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblExceptionMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
        </div>


        </div>




        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" /></legend>
            <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>



                <br />

            </div>
        </fieldset>
    </asp:Panel>

    <%--TOUR SANCTION NO DETAIL START--%>
    <asp:Button ID="btnShowPopupTourSanctionNo" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeTourSanctionNo" runat="server" TargetControlID="btnShowPopupTourSanctionNo"
        PopupControlID="pnlPopupTourSanctionNo" CancelControlID="imgBtnCancelTourSanctionNo" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopupTourSanctionNo" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelTourSanctionNo" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">
            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblTourSanctionNoRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">
                        <label>Tour Sanction No.</label>
                        <asp:TextBox ID="txtTourSanctionNoToG" runat="server" CssClass="form-control" />

                        <label>Employee Name</label>
                        <asp:TextBox ID="txtEmployeeNameToG" runat="server" CssClass="form-control" />

                        &nbsp;
                 <asp:Button ID="btnbtnSearchTourSanctionNo"
                     OnClick="btnbtnSearchTourSanctionNo_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                    </div>

                </fieldset>
            </div>

            <div class="popup-grid-container">
                <div align="center">
                    <asp:Label ID="lblTourSanctionNoMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvTourSanctionNo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvTourSanctionNo_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET">
                            <ItemTemplate>
                                <asp:Label ID="lblTourId" runat="server" Visible="false" Text='<%# Eval("TOUR_ID") %>' />
                                <asp:Label ID="lblTourNo" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                                <asp:Label ID="lblTourSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                                <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                <asp:Label ID="lblEmployeeId" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_ID") %>' />
                                <asp:Label ID="lblDesignation" runat="server" Visible="false" Text='<%# Eval("DESIGNATION") %>' />
                                <asp:Label ID="lblStartDate" runat="server" Visible="false" Text='<%# Eval("START_DATE") %>' />
                                <asp:Label ID="lblEndDate" runat="server" Visible="false" Text='<%# Eval("END_DATE") %>' />
                                <asp:Label ID="lblCustVendName" runat="server" Visible="false" Text='<%# Eval("CUST_VEND_NAME") %>' />
                                <asp:Label ID="lblPlaceOfVisit" runat="server" Visible="false" Text='<%# Eval("PLACE_OF_VISIT") %>' />
                                <asp:Label ID="lblVisitType" runat="server" Visible="false" Text='<%# Eval("VISIT_TYPE") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblBusSegment" runat="server" Visible="false" Text='<%# Eval("BUS_SEGMENT") %>' />
                                <asp:Label ID="lblAdvanceAmt" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMT") %>' />
                                <asp:Label ID="lblAdvanceCurrencyID" runat="server" Visible="false" Text='<%# Eval("ADVANCE_CURRENCY") %>' />
                                <asp:Label ID="lblCurrencyCode" runat="server" Visible="false" Text='<%# Eval("CURRENCY_CODE") %>' />
                                <asp:Button ID="btnGetProductionOrderNo" CommandArgument="GET" ToolTip="Get Production Order No"
                                    runat="server" Text="Get" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TOUR_NO" HeaderText="TOUR_NO" />
                        <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="TOUR_SANCTION_NO" />
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                        <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                        <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                        <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                        <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                        <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                        <asp:BoundField DataField="VISIT_TYPE" HeaderText="VISIT_TYPE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_AMT" />
                        <asp:BoundField DataField="CURRENCY_CODE" HeaderText="CURRENCY_CODE" />
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
    <%--TOUR SANCTION NO DETAIL END--%>

    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
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
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
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

    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
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
            id="iframeViewTourInformationInPDF"
            runat="server"></iframe>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
