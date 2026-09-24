<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportedBooksB2BCleanList.aspx.cs"
    Inherits="REPORTS_PURCHASE_BALANCE_ImportedBooksB2BCleanList" Title="Imported Books B2B List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />

    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxcenter {
            width: 100%;
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxright {
            width: 100%;
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxleft {
            width: 100%;
            padding: 5px 5px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: palegreen;*/
        }
    </style>

    <script type="text/javascript" language="javascript">

        function EnableTypes() {
            alert('hello');
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
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Imported Books B2B List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <asp:DropDownList ID="ddlYear" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="2020-2021" Value="2020-2021" />
                        <asp:ListItem Text="2021-2022" Value="2021-2022" />
                        <asp:ListItem Text="2022-2023" Value="2022-2023" />
                        <asp:ListItem Text="2023-2024" Value="2023-2024" />
                        <asp:ListItem Text="2024-2025" Value="2024-2025" />
                        <asp:ListItem Text="2025-2026" Value="2025-2026" />
                        <asp:ListItem Text="2026-2027" Value="2026-2027" />
                        <asp:ListItem Text="2027-2028" Value="2027-2028" />
                        <asp:ListItem Text="2028-2029" Value="2028-2029" />
                        <asp:ListItem Text="2029-2030" Value="2029-2030" />
                        <asp:ListItem Text="2030-2031" Value="2030-2031" />
                    </asp:DropDownList>

                    <asp:Panel runat="server" Visible="false">
                        <asp:DropDownList ID="ddlMonth" runat="server"
                            CssClass="form-control">
                            <asp:ListItem Text="All" Value="0" />
                            <asp:ListItem Text="Jan" Value="1" />
                            <asp:ListItem Text="Feb" Value="2" />
                            <asp:ListItem Text="Mar" Value="3" />
                            <asp:ListItem Text="Apr" Value="4" />
                            <asp:ListItem Text="May" Value="5" />
                            <asp:ListItem Text="Jun" Value="6" />
                            <asp:ListItem Text="Jul" Value="7" />
                            <asp:ListItem Text="Aug" Value="8" />
                            <asp:ListItem Text="Sep" Value="9" />
                            <asp:ListItem Text="Oct" Value="10" />
                            <asp:ListItem Text="Nov" Value="11" />
                            <asp:ListItem Text="Dec" Value="12" />
                        </asp:DropDownList>
                    </asp:Panel>


                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                        Text="View Imported Books" OnClick="btnSearch_Click" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnViewExcludedBooksList" CssClass="button" Width="100%" runat="server"
                    Text="View Excluded Books" OnClick="btnViewExcludedBooksList_Click" />

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
            <asp:GridView ID="gvDesignDetails"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
                CssClass="employee-grid"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowDataBound="gvDesignDetails_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />

                <Columns>

                    <asp:TemplateField HeaderText="SrNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="true" />
                            <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("VENDOR_CODE") %>' Visible="false" />
                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VENDOR_NAME") %>' Visible="false" />
                            <asp:Label ID="lblGstinOfSupplier" runat="server" Text='<%# Eval("GSTIN_OF_SUPPLIER") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%# Eval("INVOICE_NUMBER") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("INVOICE_DATE") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceValue" runat="server" Text='<%# Eval("INVOICE_VALUE") %>' Visible="false" />
                            <asp:Label ID="lblPlaceOfSupply" runat="server" Text='<%# Eval("PLACE_OF_SUPPLY") %>' Visible="false" />
                            <asp:Label ID="lblReverseCharge" runat="server" Text='<%# Eval("REVERSE_CHARGE") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceType" runat="server" Text='<%# Eval("INVOICE_TYPE") %>' Visible="false" />
                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>' Visible="false" />
                            <asp:Label ID="lblTaxableValue" runat="server" Text='<%# Eval("TAXABLE_VALUE") %>' Visible="false" />
                            <asp:Label ID="lblIntegratedTaxPaid" runat="server" Text='<%# Eval("INTEGRATED_TAX_PAID") %>' Visible="false" />
                            <asp:Label ID="lblCentralTaxPaid" runat="server" Text='<%# Eval("CENTRAL_TAX_PAID") %>' Visible="false" />
                            <asp:Label ID="lblStateUtTaxPaid" runat="server" Text='<%# Eval("STATE_UT_TAX_PAID") %>' Visible="false" />
                            <asp:Label ID="lblCessPaid" runat="server" Text='<%# Eval("CESS_PAID") %>' Visible="false" />
                            <asp:Label ID="lblEligibilityForItc" runat="server" Text='<%# Eval("ELIGIBILITY_FOR_ITC") %>' Visible="false" />
                            <asp:Label ID="lblAvailedItcIntegratedTax" runat="server" Text='<%# Eval("AVAILED_ITC_INTEGRATED_TAX") %>' Visible="false" />
                            <asp:Label ID="lblAvailedItcCentralTax" runat="server" Text='<%# Eval("AVAILED_ITC_CENTRAL_TAX") %>' Visible="false" />
                            <asp:Label ID="lblAvailedItcStateUtTax" runat="server" Text='<%# Eval("AVAILED_ITC_STATE_UT_TAX") %>' Visible="false" />
                            <asp:Label ID="lblAvailedItcCess" runat="server" Text='<%# Eval("AVAILED_ITC_CESS") %>' Visible="false" />
                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH") %>' Visible="false" />
                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="VENDOR_CODE" HeaderText="Vendor Code" />
                    <asp:BoundField DataField="VENDOR_NAME" HeaderText="Vendor Name" />
                    <asp:BoundField DataField="GSTIN_OF_SUPPLIER" HeaderText="Gstin Of Supplier" />
                    <asp:BoundField DataField="INVOICE_NUMBER" HeaderText="Invoice Number" />
                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="Invoice Date" />
                    <asp:BoundField DataField="INVOICE_VALUE" HeaderText="Invoice Value" />
                    <asp:BoundField DataField="PLACE_OF_SUPPLY" HeaderText="Place Of Supply" />
                    <asp:BoundField DataField="REVERSE_CHARGE" HeaderText="Reverse Charge" />
                    <asp:BoundField DataField="INVOICE_TYPE" HeaderText="Invoice Type" />
                    <asp:BoundField DataField="RATE" HeaderText="Rate" />
                    <asp:BoundField DataField="TAXABLE_VALUE" HeaderText="Taxable Value" />
                    <asp:BoundField DataField="INTEGRATED_TAX_PAID" HeaderText="Integrated Tax Paid" />
                    <asp:BoundField DataField="CENTRAL_TAX_PAID" HeaderText="Central Tax Paid" />
                    <asp:BoundField DataField="STATE_UT_TAX_PAID" HeaderText="State Ut Tax Paid" />
                    <asp:BoundField DataField="CESS_PAID" HeaderText="Cess Paid" />
                    <asp:BoundField DataField="ELIGIBILITY_FOR_ITC" HeaderText="Eligibility For Itc" />
                    <asp:BoundField DataField="AVAILED_ITC_INTEGRATED_TAX" HeaderText="Availed Itc Integrated Tax" />
                    <asp:BoundField DataField="AVAILED_ITC_CENTRAL_TAX" HeaderText="Availed Itc Central Tax" />
                    <asp:BoundField DataField="AVAILED_ITC_STATE_UT_TAX" HeaderText="Availed Itc State/Ut Tax" />
                    <asp:BoundField DataField="AVAILED_ITC_CESS" HeaderText="Availed Itc Cess" />
                    <asp:BoundField DataField="MONTH" HeaderText="Month" />
                    <asp:BoundField DataField="YEAR" HeaderText="Year" />

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


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
