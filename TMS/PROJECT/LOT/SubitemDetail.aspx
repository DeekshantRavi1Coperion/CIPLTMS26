<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubitemDetail.aspx.cs" Inherits="PROJECT_LOT_SubitemDetail" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <script type="text/javascript">

        function ValidateDescriptionToEdit() {
            var DescriptionToEdit = document.getElementById('<%=txtDescriptionToEdit.ClientID %>').value;
            if (DescriptionToEdit == '') {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrgNoToEdit() {
            var DrgNoToEdit = document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').value;
            if (DrgNoToEdit == '') {
                document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevNoToEdit() {
            var RevNoToEdit = document.getElementById('<%=txtRevNoToEdit.ClientID %>').value;
            if (RevNoToEdit == '') {
                document.getElementById('<%=txtRevNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRevNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCategoryToEdit() {
            var CategoryToEdit = document.getElementById('<%=chkLstCategoryTEdit.ClientID %>');
            var chkBoxToEdit = CategoryToEdit.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < chkBoxToEdit.length; i++) {
                if (chkBoxToEdit[i].checked) {
                    counter++;
                }
            }

            if (counter == 0) {
                alert("Please select atleast one category...!!");
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateNoOfCopiesToEdit() {
            var NoOfCopiesToEdit = document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').value;
            if (NoOfCopiesToEdit == '') {
                document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAddSubitemToEdit() {
            var check = true;

            if (ValidateDescriptionToEdit()) {
                check = false;
            }

            if (ValidateDrgNoToEdit()) {
                check = false;
            }

            if (ValidateRevNoToEdit()) {
                check = false;
            }

            if (ValidateCategoryToEdit()) {
                check = false;
            }

            if (ValidateNoOfCopiesToEdit()) {
                check = false;
            }

            return check;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>

        <div style="margin-top: 50px; margin-left: 10%;">
            <table width="90%">
                <tr>
                    <td>Description:</td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>

                    <td>Drg./Doc No.:</td>
                    <td>
                        <asp:TextBox ID="txtDrgNo" runat="server" Width="100%" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnSerach" runat="server" Width="100%" Text="Search" CssClass="button"
                            OnClick="btnSerach_Click" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td colspan="8">
                        <div align="center">
                            <fieldset style="width: 100%;">
                                <legend style="text-align: center;">
                                    <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" /></legend>
                                <div style='overflow: auto; width: 100%; height: 350px; border: 1px solid lightgray;'>
                                    <asp:GridView ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                                        OnRowCommand="gvSubItem_RowCommand" OnRowDataBound="gvSubItem_RowDataBound">
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>'></asp:Label>
                                                    <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit" ImageUrl="~/Images/NEWICONS/Amendment01.png" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TF No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTFNo" runat="server" Text='<%# Eval("TF_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("SUBITEM_DESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Drg./Doc.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRAWING_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rev.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CATEGORY_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Copies">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfCopies" runat="server" Text='<%# Eval("COPIES") %>'></asp:Label>
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
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <%--EDIT SUBITEM START--%>
        <asp:Button ID="btnShowPopupSubItemDetail" runat="server" Style="display: none" />
        <ajax:ModalPopupExtender ID="modalPopupExtenderSubItemDetail" runat="server" TargetControlID="btnShowPopupSubItemDetail"
            PopupControlID="pnlPopupSubItemDetail" CancelControlID="imgBtnCancelSubItemDetail" BackgroundCssClass="modalBackground">
        </ajax:ModalPopupExtender>
        <asp:Panel ID="pnlPopupSubItemDetail" runat="server" BackColor="White" Height="400px" Width="900px"
            Style="display: block">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnCancelSubItemDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                    </td>
                </tr>
            </table>
            <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                <legend style="text-align: center;">Edit Subitem Detail</legend>
                <asp:HiddenField ID="hdLOTTFSubitemID" runat="server" />
                <table style="width: 95%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Description:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="3" runat="server" Width="100%"
                                onblur="return ValidateDescriptionToEdit();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Drg./Doc_No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDrgOrDOCNoToEdit" runat="server" Width="100%" onblur="return ValidateDrgNoToEdit();" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Rev. No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRevNoToEdit" runat="server" Width="100%" onblur="return ValidateRevNoToEdit();" />
                        </td>
                    </tr>
                    <td>&nbsp;
                    </td>
                    <tr>
                        <td>Cat.:
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkLstCategoryTEdit" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                                onblur="return ValidateCategoryToEdit();" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>No. Of Copies:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoOfCopiesToEdit" runat="server" Width="100%"
                                onblur="return ValidateNoOfCopiesToEdit();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                                Width="100%" OnClick="btnUpdate_Click" OnClientClick="return ValidateAddSubitemToEdit();" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>

    </form>
</body>
</html>
