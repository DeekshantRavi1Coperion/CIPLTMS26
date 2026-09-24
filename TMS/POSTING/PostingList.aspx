<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PostingList.aspx.cs"
    Inherits="POSTING_PostingList" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
<link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function EnableSingle() {
            document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex = '0';
            document.getElementById('<%=ddlCompany.ClientID %>').disabled = false;
        }

        function EnableAll() {
            document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex = '0';
            document.getElementById('<%=ddlCompany.ClientID %>').disabled = true;
        }


        function ValidateCompany() {
            if (document.getElementById('<%=ddlCompany.ClientID %>').disabled == false) {
                var Company = document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex;
                if (Company == '' || Company == '0') {
                    document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }
        
        
        
    </script>

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

            if (ValidateCompany()) {
                return false;
            }

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
        function checkDec1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    var row = el.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;

                    var lblAmountInr = row.cells[23].innerHTML;
                    var postingValue = row.cells[24].getElementsByTagName("input")[0].value;                                        
                    unpostedValue = Math.round((parseFloat(lblAmountInr) - parseFloat(postingValue)) * 100) / 100;
                    row.cells[26].getElementsByTagName("input")[0].value = unpostedValue;

                    if (postingValue == '' || postingValue == '0') {
                        row.cells[26].getElementsByTagName("input")[0].value = '';
                    }
                                                            
                    if (parseFloat(lblAmountInr) < parseFloat(postingValue)) {
                        row.cells[24].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[24].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                }
            }
            else {
                var row = el.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;

                var lblAmountInr = row.cells[23].innerHTML;
                var postingValue = row.cells[24].getElementsByTagName("input")[0].value;                                                
                unpostedValue = Math.round((parseFloat(lblAmountInr) - parseFloat(postingValue)) * 100) / 100;
                row.cells[26].getElementsByTagName("input")[0].value = unpostedValue;

                if (postingValue == '' || postingValue == '0') {
                    row.cells[26].getElementsByTagName("input")[0].value = '';
                }
                
                if (parseFloat(lblAmountInr) < parseFloat(postingValue)) {
                        row.cells[24].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        row.cells[24].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
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
            <legend style="text-align: center;">Posting List</legend>
            <table width="100%">
                <tr>
                    <td>
                        Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%" />
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
                    <td>
                        End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%" />
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
                    <td>
                        Search By:
                    </td>
                    <td>
                        <asp:RadioButton ID="rdAll" Text="All" runat="server" Checked="true" GroupName="Company"
                            Onchange="EnableAll();" />
                        <asp:RadioButton ID="rdSingle" Text="Single" runat="server" ValidationGroup="Company"
                            Onchange="EnableSingle();" GroupName="Company" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Company:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" onblur="return ValidateCompany();"
                            Enabled="false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPost" CssClass="button" Width="100%" runat="server" Text="Post"
                            OnClick="btnPost_Click" />
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
            <div style='overflow: auto; width: 100%; height: 500px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvPostingList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" PageSize="20" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPostingList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="MONTH" HeaderText="MONTH" />
                        <asp:BoundField DataField="CUSTCODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="OA_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BILL_NO" HeaderText="BILL_NO" />
                        <asp:BoundField DataField="BILL_DATE" HeaderText="BILL_DATE" />
                        <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
                        <asp:BoundField DataField="CURR_DESC" HeaderText="CURR_DESC" />
                        <asp:BoundField DataField="RATE" HeaderText="RATE" />
                        <asp:BoundField DataField="F_CURRENCY" HeaderText="F_CURRENCY" />
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="CLASS1" HeaderText="CLASS1" />
                        <asp:BoundField DataField="TYPE" HeaderText="TYPE" />
                        <asp:BoundField DataField="DOC" HeaderText="DOC" />
                        <asp:BoundField DataField="WARRANTY" HeaderText="WARRANTY" />
                        <asp:BoundField DataField="PERCENTAGE" HeaderText="PERCENTAGE" />
                        <asp:BoundField DataField="PERCENTAGE_AMOUNT" HeaderText="PERCENTAGE_AMOUNT" />
                        <asp:BoundField DataField="GLCODE" HeaderText="GLCODE" />
                        <asp:TemplateField HeaderText="END_MARKET">
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Visible="false" Text='<%# Eval("MONTH") %>' />
                                <asp:Label ID="lblCustCode" runat="server" Visible="false" Text='<%# Eval("CUSTCODE") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("OA_NO") %>' />
                                <asp:Label ID="lblBillNo" runat="server" Visible="false" Text='<%# Eval("BILL_NO") %>' />
                                <asp:Label ID="lblBillDate" runat="server" Visible="false" Text='<%# Eval("BILL_DATE") %>' />
                                <asp:Label ID="lblClass" runat="server" Visible="false" Text='<%# Eval("CLASS") %>' />
                                <asp:Label ID="lblCurrDesc" runat="server" Visible="false" Text='<%# Eval("CURR_DESC") %>' />
                                <asp:Label ID="lblRate" runat="server" Visible="false" Text='<%# Eval("RATE") %>' />
                                <asp:Label ID="lblFcurrency" runat="server" Visible="false" Text='<%# Eval("F_CURRENCY") %>' />
                                <asp:Label ID="lblAmountINR" runat="server" Visible="false" Text='<%# Eval("AMOUNT_INR") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:Label ID="lblBusinessSegment" runat="server" Visible="false" Text='<%# Eval("BUS_SEGMENT") %>' />
                                <asp:Label ID="lblClass1" runat="server" Visible="false" Text='<%# Eval("CLASS1") %>' />
                                <asp:Label ID="lblType" runat="server" Visible="false" Text='<%# Eval("TYPE") %>' />
                                <asp:Label ID="lblDoc" runat="server" Visible="false" Text='<%# Eval("DOC") %>' />
                                <asp:Label ID="lblWarranty" runat="server" Visible="false" Text='<%# Eval("WARRANTY") %>' />
                                <asp:Label ID="lblPercentage" runat="server" Visible="false" Text='<%# Eval("PERCENTAGE") %>' />
                                <asp:Label ID="lblPercentageAmount" runat="server" Visible="false" Text='<%# Eval("PERCENTAGE_AMOUNT") %>' />
                                <asp:Label ID="lblGlcode" runat="server" Visible="false" Text='<%# Eval("GLCODE") %>' />
                                <asp:TextBox ID="txtEndMarket" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COUNTRY">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCountry" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POSTING_MONTH">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPostingMonth" runat="server" Width="100%">
                                    <asp:ListItem Value="1" Text="Jan" />
                                    <asp:ListItem Value="2" Text="Feb" />
                                    <asp:ListItem Value="3" Text="Mar" />
                                    <asp:ListItem Value="4" Text="Apr" />
                                    <asp:ListItem Value="5" Text="May" />
                                    <asp:ListItem Value="6" Text="Jun" />
                                    <asp:ListItem Value="7" Text="Jul" />
                                    <asp:ListItem Value="8" Text="Aug" />
                                    <asp:ListItem Value="9" Text="Sep" />
                                    <asp:ListItem Value="10" Text="Oct" />
                                    <asp:ListItem Value="11" Text="Nov" />
                                    <asp:ListItem Value="12" Text="Dec" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POSTING_YEAR">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPostingYear" runat="server" Width="100%">
                                    <asp:ListItem Value="2015" Text="2015" />
                                    <asp:ListItem Value="2016" Text="2016" />
                                    <asp:ListItem Value="2017" Text="2017" />
                                    <asp:ListItem Value="2018" Text="2018" />
                                    <asp:ListItem Value="2019" Text="2019" />
                                    <asp:ListItem Value="2020" Text="2020" />
                                    <asp:ListItem Value="2021" Text="2021" />
                                    <asp:ListItem Value="2022" Text="2022" />
                                    <asp:ListItem Value="2023" Text="2023" />
                                    <asp:ListItem Value="2024" Text="2024" />
                                    <asp:ListItem Value="2025" Text="2025" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AMOUNT_INR" HeaderText="AMOUNT_INR" />
                        <asp:TemplateField HeaderText="POSTING_VALUE">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPostingValue" runat="server" onKeyUp="checkDec1(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="POSTING_CURRENCY">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPostingCurrency" runat="server" Width="100%">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UNPOSTED_VALUE">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUnPostedValue" runat="server" onkeyup="checkDec(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UDF1">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUDF1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UDF2">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUDF2" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UDF3">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUDF3" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UDF4">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUDF4" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UDF5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUDF5" runat="server" />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
