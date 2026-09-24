<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="AddNewPosting.aspx.cs"
    Inherits="USGAAP_BILLING_AddNewPosting" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
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

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtInvoiceDate.ClientID %>').value = document.getElementById('<%=hdInvoiceDate.ClientID %>').value;
        }

        function clientChangedInvoiceDate(sender, args) {
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

        function ValidateCompany() {
            var Company = document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex;
            if (Company == '' || Company == '0') {
                document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJobNo() {
            var JobNo = document.getElementById('<%=txtJobNo.ClientID %>').value;
            if (JobNo == '') {
                document.getElementById('<%=txtJobNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJobNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustName() {
            var CustName = document.getElementById('<%=txtCustomerName.ClientID %>').value;
            if (CustName == '') {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustCode() {
            var CustCode = document.getElementById('<%=txtCustomerCode.ClientID %>').value;
            if (CustCode == '') {
                document.getElementById('<%=txtCustomerCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBusinessUnit() {
            var BusinessUnit = document.getElementById('<%=txtBusinessUnit.ClientID %>').value;
            if (BusinessUnit == '') {
                document.getElementById('<%=txtBusinessUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBusinessUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevenueAccount() {
            var RevenueAccount = document.getElementById('<%=txtRevenueAccount.ClientID %>').value;
            if (RevenueAccount == '') {
                document.getElementById('<%=txtRevenueAccount.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRevenueAccount.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value;
            if (Quantity == '') {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProductRate() {
            var ProductRate = document.getElementById('<%=txtProductRate.ClientID %>').value;
            if (ProductRate == '') {
                document.getElementById('<%=txtProductRate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductRate.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateInvoiceAmount() {
            var InvoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            if (InvoiceAmount == '') {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (parseFloat(InvoiceAmount) != 0) {
                    document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

        function ValidatePostedValue() {
            var PostedValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            if (PostedValue == '') {
                document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (parseFloat(PostedValue) >= 0) {
                    document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "";
                    return false;

                }
                else {
                    document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }
     
        function ValidatePostedValueNew() {
            var PostedValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            if (PostedValue == '' || PostedValue == '-') {
                document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnpostedValue() {
            var UnpostedValue = document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            if (UnpostedValue == '') {
                document.getElementById('<%=hdUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=hdUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEndMarket() {
            var EndMarket = document.getElementById('<%=ddlEndMarket.ClientID %>').selectedIndex;
            if (EndMarket == '' || EndMarket == '0') {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCountry() {
            var Country = document.getElementById('<%=ddlCountry.ClientID %>').selectedIndex;
            if (Country == '' || Country == '0') {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "";
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

        function ValidateRevenueType() {
            var RevenueType = document.getElementById('<%=ddlRevenueType.ClientID %>').selectedIndex;
            if (RevenueType == '' || RevenueType == '0') {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateInvoiceNo()) { return false; }

            if (ValidateCompany()) { return false; }

            if (ValidateJobNo()) { return false; }

            if (ValidateCustName()) { return false; }

            if (ValidateCustCode()) { return false; }

            if (ValidateBusinessUnit()) { return false; }

            if (ValidateRevenueAccount()) { return false; }

            if (ValidateQuantity()) { return false; }

            if (ValidateProductRate()) { return false; }

            if (ValidateInvoiceAmount()) { return false; }
            
            if (ValidatePostedValueNew()) { return false; }

            if (ValidateUnpostedValue()) { return false; }

            if (ValidateEndMarket()) { return false; }

            if (ValidateCountry()) { return false; }

            if (ValidateType()) { return false; }

            if (ValidateRevenueType()) { return false; }


            return check;
        }                                                                      
    </script>

    <script type="text/Javascript">
        function checkDecNew(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }        
    </script>

    <script type="text/Javascript">

        function checkDecNew1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    checkDecNew2();
                }
            }
            else {
                checkDecNew2();
            }
        }


        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }

            else {
                if (charCode < 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }


        function isNumberKeyNew(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (charCode == 45) {
                if (txt.value.indexOf('-') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode < 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }



        function checkDecNew2() {
            var invoiceAmount = '0';
            var postedValue = '0';


            if (document.getElementById('<%=txtInvoiceAmount.ClientID %>').value != '') {
                invoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            }
            else {
                invoiceAmount = '0';
            }

            if (document.getElementById('<%=txtPostedValue.ClientID %>').value != '') {
                postedValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            }
            else {
                postedValue = '0';
            }

            document.getElementById('<%=hdUnpostedValue.ClientID %>').value = Math.round((parseFloat(invoiceAmount) - parseFloat(postedValue)) * 100) / 100;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValue.ClientID %>').value;

            if (parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value) >= 0) {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
            else {
                document.getElementById('<%=txtPostedValue.ClientID %>').value = invoiceAmount;
                document.getElementById('<%=hdUnpostedValue.ClientID %>').value = '0';
                document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function checkDecNew3() {
            var invoiceAmount = '0';
            var postedValue = '0';


            if (document.getElementById('<%=txtInvoiceAmount.ClientID %>').value != '') {
                invoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            }
            else {
                invoiceAmount = '0';
            }

            if (document.getElementById('<%=txtPostedValue.ClientID %>').value != '') {
                postedValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            }
            else {
                postedValue = '0';
            }

            document.getElementById('<%=hdUnpostedValue.ClientID %>').value = Math.round((parseFloat(invoiceAmount) - parseFloat(postedValue)) * 100) / 100;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
        }
        
    </script>

    <script type="text/javascript">
        function inNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 45) {
                if (txt.value.indexOf('-') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (charCode == 46) {
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 10px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">Add New Posting</legend>
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
            <br />
            <table style="width: 95%; height: 100%; margin-left: 10px;">
                <tr>
                    <td>
                        Invoice No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" runat="server" Width="100%" onblur="return ValidateInvoiceNo();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" onblur="return ValidateCompany();">
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
                        Invoice Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdInvoiceDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarInvoiceDate" PopupButtonID="imgBtnInvoiceDate"
                                        runat="server" TargetControlID="txtInvoiceDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedInvoiceDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 10%; text-align: right;">
                                    <asp:ImageButton ID="imgBtnInvoiceDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Job No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server" Width="100%" onblur="return ValidateJobNo();"
                            MaxLength="15" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Custoner Name:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 75%">
                                    <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustName();" />
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtCustomerCode" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustCode();" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="btnSelectCustomer" runat="server" Width="100%" Text="Select" CssClass="button"
                                        OnClick="btnSelectCustomer_Click" />
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
                        Custoner Address:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtCustAddress" runat="server" Width="100%" Enabled="false" TextMode="MultiLine"
                            Rows="2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Business Unit:
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusinessUnit" runat="server" Width="100%" onblur="return ValidateBusinessUnit();"
                            MaxLength="3" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Product Code:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtProductCode" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td style="width: 30%">
                                    <asp:Button ID="btnSelectProdCode" runat="server" Width="100%" Text="Select" CssClass="button"
                                        OnClick="btnSelectProdCode_Click" />
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
                        Revenue Account:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtRevenueAccount" runat="server" Width="100%" Enabled="false" onblur="return ValidateRevenueAccount();" />
                                </td>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtRevenueAccountDesc" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td style="width: 10%">
                                    <asp:TextBox ID="txtRevenueAccountType" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td style="width: 30%">
                                    <asp:Button ID="btnGetRevenueaccount" runat="server" Width="100%" Text="Select" CssClass="button"
                                        OnClick="btnGetRevenueaccount_Click" />
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
                        Quantity:
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Width="100%" onblur="return ValidateQuantity();"
                            onkeyup="checkDecNew(this);" onkeypress="return inNumberKeyWithDecimal(this, event);"/>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Product Rate:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductRate" runat="server" Width="100%" onblur="return ValidateProductRate();"
                            onkeyup="checkDecNew(this);" onkeypress="return inNumberKeyWithDecimal(this, event);"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Invoice Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoiceAmount" runat="server" Width="100%" onblur="return ValidateInvoiceAmount();"
                            onkeypress="return inNumberKey(this, event);" /><%--onkeyup="checkDecNew1(this);"--%>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Posted Value:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%">
                                    <asp:TextBox ID="txtPostedValue" runat="server" Width="100%" onblur="return ValidatePostedValueNew();"
                                        onkeyup="checkDecNew3(this);" onkeypress="return inNumberKey(this, event);" />
                                </td>
                                <td style="width: 30%">
                                    <asp:DropDownList ID="ddlPostingCurrency" runat="server" Enabled="false" Width="100%"
                                        onblur="return ValidatePostingCurrency();">
                                    </asp:DropDownList>
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
                    <%--<td>
                                Posted Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%">
                                            <asp:TextBox ID="txtPostedDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdPostedDate" runat="server" />
                                            <asp:CalendarExtender ID="calendarPostedDate" PopupButtonID="imgBtnPostedDate" runat="server"
                                                TargetControlID="txtPostedDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedPostedDate">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnPostedDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Calendar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>--%>
                    <td>
                        Unposted Value:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnpostedValue" runat="server" Width="100%" Enabled="false" onblur="return ValidateUnpostedValue();"
                            onkeyup="checkDecNew3(this);" />
                        <asp:HiddenField ID="hdUnpostedValue" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        End Market:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEndMarket" runat="server" Width="100%" onblur="return ValidateEndMarket();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Geogrophy:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="100%" onblur="return ValidateCountry();">
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
                        Posting Month-Year:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td style="width: 40%">
                                    <asp:DropDownList ID="ddlPostingMonth" runat="server" Width="100%">
                                        <asp:ListItem Value="1" Text="Jan" />
                                        <asp:ListItem Value="2" Text="Feb" />
                                        <asp:ListItem Value="3" Text="Mar" />
                                        <asp:ListItem Value="4" Text="Apr" />
                                        <asp:ListItem Value="5" Text="May" />
                                        <asp:ListItem Value="6" Text="Jun" />
                                        <asp:ListItem Value="7" Text="Jul" />
                                        <asp:ListItem Value="8" Text="Aug" />
                                        <asp:ListItem Value="9" Text="Sep" />
                                        <asp:ListItem Value="10" Text="Oct" />
                                        <asp:ListItem Value="11" Text="Nov" />
                                        <asp:ListItem Value="12" Text="Dec" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 60%">
                                    <asp:DropDownList ID="ddlPostingYear" runat="server" Width="100%">
                                        <asp:ListItem Value="2015" Text="2015" />
                                        <asp:ListItem Value="2016" Text="2016" />
                                        <asp:ListItem Value="2017" Text="2017" />
                                        <asp:ListItem Value="2018" Text="2018" />
                                        <asp:ListItem Value="2019" Text="2019" />
                                        <asp:ListItem Value="2020" Text="2020" />
                                        <asp:ListItem Value="2021" Text="2021" />
                                        <asp:ListItem Value="2022" Text="2022" />
                                        <asp:ListItem Value="2023" Text="2023" />
                                        <asp:ListItem Value="2024" Text="2024" />
                                        <asp:ListItem Value="2025" Text="2025" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="100%" onblur="return ValidateType();"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
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
                        Revenue/Not Revenue Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRevenueType" runat="server" Width="100%" onblur="return ValidateRevenueType();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Rev. Rec. Reduction:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRevRecReduction" runat="server" Width="100%" Enabled="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        UDF2:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUDF2" runat="server" Width="100%" Enabled="true" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        UDF3:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUDF3" runat="server" Width="100%" Enabled="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        UDF4:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUDF4" runat="server" Width="100%" Enabled="true" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        UDF5:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUDF5" runat="server" Width="100%" Enabled="true" />
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
                    <td>
                        <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                            Width="100%" OnClick="btnSave_Click" />
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnPostedList" CssClass="button" runat="server" Text="Posted List"
                            Width="100%" OnClick="btnPostedList_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <asp:Button ID="btnShowPopup2" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopup2"
        PopupControlID="pnlpopup2" CancelControlID="imgBtnCancel2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" Height="600px" Width="1000px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel2" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblCustomerRecords" runat="server" Text="Records[0]" /></legend>
            <br />
            <asp:Label ID="lblCustomerMsg" runat="server" />
            <br />
            <table style="width: 95%; margin-left: 20px;">
                <tr>
                    <td>
                        Customer Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustmerNameSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Customer Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearchCustomer" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchCustomer_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <div align="center">
                    <asp:GridView ID="gvCustomerDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvCustomerDetail_RowCommand">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="GET_CUSTOMER" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                    <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                    <asp:Label ID="lblAddress" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />
                                    <asp:Button ID="btnGetCustomer" CommandArgument="GET_CUSTOMER" ToolTip="Get Customer"
                                        runat="server" Text="Get Customer" CssClass="cancelbutton" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                            <asp:BoundField DataField="CITY" HeaderText="CITY" />
                            <asp:BoundField DataField="STATE" HeaderText="STATE" />
                            <asp:BoundField DataField="PINCODE" HeaderText="PINCODE" />
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
        </fieldset>
    </asp:Panel>
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
            <legend style="text-align: center;">
                <asp:Label ID="lblProductRecords" runat="server" Text="Records[0]" /></legend>
            <br />
            <asp:Label ID="lblProductMsg" runat="server" />
            <br />
            <table style="width: 95%; margin-left: 20px;">
                <tr>
                    <td>
                        Procuct Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProcuctCodeSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Procuct Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProcuctDescSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearchProductCode" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchProductCode_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <div align="center">
                    <asp:GridView ID="gvProductDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvProductCode_RowCommand">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="GET">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCode" runat="server" Visible="false" Text='<%# Eval("PRODCODE") %>' />
                                    <asp:Label ID="lblProductDesc" runat="server" Visible="false" Text='<%# Eval("DESCRIPT") %>' />
                                    <asp:Button ID="btnGetProductCode" CommandArgument="GET" ToolTip="Get Procut Code"
                                        runat="server" Text="Get Prod. Code" CssClass="cancelbutton" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRODCODE" HeaderText="PRODUCT_CODE" />
                            <asp:BoundField DataField="DESCRIPT" HeaderText="PRODUCT_DESC" />
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
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnShowPopup3" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPopup3"
        PopupControlID="pnlpopup3" CancelControlID="imgBtnCancel3" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup3" runat="server" BackColor="White" Height="600px" Width="900px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel3" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRevenueAccRecords" runat="server" Text="Records[0]" /></legend>
            <br />
            <asp:Label ID="lblRevenueAccMsg" runat="server" />
            <br />
            <table style="width: 95%; margin-left: 20px;">
                <tr>
                    <td>
                        Revenue Account:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRevenueAccountSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Account Desc:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRevenueAccountDescSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearchRevenueAccount" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchRevenueAccount_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <div align="center">
                    <asp:GridView ID="gvRevenueAccount" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvRevenueAccount_RowCommand">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="GET">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevenueAccount" runat="server" Visible="false" Text='<%# Eval("GLCODE") %>' />
                                    <asp:Label ID="lblRevenueAccountDesc" runat="server" Visible="false" Text='<%# Eval("DESCRIPT") %>' />
                                    <asp:Label ID="lblRevenueAccountType" runat="server" Visible="false" Text='<%# Eval("PL_BS") %>' />
                                    <asp:Button ID="btnGetProductCode" CommandArgument="GET" ToolTip="Get Procut Code"
                                        runat="server" Text="Get Rev. Acc." CssClass="cancelbutton" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GLCODE" HeaderText="REVENUE_ACCOUNT" />
                            <asp:BoundField DataField="DESCRIPT" HeaderText="REVENUE_ACCOUNT_DESC" />
                            <asp:BoundField DataField="PL_BS" HeaderText="REVENUE_ACCOUNT_TYPE" />
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
        </fieldset>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
