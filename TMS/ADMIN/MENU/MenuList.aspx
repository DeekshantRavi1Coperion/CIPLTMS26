<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="MenuList.aspx.cs" Inherits="ADMIN_MENU_MenuList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>


    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>


            <div class="page-layout">
                <div class="form-grid-container">
                    <fieldset class="filter-card">
                        <legend>Filters:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

                        <div class="form-grid form-grid-3">
                            <label>Menu Name</label>
                            <asp:TextBox ID="txtMenuName"
                                runat="server"
                                CssClass="form-control" />


                            <label>Parent Menu</label>
                            <asp:DropDownList ID="ddlParentMenu"
                                runat="server"
                                CssClass="form-control" />

                            &nbsp;
                            &nbsp;
                         <asp:Button ID="btnSearch"
                             OnClick="btnSearch_Click"
                             runat="server"
                             Text="Search"
                             CssClass="button" />

                            &nbsp;
                         <asp:Button ID="btnAddNew"
                             OnClick="btnAddNew_Click"
                             runat="server"
                             Text="Add New"
                             CssClass="button" />
                        </div>

                    </fieldset>
                </div>

                <div class="employee-grid-container">
                    <div align="center">
                        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                        </asp:Panel>
                    </div>

                    <asp:GridView ID="gvMenuList"
                        CssClass="employee-grid"
                        runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowCommand="gvMenuList_RowCommand"
                            OnRowDataBound="gvMenuList_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="PROPERTIES">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMenuID" runat="server" Visible="false" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                        <asp:Label ID="lblMenuName" runat="server" Visible="false" Text='<%# Eval("MENU_NAME") %>'></asp:Label>
                                        <asp:Label ID="lblURL" runat="server" Visible="false" Text='<%# Eval("URL") %>'></asp:Label>
                                        <asp:Label ID="lblParentMenuID" runat="server" Visible="false" Text='<%# Eval("PARENT_ID") %>'></asp:Label>
                                        <asp:Label ID="lblSerialNo" runat="server" Visible="false" Text='<%# Eval("SERIAL_NO") %>'></asp:Label>
                                        <asp:ImageButton ID="imgProperties" ToolTip="Edit Tour Information" CommandArgument="PROPERTIES"
                                            runat="server" ImageUrl="~/Images/royal_search.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="MENU_ID" HeaderText="TOUR_ID" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="MENU_NAME" HeaderText="MENU_NAME"></asp:BoundField>
                                <asp:BoundField DataField="URL" HeaderText="URL"></asp:BoundField>
                                <asp:BoundField DataField="PARENT_MENU" HeaderText="PARENT_MENU"></asp:BoundField>
                                <asp:BoundField DataField="SERIAL_NO" HeaderText="SERIAL_NO"></asp:BoundField>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
