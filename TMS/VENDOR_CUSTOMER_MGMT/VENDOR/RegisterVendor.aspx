<%@ Page Title="CIPLTMS-Register Vendor"
    Language="C#" MasterPageFile="~/HOME.master"
    AutoEventWireup="true"
    CodeFile="RegisterVendor.aspx.cs"
    Inherits="VENDOR_CUSTOMER_MGMT_VENDOR_RegisterVendor" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../Images/Icons/Icon04.png" />

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

        .textboxdrawings {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textboxtagno {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: whitesmoke;
        }

        .textboxleft {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxcenter {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxright {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        /* File Upload Design  */
        .file-upload {
            display: inline-block;
            overflow: hidden;
            position: relative;
            text-align: center;
            vertical-align: middle;
            /* Cosmetics */
            /*border: 1px solid #5C005C;*/
            /*background: #5C005C;*/
            background: #003a5cba;
            color: #fff;
            /* browser can do it */
            border-radius: 6px;
            -moz-border-radius: 6px;
            /*text-shadow: #000 1px 1px 2px;*/
            -webkit-border-radius: 6px;
        }

        /* The button size */
        .file-upload {
            height: 2.3em;
        }

            .file-upload, .file-upload span {
                /*width: 3.5em;*/
                width: 100%;
            }

                .file-upload input {
                    position: absolute;
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    /* Loses tab index in webkit if width is set to 0 */
                    opacity: 0;
                    filter: alpha(opacity=0);
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                }

                .file-upload span {
                    position: absolute;
                    top: 0;
                    left: 0;
                    display: inline-block;
                    /* Adjust button text vertical alignment */
                    padding-top: .5em;
                    background: #003a5cba;
                }



        .myGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .myGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .myGrid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .myGrid .alt {
                background-color: #EFEFEF;
            }
    </style>


    <%--/*Supporting Functions=================================================================*/--%>
    <script type="text/Javascript">

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }

        document.onkeypress = stopEnterKey;

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>


    <%--/*Validation Functions=================================================================*/--%>
    <script type="text/Javascript">

        /*Vendor Details=================================================================*/

        function ValidateName() {
            var val = document.getElementById('<%=txtNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePANNumber() {

            var country = document.getElementById('<%= ddlCountryToS.ClientID %>').value;
            var val = document.getElementById('<%=txtPANNumberToS.ClientID %>').value.toUpperCase();

            var panPattern = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
            if (country == '95') {
                if (val == '') {
                    document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    if (!panPattern.test(val)) {
                        alert("Invalid PAN Number");
                        document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "#F7627F";
                        return true;
                    }
                    else {
                        document.getElementById('<%=txtPANNumberToS.ClientID %>').style.borderColor = "";
                        return false;
                    }
                }
            }

        }

        function ValidateCategory() {
            var val = document.getElementById('<%=ddlCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMSMEStatus() {
            var val = document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMSMEStatusToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Billing Address=================================================================*/

        function ValidateGSTIn() {
            var PAN = document.getElementById('<%=txtPANNumberToS.ClientID %>').value.toUpperCase();
            var val = document.getElementById('<%=txtGSTInToS.ClientID %>').value.toUpperCase();



            var gstPattern = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;

            if (val == '') {
                <%--document.getElementById('<%=txtGSTInToS.ClientID %>').style.borderColor = "#F7627F";
                return true;--%>
                return false;
            }
            else {

                var PANText = val.substring(2, 12);

                if (!gstPattern.test(val)) {
                    alert("Invalid GSTIN");
                    return true;
                }
                else if (PANText != PAN) {
                    alert("PAN number does not match the PAN embedded in GSTIN.");
                    return true;
                }
                document.getElementById('<%=txtGSTInToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAddress1() {
            var val = document.getElementById('<%=txtAddress1ToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtAddress1ToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAddress1ToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCity() {
            var val = document.getElementById('<%=txtCityToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtCityToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCityToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCountry() {
            var val = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;
            if (val == '') {
                document.getElementById('<%=ddlCountryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateState() {
            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=ddlStateToS.ClientID %>').selectedIndex;

            if (countryId == '95') {
                if (val == '' || val == 0) {
                    document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlStateToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOtherState() {
            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=txtOtherStateToS.ClientID %>').value;

            if (countryId != 95) {
                if (val == '') {
                    document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtOtherStateToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePhone() {
            var val = document.getElementById('<%=txtPhoneToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtPhoneToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPhoneToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmail() {
            var val = document.getElementById('<%=txtEmailToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtEmailToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEmailToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Contact Person Details=================================================================*/

        function ValidateContactPersonName() {
            var val = document.getElementById('<%=txtContactPersonNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonMobile() {
            var val = document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonMobileToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonPhone() {
            var val = document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonPhoneToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateContactPersonEmail() {
            var val = document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtContactPersonEmailToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Bank Details=================================================================*/

        function ValidateBankName() {
            var val = document.getElementById('<%=txtBankNameToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBankNameToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBankNameToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBranch() {
            var val = document.getElementById('<%=txtBranchToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtBranchToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBranchToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSWIFTCode() {

            var countryId = document.getElementById('<%=ddlCountryToS.ClientID %>').selectedIndex;

            var val = document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').value;

            if (countryId != 95) {
                if (val == '') {
                    document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtSWIFTCodeToS.ClientID %>').style.borderColor = "";
                return false;
            }

        }

        function ValidateAccountNumber() {
            var val = document.getElementById('<%=txtAccountNumberToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtAccountNumberToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAccountNumberToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRTGSOrIFSC() {
            var val = document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRTGSOrIFSCToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateISBN() {
            var val = document.getElementById('<%=txtISBNToS.ClientID %>').value;
            if (val == '') {
                document.getElementById('<%=txtISBNToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtISBNToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Other Details=================================================================*/

        function ValidateResponsible() {
            var val = document.getElementById('<%=ddlResponsibleToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlResponsibleToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlResponsibleToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRelationType() {
            var val = document.getElementById('<%=ddlRelationTypeToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlRelationTypeToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRelationTypeToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemCategory() {
            var val = document.getElementById('<%=ddlItemCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlItemCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemSubCategory() {
            var val = document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').selectedIndex;
            if (val == '' || val == 0) {
                document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlItemSubCategoryToS.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        /*Attachment=================================================================*/

        function ValidatefileAttachment() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachment.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachment");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachment.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachment.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }


        <%--function ValidatefileAttachmentVRF() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentVRF");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentVRF.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentVRF.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentGSTIN() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentGSTIN");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentGSTIN.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentGSTIN.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentPAN() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentPAN");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentPAN.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentPAN.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentCancelledCheque() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachmentCancelledCheque");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachmentCancelledCheque.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentCancelledCheque.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther1");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther1.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther1.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther2");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther2.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther2.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther3");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther3.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther3.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachmentOther4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment = document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').value;
            var divfileAttachment = document.getElementById("divfileAttachmentOther4");
            var lblfileAttachment = document.getElementById('<%=lblfileAttachmentOther4.ClientID %>');

            if (fileAttachment == '') {
                document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "";
                divfileAttachment.style.display = "none";
                lblfileAttachment.innerHTML = "";
                return false;
            }
            else {

                fileAttachment = fileAttachment.split(" ").join("")
                fileAttachment = fileAttachment.split("(").join("")
                fileAttachment = fileAttachment.split(")").join("")

                if (!regex.test(fileAttachment.toLowerCase())) {
                    document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment.style.display = "block";
                    lblfileAttachment.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadAttachmentOther4.ClientID %>').style.borderColor = "";
                    divfileAttachment.style.display = "none";
                    lblfileAttachment.innerHTML = "";
                    return false;
                }
            }
        }--%>

    </script>



    <%--/*Apply Validation Functions===========================================================*/--%>
    <script type="text/Javascript">

        <%--function ValidateVendorBasicDetails() {

            displayNoneAllPanels();

            var check = true;

            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            if (ValidateCategory()) { check = false; }
            if (ValidateMSMEStatus()) { check = false; }

            if (check) {
                document.getElementById('<%= imgBtnShowPnl2.ClientID %>').src = '<%= ResolveUrl("~/Images/VCM/a_pnl2.png") %>';
                document.getElementById('<%= pnlBillingAddress.ClientID %>').style.display = 'block';
            }
            else {
                document.getElementById('<%= pnlVendorDetails.ClientID %>').style.display = 'block';
            }

            return false;
        }--%>



        function ValidateBillingAddress() {
            var check = true;

            if (ValidateGSTIn()) { check = false; }
            if (ValidateAddress1()) { check = false; }
            if (ValidateCity()) { check = false; }
            if (ValidateState()) { check = false; }
            if (ValidateCountry()) { check = false; }
            if (ValidateOtherState()) { check = false; }
            if (ValidatePhone()) { check = false; }
            if (ValidateEmail()) { check = false; }

            if (check) {

                return true;
            }
            else {
                return false;
            }

            return check;
        }

        function ValidateBillingAddressGrid() {

            document.getElementById('<%= lblBillingAddressMsg.ClientID %>').innerText = ''
            var check = false;
            var grid = document.getElementById('<%= gvBillingAddress.ClientID %>');
            if (grid != null) {

                var rows = grid.getElementsByTagName('tr');
                if (rows.length > 1) {
                    check = true;
                }
            }

            if (check) {
                return true;
            }
            else {
                return false;
                document.getElementById('<%= lblBillingAddressMsg.ClientID %>').innerText = 'Please add at least one billing address.'
            }

            return false;
        }


        function ValidateContactPerson() {
            var check = true;

            if (ValidateContactPersonName()) { check = false; }
            if (ValidateContactPersonMobile()) { check = false; }
            if (ValidateContactPersonPhone()) { check = false; }
            if (ValidateContactPersonEmail()) { check = false; }

            if (check) {
                return true;
            }
            else {
                return false;
            }

            return check;
        }

        function ValidateContactPersonGrid() {

            displayNoneAllPanels();

            document.getElementById('<%= lblContactPersonMsg.ClientID %>').innerText = ''
            var check = false;
            var grid = document.getElementById('<%= gvContactPerson.ClientID %>');
            if (grid != null) {

                var rows = grid.getElementsByTagName('tr');
                if (rows.length > 1) {
                    check = true;
                }
            }

            if (check) {
                return true;
            }
            else {
                return false;
                document.getElementById('<%= lblContactPersonMsg.ClientID %>').innerText = 'Please add at least one contact person.'
            }

            return false;
        }


        function ValidateAllAttachment() {
            var check = true;

            if (ValidatefileAttachment()) { check = false; }

            //if (ValidatefileAttachmentVRF()) { check = false; }
            //if (ValidatefileAttachmentGSTIN()) { check = false; }
            //if (ValidatefileAttachmentPAN()) { check = false; }
            //if (ValidatefileAttachmentCancelledCheque()) { check = false; }
            //if (ValidatefileAttachmentOther1()) { check = false; }
            //if (ValidatefileAttachmentOther2()) { check = false; }
            //if (ValidatefileAttachmentOther3()) { check = false; }
            //if (ValidatefileAttachmentOther4()) { check = false; }

            return check;
        }


        <%--function ValidateAll() {
            var check = true;

            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            
            if (ValidateCategory()) { check = false; }
            if (ValidateMSMEStatus()) { check = false; }

            if (ValidateGSTIn()) { check = false; }
            if (ValidateAddress1()) { check = false; }
            if (ValidateCity()) { check = false; }

            if (ValidateCountry()) { check = false; }
            if (ValidateState()) { check = false; }
            if (ValidateOtherState()) { check = false; }

            if (ValidatePhone()) { check = false; }
            if (ValidateEmail()) { check = false; }

            if (ValidateContactPersonName()) { check = false; }
            if (ValidateContactPersonMobile()) { check = false; }
            if (ValidateContactPersonPhone()) { check = false; }
            if (ValidateContactPersonEmail()) { check = false; }

            if (ValidateBankName()) { check = false; }
            if (ValidateBranch()) { check = false; }
            if (ValidateSWIFTCode()) { check = false; }
            if (ValidateAccountNumber()) { check = false; }
            if (ValidateRTGSOrIFSC()) { check = false; }
            //if (ValidateISBN()) { check = false; }

            if (ValidateResponsible()) { check = false; }
            if (ValidateRelationType()) { check = false; }
            if (ValidateItemCategory()) { check = false; }
            if (ValidateItemSubCategory()) { check = false; }

            if (ValidatefileAttachmentVRF()) { check = false; }
            if (ValidatefileAttachmentGSTIN()) { check = false; }
            if (ValidatefileAttachmentPAN()) { check = false; }
            if (ValidatefileAttachmentCancelledCheque()) { check = false; }
            if (ValidatefileAttachmentOther1()) { check = false; }
            if (ValidatefileAttachmentOther2()) { check = false; }
            if (ValidatefileAttachmentOther3()) { check = false; }
            if (ValidatefileAttachmentOther4()) { check = false; }

            if (check) {
                if (confirm("Would you like to save Vendor?")) {
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
        }--%>

        //window.onload = function () {
        //    window.scrollTo(0, sessionStorage.getItem('scrollPos') || 0);
        //};

        //window.onscroll = function () {
        //    sessionStorage.setItem('scrollPos', window.scrollY);
        //};

        function ValidateAll() {
            var check = true;

            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            if (ValidateCategory()) { check = false; }

            if (ValidateBankName()) { check = false; }
            if (ValidateBranch()) { check = false; }
            if (ValidateAccountNumber()) { check = false; }

            if (ValidateResponsible()) { check = false; }
            if (ValidateRelationType()) { check = false; }
            if (ValidateItemCategory()) { check = false; }
            if (ValidateItemSubCategory()) { check = false; }

            if (check) {
                if (confirm("Would you like to save Vendor?")) {
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

        function ValidateAllForPreview() {
            var check = true;

            if (ValidateName()) { check = false; }
            if (ValidatePANNumber()) { check = false; }
            if (ValidateCategory()) { check = false; }

            if (ValidateResponsible()) { check = false; }
            if (ValidateRelationType()) { check = false; }
            if (ValidateItemCategory()) { check = false; }
            if (ValidateItemSubCategory()) { check = false; }

            if (check) {
                if (confirm("Would you like to preview vendor details?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }

    </script>


    <%--/*Paneling Functions===================================================================*/--%>
    <script type="text/Javascript">

        function ShowBillingAddressPopup() {


            var panNo = document.getElementById('<%= txtPANNumberToS.ClientID %>').value;


            if (panNo != '') {
                document.getElementById('<%= txtPANToSS.ClientID %>').value = panNo;

                //document.getElementById('<%= ddlCountryToS.ClientID %>').value = "95";

                document.getElementById('<%= hdBillingAddressUpdationFlag.ClientID %>').value = '0';
                document.getElementById('<%= btnAddUpdateAddressToList.ClientID %>').value = 'Add Address';
                $find('<%= mpeAddBillingAddress.ClientID %>').show();
            }
            else {
                alert('Please enter PAN No. !')
            }


        }

        function ShowContactPersonPopup() {
            document.getElementById('<%= hdContactPersonUpdationFlag.ClientID %>').value = '0';
            document.getElementById('<%= btnAddUpdateContactPersonToList.ClientID %>').value = 'Add Contact Person';
            $find('<%= mpeAddContactPerson.ClientID %>').show();
        }


        function GenerateGSTIN() {

            var panNo = document.getElementById('<%= txtPANToSS.ClientID %>').value.trim();
            var runningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>').value.trim();

            var ddlState = document.getElementById('<%= ddlStateToS.ClientID %>');
            var state = ddlState.options[ddlState.selectedIndex].text;

            var stateCode = state.substring(state.indexOf('[') + 1, state.indexOf(']'));

            if (!stateCode || !panNo || !runningNo) {
                return;
            }

            document.getElementById('<%= txtGSTInToS.ClientID %>').value = stateCode + panNo + runningNo;
        }

        function FillPANNo() {
            var country = document.getElementById('<%= ddlCountryToS.ClientID %>').value;
            if (country == '95') {
                document.getElementById('<%= ddlStateToS.ClientID %>').disabled = false;

                var otherState = document.getElementById('<%= txtOtherStateToS.ClientID %>');
                otherState.disabled = true;
                otherState.value = '';


                var GSTInRunningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>');
                GSTInRunningNo.disabled = false;
                GSTInRunningNo.value = '';

                var GSTIn = document.getElementById('<%= txtGSTInToS.ClientID %>');
                GSTIn.disabled = false;
                GSTIn.value = '';



                document.getElementById('<%= txtPANNumberToS.ClientID %>').value = '';
            }
            else {
                var ddlState = document.getElementById('<%= ddlStateToS.ClientID %>');
                ddlState.disabled = true;
                ddlState.selectedIndex = 0; // Optional: reset selection

                var otherState = document.getElementById('<%= txtOtherStateToS.ClientID %>');
                otherState.disabled = false;
                otherState.value = '';


                var GSTInRunningNo = document.getElementById('<%= txtGSTInRunningNoToS.ClientID %>');
                GSTInRunningNo.disabled = true;
                GSTInRunningNo.value = '';

                var GSTIn = document.getElementById('<%= txtGSTInToS.ClientID %>');
                GSTIn.disabled = true;
                GSTIn.value = '';



                document.getElementById('<%= txtPANNumberToS.ClientID %>').value = 'PANNOTAVBL';
            }
        }

    </script>




    <script type="text/javascript">

        window.addEventListener('load', function () {

            var div = document.getElementById("scrollDiv");

            // Restore scroll position
            var scrollPos = sessionStorage.getItem("divScroll");

            if (scrollPos !== null) {
                div.scrollTop = scrollPos;
            }

            // Save scroll position
            div.onscroll = function () {
                sessionStorage.setItem("divScroll", div.scrollTop);
            };

        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />


    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="form-entry-container">

        <fieldset class="form-card">
            <legend>Register Vendor</legend>

            <div class="form-grid form-grid-2">

                <div class="full-width">

                    <fieldset class="form-card">
                        <legend>Basic Details</legend>

                        <div class="form-grid form-grid-2">

                            <div class="full-width">
                                <label>Name:</label>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 70%">
                                            <asp:TextBox ID="txtNameToS" runat="server" CssClass="form-control" />
                                        </td>

                                        <td style="width: 30%">
                                            <asp:TextBox ID="txtCodeToS" runat="server" CssClass="form-control"
                                                Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <label>Country:</label>
                            <asp:DropDownList ID="ddlCountryToS" runat="server"
                                CssClass="form-control"
                                onchange="FillPANNo();">
                            </asp:DropDownList>

                            <label>PAN Number:</label>
                            <asp:TextBox ID="txtPANNumberToS" runat="server"
                                CssClass="form-control" />


                            <label>Credit Days:</label>
                            <asp:TextBox ID="txtCreditDaysToS" runat="server"
                                CssClass="form-control"
                                Text="0"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />

                            <label>Credit Limit:</label>
                            <asp:TextBox ID="txtCreditLimitToS" runat="server"
                                CssClass="form-control"
                                Text="0"
                                onkeypress="return inNumberKeyWithDecimal(this, event);" />

                            <label>Category:</label>
                            <asp:DropDownList ID="ddlCategoryToS" runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>

                            <label>MSME Status:</label>
                            <asp:DropDownList ID="ddlMSMEStatusToS" runat="server"
                                CssClass="form-control">
                            </asp:DropDownList>

                            <div class="full-width button-group">
                                <label>Do you have any relation work in our company as employee?</label>
                                <asp:RadioButtonList ID="rdAssociatedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:RadioButtonList>

                                <label>Is One Time Vendor?</label>
                                <asp:RadioButtonList ID="rdIsOneTimeVendorToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>


                        </div>
                    </fieldset>
                </div>

                <br />

                <div class="full-width">

                    <asp:HiddenField ID="hdBillingAddressSRNo" runat="server" />
                    <asp:HiddenField ID="hdBillingAddressUpdationFlag" runat="server" />
                    <asp:HiddenField ID="hdBillingAddressLastSUID" runat="server" />

                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>Billing Address:
                                <asp:Label ID="lblBillingAddressRecords" runat="server" Text="[0]" />

                                <asp:ImageButton ImageUrl="~/Images/VCM/add.png" runat="server"
                                    OnClientClick="ShowBillingAddressPopup(); return false;"
                                    ToolTip="Add new billing address"
                                    Width="25px" Height="25px" />
                            </legend>

                            <asp:GridView ID="gvBillingAddress"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="employee-grid"
                                ForeColor="#333333" GridLines="Both" Width="100%"
                                HorizontalAlign="Center"
                                OnRowCommand="gvBillingAddress_RowCommand"
                                OnRowDataBound="gvBillingAddress_RowDataBound">

                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />


                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server"
                                                ToolTip="Edit" ImageUrl="~/Images/VCM/edit.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server"
                                                ToolTip="Remove" ImageUrl="~/Images/VCM/delete.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                            <asp:Label ID="lblGSTIN" runat="server" Text='<%# Eval("GSTIN") %>' Visible="false" />
                                            <asp:Label ID="lblAddress1" runat="server" Text='<%# Eval("ADDRESS_LINE1") %>' Visible="false" />
                                            <asp:Label ID="lblAddress2" runat="server" Text='<%# Eval("ADDRESS_LINE2") %>' Visible="false" />
                                            <asp:Label ID="lblAddress3" runat="server" Text='<%# Eval("ADDRESS_LINE3") %>' Visible="false" />
                                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>' Visible="false" />
                                            <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE") %>' Visible="false" />
                                            <asp:Label ID="lblOtherState" runat="server" Text='<%# Eval("OTHER_STATE") %>' Visible="false" />
                                            <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>' Visible="false" />
                                            <asp:Label ID="lblPINCode" runat="server" Text='<%# Eval("PIN_CODE") %>' Visible="false" />
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("PHONE") %>' Visible="false" />
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAIL") %>' Visible="false" />
                                            <asp:Label ID="lblIsDefault" runat="server" Text='<%# Eval("IS_DEFAULT") %>' Visible="false" />

                                            <asp:Label ID="lblStateID" runat="server" Text='<%# Eval("STATE_ID") %>' Visible="false" />
                                            <asp:Label ID="lblCountryID" runat="server" Text='<%# Eval("COUNTRY_ID") %>' Visible="false" />

                                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="ADDRESS_LINE1" HeaderText="Address1" />
                                    <asp:BoundField DataField="ADDRESS_LINE2" HeaderText="Address2" />
                                    <asp:BoundField DataField="ADDRESS_LINE3" HeaderText="Address3" />
                                    <asp:BoundField DataField="CITY" HeaderText="City" />
                                    <asp:BoundField DataField="COUNTRY" HeaderText="Country" />
                                    <asp:BoundField DataField="STATE" HeaderText="State" />
                                    <asp:BoundField DataField="OTHER_STATE" HeaderText="Other State" />
                                    <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" />
                                    <asp:BoundField DataField="PIN_CODE" HeaderText="PIN Code" />

                                    <asp:BoundField DataField="PHONE" HeaderText="Phone" />
                                    <asp:BoundField DataField="EMAIL" HeaderText="Email" />


                                    <asp:TemplateField HeaderText="Is Default?">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>

                        </fieldset>
                    </div>

                    <div align="center">
                        <asp:Label ID="lblBillingAddressMsg" runat="server" ForeColor="Red" />
                    </div>

                </div>

                <br />

                <div class="full-width">

                    <asp:HiddenField ID="hdContactPersonSRNo" runat="server" />
                    <asp:HiddenField ID="hdContactPersonUpdationFlag" runat="server" />
                    <asp:HiddenField ID="hdContactPersonLastSUID" runat="server" />

                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>Contact Person Details:
                                <asp:Label ID="lblContactPersonRecords" runat="server" Text="[0]" />
                                <asp:ImageButton ImageUrl="~/Images/VCM/add.png" runat="server"
                                    OnClientClick="ShowContactPersonPopup(); return false;"
                                    ToolTip="Add new contact person"
                                    Width="25px" Height="25px" />
                            </legend>

                            <asp:GridView ID="gvContactPerson"
                                runat="server"
                                AutoGenerateColumns="false"
                                CellPadding="4"
                                CssClass="employee-grid"
                                ForeColor="#333333" GridLines="Both" Width="100%"
                                HorizontalAlign="Center"
                                OnRowCommand="gvContactPerson_RowCommand"
                                OnRowDataBound="gvContactPerson_RowDataBound">

                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />

                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES"
                                                runat="server" ToolTip="Edit" ImageUrl="~/Images/VCM/edit.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server"
                                                ToolTip="Remove" ImageUrl="~/Images/VCM/delete.png"
                                                Width="30px" Height="30px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                            <asp:Label ID="lblContactPersonName" runat="server" Text='<%# Eval("NAME") %>' Visible="false" />
                                            <asp:Label ID="lblContactPersonMobile" runat="server" Text='<%# Eval("MOBILE_NO") %>' Visible="false" />
                                            <asp:Label ID="lblContactPersonPhone" runat="server" Text='<%# Eval("PHONE") %>' Visible="false" />
                                            <asp:Label ID="lblContactPersonEmail" runat="server" Text='<%# Eval("EMAIL") %>' Visible="false" />
                                            <asp:Label ID="lblIsDefault" runat="server" Text='<%# Eval("IS_DEFAULT") %>' Visible="false" />


                                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="NAME" HeaderText="Name" />
                                    <asp:BoundField DataField="MOBILE_NO" HeaderText="Mobile No." />
                                    <asp:BoundField DataField="PHONE" HeaderText="Phone No." />
                                    <asp:BoundField DataField="EMAIL" HeaderText="Email" />

                                    <asp:TemplateField HeaderText="Is Default?">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>



                        </fieldset>
                    </div>


                    <div align="center">
                        <asp:Label ID="lblContactPersonMsg" runat="server" ForeColor="Red" />
                    </div>

                </div>


            </div>

            <br />

            <div class="full-width">

                <fieldset class="form-card">
                    <legend>Bank Details</legend>

                    <div class="form-grid form-grid-2">

                        <label>Bank Name:</label>
                        <asp:TextBox ID="txtBankNameToS" runat="server" CssClass="form-control" />

                        <label>Branch:</label>
                        <asp:TextBox ID="txtBranchToS" runat="server" CssClass="form-control" />


                        <label>RTGS/IFSC:</label>
                        <asp:TextBox ID="txtRTGSOrIFSCToS" runat="server" CssClass="form-control" />

                        <label>Account Number:</label>
                        <asp:TextBox ID="txtAccountNumberToS" runat="server" CssClass="form-control" />

                        <label>SWIFT Code:</label>
                        <asp:TextBox ID="txtSWIFTCodeToS" runat="server" CssClass="form-control" Enabled="false" />

                        <label>ISBN:</label>
                        <asp:TextBox ID="txtISBNToS" runat="server" CssClass="form-control" Enabled="false" />
                    </div>
                </fieldset>

            </div>

            <br />

            <div class="full-width">

                <fieldset class="form-card">
                    <legend>Other Details</legend>

                    <div class="form-grid form-grid-2">

                        <label>Is Technical Details Received?</label>
                        <asp:RadioButtonList ID="rdIsTechnicalDetailsReceivedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:RadioButtonList>

                        <label>Is ISO Certified?</label>
                        <asp:RadioButtonList ID="rdIsISOCertifiedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:RadioButtonList>

                        <label>Is QA Visited?</label>
                        <asp:RadioButtonList ID="rdIsQAVisitedToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:RadioButtonList>

                        <label>Is Government?</label>
                        <asp:RadioButtonList ID="rdIsGovernmentToS" runat="server" RepeatDirection="Horizontal" Width="30%">
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:RadioButtonList>

                        <label>Responsible:</label>
                        <asp:DropDownList ID="ddlResponsibleToS" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Type:</label>
                        <asp:DropDownList ID="ddlRelationTypeToS" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Item Category:</label>
                        <asp:DropDownList ID="ddlItemCategoryToS" runat="server"
                            CssClass="form-control"
                            OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>

                        <label>Item Sub-Category:</label>
                        <asp:DropDownList ID="ddlItemSubCategoryToS" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                </fieldset>

            </div>

            <br />

            <div class="full-width">

                <div class="form-grid form-grid-2">

                    <div class="full-width button-group">

                        <label>Attachment Type:</label>
                        <asp:DropDownList ID="ddlAttachmentType" runat="server" CssClass="form-control"></asp:DropDownList>

                        <label>Brownse:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadAttachment" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileAttachment" style="display: none;">
                                        <asp:Label ID="lblfileAttachment" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <asp:ImageButton ImageUrl="~/Images/VCM/add.png" runat="server"
                            ID="btnAddUpdateAttachmentToList"
                            OnClick="btnAddUpdateAttachmentToList_Click"
                            OnClientClick="return ValidateAllAttachment();"
                            ToolTip="Add new attachment" Height="30px" Width="30px" />

                    </div>

                </div>

                <div class="employee-grid-container">

                    <fieldset class="filter-card">
                        <legend>Attachments:
                                <asp:Label ID="lblAttachmentsRecords" runat="server" Text="[0]" /></legend>


                        <asp:GridView ID="gvAddAttachments"
                            runat="server"
                            AutoGenerateColumns="false"
                            CellPadding="4"
                            CssClass="employee-grid"
                            ForeColor="#333333" GridLines="Both" Width="100%"
                            HorizontalAlign="Center"
                            OnRowCommand="gvAddAttachments_RowCommand"
                            OnRowDataBound="gvAddAttachments_RowDataBound">

                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />

                            <Columns>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove"
                                            ImageUrl="~/Images/VCM/delete.png"
                                            Width="30px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>

                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                        <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                                        <asp:Label ID="lblDOCTypeFID" runat="server" Text='<%# Eval("DOC_TYPE_FID") %>' Visible="false" />
                                        <asp:Label ID="lblDOCName" runat="server" Text='<%# Eval("DOC_NAME") %>' Visible="false" />

                                        <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                            onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="DOC_TYPE" HeaderText="Type" />
                                <asp:BoundField DataField="DOC_NAME" HeaderText="File Name" />

                            </Columns>
                            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>


                        <asp:Label ID="lblAttachmentsMsg" runat="server" ForeColor="Red" />


                    </fieldset>
                </div>

            </div>

            <br />

            <div class="full-width">
                <label>Remarks:</label>
                <asp:TextBox ID="txtRemarksToS" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" />
            </div>


        </fieldset>

        <%--<fieldset class="form-card">--%>

            <div class="full-width button-group">

                <asp:RadioButtonList ID="rdSavingType" runat="server" RepeatDirection="Horizontal" Width="100%">
                    <asp:ListItem Text="Save & send for approval" Value="1" Selected="True" />
                    <asp:ListItem Text="Save for later" Value="0" />
                </asp:RadioButtonList>

            </div>

            <div class="full-width button-group">

                <label>Checker:</label>
                <asp:DropDownList ID="ddlCheckerToS" runat="server" CssClass="form-control">
                </asp:DropDownList>

                <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>
        <%--</fieldset>--%>

    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>





    <%--ADD BILLING ADDRESS START--%>
    <asp:Button ID="btnShowPopupAddBillingAddress" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddBillingAddress" runat="server" TargetControlID="btnShowPopupAddBillingAddress"
        PopupControlID="pnlPopupAddBillingAddress" CancelControlID="imgBtnCancelAddBillingAddress" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddBillingAddress" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddBillingAddress" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="HiddenField3" runat="server" />

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Billing Address</legend>

                <div class="form-grid form-grid-2">

                    <label>Address Line 1:</label>
                    <asp:TextBox ID="txtAddress1ToS" runat="server"
                        CssClass="form-control" />

                    <label>Address Line 2:</label>
                    <asp:TextBox ID="txtAddress2ToS" runat="server"
                        CssClass="form-control" />

                    <label>Address Line 3:</label>
                    <asp:TextBox ID="txtAddress3ToS" runat="server"
                        CssClass="form-control" />

                    <label>State:</label>
                    <asp:DropDownList ID="ddlStateToS" runat="server"
                        CssClass="form-control"
                        onchange="GenerateGSTIN();">
                    </asp:DropDownList>

                    <label>Other State:</label>
                    <asp:TextBox ID="txtOtherStateToS" runat="server"
                        CssClass="form-control"
                        Enabled="false" />

                    <label>City:</label>
                    <asp:TextBox ID="txtCityToS" runat="server"
                        CssClass="form-control" />

                    <label>PIN Code:</label>
                    <asp:TextBox ID="txtPINCodeToS" runat="server"
                        CssClass="form-control" />

                    <label>Phone:</label>
                    <asp:TextBox ID="txtPhoneToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Email:</label>
                    <asp:TextBox ID="txtEmailToS" runat="server"
                        CssClass="form-control" />

                    <label>PAN:</label>
                    <asp:TextBox ID="txtPANToSS" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                    <label>GSTIn Running No.:</label>
                    <asp:TextBox ID="txtGSTInRunningNoToS" runat="server"
                        CssClass="form-control"
                        onkeyup="GenerateGSTIN();" />

                    <label>GSTIn:</label>
                    <asp:TextBox ID="txtGSTInToS" runat="server"
                        CssClass="form-control" />

                    <label>Is Default?:</label>
                    <asp:CheckBox ID="chkIsDefaultBillingAddressToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateAddressToList" CssClass="button" runat="server" Text="Add Address"
                    Width="100%" OnClick="btnAddUpdateAddressToList_Click"
                    OnClientClick="return ValidateBillingAddress();" />
            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Label ID="lblBillingAddressWindowMsg" runat="server" ForeColor="Red" />
                </div>
            </div>
        </div>



    </asp:Panel>
    <%--ADD BILLING ADDRESS END--%>


    <%--ADD CONTACT PERSON START--%>
    <asp:Button ID="btnShowPopupAddContactPerson" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddContactPerson" runat="server" TargetControlID="btnShowPopupAddContactPerson"
        PopupControlID="pnlPopupAddContactPerson" CancelControlID="imgBtnCancelAddContactPerson" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddContactPerson" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddContactPerson" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:HiddenField ID="HiddenField5" runat="server" />
        <asp:HiddenField ID="HiddenField6" runat="server" />

        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Add Contact Person</legend>

                <div class="form-grid form-grid-2">

                    <label>Name:</label>
                    <asp:TextBox ID="txtContactPersonNameToS" runat="server"
                        CssClass="form-control" />

                    <label>Mobile:</label>
                    <asp:TextBox ID="txtContactPersonMobileToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Phone:</label>
                    <asp:TextBox ID="txtContactPersonPhoneToS" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKeyWithDecimal(this, event);" />

                    <label>Email:</label>
                    <asp:TextBox ID="txtContactPersonEmailToS" runat="server"
                        CssClass="form-control" />

                    <label>Is Default?:</label>
                    <asp:CheckBox ID="chkIsDefaultContactPersonToS" runat="server" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateContactPersonToList" CssClass="button" 
                    runat="server" Text="Add Contact Person"
                    Width="100%" OnClick="btnAddUpdateContactPersonToList_Click"
                    OnClientClick="return ValidateContactPerson();" />
            </div>

        </div>



    </asp:Panel>
    <%--ADD CONTACT PERSON END--%>



    <%-- SHOW IMAGE FILE START --%>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeViewImgFileAttachment" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-img">
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <%-- SHOW IMAGE FILE END --%>

    <%-- SHOW PDF FILE START--%>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeViewPDFFileAttachment" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            class="popup-iframe"
            id="iframeViewPDFFile"
            runat="server"></iframe>
    </asp:Panel>
    <%-- SHOW PDF FILE END--%>

    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            class="popup-iframe"
            id="iframeViewTravelStatementInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>
</asp:Content>
