<%@ Page Title="Add Update Employee" Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true" CodeFile="AddEmployee.aspx.cs" Inherits="ADMIN_EMPLOYEE_AddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Add Update Employee</title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <script type="text/javascript">

        

        function ValidateEmployeeName() {
            var EmployeeName = document.getElementById('<%=txtEmployeeName.ClientID %>').value;
            if (EmployeeName == '') {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmployeeID() {
            var EmployeeID = document.getElementById('<%=txtEmployeeID.ClientID %>').value;
            if (EmployeeID == '') {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeID.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUserType() {
            var UserType = document.getElementById('<%=ddlUserType.ClientID %>').selectedIndex;
            if (UserType == '' || UserType == '0') {
                document.getElementById('<%=ddlUserType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUserType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDesignation() {
            var Designation = document.getElementById('<%=txtDesignation.ClientID %>').value;
            if (Designation == '') {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDesignation.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTimesheetDept() {
            var TimesheetDept = document.getElementById('<%=ddlTimesheetDept.ClientID %>').selectedIndex;
            if (TimesheetDept == '' || TimesheetDept == '0') {
                document.getElementById('<%=ddlTimesheetDept.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTimesheetDept.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmailID() {
            var EmailID = document.getElementById('<%=txtEmailID.ClientID %>').value;
            if (EmailID == '') {
                document.getElementById('<%=txtEmailID.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmailID.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        

        function ValidateTimesheetEmployeeType() {
            var TimesheetEmployeeType = document.getElementById('<%=ddlTimesheetEmployeeType.ClientID %>').selectedIndex;
            if (TimesheetEmployeeType == '' || TimesheetEmployeeType == '0') {
                document.getElementById('<%=ddlTimesheetEmployeeType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTimesheetEmployeeType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUserName() {
            var UserName = document.getElementById('<%=txtUserName.ClientID %>').value;
            if (UserName == '') {
                document.getElementById('<%=txtUserName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUserName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePassword() {
            var UserName = document.getElementById('<%=txtUserName.ClientID %>').value;
            var Password = document.getElementById('<%=txtPassword.ClientID %>').value;

            if (UserName != '') {
                if (Password == '') {
                    document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtPassword.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidatefileUploadPassportCopy() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadPassportCopy = document.getElementById('<%=fileUploadPassportCopy.ClientID %>').value;
            var divfileUploadPassportCopy = document.getElementById("divfileUploadPassportCopy");
            var lblfileUploadPassportCopy = document.getElementById('<%=lblfileUploadPassportCopy.ClientID %>');

            if (fileUploadPassportCopy == '') {
                document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "";
                
                fileUploadPassportCopy.innerHTML = "";
                fileUploadPassportCopy.style.display = "none";
                return false;
            }
            else {
                if (!regex.test(fileUploadPassportCopy.toLowerCase())) {
                    document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "#F7627F";
                    fileUploadPassportCopy.style.display = "block";
                    fileUploadPassportCopy.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadPassportCopy.ClientID %>').style.borderColor = "";
                    fileUploadPassportCopy.style.display = "none";
                    fileUploadPassportCopy.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateBankDetails() {
            var BankDetails = document.getElementById('<%=chkBankDetails.ClientID %>').checked;

            document.getElementById('<%=ddlBankName.ClientID %>').selectedIndex = "0";
            document.getElementById('<%=txtIFSC.ClientID %>').value = "";
            document.getElementById('<%=txtAccountNo.ClientID %>').value = "";

            if (BankDetails == true) {
                document.getElementById('<%=ddlBankName.ClientID %>').disabled = false;
                document.getElementById('<%=txtIFSC.ClientID %>').disabled = false;
                document.getElementById('<%=txtAccountNo.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=ddlBankName.ClientID %>').disabled = true;
                document.getElementById('<%=txtIFSC.ClientID %>').disabled = true;
                document.getElementById('<%=txtAccountNo.ClientID %>').disabled = true;

                document.getElementById('<%=ddlBankName.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtIFSC.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtAccountNo.ClientID %>').style.borderColor = "";
            }
        }

        function ValidateBankName() {
            var BankDetails = document.getElementById('<%=chkBankDetails.ClientID %>').checked;
            var BankName = document.getElementById('<%=ddlBankName.ClientID %>').selectedIndex;

            if (BankDetails == true) {
                if (BankName = '' || BankName == '0') {
                    document.getElementById('<%=ddlBankName.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlBankName.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlBankName.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateIFSC() {
            var BankDetails = document.getElementById('<%=chkBankDetails.ClientID %>').checked;
            var IFSC = document.getElementById('<%=txtIFSC.ClientID %>').value;

            var reg = "[A-Z|a-z]{4}[0][a-zA-Z0-9]{6}$";

            if (BankDetails == true) {
                if (IFSC == '') {
                    document.getElementById('<%=txtIFSC.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    if (IFSC.match(reg)) {
                        document.getElementById('<%=txtIFSC.ClientID %>').style.borderColor = "";
                        return false;
                    }
                    else {
                        alert("Invalid IFSC...!!");
                        document.getElementById('<%=txtIFSC.ClientID %>').style.borderColor = "#F7627F";
                        return true;
                    }
                }
            }
            else {
                document.getElementById('<%=txtIFSC.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAccountNo() {
            var BankDetails = document.getElementById('<%=chkBankDetails.ClientID %>').checked;
            var AccountNo = document.getElementById('<%=txtAccountNo.ClientID %>').value;

            if (BankDetails == true) {
                if (AccountNo == '') {
                    document.getElementById('<%=txtAccountNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtAccountNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtAccountNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var check = true;


            if (ValidateEmployeeName()) {
                return false
            }

            if (ValidateEmployeeID()) {
                return false
            }

            if (ValidateUnit()) {
                return false
            }

            if (ValidateUserType()) {
                return false
            }

            

            if (ValidateDesignation()) {
                return false
            }

            if (ValidateDepartment()) {
                return false
            }

            if (ValidateTimesheetDept()) {
                return false
            }

            if (ValidateEmailID()) {
                return false
            }

            if (ValidateTimesheetEmployeeType()) {
                return false
            }

            if (ValidateUserName()) {
                return false
            }

            if (ValidatePassword()) {
                return false
            }

            if (ValidateBankName()) {
                return false
            }

            if (ValidateIFSC()) {
                return false
            }

            if (ValidateAccountNo()) {
                return false
            }


            

            if (ValidatefileUploadPassportCopy()) {
                return false;
            }

           


            <%--if (document.getElementById('<%=chkBankDetails.ClientID %>').checked == true) {

                if (ValidateBankName()) {
                    return false
                }

                if (ValidateIFSC()) {
                    return false
                }

                if (ValidateAccountNo()) {
                    return false
                }

            }--%>

            return check;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 100px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" Text="Add Employee"></asp:Label>
            </legend>
            <table width="80%">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width:15%;">Employee Name:</td>
                    <td style="width:35%;">
                        <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" />
                        <%--onblur="return ValidateEmployeeName();" --%>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="width:15%;">Employee ID:</td>
                    <td style="width:35%;">
                        <asp:TextBox ID="txtEmployeeID" runat="server" Width="100%" />
                        <%--onblur="return ValidateEmployeeID();"--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Unit:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="26px">
                            <%--onblur="return ValidateUnit();"--%>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="width: 15%;">User Type:</td>
                    <td style="width: 30%;">
                        <asp:DropDownList ID="ddlUserType" runat="server" Width="100%" Height="26px" Enabled="false">
                            <%--onblur="return ValidateUserType();"--%>
                            <asp:ListItem Text="Select" Value="0" />
                            <asp:ListItem Text="Admin" Value="A" />
                            <asp:ListItem Text="User" Value="U"  Selected="True"/>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                
                <tr>
                    <td>Designation:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesignation" runat="server" Width="100%" />
                        <%--onblur="return ValidateDesignation();" --%>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Department
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%" Height="26px">
                            <%--onblur="return ValidateDepartment();"--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Timesheet Department:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTimesheetDept" runat="server" Width="100%" Height="26px">
                            <%--onblur="return ValidateTimesheetDept();"--%>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Email ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailID" runat="server" Width="100%" />
                        <%--onblur="return ValidateEmailID();"--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    
                    <td>Team Leader:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeamLeader" runat="server" Width="100%" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>Timesheet Employee Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTimesheetEmployeeType" runat="server" Width="100%" Height="26px">
                            <%--onblur="return ValidateTimesheetEmployeeType();"--%>
                            <asp:ListItem Text="Select" Value="0" />
                            <asp:ListItem Text="Direct" Value="D" />
                            <asp:ListItem Text="Support" Value="S" />
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>User Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="100%" />
                        <%--onblur="return ValidateUserName();"--%>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" Width="100%" />
                        <%--onblur="return ValidatePassword();" --%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Passport Copy:
                    </td>
                    <td colspan="4">
                        <asp:FileUpload ID="fileUploadPassportCopy" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" />
                        <%--onblur="return ValidatefileUploadPassportCopy();"--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="3">&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadPassportCopy" style="display: none;">
                            <asp:Label ID="lblfileUploadPassportCopy" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Add Bank Details:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkBankDetails" runat="server" checked="true" Enabled="false" onclick="ValidateBankDetails()" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td>Bank Nme:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBankName" runat="server" Width="100%" Height="26px"
                            >
                            <%--onblur="return ValidateBankName();"--%>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>IFSC:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIFSC" runat="server" Width="100%" MaxLength="11"  />
                        <%--onblur="return ValidateIFSC();"--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>

                    <td>Account No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="100%" />
                        <%--onblur="return ValidateAccountNo();"--%>
                    </td>
                </tr>

                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                            OnClick="btnAdd_Click" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">
                        <asp:Button ID="btnEmployeeList" CssClass="button" runat="server" Text="Employee List"
                            OnClick="btnEmployeeList_Click" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
