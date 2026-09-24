<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddMenu.aspx.cs" Inherits="ADMIN_MENU_AddMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../Styles/form.css" rel="stylesheet" />

    <script type="text/javascript">

        function ValidateMenuName() {
            var MenuName = document.getElementById('<%=txtMenuName.ClientID %>').value;
            if (MenuName == '') {
                document.getElementById('<%=txtMenuName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMenuName.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateURL() {
            var URL = document.getElementById('<%=txtURL.ClientID %>').value;
            if (URL == '') {
                document.getElementById('<%=txtURL.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtURL.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var MenuName = document.getElementById('<%=txtMenuName.ClientID %>').value;
            if (MenuName == '') {
                document.getElementById('<%=txtMenuName.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            return true;
        }

    </script>

    <style type="text/css">
        .boxcss {
            margin-top: 12%;
            width: 50%;
            border-radius: 10px;
            background-color: skyblue;
            /*opacity: 0.9;*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>



            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Menu"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>Menu Name</label>
                        <asp:TextBox ID="txtMenuName"
                            runat="server"
                            CssClass="form-control" />

                        <label>URL</label>
                        <asp:TextBox ID="txtURL"
                            runat="server"
                            CssClass="form-control" />

                        <label>Parent Menu</label>
                        <asp:DropDownList ID="ddlParentMenu"
                            runat="server"
                            CssClass="form-control" />


                        <label>Serial No</label>
                        <asp:TextBox ID="txtSerialNo"
                            runat="server"
                            CssClass="form-control"
                            onkeypress="return inNumberKey(this, event);" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSubmit"
                        runat="server"
                        Text="Save"
                        CssClass="button"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnSubmit_Click" Width="100%" />

                    <asp:Button ID="btnMenuList"
                        runat="server"
                        Text="Menu List"
                        CssClass="button"
                        OnClick="btnMenuList_Click" />
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
