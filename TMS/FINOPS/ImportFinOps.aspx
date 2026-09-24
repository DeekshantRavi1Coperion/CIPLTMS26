<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportFinOps.aspx.cs"
    Inherits="FINOPS_ImportFinOps" Title="CIPLTMS- Import FinOps" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
            text-align: left;
            border-radius: 0px;
            background-color: transparent;
            /*background-color: #D8D8D8;*/
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 0px; /*4px*/
            background-color: transparent;
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


        function ValidatefileUploadfinOps() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadfinOps = document.getElementById('<%=fileUploadfinOps.ClientID %>').value;
            var divfileUploadfinOps = document.getElementById("divfileUploadfinOps");
            var lblfileUploadfinOps = document.getElementById('<%=lblfileUploadfinOps.ClientID %>');

            if (fileUploadfinOps == '') {
                document.getElementById('<%=fileUploadfinOps.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadfinOps.style.display = "block";
                lblfileUploadfinOps.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadfinOps.toLowerCase())) {
                    document.getElementById('<%=fileUploadfinOps.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadfinOps.style.display = "block";
                    lblfileUploadfinOps.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadfinOps.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadfinOps.style.display = "none";
                    lblfileUploadfinOps.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;

            if (ValidatefileUploadfinOps()) { return false; }

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
        var GridId = "<%=gvFinOps.ClientID %>";
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

    <script type="text/javascript" language="javascript">
        function onCalendarShown() {
            var cal = $find("calendarMonth");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendarMonth");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarMonth");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 50px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Import Fin Ops</legend>
            <table width="100%">
                <tr>
                    <td>
                        <asp:HiddenField ID="hdExistedRecords" runat="server" />
                        <asp:HiddenField ID="hdGVRowCount" runat="server" />
                        <asp:HiddenField ID="hdReplacementFlag" runat="server" />
                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>Browse:</td>
                    <td style="width: 35%;">
                        <asp:FileUpload ID="fileUploadfinOps" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadfinOps();" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnGetFinOpsFile" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetFinOpsFile_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                            Text="Save" OnClick="btnSave_Click" OnClientClick="Confirm();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="9">&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadfinOps" style="display: none;">
                            <asp:Label ID="lblfileUploadfinOps" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td>&nbsp;
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
    <br />
    <div align="center">
        <fieldset style="width: 85%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
            <div id="gridContainer" style='overflow-y: scroll; overflow-y: hidden; width: 100%; height: 430px; border: 1px solid lightgray;'>
                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView ID="gvFinOps" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                            HorizontalAlign="Center" OnRowDataBound="gvFinOps_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgAlreadyExisted" runat="server" Visible="false" ImageUrl="~/Images/redlight1.jpg" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tax Invoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinOpsID" runat="server" Visible="false" Text='<%# Eval("FIN_OPS_ID" ) %>' />
                                        <asp:Label ID="lblTaxInvoiceNo" runat="server" Visible="true" Text='<%# Eval("TAX_INVOICE_NO" ) %>' />
                                        <asp:Label ID="lblSRNo" runat="server" Visible="false" Text='<%# Eval("SR_NO" ) %>' />
                                        <asp:Label ID="lblValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Tax Invoice Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxInvoiceDate" runat="server" Visible="false" Text='<%# Eval("TAX_INVOICE_DATE" ) %>' />

                                        <asp:TextBox ID="txtTaxInvoiceDate" runat="server" Enabled="false"
                                            Text='<%# Eval("TAX_INVOICE_DATE") %>' Width="100px" CssClass="textbox"
                                            OnTextChanged="txtTaxInvoiceDate_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:HiddenField ID="hdTaxInvoiceDate" runat="server" />
                                        <ajax:CalendarExtender ID="calendarTaxInvoiceDate" PopupButtonID="imgbtnTaxInvoiceDate"
                                            runat="server" TargetControlID="txtTaxInvoiceDate" Format="dd-MMM-yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:ImageButton ID="imgbtnTaxInvoiceDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Tax Invoice Date Calendar" Width="20px" Visible="false" />

                                        <asp:Label ID="lblTaxInvoiceDateValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblMonth" runat="server" Visible="false" Text='<%# Eval("MONTH" ) %>' />--%>

                                        <asp:TextBox ID="txtMonth" runat="server" Text='<%# Eval("MONTH") %>' Width="100px"
                                            Enabled="false" CssClass="textbox1"
                                            OnTextChanged="txtMonth_OnTextChanged" AutoPostBack="true"></asp:TextBox>

                                        <asp:Label ID="lblMonthValidationFlag" runat="server" Visible="false" Text='0' />


                                        <%-- <asp:TextBox ID="txtMonth" runat="server" Text='<%# Eval("MONTH") %>'
                                          Enabled="false" Width="100px"></asp:TextBox>
                                        <asp:HiddenField ID="hdMonth" runat="server" />
                                        <ajax:CalendarExtender ID="calendarMonth" runat="server" OnClientHidden="onCalendarHidden"
                                            PopupButtonID="imgbtnMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                            BehaviorID="calendarMonth" TargetControlID="txtMonth">
                                        </ajax:CalendarExtender>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMonth"
                                            FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />

                                        <asp:ImageButton ID="imgbtnMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Month Calendar" Width="20px" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJobNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostCenter" runat="server" Visible="true" Text='<%# Eval("COST_CENTER" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeID" runat="server" Visible="false" Text='<%# Eval("TYPE_ID" ) %>' />
                                        <asp:Label ID="lblType" runat="server" Visible="false" Text='<%# Eval("TYPE" ) %>' />

                                        <asp:DropDownList ID="ddlType" runat="server" Width="100px"
                                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" />

                                        <asp:Label ID="lblTypeValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cost Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostTypeID" runat="server" Visible="false" Text='<%# Eval("COST_TYPE_ID" ) %>' />
                                        <asp:Label ID="lblCostType" runat="server" Visible="false" Text='<%# Eval("COST_TYPE" ) %>' />

                                        <asp:DropDownList ID="ddlCostType" runat="server" Width="100px"
                                            OnSelectedIndexChanged="ddlCostType_SelectedIndexChanged" AutoPostBack="true" />

                                        <asp:Label ID="lblCostTypeValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BU">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBU" runat="server" Visible="true" Text='<%# Eval("BU" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount1" runat="server" Text='<%# Eval("AMOUNT1") %>' Width="100px"
                                            Enabled="false" CssClass="textbox1"
                                            AutoPostBack="true" OnTextChanged="txtAmount1_OnTextChanged"></asp:TextBox>

                                        <asp:Label ID="lblAmount1ValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hours">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtHours" runat="server"
                                            Width="100px" Text='<%# Eval("HOURS" ) %>' CssClass="textbox1"
                                            AutoPostBack="true" OnTextChanged="txtHours_OnTextChanged" />
                                        <%-- <ajax:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtHours"
                                            Mask="99999:99" MaskType="Time" CultureName="en-us" MessageValidatorTip="true" runat="server">
                                        </ajax:MaskedEditExtender>
                                           meta:resourcekey="txtHoursResource"--%>

                                        <asp:Label ID="lblHoursValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Panelty Clause">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaneltyClause" runat="server" Visible="true" Text='<%# Eval("PANELTY_CLAUSE" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Term">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPaymentTerm" runat="server" Visible="true" Text='<%# Eval("PAYMENT_TERM" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Po No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoNo" runat="server" Visible="true" Text='<%# Eval("PO_NO" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contigency Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContigencyTypeID" runat="server" Visible="false" Text='<%# Eval("CONTIGENCY_TYPE_ID" ) %>' />
                                        <asp:Label ID="lblContigencyType" runat="server" Visible="false" Text='<%# Eval("CONTIGENCY_TYPE" ) %>' />

                                        <asp:DropDownList ID="ddlContigencyType" runat="server" Width="100%"
                                            OnSelectedIndexChanged="ddlContigencyType_SelectedIndexChanged" AutoPostBack="true" />

                                        <asp:Label ID="lblContigencyTypeValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Is VPOC Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsVPOCOrder" runat="server" Visible="false" Text='<%# Eval("IS_VPOC_ORDER" ) %>' />

                                        <asp:DropDownList ID="ddlIsVPOCOrder" runat="server" Width="100%"
                                            OnSelectedIndexChanged="ddlIsVPOCOrder_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="0" />
                                            <asp:ListItem Text="Yes" Value="1" />
                                            <asp:ListItem Text="No" Value="2" />
                                        </asp:DropDownList>

                                        <asp:Label ID="lblIsVPOCOrderValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delivery Month">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDeliveryMonth" runat="server" Text='<%# Eval("DELIVERY_MONTH") %>' Width="100px"
                                            Enabled="false" CssClass="textbox1"
                                            AutoPostBack="true" OnTextChanged="txtDeliveryMonth_OnTextChanged"></asp:TextBox>

                                        <asp:Label ID="lblDeliveryMonthValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus1" runat="server" Visible="false" Text='<%# Eval("STATUS1" ) %>' />
                                        <asp:Label ID="lblStatus1ID" runat="server" Visible="false" Text='<%# Eval("STATUS1_ID" ) %>' />

                                        <asp:DropDownList ID="ddlStatus1" runat="server" Width="100px"
                                            OnSelectedIndexChanged="ddlStatus1_SelectedIndexChanged" AutoPostBack="true" />

                                        <asp:Label ID="lblStatus1ValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount2" runat="server" Text='<%# Eval("AMOUNT2") %>' Width="100px"
                                            Enabled="false" CssClass="textbox1"
                                            AutoPostBack="true" OnTextChanged="txtAmount2_OnTextChanged"></asp:TextBox>

                                        <asp:Label ID="lblAmount2ValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus2" runat="server" Visible="false" Text='<%# Eval("STATUS2" ) %>' />
                                        <asp:Label ID="lblStatus2ID" runat="server" Visible="false" Text='<%# Eval("STATUS2_ID" ) %>' />

                                        <asp:DropDownList ID="ddlStatus2" runat="server" Width="100px"
                                            OnSelectedIndexChanged="ddlStatus2_SelectedIndexChanged" AutoPostBack="true" />

                                        <asp:Label ID="lblStatus2ValidationFlag" runat="server" Visible="false" Text='0' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Visible="true" Text='<%# Eval("ITEM_CODE" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Visible="true" Text='<%# Eval("ITEM_DESC" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemGroup" runat="server" Visible="true" Text='<%# Eval("ITEM_GROUP" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Subgroup">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSubgroup" runat="server" Visible="true" Text='<%# Eval("ITEM_SUBGROUP" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Pivot Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemPivotGroup" runat="server" Visible="true" Text='<%# Eval("ITEM_PIVOT_GROUP" ) %>' />
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
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
