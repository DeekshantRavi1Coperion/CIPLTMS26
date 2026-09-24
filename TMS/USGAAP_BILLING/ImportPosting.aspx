<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ImportPosting.aspx.cs" Inherits="USGAAP_BILLING_ImportPosting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>


    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


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


    <%--<script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNan(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            }
        }

    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>


    <asp:HiddenField ID="hdExistedRecords" runat="server" />
    <asp:HiddenField ID="hdReplacementFlag" runat="server" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Import Posting:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <div class="full-width button-group">
                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />

                        <label>Browse:</label>
                        <asp:FileUpload ID="fileUploadCostCenter" runat="server"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadCostCenter();" />
                    </div>

                    <div class="full-width button-group">
                        <asp:Button ID="btnGetCostCenterFile" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetCostCenterFile_Click" />

                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                            Text="Save" OnClick="btnSave_Click"
                            OnClientClick="Confirm();" />
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
                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvCostCenter" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                        HorizontalAlign="Center">
                        <%--OnRowDataBound="gvCostCenter_RowDataBound"--%>
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                        <Columns>
                            <%-- <asp:BoundField DataField="INVOICE_NUMBER" HeaderText="INVOICE_NUMBER" />--%>
                            <asp:TemplateField HeaderText="INVOICE_NUMBER">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" runat="server" Visible="true" Text='<%# Eval("INVOICE_NUMBER" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="COMPANY">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompany" runat="server" Visible="true" Text='<%# Eval("COMPANY" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INVOICE_DATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" runat="server" Visible="true" Text='<%# Eval("INVOICE_DATE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="JOB_NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblJobNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUSTOMER_NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Visible="true" Text='<%# Eval("CUSTOMER_NAME" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUSTOMER_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Visible="true" Text='<%# Eval("CUSTOMER_CODE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CUSTOMER_ADDRESS">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerAddress" runat="server" Visible="true" Text='<%# Eval("CUSTOMER_ADDRESS" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BUSINESS_UNIT">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusinessUnit" runat="server" Visible="true" Text='<%# Eval("BUSINESS_UNIT" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCT_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductCode" runat="server" Visible="true" Text='<%# Eval("PRODUCT_CODE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REVENUE_ACCOUNT">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevenueAccount" runat="server" Visible="true" Text='<%# Eval("REVENUE_ACCOUNT" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REVENUE_ACCOUNT_DESC">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevenueAccountDesc" runat="server" Visible="true" Text='<%# Eval("REVENUE_ACCOUNT_DESC" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REVENUE_ACCOUNT_TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevenueAccountType" runat="server" Visible="true" Text='<%# Eval("REVENUE_ACCOUNT_TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QUANTITY">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Visible="true" Text='<%# Eval("QUANTITY" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PRODUCT_RATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductRate" runat="server" Visible="true" Text='<%# Eval("PRODUCT_RATE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INVOICE_AMOUNT">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceAmount" runat="server" Visible="true" Text='<%# Eval("INVOICE_AMOUNT" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="POSTED_VALUE">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedValue" runat="server" Visible="true" Text='<%# Eval("POSTED_VALUE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UNPOSTED_VALUE">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnpostedValue" runat="server" Visible="true" Text='<%# Eval("UNPOSTED_VALUE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="END_MARKET">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndMarket" runat="server" Visible="true" Text='<%# Eval("END_MARKET" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GEOGRAPHY">
                                <ItemTemplate>
                                    <asp:Label ID="lblGeography" runat="server" Visible="true" Text='<%# Eval("GEOGRAPHY" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="POSTING_MONTH">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostingMonth" runat="server" Visible="true" Text='<%# Eval("POSTING_MONTH" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="POSTING_YEAR">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostingYear" runat="server" Visible="true" Text='<%# Eval("POSTING_YEAR" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Visible="true" Text='<%# Eval("TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REVENUE/NOT REVENUE TYPE">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevNonRevType" runat="server" Visible="true" Text='<%# Eval("REVENUE/NOT REVENUE TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="REV_REC_REDUCTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblRevRecReduction" runat="server" Visible="true" Text='<%# Eval("REV_REC_REDUCTION" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UDF2">
                                <ItemTemplate>
                                    <asp:Label ID="lbludf2" runat="server" Visible="true" Text='<%# Eval("UDF2" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UDF3">
                                <ItemTemplate>
                                    <asp:Label ID="lbludf3" runat="server" Visible="true" Text='<%# Eval("UDF3" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UDF4">
                                <ItemTemplate>
                                    <asp:Label ID="lbludf4" runat="server" Visible="true" Text='<%# Eval("UDF4" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UDF5">
                                <ItemTemplate>
                                    <asp:Label ID="lbludf5" runat="server" Visible="true" Text='<%# Eval("UDF5" ) %>' />
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
