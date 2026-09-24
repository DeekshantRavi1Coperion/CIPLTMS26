<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="SalesGrossMarginReportOne.aspx.cs"
    Inherits="REPORTS_SALE_ORDER_GROSS_MARGIN_SalesGrossMarginReportOne" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

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

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Monthly Sales Gross Margin Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">


                    <label>Month: </label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
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

                    <label>Company:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />

                    <label>Customer Name:</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>OA No.:</label>
                    <asp:TextBox ID="txtOANo" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Type:</label>
                    <asp:DropDownList ID="ddlCompanyType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="ALL" Value="0" />
                        <asp:ListItem Text="IC" Value="1" />
                        <asp:ListItem Text="TP" Value="2" />
                    </asp:DropDownList>

                    <label>POC/NPOC:</label>
                    <asp:DropDownList ID="ddlPOCNPOC" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="ALL" Value="0" />
                        <asp:ListItem Text="POC" Value="1" />
                        <asp:ListItem Text="Non-POC" Value="2" />
                    </asp:DropDownList>

                </div>

                <div class="full-width button-group">
                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClick="btnSearch_Click" />

                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />
                </div>

                <div class="full-width button-group">

                    <label>Total Amount INR:</label>
                    <asp:Label ID="lblTotalAmountINR" runat="server" Text="0" Font-Bold="true" Font-Size="Large" ForeColor="Green" />

                    <label>Total Cost As Per Order:</label>
                    <asp:Label ID="lblTotalCostAsPerOrder" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />

                    <label>Total Margin As Per Order</label>
                    <asp:Label ID="lblTotalMarginAsPerOrder" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />

                    <label>Total Cost As Per Cost Center</label>
                    <asp:Label ID="lblTotalCostAsPerCostCenter" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />

                </div>

                <div class="full-width button-group">
                    <label>Total Material Overheads</label>
                    <asp:Label ID="lblTotalMaterialOverheads" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />

                    <label>Total Warranty</label>
                    <asp:Label ID="lblTotalWarranty" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />

                    <label>Total Actual Grosss Margin</label>
                    <asp:Label ID="lblTotalActualGrossMargin" runat="server" Text="0" Font-Bold="true" ForeColor="Green" />
                </div>

            </fieldset>
        </div>



        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView
                CssClass="employee-grid"
                ID="gvSaleMargin" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvSaleMargin_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="OA_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblOANo" runat="server" Visible="true" Text='<%# Eval("OA_NO") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblAmountINR" runat="server" Visible="false" Text='<%# Eval("AMOUNT_INR") %>' />
                            <asp:Label ID="lblCostAsPerOrder" runat="server" Visible="false" Text='<%# Eval("COST_AS_PER_ORDER") %>' />
                            <asp:Label ID="lblMarginAsPerOrder" runat="server" Visible="false" Text='<%# Eval("MARGIN_AS_PER_ORDER") %>' />
                            <asp:Label ID="lblCostAsPerCostCenter" runat="server" Visible="false" Text='<%# Eval("COST_AS_PER_COST_CENTER") %>' />
                            <asp:Label ID="lblMaterialOverheads" runat="server" Visible="false" Text='<%# Eval("MATERIAL_OVERHEADS") %>' />
                            <asp:Label ID="lblWarranty" runat="server" Visible="false" Text='<%# Eval("WARRANTY") %>' />
                            <asp:Label ID="lblActualGrossMargin" runat="server" Visible="false" Text='<%# Eval("ACTUAL_GROSS_MARGIN") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                    <asp:BoundField DataField="WARRANTY_PER" HeaderText="WARRANTY_PER" />
                    <asp:BoundField DataField="MO" HeaderText="MO" />
                    <%--<asp:BoundField DataField="BILL_DATE" HeaderText="BILL_DATE" />--%>
                    <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
                    <asp:BoundField DataField="CURR_DESC" HeaderText="CURR_DESC" />
                    <asp:BoundField DataField="RATE" HeaderText="RATE" />
                    <asp:BoundField DataField="F_CURRENCY" HeaderText="F_CURRENCY" />
                    <asp:BoundField DataField="AMOUNT_INR" HeaderText="AMOUNT_INR" />
                    <%--<asp:BoundField DataField="BILL_NO" HeaderText="BILL_NO" />--%>
                    <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                    <%--<asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />--%>
                    <asp:BoundField DataField="BUS_SEGMENT" HeaderText="OLD_BU" />
                    <asp:BoundField DataField="NEW_BU" HeaderText="NEW_BU" />
                    <asp:BoundField DataField="GROSS_MARGIN_AS_PER_ORDER" HeaderText="GROSS_MARGIN_AS_PER_ORDER" />
                    <asp:BoundField DataField="PERCENTAGE" HeaderText="PERCENTAGE" />
                    <asp:BoundField DataField="COST_AS_PER_ORDER" HeaderText="COST_AS_PER_ORDER" />
                    <asp:BoundField DataField="MARGIN_AS_PER_ORDER" HeaderText="MARGIN_AS_PER_ORDER" />
                    <asp:BoundField DataField="COST_AS_PER_COST_CENTER" HeaderText="COST_AS_PER_COST_CENTER" />
                    <asp:BoundField DataField="MATERIAL_OVERHEADS" HeaderText="MATERIAL_OVERHEADS" />
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

    </div>

    <div align="center" style="margin-top: 20px;">
    </div>

    <br />





    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegend" runat="server" /></legend>

                <div class="form-grid form-grid-2">

                    <label>Customer Name:</label>
                    <asp:TextBox ID="txtCustomerNameNew" runat="server"
                        onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control" />

                    <label>Warranty(%):</label>
                    <asp:TextBox ID="txtWarrantyPer" runat="server" onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control" />

                    <label>MO:</label>
                    <asp:TextBox ID="txtMO" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>Bill Date:</label>
                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>Class:</label>
                    <asp:TextBox ID="txtClass" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>Curr Desc:</label>
                    <asp:TextBox ID="txtCurrDesc" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Rate:</label>
                    <asp:TextBox ID="txtRate" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>F_Currency:</label>
                    <asp:TextBox ID="txtFCurrency" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Amount INR:</label>
                    <asp:TextBox ID="txtAmountINR" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Bill No:</label>
                    <asp:TextBox ID="txtBillNoNew" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Location:</label>
                    <asp:TextBox ID="txtLocation" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Bus Segment:</label>
                    <asp:TextBox ID="txtBusSegment" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Gross Margin As Per Order:</label>
                    <asp:TextBox ID="txtGrossMarginAsPerOrder" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Percentage:</label>
                    <asp:TextBox ID="txtPercentage" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Cost As Per Order:</label>
                    <asp:TextBox ID="txtCostAsPerOrder" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Material Overheads:</label>
                    <asp:TextBox ID="txtMaterialOverheads" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />



                    <label>Margin As Per Order:</label>
                    <asp:TextBox ID="txtMarginAsPerOrder" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Cost As Per Cost Center:</label>
                    <asp:TextBox ID="txtCostAsPerCostCenter" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Project Costs:</label>
                    <asp:TextBox ID="txtProjectCosts" runat="server"
                        onKeyUp="checkDec1(this)"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        CssClass="form-control" />


                    <label>Warranty:</label>
                    <asp:TextBox ID="txtWarranty" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Actual Gross Margin:</label>
                    <asp:HiddenField ID="hdActualGrossMargin" runat="server" />
                    <asp:TextBox ID="txtActualGrossMargin" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Standard:</label>
                    <asp:TextBox ID="txtStandard" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Actual:</label>
                    <asp:HiddenField ID="hdActual" runat="server" />
                    <asp:TextBox ID="txtActual" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>High Delta:</label>
                    <asp:HiddenField ID="hdHighDelta" runat="server" />
                    <asp:TextBox ID="txtHighDelta" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Type:</label>
                    <asp:TextBox ID="txtType" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>Month:</label>
                    <asp:TextBox ID="txtMonth" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>POC/NONPOC:</label>
                    <asp:TextBox ID="txtPOCNONPOC" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                    Width="100%" OnClick="btnUpdate_Click" />

            </div>

        </div>

    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
