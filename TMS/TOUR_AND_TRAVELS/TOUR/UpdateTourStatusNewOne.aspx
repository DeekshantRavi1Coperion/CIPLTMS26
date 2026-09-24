<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTourStatusNewOne.aspx.cs"
    Inherits="TOUR_AND_TRAVELS_TOUR_UpdateTourStatusNewOne" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIPLTMS</title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
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

    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;
    </script>

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="500px"
            Style="display: block">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server"
                            OnClientClick="javascript:window.close();" />
                    </td>
                </tr>
            </table>
            <fieldset style="width: 96%; margin-left: 9px; margin-top: 10px; height: 250px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLegend" runat="server" /></legend>
                <div style='overflow: auto; width: 99%; height: 230px; border: 1px solid lightgray;
                    margin-left: 5px;'>
                    <table width="90%" align="center">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="80px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnTourList" CssClass="button" runat="server" Text="View Tour List"
                                    Width="100%" OnClick="btnTourList_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
