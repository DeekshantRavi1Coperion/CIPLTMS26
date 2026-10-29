<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AssignMenu.aspx.cs" Inherits="ADMIN_MENU_AssignMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link href="../../Styles/menu-assign.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }

    </script>

    <script type="text/javascript">
        function ValidateEmployeeName() {
            var EmployeeName = document.getElementById('<%=ddlEmployeeName.ClientID %>').selectedIndex;
            if (EmployeeName == '' || EmployeeName == '0') {
                document.getElementById('<%=ddlEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployeeName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            if (ValidateEmployeeName()) {
                return false
            }

            return true;
        }

    </script>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder2"
    runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>

            <div class="assignment-container">

                <div class="assignment-card">

                    <!-- HEADER -->
                    <div class="assignment-header">
                        <h2>Menu Assignment</h2>
                        <p>Manage employee menu permissions</p>
                    </div>

                    <!-- EMPLOYEE -->
                    <div class="assignment-employee">

                        <label>Employee Name</label>

                        <asp:DropDownList
                            ID="ddlEmployeeName"
                            runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            onblur="return ValidateEmployeeName();"
                            OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <!-- MAIN CONTENT -->
                    <div class="assignment-layout">

                        <!-- LEFT TREE -->
                        <div class="assignment-tree-card">

                            <div class="assignment-section-title">
                                Available Menus
                            </div>

                            <div class="assignment-tree-scroll">

                                <asp:TreeView
                                    ID="tvMenu"
                                    runat="server"
                                    ImageSet="Arrows"
                                    Width="100%"
                                    LineImagesFolder="~/TreeLineImages"
                                    ShowCheckBoxes="All">

                                    <ParentNodeStyle Font-Bold="False" />

                                    <HoverNodeStyle
                                        Font-Underline="True"
                                        ForeColor="#0078d4" />

                                    <SelectedNodeStyle
                                        Font-Underline="True"
                                        ForeColor="#0078d4" />

                                    <NodeStyle
                                        Font-Names="Segoe UI"
                                        Font-Size="10pt"
                                        ForeColor="#333333"
                                        HorizontalPadding="6px"
                                        NodeSpacing="5px"
                                        VerticalPadding="4px" />

                                </asp:TreeView>

                            </div>

                        </div>

                        <!-- RIGHT ACTIONS -->
                        <div class="assignment-action-card">

                            <div class="assignment-section-title">
                                Manage Access
                            </div>

                            <p>
                                Select the required menu permissions
                                from the tree and choose an action.
                            </p>

                            <div class="assignment-buttons">

                                <asp:Button
                                    ID="btnAssign"
                                    runat="server"
                                    Text="Assign Selected"
                                    CssClass="button assignment-btn-assign"
                                    OnClick="btnAssign_Click"
                                    OnClientClick="return ValidateAll();" />

                                <asp:Button
                                    ID="btnUnAssign"
                                    runat="server"
                                    Text="Unassign Selected"
                                    CssClass="button assignment-btn-unassign"
                                    OnClick="btnUnAssign_Click"
                                    OnClientClick="return ValidateAll();" />

                            </div>

                            <asp:Panel
                                ID="pnlMsg"
                                runat="server"
                                Visible="false"
                                CssClass="assignment-message">

                                <asp:Label
                                    ID="lblMsg"
                                    runat="server"
                                    Font-Bold="True" />

                            </asp:Panel>

                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

