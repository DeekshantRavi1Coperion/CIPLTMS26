<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddUpdateTicket.aspx.cs" Inherits="HR_TICKET_AddUpdateTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../../Styles/form.css" rel="stylesheet" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

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
        function ValidateTicketSubtype() {
            var TicketSubtype = document.getElementById('<%=ddlTicketSubtype.ClientID %>').selectedIndex;
            if (TicketSubtype == '' || TicketSubtype == '0') {
                document.getElementById('<%=ddlTicketSubtype.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTicketSubtype.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatefileUploadAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".xls", ".xlsx", ".eml", ".msg", ".EML", ".MSG", ".pdf", ".JPG", ".JPEG", ".BMP", ".docx", ".DOCX", ".doc", ".DOC", ".XLS", ".XLSX", ".PNG", ".GIF", ".PDF"];
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
                    lblfileUploadAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif,.xls,.xlsx file!";
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

        function ValidatefileUploadAttachment2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".xls", ".xlsx", ".eml", ".msg", ".EML", ".MSG", ".pdf", ".JPG", ".JPEG", ".BMP", ".docx", ".DOCX", ".doc", ".DOC", ".PNG", ".XLS", ".XLSX", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment2 = document.getElementById('<%=fileUploadAttachment2.ClientID %>').value;
            var divfileUploadAttachment2 = document.getElementById("divfileUploadAttachment2");
            var lblfileUploadAttachment2 = document.getElementById('<%=lblfileUploadAttachment2.ClientID %>');

            if (fileUploadAttachment2 == '') {
                document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "";
                divfileUploadAttachment2.style.display = "none";
                lblfileUploadAttachment2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment2.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadAttachment2.style.display = "block";
                    lblfileUploadAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .xls, .xlsx file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment2.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment2.style.display = "none";
                    lblfileUploadAttachment2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileUploadAttachment3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".xls", ".xlsx", ".XLS", ".XLSX", ".eml", ".msg", ".EML", ".docx", ".DOCX", ".doc", ".DOC", ".MSG", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadAttachment3 = document.getElementById('<%=fileUploadAttachment3.ClientID %>').value;
            var divfileUploadAttachment3 = document.getElementById("divfileUploadAttachment3");
            var lblfileUploadAttachment3 = document.getElementById('<%=lblfileUploadAttachment3.ClientID %>');

            if (fileUploadAttachment3 == '') {
                document.getElementById('<%=fileUploadAttachment3.ClientID %>').style.borderColor = "";
                divfileUploadAttachment3.style.display = "none";
                lblfileUploadAttachment3.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadAttachment3.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment3.ClientID %>').style.borderColor = "#F7637F";
                    divfileUploadAttachment3.style.display = "block";
                    lblfileUploadAttachment3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .xls, .xlsx  file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment3.ClientID %>').style.borderColor = "";
                    divfileUploadAttachment3.style.display = "none";
                    lblfileUploadAttachment3.innerHTML = "";
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

            var TicketType = document.getElementById('<%=ddlTicketType.ClientID %>').selectedIndex;
            if (TicketType == 1) {
                if (ValidateTicketSubtype()) { return false; }
            }

            if (ValidatefileUploadAttachment1()) { return false; }
            if (ValidatefileUploadAttachment2()) { return false; }
            if (ValidatefileUploadAttachment3()) { return false; }
            if (ValidateTicketDescription()) { return false; }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>--%>




    <div class="form-entry-container">

        <fieldset class="form-card">
            <legend>
                <asp:Label ID="lblLegend" runat="server" Text="Create Ticket" /></legend>

            <div class="form-grid form-grid-2">


                <label>Ticket Type:</label>
                <asp:DropDownList ID="ddlTicketType"
                    runat="server"
                    CssClass="form-control"
                    OnSelectedIndexChanged="ddlTicketType_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>

                <label>Ticket Type:</label>
                <asp:DropDownList ID="ddlTicketSubtype" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

                <label>Attachment:</label>
                <asp:FileUpload ID="fileUploadAttachment1" runat="server"
                    CssClass="form-control"
                    BorderStyle="Groove" onblur="return ValidatefileUploadAttachment1();" />

                <div id="divfileUploadAttachment1" style="display: none;">
                    <asp:Label ID="lblfileUploadAttachment1" runat="server" ForeColor="Red" />
                </div>



                <label>Attachment 2:</label>
                <asp:FileUpload ID="fileUploadAttachment2" runat="server" CssClass="form-control"
                    BorderStyle="Groove" onblur="return ValidatefileUploadAttachment2();" />

                <div id="divfileUploadAttachment2" style="display: none;">
                    <asp:Label ID="lblfileUploadAttachment2" runat="server" ForeColor="Red" />
                </div>



                <label>Attachment 3:</label>
                <asp:FileUpload ID="fileUploadAttachment3" runat="server" CssClass="form-control"
                    BorderStyle="Groove" onblur="return ValidatefileUploadAttachment3();" />

                <div id="divfileUploadAttachment3" style="display: none;">
                    <asp:Label ID="lblfileUploadAttachment3" runat="server" ForeColor="Red" />
                </div>



                <div class="full-width">
                    <label>Description:</label>

                    <asp:TextBox ID="txtTicketDescription" runat="server"
                        TextMode="MultiLine"
                        CssClass="form-control"
                        Rows="3"
                        onblur="return ValidateTicketDescription();" />

                </div>

                <div class="full-width">
                    <label>Remarks:</label>
                    <asp:TextBox ID="txtTicketRemarks" runat="server"
                        TextMode="MultiLine"
                        CssClass="form-control"
                        Rows="3" />
                </div>


            </div>

        </fieldset>

        <div class="full-width button-group">
            <asp:Button ID="btnCreate" runat="server" CssClass="button" OnClick="btnCreate_Click"
                Width="100%" OnClientClick="return ValidateAll();" Text="Create" />

            <asp:Button ID="btnTicketList" runat="server" CssClass="button" OnClick="btnTicketList_Click"
                Width="100%" Text="Ticket List" />
        </div>


    </div>


    <div class="full-width">

        <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
        </asp:Panel>

    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
