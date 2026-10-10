<%@ Page Title="CIPLTMS - LOT Main Subitem Managers List" Language="C#" 
    MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTMainSubitemManagersList.aspx.cs" 
    Inherits="PROJECT_LOT_LOTMainSubitemManagersList" %>

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

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartmentToEdit.ClientID %>').selectedIndex;
            if (Department == '' || Department == 0) {
                document.getElementById('<%=ddlDepartmentToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartmentToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLOTMainSubitems() {
            var LOTMainSubitem = document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').selectedIndex;
            if (LOTMainSubitem == '' || LOTMainSubitem == 0) {
                document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLOTMainSubitemsToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateManager() {
            var Manager = document.getElementById('<%=ddlManagerToEdit.ClientID %>').selectedIndex;
            if (Manager == '' || Manager == 0) {
                document.getElementById('<%=ddlManagerToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlManagerToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;


            if (ValidateDepartment()) {
                check = false;
            }

            if (ValidateLOTMainSubitems()) {
                check = false;
            }

            if (ValidateManager()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to update manager?")) {
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

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters:</legend>

                <div class="form-filter-grid">

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server"
                        CssClass="form-control" />

                    <label>LOT For</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlLOTMainSubitems" runat="server"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Manager</label>
                    <asp:DropDownList ID="ddlManager" runat="server"
                        CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnAddNewManager" CssClass="button" Width="100%" runat="server"
                    Text="Add New" OnClick="btnAddNewManager_Click" />

            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>LOT Report For Factory:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvLOTMainSubitemManagers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    AllowPaging="false" OnRowCommand="gvLOTMainSubitemManagers_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server"
                                    ImageUrl="~/Images/LOT/edit5.png" Width="35px" Height="35px" ToolTip="Edit" />
                                <asp:Label ID="lblManagerRecordID" runat="server" Visible="false" Text='<%# Eval("MANAGER_RECORD_ID") %>' />
                                <asp:Label ID="lblCompanyID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                <asp:Label ID="lblLOTMainItemID" runat="server" Visible="false" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' />
                                <asp:Label ID="lblLOTMainSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' />
                                <asp:Label ID="lblDepartmentID" runat="server" Visible="false" Text='<%# Eval("DEPARTMENT_ID") %>' />
                                <asp:Label ID="lblManagerID" runat="server" Visible="false" Text='<%# Eval("MANAGER_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="Company" />
                        <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Department" />
                        <asp:BoundField DataField="MANAGER_NAME" HeaderText="Manager" />
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



    <%-- UPDATE MAIN SUBITEM MANAGER START--%>
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

                <legend>LOT Main Subitem Manager Detail</legend>

                <div class="form-grid form-grid-2">

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompanyToEdit" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyToEdit_SelectedIndexChanged" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartmentToEdit" runat="server"
                        CssClass="form-control" />

                    <label>LOT For</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlLOTMainItemsToEdit" runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 50%">
                                <asp:DropDownList ID="ddlLOTMainSubitemsToEdit" runat="server"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Manager</label>
                    <asp:DropDownList ID="ddlManagerToEdit" runat="server" Width="100%" Height="26px" onblur="return ValidateManager();" />




                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
            </div>

        </div>

    </asp:Panel>
    <%-- UPDATE MAIN SUBITEM MANAGER END --%>


    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
