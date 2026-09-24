<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="EmployeeProfile.aspx.cs" Inherits="ADMIN_EmployeeProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Employee Profile</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 40px;">
                <fieldset style="width: 60%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblLegend" runat="server" Text="Employee Profile"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Employee Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Employee ID:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmployeeID" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesignation" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Department:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDepartment" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Timesheet Department:
                            </td>
                            <td>
                               <asp:TextBox ID="txtTimesheetDepartment" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Email ID:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmailID" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUnit" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Team Leader:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTeamLeader" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Type:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserType" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                            </td>
                            <td>
                                Is Active?:
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" Width="100%" ReadOnly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Passport copy:
                            </td>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:TextBox ID="txtPassportcopy" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 10%;" align="right">
                                            <asp:ImageButton ID="imgbtnPassportcopy" ImageUrl="~/Images/NEWICONS/PASSPORT_01.png"
                                                runat="server" OnClick="imgbtnPassportcopy_Click" />
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
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2" align="right">
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div align="center">
                                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
                    <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
                        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
                    </ajax:ModalPopupExtender>
                    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
                        Width="1050px" Style="display: block">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                            margin-left: 25px;'>
                            <asp:Image ID="imgFile" runat="server" />
                        </div>
                    </asp:Panel>
                    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
                    <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
                        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
                    </ajax:ModalPopupExtender>
                    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="600px"
                        Width="1050px" Style="display: block">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewPDFFile"
                            runat="server">
                            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray;
                                margin-left: 25px;'>
                            </div>
                        </iframe>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
