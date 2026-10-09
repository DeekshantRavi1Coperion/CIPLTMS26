<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="WorkerList.aspx.cs" Inherits="ADMIN_WORKER_WorkerList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">

        function ValidateEmployeeName() {
            var EmployeeName = document.getElementById('<%=txtEmployeeName.ClientID %>').value;
            if (EmployeeName == '') {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmployeeID() {
            var EmployeeID = document.getElementById('<%=txtEmployeeID.ClientID %>').value;
            if (EmployeeID == '') {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDesignation() {
            var Designation = document.getElementById('<%=txtDesignation.ClientID %>').value;
            if (Designation == '') {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var check = true;

            if (ValidateEmployeeName()) {
                return false
            }

            if (ValidateEmployeeID()) {
                return false
            }

            if (ValidateDesignation()) {
                return false
            }

            if (ValidateDepartment()) {
                return false
            }

            if (ValidateUnit()) {
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

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnitSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Worker Name</label>
                    <asp:TextBox ID="txtEmployeeNameSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Worker ID</label>
                    <asp:TextBox ID="txtEmployeeIDSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartmentSearch"
                        runat="server"
                        CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch"
                    OnClick="btnSearch_Click"
                    runat="server"
                    Text="Search"
                    CssClass="button" />

                <asp:Button ID="btnAddNe"
                    OnClick="btnAddNew_Click"
                    runat="server"
                    Text="Add New"
                    CssClass="button" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Workers:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:GridView ID="gvEmployeeList"
                    CssClass="employee-grid"
                    runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvEmployeeList_RowCommand"
                    OnRowDataBound="gvEmployeeList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EIDT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                <asp:Label ID="lblIsActive" runat="server" Visible="false" Text='<%# Eval("IS_ACTIVE") %>' />
                                <asp:ImageButton ID="imgProperties" ToolTip="Edit Employee Details...!!" CommandArgument="PROPERTIES"
                                    runat="server" ImageUrl="~/Images/royal_search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IS_ACTIVE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgIsActive" CommandArgument="IS_ACTIVE"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="WORKER_NAME" />
                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                        <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT_NAME" />
                        <asp:BoundField DataField="TEAM_LEADER" HeaderText="TEAM_LEADER" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT_NAME" />
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

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="250px" Width="900px"
        Style="display: block; padding: 50px; border-radius: 5px;">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">

            <fieldset class="form-card">
                <legend>Worker Details</legend>

                <div align="center">
                    <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <div class="form-grid form-grid-2">

                    <label>Worker Name</label>
                    <asp:TextBox ID="txtEmployeeName"
                        runat="server"
                        CssClass="form-control" />

                    <label>Worker ID</label>
                    <asp:TextBox ID="txtEmployeeID"
                        runat="server"
                        CssClass="form-control" />

                    <label>Designation</label>
                    <asp:TextBox ID="txtDesignation"
                        runat="server"
                        CssClass="form-control" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartment"
                        runat="server"
                        CssClass="form-control" />

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit"
                        runat="server"
                        CssClass="form-control" />

                    <label>Leader</label>
                    <asp:DropDownList ID="ddlTeamLeader"
                        runat="server"
                        CssClass="form-control" />

                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnUpdate"
                    runat="server"
                    Text="Save"
                    CssClass="button"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnUpdate_Click" Width="100%" />
            </div>
        </div>

    </asp:Panel>

    <asp:Button ID="btnShowTeamMembers" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnShowTeamMembers"
        PopupControlID="pnlShowTeamMembers" CancelControlID="imgBtnCancelShowTeamMembers" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlShowTeamMembers" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelShowTeamMembers" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 23px; margin-top: 10px;">
            <legend style="text-align: center;">Deactive Worker
            </legend>
            <div style='overflow: auto; width: 99%; height: 60px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>New Team Leader:
                        </td>
                        <td>

                            <asp:Label ID="lblCurrentTeamLeaderID" runat="server" Visible="false" />
                            <asp:DropDownList ID="ddlNewTeamleader" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateNewTeamleader();">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Employee ID:
                        </td>
                        <td>
                            <asp:Button ID="btnDeactivate" CssClass="button" Width="100%" runat="server" Text="Deactive"
                                OnClientClick="return ValidateAllNew();" OnClick="btnDeactivate_Click" />
                        </td>
                    </tr>
                </table>

                <br />

                <div align="center">
                    <asp:Panel ID="pnlDeactiveMsg" Visible="false" runat="server">
                        <asp:Label ID="lblDeactiveMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <div align="center">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblTeamMemberListRecords" runat="server" Text="Records[0]" /></legend>
                        <div style='overflow: auto; width: 100%; height: 100%; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvTeamMemberList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EMPLOYEE_NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                            <asp:Label ID="lblEmployeeName" runat="server" Visible="true" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                                    <asp:BoundField DataField="TEAM_LEADER" HeaderText="TEAM_LEADER" />
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

            </div>
        </fieldset>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
