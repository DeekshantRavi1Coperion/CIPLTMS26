<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TicketList.aspx.cs" Inherits="TICKET_TicketList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
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
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">

        function ValidateTicketType() {
            var TicketType = document.getElementById('<%=ddlTicketType.ClientID %>').selectedIndex;
            if (TicketType == '' || TicketType == '0') {
                document.getElementById('<%=ddlTicketType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTicketType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatefileUploadAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment1 = document.getElementById('<%=fileUploadAttachment1.ClientID %>').value;
            var divfileUploadAttachment1 = document.getElementById("divfileUploadAttachment1");
            var lblfileUploadAttachment1 = document.getElementById('<%=lblfileUploadAttachment1.ClientID %>');

            if (fileUploadAttachment1 == '') {
                document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                divfileUploadAttachment1.style.display = "none";
                lblfileUploadAttachment1.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment1.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadAttachment1.style.display = "block";
                    lblfileUploadAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment1.style.display = "none";
                    lblfileUploadAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateTicketDescription() {
            var TicketDescription = document.getElementById('<%=txtTicketDescription.ClientID %>').value;
            if (TicketDescription == '') {
                document.getElementById('<%=txtTicketDescription.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTicketDescription.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAll() {
            if (ValidateTicketType()) { return false; }
            if (ValidatefileUploadAttachment1()) { return false; }
            if (ValidateTicketDescription()) { return false; }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 10px;">
        <fieldset style="width: 80%">
            <legend style="text-align: center;">Ticket List</legend>
            <table width="100%">
                <tr>
                    <td align="right">Start Date:
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
                    <td>&nbsp;
                    </td>
                    <td align="right">End Date:
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
                    <td>&nbsp;
                    </td>
                    <td align="right">Ticket No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTicketNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">Ticket Status.:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTicketStatus" runat="server" Width="100%" Height="26px">
                            <asp:ListItem Text="All" Value="0" />
                            <asp:ListItem Text="New" Value="1" />
                            <asp:ListItem Text="Closed" Value="2" />
                            <asp:ListItem Text="Cancelled" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="right">Employee:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td align="right">
                        <table width="100%">
                            <tr>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnSearch" CssClass="button" runat="server" Width="100%" Text="Search"
                                        OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
                                </td>
                                <td style="width: 5%;">&nbsp;
                                </td>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnCreateNew" CssClass="button" runat="server" Width="100%" Text="New Ticket"
                                        OnClick="btnCreateNew_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 95%">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
            <div style='overflow: scroll; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvTicketList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="6" Width="70%" HorizontalAlign="Center"
                    OnRowCommand="gvTicketList_RowCommand" OnRowDataBound="gvTicketList_RowDataBound"
                    AllowPaging="True" OnPageIndexChanging="gvTicketList_PageIndexChanging">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="FILE_1">
                            <ItemTemplate>
                                <asp:Label ID="lblFileOneName" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT_ONE") %>' />
                                <asp:ImageButton ID="btnFileOneName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TICKET_ID" HeaderText="ticketid" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="TICKET_NO" HeaderText="TICKET_NO"></asp:BoundField>
                        <asp:TemplateField HeaderText="STATUS">
                            <ItemTemplate>
                                <asp:Label ID="lblTicketID" runat="server" Visible="false" Text='<%# Eval("TICKET_ID") %>'></asp:Label>
                                <asp:Label ID="lblTicketNO" runat="server" Visible="false" Text='<%# Eval("TICKET_NO") %>'></asp:Label>
                                <asp:Label ID="lblTicketTypeID" runat="server" Visible="false" Text='<%# Eval("TICKET_TYPE_ID") %>'></asp:Label>
                                <asp:Label ID="lblTicketType" runat="server" Visible="false" Text='<%# Eval("TICKET_TYPE_NAME") %>'></asp:Label>
                                <asp:Label ID="lblTicketStatus" runat="server" Visible="false" Text='<%# Eval("TICKET_STATUS") %>' />
                                <asp:Label ID="lblCreatedByID" runat="server" Visible="false" Text='<%# Eval("CREATED_BY_ID") %>'></asp:Label>
                                <asp:Label ID="lblCreatedOn" runat="server" Visible="false" Text='<%# Eval("CREATED_ON") %>'></asp:Label>
                                <asp:Label ID="lblClosedByID" runat="server" Visible="false" Text='<%# Eval("CLOSED_BY_ID") %>'></asp:Label>
                                <asp:Label ID="lblClosedOn" runat="server" Visible="false" Text='<%# Eval("CLOSED_ON") %>'></asp:Label>
                                <asp:Label ID="lblCancelledByID" runat="server" Visible="false" Text='<%# Eval("CANCELLED_BY_ID") %>'></asp:Label>
                                <asp:Label ID="lblCancelledOn" runat="server" Visible="false" Text='<%# Eval("CANCELLED_ON") %>'></asp:Label>
                                <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLOSE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnClose" CommandArgument="CLOSE" ToolTip="Close Ticket" runat="server"
                                    Text="Close" CssClass="cancelbutton" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="TICKET_TYPE_NAME" HeaderText="TICKET_TYPE"></asp:BoundField>
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundField>
                        <asp:BoundField DataField="CREATED_ON" HeaderText="CREATED_ON"></asp:BoundField>
                        <asp:BoundField DataField="CREATED_USER" HeaderText="CREATED_BY"></asp:BoundField>
                        <asp:BoundField DataField="CLOSED_ON" HeaderText="CLOSED_ON"></asp:BoundField>
                        <asp:BoundField DataField="CLOSED_USER" HeaderText="CLOSED_BY"></asp:BoundField>
                        <asp:BoundField DataField="CANCELLED_ON" HeaderText="CANCELLED_ON"></asp:BoundField>
                        <asp:BoundField DataField="CANCELLED_USER" HeaderText="CANCELLED_BY"></asp:BoundField>
                        <asp:BoundField DataField="REMARKS" HeaderText="REMARKS"></asp:BoundField>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgProperties" ToolTip="Update ticket" CommandArgument="PROPERTIES"
                                    runat="server" ImageUrl="~/Images/royal_search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CANCEL">
                            <ItemTemplate>
                                <asp:Button ID="btnCancel" CommandArgument="CANCEL" ToolTip="Cancel Ticket" runat="server"
                                    Text="Cancel" CssClass="cancelbutton" />
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
            </div>
        </fieldset>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="450px" Width="700px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div style='overflow: auto; width: 645px; height: 400px; border: 1px solid lightgray; margin-left: 27px;'>
            <fieldset style="width: 95%; margin-left: 20px; margin-top: 30px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLegend" runat="server" /></legend>
                <br />
                <table width="100%" style="width: 100%; height: 100%;">
                    <tr>
                        <td style="width: 20%;">Ticket Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTicketType" runat="server" Width="100%" Height="26px" onblur="return ValidateTicketType();">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <asp:Panel ID="pnlAttachments" runat="server" Visible="false">
                        <tr>
                            <td>Attachment:
                            </td>
                            <td>
                                <asp:FileUpload ID="fileUploadAttachment1" runat="server" Width="100%" Height="29px"
                                    BorderStyle="Groove" onblur="return ValidatefileUploadAttachment1();" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <div id="divfileUploadAttachment1" style="display: none;">
                                    <asp:Label ID="lblfileUploadAttachment1" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlViewAttachments" runat="server">
                        <tr>
                            <td>Attachment:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;" align="right">
                                            <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Width="100%"
                                Height="100px" onblur="return ValidateTicketDescription();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Width="100%" OnClientClick="return ValidateAll();"
                                Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Panel ID="Panel1" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
            </fieldset>
        </div>
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
        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
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
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
