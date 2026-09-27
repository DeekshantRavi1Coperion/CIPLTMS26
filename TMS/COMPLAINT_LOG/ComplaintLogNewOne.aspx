<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ComplaintLogNewOne.aspx.cs"
    Inherits="COMPLAINT_LOG_ComplaintLogNewOne" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icon04.png" />
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

            document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value = document.getElementById('<%=hdComplanitRcvdDate.ClientID %>').value;

            document.getElementById('<%=hdComplanitRcvdDate.ClientID %>').value = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value;
            document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value = document.getElementById('<%=hdComplanitRcvdDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdComplanitRcvdDate.ClientID %>').value = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value;
            document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value = document.getElementById('<%=hdComplanitRcvdDate.ClientID %>').value;
        }
    </script>

    <script type="text/javascript">
        function ValidateCustomerName() {
            var CustomerName = document.getElementById('<%=txtCustomerName.ClientID %>').value;
            if (CustomerName == '') {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAddress() {
            var Address = document.getElementById('<%=txtAddress.ClientID %>').value;
            if (Address == '') {
                document.getElementById('<%=txtAddress.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAddress.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLocation() {
            var Location = document.getElementById('<%=txtLocation.ClientID %>').value;
            if (Location == '') {
                document.getElementById('<%=txtLocation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtLocation.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlant() {
            var Plant = document.getElementById('<%=txtPlant.ClientID %>').value;
            if (Plant == '') {
                document.getElementById('<%=txtPlant.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlant.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemName() {
            var ItemName = document.getElementById('<%=txtItemName.ClientID %>').value;
            if (ItemName == '') {
                document.getElementById('<%=txtItemName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateModelNo() {
            var ModelNo = document.getElementById('<%=txtModelNo.ClientID %>').value;
            if (ModelNo == '') {
                document.getElementById('<%=txtModelNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtModelNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTypeOfService() {
            var TypeOfService = document.getElementById('<%=ddlTypeOfService.ClientID %>').selectedIndex;
            if (TypeOfService == '' || TypeOfService == '0') {
                document.getElementById('<%=ddlTypeOfService.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfService.ClientID %>').style.borderColor = "";
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

        function ValidatefileUploadAttachment2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
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
                    lblfileUploadAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
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
    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateCustomerName()) { return false; }
            if (ValidateAddress()) { return false; }
            if (ValidateLocation()) { return false; }
            if (ValidatePlant()) { return false; }
            if (ValidateItemName()) { return false; }
            if (ValidateModelNo()) { return false; }
            if (ValidateTypeOfService()) { return false; }
            if (ValidatefileUploadAttachment1()) { return false; }
            if (ValidatefileUploadAttachment2()) { return false; }

            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--  <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>--%>

    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>
                <asp:Label ID="lblLegend" runat="server" Text="Complaint Log"></asp:Label>
            </legend>

            <div class="form-grid form-grid-2">
                <label>Date of Complaint Received</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox
                                CssClass="form-control"
                                ID="txtComplanitRcvdDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                            <asp:HiddenField ID="hdComplanitRcvdDate" runat="server" />
                            <asp:CalendarExtender ID="calendarComplanitRcvdDate" PopupButtonID="imgbtnComplanitRcvdDate"
                                runat="server" TargetControlID="txtComplanitRcvdDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnComplanitRcvdDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Date of Complaint Received Calendar" />
                        </td>
                    </tr>
                </table>

            </div>

            <div class="form-grid form-grid-2">

                <label>Customer Name</label>

                <div class="full-width">

                    <table width="100%">
                        <tr>
                            <td style="width: 65%;">
                                <asp:TextBox ID="txtCustomerName" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txtCustomerCode" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnGetCustomer" CssClass="button" runat="server" Text="Get Customer"
                                    Width="100%" OnClick="btnGetCustomer_Click" />
                            </td>
                        </tr>
                    </table>
                </div>


                <label>Address</label>
                <div class="full-width">
                    <asp:TextBox ID="txtAddress"
                        Enabled="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>

                <label>Location</label>
                <asp:TextBox ID="txtLocation" runat="server"
                    CssClass="form-control" />


                <label>Plant</label>
                <asp:TextBox ID="txtPlant" runat="server"
                    CssClass="form-control" />

                <label>JOB No</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtJOBNo" runat="server"
                                CssClass="form-control" />
                        </td>
                        <td>
                            <asp:Button ID="btnGetJobNo" CssClass="button"
                                runat="server" Text="Get JOB No."
                                OnClick="btnGetJobNo_Click" Visible="false" />
                        </td>
                    </tr>
                </table>

                <label>Customer PO Number</label>
                <asp:TextBox ID="txtPONumber" runat="server"
                    CssClass="form-control" />

                <label>Item Name</label>
                <asp:TextBox ID="txtItemName" runat="server"
                    CssClass="form-control" />

                <label>Model No</label>
                <asp:TextBox ID="txtModelNo" runat="server"
                    CssClass="form-control" />

                <label>Type of Service</label>
                <asp:DropDownList ID="ddlTypeOfService" runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

            </div>

            <div class="form-grid form-grid-2">

                <label>Complaint Description</label>
                <div class="full-width">
                    <asp:TextBox ID="txtComplaintDescription"
                        Enabled="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>

            </div>

            <div class="form-grid form-grid-2">

                <label>Attachments</label>

                <div class="full-width">

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadAttachment1" runat="server"
                                    BorderStyle="Groove"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileUploadAttachment1" style="display: none;">
                                    <asp:Label ID="lblfileUploadAttachment1" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadAttachment2" runat="server"
                                    BorderStyle="Groove"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileUploadAttachment2" style="display: none;">
                                    <asp:Label ID="lblfileUploadAttachment2" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>

            </div>
        </fieldset>

        <div class="full-width button-group">
            <asp:Button ID="btnSubmit"
                runat="server"
                Text="Save"
                CssClass="button"
                OnClientClick="return ValidateAll();"
                OnClick="btnSubmit_Click" Width="50%" />

            <asp:Button ID="btnComplaintLogList"
                runat="server"
                Text="Complaint Log List"
                CssClass="button"
                OnClick="btnComplaintLogList_Click" Width="50%" />
        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="30px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
        </div>

    </div>



    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtCustmerNameSearch" runat="server"
                            CssClass="form-control" />

                        <label>PO No.</label>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server"
                            CssClass="form-control" />

                        <asp:Button ID="btnSearchCustomer"
                            OnClick="btnSearchCustomer_Click"
                            runat="server"
                            Text="Search"
                            CssClass="button" />

                    </div>
                </fieldset>
            </div>
            <div class="popup-grid-container">

                <asp:Label ID="lblCustomerMsg" runat="server" Font-Bold="True" Visible="false" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvCustomerAddressList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="8" Width="100%"
                    HorizontalAlign="Center" OnRowCommand="gvCustomerAddressList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET_CUSTOMER" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblAddress" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />
                                <asp:Button ID="btnGetCustomer" CommandArgument="GET_CUSTOMER" ToolTip="Get Customer"
                                    runat="server" Text="Get Customer" CssClass="cancelbutton" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                        <asp:BoundField DataField="CITY" HeaderText="CITY" />
                        <asp:BoundField DataField="STATE" HeaderText="STATE" />
                        <asp:BoundField DataField="PINCODE" HeaderText="PINCODE" />
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

    </asp:Panel>

    <asp:Button ID="btnShowPopupJobNo" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopupJobNo"
        PopupControlID="pnlpopupJobNo" CancelControlID="imgBtnCancelJobNo" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopupJobNo" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJobNo" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblRecordsJobNo" runat="server" Text="Records[0]" /></legend>
                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJobNoSearch" runat="server"
                            CssClass="form-control" />

                        <label>PO No.</label>
                        <asp:TextBox ID="txtPONoSearch" runat="server"
                            CssClass="form-control" />

                        <asp:Button ID="btnGetJobNoSearch"
                            OnClick="btnGetJobNoSearch_Click"
                            runat="server"
                            Text="Search"
                            CssClass="button" />

                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">
                <asp:Label ID="lblJOBNoMsg" runat="server" Font-Bold="True" Visible="false" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvCustomerJobList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="8" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvCustomerJobList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET_JOB_NO" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET_JOB_NO" ToolTip="Get JOB No." runat="server"
                                    Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
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
    </asp:Panel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
