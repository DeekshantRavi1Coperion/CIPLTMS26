<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportGSTR2B.aspx.cs"
    Inherits="REPORTS_PURCHASE_BALANCE_ImportGSTR2B" Title="Import GSTR2B" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

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

        function ValidatefileUploadDrawingFile() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadDrawingFile = document.getElementById('<%=fileUploadDrawingFile.ClientID %>').value;


            fileUploadDrawingFile = fileUploadDrawingFile.split(" ").join("")
            fileUploadDrawingFile = fileUploadDrawingFile.split("(").join("")
            fileUploadDrawingFile = fileUploadDrawingFile.split(")").join("")

            var divfileUploadDrawingFile = document.getElementById("divfileUploadDrawingFile");
            var lblfileUploadDrawingFile = document.getElementById('<%=lblfileUploadDrawingFile.ClientID %>');

            if (fileUploadDrawingFile == '') {
                document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadDrawingFile.style.display = "block";
                lblfileUploadDrawingFile.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadDrawingFile.toLowerCase())) {
                    document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadDrawingFile.style.display = "block";
                    lblfileUploadDrawingFile.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadDrawingFile.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadDrawingFile.style.display = "none";
                    lblfileUploadDrawingFile.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateAllDrawingFile() {
            var check = true;

            if (ValidatefileUploadDrawingFile()) { return false; }

            return check;
        }

        function EnableTypes() {

            var list = document.getElementById('<%= rdType.ClientID %>');
            var selectedValue = list.querySelector('input[type=radio]:checked').value;

            var ddlMonth = document.getElementById('<%= ddlMonth.ClientID %>');

            // Enable dropdown by default
            ddlMonth.disabled = false;

            // If selected = Yearly
            if (selectedValue == "2") {

                ddlMonth.value = ""; // reset
                ddlMonth.disabled = true; // disable
            }
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
                <legend>Import GSTR2B:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                        Text="Download Format" OnClick="btnGetFormat_Click" />

                    <asp:Panel runat="server" Visible="false">
                        <asp:RadioButtonList ID="rdType" runat="server"
                            RepeatDirection="Horizontal" Width="100%"
                            onclick="EnableTypes()">
                            <asp:ListItem Text="Yearly" Value="1" Selected="True" />
                            <asp:ListItem Text="Monthly" Value="2" />
                        </asp:RadioButtonList>
                    </asp:Panel>


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
                            <asp:ListItem Text="Select" Value="" />
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

                    <label>Browse:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadDrawingFile" runat="server" 
                                    CssClass="form-control"
                                    BorderStyle="Groove" onblur="return ValidatefileUploadDrawingFile();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileUploadDrawingFile" style="display: none;">
                                    <asp:Label ID="lblfileUploadDrawingFile" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnGetFile" CssClass="button" Width="100%" runat="server"
                    Text="Get Detail" OnClientClick="return ValidateAllDrawingFile();" OnClick="btnGetFile_Click" />

                <asp:Button ID="btnImport" CssClass="button" Width="100%" runat="server"
                    Text="Import GSTR2B" OnClick="btnImport_Click" OnClientClick="Confirm();" />

                <asp:Button ID="btnViewGSTR2BList" CssClass="button" Width="100%" runat="server"
                    Text="View GSTR2B" OnClick="btnViewGSTR2BList_Click" />
            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView
                CssClass="employee-grid"
                ID="gvDesignDetails"
                runat="server"
                AutoGenerateColumns="false"
                CellPadding="4"
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

                    <asp:TemplateField HeaderText="Sr No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' />

                            <asp:Label ID="lblGstinOfSupplier" runat="server" Text='<%# Eval("GSTIN_OF_SUPPLIER") %>' Visible="false" />
                            <asp:Label ID="lblTradeOrLegalName" runat="server" Text='<%# Eval("TRADE_OR_LEGAL_NAME") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("INVOICE_NO") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("INVOICE_DATE") %>' Visible="false" />
                            <asp:Label ID="lblInvoiceValue" runat="server" Text='<%# Eval("INVOICE_VALUE") %>' Visible="false" />
                            <asp:Label ID="lblTaxableValue" runat="server" Text='<%# Eval("TAXABLE_VALUE") %>' Visible="false" />
                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH") %>' Visible="false" />
                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>' Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="GSTIN_OF_SUPPLIER" HeaderText="Gstin Of Supplier" />
                    <asp:BoundField DataField="TRADE_OR_LEGAL_NAME" HeaderText="Trade/Legal Name" />
                    <asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No." />
                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="Invoice Date" />
                    <asp:BoundField DataField="INVOICE_VALUE" HeaderText="Invoice Value" />
                    <asp:BoundField DataField="TAXABLE_VALUE" HeaderText="Taxable Value" />
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
