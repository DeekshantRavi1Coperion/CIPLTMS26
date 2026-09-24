<%@ Page Title="CIPLTMS-Inward Information List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    AspCompat="true" CodeFile="InwardProductionList.aspx.cs" Inherits="PROJECT_InwardProductionList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
    <link href="../Styles/filter.css" rel="stylesheet" />
    <link href="../Styles/grid.css" rel="stylesheet" />
    <link href="../Styles/popup.css" rel="stylesheet" />

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

    <script type="text/javascript">
        function updateTruckTotal() {
            let total = 0;

            // Use correct class selector (qty-input from your TextBoxes)
            document.querySelectorAll('.qty-input').forEach(function (input) {
                let val = parseFloat(input.value);
                total += isNaN(val) ? 0 : val;
            });

            // Get the total textbox by ClientID (replace with actual ClientID on render)
            var truckTotalBox = document.getElementById('<%= txtNoOfTrucksEdit.ClientID %>');
            if (truckTotalBox) {
                truckTotalBox.value = total;
            } else {
                console.error("Could not find txtNoOfTrucksEdit element");
            }
        }



        function updateContainerTotal() {
            let total01 = 0;

            // Use correct class selector (qty-input from your TextBoxes)
            document.querySelectorAll('.qty-input-container').forEach(function (input) {
                let val = parseFloat(input.value);
                total01 += isNaN(val) ? 0 : val;
            });

            // Get the total textbox by ClientID (replace with actual ClientID on render)
            var containerTotalBox = document.getElementById('<%= txtNoContainersEditInt.ClientID %>');
            if (containerTotalBox) {
                containerTotalBox.value = total01;
            } else {
                console.error("Could not find txtNoContainersEditInt element");
            }
        }


    </script>


    <script type="text/javascript" language="javascript">

        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;

         <%--   document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;--%>

           <%-- document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;--%>


            document.getElementById('<%=txtLRDateC.ClientID %>').value = document.getElementById('<%=hdLRDateC.ClientID %>').value;


            document.getElementById('<%=hdLRDateC.ClientID %>').value = document.getElementById('<%=txtLRDateC.ClientID %>').value;

            document.getElementById('<%=hdLRDateC.ClientID %>').value = document.getElementById('<%=txtLRDateC.ClientID %>').value;





        }
        //return false;
        //    }
        //}

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
           <%-- document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;--%>

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
          <%--  var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");--%>
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

        function ConfirmValidate() {
            var check = true;
        }
        if (check) {

            if (confirm("Would you like to Confirm?")) {
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




    </script>

    <script type="text/javascript">

        <%--function ValidateEmployee() {
            var Employee = document.getElementById('<%=ddlEmployee.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployee.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>



       <%-- function ValidateCustVendName() {
            var CustVendName = document.getElementById('<%=txtCustVendName.ClientID %>').value;
            if (CustVendName == '') {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        <%--function ValidatePlaceOfVisit() {
            var PlaceOfVisit = document.getElementById('<%=txtPlaceOfVisit.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>


        function ValidateCountryOfVisit() {

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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>

    <asp:HiddenField ID="hdConfirmValue" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Inward Information List(Domestic/International):
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
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

                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
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

                    <label>Request No.:</label>
                    <asp:TextBox ID="txtReqNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Job No.:</label>
                    <asp:TextBox ID="txtJobNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Domestic/International.:</label>
                    <asp:DropDownList ID="ddlDomInt" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Employee Name:</label>
                    <asp:DropDownList ID="ddlEmployeeName" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />

                <asp:Button ID="btnAddNewTourInfo" CssClass="button" Width="100%" runat="server"
                    Text="Add New Request" OnClick="btnAddNewRequestInfo_Click" />
            </div>
        </div>
        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView
                CssClass="employee-grid"
                ID="gvInwardList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvInwardList_RowCommand"
                OnRowDataBound="gvInwardList_RowDataBound">
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
                    <asp:TemplateField HeaderText="Packing List" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPackingListName" runat="server" Visible="false" Text='<%# Eval("PACKING_LIST_FILE_NAME") %>' />
                            <asp:ImageButton ID="imgBtnPackingListName"
                                Height="30px"
                                Width="30px"
                                CommandArgument="VIEW_PACKING_LIST_FILE"
                                runat="server"
                                ImageUrl="~/Images/pdficon4.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sub Vendor Invoice" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSubVendorInvoiceName" runat="server" Visible="false" Text='<%# Eval("SUB_VENDOR_INVOICE_FILE_NAME") %>' />
                            <asp:ImageButton ID="imgBtnPaymentFileName"
                                Height="30px"
                                Width="30px"
                                CommandArgument="VIEW_SUB_VENDOR_INVOICE_FILE_NAME"
                                ImageUrl="~/Images/pdficon4.png"
                                runat="server" />
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
                                ToolTip="Cancel Request"
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
                                ToolTip="Approve Request"
                                runat="server"
                                Text="Approve"
                                CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <%--                        <asp:TemplateField HeaderText="SEND_MAIL">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnSendMail" 
                                    CommandArgument="SEND_MAIL" 
                                    ToolTip="Send Mail"
                                    runat="server" 
                                    ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <%-- <asp:BoundField DataField="INWARD_NO" HeaderText="INWARD_NO" />--%>
                    <%-- <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="SANCTION_NO" />--%>
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblReqID" runat="server" Visible="false" Text='<%# Eval("REQ_ID") %>' />
                            <asp:Label ID="lblInwardNO" runat="server" Visible="false" Text='<%# Eval("INWARD_NO") %>' />
                            <asp:Label ID="lblInwardDate" runat="server" Visible="false" Text='<%# Eval("INWARD_DATE") %>' />
                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                            <asp:Label ID="lblTeamLeaderID" runat="server" Visible="false" Text='<%# Eval("TEAMLEADER_ID") %>' />
                            <asp:Label ID="lblTeamLeaderName" runat="server" Visible="false" Text='<%# Eval("TEAMLEADER_NAME") %>' />
                            <asp:Label ID="lblStatusId" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                            <asp:Label ID="lblInwardStatus" runat="server" Visible="false" Text='<%# Eval("INWARD_STATUS_NAME") %>' />
                            <asp:Label ID="lblIsInternationalInward" runat="server" Visible="false" Text='<%# Eval("INWARD_TYPE") %>' />
                            <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                            <asp:Label ID="lblVendorLocation" runat="server" Visible="false" Text='<%# Eval("VENDOR_LOCATION") %>' />
                            <asp:Label ID="lblVendorEmail" runat="server" Visible="false" Text='<%# Eval("VENDOR_EMAIL") %>' />
                            <asp:Label ID="lblVendorContact" runat="server" Visible="false" Text='<%# Eval("VENDOR_CONTACT") %>' />
                            <asp:Label ID="lblDelPickLoc" runat="server" Visible="false" Text='<%# Eval("DELIVERY_PICKUP_LOCATION") %>' />
                            <asp:Label ID="lblNodeOfTransport" runat="server" Visible="false" Text='<%# Eval("INTERNATIONAL_MODE_OF_TRANSPORT") %>' />
                            <asp:Label ID="lblIsContainerStuffingPossible" runat="server" Visible="false" Text='<%# Eval("INTERNATIONAL_IS_CON_STUFFING_POSSIBLE") %>' />
                            <asp:Label ID="lblIncoterms" runat="server" Visible="false" Text='<%# Eval("INCOTERMS") %>' />
                            <asp:Label ID="lblDeliveryTerm" runat="server" Visible="false" Text='<%# Eval("DELIVERY_TERM") %>' />
                            <asp:Label ID="lblNoOfTrucks" runat="server" Visible="false" Text='<%# Eval("NO_OF_TRUCKS") %>' />
                            <asp:Label ID="lblNoOfContainers" runat="server" Visible="false" Text='<%# Eval("NO_OF_CONTAINERS") %>' />
                            <asp:Label ID="lblDatePickup" runat="server" Visible="false" Text='<%# Eval("DATE_OF_PICKUP") %>' />
                            <asp:Label ID="lblTypeConsignment" runat="server" Visible="false" Text='<%# Eval("TYPE_OF_CONSIGNMENT") %>' />
                            <asp:Label ID="lblPackingListDoc" runat="server" Visible="false" Text='<%# Eval("PACKING_LIST_DOC") %>' />
                            <asp:Label ID="lblSubVendorInvoice" runat="server" Visible="false" Text='<%# Eval("SUB_VENDOR_INVOICE") %>' />
                            <asp:Label ID="lblQtyTruck14" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_14") %>' />
                            <asp:Label ID="lblQtyTruck17" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_17") %>' />
                            <asp:Label ID="lblQtyTruck19" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_19") %>' />
                            <asp:Label ID="lblQtyTruck22" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_22") %>' />
                            <asp:Label ID="lblQtyTruck24" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_24") %>' />
                            <asp:Label ID="lblQtyTruck32" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_32") %>' />
                            <asp:Label ID="lblQtyTruck40" runat="server" Visible="false" Text='<%# Eval("QTY_OF_TRUCK_40") %>' />
                            <asp:Label ID="lblQtyLowBed" runat="server" Visible="false" Text='<%# Eval("QTY_OF_LOW_BED") %>' />

                            <asp:Label ID="lblQtyODCTruck" runat="server" Visible="false" Text='<%# Eval("QTY_OF_ODC_TRUCK") %>' />
                            <asp:Label ID="lblQtyODCContainer" runat="server" Visible="false" Text='<%# Eval("QTY_OF_ODC_CONTAINER") %>' />

                            <asp:Label ID="lblQtyContainers20" runat="server" Visible="false" Text='<%# Eval("QTY_OF_CONTAINERS_20") %>' />
                            <asp:Label ID="lblQtyContainers40" runat="server" Visible="false" Text='<%# Eval("QTY_OF_CONTAINERS_40") %>' />
                            <asp:Label ID="lblQtyContainers40HC" runat="server" Visible="false" Text='<%# Eval("QTY_OF_CONTAINERS_40HC") %>' />
                            <asp:Label ID="lblQtyFR" runat="server" Visible="false" Text='<%# Eval("QTY_OF_FR") %>' />
                            <asp:Label ID="lblIsDeleted" runat="server" Visible="false" Text='<%# Eval("IS_DELETED") %>' />
                            <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                            <asp:Label ID="lblCreatedByName" runat="server" Visible="false" Text='<%# Eval("CREATED_BY_NAME") %>' />
                            <asp:Label ID="lblCreatedOn" runat="server" Visible="false" Text='<%# Eval("CREATED_ON") %>' />
                            <asp:Label ID="lblModifiedBy" runat="server" Visible="false" Text='<%# Eval("MODIFIED_BY") %>' />
                            <asp:Label ID="lblModifiedOn" runat="server" Visible="false" Text='<%# Eval("MODIFIED_ON") %>' />
                            <asp:Label ID="lblIsApprovalMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVAL_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsDeletedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_DELETED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsCancelledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CANCELLED_MAIL_SENT") %>' />
                            <asp:Label ID="lblFinalApprovedBy" runat="server" Visible="false" Text='<%# Eval("FINAL_APPROVED_BY") %>' />
                            <asp:Label ID="lblFinalApprovedOn" runat="server" Visible="false" Text='<%# Eval("FINAL_APPROVED_ON") %>' />
                            <asp:Label ID="lblFinalApprovedRemarks" runat="server" Visible="false" Text='<%# Eval("FINAL_APPROVED_REMARKS") %>' />
                            <asp:Label ID="lblFinalApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_FINAL_APPROVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblFinalConfirmationNumber" runat="server" Visible="false" Text='<%# Eval("FINAL_CONFIRMATION_NUMBER") %>' />
                            <asp:Label ID="lblTransporterName" runat="server" Visible="false" Text='<%# Eval("TRANSPORTER_NAME") %>' />
                            <asp:Label ID="lblTransporterNumber" runat="server" Visible="false" Text='<%# Eval("TRANSPORTER_CONTACT_NUMBER") %>' />
                            <asp:Label ID="lblTransporterEmail" runat="server" Visible="false" Text='<%# Eval("TRANSPORTER_EMAIL") %>' />


                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="REQ_ID" HeaderText="REQ_ID" />
                    <asp:BoundField DataField="INWARD_NO" HeaderText="INWARD_NO" />
                    <asp:BoundField DataField="INWARD_DATE" HeaderText="INWARD_DATE" />
                    <asp:BoundField DataField="CREATED_BY_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="FINAL_CONFIRMATION_NUMBER" HeaderText="FINAL_CONFIRMATION_NUMBER" />
                    <asp:BoundField DataField="INWARD_STATUS_NAME" HeaderText="INWARD_STATUS_NAME" />
                    <asp:BoundField DataField="INWARD_TYPE" HeaderText="INWARD_TYPE" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="VENDOR_NAME" />
                    <asp:BoundField DataField="VENDOR_LOCATION" HeaderText="VENDOR_LOCATION" />
                    <asp:BoundField DataField="VENDOR_EMAIL" HeaderText="VENDOR_EMAIL" />
                    <asp:BoundField DataField="VENDOR_CONTACT" HeaderText="VENDOR_CONTACT" />
                    <asp:BoundField DataField="DELIVERY_PICKUP_LOCATION" HeaderText="DELIVERY_LOCATION" />
                    <asp:BoundField DataField="INTERNATIONAL_MODE_OF_TRANSPORT" HeaderText="MODE_OF_TRANSPORT" />
                    <%--  <asp:BoundField DataField="INTERNATIONAL_IS_CON_STUFFING_POSSIBLE" HeaderText="IS_CONTAINER_STUFFING_POSSIBLE" />--%>
                    <asp:BoundField DataField="INCOTERMS" HeaderText="INCOTERMS" />
                    <asp:BoundField DataField="DELIVERY_TERM" HeaderText="DELIVERY_TERM" />
                    <asp:BoundField DataField="NO_OF_TRUCKS" HeaderText="NO_OF_TRUCKS" />
                    <asp:BoundField DataField="NO_OF_CONTAINERS" HeaderText="NO_OF_CONTAINERS" />
                    <asp:BoundField DataField="DATE_OF_PICKUP" HeaderText="READINESS_DATE" />
                    <asp:BoundField DataField="TYPE_OF_CONSIGNMENT" HeaderText="TYPE_OF_CONSIGNMENT" />
                    <%--<asp:BoundField DataField="PACKING_LIST_DOC" HeaderText="PACKING_LIST_DOC" />
                    <asp:BoundField DataField="SUB_VENDOR_INVOICE" HeaderText="SUB_VENDOR_INVOICE" />--%>
                    <%-- <asp:BoundField DataField="QTY_OF_TRUCKS_14" HeaderText="QTY_TRUCK_14" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_17" HeaderText="QTY_TRUCK_17" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_19" HeaderText="QTY_TRUCK_19" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_22" HeaderText="QTY_TRUCK_22" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_24" HeaderText="QTY_TRUCK_24" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_32" HeaderText="QTY_TRUCK_32" />
                    <asp:BoundField DataField="QTY_OF_TRUCKS_40" HeaderText="QTY_TRUCK_40" />
                    <asp:BoundField DataField="QTY_OF_LOW_BED" HeaderText="QTY_LOW_BED" />
                    <asp:BoundField DataField="QTY_OF_ODC" HeaderText="QTY_ODC" />
                    <asp:BoundField DataField="QTY_OF_CONTAINERS_20" HeaderText="QTY_CONTAINERS_20" />
                    <asp:BoundField DataField="QTY_OF_CONTAINERS_40" HeaderText="QTY_CONTAINERS_40" />
                    <asp:BoundField DataField="QTY_OF_CONTAINERS_40HC" HeaderText="QTY_CONTAINERS_40HC" />
                    <asp:BoundField DataField="QTY_OF_FR" HeaderText="QTY_FR" />--%>
                    <%-- <asp:BoundField DataField="IS_DELETED" HeaderText="IS_DELETED" />
                    <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY" />
                    <asp:BoundField DataField="CREATED_ON" HeaderText="CREATED_ON" />
                    <asp:BoundField DataField="MODIFIED_BY" HeaderText="MODIFIED_BY" />
                    <asp:BoundField DataField="MODIFIED_ON" HeaderText="MODIFIED_ON" />
                    <asp:BoundField DataField="IS_APPROVAL_MAIL_SENT" HeaderText="IS_APPROVAL_MAIL_SENT" />
                    <asp:BoundField DataField="IS_APPROVED_MAIL_SENT" HeaderText="IS_APPROVED_MAIL_SENT" />
                    <asp:BoundField DataField="IS_DELETED_MAIL_SENT" HeaderText="IS_DELETED_MAIL_SENT" />
                    <asp:BoundField DataField="IS_CANCELLED_MAIL_SENT" HeaderText="IS_CANCELLED_MAIL_SENT" />
                    <asp:BoundField DataField="FINAL_APPROVED_BY" HeaderText="FINAL_APPROVED_BY" />
                    <asp:BoundField DataField="FINAL_APPROVED_ON" HeaderText="FINAL_APPROVED_ON" />
                    <asp:BoundField DataField="FINAL_APPROVED_REMARKS" HeaderText="FINAL_APPROVED_REMARKS" />
                    <asp:BoundField DataField="IS_FINAL_APPROVED_MAIL_SENT" HeaderText="IS_FINAL_APPROVED_MAIL_SENT" />--%>
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
    <asp:Panel ID="pnlpopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" /></legend>
            <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>JOB Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNoEdit" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:TextBox>

                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Vendor Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendorName" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor Location/Address:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtVendorLocation" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor's Email:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendorEmail" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Vendor's Contact:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendorContact" runat="server" Width="100%" Enabled="false" />

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblDateMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>Material Delivery location:
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtCustVendName" runat="server" Width="100%" Enabled="true" onblur="return ValidateCustVendName();" />--%>
                            <asp:DropDownList ID="ddlVenLoc" runat="server" Width="100%" Height="25px" >
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td style="display:none;">Incoterms:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList12" runat="server" Visible="false" Width="100%" Height="25px" onblur="return ValidateEmployee();">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Delivery Term:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeliveryTermEdit" runat="server" Width="100%" Height="25px" onblur="return ValidateEmployee();">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>No Of Trucks(if required):
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoOfTrucksEdit" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <%--      ///////////--%>


                    <tr>
                        <td>Readiness Date :
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" width="100%"></asp:TextBox>
                                        <asp:HiddenField ID="hdStartDate" runat="server" />
                                        <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate" runat="server"
                                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Start Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Type Of Consignment:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTypeOfConsignmentEdit" runat="server" Width="100%" Height="25px">
                            </asp:DropDownList>
                        </td>

                    </tr>

                    <tr>
                        <td>Enter Truck Details:</td>
                        <td>
                            <asp:Panel ID="Panel4" runat="server" Width="300px" Height="50px">
                                <table border="1" cellpadding="5">
                                    <tr>
                                        <th width="80px">Full Truck</th>
                                        <th>Qty of Trucks Required</th>
                                    </tr>
                                    <asp:Repeater ID="rptTruck2" runat="server" OnItemDataBound="rptTrucks_ItemDataBound">

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTruck" runat="server" Text='<%# Eval("TruckName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfTruckType" runat="server" Value='<%# Eval("TruckName") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input" Width="200px" oninput="updateTruckTotal()"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>

                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Packing List:
                        </td>
                        <td>

                            <asp:FileUpload ID="fileUpload1" runat="server" Enabled="true" Width="100%"
                                Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                        </td>


                    </tr>
                    <tr>
                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Sub Vendor Invoice:
                        </td>
                        <td>

                            <asp:FileUpload ID="fileUpload2" runat="server" Enabled="true" Width="100%"
                                Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                        </td>
                    </tr>
                    <tr  id="canceldomesticid0" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid1" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid2" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid3" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid4" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                          <tr  id="canceldomesticid5" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid6" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid7" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid8" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                      <tr id="canceldomesticid10" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                     <tr  id="canceldomesticid11" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                  
                    <tr id="canceldomesticid9" runat="server">
                         <td>Cancel Remarks:
                        </td>
                       
                        <td colspan="4">
                            <asp:TextBox ID="txtCancelRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                Rows="2" />
                        </td>
                        
                    </tr>
                  
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>

                <div id="submitbtncanceleditdom" runat="server" style="margin-top: 10px; text-align: center;">
                    <asp:Button
                        ID="btnSubmit"
                        runat="server"
                        Text="Update Inward Information"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnSubmit_Click"
                        Style="background-color: #28a745; color: white; padding: 10px 20px; border: none; border-radius: 5px; width: 50%; cursor: pointer;" />
                </div>

                <br />

            </div>

        </fieldset>
    </asp:Panel>


    <%--------------------------------international edit modal start ----------------------------------------------------%>
    <asp:Button ID="Button2" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="Panel5" CancelControlID="ImageButton8" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>    
    <asp:Panel ID="Panel5" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="ImageButton8" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="Label5" runat="server" /></legend>
            <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>JOB Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobIntEdit" runat="server" Width="70%" Height="25px" Enabled="false">
                            </asp:TextBox>

                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Vendor Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendNameIntEdit" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor Location/Address:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtVendLocIntEdit" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor's Email:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendEmailIntEdit" runat="server" Width="70%" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Vendor's Contact:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVendContactIntEdit" runat="server" Width="100%" Enabled="false" />

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4" align="center">
                            <asp:Label ID="Label6" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>Material Delivery location:
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtCustVendName" runat="server" Width="100%" Enabled="true" onblur="return ValidateCustVendName();" />--%>
                            <asp:DropDownList ID="ddlMatDelLocIntEdit" runat="server" Width="70%" Height="25px" onblur="return ValidateEmployee();">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Incoterms:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIncotermsIntEdit" runat="server" Width="100%" Height="25px" onblur="return ValidateEmployee();">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Mode Of Transport:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlModeOfTransportIntEdit" runat="server" Width="70%" Height="25px" onblur="return ValidateEmployee();">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Container Stuffing Possible:</td>
                    <td>
                        <asp:DropDownList ID="ddlIsStuffingEditInt" runat="server" Width="100%"  Enabled="true" Height="25px" onblur="return ValidateEmployee();"
                           >
                        </asp:DropDownList>
                    </td>
                    </tr>
                    
                    <%--      ///////////--%>
                     <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>No. Of Containers Required:
                        </td>
                         <td>
                        <asp:TextBox ID="txtNoContainersEditInt" runat="server" Style="width: 70%;" Enabled="false" />
                    </td>
                        <td>&nbsp;
                        </td>
                        <td>No. Of Trucks Required (if container stuffing not possible)
                        </td>
                        <td>
                           <asp:TextBox ID="txtNoOfTruckIntEdit" runat="server" Style="width: 100%;" Enabled="true" />
                        </td>

                    </tr>
                     <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Readiness Date:</td>
                        <td>
                           <table width="70%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox15" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                    <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton9"
                                        runat="server" TargetControlID="TextBox15" >
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />

                                    <%-- OnClientClick="return false;"--%>
                                </td>
                            </tr>
                        </table>

                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Type Of Consignment:
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlTypeConsignmentEditInt" runat="server" Style="width: 100%;" Height="26px" onblur="return ValidateModeOfTravel();"
                            onchange="EnableModeOfTravel()">
                        </asp:DropDownList>
                        </td>

                        </tr>
                     <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>

                        
                    <td>In case of Sea Shipment:</td>
                   
                    <td>
                        
                    <asp:Panel ID="Panel6" runat="server" Width="150px" Height="50px">
                        <table border="1" cellpadding="5">
                            <tr>
                                <th Width="80px">Container type/size</th>
                                <th>Qty of Containers Required</th>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("ContainerName") %>'></asp:Label>
                                        </td>
                                        <asp:HiddenField ID="hfContainerType" runat="server" Value='<%# Eval("ContainerName") %>' />
                                       <td>
                                        <asp:TextBox ID="txtQtyContainer" runat="server" CssClass="qty-input-container" Width="200px" 
                                                     Text='<%# Eval("QtyCon") %>' oninput="updateContainerTotal()"></asp:TextBox>
                                    </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>

                    </td>
                        <td>&nbsp;
                        </td>
                     <td style="padding-left: 50px;">Packing List:</td>
                    <td>
                        <asp:FileUpload ID="fileUpload4" runat="server" Enabled="true" Width="100%"
                            Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                    </td>   
                    </tr>
                     <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                    <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td>&nbsp;
                    </td>
                    </tr>
                <tr>

                    <td>Sub Vendor Invoice:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUpload5" runat="server" Enabled="true" Width="100%"
                            Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                    </td>
                </tr>
                    <tr  id="cancelInt0" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>
                    
                    <tr id="cancelInt2" runat="server">
                         <td>Cancel Remarks:
                        </td>
                       
                        <td colspan="4">
                            <asp:TextBox ID="txtCancelRemarksInt" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                Rows="2" />
                        </td>
                        
                    </tr>
                  
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                
                </table>

               
                <br />
                  <div id="submitbtncanceleditint" runat="server" style="margin-top: 5px; text-align: center;">
                <%--<div style="margin-top: 30px; text-align: center;">--%>
                    <asp:Button
                        ID="Button4"
                        runat="server"
                        Text="Update Inward Information"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnSubmit_Click"
                        Style="background-color: #28a745; color: white; padding: 10px 20px; border: none; border-radius: 5px; width: 50%; cursor: pointer;" />
                </div>

                <br />

            </div>

        </fieldset>
    </asp:Panel>
   
    <%--------------------------------international edit modal ----------------------------------------------------%>



    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" CssClass="popup-pdf">
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
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server">
        </iframe>
    </asp:Panel>
 
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe class="popup-iframe"
            id="iframeViewTourInformationInPDF"
            runat="server">
        </iframe>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="popup-edit"
        Style="display: none">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="Label1" runat="server" /></legend>
            <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Employee Name:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Employee ID:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Designation:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox2" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Starting Date Of Tour:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgbtnStartDate" runat="server"
                                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Start Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>End Date Of Tour:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgbtnEndDate" runat="server"
                                            TargetControlID="TextBox4" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Start Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4" align="center">
                            <asp:Label ID="Label2" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>Name of Customer/Vendor:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox5" runat="server" Width="100%" Enabled="true" onblur="return ValidateCustVendName();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Place of Visit:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox6" runat="server" Width="100%" Enabled="true" onblur="return ValidatePlaceOfVisit();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <%--      ///////////--%>






                    <tr>
                        <td>Country of Visit:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="100%" Height="25px"
                                onblur="return ValidatePurposeOfVisit();">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>Customer Based On:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="100%" Height="25px"
                                onblur="return ValidateTourBasedOn();">
                            </asp:DropDownList>
                        </td>
                    </tr>


                    <%-- ////////////--%>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Purpose of Visit:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList4" runat="server" Width="100%" Height="25px"
                                onblur="return ValidatePurposeOfVisit();">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>Job/Inq No. Where Applicable:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" Width="100%" Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Business Segment:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateBusinessSegment();" onchange="EnableBusinessSegmentOther()">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" Width="100%" Visible="true" Enabled="false"
                                onblur="return ValidateBusinessSegmentOther();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Mode of Travel:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList6" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateModeOfTravel();" onchange="EnableModeOfTravel()">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server" Width="100%" Visible="true" Enabled="false"
                                onblur="return ValidateModeOfTravelOther();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Type of Trip:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList7" runat="server" Width="100%" Height="26px" onblur="return ValidateTypeOfTrip();">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>Tour Initiative:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList8" runat="server" Width="100%" Height="25px">
                            </asp:DropDownList>


                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>In Case of Local Travelling:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList9" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateLocalTravelling();" onchange="EnableLocalTravelling()">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" Width="100%" Visible="true" Enabled="false"
                                onblur="return ValidateLocalTravellingOther();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Expected Expenditure Of The Trip:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 70%;">
                                        <asp:TextBox ID="TextBox11" runat="server" Width="100%" Enabled="true" onblur="return ValidateExpenditureAmt();"
                                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList10" runat="server" Width="100%" Height="26px" onchange="ChangeByExpenditureCurrency()">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td>Advance Required:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 70%;">
                                        <asp:TextBox ID="TextBox12" runat="server" Width="100%" Enabled="true" onkeyup="checkDec(this);"
                                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList11" runat="server" Width="100%" Height="26px" onchange="ChangeByAdvanceCurrency()">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>





                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Remarks:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox13" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                Rows="4" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4" align="center">
                            <asp:Panel ID="Panel2" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td colspan="4">
                            <asp:Button ID="Button1" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                OnClick="btnSubmit_Click" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </fieldset>
    </asp:Panel>


    <asp:Button ID="btnShowEditPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="btnShowEditPopup"
        PopupControlID="pnlpopup01" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup01" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="ImageButton4" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="Label4" runat="server" /></legend>
            <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Inward Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInwardNo" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Job Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNo1" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor Name:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtVenName" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor Location:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVenLoc" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Vendor Email:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVenEmail" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Vendor Contact Details For Unloading:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVenConDetForUnloading" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Material Delivery Location:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMaterialDelLocation" runat="server" Width="100%" Height="25px" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Incoterms:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIncoTerms" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Delivery Term:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeliveryTerms" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>No. Of Trucks Required(If Possible):
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoOfTruckReq" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Date:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox14" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                        <asp:CalendarExtender ID="CalendarExtender5" PopupButtonID="imgbtnStartDate" runat="server"
                                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Start Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td>Type Of Consignment:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTypeOfConsignment" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Packing List Doc:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList23" runat="server" Width="100%" Height="25px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td>Subitem Invoice:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList16" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateBusinessSegment();" onchange="EnableBusinessSegmentOther()">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>

                        <td>Enter Truck Details:</td>
                        <td>

                            <asp:Panel ID="pnlTrucks" runat="server" Width="300px" Height="50px">
                                <table border="1" cellpadding="5">
                                    <tr>
                                        <th width="100px">Full Truck</th>
                                        <th>Qty of Trucks Required</th>
                                    </tr>
                                    <asp:Repeater ID="rptTrucks" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTruck" runat="server" Text='<%# Eval("TruckName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfTruckType" runat="server" Value='<%# Eval("TruckName") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Qty Of Containers:
                        </td>
                        <td>

                            <asp:Panel ID="Panel3" runat="server" Width="300px" Height="50px">
                                <table border="1" cellpadding="5">
                                    <tr>
                                        <th width="100px">Container type/size</th>
                                        <th>Qty of Containers Required</th>
                                    </tr>
                                    <asp:Repeater ID="rptContainer" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("ContainerName") %>'></asp:Label>
                                                </td>
                                                <asp:HiddenField ID="hfContainerType" runat="server" Value='<%# Eval("ContainerName") %>' />
                                                <td>
                                                    <asp:TextBox ID="txtQtyContainer" runat="server" CssClass="qty-input-truck-edit" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>

                        </td>
                        <td></td>
                        <td>&nbsp;
                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr id="domesticrow1" runat="server">
                        <td>LR No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLRNo" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>LR Date:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtLRDate" runat="server" Width="100%"  Enabled="true"></asp:TextBox>
                                        <asp:HiddenField ID="hdLRDate" runat="server" />
                                        <asp:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton5" runat="server"
                                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="LR Date Calendar"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr id="domesticrow2" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr id="domesticrow3" runat="server">
                        <td>Truck No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTruckNo" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>POD(Attachment):
                        </td>
                        <td>
                            <asp:FileUpload ID="podattachment1" runat="server" Enabled="true" Width="100%"
                                Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                        </td>
                    </tr>

                    <tr id="domesticrow4" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr id="internationalrow1" runat="server">
                        <td>FCR/BL:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFCRBL" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>FCR/BL Date:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDateFCRBL" runat="server" Width="100%"  Enabled="true" ></asp:TextBox>
                                        <asp:HiddenField ID="hdDateFCRBL" runat="server" />
                                        <asp:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton6" runat="server"
                                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="LR Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr id="internationalrow2" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr id="internationalrow3" runat="server">
                        <td>Container No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtContainerNo" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>POD(Attachment):
                        </td>
                        <td>
                            <asp:FileUpload ID="txtPODAttachment2" runat="server" Enabled="true" Width="100%"
                                Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                        </td>
                    </tr>

                    <tr id="internationalrow4" runat="server">
                        <td>&nbsp;
                        </td>
                    </tr>






                    <tr>

                        <td>
                            <asp:Button ID="Button3" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                OnClick="btnSubmit_Click" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </fieldset>
    </asp:Panel>



    <asp:Button ID="btnShowAppConfPopUp" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ApproveConfirmPopUp" runat="server" TargetControlID="btnShowAppConfPopUp"
        PopupControlID="pnlapproveconfirm" CancelControlID="imgBtnCancel02" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlapproveconfirm" runat="server" CssClass="popup-edit">
        <asp:ImageButton ID="imgBtnCancel02" runat="server" ImageUrl="~/Images/cancelled_img.png"
            Style="position: absolute; top: 0; right: 0; z-index: 1000;" />
        <br />
        <table style="width: 90%; margin: 15px auto; padding: 2px 2px; border: 1px solid #ddd;">
            <%--   <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel02" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" Style="margin-right: 0;" />
                </td>
            </tr>--%>
            <tr>

                <td style="width: 15%;">JOB Number:</td>

                <td style="width: 30%">
                    <asp:TextBox ID="txtJobNoIO" runat="server" Width="100%" Enabled="false" onblur="return ValidateJOBNo();" />
                </td>
                <td>&nbsp;
                </td>
                <td>Vendor Name:</td>
                <td style="width: 40%">
                    <asp:TextBox ID="txtVenNameIO" runat="server" Style="width: 100%;" Enabled="false" />
                </td>

            </tr>

            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>Vendor Location/Address:</td>
                <td>
                    <asp:TextBox ID="txtVenLocIO" runat="server" Style="width: 100%;" Enabled="false" />
                </td>
                <td>&nbsp;
                </td>
                <td>Vendor's Email:</td>
                <td>
                    <asp:TextBox ID="txtVenEmailIO" runat="server" Style="width: 100%;" Enabled="false" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>

            <tr>
                <td>Vendor Contact Details for unloading (If available):</td>
                <td>
                    <asp:TextBox ID="txtVenContactIO" runat="server" Style="width: 100%;" Enabled="false" />
                </td>
                <td>&nbsp;
                </td>
                <td>Material Delivery Location:</td>
                <td>
                    <asp:DropDownList ID="ddlDeliveryLocIO" runat="server" Width="100%" Height="25px" Enabled="false" onblur="return ValidateEmployee();">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>Incoterms:</td>
                <td>

                   
                    <asp:TextBox ID="txtIncoterms" runat="server" Width="100%" Height="25px" Enabled="false">


                    </asp:TextBox>

                </td>
                <td>&nbsp;
                </td>
                <td>Delivery Term:</td>
                <td>
                   

                    <asp:TextBox ID="txtDelTerm" runat="server" Width="100%" Height="25px" Enabled="false">


                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>

            <tr>
                <td>No. Of Trucks Required (if container not possible)</td>
                <td>
                    <asp:TextBox ID="txtIsNoOfTrucksReqIO" runat="server" Style="width: 100%;" Enabled="false" />
                </td>
                <td>&nbsp;
                </td>

                <td>Readiness Date:
                </td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    <asp:HiddenField ID="hdDate" runat="server" />
                    <asp:CalendarExtender ID="CalendarExtender7" PopupButtonID="imgBtnDate"
                                        runat="server" TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations">
                            </asp:CalendarExtender>
                  
                </td>
                <td>
                    <asp:ImageButton ID="imgBtnDate" runat="server" OnClientClick="return false;" ImageUrl="~/Images/Calendar2.png"
                        ToolTip="Date of Logging Lesson Learnt" />
                </td>
            </tr>

            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>Type Of Consignment:</td>
                <td>
                    <%--  <asp:DropDownList ID="ddlTypeOfConsignmentIO" runat="server" Width="100%" Height="25px" onblur="return ValidateEmployee();"
                            AutoPostBack="true">
                        </asp:DropDownList>--%>

                    <asp:TextBox ID="txtTypeOfConsignmentC" runat="server" Style="width: 100%;" Enabled="false" />
                </td>
                <td>&nbsp;
                </td>
                <td>Type Of Inward:</td>
                <td>
                    <asp:TextBox ID="txtTypeInward" runat="server" Style="width: 100%;" Enabled="false" />
                </td>

            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>


           <%-- <tr>

                <td>Packing List:
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadVisitSummary1" runat="server" Enabled="false" Width="100%"
                        Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                </td>
                <td>&nbsp;
                </td>
                <td>Sub Vendor Invoice:
                </td>
                <td>
                    <asp:FileUpload ID="fileUpload3" runat="server" Enabled="false" Width="100%"
                        Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                </td>

            </tr>--%>
            <tr>
                <td>&nbsp;</td>
            </tr>
            
                 <tr runat="server" id="row0domesticC">
                
                        <td>Truck Details:</td>
                        <td>
                            <asp:Panel ID="Panel7" runat="server" Width="300px" Height="50px">
                                <table border="1" cellpadding="5">
                                    <tr>
                                        <th width="80px">Full Truck</th>
                                        <th>Qty of Trucks Required</th>
                                    </tr>
                                    <asp:Repeater ID="rptConfirmDomestic" runat="server" OnItemDataBound="rptTrucks_ItemDataBound" EnableViewState="true">

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTruck" runat="server" Text='<%# Eval("TruckName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfTruckType" runat="server" Value='<%# Eval("TruckName") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input" Width="200px" oninput="updateTruckTotal()"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>

                        </td>
                        <td>&nbsp;
                        </td>
                       <%-- <td>Packing List:
                        </td>
                        <td>

                            <asp:FileUpload ID="fileUpload6" runat="server" Enabled="true" Width="100%"
                                Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                        </td>
--%>

            </tr>
            <tr runat="server" id="DomesticTruckSpacing1">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing2">
                <td>&nbsp;</td>
            </tr>
             <tr runat="server" id="DomesticTruckSpacing3">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing4">
                <td>&nbsp;</td>
            </tr>
             <tr runat="server" id="DomesticTruckSpacing5">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing6">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing7">
                <td>&nbsp;</td>
            </tr>
             <tr runat="server" id="DomesticTruckSpacing8">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing9">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing10">
                <td>&nbsp;</td>
            </tr>
             <tr runat="server" id="DomesticTruckSpacing11">
                <td>&nbsp;</td>
            </tr>
            <%--<tr  runat="server" id="DomesticTruckSpacing12">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing13">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="DomesticTruckSpacing14">
                <td>&nbsp;</td>
            </tr>--%>


            <tr runat="server" id="row1domesticC">

                <td>LR No.:
                </td>
                <td>
                    <asp:TextBox ID="txtLRNoC" runat="server" Style="width: 100%;" Enabled="true" />
                </td>
                <td>&nbsp;
                </td>
                <td>LR Date.:
                </td>
                <td>
                    <asp:TextBox ID="txtLRDateC" runat="server" Width="100%" Enabled="true" AutoPostBack="false"></asp:TextBox>
                    <asp:HiddenField ID="hdLRDateC" runat="server" />
                     <asp:CalendarExtender ID="CalendarExtender8" PopupButtonID="imgLRDate"
                                        runat="server" TargetControlID="txtLRDateC" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations">
                            </asp:CalendarExtender>

                </td>
                <td>
                    <asp:ImageButton ID="imgLRDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                        ToolTip="LR Date" />
                </td>

            </tr>

            <tr id="row2domesticC" runat="server">

                <td>Truck No.:
                </td>
                <td>
                    <asp:TextBox ID="txtTruckNoC" runat="server" Style="width: 100%;" Enabled="true" />
                </td>
                <td>&nbsp;
                </td>
                <%--<td>POD.:
                </td>

                <td>
                    <asp:FileUpload ID="txtPOD1" runat="server" Enabled="true" Width="100%"
                        Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                </td>--%>
                <td>Vehicle Placement Date.:
                </td>
                <td>
                    <asp:TextBox ID="txtVehiclePDate2D" runat="server" Width="100%" Enabled="true"  ></asp:TextBox>
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                    <asp:CalendarExtender ID="CalendarExtender10" PopupButtonID="imgVehiclePDate2D"
                                        runat="server" TargetControlID="txtVehiclePDate2D" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations">
                            </asp:CalendarExtender>
                </td>
                <td>
                    <asp:ImageButton ID="imgVehiclePDate2D" runat="server" ImageUrl="~/Images/Calendar2.png"
                        ToolTip="Vehicle Placement Date" />
                </td>

            </tr>

             <tr runat="server" id="row0InternationalC">

                <td>Container Details:</td>
                        <td>
                            <asp:Panel ID="Panel8" runat="server" Width="300px" Height="50px">
                                <table border="1" cellpadding="5">
                                    <tr>
                                        <th width="80px">Container type/size</th>
                                        <th>Qty of Container Required</th>
                                    </tr>
                 <asp:Repeater ID="rptConfirmInternational" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("ContainerName") %>'></asp:Label>
                                        </td>
                                        <asp:HiddenField ID="hfContainerType" runat="server" Value='<%# Eval("ContainerName") %>' />
                                       <td>
                                        <asp:TextBox ID="txtQtyContainer" runat="server" CssClass="qty-input-container" Width="200px" 
                                                     Text='<%# Eval("QtyCon") %>' oninput="updateContainerTotal()"></asp:TextBox>
                                    </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                                    </table>
                                    </asp:Panel>
                            </td>

            </tr>
            <tr runat="server" id="intContainerspacing1">
                <td>&nbsp;</td>
            </tr>
            <tr  runat="server" id="intContainerspacing2">
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="intContainerspacing3">
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="intContainerspacing4">
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="intContainerspacing5">
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="intContainerspacing6">
                <td>&nbsp;</td>
            </tr>
            <tr runat="server" id="intContainerspacing7">
                <td>&nbsp;</td>
            </tr>

            <tr id="row1InternationalC" runat="server">

                <td>FCR /BL:
                </td>
                <td>
                    <asp:TextBox ID="txtFCRBLC" runat="server" Style="width: 100%;" Enabled="true" />
                </td>
                <td>&nbsp;
                </td>
                <td>FCR/BL Date.:
                </td>
                <td>
                    <asp:TextBox ID="txtFCRBLDateC" runat="server" Width="100%" Enabled="true"  ></asp:TextBox>
                    <asp:HiddenField ID="hdFCRBLC" runat="server" />
                    <asp:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgFCRBLDateC"
                                        runat="server" TargetControlID="txtFCRBLDateC" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations">
                            </asp:CalendarExtender>
                </td>
                <td>
                    <asp:ImageButton ID="imgFCRBLDateC" runat="server" ImageUrl="~/Images/Calendar2.png"
                        ToolTip="FCR/BL Date" />
                </td>




            </tr>
            <tr id="rowa1" runat="server">
                <td>&nbsp;</td>
            </tr>
            <tr id="row2InternationalC" runat="server">

                <td>Container No.:
                </td>
                <td>
                    <asp:TextBox ID="txtContainerNoC" runat="server" Style="width: 100%;" Enabled="true" />
                </td>
                <td>&nbsp;
                </td>
                <td>Vehicle Placement Date.:
                </td>
                <td>
                    <asp:TextBox ID="txtVehiclePDate" runat="server" Width="100%" Enabled="true"  ></asp:TextBox>
                    <asp:HiddenField ID="hfVPDate" runat="server" />
                    <asp:CalendarExtender ID="calVPDate" PopupButtonID="imgVehiclePDate"
                                        runat="server" TargetControlID="txtVehiclePDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdations">
                            </asp:CalendarExtender>
                </td>
                <td>
                    <asp:ImageButton ID="imgVehiclePDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                        ToolTip="FCR/BL Date" />
                </td>


            </tr>
            <tr id="rowa2" runat="server">
                <td>&nbsp;</td>
            </tr>
             <tr id="transporter0" runat="server">
                
            </tr>
            <tr id="transporter0a" runat="server">
              <td>Transporter Name:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtTransporterName" runat="server" Width="100%" Enabled="true" />
                    </td>   
            </tr>
            <tr id="rowa3" runat="server">
                <td>&nbsp;</td>
            </tr>
            <tr id="rowb3" runat="server">
                <td>&nbsp;</td>
            </tr>
             <tr id="transporter1" runat="server">
                 <td>Transporter's Contact No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransporterPhn" runat="server" Width="100%" Enabled="true" />
                    </td>
                     <td>&nbsp;</td>
                 <td>Transporter's Email:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransporterEmail" runat="server" Width="100%" Enabled="true" />
                    </td>
            </tr>
             <tr id="transporter2" runat="server">
                <td>&nbsp;</td>
            </tr>
             <tr>
                 <td> Comment:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtApproveRemark" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                            Rows="2" />
                    </td>
            </tr>
             <tr>
                <td>&nbsp;</td>
            </tr>

        </table>
        <table>

            <div style="width: 100%; text-align: left;">
                <div style="display: inline-block; margin-left: 230px; margin-top: 20px;">
                    <asp:Button ID="btnSubmitCONFIRM" CssClass="button" runat="server" Text="Confirm"
                        Width="300px" Style="margin-right: 20px;" OnClick="btnSubmitCONFIRM_Click" 
                        OnClientClick="return ConfirmValidate();" />
                    <%--  OnClientClick="return ValidateAll();"--%>
                    <br />
                    <br />

                </div>
            </div>

        </table>


    </asp:Panel>



</asp:Content>
