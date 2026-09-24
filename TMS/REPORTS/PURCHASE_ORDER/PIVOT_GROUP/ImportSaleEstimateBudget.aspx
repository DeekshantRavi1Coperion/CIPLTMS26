<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportSaleEstimateBudget.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_PIVOT_GROUP_ImportSaleEstimateBudget" Title="CIPLTMS- Import Sale Estimate Budget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

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


        function ValidateJobNo() {
            var JobNo = document.getElementById('<%=txtJobNo.ClientID %>').value;

            if (JobNo == '') {
                document.getElementById('<%=txtJobNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJobNo.ClientID %>').style.borderColor = "";
                return false;
            }

        }


        function ValidatefileUploadSaleEstimateBudget() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadSaleEstimateBudget = document.getElementById('<%=fileUploadSaleEstimateBudget.ClientID %>').value;
            var divfileUploadSaleEstimateBudget = document.getElementById("divfileUploadSaleEstimateBudget");
            var lblfileUploadSaleEstimateBudget = document.getElementById('<%=lblfileUploadSaleEstimateBudget.ClientID %>');

            if (fileUploadSaleEstimateBudget == '') {
                document.getElementById('<%=fileUploadSaleEstimateBudget.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadSaleEstimateBudget.style.display = "block";
                lblfileUploadSaleEstimateBudget.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadSaleEstimateBudget.toLowerCase())) {
                    document.getElementById('<%=fileUploadSaleEstimateBudget.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadSaleEstimateBudget.style.display = "block";
                    lblfileUploadSaleEstimateBudget.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadSaleEstimateBudget.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadSaleEstimateBudget.style.display = "none";
                    lblfileUploadSaleEstimateBudget.innerHTML = "";
                    return false;
                }
            }
        }

    </script>


    <script type="text/javascript" language="javascript">

        function ValidateJobNoForFormat() {
            var check = true;

            if (ValidateJobNo()) { return false; }

            return check;
        }

    </script>


    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;

            if (ValidatefileUploadSaleEstimateBudget()) { return false; }

            return check;
        }

    </script>


    <script type="text/javascript">

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function Confirm() {
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;
            var GVRowCountNew = 0;

            if (GVRowCount == '')
                GVRowCountNew = 0;
            else
                GVRowCountNew = parseInt(GVRowCount);

            if (GVRowCountNew > 0) {

                if (confirm("Would you like to import sale estimate budget?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return false;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return true;
                }
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return true;
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Import Sale Estimate Budget:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width button-group">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJobNo" runat="server" 
                            Style="text-transform: uppercase"
                            CssClass="form-control"></asp:TextBox>


                        <asp:HiddenField ID="hdConfirmValue" runat="server" />
                        <asp:HiddenField ID="hdGVRowCount" runat="server" />
                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            OnClientClick="return ValidateJobNoForFormat();" Text="Download Format" OnClick="btnGetFormat_Click" />

                    </div>

                    <div class="full-width button-group">
                        <label>Browse:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadSaleEstimateBudget" runat="server" 
                                        CssClass="form-control"
                                        BorderStyle="Groove" onblur="return ValidatefileUploadSaleEstimateBudget();" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadSaleEstimateBudget" style="display: none;">
                                        <asp:Label ID="lblfileUploadSaleEstimateBudget" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <asp:Button ID="btnGetDetails" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetDetails_Click" />

                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                    Text="Save" OnClick="btnSave_Click" OnClientClick="Confirm();" />
            </div>
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
                        ID="gvSaleEstimateBudget" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                        HorizontalAlign="Center" OnRowDataBound="gvSaleEstimateBudget_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>

                            <asp:TemplateField HeaderText="Serial_Number" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNo" runat="server" Visible="true" Text='<%# Eval("SR_NO") %>' />
                                    <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                    <asp:Label ID="lblPivotGroup" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP") %>' />
                                    <asp:Label ID="lblPivotGroupDesc" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP_DESCRIPTION") %>' />
                                    <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                    <asp:Label ID="lblTableName" runat="server" Visible="false" Text='<%# Eval("TABLE_NAME") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Pivot Group" DataField="PIVOT_GROUP" />
                            <asp:BoundField HeaderText="Pivot Group Description" DataField="PIVOT_GROUP_DESCRIPTION" />
                            <asp:BoundField HeaderText="Job Number" DataField="JOB_NO" />

                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" Width="100%" CssClass="textbox2"
                                        Text='<%# Eval("TOTAL_AMOUNT") %>' onkeypress="return inNumberKeyWithDecimal(this, event);" />
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

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
