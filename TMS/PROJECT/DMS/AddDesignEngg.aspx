<%@ Page Title="Add Update Employee" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddDesignEngg.aspx.cs" Inherits="PROJECT_DMS_AddDesignEngg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Design Engineer</title>

    <link rel="icon" href="../../Images/Icon04.png" />

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <script type="text/javascript">

        function ValidateEmployees() {
            var Employees = document.getElementById('<%=ddlEmployees.ClientID %>').selectedIndex;

            if (Employees == '' || Employees == '0') {
                document.getElementById('<%=ddlEmployees.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployees.ClientID %>').style.borderColor = "";
                return false;
            }

        }


        function ValidateAll() {

            var check = true;

            if (ValidateEmployees()) {
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
                <asp:Label ID="lblLegend" runat="server" Text="Add Design Engineer"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">
                <label>Employees</label>
                <asp:DropDownList ID="ddlEmployees" runat="server"
                    CssClass="form-control" />

            </div>
        </fieldset>
        <div class="full-width button-group">
            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Save"
                OnClientClick="return ValidateAll();"
                OnClick="btnAdd_Click" Width="100%" />

            <asp:Button ID="btnEnggList" CssClass="button" runat="server" Text="Design Engg. List"
                OnClick="btnEnggList_Click" Width="100%" />
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
