<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PostedPOReportPivotGroupReport.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_PIVOT_GROUP_PostedPOReportPivotGroupReport" Title="Post Pivot Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <style type="text/css">
        .textbox {
            width: 120px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
        }

        .textboxcenter {
            width: 70px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxright {
            width: 120px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxrightsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }
    </style>

    <script type="text/javascript">

        function pageLoad() {
            if (document.getElementById('<%=chkSelectPostingMonth.ClientID %>').checked) {
                document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtPostingMonth.ClientID %>').value = "";
            }

        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }
    </script>

    <script type="text/Javascript">       
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">


        function ValidateJOBNo() {
            var jobNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (jobNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            if (ValidateJOBNo()) {
                return false;
            }
            return true;
        }

        function ConfirmApprove() {
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;
            var GVRowCountNew = 0;

            if (GVRowCount == '')
                GVRowCountNew = 0;
            else
                GVRowCountNew = parseInt(GVRowCount);

            if (GVRowCountNew > 0) {

                if (confirm("Would you like to approve pivot group?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
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

    </script>

    <script type="text/Javascript">
        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectPostingMonth.ClientID %>').checked;
            if (chk == true) {
                document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtPostingMonth.ClientID %>').value = "";
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdGVRowCount" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Posted PO Pivot Group Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Pivot Group:</label>
                    <asp:TextBox ID="txtPivotGroup" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Pivot Group Desc:</label>
                    <asp:TextBox ID="txtPivotGroupDesc" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Posted Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server"
                                    onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                <ajax:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth"
                                    OnClientDateSelectionChanged="clientChangedPostingMonth">
                                </ajax:CalendarExtender>
                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Posting Month Calendar" Width="20px" /></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:CheckBox ID="chkSelectPostingMonth" runat="server"
                                                onchange="EnableDisableDates()" Checked="true" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Report Type:</label>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                        CssClass="form-control">
                        <asp:ListItem Text="Both" Value="0" />
                        <asp:ListItem Text="As Per PO Budget" Value="1" />
                        <asp:ListItem Text="As Per Sale Estimate Budget" Value="2" Selected="True" />
                    </asp:DropDownList>

                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />


                    <div class="full-width button-group">
                        <table width="100%">
                            <tr runat="server" id="trAsPerPOBudget">
                                <td>Total PO Budget[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total PO Value[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValueINR1" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total Delta As Per PO Budget[INR]:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtTotalDeltaAsPerPOBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>


                            <tr runat="server" id="trAsPerSaleEstimateBudget">
                                <td>Total Sale Estimate Budget[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalSaleEstimateBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total PO Value[INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValueINR2" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>

                                <td>Total Delta As Per Sale Estimate Budget[INR]:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtTotalDeltaAsPerSaleEstimateBudgetINR" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <label>JOB No.:</label>
                    <asp:Label ID="lblJOBNo" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />

                    <label>Posting Month:</label>
                    <asp:Label ID="lblPostingMonth" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />


                </div>
            </fieldset>
            <div class="full-width button-group">
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
                ID="gvPGReport" runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvPGReport_RowDataBound"
                OnRowCommand="gvPGReport_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordIDInList" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                            <asp:Label ID="lblJOBNoInList" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblTableNameInList" runat="server" Visible="false" Text='<%# Eval("TABLE_NAME") %>' />
                            <asp:TextBox ID="txtSrNoInList" runat="server" Text='<%# Eval("SR_NO") %>'
                                CssClass="textboxcenter" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Zoom" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetailInList" Height="20px" Width="20px" CommandArgument="ViewDETAIL" runat="server"
                                ImageUrl="~/Images/viewdetails.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Posted Month">
                        <ItemTemplate>
                            <asp:Label ID="lblPostedMonthInList" runat="server" Visible="true" Text='<%# Eval("POSTING_MONTH") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pivot Group Desc">
                        <ItemTemplate>
                            <asp:Label ID="lblPivotGroupDescInList" runat="server" Visible="true" Text='<%# Eval("PIVOT_GROUP_DESC") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pivot Group">
                        <ItemTemplate>
                            <asp:Label ID="lblPivotGroupInList" runat="server" Visible="true" Text='<%# Eval("PIVOT_GROUP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO Budget">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTotalBudgetINR" runat="server" Visible="true" Text='<%# Eval("PO_BUDGET") %>' />--%>
                            <asp:TextBox ID="txtTotalBudgetINRInList" runat="server" Text='<%# Eval("PO_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sale Estimate Budget">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblSaleEstimateBudgetINR" runat="server" Visible="true" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>' />--%>
                            <asp:TextBox ID="txtSaleEstimateBudgetINRInList" runat="server" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO Value[INR]">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTotalPOValueINR" runat="server" Visible="true" Text='<%# Eval("PO_VALUE_INR") %>' />--%>
                            <asp:TextBox ID="txtTotalPOValueINRInList" runat="server" Text='<%# Eval("PO_VALUE_INR") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delta As Per PO Budget">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTotalDeltaAsPerPOBudgetINR" runat="server" Visible="true" Text='<%# Eval("DELTA_AS_PER_PO_BUDGET") %>' />--%>
                            <asp:TextBox ID="txtTotalDeltaAsPerPOBudgetINRInList" runat="server" Text='<%# Eval("DELTA_AS_PER_PO_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delta As Per Sale Estimate Budget">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblTotalDeltaAsPerSaleEstimateBudgetINR" runat="server" Visible="true" Text='<%# Eval("DELTA_AS_PER_SALE_ESTIMATE_BUDGET") %>' />--%>
                            <asp:TextBox ID="txtTotalDeltaAsPerSaleEstimateBudgetINRInList" runat="server" Text='<%# Eval("DELTA_AS_PER_SALE_ESTIMATE_BUDGET") %>'
                                CssClass="textboxright" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusInList" runat="server" Visible="true" Text='<%# Eval("PIVOT_GROUP_DESC") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Pending Cost">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPendingCostInList" runat="server" Text='<%# Eval("PENDING_COST") %>'
                                CssClass="textboxright" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Saving">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSavingInList" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("SAVING_COST") %>' CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Saving Of Original Estimate[%]">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSavingOfOriginalEstimatePercentageInList" runat="server"
                                Text='<%# Eval("SAVING_OF_ORIGINAL_ESTIMATE_PERCENTAGE") %>' CssClass="textboxrightsmall"
                                onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VPOC" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lblIsVPOCInList" runat="server" Visible="false" Text='<%# Eval("IS_VPOC") %>' />
                            <asp:CheckBox ID="chkIsVPOCInList" runat="server" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Likely Delivery Date">
                        <ItemTemplate>
                            <asp:Label ID="lblLikelyDeliveryDateInList" runat="server" Visible="true" Text='<%# Eval("LIKELY_DELIVERY_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plan Date Of Procurement">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanDateOfProcurementInList" runat="server" Visible="true" Text='<%# Eval("PLAN_DATE_OF_PROCUREMENT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Proc- Approved By">
                        <ItemTemplate>
                            <asp:Label ID="lblProcApprovedByIDInList" runat="server" Visible="false" Text='<%# Eval("PROC_APPROVED_BY_ID") %>' />
                            <asp:Label ID="lblProcApprovedByInList" runat="server" Visible="true" Text='<%# Eval("PROC_APPROVED_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Proc- Approved On">
                        <ItemTemplate>
                            <asp:Label ID="lblProcApprovedByOnInList" runat="server" Visible="true" Text='<%# Eval("PROC_APPROVED_ON") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="PM- Approved By">
                        <ItemTemplate>
                            <asp:Label ID="lblPMApprovedByIDInList" runat="server" Visible="false" Text='<%# Eval("PM_APPROVED_BY_ID") %>' />
                            <asp:Label ID="lblPMApprovedByInList" runat="server" Visible="true" Text='<%# Eval("PM_APPROVED_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PM- Approved On">
                        <ItemTemplate>
                            <asp:Label ID="lblPMApprovedByOnInList" runat="server" Visible="true" Text='<%# Eval("PM_APPROVED_ON") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Acc- Approved By">
                        <ItemTemplate>
                            <asp:Label ID="lblAccApprovedByIDInList" runat="server" Visible="false" Text='<%# Eval("ACC_APPROVED_BY_ID") %>' />
                            <asp:Label ID="lblAccApprovedByInList" runat="server" Visible="true" Text='<%# Eval("ACC_APPROVED_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acc- Approved On">
                        <ItemTemplate>
                            <asp:Label ID="lblAccApprovedByOnInList" runat="server" Visible="true" Text='<%# Eval("ACC_APPROVED_ON") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

        </div>

    </div>


    <asp:Button ID="btnShowPOListPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePOList" runat="server" TargetControlID="btnShowPOListPopup"
        PopupControlID="pnlPOListPopup" CancelControlID="imgBtnPOListCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPOListPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPOListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>PO List:
                        <asp:Label ID="lblPOdetailRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">


                        <label>Pivot Group:</label>
                        <asp:TextBox ID="txtPivotGroupInPOList" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Budget [INR]:</label>
                        <asp:TextBox ID="txtTotalBudgetInPOList" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total PO Value [INR]:</label>
                        <asp:TextBox ID="txtTotalPOValueINRInPOList" runat="server" CssClass="form-control" Enabled="false" />


                        <label>Total Delta [INR]:</label>
                        <asp:TextBox ID="txtTotalDeltaInPOList" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnExportPOList" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportPOList_Click" />

                </div>
            </div>

            <div class="employee-grid-container">

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPOList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPOList_RowDataBound"
                    OnRowCommand="gvPOList_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ZOOM">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/viewdetails.png" />
                                <asp:Label ID="lblAmount" runat="server" Visible="false" Text='<%# Eval("PO_VALUE_INR") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblDocClass" runat="server" Visible="false" Text='<%# Eval("DOC_CLASS") %>' />

                                <asp:Label ID="lblBudget" runat="server" Visible="false" Text='<%# Eval("BUDGET") %>' />
                                <asp:Label ID="lblDelta" runat="server" Visible="false" Text='<%# Eval("DELTA") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="PIVOT_GROUP" DataField="PIVOT_GROUP" />
                        <asp:BoundField HeaderText="PO_NO" DataField="PO_NO" />
                        <asp:BoundField HeaderText="PO_DATE" DataField="PO_DATE" />
                        <asp:BoundField HeaderText="EXPECTED_DATE" DataField="EXPECTED_DATE" />
                        <asp:BoundField HeaderText="VENDOR_CODE" DataField="VENDOR_CODE" />
                        <asp:BoundField HeaderText="VENDOR_NAME" DataField="VENDOR_NAME" />

                        <asp:BoundField HeaderText="PO_VALUE[FC]" DataField="PO_VALUE" />
                        <asp:BoundField HeaderText="CURR_DESC" DataField="CURR_DESC" />
                        <asp:BoundField HeaderText="CURR_RATE" DataField="CURR_RATE" />

                        <asp:BoundField HeaderText="BUDGET" DataField="BUDGET" />
                        <asp:BoundField HeaderText="PO_VALUE_INR" DataField="PO_VALUE_INR" />
                        <asp:BoundField HeaderText="DELTA" DataField="DELTA" />

                        <asp:BoundField HeaderText="DOC_CLASS" DataField="DOC_CLASS" />
                        <asp:BoundField HeaderText="PO_STATUS" DataField="PO_STATUS" />
                        <asp:BoundField HeaderText="REVISION" DataField="REVISION" />

                        <asp:BoundField HeaderText="LOCATION" DataField="LOCATION" />
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>

        </div>

    </asp:Panel>


    <asp:Button ID="btnShowPOItemListPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePOItemList" runat="server" TargetControlID="btnShowPOItemListPopup"
        PopupControlID="pnlPOItemListPopup" CancelControlID="imgBtnPOItemListCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPOItemListPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnPOItemListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>PO Detail:
                    <asp:Label ID="Label2" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>PO No.:</label>
                        <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" Enabled="false" />

                        <label>PO Date:</label>
                        <asp:TextBox ID="txtPODate" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Vendor:</label>
                        <table width="100%">
                            <tr>
                                <td width="80%">
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control" Enabled="false" />
                                </td>
                            </tr>
                        </table>

                        <label>Total PO Value:</label>
                        <asp:TextBox ID="txtTotalPOValue" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total Invoice Value:</label>
                        <asp:TextBox ID="txtTotalInvoiceValue" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total PO Quantity:</label>
                        <asp:TextBox ID="txtTotalPOQuantity" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Total MRN Quantity:</label>
                        <asp:TextBox ID="txtTotalMRNQuantity" runat="server" CssClass="form-control" Enabled="false" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnExportPOItemList" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExportPOItemList_Click" />

                </div>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="Label1" runat="server" Text="Records[0]" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPOItemList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="true" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPOItemList_RowDataBound  ">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPOValue" runat="server" Visible="false" Text='<%# Eval("PO_VALUE") %>' />
                                <asp:Label ID="lblPOQuantity" runat="server" Visible="false" Text='<%# Eval("PO_QUANTITY") %>' />
                                <asp:Label ID="lblMRNQuantity" runat="server" Visible="false" Text='<%# Eval("MRN_QUANTITY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>

        </div>

    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
