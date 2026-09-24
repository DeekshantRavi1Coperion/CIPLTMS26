<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="SaleOrderBacklogPOCPosting.aspx.cs" Inherits="REPORTS_SALE_ORDER_SaleOrderBacklogPOCPosting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script>
        function ValidateMonth() {
            var Month = document.getElementById('<%=ddlMonth.ClientID %>').selectedIndex;
            if (Month == '' || Month == '0') {
                document.getElementById('<%=ddlMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateYear() {
            var Year = document.getElementById('<%=ddlYear.ClientID %>').selectedIndex;
            if (Year == '' || Year == '0') {
                document.getElementById('<%=ddlYear.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlYear.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidateMonth()) { return false; }
            if (ValidateYear()) { return false; }
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
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
    </script>

    <script type="text/Javascript">

        function checkDec1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {

                    var row = el.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;


                    var lblOrderAmount = row.cells[5].innerText;
                    var lblPreviousBilling = row.cells[6].innerText;
                    var txtPOCBilling = row.cells[7].getElementsByTagName("input")[0].value;
                    var txtPOCReverse = row.cells[8].getElementsByTagName("input")[0].value;
                    var lblNetBilling = row.cells[9].getElementsByTagName("input")[0].value;

                    var OrderAmount = 0;
                    var PreviousBilling = 0;
                    var POCBilling = 0;
                    var POCReverse = 0;
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


                    if (txtPOCReverse != '' && parseFloat(txtPOCReverse) > '0') {
                        POCReverse = parseFloat(txtPOCReverse);
                    }
                    else {
                        POCReverse = 0;
                    }


                    if ((PreviousBilling + POCBilling) > OrderAmount) {
                        POCBilling = OrderAmount - PreviousBilling;
                    }

                    row.cells[7].getElementsByTagName("input")[0].value = POCBilling;


                    NetBilling = POCBilling - POCReverse;
                    if (parseFloat(POCBilling - POCReverse) >= '0') {
                        row.cells[9].getElementsByTagName("input")[0].value = parseFloat(NetBilling);
                    }
                    else {
                        row.cells[8].getElementsByTagName("input")[0].value = parseFloat(POCBilling);
                        row.cells[9].getElementsByTagName("input")[0].value = '0';
                    }
                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblOrderAmount = row.cells[5].innerText;
                var lblPreviousBilling = row.cells[6].innerText;
                var txtPOCBilling = row.cells[7].getElementsByTagName("input")[0].value;
                var txtPOCReverse = row.cells[8].getElementsByTagName("input")[0].value;
                var lblNetBilling = row.cells[9].getElementsByTagName("input")[0].value;


                var OrderAmount = 0;
                var PreviousBilling = 0;
                var POCBilling = 0;
                var POCReverse = 0;
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

                if (txtPOCReverse != '' && parseFloat(txtPOCReverse) > '0') {
                    POCReverse = parseFloat(txtPOCReverse);
                }
                else {
                    POCReverse = 0;
                }


                if ((PreviousBilling + POCBilling) > OrderAmount) {
                    POCBilling = OrderAmount - PreviousBilling;
                }

                row.cells[7].getElementsByTagName("input")[0].value = parseFloat(POCBilling);


                NetBilling = POCBilling - POCReverse;
                if (parseFloat(POCBilling - POCReverse) >= '0') {
                    row.cells[9].getElementsByTagName("input")[0].value = parseFloat(NetBilling);
                }
                else {
                    row.cells[8].getElementsByTagName("input")[0].value = parseFloat(POCBilling);
                    row.cells[9].getElementsByTagName("input")[0].value = '0';
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">POC Billing [Monthly]</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Month:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                            onblur="return ValidateMonth();" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                            <asp:ListItem Text="SELECT" Value="0" />
                            <asp:ListItem Text="JAN" Value="1" />
                            <asp:ListItem Text="FEB" Value="2" />
                            <asp:ListItem Text="MAR" Value="3" />
                            <asp:ListItem Text="APR" Value="4" />
                            <asp:ListItem Text="MAY" Value="5" />
                            <asp:ListItem Text="JUN" Value="6" />
                            <asp:ListItem Text="JUL" Value="7" />
                            <asp:ListItem Text="AUG" Value="8" />
                            <asp:ListItem Text="SEP" Value="9" />
                            <asp:ListItem Text="OCT" Value="10" />
                            <asp:ListItem Text="NOV" Value="11" />
                            <asp:ListItem Text="DEC" Value="12" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                            onblur="return ValidateYear();" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            <asp:ListItem Text="SELECT" Value="0" />
                            <asp:ListItem Text="2015" Value="2015" />
                            <asp:ListItem Text="2016" Value="2016" />
                            <asp:ListItem Text="2017" Value="2017" />
                            <asp:ListItem Text="2018" Value="2018" />
                            <asp:ListItem Text="2019" Value="2019" />
                            <asp:ListItem Text="2020" Value="2020" />
                            <asp:ListItem Text="2021" Value="2021" />
                            <asp:ListItem Text="2022" Value="2022" />
                            <asp:ListItem Text="2023" Value="2023" />
                            <asp:ListItem Text="2024" Value="2024" />
                            <asp:ListItem Text="2025" Value="2025" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        JOB No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" on />
                    </td>
                    <td>
                        &nbsp;
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
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvSaleOrderPosting" runat="server" CellPadding="4" ForeColor="#333333"
                    PageSize="20" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvSaleOrderPosting_RowDataBound"
                    AutoGenerateColumns="False">
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
                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("Unit") %>' Visible="false" />
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
                                    onkeypress="return inNumberKey(this, event);" onKeyUp="checkDec1(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net_Billing">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNetBilling" Width="100%" runat="server" Text='<%# Eval("Net_Billing") %>'
                                    onkeyDown="javascript:preventInput(event);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Unit" DataField="Unit" />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
