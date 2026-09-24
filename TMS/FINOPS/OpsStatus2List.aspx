<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="OpsStatus2List.aspx.cs" Inherits="FINOPS_OpsStatus2List" %>

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
        function ValidateOpsStatus2ToEdit() {
            var OpsStatus2ToEdit = document.getElementById('<%=txtOpsStatus2ToEdit.ClientID %>').value;
            if (OpsStatus2ToEdit == '') {
                document.getElementById('<%=txtOpsStatus2ToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOpsStatus2ToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;

            if (ValidateOpsStatus2ToEdit()) {
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
                    <legend style="text-align: center;">Status2 List</legend>
                    <table width="100%">
                        <tr>
                            <td>Status2 Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtOpsStatus2" runat="server" Width="100%" />
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
                    <asp:GridView ID="gvOpsStatus2List" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" HorizontalAlign="Center"
                        OnRowCommand="gvOpsStatus2List_RowCommand" OnRowDataBound="gvOpsStatus2List_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="Status2" HeaderText="Status2"></asp:BoundField>
                            <asp:TemplateField HeaderText="PROPERTIES">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus2ID" runat="server" Visible="false" Text='<%# Eval("Status2_ID") %>'></asp:Label>
                                    <asp:Label ID="lblStatus2" runat="server" Visible="false" Text='<%# Eval("Status2") %>'></asp:Label>
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
            <%--EDIT OPS Status2 START--%>
            <asp:Button ID="btnShowPopupOpsType" runat="server" Style="display: none" />
            <ajax:ModalPopupExtender ID="modalPopupExtenderOpsType" runat="server" TargetControlID="btnShowPopupOpsType"
                PopupControlID="pnlPopupOpsType" CancelControlID="imgBtnCancelOpsType" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlPopupOpsType" runat="server" BackColor="White" Height="300px" Width="600px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnCancelOpsType" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>
                <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                    <legend style="text-align: center;">Edit Status2</legend>
                    <table style="width: 90%; margin-left: 20px;">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <p>Status2 Name:</p>
                                <asp:TextBox ID="txtOpsStatus2ToEdit" runat="server" Width="100%" onblur="return ValidateOpsStatus2ToEdit();" />
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
            <%--EDIT OPS Status2 END--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
