<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddTCDepartment.aspx.cs" Inherits="TC_AddTCDepartment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
            var Department = document.getElementById('<%=txtDepartment.ClientID %>').value;
            if (Department == '') {
                document.getElementById('<%=txtDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDepartment.ClientID %>').style.borderColor = "";
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
                if (confirm("Would you like to add department?")) {
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

    <div align="center" style="margin-top: 180px;">
        <fieldset style="width: 40%;">
            <legend style="text-align: center;">Add TC Department
            </legend>
            <table width="75%">
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>
                        <p>
                            Unit:
                        </p>
                        <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="26px" onblur="return ValidateUnit();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>
                        <p>
                            Department:                       
                        </p>
                        <asp:TextBox ID="txtDepartment" runat="server" Width="100%" onblur="return ValidateDepartment();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <p>
                            Checker:
                        </p>
                        <asp:DropDownList ID="ddlChecker" runat="server" Width="100%" Height="26px" onblur="return ValidateChecker();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                            OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnViewDepartmentList" runat="server" Width="100%" Text="View Department List" CssClass="button"
                            OnClick="btnViewDepartmentList_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
