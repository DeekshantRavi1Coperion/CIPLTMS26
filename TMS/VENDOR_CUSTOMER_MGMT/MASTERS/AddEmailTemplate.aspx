<%@ Page Title="Add Update Email Template" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddEmailTemplate.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_AddEmailTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Update Employee</title>

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />

    <script type="text/javascript">

        function ValidateEntityType() {
            var val = document.getElementById('<%=ddlEntityTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlEntityTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEntityTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateStatus() {
            var val = document.getElementById('<%=ddlStatusToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlStatusToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStatusToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateSubject() {
            var val = document.getElementById('<%=txtSubjectToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtSubjectToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSubjectToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSalutation() {
            var val = document.getElementById('<%=txtSalutationToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtSalutationToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSalutationToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBodyLine() {
            var val = document.getElementById('<%=txtBodyLineToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBodyLineToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBodyLineToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateClosingLine() {
            var val = document.getElementById('<%=txtClosingLineToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtClosingLineToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtClosingLineToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;
            if (ValidateEntityType()) { return false }
            if (ValidateStatus()) { return false }
            if (ValidateSubject()) { return false }
            if (ValidateSalutation()) { return false }
            if (ValidateBodyLine()) { return false }
            if (ValidateClosingLine()) { return false }

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
                <asp:Label ID="lblLegend" runat="server" Text="Add Email Template"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">

                <label>Entity Type:</label>
                <asp:DropDownList ID="ddlEntityTypeToS" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Staus:</label>
                <asp:DropDownList ID="ddlStatusToS" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Subject:</label>
                <asp:TextBox ID="txtSubjectToS" runat="server"
                    CssClass="form-control" />

                <label>Salutation:</label>
                <asp:TextBox ID="txtSalutationToS" runat="server"
                    CssClass="form-control" />

                <label>Body Line:</label>
                <asp:TextBox ID="txtBodyLineToS" runat="server"
                    CssClass="form-control" />

                <label>Closing Line:</label>
                <asp:TextBox ID="txtClosingLineToS" runat="server"
                    CssClass="form-control" />

                <label>Link:</label>
                <asp:TextBox ID="txtLinkToS" runat="server"
                    CssClass="form-control" />

                <label>CC:</label>
                <asp:TextBox ID="txtCCToS" runat="server"
                    CssClass="form-control" />

                <label>BCC:</label>
                <asp:TextBox ID="txtBCCToS" runat="server"
                    CssClass="form-control" />

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
