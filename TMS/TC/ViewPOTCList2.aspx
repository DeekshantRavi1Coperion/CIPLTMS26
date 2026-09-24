<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ViewPOTCList2.aspx.cs"
    Inherits="TC_ViewPOTCList2" Title="CIPLTMS- View PO TC List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: palegreen;*/
        }
    </style>

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


        function ValidateRemarks() {
            var Remarks = document.getElementById('<%=txtRemarksTUS.ClientID %>').value;
            if (Remarks == '') {
                document.getElementById('<%=txtRemarksTUS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRemarksTUS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAllSearch() {

            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }





        function ValidateAccept() {
            if (confirm("Would you like to accept TC?")) {
                document.getElementById('<%=hdAcceptConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdAcceptConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }




        function ValidateNotAccept() {
            var check = true;

            if (ValidateRemarks()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to not accept TC?")) {
                    document.getElementById('<%=hdNotAcceptConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdNotAcceptConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }

    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>



    <script type="text/javascript">        
        var GridId = "<%=gvPOList.ClientID %>";
        var ScrollHeight = 380;
        window.onload = function () {
            if (GridId != null) {

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
        }
    </script>



    <script type="text/javascript" language="javascript">

        function ValidatefileTC1ToEdit() {
            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileTC1ToEdit = document.getElementById('<%=fileTC1ToEdit.ClientID %>').value;
            var divfileTC1ToEdit = document.getElementById("divfileTC1ToEdit");
            var lblfileTC1ToEdit = document.getElementById('<%=lblfileTC1ToEdit.ClientID %>');



            if (fileTC1ToEdit == '') {
                document.getElementById('<%=fileTC1ToEdit.ClientID %>').style.borderColor = "#F7627F";
                divfileTC1ToEdit.style.display = "block";
                lblfileTC1ToEdit.innerHTML = "Please enter only .pdf file!";
                return true;
            }
            else {

                fileTC1ToEdit = fileTC1ToEdit.split(" ").join("")
                fileTC1ToEdit = fileTC1ToEdit.split("(").join("")
                fileTC1ToEdit = fileTC1ToEdit.split(")").join("")

                if (!regex.test(fileTC1ToEdit.toLowerCase())) {
                    document.getElementById('<%=fileTC1ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC1ToEdit.style.display = "block";
                    lblFileSiDrawing2.innerHTML = "Please enter only .pdf file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileTC1ToEdit.ClientID %>').style.borderColor = "";
                    divfileTC1ToEdit.style.display = "none";
                    lblfileTC1ToEdit.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileTC2ToEdit() {
            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileTC2ToEdit = document.getElementById('<%=fileTC2ToEdit.ClientID %>').value;
            var divfileTC2ToEdit = document.getElementById("divfileTC2ToEdit");
            var lblfileTC2ToEdit = document.getElementById('<%=lblfileTC2ToEdit.ClientID %>');


            if (fileTC2ToEdit == '') {
                document.getElementById('<%=fileTC2ToEdit.ClientID %>').style.borderColor = "";
                divfileTC2ToEdit.style.display = "none";
                lblfileTC2ToEdit.innerHTML = "";
                return false;
            }
            else {

                fileTC2ToEdit = fileTC2ToEdit.split(" ").join("")
                fileTC2ToEdit = fileTC2ToEdit.split("(").join("")
                fileTC2ToEdit = fileTC2ToEdit.split(")").join("")

                if (!regex.test(fileTC2ToEdit.toLowerCase())) {
                    document.getElementById('<%=fileTC2ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC2ToEdit.style.display = "block";
                    lblFileSiDrawing2.innerHTML = "Please enter only .pdf file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileTC2ToEdit.ClientID %>').style.borderColor = "";
                    divfileTC2ToEdit.style.display = "none";
                    lblfileTC2ToEdit.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileTC3ToEdit() {
            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileTC3ToEdit = document.getElementById('<%=fileTC3ToEdit.ClientID %>').value;
            var divfileTC3ToEdit = document.getElementById("divfileTC3ToEdit");
            var lblfileTC3ToEdit = document.getElementById('<%=lblfileTC3ToEdit.ClientID %>');


            if (fileTC3ToEdit == '') {
                document.getElementById('<%=fileTC3ToEdit.ClientID %>').style.borderColor = "";
                divfileTC3ToEdit.style.display = "none";
                lblfileTC3ToEdit.innerHTML = "";
                return false;
            }
            else {

                fileTC3ToEdit = fileTC3ToEdit.split(" ").join("")
                fileTC3ToEdit = fileTC3ToEdit.split("(").join("")
                fileTC3ToEdit = fileTC3ToEdit.split(")").join("")

                if (!regex.test(fileTC3ToEdit.toLowerCase())) {
                    document.getElementById('<%=fileTC3ToEdit.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC3ToEdit.style.display = "block";
                    lblFileSiDrawing2.innerHTML = "Please enter only .pdf file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileTC3ToEdit.ClientID %>').style.borderColor = "";
                    divfileTC3ToEdit.style.display = "none";
                    lblfileTC3ToEdit.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateEmployeeToEdit() {
            var AssignedTo = document.getElementById('<%=ddlEmployeeToEdit.ClientID %>').selectedIndex;
            if (AssignedTo == '' || AssignedTo == 0) {
                document.getElementById('<%=ddlEmployeeToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployeeToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAllUpload() {
            var check = true;

            if (ValidatefileTC1ToEdit()) {
                check = false;
            }

            if (ValidatefileTC2ToEdit()) {
                check = false;
            }

            if (ValidatefileTC3ToEdit()) {
                check = false;
            }

            if (ValidateEmployeeToEdit()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to upload TC?")) {
                    document.getElementById('<%=hdEditConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdEditConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdAcceptConfirmValue" runat="server" />
    <asp:HiddenField ID="hdNotAcceptConfirmValue" runat="server" />
    <asp:HiddenField ID="hdEditConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 70%">
            <legend style="text-align: center;">Procurement Status- TC list</legend>
            <table width="90%">
                <tr>
                    <td align="right">Start Date:</td>
                    <td style="width: 15%;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
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
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">End Date:</td>
                    <td style="width: 15%;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
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
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">PO No.:</td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtPONo" runat="server" Width="100%"></asp:TextBox>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">Vendor Name:</td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtVendorName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">JOB No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td align="right">Company:</td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td>&nbsp;</td>

                    <td align="right">Item Name:</td>
                    <td style="width: 15%;">
                        <asp:TextBox ID="txtItemName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td align="right">Status:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="26px" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td>Assigned To:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="26px" />
                    </td>

                    <td colspan="5">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAllSearch();" OnClick="btnSearch_Click" />
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <div align="center">
        <fieldset style="width: 90%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>
            <%--<div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>--%>
            <div id="gridContainer" style='overflow-y: hidden; overflow-x: scroll; width: 100%; height: 430px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvPOList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPOList_RowDataBound"
                    OnRowCommand="gvPOList_RowCommand">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnAccept" CommandArgument="ACCEPT_TC" ToolTip="Accept TC" runat="server"
                                    Text="Accept TC" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                                <asp:Button ID="btnEdit" CommandArgument="EDIT" ToolTip="Upload or change with new TC" runat="server"
                                    Text="Edit" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" Height="35px" Width="35px" Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnSendMail" CommandArgument="SEND_MAIL" ToolTip="Send Mail"
                                    runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TC1" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTCAttachment1" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgBtnTCAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                    ImageUrl="~/Images/pdficon3.png"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TC2" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTCAttachment2" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgBtnTCAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                    ImageUrl="~/Images/pdficon3.png"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="TC3" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblTCAttachment3" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT3_NAME") %>' />
                                <asp:ImageButton ID="imgBtnTCAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                    ImageUrl="~/Images/pdficon3.png"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PO No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                                <asp:Label ID="lblEmployeeRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                <asp:Label ID="lblCreatedByID" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />

                                <asp:Label ID="lblAcceptedBy" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_BY") %>' />
                                <asp:Label ID="lblNotAcceptedBy" runat="server" Visible="false" Text='<%# Eval("NOT_ACCEPTED_BY") %>' />

                                <asp:Label ID="lblIsAcceptedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_ACCEPTED_MAIL_SENT") %>' />
                                <asp:Label ID="lblIsNotAcceptedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_NOT_ACCEPTED_MAIL_SENT") %>' />


                                <asp:Label ID="lblPONo" runat="server" Visible="true" Text='<%# Eval("PO_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PO Date">
                            <ItemTemplate>
                                <asp:Label ID="lblPODate" runat="server" Visible="true" Text='<%# Eval("PO_DATE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vendor Name">
                            <ItemTemplate>
                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                <asp:Label ID="lblVendorName" runat="server" Visible="true" Text='<%# Eval("VENDOR_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JOB No.">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="lblPOFirstItemName" runat="server" Visible="true" Text='<%# Eval("ITEM_NAME") %>' />
                                <asp:Label ID="lblPOFirstItemCode" runat="server" Visible="false" Text='<%# Eval("ITEM_CODE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Assigned To">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                <asp:Label ID="lblUnit" runat="server" Visible="true" Text='<%# Eval("UNIT_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

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

    <%-- UPDATE STATUS START--%>
    <asp:Button ID="btnShowDetailFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeDetailTUS" runat="server" TargetControlID="btnShowDetailFile"
        PopupControlID="pnlViewDetailPopup" CancelControlID="imgBtnCancelDetailFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewDetailPopup" runat="server" BackColor="White" Height="500px" Width="980px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">Accept/Not Accept PO</legend>
            <table width="100%">
                <tr>
                    <td>PO No.:</td>
                    <td>
                        <asp:TextBox ID="txtPONoTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>PO Date:</td>
                    <td>
                        <asp:TextBox ID="txtPODateTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Vendor Name:</td>
                    <td colspan="4">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtVendorNameTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtVendorCodeTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Item Name:</td>
                    <td colspan="4">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtItemNameTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtItemCodeTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>JOB No.:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNoTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>Unit:</td>
                    <td>
                        <asp:TextBox ID="txtUnitTUS" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>









                <tr>
                    <td>TC1:</td>
                    <td>
                        <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                        <div>
                            <asp:Panel ID="pnlViewSiDrawingToEdit1" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdSiDrawingToEdit1" runat="server" Value="0" />
                                            <asp:TextBox ID="txtSiDrawingToEdit1" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewSiDrawingToEdit1" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit1_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit1" runat="server" Height="25px" Width="25px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit1_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadSiDrawingToEdit1" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdUploadSiDrawingToEdit1" runat="server" Value="0" />
                                            <asp:FileUpload ID="uploadFileSiDrawingToEdit1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />

                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit1" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndoSiDrawingToEdit1_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                    </td>

                    <td>&nbsp;</td>

                    <td>TC2:</td>
                    <td>
                        <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                        <div>
                            <asp:Panel ID="pnlViewSiDrawingToEdit2" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdSiDrawingToEdit2" runat="server" Value="0" />
                                            <asp:TextBox ID="txtSiDrawingToEdit2" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewSiDrawingToEdit2" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit2_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit2" runat="server" Height="25px" Width="25px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit2_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadSiDrawingToEdit2" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdUploadSiDrawingToEdit2" runat="server" Value="0" />
                                            <asp:FileUpload ID="uploadFileSiDrawingToEdit2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />

                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit2" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndoSiDrawingToEdit2_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <div id="divFileSiDrawingToEdit1" style="display: none;">
                            <asp:Label ID="lblFileSiDrawingToEdit1" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td>
                        <div id="divFileSiDrawingToEdit2" style="display: none;">
                            <asp:Label ID="lblFileSiDrawingToEdit2" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>TC3:</td>
                    <td>
                        <%--<asp:FileUpload ID="uploadFileSiDrawingToEdit3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />--%>

                        <div>
                            <asp:Panel ID="pnlViewSiDrawingToEdit3" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdSiDrawingToEdit3" runat="server" Value="0" />
                                            <asp:TextBox ID="txtSiDrawingToEdit3" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewSiDrawingToEdit3" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawingToEdit3_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveSiDrawingToEdit3" runat="server" Height="25px" Width="25px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawingToEdit3_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadSiDrawingToEdit3" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdUploadSiDrawingToEdit3" runat="server" Value="0" />
                                            <asp:FileUpload ID="uploadFileSiDrawingToEdit3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndoSiDrawingToEdit3" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndoSiDrawingToEdit3_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>

                    </td>

                    <td>&nbsp;</td>

                    <td>Assigned To:</td>
                    <td>
                        <asp:DropDownList ID="ddlEmployeeTUS" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateEmployeeTUS();" Enabled="false" />
                    </td>
                </tr>
                <tr style="visibility: hidden;">
                    <td>&nbsp;</td>
                    <td>
                        <div id="divFileSiDrawingToEdit3" style="display: none;">
                            <asp:Label ID="lblFileSiDrawingToEdit3" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td>
                        
                    </td>
                </tr>







































                <%--<tr>
                    <td>View TC1:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdViewAttachment1" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                        OnClick="btnViewAttachment1_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td>&nbsp;</td>

                    <td>View TC2:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtViewAttachment2" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdViewAttachment2" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnViewAttachment2" Height="20px" Width="20px" runat="server"
                                        OnClick="btnViewAttachment2_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td>&nbsp;</td>
                </tr>--%>

                <%--<tr>
                    <td>View TC3:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtViewAttachment3" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdViewAttachment3" runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnViewAttachment3" Height="20px" Width="20px" runat="server"
                                        OnClick="btnViewAttachment3_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                    <td>Assigned To:</td>
                    <td>
                        <asp:DropDownList ID="ddlEmployeeTUS" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateEmployeeTUS();" Enabled="false" />
                    </td>
                </tr>--%>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Remarks:</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtRemarksTUS" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <tr>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnAcceptTC" CssClass="button" Width="100%" runat="server" Text="Accept TC"
                                        OnClick="btnAcceptTC_Click" OnClientClick="return ValidateNotAccept();" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnNotAcceptTC" CssClass="button" Width="100%" runat="server" Text="Not Accept TC"
                                        OnClick="btnNotAcceptTC_Click" OnClientClick="return ValidateNotAccept();" />
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 95%;" align="center">
            </table>
        </fieldset>
    </asp:Panel>
    <%-- UPDATE STATUS END--%>



    <%-- EDIT START--%>
    <asp:Button ID="btnShowDetailFileToEdit" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeDetailToEdit" runat="server" TargetControlID="btnShowDetailFileToEdit"
        PopupControlID="pnlViewDetailPopupToEdit" CancelControlID="imgBtnCancelDetailFileToEdit" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewDetailPopupToEdit" runat="server" BackColor="White" Height="500px" Width="980px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDetailFileToEdit" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">Edit PO TC</legend>
            <table width="100%">
                <tr>
                    <td>PO No.:</td>
                    <td>
                        <asp:TextBox ID="txtPONoToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>PO Date:</td>
                    <td>
                        <asp:TextBox ID="txtPODateToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Vendor Name:</td>
                    <td colspan="4">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtVendorNameToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtVendorCodeToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Item Name:</td>
                    <td colspan="4">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 70%;">
                                    <asp:TextBox ID="txtItemNameToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtItemCodeToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>JOB No.:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNoToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>Unit:</td>
                    <td>
                        <asp:TextBox ID="txtUnitToEdit" runat="server" Width="100%" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Attach TC1:</td>
                    <td>
                        <asp:HiddenField ID="hdFileTC1ToEdit" runat="server" Value="0" />
                        <asp:FileUpload ID="fileTC1ToEdit" runat="server" Width="100%" Height="29px" BorderStyle="Groove"
                            onblur="return ValidateJOBNo();" />
                    </td>
                    <td>&nbsp;</td>
                    <td>Attach TC2:</td>
                    <td>
                        <asp:HiddenField ID="hdFileTC2ToEdit" runat="server" Value="0" />
                        <asp:FileUpload ID="fileTC2ToEdit" runat="server" Width="100%" Height="29px" BorderStyle="Groove"
                            onblur="return ValidateJOBNo();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <div id="divfileTC1ToEdit" style="display: none;">
                            <asp:Label ID="lblfileTC1ToEdit" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <div id="divfileTC2ToEdit" style="display: none;">
                            <asp:Label ID="lblfileTC2ToEdit" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>Attach TC3:</td>
                    <td>
                        <asp:HiddenField ID="hdFileTC3ToEdit" runat="server" Value="0" />
                        <asp:FileUpload ID="fileTC3ToEdit" runat="server" Width="100%" Height="29px" BorderStyle="Groove"
                            onblur="return ValidateJOBNo();" />
                    </td>
                    <td>&nbsp;</td>
                    <td>Assigned To:</td>
                    <td>
                        <asp:DropDownList ID="ddlEmployeeToEdit" runat="server" Width="100%" Height="26px"
                            onblur="return ValidateEmployeeToEdit();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <div id="divfileTC3ToEdit" style="display: none;">
                            <asp:Label ID="lblfileTC3ToEdit" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Remarks:</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtRemarksToEdit" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnUpload" CssClass="button" Width="100%" runat="server" Text="Upload TC"
                            OnClick="btnUpload_Click" OnClientClick="return ValidateAllUpload();" />
                    </td>
                </tr>
            </table>
            <table style="width: 95%;" align="center">
            </table>
        </fieldset>
    </asp:Panel>
    <%-- EDIT END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
