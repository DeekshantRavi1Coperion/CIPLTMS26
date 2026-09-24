<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddUpdateProject.aspx.cs" Inherits="PROJECT_AddUpdateProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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

        function ValidateCustomerName() {
            var JobNo = document.getElementById('<%=ddlCustomerName.ClientID %>').selectedIndex;
            if (CustomerName == '' || CustomerName == '0') {
                document.getElementById('<%=ddlCustomerName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCustomerName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBudgetedHours() {
            var BudgetedHours = document.getElementById('<%=txtBudgetedHours.ClientID %>').value;
            if (BudgetedHours == '') {
                document.getElementById('<%=txtBudgetedHours.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBudgetedHours.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateStatus() {
            var Status = document.getElementById('<%=ddlStatus.ClientID %>').selectedIndex;
            if (Status == '' || Status == '0') {
                document.getElementById('<%=ddlStatus.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStatus.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRemarks() {
            var Remarks = document.getElementById('<%=txtRemarks.ClientID %>').value;
            if (Remarks == '') {
                document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var JobNo = document.getElementById('<%=ddlJobNo.ClientID %>').selectedIndex;
            if (JobNo == '' || JobNo == '0') {
                document.getElementById('<%=ddlJobNo.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }
            
            var BudgetedHours = document.getElementById('<%=txtBudgetedHours.ClientID %>').value;
            if (BudgetedHours == '') {
                document.getElementById('<%=txtBudgetedHours.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            var Status = document.getElementById('<%=ddlStatus.ClientID %>').selectedIndex;
            if (Status == '' || Status == '0') {
                document.getElementById('<%=ddlStatus.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            var Remarks = document.getElementById('<%=txtRemarks.ClientID %>').value;
            if (Remarks == '') {
                document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "#F7627F";
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
            <div align="center" style="margin-top: 80px;">
                <fieldset style="width: 65%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblLegend" runat="server" Text="Add Project"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td>
                                JOB No.:
                            </td>
                            <td style="width: 30%;">
                                <asp:DropDownList ID="ddlJobNo" runat="server" Width="100%" Height="26px" onblur="return ValidateJobNo();"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlJobNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Customer Name:
                            </td>
                            <td style="width: 40%;">
                                <table width="100%;">
                                    <tr>
                                        <td style="width: 80%;">
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" Width="100%" Height="26px"
                                                Enabled="false" onblur="return ValidateCustomerName();" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged"
                                                AutoPostBack="true" Visible="false">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtCustomerID" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Budgeted Hours:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBudgetedHours" runat="server" Width="100%" onblur="return ValidateBudgetedHours();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" Height="26px" onblur="return ValidateStatus();">
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
                                Remarks:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                                    Rows="4" onblur="return ValidateRemarks();" />
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
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td align="center" style="width: 45%">
                                            <asp:Button ID="btnSave" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                                Width="100%" OnClick="btnSave_Click" />
                                        </td>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                        <td align="center" style="width: 45%">
                                            <asp:Button ID="btnProjectList" CssClass="button" runat="server" Text="Project List"
                                                Width="100%" OnClick="btnProjectList_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                            <td colspan="4" align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
