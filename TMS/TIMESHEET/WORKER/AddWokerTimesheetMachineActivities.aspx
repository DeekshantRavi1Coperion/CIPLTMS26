<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddWokerTimesheetMachineActivities.aspx.cs" 
    Inherits="TIMESHEET_WORKER_AddWokerTimesheetMachineActivities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tour Information</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateActivityName() {
            var ActivityName = document.getElementById('<%=txtActivityName.ClientID %>').value;
            if (ActivityName == '') {
                document.getElementById('<%=txtActivityName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtActivityName.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {

            var ActivityName = document.getElementById('<%=txtActivityName.ClientID %>').value;
            if (ActivityName == '') {
                document.getElementById('<%=txtActivityName.ClientID %>').style.borderColor = "#F7627F";
                return false;
            }

            return true;
        }

    </script>

    <style type="text/css">
        .boxcss {
            margin-top: 12%;
            width: 50%;
            border-radius: 10px;
            background-color: skyblue;
            /*opacity: 0.9;*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center">

                <div class="boxcss">
                    <br />
                    <fieldset style="width: 80%;">
                        <legend style="text-align: center;">
                            <asp:Label ID="lblLegend" runat="server" Text="Add Menu"></asp:Label></legend>
                        <table width="80%">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        Activity Name:
                                    </p>
                                    <p>
                                        <asp:TextBox ID="txtActivityName" runat="server" Width="100%" onblur="return ValidateActivityName();" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>                                                       
                            <tr>
                                <td>
                                    <table width="100%;">
                                        <tr>
                                            <td style="width: 45%;">
                                                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                                                    Width="100%" OnClick="btnSubmit_Click" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td style="width: 45%;">
                                                <asp:Button ID="btnMenuList" CssClass="button" runat="server" Text="Menu List"
                                                    Width="100%" OnClick="btnMenuList_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center"></td>
                            </tr>
                            <tr>
                                <td align="center"></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
