<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="UpdateTravelDates.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_UpdateTravelDates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            ValidateDateRange();
        }
    </script>

    <script type="text/Javascript">

        function ValidateSanctionNo() {
            var SanctionNo = document.getElementById('<%=ddlSanctionNo.ClientID %>').selectedIndex;
            if (SanctionNo == '' || SanctionNo == '0') {
                document.getElementById('<%=ddlSanctionNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSanctionNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

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

    <script type="text/Javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateSanctionNo()) {
                check = false;
            }

            if (ValidateDateRange()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to update?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }


        }
    </script>

    <script type="text/javascript">
        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;
    </script>

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>


            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>Update Travel Dates</legend>


                    <div class="form-grid form-grid-2">

                        <label>Sanction No</label>
                        <asp:DropDownList ID="ddlSanctionNo"
                            runat="server"
                            CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged" />


                        <label>Tour No</label>
                        <asp:TextBox ID="txtTourNo"
                            runat="server"
                            Enabled="false"
                            CssClass="form-control" />
                        <asp:HiddenField ID="hdTourID" runat="server" />


                        <label>Name of Customer/Vendor</label>
                        <asp:TextBox ID="txtCustVendName"
                            runat="server"
                            Enabled="false"
                            CssClass="form-control" />



                        <label>Place of Visit</label>
                        <asp:TextBox ID="txtPlaceOfVisit"
                            runat="server"
                            Enabled="false"
                            CssClass="form-control" />



                        <label>Starting Date Of Tour</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDate"
                                        runat="server"
                                        ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdStartDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>


                        <label>End Date Of Tour</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDate"
                                        runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdEndDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDateSearch" runat="server"
                                        TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarks"
                            runat="server"
                            Enabled="true"
                            CssClass="form-control" />
                    </div>
                </fieldset>

                <div class="full-width button-group">
                    <asp:Button ID="btnSubmit"
                        runat="server"
                        Text="Update Travel Dates"
                        CssClass="button"
                        OnClientClick="return ValidateAll();"
                        OnClick="btnSubmit_Click" Width="50%" />

                    <asp:Button ID="btnTourList"
                        runat="server"
                        Text="Travel Statement List"
                        CssClass="button"
                        OnClick="btnTourList_Click" Width="50%" />
                </div>

                <div class="full-width">
                    <asp:Panel ID="pnlExceptionMsg" Visible="false" runat="server" Height="50px">
                        <asp:Label ID="lblExceptionMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>

                    <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                        <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
