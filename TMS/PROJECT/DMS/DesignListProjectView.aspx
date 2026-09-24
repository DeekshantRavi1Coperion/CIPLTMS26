<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="DesignListProjectView.aspx.cs"
    Inherits="PROJECT_DMS_DesignListProjectView" Title="CIPLTMS- Design List Project View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript" language="javascript">

        function ValidateAllSearch() {

            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }
    </script>



    <script type="text/javascript">

        function EnableEndDateSearch() {
            var sign = document.getElementById('<%=ddlSignMainSearch.ClientID %>');
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
    </script>


    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
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

    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
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

        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;

    </script>

    <script type="text/Javascript">
        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectDates.ClientID %>').checked;
            var datetype = document.getElementById('<%=ddlSignMainSearch.ClientID %>').selectedIndex;
            if (chk == true) {
                if (datetype == 0) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
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

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Design Status- Project View:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width">
                        <label>Date Filter</label>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlOnWhichDateMainSearch" runat="server"
                                        CssClass="form-control">
                                        <asp:ListItem Text="Reqd. Date By Project Team" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Expected Completion Date" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Planned Dates By Design Team" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSignMainSearch" runat="server"
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

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
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
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td style="width: 20%;" align="center">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                            <td style="width: 10%;">
                                <asp:CheckBox ID="chkSelectDates" runat="server" onchange="EnableDisableDates()" />
                            </td>
                        </tr>
                    </table>

                    <label>Drawing No.</label>
                    <asp:TextBox ID="txtDrawingNoMainSearch" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryMainSearch" runat="server"
                        CssClass="form-control" />

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNoMainSearch" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Status</label>
                    <asp:DropDownList ID="ddlPostingStatusMainSearch" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Design Checker</label>
                    <asp:DropDownList ID="ddlDesignCheckerMainSearch" runat="server" CssClass="form-control" />

                    <label>Responsible Design Engineer</label>
                    <asp:DropDownList ID="ddlRespDesignEnggMainSearch" runat="server"
                        CssClass="form-control" />


                    <div class="full-width button-group">
                        <label>Created By</label>
                        <asp:DropDownList ID="ddlCreatedByMainSearch" runat="server"
                            CssClass="form-control" />

                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAllSearch();" OnClick="btnSearch_Click" />

                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export" OnClick="btnExport_Click" />
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
                ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvDesignDetails_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="Sr.No.">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("Sr_No") %>'
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>--%>

                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("Sr_No") %>' Visible="true" />
                            <asp:Label ID="lblJOBNo" runat="server" Text='<%# Eval("JOB_No") %>' Visible="false" />
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Visible="false" />
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Visible="false" />
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' Visible="false" />
                            <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Text='<%# Eval("Reqd_Date_By_Project_Team") %>' Visible="false" />
                            <asp:Label ID="lblPlannedStartDateByDesignTeam" runat="server" Text='<%# Eval("Planned_Start_Date_By_Design_Team") %>' Visible="false" />
                            <asp:Label ID="lblPlannedCompletionDateByDesignTeam" runat="server" Text='<%# Eval("Planned_Completion_Date_By_Design_Team") %>' Visible="false" />
                            <asp:Label ID="lblDrawingNo" runat="server" Text='<%# Eval("Drawing_No") %>' Visible="false" />

                            <asp:Label ID="lblClientDrawingNo" runat="server" Text='<%# Eval("Client_Drawing_No") %>' Visible="false" />
                            <asp:Label ID="lblContractorDrawingNo" runat="server" Text='<%# Eval("Contractor_Drawing_No") %>' Visible="false" />

                            <asp:Label ID="lblRevisionNo" runat="server" Text='<%# Eval("Revision_No") %>' Visible="false" />
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>' Visible="false" />
                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("Expected_Completion_Date") %>' Visible="false" />

                            <asp:Label ID="lblDrawingSentToDesign" runat="server" Text='<%# Eval("Drawing_Sent_To_Design") %>' Visible="false" />

                            <asp:Label ID="lblDesignEngineer" runat="server" Text='<%# Eval("Responsible_Design_Engineer") %>' Visible="false" />
                            <asp:Label ID="lblDrawingStatus" runat="server" Text='<%# Eval("Drawing_Status") %>' Visible="false" />
                            <asp:Label ID="lblHoursSpent" runat="server" Text='<%# Eval("Hours_Spent") %>' Visible="false" />

                            <asp:Label ID="lblCompletedByDesignEnggDate" runat="server" Text='<%# Eval("Completed_By_Design_Engg_Date") %>' Visible="false" />
                            <asp:Label ID="lblCheckedDate" runat="server" Text='<%# Eval("Checked_Date") %>' Visible="false" />

                            <asp:Label ID="lblDesigner" runat="server" Text='<%# Eval("Designer") %>' Visible="false" />
                            <asp:Label ID="lblDesignerTimesheetHours" runat="server" Text='<%# Eval("Designer_Timesheet_Hours") %>' Visible="false" />
                            <asp:Label ID="lblChecker" runat="server" Text='<%# Eval("Checker") %>' Visible="false" />
                            <asp:Label ID="lblCheckerTimesheetHours" runat="server" Text='<%# Eval("Checker_Timesheet_Hours") %>' Visible="false" />
                            <asp:Label ID="lblTotalTimehseetHours" runat="server" Text='<%# Eval("Total_Timehseet_Hours") %>' Visible="false" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="JOB No." DataField="JOB_No" />
                    <asp:BoundField HeaderText="Category" DataField="Category" />
                    <asp:BoundField HeaderText="Description" DataField="Description" />
                    <asp:BoundField HeaderText="UOM" DataField="UOM" />
                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                    <asp:BoundField HeaderText="Reqd Date By Project Team" DataField="Reqd_Date_By_Project_Team" />
                    <asp:BoundField HeaderText="Planned Start Date By Design Team" DataField="Planned_Start_Date_By_Design_Team" />
                    <asp:BoundField HeaderText="Planned Completion Date By Design Team" DataField="Planned_Completion_Date_By_Design_Team" />
                    <asp:BoundField HeaderText="Drawing No." DataField="Drawing_No" />
                    <asp:BoundField HeaderText="Client Drawing No." DataField="Client_Drawing_No" />
                    <asp:BoundField HeaderText="Contractor Drawing No." DataField="Contractor_Drawing_No" />
                    <asp:BoundField HeaderText="Revision No." DataField="Revision_No" />
                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                    <asp:BoundField HeaderText="Expected Completion Date" DataField="Expected_Completion_Date" />

                    <asp:BoundField HeaderText="Drawing Sent To Design" DataField="Drawing_Sent_To_Design" />
                    <asp:BoundField HeaderText="Design Engineer" DataField="Responsible_Design_Engineer" />
                    <asp:BoundField HeaderText="Completed By Design Engg Date" DataField="Completed_By_Design_Engg_Date" />
                    <asp:BoundField HeaderText="Drawing Status" DataField="Drawing_Status" />
                    <asp:BoundField HeaderText="Checked Date" DataField="Checked_Date" />
                    <asp:BoundField HeaderText="Hours Spent" DataField="Hours_Spent" />

                    <asp:BoundField HeaderText="Designer" DataField="Designer" />
                    <asp:BoundField HeaderText="Designer Timesheet Hours" DataField="Designer_Timesheet_Hours" />
                    <asp:BoundField HeaderText="Checker" DataField="Checker" />
                    <asp:BoundField HeaderText="Checker Timesheet Hours" DataField="Checker_Timesheet_Hours" />
                    <asp:BoundField HeaderText="Total Timesheet Hours" DataField="Total_Timehseet_Hours" />
                    <%--<asp:BoundField HeaderText="Design Checker" DataField="DESIGN_CHECKER" />--%>
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
            id="iframeViewDrawingDetailsInPDF"
            runat="server">
        </iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
