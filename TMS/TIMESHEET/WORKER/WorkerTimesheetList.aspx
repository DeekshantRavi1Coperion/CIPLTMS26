<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="WorkerTimesheetList.aspx.cs" Inherits="TIMESHEET_WORKER_WorkerTimesheetList" %>

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
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
            document.getElementById('<%=txtDateUpdation.ClientID %>').value = document.getElementById('<%=hdDateUpdation.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
        } 
        
        function clientChangedUpdation(sender, args) {
            document.getElementById('<%=hdDateUpdation.ClientID %>').value = document.getElementById('<%=txtDateUpdation.ClientID %>').value;
        }                         
    </script>

    <script type="text/Javascript"> 
     
     function ValidateJOBNo() {            
            var JOBNo = document.getElementById('<%=ddlJOBNo.ClientID %>').selectedIndex;
            if (JOBNo== '' || JOBNo == '0') {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }            
        }  
        
        function ValidateEmployeeUpdation() {            
            var EmployeeUpdation = document.getElementById('<%=ddlEmployeeUpdation.ClientID %>').selectedIndex;
            if (EmployeeUpdation== '' || EmployeeUpdation == '0') {
                document.getElementById('<%=ddlEmployeeUpdation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEmployeeUpdation.ClientID %>').style.borderColor = "";
                return false;
            }            
        }   
        
                     
        function ValidateInTime() {
            var InTime = document.getElementById('<%=txtInTime.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(InTime);
            if (InTime == '' || InTime == '__:__' || InTime == '00:00') {
                document.getElementById('<%=txtInTime.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtInTime.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtInTime.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }    
             
             
             
        function ValidateHours() {            
            var Hours = document.getElementById('<%=txtHours.ClientID %>').value;
            if (Hours== '' ) {
                document.getElementById('<%=txtHours.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtHours.ClientID %>').style.borderColor = "";
                return false;
            }            
        }                     
    </script>

    <script type="text/javascript" language="javascript">
            
        function ValidateAll() {
            var check = true;

            if (ValidateJOBNo()) {return false;}
            if (ValidateEmployeeUpdation()) {return false;}
            if (ValidateInTime()) {return false;}
            if (ValidateHours()) {return false;}                       
            return true;
        }
        
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
           if(event.which!=9)
            {
                event.preventDefault();
            }
        }
        
        function checkDec(el) {
            var ex = /^[0-9]+\:?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
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
                    <legend style="text-align: center;">Add/Import Worker Timesheet</legend>
                    <table width="100%">
                        <tr>
                            <td>
                                JOB No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtJOBNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                Entry Date:
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                            <asp:HiddenField ID="hdDate" runat="server" />
                                            <asp:CalendarExtender ID="calendarDate" PopupButtonID="imgbtnDate" runat="server"
                                                TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="imgbtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Start Date Calendar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                Employee:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                    OnClick="btnSearch_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnAddNew" CssClass="button" Width="100%" runat="server" Text="Add New"
                                    OnClick="btnAddNew_Click" />
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
                        <asp:GridView ID="gvTimesheetList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" AllowPaging="true"
                            OnRowCommand="gvTimesheetList_RowCommand" OnPageIndexChanging="gvTimesheetList_PageIndexChanging">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="EDIT">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                        <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                        <asp:Label ID="lblEmployeeCode" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_CODE") %>' />
                                        <asp:Label ID="lblEntryDate" runat="server" Visible="false" Text='<%# Eval("ENTRY_DATE") %>' />
                                        <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NUMBER") %>' />
                                        <asp:Label ID="lblInTime" runat="server" Visible="false" Text='<%# Eval("IN_TIME") %>' />
                                        <asp:Label ID="lblHours" runat="server" Visible="false" Text='<%# Eval("HOURS") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                                <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="EMPLOYEE_CODE" />
                                <asp:BoundField DataField="JOB_NUMBER" HeaderText="JOB_NUMBER" />
                                <asp:BoundField DataField="ENTRY_DATE" HeaderText="ENTRY_DATE" />
                                <asp:BoundField DataField="IN_TIME" HeaderText="IN_TIME" />
                                <asp:BoundField DataField="HOURS" HeaderText="HOURS" />
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
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="800px"
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
                    <div style='width: 99%; height: 200px; border: 1px solid lightgray; margin-left: 5px;'>
                        <table style="width: 90%; height: 100%; margin-left: 20px;">
                            <tr>
                                <td width="15%">
                                    Job No.:
                                </td>
                                <td width="30%">
                                    <asp:DropDownList ID="ddlJOBNo" runat="server" Width="100%" Height="25px" onblur="return ValidateJOBNo();">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="15%">
                                    Employee:
                                </td>
                                <td width="40%">
                                    <table width="100%">
                                        <tr>
                                            <td width="75%">
                                                <asp:DropDownList ID="ddlEmployeeUpdation" runat="server" Width="100%" Height="25px"
                                                    onblur="return ValidateEmployeeUpdation();" OnSelectedIndexChanged="ddlEmployeeUpdation_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="25%">
                                                <asp:TextBox ID="txtEmployeeCode" runat="server" Width="100%" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In Time:
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtInTime" runat="server" Width="100%" onblur="return ValidateInTime();" />--%>
                                    <asp:TextBox ID="txtInTime" runat="server" meta:resourcekey="txtInTimeResource" Width="100%"
                                        ValidationGroup="vgrpUpdateTask" onblur="return ValidateInTime();" Text="00:00" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtInTime" Mask="99:99"
                                        MaskType="Time" CultureName="en-us" MessageValidatorTip="true" runat="server">
                                    </asp:MaskedEditExtender>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Hours:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHours" runat="server" Width="100%" onblur="return ValidateHours();"
                                        onkeyup="checkDec(this);" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Entry Date:
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateUpdation" runat="server" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hdDateUpdation" runat="server" />
                                                <asp:CalendarExtender ID="calendarDateUpdation" PopupButtonID="imgbtnDateUpdation"
                                                    runat="server" TargetControlID="txtDateUpdation" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedUpdation" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnDateUpdation" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                    ToolTip="Entry Calendar" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="4">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                        OnClick="btnSubmit_Click" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div align="center">
                                        <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                                            <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                                        </asp:Panel>
                                    </div>
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
