<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="FDMEEReport.aspx.cs"
    Inherits="FINANCE_FDMEEReport" Title="CIPLTMS - FDMEE Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            display: inline-block;
            border-radius: 4px;
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            display: inline-block;
            border-radius: 4px;
            background-color: lightgray;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtPostingMonth.ClientID %>').value = document.getElementById('<%=hdPostingMonth.ClientID %>').value;
        }

        function clientChangedPostingMonth(sender, args) {
            document.getElementById('<%=hdPostingMonth.ClientID %>').value = document.getElementById('<%=txtPostingMonth.ClientID %>').value;
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
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Year-Period: </label>
                    <table width="100%">
                        <tr>
                            <td style="width:90%;">
                                <asp:TextBox ID="txtPostingMonth" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
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
                                    ToolTip="Year-Period" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Quarter:</label>
                    <asp:DropDownList ID="ddlQuarter" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="1" Value="1" />
                        <asp:ListItem Text="2" Value="2" />
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="4" Value="4" />
                    </asp:DropDownList>

                    <label>GL Account:</label>
                    <asp:TextBox ID="txtGLAccount" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>BSPL Type:</label>
                    <asp:DropDownList ID="ddlBSPLType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="ALL" Value="0" />
                        <asp:ListItem Text="B" Value="1" />
                        <asp:ListItem Text="P" Value="2" />
                    </asp:DropDownList>

                    <label>HFM Account:</label>
                    <asp:TextBox ID="txtHFMAccount" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Type:</label>
                    <asp:DropDownList ID="ddlType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="ALL" Value="0" />
                        <asp:ListItem Text="Actual" Value="1" />
                        <asp:ListItem Text="Budget" Value="2" />
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                    OnClick="btnExport_Click" />

            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>FDMEE Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>


            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvFDMEEList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvFDMEEList_RowDataBound" OnRowCommand="gvFDMEEList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID" ) %>' />
                                <asp:Label ID="lblYear" runat="server" Visible="false" Text='<%# Eval("YEAR" ) %>' />
                                <asp:Label ID="lblPeriod" runat="server" Visible="false" Text='<%# Eval("PERIOD" ) %>' />
                                <asp:Label ID="lblQuarter" runat="server" Visible="false" Text='<%# Eval("QUARTER" ) %>' />
                                <asp:Label ID="lblGLAccount" runat="server" Visible="false" Text='<%# Eval("GL_ACCOUNT" ) %>' />
                                <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION" ) %>' />
                                <asp:Label ID="lblBSPLType" runat="server" Visible="false" Text='<%# Eval("BSPL_TYPE" ) %>' />
                                <asp:Label ID="lblHFMAccount" runat="server" Visible="false" Text='<%# Eval("HFM_ACCOUNT" ) %>' />
                                <asp:Label ID="lblHFMAccountDesc" runat="server" Visible="false" Text='<%# Eval("HFM_ACCOUNT_DESC" ) %>' />
                                <asp:Label ID="lblType" runat="server" Visible="false" Text='<%# Eval("TYPE" ) %>' />
                                <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("CATEGORY" ) %>' />
                                <asp:Label ID="lblICP" runat="server" Visible="false" Text='<%# Eval("ICP" ) %>' />
                                <asp:Label ID="lblHFMCustom1" runat="server" Visible="false" Text='<%# Eval("HFM_CUSTOM1" ) %>' />
                                <asp:Label ID="lblHFMNewCustom4" runat="server" Visible="false" Text='<%# Eval("HFMNEW_CUSTOM4" ) %>' />
                                <asp:Label ID="lblAmount" runat="server" Visible="false" Text='<%# Eval("AMOUNT" ) %>' />
                                <asp:Label ID="lblSourceAmount" runat="server" Visible="false" Text='<%# Eval("SOURCE_AMOUNT" ) %>' />

                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                    ToolTip="Edit Posted Sales Gross Margin" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="YEAR" HeaderText="Year" />
                        <asp:BoundField DataField="PERIOD" HeaderText="Period" />
                        <asp:BoundField DataField="QUARTER" HeaderText="Quarter" />
                        <asp:BoundField DataField="GL_ACCOUNT" HeaderText="GL_Account" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                        <asp:BoundField DataField="BSPL_TYPE" HeaderText="BSPL_Type" />
                        <asp:BoundField DataField="HFM_ACCOUNT" HeaderText="HFM_Account" />
                        <asp:BoundField DataField="HFM_ACCOUNT_DESC" HeaderText="HFM_Account_Desc" />
                        <asp:BoundField DataField="TYPE" HeaderText="Type" />
                        <asp:BoundField DataField="CATEGORY" HeaderText="Category" />
                        <asp:BoundField DataField="ICP" HeaderText="ICP" />
                        <asp:BoundField DataField="HFM_CUSTOM1" HeaderText="HFM_Custom1" />
                        <asp:BoundField DataField="HFMNEW_CUSTOM4" HeaderText="HFMNew_Custom4" />
                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                        <asp:BoundField DataField="SOURCE_AMOUNT" HeaderText="Source_Amount" />
                    </Columns>
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



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegend" runat="server" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Year:</label>
                    <asp:TextBox ID="txtYearNew" runat="server"
                        onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control" />

                    <label>Period</label>
                    <asp:TextBox ID="txtPeriodNew" runat="server"
                        onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control" />

                    <label>Quarter:</label>
                    <asp:TextBox ID="txtQuarterNew" runat="server"
                        CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>GL Account:</label>
                    <asp:TextBox ID="txtGLAccountNew" runat="server"
                        CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>Description:</label>
                    <asp:TextBox ID="txtDescription" runat="server"
                        CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <label>BSPL Type:</label>
                    <asp:TextBox ID="txtBSPLTypeNew" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>HFM Account:</label>
                    <asp:TextBox ID="txtHFMAccountNew" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>HFM Account Desc.:</label>
                    <asp:TextBox ID="txtHFMAccountDesc" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Type:</label>
                    <asp:TextBox ID="txtType" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Category:</label>
                    <asp:TextBox ID="txtCategory" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />


                    <label>ICP:</label>
                    <asp:TextBox ID="txtICP" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>HFM Custom1:</label>
                    <asp:TextBox ID="txtHFMCustom1" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>HFM New Custom4:</label>
                    <asp:TextBox ID="txtHFMNewCustom4" runat="server"
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" />

                    <label>Amount:</label>
                    <asp:TextBox ID="txtAmount" runat="server"
                        onkeypress="return negNumberKeyWithDecimal(this, event);" CssClass="form-control" />

                    <label>Source Amount:</label>
                    <asp:TextBox ID="txtSourceAmount" runat="server"
                        CssClass="form-control"
                        onkeypress="return negNumberKeyWithDecimal(this, event);" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                    Width="100%" OnClick="btnUpdate_Click" />
            </div>

            <div class="full-width">
            </div>
        </div>

    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
