<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ApproversList.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_ApproversList" %>

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

        function ValidateApproverType() {
            var val = document.getElementById('<%=ddlApproverTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlApproverTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlApproverTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateApprover() {
            var val = document.getElementById('<%=ddlApproverToS.ClientID %>').selectedIndex;
            if (val == '' || val == '0') {
                document.getElementById('<%=ddlApproverToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlApproverToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var check = true;


            if (ValidateEntityType()) {
                return false
            }

            if (ValidateApproverType()) {
                return false
            }

            if (ValidateApprover()) {
                return false
            }

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
                <legend>Approvers List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Entity Type:</label>
                    <asp:DropDownList ID="ddlEntityType" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Approver Type:</label>
                    <asp:DropDownList ID="ddlApproverType" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Approver:</label>
                    <asp:DropDownList ID="ddlApprover" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click"
                    Width="100%" />

                <asp:Button ID="btnAddNew" CssClass="button" runat="server" Text="Add New" OnClick="btnAddNew_Click"
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
                ID="gvEmployeeList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvEmployeeList_RowCommand"
                OnRowDataBound="gvEmployeeList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />
                            <asp:Label ID="lblEntityTypeFID" runat="server" Visible="false" Text='<%# Eval("ENTITY_TYPE_FID") %>' />
                            <asp:Label ID="lblApproverTypeFID" runat="server" Visible="false" Text='<%# Eval("APPROVER_TYPE_FID") %>' />
                            <asp:Label ID="lblApproverID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_FID") %>' />
                            <asp:Label ID="lblIsDeleted" runat="server" Visible="false" Text='<%# Eval("IS_DELETED") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Approver Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ENTITY_TYPE" HeaderText="Entity Type" />
                    <asp:BoundField DataField="APPROVER_TYPE" HeaderText="Approver Type" />
                    <asp:BoundField DataField="APPROVER" HeaderText="Approver" />
                    <asp:BoundField DataField="APPROVER_EMAIL_ID" HeaderText="Email" />


                    <asp:TemplateField HeaderText="Is Active" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsActive" runat="server" Height="20px" Width="20px" Enabled="false" />
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

    </div>



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" CssClass="popup-edit">
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
                    <asp:Label ID="lblLegend" runat="server" Text="Approver Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Entity Type:</label>
                    <asp:DropDownList ID="ddlEntityTypeToS" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Approver Type:</label>
                    <asp:DropDownList ID="ddlApproverTypeToS" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Approver:</label>
                    <asp:DropDownList ID="ddlApproverToS" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Is Active?</label>
                    <asp:CheckBox ID="chkIsActiveToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" Width="100%" runat="server" Text="Update"
                    OnClientClick="return ValidateAll();" OnClick="btnUpdate_Click" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                    <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
