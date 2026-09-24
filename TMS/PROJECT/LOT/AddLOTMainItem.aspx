<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddLOTMainItem.aspx.cs" Inherits="PROJECT_LOT_AddLOTMainItem" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />
    
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    
    <link href="../../Styles/form.css" rel="stylesheet" />
    

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <script type="text/javascript">

        function ValidateMainItem() {
            var MainItem = document.getElementById('<%=txtMainItem.ClientID %>').value;
            if (MainItem == '') {
                document.getElementById('<%=txtMainItem.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMainItem.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateMainItem()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to add main item?")) {
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

            <legend>Add LOT Main Item</legend>

            <div class="form-grid form-grid-2">

                <label>Main Item</label>
                <asp:TextBox ID="txtMainItem" runat="server" 
                    CssClass="form-control"/>

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />


            <asp:Button ID="btnLOTMainItemList" runat="server" Width="100%" Text="Main Item List" CssClass="button"
                OnClick="btnLOTMainItemList_Click" />

        </div>

        <div class="full-width">

            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />

            </asp:Panel>
        </div>
    </div>

</asp:Content>
