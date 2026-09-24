<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AssignMenu.aspx.cs" Inherits="ADMIN_MENU_AssignMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
<link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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
    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 40%; margin-top: 100px;">
                    <legend style="text-align: center;">Assign Menu</legend>
                    <table width="80%">
                        <tr>
                            <td colspan="1">
                                <p>
                                    Employee Name:</p>
                                <p>
                                    <asp:DropDownList ID="ddlEmployeeName" runat="server" Width="100%" Height="26px"
                                        onblur="return ValidateEmployeeName();" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 45%;">
                                <p>
                                    Menu Name:
                                </p>
                                <div style='overflow: auto; width: 100%; height: 250px; border: 1px solid lightgray;'>
                                    <p>
                                        <asp:TreeView ID="tvMenu" runat="server" ImageSet="Arrows" Width="100%" Height="100%"
                                            LineImagesFolder="~/TreeLineImages" ShowCheckBoxes="All">
                                            <%--OnTreeNodeCheckChanged="tvMenu_TreeNodeCheckChanged"
                                            onclick="javascript:postBackByObject()"--%>
                                            <ParentNodeStyle Font-Bold="False" />
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                                ForeColor="#5555DD" />
                                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                                NodeSpacing="0px" VerticalPadding="0px" />
                                        </asp:TreeView>
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnAssign" CssClass="button" runat="server" Width="100%" Text="Assign"
                                                OnClick="btnAssign_Click" OnClientClick="return ValidateAll();" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnUnAssign" CssClass="button" runat="server" Width="100%" Text="Unassign"
                                                OnClick="btnUnAssign_Click" OnClientClick="return ValidateAll();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
