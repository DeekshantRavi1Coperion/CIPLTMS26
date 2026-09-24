<%@ Page Title="GST Reconciliation  Report By Books B2B" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="GSTR2BToFACTGSTINLookupReport.aspx.cs" Inherits="REPORTS_PURCHASE_BALANCE_GSTR2BToFACTGSTINLookupReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../Images/Icons/Icon04.png" />

    <script src="../../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" />
    <%--<link href="../../../Styles/Site.css" rel="stylesheet" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">
        function ClearAllFilters() {

            document.getElementById('<%=txtGSTINToS.ClientID %>').value = "";
            document.getElementById('<%=txtPANToS.ClientID %>').value = "";
            document.getElementById('<%=txtVendorCodeToS.ClientID %>').value = "";
            document.getElementById('<%=txtVendorNameToS.ClientID %>').value = "";

            return false;
        }

    </script>


    <style type="text/css">
        .myGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .myGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .myGrid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .myGrid .alt {
                background-color: #EFEFEF;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" Value="0" />
    <%-- <asp:UpdatePanel ID="uppanel" runat="server">
        <contenttemplate>--%>


    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>GSTR2B To FACT GSTIN Lookup Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>GSTIN:</label>
                    <asp:TextBox ID="txtGSTINToS" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>PAN:</label>
                    <asp:TextBox ID="txtPANToS" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <div class="full-width">
                        <label>Vendor:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="txtVendorCodeToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendorNameToS" runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="full-width">
                        <label>Status</label>
                        <asp:RadioButtonList runat="server" ID="rdStatus" RepeatDirection="Vertical">
                            <asp:ListItem Text="All" Selected="True" Value="" />
                            <asp:ListItem Text="Gstin And Pan Not Found In Fact" Value="GSTIN_AND_PAN_NOT_FOUND_IN_FACT" />
                            <asp:ListItem Text="Gstin Not Found In Fact" Value="GSTIN_NOT_FOUND_IN_FACT" />
                            <asp:ListItem Text="Pan Not Found In Fact" Value="PAN_NOT_FOUND_IN_FACT" />
                            <asp:ListItem Text="Both Gstin And Pan Mismatch" Value="BOTH_GSTIN_AND_PAN_MISMATCH" />
                            <asp:ListItem Text="Pan Match But Gstin Mismatch" Value="PAN_MATCH_BUT_GSTIN_MISMATCH" />
                            <asp:ListItem Text="Gstin Match But Pan Mismatch" Value="GSTIN_MATCH_BUT_PAN_MISMATCH" />
                            <asp:ListItem Text="Both Gstin And Pan Match" Value="BOTH_GSTIN_AND_PAN_MATCH" />
                        </asp:RadioButtonList>
                    </div>

                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                    Text="Search"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export to Excel"
                    OnClick="btnExport_Click" />
            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView ID="gvReport"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                CssClass="employee-grid"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowDataBound="gvReport_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:BoundField DataField="GSTR2B_GSTIN" HeaderText="GSTR2B_GSTIN" />
                    <asp:BoundField DataField="FACT_GSTIN" HeaderText="FACT_GSTIN" />
                    <asp:BoundField DataField="GSTR2B_PAN_NO" HeaderText="GSTR2B_PAN_NO" />
                    <asp:BoundField DataField="FACT_PAN_NO" HeaderText="FACT_PAN_NO" />
                    <asp:BoundField DataField="GSTR2B_VENDOR_NAME" HeaderText="GSTR2B_VENDOR_NAME" />
                    <asp:BoundField DataField="FACT_VENDOR_CODE" HeaderText="FACT_VENDOR_CODE" />
                    <asp:BoundField DataField="FACT_VENDOR_NAME" HeaderText="FACT_VENDOR_NAME" />
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" />

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




    <%-- </contenttemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
