<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="EmployeeList.aspx.cs" Inherits="ADMIN_EMPLOYEE_EmployeeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../../Styles/list.css" rel="stylesheet" />--%>
    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <script type="text/javascript">

        function ValidateEmployeeName() {
            var EmployeeName = document.getElementById('<%=txtEmployeeNameNew.ClientID %>').value;
            if (EmployeeName == '') {
                document.getElementById('<%=txtEmployeeNameNew.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeNameNew.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmployeeID() {
            var EmployeeID = document.getElementById('<%=txtEmployeeIDNew.ClientID %>').value;
            if (EmployeeID == '') {
                document.getElementById('<%=txtEmployeeIDNew.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeIDNew.ClientID %>').style.borderColor = "";
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
            if (UserType == 'Select' || UserType == '0') {
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
            var Department = document.getElementById('<%=ddlDepartmentNew.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartmentNew.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartmentNew.ClientID %>').style.borderColor = "";
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
            if (TimesheetEmployeeType == 'Select' || TimesheetEmployeeType == '0') {
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
                fileUploadPassportCopy.style.display = "none";
                fileUploadPassportCopy.innerHTML = "";
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

            var fileUploadPassportCopy = document.getElementById('<%=fileUploadPassportCopy.ClientID %>').value;

            if (document.getElementById('<%=chkBankDetails.ClientID %>').checked == true) {

                if (ValidateBankName()) {
                    return false
                }

                if (ValidateIFSC()) {
                    return false
                }

                if (ValidateAccountNo()) {
                    return false
                }

            }

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

            if (fileUploadPassportCopy != '') {
                if (ValidatefileUploadPassportCopy()) {
                    return false;
                }
            }

            return true;
        }


        function ValidateNewTeamleader() {
            var NewTeamleader = document.getElementById('<%=ddlNewTeamleader.ClientID %>').selectedIndex;
            if (NewTeamleader == 'Select' || NewTeamleader == '0') {
                document.getElementById('<%=ddlNewTeamleader.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlNewTeamleader.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAllNew() {

            if (ValidateNewTeamleader()) {
                return false
            }

            return true;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Filters:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnitSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Employee Name</label>
                    <asp:TextBox ID="txtEmployeeNameSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Employee ID</label>
                    <asp:TextBox ID="txtEmployeeIDSearch"
                        runat="server"
                        CssClass="form-control" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartmentSearch"
                        runat="server"
                        CssClass="form-control" />

                    &nbsp;
                    &nbsp;
                 <asp:Button ID="btnSearch"
                     OnClick="btnSearch_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                </div>
            </fieldset>
        </div>

        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>


            <asp:GridView ID="gvEmployeeList"
                CssClass="employee-grid"
                runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvEmployeeList_RowCommand"
                OnRowDataBound="gvEmployeeList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="PASSPORT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPassportCopyName" runat="server" Visible="false" Text='<%# Eval("PASSPORT_COPY_NAME") %>' />
                            <asp:ImageButton ID="btnPassportCopy" Height="20px" Width="20px" CommandArgument="VIEWPASSPORT"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                            <asp:Label ID="lblIsActive" runat="server" Visible="false" Text='<%# Eval("IS_ACTIVE") %>' />

                            <%--<asp:Label ID="lblDepartmentID" runat="server" Visible="false" Text='<%# Eval("DEPARTMENT_ID") %>' />
                                <asp:Label ID="lblTimesheetDeptID" runat="server" Visible="false" Text='<%# Eval("TIMESHEET_DEPT_ID") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                
                                <asp:Label ID="lblUserType" runat="server" Visible="false" Text='<%# Eval("USER_TYPE") %>' />

                                <asp:Label ID="lblBankID" runat="server" Visible="false" Text='<%# Eval("BANK_ID") %>' />
                                <asp:Label ID="lblIFSC" runat="server" Visible="false" Text='<%# Eval("IFSC") %>' />
                                <asp:Label ID="lblAccountNo" runat="server" Visible="false" Text='<%# Eval("ACCOUNT_NO") %>' />--%>

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Employee Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IS_ACTIVE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgIsActive" CommandArgument="IS_ACTIVE"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                    <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                    <asp:BoundField DataField="EMAIL_ID" HeaderText="EMAIL_ID" />
                    <asp:BoundField DataField="USER_NAME" HeaderText="USER_NAME" />
                    <asp:BoundField DataField="PASSWORD" HeaderText="PASSWORD" />
                    <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                    <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT_NAME" />
                    <asp:BoundField DataField="TIMESHEET_DEPARTMENT" HeaderText="TIMESHEET_DEPARTMENT" />
                    <asp:BoundField DataField="TEAM_LEADER" HeaderText="TEAM_LEADER" />
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="UNIT_NAME" />
                    <asp:BoundField DataField="USER_TYPE" HeaderText="USER_TYPE" />
                    <asp:BoundField DataField="TIMESHEET_EMP_TYPE" HeaderText="TIMESHEET_EMP_TYPE" />
                    <asp:BoundField DataField="BANK_NAME" HeaderText="BANK_NAME" />
                    <asp:BoundField DataField="IFSC" HeaderText="IFSC" />
                    <asp:BoundField DataField="ACCOUNT_NO" HeaderText="ACCOUNT_NO" />
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

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">

            <%--<fieldset style="width: 93%; margin-left: 23px; margin-top: 5px;">--%>
            <fieldset class="form-card">
                <%-- <legend style="text-align: center;">--%>
                <legend>Employee Details
                <%--<asp:Label ID="lblLegend" runat="server" Text="Employee Details" />--%>
                </legend>
                <div align="center">
                    <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <div class="form-grid form-grid-2">
                    <label>Employee Name</label>
                    <asp:TextBox ID="txtEmployeeNameNew"
                        runat="server"
                        CssClass="form-control" />

                    <label>Employee ID</label>
                    <asp:TextBox ID="txtEmployeeIDNew"
                        runat="server"
                        CssClass="form-control" />


                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit"
                        runat="server"
                        CssClass="form-control" />

                    <label>User Type</label>
                    <asp:DropDownList ID="ddlUserType"
                        runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Admin" Value="A" />
                        <asp:ListItem Text="User" Value="U" Selected="True" />
                    </asp:DropDownList>

                    <label>Designation</label>
                    <asp:TextBox ID="txtDesignation"
                        runat="server"
                        CssClass="form-control" />

                    <label>Department</label>
                    <asp:DropDownList ID="ddlDepartmentNew"
                        runat="server"
                        CssClass="form-control" />

                    <label>Timesheet Department</label>
                    <asp:DropDownList ID="ddlTimesheetDept"
                        runat="server"
                        CssClass="form-control" />


                    <label>Email ID</label>
                    <asp:TextBox ID="txtEmailID"
                        runat="server"
                        CssClass="form-control" />

                    <label>Team Leader</label>
                    <asp:DropDownList ID="ddlTeamLeader"
                        runat="server"
                        CssClass="form-control" />

                    <label>Timesheet Employee Type</label>
                    <asp:DropDownList ID="ddlTimesheetEmployeeType"
                        runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="Select" Value="0" />
                        <asp:ListItem Text="Direct" Value="D" />
                        <asp:ListItem Text="Support" Value="S" />
                    </asp:DropDownList>

                    <label>Username</label>
                    <asp:TextBox ID="txtUserName"
                        runat="server"
                        CssClass="form-control" />

                    <label>Password</label>
                    <asp:TextBox ID="txtPassword"
                        runat="server"
                        CssClass="form-control" />

                    <label>Passport Copy</label>
                    <div class="full-width">
                        <asp:FileUpload ID="fileUploadPassportCopy"
                            runat="server"
                            CssClass="form-file" />
                    </div>
                    <div class="full-width" id="divfileUploadPassportCopy" style="display: none;">
                        <asp:Label ID="lblfileUploadPassportCopy" runat="server" ForeColor="Red" />
                    </div>


                    <label>Add Bank Details</label>
                    <asp:CheckBox ID="chkBankDetails" runat="server" Checked="true"
                        Enabled="false" onclick="ValidateBankDetails()" CssClass="form-control" />



                    <label>Bank Nme</label>
                    <asp:DropDownList ID="ddlBankName"
                        runat="server"
                        CssClass="form-control" />

                    <label>IFSC</label>
                    <asp:TextBox ID="txtIFSC"
                        runat="server"
                        CssClass="form-control" />

                    <label>Account No.</label>
                    <asp:TextBox ID="txtAccountNo"
                        runat="server"
                        CssClass="form-control" />

                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnUpdate"
                    runat="server"
                    Text="Save"
                    CssClass="button"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnUpdate_Click" Width="100%" />
            </div>
        </div>
    </asp:Panel>

    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>


    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server">
        </iframe>
    </asp:Panel>

    <asp:Button ID="btnShowTeamMembers" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnShowTeamMembers"
        PopupControlID="pnlShowTeamMembers" CancelControlID="imgBtnCancelShowTeamMembers" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlShowTeamMembers" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelShowTeamMembers" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 23px; margin-top: 10px;">
            <legend style="text-align: center;">Deactive Employee
            </legend>
            <div style='overflow: auto; width: 99%; height: 600px; border: 1px solid lightgray; margin-left: 5px;'>
                <table style="width: 95%; margin-left: 20px;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>New Team Leader:
                        </td>
                        <td>

                            <asp:Label ID="lblCurrentTeamLeaderID" runat="server" Visible="false" />
                            <asp:DropDownList ID="ddlNewTeamleader" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateNewTeamleader();">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Employee ID:
                        </td>
                        <td>
                            <asp:Button ID="btnDeactivate" CssClass="button" Width="100%" runat="server" Text="Deactive"
                                OnClientClick="return ValidateAllNew();" OnClick="btnDeactivate_Click" />
                        </td>
                    </tr>
                </table>

                <br />

                <div align="center">
                    <asp:Panel ID="pnlDeactiveMsg" Visible="false" runat="server">
                        <asp:Label ID="lblDeactiveMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <div align="center">
                    <fieldset style="width: 95%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblTeamMemberListRecords" runat="server" Text="Records[0]" /></legend>
                        <div style='overflow: auto; width: 100%; height: 100%; border: 1px solid lightgray;'>
                            <asp:GridView ID="gvTeamMemberList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EMPLOYEE_NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                            <asp:Label ID="lblEmployeeName" runat="server" Visible="true" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                                    <asp:BoundField DataField="TEAM_LEADER" HeaderText="TEAM_LEADER" />
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

            </div>
        </fieldset>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
