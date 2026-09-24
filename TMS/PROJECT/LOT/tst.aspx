<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tst.aspx.cs" Inherits="PROJECT_LOT_tst" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlSiDrawing1" runat="server" Visible="true">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdSiDrawing1" runat="server" />
                            <asp:TextBox ID="txtSiDrawing1" runat="server" Width="100%" /></td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnSiDrawing1" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnSiDrawing1_Click" ToolTip="Remove" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlUploadSiDrawing1" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdUploadSiDrawing1" runat="server" />
                            <asp:FileUpload ID="uploadFileSiDrawing1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                        </td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnUploadSiDrawing1" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUploadSiDrawing1_Click" ToolTip="Undo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <br />

        <div>
            <asp:Panel ID="pnlSiDrawing2" runat="server" Visible="true">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdSiDrawing2" runat="server" />
                            <asp:TextBox ID="txtSiDrawing2" runat="server" Width="100%" /></td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnSiDrawing2" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnSiDrawing2_Click" ToolTip="Remove" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlUploadSiDrawing2" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdUploadSiDrawing2" runat="server" />
                            <asp:FileUpload ID="uploadFileSiDrawing2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                        </td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnUploadSiDrawing2" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUploadSiDrawing2_Click" ToolTip="Undo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <br />

        <div>
            <asp:Panel ID="pnlSiDrawing3" runat="server" Visible="true">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdSiDrawing3" runat="server" />
                            <asp:TextBox ID="txtSiDrawing3" runat="server" Width="100%" /></td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnSiDrawing3" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnSiDrawing3_Click" ToolTip="Remove" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlUploadSiDrawing3" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdUploadSiDrawing3" runat="server" />
                            <asp:FileUpload ID="uploadFileSiDrawing3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                        </td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnUploadSiDrawing3" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUploadSiDrawing3_Click" ToolTip="Undo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <br />

        <div>
            <asp:Panel ID="pnlSiDrawing4" runat="server" Visible="true">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdSiDrawing4" runat="server" />
                            <asp:TextBox ID="txtSiDrawing4" runat="server" Width="100%" /></td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnSiDrawing4" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnSiDrawing4_Click" ToolTip="Remove" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlUploadSiDrawing4" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td style="width: 80%;">
                            <asp:HiddenField ID="hdUploadSiDrawing4" runat="server" />
                            <asp:FileUpload ID="uploadFileSiDrawing4" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                        </td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="imgBtnUploadSiDrawing4" runat="server" Height="30px" Width="30px"
                                ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUploadSiDrawing4_Click" ToolTip="Undo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

    </form>
</body>
</html>
