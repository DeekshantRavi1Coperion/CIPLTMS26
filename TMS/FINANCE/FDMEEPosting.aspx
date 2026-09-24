<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="FDMEEPosting.aspx.cs"
    Inherits="FINANCE_FDMEEPosting" Title="FDME Posting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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


        function ValidatefileUploadCostCenter() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadCostCenter = document.getElementById('<%=fileUploadCostCenter.ClientID %>').value;
            var divfileUploadCostCenter = document.getElementById("divfileUploadCostCenter");
            var lblfileUploadCostCenter = document.getElementById('<%=lblfileUploadCostCenter.ClientID %>');

            if (fileUploadCostCenter == '') {
                document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadCostCenter.style.display = "block";
                lblfileUploadCostCenter.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadCostCenter.toLowerCase())) {
                    document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "block";
                    lblfileUploadCostCenter.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "none";
                    lblfileUploadCostCenter.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;

            if (ValidatefileUploadCostCenter()) { return false; }

            return check;
        }

    </script>



    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>


    <script type="text/javascript">
        var GridId = "<%=gvFDMEE.ClientID %>";
        var ScrollHeight = 380;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }



        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function Confirm() {
            var existedRecordsCount = document.getElementById('<%=hdExistedRecords.ClientID %>').value;
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;

            if (parseInt(GVRowCount) > 0) {
                if (parseInt(existedRecordsCount) > 0) {
                    var confirm_value = document.createElement("input");
                    confirm_value.type = "hidden";
                    confirm_value.name = "Confirm Value";
                    if (confirm("There are " + existedRecordsCount + " records exist in system. Do you want to replace?")) {
                        document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 1;
                    }
                    else {
                        document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
                    }
                }
                else {
                    document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
                }
            }
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <asp:HiddenField ID="hdExistedRecords" runat="server" />
    <asp:HiddenField ID="hdGVRowCount" runat="server" />
    <asp:HiddenField ID="hdReplacementFlag" runat="server" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>FDMEE Posting:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                        Text="Download Format" OnClick="btnGetFormat_Click" />

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadCostCenter" runat="server" CssClass="form-control"
                                    BorderStyle="Groove" onblur="return ValidatefileUploadCostCenter();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileUploadCostCenter" style="display: none;">
                                    <asp:Label ID="lblfileUploadCostCenter" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                    <asp:Button ID="btnGetFDMEEFile" CssClass="button" Width="100%" runat="server"
                        Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetFDMEEFile_Click" />

                    <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                        Text="Save" OnClick="btnSave_Click" OnClientClick="Confirm();" />

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
                        ID="gvFDMEE" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                        HorizontalAlign="Center" OnRowDataBound="gvFDMEE_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID" ) %>' />
                                    <asp:Label ID="lblYear" runat="server" Visible="true" Text='<%# Eval("YEAR" ) %>' />
                                    <asp:Label ID="lblSRNo" runat="server" Visible="false" Text='<%# Eval("SR_NO" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Period">
                                <ItemTemplate>
                                    <asp:Label ID="lblPeriod" runat="server" Visible="true" Text='<%# Eval("PERIOD" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quarter">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuarter" runat="server" Visible="true" Text='<%# Eval("QUARTER" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GL_Account">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLAccount" runat="server" Visible="true" Text='<%# Eval("GL_ACCOUNT" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Visible="true" Text='<%# Eval("DESCRIPTION" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BSPL_Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBSPLType" runat="server" Visible="true" Text='<%# Eval("BSPL_TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HFM_Account">
                                <ItemTemplate>
                                    <asp:Label ID="lblHFMAccount" runat="server" Visible="true" Text='<%# Eval("HFM_ACCOUNT" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HFM_Account_Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lblHFMAccountDesc" runat="server" Visible="true" Text='<%# Eval("HFM_ACCOUNT_DESC" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Visible="true" Text='<%# Eval("TYPE" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Visible="true" Text='<%# Eval("CATEGORY" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ICP">
                                <ItemTemplate>
                                    <asp:Label ID="lblICP" runat="server" Visible="true" Text='<%# Eval("ICP" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HFM_Custom1">
                                <ItemTemplate>
                                    <asp:Label ID="lblHFMCustom1" runat="server" Visible="true" Text='<%# Eval("HFM_CUSTOM1" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HFMNew_Custom4">
                                <ItemTemplate>
                                    <asp:Label ID="lblHFMNewCustom4" runat="server" Visible="true" Text='<%# Eval("HFMNEW_CUSTOM4" ) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("AMOUNT" ) %>' Width="100%"
                                        onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source_Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSourceAmount" runat="server" Text='<%# Eval("SOURCE_AMOUNT" ) %>' Width="100%"
                                        onkeyDown="javascript:preventInput(event);" CssClass="textbox"></asp:TextBox>
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
