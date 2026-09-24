<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="LessonReport.aspx.cs" Inherits="ADMIN_LessonReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
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

    <div align="center" style="margin-top: 20px;">
      <fieldset style="width: 90%">
          <legend style="text-align: center;">Lesson Learnt Report</legend>
          <table width="100%">
              <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td align="right">
                        From Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                        <asp:HiddenField ID="hdStartDate" runat="server" />
                        <ajax:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDate" runat="server"
                            TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                            ToolTip="Start Date Calendar" />
                    </td>

                    <td align="right">
                        To Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                        <asp:HiddenField ID="hdEndDate" runat="server" />
                        <ajax:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDate" runat="server"
                            TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                        </ajax:CalendarExtender>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                            ToolTip="End Date Calendar" Width="20px" />
                    </td>
                  <td></td>
                    <td align="right">
                        Initiated By:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>
                    </td>
                  <td colspan="1">
                        &nbsp;
                    </td>
                 
                    <td align="right">
                        Department:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="80%" Height="26px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
              <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Unit:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        Job No.:
                    </td>
                    <td>
                         <asp:TextBox ID="txtJobNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                  <td align="right">
                        Customer Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td colspan="1">
                        &nbsp;
                    </td>
                  
                  <td align="right">
                        Equipment:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEquipments" runat="server" Width="80%" Height="26px">
                        </asp:DropDownList>
                    </td>
                </tr>
               <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
              <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        Created By:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCreatedBy" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td>
                         &nbsp;
                    </td>
                   <td>
                         &nbsp;
                    </td>
                   
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllNew();" />
                    </td>
                     <td>
                         &nbsp;
                    </td>
                   <td>
                         &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnExport" CssClass="button" Width="160%" runat="server" Text="Export"
                            OnClick="btnExport_Click" />
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
    <br />
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
            <div style='overflow: auto; width: 100%; height: 350px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvLessonLearntList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Vertical" PageSize="15" Width="100%"
                    HorizontalAlign="Center" OnRowDataBound="gvLessonLearntList_RowDataBound" AllowPaging="True"
                    OnPageIndexChanging="gvLessonLearntList_PageIndexChanging">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <%--<asp:TemplateField HeaderText="COMPLAINT_NO">
                            <ItemTemplate>
                                <asp:Label ID="lblStatusName" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                                <asp:Label ID="lblComplaintLogNo" runat="server" Text='<%# Eval("COMPLAINT_LOG_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="LESSON_ID" HeaderText="LESSON_ID" />
                        <asp:BoundField DataField="LESSON_DATE" HeaderText="LESSON_DATE" />
                        <asp:BoundField DataField="INITIATED_BY" HeaderText="INITIATED_BY" />
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT_NAME" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT_NAME" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="EQUIPMENT_NAME" HeaderText="EQUIPMENT_NAME" />
                        <asp:BoundField DataField="PROBLEM_FACED" HeaderText="PROBLEM_FACED" />
                        <asp:BoundField DataField="LESSON_LEARNT" HeaderText="LESSON_LEARNT" />
                        <asp:BoundField DataField="CREATED_BY" HeaderText="CREATED_BY" />
                      
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
</asp:Content>
