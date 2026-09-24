<%@ Page Title="Add Update Employee" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddApprovers.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_AddApprovers" %>

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

        function ValidateApproverType() {
            var val = document.getElementById('<%=ddlApproverTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlApproverTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlApproverTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateApprover() {
            var val = document.getElementById('<%=ddlApproverToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlApproverToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlApproverToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var check = true;


            if (ValidateEntityType()) {
                return false
            }

            if (ValidateApproverType()) {
                return false
            }

            if (ValidateApprover()) {
                return false
            }

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
                <asp:Label ID="lblLegend" runat="server" Text="Add Approver"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">

                <label>Entity Type:</label>
                <asp:DropDownList ID="ddlEntityTypeToS" runat="server" 
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Approver Type:</label>
                <asp:DropDownList ID="ddlApproverTypeToS" runat="server" 
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Approver:</label>
                <asp:DropDownList ID="ddlApproverToS" runat="server" 
                    CssClass="form-control">
                </asp:DropDownList>

            </div>

        </fieldset>

        <div class="full-width button-group">
            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Save"
                OnClientClick="return ValidateAll();"
                OnClick="btnAdd_Click" Width="100%" />

            <asp:Button ID="btnApproversList" CssClass="button" runat="server" Text="Approvers List"
                OnClick="btnApproversList_Click" Width="100%" />
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
