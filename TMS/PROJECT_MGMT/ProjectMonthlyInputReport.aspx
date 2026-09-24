<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ProjectMonthlyInputReport.aspx.cs" Inherits="PROJECT_MGMT_ProjectMonthlyInputReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
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
            document.getElementById('<%=txtMonth.ClientID %>').value = document.getElementById('<%=hdMonth.ClientID %>').value;
        }

        function clientChangedMonth(sender, args) {
            document.getElementById('<%=hdMonth.ClientID %>').value = document.getElementById('<%=txtMonth.ClientID %>').value;
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Project Monthly Input Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>JOB No</label>
                    <asp:DropDownList ID="ddlJOBNo" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Month</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtMonth" runat="server"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdMonth" runat="server" />
                                <asp:CalendarExtender ID="calendarMonth" runat="server" OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarMonth" TargetControlID="txtMonth" OnClientDateSelectionChanged="clientChangedMonth">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Month Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>
                    <div class="full-width button-group">

                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" />


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
                ID="gvProjectMonthlyInputReport" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvProjectMonthlyInputReport_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="MONTH" HeaderText="MONTH" />
                    <asp:BoundField DataField="VPOC" HeaderText="VPOC" />
                    <asp:BoundField DataField="ICPOC" HeaderText="ICPOC" />
                    <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" />
                    <asp:BoundField DataField="ENGINEERING" HeaderText="ENGINEERING" />
                    <asp:BoundField DataField="TRAVELLING" HeaderText="TRAVELLING" />
                    <asp:BoundField DataField="OTHER_COST" HeaderText="OTHER_COST" />
                    <asp:BoundField DataField="WARRANTY" HeaderText="WARRANTY" />
                    <asp:BoundField DataField="COMMISSION" HeaderText="COMMISSION" />
                    <asp:BoundField DataField="ROYALTY" HeaderText="ROYALTY" />
                    <asp:BoundField DataField="LATE_DELIVERY" HeaderText="LATE_DELIVERY" />
                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
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
