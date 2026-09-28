<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="MonthlySiteReport.aspx.cs" Inherits="ADMIN_MonthlySiteReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>




    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Monthly Site Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Year:</label>
                    <asp:DropDownList ID="ddlYear" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                        <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                        <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                        <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                        <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                        <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                        <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                        <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                        <asp:ListItem Text="2030" Value="2030"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:CheckBox ID="chkEnableMonth" runat="server" Text="Month:" Font-Bold="true"
                        AutoPostBack="true" OnCheckedChanged="chkEnableMonth_CheckedChanged" Style="margin-right: 5px;" />

                    <asp:DropDownList ID="ddlMonth" runat="server" Enabled="false" 
                        CssClass="form-control">
                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" runat="server" Text="Search"
                    OnClick="btnSearch_Click"
                    CssClass="button"
                    Width="100%"/>

                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"
                    CssClass="button"
                    Width="100%"/>
            </div>
        </div>
        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView ID="gvLessonLearntList"
                CssClass="employee-grid"
                runat="server" AutoGenerateColumns="False"
                CellPadding="8" ForeColor="#333333" GridLines="Both" PageSize="15" Width="300px"
                HorizontalAlign="Center" OnRowDataBound="gvLessonLearntList_RowDataBound" AllowPaging="True"
                OnPageIndexChanging="gvLessonLearntList_PageIndexChanging" ShowFooter="True">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" Height="35px" />

                <Columns>
                    <asp:BoundField DataField="Month" HeaderText="Month" />
                    <asp:BoundField DataField="Total" HeaderText="Total Working Hours" />
                </Columns>

                <FooterStyle BackColor="#bfbfbf" ForeColor="Black" Font-Bold="True" HorizontalAlign="Center" Height="35px" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#bfbfbf" Font-Bold="True" ForeColor="Black" Height="40px" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" Height="35px" />
            </asp:GridView>
        </div>

    </div>
</asp:Content>
