<%@ Page Title="Add Update Master" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddMasters.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_AddMasters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Update Employee</title>
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />

    <script type="text/javascript">

        function ValidateMasterTypes() {
            var val = document.getElementById('<%=ddlMasterTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlMasterTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMasterTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemCategory() {
            var val = document.getElementById('<%=ddlItemCategoryToS.ClientID %>').selectedIndex;
            var ival = document.getElementById('<%=ddlMasterTypeToS.ClientID %>').selectedIndex;

            if (ival = '6') {
                if (val == '' || val == '0') {
                    document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            } else {
                document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }


        }


        function ValidateName() {
            var val = document.getElementById('<%=txtNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;


            if (ValidateMasterTypes()) { return false }
            if (ValidateItemCategory()) { return false }
            if (ValidateName()) { return false }

            return check;
        }


        function EnableItemCategory() {

            var val = document.getElementById('<%=ddlMasterTypeToS.ClientID %>').selectedIndex;

            var itemCategory = document.getElementById('<%=ddlItemCategoryToS.ClientID %>');

            itemCategory.selectedIndex = 0;
            itemCategory.disabled = true;

            if (val === 6) {
                itemCategory.disabled = false;
            }
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
                <asp:Label ID="lblLegend" runat="server" Text="Add Master"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">

                <label>Type:</label>
                <asp:DropDownList ID="ddlMasterTypeToS" runat="server"
                    CssClass="form-control"
                    onchange="EnableItemCategory()">
                    <asp:ListItem Text="Select" Value="0" />
                    <asp:ListItem Text="Item Categorys" Value="1" />
                    <asp:ListItem Text="Organization Types" Value="2" />
                    <asp:ListItem Text="Relation Types" Value="3" />
                    <asp:ListItem Text="Revision Types" Value="4" />
                    <asp:ListItem Text="Vendor Categorys" Value="5" />
                    <asp:ListItem Text="Item Subcategorys" Value="6" />
                </asp:DropDownList>

                <label>Item Category:</label>
                <asp:DropDownList ID="ddlItemCategoryToS" runat="server"
                    CssClass="form-control"
                    Enabled="false">
                </asp:DropDownList>

                <label>Name:</label>
                <asp:TextBox ID="txtNameToS" runat="server" CssClass="form-control" />

                <label>Dexription:</label>
                <asp:TextBox ID="txtDescriptionToS" runat="server" CssClass="form-control" />

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Save"
                OnClientClick="return ValidateAll();"
                OnClick="btnAdd_Click" Width="100%" />

            <asp:Button ID="btnList" CssClass="button" runat="server" Text="Masters List"
                OnClick="btnList_Click" Width="100%" />

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
