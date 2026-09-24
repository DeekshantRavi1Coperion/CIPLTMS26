<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POHeaderDetail.aspx.cs" Inherits="REPORTS_PURCHASE_ORDER_POHeaderDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="uppanel">
            <ContentTemplate>
                <div align="center" style="margin-top: 20px;">
                    <fieldset style="width: 80%;">
                        <legend style="text-align: center;">PO Detail</legend>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>PO No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPONo" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>PO Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPODate" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Vendor:
                                </td>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtVendorName" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtVendorCode" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Total PO Value:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValue" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>Total Invoice Value:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalInvoiceValue" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Total PO Quantity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOQuantity" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>Total MRN Quantity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalMRNQuantity" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvPOReportDetail" runat="server" CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="true" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvPOReportDetail_RowDataBound  ">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOValue" runat="server" Visible="false" Text='<%# Eval("PO_VALUE") %>' />
                                            <asp:Label ID="lblPOQuantity" runat="server" Visible="false" Text='<%# Eval("PO_QUANTITY") %>' />
                                            <asp:Label ID="lblMRNQuantity" runat="server" Visible="false" Text='<%# Eval("MRN_QUANTITY") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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
    </form>
</body>
</html>
