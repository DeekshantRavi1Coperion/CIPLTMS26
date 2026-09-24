<%@ Page Title="CIPLTMS- Add New Fin Ops" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddFinOps.aspx.cs" Inherits="FINOPS_AddFinOps" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtInvoiceDate.ClientID %>').value = document.getElementById('<%=hdInvoiceDate.ClientID %>').value;
        }

        function clientChanged() {
            document.getElementById('<%=hdInvoiceDate.ClientID %>').value = document.getElementById('<%=txtInvoiceDate.ClientID %>').value;
        }
    </script>



    <script type="text/javascript">

        function ValidateInvoiceNo() {
            var InvoiceNo = document.getElementById('<%=txtInvoiceNo.ClientID %>').value;
            if (InvoiceNo == '') {
                document.getElementById('<%=txtInvoiceNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtInvoiceNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJOBNo() {
            var JOBNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCostCenter() {
            var CostCenter = document.getElementById('<%=txtCostCenter.ClientID %>').value;
            if (CostCenter == '') {
                document.getElementById('<%=txtCostCenter.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCostCenter.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateType() {
            var Type = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Type == '' || Type == '0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCostType() {
            var CostType = document.getElementById('<%=ddlCostType.ClientID %>').selectedIndex;
            if (CostType == '' || CostType == '0') {
                document.getElementById('<%=ddlCostType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCostType.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateBU() {
            var BU = document.getElementById('<%=txtBU.ClientID %>').value;
            if (BU == '') {
                document.getElementById('<%=txtBU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAmount1() {
            var Amount1 = document.getElementById('<%=txtAmount1.ClientID %>').value;
            if (Amount1 == '') {
                document.getElementById('<%=txtAmount1.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAmount1.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateHours() {
            var Hours = document.getElementById('<%=txtHours.ClientID %>').value;
            if (Hours == '') {
                document.getElementById('<%=txtHours.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtHours.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePaneltyClause() {
            var PaneltyClause = document.getElementById('<%=txtPaneltyClause.ClientID %>').value;
            if (PaneltyClause == '') {
                document.getElementById('<%=txtPaneltyClause.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPaneltyClause.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePaymentTerm() {
            var PaymentTerm = document.getElementById('<%=txtPaymentTerm.ClientID %>').value;
            if (PaymentTerm == '') {
                document.getElementById('<%=txtPaymentTerm.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPaymentTerm.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePONo() {
            var PONo = document.getElementById('<%=txtPONo.ClientID %>').value;
            if (PONo == '') {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateContigencyType() {
            var ContigencyType = document.getElementById('<%=ddlContigencyType.ClientID %>').selectedIndex;
            if (ContigencyType == '' || ContigencyType == '0') {
                document.getElementById('<%=ddlContigencyType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlContigencyType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateIsVPOCOrder() {
            var IsVPOCOrder = document.getElementById('<%=ddlIsVPOCOrder.ClientID %>').selectedIndex;
            if (IsVPOCOrder == '' || IsVPOCOrder == '0') {
                document.getElementById('<%=ddlIsVPOCOrder.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlIsVPOCOrder.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateStatus1() {
            var Status1 = document.getElementById('<%=ddlStatus1.ClientID %>').selectedIndex;
            if (Status1 == '' || Status1 == '0') {
                document.getElementById('<%=ddlStatus1.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStatus1.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAmount2() {
            var txtAmount2 = document.getElementById('<%=txtAmount2.ClientID %>').value;
            if (txtAmount2 == '') {
                document.getElementById('<%=txtAmount2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAmount2.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateStatus2() {
            var Status2 = document.getElementById('<%=ddlStatus2.ClientID %>').selectedIndex;
            if (Status2 == '' || Status2 == '0') {
                document.getElementById('<%=ddlStatus2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStatus2.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateItemCode() {
            var ItemCode = document.getElementById('<%=txtItemCode.ClientID %>').value;
            if (ItemCode == '') {
                document.getElementById('<%=txtItemCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemDesc() {
            var ItemDesc = document.getElementById('<%=txtItemDesc.ClientID %>').value;
            if (ItemDesc == '') {
                document.getElementById('<%=txtItemDesc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemDesc.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateItemGroup() {
            var ItemGroup = document.getElementById('<%=txtItemGroup.ClientID %>').value;
            if (ItemGroup == '') {
                document.getElementById('<%=txtItemGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateItemSubgroup() {
            var ItemSubgroup = document.getElementById('<%=txtItemSubgroup.ClientID %>').value;
            if (ItemSubgroup == '') {
                document.getElementById('<%=txtItemSubgroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemSubgroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemPivotGroup() {
            var ItemPivotGroup = document.getElementById('<%=txtItemPivotGroup.ClientID %>').value;
            if (ItemPivotGroup == '') {
                document.getElementById('<%=txtItemPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemPivotGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var check = true;

            if (ValidateInvoiceNo()) {
                check = false;
            }

            if (ValidateJOBNo()) {
                check = false;
            }

            if (ValidateCostCenter()) {
                check = false;
            }

            if (ValidateType()) {
                check = false;
            }

            if (ValidateCostType()) {
                check = false;
            }

            if (ValidateBU()) {
                check = false;
            }

            if (ValidateAmount1()) {
                check = false;
            }

            if (ValidateHours()) {
                check = false;
            }

            if (ValidatePaneltyClause()) {
                check = false;
            }

            if (ValidatePaymentTerm()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateContigencyType()) {
                check = false;
            }

            if (ValidateIsVPOCOrder()) {
                check = false;
            }

            if (ValidateStatus1()) {
                check = false;
            }

            if (ValidateAmount2()) {
                check = false;
            }

            if (ValidateStatus2()) {
                check = false;
            }

            if (ValidateItemCode()) {
                check = false;
            }

            if (ValidateItemDesc()) {
                check = false;
            }

            if (ValidateItemGroup()) {
                check = false;
            }

            if (ValidateItemSubgroup()) {
                check = false;
            }

            if (ValidateItemPivotGroup()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
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

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }


        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };


    </script>




    <script type="text/javascript" language="javascript">
        function onCalendarMonthShown() {
            var cal = $find("calendarMonth");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callMonth);
                    }
                }
            }
        }

        function onCalendarMonthHidden() {
            var cal = $find("calendarMonth");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callMonth);
                    }
                }
            }
        }

        function callMonth(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarMonth");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <script type="text/javascript" language="javascript">
        function onCalendarDeliveryMonthShown() {
            var cal = $find("calendarDeliveryMonth");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callDeliveryMonth);
                    }
                }
            }
        }

        function onCalendarDeliveryMonthHidden() {
            var cal = $find("calendarDeliveryMonth");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callDeliveryMonth);
                    }
                }
            }
        }

        function callDeliveryMonth(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarDeliveryMonth");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
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
    <div align="center" style="margin-top: 2px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">Add New Fin Ops</legend>
            <table width="100%">
                <%--<tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="4" align="center">
                        
                    </td>
                </tr>--%>
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td>Invoice No:</td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" runat="server" Width="100%" onblur="return ValidateInvoiceNo();" />
                    </td>
                    <td>&nbsp;</td>
                    <td>Invoice Date:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 100%;">
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdInvoiceDate" runat="server" />
                                    <ajax:CalendarExtender ID="calendarInvoiceDate" PopupButtonID="imgbtnInvoiceDate"
                                        runat="server" TargetControlID="txtInvoiceDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnInvoiceDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Month:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 100%;">
                                    <asp:TextBox ID="txtMonth" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>                                    
                                    <ajax:CalendarExtender ID="calendarMonth" runat="server" OnClientHidden="onCalendarMonthHidden"
                                        PopupButtonID="imgbtnMonth" OnClientShown="onCalendarMonthShown" Format="MM/yyyy"
                                        BehaviorID="calendarMonth" TargetControlID="txtMonth">
                                    </ajax:CalendarExtender>

                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>

                    <td>JOB No:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" onblur="return ValidateJOBNo();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Cost Center:</td>
                    <td>
                        <asp:TextBox ID="txtCostCenter" runat="server" Width="100%" onblur="return ValidateCostCenter();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="100%" Height="25px" onblur="return ValidateType();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Cost Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlCostType" runat="server" Width="100%" Height="25px" onblur="return ValidateCostType();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>BU:</td>
                    <td>
                        <asp:TextBox ID="txtBU" runat="server" Width="100%" onblur="return ValidateBU();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>


                <tr>
                    <td>Amount1:</td>
                    <td>
                        <asp:TextBox ID="txtAmount1" runat="server" Width="100%"
                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);"
                            onblur="return ValidateAmount1();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Hours:</td>
                    <td>
                        <asp:TextBox ID="txtHours" runat="server" Width="100%" onblur="return ValidateHours();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Panelty Clause:</td>
                    <td>
                        <asp:TextBox ID="txtPaneltyClause" runat="server" Width="100%" onblur="return ValidatePaneltyClause();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Payment Term:</td>
                    <td>
                        <asp:TextBox ID="txtPaymentTerm" runat="server" Width="100%" onblur="return ValidatePaymentTerm();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>PO No:</td>
                    <td>
                        <asp:TextBox ID="txtPONo" runat="server" Width="100%" onblur="return ValidatePONo();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Contigency Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlContigencyType" runat="server" Width="100%" Height="25px" onblur="return ValidateContigencyType();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Is VPOC Order:</td>
                    <td>
                        <asp:DropDownList ID="ddlIsVPOCOrder" runat="server" Width="100%" Height="25px" onblur="return ValidateIsVPOCOrder();">
                            <asp:ListItem Text="Select" />
                            <asp:ListItem Text="Yes" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>

                    <td>Delivery Month:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 100%;">
                                    <asp:TextBox ID="txtDeliveryMonth" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>                                    
                                    <ajax:CalendarExtender ID="calendarDeliveryMonth" runat="server" OnClientHidden="onCalendarDeliveryMonthHidden"
                                        PopupButtonID="imgbtnDeliveryMonth" OnClientShown="onCalendarDeliveryMonthShown" Format="MM/yyyy"
                                        BehaviorID="calendarDeliveryMonth" TargetControlID="txtDeliveryMonth">
                                    </ajax:CalendarExtender>

                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtDeliveryMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnDeliveryMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="DeliveryMonth Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Status1:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus1" runat="server" Width="100%" Height="25px" onblur="return ValidateStatus1();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Amount2:</td>
                    <td>
                        <asp:TextBox ID="txtAmount2" runat="server" Width="100%"
                            onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);"
                            onblur="return ValidateAmount2();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Status2:</td>
                    <td>
                        <asp:DropDownList ID="ddlStatus2" runat="server" Width="100%" Height="25px" onblur="return ValidateStatus2();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Item Code:</td>
                    <td>
                        <asp:TextBox ID="txtItemCode" runat="server" Width="100%" onblur="return ValidateItemCode();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Item Desc:</td>
                    <td>
                        <asp:TextBox ID="txtItemDesc" runat="server" Width="100%" onblur="return ValidateItemDesc();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Item Group:</td>
                    <td>
                        <asp:TextBox ID="txtItemGroup" runat="server" Width="100%" onblur="return ValidateItemGroup();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>Item Subgruop:</td>
                    <td>
                        <asp:TextBox ID="txtItemSubgroup" runat="server" Width="100%" onblur="return ValidateItemSubgroup();" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Item Pivot Group:</td>
                    <td>
                        <asp:TextBox ID="txtItemPivotGroup" runat="server" Width="100%" onblur="return ValidateItemPivotGroup();" />
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                            OnClick="btnSubmit_Click" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">
                        <asp:Button ID="btnFinOpsList" CssClass="button" runat="server" Text="Fin Ops List"
                            OnClick="btnFinOpsList_Click" Width="100%" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
