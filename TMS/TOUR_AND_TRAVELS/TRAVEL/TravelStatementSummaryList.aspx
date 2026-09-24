<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="TravelStatementSummaryList.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TravelStatementSummaryList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
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


   <%-- <script type="text/javascript">

        window.onload = function () {

            var currentPosX = document.getElementById("<%=hdScrollPositionX.ClientID%>").value;
            var currentPosY = document.getElementById("<%=hdScrollPositionY.ClientID%>").value;

            //var strCook = document.cookie;
            var strCookX = currentPosX;
            var strCookY = currentPosY;


            document.getElementById("<%=gridContainer.ClientID%>").scrollLeft = strCookX;
            document.getElementById("<%=gridContainer.ClientID%>").scrollTop = strCookY;

        }

        function SetDivPosition() {
            var intX = document.getElementById("<%=gridContainer.ClientID%>").scrollLeft;
            var intY = document.getElementById("<%=gridContainer.ClientID%>").scrollTop;

            document.getElementById("<%=hdScrollPositionX.ClientID%>").value = intX
            document.getElementById("<%=hdScrollPositionY.ClientID%>").value = intY

        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdUpdationFlag" runat="server" />
    <asp:HiddenField ID="hdAdditionalAttachmentConfirmValue" runat="server" Value="0" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Travel Statement Summary List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkDates" Checked="true" OnChange="GetDates()" />
                            </td>
                        </tr>
                    </table>

                    <label>Sanction No</label>
                    <asp:TextBox ID="txtSanctionNo" runat="server" CssClass="form-control" />

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Employee Name</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    &nbsp;
                    &nbsp;
                 <asp:Button ID="btnSearch"
                     OnClick="btnSearch_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                    &nbsp;
                    &nbsp;
                 <asp:Button ID="btnAddNewTravelStmt"
                     OnClick="btnAddNewTravelStmt_Click"
                     runat="server"
                     Text="Add New"
                     CssClass="button" />

                </div>

            </fieldset>
        </div>


        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView ID="gvTravelList"
                CssClass="employee-grid"
                runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" 
                HorizontalAlign="Center"
                OnRowCommand="gvTravelList_RowCommand"
                OnRowDataBound="gvTravelList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="STATEMENT_ID" HeaderText="STATEMENT_ID" Visible="False" />
                    <asp:TemplateField HeaderText="VIEW" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PASSPORT"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPassportCopyName" runat="server" Visible="false" Text='<%# Eval("PASSPORT_COPY_NAME") %>' />
                            <asp:ImageButton ID="btnPassportCopy" Height="20px" Width="20px" ImageUrl="~/Images/NEWICONS/PASSPORT_01.png"
                                CommandArgument="VIEWPASSPORT" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitRptSummaryOneName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_ONE_NAME") %>' />
                            <asp:ImageButton ID="btnVisitRptSummaryOneName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitRptSummaryTwoName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_TWO_NAME") %>' />
                            <asp:ImageButton ID="btnVisitRptSummaryTwoName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitRptSummaryThreeName" runat="server" Visible="false" Text='<%# Eval("VISIT_RPT_SUMMARY_THREE_NAME") %>' />
                            <asp:ImageButton ID="btnVisitRptSummaryThreeName" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <%--<asp:TemplateField HeaderText="Add Att.1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAddAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAddAttachment1" CommandArgument="ADD_ATTACHMENT_1" runat="server"
                                    ImageUrl="~/Images/Icons/add05.png"
                                    Height="30px" Width="30px" ToolTip="Add Additional Attachment 1" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Add Att.2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAddAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAddAttachment2" CommandArgument="ADD_ATTACHMENT_2" runat="server"
                                    ImageUrl="~/Images/Icons/add05.png"
                                    Height="30px" Width="30px" ToolTip="Add Additional Attachment 2" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                    <%--<asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="SEND_MAIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnSendMail" CommandArgument="SEND_MAIL" runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>


                    <%-- <asp:TemplateField HeaderText="APPROVE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnApproval" CommandArgument="APPROVE" ToolTip="Approve Tour" runat="server"
                                    Text="Approve" CssClass="cancelbutton" BorderStyle="Solid" BorderColor="Yellow" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>


                    <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="SANCTION_NO" />

                    <asp:TemplateField HeaderText="STATUS"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatementID" runat="server" Visible="false" Text='<%# Eval("STATEMENT_ID") %>' />
                            <asp:Label ID="lblStatementNo" runat="server" Visible="false" Text='<%# Eval("STATEMENT_NO") %>' />
                            <asp:Label ID="lblTourID" runat="server" Visible="false" Text='<%# Eval("TOUR_ID") %>' />
                            <asp:Label ID="lblTourNo" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                            <asp:Label ID="lblSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                            <asp:Label ID="lblCurrentStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                            <asp:Label ID="lblCurrentStatus" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                            <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                            <asp:Label ID="lblTeamLeaderID" runat="server" Visible="false" Text='<%# Eval("TEAMLEADER_ID") %>' />

                            <asp:Label ID="lblAdvanceAmt" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMT") %>' />
                            <asp:Label ID="lblTripTypeID" runat="server" Visible="false" Text='<%# Eval("TRIP_TYPE_ID") %>' />

                            <asp:Label ID="lblAdvanceCurrencyID" runat="server" Visible="false" Text='<%# Eval("ADVANCE_CURRENCY") %>' />
                            <asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNT") %>' />
                            <asp:Label ID="lblIsApprovalMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVAL_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsCheckedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CHECKED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsPassedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PASSED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsSettledMailSent" runat="server" Visible="false" Text='<%# Eval("IS_SETTLED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAmendmentMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDMENT_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAmendedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAmendedApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_APPROVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAmendedCehckedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_CHECKED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsAmendedPassedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_AMENDED_PASSED_MAIL_SENT") %>' />

                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" Height="40px" Width="40px" Enabled="false" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                    <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                    <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                    <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                    <asp:BoundField DataField="VISIT_TYPE" HeaderText="VISIT_TYPE" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                    <asp:BoundField DataField="AIRFARE_AMT" HeaderText="AIRFARE_AMT" />
                    <asp:BoundField DataField="TELEPHONE_MOBILE_AMT" HeaderText="TELEPHONE_MOBILE_AMT" />
                    <asp:BoundField DataField="LODGING_AMT" HeaderText="LODGING_AMT" />
                    <asp:BoundField DataField="TIPS_AMT" HeaderText="TIPS_AMT" />
                    <asp:BoundField DataField="MEALS_AMT" HeaderText="MEALS_AMT" />
                    <asp:BoundField DataField="VISAFEE_AMT" HeaderText="VISAFEE_AMT" />
                    <asp:BoundField DataField="GROUND_TRANSPORT_AMT" HeaderText="GROUND_TRANSPORT_AMT" />
                    <asp:BoundField DataField="DAILY_ALLOWANCE_AMT" HeaderText="DAILY_ALLOWANCE_AMT" />
                    <asp:BoundField DataField="ENTERTAINMENT_AMT" HeaderText="ENTERTAINMENT_AMT" />
                    <asp:BoundField DataField="OTHER_AMT" HeaderText="OTHER_AMT" />
                    <asp:BoundField DataField="GIFTS_AMT" HeaderText="GIFTS_AMT" />
                    <asp:BoundField DataField="TOTAL_AMT" HeaderText="TOTAL_AMT" />
                    <asp:BoundField DataField="TOTAL_CURRENCY" HeaderText="TOTAL_CURRENCY" />
                    <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_AMT" />
                    <asp:BoundField DataField="ADV_CURRENCY" HeaderText="ADV_CURRENCY" />
                    <asp:BoundField DataField="ADJUSTED_AMT" HeaderText="ADJUSTED_AMT" />
                    <asp:BoundField DataField="ADJUSTED_AMT_CURRENCY" HeaderText="ADJUSTED_AMT_CURRENCY" />
                    <asp:BoundField DataField="IS_COST_RECOVERABLE" HeaderText="IS_COST_RECOVERABLE" />
                    <asp:BoundField DataField="DN_NO" HeaderText="DN_NO" />
                    <asp:BoundField DataField="AMOUNT_DATED" HeaderText="AMOUNT_DATED" />
                    <asp:BoundField DataField="FINAL_AMT" HeaderText="FINAL_AMT" />
                    <asp:BoundField DataField="FINAL_AMT_CURRENCY" HeaderText="FINAL_AMT_CURRENCY" />
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

    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>

    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server">            
        </iframe>
    </asp:Panel>

    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            class="popup-iframe"
            id="iframeViewTravelStatementInPDF"
            runat="server">           
        </iframe>
    </asp:Panel>







    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
