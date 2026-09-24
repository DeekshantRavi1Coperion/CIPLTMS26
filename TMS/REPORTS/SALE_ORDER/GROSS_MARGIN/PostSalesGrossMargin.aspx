<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PostSalesGrossMargin.aspx.cs" Inherits="REPORTS_SALE_ORDER_GROSS_MARGIN_PostSalesGrossMargin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>


    <style type="text/css">
        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: #D8D8D8;
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
        }
    </style>

    <script type="text/javascript">

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

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>


    <script type="text/javascript">       
        function Confirm() {
            var existedRecordsCount = document.getElementById('<%=hdExistedRecords.ClientID %>').value;
            var month = document.getElementById('<%=txtPostingMonth.ClientID %>').value;

            if (parseInt(existedRecordsCount) > 0) {
                var confirm_value = document.createElement("input");
                confirm_value.type = "hidden";
                confirm_value.name = "Confirm Value";
                if (confirm("There are " + existedRecordsCount + " records exist in system with " + month + " month.Do you want to replace?")) {
                    document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 1;
                }
                else {
                    document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
                }
            }
            else {
                document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
            }
        }
    </script>

    <script type="text/Javascript">       
        function checkDec1(el) {
            var row = el.parentNode.parentNode;

            var amountINR = 0;
            var pojectCosts = 0;
            var actualGrossMargin = 0;
            var standard = 0;
            var actual = 0;
            var highDelta = 0;


            if (parseFloat(row.cells[20].getElementsByTagName("input")[0].value) > 0)
                actualGrossMargin = parseFloat(row.cells[20].getElementsByTagName("input")[0].value).toFixed(2);
            else
                actualGrossMargin = 0;

            if (parseFloat(row.cells[9].getElementsByTagName("input")[0].value) > 0)
                amountINR = parseFloat(row.cells[9].getElementsByTagName("input")[0].value).toFixed(2);
            else
                amountINR = 0;

            if (row.cells[18].getElementsByTagName("input")[0].value != '')
                pojectCosts = parseFloat(row.cells[18].getElementsByTagName("input")[0].value).toFixed(2);
            else {
                pojectCosts = 0;
                row.cells[18].getElementsByTagName("input")[0].value = parseFloat(0).toFixed(2);
            }


            if (row.cells[22].getElementsByTagName("input")[0].value != '')
                standard = parseFloat(row.cells[22].getElementsByTagName("input")[0].value).toFixed(2);
            else
                standard = 0;


            if ((parseFloat(actualGrossMargin) + parseFloat(pojectCosts)) > 0)
                row.cells[21].getElementsByTagName("input")[0].value = parseFloat((parseFloat(actualGrossMargin) + parseFloat(pojectCosts))).toFixed(2);
            else
                row.cells[21].getElementsByTagName("input")[0].value = 0;


            if (parseFloat(row.cells[23].getElementsByTagName("input")[0].value) > 0)
                actual = parseFloat(row.cells[23].getElementsByTagName("input")[0].value).toFixed(2);
            else
                actual = 0;


            if (pojectCosts > 0) {
                if (amountINR > 0)
                    row.cells[24].getElementsByTagName("input")[0].value = parseFloat(parseFloat(row.cells[21].getElementsByTagName("input")[0].value) / amountINR).toFixed(2);
                else
                    row.cells[24].getElementsByTagName("input")[0].value = 0;
            }
            else
                row.cells[24].getElementsByTagName("input")[0].value = parseFloat(actual).toFixed(2);


            if (pojectCosts > 0)
                highDelta = parseFloat(row.cells[25].getElementsByTagName("input")[0].value).toFixed(2);
            else
                highDelta = 0;


            if (parseFloat(row.cells[21].getElementsByTagName("input")[0].value) > 0)
                row.cells[26].getElementsByTagName("input")[0].value = parseFloat(parseFloat(row.cells[24].getElementsByTagName("input")[0].value) - standard).toFixed(2);
            else
                row.cells[26].getElementsByTagName("input")[0].value = parseFloat(highDelta).toFixed(2);

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 75%">
            <legend style="text-align: center;">Post Monthly Sales Gross Margin (Non-POC)</legend>
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
                        <asp:DropDownList ID="ddlPOCNPOC" runat="server" Width="150px" Height="26px" Enabled="false">
                            <asp:ListItem Text="ALL" Value="0" />
                            <asp:ListItem Text="POC" Value="1" />
                            <asp:ListItem Text="Non-POC" Value="2" />
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td align="right">
                        <asp:HiddenField ID="hdExistedRecords" runat="server" />
                        <asp:HiddenField ID="hdReplacementFlag" runat="server" />
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnPost" CssClass="button" Width="100%" runat="server" Text="Post"
                            OnClick="btnPost_Click" OnClientClick="Confirm();" />
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
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView ID="gvSaleMargin" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowDataBound="gvSaleMargin_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="OA_NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                        <asp:Label ID="lblSRNo" runat="server" Visible="false" Text='<%# Eval("SR_NO" ) %>' />
                                        <asp:Label ID="lblOANo" runat="server" Text='<%# Eval("OA_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CUSTOMER_NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="WARRANTY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWarrantyPer" runat="server" Text='<%# Eval("WARRANTY_PER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMO" runat="server" Text='<%# Eval("MO" ) %>' Width="80px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BILL_DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillDate" runat="server" Text='<%# Eval("BILL_DATE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CLASS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClass" runat="server" Text='<%# Eval("CLASS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CURR_DESC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrDesc" runat="server" Text='<%# Eval("CURR_DESC") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="RATE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" runat="server" Text='<%# Eval("RATE" ) %>' Width="100px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="F_CURRENCY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFCurrency" runat="server" Text='<%# Eval("F_CURRENCY" ) %>' Width="120px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--9--%>
                                <asp:TemplateField HeaderText="AMOUNT_INR">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmountINR" runat="server" Text='<%# Eval("AMOUNT_INR" ) %>' Width="150px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BILL_NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("BILL_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LOCATION">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("LOCATION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BUS_SEGMENT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBusSegment" runat="server" Text='<%# Eval("BUS_SEGMENT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="GROSS_MARGIN_AS_PER_ORDER">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGrossMarginAsPerOrder" runat="server" Text='<%# Eval("GROSS_MARGIN_AS_PER_ORDER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PERCENTAGE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPercentage" runat="server" Text='<%# Eval("PERCENTAGE" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="COST_AS_PER_ORDER">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCostAsPerOrder" runat="server" Text='<%# Eval("COST_AS_PER_ORDER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MARGIN_AS_PER_ORDER">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMarginAsPerOrder" runat="server" Text='<%# Eval("MARGIN_AS_PER_ORDER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="COST_AS_PER_COST_CENTER">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCostAsPerCostCenter" runat="server" Text='<%# Eval("COST_AS_PER_COST_CENTER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <%--18--%>
                                <asp:TemplateField HeaderText="PROJECT_COSTS">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProjectCosts" runat="server" Text='<%# Eval("PROJECT_COSTS" ) %>' Width="100%"
                                            onKeyUp="checkDec1(this)" onkeypress="return inNumberKeyWithDecimal(this, event);" CssClass="textbox1"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="WARRANTY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWarranty" runat="server" Text='<%# Eval("WARRANTY" ) %>' Width="120px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdActualGrossMargin" runat="server" Value='<%# Eval("ACTUAL_GROSS_MARGIN" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--21--%>
                                <asp:TemplateField HeaderText="ACTUAL_GROSS_MARGIN">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActualGrossMargin" runat="server" Text='<%# Eval("ACTUAL_GROSS_MARGIN" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--22--%>
                                <asp:TemplateField HeaderText="STANDARD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStandard" runat="server" Text='<%# Eval("STANDARD" ) %>' Width="120px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--23--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdActual" runat="server" Value='<%# Eval("ACTUAL" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--24--%>
                                <asp:TemplateField HeaderText="ACTUAL">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActual" runat="server" Text='<%# Eval("ACTUAL" ) %>' Width="120px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--25--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdHighDelta" runat="server" Value='<%# Eval("HIGH_DELTA" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--26--%>
                                <asp:TemplateField HeaderText="HIGH_DELTA">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtHighDelta" runat="server" Text='<%# Eval("HIGH_DELTA" ) %>' Width="120px"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TYPE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("TYPE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MONTH">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="POC_NONPOC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOCNONPOC" runat="server" Text='<%# Eval("POC_NONPOC") %>' />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
