<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="EnggHoursReportAllDepts1.aspx.cs"
    Inherits="REPORTS_EnggHoursReportAllDepts1" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div align="center" style="margin-top: 100px;">
        <fieldset style="width: 50%">
            <legend style="text-align: center;">Timesheet Report All Depts</legend>
            <table width="100%">
                <tr>
                    <td align="right">Month:</td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Value="1" Text="JAN" />
                            <asp:ListItem Value="2" Text="FEB" />
                            <asp:ListItem Value="3" Text="MAR" />
                            <asp:ListItem Value="4" Text="APR" />
                            <asp:ListItem Value="5" Text="MAY" />
                            <asp:ListItem Value="6" Text="JUN" />
                            <asp:ListItem Value="7" Text="JUL" />
                            <asp:ListItem Value="8" Text="AUG" />
                            <asp:ListItem Value="9" Text="SEP" />
                            <asp:ListItem Value="10" Text="OCT" />
                            <asp:ListItem Value="11" Text="NOV" />
                            <asp:ListItem Value="12" Text="DEC" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td align="right">Year:</td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Value="2018" Text="2018" />
                            <asp:ListItem Value="2019" Text="2019" />
                            <asp:ListItem Value="2020" Text="2020" />
                            <asp:ListItem Value="2021" Text="2021" />
                            <asp:ListItem Value="2022" Text="2022" />
                            <asp:ListItem Value="2023" Text="2023" />
                            <asp:ListItem Value="2024" Text="2024" />
                            <asp:ListItem Value="2025" Text="2025" />
                            <asp:ListItem Value="2026" Text="2026" />
                            <asp:ListItem Value="2027" Text="2027" />
                            <asp:ListItem Value="2028" Text="2028" />
                            <asp:ListItem Value="2029" Text="2029" />
                            <asp:ListItem Value="2030" Text="2030" />
                            <asp:ListItem Value="2031" Text="2031" />
                            <asp:ListItem Value="2032" Text="2032" />
                            <asp:ListItem Value="2033" Text="2033" />
                            <asp:ListItem Value="2034" Text="2034" />
                            <asp:ListItem Value="2035" Text="2035" />
                            <asp:ListItem Value="2036" Text="2036" />
                            <asp:ListItem Value="2037" Text="2037" />
                            <asp:ListItem Value="2038" Text="2038" />
                            <asp:ListItem Value="2039" Text="2039" />
                            <asp:ListItem Value="2040" Text="2040" />

                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>
                    <td align="right">Department:</td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%" Height="26px">                            
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" on />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />
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
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvEnggHoursReport" runat="server" AutoGenerateColumns="true" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="20" Width="100%" HorizontalAlign="Center"
                    AllowPaging="True" OnRowDataBound="gvEnggHoursReport_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
