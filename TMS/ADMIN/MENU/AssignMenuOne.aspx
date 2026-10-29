<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AssignMenuOne.aspx.cs" Inherits="ADMIN_MENU_AssignMenuOne" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <%--<link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />--%>

    <link href="../../Styles/menu-assign.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>--%>



    <div class="form-entry-container">

        <fieldset class="form-card">

            <legend>Menu Assignment</legend>

            <!-- Employee Selection -->
            <div class="form-grid form-grid-2">

                <label>Employee Name</label>

                <asp:DropDownList
                    ID="ddlEmployeeName"
                    runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged" />

            </div>

            <!-- Menu Tree -->
            <div class="form-grid form-grid-2">

                <label>Menu Name</label>

                <div class="menu-tree-container">

                    <asp:TreeView
                        ID="tvMenu"
                        runat="server"
                        ImageSet="Arrows"
                        Width="100%"
                        LineImagesFolder="~/TreeLineImages"
                        ShowCheckBoxes="All">

                        <parentnodestyle font-bold="False" />

                        <hovernodestyle
                            font-underline="True"
                            forecolor="#0078d4" />

                        <selectednodestyle
                            font-underline="True"
                            horizontalpadding="0px"
                            verticalpadding="0px"
                            forecolor="#0078d4" />

                        <nodestyle
                            font-names="Segoe UI"
                            font-size="10pt"
                            forecolor="#333333"
                            horizontalpadding="6px"
                            nodespacing="4px"
                            verticalpadding="4px" />

                    </asp:TreeView>

                </div>

            </div>

            <!-- Select All -->
            <div class="form-grid form-grid-2">

                <label>Select/Deselect All</label>

                <asp:CheckBox
                    ID="chkSelectAll"
                    runat="server"
                    AutoPostBack="true"
                    OnCheckedChanged="chkSelectAll_CheckedChanged" />

            </div>

        </fieldset>

        <!-- Save Button -->
        <div class="full-width button-group">

            <asp:Button
                ID="btnSave"
                runat="server"
                Text="Save"
                CssClass="button"
                OnClientClick="return ValidateAll();"
                OnClick="btnSave_Click"
                Width="100%" />

        </div>

        <!-- Message -->
        <div class="full-width">

            <asp:Panel
                ID="pnlMsg"
                Visible="false"
                runat="server"
                Height="50px">

                <asp:Label
                    ID="lblMsg"
                    runat="server"
                    Font-Bold="True" />

            </asp:Panel>

        </div>

    </div>



    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
