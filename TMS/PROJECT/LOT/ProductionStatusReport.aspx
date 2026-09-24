<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ProductionStatusReport.aspx.cs"
    Inherits="PROJECT_LOT_ProductionStatusReport" Title="Production Status Report" %>

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
        .textboxcenter {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxright {
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

        .textboxper {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: lightgreen;
        }

        .textboxleft {
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

        function ValidateAll() {

            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }




        function PostingPlanningRemarkConfirmation() {
            if (confirm("Would you like to post planning remarks?")) {
                document.getElementById('<%=hdPostingPlanningRemarksConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdPostingPlanningRemarksConfirmValue.ClientID %>').value = "0";
                return false;
            }
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

        function EnableEndDateSearch() {
            var sign = document.getElementById('<%=ddlSign.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 0) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = false;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }
        }
    </script>

    <script type="text/javascript">        
        var GridId = "<%=gvFabricationList.ClientID %>";
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



    <script type="text/Javascript">

        function checkForPercentage(el) {

            var row = el.parentNode.parentNode;

            var quantityCol = 12;
            var inFitupInsQuantityCol = 13;
            var percentageCol = 18;

            var quantity = row.cells[quantityCol].getElementsByTagName("input")[0].value;
            var inFitupInsQuantity = row.cells[inFitupInsQuantityCol].getElementsByTagName("input")[0].value;
            var percentage = row.cells[percentageCol].getElementsByTagName("input")[0].value;

            if (percentage == '') {
                row.cells[percentageCol].getElementsByTagName("input")[0].value = '0';
            }
            else {
                if (parseInt(percentage) > 100) {
                    row.cells[percentageCol].getElementsByTagName("input")[0].value = '100';

                    if (inFitupInsQuantity < quantity) {
                        if (row.cells[percentageCol].getElementsByTagName("input")[0].value == '100') {
                            row.cells[percentageCol].getElementsByTagName("input")[0].value = '0';
                        }
                    }

                }
                else {
                    if (inFitupInsQuantity < quantity) {
                        if (row.cells[percentageCol].getElementsByTagName("input")[0].value == '100') {
                            row.cells[percentageCol].getElementsByTagName("input")[0].value = '0';
                        }
                    }
                }
            }
        }
    </script>

    <%--<script type="text/Javascript">

            function checkForPercentage(el) {

                var row = el.parentNode.parentNode;

                var quantity = row.cells[11].getElementsByTagName("input")[0].value;
                var inFitupInsQuantity = row.cells[12].getElementsByTagName("input")[0].value;
                var percentage = row.cells[16].getElementsByTagName("input")[0].value;

                if (percentage == '') {
                    row.cells[16].getElementsByTagName("input")[0].value = '0';
                }
                else {
                    if (parseInt(percentage) > 100) {
                        row.cells[16].getElementsByTagName("input")[0].value = '100';

                        if (inFitupInsQuantity < quantity) {
                            if (row.cells[16].getElementsByTagName("input")[0].value == '100') {
                                row.cells[16].getElementsByTagName("input")[0].value = '0';
                            }
                        }

                    }
                    else {
                        if (inFitupInsQuantity < quantity) {
                            if (row.cells[16].getElementsByTagName("input")[0].value == '100') {
                                row.cells[16].getElementsByTagName("input")[0].value = '0';
                            }
                        }
                    }
                }
            }
        </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdPostingConfirmValue" runat="server" />
    <asp:HiddenField ID="hdPostingPlanningRemarksConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDeletionConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
            <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Production Status Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width">

                        <label>Date Filter</label>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlOnWhichDate" runat="server" 
                                        CssClass="form-control">
                                        <asp:ListItem Text="Production Order Date" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Production Order Delivery Date" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSign" runat="server" 
                                        CssClass="form-control"
                                        onchange="EnableEndDateSearch()">
                                        <asp:ListItem Text="BETWEEN" Value="BETWEEN"></asp:ListItem>
                                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" 
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" 
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" 
                                    ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date</label>

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"/>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" 
                                    PopupButtonID="imgbtnEndDateSearch"
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


                    <label>Production Order No.</label>
                    <asp:TextBox ID="txtProductionOrderNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Drawing No.</label>
                    <asp:TextBox ID="txtDrawingNo" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlUnit" runat="server" 
                        CssClass="form-control"/>
                    <%-- AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" --%>

                    <label>Product Code:</label>
                    <asp:TextBox ID="txtProductCode" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>Equipment/Item</label>
                    <asp:TextBox ID="txtEquipment" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>LOT No.</label>
                    <asp:TextBox ID="txtLOTNo" runat="server" 
                        CssClass="form-control"></asp:TextBox>


                    <label>Posting Status</label>
                    <asp:DropDownList ID="ddlPostingStatus" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="Open" Value="Open" />
                        <asp:ListItem Text="Close" Value="Close" />
                    </asp:DropDownList>

                    <label>Present (%) Of Work Done</label>
                    <asp:TextBox ID="txtPresentPercOfWorkDone" runat="server" 
                        CssClass="form-control"></asp:TextBox>


                    <label>Production Manager</label>
                    <asp:DropDownList ID="ddlProductionManager" runat="server" 
                        CssClass="form-control"/>


                    <label>Select All</label>
                    <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" AutoPostBack="true"
                        OnCheckedChanged="chkSelectAll_CheckedChanged" />


                    <label>Select Outstanding ED Date of Insp</label>
                    <asp:CheckBox ID="chkOutstandingEDDate" Checked="false" runat="server" AutoPostBack="true"
                        OnCheckedChanged="chkOutstandingEDDate_CheckedChanged" />


                    <div class="full-width button-group">

                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />

                        <asp:Button ID="btnPostPlanningRemarks" CssClass="button" Width="100%" runat="server"
                            Text="Post Planning Remarks"
                            OnClientClick="return PostingPlanningRemarkConfirmation();"
                            OnClick="btnPostPlanningRemarks_Click" />

                        <asp:Button ID="btnPost" CssClass="button" Width="100%" runat="server" Text="Post"
                            OnClientClick="return PostingConfirmation();" OnClick="btnPost_Click" />

                        <asp:Button ID="btnDelete" CssClass="button" Width="100%" runat="server" Text="Delete"
                            OnClientClick="return DeletionConfirmation();" OnClick="btnDelete_Click" />

                    </div>
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
                ID="gvFabricationList" runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvFabricationList_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="SR_NO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSRNo" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("SR_NO") %>' CssClass="textboxcenter"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UNIT">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                            <asp:Label ID="lblTableName" runat="server" Visible="false" Text='<%# Eval("TABLE_NAME") %>' />
                            <asp:Label ID="lblUnit" runat="server" Visible="true" Text='<%# Eval("UNIT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LOT_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblLOTNo" runat="server" Visible="true" Text='<%# Eval("LOT_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Visible="true" Text='<%# Eval("STATUS_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblShortJOBNo" runat="server" Visible="false" Text='<%# Eval("SHORT_JOB_NO") %>' />
                            <asp:Label ID="lblJOBNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LOT_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblLOTDate" runat="server" Visible="true" Text='<%# Eval("LOT_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRODUCTION_ORDER_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionOrderNo" runat="server" Visible="true" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRODUCTION_ORDER_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionOrderDate" runat="server" Visible="true" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRODUCTION_ORDER_DELIVERY_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionOrderDeliveryDate" runat="server" Visible="true" Text='<%# Eval("PRODUCTION_ORDER_DELIVERY_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRODUCT_CODE">
                        <ItemTemplate>
                            <asp:Label ID="lblProductCode" runat="server" Visible="true" Text='<%# Eval("PRODUCT_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="EQUIPMENT/ITEM">
                        <ItemTemplate>
                            <asp:Label ID="lblEquipment" runat="server" Visible="true" Text='<%# Eval("EQUIPMENT/ITEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Visible="true" Text='<%# Eval("UOM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="QUANTITY">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("QUANTITY") %>' CssClass="textboxright"
                                onpaste="return false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IN FITUP INSP QUANTITY">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInFitupInspQuantity" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("IN_FITUP_INSP_QUANTITY") %>' CssClass="textboxright"
                                onpaste="return false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SELECT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DRAWING_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblDrawingNo" runat="server" Visible="true" Text='<%# Eval("DRAWING_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PLANNING_REMARKS">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanningRemarks" runat="server" Visible="false" Text='<%# Eval("PLANNING_REMARKS") %>' />
                            <asp:TextBox ID="txtPlanningRemarks" runat="server" Width="300px" Text='<%# Eval("PLANNING_REMARKS") %>'
                                TextMode="MultiLine" Rows="3" CssClass="textboxleft"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%----------------------------------------------------------------------------------------------%>
                    <%--<asp:TemplateField HeaderText="REMARKS_FOR_PROCUREMENT">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarksForProcurement" runat="server" Visible="false" Text='<%# Eval("REMARKS_FOR_PROCUREMENT") %>' />
                                    <asp:TextBox ID="txtRemarksForProcurement" runat="server" Width="300px" Text='<%# Eval("REMARKS_FOR_PROCUREMENT") %>'
                                        TextMode="MultiLine" Rows="3" CssClass="textboxleft"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <%----------------------------------------------------------------------------------------------%>
                    <asp:TemplateField HeaderText="PRESENT_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblPresentStatus" runat="server" Visible="false" Text='<%# Eval("PRESENT_STATUS") %>' />
                            <asp:TextBox ID="txtPresentStatus" runat="server" Width="300px" Text='<%# Eval("PRESENT_STATUS") %>'
                                TextMode="MultiLine" Rows="3" CssClass="textboxleft"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRESENT (%) OF_WORK_DONE">
                        <ItemTemplate>
                            <asp:Label ID="lblPresentPercentageOfWorkDone" runat="server" Visible="false" Text='<%# Eval("PRESENT_PERC_OF_WORK_DONE") %>' />
                            <asp:TextBox ID="txtPresentPercentageOfWorkDone" runat="server" Width="100%" Text='<%# Eval("PRESENT_PERC_OF_WORK_DONE") %>'
                                CssClass="textboxper"
                                onkeyup="checkForPercentage(this);"
                                onkeypress="return inNumberKey(this, event);"
                                onpaste="return false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblEdOfInspection" runat="server" Visible="false" Text='<%# Eval("ED_OF_INSP_COMP") %>' />
                            <asp:TextBox ID="txtEdOfInspection" runat="server" Width="80%" Text='<%# Eval("ED_OF_INSP_COMP") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft"></asp:TextBox>
                            <asp:CalendarExtender ID="calendarEdOfInspection" PopupButtonID="imgbtnEdOfInspection"
                                runat="server" TargetControlID="txtEdOfInspection" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:ImageButton ID="imgbtnEdOfInspection" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LAST_PLANNING_REMARKS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLastPlanningRemarks" runat="server" Width="300px"
                                Text='<%# Eval("LAST_PLANNING_REMARKS") %>'
                                TextMode="MultiLine" Rows="3" CssClass="textboxleft"
                                onkeyDown="javascript:preventInput(event);" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LAST_STATUS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtLastStatus" runat="server" Width="300px" Text='<%# Eval("LAST_STATUS") %>'
                                TextMode="MultiLine" Rows="3"
                                onkeyDown="javascript:preventInput(event);" Enabled="false"
                                CssClass="textboxleft"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LAST (%) OF_WORK_DONE">
                        <ItemTemplate>
                            <asp:Label ID="lblLastPercentageOfWorkDone" runat="server" Visible="false" Text='<%# Eval("LAST_PERC_OF_WORK_DONE") %>' />
                            <asp:TextBox ID="txtLastPercentageOfWorkDone" runat="server" Width="100%" Text='<%# Eval("LAST_PERC_OF_WORK_DONE") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LAST_ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblLastEdOfInspection" runat="server" Visible="true" Text='<%# Eval("LAST_ED_OF_INSP_COMP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="COMPLETED_BY">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompletedBy" runat="server" Visible="true" Text='<%# Eval("COMPLETED_BY") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="COMPLETED_ON">
                        <ItemTemplate>
                            <asp:Label ID="lblCompletedOn" runat="server" Visible="true" Text='<%# Eval("COMPLETED_ON") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NOT_ACCEPTED_REASON">
                        <ItemTemplate>
                            <asp:Label ID="lblNotAcceptedReason" runat="server" Visible="true" Text='<%# Eval("NOT_ACCEPTED_REASON") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AMOUNT_INR">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmountINR" runat="server" Width="150px" Text='<%# Eval("AMOUNT_INR") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
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


    <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>
