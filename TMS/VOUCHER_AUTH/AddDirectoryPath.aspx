<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddDirectoryPath.aspx.cs" Inherits="VOUCHER_AUTH_AddDirectoryPath" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <script type="text/javascript">

        function ValidateFuser() {
            var Value = document.getElementById('<%=ddlUsers.ClientID %>').selectedIndex;
            if (Value == '' || Value == '0') {
                document.getElementById('<%=ddlUsers.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUsers.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateType() {
            var Value = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Value == '' || Value == '0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePath() {
            var Value = document.getElementById('<%=txtFilePath.ClientID %>').value;
            if (Value == '') {
                document.getElementById('<%=txtFilePath.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtFilePath.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            var check = true;

            if (ValidateFuser()) {
                check = false;
            }

            if (ValidateType()) {
                check = false;
            }

            if (ValidatePath()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you save path?")) {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdAttachment1ConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }


    </script>


    <style type="text/css">
        .boxcss {
            margin-top: 12%;
            width: 50%;
            border-radius: 10px;
            background-color: skyblue;
            /*opacity: 0.9;*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:HiddenField ID="hdAttachment1ConfirmValue" runat="server" Value="0" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>


            <div class="form-entry-container">
                <fieldset class="form-card">

                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Directory Path"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>User:</label>
                        <asp:DropDownList ID="ddlUsers" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
                        </asp:DropDownList>


                        <label>Type:</label>
                        <asp:DropDownList ID="ddlType" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>


                        <label>Path:</label>
                        <asp:TextBox ID="txtFilePath" runat="server"
                            CssClass="form-control" />

                    </div>

                </fieldset>

                <div class="full-width button-group">
                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit"
                        OnClientClick="return ValidateAll();"
                        Width="100%" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnVoucherDirectoryPathList" CssClass="button"
                        runat="server" Text="Voucher Directory Path List"
                        Width="100%" OnClick="btnVoucherDirectoryPathList_Click" />
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
