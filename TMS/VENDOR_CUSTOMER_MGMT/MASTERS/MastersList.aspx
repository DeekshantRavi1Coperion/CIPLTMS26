<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="MastersList.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_MastersList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

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

            document.getElementById('<%=ddlItemCategoryToS.ClientID %>').selectedIndex = '0';
            document.getElementById('<%=ddlItemCategoryToS.ClientID %>').enable = false;

            if (val = '6') {
                document.getElementById('<%=ddlMasterTypeToS.ClientID %>').enable = true;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Masters List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Type:</label>
                    <asp:DropDownList ID="ddlMasterType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Item Categorys" Value="1" />
                        <asp:ListItem Text="Organization Types" Value="2" />
                        <asp:ListItem Text="Relation Types" Value="3" />
                        <asp:ListItem Text="Revision Types" Value="4" />
                        <asp:ListItem Text="Vendor Categorys" Value="5" />
                        <asp:ListItem Text="Item Subcategorys" Value="6" />
                    </asp:DropDownList>

                    <label>Item Category:</label>
                    <asp:DropDownList ID="ddlItemCategory" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Item Subcategory:</label>
                    <asp:DropDownList ID="ddlItemSubcategory" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Name:</label>
                    <asp:TextBox ID="txtName" runat="server"
                        CssClass="form-control" />

                    <label>Dexription:</label>
                    <asp:TextBox ID="txtDescription" runat="server"
                        CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" runat="server"
                    Text="Search" OnClick="btnSearch_Click"
                    Width="100%" />

                <asp:Button ID="btnAddNew" CssClass="button" runat="server"
                    Text="Add New" OnClick="btnAddNew_Click"
                    Width="100%" />
            </div>
        </div>

        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView
                CssClass="employee-grid"
                ID="gvMastersList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvMastersList_RowCommand"
                OnRowDataBound="gvMastersList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />
                            <asp:Label ID="lblItemCategoryFID" runat="server" Visible="false" Text='<%# Eval("ITEM_CATEGORY_FID") %>' />
                            <asp:Label ID="lblTypeFID" runat="server" Visible="false" Text='<%# Eval("TYPE_FID") %>' />
                            <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# Eval("NAME") %>' />
                            <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Master Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="TYPE" HeaderText="Type" />
                    <asp:BoundField DataField="ITEM_CATEGORY" HeaderText="Item Category" />

                    <%--<asp:TemplateField HeaderText="ITEM_CATEGORY">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCategory" runat="server" Text='<%# Eval("ITEM_CATEGORY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:BoundField DataField="NAME" HeaderText="Name" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                </Columns>
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

        </div>

    </div>





    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegend" runat="server" Text="Master Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Type:</label>
                    <asp:DropDownList ID="ddlMasterTypeToS" runat="server"
                        CssClass="form-control"
                        Enabled="false">
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
                    <asp:TextBox ID="txtNameToS" runat="server"
                        CssClass="form-control" />

                    <label>Dexription:</label>
                    <asp:TextBox ID="txtDescriptionToS" runat="server"
                        CssClass="form-control" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" Width="100%" runat="server" Text="Update"
                    OnClientClick="return ValidateAll();" OnClick="btnUpdate_Click" />
            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
