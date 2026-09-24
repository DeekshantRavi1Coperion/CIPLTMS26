<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddCurrentFXRate.aspx.cs" Inherits="REPORTS_SALE_ORDER_AddCurrentFXRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
        }

        function ValidatePostingMonth() {
            var PostingMonth = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
            if (PostingMonth == '') {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPostingMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidatePostingMonth()) { return false; }
            return true;
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

    <script type="text/javascript">
        function inNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Add Current FX Rate:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Posting Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPostingMonth" runat="server" 
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:HiddenField ID="hdPostingMonth" runat="server" />
                                <asp:CalendarExtender ID="calendarPostingMonth" runat="server" OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnPostingMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarPostingMonth" TargetControlID="txtPostingMonth" OnClientDateSelectionChanged="clientChangedPostingMonth">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPostingMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnPostingMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Posting Month Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                    <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save"
                        OnClick="btnSave_Click" />

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
            <asp:UpdatePanel runat="server" ID="uppanel">
                <ContentTemplate>
                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvCurrencyList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvCurrencyList_RowDataBound">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="CURRENCY_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrencyCode" runat="server" Text='<%# Eval("CODE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CREATED_DATED[FACT]">
                                <ItemTemplate>
                                    <asp:Label ID="lblDated" runat="server" Text='<%# Eval("DATED") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CURRENT_MONTH">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrentMonth" runat="server" Text='<%# Eval("CURRENT_MONTH") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CURRENT_FX_RATE">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFxRate" 
                                        CssClass="form-control"
                                        runat="server" Text='<%# Eval("CURRENT_FX_RATE") %>'
                                        onkeypress="return inNumberKey(this, event);" />
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
