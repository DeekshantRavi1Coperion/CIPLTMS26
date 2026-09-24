<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportPOReportSummary.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_ImportPOReportSummary" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .aligntxtcenter {
            text-align: center;
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
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Post PO Summary(Header) </legend>
            <table width="90%">
                <tr>
                    <td>Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
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
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
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
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>PO No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPONo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Vendor Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtVendorName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>JOB No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Amount >=:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Status:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="OPEN" Value="1"></asp:ListItem>
                            <asp:ListItem Text="CLOSE" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Exclude CIDF:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkExcludeCIDF" runat="server" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td colspan="2"></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td></td>
                    <td></td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnImport" CssClass="button" Width="100%" runat="server" Text="Post"
                                        OnClick="btnImport_Click" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" CssClass="button" Width="100%" runat="server" Text="Update"
                                        OnClick="btnUpdate_Click" />
                                </td>
                            </tr>
                        </table>
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
        <fieldset style="width: 95%;">
            <%--<legend style="text-align: center;"></legend>--%>
            <table width="99%">
                <tr>
                    <td align="left" width="2%">
                        <asp:CheckBox ID="chkSelectAll" Checked="false" runat="server" AutoPostBack="true"
                            OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </td>
                    <td align="left" width="10%">Select All
                    </td>
                    <td align="left" width="8%">Records:
                    </td>
                    <td>
                        <asp:Label ID="lblTotalAmount" runat="server" Text="0" Font-Bold="true" Visible="false" />
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                    </td>
                </tr>
            </table>
            <br />
            <div style='overflow: auto; width: 100%; height: 370px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvPOReportSummery" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowDataBound="gvPOReportSummery_RowDataBound">
                    <%--OnRowCommand="gvPOReportSummery_RowCommand"--%>
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                <asp:Label ID="lblSerialNo" runat="server" Visible="false" Text='<%# Eval("SERIAL_NO") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblPODate" runat="server" Visible="false" Text='<%# Eval("PO_DATE") %>' />
                                <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                                <asp:Label ID="lblVendorName" runat="server" Visible="false" Text='<%# Eval("VENDOR_NAME") %>' />
                                <asp:Label ID="lblAmount" runat="server" Visible="false" Text='<%# Eval("PO_VALUE_INR") %>' />
                                <asp:Label ID="lblDocClass" runat="server" Visible="false" Text='<%# Eval("DOC_CLASS") %>' />
                                <asp:Label ID="lblPOStatus" runat="server" Visible="false" Text='<%# Eval("PO_STATUS") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="PO_NO" DataField="PO_NO" />
                        <asp:BoundField HeaderText="PO_DATE" DataField="PO_DATE" />
                        <asp:BoundField HeaderText="VENDOR_CODE" DataField="VENDOR_CODE" />
                        <asp:BoundField HeaderText="VENDOR_NAME" DataField="VENDOR_NAME" />
                        <asp:BoundField HeaderText="PO_VALUE_INR" DataField="PO_VALUE_INR" />
                        <asp:BoundField HeaderText="DOC_CLASS" DataField="DOC_CLASS" />
                        <asp:BoundField HeaderText="PO_STATUS" DataField="PO_STATUS" />
                        <asp:BoundField HeaderText="LOCATION" DataField="LOCATION" />
                        <asp:TemplateField HeaderText="LAST_DELIVERY_DATE">
                            <%-- <ItemTemplate><asp:Label ID="lblLastDeliveryDate" runat="server" Text='<%# Eval("LAST_DELIVERY_DATE") %>' /></ItemTemplate>--%>
                            <ItemTemplate>
                                <asp:TextBox ID="txtLastDeliveryDate" runat="server" Width="80%" onkeyDown="javascript:preventInput(event);"
                                    Text='<%# Eval("LAST_DELIVERY_DATE") %>'></asp:TextBox>
                                <asp:CalendarExtender ID="calendarLastDeliveryDate" PopupButtonID="imgbtnLastDeliveryDate"
                                    runat="server" TargetControlID="txtLastDeliveryDate" Format="dd-MMM-yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnLastDeliveryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Last Delivery Date Calendar" Width="20px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EXPECTED_DELIVERY_DATE">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" Width="80%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:CalendarExtender ID="calendarExpectedDeliveryDate" PopupButtonID="imgbtnExpectedDeliveryDate"
                                    runat="server" TargetControlID="txtExpectedDeliveryDate" Format="dd-MMM-yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnExpectedDeliveryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Expected Delivery Date Calendar" Width="20px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IS_ORDER_SUBJECT_TO">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlIsOrderSubjectTo" runat="server" Width="100%">
                                    <asp:ListItem Text="YES" Value='YES' />
                                    <asp:ListItem Text="NO" Value='NO' />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NO_OF_MONTHS_TO_DELIVER">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNoOfMonthsToDeliver" runat="server" Width="100%" onkeyup="checkDec(this);"
                                    CssClass="aligntxtcenter"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NO_OF_MONTHS_BASED_UPON">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNoOfMonthsBasedUpon" runat="server" Width="100%" onkeyup="checkDec(this);"
                                    CssClass="aligntxtcenter"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PAYMENT_TERM">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPaymentTerm" runat="server" Width="100%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="LIKELY_DATE_OF_RECEIPT_OF_MATERIAL">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLikelyDateOfReceiptOfMaterial" runat="server" Width="80%" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                <asp:CalendarExtender ID="calendarLikelyDateOfReceiptOfMaterial" PopupButtonID="imgbtnLikelyDateOfReceiptOfMaterial"
                                    runat="server" TargetControlID="txtLikelyDateOfReceiptOfMaterial" Format="dd-MMM-yyyy">
                                </asp:CalendarExtender>
                                <asp:ImageButton ID="imgbtnLikelyDateOfReceiptOfMaterial" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Date Of Receipt Of Material Calendar" Width="20px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="SALES_PIVOT_GROUP">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSalesPivotGroup" runat="server" Width="100%">
                                </asp:DropDownList>
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
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
                PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="900px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>
                <iframe style="margin-left: 0px; width: 94%; height: 560px;" id="iframePOHeaderDetail"
                    runat="server">
                    <div style='overflow: auto; width: 94%; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
                    </div>
                </iframe>
            </asp:Panel>
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
