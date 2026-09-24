<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMRNInPDFTwo.aspx.cs"
    EnableEventValidation="false" Inherits="REPORTS_MRN_ViewMRNInPDFTwo" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; border-collapse: collapse; border: 1px solid #ddd; padding: 5px; width: 100%;">
            <tr>
                <td style="border: 1px solid #ddd; padding: 5px; font-size: 10pt;">
                    <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="~/Images/pdficon3.png"
                        OnClick="imgBtnExport_Click" Width="30px" Height="30px" ToolTip="Save as PDF" />
                </td>
            </tr>
        </table>
        <br />

        <div id="html-2-pdfwrapper">
            <asp:Literal ID="ltTable" runat="server"></asp:Literal>
        </div>

        <script type="text/javascript" src="../../Scripts/jspdf/jspdf.min.js"></script>

    </form>
</body>
</html>
