<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddWorkerConvenience.aspx.cs" Inherits="HR_AddWorkerConvenience" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
  

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <script type="text/javascript">

        function ValidateWorker() {
            var Worker = document.getElementById('<%=ddlWorker.ClientID %>').selectedIndex;
            if (Worker == '' || Worker == 0) {
                document.getElementById('<%=ddlWorker.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlWorker.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateConvenience() {
            var Convenience = document.getElementById('<%=txtConvenienceAmt.ClientID %>').value;
            if (Convenience == '' || parseFloat(Convenience) == 0) {
                document.getElementById('<%=txtConvenienceAmt.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtConvenienceAmt.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;

            if (ValidateWorker()) {
                check = false
            }

            if (ValidateConvenience()) {
                check = false
            }

            return check;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>

            <div class="form-entry-container">
                <fieldset class="form-card">

                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Worker Convenience"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>Worker Name:</label>
                        <asp:DropDownList ID="ddlWorker" runat="server" 
                            CssClass="form-control">
                        </asp:DropDownList>

                        <label>Convenience Amount:</label>
                        <asp:TextBox ID="txtConvenienceAmt" runat="server" 
                            onkeypress="return inNumberKeyWithDecimal(this, event);"
                            CssClass="form-control"/>

                    </div>

                </fieldset>

                <div class="full-width button-group">
                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit"
                            OnClientClick="return ValidateAll();"
                            OnClick="btnSubmit_Click" Width="100%" />

                        <asp:Button ID="btnWorkerList" CssClass="button" runat="server" Text="Worker List"
                            OnClick="btnWorkerList_Click" Width="100%" />
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
