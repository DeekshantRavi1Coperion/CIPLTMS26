<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="POReportSummaryForcast.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_POReportSummaryForcast" Title="PO Header Summary Forcast Report" %>

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

    <script type="text/javascript" language="javascript">

        function pageLoad() {
            document.getElementById('<%=txtPOMonth.ClientID %>').value = document.getElementById('<%=hdPOMonth.ClientID %>').value;
        }

        function clientChangedPOMonth(sender, args) {
            document.getElementById('<%=hdPOMonth.ClientID %>').value = document.getElementById('<%=txtPOMonth.ClientID %>').value;
        }

        function ValidatePOMonth() {
            var POMonth = document.getElementById('<%=txtPOMonth.ClientID %>').value;
            if (POMonth == '') {
                document.getElementById('<%=txtPOMonth.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPOMonth.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            var check = true;
            if (ValidatePOMonth()) { return false; }
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

    <script type="text/javascript" language="javascript">
        function onCalendarShown() {
            var cal = $find("calendarPOMonth");
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
            var cal = $find("calendarPOMonth");
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
                    var cal = $find("calendarPOMonth");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>

    <script type="text/javascript">
        var GridId = "<%=gvPOReportSummery.ClientID %>";
        var ScrollHeight = 400;
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
                <legend>PO Summary Forcast(Header):
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>PO Month:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPOMonth" runat="server" 
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:HiddenField ID="hdPOMonth" runat="server" />
                                <asp:CalendarExtender ID="calendarPOMonth" runat="server" OnClientHidden="onCalendarHidden"
                                    PopupButtonID="imgbtnPOMonth" OnClientShown="onCalendarShown" Format="MM/yyyy"
                                    BehaviorID="calendarPOMonth" TargetControlID="txtPOMonth" OnClientDateSelectionChanged="clientChangedPOMonth">
                                </asp:CalendarExtender>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPOMonth"
                                    FilterType="Custom, Numbers" ValidChars="/" Enabled="True" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnPOMonth" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="PO Month Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>PO No.:</label>
                    <asp:TextBox ID="txtPONo" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorName" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" 
                        CssClass="form-control"></asp:TextBox>

                    <div class="full-width button-group">

                        <label>Amount:</label>
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

                        <asp:TextBox ID="txtAmountOne" runat="server" CssClass="form-control"
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

                    <label>Only POC Jobs:</label>
                    <asp:CheckBox ID="chkOnlyPOCJobs" runat="server" />

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />

                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                        OnClick="btnExport_Click" />

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
                AutoGenerateColumns="true" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvPOReportSummery_RowDataBound">

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
