<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddUpdateWorkerTimesheet.aspx.cs" Inherits="TIMESHEET_WORKER_AddUpdateWorkerTimesheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
        }
    </script>

    <script type="text/Javascript">
        function ValidatefileUploadTimesheet() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadTimesheet = document.getElementById('<%=fileUploadTimesheet.ClientID %>').value;
            var divfileUploadTimesheet = document.getElementById("divfileUploadTimesheet");
            var lblfileUploadTimesheet = document.getElementById('<%=lblfileUploadTimesheet.ClientID %>');

            if (fileUploadTimesheet == '') {
                document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadTimesheet.style.display = "block";
                lblfileUploadTimesheet.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadTimesheet.toLowerCase())) {
                    document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadTimesheet.style.display = "block";
                    lblfileUploadTimesheet.innerHTML = "Please enter only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadTimesheet.style.display = "none";
                    lblfileUploadTimesheet.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateTimesheetAll() {

            var check = true;
            if (ValidatefileUploadTimesheet()) { return false; }
            return check;
        }

        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/Javascript"> 

        function ValidateJOBNo() {
            var JOBNo = document.getElementById('<%=ddlJOBNo.ClientID %>').selectedIndex;
            if (JOBNo == '' || JOBNo == '0') {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmployeeName() {
            alert("hello");
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAllJobNo() {
            var check = true;
            if (ValidateJOBNo()) { return false; }
            return true;
        }


        function ValidateAll() {
            var check = true;

            if (ValidateEmployeeName()) { return false; }

            return true;
        }


    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
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

    <script type="text/Javascript">
        function checkDec1(el) {
            var row = el.parentNode.parentNode;
            var InTime = row.cells[3].getElementsByTagName("input")[0].value;

            if (InTime != '') {
                var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(InTime);
                if (InTime == '' || InTime == '__:__' || InTime == '00:00') {
                    row.cells[3].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    if (isValid) {
                        row.cells[3].getElementsByTagName("input")[0].style.borderColor = "";
                        return false;
                    }
                    else {
                        row.cells[3].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                        return true;
                    }
                }
            }
            else {
                row.cells[3].getElementsByTagName("input")[0].style.borderColor = "#F7627F";
                return true;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                    : Add/Import Worker Timesheet</legend>
                <div class="form-grid form-grid-3">

                    <label>JOB No.:</label>
                    <asp:DropDownList ID="ddlJOBNo" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
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

                    <label>Unit :</label>
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Browse Timesheet:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadTimesheet" runat="server" CssClass="form-control"
                                    BorderStyle="Groove" onblur="return ValidatefileUploadTimesheet();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileUploadTimesheet" style="display: none;">
                                    <asp:Label ID="lblfileUploadTimesheet" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                    <asp:Button ID="btnAddNewRow" CssClass="button" Width="100%" runat="server" Text="Add New Row"
                        OnClick="btnAddNewRow_Click" OnClientClick="return ValidateAllJobNo();" />


                    <asp:Button ID="btnGetTimesheetFile" CssClass="button" Width="100%" runat="server"
                        Text="Get Timesheet" OnClientClick="return ValidateTimesheetAll();" OnClick="btnGetTimesheetFile_Click" />

                    <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server" Text="Save"
                        OnClick="btnSave_Click" />

                </div>
            </fieldset>
        </div>
        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView ID="gvEmpJobList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvEmpJobList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="EMPLOYEE_NAME">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlEmployeeName" Width="100%" runat="server" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMP_CODE">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEmployeeCode" Width="100%" runat="server" onkeyDown="javascript:preventInput(event);"
                                Text='<%# Eval("EMPLOYEE_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JOB_NO">
                        <ItemTemplate>
                            <asp:TextBox ID="txtJOBNo" Width="100%" runat="server" Text='<%# Eval("JOB_NO") %>'
                                onkeyDown="javascript:preventInput(event);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IN_TIME">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInTime" Width="100%" runat="server" Text='<%# Eval("IN_TIME") %>'
                                onKeyUp="checkDec1(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HOURS">
                        <ItemTemplate>
                            <asp:TextBox ID="txtHours" Width="100%" runat="server" Text='<%# Eval("HOURS") %>'
                                onKeyUp="checkDec(this)" />
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

    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
