<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportedCostCenterList.aspx.cs"
    Inherits="REPORTS_SALE_ORDER_GROSS_MARGIN_ImportedCostCenterList" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            display: inline-block;
            border-radius: 4px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
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


    <script type="text/javascript" language="javascript">

        function ValidateOrderNoNew() {
            var OrderNoNew = document.getElementById('<%=txtOrderNoNew.ClientID %>').value;
            if (OrderNoNew == '') {
                document.getElementById('<%=txtOrderNoNew.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOrderNoNew.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDirectBilling() {
            var DirectBilling = document.getElementById('<%=txtDirectBilling.ClientID %>').value;
            if (DirectBilling == '') {
                document.getElementById('<%=txtDirectBilling.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDirectBilling.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePartialBilling() {
            var PartialBilling = document.getElementById('<%=txtPartialBilling.ClientID %>').value;
            if (PartialBilling == '') {
                document.getElementById('<%=txtPartialBilling.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPartialBilling.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateNetAmount() {
            var NetAmount = document.getElementById('<%=txtNetAmount.ClientID %>').value;
            if (NetAmount == '') {
                document.getElementById('<%=txtNetAmount.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNetAmount.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllNew() {

            if (ValidateOrderNoNew()) {
                return false;
            }

            if (ValidateDirectBilling()) {
                return false;
            }

            if (ValidatePartialBilling()) {
                return false;
            }

            if (ValidateNetAmount()) {
                return false;
            }

            return true;
        }

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>


    <script type="text/Javascript">
        function checkDec1(el) {

            var costCenterAmount;
            var directBilling;
            var partialBilling;

            if (document.getElementById('<%=txtCostCenterAmount.ClientID %>').value != '')
                costCenterAmount = parseFloat(document.getElementById('<%=txtCostCenterAmount.ClientID %>').value);
            else
                costCenterAmount = 0;

            if (document.getElementById('<%=txtDirectBilling.ClientID %>').value != '')
                directBilling = parseFloat(document.getElementById('<%=txtDirectBilling.ClientID %>').value);
            else
                directBilling = 0;

            if (document.getElementById('<%=txtPartialBilling.ClientID %>').value != '' && document.getElementById('<%=txtPartialBilling.ClientID %>').value != '-')
                partialBilling = parseFloat(document.getElementById('<%=txtPartialBilling.ClientID %>').value);
            else
                partialBilling = 0;


            var netBilling = costCenterAmount + directBilling - partialBilling
            document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(netBilling);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Imported Cost Center Report</legend>
            <table width="90%">
                <tr>
                    <td>Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPostingMonth" runat="server" onkeyDown="javascript:preventInput(event);" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                    <ajax:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                        PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                        BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                    </ajax:CalendarExtender>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Posting Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td>&nbsp;
                    </td>
                    <td>Order No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Cost Center Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCenterCode" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Cost Center Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCenterName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>

                    <td>Company:</td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                        OnClick="btnSearch_Click" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                                        OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
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
        <fieldset style="width: 80%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <%--<div id="gridContainer" style='overflow-y: scroll; overflow-y: hidden; width: 100%; height: 100%; border: 1px solid lightgray;'>--%>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvCostCenterList" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%"
                    ForeColor="#333333" GridLines="Both" PageSize="20" HorizontalAlign="Center" OnRowDataBound="gvCostCenterList_RowDataBound"
                    OnRowCommand="gvCostCenterList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:Label ID="lblCostCenterID" runat="server" Visible="false" Text='<%# Eval("COST_CENTER_ID" ) %>' />
                                <asp:Label ID="lblCostCenterCode" runat="server" Visible="false" Text='<%# Eval("COST_CENTER_CODE" ) %>' />
                                <asp:Label ID="lblCostCenterName" runat="server" Visible="false" Text='<%# Eval("COST_CENTER_NAME" ) %>' />
                                <asp:Label ID="lblOrderNo" runat="server" Visible="false" Text='<%# Eval("ORDER_NO" ) %>' />
                                <asp:Label ID="lblCostCenterAmount" runat="server" Visible="false" Text='<%# Eval("COST_CENTER_AMOUNT" ) %>' />
                                <asp:Label ID="lblDirectBilling" runat="server" Visible="false" Text='<%# Eval("DIRECT_BILLING" ) %>' />
                                <asp:Label ID="lblPartialBilling" runat="server" Visible="false" Text='<%# Eval("PARTIAL_BILLING" ) %>' />
                                <asp:Label ID="lblNetAmount" runat="server" Visible="false" Text='<%# Eval("NET_AMOUNT" ) %>' />
                                <asp:Label ID="lblMonth" runat="server" Visible="false" Text='<%# Eval("MONTH" ) %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID" ) %>' />
                                <asp:Label ID="lblUnit" runat="server" Visible="false" Text='<%# Eval("UNIT" ) %>' />

                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                    ToolTip="Edit Posted Bill" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST_CENTER_CODE" />
                        <asp:BoundField DataField="COST_CENTER_NAME" HeaderText="COST_CENTER_NAME" />
                        <asp:BoundField DataField="ORDER_NO" HeaderText="ORDER_NO" />
                        <asp:BoundField DataField="COST_CENTER_AMOUNT" HeaderText="COST_CENTER_AMOUNT" />
                        <asp:BoundField DataField="DIRECT_BILLING" HeaderText="DIRECT_BILLING" />
                        <asp:BoundField DataField="PARTIAL_BILLING" HeaderText="PARTIAL_BILLING" />
                        <asp:BoundField DataField="NET_AMOUNT" HeaderText="NET_AMOUNT" />
                        <asp:BoundField DataField="MONTH" HeaderText="MONTH" />
                        <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" CssClass="FrozenHeader" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="400px" Width="900px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 0px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" />
            </legend>
            <div style='overflow: auto; width: 99%; height: 300px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 10px;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Cost Center Name:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtCostCenterNameNew" runat="server" Width="100%" Enabled="false"
                                CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Order No:</td>
                        <td>
                            <asp:TextBox ID="txtOrderNoNew" runat="server" Width="100%" Enabled="false"
                                CssClass="textbox" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Cost Center Amount:</td>
                        <td>
                            <asp:TextBox ID="txtCostCenterAmount" runat="server" Width="100%" CssClass="textbox"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this)" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Direct Billing:</td>
                        <td>
                            <asp:TextBox ID="txtDirectBilling" runat="server" Width="100%" CssClass="textbox"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this)" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Partial Billing:</td>
                        <td>
                            <asp:TextBox ID="txtPartialBilling" runat="server" Width="100%" CssClass="textbox"
                                onkeypress="return negNumberKeyWithDecimal(this, event);" onKeyUp="checkDec1(this)" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Net Amount:</td>
                        <td>
                            <asp:TextBox ID="txtNetAmount" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Month:</td>
                        <td>
                            <asp:TextBox ID="txtMonth" runat="server" Width="100%" Enabled="false" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update" OnClientClick="return ValidateAllNew();"
                                Width="100%" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
