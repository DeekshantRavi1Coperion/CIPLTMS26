<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="DesignDrawingList.aspx.cs"
    Inherits="PROJECT_DMS_DesignDrawingList" Title="CIPLTMS - Design List" %>

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
            if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }

            document.getElementById('<%=txtDateByProjectTeamToEdit.ClientID %>').value = document.getElementById('<%=hdDateByProjectTeamToEdit.ClientID %>').value;
        }

        function clientChangedDateByProjectTeamToEdit(sender, args) {
            document.getElementById('<%=hdDateByProjectTeamToEdit.ClientID %>').value = document.getElementById('<%=txtDateByProjectTeamToEdit.ClientID %>').value;
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

        function ValidateJobNo() {
            var JOBNo = document.getElementById('<%=txtJOBNoToEdit.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingNumber() {
            var DrawingNumber = document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').value;
            if (DrawingNumber == '') {
                document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescriptionToEdit.ClientID %>').value;
            if (Description == '') {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantityToEdit.ClientID %>').value;
            if (Quantity == '' || parseInt(Quantity) == 0) {
                document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUOM() {
            var UOM = document.getElementById('<%=txtUOMToEdit.ClientID %>').value;
            if (UOM == '') {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            if (ValidateDrawingNumber()) {
                check = false;
            }

            if (ValidateDescription()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }

            if (ValidateQuantity()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }



            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to update design drawing details?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="filter-grid-container">

            <fieldset class="filter-card">
                <legend>Filters:</legend>

                <div class="form-filter-grid">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch"
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
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
                                    runat="server" TargetControlID="txtEndDateSearch"
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
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
                    <asp:TextBox ID="txtDrawingNoMainSearch" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNoMainSearch" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Is Active</label>
                    <asp:DropDownList ID="ddlIsActive" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" />
                        <asp:ListItem Text="Active" Value="1" />
                        <asp:ListItem Text="Inactive" Value="0" />
                    </asp:DropDownList>





                </div>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search" OnClick="btnSearch_Click"
                    OnClientClick="return ValidateAllSearch();" />

                <asp:Button ID="btnAddDrawing" CssClass="button" Width="100%" runat="server" Text="Add Design Request"
                    OnClick="btnAddDrawing_Click" />

                <asp:Button ID="btnImportDrawings" CssClass="button" Width="100%" runat="server" Text="Import Design List"
                    OnClick="btnImportDrawings_Click" />


                <asp:Button ID="btnSaveDesign" CssClass="button" Width="100%" runat="server" Text="Send Selected to Design"
                    OnClick="btnSaveDesign_Click" />

                <asp:Button ID="btnDesignList" CssClass="button" Width="100%" runat="server" Text="Design List"
                    OnClick="btnDesignList_Click" />
            </div>            
        </div>


        <fieldset class="employee-grid-fieldset">
            <legend>Design Allocation List:
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
                    ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvDesignDetails_RowCommand"
                    OnRowDataBound="gvDesignDetails_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDrawingID" runat="server" Text='<%# Eval("DRAWING_ID") %>' Visible="false" />
                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IS_ACTIVE") %>' Visible="false" />

                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit"
                                    ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                    ImageUrl="~/Images/cancelled_img.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnActivate" CommandArgument="ACTIVATE" runat="server" ToolTip="Activate"
                                    ImageUrl="~/Images/closed_img.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JOB No.">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Text='<%# Eval("JOB_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBUnitID" runat="server" Text='<%# Eval("JOB_UNIT_ID") %>' Visible="false" />
                                <asp:Label ID="lblJOBUnit" runat="server" Text='<%# Eval("JOB_UNIT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Drawing No.">
                            <ItemTemplate>
                                <asp:Label ID="lblDrawingNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Client Drawing No.">
                            <ItemTemplate>
                                <asp:Label ID="lblClientDrawingNo" runat="server" Text='<%# Eval("CLIENT_DRAWING_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contractor Drawing No.">
                            <ItemTemplate>
                                <asp:Label ID="lblContractorDrawingNo" runat="server" Text='<%# Eval("CONTRACTOR_DRAWING_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="50px"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reqd Date By Project Team">
                            <ItemTemplate>
                                <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--11--%>
                        <asp:TemplateField HeaderText="Document Link">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDocumentLink" runat="server" Width="200px" Height="26px"
                                    CssClass="textboxleft" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Drawing Revisioin Number">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlDrawingRevNo" runat="server" Width="100%" Height="26px"
                                    CssClass="textboxleft">
                                    <asp:ListItem Text="00" Value="0" />
                                    <asp:ListItem Text="01" Value="1" />
                                    <asp:ListItem Text="02" Value="2" />
                                    <asp:ListItem Text="03" Value="3" />
                                    <asp:ListItem Text="04" Value="4" />
                                    <asp:ListItem Text="05" Value="5" />
                                    <asp:ListItem Text="06" Value="6" />
                                    <asp:ListItem Text="07" Value="7" />
                                    <asp:ListItem Text="08" Value="8" />
                                    <asp:ListItem Text="09" Value="9" />
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                    <asp:ListItem Text="13" Value="13" />
                                    <asp:ListItem Text="14" Value="14" />
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="16" Value="16" />
                                    <asp:ListItem Text="17" Value="17" />
                                    <asp:ListItem Text="18" Value="18" />
                                    <asp:ListItem Text="19" Value="19" />
                                    <asp:ListItem Text="20" Value="20" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" Height="26px"
                                    CssClass="textboxleft" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="300PX" TextMode="MultiLine"
                                    Rows="2" CssClass="textboxleft" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Button ID="btnSaveDesign" CommandArgument="SAVE_DESIGN" ToolTip="Send To Design" runat="server"
                                    Text="Send To Design" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
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


    <%--UPDATE DESIGN DRAWING START--%>
    <asp:Button ID="btnShowPopupUpdateDrawing" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdateDrawing" runat="server" TargetControlID="btnShowPopupUpdateDrawing"
        PopupControlID="pnlPopupUpdateDrawing" CancelControlID="imgBtnCancelUpdateDrawing" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupUpdateDrawing" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelUpdateDrawing" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>Update Design Drawing</legend>

                <div class="form-grid form-grid-2">

                    <label>JOB Number</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:TextBox ID="txtJOBNoToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNo_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Drawing Number</label>
                    <asp:TextBox ID="txtDrawingNumberToEdit" runat="server"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />

                    <label>Client Drawing Number</label>
                    <asp:TextBox ID="txtClientDrawingNumberToEdit" runat="server"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />

                    <label>Contractor Drawing Number</label>
                    <asp:TextBox ID="txtContractorDrawingNumberToEdit" runat="server"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />



                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantityToEdit" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKey(this, event);" />


                    <div class="full-width">
                        <label>Description</label>
                        <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="2"
                            runat="server"
                            CssClass="form-control" />
                    </div>


                    <label>UOM</label>
                    <asp:TextBox ID="txtUOMToEdit" runat="server"
                        CssClass="form-control" />

                    <label>Reqd. Date By Project Team</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDateByProjectTeamToEdit"
                                    runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdDateByProjectTeamToEdit" runat="server" />
                                <ajax:CalendarExtender ID="calendarDateByProjectTeamToEdit"
                                    PopupButtonID="imgBtnDateByProjectTeamToEdit" runat="server"
                                    TargetControlID="txtDateByProjectTeamToEdit" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedDateByProjectTeamToEdit">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnDateByProjectTeamToEdit" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdateDrawing" runat="server" Width="100%" Text="Update Drawing" CssClass="button"
                    OnClick="btnUpdateDrawing_Click" OnClientClick="return ValidateAll();" />
            </div>

        </div>

    </asp:Panel>
    <%--UPDATE DESIGN DRAWING END--%>


    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompanySearch" runat="server" CssClass="form-control" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />
                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No."
                                    runat="server" Text="Get JOB No." CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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
    <%--JOB DETAIL END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
