<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddDeisgnDrawing.aspx.cs" Inherits="PROJECT_DMS_AddDeisgnDrawing" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">

        function pageLoad() {
            document.getElementById('<%=txtDateByProjectTeam.ClientID %>').value = document.getElementById('<%=hdDateByProjectTeam.ClientID %>').value;
        }

        function clientChangedDateByProjectTeam(sender, args) {
            document.getElementById('<%=hdDateByProjectTeam.ClientID %>').value = document.getElementById('<%=txtDateByProjectTeam.ClientID %>').value;
        }

    </script>


    <script type="text/javascript">

        function ValidateJobNo() {
            var JOBNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingNumber() {
            var DrawingNumber = document.getElementById('<%=txtDrawingNumber.ClientID %>').value;
            if (DrawingNumber == '') {
                document.getElementById('<%=txtDrawingNumber.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingNumber.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescription.ClientID %>').value;
            if (Description == '') {
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value;
            if (Quantity == '' || parseInt(Quantity) == 0) {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUOM() {
            var UOM = document.getElementById('<%=txtUOM.ClientID %>').value;
            if (UOM == '') {
                document.getElementById('<%=txtUOM.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUOM.ClientID %>').style.borderColor = "";
                return false;
            }
        }



    </script>



    <script type="text/javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            if (ValidateDrawingNumber()) {
                check = false;
            }

            if (ValidateDescription()) {
                check = false;
            }

            if (ValidateQuantity()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }

            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to save design drawing?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }
    </script>



    <script type="text/javascript">

        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;

    </script>

    <script type="text/javascript">

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>
               Add Design Request</legend>

            <div class="form-grid form-grid-2">

                <label>Company</label>
                <asp:DropDownList ID="ddlCompany" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />

                <label>JOB Number</label>
                <table width="100%">
                    <tr>
                        <td style="width: 90%">
                            <asp:TextBox ID="txtJOBNo" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>

                        <td style="width: 10%">
                            <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                OnClick="btnGetJOBNo_Click" />
                        </td>
                    </tr>
                </table>

                <label>Drawing Number</label>
                <asp:TextBox ID="txtDrawingNumber" runat="server"
                    CssClass="form-control"
                    Style="text-transform: uppercase" />

                <label>Client Drawing Number</label>
                <asp:TextBox ID="txtClientDrawingNumber" runat="server"
                    CssClass="form-control"
                    Style="text-transform: uppercase" />

                <label>Contractor Drawing Number</label>
                <asp:TextBox ID="txtContractorDrawingNumber" runat="server"
                    CssClass="form-control"
                    Style="text-transform: uppercase" />



                <label>Quantity</label>
                <asp:TextBox ID="txtQuantity" runat="server"
                    CssClass="form-control"
                    onkeypress="return inNumberKey(this, event);" />


                <div class="full-width">
                    <label>Description</label>
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="2"
                        runat="server"
                        CssClass="form-control" />
                </div>


                <label>UOM</label>
                <asp:TextBox ID="txtUOM" runat="server"
                    CssClass="form-control" />

                <label>Reqd. Date By Project Team</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDateByProjectTeam" runat="server" onkeyDown="javascript:preventInput(event);"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdDateByProjectTeam" runat="server" />
                            <ajax:CalendarExtender ID="calendarDateByProjectTeam" PopupButtonID="imgBtnDateByProjectTeam" runat="server"
                                TargetControlID="txtDateByProjectTeam" Format="dd-MMM-yyyy"
                                OnClientDateSelectionChanged="clientChangedDateByProjectTeam">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnDateByProjectTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Calendar" />
                        </td>
                    </tr>
                </table>


            </div>
        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnAddDrawing" runat="server" Width="100%" Text="Add Design" CssClass="button"
                OnClick="btnAddDrawing_Click" OnClientClick="return ValidateAll();" />


            <asp:Button ID="btnImportDrawings" CssClass="button" Width="100%" runat="server" Text="Import Design List"
                OnClick="btnImportDrawings_Click" />

            <asp:Button ID="btnDrawingList" runat="server" Width="100%" Text="View Design List" CssClass="button"
                OnClick="btnDrawingList_Click" />

        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>

    </div>


    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />
                    </div>                   
                </fieldset>
            </div>

            <div class="employee-grid-container">

                        <asp:Label ID="lblJOBMsg" runat="server" />

                        <asp:GridView
                            CssClass="employee-grid"
                            ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Get JOB No." HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                        <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                            runat="server" Text="Get JOB No." CssClass="button" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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
    <%--JOB DETAIL END--%>
</asp:Content>
