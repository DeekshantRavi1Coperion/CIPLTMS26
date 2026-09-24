<%@ Page Title="Tour Information" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="MonthlyInput.aspx.cs" Inherits="PROJECT_MGMT_MonthlyInput" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtMonth.ClientID %>').value = document.getElementById('<%=hdMonth.ClientID %>').value;
        }

        function clientChangedMonth(sender, args) {
            document.getElementById('<%=hdMonth.ClientID %>').value = document.getElementById('<%=txtMonth.ClientID %>').value;
        }
    </script>

    <script type="text/javascript">

        function ValidateJOBNo() {
            var JOBNo = document.getElementById('<%=ddlJOBNo.ClientID %>').selectedIndex;
            if (JOBNo == '' || JOBNo == '0') {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMonth() {
            var Month = document.getElementById('<%=txtMonth.ClientID %>').value;
            if (Month == '') {
                document.getElementById('<%=txtMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateVPOC() {
            var VPOC = document.getElementById('<%=txtVPOC.ClientID %>').value;
            if (VPOC == '') {
                document.getElementById('<%=txtVPOC.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVPOC.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateICPOC() {
            var ICPOC = document.getElementById('<%=txtICPOC.ClientID %>').value;
            if (ICPOC == '') {
                document.getElementById('<%=txtICPOC.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtICPOC.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateMaterial() {
            var Material = document.getElementById('<%=txtMaterial.ClientID %>').value;
            if (Material == '') {
                document.getElementById('<%=txtMaterial.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMaterial.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateEngineering() {
            var Engineering = document.getElementById('<%=txtEngineering.ClientID %>').value;
            if (Engineering == '') {
                document.getElementById('<%=txtEngineering.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEngineering.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateTravelling() {
            var Travelling = document.getElementById('<%=txtTravelling.ClientID %>').value;
            if (Travelling == '') {
                document.getElementById('<%=txtTravelling.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTravelling.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOtherCost() {
            var OtherCost = document.getElementById('<%=txtOtherCost.ClientID %>').value;
            if (OtherCost == '') {
                document.getElementById('<%=txtOtherCost.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOtherCost.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateWarranty() {
            var Warranty = document.getElementById('<%=txtWarranty.ClientID %>').value;
            if (Warranty == '') {
                document.getElementById('<%=txtWarranty.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtWarranty.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCommission() {
            var Commission = document.getElementById('<%=txtCommission.ClientID %>').value;
            if (Commission == '') {
                document.getElementById('<%=txtCommission.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCommission.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRoyalty() {
            var Royalty = document.getElementById('<%=txtRoyalty.ClientID %>').value;
            if (Royalty == '') {
                document.getElementById('<%=txtRoyalty.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRoyalty.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLateDelivery() {
            var LateDelivery = document.getElementById('<%=txtLateDelivery.ClientID %>').value;
            if (LateDelivery == '') {
                document.getElementById('<%=txtLateDelivery.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtLateDelivery.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;



            if (ValidateJOBNo()) {
                return false;
            }

            if (ValidateMonth()) {
                return false;
            }

            if (ValidateVPOC()) {
                return false;
            }

            if (ValidateICPOC()) {
                return false;
            }

            if (ValidateMaterial()) {
                return false;
            }

            if (ValidateEngineering()) {
                return false;
            }

            if (ValidateTravelling()) {
                return false;
            }

            if (ValidateOtherCost()) {
                return false;
            }

            if (ValidateWarranty()) {
                return false;
            }

            if (ValidateCommission()) {
                return false;
            }

            if (ValidateRoyalty()) {
                return false;
            }

            if (ValidateLateDelivery()) {
                return false;
            }

            return check;
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
        function onCalendarShown() {
            var cal = $find("calendarMonth");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendarMonth");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 100px;">
        <fieldset style="width: 50%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" Text="Monthly Input Form"></asp:Label></legend>
            <table width="100%">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        JOB No:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlJOBNo" runat="server" Width="100%" Height="25px" onblur="return ValidateJOBNo();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Month:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMonth" runat="server" ReadOnly="true" Width="100%" onblur="return ValidateMonth();"></asp:TextBox>
                                    <asp:HiddenField ID="hdMonth" runat="server" />
                                    <asp:CalendarExtender ID="calendarMonth" runat="server" OnClientHidden="onCalendarHidden"
                                        PopupButtonID="imgbtnMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                        BehaviorID="calendarMonth" TargetControlID="txtMonth" OnClientDateSelectionChanged="clientChangedMonth">
                                    </asp:CalendarExtender>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        VPOC:
                    </td>
                    <td>
                        <asp:TextBox ID="txtVPOC" runat="server" Width="100%" onblur="return ValidateVPOC();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        ICPOC:
                    </td>
                    <td>
                        <asp:TextBox ID="txtICPOC" runat="server" Width="100%" onblur="return ValidateICPOC();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Material:
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaterial" runat="server" Width="100%" onblur="return ValidateMaterial();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Engineering:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEngineering" runat="server" Width="100%" onblur="return ValidateEngineering();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Travelling:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTravelling" runat="server" Width="100%" onblur="return ValidateTravelling();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Other Cost:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOtherCost" runat="server" Width="100%" onblur="return ValidateOtherCost();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Warranty:
                    </td>
                    <td>
                        <asp:TextBox ID="txtWarranty" runat="server" Width="100%" onblur="return ValidateWarranty();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        Commission:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCommission" runat="server" Width="100%" onblur="return ValidateCommission();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Royalty:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoyalty" runat="server" Width="100%" onblur="return ValidateRoyalty();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        Late Delivery:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLateDelivery" runat="server" Width="100%" onblur="return ValidateLateDelivery();"
                            onkeyup="checkDec(this);" onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Remarks:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                        OnClick="btnSubmit_Click" Width="100%" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnMonthlyInputList" CssClass="button" runat="server" Text="Monthly Input Report"
                                        Width="100%" OnClick="btnMonthlyInputList_Click" />
                                </td>
                            </tr>
                        </table>
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
