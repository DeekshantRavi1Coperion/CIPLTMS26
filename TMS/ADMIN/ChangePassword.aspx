<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ADMIN_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
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

            <div class="form-entry-container">

                <fieldset class="form-card">
                    <legend>Change Password</legend>

                    <div class="form-grid form-grid-2">
                        
                        <label>User Name:</label>
                        <input id="txtUserName" type="text"
                            cssclass="form-control"
                            runat="server" enabled="false"
                            disabled="disabled" />

                        <label>Old Password:</label>
                        <input id="txtOldPassword" type="text"
                            cssclass="form-control"
                            runat="server" enabled="false"
                            disabled="disabled" />

                        <label>New Password:</label>
                        <input id="txtNewPassword" type="password"
                            cssclass="form-control"
                            runat="server" enabled="true"
                            maxlength="10" />

                        <label>Confirm Password:</label>
                        <input id="txtConfirmedPassword" type="password"
                            cssclass="form-control"
                            runat="server"
                            enabled="true" maxlength="10" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnUpdate" CssClass="button" runat="server" Width="100%" Text="Update"
                        OnClientClick="return ValidateAll();" OnClick="btnUpdate_Click" />
                </div>
                <div class="full-width">

                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
