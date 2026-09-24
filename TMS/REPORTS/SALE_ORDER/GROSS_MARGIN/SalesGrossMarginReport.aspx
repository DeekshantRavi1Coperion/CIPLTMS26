<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="SalesGrossMarginReport.aspx.cs"
    Inherits="REPORTS_SALE_ORDER_GROSS_MARGIN_SalesGrossMarginReport" Title="Untitled Page" %>

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

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            display: inline-block;
            border-radius: 4px;
            background-color: lightgray;
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

    <script type="text/Javascript">       
        function checkDec1(el) {

            var amountINR = 0;
            var pojectCosts = 0;
            var actualGrossMargin = 0;
            var standard = 0;
            var actual = 0;
            var highDelta = 0;

            if (parseFloat(document.getElementById('<%=hdActualGrossMargin.ClientID %>').value) > 0)
                actualGrossMargin = parseFloat(document.getElementById('<%=hdActualGrossMargin.ClientID %>').value).toFixed(2);
            else
                actualGrossMargin = 0;

            if (parseFloat(document.getElementById('<%=txtAmountINR.ClientID %>').value) > 0)
                amountINR = parseFloat(document.getElementById('<%=txtAmountINR.ClientID %>').value).toFixed(2);
            else
                amountINR = 0;

            if (document.getElementById('<%=txtProjectCosts.ClientID %>').value != '')
                pojectCosts = parseFloat(document.getElementById('<%=txtProjectCosts.ClientID %>').value).toFixed(2);
            else {
                pojectCosts = 0;
                document.getElementById('<%=txtProjectCosts.ClientID %>').value = parseFloat(0).toFixed(2);
            }

            if (document.getElementById('<%=txtStandard.ClientID %>').value != '')
                standard = parseFloat(document.getElementById('<%=txtStandard.ClientID %>').value).toFixed(2);
            else
                standard = 0;

            if ((parseFloat(actualGrossMargin) + parseFloat(pojectCosts)) > 0)
                document.getElementById('<%=txtActualGrossMargin.ClientID %>').value = parseFloat((parseFloat(actualGrossMargin) + parseFloat(pojectCosts))).toFixed(2);
            else
                document.getElementById('<%=txtActualGrossMargin.ClientID %>').value = 0;

            if (parseFloat(document.getElementById('<%=hdActual.ClientID %>').value) > 0)
                actual = parseFloat(document.getElementById('<%=hdActual.ClientID %>').value).toFixed(2);
            else
                actual = 0;


            if (pojectCosts > 0) {
                if (amountINR > 0)
                    document.getElementById('<%=txtActual.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtActualGrossMargin.ClientID %>').value) / amountINR).toFixed(2);
                else
                    document.getElementById('<%=txtActual.ClientID %>').value = 0;
            }
            else
                document.getElementById('<%=txtActual.ClientID %>').value = parseFloat(actual).toFixed(2);


            if (pojectCosts > 0)
                highDelta = parseFloat(document.getElementById('<%=hdHighDelta.ClientID %>').value).toFixed(2);
            else
                highDelta = 0;


            if (parseFloat(document.getElementById('<%=txtActualGrossMargin.ClientID %>').value) > 0)
                document.getElementById('<%=txtHighDelta.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtActual.ClientID %>').value) - standard).toFixed(2);
            else
                document.getElementById('<%=txtHighDelta.ClientID %>').value = parseFloat(highDelta).toFixed(2);

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
            <legend style="text-align: center;">Monthly Sales Gross Margin Report</legend>
            <table width="100%">
                <tr>
                    <td>Month: </td>
                    <td align="left">
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

                    <td>&nbsp;</td>

                    <td align="right">Company:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="220px" Height="26px" />
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">Customer Name:</td>
                    <td colspan="8">
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="100%"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right">OA No.:</td>
                    <td>
                        <asp:TextBox ID="txtOANo" runat="server" Width="100%"></asp:TextBox>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">Bill No.:</td>
                    <td>
                        <asp:TextBox ID="txtBillNo" runat="server" Width="100%"></asp:TextBox>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">Type:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCompanyType" runat="server" Width="150px" Height="26px">
                            <asp:ListItem Text="ALL" Value="0" />
                            <asp:ListItem Text="IC" Value="1" />
                            <asp:ListItem Text="TP" Value="2" />
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">POC/NPOC:</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPOCNPOC" runat="server" Width="150px" Height="26px">
                            <asp:ListItem Text="ALL" Value="0" />
                            <asp:ListItem Text="POC" Value="1" />
                            <asp:ListItem Text="Non-POC" Value="2" />
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />
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
            <%--<div id="gridContainer" style='overflow-y: scroll; overflow-y: hidden; width: 100%; height: 100%; border: 1px solid lightgray;'>--%>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvSaleMargin" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvSaleMargin_RowDataBound" OnRowCommand="gvSaleMargin_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                <asp:Label ID="lblOANo" runat="server" Visible="false" Text='<%# Eval("OA_NO") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblWarrantyPer" runat="server" Visible="false" Text='<%# Eval("WARRANTY_PER") %>' />
                                <asp:Label ID="lblMO" runat="server" Visible="false" Text='<%# Eval("MO") %>' />
                                <asp:Label ID="lblBillDate" runat="server" Visible="false" Text='<%# Eval("BILL_DATE") %>' />
                                <asp:Label ID="lblClass" runat="server" Visible="false" Text='<%# Eval("CLASS") %>' />
                                <asp:Label ID="lblCurrDesc" runat="server" Visible="false" Text='<%# Eval("CURR_DESC") %>' />
                                <asp:Label ID="lblRate" runat="server" Visible="false" Text='<%# Eval("RATE") %>' />
                                <asp:Label ID="lblFCurrency" runat="server" Visible="false" Text='<%# Eval("F_CURRENCY") %>' />
                                <asp:Label ID="lblAmountINR" runat="server" Visible="false" Text='<%# Eval("AMOUNT_INR") %>' />
                                <asp:Label ID="lblBillNo" runat="server" Visible="false" Text='<%# Eval("BILL_NO") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:Label ID="lblBusSegment" runat="server" Visible="false" Text='<%# Eval("BUS_SEGMENT") %>' />
                                <asp:Label ID="lblGrossMarginAsPerOrder" runat="server" Visible="false" Text='<%# Eval("GROSS_MARGIN_AS_PER_ORDER") %>' />
                                <asp:Label ID="lblPercentage" runat="server" Visible="false" Text='<%# Eval("PERCENTAGE") %>' />
                                <asp:Label ID="lblCostAsPerOrder" runat="server" Visible="false" Text='<%# Eval("COST_AS_PER_ORDER") %>' />
                                <asp:Label ID="lblMarginAsPerOrder" runat="server" Visible="false" Text='<%# Eval("MARGIN_AS_PER_ORDER") %>' />
                                <asp:Label ID="lblCostAsPerCostCenter" runat="server" Visible="false" Text='<%# Eval("COST_AS_PER_COST_CENTER") %>' />                              
                                <asp:Label ID="lblProjectCosts" runat="server" Visible="false" Text='<%# Eval("PROJECT_COSTS") %>' />
                                <asp:Label ID="lblWarranty" runat="server" Visible="false" Text='<%# Eval("WARRANTY") %>' />
                                <asp:Label ID="lblActualGrossMargin" runat="server" Visible="false" Text='<%# Eval("ACTUAL_GROSS_MARGIN") %>' />
                                <asp:Label ID="lblStandard" runat="server" Visible="false" Text='<%# Eval("STANDARD") %>' />
                                <asp:Label ID="lblActual" runat="server" Visible="false" Text='<%# Eval("ACTUAL") %>' />
                                <asp:Label ID="lblHighDelta" runat="server" Visible="false" Text='<%# Eval("HIGH_DELTA") %>' />
                                <asp:Label ID="lblType" runat="server" Visible="false" Text='<%# Eval("TYPE") %>' />
                                <asp:Label ID="lblMonth" runat="server" Visible="false" Text='<%# Eval("MONTH") %>' />
                                <asp:Label ID="lblPOCNONPOC" runat="server" Visible="false" Text='<%# Eval("POC_NONPOC") %>' />

                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                    ToolTip="Edit Posted Sales Gross Margin" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OA_NO" HeaderText="OA_NO" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="WARRANTY_PER" HeaderText="WARRANTY_PER" />
                        <asp:BoundField DataField="MO" HeaderText="MO" />
                        <asp:BoundField DataField="BILL_DATE" HeaderText="BILL_DATE" />
                        <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
                        <asp:BoundField DataField="CURR_DESC" HeaderText="CURR_DESC" />
                        <asp:BoundField DataField="RATE" HeaderText="RATE" />
                        <asp:BoundField DataField="F_CURRENCY" HeaderText="F_CURRENCY" />
                        <asp:BoundField DataField="AMOUNT_INR" HeaderText="AMOUNT_INR" />
                        <asp:BoundField DataField="BILL_NO" HeaderText="BILL_NO" />
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="GROSS_MARGIN_AS_PER_ORDER" HeaderText="GROSS_MARGIN_AS_PER_ORDER" />
                        <asp:BoundField DataField="PERCENTAGE" HeaderText="PERCENTAGE" />
                        <asp:BoundField DataField="COST_AS_PER_ORDER" HeaderText="COST_AS_PER_ORDER" />
                        <asp:BoundField DataField="MARGIN_AS_PER_ORDER" HeaderText="MARGIN_AS_PER_ORDER" />
                        <asp:BoundField DataField="COST_AS_PER_COST_CENTER" HeaderText="COST_AS_PER_COST_CENTER" />                       
                        <asp:BoundField DataField="PROJECT_COSTS" HeaderText="PROJECT_COSTS" />
                        <asp:BoundField DataField="WARRANTY" HeaderText="WARRANTY" />
                        <asp:BoundField DataField="ACTUAL_GROSS_MARGIN" HeaderText="ACTUAL_GROSS_MARGIN" />
                        <asp:BoundField DataField="STANDARD" HeaderText="STANDARD" />
                        <asp:BoundField DataField="ACTUAL" HeaderText="ACTUAL" />
                        <asp:BoundField DataField="HIGH_DELTA" HeaderText="HIGH_DELTA" />
                        <asp:BoundField DataField="TYPE" HeaderText="TYPE" />
                        <asp:BoundField DataField="MONTH" HeaderText="MONTH" />
                        <asp:BoundField DataField="POC_NONPOC" HeaderText="POC_NONPOC" />
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
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="650px" Width="950px"
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
            <div style='overflow: auto; width: 99%; height: 550px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 10px;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Customer Name:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtCustomerNameNew" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                CssClass="textbox1" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Warranty(%):</td>
                        <td>
                            <asp:TextBox ID="txtWarrantyPer" runat="server" Width="100%" onkeyDown="javascript:preventInput(event);"
                                CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>MO:</td>
                        <td>
                            <asp:TextBox ID="txtMO" runat="server" Width="100%" CssClass="textbox1"
                                onkeyDown="javascript:preventInput(event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Bill Date:</td>
                        <td>
                            <asp:TextBox ID="txtBillDate" runat="server" Width="100%" CssClass="textbox1"
                                onkeyDown="javascript:preventInput(event);" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Class:</td>
                        <td>
                            <asp:TextBox ID="txtClass" runat="server" Width="100%" CssClass="textbox1"
                                onkeyDown="javascript:preventInput(event);" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Curr Desc:</td>
                        <td>
                            <asp:TextBox ID="txtCurrDesc" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Rate:</td>
                        <td>
                            <asp:TextBox ID="txtRate" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>F_Currency:</td>
                        <td>
                            <asp:TextBox ID="txtFCurrency" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Amount INR:</td>
                        <td>
                            <asp:TextBox ID="txtAmountINR" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Bill No:</td>
                        <td>
                            <asp:TextBox ID="txtBillNoNew" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Location:</td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Bus Segment:</td>
                        <td>
                            <asp:TextBox ID="txtBusSegment" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Gross Margin As Per Order:</td>
                        <td>
                            <asp:TextBox ID="txtGrossMarginAsPerOrder" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Percentage:</td>
                        <td>
                            <asp:TextBox ID="txtPercentage" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Cost As Per Order:</td>
                        <td>
                            <asp:TextBox ID="txtCostAsPerOrder" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Margin As Per Order:</td>
                        <td>
                            <asp:TextBox ID="txtMarginAsPerOrder" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Cost As Per Cost Center:</td>
                        <td>
                            <asp:TextBox ID="txtCostAsPerCostCenter" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Project Costs:</td>
                        <td>
                            <asp:TextBox ID="txtProjectCosts" runat="server" Width="100%" onKeyUp="checkDec1(this)"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" CssClass="textbox" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Warranty:</td>
                        <td>
                            <asp:TextBox ID="txtWarranty" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>Actual Gross Margin:</td>
                        <td>
                            <asp:HiddenField ID="hdActualGrossMargin" runat="server" />
                            <asp:TextBox ID="txtActualGrossMargin" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Standard:</td>
                        <td>
                            <asp:TextBox ID="txtStandard" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>Actual:</td>
                        <td>
                            <asp:HiddenField ID="hdActual" runat="server" />
                            <asp:TextBox ID="txtActual" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>High Delta:</td>
                        <td>
                            <asp:HiddenField ID="hdHighDelta" runat="server" />
                            <asp:TextBox ID="txtHighDelta" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>Type:</td>
                        <td>
                            <asp:TextBox ID="txtType" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Month:</td>
                        <td>
                            <asp:TextBox ID="txtMonth" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>POC/NONPOC:</td>
                        <td>
                            <asp:TextBox ID="txtPOCNONPOC" runat="server" Width="100%"
                                onkeyDown="javascript:preventInput(event);" CssClass="textbox1" />
                        </td>
                        <td colspan="2">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
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
