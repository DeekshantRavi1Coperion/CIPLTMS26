<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddTCChecker.aspx.cs" Inherits="TC_AddTCChecker" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <script type="text/javascript">

        function ValidateUnit() {
            var unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (unit == '' || unit == 0) {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == 0) {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateChecker() {
            var Checker = document.getElementById('<%=ddlChecker.ClientID %>').selectedIndex;
            if (Checker == '' || Checker == 0) {
                document.getElementById('<%=ddlChecker.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlChecker.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateUnit()) {
                check = false;
            }

            if (ValidateDepartment()) {
                check = false;
            }

            if (ValidateChecker()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to add checker?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />


    <div class="form-entry-container">
        <fieldset class="form-card">

            <legend>Add TC Checker</legend>

            <div class="form-grid form-grid-2">

                <label>Unit:</label>
                <asp:DropDownList ID="ddlUnit" runat="server" 
                    CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" />

                <label>Department:</label>
                <asp:DropDownList ID="ddlDepartment" runat="server" 
                    CssClass="form-control" />

                <label>Checker:</label>
                <asp:DropDownList ID="ddlChecker" runat="server" 
                    CssClass="form-control"/>

            </div>

        </fieldset>

        <div class="full-width button-group">
            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />

            <asp:Button ID="btnViewCheckerList" runat="server" Width="100%" Text="View Checker List" CssClass="button"
                OnClick="btnViewCheckerList_Click" />
        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>
    </div>

</asp:Content>
