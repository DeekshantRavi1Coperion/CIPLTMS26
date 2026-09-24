<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TourAdditionalAdvanceList.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TourAdditionalAdvanceList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
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

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;

            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
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


        function clientChangedUpdations(sender, args) {

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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


    <script type="text/javascript">

        function ValidateEmployee() {
            var Employee = document.getElementById('<%=ddlEmployee.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateCustVendName() {
            var CustVendName = document.getElementById('<%=txtCustVendName.ClientID %>').value;
            if (CustVendName == '') {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlaceOfVisit() {
            var PlaceOfVisit = document.getElementById('<%=txtPlaceOfVisit.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        function ValidateCountryOfVisit() {

        }


        function ValidateExpenditureAmt() {
            var ExpenditureAmt = document.getElementById('<%=txtExpenditureAmt.ClientID %>').value;
            if (ExpenditureAmt == '') {
                document.getElementById('<%=txtExpenditureAmt.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtExpenditureAmt.ClientID %>').style.borderColor = "";
                return false;
            }
        }





        function ValidateAll() {
            //button.disabled = true;
            // this.disabled = true;
            var check = true;

            if (ValidateEmployee()) {
                check = false;
            }

            if (ValidateDateRange()) {
                check = false;
            }

            if (ValidateCustVendName()) {
                check = false;
            }

            if (ValidatePlaceOfVisit()) {
                check = false;
            }

            if (ValidatePurposeOfVisit()) {
                check = false;
            }

            if (ValidateBusinessSegment()) {
                check = false;
            }

            if (ValidateModeOfTravel()) {
                check = false;
            }

            if (ValidateTypeOfTrip()) {
                check = false;
            }

            if (ValidateLocalTravelling()) {
                check = false;
            }

            if (ValidateExpenditureAmt()) {
                check = false;
            }



            if (ValidateTourBasedOn()) {
                check = false;
            }


            if (check) {

                if (confirm("Would you like to update?")) {
                     <%--//document.getElementById('<%=btnSubmit.ClientID %>').disabled = true;--%>
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";

                    return true;

                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    <%--//document.getElementById('<%=btnSubmit.ClientID %>').disabled = false;--%>
                    return false;
                }
            }
            else {
                return false;
            }
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

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
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
        function ChangeByExpenditureCurrency() {

            var ExpenditureCurrency = document.getElementById('<%=ddlExpenditureCurrency.ClientID %>');
            var ExpenditureCurrencyValue = ExpenditureCurrency.options[ExpenditureCurrency.selectedIndex].value;
            document.getElementById('<%=ddlAdvanceCurrency.ClientID %>').value = ExpenditureCurrencyValue;
        }

        function ChangeByAdvanceCurrency() {

            var AdvanceCurrency = document.getElementById('<%=ddlAdvanceCurrency.ClientID %>');
            var AdvanceCurrencyValue = AdvanceCurrency.options[AdvanceCurrency.selectedIndex].value;
            document.getElementById('<%=ddlExpenditureCurrency.ClientID %>').value = AdvanceCurrencyValue;
        }
    </script>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:HiddenField ID="hdConfirmValue" runat="server" />



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Additional Tour Advance Request List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch"
                                    runat="server" ReadOnly="true"
                                    CssClass="form-control">
                                </asp:TextBox>
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
                                    CssClass="form-control">
                                </asp:TextBox>
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

                    <label>Tour No.</label>
                    <asp:TextBox ID="txtTourNo"
                        runat="server"
                        CssClass="form-control" />


                    <label>Sanction No.</label>
                    <asp:TextBox ID="txtSanctionNo"
                        runat="server"
                        CssClass="form-control" />


                    <label>Status</label>
                    <asp:DropDownList ID="ddlTourStatus"
                        runat="server"
                        CssClass="form-control" />

                    <label>Employee Name</label>
                    <asp:DropDownList ID="ddlEmployeeName"
                        runat="server"
                        CssClass="form-control" />


                    &nbsp;
                    &nbsp;
                    <asp:Button ID="btnSearch"
                        OnClick="btnSearch_Click"
                        runat="server"
                        Text="Search"
                        CssClass="button" />

                    &nbsp;
                    <asp:Button ID="btnAddNewTourInfo"
                        OnClick="btnAddNewTourInfo_Click"
                        runat="server"
                        Text="Add new tour"
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

            <asp:GridView ID="gvTourList"
                CssClass="employee-grid"
                runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvTourList_RowCommand" OnRowDataBound="gvTourList_RowDataBound">

                <%--OnRowDataBound="gvTourList_RowDataBound"--%>
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="VIEW" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail"
                                Height="20px"
                                Width="20px"
                                CommandArgument="ViewDETAIL"
                                runat="server"
                                ImageUrl="~/Images/pdficon1.png" />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="EDIT">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgProperties"
                                CommandArgument="PROPERTIES"
                                runat="server"
                                ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CANCEL" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnCancel"
                                CommandArgument="CANCEL"
                                ToolTip="Cancel Tour"
                                runat="server"
                                Text="Cancel"
                                CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="APPROVE" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove"
                                CommandArgument="APPROVE"
                                ToolTip="Approve Tour"
                                runat="server"
                                Text="Approve"
                                CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="ADVICE">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnGenerateAdvice" 
                                    CommandArgument="ADVICE" 
                                    ToolTip="Generate Advice"
                                    runat="server" 
                                    ImageUrl="~/Images/NEWICONS/ADVICE03.PNG" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SEND_MAIL">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnSendMail" 
                                    CommandArgument="SEND_MAIL" 
                                    ToolTip="Send Mail"
                                    runat="server" 
                                    ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:BoundField DataField="TOUR_NO" HeaderText="TOUR_NO" />
                    <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="SANCTION_NO" />
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>

                            <asp:Label ID="lblTourNO" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                            <asp:Label ID="lblRequestID" runat="server" Visible="false" Text='<%# Eval("ADD_ADVANCE_REQUEST_ID") %>' />
                            <asp:Label ID="lblRequestNo" runat="server" Visible="false" Text='<%# Eval("REQ_NO") %>' />
                            <asp:Label ID="lblTourSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                            <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            <asp:Label ID="lblEmployeeID" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_ID") %>' />
                            <asp:Label ID="lblEmpEmailID" runat="server" Visible="false" Text='<%# Eval("EMAIL_ID") %>' />
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                            <asp:Label ID="lblDesignation" runat="server" Visible="false" Text='<%# Eval("DESIGNATION") %>' />
                            <asp:Label ID="lblStartDate" runat="server" Visible="false" Text='<%# Eval("START_DATE") %>' />
                            <asp:Label ID="lblEndDate" runat="server" Visible="false" Text='<%# Eval("END_DATE") %>' />
                            <asp:Label ID="lblCustVendName" runat="server" Visible="false" Text='<%# Eval("CUST_VEND_NAME") %>' />
                            <asp:Label ID="lblPlaceOfVisit" runat="server" Visible="false" Text='<%# Eval("PLACE_OF_VISIT") %>' />

                            <asp:Label ID="lblAdvanceTaken" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMT") %>' />
                            <asp:Label ID="lblAdvanceTakenCurr" runat="server" Visible="false" Text='<%# Eval("ADVANCE_TAKEN_CURRENCY") %>' />
                            <asp:Label ID="lblAdvanceTakenCurrId" runat="server" Visible="false" Text='<%# Eval("ADVANCE_TAKEN_CURRENCY_ID") %>' />

                            <asp:Label ID="lblAdditionalRequestedAmt" runat="server" Visible="false" Text='<%# Eval("ADDITIONAL_REQUESTED_AMT") %>' />
                            <asp:Label ID="lblAdditionalRequestedAmtCurrency" runat="server" Visible="false" Text='<%# Eval("ADDITIONAL_REQUESTED_AMT_CURRENCY") %>' />
                            <asp:Label ID="lblAdditionalRequestedAmtCurrencyId" runat="server" Visible="false" Text='<%# Eval("ADDITIONAL_REQUESTED_AMT_CURRENCY_ID") %>' />

                            <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("REMARKS") %>' />

                            <asp:Label ID="lblRequestStatusID" runat="server" Visible="false" Text='<%# Eval("REQUEST_STATUS_ID") %>' />

                            <asp:Label ID="lblRequestStatusName" runat="server" Visible="false" Text='<%# Eval("ADD_ADVANCE_STATUS_NAME") %>' />



                            <asp:Label ID="lblTeamLeaderID" runat="server" Visible="false" Text='<%# Eval("TEAMLEADER_ID") %>' />

                            <asp:Label ID="lblIsMailSentToHOD" runat="server" Visible="false" Text='<%# Eval("IS_MAIL_SENT_TO_HOD") %>' />


                            <asp:Label ID="lblIsMailSentToAccounts" runat="server" Visible="false" Text='<%# Eval("IS_MAIL_SENT_TO_ACCOUNTS") %>' />



                            <asp:Label ID="lblIsCancelledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CANCELLED_MAIL_SENT") %>' />

                            <%--<asp:Label ID="lblIsAdviceGenerated" runat="server" Visible="false" Text='<%# Eval("IS_ADVICE_GENERATED") %>' />--%>

                            <asp:Label ID="lblCretedByAccNo" runat="server" Visible="false" Text='<%# Eval("CREATED_BY_ACC") %>' />

                            <asp:Label ID="lblCretedByIFSC" runat="server" Visible="false" Text='<%# Eval("CREATED_BY_IFSC") %>' />

                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:BoundField DataField="ADD_ADVANCE_REQUEST_ID" HeaderText="REQUEST_ID" />--%>

                    <asp:BoundField DataField="REQ_NO" HeaderText="REQUEST_NUMBER" />
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                    <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                    <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                    <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />



                    <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_TAKEN_AMT" />

                    <asp:BoundField DataField="ADVANCE_TAKEN_CURRENCY" HeaderText="ADVANCE_CURRENCY" />

                    <asp:BoundField DataField="ADDITIONAL_REQUESTED_AMT" HeaderText="ADDITIONAL_ADVANCE_AMT" />

                    <asp:BoundField DataField="ADDITIONAL_REQUESTED_AMT_CURRENCY" HeaderText="ADDITIONAL_ADVANCE_CURRENCY" />

                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />

                    <asp:BoundField DataField="ADD_ADVANCE_STATUS_NAME" HeaderText="REQUEST_STATUS" />

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
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup"
        runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>
                    <asp:Label ID="lblLegend" runat="server"></asp:Label>
                </legend>
                <div class="form-grid form-grid-2">


                    <label>Employee Name</label>
                    <asp:DropDownList ID="ddlEmployee"
                        CssClass="form-control"
                        runat="server"
                        Enabled="false">
                    </asp:DropDownList>


                    <label>Employee ID</label>
                    <asp:TextBox ID="txtEmployeeID"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Designation</label>
                    <asp:TextBox ID="txtDesignation"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Sanction No.</label>
                    <asp:TextBox ID="txtTourSanctionNo"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />


                    <label>Starting Date Of Tour</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:HiddenField ID="hdStartDate" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate"
                                    runat="server"
                                    TargetControlID="txtStartDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedUpdations" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date Of Tour</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:HiddenField ID="hdEndDate" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDate" runat="server"
                                    TargetControlID="txtEndDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedUpdations" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblDateMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />


                    <label>Name of Customer/Vendor</label>
                    <asp:TextBox ID="txtCustVendName"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />


                    <label>Place of Visit</label>
                    <asp:TextBox ID="txtPlaceOfVisit"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Advance Taken</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%;">
                                <asp:TextBox ID="txtExpenditureAmt" runat="server"
                                    CssClass="form-control"
                                    Enabled="false"
                                    onkeyup="checkDec(this);"
                                    onpaste="return false"
                                    onkeypress="return inNumberKeyWithDecimal(this, event);" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlExpenditureCurrency"
                                    CssClass="form-control"
                                    runat="server"
                                    Enabled="false"
                                    onchange="ChangeByExpenditureCurrency()">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <label>Additional Advance Required</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%;">
                                <asp:TextBox ID="txtAdvanceAmt"
                                    runat="server"
                                    CssClass="form-control"
                                    Enabled="true"
                                    onkeyup="checkDec(this);"
                                    onpaste="return false"
                                    onkeypress="return inNumberKeyWithDecimal(this, event);" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAdvanceCurrency"
                                    runat="server"
                                    CssClass="form-control"
                                    Enabled="false"
                                    onchange="ChangeByAdvanceCurrency()">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <%-- </div>
                <div class="form-grid form-grid-2">--%>

                    <label>Requester's Remarks</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRequestRemarks"
                            Enabled="false"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <%--  </div>

                <div class="form-grid form-grid-2">--%>

                    <label>Remarks</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRemarks"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <div class="full-width button-group">
                        <asp:Button ID="btnSubmit"
                            runat="server"
                            Text="Save"
                            CssClass="button"
                            OnClientClick="return ValidateAll();"
                            OnClick="btnSubmit_Click" Width="100%" />
                    </div>


                    <div class="full-width">
                        <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>
                    </div>

                </div>

            </fieldset>
        </div>

    </asp:Panel>



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
        <iframe class="popup-iframe"
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


</asp:Content>


