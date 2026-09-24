<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="POCPostingThree.aspx.cs" Inherits="REPORTS_SALE_ORDER_POCPostingThree" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        function ValidatePostingMonth() {
            var PostingMonth = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
            if (PostingMonth == '') {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidatePostingMonth()) { return false; }
            return true;
        }
    </script>

    <script type="text/javascript">
        function inNumberKey(txt, evt) {
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
                if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
    </script>

    <script type="text/Javascript">

        function checkDec1(el) {
            var ex = /^-?[0-9]+(.[0-9]{1,20})?$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {

                    var row = el.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;

                    var lblOrderAmount = row.cells[5].innerText;
                    var lblPreviousBilling = row.cells[6].innerText;
                    var txtPOCBilling = row.cells[7].getElementsByTagName("input")[0].value;
                    var lblNetBilling = row.cells[10].getElementsByTagName("input")[0].value;

                    var OrderAmount = 0;
                    var PreviousBilling = 0;
                    var POCBilling = 0;
                    var NetBilling = 0;


                    if (lblOrderAmount != '' && parseFloat(lblOrderAmount) > '0') {
                        OrderAmount = parseFloat(lblOrderAmount);
                    }
                    else {
                        OrderAmount = 0;
                    }

                    if (lblPreviousBilling != '' && parseFloat(lblPreviousBilling) > '0') {
                        PreviousBilling = parseFloat(lblPreviousBilling);
                    }
                    else {
                        PreviousBilling = 0;
                    }

                    if (txtPOCBilling != '' && parseFloat(txtPOCBilling) > '0') {
                        POCBilling = parseFloat(txtPOCBilling);
                    }
                    else {
                        POCBilling = 0;
                    }


                    if (PreviousBilling > 0) {
                        if ((OrderAmount - PreviousBilling) < (POCBilling)) {
                            row.cells[7].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - PreviousBilling);
                        }
                    }
                    else {
                        if (OrderAmount < POCBilling) {
                            row.cells[7].getElementsByTagName("input")[0].value = parseFloat(OrderAmount);
                        }
                    }

                    if (PreviousBilling > 0) {
                        row.cells[10].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - PreviousBilling - parseFloat(row.cells[7].getElementsByTagName("input")[0].value));
                    }
                    else {
                        row.cells[10].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - parseFloat(row.cells[7].getElementsByTagName("input")[0].value));
                    }






                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblOrderAmount = row.cells[5].innerText;
                var lblPreviousBilling = row.cells[6].innerText;
                var txtPOCBilling = row.cells[7].getElementsByTagName("input")[0].value;
                var lblNetBilling = row.cells[10].getElementsByTagName("input")[0].value;

                var OrderAmount = 0;
                var PreviousBilling = 0;
                var POCBilling = 0;
                var NetBilling = 0;


                if (lblOrderAmount != '' && parseFloat(lblOrderAmount) > '0') {
                    OrderAmount = parseFloat(lblOrderAmount);
                }
                else {
                    OrderAmount = 0;
                }

                if (lblPreviousBilling != '' && parseFloat(lblPreviousBilling) > '0') {
                    PreviousBilling = parseFloat(lblPreviousBilling);
                }
                else {
                    PreviousBilling = 0;
                }

                if (txtPOCBilling != '' && parseFloat(txtPOCBilling) > '0') {
                    POCBilling = parseFloat(txtPOCBilling);
                }
                else {
                    POCBilling = 0;
                }

                if (PreviousBilling > 0) {
                    if ((OrderAmount - PreviousBilling) < (POCBilling)) {
                        row.cells[7].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - PreviousBilling);
                    }
                }
                else {
                    if (OrderAmount < POCBilling) {
                        row.cells[7].getElementsByTagName("input")[0].value = parseFloat(OrderAmount);
                    }
                }

                if (PreviousBilling > 0) {
                    row.cells[10].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - PreviousBilling - parseFloat(row.cells[7].getElementsByTagName("input")[0].value));
                }
                else {
                    row.cells[10].getElementsByTagName("input")[0].value = parseFloat(OrderAmount - parseFloat(row.cells[7].getElementsByTagName("input")[0].value));
                }





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

    <script type="text/javascript" language="javascript">
        function onCalendarShown() {
            var cal = $find("calendarPostingMonth");
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
            var cal = $find("calendarPostingMonth");
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
                    var cal = $find("calendarPostingMonth");
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
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 80%">
                    <legend style="text-align: center;">POC Billing [Monthly]</legend>
                    <table width="100%">
                        <tr>
                            <td>Posting Month:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPostingMonth" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                            <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                            <asp:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                                PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                                BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                            </asp:CalendarExtender>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                                FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Posting Month Calendar" Width="20px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">JOB No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save"
                                    OnClick="btnSave_Click" />
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
                <fieldset style="width: 90%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                    <div style='overflow: auto; width: 100%; height: 430px; border: 1px solid lightgray;'>
                        <asp:GridView ID="gvSaleOrderPosting" runat="server" CellPadding="4" ForeColor="#333333"
                            Width="100%" HorizontalAlign="Center" OnRowDataBound="gvSaleOrderPosting_RowDataBound"
                            OnRowCommand="gvSaleOrderPosting_RowCommand" AutoGenerateColumns="False">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField HeaderText="Job_No" DataField="Job_No" />
                                <asp:BoundField HeaderText="Order_Date" DataField="Order_Date" />
                                <asp:BoundField HeaderText="Customer_Code" DataField="Customer_Code" />
                                <asp:BoundField HeaderText="Customer_Name" DataField="Customer_Name" />
                                <asp:BoundField HeaderText="BU" DataField="BU" />
                                <asp:TemplateField HeaderText="Order_Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("Serial_No") %>' Visible="false" />
                                        <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("Job_No") %>' Visible="false" />
                                        <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("Order_Date") %>' Visible="false" />
                                        <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Eval("Customer_Code") %>'
                                            Visible="false" />
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("Customer_Name") %>'
                                            Visible="false" />
                                        <asp:Label ID="lblBU" runat="server" Text='<%# Eval("BU") %>' Visible="false" />
                                        <asp:Label ID="lblOrderAmount" runat="server" Text='<%# Eval("Order_Amount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous_Billing">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreviousBilling" runat="server" Text='<%# Eval("Previous_Billing") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="POC_Billing">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPOCBilling" Width="100%" runat="server" Text='<%# Eval("POC_Billing") %>'
                                            onkeypress="return inNumberKey(this, event);" onKeyUp="checkDec1(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TAX_Billing">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTAXBilling" Width="100%" runat="server" Text='<%# Eval("TAX_Billing") %>'
                                            onkeypress="return inNumberKey(this, event);" onkeyDown="javascript:preventInput(event);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:Button ID="btnPost" CssClass="button" Width="100%" Height="30px" runat="server"
                                            Text="Add Tax Bill" CommandArgument="ADD_TAX_BILL" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net_POC_Backlog">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNetPOCBacklog" Width="100%" runat="server" Text='<%# Eval("Net_POC_Backlog") %>'
                                            onkeyDown="javascript:preventInput(event);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="POST">
                            <ItemTemplate>
                                <asp:Button ID="btnPost" CommandArgument="POST" runat="server" Text="Post" CssClass="cancelbutton" />
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
                    <%--<input type="hidden" id="div_position" name="div_position" />--%>
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
                <div align="center" style="margin-top: 20px;">
                    <fieldset style="width: 95%">
                        <legend style="text-align: center;">Invoice List</legend>
                        <table width="100%">
                            <tr style="height: 50px;">
                                <td align="right">Month:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMonth" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td align="right">JOB No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJOBNoNew" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td align="right">Total Amount:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="100%" Enabled="false" Text="0"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnAddTaxBill" CssClass="button" Width="100%" runat="server" Text="Add"
                                        OnClick="btnAddTaxBill_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <br />
                <div align="center">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblInvoiceRecoreds" runat="server" Text="Records[0]" />
                            <asp:Label ID="lblSelectedRecords" runat="server" Text="[0]" />
                            <asp:Label ID="lblRowIndex" runat="server" Visible="false" /></legend>
                        <div style='overflow: auto; width: 100%; height: 370px; border: 1px solid lightgray;'>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                        AutoPostBack="true" />
                                    <asp:GridView ID="gvInvoiceList" runat="server" CellPadding="4" ForeColor="#333333"
                                        PageSize="20" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False">
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("Job_No") %>' Visible="false" />
                                                    <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("Invoice_No") %>' Visible="false" />
                                                    <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("Invoice_Date") %>'
                                                        Visible="false" />
                                                    <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Job_No" DataField="Job_No" />
                                            <asp:BoundField HeaderText="Invoice_No" DataField="Invoice_No" />
                                            <asp:BoundField HeaderText="Invoice_Date" DataField="Invoice_Date" />
                                            <asp:TemplateField HeaderText="Invoice_Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# Eval("Invoice_Amount") %>' />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
