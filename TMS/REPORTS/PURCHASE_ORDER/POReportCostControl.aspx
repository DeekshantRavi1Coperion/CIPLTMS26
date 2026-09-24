<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="POReportCostControl.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_POReportCostControl" Title="CIPLTMS- PO Report (Cost Control)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxtstatustext {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxdrawings {
            padding: 5px 2px 5px 5px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightgreen;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxleft {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxcenter {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxright {
            padding: 5px 5px 5px 5px;
            margin: 5px 5px 5px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxrightgreendouble {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            border-color: lightyellow;
            background-color: lightgreen;
            border: double;
            font-size: small;
        }

        .textboxrightgreen {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            border-color: lightyellow;
            background-color: lightgreen;
            border: thin;
            font-size: small;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdStartDateSearch.ClientID %>').value = document.getElementById('<%=txtStartDateSearch.ClientID %>').value;
            document.getElementById('<%=hdEndDateSearch.ClientID %>').value = document.getElementById('<%=txtEndDateSearch.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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

    <script type="text/javascript" language="javascript">

        function ValidateAll() {

            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            return true;
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

        function EnableAmount2() {
            var sign = document.getElementById('<%=ddlSign.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtAmountOne.ClientID %>').value = "";
            document.getElementById('<%=txtAmountTwo.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 5) {
                document.getElementById('<%=txtAmountTwo.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtAmountTwo.ClientID %>').disabled = true;
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
                <legend>PO Report (Cost Control):
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <div class="full-width">
                        <label>JOB No:</label>
                        <asp:TextBox ID="txtJOBNo" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control" />
                    </div>

                    <label>Reference:</label>
                    <asp:TextBox ID="txtPONo" runat="server"
                        CssClass="form-control" />

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorName" runat="server"
                        CssClass="form-control" />

                    <label>Vendor Code:</label>
                    <asp:TextBox ID="txtVendorCode" runat="server"
                        CssClass="form-control" />


                    <div class="full-width button-group">
                        <label>Costs Committed:</label>

                        <asp:DropDownList ID="ddlSign" runat="server"
                            CssClass="form-control"
                            onchange="EnableAmount2()">
                            <asp:ListItem Text=">=" Value="1"></asp:ListItem>
                            <asp:ListItem Text=">" Value="2"></asp:ListItem>
                            <asp:ListItem Text="<=" Value="3"></asp:ListItem>
                            <asp:ListItem Text="<" Value="4"></asp:ListItem>
                            <asp:ListItem Text="=" Value="5"></asp:ListItem>
                            <asp:ListItem Text="BETWEEN" Value="6"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:TextBox ID="txtAmountOne" runat="server"
                            CssClass="form-control"
                            onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>

                        <asp:TextBox ID="txtAmountTwo" runat="server"
                            CssClass="form-control"
                            Enabled="false"
                            onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                    </div>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="OPEN" Value="1"></asp:ListItem>
                        <asp:ListItem Text="CLOSE" Value="2"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Company:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control" />

                    <label>Exclude CIDF:</label>
                    <asp:CheckBox ID="chkExcludeCIDF" runat="server" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                    OnClick="btnExport_Click" />

            </div>

            <div class="full-width button-group">

                <label>Total Qty:</label>
                <asp:TextBox ID="txtTotalQty" runat="server" 
                     CssClass="form-control"
                    Enabled="false" Text="0"></asp:TextBox>

                <label>Total Costs Calculated:</label>
                <asp:TextBox ID="txtTotalCostsCalculated" runat="server" 
                    Enabled="false" Text="0" 
                     CssClass="form-control"></asp:TextBox>


                <label>Total Costs Committed:</label>
                <asp:TextBox ID="txtTotalCostsCommitted" runat="server" 
                    Enabled="false" Text="0" 
                     CssClass="form-control"></asp:TextBox>

                <label>Total Costs Deviation:</label>
                <asp:TextBox ID="txtTotalCostsDeviation" runat="server" 
                    Enabled="false" Text="0"  CssClass="form-control"></asp:TextBox>

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
                ID="gvPOReportSummery" runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvPOReportSummery_RowDataBound"
                OnRowCommand="gvPOReportSummery_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Visible="true" Text='<%# Eval("No") %>' />
                            <asp:Label ID="lblQty" runat="server" Visible="false" Text='<%# Eval("Qty") %>' />
                            <asp:Label ID="lblCostsCommitted" runat="server" Visible="false" Text='<%# Eval("Costs_Committed") %>' />
                            <asp:Label ID="lblCostsCalculated" runat="server" Visible="false" Text='<%# Eval("Costs_Calculated") %>' />
                            <asp:Label ID="lblCostsDeviation" runat="server" Visible="false" Text='<%# Eval("Cost_Deviation_Committed_Versus_Calculated") %>' />
                            <asp:Label ID="lblIsTotal" runat="server" Visible="false" Text='<%# Eval("IsTotal") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Date" DataField="Date" />
                    <asp:BoundField HeaderText="Job No" DataField="Job_No" />
                    <asp:BoundField HeaderText="Description" DataField="Description" />
                    <asp:BoundField HeaderText="Type" DataField="Type" />
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server" Width="100px" Enabled="false" Text='<%# Eval("Qty") %>'
                                CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="System" DataField="System" />
                    <asp:BoundField HeaderText="Item Group" DataField="Item_Group" />
                    <asp:BoundField HeaderText="Vendor Name" DataField="Vendor_Name" />
                    <asp:BoundField HeaderText="Procurement Type" DataField="Procurement_Type" />
                    <asp:TemplateField HeaderText="Costs Calculated">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCostsCalculated" runat="server" Width="150px" Enabled="false" Text='<%# Eval("Costs_Calculated") %>'
                                CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Costs Expected" DataField="Costs_Expected" />

                    <asp:TemplateField HeaderText="Costs Committed">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCostsCommitted" runat="server" Width="150px" Enabled="false" Text='<%# Eval("Costs_Committed") %>'
                                CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="PO No." DataField="PO_NO" />
                    <asp:BoundField HeaderText="Revision" DataField="Revision" />

                    <asp:TemplateField HeaderText="Cost Deviation (Committed Versus Calculated)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCostDeviationCommittedVersusCalculated" runat="server" Width="95%" Enabled="false" Text='<%# Eval("Cost_Deviation_Committed_Versus_Calculated") %>'
                                CssClass="textboxright"></asp:TextBox>
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



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            class="popup-iframe"
            id="iframePOHeaderDetail"
            runat="server"></iframe>
    </asp:Panel>





    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
