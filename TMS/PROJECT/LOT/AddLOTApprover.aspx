<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddLOTApprover.aspx.cs" Inherits="PROJECT_LOT_AddLOTApprover" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


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

        function ValidateJobNo() {
            var JOBNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProjectManager() {
            var ProjectManager = document.getElementById('<%=ddlProjectManager.ClientID %>').selectedIndex;
            if (ProjectManager == '' || ProjectManager == 0) {
                document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProjectEngineer() {
            var ProjectEngineer = document.getElementById('<%=ddlProjectEngineer.ClientID %>').selectedIndex;
            if (ProjectEngineer == '' || ProjectEngineer == 0) {
                document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            if (ValidateProjectManager()) {
                check = false;
            }

            if (ValidateProjectEngineer()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to add approvers?")) {
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
    <asp:HiddenField ID="hdConfirmValue" runat="server" />

    <div class="form-entry-container">
        <fieldset class="form-card">

            <legend>Add LOT Project Approvers</legend>

            <div class="form-grid form-grid-2">

                <label>Company</label>
                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />

                <label>JOB Number</label>
                <table width="100%">
                    <tr>
                        <td style="width: 90%">
                            <asp:TextBox ID="txtJOBNo" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>

                        <td style="width: 10%">
                            <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                OnClick="btnGetJOBNo_Click" />
                        </td>
                    </tr>
                </table>

                <label>Project Manager</label>
                <asp:DropDownList ID="ddlProjectManager" runat="server" Width="100%" Height="26px"
                    onblur="return ValidateProjectManager();" />

                <label>Project Engineer</label>
                <asp:DropDownList ID="ddlProjectEngineer" runat="server" Width="100%" Height="26px"
                    onblur="return ValidateProjectEngineer();" />

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />

            <asp:Button ID="btnApproverList" runat="server" Width="100%" Text="Approver List" CssClass="button"
                OnClick="btnApproverList_Click" />

        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="100%">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>

        </div>
    </div>


    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="modalPopupExtenderJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>LOT Report For Factory:
                    <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server"
                            CssClass="form-control" />

                        <label>PO No.</label>
                        <asp:TextBox ID="txtPONoSearch" runat="server"
                            CssClass="form-control" />

                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server"
                            CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchJOBNo_Click" />
                </div>
            </div>

            <div class="popup-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No."
                                    runat="server" Text="Get JOB No." CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
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

    </asp:Panel>
    <%--JOB DETAIL END--%>
</asp:Content>
