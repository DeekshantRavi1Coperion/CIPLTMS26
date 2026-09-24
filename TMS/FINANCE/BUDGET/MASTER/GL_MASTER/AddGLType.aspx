<%@ Page Title="CIPLTMS- Add GL Types" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddGLType.aspx.cs" Inherits="FINANCE_BUDGET_MASTER_GL_MASTER_AddGLType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
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

    <script type="text/javascript">

      
        function ValidateGLType() {
            var GLType = document.getElementById('<%=txtGLType.ClientID %>').value;
            if (GLType == '') {
                document.getElementById('<%=txtGLType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGLType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        


        function ValidateAll() {

            var check = true;

            if (ValidateGLType()) {
                check = false;
            }

            

            if (check) {
                if (confirm("Would you like to submit?")) {
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



    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 2px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" Text="Add GL Type"></asp:Label></legend>
            <table width="100%">
                
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>GL Type:</td>
                    <td>
                        <asp:TextBox ID="txtGLType" runat="server" Width="100%"
                            onblur="return ValidateGLType();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>GL Type Description:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtGLTypeDescription" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" 
                                        OnClientClick="return ValidateAll();"
                                        OnClick="btnSubmit_Click" Width="100%" />
                                </td>
                                <td style="width: 10%">&nbsp;
                                </td>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnGLTypeList" CssClass="button" runat="server" Text="GL Type List"
                                        OnClick="btnGLTypeList_Click" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
