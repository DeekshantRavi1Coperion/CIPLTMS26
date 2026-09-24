<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateWorkerTimesheetListOne.aspx.cs"
    Inherits="TIMESHEET_WORKER_UpdateWorkerTimesheetListOne" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script type="text/javascript">
       window.onload=function(){
        var div=document.getElementById("dvScroll");
        var div_position=document.getElementById("div_position");
        var position=parseInt('<%=Request.Form["div_position"] %>');
            if(isNaN(position))
            {
                position=0;
            }
            div.scrollTop=position;
            div.onscroll=function(){
                div_position.value=div.scrollTop;
            };
       };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 100%">
            <table width="95%">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Employee Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="right">
                        Employee Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeCode" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="right">
                        Unit:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Entry Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntryDate" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="right">
                        In Time:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInTime" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="right">
                        Out Time:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOutTime" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Working Hours:
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkingHours" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="right">
                        OT Hours:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOTHours" runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 100%">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <fieldset style="width: 100%;">
                <asp:HiddenField ID="hdMinuts" runat="server" />
                <asp:HiddenField ID="hdTotalMinuts" runat="server" />
                <legend style="text-align: center;">
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div id="dvScroll" style='overflow: scroll; width: 100%; height: 320px; border: 1px solid lightgray;'>
                    <asp:UpdatePanel runat="server" ID="uppanel">
                        <ContentTemplate>
                            <asp:GridView ID="gvWorkerTimesheetList" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                                OnRowDataBound="gvWorkerTimesheetList_RowDataBound" OnRowCommand="gvWorkerTimesheetList_RowCommand">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="EMPLOYEE_NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMP_CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="JOB_NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EMPLOYEE_NAME") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EMPLOYEE_CODE") %>' Visible="false" />
                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT") %>' Visible="false" />
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("ENTRY_DATE") %>' Visible="false" />
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("IN_TIME") %>' Visible="false" />
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUT_TIME") %>' Visible="false" />
                                            <asp:Label ID="lblWorkingHours" runat="server" Text='<%# Eval("WORKING_HOURS") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblOTHours" runat="server" Text='<%# Eval("OT_HOURS") %>' Visible="false" />
                                            <asp:DropDownList ID="ddlJOBNo" Width="140px" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="ENTRY_DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("ENTRY_DATE") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IN_TIME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("IN_TIME") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OUT_TIME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUT_TIME") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WRK_HRS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkingHours" runat="server" Text='<%# Eval("WORKING_HOURS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OT_HRS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOTHours" runat="server" Text='<%# Eval("OT_HOURS") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="ADJ_HRS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlHours" runat="server" Width="96%" OnSelectedIndexChanged="ddlHours_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="00" Value="0" />
                                                <asp:ListItem Text="01" Value="1" />
                                                <asp:ListItem Text="02" Value="2" />
                                                <asp:ListItem Text="03" Value="3" />
                                                <asp:ListItem Text="04" Value="4" />
                                                <asp:ListItem Text="05" Value="5" />
                                                <asp:ListItem Text="06" Value="6" />
                                                <asp:ListItem Text="07" Value="7" />
                                                <asp:ListItem Text="08" Value="8" />
                                                <asp:ListItem Text="09" Value="9" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                                <asp:ListItem Text="13" Value="13" />
                                                <asp:ListItem Text="14" Value="14" />
                                                <asp:ListItem Text="15" Value="15" />
                                                <asp:ListItem Text="16" Value="16" />
                                                <asp:ListItem Text="17" Value="17" />
                                                <asp:ListItem Text="18" Value="18" />
                                                <asp:ListItem Text="19" Value="19" />
                                                <asp:ListItem Text="20" Value="20" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ADJ_MNS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlMins" runat="server" Width="96%" OnSelectedIndexChanged="ddlMins_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="00" Value="0" />
                                                <asp:ListItem Text="01" Value="1" />
                                                <asp:ListItem Text="02" Value="2" />
                                                <asp:ListItem Text="03" Value="3" />
                                                <asp:ListItem Text="04" Value="4" />
                                                <asp:ListItem Text="05" Value="5" />
                                                <asp:ListItem Text="06" Value="6" />
                                                <asp:ListItem Text="07" Value="7" />
                                                <asp:ListItem Text="08" Value="8" />
                                                <asp:ListItem Text="09" Value="9" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                                <asp:ListItem Text="13" Value="13" />
                                                <asp:ListItem Text="14" Value="14" />
                                                <asp:ListItem Text="15" Value="15" />
                                                <asp:ListItem Text="16" Value="16" />
                                                <asp:ListItem Text="17" Value="17" />
                                                <asp:ListItem Text="18" Value="18" />
                                                <asp:ListItem Text="19" Value="19" />
                                                <asp:ListItem Text="20" Value="20" />
                                                <asp:ListItem Text="21" Value="21" />
                                                <asp:ListItem Text="22" Value="22" />
                                                <asp:ListItem Text="23" Value="23" />
                                                <asp:ListItem Text="24" Value="24" />
                                                <asp:ListItem Text="25" Value="25" />
                                                <asp:ListItem Text="26" Value="26" />
                                                <asp:ListItem Text="27" Value="27" />
                                                <asp:ListItem Text="28" Value="28" />
                                                <asp:ListItem Text="29" Value="29" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="31" Value="31" />
                                                <asp:ListItem Text="32" Value="32" />
                                                <asp:ListItem Text="33" Value="33" />
                                                <asp:ListItem Text="34" Value="34" />
                                                <asp:ListItem Text="35" Value="35" />
                                                <asp:ListItem Text="36" Value="36" />
                                                <asp:ListItem Text="37" Value="37" />
                                                <asp:ListItem Text="38" Value="38" />
                                                <asp:ListItem Text="39" Value="39" />
                                                <asp:ListItem Text="40" Value="40" />
                                                <asp:ListItem Text="41" Value="41" />
                                                <asp:ListItem Text="42" Value="42" />
                                                <asp:ListItem Text="43" Value="43" />
                                                <asp:ListItem Text="44" Value="44" />
                                                <asp:ListItem Text="45" Value="45" />
                                                <asp:ListItem Text="46" Value="46" />
                                                <asp:ListItem Text="47" Value="47" />
                                                <asp:ListItem Text="48" Value="48" />
                                                <asp:ListItem Text="49" Value="49" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="51" Value="51" />
                                                <asp:ListItem Text="52" Value="52" />
                                                <asp:ListItem Text="53" Value="53" />
                                                <asp:ListItem Text="54" Value="54" />
                                                <asp:ListItem Text="55" Value="55" />
                                                <asp:ListItem Text="56" Value="56" />
                                                <asp:ListItem Text="57" Value="57" />
                                                <asp:ListItem Text="58" Value="58" />
                                                <asp:ListItem Text="59" Value="59" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ADJ_WRK_HRS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkingMinuts" runat="server" Text='<%# Eval("WORKING_MINUTS") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblAdjustedWorkingHours" runat="server" Text='<%# Eval("ADJUSTED_WORKING_HOURS") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIAL_NO") %>' Visible="false" />
                                            <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("RECORD_ID") %>' Visible="false" />
                                            <asp:TextBox ID="txtAdjustedWorkingHours" Width="100%" runat="server" Text='<%# Eval("ADJUSTED_WORKING_HOURS") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BAL_WRK_HRS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalanceWorkingHours" runat="server" Text='<%# Eval("BAL_WRK_HRS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REMARKS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2"
                                                Columns="50" Text='<%# Eval("REMARKS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ADD">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnAddNewRecord" ImageUrl="~/Images/Icons/ADD05.png" ToolTip="Add New Record"
                                                runat="server" CommandArgument="ADD" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <input type="hidden" id="div_position" name="div_position" />
            </fieldset>
        </fieldset>
    </div>
    </form>
</body>
</html>
