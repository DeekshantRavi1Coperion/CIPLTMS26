<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddTaxBill.aspx.cs" Inherits="REPORTS_SALE_ORDER_AddTaxBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 95%">
            <legend style="text-align: center;">Invoice List</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Month:
                    </td>
                    <td>
                        <asp:TextBox ID="txtMonth" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        JOB No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJOBNoNew" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Total Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalAmount" runat="server" Width="100%" Enabled="false" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnAddTaxBill" CssClass="button" Width="100%" runat="server" 
                            Text="Add" onclick="btnAddTaxBill_Click"
                             />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
