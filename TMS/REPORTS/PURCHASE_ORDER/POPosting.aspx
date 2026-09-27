<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="POPosting.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_POPosting" Title="PO Posting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        div.sticky {
            position: -webkit-sticky;
            position: sticky;
            top: 0;
            color: White;
            padding-left: 0px;
        }
    </style>

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



        function clientChangedFollowUpDateSearch(sender, args) {
            document.getElementById('<%=hdFollowUpDateSearch.ClientID %>').value = document.getElementById('<%=txtFollowUpDateSearch.ClientID %>').value;
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


        function PostingConfirmation() {
            if (confirm("Would you like to post?")) {
                document.getElementById('<%=hdPostingConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdPostingConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }

        function DeletionConfirmation() {
            if (confirm("Would you like to delete?")) {
                document.getElementById('<%=hdDeletionConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdDeletionConfirmValue.ClientID %>').value = "0";
                return false;
            }
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

        function EnableAmount2() {
            var sign = document.getElementById('<%=ddlSign.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtAmountOne.ClientID %>').value = "";
            document.getElementById('<%=txtAmountTwo.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 5) {
                document.getElementById('<%=txtAmountTwo.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtAmountTwo.ClientID %>').disabled = true;
            }
        }
    </script>

    <%--<script type="text/javascript">        
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
    </script>--%>

    <script type="text/javascript">
        function ClearFollowupDate() {
            document.getElementById('<%=hdFollowUpDateSearch.ClientID %>').value = "";
            document.getElementById('<%=txtFollowUpDateSearch.ClientID %>').value = "";
            document.getElementById('<%=chkClearFollowUpDate.ClientID %>').checked = false;
        }
    </script>




    <script type="text/javascript">  

        function Validate(lnkUpdate) {
            alert(lnkUpdate)
            var txtName, ddlCountries;

            //Find the GridView Row using the LinkButton reference.
            var row = lnkUpdate.parentNode.parentNode;

            //Fetch all controls in GridView Row.
            var controls = row.getElementsByTagName("*");

            //Loop through the fetched controls.
            for (var i = 0; i < controls.length; i++) {

                //Find the TextBox control.
                if (controls[i].id.indexOf("txtName") != -1) {
                    txtName = controls[i];
                }

                //Find the DropDownList control.
                if (controls[i].id.indexOf("ddlCountries") != -1) {
                    ddlCountries = controls[i];
                }
            }



            function NewCalculation1() {
                var grid = document.getElementById("<%= gvPOList.ClientID %>");

                //variable to contain the cell of the grid

                var cell;

                if (grid.rows.length > 0) {

                    //loop starts from 1. rows[0] points to the header.

                    for (i = 1; i < grid.rows.length; i++) {

                        //get the reference of first column

                        cell = grid.rows[i].cells[0];



                        //loop according to the number of childNodes in the cell

                        for (j = 0; j < cell.childNodes.length; j++) {

                            //if childNode type is radion



                            if (cell.childNodes[j].type == "text") {

                                //assign the status of the Select All checkbox to the cell radio within the grid



                                if (cellcollectin.childNodes[j].name.indexOf("txtDeliveryTermsAgreedWithSupplierInWeeks") > 1) {
                                    alert('hello')
                                }

                            }

                        }

                    }

                }

            }



            function NewCalculation() {
                var grid = document.getElementById("<%= gvPOList.ClientID%>");
                for (var i = 0; i < grid.rows.length - 1; i++) {
                    var txtAmountReceive = $("input[id*=txtDeliveryTermsAgreedWithSupplierInWeeks]")
                    if (txtAmountReceive[i].value != '') {
                        alert(txtAmountReceive[i].value);
                    }
                }
            }
    </script>

    <script type="text/Javascript" language="javascript">
            function Keydown() {
                alert('hello');
                var GV = document.getElementById("<%=gvPOList.ClientID %>");
                var row = txt.parentNode.parentNode;
                var rowIndex = row.rowIndex;
                GV.rows[rowIndex].cells[0].getElementsByTagName("INPUT")[0].checked = true;
            }

            function Calculation(event) {
                alert(event);
                alert(event.value);

                var grid = document.getElementById("<%= gvPOList.ClientID%>");
                alert(grid.rows.length)
                for (var i = 0; i < grid.rows.length - 1; i++) {
                    var txtAmountReceive = $("input[id*=txtDeliveryTermsAgreedWithSupplierInWeeks]")
                    alert(txtAmountReceive);
                    if (txtAmountReceive[i].value != '') {
                        alert(txtAmountReceive[i].value);
                    }
                }
            }


            function OnChange(event) {
                var GV = document.getElementById("<%=gvPOList.ClientID %>");
                var row = event.parentNode.parentNode;
                var rowIndex = row.rowIndex;
                alert(rowIndex)

                var val = GV.rows[rowIndex].cells[5].getElementsByTagName("INPUT").value;
                alert(val);
            }

        <%--//alert(GV.rows[rowIndex].cells[5].getElementsByTagName("INPUT")[5].value);
            //alert(GV.rows[rowIndex].cells[5].getElementById("<%=gvPOList.ClientID %>"));--%>

    </script>

    <script type="text/Javascript">

            function GetWeeks(el) {
                var row;

                //const pddCol = 3;
                //const wdDateCol = 27;
                //const weekCol = 27;
                //const importModeCol = 28;
                //const fdDateCol = 28;

                const pddCol = 3;
                const wdDateCol = 27;
                const weekCol = 28;
                const importModeCol = 29;
                const fdDateCol = 30;


                var poDeliveryDate;
                var vendorDate;
                var weeks = 0;
                var importModeId = 0;
                var buffer = 0;
                var finalDate;
                var days = 0;

                row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                var gvPOList = document.getElementById('<%=gvPOList.ClientID %>')

                poDeliveryDate = row.cells[pddCol].innerText;
                vendorDate = row.cells[wdDateCol].getElementsByTagName("input")[0].value;


                if (row.cells[weekCol].getElementsByTagName("input")[0].value != '') {
                    weeks = row.cells[weekCol].getElementsByTagName("input")[0].value;
                }

                if (weeks > 0) {
                    days = (weeks * 7);
                }



                if (el == '[object HTMLSelectElement]') {
                    importModeId = el.options[el.selectedIndex].value;
                    document.getElementById('<%=hdImportModeVal.ClientID %>').value = importModeId;
                }


                if (parseInt(document.getElementById('<%=hdImportModeVal.ClientID %>').value) > 0) {
                    importModeId = document.getElementById('<%=hdImportModeVal.ClientID %>').value;
                }


                if (importModeId == 1) {
                    buffer = 20;
                }
                else if (importModeId == 2) {
                    buffer = 60;
                }
                else {
                    buffer = 10;
                }


                if (days > 0) {
                    days = days + buffer;
                }
                else {
                    days = buffer;
                }


                //alert(vendorDate + ', days: ' + days + ', importModeId: ' + importModeId + ', buffer: ' + buffer);

                if (vendorDate == '') {
                    vendorDate = poDeliveryDate;
                }


                //if (vendorDate != '' && weeks > 0) {
                if (vendorDate != '' && days > 0) {
                    var result = new Date(vendorDate);
                    result.setDate(result.getDate() + days);

                    finalDate = GetFormattedDate(result)
                }
                else {
                    finalDate = vendorDate;
                }

                row.cells[fdDateCol].getElementsByTagName("input")[0].value = finalDate;
            }

            function GetFormattedDate(result) {
                var d = new Date(result),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                var monthTxt = GetMonthName(month);

                if (day.length < 2)
                    day = '0' + day;

                var finalDate = [day, monthTxt, year].join('-');

                return finalDate;
            }

            function GetMonthName(month) {
                var monthTxt;

                if (month == 1) {
                    monthTxt = "Jan";
                }
                else if (month == 2) {
                    monthTxt = "Feb";
                }
                else if (month == 3) {
                    monthTxt = "Mar";
                }
                else if (month == 4) {
                    monthTxt = "Apr";
                }
                else if (month == 5) {
                    monthTxt = "May";
                }
                else if (month == 6) {
                    monthTxt = "Jun";
                }
                else if (month == 7) {
                    monthTxt = "Jul";
                }
                else if (month == 8) {
                    monthTxt = "Aug";
                }
                else if (month == 9) {
                    monthTxt = "Sep";
                }
                else if (month == 10) {
                    monthTxt = "Oct";
                }
                else if (month == 11) {
                    monthTxt = "Nov";
                }
                else if (month == 12) {
                    monthTxt = "Dec";
                }
                return monthTxt;

            }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">


    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdPostingConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDeletionConfirmValue" runat="server" />
    <asp:HiddenField ID="hdImportModeVal" runat="server" Value="0" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Procurement Status- Procurement View:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                    - 
                Select All
                <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" AutoPostBack="true"
                    OnCheckedChanged="chkSelectAll_CheckedChanged" />

                </legend>

                <div class="form-grid form-grid-3">

                    <label>Date Type</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="PO Date" Value="1" />
                        <asp:ListItem Text="PO Delivery Date" Value="2" />
                    </asp:DropDownList>

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch"
                                    Format="dd-MMM-yyyy"
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
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch"
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>PO No.</label>
                    <asp:TextBox ID="txtPONo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Vendor Name</label>
                    <asp:TextBox ID="txtVendorName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />

                    <label>Follow-Up By</label>
                    <asp:DropDownList ID="ddlFollowUpBy" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="UG" Value="UG" />
                        <asp:ListItem Text="PKS" Value="PKS" />
                        <asp:ListItem Text="E&I" Value="E&I" />
                        <asp:ListItem Text="PREET" Value="PREET" />
                    </asp:DropDownList>

                    <label>Follow-Up Date</label>
                    <table width="100%">
                        <tr>

                            <td>
                                <asp:TextBox ID="txtFollowUpDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdFollowUpDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarFollowUpDateSearch" PopupButtonID="imgbtnFollowUpDateSearch"
                                    runat="server" TargetControlID="txtFollowUpDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedFollowUpDateSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnFollowUpDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkClearFollowUpDate" runat="server"
                                    onclick="ClearFollowupDate();" />
                            </td>
                        </tr>
                    </table>


                    <label>Posting Status</label>
                    <asp:DropDownList ID="ddlPostingStatusSearch" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="Open" Value="Open" />
                        <asp:ListItem Text="Close" Value="Close" />
                    </asp:DropDownList>

                    <label>Follow-Up</label>
                    <asp:DropDownList ID="ddlFollowUp" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Less Than Last 14 Days" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Greater Than Last 14 Days" Value="2"></asp:ListItem>
                    </asp:DropDownList>

                    <label>MR No.</label>
                    <asp:TextBox ID="txtMRNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>MR-Created-By</label>
                    <asp:DropDownList ID="ddlMRCreatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>



                    <div class="full-width">
                        <label>Amount</label>

                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <asp:DropDownList ID="ddlSign" runat="server"
                                        CssClass="form-control"
                                        onchange="EnableAmount2()">
                                        <asp:ListItem Text=">=" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=">" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="<=" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="<" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="=" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="BETWEEN" Value="6"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 30%">
                                    <asp:TextBox ID="txtAmountOne" runat="server"
                                        CssClass="form-control"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                                <td style="width: 30%">
                                    <asp:TextBox ID="txtAmountTwo" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"
                                        onkeypress="return inNumberKeyWithDecimal(this, event);">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>

                    </div>


                    <label>Exclude CIDF</label>
                    <asp:CheckBox ID="chkExcludeCIDF" runat="server" />

                    <label>Exclude Engg. Service</label>
                    <asp:CheckBox ID="chkExcludeEngineeringService" runat="server" />


                    <label>PO Checked By</label>
                    <asp:DropDownList ID="ddlCheckedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Total PO Value [INR]</label>
                    <asp:Label ID="lblTotalInvoiceINR" runat="server" Text="0" Font-Bold="true" Font-Size="Larger" ForeColor="Green" />


                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />


                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />


                    <asp:Button ID="btnPost" CssClass="button" Width="100%" runat="server" Text="Post"
                        OnClientClick="return PostingConfirmation();" OnClick="btnPost_Click" />

                    <asp:Button ID="btnDelete" CssClass="button" Width="100%" runat="server" Text="Delete"
                        OnClientClick="return DeletionConfirmation();" OnClick="btnDelete_Click" />


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
                ID="gvPOList" runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" HorizontalAlign="Center"
                OnRowDataBound="gvPOList_RowDataBound"
                OnRowCommand="gvPOList_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDETAIL" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View Subitem Details" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Visible="false" Text='<%# Eval("SR_NO") %>' />
                            <asp:Label ID="lblFollowUpDays" runat="server" Visible="false" Text='<%# Eval("FOLLOW_UP_DAYS") %>' />
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblTableName" runat="server" Visible="false" Text='<%# Eval("TABLE_NAME") %>' />
                            <asp:Label ID="lblMRCreatedBy" runat="server" Visible="false" Text='<%# Eval("MR_CREATED_BY") %>' Width="150px" />

                            <asp:Label ID="lblPONo" runat="server" Visible="true" Text='<%# Eval("PO_NO") %>'
                                Width="130px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblPODate" runat="server" Visible="true" Text='<%# Eval("PO_DATE") %>'
                                Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--3--%>
                    <asp:TemplateField HeaderText="PO_DELIVERY_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblPODeliveryDate" runat="server" Visible="true" Text='<%# Eval("PO_DELIVERY_DATE") %>'
                                Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VENDOR_NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                            <asp:Label ID="lblVendorName" runat="server" Visible="true" Text='<%# Eval("VENDOR_NAME") %>'
                                Width="350px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--5--%>
                    <asp:TemplateField HeaderText="ITEM_NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemName" runat="server" Text='<%# Eval("ITEM_NAME") %>'
                                CssClass="textbox1" Wrap="true" TextMode="MultiLine" Rows="3"
                                Width="300px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="QUANTITY">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblService" runat="server" Visible="false" Text='<%# Eval("SERVICE") %>' />--%>
                            <asp:TextBox ID="txtQuantity" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("QUANTITY") %>' CssClass="textbox"
                                Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="BALANCE_QUANTITY">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBalQuantity" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("BAL_QUANTITY") %>' CssClass="textbox"
                                Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Visible="true" Text='<%# Eval("UOM") %>'
                                Width="60px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_VALUE_INR">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPOValueINR" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("PO_VALUE_INR") %>' CssClass="textbox"
                                Width="120px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--10--%>
                    <asp:TemplateField HeaderText="JOB_NO">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblCompleteJOBNo" runat="server" Visible="false" Text='<%# Eval("COMPLETE_JOB_NO") %>' Width="150px" />--%>
                            <asp:Label ID="lblJOBNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO") %>'
                                Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="BUDGET">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBudget" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("BUDGET") %>' CssClass="textbox" Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="FOLLOW_UP_BY">
                        <ItemTemplate>
                            <asp:Label ID="lblFollowUpBy" runat="server" Visible="true" Text='<%# Eval("FOLLOW_UP_BY") %>'
                                Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SELECT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" Width="60px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LOCATION">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                            <asp:Label ID="lblLocation" runat="server" Visible="true" Text='<%# Eval("LOCATION") %>'
                                Width="80px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--15--%>
                    <asp:TemplateField HeaderText="REVISION">
                        <ItemTemplate>
                            <asp:Label ID="lblRevision" runat="server" Visible="true" Text='<%# Eval("REVISION") %>'
                                Width="150px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_FIRST_ITEM">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPOFirstItem" runat="server" Text='<%# Eval("PO_FIRST_ITEM") %>'
                                TextMode="MultiLine" Rows="3" onkeyDown="javascript:preventInput(event);" CssClass="textbox1"
                                Width="300px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ENGG_APPROVAL_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblEnggApprovalStatus" runat="server" Visible="false" Text='<%# Eval("ENGG_APPROVAL_STATUS") %>' />
                            <asp:DropDownList ID="ddlEnggApprovalStatus" runat="server" Height="26px" Width="150px">
                                <asp:ListItem Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Not Approved" Value="Not Approved" />
                                <asp:ListItem Text="Not Applicable" Value="Not Applicable" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRESENT_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblPresentStatus" runat="server" Visible="false" Text='<%# Eval("PRESENT_STATUS") %>' />
                            <asp:TextBox ID="txtPresentStatus" runat="server" Text='<%# Eval("PRESENT_STATUS") %>'
                                TextMode="MultiLine" Rows="3"
                                CssClass="textbox1" Width="300px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblEdOfInspection" runat="server" Visible="false" Text='<%# Eval("ED_OF_INSP/COMP") %>' />
                            <asp:TextBox ID="txtEdOfInspection" runat="server" Text='<%# Eval("ED_OF_INSP/COMP") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender ID="calendarEdOfInspection" PopupButtonID="imgbtnEdOfInspection"
                                runat="server" TargetControlID="txtEdOfInspection" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnEdOfInspection" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--20--%>
                    <asp:TemplateField HeaderText="POSTING_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblPostingStatus" runat="server" Visible="false" Text='<%# Eval("POSTING_STATUS") %>' />
                            <asp:DropDownList ID="ddlPostingStatus" runat="server" Height="26px" Width="100px">
                                <asp:ListItem Text="Open" Value="Open" />
                                <asp:ListItem Text="Close" Value="Close" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NEXT_FOLLOWUP_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblNextFollowupDate" runat="server" Visible="false" Text='<%# Eval("NEXT_FOLLOWUP_DATE") %>' />
                            <asp:TextBox ID="txtNextFollowupDate" runat="server" Text='<%# Eval("NEXT_FOLLOWUP_DATE") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender ID="calendarNextFollowupDate" PopupButtonID="imgbtnNextFollowupDate"
                                runat="server" TargetControlID="txtNextFollowupDate" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnNextFollowupDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="LAST_STATUS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLastStatus" runat="server" Text='<%# Eval("LAST_STATUS") %>'
                                TextMode="MultiLine" Rows="3"
                                onkeyDown="javascript:preventInput(event);" Enabled="false"
                                CssClass="textbox1" Width="300px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LAST_ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblLastEdOfInspection" runat="server" Visible="true"
                                Text='<%# Eval("LAST_ED_OF_INSP/COMP") %>' Width="300px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="LAST_FOLLOW_UP_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblLastModifiedDate" runat="server" Visible="true"
                                Text='<%# Eval("LAST_FOLLOW_UP_DATE") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="GAD_SENT_1" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADSENT1" runat="server" Visible="FALSE" Text='<%# Eval("GAD_GIVEN_1") %>' />
                <asp:TextBox ID="txtGADsent1" runat="server" Text='<%# Eval("GAD_GIVEN_1") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADsent1" PopupButtonID="imgbtnGADsent1"
                    runat="server" TargetControlID="txtGADsent1" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADsent1" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Sent 1" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:TemplateField HeaderText="GAD_RECEIVED_1" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADREC1" runat="server" Visible="false" Text='<%# Eval("GAD_RECEIVED_1") %>' />
                <asp:TextBox ID="txtGADrec1" runat="server" Text='<%# Eval("GAD_RECEIVED_1") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADrec1" PopupButtonID="imgbtnGADrec1"
                    runat="server" TargetControlID="txtGADrec1" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADrec1" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Received 1" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="GAD_SENT_2" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADSENT2" runat="server" Visible="false" Text='<%# Eval("GAD_GIVEN_2") %>' />
                <asp:TextBox ID="txtGADsent2" runat="server" Text='<%# Eval("GAD_GIVEN_2") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADsent2" PopupButtonID="imgbtnGADsent2"
                    runat="server" TargetControlID="txtGADsent2" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADsent2" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Sent 2" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="GAD_RECEIVED_2" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADREC2" runat="server" Visible="false" Text='<%# Eval("GAD_RECEIVED_2") %>' />
                <asp:TextBox ID="txtGADrec2" runat="server" Text='<%# Eval("GAD_RECEIVED_2") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADrec2" PopupButtonID="imgbtnGADrec2"
                    runat="server" TargetControlID="txtGADrec2" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADrec2" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Received 2" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>

     
        <asp:TemplateField HeaderText="GAD_SENT_3" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADSENT3" runat="server" Visible="false" Text='<%# Eval("GAD_GIVEN_3") %>' />
                <asp:TextBox ID="txtGADsent3" runat="server" Text='<%# Eval("GAD_GIVEN_3") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADsent3" PopupButtonID="imgbtnGADsent3"
                    runat="server" TargetControlID="txtGADsent3" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADsent3" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Sent 3" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="GAD_RECEIVED_3" ItemStyle-Width="150px">
            <ItemTemplate>
                <asp:Label ID="lblGADREC3" runat="server" Visible="false" Text='<%# Eval("GAD_RECEIVED_3") %>' />
                <asp:TextBox ID="txtGADrec3" runat="server" Text='<%# Eval("GAD_RECEIVED_3") %>'
                    onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="calendarNextGADrec3" PopupButtonID="imgbtnGADrec3"
                    runat="server" TargetControlID="txtGADrec3" Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:ImageButton ID="imgbtnGADrec3" runat="server" ImageUrl="~/Images/Calendar2.png" ToolTip="GAD Received 3" Width="20px" />
            </ItemTemplate>
        </asp:TemplateField>



                    <%--25--%>
                    <asp:TemplateField HeaderText="OA_RECEIVED">
                        <ItemTemplate>
                            <asp:Label ID="lblOAReceived" runat="server" Visible="false" Text='<%# Eval("OA_RECEIVED") %>' />
                            <asp:TextBox ID="txtOnReceived" runat="server" Text='<%# Eval("OA_RECEIVED") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender ID="calendarOnReceived" PopupButtonID="imgbtnOnReceived"
                                runat="server" TargetControlID="txtOnReceived" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnOnReceived" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="On Received Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DATE_OF_DRAWING_RECEIVED_FROM_VENDOR">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfDrawingReceivedFromVendor" runat="server" Visible="false" Text='<%# Eval("DATE_OF_DRAWING_RECEIVED_FROM_VENDOR") %>' />
                            <asp:TextBox ID="txtDateOfDrawingReceivedFromVendor" runat="server" Text='<%# Eval("DATE_OF_DRAWING_RECEIVED_FROM_VENDOR") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender ID="calendarDateOfDrawingReceivedFromVendor" PopupButtonID="imgbtnDateOfDrawingReceivedFromVendor"
                                runat="server" TargetControlID="txtDateOfDrawingReceivedFromVendor" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnDateOfDrawingReceivedFromVendor" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Date Of Drawing Received From Vendor Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--27--%>
                    <asp:TemplateField HeaderText="DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfApprovedDrawingSentToVendor" runat="server" Visible="false"
                                Text='<%# Eval("DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR") %>' />
                            <asp:TextBox ID="txtDateOfApprovedDrawingSentToVendor" runat="server"
                                Text='<%# Eval("DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" Width="100px"
                                onChange="GetWeeks(this)">
                                    <%--OnTextChanged="txtDateOfApprovedDrawingSentToVendor_OnTextChanged"
                                    AutoPostBack="true"--%>
                            </asp:TextBox>
                            <asp:CalendarExtender ID="calendarDateOfApprovedDrawingSentToVendor" PopupButtonID="imgbtnDateOfApprovedDrawingSentToVendor"
                                runat="server" TargetControlID="txtDateOfApprovedDrawingSentToVendor" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnDateOfApprovedDrawingSentToVendor" runat="server"
                                ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Date Of Approved Drawing Sent To Vendor Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDeliveryTermsAgreedWithSupplierInWeeks" runat="server"
                                Text='<%# Eval("DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS") %>'
                                onkeypress="return inNumberKeyWithDecimal(this, event);" CssClass="textbox1"
                                onChange="GetWeeks(this)"
                                Width="100px"></asp:TextBox>

                            <%--onKeyUp="OnChange(this);"
                                    OnTextChanged="txtDeliveryTermsAgreedWithSupplierInWeeks_OnTextChanged"
                                    AutoPostBack="true"--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IMPORT_MODE">
                        <ItemTemplate>
                            <asp:Label ID="lblImportMode" runat="server" Visible="false" Text='<%# Eval("IMPORT_MODE_ID") %>' />
                            <asp:DropDownList ID="ddlImportMode" runat="server" Height="26px" Width="180px"
                                onChange="GetWeeks(this)">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFinalDeliveryDateAsPerAgreedTerms" runat="server"
                                Text='<%# Eval("FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS") %>'
                                onkeyDown="javascript:preventInput(event);" Enabled="false"
                                CssClass="textbox1" Width="120px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--30--%>
                    <asp:TemplateField HeaderText="NON_NEGOTIABLE_OFFERED_PRICE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNonNegotiableOfferedPrice" runat="server"
                                onkeypress="return inNumberKeyWithDecimal(this, event);"
                                Text='<%# Eval("NON_NEGOTIABLE_OFFERED_PRICE") %>'
                                CssClass="textbox" Width="120px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CHECKED_BY">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckedBy" runat="server" Visible="true"
                                Text='<%# Eval("CHECKED_BY") %>' Width="100px" />
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

    </div>


    <%-- SHOW DETAIL START--%>
    <asp:Button ID="btnShowDetailFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeDetail" runat="server" TargetControlID="btnShowDetailFile"
        PopupControlID="pnlViewDetailPopup" CancelControlID="imgBtnCancelDetailFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewDetailPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">
            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>PO Detail:
                        <asp:Label ID="lblPODetailRerords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-2">
                        <label>Total Quantity</label>
                        <asp:TextBox ID="txtTotalQuantity" runat="server" Width="100%" Enabled="false" />

                        <label>Received Quantity</label>
                        <asp:TextBox ID="txtTotalRecQuantity" runat="server" Width="100%" Enabled="false" />

                        <label>Balance Quantity</label>
                        <asp:TextBox ID="txtTotalBalQuantity" runat="server" Width="100%" Enabled="false" />

                        &nbsp;
                        <asp:Button ID="btnExportPODetails" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExportPODetails_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">
                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvPODetailsList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPODetailsList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="PO_NO">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PO_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MR_NO">
                            <ItemTemplate>
                                <asp:Label ID="lblMRNo" runat="server" Text='<%# Eval("MR_NO") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRODUCT_CODE">
                            <ItemTemplate>
                                <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRODUCT">
                            <ItemTemplate>
                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCT") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="100%"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RECEIVED_QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRecQuantity" runat="server" Text='<%# Eval("REC_QUANTITY") %>' Width="100%"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="BALANCE_QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBalQuantity" runat="server" Text='<%# Eval("BAL_QUANTITY") %>' Width="100%"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
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
    <%-- SHOW DETAIL END--%>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
