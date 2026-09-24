<%@ Page Title="CIPLTMS-Add FC Vendor Payment" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddFCVendorPayment.aspx.cs" Inherits="FINANCE_FC_PAYMENT_AddFCVendorPayment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../Images/Icons/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

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
        .textboxdrawings {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: whitesmoke;
        }

        .textboxleft {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxcenter {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxright {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }
    </style>


    <style type="text/css">
        /* File Upload Design  */
        .file-upload {
            display: inline-block;
            overflow: hidden;
            position: relative;
            text-align: center;
            vertical-align: middle;
            /* Cosmetics */
            /*border: 1px solid #5C005C;*/
            /*background: #5C005C;*/
            background: #003a5cba;
            color: #fff;
            /* browser can do it */
            border-radius: 6px;
            -moz-border-radius: 6px;
            /*text-shadow: #000 1px 1px 2px;*/
            -webkit-border-radius: 6px;
        }

        /* The button size */
        .file-upload {
            height: 2.3em;
        }

            .file-upload, .file-upload span {
                /*width: 3.5em;*/
                width: 100%;
            }

                .file-upload input {
                    position: absolute;
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    /* Loses tab index in webkit if width is set to 0 */
                    opacity: 0;
                    filter: alpha(opacity=0);
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                }

                .file-upload span {
                    position: absolute;
                    top: 0;
                    left: 0;
                    display: inline-block;
                    /* Adjust button text vertical alignment */
                    padding-top: .5em;
                    background: #003a5cba;
                }
    </style>


    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">

        function pageLoad() {
            document.getElementById('<%=txtCutOffDate.ClientID %>').value = document.getElementById('<%=hdCutOffDate.ClientID %>').value;
            document.getElementById('<%=txtVendorInvoiceDate.ClientID %>').value = document.getElementById('<%=hdVendorInvoiceDate.ClientID %>').value;
        }

        function clientChangedCutOffDate(sender, args) {
            document.getElementById('<%=hdCutOffDate.ClientID %>').value = document.getElementById('<%=txtCutOffDate.ClientID %>').value;
        }

        function clientChangedVendorInvoiceDate(sender, args) {
            document.getElementById('<%=hdVendorInvoiceDate.ClientID %>').value = document.getElementById('<%=txtVendorInvoiceDate.ClientID %>').value;
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

        <%--function checkCurrency(dropdown) {
            var value = dropdown.value;

            document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>').value = '';

            var textbox = document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>');

            if (value === "-1" || value.toLowerCase() === "zzz-other") {
                textbox.disabled = false;
            } else {
                textbox.disabled = true;
            }
        }--%>

        function ValidateJobNo() {
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

        function ValidateVendorCode() {
            var vendorCode = document.getElementById('<%=txtVendorCode.ClientID %>').value;
            if (vendorCode == '') {
                document.getElementById('<%=txtVendorCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendorCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePaymentType() {
            var PaymentType = document.getElementById('<%=ddlPaymentType.ClientID %>').selectedIndex;
            if (PaymentType == '' || PaymentType == 0) {
                document.getElementById('<%=ddlPaymentType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPaymentType.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateVendorInvoiceNo() {
            var value = document.getElementById('<%=txtVendorInvoiceNo.ClientID %>').value;
            if (value == '') {
                document.getElementById('<%=txtVendorInvoiceNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendorInvoiceNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        <%--function ValidateCurrency() {
            var Currency = document.getElementById('<%=ddlCurrencyToBeReleased.ClientID %>').selectedIndex;
            if (Currency == '' || Currency == 0) {
                document.getElementById('<%=ddlCurrencyToBeReleased.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCurrencyToBeReleased.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        <%--function ValidateOtherCurrency() {

            var Currency = document.getElementById('<%=ddlCurrencyToBeReleased.ClientID %>').value;
            var textbox = document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>').value;

            if (Currency === "-1" || Currency.toLowerCase() === "zzz-other") {

                if (textbox == '') {
                    document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>').style.borderColor = "";
                    return false;
                }
            } else {
                document.getElementById('<%= txtOtherCurrencyToBeReleased.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>




        function ValidateAmountToBeReleased() {
            var value = document.getElementById('<%=txtRequestedAmountToBeReleased.ClientID %>').value;

            if (value == '' || value == 0) {
                document.getElementById('<%=txtRequestedAmountToBeReleased.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {

                var balanceAmount = document.getElementById('<%=txtBalanceAmount.ClientID %>').value;

                if (parseFloat(value) > parseFloat(balanceAmount)) {

                    document.getElementById('<%=lblAmountToBeReleasedMsg.ClientID %>').innerText = "Please enter amount less then or equal to available amount."
                    document.getElementById('<%=txtRequestedAmountToBeReleased.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=lblAmountToBeReleasedMsg.ClientID %>').innerText = ""
                    document.getElementById('<%=txtRequestedAmountToBeReleased.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }


        function ValidateVendorBankName() {
            var value = document.getElementById('<%=txtVendorBankName.ClientID %>').value;
            if (value == '') {
                document.getElementById('<%=txtVendorBankName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendorBankName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        <%--function ValidateVendorBankBranch() {
            var value = document.getElementById('<%=txtBankVendorBranch.ClientID %>').value;
            if (value == '') {
                document.getElementById('<%=txtBankVendorBranch.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBankVendorBranch.ClientID %>').style.borderColor = "";
                return false;
            }
        }--%>

        function ValidateVendorSwiftCode() {
            var value = document.getElementById('<%=txtVendorSwiftCode.ClientID %>').value;
            if (value == '') {
                document.getElementById('<%=txtVendorSwiftCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendorSwiftCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateVendorBankAccountNumber() {
            var value = document.getElementById('<%=txtBankAccountNumber.ClientID %>').value;
            if (value == '') {
                document.getElementById('<%=txtBankAccountNumber.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBankAccountNumber.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateInvoiceFileAttachment() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment1 = document.getElementById('<%=uploadFileAttachmentInvoice.ClientID %>').value;
            var divfileAttachmentInvoice = document.getElementById("divfileAttachmentInvoice");
            var lblfileAttachmentInvoice = document.getElementById('<%=lblfileAttachmentInvoice.ClientID %>');


            if (fileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachmentInvoice.ClientID %>').style.borderColor = "";
                divfileAttachmentInvoice.style.display = "none";
                lblfileAttachmentInvoice.innerHTML = "";
                return false;
            }
            else {

                fileAttachment1 = fileAttachment1.split(" ").join("")
                fileAttachment1 = fileAttachment1.split("(").join("")
                fileAttachment1 = fileAttachment1.split(")").join("")

                if (!regex.test(fileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachmentInvoice.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachmentInvoice.style.display = "block";
                    lblfileAttachmentInvoice.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachmentInvoice.ClientID %>').style.borderColor = "";
                    divfileAttachmentInvoice.style.display = "none";
                    lblfileAttachmentInvoice.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            if (ValidateVendorCode()) {
                check = false;
            }

            if (ValidatePaymentType()) {
                check = false;
            }

            if (ValidateVendorInvoiceNo()) {
                check = false;
            }


            //if (ValidateCurrency()) {
            //    check = false;
            //}

            //if (ValidateOtherCurrency()) {
            //    check = false;
            //}

            if (ValidateVendorBankName()) {
                check = false;
            }

            //if (ValidateVendorBankBranch()) {
            //    check = false;
            //}

            if (ValidateVendorSwiftCode()) {
                check = false;
            }

            if (ValidateVendorBankAccountNumber()) {
                check = false;
            }

            if (ValidateInvoiceFileAttachment()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to save Payment?")) {
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

            return check;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Add FC Vendor Payment</legend>

            <div class="form-grid form-grid-2">

                <div class="full-width">
                    <label>Job No. / Project No.:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtJOBNo" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>

                            <td style="width: 20%">
                                <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="full-width">
                    <label>Vendor Code:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtVendorCode" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>

                            <td style="width: 20%">
                                <asp:Button ID="btnGetVendor" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetVendor_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <label>Vendor Name:</label>
                <asp:TextBox ID="txtVendorName" runat="server"
                    CssClass="form-control"
                    Enabled="false" />


                <label>Vendor Address:</label>
                <asp:TextBox ID="txtVendorAddress" runat="server"
                    CssClass="form-control"
                    Rows="3" Enabled="false" />

                <label>CID PO No. to Vendor:</label>
                <asp:TextBox ID="txtPONo" runat="server"
                    CssClass="form-control"
                    Enabled="false" />


                <label>Total PO Amount:</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%">
                            <asp:TextBox ID="txtTotalPOAmount" runat="server"
                                CssClass="form-control"
                                Enabled="false" Text="0.000" />
                        </td>

                        <td style="width: 30%">
                            <asp:TextBox ID="txtPOAmountCurrency" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>
                    </tr>
                </table>

                <label>Released Amount:</label>
                <asp:TextBox ID="txtReleasedAmount" runat="server"
                    Enabled="false"
                    CssClass="form-control"
                    Text="0.000" />

                <label>Payment Release Cut-Off Date:</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCutOffDate" runat="server" onkeyDown="javascript:preventInput(event);"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdCutOffDate" runat="server" />
                            <ajax:CalendarExtender ID="calendarCutOffDate" PopupButtonID="imgBtnCutOffDate" runat="server"
                                TargetControlID="txtCutOffDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedCutOffDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnCutOffDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Calendar" />
                        </td>
                    </tr>
                </table>

                <label>Balance Amount:</label>
                <asp:TextBox ID="txtBalanceAmount" runat="server"
                    CssClass="form-control"
                    ReadOnly="true" Text="0.000" />

                <label>Requested Amount to be Released:</label>
                <asp:TextBox ID="txtRequestedAmountToBeReleased" runat="server"
                    Text="0.000"
                    CssClass="form-control"
                    onkeyup="checkDec(this);"
                    onpaste="return false"
                    onkeypress="return inNumberKeyWithDecimal(this, event);" />


                <label>Type of Payment:</label>
                <asp:DropDownList ID="ddlPaymentType"
                    runat="server"
                    CssClass="form-control" />

                <asp:Label ID="lblAmountToBeReleasedMsg" runat="server" ForeColor="Red" />

            </div>
            <br />
            <div class="form-grid form-grid-2">
                <label>Vendor Invoice No.:</label>
                <asp:TextBox ID="txtVendorInvoiceNo" runat="server"
                    CssClass="form-control" />


                <label>Vendor Invoice Date:</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtVendorInvoiceDate" runat="server" onkeyDown="javascript:preventInput(event);"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdVendorInvoiceDate" runat="server" />
                            <ajax:CalendarExtender ID="calendarVendorInvoiceDate" PopupButtonID="imgBtnVendorInvoiceDate" runat="server"
                                TargetControlID="txtVendorInvoiceDate" Format="dd-MMM-yyyy"
                                OnClientDateSelectionChanged="clientChangedVendorInvoiceDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnVendorInvoiceDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Calendar" />
                        </td>
                    </tr>
                </table>

                <label>Attachment:</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="uploadFileAttachmentInvoice" runat="server"
                                CssClass="form-control"
                                BorderStyle="Groove" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfileAttachmentInvoice" style="display: none;">
                                <asp:Label ID="lblfileAttachmentInvoice" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <label>Bank Name:</label>
                <asp:TextBox ID="txtVendorBankName" runat="server" CssClass="form-control"
                    Enabled="false" />

                <label>Bank Branch:</label>
                <asp:TextBox ID="txtBankVendorBranch" runat="server" CssClass="form-control"
                    Enabled="false" />

                <label>Swift Code:</label>
                <asp:TextBox ID="txtVendorSwiftCode" runat="server" CssClass="form-control"
                    Enabled="false" />

                <label>Account Number:</label>
                <asp:TextBox ID="txtBankAccountNumber" runat="server" CssClass="form-control"
                    Enabled="false" />


                <div class="full-width">
                    <label>Remarks:</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />

            <asp:Button ID="btnViewPaymentList" runat="server" Width="100%" Text="View List" CssClass="button"
                OnClick="btnViewPaymentList_Click" />

        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>
    </div>



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

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.:</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control"/>

                        <label>Customer Code:</label>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblCustCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB No." />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Custmer Code" />
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



    <%--VENDOR DETAIL START--%>
    <asp:Button ID="btnShowPopupVendorDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeVendorDetail" runat="server" TargetControlID="btnShowPopupVendorDetail"
        PopupControlID="pnlPopupVendorDetail" CancelControlID="imgBtnCancelVendorDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupVendorDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelVendorDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblVendorRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>PO No.:</label>
                        <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />

                        <label>Vendor Code:</label>
                        <asp:TextBox ID="txtVendorCodeSearch" runat="server" CssClass="form-control" />

                        <label>Vendor Name:</label>
                        <asp:TextBox ID="txtVendorNameSearch" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnSearchVendorDetails" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchVendorDetails_Click" />

                </div>
            </div>

            <div class="employee-grid-container">
                <asp:Label ID="lblVendorMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvVendorDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvVendorDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />

                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                                <asp:Label ID="lblVendorAddress" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />

                                <asp:Label ID="lblVendorBankName" runat="server" Visible="false" Text='<%# Eval("BANK_NAME") %>' />
                                <asp:Label ID="lblVendorBankBranch" runat="server" Visible="false" Text='<%# Eval("BANK_BRANCH") %>' />
                                <asp:Label ID="lblVendorBankIFSCSWIFT" runat="server" Visible="false" Text='<%# Eval("BANK_IFSC_SWIFT") %>' />
                                <asp:Label ID="lblVendorBankAccountNo" runat="server" Visible="false" Text='<%# Eval("BANK_ACCOUNT_NO") %>' />

                                <asp:Label ID="lblTotalPOAmount" runat="server" Visible="false" Text='<%# Eval("TOTAL_PO_AMOUNT") %>' />
                                <asp:Label ID="lblPOAmountCurrency" runat="server" Visible="false" Text='<%# Eval("PO_AMOUNT_CURRENCY") %>' />

                                <asp:Label ID="lblReleasedAmount" runat="server" Visible="false" Text='<%# Eval("RELEASED_AMOUNT") %>' />


                                <asp:Button ID="btnGetVendorDetails" CommandArgument="GET" ToolTip="Get Vendor Details" Width="100%"
                                    runat="server" Text="Get Vendor" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="PO_NO" HeaderText="PO No." />
                        <asp:BoundField DataField="VENDOR_CODE" HeaderText="Vendor Code" />
                        <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                        <asp:BoundField DataField="ADDRESS" HeaderText="Address" />

                        <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" />
                        <asp:BoundField DataField="BANK_BRANCH" HeaderText="Bank Branch" />
                        <asp:BoundField DataField="BANK_IFSC_SWIFT" HeaderText="SWIFT Code" />
                        <asp:BoundField DataField="BANK_ACCOUNT_NO" HeaderText="Account No." />

                        <asp:BoundField DataField="TOTAL_PO_AMOUNT" HeaderText="Total PO Amount" />
                        <asp:BoundField DataField="PO_AMOUNT_CURRENCY" HeaderText="PO Amount Currency" />
                        <asp:BoundField DataField="RELEASED_AMOUNT" HeaderText="Released Amount" />

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
    <%--VENDOR DETAIL END--%>
</asp:Content>
