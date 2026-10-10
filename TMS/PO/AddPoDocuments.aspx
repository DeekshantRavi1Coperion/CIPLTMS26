<%@ Page Title="CIPLTMS - Add PO Documents" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddPoDocuments.aspx.cs" Inherits="PO_AddPoDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
    <link href="../Styles/filter.css" rel="stylesheet" />
    <link href="../Styles/grid.css" rel="stylesheet" />
    <link href="../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

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

    <script type="text/javascript" language="javascript">
        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>

    <%--<script type="text/javascript">
        var GridId = "<%=gvPOList.ClientID %>";
        var ScrollHeight = 570;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

        function ClearAllFilters() {

            document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtVendorCode.ClientID %>').value = "";
            document.getElementById('<%=txtVendorName.ClientID %>').value = "";

            document.getElementById('<%=ddlAttachedBy.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtPoNumber.ClientID %>').value = "";
            document.getElementById('<%=txtJobNumber.ClientID %>').value = "";

            return false;
        }

    </script>--%>

    <script type="text/javascript">
        function ClearAllFilters() {

            document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtVendorCode.ClientID %>').value = "";
            document.getElementById('<%=txtVendorName.ClientID %>').value = "";

            document.getElementById('<%=ddlAttachedBy.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=ddlCheckedBy.ClientID %>').selectedIndex = 0;
            document.getElementById('<%=txtPoNumber.ClientID %>').value = "";
            document.getElementById('<%=txtJobNumber.ClientID %>').value = "";
            document.getElementById('<%=txtMRNumber.ClientID %>').value = "";

            return false;
        }

    </script>



    <script type="text/javascript">

        function ValidateFileAttachment1() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG", ".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileAttachment1 = document.getElementById('<%=uploadFileAttachment1.ClientID %>').value;
            var divfileAttachment1 = document.getElementById("divfileAttachment1");
            var lblfileAttachment1 = document.getElementById('<%=lblfileAttachment1.ClientID %>');



            if (FileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "#F7627F";
                divfileAttachment1.style.display = "none";
                lblfileAttachment1.innerHTML = "";
                return true;
            }
            else {

                FileAttachment1 = FileAttachment1.split(" ").join("")
                FileAttachment1 = FileAttachment1.split("(").join("")
                FileAttachment1 = FileAttachment1.split(")").join("")

                if (!regex.test(FileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment1.style.display = "block";
                    lblfileAttachment1.innerHTML = "Please enter only Pdf or Image file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                    divfileAttachment1.style.display = "none";
                    lblfileAttachment1.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateAllFileAttachment1() {
            var check = true;

            if (ValidateFileAttachment1()) {
                check = false;
            }



            if (check) {
                if (confirm("Would you like to save attachment?")) {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "0";
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
    <asp:HiddenField ID="hdAttachment1ConfirmValue" runat="server" Value="0" />
    <%-- <asp:UpdatePanel ID="uppanel" runat="server">
        <contenttemplate>--%>

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters:</legend>

                <div class="form-filter-grid">

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

                    <label>Date Type:</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="PO Date" Value="PO_DATE"></asp:ListItem>
                        <asp:ListItem Text="Attached On" Value="ATTACHMENT1_ON"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
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
                            <td style="width:90%;">
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

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                    </asp:DropDownList>

                    <div class="full-width">
                        <label>Vendor Code/Name:</label>
                        <table width="100%;">
                            <tr>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <label>Is Document Attached:</label>
                    <asp:DropDownList ID="ddlDocumentAttached" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Both" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Attached" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Attached" Value="2"></asp:ListItem>
                    </asp:DropDownList>

                    <div class="full-width">
                        <label>PO Number:</label>
                        <asp:TextBox ID="txtPoNumber" runat="server" TextMode="MultiLine" Rows="10"
                            CssClass="form-control" />
                    </div>

                    <label>MR Number:</label>
                    <asp:TextBox ID="txtMRNumber" runat="server" CssClass="form-control">
                    </asp:TextBox>

                    <label>Job Number:</label>
                    <asp:TextBox ID="txtJobNumber" runat="server" CssClass="form-control">
                    </asp:TextBox>

                    <label>Attached By:</label>
                    <asp:DropDownList ID="ddlAttachedBy" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Checked By:</label>
                    <asp:DropDownList ID="ddlCheckedBy" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnSearch_Click" />
            </div>
        </div>


        <fieldset class="employee-grid-fieldset">
            <legend>Upload Singed PO:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">

                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>

                    <asp:GridView ID="gvPOList"
                        runat="server"
                        AutoGenerateColumns="false"
                        CellPadding="4"
                        CssClass="employee-grid"
                        ForeColor="#333333" GridLines="Both" Width="100%"
                        HorizontalAlign="Center"
                        OnRowCommand="gvPOList_RowCommand"
                        OnRowDataBound="gvPOList_RowDataBound">

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
                                    <asp:Label ID="lblPoFID" runat="server" Text='<%# Eval("PO_FID") %>' Visible="false" />
                                    <asp:Label ID="lblDocumentPID" runat="server" Text='<%# Eval("D_PID") %>' Visible="false" />
                                    <asp:Label ID="lblUnitID" runat="server" Text='<%# Eval("UNIT_ID") %>' Visible="false" />

                                    <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UNIT_NAME") %>' Visible="false" />
                                    <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("PO_NO") %>' Visible="false" />
                                    <asp:Label ID="lblMRNo" runat="server" Text='<%# Eval("MR_NO") %>' Visible="false" />
                                    <asp:Label ID="lblPoDate" runat="server" Text='<%# Eval("PO_DATE") %>' Visible="false" />
                                    <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JOB_NO") %>' Visible="false" />
                                    <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("VENDOR_CODE") %>' Visible="false" />
                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VENDOR_NAME") %>' Visible="false" />

                                    <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Zoom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>

                                    <table width="10%" id="tblViewDetail" runat="server">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnViewDetail" Height="30px" Width="30px"
                                                    CommandArgument="ViewDETAIL"
                                                    runat="server" ImageUrl="~/Images/viewdetails.png"
                                                    ToolTip="View Details" />
                                            </td>
                                            <td><b>[<asp:Label ID="lblDocumentsCount" runat="server" Text='<%# Eval("DOCUMENTS_COUNT") %>' />]</b></td>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Doc."
                                HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnAttachment1"
                                        Height="30px"
                                        Width="30px"
                                        CommandArgument="ViewATTACHMENT1"
                                        runat="server" />
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
                                        Height="35px"
                                        Width="35px"
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
                                        Height="35px"
                                        Width="35px"
                                        ToolTip="Download All Documents" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                            <asp:BoundField DataField="PO_NO" HeaderText="Po No" />
                            <asp:BoundField DataField="MR_NO" HeaderText="MR No" />

                            <asp:BoundField DataField="PO_DATE" HeaderText="Po Date" />
                            <asp:BoundField DataField="JOB_NO" HeaderText="Job No" />
                            <asp:BoundField DataField="VENDOR_CODE" HeaderText="Vendor Code" />
                            <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                            <%--<asp:BoundField DataField="DOCUMENTS_COUNT" HeaderText="Counts" />--%>
                            <asp:BoundField DataField="ATTACHMENT1_NAME" HeaderText="Default Attachment Name" />
                            <asp:BoundField DataField="ATTACHMENT1_ON" HeaderText="Attached On" />
                            <asp:BoundField DataField="ATTACHMENT1_BY" HeaderText="Attached By" />
                            <asp:BoundField DataField="CHECKED_BY" HeaderText="Checked By" />
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

        </fieldset>
    </div>



    <%-- SHOW DETAIL START--%>
    <asp:Button ID="btnShowPoDetailFile" runat="server" Style="display: none" />

    <asp:ModalPopupExtender
        ID="mpePoDetail"
        runat="server"
        TargetControlID="btnShowPoDetailFile"
        BehaviorID="mpePoDetailBID"
        PopupControlID="pnlViewPoDetailPopup"
        CancelControlID="imgBtnCancelPoDetailFile"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewPoDetailPopup" runat="server" CssClass="popup-edit">

        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPoDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblMRNDetailRecors" runat="server" Text="MRN Detailed Records[0]" />
                    </legend>
                    <div class="form-grid form-grid-3">

                        <label>Unit:</label>
                        <asp:TextBox ID="txtUnitToS" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Po No:</label>
                        <asp:TextBox ID="txtPoNoToS" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Po Date:</label>
                        <asp:TextBox ID="txtPoDateToS" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Job No:</label>
                        <asp:TextBox ID="txtJobNoToS" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor Name:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <asp:TextBox ID="txtVendorCodeToS" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendorNameToS" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>


                        <label>Attachment:</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="uploadFileAttachment1" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileAttachment1" style="display: none;">
                                        <asp:Label ID="lblfileAttachment1" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <div class="full-width">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksToS" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSaveAttachment" runat="server" Width="100%"
                        Text="Save Attachment"
                        CssClass="button"
                        OnClick="btnSaveAttachment_Click"
                        OnClientClick="return ValidateAllFileAttachment1();" />
                </div>
            </div>
            <div class="popup-grid-container">

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvAttachedPoList"
                    runat="server"
                    AutoGenerateColumns="false"
                    CellPadding="4"
                    ForeColor="#333333"
                    GridLines="Both"
                    PageSize="20"
                    Width="100%"
                    HorizontalAlign="Center"
                    AllowPaging="false"
                    OnRowDataBound="gvAttachedPoList_RowDataBound"
                    OnRowCommand="gvAttachedPoList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>

                        <asp:TemplateField HeaderText="Sl No."
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                                <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />

                                <asp:Label ID="lblAttachedAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                                <asp:Label ID="lblIsDefault" runat="server" Visible="false" Text='<%# Eval("IS_DEFAULT") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remove"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                    ImageUrl="~/Images/Icons/REMOVE03.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnAttachment1"
                                    Height="30px"
                                    Width="30px"
                                    CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ATTACHMENT1_NAME" HeaderText="File Name" />
                        <asp:BoundField DataField="ATTACHMENT1_BY" HeaderText="Attached By" />
                        <asp:BoundField DataField="ATTACHMENT1_ON" HeaderText="Attached On" />
                        <asp:BoundField DataField="ATTACHMENT1_REMARKS" HeaderText="Attached Remarks" />


                        <asp:TemplateField HeaderText="Is Default"
                            HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false" />
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
    <%-- SHOW SUBITEM DETAIL END--%>


    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender
        ID="ModalPopupExtender2"
        runat="server"
        TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup"
        CancelControlID="imgBtnCancelImgFile"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div cssclass="popup-img">
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
            cssclass="popup-iframe"
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
            cssclass="popup-iframe"
            id="iframeViewTravelStatementInPDF"
            runat="server"></iframe>
    </asp:Panel>


    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
