<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateLOTApprover.aspx.cs" 
    Inherits="MR_WORKFLOW_UpdateLOTApprover" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="../../Images/Icon04.png" />

    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <div align="center" style="margin-top: 20px;">
            <fieldset style="width: 60%;">
                <legend style="text-align: center;">Update LOT Project Approvers
                </legend>
                <table width="96%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                            </asp:Panel>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- <p>JOB Number:</p>
                            <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" Enabled="false" />--%>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <p>JOB Number:</p>
                                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        <p>Unit:</p>
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>


                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Project Manager:
                            </p>
                            <asp:DropDownList ID="ddlProjectManager" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateProjectManager();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                Project Engineer:
                            </p>
                            <asp:DropDownList ID="ddlProjectEngineer" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateProjectEngineer();" />
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
                </table>
            </fieldset>
        </div>




        <script type="text/javascript">

            function ValidateCompany() {
                var Company = document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex;
                if (Company == '' || Company == 0) {
                    document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "";
                    return false;
                }
            }


            function ValidateProjectManager() {
                var ProjectManager = document.getElementById('<%=ddlProjectManager.ClientID %>').selectedIndex;
                if (ProjectManager == '' || ProjectManager == 0) {
                    document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateProjectEngineer() {
                var ProjectEngineer = document.getElementById('<%=ddlProjectEngineer.ClientID %>').selectedIndex;
                if (ProjectEngineer == '' || ProjectEngineer == 0) {
                    document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

        </script>


        <script type="text/javascript">

            function ValidateAll() {
                var check = true;

                if (ValidateCompany()) {
                    check = false;
                }
                
                if (ValidateProjectManager()) {
                    check = false;
                }

                if (ValidateProjectEngineer()) {
                    check = false;
                }

                if (check) {
                    if (confirm("Would you like to add approvers?")) {
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

        <script type="text/javascript">

            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };

        </script>


    </form>
</body>
</html>
