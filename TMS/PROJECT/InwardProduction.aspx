<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="InwardProduction.aspx.cs" Inherits="PROJECT_InwardProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SiteNew.css" rel="stylesheet" type="text/css" />    
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
    <link href="../Styles/filter.css" rel="stylesheet" />
    <link href="../Styles/grid.css" rel="stylesheet" />
    <link href="../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>


    <style type="text/css">
        .qty-input-container[readonly] {
            background-color: #f2f2f2;
            cursor: not-allowed;
        }
    </style>

    <style type="text/css">
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
    </style>
    <style type="text/css">
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

        .radio-list label {
            margin-right: 20px; /* Adjust space between radio options */
            display: inline-block;
        }


        .radio-list input[type="radio"] {
            transform: scale(1.5); /* Increases radio button size */
            margin-right: 5px; /* Adjust spacing */
        }

        .radio-list label {
            font-size: 35px; /* Increases text size */
            margin-right: 20px; /* Adds space between options */
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var txtDate = document.getElementById('<%=txtDate.ClientID %>');
            var hdDate = document.getElementById('<%=hdDate.ClientID %>');

            if (hdDate && txtDate) {
                // Prefer hdDate if it's set, else keep txtDate as-is
                if (hdDate.value.trim() !== '') {
                    txtDate.value = hdDate.value;
                } else {
                    hdDate.value = txtDate.value;
                }
            }


            var TDate = document.getElementById("<%=txtIsNoOfTrucksReq.ClientID %>");
            var TDate = document.getElementById("<%=hdnNoOfTruck.ClientID %>");

            if (hdnNoOfTruck && txtIsNoOfTrucksReq) {
                // Prefer hdDate if it's set, else keep txtDate as-is
                if (hdnTotalContainers.value.trim() !== "") {
                    txtIsNoOfTrucksReq.value = hdnNoOfTruck.value;
                } else {
                    hdnTotalContainers.value = txtIsNoOfTrucksReq.value;
                }
            }



        }


        function clientChanged(sender, args) {
            var txtDate = document.getElementById('<%=txtDate.ClientID %>');
            var hdDate = document.getElementById('<%=hdDate.ClientID %>');

            if (txtDate && hdDate && txtDate.value.trim() !== '') {
                hdDate.value = txtDate.value;
            }

            var TDate = document.getElementById("<%=txtIsNoOfTrucksReq.ClientID %>");
            var DTate = document.getElementById("<%=hdnNoOfTruck.ClientID %>");

            if (txtIsNoOfTrucksReq && hdnNoOfTruck && txtIsNoOfTrucksReq.value.trim() !== "") {
                hdnNoOfTruck.value = txtIsNoOfTrucksReq.value;
            }

        }


        function ValidateJobDomestic() {
            var ProblemFacedRemark = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (ProblemFacedRemark == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function VenNameDomestic() {
            var VenName = document.getElementById('<%=txtVenName2.ClientID %>').value;
            if (VenName == '') {
                document.getElementById('<%=txtVenName2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenName2.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function RemarkValidate() {
            var RemarksDomestic = document.getElementById('<%=txtRemarksDomestic.ClientID %>').value;
            if (RemarksDomestic == '') {
                document.getElementById('<%=txtRemarksDomestic.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRemarksDomestic.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function VenLocDomestic() {
            var VenLOc = document.getElementById('<%=txtVenLoc2.ClientID %>').value;
            if (VenLOc == '') {
                document.getElementById('<%=txtVenLoc2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenLoc2.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function VenEmailDomestic() {
            var VenEmail = document.getElementById('<%=txtVenEmail2.ClientID %>').value;
            if (VenEmail == '') {
                document.getElementById('<%=txtVenEmail2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenEmail2.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function VenContactDomestic() {
            var VenContact = document.getElementById('<%=txtVenContact.ClientID %>').value;
            if (VenContact == '') {
                document.getElementById('<%=txtVenContact.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenContact.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function VenDelLocDomestic() {
            var delLoc = document.getElementById('<%=ddlDeliveryLoc.ClientID %>').selectedIndex;
            if (delLoc == '') {
                document.getElementById('<%=ddlDeliveryLoc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDeliveryLoc.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function IncoTermsDomestic() {
            var IncoTerms = document.getElementById('<%=ddlIncoterms2.ClientID %>').selectedIndex;
            if (IncoTerms == '') {
                document.getElementById('<%=ddlIncoterms2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlIncoterms2.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        function DeliveryTerm() {
            var DelTerm = document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').selectedIndex;
            if (DelTerm == '' || DelTerm == '0') {
                document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateNoOfTrucksReqDomestic() {
            var inputElement = document.getElementById('<%=txtIsNoOfTrucksReq.ClientID %>');
            var value = inputElement.value.trim();
            var isValid = true;

            // Check if value is numeric and greater than 0
            if (value === '' || isNaN(value) || Number(value) <= 0) {
                inputElement.style.borderColor = "#F7627F"; // Red border
                isValid = false;
            } else {
                inputElement.style.borderColor = ""; // Reset border
            }

            return isValid;
        }



        function DateDomestic() {
            var VenContact = document.getElementById('<%=txtDate.ClientID %>').value;
            if (VenContact == '') {
                document.getElementById('<%=txtDate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDate.ClientID %>').stylrderColor = "";
                return false;
            }
        }


        function ValidateTruckQuantities() {
            var isAnyFilled = false;
            var isAllNumeric = true;
            var inputs = document.querySelectorAll('.qty-input');

            inputs.forEach(function (input) {
                var value = input.value.trim();

                // Reset border
                input.style.borderColor = "";

                if (value !== "") {
                    isAnyFilled = true;
                    if (isNaN(value) || Number(value) < 0) {
                        isAllNumeric = false;
                        input.style.borderColor = "#F7627F"; // highlight invalid
                    }
                }
            });

            if (!isAnyFilled) {
                return false;
            }

            if (!isAllNumeric) {
                return false;
            }

            return true;
        }

        function ValidatefileUploadVisitSummary1() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById('<%=fileUploadVisitSummary1.ClientID %>');
            var filePath = fileInput.value.trim().toLowerCase();

            // If no file selected
            if (filePath === '') {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Get file extension
            var fileExtension = filePath.substring(filePath.lastIndexOf('.'));

            // If invalid file extension
            if (!allowedExtensions.includes(fileExtension)) {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Valid file
            fileInput.style.borderColor = "";
            return false;
        }


        function TypeConsignmentDomestic() {
            var DelTerm = document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').selectedIndex;
            if (DelTerm == '' || DelTerm == '0') {
                document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        function ValidatefileUploadVisitSummary2() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById('<%=fileUpload3.ClientID %>');
            var filePath = fileInput.value.trim().toLowerCase();

            // If no file selected
            if (filePath === '') {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Get file extension
            var fileExtension = filePath.substring(filePath.lastIndexOf('.'));

            // If invalid file extension
            if (!allowedExtensions.includes(fileExtension)) {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Valid file
            fileInput.style.borderColor = "";
            return true;
        }




        function ValidateDomestic() {
            var check = true;

            /*------------------------------*/
            //if (VenLocDomestic()) check = false;
            //if (VenEmailDomestic()) check = false;
            //if (VenContactDomestic()) check = false;
            //if (VenDelLocDomestic()) check = false;
            //if (IncoTermsDomestic()) check = false;
            /*if (!ValidateNoOfTrucksReqDomestic()) check = false;*/
            /*------------------------------*/

            if (ValidateJobDomestic()) check = false;
            if (VenNameDomestic()) check = false;
            if (TypeConsignmentDomestic()) check = false;
            if (DeliveryTerm()) check = false;

            if (document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').selectedIndex != 2) {
                if (!ValidateTruckQuantities()) check = false;
                if (!ValidateNoOfTrucksReqDomestic()) check = false;
            }


            //if (!ValidateTruckQuantities()) check = false;
            if (ValidatefileUploadVisitSummary1()) check = false;
            if (RemarkValidate()) check = false;

            console.log('ValidateDomestic returning:', check);

            return check;


        }


    </script>

    <%--/////////////////INTERNATIONAL VALIDATION START--%>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var txtDate = document.getElementById("<%=txtStartDate.ClientID %>");
            var hdDate = document.getElementById("<%=hdStartDate.ClientID %>");

            if (hdDate && txtDate) {
                // Prefer hdDate if it's set, else keep txtDate as-is
                if (hdDate.value.trim() !== "") {
                    txtDate.value = hdDate.value;
                } else {
                    hdDate.value = txtDate.value;
                }
            }

         <%--var CDate = document.getElementById("<%=txtIsNoOfContainersRequired.ClientID %>");
            var hdCDate = document.getElementById("<%=hdnTotalContainers.ClientID %>");

            if (hdnTotalContainers && txtIsNoOfContainersRequired) {
                // Prefer hdDate if it's set, else keep txtDate as-is
                if (hdnTotalContainers.value.trim() !== "") {
                    txtIsNoOfContainersRequired.value = hdnTotalContainers.value;
                } else {
                    hdnTotalContainers.value = txtIsNoOfContainersRequired.value;
                }
            }--%>

        }

        function clientChanged(sender, args) {
            var txtDate = document.getElementById("<%=txtStartDate.ClientID %>");
            var hdDate = document.getElementById("<%=hdStartDate.ClientID %>");

            if (txtDate && hdDate && txtDate.value.trim() !== "") {
                hdDate.value = txtDate.value;
            }


            var CDate = document.getElementById("<%=txtIsNoOfContainersRequired.ClientID %>");
            var CDate = document.getElementById("<%=hdnTotalContainers.ClientID %>");

            if (txtIsNoOfContainersRequired && hdnTotalContainers && txtIsNoOfContainersRequired.value.trim() !== "") {
                hdnTotalContainers.value = txtIsNoOfContainersRequired.value;
            }


        }

        function ValidateJobInternational() {
            var ProblemFacedRemark = document.getElementById(
      "<%=TextBox1.ClientID %>"
            ).value;
            if (ProblemFacedRemark == "") {
                document.getElementById("<%=TextBox1.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=TextBox1.ClientID %>").style.borderColor = "";
                return false;
            }
        }

        function VenNameInternational() {
            var VenName = document.getElementById("<%=txtVendorName.ClientID %>").value;
            if (VenName == "") {
                document.getElementById(
        "<%=txtVendorName.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorName.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function VenLocInternational() {
            var VenLOc = document.getElementById(
      "<%=txtVendorLocation.ClientID %>"
            ).value;
            if (VenLOc == "") {
                document.getElementById(
        "<%=txtVendorLocation.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorLocation.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function VenEmailInternational() {
            var VenEmail = document.getElementById(
      "<%=txtVendorEmail.ClientID %>"
            ).value;
            if (VenEmail == "") {
                document.getElementById(
        "<%=txtVendorEmail.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorEmail.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function VenContactInternational() {
            var VenContact = document.getElementById(
      "<%=txtVendorContactInt.ClientID %>"
            ).value;
            if (VenContact == "") {
                document.getElementById(
        "<%=txtVendorContactInt.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorContactInt.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function VenDelLocInternational() {
            var delLoc = document.getElementById(
      "<%=ddlVenLoc.ClientID %>"
            ).selectedIndex;
            if (delLoc == "" || delLoc == "0") {
                document.getElementById("<%=ddlVenLoc.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=ddlVenLoc.ClientID %>").style.borderColor =
                    "";
                return false;
            }
        }

        function RemarkValidateInt() {
            var RemarksInternational = document.getElementById('<%=txtRemarksInternational.ClientID %>').value;
            if (RemarksInternational == '') {
                document.getElementById('<%=txtRemarksInternational.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRemarksInternational.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function IsContainerStuffingPossibleInternational() {
            var delLoc = document.getElementById(
      "<%=ddlIsStuffing.ClientID %>"
            ).selectedIndex;
            if (delLoc == "" || delLoc == "0") {
                document.getElementById("<%=ddlIsStuffing.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=ddlIsStuffing.ClientID %>").style.borderColor =
                    "";
                return false;
            }
        }

        function IncoTermsInternational() {
            var IncoTerms = document.getElementById(
      "<%=ddlInco2.ClientID %>"
            ).selectedIndex;
            if (IncoTerms == "") {
                document.getElementById("<%=ddlInco2.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=ddlInco2.ClientID %>").style.borderColor = "";
                return false;
            }
        }

        function ModeOfTransportInternational() {
            var DelTerm = document.getElementById(
      "<%=ddlModeOfTransport.ClientID %>"
            ).selectedIndex;
            if (DelTerm == "" || DelTerm == "0") {
                document.getElementById(
        "<%=ddlModeOfTransport.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=ddlModeOfTransport.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function ValidateNoOfContainersReqInternational() {
            var inputElement = document.getElementById(
      "<%=txtIsNoOfContainersRequired.ClientID %>"
            );
            var value = inputElement.value.trim();
            var isValid = true;

            // Check if value is numeric and greater than 0
            if (value == "" || isNaN(value) || Number(value) <= 0) {
                inputElement.style.borderColor = "#F7627F"; // Red border
                isValid = false;
            } else {
                inputElement.style.borderColor = ""; // Reset border
                inputElement.style.borderColor = "green";

            }

            return isValid;
        }

        function DateInternational() {
            var VenContact = document.getElementById(
      "<%=txtStartDate.ClientID %>"
            ).value;

            if (VenContact == "") {
                document.getElementById("<%=txtStartDate.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=txtStartDate.ClientID %>").style.borderColor =
                    "";
                return false;
            }
        }

        function ValidateContainerQuantities() {
            var isAnyFilled = false;
            var isAllNumeric = true;
            var inputs = document.querySelectorAll(".qty-input-container");

            inputs.forEach(function (input) {
                var value = input.value.trim();

                // Reset border
                input.style.borderColor = "";

                if (value !== "") {
                    isAnyFilled = true;
                    if (isNaN(value) || Number(value) < 0) {
                        isAllNumeric = false;
                        input.style.borderColor = "#F7627F"; // highlight invalid
                    }
                }
            });

            if (!isAnyFilled) {
                return false;
            }

            if (!isAllNumeric) {
                return false;
            }

            return true;
        }

        function ValidatefileUploadPackingList() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById("<%=fileUpload1.ClientID %>");
            var filePath = fileInput.value.trim().toLowerCase();

            // If no file selected
            if (filePath === "") {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Get file extension
            var fileExtension = filePath.substring(filePath.lastIndexOf("."));

            // If invalid file extension
            if (!allowedExtensions.includes(fileExtension)) {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Valid file
            fileInput.style.borderColor = "";
            return true;
        }

        function TypeConsignmentInternational() {
            var DelTerm = document.getElementById(
      "<%=ddlTypeConsignment.ClientID %>"
            ).selectedIndex;
            if (DelTerm === 0) {
                document.getElementById(
        "<%=ddlTypeConsignment.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=ddlTypeConsignment.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }
        function ValidatefileUploadInvoiceCOO() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById("<%=fileUpload2.ClientID %>");
            var filePath = fileInput.value.trim().toLowerCase();

            // If no file selected
            if (filePath === "") {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Get file extension
            var fileExtension = filePath.substring(filePath.lastIndexOf("."));

            // If invalid file extension
            if (!allowedExtensions.includes(fileExtension)) {
                fileInput.style.borderColor = "#F7627F";
                return false;
            }

            // Valid file
            fileInput.style.borderColor = "";
            return true;
        }

        function ValidateInternational() {
            var check = true;


            if (ValidateJobInternational()) check = false;
            if (VenNameInternational()) check = false;
            /* //if (VenLocInternational()) check = false;
             //if (VenEmailInternational()) check = false;
             //if (VenContactInternational()) check = false;*/
            //if (VenDelLocInternational()) check = false;
            if (IncoTermsInternational()) check = false;

            if (ModeOfTransportInternational()) check = false;

            if (DateInternational()) check = false;

            if (!ValidatefileUploadPackingList()) check = false;
            if (RemarkValidateInt()) check = false;

            var ddl = document.getElementById('<%= ddlModeOfTransport.ClientID %>');
            var selectedValue = ddl ? ddl.value : "";

            if (selectedValue !== "Air") {
                if (IsContainerStuffingPossibleInternational()) check = false;
                if (TypeConsignmentInternational()) check = false;
                if (!ValidateContainerQuantities()) check = false;
                if (!ValidateNoOfContainersReqInternational()) check = false;
            }
            else if (selectedValue !== "Air") {

            }


            return check;
        }
    </script>


    <script type="text/javascript">
        function updateTruckTotal() {
            let total = 0;
            document.querySelectorAll('.qty-input').forEach(function (input) {
                let val = parseInt(input.value);
                if (!isNaN(val)) {
                    total += val;
                }
            });
            document.getElementById('<%= txtIsNoOfTrucksReq.ClientID %>').value = total;
            document.getElementById('<%= hdnNoOfTruck.ClientID %>').value = total;
        }

        function updateContainerTotal() {
            let total = 0;
            document.querySelectorAll('.qty-input-container').forEach(function (input) {
                let val = parseInt(input.value);
                if (!isNaN(val)) {
                    total += val;
                }
            });
            document.getElementById('<%=txtIsNoOfContainersRequired.ClientID %>').value = total;
            document.getElementById('<%= hdnTotalContainers.ClientID %>').value = total;
            console.log(total);
        }

        window.onload = function () {
            document.querySelectorAll('.qty-input').forEach(function (input) {
                input.addEventListener('input', updateTruckTotal);
            });
            document.querySelectorAll('.qty-input-container').forEach(function (input) {
                input.addEventListener('input', updateContainerTotal);
            });
        };
    </script>

    <script type="text/Javascript">

        function EnableMatPickupLoc() {
            var chk = document.getElementById('<%=chkMatPickupLocSameAsAbove.ClientID %>').checked;
            document.getElementById('<%=txtMatPickupLoc.ClientID %>').disabled = !chk;
        }

        function EnableMatPickupLocINT() {
            var chk = document.getElementById('<%=chkMatPickupLocSameAsAboveINT.ClientID %>').checked;
            document.getElementById('<%=txtMatPickupLocINT.ClientID %>').disabled = !chk;
        }

        function onModeOfTransportChange(ddl) {
            var selectedValue = ddl.options[ddl.selectedIndex].text.toLowerCase();
            var stuffingDropdown = document.getElementById('<%= ddlIsStuffing.ClientID %>');
            var noOfContainers = document.getElementById('<%=txtIsNoOfContainersRequired.ClientID %>');

            var typeOfConsignmentDropdown = document.getElementById('<%= ddlTypeConsignment.ClientID %>');
            var typeContainer = document.getElementById('<%= rptContainer.ClientID %>');
            var panel4 = document.getElementById('<%= Panel4.ClientID %>');

            if (selectedValue === "air") {
                stuffingDropdown.disabled = true;
                typeOfConsignmentDropdown.disabled = true;
                noOfContainers.disabled = true;
                noOfContainers.readOnly = true;
                // typeContainer.disabled = true;
                if (panel4) {
                    var inputs = panel4.querySelectorAll('input.qty-input-container');
                    console.log("Inputs inside Panel4 found:", inputs.length); // Debug

                    for (var i = 0; i < inputs.length; i++) {
                        var input = inputs[i];
                        input.readOnly = true;
                        input.disabled = true;
                        input.style.backgroundColor = "#f5f5f5";
                    }
                }
            }
            else {
                stuffingDropdown.disabled = false;
                typeOfConsignmentDropdown.disabled = false;
                noOfContainers.disabled = false;
                noOfContainers.readOnly = false;
                //typeContainer.disabled = false;
                if (panel4) {
                    var inputs = panel4.querySelectorAll('input.qty-input-container');
                    console.log("Inputs inside Panel4 found:", inputs.length); // Debug

                    for (var i = 0; i < inputs.length; i++) {
                        var input = inputs[i];
                        input.readOnly = false;
                        input.disabled = false;
                        input.style.backgroundColor = "";
                    }
                }


            }
        }



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
    <%--   <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>--%>

    <%--<add tagPrefix="ajaxToolkit" namespace="AjaxControlToolkit" assembly="AjaxControlToolkit" />--%>

    <asp:HiddenField ID="HiddenField1" runat="server" />

    <div class="form-entry-container">
        <fieldset class="form-card">

            <%--<div class="form-grid form-grid-2">--%>

            <div class="full-width">
                <asp:RadioButtonList ID="rblForms" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="rblForms_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Domestic Inward" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="International Inward" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <%--</div>--%>
        </fieldset>
    </div>


    <div class="form-entry-container">

        <div class="full-width">

            <asp:Panel ID="pnlForm1" runat="server" Visible="false">
                <asp:HiddenField ID="hdConfirmValue" runat="server" />


                <fieldset class="form-card">
                    <legend>Domestic Inward</legend>

                    <div class="form-grid form-grid-2">

                        <label>JOB Number:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control"
                                        Enabled="false" onblur="return ValidateJOBNo();" />
                                </td>

                                <td style="width: 10%">
                                    <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetJOBNo_Click" />

                                    <asp:Button ID="btnAddApprover" runat="server" Width="100%" Text="Add Approver" CssClass="button"
                                        OnClick="btnAddApprover_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>


                        <label>Vendor Name:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtVenName2" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>

                                <td style="width: 10%">
                                    <asp:Button ID="Button6" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetDomestic_Click" />

                                </td>
                            </tr>
                        </table>

                        <label>Vendor Location/Address:</label>
                        <asp:TextBox ID="txtVenLoc2" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor's Email:</label>
                        <asp:TextBox ID="txtVenEmail2" runat="server"
                            CssClass="form-control"
                            Enabled="true" />

                        <label>Vendor Contact Details for loading (If available):</label>
                        <asp:TextBox ID="txtVenContact" runat="server"
                            CssClass="form-control"
                            Enabled="true" />

                        <label>Material Delivery Location:</label>
                        <asp:DropDownList ID="ddlDeliveryLoc" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                        <div class="full-width">
                            <label>Is Material Pickup location other than above :</label>
                            <asp:CheckBox ID="chkMatPickupLocSameAsAbove" runat="server" Checked="false"
                                Enabled="true" onchange="EnableMatPickupLoc()" />
                        </div>

                        <div class="full-width">
                            <label>Enter Material Pickup Location & Concerned Person Contact Details:</label>
                            <asp:TextBox ID="txtMatPickupLoc" runat="server"
                                CssClass="form-control"
                                Enabled="false" TextMode="MultiLine" colspan="2"
                                Rows="2" />
                        </div>

                        <div style="visibility: hidden">
                            <label>Incoterms:</label>
                        </div>
                        <asp:DropDownList ID="ddlIncoterms2" disabled="true" Visible="false" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                    <div class="form-grid form-grid-2">

                        <label>Delivery Term:</label>
                        <asp:DropDownList ID="ddlDeliveryTerm2" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>No. Of Trucks Required (if container not possible)</label>
                        <asp:TextBox ID="txtIsNoOfTrucksReq" runat="server"
                            CssClass="form-control"
                            Enabled="false" />
                        <asp:HiddenField ID="hdnNoOfTruck" runat="server" />

                        <label>Readiness Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdDate" runat="server" />
                                    <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate"
                                        runat="server" TargetControlID="txtDate" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChanged">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Date of Logging Lesson Learnt" />
                                </td>
                            </tr>
                        </table>

                        <label>Type Of Consignment:</label>
                        <asp:DropDownList ID="ddlTypeOfConsignment2" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true">
                        </asp:DropDownList>

                        <label>Packing List:</label>
                        <asp:FileUpload ID="fileUploadVisitSummary1" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />


                        <label>Sub Vendor Invoice:</label>
                        <asp:FileUpload ID="fileUpload3" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadVisitSummary1();" />

                        <div class="full-width">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksDomestic"
                                CssClass="form-control"
                                runat="server"
                                Enabled="true" TextMode="MultiLine"
                                Rows="2" />
                        </div>

                        <div class="full-width">
                            <label>Enter Truck Details:</label>
                            <asp:Panel ID="pnlTrucks" runat="server" Height="50px">
                                <table border="1" cellpadding="5" width="100%">
                                    <tr>
                                        <td>Full Truck</td>
                                        <td>Qty of Trucks Required</td>
                                    </tr>
                                    <asp:Repeater ID="rptTrucks" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 30%;">
                                                    <asp:Label ID="lblTruck" runat="server" Text='<%# Eval("TruckName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfTruckType" runat="server" Value='<%# Eval("TruckName") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </div>

                    </div>
                </fieldset>


                <div class="full-width button-group">
                    <br />
                    <asp:Button ID="Button1" CssClass="button" runat="server" Text="Save"
                        OnClientClick="return ValidateDomestic();" Width="100%" Style="margin-right: 20px;"
                        OnClick="btnSubmit_Click" />

                    <asp:Button ID="Button2" CssClass="button" runat="server" Text="View List"
                        Width="100%" OnClick="btnViewList_Click" />
                </div>

                <div class="full-width">
                    <asp:Panel ID="pnlDomestic" Visible="false" runat="server" Style="min-height: 20px;">
                        <asp:Label ID="lblDomestic" runat="server" Font-Bold="True" Font-Size="Large" />
                    </asp:Panel>
                </div>


            </asp:Panel>

        </div>


        <div class="full-width">
            <asp:Panel ID="pnlForm2" runat="server">

                <fieldset class="form-card">
                    <legend>International Inward</legend>

                    <div class="form-grid form-grid-2">

                        <label>Project Number:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"
                                        Enabled="false"
                                        CssClass="form-control"
                                        onblur="return ValidateJOBNo();" />
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetJOBNo_Click" />

                                </td>

                            </tr>
                        </table>


                        <label>Vendor Name:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtVendorName" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                                <td>
                                    <asp:Button ID="btnInterVendor" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetDomestic_Click" />

                                </td>

                            </tr>
                        </table>


                        <label>Vendor Location/Address:</label>
                        <asp:TextBox ID="txtVendorLocation" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor's Email:</label>
                        <asp:TextBox ID="txtVendorEmail" runat="server"
                            CssClass="form-control"
                            Enabled="true" />

                        <label>Vendor Contact Details for loading (If available):</label>
                        <asp:TextBox ID="txtVendorContactInt" runat="server"
                            CssClass="form-control"
                            Enabled="true" />

                        <label>Material Delivery Location:</label>
                        <asp:DropDownList ID="ddlVenLoc" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true">
                        </asp:DropDownList>

                        <div class="full-width">
                            <label>Is Material Pickup location other than above :</label>
                            <asp:CheckBox ID="chkMatPickupLocSameAsAboveINT" runat="server" Checked="false"
                                Enabled="true" onchange="EnableMatPickupLocINT()" />
                        </div>

                        <div class="full-width">
                            <label>Enter Material Pickup Location & Concerned Person Contact Details:</label>
                            <asp:TextBox ID="txtMatPickupLocINT" runat="server"
                                CssClass="form-control"
                                Enabled="false" TextMode="MultiLine"
                                colspan="2"
                                Rows="2" />
                        </div>

                        <label>Incoterms:</label>
                        <asp:DropDownList ID="ddlInco2" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                        <label>Mode Of Transport:</label>
                        <asp:DropDownList ID="ddlModeOfTransport" runat="server"
                            CssClass="form-control"
                            onchange="onModeOfTransportChange(this)">
                        </asp:DropDownList>

                        <label>Container Stuffing Possible:</label>
                        <asp:DropDownList ID="ddlIsStuffing" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                        <label>No. Of Containers Required:</label>
                        <asp:TextBox ID="txtIsNoOfContainersRequired" runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />
                        <asp:HiddenField ID="hdnTotalContainers" runat="server" />


                        <div style="visibility: hidden;">
                            <label>No. Of Trucks Required (if container not possible)</label>
                            <asp:TextBox ID="txtNoOfTrucks" runat="server"
                                CssClass="form-control"
                                Enabled="false" />
                        </div>

                    </div>

                    <div class="form-grid form-grid-2">

                        <label>Readiness Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdStartDate" runat="server" />
                                    <ajax:CalendarExtender ID="calendarStartDate" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDate" Format="dd-MMM-yyyy">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>

                        <label>Type Of Consignment:</label>
                        <asp:DropDownList ID="ddlTypeConsignment" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>


                        <label>Packing List:</label>
                        <asp:FileUpload ID="fileUpload1" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadPackingList();" />

                        <label>Sub Vendor Invoice:</label>
                        <asp:FileUpload ID="fileUpload2" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadInvoiceCOO();" />

                        <div class="full-width">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksInternational"
                                CssClass="form-control"
                                runat="server"
                                Enabled="true" TextMode="MultiLine"
                                Rows="2" />
                        </div>


                        <div class="full-width">
                            <label>In case of Sea Shipment:</label>
                            <asp:Panel ID="Panel4" runat="server" Height="50px">
                                <table border="1" cellpadding="5" width="100%">
                                    <tr>
                                        <td>Container type/size</td>
                                        <td>Qty of Containers Required</td>
                                    </tr>
                                    <asp:Repeater ID="rptContainer" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 30%;">
                                                    <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("ContainerName") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfContainerType" runat="server" Value='<%# Eval("ContainerName") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQtyContainer" runat="server" CssClass="qty-input-container form-control" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>

                        </div>

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <br />
                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save"
                        OnClientClick="return ValidateInternational();" Width="100%" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnTourList" CssClass="button" runat="server" Text="View List"
                        Width="100%" OnClick="btnViewList_Click" />
                </div>
                <div class="full-width">
                    <asp:Panel ID="pnlInternational" Visible="false" runat="server" Style="min-height: 20px;">
                        <asp:Label ID="lbllInternational" runat="server" Font-Bold="True" Font-Size="Large" />
                    </asp:Panel>
                </div>

            </asp:Panel>
        </div>


    </div>


    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="upnlPopup" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server" CssClass="popup-edit">
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
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <div class="full-width button-group">

                            <label>JOB No.:</label>
                            <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                            <label>Customer PO No.:</label>
                            <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />

                            <label>Customer Code:</label>
                            <asp:TextBox ID="txtCustomerCodeSearch" runat="server" CssClass="form-control" />

                            <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchJOBNo_Click" />

                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvAttachments_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblAppJOBNo" runat="server" Visible="false" Text='<%# Eval("APP_JOB_NO") %>' />
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblCustCode" runat="server" Visible="false" Text='<%# Eval("CUST_CODE") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT_ID") %>' />
                                <asp:Label ID="lblUnitName" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT") %>' />

                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                        <asp:BoundField DataField="JOB_UNIT" HeaderText="UNIT" />
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

    <%--VENDOR DETAIL START--%>

    <asp:Button ID="btnShowPopupVendorDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeVendorDetail" runat="server" TargetControlID="btnShowPopupVendorDetail"
        PopupControlID="pnlPopupVendorDetail" CancelControlID="imgBtnCancelVendorDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupVendorDetail" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelVendorDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblVendorDetail" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <div class="full-width button-group">
                            <label>Vendor Code:</label>
                            <asp:TextBox ID="txtVenCode" runat="server" CssClass="form-control" />

                            <label>Vendor Name:</label>
                            <asp:TextBox ID="txtVenName" runat="server" CssClass="form-control" />

                            <asp:Button ID="Button8" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchVendor_Click" />

                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">

                <asp:GridView
                    CssClass="employee-grid"
                    ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvVendDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" Visible="false" Text='<%# Eval("UNIT") %>' />
                                <asp:Label ID="lblVendCode" runat="server" Visible="false" Text='<%# Eval("CUSTCODE") %>' />
                                <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# Eval("NAME") %>' />
                                <asp:Label ID="lblAddress1" runat="server" Visible="false" Text='<%# Eval("ADDRESS1") %>' />
                                <asp:Label ID="lblAddress2" runat="server" Visible="false" Text='<%# Eval("ADDRESS2") %>' />
                                <asp:Label ID="lblAddress0" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />
                                <asp:Label ID="lblCity" runat="server" Visible="false" Text='<%# Eval("CITY") %>' />
                                <asp:Label ID="lblState" runat="server" Visible="false" Text='<%# Eval("STATE") %>' />
                                <asp:Label ID="lblCountry" runat="server" Visible="false" Text='<%# Eval("COUNTRY") %>' />
                                <asp:Label ID="lblPin" runat="server" Visible="false" Text='<%# Eval("PINCODE") %>' />
                                <asp:Label ID="lblEmail" runat="server" Visible="false" Text='<%# Eval("EMAIL") %>' />
                                <asp:Label ID="lblMob" runat="server" Visible="false" Text='<%# Eval("MOBPHONE") %>' />


                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get Vendor Details" CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
                        <asp:BoundField DataField="CUSTCODE" HeaderText="VEND_CODE" />
                        <asp:BoundField DataField="NAME" HeaderText="NAME" />
                        <asp:BoundField DataField="ADDRESS1" HeaderText="ADDRESS1" />
                        <asp:BoundField DataField="ADDRESS2" HeaderText="ADDRESS2" />
                        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                        <asp:BoundField DataField="CITY" HeaderText="CITY" />
                        <asp:BoundField DataField="STATE" HeaderText="STATE" />
                        <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" />
                        <asp:BoundField DataField="PINCODE" HeaderText="PINCODE" />
                        <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                        <asp:BoundField DataField="MOBPHONE" HeaderText="PHONE" />

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
    <%--VENDOR DETAIL END--%>
</asp:Content>

