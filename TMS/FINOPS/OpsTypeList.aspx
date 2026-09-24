<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="OpsTypeList.aspx.cs" Inherits="FINOPS_OpsTypeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">
        function ValidateTypeToEdit() {
            var typeToEdit = document.getElementById('<%=txtTypeToEdit.ClientID %>').value;
            if (typeToEdit== '') {
                document.getElementById('<%=txtTypeToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTypeToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateTypeToEdit()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to upadte?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 100px;">
                <fieldset style="width: 50%">
                    <legend style="text-align: center;">Type List</legend>
                    <table width="100%">
                        <tr>
                            <td>Type:
                            </td>
                            <td>
                                <asp:TextBox ID="txtType" runat="server" Width="100%" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                Width="100%" /></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddNew" CssClass="button" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                                Width="100%" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <br />
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <br />
            <div align="center">
                <fieldset style="width: 50%">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
                    <asp:GridView ID="gvOpsTypeList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" HorizontalAlign="Center"
                        OnRowCommand="gvOpsTypeList_RowCommand" OnRowDataBound="gvOpsTypeList_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="TYPE" HeaderText="TYPE"></asp:BoundField>
                            <asp:TemplateField HeaderText="PROPERTIES">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeID" runat="server" Visible="false" Text='<%# Eval("TYPE_ID") %>'></asp:Label>
                                    <asp:Label ID="lblType" runat="server" Visible="false" Text='<%# Eval("TYPE") %>'></asp:Label>
                                    <asp:ImageButton ID="imgProperties" ToolTip="Edit Tour Information" CommandArgument="PROPERTIES"
                                        runat="server" ImageUrl="~/Images/royal_search.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </fieldset>
            </div>
            <%--EDIT OPS TYPE START--%>
            <asp:Button ID="btnShowPopupType" runat="server" Style="display: none" />
            <ajax:ModalPopupExtender ID="modalPopupExtenderType" runat="server" TargetControlID="btnShowPopupType"
                PopupControlID="pnlPopupType" CancelControlID="imgBtnCancelType" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlPopupType" runat="server" BackColor="White" Height="300px" Width="600px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnCancelType" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>
                <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                    <legend style="text-align: center;">Edit Type</legend>
                    <table style="width: 90%; margin-left: 20px;">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <p>Type:</p>
                                <asp:TextBox ID="txtTypeToEdit" runat="server" Width="100%" onblur="return ValidateTypeToEdit();" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                                    Width="100%" OnClick="btnUpdate_Click" OnClientClick="return ValidateAll();" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <%--EDIT OPS TYPE END--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
