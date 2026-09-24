<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="WorkerTimesheetListNewOne.aspx.cs" Inherits="TIMESHEET_WORKER_WorkerTimesheetListNewOne" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
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

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }   
    </script>

    <script type="text/Javascript"> 
     
     function ValidateJOBNoForUpdation() {            
            var JOBNoForUpdation = document.getElementById('<%=ddlJOBNoForUpdation.ClientID %>').selectedIndex;
            if (JOBNoForUpdation== '' || JOBNoForUpdation== '0') {
                document.getElementById('<%=ddlJOBNoForUpdation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJOBNoForUpdation.ClientID %>').style.borderColor = "";
                return false;
            }            
        }                                         
    </script>

    <script type="text/javascript" language="javascript">
    
        function ValidateAll() {
            var check = true;            
            if (ValidateJOBNoForUpdation()) {return false;}
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 80%">
                    <legend style="text-align: center;">Worker Timesheet List</legend>
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
                                JOB No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJOBNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="25px" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                Project Head:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectHead" runat="server" Width="100%" Height="25px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlProjectHead_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            <td align="right">
                                Supervisor:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectSupervisor" runat="server" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
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
                        <asp:GridView ID="gvWorkerTimesheetList" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="8" Width="100%"
                            HorizontalAlign="Center" AllowPaging="True" OnPageIndexChanging="gvWorkerTimesheetList_PageIndexChanging"
                            OnRowDataBound="gvWorkerTimesheetList_RowDataBound" OnRowCommand="gvWorkerTimesheetList_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="EDIT">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" ToolTip="Edit" runat="server"
                                            ImageUrl="~/Images/royal_search.png" />
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                        <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                        <asp:Label ID="lblEmployeeCode" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                                        <asp:Label ID="lblEntryDate" runat="server" Visible="false" Text='<%# Eval("ENTRY_DATE") %>' />
                                        <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NUMBER") %>' />
                                        <asp:Label ID="lblInTime" runat="server" Visible="false" Text='<%# Eval("IN_TIME") %>' />
                                        <asp:Label ID="lblOutTime" runat="server" Visible="false" Text='<%# Eval("OUT_TIME") %>' />
                                        <asp:Label ID="lblHours" runat="server" Visible="false" Text='<%# Eval("HOURS") %>' />
                                        <asp:Label ID="lblWorkingHours" runat="server" Visible="false" Text='<%# Eval("WORKING_HOURS") %>' />
                                        <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("REMARKS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DELETE" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnDelete" CommandArgument="DELETE" ToolTip="Delete" runat="server"
                                            ImageUrl="~/Images/Cancelled01.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                                <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" />
                                <asp:BoundField DataField="ENTRY_DATE" HeaderText="ENTRY_DATE" />
                                <asp:BoundField DataField="JOB_NUMBER" HeaderText="JOB_NUMBER" />
                                <asp:BoundField DataField="IN_TIME" HeaderText="IN_TIME" />
                                <asp:BoundField DataField="OUT_TIME" HeaderText="OUT_TIME" />
                                <asp:BoundField DataField="HOURS" HeaderText="HOURS" />
                                <asp:BoundField DataField="WORKING_HOURS" HeaderText="WORKING_HOURS" />
                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
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
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="900px"
                Style="display: block">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                        </td>
                    </tr>
                </table>
                <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblLegend" runat="server" /></legend>
                    <%--<table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                                cellpadding="0" cellspacing="0">--%>
                    <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray;
                        margin-left: 5px;'>
                        <table style="width: 95%; height: 100%; margin-left: 20px;">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Employee Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Employee Code:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmployeeCode" runat="server" Width="100%" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Entry Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEntryDate" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    JOB No.:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJOBNoForUpdation" runat="server" Height="25px" onblur="return ValidateJOBNoForUpdation();"
                                        Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In Time:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInTime" runat="server" Enabled="false" Width="100%" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Out Time:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOutTime" runat="server" Enabled="false" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Hours:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHours" runat="server" Enabled="false" Width="100%" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Working Hours:
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdHoursMins" runat="server" />
                                    <asp:HiddenField ID="hdTotalWorkingMins" runat="server" />
                                    <asp:HiddenField ID="hdBalanceworkingMins" runat="server" />
                                    <asp:TextBox ID="txtWorkingHours" runat="server" Enabled="true" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remarks:
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtRemarks" runat="server" Enabled="true" Rows="4" TextMode="MultiLine"
                                        Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="4">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" OnClientClick="return ValidateAll();"
                                        Text="Update" Width="100%" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center" colspan="4">
                                    <asp:Panel ID="pnlUpdateMsg" runat="server" Height="50px" Visible="false">
                                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="True" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
