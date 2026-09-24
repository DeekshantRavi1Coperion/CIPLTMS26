<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="OneStreamPostedReport.aspx.cs"
    Inherits="FINANCE_ONESTREAM_OneStreamPostedReport" Title="One Stream- Posted Report" %>

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
        .myGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .myGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .myGrid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .myGrid .alt {
                background-color: #EFEFEF;
            }
    </style>


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

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>One Stream Posted Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Year-Period: </label>
                    <table width="100%">
                        <tr>
                            <td>
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
                        <asp:ListItem Text="Q-1" Value="1" />
                        <asp:ListItem Text="Q-2" Value="2" />
                        <asp:ListItem Text="Q-3" Value="3" />
                        <asp:ListItem Text="Q-4" Value="4" />
                    </asp:DropDownList>


                    <label>GL Account:</label>
                    <asp:TextBox ID="txtGLAccount" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClick="btnSearch_Click" />

                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />

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
                ID="gvOneStreamList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvOneStreamList_RowDataBound" OnRowCommand="gvOneStreamList_RowCommand">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID" ) %>' />
                            <asp:Label ID="lblYear" runat="server" Visible="false" Text='<%# Eval("F_YEAR" ) %>' />
                            <asp:Label ID="lblPeriod" runat="server" Visible="false" Text='<%# Eval("F_PERIOD" ) %>' />
                            <asp:Label ID="lblQuarter" runat="server" Visible="false" Text='<%# Eval("F_QUARTER" ) %>' />
                            <asp:Label ID="lblGLAccount" runat="server" Visible="false" Text='<%# Eval("FW" ) %>' />

                            <asp:Label ID="lblTMT" runat="server" Visible="false" Text='<%# Eval("TMT" ) %>' />
                            <asp:Label ID="lblAC" runat="server" Visible="false" Text='<%# Eval("AC" ) %>' />
                            <asp:Label ID="lblACT" runat="server" Visible="false" Text='<%# Eval("ACT" ) %>' />
                            <asp:Label ID="lblICT" runat="server" Visible="false" Text='<%# Eval("ICT" ) %>' />
                            <asp:Label ID="lblU3T" runat="server" Visible="false" Text='<%# Eval("U3T" ) %>' />
                            <asp:Label ID="lblU4T" runat="server" Visible="false" Text='<%# Eval("U4T" ) %>' />
                            <asp:Label ID="lblU7T" runat="server" Visible="false" Text='<%# Eval("U7T" ) %>' />
                            <asp:Label ID="lblRawAmount" runat="server" Visible="false" Text='<%# Eval("RAW_AMOUNT" ) %>' />
                            <asp:Label ID="lblConvertedAmount" runat="server" Visible="false" Text='<%# Eval("CONVERTED_AMOUNT" ) %>' />

                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                ToolTip="Edit Posted Sales Gross Margin" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TMT" HeaderText="TMT" />
                    <asp:BoundField DataField="AC" HeaderText="AC" />
                    <asp:BoundField DataField="ACT" HeaderText="ACT" />
                    <asp:BoundField DataField="ICT" HeaderText="ICT" />
                    <asp:BoundField DataField="U3T" HeaderText="U3T" />
                    <asp:BoundField DataField="U4T" HeaderText="U4T" />
                    <asp:BoundField DataField="U7T" HeaderText="U7T" />
                    <asp:BoundField DataField="FW" HeaderText="FW" />
                    <asp:BoundField DataField="RAW_AMOUNT" HeaderText="RAW_AMOUNT" />
                    <asp:BoundField DataField="CONVERTED_AMOUNT" HeaderText="CONVERTED_AMOUNT" />
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



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="650px" Width="950px"
        Style="display: block">
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
                        CssClass="form-control" Enabled="false" />

                    <label>Period</label>
                    <asp:TextBox ID="txtPeriodNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);"
                        CssClass="form-control" Enabled="false" />

                    <label>Quarter:</label>
                    <asp:TextBox ID="txtQuarterNew" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" Enabled="false" />

                    <label>TMT:</label>
                    <asp:TextBox ID="txtTMTNew" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" Enabled="false" />


                    <label>AC:</label>
                    <asp:TextBox ID="txtACNew" runat="server" CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" Enabled="false" />

                    <label>ACT:</label>
                    <asp:TextBox ID="txtACTNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />


                    <label>ICT:</label>
                    <asp:TextBox ID="txtICTNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />


                    <label>U3T:</label>
                    <asp:TextBox ID="txtU3TNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />


                    <label>U4T:</label>
                    <asp:TextBox ID="txtU4TNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />

                    <label>U7T:</label>
                    <asp:TextBox ID="txtU7TNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />

                    <label>FW:</label>
                    <asp:TextBox ID="txtFWNew" runat="server" 
                        onkeyDown="javascript:preventInput(event);" CssClass="form-control" Enabled="false" />


                    <label>Raw Amount:</label>
                    <asp:TextBox ID="txtRawAmountNew" runat="server" 
                        onkeypress="return negNumberKeyWithDecimal(this, event);" CssClass="form-control" />

                    <label>Converted Amount:</label>
                    <asp:TextBox ID="txtConvertedAmountNew" runat="server" 
                        onkeypress="return negNumberKeyWithDecimal(this, event);" CssClass="form-control" />


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
