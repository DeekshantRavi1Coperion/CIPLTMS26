<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="SaleOrderOutstandingReportTwo.aspx.cs" Inherits="REPORTS_SALE_ORDER_SaleOrderOutstandingReportTwo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script>
    function ValidateMonth() {
            var Month = document.getElementById('<%=ddlMonth.ClientID %>').selectedIndex;
                if (Month == '' || Month == '0') {
                    document.getElementById('<%=ddlMonth.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlMonth.ClientID %>').style.borderColor = "";
                    return false;
                }
        }
        
        function ValidateYear() {
            var Year = document.getElementById('<%=ddlYear.ClientID %>').selectedIndex;
                if (Year== '' || Year== '0') {
                    document.getElementById('<%=ddlYear.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlYear.ClientID %>').style.borderColor = "";
                    return false;
                }
        }
        
        
        function ValidateAll() {
            var check = true;
            if (ValidateMonth()) {return false;}  
            if (ValidateYear()) {return false;}           
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Sale Order Backlog/Forecast Report</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Month:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                            onblur="return ValidateMonth();" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                            <asp:ListItem Text="SELECT" Value="0" />
                            <asp:ListItem Text="JAN" Value="1" />
                            <asp:ListItem Text="FEB" Value="2" />
                            <asp:ListItem Text="MAR" Value="3" />
                            <asp:ListItem Text="APR" Value="4" />
                            <asp:ListItem Text="MAY" Value="5" />
                            <asp:ListItem Text="JUN" Value="6" />
                            <asp:ListItem Text="JUL" Value="7" />
                            <asp:ListItem Text="AUG" Value="8" />
                            <asp:ListItem Text="SEP" Value="9" />
                            <asp:ListItem Text="OCT" Value="10" />
                            <asp:ListItem Text="NOV" Value="11" />
                            <asp:ListItem Text="DEC" Value="12" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Year:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                            onblur="return ValidateYear();" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                            <asp:ListItem Text="SELECT" Value="0" />
                            <asp:ListItem Text="2015" Value="2015" />
                            <asp:ListItem Text="2016" Value="2016" />
                            <asp:ListItem Text="2017" Value="2017" />
                            <asp:ListItem Text="2018" Value="2018" />
                            <asp:ListItem Text="2019" Value="2019" />
                            <asp:ListItem Text="2020" Value="2020" />
                            <asp:ListItem Text="2021" Value="2021" />
                            <asp:ListItem Text="2022" Value="2022" />
                            <asp:ListItem Text="2023" Value="2023" />
                            <asp:ListItem Text="2024" Value="2024" />
                            <asp:ListItem Text="2025" Value="2025" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        JOB No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" on />
                    </td>
                    <td>
                        &nbsp;
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
                <asp:GridView ID="gvSaleOrder" runat="server" AutoGenerateColumns="true" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="20" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvSaleOrder_RowDataBound">
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
