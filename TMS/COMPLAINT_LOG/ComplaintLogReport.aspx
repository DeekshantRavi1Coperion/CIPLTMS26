<%@ Page Title="CIPLTMS - Complaint Log Report" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ComplaintLogReport.aspx.cs" Inherits="COMPLAINT_LOG_ComplaintLogReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
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
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
        }

        function clientChangedSearch(sender, args) {

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
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
                asdasd123213
                return false;
            }
            return true;
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
                    <label>Start Date</label>
                    <table>
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox
                                    CssClass="form-control"
                                    ID="txtStartDate" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate" runat="server"
                                    TargetControlID="txtStartDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>



                    <label>End Date</label>
                    <table>
                        <tr>
                            <td style="width: 90%;">
                                <asp:TextBox
                                    CssClass="form-control"
                                    ID="txtEndDate" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDate" runat="server"
                                    TargetControlID="txtEndDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>
                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Complaint Log No.</label>
                    <asp:TextBox ID="txtComplaintLogNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Responsible Person</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>




                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch"
                    OnClick="btnSearch_Click"
                    runat="server"
                    Text="Search"
                    CssClass="button" />

                <asp:Button ID="btnExport"
                    OnClick="btnExport_Click"
                    runat="server"
                    Text="Export"
                    CssClass="button" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Complaint Log Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

            <div class="employee-grid-container">

                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvComplaintLogList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Vertical" PageSize="15" Width="100%"
                    HorizontalAlign="Center" OnRowDataBound="gvComplaintLogList_RowDataBound" AllowPaging="True"
                    OnPageIndexChanging="gvComplaintLogList_PageIndexChanging">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="COMPLAINT_NO">
                            <ItemTemplate>
                                <asp:Label ID="lblStatusName" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                                <asp:Label ID="lblComplaintLogNo" runat="server" Text='<%# Eval("COMPLAINT_LOG_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="COMPLAINT_RECEIVED_ON" HeaderText="COMPLAINT_RECEIVED_ON" />
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS_NAME" />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="PLANT" HeaderText="PLANT" />
                        <asp:BoundField DataField="COMPLAINT_DESCRIPTION" HeaderText="COMPLAINT_DESCRIPTION" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM_NAME" />
                        <asp:BoundField DataField="MODEL_NO" HeaderText="MODEL_NO" />
                        <asp:BoundField DataField="SERVICE_TYPE" HeaderText="SERVICE_TYPE" />
                        <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT" />
                        <asp:BoundField DataField="MANUFACTURER_NAME" HeaderText="MANUFACTURER_NAME" />
                        <asp:BoundField DataField="PROPOSED_ACTIONS" HeaderText="PROPOSED_ACTIONS" />
                        <asp:BoundField DataField="ROOT_CAUSE" HeaderText="ROOT_CAUSE" />
                        <asp:BoundField DataField="TARGET_COMPLETION_DATE" HeaderText="TARGET_COMPLETION_DATE" />
                        <asp:BoundField DataField="ACTUAL_COMPLETION_DATE" HeaderText="ACTUAL_COMPLETION_DATE" />

                        <asp:BoundField DataField="RESP_PERSON_LESSON_LEARNT" HeaderText="RESP_PERSON_LESSON_LEARNT" />
                        <asp:BoundField DataField="RESP_DEPT_HOD_LESSON_LEARNT" HeaderText="RESP_DEPT_HOD_LESSON_LEARNT" />
                        <asp:BoundField DataField="SERVICE_DEPT_HOD_LESSON_LEARNT" HeaderText="SERVICE_DEPT_HOD_LESSON_LEARNT" />

                        <asp:BoundField DataField="CORRECTIVE_ACTION" HeaderText="CORRECTIVE_ACTION" />
                        <asp:BoundField DataField="RESPONSIBLE_DEPT_ASSIGNED_BY" HeaderText="DEPT_ASSIGNED_BY" />
                        <asp:BoundField DataField="RESPONSIBLE_DEPT_ASSIGNED_ON" HeaderText="DEPT_ASSIGNED_ON" />
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT_NAME" />
                        <asp:BoundField DataField="RESPONSIBLE_PERSON_ASSIGNED_BY" HeaderText="PERSON_ASSIGNED_BY" />
                        <asp:BoundField DataField="RESPONSIBLE_PERSON_ASSIGNED_ON" HeaderText="PERSON_ASSIGNED_ON" />
                        <asp:BoundField DataField="RESPONSIBLE_PERSON" HeaderText="RESPONSIBLE_PERSON" />
                        <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY" />
                        <asp:BoundField DataField="CREATED_ON" HeaderText="CREATED_ON" />
                        <asp:BoundField DataField="RESOLVED_ON" HeaderText="RESOLVED_ON" />
                        <asp:BoundField DataField="APPROVED_BY" HeaderText="APPROVED_BY" />
                        <asp:BoundField DataField="APPROVED_ON" HeaderText="APPROVED_ON" />
                        <asp:BoundField DataField="CLOSED_BY" HeaderText="CLOSED_BY" />
                        <asp:BoundField DataField="CLOSED_ON" HeaderText="CLOSED_ON" />
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

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
