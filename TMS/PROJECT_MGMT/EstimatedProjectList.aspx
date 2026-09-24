<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="EstimatedProjectList.aspx.cs" Inherits="PROJECT_MGMT_EstimatedProjectList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

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

    <script type="text/javascript">

        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescription.ClientID %>').value;
            if (Description== '') {
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        

        function ValidateQuantity() {
            var Quantity= document.getElementById('<%=txtQuantity.ClientID %>').value;
            if (Quantity == '') {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnitRateINR() {
            var UnitRateINR = document.getElementById('<%=txtUnitRateINR.ClientID %>').value;
            if (UnitRateINR== '') {
                document.getElementById('<%=txtUnitRateINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUnitRateINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePriceINR() {
            var PriceINR = document.getElementById('<%=txtPriceINR.ClientID %>').value;
            if (PriceINR == '') {
                document.getElementById('<%=txtPriceINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPriceINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
        function ValidatePandF() {
            var PandF = document.getElementById('<%=txtPandF.ClientID %>').value;
            if (PandF== '') {
                document.getElementById('<%=txtPandF.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPandF.ClientID %>').style.borderColor = "";
                return false;
            }
        }
               
        function ValidatePFD() {
            var PFD = document.getElementById('<%=txtPFD.ClientID %>').value;
            if (PFD == '') {
                document.getElementById('<%=txtPFD.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPFD.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateED() {
            var ED = document.getElementById('<%=txtED.ClientID %>').value;
            if (ED == '') {
                document.getElementById('<%=txtED.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtED.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
        function ValidateTotalED() {
            var TotalED = document.getElementById('<%=txtTotalED.ClientID %>').value;
            if (TotalED == '') {
                document.getElementById('<%=txtTotalED.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTotalED.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateST() {
            var ST = document.getElementById('<%=txtST.ClientID %>').value;
            if (ST == '') {
                document.getElementById('<%=txtST.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtST.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
        function ValidateSTAmt() {
            var STAmt = document.getElementById('<%=txtSTAmt.ClientID %>').value;
            if (STAmt == '') {
                document.getElementById('<%=txtSTAmt.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSTAmt.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                
        function ValidateTotalCostInclEDST() {
            var TotalCostInclEDST = document.getElementById('<%=txtTotalCostInclEDST.ClientID %>').value;
            if (TotalCostInclEDST == '') {
                document.getElementById('<%=txtTotalCostInclEDST.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTotalCostInclEDST.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                        
        function ValidateImportedEXWRateEURO() {
            var ImportedEXWRateEURO = document.getElementById('<%=txtImportedEXWRateEURO.ClientID %>').value;
            if (ImportedEXWRateEURO == '') {
                document.getElementById('<%=txtImportedEXWRateEURO.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtImportedEXWRateEURO.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateImportedEXWPriceEURO() {
            var ImportedEXWPriceEURO = document.getElementById('<%=txtImportedEXWPriceEURO.ClientID %>').value;
            if (ImportedEXWPriceEURO == '') {
                document.getElementById('<%=txtImportedEXWPriceEURO.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtImportedEXWPriceEURO.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                        
        function ValidateFOBPriceEURO() {
            var FOBPriceEURO = document.getElementById('<%=txtFOBPriceEURO.ClientID %>').value;
            if (FOBPriceEURO == '') {
                document.getElementById('<%=txtFOBPriceEURO.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtFOBPriceEURO.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateEquivRupeePrice() {
            var EquivRupeePrice = document.getElementById('<%=txtEquivRupeePrice.ClientID %>').value;
            if (EquivRupeePrice == '') {
                document.getElementById('<%=txtEquivRupeePrice.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEquivRupeePrice.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateFullDuty() {
            var FullDuty = document.getElementById('<%=txtFullDuty.ClientID %>').value;
            if (FullDuty == '') {
                document.getElementById('<%=txtFullDuty.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtFullDuty.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateTotalImportCost() {
            var TotalImportCost = document.getElementById('<%=txtTotalImportCost.ClientID %>').value;
            if (TotalImportCost == '') {
                document.getElementById('<%=txtTotalImportCost.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTotalImportCost.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidateTotalCostINR() {
            var TotalCostINR = document.getElementById('<%=txtTotalCostINR.ClientID %>').value;
            if (TotalCostINR  == '') {
                document.getElementById('<%=txtTotalCostINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTotalCostINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                               
        function ValidateTotalCostEURO() {
            var TotalCostEURO = document.getElementById('<%=txtTotalCostEURO.ClientID %>').value;
            if (TotalCostEURO  == '') {
                document.getElementById('<%=txtTotalCostEURO.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTotalCostEURO.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                        
        function ValidateCategory() {
            var Category = document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex;
            if (Category == '' || Category == '0') {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateCodes() {
            var Codes = document.getElementById('<%=ddlCodes.ClientID %>').selectedIndex;
            if (Codes == '' || Codes == '0') {
                document.getElementById('<%=ddlCodes.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCodes.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                                       
    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if ( ValidateDescription()) {return false;}            
            if ( ValidateQuantity()) {return false;}
            if ( ValidateUnitRateINR()) {return false;}
            if ( ValidatePriceINR()) {return false;}
            if ( ValidatePandF()) {return false;}
            if ( ValidatePFD()) {return false;}
            if ( ValidateED()) {return false;}
            if ( ValidateTotalED()) {return false;}
            if ( ValidateST()) {return false;}
            if ( ValidateSTAmt()) {return false;}
            if ( ValidateTotalCostInclEDST()) {return false;}
            if ( ValidateImportedEXWRateEURO()) {return false;}
            if ( ValidateImportedEXWPriceEURO()) {return false;}
            if ( ValidateFOBPriceEURO()) {return false;}
            if ( ValidateEquivRupeePrice()) {return false;}
            if ( ValidateFullDuty()) {return false;}
            if ( ValidateTotalImportCost()) {return false;}
            if ( ValidateTotalCostINR()) {return false;}
            if ( ValidateTotalCostEURO()) {return false;}
            if ( ValidateCategory()) {return false;}
            if ( ValidateCodes()) {return false;}
            return check;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Estimated Project List</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
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
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
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
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Project No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnImportNewEstimatedProject" CssClass="button" Width="100%" runat="server"
                            Text="Import New Estimated Project" OnClick="btnImportNewEstimatedProject_Click" />
                    </td>
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
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 100%; border: 1px solid lightgray;'>
                <asp:GridView ID="gvEstimatedProjectList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="15" Width="100%"
                    HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvEstimatedProjectList_PageIndexChanging"
                    OnRowCommand="gvEstimatedProjectList_RowCommand" OnRowDataBound="gvEstimatedProjectList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                                <asp:Label ID="lblProjectID" runat="server" Visible="false" Text='<%# Eval("ESTIMATED_PROJECT_ID") %>' />
                                <asp:Label ID="lblProjectNo" runat="server" Visible="false" Text='<%# Eval("PROJECT_NO") %>' />
                                <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>' />
                                <asp:Label ID="lblScope" runat="server" Visible="false" Text='<%# Eval("SCOPE") %>' />
                                <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                                <asp:Label ID="lblUnitRateINR" runat="server" Visible="false" Text='<%# Eval("UNIT_RATE_INR") %>' />
                                <asp:Label ID="lblPriceINR" runat="server" Visible="false" Text='<%# Eval("PRICE_INR") %>' />
                                <asp:Label ID="lblPandF" runat="server" Visible="false" Text='<%# Eval("P_AND_F") %>' />
                                <asp:Label ID="lblPFD" runat="server" Visible="false" Text='<%# Eval("PFD") %>' />
                                <asp:Label ID="lblED" runat="server" Visible="false" Text='<%# Eval("ED") %>' />
                                <asp:Label ID="lblTotalED" runat="server" Visible="false" Text='<%# Eval("TOTAL_ED") %>' />
                                <asp:Label ID="lblST" runat="server" Visible="false" Text='<%# Eval("ST") %>' />
                                <asp:Label ID="lblSTAmt" runat="server" Visible="false" Text='<%# Eval("ST_AMT") %>' />
                                <asp:Label ID="lblTotalCostInclEDST" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_INCL_ED_ST") %>' />
                                <asp:Label ID="lblImportedEXWRateEuro" runat="server" Visible="false" Text='<%# Eval("IMPORTED_EX_W_RATE_EURO") %>' />
                                <asp:Label ID="lblImportedEXWPriceEURO" runat="server" Visible="false" Text='<%# Eval("IMPORTED_EX_W_PRICE_EURO") %>' />
                                <asp:Label ID="lblFOBPriceEuro" runat="server" Visible="false" Text='<%# Eval("FOB_PRICE_EURO") %>' />
                                <asp:Label ID="lblEquivRupeePrice" runat="server" Visible="false" Text='<%# Eval("EQUIV_RUPEE_PRICE") %>' />
                                <asp:Label ID="lblFullDuty" runat="server" Visible="false" Text='<%# Eval("FULL_DUTY") %>' />
                                <asp:Label ID="lblTotalImportCost" runat="server" Visible="false" Text='<%# Eval("TOTAL_IMPORT_COST") %>' />
                                <asp:Label ID="lblTotalCostINR" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_INR") %>' />
                                <asp:Label ID="lblTotalCostEURO" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_EURO") %>' />
                                <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("CATEGORY") %>' />
                                <asp:Label ID="lblCodes" runat="server" Visible="false" Text='<%# Eval("CODES") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PROJECT_NO" HeaderText="PROJECT_NO" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                        <asp:BoundField DataField="SCOPE" HeaderText="SCOPE" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="UNIT_RATE_INR" HeaderText="UNIT_RATE_INR" />
                        <asp:BoundField DataField="PRICE_INR" HeaderText="PRICE_INR" />
                        <asp:BoundField DataField="P_AND_F" HeaderText="P_AND_F" />
                        <asp:BoundField DataField="PFD" HeaderText="PFD" />
                        <asp:BoundField DataField="ED" HeaderText="ED" />
                        <asp:BoundField DataField="TOTAL_ED" HeaderText="TOTAL_ED" />
                        <asp:BoundField DataField="ST" HeaderText="ST" />
                        <asp:BoundField DataField="ST_AMT" HeaderText="ST_AMT" />
                        <asp:BoundField DataField="TOTAL_COST_INCL_ED_ST" HeaderText="TOTAL_COST_INCL_ED_ST" />
                        <asp:BoundField DataField="IMPORTED_EX_W_RATE_EURO" HeaderText="IMPORTED_EX_W_RATE_EURO" />
                        <asp:BoundField DataField="IMPORTED_EX_W_PRICE_EURO" HeaderText="IMPORTED_EX_W_PRICE_EURO" />
                        <asp:BoundField DataField="FOB_PRICE_EURO" HeaderText="FOB_PRICE_EURO" />
                        <asp:BoundField DataField="EQUIV_RUPEE_PRICE" HeaderText="EQUIV_RUPEE_PRICE" />
                        <asp:BoundField DataField="FULL_DUTY" HeaderText="FULL_DUTY" />
                        <asp:BoundField DataField="TOTAL_IMPORT_COST" HeaderText="TOTAL_IMPORT_COST" />
                        <asp:BoundField DataField="TOTAL_COST_INR" HeaderText="TOTAL_COST_INR" />
                        <asp:BoundField DataField="TOTAL_COST_EURO" HeaderText="TOTAL_COST_EURO" />
                        <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                        <asp:BoundField DataField="CODES" HeaderText="CODES" />
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
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="900px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">Project No. - [<asp:Label ID="lblLegendProjectNo"
                runat="server" />]</legend>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 20px;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description:
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Width="100%"
                                onblur="return ValidateDescription();" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Scope:
                        </td>
                        <td>
                            <asp:TextBox ID="txtScope" runat="server" Width="100%" />
                        </td>
                        <td>
                            Quantity:
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="100%" onblur="return ValidateQuantity();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Unit Rate INR:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUnitRateINR" runat="server" Width="100%" onblur="return ValidateUnitRateINR();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Price INR:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceINR" runat="server" Width="100%" onblur="return ValidatePriceINR();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PandF:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPandF" runat="server" Width="100%" onblur="return ValidatePandF();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            PFD:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPFD" runat="server" Width="100%" onblur="return ValidatePFD();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ED:
                        </td>
                        <td>
                            <asp:TextBox ID="txtED" runat="server" Width="100%" onblur="return ValidateED();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Total ED:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalED" runat="server" Width="100%" onblur="return ValidateTotalED();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ST:
                        </td>
                        <td>
                            <asp:TextBox ID="txtST" runat="server" Width="100%" onblur="return ValidateST();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            ST Amt:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSTAmt" runat="server" Width="100%" onblur="return ValidateSTAmt();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total Cost Incl. ED ST:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalCostInclEDST" runat="server" Width="100%" onblur="return ValidateTotalCostInclEDST();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Imported EXW Rate EURO:
                        </td>
                        <td>
                            <asp:TextBox ID="txtImportedEXWRateEURO" runat="server" Width="100%" onblur="return ValidateImportedEXWRateEURO();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Imported EXW Price EURO:
                        </td>
                        <td>
                            <asp:TextBox ID="txtImportedEXWPriceEURO" runat="server" Width="100%" onblur="return ValidateImportedEXWPriceEURO();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            FOB Price EURO:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFOBPriceEURO" runat="server" Width="100%" onblur="return ValidateFOBPriceEURO();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Equiv Rupee Price:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEquivRupeePrice" runat="server" Width="100%" onblur="return ValidateEquivRupeePrice();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Full Duty:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFullDuty" runat="server" Width="100%" onblur="return ValidateFullDuty();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total Import Cost:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalImportCost" runat="server" Width="100%" onblur="return ValidateTotalImportCost();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Total Cost INR:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalCostINR" runat="server" Width="100%" onblur="return ValidateTotalCostINR();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total Cost EURO:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotalCostEURO" runat="server" Width="100%" onblur="return ValidateTotalCostEURO();"
                                onkeyup="checkDec(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            Category:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" Height="25px" onblur="return ValidateCategory();">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Codes
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCodes" runat="server" Width="100%" Height="25px" onblur="return ValidateCodes();">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                OnClick="btnSubmit_Click" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
            margin-left: 25px;'>
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewPDFFile"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewTourInformationInPDF"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
