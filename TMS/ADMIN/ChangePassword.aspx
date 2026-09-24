<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ADMIN_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png"/>
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">

        function ValidateNewPassword() {
            var NewPassword = document.getElementById('<%=txtNewPassword.ClientID %>').value;
            if (NewPassword == '') {
                document.getElementById('<%=txtNewPassword.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (NewPassword.length < '6') {
                    document.getElementById('<%=txtNewPassword.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtNewPassword.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }

        function ValidateConfirmPassword() {
            var ConfirmPassword = document.getElementById('<%=txtConfirmedPassword.ClientID %>').value;
            if (ConfirmPassword == '') {
                document.getElementById('<%=txtConfirmedPassword.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtConfirmedPassword.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            var NewPassword = document.getElementById('<%=txtNewPassword.ClientID %>').value;
            if (NewPassword == '') {
                document.getElementById('<%=txtNewPassword.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            var ConfirmPassword = document.getElementById('<%=txtConfirmedPassword.ClientID %>').value;
            if (ConfirmPassword == '') {
                document.getElementById('<%=txtConfirmedPassword.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }
            return (true);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>
            <div align="center" style="margin-top: 100px;">
                <fieldset style="width: 30%;">
                    <legend style="text-align: center;">Change Password</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Name:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtUserName" runat="server" Width="100%" Enabled="false" />--%>
                                <input id="txtUserName" type="text" style="width: 100%;" runat="server" enabled="false"
                                    disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Old Password:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtOldPassword" runat="server" Width="100%" Enabled="false" />--%>
                                <input id="txtOldPassword" type="text" style="width: 100%;" runat="server" enabled="false"
                                    disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Password:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtNewPassword" runat="server" Width="100%" Enabled="true" onblur="return ValidateNewPassword();"
                                    MaxLength="6" />--%>
                                <input id="txtNewPassword" type="password" style="width: 100%;" runat="server" enabled="true"
                                    maxlength="10" onblur="return ValidateNewPassword();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm Password:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtConfirmedPassword" runat="server" Width="100%" Enabled="true"
                                    onblur="return ValidateConfirmPassword();" MaxLength="6" />--%>
                                <input id="txtConfirmedPassword" type="password" style="width: 100%;" runat="server"
                                    enabled="true" maxlength="10" onblur="return ValidateConfirmPassword();" />
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
                            <td>
                                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Width="100%" Text="Update"
                                    OnClientClick="return ValidateAll();" OnClick="btnUpdate_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
