<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TourExpense.aspx.cs" Inherits="TOUR_AND_TRAVELS_TOUR_EXPENSE_TourExpense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/Javascript">

        function checkDecNew1(el) {
            var ex = /^-?[0-9]+(?:\.[0-9]+)?$/  ///^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    checkDecNew2();
                }
            }
            else {
                checkDecNew2();
            }
        }

        function checkDecNew2() {
            var advanceObtained = '0';
            var AirTicket = '0';
            var Hotel = '0';
            var Taxi = '0';
            var Others = '0';

            if (document.getElementById('<%=txtAirTicket.ClientID %>').value != '') {
                AirTicket = document.getElementById('<%=txtAirTicket.ClientID %>').value;
            }
            else {
                AirTicket = '0';
            }

            if (document.getElementById('<%=txtHotel.ClientID %>').value != '') {
                Hotel = document.getElementById('<%=txtHotel.ClientID %>').value;
            }
            else {
                Hotel = '0';
            }

            if (document.getElementById('<%=txtTaxi.ClientID %>').value != '') {
                Taxi = document.getElementById('<%=txtTaxi.ClientID %>').value;
            }
            else {
                Taxi = '0';
            }

            if (document.getElementById('<%=txtOthers.ClientID %>').value != '') {
                Others = document.getElementById('<%=txtOthers.ClientID %>').value;
            }
            else {
                Others = '0';
            }


            document.getElementById('<%=hdTotal.ClientID %>').value = Math.round(
                (
                    parseFloat(AirTicket)
                    + parseFloat(Hotel)
                    + parseFloat(Taxi)
                    + parseFloat(Others)
                ) * 100) / 100;


            document.getElementById('<%=txtTotal.ClientID %>').value = document.getElementById('<%=hdTotal.ClientID %>').value;

            if (document.getElementById('<%=txtAdvanceObtained.ClientID %>').value != '') {
                advanceObtained = document.getElementById('<%=txtAdvanceObtained.ClientID %>').value;
            }
            else {
                advanceObtained = '0';
            };


            <%--document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value = Math.round(parseFloat(advanceObtained) - parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value));
            document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value = document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value


            if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) == 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = '';
            }

            else if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
            }
            else {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
            }--%>
        }

    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/Javascript">
        function checkDecNew5(el) {
            var ex = /^[0-9]+\*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

    <script type="text/javascript">

        function ValidateSanctionNo() {
            var tourSanctionNo = document.getElementById('<%=txtTourSanctionNo.ClientID %>').value;
            if (tourSanctionNo == '') {
                document.getElementById('<%=txtTourSanctionNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTourSanctionNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function Validatefile1Upload() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var file1Upload = document.getElementById('<%=file1Upload.ClientID %>').value;
            var divfile1Upload = document.getElementById("divfile1Upload");
            var lblfile1Upload = document.getElementById('<%=lblfile1Upload.ClientID %>');

            var airTicketAmount = document.getElementById('<%=txtAirTicket.ClientID %>').value;
            var hotelAmount = document.getElementById('<%=txtHotel.ClientID %>').value;

            if (airTicketAmount == '')
                airTicketAmount = 0;

            if (hotelAmount == '')
                hotelAmount = 0;

            var totalAmount = airTicketAmount + hotelAmount;

            if (totalAmount > 0) {
                if (file1Upload == '') {
                    document.getElementById('<%=file1Upload.ClientID %>').style.borderColor = "#F7627F";
                    divfile1Upload.style.display = "none";
                    lblfile1Upload.innerHTML = "";
                    return true;
                }
                else {
                    if (!regex.test(file1Upload.toLowerCase())) {
                        document.getElementById('<%=file1Upload.ClientID %>').style.borderColor = "#F7627F";
                        divfile1Upload.style.display = "block";
                        lblfile1Upload.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=file1Upload.ClientID %>').style.borderColor = "";
                        divfile1Upload.style.display = "none";
                        lblfile1Upload.innerHTML = "";
                        return false;
                    }
                }
            }
            else {
                document.getElementById('<%=file1Upload.ClientID %>').style.borderColor = "";
                divfile1Upload.style.display = "none";
                lblfile1Upload.innerHTML = "";
                return false;
            }
        }



        function Validatefile2Upload() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var file2Upload = document.getElementById('<%=file2Upload.ClientID %>').value;
            var divfile2Upload = document.getElementById("divfile2Upload");
            var lblfile2Upload = document.getElementById('<%=lblfile2Upload.ClientID %>');

            if (file2Upload == '') {
                document.getElementById('<%=file2Upload.ClientID %>').style.borderColor = "";
                divfile2Upload.style.display = "none";
                lblfile2Upload.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(file2Upload.toLowerCase())) {
                    document.getElementById('<%=file2Upload.ClientID %>').style.borderColor = "#F7627F";
                    divfile2Upload.style.display = "block";
                    lblfile2Upload.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=file2Upload.ClientID %>').style.borderColor = "";
                    divfile2Upload.style.display = "none";
                    lblfile2Upload.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateSanctionNo()) {
                check = false;
            }

            if (Validatefile1Upload()) {
                check = false;
            }

            if (Validatefile2Upload()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to save tour expense?")) {
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


            return check;
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


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Tour Expense</legend>

            <div class="form-grid form-grid-2">

                <label>Sanction No.</label>
                <table width="100%">
                    <tr>
                        <td style="width: 80%">
                            <asp:TextBox ID="txtTourSanctionNo" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>
                        <td style="width: 20%">
                            <asp:Button ID="btnGetTourSanctionNo" CssClass="button" runat="server" Text="Get"
                                Width="100%"
                                OnClick="btnGetTourSanctionNo_Click" />
                        </td>
                    </tr>
                </table>

                <label>Tour No.</label>
                <asp:TextBox ID="txtTourNo"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Employee Name</label>
                <asp:TextBox ID="txtEmployeeName"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Employee ID</label>
                <asp:TextBox ID="txtEmployeeID"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

            </div>


            <div class="form-grid form-grid-2">


                <label>Tour Dates</label>
                <div class="full-width">

                    <table width="100%">
                        <tr>
                            <td style="width: 28%;">
                                <asp:TextBox ID="txtStartDate"
                                    runat="server"
                                    Enabled="false"
                                    ReadOnly="true"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 28%;">
                                <asp:TextBox ID="txtEndDate"
                                    runat="server"
                                    Enabled="false"
                                    ReadOnly="true"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 5%;">Days:
                            </td>
                            <td style="width: 10%;">
                                <asp:TextBox ID="txtDays" runat="server"
                                    Enabled="false"
                                    ReadOnly="true"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                </div>


                <label>Name of Customer/Vendor</label>
                <asp:TextBox ID="txtCustVendName"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Place of Visit</label>
                <asp:TextBox ID="txtPlaceOfVisit"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Purpose Of Visit</label>
                <asp:TextBox ID="txtPurposeOfVisit"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Job/Inq No. Where Applicable</label>
                <asp:TextBox ID="txtJobInqNo"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />

                <label>Business Segment</label>
                <asp:TextBox ID="txtBusinessSegment"
                    CssClass="form-control"
                    runat="server"
                    Enabled="false" />


                <label>Attachment1</label>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="file1Upload" runat="server"                                
                                BorderStyle="Groove"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfile1Upload" style="display: none;">
                                <asp:Label ID="lblfile1Upload" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <label>Attachment2</label>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="file2Upload" runat="server"                               
                                BorderStyle="Groove"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfile2Upload" style="display: none;">
                                <asp:Label ID="lblfile2Upload" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <label>Air Ticket</label>
                <asp:TextBox ID="txtAirTicket"
                    runat="server"
                    onKeyUp="checkDecNew1(this)"
                    onpaste="return false"
                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                    Text="0"
                    CssClass="form-control" />


                <label>Hotel</label>
                <asp:TextBox ID="txtHotel"
                    runat="server"
                    onKeyUp="checkDecNew1(this)"
                    onpaste="return false"
                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                    Text="0"
                    CssClass="form-control" />


                <label>Taxi</label>
                <asp:TextBox ID="txtTaxi"
                    runat="server"
                    onKeyUp="checkDecNew1(this)"
                    onpaste="return false"
                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                    Text="0"
                    CssClass="form-control" />


                <label>Others</label>
                <asp:TextBox ID="txtOthers"
                    runat="server"
                    onKeyUp="checkDecNew1(this)"
                    onpaste="return false"
                    onkeypress="return inNumberKeyWithDecimal(this, event);"
                    Text="0"
                    CssClass="form-control" />


                <label>Total Amount</label>
                <table>
                    <tr>
                        <td style="width: 65%;">
                            <asp:TextBox ID="txtTotal" runat="server" Text="0.00" Enabled="false"
                                CssClass="form-control" />
                            <asp:HiddenField ID="hdTotal" runat="server" />
                        </td>
                        <td style="width: 35%;">
                            <asp:DropDownList ID="ddlTotalCurrency" runat="server"
                                Enabled="false"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Advance Obtained</label>
                <table>
                    <tr>
                        <td style="width: 65%;">
                            <asp:TextBox ID="txtAdvanceObtained"
                                runat="server"
                                Text="0.00"
                                Enabled="false"
                                onkeyup="checkDec(this);"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 35%;">
                            <asp:DropDownList ID="ddlAdvanceObtainedCurreny" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </td>
                    </tr>
                </table>

                <label>Remarks</label>
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
                OnClick="btnSubmit_Click" Width="50%" />

            <asp:Button ID="btnExpenseList"
                runat="server"
                Text="Expense List"
                CssClass="button"
                OnClick="btnExpenseList_Click" Width="50%" />
        </div>
        <div class="full-width">
            <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
            </asp:Panel>

            <asp:Panel ID="pnlExceptionMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblExceptionMsg" runat="server" Font-Bold="True" />
            </asp:Panel>
        </div>


    </div>

    <%--TOUR SANCTION NO DETAIL START--%>
    <asp:Button ID="btnShowPopupTourSanctionNo" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeTourSanctionNo" runat="server" TargetControlID="btnShowPopupTourSanctionNo"
        PopupControlID="pnlPopupTourSanctionNo" CancelControlID="imgBtnCancelTourSanctionNo" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupTourSanctionNo" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelTourSanctionNo" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblTourSanctionNoRecords" runat="server" Text="Records[0]" /></legend>

                    

                    <div class="form-grid form-grid-3">
                        <label>Tour Sanction No.</label>
                        <asp:TextBox ID="txtTourSanctionNoToG" runat="server" CssClass="form-control" />

                        <label>Employee Name</label>
                        <asp:TextBox ID="txtEmployeeNameToG" runat="server" CssClass="form-control" />

                        &nbsp;
                 <asp:Button ID="btnbtnSearchTourSanctionNo"
                     OnClick="btnbtnSearchTourSanctionNo_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                    </div>

                </fieldset>
            </div>


            <div class="popup-grid-container">

                <div align="center">
                    <asp:Label ID="lblTourSanctionNoMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvTourSanctionNo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvTourSanctionNo_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET">
                            <ItemTemplate>
                                <asp:Label ID="lblTourId" runat="server" Visible="false" Text='<%# Eval("TOUR_ID") %>' />
                                <asp:Label ID="lblTourNo" runat="server" Visible="false" Text='<%# Eval("TOUR_NO") %>' />
                                <asp:Label ID="lblTourSanctionNo" runat="server" Visible="false" Text='<%# Eval("TOUR_SANCTION_NO") %>' />
                                <asp:Label ID="lblEmployeeName" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                <asp:Label ID="lblEmployeeId" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_ID") %>' />
                                <asp:Label ID="lblDesignation" runat="server" Visible="false" Text='<%# Eval("DESIGNATION") %>' />
                                <asp:Label ID="lblStartDate" runat="server" Visible="false" Text='<%# Eval("START_DATE") %>' />
                                <asp:Label ID="lblEndDate" runat="server" Visible="false" Text='<%# Eval("END_DATE") %>' />
                                <asp:Label ID="lblCustVendName" runat="server" Visible="false" Text='<%# Eval("CUST_VEND_NAME") %>' />
                                <asp:Label ID="lblPlaceOfVisit" runat="server" Visible="false" Text='<%# Eval("PLACE_OF_VISIT") %>' />
                                <asp:Label ID="lblVisitType" runat="server" Visible="false" Text='<%# Eval("VISIT_TYPE") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblBusSegment" runat="server" Visible="false" Text='<%# Eval("BUS_SEGMENT") %>' />
                                <asp:Label ID="lblAdvanceAmt" runat="server" Visible="false" Text='<%# Eval("ADVANCE_AMT") %>' />
                                <asp:Label ID="lblAdvanceCurrencyID" runat="server" Visible="false" Text='<%# Eval("ADVANCE_CURRENCY") %>' />
                                <asp:Label ID="lblCurrencyCode" runat="server" Visible="false" Text='<%# Eval("CURRENCY_CODE") %>' />
                                
                                <asp:Button ID="btnGetProductionOrderNo" CommandArgument="GET" ToolTip="Get Production Order No"
                                    runat="server" Text="Get" CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TOUR_NO" HeaderText="TOUR_NO" />
                        <asp:BoundField DataField="TOUR_SANCTION_NO" HeaderText="TOUR_SANCTION_NO" />
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="EMPLOYEE_NAME" />
                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" />
                        <asp:BoundField DataField="DESIGNATION" HeaderText="DESIGNATION" />
                        <asp:BoundField DataField="START_DATE" HeaderText="START_DATE" />
                        <asp:BoundField DataField="END_DATE" HeaderText="END_DATE" />
                        <asp:BoundField DataField="CUST_VEND_NAME" HeaderText="CUST_VEND_NAME" />
                        <asp:BoundField DataField="PLACE_OF_VISIT" HeaderText="PLACE_OF_VISIT" />
                        <asp:BoundField DataField="VISIT_TYPE" HeaderText="VISIT_TYPE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BUS_SEGMENT" HeaderText="BUS_SEGMENT" />
                        <asp:BoundField DataField="ADVANCE_AMT" HeaderText="ADVANCE_AMT" />
                        <asp:BoundField DataField="CURRENCY_CODE" HeaderText="CURRENCY_CODE" />
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
    <%--TOUR SANCTION NO DETAIL END--%>
</asp:Content>

