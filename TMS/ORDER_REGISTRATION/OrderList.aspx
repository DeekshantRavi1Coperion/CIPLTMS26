<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs"
    Inherits="ORDER_REGISTRATION_OrderList" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
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

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Order List</legend>
            <table width="100%">
                <tr>
                    <td align="right">
                        Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
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
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
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
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Order No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <%--Status.:--%>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Visible="false" Width="100%" Height="25px">
                            <asp:ListItem Text="Select" Value="0" />
                            <asp:ListItem Text="New" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <td colspan="4">
                            &nbsp;
                        </td>
                        <td align="right">
                            <table width="100%">
                                <tr>
                                    <td style="width: 45%;">
                                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 45%;">
                                        <asp:Button ID="btnAddNewOrder" OnClick="btnAddNewOrder_Click" CssClass="button" Width="100%" runat="server" Text="Add New Order" />
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
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 100%; border: 1px solid lightgray;'>
                <asp:GridView ID="gvOrderList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" PageSize="8" Width="100%" HorizontalAlign="Center"
                    AllowPaging="True" OnRowCommand="gvOrderList_RowCommand" OnRowDataBound="gvOrderList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="VIEW">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/pdficon1.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REVISE">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgRevise" CommandArgument="REVISE" runat="server" ImageUrl="~/Images/royal_search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="SEND_MAIL">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnSendMail" CommandArgument="SEND_MAIL" ToolTip="Send Mail"
                                    runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="ORDER_NO" HeaderText="ORDER_NO " />
                        <asp:TemplateField HeaderText="STATUS">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderID" runat="server" Visible="false" Text='<%# Eval("ORDER_ID") %>' />
                                <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                                <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Label ID="lblDivisionID" runat="server" Visible="false" Text='<%# Eval("DIVISION_ID") %>' />
                                <asp:Label ID="lblBusinessUnitID" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT_ID") %>' />
                                <asp:Label ID="lblTargetGroupID" runat="server" Visible="false" Text='<%# Eval("TARGET_GROUP_ID") %>' />
                                <asp:Label ID="lblProjectTypeID" runat="server" Visible="false" Text='<%# Eval("PROJECT_TYPE_ID") %>' />
                                <asp:Label ID="lblCustomerCharacterID" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CHARACTER_ID") %>' />
                                <asp:Label ID="lblOrderBookingLocationID" runat="server" Visible="false" Text='<%# Eval("ORDER_BOOKING_LOCATION_ID") %>' />
                                <asp:Label ID="lblProjectManagerID" runat="server" Visible="false" Text='<%# Eval("PROJECT_MANAGER_ID") %>' />
                                <asp:Label ID="lblProjectEngineerID" runat="server" Visible="false" Text='<%# Eval("PROJECT_ENGINEER_ID") %>' />
                                <asp:Label ID="lblSalesManagerID" runat="server" Visible="false" Text='<%# Eval("SALES_MANAGER_ID") %>' />
                                <asp:Label ID="lblSalesEngineerID" runat="server" Visible="false" Text='<%# Eval("SALES_ENGINEER_ID") %>' />
                                <asp:Label ID="lblDeliveryTermsID" runat="server" Visible="false" Text='<%# Eval("DELIVERY_TERMS_ID") %>' />
                                <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GSS_NO" HeaderText="GSS_NO " />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME " />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE " />
                        <asp:BoundField DataField="END_USER_NAME" HeaderText="END_USER_NAME " />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO " />
                        <asp:BoundField DataField="PO_DATE" HeaderText="PO_DATE " />
                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN_CODE " />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION " />
                        <asp:BoundField DataField="DIVISION_NAME" HeaderText="DIVISION_NAME " />
                        <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT " />
                        <asp:BoundField DataField="TARGET_GROUP" HeaderText="TARGET_GROUP " />
                        <asp:BoundField DataField="TARGET_GROUP_CODE" HeaderText="TARGET_GROUP_CODE " />
                        <asp:BoundField DataField="INDUSTRY_CODE" HeaderText="INDUSTRY_CODE " />
                        <asp:BoundField DataField="PROJECT_TYPE" HeaderText="PROJECT_TYPE " />
                        <asp:BoundField DataField="CUSTOMER_CHARACTER" HeaderText="CUSTOMER_CHARACTER " />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT_NAME " />
                        <asp:BoundField DataField="PROJECT_MANAGER" HeaderText="PROJECT_MANAGER " />
                        <asp:BoundField DataField="PROJECT_ENGINEER" HeaderText="PROJECT_ENGINEER " />
                        <asp:BoundField DataField="SALES_MANAGER" HeaderText="SALES_MANAGER " />
                        <asp:BoundField DataField="SALES_ENGINEER" HeaderText="SALES_ENGINEER " />
                        <asp:BoundField DataField="QUOTATION_NO" HeaderText="QUOTATION_NO " />
                        <asp:BoundField DataField="ESTIMATE_ATTACHED" HeaderText="ESTIMATE_ATTACHED " />
                        <asp:BoundField DataField="PAYMENT_TERMS" HeaderText="PAYMENT_TERMS " />
                        <asp:BoundField DataField="CREDIT_DAYS" HeaderText="CREDIT_DAYS " />
                        <asp:BoundField DataField="DELIVERY_TERMS_ID" HeaderText="DELIVERY_TERMS_ID " />
                        <asp:BoundField DataField="DELIVERY_TERM" HeaderText="DELIVERY_TERM " />
                        <asp:BoundField DataField="LD" HeaderText="LD " />
                        <asp:BoundField DataField="DELIVERY_DATE" HeaderText="DELIVERY_DATE " />
                        <asp:BoundField DataField="ORDER_REGISTRATION_DATE" HeaderText="ORDER_REGISTRATION_DATE " />
                        <asp:BoundField DataField="REMARKS" HeaderText="REMARKS " />
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
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="700px" Width="1300px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 0px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" /></legend>
            <%-- <div style='overflow: auto; width: 100%; height: 530px; border: 1px solid lightgray;
                margin-left: 5px;'>--%>
            <iframe style="margin-left: 15px; width: 100%; height: 640px; border-style: hidden;
                border-top-width: thin; border-width: thin;" id="iframeRevise" runat="server">
            </iframe>
            <%--</div>--%>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
            margin-left: 25px;'>
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewPDFFile"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewTourInformationInPDF"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
