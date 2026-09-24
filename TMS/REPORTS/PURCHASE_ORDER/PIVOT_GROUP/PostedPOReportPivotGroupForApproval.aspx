<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PostedPOReportPivotGroupForApproval.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_PIVOT_GROUP_PostedPOReportPivotGroupForApproval" Title="Pivot Group Report For Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Posted PO Pivot Group For Approval</legend>
            <table width="80%">
                <tr>
                    <td>Pivot Group:</td>
                    <td>
                        <asp:TextBox ID="txtPivotGroup" runat="server" Width="100%"></asp:TextBox>
                    </td>

                    <td>&nbsp;</td>

                    <td>Pivot Group Desc:</td>
                    <td>
                        <asp:TextBox ID="txtPivotGroupDesc" runat="server" Width="100%"></asp:TextBox>
                    </td>

                    <td>&nbsp;</td>

                    <td>Posted Month:</td>
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
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Posting Month Calendar" Width="20px" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="chkSelectPostingMonth" runat="server" onchange="EnableDisableDates()" Checked="true" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>JOB No.:</td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>Report Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Width="100%"
                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Height="26px">
                            <asp:ListItem Text="Both" Value="0" />
                            <asp:ListItem Text="As Per PO Budget" Value="1" />
                            <asp:ListItem Text="As Per Sale Estimate Budget" Value="2" Selected="True" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                                        OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr runat="server" id="trAsPerPOBudget">
                    <td>Total PO Budget[INR]:</td>
                    <td>
                        <asp:TextBox ID="txtTotalPOBudgetINR" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>

                    <td>Total PO Value[INR]:</td>
                    <td>
                        <asp:TextBox ID="txtTotalPOValueINR1" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>

                    <td>Total Delta As Per PO Budget[INR]:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtTotalDeltaAsPerPOBudgetINR" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr runat="server" id="trAsPerSaleEstimateBudget">
                    <td>Total Sale Estimate Budget[INR]:</td>
                    <td>
                        <asp:TextBox ID="txtTotalSaleEstimateBudgetINR" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>

                    <td>Total PO Value[INR]:</td>
                    <td>
                        <asp:TextBox ID="txtTotalPOValueINR2" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>

                    <td>Total Delta As Per Sale Estimate Budget[INR]:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtTotalDeltaAsPerSaleEstimateBudgetINR" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Select/Deselect All:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_SelectedIndexChanged" />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rdApproverTypes" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                            OnSelectedIndexChanged="rdApproverTypes_SelectedIndexChanged">
                            <asp:ListItem Text="Procurement- Approval" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Project Manager- Approval" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Accounts- Approval" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnApprove" CssClass="button" Width="100%" runat="server"
                            Text="Approve" OnClick="btnApprove_Click" OnClientClick="return ConfirmApprove();" />
                    </td>
                </tr>
            </table>
            <table width="20%">
                <tr>
                    <td>JOB No.:
                    </td>
                    <td>
                        <asp:Label ID="lblJOBNo" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />
                    </td>
                    <td>&nbsp;</td>
                    <td>Posting Month:
                    </td>
                    <td>
                        <asp:Label ID="lblPostingMonth" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="DarkGreen" />
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
    <div align="center">
        <fieldset style="width: 85%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvPGReport" runat="server" CellPadding="4" ForeColor="#333333"
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

                        <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectProc" runat="server" Visible="true" />
                                <asp:CheckBox ID="chkSelectPM" runat="server" Visible="true" />
                                <asp:CheckBox ID="chkSelectAcc" runat="server" Visible="true" />
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


            <asp:Button ID="btnShowPOListPopup" runat="server" Style="display: none" />
            <ajax:ModalPopupExtender ID="mpePOList" runat="server" TargetControlID="btnShowPOListPopup"
                PopupControlID="pnlPOListPopup" CancelControlID="imgBtnPOListCancel" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlPOListPopup" runat="server" BackColor="White" Height="600px" Width="1000px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnPOListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>

                <div align="center" style="margin-top: 20px;">
                    <fieldset style="width: 80%;">
                        <legend style="text-align: center;">PO List</legend>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Pivot Group:</td>
                                <td>
                                    <asp:TextBox ID="txtPivotGroupInPOList" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td>Total Budget [INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalBudgetInPOList" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Total PO Value [INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValueINRInPOList" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td>Total Delta [INR]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalDeltaInPOList" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnExportPOList" CssClass="button" Width="100%" runat="server" Text="Export"
                                        OnClick="btnExportPOList_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblPOdetailRecords" runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvPOList" runat="server" CellPadding="4" ForeColor="#333333"
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
                    </fieldset>
                </div>

            </asp:Panel>



            <asp:Button ID="btnShowPOItemListPopup" runat="server" Style="display: none" />
            <ajax:ModalPopupExtender ID="mpePOItemList" runat="server" TargetControlID="btnShowPOItemListPopup"
                PopupControlID="pnlPOItemListPopup" CancelControlID="imgBtnPOItemListCancel" BackgroundCssClass="modalBackground">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlPOItemListPopup" runat="server" BackColor="White" Height="700px" Width="1100px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnPOItemListCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>

                <div align="center" style="margin-top: 20px;">
                    <fieldset style="width: 80%;">
                        <legend style="text-align: center;">PO Detail</legend>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>PO No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPONo" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>PO Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPODate" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Vendor:
                                </td>
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtVendorName" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td width="80%">
                                                <asp:TextBox ID="txtVendorCode" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Total PO Value:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOValue" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>Total Invoice Value:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalInvoiceValue" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Total PO Quantity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalPOQuantity" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>Total MRN Quantity:
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 45%;">
                                                <asp:TextBox ID="txtTotalMRNQuantity" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td style="width: 45%;">
                                                <asp:Button ID="btnExportPOItemList" CssClass="button" Width="100%" runat="server" Text="Export"
                                                    OnClick="btnExportPOItemList_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text="Records[0]" />
                        </legend>
                        <div style='overflow: auto; width: 100%; height: 300px; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvPOItemList" runat="server" CellPadding="4" ForeColor="#333333"
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
                    </fieldset>
                </div>
            </asp:Panel>

        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
