<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="JobCosting.aspx.cs" Inherits="JCOSTING_JobCosting" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>

    <div align="center" style="margin-top: 20px;">

        <fieldset style="width: 90%">
            <legend style="text-align: center;">Job Costing Report</legend>
            <table width="100%">
                <tr>

                    <td align="right">Job Number:
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJobNo" runat="server" Width="80%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" align="left" CssClass="button" Width="70%" runat="server" Text="Search" />
                    </td>

                    <td>
                        <asp:Button ID="btnExport" align="left" CssClass="button" Width="70%" runat="server" Text="Export" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Sale Order Cost</legend>
            <table width="80%">
                <tr width="50%">
                    <td>&nbsp;</td>
                    <td>Amount :</td>
                    <td align="left">
                        <asp:Label ID="lblTotalAmountINR" runat="server" Text="0" Font-Bold="true" Font-Size="Large" ForeColor="Green"></asp:Label></td>

                    <td>&nbsp;</td>

                    <td>In Cr:</td>
                    <td align="left">
                        <asp:Label ID="lblTotalCostAsPerOrder" runat="server" Text="0" Font-Bold="true" Font-Size="Large" ForeColor="Green"></asp:Label></td>

                    <td>&nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>

    <br />
    <br />
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <div align="center">

        <fieldset style="width: 90%;">

            <legend style="text-align: center;">
                <span class="inline-title">
                    <h2 style="display: inline;">Cost Incurred - </h2>
                </span>
                <asp:Label ID="lblRecords" CssClass="inline-label" runat="server" Text="Records[0]" /></legend>

            <div style='overflow: auto; width: 100%; height: 350px; border: 1px solid lightgray;'>

                <asp:GridView ID="gvCostIncurred" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>
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
    <br /><br />
      <div align="center">

        <fieldset style="width: 90%;">

            <legend style="text-align: center;">
                <span class="inline-title">
                    <h2 style="display: inline;">Outstanding Cost Incurred - </h2>
                </span>
                <asp:Label ID="Label1" CssClass="inline-label" runat="server" Text="Records[0]" /></legend>

            <div style='overflow: auto; width: 100%; height: 350px; border: 1px solid lightgray;'>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>
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
    <br /><br />
      <div align="center">

        <fieldset style="width: 90%;">

            <legend style="text-align: center;">
                <span class="inline-title">
                    <h2 style="display: inline;">Summary </h2>
                </span>
                <asp:Label ID="Label2" CssClass="inline-label" runat="server" Text="" /></legend>

            <div style='overflow: auto; width: 100%; height: 250px; border: 1px solid lightgray;'>

                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                    <Columns>
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
            <br />
            <br />

        </fieldset>
    </div>
</asp:Content>
