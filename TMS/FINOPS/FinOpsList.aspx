<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="FinOpsList.aspx.cs"
    Inherits="FINOPS_FinOpsList" Title="CIPLTMS- Fin Ops List" %>

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
            document.getElementById('<%=txtMonth.ClientID %>').value = document.getElementById('<%=hdMonth.ClientID %>').value;
        }

        function clientChangedMonth(sender, args) {
            document.getElementById('<%=hdMonth.ClientID %>').value = document.getElementById('<%=txtMonth.ClientID %>').value;
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
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 90%">
                    <legend style="text-align: center;">Fin Ops List</legend>
                    <table width="100%">
                        <tr>
                            <td>Year-Period: </td>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtMonth" runat="server" onkeyDown="javascript:preventInput(event);" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdMonth" runat="server" />
                                            <ajax:CalendarExtender ID="calendarMonth" runat="server" OnClientHidden="onCalendarHidden"
                                                PopupButtonID="imgbtnMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                                BehaviorID="calendarMonth" TargetControlID="txtMonth" OnClientDateSelectionChanged="clientChangedMonth">
                                            </ajax:CalendarExtender>
                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMonth"
                                                FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Year-Period" Width="20px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td>&nbsp;</td>

                            <td align="right">Invoice No:</td>
                            <td align="left">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" Width="100%"></asp:TextBox>
                            </td>

                            <td>&nbsp;</td>

                            <td align="right">Job No:</td>
                            <td>
                                <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td align="right">Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" Width="100%" Height="26px">
                                </asp:DropDownList>
                            </td>

                            <td>&nbsp;</td>

                            <td align="right">Cost Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlCostType" runat="server" Width="100%" Height="26px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Contigency Type:</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlContigencyType" runat="server" Width="100%" Height="26px">                                    
                                </asp:DropDownList>
                            </td>

                            <td>&nbsp;</td>
                            <td align="right">Is VPOC Order:</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlIsVPOCOrder" runat="server" Width="100%" Height="26px">
                                    <asp:ListItem Text="All" Value="0" />
                                    <asp:ListItem Text="Yes" Value="1" />
                                    <asp:ListItem Text="No" Value="2" />
                                </asp:DropDownList>
                            </td>

                            <td>&nbsp;</td>


                            <td align="right">Status1:</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStatus1" runat="server" Width="100%" Height="26px">
                                </asp:DropDownList>
                            </td>

                            <td>&nbsp;</td>

                            <td align="right">Status2:</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStatus2" runat="server" Width="100%" Height="26px">
                                </asp:DropDownList>
                            </td>

                            <td colspan="2">&nbsp;</td>

                            <td align="right">
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClick="btnSearch_Click" />
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
                    <div style='overflow: auto; width: 100%; height: 350px; border: 1px solid lightgray;'>
                        <asp:GridView ID="gvFinOpsList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                            OnRowDataBound="gvFinOpsList_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="EDIT" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinOpsID" runat="server" Visible="false" Text='<%# Eval("FIN_OPS_ID" ) %>' />

                                        <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                            ToolTip="Edit Fin Ops Detail" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TAX_INVOICE_NO" HeaderText="Tax Invoice No" />
                                <asp:BoundField DataField="TAX_INVOICE_DATE" HeaderText="Tax Invioice Date" />
                                <asp:BoundField DataField="MONTH" HeaderText="Month" />
                                <asp:BoundField DataField="JOB_NO" HeaderText="Job No" />
                                <asp:BoundField DataField="COST_CENTER" HeaderText="Cost Center" />
                                <asp:BoundField DataField="TYPE" HeaderText="Type" />
                                <asp:BoundField DataField="COST_TYPE" HeaderText="Cost Type" />
                                <asp:BoundField DataField="BU" HeaderText="BU" />
                                <asp:BoundField DataField="AMOUNT1" HeaderText="Amount1" />
                                <asp:BoundField DataField="HOURS" HeaderText="Hours" />
                                <asp:BoundField DataField="PANELTY_CLAUSE" HeaderText="Panelty Clause" />
                                <asp:BoundField DataField="PAYMENT_TERM" HeaderText="Payment Term" />
                                <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                                <asp:BoundField DataField="CONTIGENCY_TYPE" HeaderText="Contigency Type" />
                                <asp:BoundField DataField="IS_VPOC_ORDER" HeaderText="Is VPOC Order" />
                                <asp:BoundField DataField="DELIVERY_MONTH" HeaderText="Delivery Month" />
                                <asp:BoundField DataField="STATUS1" HeaderText="Status1" />
                                <asp:BoundField DataField="AMOUNT2" HeaderText="Amount2" />
                                <asp:BoundField DataField="STATUS2" HeaderText="Status2" />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Desc" />
                                <asp:BoundField DataField="ITEM_GROUP" HeaderText="Item Group" />
                                <asp:BoundField DataField="ITEM_SUBGROUP" HeaderText="Item Subroup" />
                                <asp:BoundField DataField="ITEM_PIVOT_GROUP" HeaderText="Item Pivot Group" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
