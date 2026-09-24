<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ProjectList.aspx.cs" Inherits="PROJECT_ProjectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 80%">
                    <legend style="text-align: center;">Project List</legend>
                    <table width="60%">
                        <tr>
                            <td align="right">
                                JOB No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                Customer Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Status.:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                        <td style="width: 10%;">
                                        </td>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnAddNewProject" CssClass="button" Width="100%" runat="server" Text="Add New"
                                                OnClick="btnAddNewProject_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <br />
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <br />
            <div align="center">
                <fieldset style="width: 90%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
                    <asp:GridView ID="gvProjectList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Vertical" PageSize="15" Width="100%" HorizontalAlign="Center"
                        AllowPaging="True" OnPageIndexChanging="gvProjectList_PageIndexChanging" OnRowCommand="gvProjectList_RowCommand"
                        OnRowDataBound="gvProjectList_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="PROJECT_ID" HeaderText="PROJECT_ID" Visible="False" />
                            <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                            <asp:TemplateField HeaderText="STATUS">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectID" runat="server" Visible="false" Text='<%# Eval("PROJECT_ID") %>' />
                                    <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                    <asp:Label ID="lblCustCode" runat="server" Visible="false" Text='<%# Eval("CUST_CODE") %>' />
                                    <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                    <asp:Label ID="lblBudgetedHours" runat="server" Visible="false" Text='<%# Eval("BUDGETED_HOURS") %>' />
                                    <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                                    <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("REMARKS") %>' />
                                    <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                                    <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                            <asp:BoundField DataField="BUDGETED_HOURS" HeaderText="BUDGETED_HOURS" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="CREATED_BY" />
                            <asp:BoundField DataField="CREATED_ON" HeaderText="CREATED_ON" />
                            <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                    <asp:Label ID="lblTourID" runat="server" Visible="false" Text='<%# Eval("PROJECT_ID") %>' />
                                    <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ADJUST HOURS" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnAdjust" CommandArgument="ADJUST" ToolTip="Adjust Buddeted Hours"
                                        runat="server" Text="Adjust" CssClass="cancelbutton" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
