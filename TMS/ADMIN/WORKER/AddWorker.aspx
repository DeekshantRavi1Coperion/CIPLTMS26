<%@ Page Title="Add Update Employee" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddWorker.aspx.cs" Inherits="ADMIN_WORKER_AddWorker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Update Employee</title>
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <script type="text/javascript">

        function ValidateEmployeeName() {
            var EmployeeName = document.getElementById('<%=txtEmployeeName.ClientID %>').value;
            if (EmployeeName == '') {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "";
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

        function ValidateDesignation() {
            var Designation = document.getElementById('<%=txtDesignation.ClientID %>').value;
            if (Designation == '') {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;

            if (ValidateEmployeeName()) {
                return false
            }

            if (ValidateEmployeeID()) {
                return false
            }

            if (ValidateDesignation()) {
                return false
            }

            if (ValidateDepartment()) {
                return false
            }

            if (ValidateUnit()) {
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
            <legend>Add Worker</legend>

            <div class="form-grid form-grid-2">

                <label>Worker Name</label>
                <asp:TextBox ID="txtEmployeeName"
                    runat="server"
                    CssClass="form-control" />

                <label>Worker ID</label>
                <asp:TextBox ID="txtEmployeeID"
                    runat="server"
                    CssClass="form-control" />

                <label>Designation</label>
                <asp:TextBox ID="txtDesignation"
                    runat="server"
                    CssClass="form-control" />

                <label>Department</label>
                <asp:DropDownList ID="ddlDepartment"
                    runat="server"
                    CssClass="form-control" />

                <label>Unit</label>
                <asp:DropDownList ID="ddlUnit"
                    runat="server"
                    CssClass="form-control" />

                <label>Leader</label>
                <asp:DropDownList ID="ddlTeamLeader"
                    runat="server"
                    CssClass="form-control" />

            </div>
        </fieldset>


        <div class="full-width button-group">
            <asp:Button ID="btnAdd"
                runat="server"
                Text="Save"
                CssClass="button"
                OnClientClick="return ValidateAll();"
                OnClick="btnAdd_Click" Width="100%" />

            <asp:Button ID="btnEmployeeList"
                runat="server"
                Text="Worker List"
                CssClass="button"
                OnClick="btnEmployeeList_Click" />
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
