<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIPLTMS-Login</title>

    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Home.css" rel="stylesheet" type="text/css" />--%>

    <%--  <link href="Styles/form.css" rel="stylesheet" />
    <link href="Styles/filter.css" rel="stylesheet" />
    <link href="Styles/grid.css" rel="stylesheet" />
    <link href="Styles/popup.css" rel="stylesheet" />--%>


    <link href="Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


    <link rel="icon" href="Images/Icon04.png" />

    <script type="text/javascript">

        function EnableUserName() {
            document.getElementById('<%=txtUserNameNew.ClientID %>').value = '';
            document.getElementById('<%=txtUserNameNew.ClientID %>').disabled = false;

            document.getElementById('<%=txtEmployeeID.ClientID %>').value = '';
            document.getElementById('<%=txtEmployeeID.ClientID %>').disabled = true;

            document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "";
        }

        function EnableEmployeeID() {
            document.getElementById('<%=txtUserNameNew.ClientID %>').value = '';
            document.getElementById('<%=txtUserNameNew.ClientID %>').disabled = true;

            document.getElementById('<%=txtEmployeeID.ClientID %>').value = '';
            document.getElementById('<%=txtEmployeeID.ClientID %>').disabled = false;

            document.getElementById('<%=txtUserNameNew.ClientID %>').style.borderColor = "";
        }

        function ValidateUserName() {
            var UserName = document.getElementById('<%=txtUserName.ClientID %>').value;
            if (UserName == '') {
                document.getElementById('<%=txtUserName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUserName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePassword() {
            var Password = document.getElementById('<%=txtPassword.ClientID %>').value;
            if (Password == '') {
                document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUserNameNew() {
            var UserNameNew = document.getElementById('<%=txtUserNameNew.ClientID %>').value;
            if (UserNameNew == '') {
                document.getElementById('<%=txtUserNameNew.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUserNameNew.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateEmployeeID() {
            var EmployeeID = document.getElementById('<%=txtEmployeeID.ClientID %>').value;
            if (EmployeeID == '') {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var UserName = document.getElementById('<%=txtUserName.ClientID %>').value;
            if (UserName == '') {
                document.getElementById('<%=txtUserName.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            var Password = document.getElementById('<%=txtPassword.ClientID %>').value;
            if (Password == '') {
                document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            return (true);
        }

        function ValidateAllNew() {

            if (document.getElementById('<%=rdUserName.ClientID %>').checked == true) {
                var UserNameNew = document.getElementById('<%=txtUserNameNew.ClientID %>').value;
                if (UserNameNew == '') {
                    document.getElementById('<%=txtUserNameNew.ClientID %>').style.borderColor = "#F7627F";
                    return false;
                }
            }

            if (document.getElementById('<%=rdEmployeeID.ClientID %>').checked == true) {
                var EmployeeID = document.getElementById('<%=txtEmployeeID.ClientID %>').value;
                if (EmployeeID == '') {
                    document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "#F7627F";
                    return false;
                }
            }

            return (true);
        }

        function ValidateForgetPassword() {
            if (document.getElementById('<%=chkForgetPassword.ClientID %>').checked == true) {
                alert("hello");
            }
        }
    </script>

    <style type="text/css">
        body {
            background: url(Images/COPERION/coperion_logo02.jpg) no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>






    <meta name="viewport"
        content="width=device-width, initial-scale=1.0" />
    
    <link rel="icon" href="Images/Icon04.png" />

    <style type="text/css">
        /* =========================================================
           LOGIN PAGE
           ========================================================= */

        * {
            box-sizing: border-box;
        }

        html,
        body {
            margin: 0;
            padding: 0;
            width: 100%;
            min-height: 100%;
            font-family: "Segoe UI", Arial, sans-serif;
        }

        body {
            min-height: 100vh;
            background: linear-gradient( 135deg, rgba(7, 18, 36, 0.88), rgba(10, 32, 58, 0.72) ), url("Images/COPERION/coperion_logo02.jpg") center center / cover no-repeat fixed;
        }

        .login-page {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 30px 20px;
        }

        /* =========================================================
           LOGIN CARD
           ========================================================= */

        .login-card {
            width: 100%;
            max-width: 440px;
            background: rgba(255, 255, 255, 0.97);
            border-radius: 18px;
            padding: 42px;
            box-shadow: 0 25px 70px rgba(0, 0, 0, 0.30);
        }

        /* =========================================================
           BRANDING
           ========================================================= */

        .login-brand {
            text-align: center;
            margin-bottom: 30px;
        }

        .login-logo {
            width: 82px;
            height: 82px;
            margin: 0 auto 18px;
            border-radius: 18px;
            display: flex;
            align-items: center;
            justify-content: center;
            background: #ffffff;
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.12);
            overflow: hidden;
        }

            .login-logo img {
                max-width: 72px;
                max-height: 72px;
                object-fit: contain;
            }

        .login-title {
            margin: 0;
            font-size: 28px;
            font-weight: 700;
            color: #172033;
        }

        .login-subtitle {
            margin-top: 7px;
            font-size: 14px;
            color: #718096;
        }

        /* =========================================================
           FORM
           ========================================================= */

        .login-form {
            margin-top: 28px;
        }

        .login-field {
            margin-bottom: 20px;
        }

            .login-field label {
                display: block;
                margin-bottom: 8px;
                font-size: 14px;
                font-weight: 600;
                color: #374151;
            }

        .login-input {
            width: 100%;
            height: 48px;
            padding: 0 15px;
            border: 1px solid #d7dce5;
            border-radius: 10px;
            background: #ffffff;
            color: #172033;
            font-size: 15px;
            outline: none;
            transition: border-color 0.2s ease, box-shadow 0.2s ease;
        }

            .login-input:focus {
                border-color: #2563eb;
                box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.12);
            }

        /* =========================================================
           LOGIN BUTTON
           ========================================================= */

        .login-button {
            width: 100%;
            height: 50px;
            margin-top: 8px;
            border: 0;
            border-radius: 10px;
            background: linear-gradient( 135deg, #2563eb, #1d4ed8 );
            color: #ffffff;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            box-shadow: 0 8px 20px rgba(37, 99, 235, 0.25);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

            .login-button:hover {
                transform: translateY(-1px);
                box-shadow: 0 12px 25px rgba(37, 99, 235, 0.30);
            }

            .login-button:active {
                transform: translateY(0);
            }

        /* =========================================================
           FORGOT PASSWORD
           ========================================================= */

        .forgot-link {
            margin-top: 20px;
            text-align: center;
        }

            .forgot-link label {
                color: #2563eb;
                font-size: 14px;
                cursor: pointer;
            }

                .forgot-link label:hover {
                    text-decoration: underline;
                }

        .forgot-panel {
            margin-top: 25px;
            padding-top: 25px;
            border-top: 1px solid #e5e7eb;
        }

        .forgot-title {
            margin-bottom: 20px;
            font-size: 18px;
            font-weight: 700;
            color: #172033;
        }

        .radio-group {
            display: flex;
            gap: 25px;
            margin-bottom: 22px;
        }

            .radio-group label {
                font-size: 13px;
                color: #4b5563;
                cursor: pointer;
            }

        .forgot-button {
            width: 100%;
            height: 46px;
            border: 1px solid #2563eb;
            border-radius: 9px;
            background: #ffffff;
            color: #2563eb;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
        }

            .forgot-button:hover {
                background: #eff6ff;
            }

        /* =========================================================
           MESSAGE
           ========================================================= */

        .login-message {
            margin-top: 18px;
            padding: 12px 14px;
            border-radius: 8px;
            background: #f3f4f6;
            color: #374151;
            font-size: 13px;
            text-align: center;
        }

        /* =========================================================
           FOOTER
           ========================================================= */

        .login-footer {
            margin-top: 28px;
            text-align: center;
            font-size: 12px;
            color: #9ca3af;
        }

        /* =========================================================
           RESPONSIVE
           ========================================================= */

        @media (max-width: 520px) {

            .login-page {
                padding: 20px 15px;
            }

            .login-card {
                padding: 30px 24px;
                border-radius: 15px;
            }

            .login-title {
                font-size: 24px;
            }

            .radio-group {
                flex-direction: column;
                gap: 12px;
            }
        }
    </style>













</head>
<body>

    <form id="form1" runat="server">

        <asp:ScriptManager
            ID="ScriptManager1"
            runat="server">
        </asp:ScriptManager>

        <div class="login-page">

            <div class="login-card">

                <!-- BRANDING -->

                <div class="login-brand">

                    <div class="login-logo">

                        <img src="Images/Icon04.png"
                             alt="CIPLTMS" />

                    </div>

                    <h1 class="login-title">
                        CIPLTMS
                    </h1>

                    <div class="login-subtitle">
                        Customized Solution
                    </div>

                </div>


                <!-- LOGIN FORM -->

                <div class="login-form">

                    <div class="login-field">

                        <label for="<%= txtUserName.ClientID %>">
                            User Name
                        </label>

                        <asp:TextBox
                            ID="txtUserName"
                            runat="server"
                            CssClass="login-input" />

                    </div>


                    <div class="login-field">

                        <label for="<%= txtPassword.ClientID %>">
                            Password
                        </label>

                        <asp:TextBox
                            ID="txtPassword"
                            runat="server"
                            TextMode="Password"
                            CssClass="login-input" />

                    </div>


                    <asp:Button
                        ID="btnLogin"
                        runat="server"
                        Text="Login"
                        CssClass="login-button"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnLogin_Click" />

                </div>


                <!-- FORGOT PASSWORD -->

                <div class="forgot-link">

                    <asp:CheckBox
                        ID="chkForgetPassword"
                        runat="server"
                        Text="Forgot Password?"
                        AutoPostBack="true"
                        OnCheckedChanged="chkForgetPassword_CheckedChanged" />

                </div>


                <asp:Panel
                    ID="pnlForgetPassword"
                    runat="server"
                    Visible="false"
                    CssClass="forgot-panel">

                    <div class="forgot-title">
                        Reset Password
                    </div>


                    <div class="radio-group">

                        <asp:RadioButton
                            ID="rdUserName"
                            runat="server"
                            Text="By User Name"
                            Checked="true"
                            GroupName="ForgetPassword"
                            onchange="EnableUserName();" />

                        <asp:RadioButton
                            ID="rdEmployeeID"
                            runat="server"
                            Text="By Employee ID"
                            GroupName="ForgetPassword"
                            onchange="EnableEmployeeID();" />

                    </div>


                    <div class="login-field">

                        <label for="<%= txtUserNameNew.ClientID %>">
                            User Name
                        </label>

                        <asp:TextBox
                            ID="txtUserNameNew"
                            runat="server"
                            CssClass="login-input" />

                    </div>


                    <div class="login-field">

                        <label for="<%= txtEmployeeID.ClientID %>">
                            Employee ID
                        </label>

                        <asp:TextBox
                            ID="txtEmployeeID"
                            runat="server"
                            CssClass="login-input" />

                    </div>


                    <asp:Button
                        ID="btnSendPassword"
                        runat="server"
                        Text="Send Password"
                        CssClass="forgot-button"
                        OnClientClick="return ValidateAllNew();"
                        OnClick="btnSendPassword_Click" />

                </asp:Panel>


                <!-- MESSAGE -->

                <asp:Panel
                    ID="pnlMsg"
                    runat="server"
                    Visible="false"
                    CssClass="login-message">

                    <asp:Label
                        ID="lblMsg"
                        runat="server"
                        Visible="true" />

                </asp:Panel>


                <!-- FOOTER -->

                <div class="login-footer">
                    © CIPLTMS. All rights reserved.
                </div>

            </div>

        </div>

    </form>

</body>
</html>
