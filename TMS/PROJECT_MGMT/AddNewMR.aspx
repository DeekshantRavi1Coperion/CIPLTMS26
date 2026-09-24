<%@ Page Title="Add New MR" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddNewMR.aspx.cs" Inherits="PROJECT_MGMT_AddNewMR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtExpectedPODate.ClientID %>').value = document.getElementById('<%=hdExpectedPODate.ClientID %>').value;
            document.getElementById('<%=txtDeliveryRequiredBy.ClientID %>').value = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value;
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdExpectedPODate.ClientID %>').value = document.getElementById('<%=txtExpectedPODate.ClientID %>').value;
            document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value = document.getElementById('<%=txtDeliveryRequiredBy.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdExpectedPODate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }
            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);

            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return false;
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdExpectedPODate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdDeliveryRequiredBy.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return true;
            }
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

        function ValidateProjectNo() {
            var ProjectNo = document.getElementById('<%=ddlProjectNo.ClientID %>').selectedIndex;
            if (ProjectNo == '' || ProjectNo == '0') {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "";
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


        function ValidateProductCode() {
            var ProductCode = document.getElementById('<%=ddlProductCode.ClientID %>').selectedIndex;
            if (ProductCode == '' || ProductCode == '0') {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMRType() {
            var MRType = document.getElementById('<%=ddlMRType.ClientID %>').selectedIndex;
            if (MRType == '' || MRType == '0') {
                document.getElementById('<%=ddlMRType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMRType.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateDocumentClass() {
            var DocumentClass = document.getElementById('<%=txtDocumentClass.ClientID %>').value;
            if (DocumentClass == '') {
                document.getElementById('<%=txtDocumentClass.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDocumentClass.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidateProjectNo()) { return false; }
            if (ValidateUnit()) { return false; }
            if (ValidateProductCode()) { return false; }
            if (ValidateMRType()) { return false; }
            if (ValidateDocumentClass()) { return false; }
            if (ValidateDateRange()) { return false; }
            return check;
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
                <legend>Project Monthly Input Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Project No</label>
                    <asp:DropDownList ID="ddlProjectNo" runat="server" 
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlProjectNo_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit" runat="server" 
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Product Code</label>
                    <asp:DropDownList ID="ddlProductCode" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>MR Type</label>
                    <asp:DropDownList ID="ddlMRType" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="General/Meterial" Value="1" />
                        <asp:ListItem Text="Service" Value="2" />
                        <asp:ListItem Text="Production" Value="3" />
                    </asp:DropDownList>

                    <label>Document Class</label>
                    <asp:TextBox ID="txtDocumentClass" MaxLength="15" runat="server" 
                        CssClass="form-control"></asp:TextBox>
                    
                    <label>Expected PO Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtExpectedPODate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdExpectedPODate" runat="server" />
                                <asp:CalendarExtender ID="calendarExpectedPODate" PopupButtonID="imgbtnExpectedPODate"
                                    runat="server" TargetControlID="txtExpectedPODate" Format="dd-MMM-yyyy" 
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnExpectedPODate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>
                    <label>Delivery Required By</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDeliveryRequiredBy" runat="server" ReadOnly="true" 
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdDeliveryRequiredBy" runat="server" />
                                <asp:CalendarExtender ID="calendarDeliveryRequiredBy" PopupButtonID="imgbtnDeliveryRequiredBy"
                                    runat="server" TargetControlID="txtDeliveryRequiredBy" 
                                    Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnDeliveryRequiredBy" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <div class="full-width button-group">
                        <asp:Button ID="btnAddNewMR" CssClass="button" Width="100%" runat="server" Text="Add New MR"
                            OnClick="btnAddNewMR_Click" OnClientClick="return ValidateAll();" />

                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save MR"
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
                ID="gvMRList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvMRList_RowDataBound"
                OnRowCommand="gvMRList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="REMOVE" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIAL_NO") %>' Visible="false" />
                            <asp:ImageButton ID="imgBtnDelete" CommandArgument="REMOVE" ToolTip="Remove" runat="server"
                                ImageUrl="~/Images/Cancelled01.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRODUCT_CODE">
                        <ItemTemplate>
                            <asp:Label ID="lblFinancialYear" runat="server" Text='<%# Eval("FINANCIAL_YEAR") %>'
                                Visible="false" />
                            <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("PRODUCT_ID") %>' Visible="false" />
                            <asp:Label ID="lblTypeID" runat="server" Text='<%# Eval("TYPE_ID") %>' Visible="false" />
                            <asp:Label ID="lblUnitID" runat="server" Text='<%# Eval("UNIT_ID") %>' Visible="false" />
                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="txtDescription" runat="server" Width="100%" Enabled="false" Text='<%# Eval("DESCRIPTION") %>' />--%>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MR_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblMRNo" runat="server" Text='<%# Eval("MR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOCUMENT_CLASS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocumentClassG" runat="server" Width="150px" MaxLength="15" Text='<%# Eval("DOCUMENT_CLASS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlUOM" runat="server" Width="150px" Height="25px" Text='<%# Eval("UOM") %>'>
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
                    <asp:TemplateField HeaderText="QUANTITY">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="150px" onpaste="return false"
                                Text='<%# Eval("QUANTITY") %>' onkeyup="checkDec(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDITIONAL_DESCRIPTION">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAdditionalDescription" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="2" Text='<%# Eval("ADDITIONAL_DESCRIPTION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AV1">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAV1" runat="server" Width="200px" Text='<%# Eval("AV1") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AV2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAV2" runat="server" Width="200px" Text='<%# Eval("AV2") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AV3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAV3" runat="server" Width="200px" Text='<%# Eval("AV3") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AV4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAV4" runat="server" Width="200px" Text='<%# Eval("AV4") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AV5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAV5" runat="server" Width="200px" Text='<%# Eval("AV5") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BUDGET">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBudget" runat="server" Width="150px" onpaste="return false" Text='<%# Eval("BUDGET") %>'
                                onkeyup="checkDec(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REMARKS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="300px" TextMode="MultiLine" Rows="2"
                                Text='<%# Eval("REMARKS") %>' />
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
