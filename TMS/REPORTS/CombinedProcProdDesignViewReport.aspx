<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="CombinedProcProdDesignViewReport.aspx.cs" Inherits="REPORTS_CombinedProcProdDesignViewReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>

    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 70%">
            <legend style="text-align: center;">Combined Procurement Production Design Report</legend>
            <table width="100%">
                <tr>
                    <td align="right">JOB No:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                    <td align="right">Company:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Text="All" Value="0" />
                            <asp:ListItem Text="Procurement View" Value="1" />
                            <asp:ListItem Text="Production View" Value="2" />
                            <asp:ListItem Text="Design View" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" on />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnExportProcurementReport" CssClass="button" Width="100%" runat="server" Text="Export Procurement View"
                            OnClick="btnExportProcurementReport_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnExportProductionReport" CssClass="button" Width="100%" runat="server" Text="Export Production View"
                            OnClick="btnExportProductionReport_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnExportDesignReport" CssClass="button" Width="100%" runat="server" Text="Export Design View"
                            OnClick="btnExportDesignReport_Click" />
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
        <fieldset style="width: 90%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblProcurementViewRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>

                <asp:GridView ID="gvProcurementViewReport" runat="server" AutoGenerateColumns="false"
                    CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                        <asp:BoundField DataField="PO_DATE" HeaderText="PO_DATE" />
                        <asp:BoundField DataField="PO_DELIVERY_DATE" HeaderText="PO_DELIVERY_DATE" />
                        <asp:BoundField DataField="VENDOR_NAME" HeaderText="VENDOR_NAME" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM_NAME" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="PO_VALUE_INR" HeaderText="PO_VALUE_INR" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BUDGET" HeaderText="BUDGET" />
                        <asp:BoundField DataField="FOLLOW_UP_BY" HeaderText="FOLLOW_UP_BY" />
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="PO_FIRST_ITEM" HeaderText="PO_FIRST_ITEM" />
                        <asp:BoundField DataField="ENGG_APPROVAL_STATUS" HeaderText="ENGG_APPROVAL_STATUS" />
                        <asp:BoundField DataField="PRESENT_STATUS" HeaderText="PRESENT_STATUS" />
                        <asp:BoundField DataField="ED_OF_INSP/COMP" HeaderText="ED_OF_INSP/COMP" />
                        <asp:BoundField DataField="POSTING_STATUS" HeaderText="POSTING_STATUS" />
                        <asp:BoundField DataField="LAST_STATUS" HeaderText="LAST_STATUS" />
                        <asp:BoundField DataField="LAST_ED_OF_INSP/COMP" HeaderText="LAST_ED_OF_INSP/COMP" />
                        <asp:BoundField DataField="LAST_FOLLOW_UP_DATE" HeaderText="LAST_FOLLOW_UP_DATE" />
                        <asp:BoundField DataField="FOLLOW_UP_DAYS" HeaderText="FOLLOW_UP_DAYS" />
                    </Columns>
                </asp:GridView>

            </div>
        </fieldset>
    </div>


    <div align="center" style="float: left; width: 50%; margin-top: 20px;">
        <fieldset style="width: 80%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblProductionViewRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvProductionViewReport" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="SR_NO" HeaderText="SR_NO" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="LOT_DATE" HeaderText="LOT_DATE" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="PRODUCTION_ORDER_NO" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="PRODUCTION_ORDER_DATE" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_DELIVERY_DATE" HeaderText="PRODUCTION_ORDER_DELIVERY_DATE" />
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="PRODUCT_CODE" />
                        <asp:BoundField DataField="EQUIPMENT/ITEM" HeaderText="EQUIPMENT/ITEM" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
                        <asp:BoundField DataField="PRESENT_STATUS" HeaderText="PRESENT_STATUS" />
                        <asp:BoundField DataField="PRESENT_PERC_OF_WORK_DONE" HeaderText="PRESENT_PERC_OF_WORK_DONE" />
                        <asp:BoundField DataField="ED_OF_INSP_COMP" HeaderText="ED_OF_INSP_COMP" />
                        <asp:BoundField DataField="LAST_STATUS" HeaderText="LAST_STATUS" />
                        <asp:BoundField DataField="LAST_PERC_OF_WORK_DONE" HeaderText="LAST_PERC_OF_WORK_DONE" />
                        <asp:BoundField DataField="LAST_ED_OF_INSP_COMP" HeaderText="LAST_ED_OF_INSP_COMP" />
                        <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>

    <div align="center" style="float: right; width: 50%; margin-top: 20px;">
        <fieldset style="width: 80%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblDesignViewRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvDesignViewReport" runat="server" AutoGenerateColumns="false" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="SR_NO" HeaderText="SR_NO" />                        
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />                        
                        <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="REQD_DATE_BY_PROJECT_TEAM" HeaderText="REQD_DATE_BY_PROJECT_TEAM" />                        
                        <asp:BoundField DataField="IS_PLANNED" HeaderText="IS_PLANNED" />
                        <asp:BoundField DataField="PLANNED_START_DATE_BY_DESIGN_TEAM" HeaderText="PLANNED_START_DATE_BY_DESIGN_TEAM" />
                        <asp:BoundField DataField="PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM" HeaderText="PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
                        <asp:BoundField DataField="DRAWING_REV_NO" HeaderText="DRAWING_REV_NO" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                        <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="EXPECTED_COMPLETION_DATE" />                        
                        <asp:BoundField DataField="DESIGN_RESPONSIBLE_ENGG" HeaderText="DESIGN_RESPONSIBLE_ENGG" />
                        <asp:BoundField DataField="POSTING_STATUS" HeaderText="POSTING_STATUS" />
                        <asp:BoundField DataField="TOTAL_HOURS_SPENT" HeaderText="TOTAL_HOURS_SPENT" />
                        <asp:BoundField DataField="APPLICABLE_FOR_PRODUCTION" HeaderText="APPLICABLE_FOR_PRODUCTION" />
                        <asp:BoundField DataField="IS_REVISED" HeaderText="IS_REVISED" />
                        <asp:BoundField DataField="TOTAL_HOURS_SPENT" HeaderText="TOTAL_HOURS_SPENT" />
                        <asp:BoundField DataField="TOTAL_HOURS_SPENT" HeaderText="TOTAL_HOURS_SPENT" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
