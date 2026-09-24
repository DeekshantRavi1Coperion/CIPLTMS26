<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddMacineActivities.aspx.cs" Inherits="PROJECT_MACHINE_SCH_AddMacineActivities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateActivity() {
            var Activity = document.getElementById('<%=ddlActivity.ClientID %>').selectedIndex;
            if (Activity == '' || Activity == 0) {
                document.getElementById('<%=ddlActivity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlActivity.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateMachine() {
            var Machine = document.getElementById('<%=ddlMacnine.ClientID %>').selectedIndex;
            if (Machine == '' || Machine == 0) {
                document.getElementById('<%=ddlMacnine.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMacnine.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateActivity()) {
                check = false;
            }

            if (ValidateMachine()) {
                check = false;
            }

            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to save machine activity?")) {
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
                        <asp:Label ID="lblLegend" runat="server" Text="Add Machine Activites"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">


                        <label>Activity:</label>
                        <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Machine:</label>
                        <asp:DropDownList ID="ddlMacnine" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                    </div>

                </fieldset>

                <div class="full-width button-group">

                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                        Width="100%" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnMachineActivityList" CssClass="button" runat="server" Text="Machine Activities List"
                        OnClick="btnMachineActivityList_Click" Width="100%" /></td>
                                    

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
