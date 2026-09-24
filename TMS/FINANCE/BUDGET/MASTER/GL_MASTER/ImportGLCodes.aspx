<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportGLCodes.aspx.cs"
    Inherits="FINANCE_BUDGET_MASTER_ImportGLCodes" Title="CIPLTMS- Import GL Master" %>

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

        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        function ValidatefileUploadBudget() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadCostCenter = document.getElementById('<%=fileUploadBudget.ClientID %>').value;
            var divfileUploadCostCenter = document.getElementById("divfileUploadCostCenter");
            var lblfileUploadCostCenter = document.getElementById('<%=lblfileUploadCostCenter.ClientID %>');

            if (fileUploadCostCenter == '') {
                document.getElementById('<%=fileUploadBudget.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadCostCenter.style.display = "block";
                lblfileUploadCostCenter.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadCostCenter.toLowerCase())) {
                    document.getElementById('<%=fileUploadBudget.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "block";
                    lblfileUploadCostCenter.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadBudget.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "none";
                    lblfileUploadCostCenter.innerHTML = "";
                    return false;
                }
            }
        }

        function Confirm() {
            var existedRecordsCount = document.getElementById('<%=hdExistedRecords.ClientID %>').value;
            var month = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
            document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;

            if (confirm("Would you like to import budget?")) {
                if (parseInt(existedRecordsCount) > 0) {
                    var confirm_value = document.createElement("input");
                    confirm_value.type = "hidden";
                    confirm_value.name = "Confirm Value";
                    if (confirm("There are " + existedRecordsCount + " records exist in system with " + month + " month.Do you want to replace?")) {
                        document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 1;
                    }
                }
            }
            else {
                return false;
                alert('no');
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function onCalendarShown() {
            var cal = $find("calendarPostingMonth");
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
            var cal = $find("calendarPostingMonth");
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
                    var cal = $find("calendarPostingMonth");
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

    <script type="text/Javascript">
        function checkDec1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;

            if (ex.test(el.value) == false) {

            }
            var row = el.parentNode.parentNode;
            var costCenterAmount;
            var directBilling;
            var partialBilling;

            if (row.cells[3].getElementsByTagName("input")[0].value != '')
                costCenterAmount = parseFloat(row.cells[3].getElementsByTagName("input")[0].value);
            else
                costCenterAmount = 0;

            if (row.cells[4].getElementsByTagName("input")[0].value != '')
                directBilling = parseFloat(row.cells[4].getElementsByTagName("input")[0].value);
            else
                directBilling = 0;

            if (row.cells[5].getElementsByTagName("input")[0].value != '' && row.cells[5].getElementsByTagName("input")[0].value != '-')
                partialBilling = parseFloat(row.cells[5].getElementsByTagName("input")[0].value);
            else
                partialBilling = 0;


            var netBilling = costCenterAmount + directBilling - partialBilling
            row.cells[6].getElementsByTagName("input")[0].value = parseFloat(netBilling);
        }
    </script>


    <script type="text/javascript">
        var GridId = "<%=gvBudget.ClientID %>";
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
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 50px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Import Budget</legend>
            <table width="100%">
                <tr>
                    <td>
                        <asp:HiddenField ID="hdExistedRecords" runat="server" />
                        <asp:HiddenField ID="hdReplacementFlag" runat="server" />
                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>Month:</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPostingMonth" runat="server" onkeyDown="javascript:preventInput(event);" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                    <ajax:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                        PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                        BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                    </ajax:CalendarExtender>
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                        FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Posting Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td>&nbsp;</td>

                    <td>Browse:</td>
                    <td style="width: 35%;">
                        <asp:FileUpload ID="fileUploadBudget" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadBudget();" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnGetBudgetDetails" CssClass="button" Width="100%" runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetBudgetDetails_Click" />
                    </td>

                    <td>&nbsp;</td>

                    <td>
                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                            Text="Save" OnClick="btnSave_Click"
                            OnClientClick="return Confirm();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="9">&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadCostCenter" style="display: none;">
                            <asp:Label ID="lblfileUploadCostCenter" runat="server" ForeColor="Red" />
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
        <asp:Panel ID="pnlMsg1" Visible="false" runat="server">
            <asp:Label ID="lblMsg1" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 85%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
            <%--<div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>--%>
            <div id="gridContainer" style='overflow-y: scroll; overflow-y: hidden; width: 100%; height: 430px; border: 1px solid lightgray;'>
                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                            HorizontalAlign="Center" OnRowDataBound="gvBudget_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="Sr_No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Year" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtYear" runat="server" Text='<%# Eval("YEAR" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Period" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPeriod" runat="server" Text='<%# Eval("PERIOD" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quarter" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuarter" runat="server" Text='<%# Eval("QUARTER" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OS_TMT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOSTMT" runat="server" Text='<%# Eval("OS_TMT" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GL Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                        <asp:Label ID="lblGLPID" runat="server" Visible="false" Text='<%# Eval("GL_FID") %>' />
                                        <asp:Label ID="lblGLTypeID" runat="server" Visible="false" Text='<%# Eval("GL_TYPE_FID") %>' />
                                        <asp:Label ID="lblGLSubTypeID" runat="server" Visible="false" Text='<%# Eval("GL_SUBTYPE_FID") %>' />

                                        <asp:DropDownList ID="ddlGLCode" Width="250px" Height="26px" runat="server"
                                            OnSelectedIndexChanged="ddlGLCode_SelectedIndexChanged" AutoPostBack="true"  Enabled="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GL Description" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGLDescription" runat="server" Text='<%# Eval("GL_DESCRIPTION" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GL Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGLType" runat="server" Text='<%# Eval("GL_TYPE" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="GL Sub-Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGLSubType" runat="server" Text='<%# Eval("GL_SUBTYPE" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="U7T" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtU7T" runat="server" Text='<%# Eval("U7T" ) %>' Width="100%"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textbox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Period Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodTypeID" runat="server" Visible="false" Text='<%# Eval("PERIOD_TYPE_FID") %>' />
                                        <asp:DropDownList ID="ddlPeriodType" Width="250px" Height="26px" runat="server"  Enabled="false"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("AMOUNT" ) %>' Width="100%"
                                            onkeypress="return inNumberKeyWithDecimal(this, event);"
                                            CssClass="textbox"></asp:TextBox>
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
