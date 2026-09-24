<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TravelStatementOne.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TravelStatementOne" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/Javascript">
        function checkDecNew(el) {
            var advanceObtained = '0';
            var Airfare = '0';
            var mobile = '0';
            var lodging = '0';
            var tips = '0';
            var meals = '0';
            var visaFee = '0';
            var groundTransport = '0';
            var dailyAllowance = '0';
            var entertainment = '0';
            var other = '0';
            var gifts = '0';


            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value != '') {
                    alert(el.value);
                }
                else {
                    alert('null');
                }
            }
            else {
                if (document.getElementById('<%=txtAirfare.ClientID %>').value != '') {
                    Airfare = document.getElementById('<%=txtAirfare.ClientID %>').value;
                }
                else {
                    Airfare = '0';
                }

                if (document.getElementById('<%=txtMobile.ClientID %>').value != '') {
                    mobile = document.getElementById('<%=txtMobile.ClientID %>').value;
                }
                else {
                    mobile = '0';
                }

                if (document.getElementById('<%=txtLodging.ClientID %>').value != '') {
                    lodging = document.getElementById('<%=txtLodging.ClientID %>').value;
                }
                else {
                    lodging = '0';
                }

                if (document.getElementById('<%=txtTips.ClientID %>').value != '') {
                    tips = document.getElementById('<%=txtTips.ClientID %>').value;
                }
                else {
                    tips = '0';
                }

                if (document.getElementById('<%=txtMeals.ClientID %>').value != '') {
                    meals = document.getElementById('<%=txtMeals.ClientID %>').value;
                }
                else {
                    meals = '0';
                }

                if (document.getElementById('<%=txtVisaFee.ClientID %>').value != '') {
                    visaFee = document.getElementById('<%=txtVisaFee.ClientID %>').value;
                }
                else {
                    visaFee = '0';
                }

                if (document.getElementById('<%=txtGroundTransport.ClientID %>').value != '') {
                    groundTransport = document.getElementById('<%=txtGroundTransport.ClientID %>').value;
                }
                else {
                    groundTransport = '0';
                }

                if (document.getElementById('<%=txtDailyAllowance.ClientID %>').value != '') {
                    dailyAllowance = document.getElementById('<%=txtDailyAllowance.ClientID %>').value;
                }
                else {
                    dailyAllowance = '0';
                }

                if (document.getElementById('<%=txtEntertainment.ClientID %>').value != '') {
                    entertainment = document.getElementById('<%=txtEntertainment.ClientID %>').value;
                }
                else {
                    entertainment = '0';
                }

                if (document.getElementById('<%=txtOther.ClientID %>').value != '') {
                    other = document.getElementById('<%=txtOther.ClientID %>').value;
                }
                else {
                    other = '0';
                }

                if (document.getElementById('<%=txtGifts.ClientID %>').value != '') {
                    gifts = document.getElementById('<%=txtGifts.ClientID %>').value;
                }
                else {
                    gifts = '0';
                }

                document.getElementById('<%=txtTotal.ClientID %>').value = parseFloat(Airfare) + parseFloat(mobile) + parseFloat(lodging) +
                    parseFloat(tips) + parseFloat(meals) + parseFloat(visaFee) +
                    parseFloat(groundTransport) + parseFloat(dailyAllowance) +
                    parseFloat(entertainment) + parseFloat(other) + parseFloat(gifts);

                if (document.getElementById('<%=txtAdvanceObtained.ClientID %>').value != '') {
                    advanceObtained = document.getElementById('<%=txtAdvanceObtained.ClientID %>').value;
                }
                else {
                    advanceObtained = '0';
                };

                <%--document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value = parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value) - parseFloat(advanceObtained);

                if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
                }
                else {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
                }--%>
            }
        }
    </script>

    <script type="text/Javascript">

        function checkDecNew1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
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
            var Airfare = '0';
            var mobile = '0';
            var lodging = '0';
            var tips = '0';
            var meals = '0';
            var visaFee = '0';
            var groundTransport = '0';
            var dailyAllowance = '0';
            var entertainment = '0';
            var other = '0';
            var gifts = '0';

            if (document.getElementById('<%=txtAirfare.ClientID %>').value != '') {
                Airfare = document.getElementById('<%=txtAirfare.ClientID %>').value;
            }
            else {
                Airfare = '0';
            }

            if (document.getElementById('<%=txtMobile.ClientID %>').value != '') {
                mobile = document.getElementById('<%=txtMobile.ClientID %>').value;
            }
            else {
                mobile = '0';
            }

            if (document.getElementById('<%=txtLodging.ClientID %>').value != '') {
                lodging = document.getElementById('<%=txtLodging.ClientID %>').value;
            }
            else {
                lodging = '0';
            }

            if (document.getElementById('<%=txtTips.ClientID %>').value != '') {
                tips = document.getElementById('<%=txtTips.ClientID %>').value;
            }
            else {
                tips = '0';
            }

            if (document.getElementById('<%=txtMeals.ClientID %>').value != '') {
                meals = document.getElementById('<%=txtMeals.ClientID %>').value;
            }
            else {
                meals = '0';
            }

            if (document.getElementById('<%=txtVisaFee.ClientID %>').value != '') {
                visaFee = document.getElementById('<%=txtVisaFee.ClientID %>').value;
            }
            else {
                visaFee = '0';
            }

            if (document.getElementById('<%=txtGroundTransport.ClientID %>').value != '') {
                groundTransport = document.getElementById('<%=txtGroundTransport.ClientID %>').value;
            }
            else {
                groundTransport = '0';
            }

            if (document.getElementById('<%=txtDailyAllowance.ClientID %>').value != '') {
                dailyAllowance = document.getElementById('<%=txtDailyAllowance.ClientID %>').value;
            }
            else {
                dailyAllowance = '0';
            }

            if (document.getElementById('<%=txtEntertainment.ClientID %>').value != '') {
                entertainment = document.getElementById('<%=txtEntertainment.ClientID %>').value;
            }
            else {
                entertainment = '0';
            }

            if (document.getElementById('<%=txtOther.ClientID %>').value != '') {
                other = document.getElementById('<%=txtOther.ClientID %>').value;
            }
            else {
                other = '0';
            }

            if (document.getElementById('<%=txtGifts.ClientID %>').value != '') {
                gifts = document.getElementById('<%=txtGifts.ClientID %>').value;
            }
            else {
                gifts = '0';
            }

            document.getElementById('<%=hdTotal.ClientID %>').value = Math.round((parseFloat(Airfare) + parseFloat(mobile) + parseFloat(lodging) +
                parseFloat(tips) + parseFloat(meals) + parseFloat(visaFee) +
                parseFloat(groundTransport) + parseFloat(dailyAllowance) +
                parseFloat(entertainment) + parseFloat(other) + parseFloat(gifts)) * 100) / 100;


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

        function ValidateEmployee() {
            var Employee = document.getElementById('<%=txtEmployeeName.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmployeeName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustVendName() {
            var CustVendName = document.getElementById('<%=txtCustVendName.ClientID %>').value;
            if (CustVendName == '') {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustVendName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlaceOfVisit() {
            var PlaceOfVisit = document.getElementById('<%=txtPlaceOfVisit.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlaceOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidatePurposeOfVisit() {
            var PurposeOfVisit = document.getElementById('<%=txtPurposeOfVisit.ClientID %>').value;
            if (PurposeOfVisit == '') {
                document.getElementById('<%=txtPurposeOfVisit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPurposeOfVisit.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateBusinessSegment() {
            var BusinessSegment = document.getElementById('<%=txtBusinessSegment.ClientID %>').value;
            if (BusinessSegment == '') {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBusinessSegment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateddlVisitSummaryAttached() {
            var visitSummaryAttached = document.getElementById('<%=ddlVisitSummaryAttached.ClientID %>').selectedIndex;
            document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').value = '';
            document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').value = '';
            document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').value = '';
            if (visitSummaryAttached == '' || visitSummaryAttached == '0') {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').disabled = true;
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').disabled = true;
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').disabled = true;
                ValidatefileUploadVisitSummary1();
                ValidatefileUploadVisitSummary2();
                ValidatefileUploadVisitSummary3();
            }
            else {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').disabled = false;
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').disabled = false;
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').disabled = false;
            }
        }



        function ValidatefileUploadVisitSummary1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary1 = document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').value;
            var divfileUploadVisitSummary1 = document.getElementById("divfileUploadVisitSummary1");
            var lblfileUploadVisitSummary1 = document.getElementById('<%=lblfileUploadVisitSummary1.ClientID %>');
            var visitSummaryAttached = document.getElementById('<%=ddlVisitSummaryAttached.ClientID %>').selectedIndex;
            if (visitSummaryAttached != '' || visitSummaryAttached != '0') {
                if (fileUploadVisitSummary1 == '') {
                    document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary1.style.display = "none";
                    lblfileUploadVisitSummary1.innerHTML = "";
                    return true;
                }

                else {
                    if (!regex.test(fileUploadVisitSummary1.toLowerCase())) {
                        document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "#F7627F";
                        divfileUploadVisitSummary1.style.display = "block";
                        lblfileUploadVisitSummary1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "";
                        divfileUploadVisitSummary1.style.display = "none";
                        lblfileUploadVisitSummary1.innerHTML = "";
                        return false;
                    }
                }
            }
            else {
                document.getElementById('<%=fileUploadVisitSummary1.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary1.style.display = "none";
                lblfileUploadVisitSummary1.innerHTML = "";
                return false;
            }
        }


        function ValidatefileUploadVisitSummary2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary2 = document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').value;
            var divfileUploadVisitSummary2 = document.getElementById("divfileUploadVisitSummary2");
            var lblfileUploadVisitSummary2 = document.getElementById('<%=lblfileUploadVisitSummary2.ClientID %>');

            if (fileUploadVisitSummary2 == '') {
                document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary2.style.display = "none";
                lblfileUploadVisitSummary2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitSummary2.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary2.style.display = "block";
                    lblfileUploadVisitSummary2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitSummary2.ClientID %>').style.borderColor = "";
                    divfileUploadVisitSummary2.style.display = "none";
                    lblfileUploadVisitSummary2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileUploadVisitSummary3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitSummary3 = document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').value;
            var divfileUploadVisitSummary3 = document.getElementById("divfileUploadVisitSummary3");
            var lblfileUploadVisitSummary3 = document.getElementById('<%=lblfileUploadVisitSummary3.ClientID %>');

            if (fileUploadVisitSummary3 == '') {
                document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "";
                divfileUploadVisitSummary3.style.display = "none";
                lblfileUploadVisitSummary3.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitSummary3.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitSummary3.style.display = "block";
                    lblfileUploadVisitSummary3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitSummary3.ClientID %>').style.borderColor = "";
                    divfileUploadVisitSummary3.style.display = "none";
                    lblfileUploadVisitSummary3.innerHTML = "";
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

            if (ValidateEmployee()) {
                check = false;
            }

            if (ValidateCustVendName()) {
                check = false;
            }

            if (ValidatePlaceOfVisit()) {
                check = false;
            }

            if (ValidatePurposeOfVisit()) {
                check = false;
            }

            if (ValidateBusinessSegment()) {
                check = false;
            }

            if (ValidatefileUploadVisitSummary1()) {
                check = false;
            }

            if (ValidatefileUploadVisitSummary2()) {
                check = false;
            }

            if (ValidatefileUploadVisitSummary3()) {
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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />


    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Travel Statement &nbsp;<asp:Label ID="lblTravelStatementNo" runat="server" /></legend>

            <div class="form-grid form-grid-2">

                <label>Sanction No.</label>
                <asp:DropDownList ID="ddlSanctionNo"
                    CssClass="form-control"
                    runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged">
                </asp:DropDownList>

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

                <label>Designation</label>
                <asp:TextBox ID="txtDesignation"
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

                <label>Visit Report Summary Attached?</label>
                <asp:DropDownList ID="ddlVisitSummaryAttached"
                    runat="server"
                    CssClass="form-control"
                    onChange="return ValidateddlVisitSummaryAttached();"
                    Enabled="true">
                    <asp:ListItem Text="No" Value="0" />
                    <asp:ListItem Text="Yes" Value="1" />
                </asp:DropDownList>

                <label>Attachment1</label>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUploadVisitSummary1" runat="server"
                                Enabled="false"
                                BorderStyle="Groove"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfileUploadVisitSummary1" style="display: none;">
                                <asp:Label ID="lblfileUploadVisitSummary1" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <label>Attachment2</label>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUploadVisitSummary2" runat="server"
                                Enabled="false"
                                BorderStyle="Groove"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfileUploadVisitSummary2" style="display: none;">
                                <asp:Label ID="lblfileUploadVisitSummary2" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <label>Attachment3</label>
                <table>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fileUploadVisitSummary3" runat="server"
                                Enabled="false"
                                BorderStyle="Groove"
                                CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfileUploadVisitSummary3" style="display: none;">
                                <asp:Label ID="lblfileUploadVisitSummary3" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

            </div>

            <label><b>Expense Statement</b></label>

            <div class="form-grid form-grid-2">

                <label>Airfare</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtAirfare"
                                runat="server"
                                onKeyUp="checkDecNew1(this)"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);"
                                Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtAirfareGL"
                                runat="server"
                                Enabled="false"
                                Text="40/41"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>


                <label>Telephone/Mobile</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtMobile" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtMobileGL" runat="server"
                                Enabled="false" Text="52"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Lodging</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtLodging" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtLodgingGL" runat="server"
                                Enabled="false" Text="42"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>


                <label>Tips</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtTips" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtTipsGL" runat="server"
                                Enabled="false" Text="54"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Meals</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtMeals" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtMealsGL" runat="server"
                                Enabled="false" Text="44"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Visa Fee</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtVisaFee" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtVisaFeeGL" runat="server"
                                Enabled="false" Text="56"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Ground Transport</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtGroundTransport" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtGroundTransportGL" runat="server"
                                Enabled="false"
                                Text="46"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Daily Allowance</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtDailyAllowance" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtDailyAllowanceGL" runat="server"
                                Enabled="false"
                                Text="58"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Entertainment</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtEntertainment" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtEntertainmentGL" runat="server"
                                Enabled="false"
                                Text="48"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Other</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtOther" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtOtherGL" runat="server"
                                Enabled="false" Text="60"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>


                <label>Gifts</label>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 75%;">
                            <asp:TextBox ID="txtGifts" runat="server"
                                onkeyup="checkDecNew1(this);"
                                onpaste="return false" onkeypress="return inNumberKeyWithDecimal(this, event);" Text="0"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 25%;">
                            <asp:TextBox ID="txtGiftsGL" runat="server"
                                Enabled="false" Text="50"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Total Submitted Bill Amount</label>
                <table style="width: 100%; height: 22px;">
                    <tr>
                        <td style="width: 65%;">
                            <asp:TextBox ID="txtTotal" runat="server"
                                Text="0.00"
                                Enabled="false"
                                CssClass="form-control" />
                            <asp:HiddenField ID="hdTotal" runat="server" />
                        </td>
                        <td style="width: 35%;">
                            <asp:DropDownList ID="ddlTotalCurrency"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Advance Obtained</label>
                <table style="width: 100%; height: 22px;">
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
                            <asp:DropDownList ID="ddlAdvanceObtainedCurreny"
                                runat="server"
                                Enabled="false"
                                CssClass="form-control" />
                        </td>
                    </tr>
                </table>

                <label>Tour Cost Recoverable</label>
                <asp:DropDownList ID="ddlTourCostRecoverable"
                    runat="server"
                    Enabled="false"
                    CssClass="form-control">
                    <asp:ListItem Text="No" Value="0" />
                    <asp:ListItem Text="Yes" Value="1" />
                </asp:DropDownList>


                <label>Remarks</label>
                <div class="full-width">

                    <asp:TextBox ID="txtRemarks"
                        Enabled="true"
                        runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine"
                        Rows="2" />
                </div>


                <label><b>Important Notes</b></label>
                <div class="full-width">

                    <table width="100%">
                        <tr>
                            <td>

                                <label>
                                    01. Please send a copy of this form to HRD & Accounts after approval from your HOD.
                                        In case of direct reporting to CEO, please get approval from CEO.</label>

                                <br />
                                <label>
                                    02. In case tour expenditure is expected to be more than INR 1,50,000, please seek
                                        approval from the CEO.</label>

                                <br />

                                <label>
                                    03. Only in case of absence of HRD, statement can be submitted to Accounts directly
                                        for settlement.
                                </label>
                                <br />
                                <label>
                                    04. In case of absence of your HOD, please seek approval from his/her HOD.
                                </label>
                                <br />
                                <label>
                                    05. Please maintain same currency in both the tour info & tour settlement forms
                                </label>
                                <br />
                                <label>
                                    06. DA: Managers & above INR 700, Others INR 400
                                </label>
                                <br />
                                <label>
                                    07. In case of foreign travel please attach passport and visa copy.
                                </label>
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

            <asp:Button ID="btnTourList"
                runat="server"
                Text="Travel Statement List"
                CssClass="button"
                OnClick="btnTourList_Click" Width="50%" />
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

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
