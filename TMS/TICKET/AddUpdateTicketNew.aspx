<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddUpdateTicketNew.aspx.cs" Inherits="TICKET_AddUpdateTicketNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ValidateTicketType() {
            var TicketType = document.getElementById('<%=ddlTicketType.ClientID %>').selectedIndex;
            if (TicketType == '' || TicketType == '0') {
                document.getElementById('<%=ddlTicketType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTicketType.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatefileUploadAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment1 = document.getElementById('<%=fileUploadAttachment1.ClientID %>').value;
            var divfileUploadAttachment1 = document.getElementById("divfileUploadAttachment1");
            var lblfileUploadAttachment1 = document.getElementById('<%=lblfileUploadAttachment1.ClientID %>');

            if (fileUploadAttachment1 == '') {
                document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                divfileUploadAttachment1.style.display = "none";
                lblfileUploadAttachment1.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment1.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadAttachment1.style.display = "block";
                    lblfileUploadAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment1.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment1.style.display = "none";
                    lblfileUploadAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateTicketDescription() {
            var TicketDescription = document.getElementById('<%=txtTicketDescription.ClientID %>').value;
            if (TicketDescription == '') {
                document.getElementById('<%=txtTicketDescription.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTicketDescription.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAll() {
            if (ValidateTicketType()) { return false; }
            if (ValidatefileUploadAttachment1()) { return false; }
            if (ValidateTicketDescription()) { return false; }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 100px;">
        <fieldset style="width: 50%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" Text="Create Ticket" /></legend>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        Ticket Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTicketType" runat="server" Width="100%" Height="26px" onblur="return ValidateTicketType();">
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
                        Attachment:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadAttachment1" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadAttachment1();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadAttachment1" style="display: none;">
                            <asp:Label ID="lblfileUploadAttachment1" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTicketDescription" runat="server" TextMode="MultiLine" Width="100%"
                            Height="100px" onblur="return ValidateTicketDescription();" />
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
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCreate" runat="server" CssClass="button" OnClick="btnCreate_Click"
                                        Width="100%" OnClientClick="return ValidateAll();" Text="Create" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnTicketList" runat="server" CssClass="button" OnClick="btnTicketList_Click"
                                        Width="100%" Text="Ticket List" />
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
                    <td colspan="2" align="center">
                        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
