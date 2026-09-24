<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ProjectPivotGroupMonthlyPostedReport.aspx.cs" Inherits="PROJECT_MGMT_ProjectPivotGroupMonthlyPostedReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>


    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }

        function check() {
            var chkall = document.getElementById('<%=chkAll.ClientID %>').checked;
            var postingMonth = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
            if (chkall == true) {
                document.getElementById('<%=hdPostingMonth.ClientID %>').value = "";
                document.getElementById('<%=txtPostingMonth.ClientID %>').value = "";
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

                    <label>JOB No.</label>
                    <asp:DropDownList ID="ddlJOBNo" runat="server" 
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlJOBNo_SelectedIndexChanged" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Revision No.</label>
                    <asp:DropDownList ID="ddlRevisionNo" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" 
                        CssClass="form-control"/>

                    <label>Posting Month</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" ReadOnly="true" 
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                <asp:CalendarExtender ID="calendarPostingMonth" runat="server" 
                                    OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" 
                                    OnClientDateSelectionChanged="clientChangedPostingMonth">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                    TargetControlID="txtPostingMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Posting Month Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <asp:CheckBox ID="chkAll" runat="server" Text="All Months" onclick="check()" />
                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" 
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="Saving" Value="Saving" />
                        <asp:ListItem Text="Yet To Be Incurred" Value="Yet To Be Incurred" />
                        <asp:ListItem Text="Need To Be Chceked" Value="Need To Be Chceked" />
                    </asp:DropDownList>


                    <div class="full-width button-group">
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />

                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />
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
                ID="gvProjectPivotGroupList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="15" Width="100%"
                HorizontalAlign="Center" AllowPaging="True" OnRowDataBound="gvProjectPivotGroupList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="REVISION_NO" HeaderText="REVISION_NO" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="PIVOT_GROUP" HeaderText="PIVOT_GROUP" />
                    <asp:BoundField DataField="GROSS_MARGIN_LINE" HeaderText="GROSS_MARGIN_LINE" />
                    <asp:BoundField DataField="BUDGTED_AMOUNT" HeaderText="BUDGTED_AMOUNT" />
                    <asp:BoundField DataField="ACTUAL_AMOUNT" HeaderText="ACTUAL_AMOUNT(FACT)" />
                    <asp:TemplateField HeaderText="DIFFERENCE">
                        <ItemTemplate>
                            <asp:Label ID="lblDifference" runat="server" Visible="true" Text='<%# Eval("DIFFERENCE_AMOUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                    <asp:BoundField DataField="POSTED_MONTH" HeaderText="POSTED_MONTH" />
                    <asp:BoundField DataField="REMARK" HeaderText="REMARK" />
                    <asp:BoundField DataField="UDF1" HeaderText="UDF1" />
                    <asp:BoundField DataField="UDF2" HeaderText="UDF2" />
                    <asp:BoundField DataField="UDF3" HeaderText="UDF3" />
                    <asp:BoundField DataField="UDF4" HeaderText="UDF4" />
                    <asp:BoundField DataField="UDF5" HeaderText="UDF5" />
                    <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
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
