<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddMachines.aspx.cs" Inherits="PROJECT_MACHINE_SCH_AddMachines" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateMachine() {
            var Machine = document.getElementById('<%=txtMachine.ClientID %>').value;
            if (Machine == '') {
                document.getElementById('<%=txtMachine.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMachine.ClientID %>').style.borderColor = "";
                return false;
            }
        }


    </script>

    <script type="text/javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateMachine()) {
                check = false;
            }


            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to save machine?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />


    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>


            <div class="form-entry-container">
                <fieldset class="form-card">

                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Machine"></asp:Label></legend>

                    <div class="form-grid form-grid-2">

                        <label>Machine:</label>
                        <asp:TextBox ID="txtMachine" runat="server"
                            CssClass="form-control" />


                    </div>

                </fieldset>

                <div class="full-width button-group">


                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                        Width="100%" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnMachineList" CssClass="button" runat="server" Text="Machine List" OnClick="btnMachineList_Click" Width="100%" /></td>
                                   

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
