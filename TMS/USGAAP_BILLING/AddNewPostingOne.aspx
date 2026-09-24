<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="AddNewPostingOne.aspx.cs"
    Inherits="USGAAP_BILLING_AddNewPostingOne" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%-- <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>


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
            if (InvoiceAmount == '' || InvoiceAmount == '-') {
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
            if (PostedValue == '' || PostedValue == '-') {
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

        function ValidateInvoiceAmountNew() {
            var InvoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            if (InvoiceAmount == '' || InvoiceAmount == '-') {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtInvoiceAmount.ClientID %>').style.borderColor = "";
                return false;
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


            if (UnpostedValue == '' || UnpostedValue < 0) {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                alert("Unposted value must be greater than or equal to zero...!!");
                return true;
            }
            else {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
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

            if (ValidateInvoiceAmountNew()) { return false; }

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


            if (document.getElementById('<%=hdUnpostedValue.ClientID %>').value < 0) {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
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

    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Add New Posting</legend>

            <div class="form-grid form-grid-2">
                <label>Invoice No.</label>
                <asp:TextBox ID="txtInvoiceNo" runat="server"
                    CssClass="form-control" />

                <label>Company</label>
                <asp:DropDownList ID="ddlCompany" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Invoice Date</label>
                <table width="100%">
                    <tr>
                        <td style="width: 90%">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdInvoiceDate" runat="server" />
                            <asp:CalendarExtender ID="calendarInvoiceDate" PopupButtonID="imgBtnInvoiceDate"
                                runat="server" TargetControlID="txtInvoiceDate" Format="dd-MMM-yyyy"
                                OnClientDateSelectionChanged="clientChangedInvoiceDate">
                            </asp:CalendarExtender>
                        </td>
                        <td style="width: 10%; text-align: right;">
                            <asp:ImageButton ID="imgBtnInvoiceDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Calendar" />
                        </td>
                    </tr>
                </table>

                <label>Job No.</label>
                <asp:TextBox ID="txtJobNo" runat="server" CssClass="form-control"
                    MaxLength="15" />

                <label>Customer Name</label>
                <div class="full-width">
                    <table width="100%">
                        <tr>
                            <td style="width: 75%">
                                <asp:TextBox ID="txtCustomerName" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCode" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 10%">
                                <asp:Button ID="btnSelectCustomer" runat="server" Width="100%" Text="Select" CssClass="button"
                                    OnClick="btnSelectCustomer_Click" />
                            </td>
                        </tr>
                    </table>
                </div>



                <label>Customer Address</label>
                <div class="full-width">
                    <asp:TextBox ID="txtCustAddress" runat="server"
                        CssClass="form-control"
                        Enabled="false" TextMode="MultiLine"
                        Rows="2" />
                </div>

                <label>Business Unit</label>
                <asp:TextBox ID="txtBusinessUnit" runat="server" CssClass="form-control"
                    MaxLength="3" />

                <label>Product Code</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtProductCode" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>
                        <td style="width: 30%">
                            <asp:Button ID="btnSelectProdCode" runat="server" Width="100%" Text="Select" CssClass="button"
                                OnClick="btnSelectProdCode_Click" />
                        </td>
                    </tr>
                </table>

                <label>Revenue Account</label>
                <div class="full-width">
                    <table width="100%">
                        <tr>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtRevenueAccount" runat="server" Enabled="false" CssClass="form-control" />
                            </td>
                            <td style="width: 70%">
                                <asp:TextBox ID="txtRevenueAccountDesc" runat="server" CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 10%">
                                <asp:TextBox ID="txtRevenueAccountType" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 30%">
                                <asp:Button ID="btnGetRevenueaccount" runat="server"
                                    Text="Select" CssClass="button"
                                    OnClick="btnGetRevenueaccount_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <label>Quantity</label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                    onkeyup="checkDecNew(this);"
                    onkeypress="return inNumberKeyWithDecimal(this, event);" />

                <label>Product Rate</label>
                <asp:TextBox ID="txtProductRate" runat="server" CssClass="form-control"
                    onkeyup="checkDecNew(this);" onkeypress="return inNumberKeyWithDecimal(this, event);" />

                <label>Invoice Amount</label>
                <asp:TextBox ID="txtInvoiceAmount" runat="server" CssClass="form-control"
                    onkeypress="return inNumberKey(this, event);" onkeyup="checkDecNew3(this);" />

                <label>Posted Value</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtPostedValue" runat="server" CssClass="form-control"
                                onkeyup="checkDecNew3(this);" onkeypress="return inNumberKey(this, event);" />
                        </td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddlPostingCurrency" runat="server" Enabled="false"
                                CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <label>Unposted Value</label>
                <asp:TextBox ID="txtUnpostedValue" runat="server" Enabled="false" CssClass="form-control"
                    onkeyup="checkDecNew3(this);" />
                <asp:HiddenField ID="hdUnpostedValue" runat="server" />

                <label>End Market</label>
                <asp:DropDownList ID="ddlEndMarket" runat="server" CssClass="form-control">
                </asp:DropDownList>

                <label>Geogrophy</label>
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                </asp:DropDownList>

                <label>Posting Month-Year</label>
                <table width="100%">
                    <tr>
                        <td style="width: 40%">
                            <asp:DropDownList ID="ddlPostingMonth" runat="server" CssClass="form-control">
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
                            <asp:DropDownList ID="ddlPostingYear" runat="server" CssClass="form-control">
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

                <label>Type</label>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                </asp:DropDownList>

                <label>Revenue/Not Revenue Type</label>
                <asp:DropDownList ID="ddlRevenueType" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Rev. Rec. Reduction</label>
                <asp:TextBox ID="txtRevRecReduction" runat="server"
                    CssClass="form-control"
                    Enabled="true" />

                <label>UDF2</label>
                <asp:TextBox ID="txtUDF2" runat="server"
                    CssClass="form-control"
                    Enabled="true" />

                <label>UDF3</label>
                <asp:TextBox ID="txtUDF3" runat="server"
                    CssClass="form-control"
                    Enabled="true" />

                <label>UDF4</label>
                <asp:TextBox ID="txtUDF4" runat="server"
                    CssClass="form-control"
                    Enabled="true" />

                <label>UDF5</label>
                <asp:TextBox ID="txtUDF5" runat="server"
                    CssClass="form-control"
                    Enabled="true" />

            </div>
        </fieldset>
        <div class="full-width button-group">
            <asp:Button
                ID="btnSave"
                CssClass="button"
                runat="server"
                Text="Save"
                OnClientClick="return ValidateAll();"
                Width="100%"
                OnClick="btnSave_Click" />

            <asp:Button
                ID="btnPostedList"
                CssClass="button"
                runat="server"
                Text="Posted List"
                Width="100%"
                OnClick="btnPostedList_Click" />
        </div>
        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
        </div>

    </div>

    <asp:Button ID="btnShowPopup2" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopup2"
        PopupControlID="pnlpopup2" CancelControlID="imgBtnCancel2" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup2" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel2" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">
            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblCustomerRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-2">
                        <label>Customer Name</label>
                        <asp:TextBox ID="txtCustmerNameSearch" runat="server" CssClass="form-control" />

                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server" CssClass="form-control" />

                        <asp:Button
                            ID="btnSearchCustomer"
                            CssClass="button"
                            runat="server"
                            Text="Search"
                            Width="100%"
                            OnClick="btnSearchCustomer_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblCustomerMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvCustomerDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
    </asp:Panel>


    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">
            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblProductRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-2">
                        <label>Procuct Code</label>
                        <asp:TextBox ID="txtProcuctCodeSearch" runat="server" CssClass="form-control" />

                        <label>Procuct Description</label>
                        <asp:TextBox ID="txtProcuctDescSearch" runat="server" CssClass="form-control" />

                        <asp:Button
                            ID="btnSearchProductCode"
                            CssClass="button"
                            runat="server"
                            Text="Search"
                            Width="100%"
                            OnClick="btnSearchProductCode_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblProductMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvProductDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
    </asp:Panel>



    <asp:Button ID="btnShowPopup3" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPopup3"
        PopupControlID="pnlpopup3" CancelControlID="imgBtnCancel3" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup3" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel3" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">
            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblRevenueAccRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>Revenue Account</label>
                        <asp:TextBox ID="txtRevenueAccountSearch" runat="server" CssClass="form-control" />

                        <label>Account Desc</label>
                        <asp:TextBox ID="txtRevenueAccountDescSearch" runat="server" CssClass="form-control" />

                        <asp:Button
                            ID="btnSearchRevenueAccount"
                            CssClass="button"
                            runat="server"
                            Text="Search"
                            Width="100%"
                            OnClick="btnSearchRevenueAccount_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblRevenueAccMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvRevenueAccount" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" 
                    OnRowCommand="gvRevenueAccount_RowCommand">
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


    </asp:Panel>
    <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>
