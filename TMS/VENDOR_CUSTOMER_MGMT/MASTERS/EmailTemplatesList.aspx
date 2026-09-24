<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="EmailTemplatesList.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_EmailTemplatesList" %>

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

        function ValidateEntityType() {
            var val = document.getElementById('<%=ddlEntityTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlEntityTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEntityTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateStatus() {
            var val = document.getElementById('<%=ddlStatusToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlStatusToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStatusToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateSubject() {
            var val = document.getElementById('<%=txtSubjectToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtSubjectToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSubjectToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSalutation() {
            var val = document.getElementById('<%=txtSalutationToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtSalutationToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSalutationToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBodyLine() {
            var val = document.getElementById('<%=txtBodyLineToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBodyLineToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBodyLineToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateClosingLine() {
            var val = document.getElementById('<%=txtClosingLineToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtClosingLineToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtClosingLineToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;
            if (ValidateEntityType()) { return false }
            if (ValidateStatus()) { return false }
            if (ValidateSubject()) { return false }
            if (ValidateSalutation()) { return false }
            if (ValidateBodyLine()) { return false }
            if (ValidateClosingLine()) { return false }

            return check;
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
                <legend>Email Templates List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Entity Type:</label>
                    <asp:DropDownList ID="ddlEntityTypeS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Staus:</label>
                    <asp:DropDownList ID="ddlStatusS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

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
                ID="gvEmailTemplatesList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvEmailTemplatesList_RowCommand"
                OnRowDataBound="gvEmailTemplatesList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />
                            <asp:Label ID="lblEntityTypeId" runat="server" Visible="false" Text='<%# Eval("ENTITY_TYPE_FID") %>' />
                            <asp:Label ID="lblStatusId" runat="server" Visible="false" Text='<%# Eval("STATUS_FID") %>' />
                            <asp:Label ID="lblSubject" runat="server" Visible="false" Text='<%# Eval("SUBJECT") %>' />
                            <asp:Label ID="lblSalutation" runat="server" Visible="false" Text='<%# Eval("SALUTATION") %>' />
                            <asp:Label ID="lblBodyLine" runat="server" Visible="false" Text='<%# Eval("BODY_LINE") %>' />
                            <asp:Label ID="lblClosingLine" runat="server" Visible="false" Text='<%# Eval("CLOSING_LINE") %>' />
                            <asp:Label ID="lblLink" runat="server" Visible="false" Text='<%# Eval("LINK") %>' />
                            <asp:Label ID="lblCC" runat="server" Visible="false" Text='<%# Eval("CC") %>' />
                            <asp:Label ID="lblBCC" runat="server" Visible="false" Text='<%# Eval("BCC") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Email Template...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ENTITY_TYPE" HeaderText="Entity Type" />
                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                    <asp:BoundField DataField="SUBJECT" HeaderText="Subject" />
                    <asp:BoundField DataField="SALUTATION" HeaderText="Salutation" />
                    <asp:BoundField DataField="BODY_LINE" HeaderText="Body Line" />
                    <asp:BoundField DataField="CLOSING_LINE" HeaderText="Closing Line" />
                    <asp:BoundField DataField="LINK" HeaderText="Link" />
                    <asp:BoundField DataField="CC" HeaderText="CC" />
                    <asp:BoundField DataField="BCC" HeaderText="BCC" />

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
                    <asp:Label ID="lblLegend" runat="server" Text="Email Template Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Entity Type:</label>
                    <asp:DropDownList ID="ddlEntityTypeToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Staus:</label>
                    <asp:DropDownList ID="ddlStatusToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Subject:</label>
                    <asp:TextBox ID="txtSubjectToS" runat="server"
                        CssClass="form-control" />

                    <label>Salutation:</label>
                    <asp:TextBox ID="txtSalutationToS" runat="server"
                        CssClass="form-control" />

                    <label>Body Line:</label>
                    <asp:TextBox ID="txtBodyLineToS" runat="server"
                        CssClass="form-control" />

                    <label>Closing Line:</label>
                    <asp:TextBox ID="txtClosingLineToS" runat="server"
                        CssClass="form-control" />

                    <label>Link:</label>
                    <asp:TextBox ID="txtLinkToS" runat="server"
                        CssClass="form-control" />

                    <label>CC:</label>
                    <asp:TextBox ID="txtCCToS" runat="server"
                        CssClass="form-control" />

                    <label>BCC:</label>
                    <asp:TextBox ID="txtBCCToS" runat="server"
                        CssClass="form-control" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Save"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnUpdate_Click" Width="100%" />
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
