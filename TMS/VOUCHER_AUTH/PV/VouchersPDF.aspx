<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VouchersPDF.aspx.cs"
    EnableEventValidation="false" Inherits="VOUCHER_AUTH_PV_VouchersPDF" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../../viewpdftablecss.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/LOT.css" rel="stylesheet" />

</head>
<body>

    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/pdficon3.png"
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>
        <br />
        <div>
            <asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/COPERION/logo2.png" Height="70px"/>
        </div>
        <div>
            <asp:Literal ID="ltTable" runat="server" />
        </div>
    </form>
    
</body>
</html>
