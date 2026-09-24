<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOTupdateDrawing.aspx.cs" Inherits="PROJECT_LOT_LOTupdateDrawing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 50%;">
            <table>
                <tr>
                    <td>LOT TF Subitem ID:</td>
                    <td>
                        <asp:TextBox ID="txtRecordID" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>File1:</td>
                    <td>
                        <asp:FileUpload ID="uploadFileSiDrawing1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>File2:</td>
                    <td>
                        <asp:FileUpload ID="uploadFileSiDrawing2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>File3:</td>
                    <td>
                        <asp:FileUpload ID="uploadFileSiDrawing3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>File4:</td>
                    <td>
                        <asp:FileUpload ID="uploadFileSiDrawing4" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="button" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>

            <br />
            <table>
                <tr>
                    <td>LOT TF ID:</td>
                    <td>
                        <asp:TextBox ID="txtLotTfId" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>File1:</td>
                    <td>
                        <asp:FileUpload ID="uploadFileClientApprovedDrawing" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpdateClientApprovedDrawing" runat="server" 
                            Text="Update Client Approved Drawing"
                            OnClick="btnUpdateClientApprovedDrawing_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
