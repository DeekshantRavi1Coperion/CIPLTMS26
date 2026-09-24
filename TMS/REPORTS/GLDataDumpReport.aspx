<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GLDataDumpReport.aspx.cs" Inherits="REPORTS_GLDataDumpReport" %>--%>

<%@ Page Title="GL Data Dump Report" Language="C#" AutoEventWireup="true" MasterPageFile="~/HOME.master" CodeFile="GLDataDumpReport.aspx.cs" Inherits="REPORTS_GLDataDumpReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>

    <style type="text/css">
        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: #D8D8D8;
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #ebdef0;*/
        }

        .textbox2 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #fadbd8;*/
        }

        .textbox3 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: #D8D8D8;
        }
    </style>

    <script type="text/javascript">


</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>

    <asp:HiddenField ID="hdExistedRecords" runat="server" />
    <asp:HiddenField ID="hdReplacementFlag" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>GL Data Dump Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <div class="full-width button-group">

                        <asp:Button ID="Button1" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />

                        <label>Browse:</label>
                        <asp:FileUpload ID="fileUploadCostCenter" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadCostCenter();" />

                    </div>

                    <div class="full-width button-group">

                        <asp:Button ID="btnGetCostCenterFile" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetCostCenterFile_Click" />

                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                            Text="Save" OnClick="btnSave_Click"
                            OnClientClick="Confirm();" />
                    </div>


                    <div class="full-width button-group">
                        <label>Status.:</label>
                        <asp:DropDownList ID="ddlPLBS" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>

                        <asp:Button ID="btnGetGLDataDumpReport" CssClass="button" Width="100%" runat="server"
                            Text="Get Report" OnClick="btnGetGLDataDumpReport_Click" />

                        <asp:Button ID="btnExportReport" CssClass="button" Width="100%" runat="server"
                            Text="Export" OnClick="btnExportReport_Click" />
                    </div>



                </div>
            </fieldset>
        </div>
        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:UpdatePanel runat="server" ID="uppanel">
                <ContentTemplate>
                    <asp:GridView ID="gvCostCenter"
                        CssClass="employee-grid"
                        runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333"
                        GridLines="Both" PageSize="7" Width="100%"
                        HorizontalAlign="Center" OnRowDataBound="gvCostCenter_RowDataBound">
                        <%--OnRowDataBound="gvCostCenter_RowDataBound"--%>
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                        <Columns>
                            <%-- <asp:BoundField DataField="INVOICE_NUMBER" HeaderText="INVOICE_NUMBER" />--%>
                            <asp:TemplateField HeaderText="COMPANY_INITIALS">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyInitials" runat="server" Visible="true" Text='<%# Eval("COMPANY_INITIALS" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOCUMENT_TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocType" runat="server" Visible="true" Text='<%# Eval("DOCUMENT_TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOCUMENT_NUMBER">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocNumber" runat="server" Visible="true" Text='<%# Eval("DOCUMENT_NUMBER" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DOCUMENT_DATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocumentDate" runat="server" Visible="true" Text='<%# Eval("DOCUMENT_DATE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VOUCHER_NUMBER">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoucherNumber" runat="server" Visible="true" Text='<%# Eval("VOUCHER_NUMBER" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PL / BS TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblPLOrBS" runat="server"
                                        Text='<%# Eval("PL_OR_BS_TYPE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GENERAL_LEDGER_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLCode" runat="server" Visible="true" Text='<%# Eval("GENERAL_LEDGER_CODE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GENERAL_LEDGER_DESC">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLDesc" runat="server" Visible="true" Text='<%# Eval("GENERAL_LEDGER_DESCRIPTION" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUSTOMER_VENDOR_COST_CENTER_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustVendCostCenterCode" runat="server" Visible="true" Text='<%# Eval("CUSTOMER_VENDOR_COST_CENTER_CODE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUSTOMER_VENDOR_COST_CENTER_NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustVendCostCenterName" runat="server" Visible="true" Text='<%# Eval("CUSTOMER_VENDOR_COST_CENTER_NAME" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCT_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCode" runat="server" Visible="true" Text='<%# Eval("PRODUCT_CODE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCT_DESC">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductDesc" runat="server" Visible="true" Text='<%# Eval("PRODUCT_DESCRIPTION" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NET_AMOUNT_INR">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetAmountInr" runat="server" Visible="true" Text='<%# Eval("NET_AMOUNT_INR") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />

                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>

    </div>


</asp:Content>
