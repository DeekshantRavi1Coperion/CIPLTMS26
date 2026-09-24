<%@ Page Title="Add Update Master" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddResponsibles.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_AddResponsibles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Update Employee</title>
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />

    <script type="text/javascript">

        function ValidateName() {
            var val = document.getElementById('<%=txtShortNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtShortNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtShortNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;
            if (ValidateName()) { return false }

            return check;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="form-entry-container">
        <fieldset class="form-card">

            <legend>
                <asp:Label ID="lblLegend" runat="server" Text="Add Responsible"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">

                <label>Short Name:</label>
                <asp:TextBox ID="txtShortNameToS" runat="server"
                    CssClass="form-control" />

                <label>Full Name:</label>
                <asp:TextBox ID="txtFullNameToS" runat="server"
                    CssClass="form-control" />

                <label>Tag Employee:</label>
                <asp:DropDownList ID="ddlEmployeeToS" runat="server"
                    CssClass="form-control"
                    Enabled="false">
                </asp:DropDownList>

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Save"
                OnClientClick="return ValidateAll();"
                OnClick="btnAdd_Click" Width="100%" />

            <asp:Button ID="btnList" CssClass="button" runat="server" Text="Responsible List"
                OnClick="btnList_Click" Width="100%" />

        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
        </div>
    </div>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
