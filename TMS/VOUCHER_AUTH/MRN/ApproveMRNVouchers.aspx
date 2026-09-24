<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ApproveMRNVouchers.aspx.cs" Inherits="VOUCHER_AUTH_MRN_ApproveMRNVouchers" %>

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

        function ConfirmReauth() {

            if (confirm("Would you send voucher for reauthorization?")) {
                return true;
            }
            else {
                return false;
            }
        }

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



            document.getElementById('<%=ddlWarehouse.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlStatus.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlCreatedBy.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtVoucherNumberToS.ClientID %>').value = "";

            return false;
        }

    </script>


    <script type="text/javascript">

        function ValidateFileAttachment1() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachmentToAS1.ClientID %>').value;
            var divfileAttachmentToAS1 = document.getElementById("divfileAttachmentToAS1");
            var lblfileAttachmentToAS1 = document.getElementById('<%=lblfileAttachmentToAS1.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentToAS1.ClientID %>').style.borderColor = "";
                divfileAttachmentToAS1.style.display = "none";
                lblfileAttachmentToAS1.innerHTML = "";
                return false;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentToAS1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentToAS1.style.display = "block";
                    lblfileAttachmentToAS1.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentToAS1.ClientID %>').style.borderColor = "";
                    divfileAttachmentToAS1.style.display = "none";
                    lblfileAttachmentToAS1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileAttachment2() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachmentToAS2.ClientID %>').value;
            var divfileAttachmentToAS2 = document.getElementById("divfileAttachmentToAS2");
            var lblfileAttachmentToAS2 = document.getElementById('<%=lblfileAttachmentToAS2.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentToAS2.ClientID %>').style.borderColor = "";
                divfileAttachmentToAS2.style.display = "none";
                lblfileAttachmentToAS2.innerHTML = "";
                return false;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentToAS2.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentToAS2.style.display = "block";
                    lblfileAttachmentToAS2.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentToAS2.ClientID %>').style.borderColor = "";
                    divfileAttachmentToAS2.style.display = "none";
                    lblfileAttachmentToAS2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileAttachment3() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachmentToAS3.ClientID %>').value;
            var divfileAttachmentToAS3 = document.getElementById("divfileAttachmentToAS3");
            var lblfileAttachmentToAS3 = document.getElementById('<%=lblfileAttachmentToAS3.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentToAS3.ClientID %>').style.borderColor = "";
                divfileAttachmentToAS3.style.display = "none";
                lblfileAttachmentToAS3.innerHTML = "";
                return false;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentToAS3.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentToAS3.style.display = "block";
                    lblfileAttachmentToAS3.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentToAS3.ClientID %>').style.borderColor = "";
                    divfileAttachmentToAS3.style.display = "none";
                    lblfileAttachmentToAS3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileAttachment4() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachmentToAS4.ClientID %>').value;
            var divfileAttachmentToAS4 = document.getElementById("divfileAttachmentToAS4");
            var lblfileAttachmentToAS4 = document.getElementById('<%=lblfileAttachmentToAS4.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentToAS4.ClientID %>').style.borderColor = "";
                divfileAttachmentToAS4.style.display = "none";
                lblfileAttachmentToAS4.innerHTML = "";
                return false;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentToAS4.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentToAS4.style.display = "block";
                    lblfileAttachmentToAS4.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentToAS4.ClientID %>').style.borderColor = "";
                    divfileAttachmentToAS4.style.display = "none";
                    lblfileAttachmentToAS4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileAttachment5() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachmentToAS5.ClientID %>').value;
            var divfileAttachmentToAS5 = document.getElementById("divfileAttachmentToAS5");
            var lblfileAttachmentToAS5 = document.getElementById('<%=lblfileAttachmentToAS5.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentToAS5.ClientID %>').style.borderColor = "";
                divfileAttachmentToAS5.style.display = "none";
                lblfileAttachmentToAS5.innerHTML = "";
                return false;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentToAS5.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentToAS5.style.display = "block";
                    lblfileAttachmentToAS5.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentToAS5.ClientID %>').style.borderColor = "";
                    divfileAttachmentToAS5.style.display = "none";
                    lblfileAttachmentToAS5.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateAllFiles() {
            var check = true;

            if (ValidateFileAttachment1()) {
                check = false;
            }

            if (ValidateFileAttachment2()) {
                check = false;
            }

            if (ValidateFileAttachment3()) {
                check = false;
            }

            if (ValidateFileAttachment4()) {
                check = false;
            }

            if (ValidateFileAttachment5()) {
                check = false;
            }



            if (check) {
                if (confirm("Would you approve voucher?")) {
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
                <legend>Approve MRN Vouchers:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width">
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
                    </div>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server"
                                    ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
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
                                <asp:TextBox ID="txtEndDateSearch" runat="server"
                                    ReadOnly="true" CssClass="form-control"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Warehouse:</label>
                    <asp:DropDownList ID="ddlWarehouse" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="" />
                        <asp:ListItem Text="A35" Value="A" />
                        <asp:ListItem Text="GNU" Value="G" />
                    </asp:DropDownList>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Created By:</label>
                    <asp:DropDownList ID="ddlCreatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <div class="full-width">
                        <label>Voucher Number:</label>
                        <asp:TextBox ID="txtVoucherNumberToS" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="6">
                        </asp:TextBox>
                    </div>

                </div>

            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClientClick="return ValidateAllPath();"
                    OnClick="btnSearch_Click" />

                <asp:CheckBox ID="chkSelectDeselectVouchers" Text="Tag All Vouchers"
                    runat="server" AutoPostBack="true"
                    OnCheckedChanged="chkSelectDeselectVouchers_CheckedChanged" />

                <asp:Button ID="btnBulkApprove" CssClass="button"
                    Width="100%" runat="server"
                    Text="Bulk Approve"
                    OnClick="btnBulkApprove_Click" />

            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView ID="gvVouchersList"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                CssClass="employee-grid"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvVouchersList_RowCommand"
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

                    <asp:TemplateField HeaderText="Sl No."
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                            <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VOUCHER_NO") %>' Visible="false" />
                            <asp:Label ID="lblVoucherDate" runat="server" Text='<%# Eval("VOUCHER_DATE") %>' Visible="false" />
                            <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_FID") %>' Visible="false" />
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("VOUCHER_CREATED_BY") %>' Visible="false" />
                            <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("UNIT_ID") %>' Visible="false" />
                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UNIT_NAME") %>' Visible="false" />
                            <asp:Label ID="lblIsMRNClosed" runat="server" Text='<%# Eval("IS_MRN_CLOSED") %>' Visible="false" />

                            <asp:Label ID="lblCurrencyDesc" runat="server" Text='<%# Eval("CURRENCY_DESC") %>' Visible="false" />
                            <asp:Label ID="lblCurrencyRate" runat="server" Text='<%# Eval("CURRENCY_RATE") %>' Visible="false" />
                            <asp:Label ID="lblInrBasicAmount" runat="server" Text='<%# Eval("INR_BASIC_AMOUNT") %>' Visible="false" />
                            <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Eval("FC_BASIC_AMOUNT") %>' Visible="false" />
                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NET_AMT") %>' Visible="false" />

                            <asp:Label ID="lblGateEntryNo" runat="server" Text='<%# Eval("GATE_ENTRY_NO") %>' Visible="false" />
                            <asp:Label ID="lblGateEntryDate" runat="server" Text='<%# Eval("GATE_ENTRY_DATE") %>' Visible="false" />
                            <asp:Label ID="lblGateEntryDateFormatted" runat="server" Text='<%# Eval("GATE_ENTRY_DATE_FORMATTED") %>' Visible="false" />

                            <asp:Label ID="lblApproverID" runat="server" Text='<%# Eval("APPROVER_ID") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        HeaderText="Select"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkSelectVoucher" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewPDFCopy"
                                CommandArgument="VIEW_VOUCHER_PDF"
                                runat="server"
                                ImageUrl="~/Images/pdficon1.png"
                                Height="20px"
                                Width="20px"
                                ToolTip="View Voucher Copy" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Zoom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <table width="10%" id="tblViewDetail" runat="server">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px"
                                            CommandArgument="ViewDETAIL"
                                            runat="server" ImageUrl="~/Images/viewdetails.png"
                                            ToolTip="View Details" />
                                    </td>
                                    <td><b>[<asp:Label ID="lblDocumentsCount" runat="server" Text='<%# Eval("DOCUMENTS_COUNT") %>' />]</b></td>
                                </tr>
                            </table>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        HeaderText="Approve"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnApprove"
                                CommandArgument="APPROVE"
                                runat="server"
                                ImageUrl="~/Images/VC/vc-authorize.png"
                                Height="20px"
                                Width="20px"
                                ToolTip="Authorize Voucher" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        HeaderText="Add"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnAddNewDocument"
                                CommandArgument="ADD_NEW_DOCUMENT"
                                runat="server"
                                ImageUrl="~/Images/Icons/add05.png"
                                Height="20px"
                                Width="20px"
                                ToolTip="Add New Document" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField
                        HeaderText="Download"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnDownloadAllDocuments"
                                CommandArgument="DOWNLOAD_ALL_DOCUMENTS"
                                runat="server"
                                ImageUrl="~/Images/Download/download3.png"
                                Height="20px"
                                Width="20px"
                                ToolTip="Download All Documents" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:BoundField DataField="SR_NO" HeaderText="Sr No." />--%>
                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                    <asp:BoundField DataField="VOUCHER_DATE" HeaderText="Voucher Date" />
                    <asp:BoundField DataField="AUTHORIZED_ON" HeaderText="Authorized On" />
                    <asp:BoundField DataField="PURCHASE_ORDER_DATE" HeaderText="Purchase Order Date" />
                    <asp:BoundField DataField="VENDOR_CODE" HeaderText="Vendor Code" />
                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                    <asp:BoundField DataField="DOC_CLASS" HeaderText="DOC Class" />

                    <asp:BoundField DataField="CURRENCY_DESC" HeaderText="Currency Desc" />
                    <asp:BoundField DataField="CURRENCY_RATE" HeaderText="Currency Rate" />

                    <asp:BoundField DataField="INR_BASIC_AMOUNT" HeaderText="INR Basic Amount" />
                    <asp:BoundField DataField="INR_OTHER_AMOUNT" HeaderText="INR Other Amount" />

                    <asp:BoundField DataField="FC_BASIC_AMOUNT" HeaderText="FC Basic Amount" />
                    <asp:BoundField DataField="FC_OTHER_AMOUNT" HeaderText="FC Other Amount" />

                    <asp:BoundField DataField="NET_AMT" HeaderText="Net Amount" />

                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />

                    <asp:BoundField DataField="GATE_ENTRY_NO" HeaderText="Gate Entry No." />
                    <asp:BoundField DataField="GATE_ENTRY_DATE_FORMATTED" HeaderText="Gate Entry Date" />

                    <asp:BoundField DataField="VOUCHER_CREATED_BY" HeaderText="Created By" />
                    <asp:BoundField DataField="APPROVED_BY" HeaderText="Approved By" />


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
    <asp:Panel ID="pnlViewAuthorizeVoucherPopup" runat="server"  CssClass="popup-edit">

        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAuthorizeVoucher" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblAuthorizeVoucherDetailsLegend" runat="server" />
                    [<asp:Label ID="lblVoucherNoToAS" runat="server" />]
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Voucher Date:</label>
                    <asp:TextBox ID="txtVoucherDateToAS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnitToAS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Created By:</label>
                    <asp:TextBox ID="txtCreatedByToAS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Gate Entry No:</label>
                    <asp:TextBox ID="txtGateEntryNoToAS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>Gate Entry Date:</label>
                    <asp:TextBox ID="txtGateEntryDateToAS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <div class="full-width">
                        <div class="employee-grid-container">

                            <asp:GridView ID="gvVoucherDetails"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="myGrid"
                                ForeColor="#333333" GridLines="Both"
                                Width="100%"
                                HorizontalAlign="Center"
                                OnRowDataBound="gvVoucherDetails_RowDataBound">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl No."
                                        HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNoD" runat="server" Text='<%# Eval("SR_NO") %>' />
                                            <asp:Label ID="lblVoucherNoD" runat="server" Text='<%# Eval("VOUCHER_NO") %>' Visible="false" />
                                            <asp:Label ID="lblPurchaseOrderNoD" runat="server" Text='<%# Eval("PURCHASE_ORDER_NO") %>' Visible="false" />
                                            <asp:Label ID="lblClassD" runat="server" Text='<%# Eval("CLASS") %>' Visible="false" />
                                            <asp:Label ID="lblProductCodeD" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                            <asp:Label ID="lblProductDescriptionD" runat="server" Text='<%# Eval("PRODUCT_DESCRIPTION") %>' Visible="false" />
                                            <asp:Label ID="lblQuantityD" runat="server" Text='<%# Eval("QUANTITY") %>' Visible="false" />
                                            <asp:Label ID="lblUOMD" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />
                                            <asp:Label ID="lblRateD" runat="server" Text='<%# Eval("RATE") %>' Visible="false" />
                                            <asp:Label ID="lblAmountD" runat="server" Text='<%# Eval("AMOUNT") %>' Visible="false" />

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="PURCHASE_ORDER_NO" HeaderText="Purchase Order No." />
                                    <asp:BoundField DataField="CLASS" HeaderText="Class" />
                                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                                    <asp:BoundField DataField="PRODUCT_DESCRIPTION" HeaderText="Product Description" />
                                    <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />

                                    <%--<asp:BoundField DataField="RATE" HeaderText="Rate" />
                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />--%>

                                    <asp:BoundField DataField="CURRENCY_DESC" HeaderText="Currency" />
                                    <asp:BoundField DataField="CURRENCY_RATE" HeaderText="Currency Rate" />
                                    <asp:BoundField DataField="FC_RATE" HeaderText="FC Rate" />
                                    <asp:BoundField DataField="FC_BASIC_AMOUNT" HeaderText="FC Basic Amount" />
                                    <%--<asp:BoundField DataField="FC_OTHER_AMOUNT" HeaderText="FC Other Amount" />--%>

                                    <asp:BoundField DataField="INR_RATE" HeaderText="INR Rate" />
                                    <asp:BoundField DataField="INR_BASIC_AMOUNT" HeaderText="INR Basic Amount" />
                                    <asp:BoundField DataField="INR_GST_AMOUNT" HeaderText="INR GST Amount" />
                                    <%--<asp:BoundField DataField="INR_OTHER_AMOUNT" HeaderText="INR Other Amount" />--%>
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



                    <div class="full-width">
                        <asp:Panel ID="pnlAttachmentToAS" runat="server" Visible="false">

                            <div class="form-grid form-grid-2">

                                <div class="full-width button-group">

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>Attachment1:</label>
                                                <asp:FileUpload ID="uploadFileAttachmentToAS1" runat="server"
                                                    CssClass="form-control"
                                                    BorderStyle="Groove" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachmentToAS1" style="display: none;">
                                                    <asp:Label ID="lblfileAttachmentToAS1" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>Attachment2:</label>
                                                <asp:FileUpload ID="uploadFileAttachmentToAS2" runat="server"
                                                    CssClass="form-control"
                                                    BorderStyle="Groove" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachmentToAS2" style="display: none;">
                                                    <asp:Label ID="lblfileAttachmentToAS2" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div class="full-width button-group">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>Attachment3:</label>
                                                <asp:FileUpload ID="uploadFileAttachmentToAS3" runat="server"
                                                    CssClass="form-control"
                                                    BorderStyle="Groove" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachmentToAS3" style="display: none;">
                                                    <asp:Label ID="lblfileAttachmentToAS3" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>Attachment4:</label>
                                                <asp:FileUpload ID="uploadFileAttachmentToAS4" runat="server"
                                                    CssClass="form-control"
                                                    BorderStyle="Groove" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachmentToAS4" style="display: none;">
                                                    <asp:Label ID="lblfileAttachmentToAS4" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </div>

                                <div class="full-width button-group">

                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <label>Attachment5:</label>
                                                <asp:FileUpload ID="uploadFileAttachmentToAS5" runat="server"
                                                    CssClass="form-control"
                                                    BorderStyle="Groove" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachmentToAS5" style="display: none;">
                                                    <asp:Label ID="lblfileAttachmentToAS5" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <asp:Panel ID="pnlCloseMRNToAS" runat="server" Visible="false">
                                        <label>Close MRN:</label>
                                        <asp:CheckBox ID="chkCloseMRNToAS" runat="server" />
                                    </asp:Panel>

                                </div>

                            </div>
                        </asp:Panel>
                    </div>

                    <div class="full-width">
                        <asp:Panel ID="pnlRemarksToAS" runat="server" Visible="false">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksToAS" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />
                        </asp:Panel>
                    </div>

                    <div class="full-width">
                        <div class="employee-grid-container">

                            <div id="divAttachedFiles" runat="server">
                                <asp:GridView
                                    CssClass="employee-grid"
                                    ID="gvAttachedFiles"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CellPadding="4"
                                    ForeColor="#333333" GridLines="Both"
                                    Width="100%"
                                    HorizontalAlign="Center"
                                    OnRowDataBound="gvAttachedFiles_RowDataBound"
                                    OnRowCommand="gvAttachedFiles_RowCommand">
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl No."
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                                                <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />

                                                <asp:Label ID="lblAttachedFileName" runat="server" Visible="false" Text='<%# Eval("FILE_NAME") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remove"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                                    ImageUrl="~/Images/Icons/REMOVE03.png" Height="20px" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnAttachment1"
                                                    Height="20px"
                                                    Width="20px"
                                                    CommandArgument="ViewATTACHMENT1"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
                                        <asp:BoundField DataField="ATTACHED_BY" HeaderText="Attached By" />
                                        <asp:BoundField DataField="ATTACHED_ON" HeaderText="Attached On" />

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
                    </div>


                </div>

            </fieldset>
            <div class="full-width button-group">


                <asp:Button ID="btnSendToReauthorization" runat="server" Width="100%"
                    Text="Send Voucher To Reauthorization"
                    CssClass="button"
                    OnClick="btnSendToReauthorization_Click"
                    OnClientClick="return ConfirmReauth();" />

                <asp:Button ID="btnApprove" runat="server" Width="100%"
                    Text="Approve Voucher"
                    CssClass="button"
                    OnClick="btnApprove_Click"
                    OnClientClick="return ValidateAllFiles();" />

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


    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeViewVoucherInPDF" runat="server" TargetControlID="btnViewInPDF"
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
            id="iframeVoucherInPDF"
            runat="server"></iframe>
    </asp:Panel>


    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
