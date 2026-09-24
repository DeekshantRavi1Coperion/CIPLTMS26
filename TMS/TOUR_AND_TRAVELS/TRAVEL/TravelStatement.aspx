<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TravelStatement.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_TravelStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
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

                document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value = parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value) - parseFloat(advanceObtained);

                if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
                }
                else {
                    document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
                }
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


            document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value = Math.round(parseFloat(advanceObtained) - parseFloat(document.getElementById('<%=txtTotal.ClientID %>').value));
            document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value = document.getElementById('<%=hdAdjustmentAmt.ClientID %>').value


            if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) == 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = '';
            }

            else if (parseFloat(document.getElementById('<%=txtAdjustmentAmt.ClientID %>').value) < 0) {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Payble';
            }
            else {
                document.getElementById('<%=txtAdjustmentAmtType.ClientID %>').value = 'Recoverable';
            }
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
                return false;
            }

            if (ValidateEmployee()) {
                return false;
            }

            if (ValidateCustVendName()) {
                return false;
            }

            if (ValidatePlaceOfVisit()) {
                return false;
            }

            if (ValidatePurposeOfVisit()) {
                return false;
            }

            if (ValidateBusinessSegment()) {
                return false;
            }

            if (ValidatefileUploadVisitSummary1()) {
                return false;
            }

            if (ValidatefileUploadVisitSummary2()) {
                return false;
            }

            if (ValidatefileUploadVisitSummary3()) {
                return false;
            }

            return check;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 40px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">Travel Statement &nbsp;<asp:Label ID="lblTravelStatementNo"
                runat="server" /></legend>
            <table width="100%">
                <tr>
                    <td colspan="5" align="center">
                        <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>Sanction No:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSanctionNo" runat="server" Width="100%" Height="25px" onblur="return ValidateSanctionNo();"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Tour No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTourNo" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Employee Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%" Enabled="false" onblur="return ValidateEmployee();" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Employee ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployeeID" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Designation:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtDesignation" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Starting Date Of Tour:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 28%;">
                                    <asp:TextBox ID="txtStartDate" runat="server" Enabled="false" ReadOnly="true" Width="100%"
                                        onblur="return ValidateStartDate();" />
                                </td>
                                <td style="width: 19%;">End Date Of Tour:
                                </td>
                                <td style="width: 28%;">
                                    <asp:TextBox ID="txtEndDate" runat="server" Enabled="false" ReadOnly="true" Width="100%"
                                        onblur="return ValidateEndDate();" />
                                </td>
                                <td style="width: 5%;">Days:
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtDays" runat="server" Width="100%" Enabled="false" ReadOnly="true" />
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
                    <td>Name of Customer/Vendor:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtCustVendName" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustVendName();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Place of Visit:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPlaceOfVisit" runat="server" Width="100%" Enabled="false" onblur="return ValidatePlaceOfVisit();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Purpose Of Visit:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPurposeOfVisit" runat="server" Width="100%" Enabled="false" onblur="return ValidatePurposeOfVisit();" />
                    </td>
                    <td></td>
                    <td>Job/Inq No. Where Applicable:
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobInqNo" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Business Segment:
                    </td>
                    <td>
                        <asp:TextBox ID="txtBusinessSegment" runat="server" Width="100%" Visible="true" Enabled="false"
                            onblur="return ValidateBusinessSegment();" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Visit Report Summary Attached?:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlVisitSummaryAttached" runat="server" Width="100%" Height="26px"
                            onChange="return ValidateddlVisitSummaryAttached();" Enabled="true">
                            <asp:ListItem Text="No" Value="0" />
                            <asp:ListItem Text="Yes" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Attachment1:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadVisitSummary1" runat="server" Enabled="false" Width="100%"
                            Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="3">&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadVisitSummary1" style="display: none;">
                            <asp:Label ID="lblfileUploadVisitSummary1" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Attachment2:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadVisitSummary2" runat="server" Enabled="false" Width="100%"
                            Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary2();" />
                    </td>
                    <td></td>
                    <td>Attachment3:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadVisitSummary3" runat="server" Enabled="false" Width="100%"
                            Height="29px" BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary3();" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadVisitSummary2" style="display: none;">
                            <asp:Label ID="lblfileUploadVisitSummary2" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadVisitSummary3" style="display: none;">
                            <asp:Label ID="lblfileUploadVisitSummary3" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Expense Statement</b>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Airfare:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtAirfare" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtAirfareGL" runat="server" Width="100%" Enabled="false" Text="40/41" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Telephone/Mobile:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtMobile" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtMobileGL" runat="server" Width="100%" Enabled="false" Text="52" />
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
                    <td>Lodging:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtLodging" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtLodgingGL" runat="server" Width="100%" Enabled="false" Text="42" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Tips:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtTips" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtTipsGL" runat="server" Width="100%" Enabled="false" Text="54" />
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
                    <td>Meals:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtMeals" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtMealsGL" runat="server" Width="100%" Enabled="false" Text="44" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Visa Fee:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtVisaFee" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtVisaFeeGL" runat="server" Width="100%" Enabled="false" Text="56" />
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
                    <td>Ground Transport:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtGroundTransport" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtGroundTransportGL" runat="server" Width="100%" Enabled="false"
                                        Text="46" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Daily Allowance:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtDailyAllowance" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtDailyAllowanceGL" runat="server" Width="100%" Enabled="false"
                                        Text="58" />
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
                    <td>Entertainment:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtEntertainment" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtEntertainmentGL" runat="server" Width="100%" Enabled="false"
                                        Text="48" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Other:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtOther" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtOtherGL" runat="server" Width="100%" Enabled="false" Text="60" />
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
                    <td>Gifts:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtGifts" runat="server" Width="100%" onkeyup="checkDecNew1(this);"
                                        onpaste="return false" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:TextBox ID="txtGiftsGL" runat="server" Width="100%" Enabled="false" Text="50" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Total:
                    </td>
                    <td>
                        <table style="width: 100%; height: 22px;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtTotal" runat="server" Width="100%" Text="0.00" Enabled="false" />
                                    <asp:HiddenField ID="hdTotal" runat="server" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:DropDownList ID="ddlTotalCurrency" runat="server" Width="100%" Height="26px"
                                        Enabled="false" />
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
                    <td>Advance Obtained:
                    </td>
                    <td>
                        <table style="width: 100%; height: 22px;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtAdvanceObtained" runat="server" Width="100%" Text="0.00" Enabled="false"
                                        onkeyup="checkDec(this);" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:DropDownList ID="ddlAdvanceObtainedCurreny" runat="server" Width="100%" Height="26px"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Amount Adjustment:
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:TextBox ID="txtAdjustmentAmt" runat="server" Width="100%" Text="0" Enabled="false" />
                                    <asp:HiddenField ID="hdAdjustmentAmt" runat="server" />
                                </td>
                                <td style="width: 25%;">
                                    <asp:DropDownList ID="ddlAdjustmentAmtCurrency" runat="server" Width="100%" Height="26px"
                                        Enabled="false" />
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
                    <td>Tour Cost Recoverable:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTourCostRecoverable" runat="server" Enabled="false" Width="100%"
                            Height="26px">
                            <asp:ListItem Text="No" Value="0" />
                            <asp:ListItem Text="Yes" Value="1" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>Adjustment Type:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdjustmentAmtType" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>Remarks:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
                            Rows="2" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Panel ID="pnlExceptionMsg" Visible="false" runat="server" Height="50px">
                            <asp:Label ID="lblExceptionMsg" runat="server" Font-Bold="True" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return ValidateAll();"
                                        OnClick="btnSubmit_Click" Width="100%" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%;">
                                    <asp:Button ID="btnTourList" CssClass="button" runat="server" Text="Travel Statement List"
                                        OnClick="btnTourList_Click" Width="100%" />
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
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table width="100%">
                            <tr>
                                <td>
                                    <b>Important Notes</b>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p>
                                        01. Please send a copy of this form to HRD & Accounts after approval from your HOD.
                                        In case of direct reporting to CEO, please get approval from CEO.
                                    </p>
                                    <p>
                                        02. In case tour expenditure is expected to be more than INR 1,50,000, please seek
                                        approval from the CEO.
                                    </p>
                                    <p>
                                        03. Only in case of absence of HRD, statement can be submitted to Accounts directly
                                        for settlement.
                                    </p>
                                    <p>
                                        04. In case of absence of your HOD, please seek approval from his/her HOD.
                                    </p>
                                    <p>
                                        05. Please maintain same currency in both the tour info & tour settlement forms
                                    </p>
                                    <p>
                                        06. DA: Managers & above INR 400, Others INR 250
                                    </p>
                                    <p>
                                        07. In case of foreign travel please attach passport and visa copy.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
