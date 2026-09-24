<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTicketStatus.aspx.cs"
    Inherits="HR_TICKET_UpdateTicketStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIPLTMS</title>

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <script type="text/javascript">

        function ValidateAllocateTo() {
            var AllocateTo = document.getElementById('<%=ddlAllocateTo.ClientID %>').selectedIndex;
            if (AllocateTo == '' || AllocateTo == '0') {
                document.getElementById('<%=ddlAllocateTo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlAllocateTo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            if (ValidateAllocateTo()) { return false; }
        }
    </script>

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
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="480px" Width="600px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server"
                                OnClientClick="javascript:window.close();" />
                        </td>
                    </tr>
                </table>

                <div class="form-entry-container">

                    <fieldset class="form-card">
                        <legend>
                            <asp:Label ID="lblLegend" runat="server" /></legend>

                        <div class="form-grid form-grid-2">

                            <label>Ticket Number:</label>
                            <asp:TextBox ID="txtTicketNumber" runat="server"
                                CssClass="form-control"
                                Enabled="false" />

                            <label>Ticket Type:</label>
                            <asp:TextBox ID="txtTicketType" runat="server"
                                CssClass="form-control"
                                Enabled="false" />

                            <label>Ticket Subtype:</label>
                            <asp:TextBox ID="txtTicketSubtype" runat="server"
                                CssClass="form-control"
                                Enabled="false" />

                            <label>Allocate To:</label>
                            <asp:DropDownList ID="ddlAllocateTo"
                                runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>

                            <label>Priority:</label>
                            <asp:DropDownList ID="ddlAllocatePriority"
                                runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>
                            <%--onblur="return ValidateAllocateTo();"--%>

                            <div class="full-width">
                                <label>Remarks:</label>
                                <asp:TextBox ID="txtTicketRemarks" runat="server" TextMode="MultiLine"
                                    CssClass="form-control" />
                            </div>
                        </div>

                    </fieldset>

                    <div class="full-width button-group">

                        <asp:Button ID="btnAllocate" CssClass="button" runat="server"
                            Text="Allocate Ticket"
                            Width="100%"
                            OnClientClick="return ValidateAll();"
                            OnClick="btnAllocate_Click" />

                        <asp:Button ID="btnCancel" CssClass="button" runat="server"
                            Text="Cancel Ticket"
                            Width="100%" OnClick="btnCancel_Click" />

                        <asp:Button ID="btnTicketList" CssClass="button" runat="server"
                            Text="View Ticket List"
                            Width="100%" OnClick="btnTicketList_Click" />
                    </div>


                    <div class="full-width">

                        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="80px">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>

                    </div>


                </div>

                <fieldset style="width: 96%; margin-left: 9px; margin-top: 10px; height: 250px;">
                    <legend style="text-align: center;"></legend>
                    <div style='overflow: auto; width: 99%; height: 420px; border: 1px solid lightgray; margin-left: 5px;'>
                    </div>
                </fieldset>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
