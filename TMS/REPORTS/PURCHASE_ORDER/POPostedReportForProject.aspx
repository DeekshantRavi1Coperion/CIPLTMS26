<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="POPostedReportForProject.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_POPostedReportForProject" Title="PO Posted Report [Project]" %>

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

    <script type="text/javascript">
        function ClearFollowupDate() {
            document.getElementById('<%=hdFollowUpDateSearch.ClientID %>').value = "";
            document.getElementById('<%=txtFollowUpDateSearch.ClientID %>').value = "";
            document.getElementById('<%=chkClearFollowUpDate.ClientID %>').checked = false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdPostingConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDeletionConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Procurement Status- Project View:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
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

                    &nbsp;

                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                    &nbsp;
                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />

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
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
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
                            <asp:Label ID="lblFollowUpDays" runat="server" Visible="false" Text='<%# Eval("FOLLOW_UP_DAYS") %>' />
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblPONo" runat="server" Visible="true" Text='<%# Eval("PO_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblPODate" runat="server" Visible="true" Text='<%# Eval("PO_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_DELIVERY_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblPODeliveryDate" runat="server" Visible="true" Text='<%# Eval("PO_DELIVERY_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VENDOR_NAME">
                        <ItemTemplate>

                            <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                            <asp:Label ID="lblVendorName" runat="server" Visible="true" Text='<%# Eval("VENDOR_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ITEM_NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Visible="true" Text='<%# Eval("ITEM_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="QUANTITY">
                        <ItemTemplate>

                            <asp:Label ID="lblBalQuantity" runat="server" Visible="false" Text='<%# Eval("BAL_QUANTITY") %>' />
                            <asp:TextBox ID="txtQuantity" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("QUANTITY") %>' CssClass="textbox"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="BALANCE_QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBalQuantity" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                    Text='<%# Eval("BAL_QUANTITY") %>' CssClass="textbox"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>

                            <%--<asp:Label ID="lblService" runat="server" Visible="false" Text='<%# Eval("SERVICE") %>' />--%>
                            <asp:Label ID="lblUOM" runat="server" Visible="true" Text='<%# Eval("UOM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_VALUE_INR">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPOValueINR" runat="server" Width="120px" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("PO_VALUE_INR") %>' CssClass="textbox"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblJOBNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="BUDGET">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBudget" runat="server" Width="120px" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("BUDGET") %>' CssClass="textbox" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="FOLLOW_UP_BY">
                        <ItemTemplate>

                            <asp:Label ID="lblMrCreatedBy" runat="server" Visible="false" Text='<%# Eval("MR_CREATED_BY") %>' />
                            <asp:Label ID="lblFollowUpBy" runat="server" Visible="true" Text='<%# Eval("FOLLOW_UP_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="LOCATION">
                        <ItemTemplate>
                            <asp:Label ID="lblLocation" runat="server" Visible="true" Text='<%# Eval("LOCATION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="REVISION">
                        <ItemTemplate>
                            <asp:Label ID="lblRevision" runat="server" Visible="true" Text='<%# Eval("REVISION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO_FIRST_ITEM">
                        <ItemTemplate>
                            <asp:Label ID="lblPOFirstItem" runat="server" Visible="true" Text='<%# Eval("PO_FIRST_ITEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ENGG_APPROVAL_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblEnggApprovalStatus" runat="server" Visible="true" Text='<%# Eval("ENGG_APPROVAL_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PRESENT_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblPresentStatus" runat="server" Visible="true" Text='<%# Eval("PRESENT_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblEdOfInspection" runat="server" Visible="true" Text='<%# Eval("ED_OF_INSP/COMP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="POSTING_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblNextFollowupDate" runat="server" Visible="false" Text='<%# Eval("NEXT_FOLLOWUP_DATE") %>' />
                            <asp:Label ID="lblPostingStatus" runat="server" Visible="true" Text='<%# Eval("POSTING_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--  <asp:TemplateField HeaderText="NEXT_FOLLOWUP_DATE">
                            <ItemTemplate>
                                <asp:Label ID="lblNextFollowupDate" runat="server" Visible="true" Text='<%# Eval("NEXT_FOLLOWUP_DATE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                    <asp:TemplateField HeaderText="LAST_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblLastStatus" runat="server" Visible="true" Text='<%# Eval("LAST_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LAST_ED_OF_INSP/COMP">
                        <ItemTemplate>
                            <asp:Label ID="lblLastEdOfInspection" runat="server" Visible="true" Text='<%# Eval("LAST_ED_OF_INSP/COMP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="LAST_FOLLOW_UP_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblLastModifiedDate" runat="server" Visible="true" Text='<%# Eval("LAST_FOLLOW_UP_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="OA_RECEIVED">
                        <ItemTemplate>
                            <asp:Label ID="lblOAReceived" runat="server" Visible="true" Text='<%# Eval("OA_RECEIVED") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DATE_OF_DRAWING_RECEIVED_FROM_VENDOR">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfDrawingReceivedFromVendor" runat="server" Visible="true" Text='<%# Eval("DATE_OF_DRAWING_RECEIVED_FROM_VENDOR") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfApprovedDrawingSentToVendor" runat="server" Visible="true"
                                Text='<%# Eval("DATE_OF_APPROVED_DRAWING_SENT_TO_VENDOR") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryTermsAgreedWithSupplierInWeeks" runat="server" Visible="true"
                                Text='<%# Eval("DELIVERY_TERMS_AGREED_WITH_SUPPLIER_IN_WEEKS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS">
                        <ItemTemplate>
                            <asp:Label ID="lblFinalDeliveryDateAsPerAgreedTerms" runat="server" Visible="true"
                                Text='<%# Eval("FINAL_DELIVERY_DATE_AS_PER_AGREED_TERMS") %>' />
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

        <div class="page-layout">
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

            <div class="employee-grid-container">
                <asp:GridView
                    CssClass="employee-grid"
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
