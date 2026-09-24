<%@ Page Title="CIPLTMS- LOT Approver List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTMainItemList.aspx.cs" Inherits="PROJECT_LOT_LOTMainItemList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">

        function ValidateMainItem() {
            var MainItem = document.getElementById('<%=txtMainItemToEdit.ClientID %>').value;
            if (MainItem == '') {
                document.getElementById('<%=txtMainItemToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMainItemToEdit.ClientID %>').style.borderColor = "";
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
                if (confirm("Would you like to update main item?")) {
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

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdUpdationFlag" runat="server" />
    <asp:HiddenField ID="hdProductionManagerIDFlag" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>LOT Main Item List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Main Item.</label>
                    <asp:TextBox ID="txtMainItem" runat="server"
                        CssClass="form-control" />
                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnAddNewMainItem" CssClass="button" Width="100%" runat="server"
                    Text="Add New" OnClick="btnAddNewMainItem_Click" />

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
                ID="gvLOTMainItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                AllowPaging="True" OnRowCommand="gvLOTMainItem_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server"
                                ImageUrl="~/Images/LOT/edit5.png" Width="35px" Height="35px" ToolTip="Edit" />
                            <asp:Label ID="lblMainItemID" runat="server" Visible="false" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' />
                            <asp:Label ID="lblMainItem" runat="server" Visible="false" Text='<%# Eval("LOT_MAIN_ITEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="Main Item" />
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





    <%-- UPDATE MAIN ITEM START--%>
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

                <legend>Main Item Detail</legend>

                <div class="form-grid form-grid-2">

                    <label>Main Item:</label>
                    <asp:TextBox ID="txtMainItemToEdit" runat="server"
                        CssClass="form-control" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
            </div>
            
        </div>

    </asp:Panel>
    <%-- UPDATE MAIN ITEM END --%>


    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
