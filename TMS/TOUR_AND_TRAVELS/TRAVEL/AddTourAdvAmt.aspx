<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddTourAdvAmt.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_AddTourAdvAmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link rel="icon" href="../../Images/Icon04.png" />
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


    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            ValidateDateRange();

        }
    </script>

    <script type="text/javascript">


        function ValidateAll() {

            var check = true;

            function ValidateSanctionNo() {
                var SanctionNo = document.getElementById('<%=ddlSanctionNo.ClientID %>').selectedIndex;
                if (SanctionNo == '' || SanctionNo == '0') {
                    document.getElementById('<%=ddlSanctionNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlSanctionNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }


            function ValidateRemarks() {
                var remarks = document.getElementById('<%=txtRemarks.ClientID %>').value;
                if (remarks === '') {
                    document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateAdditionalAdvanceAmt() {
                var additionaladvance = document.getElementById('<%=txtAdvanceAmt.ClientID %>').value;
                if (additionaladvance == '' || additionaladvance == 0) {
                    document.getElementById('<%=txtAdvanceAmt.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtAdvanceAmt.ClientID %>').style.borderColor = "";
                    return false;
                }
            }



            if (ValidateSanctionNo()) {
                check = false;
            }


            if (ValidateAdditionalAdvanceAmt()) {
                check = false;
            }


            if (ValidateRemarks()) {
                check = false;
            }




            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
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

     <script type="text/Javascript">
         function checkDec(el) {
             var ex = /^[0-9]+\.?[0-9]*$/;
             if (ex.test(el.value) == false) {
                 el.value = el.value.substring(0, el.value.length - 1);
             }
         }
     </script>






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%-- <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Additional Advance Amount Request</legend>


            <div class="form-grid form-grid-2">

                <label>Sanction No</label>
                <asp:DropDownList ID="ddlSanctionNo"
                    runat="server"
                    CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged" />


                <label>Tour No</label>
                <asp:TextBox ID="txtTourNo"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control" />
                <asp:HiddenField ID="hdTourID" runat="server" />


                <label>Name of Customer/Vendor</label>
                <asp:TextBox ID="txtCustVendName"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control" />



                <label>Place of Visit</label>
                <asp:TextBox ID="txtPlaceOfVisit"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control" />



                <label>Starting Date Of Tour</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtStartDate"
                                runat="server"
                                ReadOnly="true" CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdStartDate" runat="server" />
                            <asp:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDateSearch"
                                runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Start Date Calendar" Enabled="false" />
                        </td>
                    </tr>
                </table>


                <label>End Date Of Tour</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEndDate"
                                runat="server"
                                ReadOnly="true"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdEndDate" runat="server" />
                            <asp:CalendarExtender ID="calendarEndDate" PopupButtonID="imgbtnEndDateSearch" runat="server"
                                TargetControlID="txtEndDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="End Date Calendar" Width="20px" Enabled="false" />
                        </td>
                    </tr>
                </table>

                <label>Advance Taken</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%;">
                            <asp:TextBox ID="txtPrevAmt"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control"
                                onkeyup="checkDec(this);"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPrevAmt"
                                runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>
                    </tr>
                </table>

                <label>Additional Advance Requested</label>
                <table width="100%">
                    <tr>
                        <td style="width: 70%;">
                            <asp:TextBox ID="txtAdvanceAmt"
                                runat="server"
                                Enabled="true"
                                CssClass="form-control"
                                onkeyup="checkDec(this);"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAdvanceCurrency"
                                runat="server"
                                CssClass="form-control"
                                onchange="ChangeByAdvanceCurrency()"
                                Enabled="false" />
                        </td>
                    </tr>
                </table>



                <label>Reason</label>
                <div class="full-width">

                    <asp:TextBox ID="txtRemarks"
                        Enabled="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>
            </div>
        </fieldset>

        <div class="full-width button-group">
            <asp:Button ID="btnSubmit"
                runat="server"
                Text="Save"
                CssClass="button"
                OnClientClick="return ValidateAll();"
                OnClick="btnSubmit_Click"
                Width="50%" />

            <asp:Button ID="btnList"
                runat="server"
                Text="View List"
                CssClass="button"
                OnClick="btnList_Click" Width="50%" />
        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>
    </div>



    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup"
        runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <%-- ///////////////////////////--%>

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>
                    <asp:Label ID="lblOpenTours" runat="server" Text="Records[0]" /></legend>
                <table style="width: 95%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblOpenToursMsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large" />
                        </td>
                    </tr>
                </table>
                <div class="form-grid form-grid-3">
                    <asp:GridView ID="gvOpenTours"
                        CssClass="employee-grid"
                        runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:BoundField DataField="REQUEST_ID" HeaderText="REQUEST_ID" />--%>
                            <asp:BoundField DataField="REQ_NO" HeaderText="REQUEST_NUMBER" />
                            <asp:BoundField DataField="TOUR_NO" HeaderText="TOUR_NO" />
                            <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="TOUR_SANCTION_NO" />
                            <asp:BoundField DataField="ADD_ADVANCE_STATUS_NAME" HeaderText="REQUEST_STATUS" />
                            <asp:BoundField DataField="CREATED_BY_NAME" HeaderText="CREATED_BY" />
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
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
