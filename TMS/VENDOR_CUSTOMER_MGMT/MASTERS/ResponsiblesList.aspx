<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ResponsiblesList.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_MASTERS_ResponsiblesList" %>

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

        function ValidateName() {
            var val = document.getElementById('<%=txtShortNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtShortNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtShortNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;
            if (ValidateName()) { return false }

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
                <legend>Responsibles List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Short Name:</label>
                    <asp:TextBox ID="txtShortName" runat="server"
                        CssClass="form-control" />

                    <label>Full Name:</label>
                    <asp:TextBox ID="txtFullName" runat="server"
                        CssClass="form-control" />

                    <label>Tagged Employee:</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server"
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
                ID="gvMastersList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvMastersList_RowCommand"
                OnRowDataBound="gvMastersList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%# Eval("PID") %>' />
                            <asp:Label ID="lblEmployeeRecordId" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_FID") %>' />
                            <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# Eval("NAME") %>' />
                            <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>' />
                            <asp:Label ID="lblIsDeleted" runat="server" Visible="false" Text='<%# Eval("IS_DELETED") %>' />
                            <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("IS_DELETED") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Master Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="NAME" HeaderText="Name" />
                    <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Tagged Employee" />

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
                    <asp:Label ID="lblLegend" runat="server" Text="Master Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Short Name:</label>
                    <asp:TextBox ID="txtShortNameToS" runat="server"
                        CssClass="form-control" />

                    <label>Full Name:</label>
                    <asp:TextBox ID="txtFullNameToS" runat="server"
                        CssClass="form-control" />

                    <label>Tag Employee:</label>
                    <asp:DropDownList ID="ddlEmployeeToS" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Is Active?</label>
                    <asp:CheckBox ID="chkIsActiveToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Save"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnUpdate_Click" Width="100%" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                    <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
