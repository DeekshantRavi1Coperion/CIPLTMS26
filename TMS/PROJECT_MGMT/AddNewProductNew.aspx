<%@ Page Title="Add New Product" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddNewProductNew.aspx.cs" Inherits="PROJECT_MGMT_AddNewProductNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
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

        function ValidateProject() {
            var Project = document.getElementById('<%=ddlProjectNo.ClientID %>').selectedIndex;
            if (Project == '' || Project == '0') {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEstimatedProduct() {
            var EstimatedProduct = document.getElementById('<%=ddlEstimatedProduct.ClientID %>').selectedIndex;
            if (EstimatedProduct == '' || EstimatedProduct == '0') {
                document.getElementById('<%=ddlEstimatedProduct.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEstimatedProduct.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateGroup() {
            var Group = document.getElementById('<%=ddlGroup.ClientID %>').selectedIndex;
            if (Group == '' || Group == '0') {
                document.getElementById('<%=ddlGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSubgroup() {
            var Subgroup = document.getElementById('<%=ddlSubgroup.ClientID %>').selectedIndex;
            if (Subgroup == '' || Subgroup == '0') {
                document.getElementById('<%=ddlSubgroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSubgroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            var check = true;
            if (ValidateProject()) { return false; }
            if (ValidateEstimatedProduct()) { return false; }
            if (ValidateUnit()) { return false; }
            if (ValidateGroup()) { return false; }
            if (ValidateSubgroup()) { return false; }
            return check;
        }
    </script>

    <script type="text/Javascript">
        function ValidateTypeChange(el) {
            var row = el.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var selectedIndex = row.cells[3].childNodes[1].selectedIndex;
            var selectedText = row.cells[3].childNodes[1].options[row.cells[3].childNodes[1].selectedIndex].innerHTML;
            alert(selectedIndex + ':' + selectedText);
        }


        function GetItemCode(el) {
            var TypeTextFirstC;
            var CategoryTextFirstC;
            var itemcodeTxt;

            var row = el.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var ProjectNo = document.getElementById('<%=ddlProjectNo.ClientID %>');
            var ProjectNoText = ProjectNo.options[ProjectNo.selectedIndex].innerHTML;
            var ProjectNoTextChar = ProjectNoText.substring(5, 20);


            var TypeSelectedIndex = row.cells[3].childNodes[1].selectedIndex;
            var TypeSelectedText = row.cells[3].childNodes[1].options[row.cells[3].childNodes[1].selectedIndex].innerHTML;

            if (parseInt(TypeSelectedIndex) > 0) {
                TypeTextFirstC = TypeSelectedText.substring(0, 1);
            }
            else {
                TypeTextFirstC = ''
            }

            var CatetorySelectedIndex = row.cells[4].childNodes[1].selectedIndex;
            var CatetorySelectedText = row.cells[4].childNodes[1].options[row.cells[4].childNodes[1].selectedIndex].innerHTML;

            if (parseInt(CatetorySelectedIndex) > 0) {
                CategoryTextFirstC = CatetorySelectedText.substring(0, 1);
            }
            else {
                CategoryTextFirstC = ''
            }


            var d = new Date();
            var n = d.getFullYear();
            var year = String(n).substring(2, 4);



            itemcodeTxt = TypeTextFirstC + CategoryTextFirstC + ProjectNoTextChar;
            row.cells[5].getElementsByTagName("input")[0].value = itemcodeTxt;
        }
    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Add Products:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Project No.</label>
                    <asp:DropDownList ID="ddlProjectNo" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlProjectNo_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Estimated Product</label>
                    <asp:DropDownList ID="ddlEstimatedProduct" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Group</label>
                    <asp:DropDownList ID="ddlGroup" runat="server"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Subgroup</label>
                    <asp:DropDownList ID="ddlSubgroup" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <div class="full-width button-group">

                        <asp:Button ID="btnAddNewProduct" CssClass="button" Width="100%" runat="server" Text="Add New Product"
                            OnClick="btnAddNewProduct_Click" OnClientClick="return ValidateAll();" />

                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save Product"
                            OnClick="btnSave_Click" />

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

            <asp:GridView
                CssClass="employee-grid"
                ID="gvProductList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvProductList_RowDataBound"
                OnRowCommand="gvProductList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="REMOVE" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIAL_NO") %>' Visible="false" />
                            <asp:ImageButton ID="imgBtnDelete" CommandArgument="REMOVE" ToolTip="Remove" runat="server"
                                ImageUrl="~/Images/Cancelled01.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PROJECT_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblProjectNo" runat="server" Text='<%# Eval("PROJECT_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ESTIMATED_ITEM">
                        <ItemTemplate>
                            <asp:Label ID="lblEstimatedItem" runat="server" Text='<%# Eval("ESTIMATED_ITEM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TYPE">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100px" Height="25px" Text='<%# Eval("TYPE") %>'
                                onchange="GetItemCode(this)">
                                <asp:ListItem Text="Select" Value="0" />
                                <asp:ListItem Text="Boughtout" Value="1" />
                                <asp:ListItem Text="Assembled" Value="2" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CATEGORY">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" Height="25px" Text='<%# Eval("CATEGORY") %>'
                                onchange="GetItemCode(this)">
                                <asp:ListItem Text="Select" Value="0" />
                                <asp:ListItem Text="System" Value="1" />
                                <asp:ListItem Text="Component" Value="2" />
                                <asp:ListItem Text="Parts/Spare" Value="3" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRODUCT_CODE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtProductCode" runat="server" Width="200px" Text='<%# Eval("PRODUCT_CODE") %>'
                                onpaste="return false;" MaxLength="15" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="2" Text='<%# Eval("DESCRIPTION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDITIONAL_DESCRIPTION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAdditionalDescription" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="2" Text='<%# Eval("ADDITIONAL_DESCRIPTION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GST_HSN">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGSTHSN" runat="server" Width="200px" Text='<%# Eval("GST_HSN") %>' /><%--onblur="return ValidateGSTHSN();"--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GST_RATE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGSTRate" runat="server" Width="150px" onpaste="return false"
                                Text='<%# Eval("GST_RATE") %>' onkeyup="checkDec(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PURCHASE_UNIT">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlPurchaseUnit" runat="server" Width="150px" Height="25px"
                                Text='<%# Eval("PURCHASE_UNIT") %>'>
                                <%--onblur="return ValidatePurchaseUnit();"--%>
                                <asp:ListItem Value="0" Text="Select" />
                                <asp:ListItem Value="1" Text="sqmtr" />
                                <asp:ListItem Value="2" Text="PKT." />
                                <asp:ListItem Value="3" Text="EA" />
                                <asp:ListItem Value="4" Text="Ltrs." />
                                <asp:ListItem Value="5" Text="Kg." />
                                <asp:ListItem Value="6" Text="Pair" />
                                <asp:ListItem Value="7" Text="No." />
                                <asp:ListItem Value="8" Text="SET" />
                                <asp:ListItem Value="9" Text="Mtrs." />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STOCK_UNIT">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStockUnit" runat="server" Width="150px" Height="25px" Text='<%# Eval("STOCK_UNIT") %>'>
                                <asp:ListItem Value="0" Text="Select" />
                                <asp:ListItem Value="1" Text="sqmtr" />
                                <asp:ListItem Value="2" Text="PKT." />
                                <asp:ListItem Value="3" Text="EA" />
                                <asp:ListItem Value="4" Text="Ltrs." />
                                <asp:ListItem Value="5" Text="Kg." />
                                <asp:ListItem Value="6" Text="Pair" />
                                <asp:ListItem Value="7" Text="No." />
                                <asp:ListItem Value="8" Text="SET" />
                                <asp:ListItem Value="9" Text="Mtrs." />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SALE_UNIT">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSaleUnit" runat="server" Width="150px" Height="25px" Text='<%# Eval("SALE_UNIT") %>'>
                                <asp:ListItem Value="0" Text="Select" />
                                <asp:ListItem Value="1" Text="sqmtr" />
                                <asp:ListItem Value="2" Text="PKT." />
                                <asp:ListItem Value="3" Text="EA" />
                                <asp:ListItem Value="4" Text="Ltrs." />
                                <asp:ListItem Value="5" Text="Kg." />
                                <asp:ListItem Value="6" Text="Pair" />
                                <asp:ListItem Value="7" Text="No." />
                                <asp:ListItem Value="8" Text="SET" />
                                <asp:ListItem Value="9" Text="Mtrs." />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TAG_NO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTagNo" runat="server" Width="200px" Text='<%# Eval("TAG_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MODEL_NO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtModelNo" runat="server" Width="200px" Text='<%# Eval("MODEL_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDITIONAL_INFORMATION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAdditionalInformation" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="2" Text='<%# Eval("ADDITIONAL_INFORMATION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TECHNICAL_SPECIFICATION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTechnicalSpecification" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="2" Text='<%# Eval("TECHNICAL_SPECIFICATION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitID" runat="server" Text='<%# Eval("UNIT_ID") %>' Visible="false" />
                            <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GROUP">
                        <ItemTemplate>
                            <asp:Label ID="lblGroupID" runat="server" Text='<%# Eval("GROUP_ID") %>' Visible="false" />
                            <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("GROUP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SUB_GROUP">
                        <ItemTemplate>
                            <asp:Label ID="lblSubgroupID" runat="server" Text='<%# Eval("SUBGROUP_ID") %>' Visible="false" />
                            <asp:Label ID="lblSubgroup" runat="server" Text='<%# Eval("SUBGROUP") %>' />
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

        </div>

    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
