<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ProjectListMonthWise.aspx.cs" Inherits="PROJECT_ProjectListMonthWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ValidateJobNo() {
            var JobNo = document.getElementById('<%=ddlJobNo.ClientID %>').selectedIndex;
            if (JobNo == '' || JobNo == '0') {
                document.getElementById('<%=ddlJobNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJobNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {

            var JobNo = document.getElementById('<%=ddlJobNo.ClientID %>').selectedIndex;
            if (JobNo == '' || JobNo == '0') {
                document.getElementById('<%=ddlJobNo.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }
            return true;
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 80%">
                    <legend style="text-align: center;">Adjust Project Budgeted Hours</legend>
                    <table width="80%">
                        <tr>
                            <td align="right">
                                JOB No:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJobNo" runat="server" Width="100%" Height="26px" onblur="return ValidateJobNo();"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Customer Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Start Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Start Date Calendar" OnClick="imgbtnStartDate_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                End Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtEndDate" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="End Date Calendar" Width="20px" OnClick="imgbtnEndDate_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClick="btnSearch_Click" OnClientClick="return ValidateAll();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <div id="divStartDate" runat="server" style="display: block; position: absolute">
                                    <asp:Panel ID="pnlStartDate" runat="server" Visible="false">
                                        <asp:Calendar ID="calendarStartDate" runat="server" BackColor="White" BorderColor="#3366CC"
                                            Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px"
                                            CellPadding="1" DayNameFormat="Shortest" BorderWidth="1px" OnSelectionChanged="calendarStartDate_SelectionChanged">
                                            <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <TitleStyle BackColor="#003399" Font-Bold="True" BorderColor="#3366CC" BorderWidth="1px"
                                                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        </asp:Calendar>
                                    </asp:Panel>
                                </div>
                            </td>
                            <td>
                            </td>
                            <td>
                                <div id="divEndDate" runat="server" style="display: block; position: absolute">
                                    <asp:Panel ID="pnlEndDate" runat="server" Visible="false">
                                        <asp:Calendar ID="calendarEndDate" runat="server" BackColor="White" BorderColor="#3366CC"
                                            CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                            ForeColor="#003399" Height="200px" Width="220px" BorderWidth="1px" OnSelectionChanged="calendarEndDate_SelectionChanged">
                                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" Font-Bold="True" BorderWidth="1px"
                                                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        </asp:Calendar>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <br />
            <div align="center">
                <asp:Panel ID="pnlMsg" runat="server" Visible="true">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" />
                </asp:Panel>
            </div>
            <br />
            <div align="center">
                <fieldset style="width: 40%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
                    <asp:GridView ID="gvProjectList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        AllowPaging="true" ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                        OnPageIndexChanging="gvProjectList_PageIndexChanging">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="RECORD_ID" HeaderText="RECORD_ID" Visible="false" />
                            <asp:BoundField DataField="PROJECT_ID" HeaderText="PROJECT_ID" Visible="false" />
                            <asp:BoundField DataField="MONTH_NAME" HeaderText="MONTH_NAME" />
                            <asp:BoundField DataField="BUDGETED_HOURS" HeaderText="BUDGETED_HOURS" />
                            <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS_NAME" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </fieldset>
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
