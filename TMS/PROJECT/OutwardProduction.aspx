<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="OutwardProduction.aspx.cs" Inherits="PROJECT_OutwardProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
    <link href="../Styles/filter.css" rel="stylesheet" />
    <link href="../Styles/grid.css" rel="stylesheet" />
    <link href="../Styles/popup.css" rel="stylesheet" />

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







    <script type="text/javascript">
        function ValidateDateRangeD() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdDate.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);



        }
    </script>

    <script type="text/javascript">

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

        function EnableMatPickupLoc() {
            var chk = document.getElementById('<%=chkMatPickupLocSameAsAbove.ClientID %>').checked;
            document.getElementById('<%=txtMatPickupLoc.ClientID %>').disabled = !chk;
        }

        function EnableMatPickupLocInt() {
            var chk = document.getElementById('<%=chkClientDelAddress.ClientID %>').checked;
            document.getElementById('<%=txtClientAddress.ClientID %>').disabled = !chk;
        }
    </script>


    <script type="text/javascript">
        function updateTruckTotal() {
            let total = 0;

            // Use correct class selector (qty-input from your TextBoxes)
            document.querySelectorAll('.qty-input-truck').forEach(function (input) {
                let val = parseFloat(input.value);
                total += isNaN(val) ? 0 : val;
            });

            // Get the total textbox by ClientID (replace with actual ClientID on render)
            var truckTotalBox = document.getElementById('<%= txtIsNoOfTrucksReq.ClientID %>');
            if (truckTotalBox) {
                truckTotalBox.value = total;
                document.getElementById('<%= hdnNoOfTruck.ClientID %>').value = total;
            }
            else {
                console.error("Could not find txtIsNoOfTrucksReq element");
            }
        }



        function updateContainerTotal() {
            let total01 = 0;

            // Use correct class selector (qty-input from your TextBoxes)
            document.querySelectorAll('.qty-input-container').forEach(function (input) {
                let val = parseFloat(input.value);
                total01 += isNaN(val) ? 0 : val;
            });

            // Get the total textbox by ClientID (replace with actual ClientID on render)
            var containerTotalBox = document.getElementById('<%= txtIsNoOfContainersRequired.ClientID %>');
            if (containerTotalBox) {
                containerTotalBox.value = total01;
                document.getElementById('<%= hdnTotalContainers.ClientID %>').value = total01;
            } else {
                console.error("Could not find txtNoContainersEditInt element");
            }
        }


    </script>


    <script type="text/javascript">


        function ValidateJobDomestic() {
            var JobNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (JobNo == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateVenNameD() {
            var CustVendName = document.getElementById('<%=txtVenName2.ClientID %>').value;
            if (CustVendName == '') {
                document.getElementById('<%=txtVenName2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenName2.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLocD() {
            var PlaceOfVisit = document.getElementById('<%=txtVenLoc2.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtVenLoc2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenLoc2.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEmailD() {
            var emailField = document.getElementById('<%=txtVenEmail2.ClientID %>');
            var email = emailField.value.trim();
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (email === '' || !emailPattern.test(email)) {
                emailField.style.borderColor = "#F7627F";
                return false; // Invalid email
            } else {
                emailField.style.borderColor = "";
                return true; // Valid email
            }
        }


        function ValidateContactD() {
            var contactField = document.getElementById('<%=txtVenContact.ClientID %>');
            var contact = contactField.value.trim();

            var phonePattern = /^[6-9]\d{9}$/;

            if (contact === '' || !phonePattern.test(contact)) {
                contactField.style.borderColor = "#F7627F";
                return false; // Invalid phone number
            } else {
                contactField.style.borderColor = "";
                return true; // Valid phone number
            }
        }


        function ValidateVendorDelLocD() {
            var TourBasedOn = document.getElementById('<%=ddlDeliveryLoc.ClientID %>').selectedIndex;
            if (TourBasedOn == '') {
                document.getElementById('<%=ddlDeliveryLoc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDeliveryLoc.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateVendorNamePickupD() {
            var PlaceOfVisit = document.getElementById('<%=txtVendornameVenPickup.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtVendornameVenPickup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendornameVenPickup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateVendorAddressPickupD() {
            var PlaceOfVisit = document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateContactDetailsPickupD() {
            var PlaceOfVisit = document.getElementById('<%=txtVenContactDetailsPickup.ClientID %>').value;
            if (PlaceOfVisit == '') {
                document.getElementById('<%=txtVenContactDetailsPickup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtVenContactDetailsPickup.ClientID %>').style.borderColor = "";
                return false;
            }
        }


<%--           function ValidateVendorAddressPickupD() {
               var PlaceOfVisit = document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').value;
               if (PlaceOfVisit == '') {
                   document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                   document.getElementById('<%=txtVendAddressVenPickup.ClientID %>').style.borderColor = "";
                   return false;
               }
           }--%>

         <%--  function ValidateIncotermsD() {
               var ModeOfTravel = document.getElementById('<%=ddlIncoterms2.ClientID %>').selectedIndex;
            if (ModeOfTravel == '' || ModeOfTravel == '0') {
                document.getElementById('<%=ddlIncoterms2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlIncoterms2.ClientID %>').style.borderColor = "";
                return false;
            }
           }--%>

        function ValidateDeliverytermsD() {
            var ModeOfTravel = document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').selectedIndex;
            if (ModeOfTravel == '' || ModeOfTravel == '0') {
                document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDeliveryTerm2.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMatPickLocOthers() {
            var selectedOption = document.getElementById('<%=ddlDeliveryLoc.ClientID %>').selectedIndex;
            var value = document.getElementById('<%=txtothersPickupLocD.ClientID %>').value;

            if (selectedOption == '5' && value == '') {
                document.getElementById('<%=txtothersPickupLocD.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtothersPickupLocD.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateIsClientAddressOtherThanAboveChecked() {
            var isChecked = document.getElementById('<%=chkMatPickupLocSameAsAbove.ClientID %>').checked;
            var value = document.getElementById('<%=txtMatPickupLoc.ClientID %>').value;

            if (isChecked && value == '') {
                document.getElementById('<%=txtMatPickupLoc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById('<%=txtMatPickupLoc.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateNoOfTruckD() {
            var truckInput = document.getElementById('<%=txtIsNoOfTrucksReq.ClientID %>');
            var value = truckInput.value.trim();

            if (value === '' || isNaN(value)) {
                truckInput.style.borderColor = "#F7627F"; // highlight invalid input
                return true; // validation failed
            } else {
                truckInput.style.borderColor = ""; // reset border color
                return false; // validation passed
            }
        }


        function ValidateTypeConsignmentDateD() {
            var TypeOfTrip = document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').selectedIndex;

            if (TypeOfTrip == '') {
                document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').style.borderColor = "#F7627F";
                return true; // Validation failed
            } else {
                document.getElementById('<%=ddlTypeOfConsignment2.ClientID %>').style.borderColor = "";
                return false; // Validation passed

            }
        }


        function validateTruckRepeater() {
            var textboxes = document.querySelectorAll("input[type='text'][class*='qty-input-truck']");
            var atLeastOneFilled = false;
            var allValid = true;

            for (var i = 0; i < textboxes.length; i++) {
                var textbox = textboxes[i];
                var value = textbox.value.trim();

                // Reset styles
                textbox.style.border = "1px solid #ced4da"; // Bootstrap default, or adjust

                if (value !== '') {
                    atLeastOneFilled = true;

                    // Check if numeric
                    if (isNaN(value)) {
                        textbox.style.border = "2px solid #F7627F"; // Highlight in red
                        allValid = false;
                    }
                }
            }

            if (!atLeastOneFilled) {
                return false;
            }

            if (!allValid) {
                return false;
            }

            return true; // All good
        }

        function ValidatePartLoadDetailsD() {
            var TourInitiative = document.getElementById('<%=ddlPartLoadDetails.ClientID %>').selectedIndex;
            if (TourInitiative == '' && TourInitiative == 0) {
                document.getElementById('<%=ddlPartLoadDetails.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPartLoadDetails.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        function ValidatefileUploadPackingList() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById('<%=fileUploadPackingList.ClientID %>');
            var divError = document.getElementById("divfileUploadVisitSummary1"); // You need this!

            // Clear previous error
            if (divError) divError.innerHTML = "";
            fileInput.style.borderColor = "";

            console.log("File input element:", fileInput);

            // Check if any file is selected
            if (!fileInput.files || fileInput.files.length === 0) {
                console.log("No file selected");
                fileInput.style.borderColor = "#F7627F";
                if (divError) divError.innerHTML = "Please select a file to upload.";
                return false;
            }

            var fileName = fileInput.files[0].name;
            console.log("Selected file name:", fileName);

            var fileExtension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
            console.log("File extension:", fileExtension);

            // Check if extension is allowed
            if (allowedExtensions.indexOf(fileExtension) === -1) {
                console.log("Invalid file extension");
                fileInput.style.borderColor = "#F7627F";
                if (divError) divError.innerHTML = "Invalid file type. Allowed: " + allowedExtensions.join(", ");
                return false;
            }

            // All good
            console.log("File valid");
            fileInput.style.borderColor = "";
            if (divError) divError.innerHTML = "";
            return true;
        }

        function RemarkValidateDom() {
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

        function ValidatefileUploadInvoiceInstructionTest() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById('<%=fileUploadInvoiceInstruction.ClientID %>');
            if (!fileInput.files || fileInput.files.length === 0) {
                console.log("No file selected");
                fileInput.style.borderColor = "#F7627F";

                return false;
            }

            var fileName = fileInput.files[0].name;
            console.log("Selected file name:", fileName);

            var fileExtension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
            console.log("File extension:", fileExtension);
            if (allowedExtensions.indexOf(fileExtension) === -1) {
                console.log("Invalid file extension");
                fileInput.style.borderColor = "#F7627F";
                return false;
            }
            console.log("File valid");
            fileInput.style.borderColor = "";
            return true;
        }



        function ValidateAllOutwardDomestic() {

            var check = true;

            if (ValidateJobDomestic()) {
                check = false;
            }

            if (ValidateVenNameD()) {
                check = false;
            }


            /*  ------------------------------------------------   */
            //if (ValidateLocD()) {
            //    check = false;
            //}

            //if (ValidateEmailD()) {
            //    check = false;
            //}

            //if (ValidateContactD()) {
            //    check = false;
            //}

            //if (ValidateVendorDelLocD()) {
            //    check = false;
            //}

            // if (ValidateVendorNamePickupD()) {
            //     check = false;
            // }
            ///// 
            // if (ValidateVendorAddressPickupD()) {
            //     check = false;
            // }
            // //
            //    if (ValidateContactDetailsPickupD()) {
            //     check = false;
            // }

            //if (ValidateIncotermsD()) {
            //    check = false;
            //}

            /*  ------------------------------------------------   */
            if (ValidateDeliverytermsD()) {
                check = false;
            }

            if (ValidateNoOfTruckD()) {
                check = false;
            }

            if (RemarkValidateDom()) {
                check = false;
            }

            if (ValidateMatPickLocOthers()) {
                check = false;
            }



            if (ValidateIsClientAddressOtherThanAboveChecked()) {
                check = false;
            }

            /*  ------------------------------------------------   */
            //   if (ValidateOutwardDateD()) {
            //    check = false;
            //}
            /*  ------------------------------------------------   */

            if (ValidateTypeConsignmentDateD()) {
                check = false;
            }

            if (ValidatePartLoadDetailsD()) {
                check = false;
            }

            if (!ValidatefileUploadPackingList()) {
                check = false;
            }

            if (!ValidatefileUploadInvoiceInstructionTest()) {
                check = false;
            }

            console.log(check);


            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById('<%=HiddenField1.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=HiddenField1.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

        }


    </script>


    <%--  //////////INTERNATOINAL OUTWARD VALIDATION START ////////////////--%>

    <script type="text/javascript">
        function ValidateJobInt() {
            var JobNo = document.getElementById("<%=txtJobNoInt.ClientID %>").value;
            if (JobNo == "") {
                document.getElementById("<%=txtJobNoInt.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=txtJobNoInt.ClientID %>").style.borderColor =
                    "";
                return false;
            }
        }

        function ValidateVenNameI() {
            var CustVendName = document.getElementById(
      "<%=txtVendorName.ClientID %>"
            ).value;
            if (CustVendName == "") {
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

        function ValidateLocI() {
            var PlaceOfVisit = document.getElementById(
      "<%=txtVendorLocation.ClientID %>"
            ).value;
            if (PlaceOfVisit == "") {
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

        function ValidateEmailI() {
            var emailField = document.getElementById("<%=txtVendorEmail.ClientID %>");
            var email = emailField.value.trim();

            // Regular expression for basic email validation
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (email === "" || !emailPattern.test(email)) {
                emailField.style.borderColor = "#F7627F";
                return false; // Invalid email
            } else {
                emailField.style.borderColor = "";
                return true; // Valid email
            }
        }

        function RemarkValidate() {
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

        function ValidateContactI() {
            var contactField = document.getElementById(
      "<%=txtVendorContactInt.ClientID %>"
            );
            var contact = contactField.value.trim();

            // Basic pattern: 10 digits, starts with 6-9 (common in India)
            var phonePattern = /^[6-9]\d{9}$/;

            if (contact === "" || !phonePattern.test(contact)) {
                contactField.style.borderColor = "#F7627F";
                return false; // Invalid phone number
            } else {
                contactField.style.borderColor = "";
                return true; // Valid phone number
            }
        }

        function ValidateVendorDelLocI() {
            var TourBasedOn = document.getElementById(
      "<%=ddlVenLoc.ClientID %>"
            ).selectedIndex;
            if (TourBasedOn == "") {
                document.getElementById("<%=ddlVenLoc.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=ddlVenLoc.ClientID %>").style.borderColor =
                    "";
                return false;
            }
        }

        function ValidateVendorNamePickupI() {
            var PlaceOfVisit = document.getElementById(
      "<%=txtVendorNameInt.ClientID %>"
            ).value;
            if (PlaceOfVisit == "") {
                document.getElementById(
        "<%=txtVendorNameInt.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorNameInt.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function ValidateVendorAddressPickupI() {
            var PlaceOfVisit = document.getElementById(
      "<%=txtVendorAddressInt.ClientID %>"
            ).value;
            if (PlaceOfVisit == "") {
                document.getElementById(
        "<%=txtVendorAddressInt.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtVendorAddressInt.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function ValidateContactDetailsPickupI() {
            var PlaceOfVisit = document.getElementById(
      "<%=txtContactDetailsVenPickup.ClientID %>"
            ).value;
            if (PlaceOfVisit == "") {
                document.getElementById(
        "<%=txtContactDetailsVenPickup.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true;
            } else {
                document.getElementById(
        "<%=txtContactDetailsVenPickup.ClientID %>"
                ).style.borderColor = "";
                return false;
            }
        }

        function ValidateIncotermsI() {
            var ModeOfTravel = document.getElementById(
      "<%=ddlInco2.ClientID %>"
            ).selectedIndex;
            if (ModeOfTravel == "" || ModeOfTravel == "0") {
                document.getElementById("<%=ddlInco2.ClientID %>").style.borderColor =
                    "#F7627F";
                return true;
            } else {
                document.getElementById("<%=ddlInco2.ClientID %>").style.borderColor = "";
                return false;
            }
        }

        function ValidateNoOfContainersI() {
            var truckInput = document.getElementById(
      "<%=txtIsNoOfContainersRequired.ClientID %>"
            );
            var value = truckInput.value.trim();

            if (value === "" || isNaN(value)) {
                truckInput.style.borderColor = "#F7627F"; // highlight invalid input
                return true; // validation failed
            } else {
                truckInput.style.borderColor = ""; // reset border color
                return false; // validation passed
            }
        }

        function ValidateTypeConsignmentDateI() {
            var TypeOfTrip = document.getElementById(
      "<%=ddlTypeConsignment.ClientID %>"
            ).selectedIndex;

            if (TypeOfTrip == "") {
                document.getElementById(
        "<%=ddlTypeConsignment.ClientID %>"
                ).style.borderColor = "#F7627F";
                return true; // Validation failed
            } else {
                document.getElementById(
        "<%=ddlTypeConsignment.ClientID %>"
                ).style.borderColor = "";
                return false; // Validation passed
            }
        }

        function validateContainerRepeater() {
            var textboxes = document.querySelectorAll("input.qty-input-container");
            var atLeastOneFilled = false;
            var allValid = true;

            for (var i = 0; i < textboxes.length; i++) {
                var textbox = textboxes[i];
                var value = textbox.value.trim();

                // Reset border
                textbox.style.border = "1px solid #ced4da";

                if (value != "") {
                    atLeastOneFilled = true;

                    // Allow only whole numbers (digits only)
                    if (!/^\d+$/.test(value)) {
                        textbox.style.border = "2px solid #F7627F";
                        allValid = false;
                    }
                }
            }

            if (!atLeastOneFilled || !allValid) {
                return false;
            }

            return true;
        }


        function ValidatePackingListInternational() {
            var allowedExtensions = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var fileInput = document.getElementById("<%=fileUpload1.ClientID %>");
            var divError = document.getElementById("divfileUploadVisitSummary1"); // You need this!

            // Clear previous error
            if (divError) divError.innerHTML = "";
            fileInput.style.borderColor = "";

            console.log("File input element:", fileInput);

            // Check if any file is selected
            if (!fileInput.files || fileInput.files.length === 0) {
                console.log("No file selected");
                fileInput.style.borderColor = "#F7627F";
                if (divError) divError.innerHTML = "Please select a file to upload.";
                return false;
            }

            var fileName = fileInput.files[0].name;
            console.log("Selected file name:", fileName);

            var fileExtension = fileName
                .substring(fileName.lastIndexOf("."))
                .toLowerCase();
            console.log("File extension:", fileExtension);

            // Check if extension is allowed
            if (allowedExtensions.indexOf(fileExtension) === -1) {
                console.log("Invalid file extension");
                fileInput.style.borderColor = "#F7627F";
                if (divError)
                    divError.innerHTML =
                        "Invalid file type. Allowed: " + allowedExtensions.join(", ");
                return false;
            }

            // All good
            console.log("File valid");
            fileInput.style.borderColor = "";
            if (divError) divError.innerHTML = "";
            return true;
        }


        function ValidateModeOfTransportI() {
            var dropdown = document.getElementById('<%=ddlModeOfTransport.ClientID %>');
            var selectedIndex = dropdown.selectedIndex;

            if (selectedIndex == 0) {
                dropdown.style.borderColor = "#F7627F";
                return true;
            } else {
                dropdown.style.borderColor = "";
                return false;
            }
        }


        function ValidateIsStuffingI() {
            var dropdown = document.getElementById('<%=ddlIsStuffing.ClientID %>');
            var selectedIndex = dropdown.selectedIndex;

            if (selectedIndex === 0) {
                dropdown.style.borderColor = "#F7627F";
                return true;
            } else {
                dropdown.style.borderColor = "";
                return false;
            }
        }

        function ValidateInvoiceInstructionInternational() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf"];
            var regex = new RegExp(
                "([a-zA-Z0-9\\s_\\.:\\-])+(" + allowedFiles.join("|") + ")$",
                "i"
            );

            var fileInput = document.getElementById("<%=fileUpload2.ClientID %>");
            var fileValue = fileInput.value.trim();

            var divError = document.getElementById("divfileUploadVisitSummary1"); // Make sure this div exists next to file input

            if (fileValue === "") {
                fileInput.style.borderColor = "#F7627F";
                //divError.style.display = "block";
                //divError.innerHTML = "Please upload a file.";
                return false; // prevent form submission
            } else if (!regex.test(fileValue)) {
                fileInput.style.borderColor = "#F7627F";
                //divError.style.display = "block";
                //divError.innerHTML = "Only .pdf, .jpg, .jpeg, .bmp, .png, .gif files are allowed!";
                return false; // prevent form submission
            } else {
                fileInput.style.borderColor = "";
                //divError.style.display = "none";
                //divError.innerHTML = "";
                return true; // validation passed
            }
        }

        function ValidateAllOutwardInt() {
            var check = true;

            if (ValidateJobInt()) {
                check = false;
            }

            if (ValidateVenNameI()) {
                check = false;
            }

            // if (ValidateLocI()) {
            //     check = false;
            // }

            // if (ValidateEmailI()) {
            //     check = false;
            // }

            // if (ValidateContactI()) {
            //     check = false;
            // }

            //if (ValidateVendorDelLocI()) {
            //  check = false;
            //}

            // if (ValidateVendorNamePickupI()) {
            //     check = false;
            // }
            // if (ValidateVendorAddressPickupI()) {
            //     check = false;
            // }
            // if (ValidateContactDetailsPickupI()) {
            //     check = false;
            // }

            if (ValidateIncotermsI()) {
                check = false;
            }

            if (ValidateTypeConsignmentDateI()) {
                check = false;
            }

            if (!ValidatePackingListInternational()) {
                check = false;
                console.log(check);
            }

            //if (ValidateInvoiceInstructionInternational()) {
            //  check = false;
            //}

            if (ValidateModeOfTransportI()) {
                check = false;
            }

            if (ValidateIsStuffingI()) {
                check = false;
            }

            if (!validateContainerRepeater()) {
                check = false;
            }

            if (RemarkValidate()) {
                check = false;
            }

            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById("<%=HiddenField1.ClientID %>").value = "1";
                    return true;
                } else {
                    document.getElementById("<%=HiddenField1.ClientID %>").value = "0";
                    return false;
                }
            } else {
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
                    <asp:ListItem Text="Domestic Outward" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="International Outward" Value="2"></asp:ListItem>
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
                    <legend>Domestic Outward</legend>

                    <div class="form-grid form-grid-2">

                        <label>JOB Number:</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtJOBNo" runat="server"
                                        CssClass="form-control"
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

                        <label>Client's Name:</label>
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


                        <label>Client Location/Address:</label>
                        <asp:TextBox ID="txtVenLoc2" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Client's Email:</label>
                        <asp:TextBox ID="txtVenEmail2" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Client's Contact Details for unloading:</label>
                        <asp:TextBox ID="txtVenContact" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                         <label>Is it Client Scoped:</label>
                        <asp:CheckBox ID="chkscope" runat="server" Checked="false" Enabled="true" />

                        <label>Material Pick Up Location:</label>
                        <asp:DropDownList ID="ddlDeliveryLoc" runat="server"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlMaterialPickUpLocation_SelectedIndexChanged">
                        </asp:DropDownList>

                       
                        <label>Other Pickup Location:</label>
                        <asp:TextBox ID="txtothersPickupLocD" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor Name For Pickup(in case of pickup from Vendor's side):</label>
                        <asp:TextBox ID="txtVendornameVenPickup" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor Address:</label>
                        <asp:TextBox ID="txtVendAddressVenPickup" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor Contact Details:</label>
                        <asp:TextBox ID="txtVenContactDetailsPickup" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Delivery Term:</label>
                        <asp:DropDownList ID="ddlDeliveryTerm2" runat="server"
                            CssClass="form-control">
                        </asp:DropDownList>

                        <label>Is Client's Delivery Address other than above :</label>
                        <asp:CheckBox ID="chkMatPickupLocSameAsAbove" runat="server" Checked="false" Enabled="true" onchange="EnableMatPickupLoc()" />


                        <div class="full-width">
                            <label>Enter Client's Delivery Address & Concerned Person Contact Details:</label>
                            <asp:TextBox ID="txtMatPickupLoc" runat="server"
                                CssClass="form-control"
                                Enabled="false" TextMode="MultiLine" colspan="2"
                                Rows="2" />
                        </div>

                        <label>No. Of Trucks Required:</label>
                        <asp:TextBox ID="txtIsNoOfTrucksReq" runat="server"
                            CssClass="form-control"
                            Enabled="false" />
                        <asp:HiddenField ID="hdnNoOfTruck" runat="server" />

                        <label>Readiness Date Of Pickup:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdDate" runat="server" />
                                    <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate"
                                        runat="server" TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChanged">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Date of Logging Lesson Learnt" />
                                </td>
                            </tr>
                        </table>

                        <label>Type Of Consignment:</label>
                        <asp:DropDownList ID="ddlTypeOfConsignment2" runat="server" CssClass="form-control"
                            AutoPostBack="true">
                        </asp:DropDownList>


                        <label>Part Load Details:</label>
                        <asp:DropDownList ID="ddlPartLoadDetails" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Packing List:</label>
                        <asp:FileUpload ID="fileUploadPackingList" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatefileUploadPackingList();" />

                        <label>Invoice Instruction:</label>
                        <asp:FileUpload ID="fileUploadInvoiceInstruction" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" />

                        <div class="full-width">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksDomestic" runat="server"
                                CssClass="form-control"
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
                                                <td>
                                                    <asp:Label ID="lblTruck" runat="server" Text='<%# Eval("TRUCK_TYPE_NAME") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfTruckType" runat="server" Value='<%# Eval("TRUCK_TYPE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="qty-input-truck form-control"
                                                        oninput="updateTruckTotal()"></asp:TextBox>
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
                    <asp:Button ID="btnSaveDomesticOutward" CssClass="button" runat="server" Text="Save"
                        OnClientClick="return ValidateAllOutwardDomestic();" OnClick="btnSubmit_Click" Width="100%"
                        Style="margin-right: 20px;" />

                    <asp:Button ID="btnViewList" CssClass="button" runat="server" Text="View List"
                        Width="100%" />
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
                    <legend>International Outward</legend>

                    <div class="form-grid form-grid-2">

                        <label>Project Number:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtJobNoInt" runat="server"
                                        CssClass="form-control"
                                        Enabled="false"
                                        onblur="return ValidateJobInt();" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="Button3" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetJOBNo_Click" />

                                </td>

                            </tr>
                        </table>

                        <label>Client's Name:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtVendorName" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="btnInterVendor" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetDomestic_Click" />

                                </td>
                            </tr>
                        </table>


                        <label>Client's Location/Address:</label>
                        <asp:TextBox ID="txtVendorLocation" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Client's Email:</label>
                        <asp:TextBox ID="txtVendorEmail" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Client Contact Details for unloading:</label>
                        <asp:TextBox ID="txtVendorContactInt" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Material Pickup Location:</label>
                        <asp:DropDownList ID="ddlVenLoc" runat="server"
                            CssClass="form-control"
                            OnSelectedIndexChanged="ddlMaterialPickUpLocation_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>

                        <label>Other Pickup Location:</label>
                        <asp:TextBox ID="txtOthersInt" runat="server"
                            CssClass="form-control"
                            Enabled="false" />



                        <label>Vendor Name For Pickup(in case of pickup from Vendor's side):</label>
                        <asp:TextBox ID="txtVendorNameInt" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Vendor Address:</label>
                        <asp:TextBox ID="txtVendorAddressInt" runat="server"
                            CssClass="form-control"
                            Enabled="false" />


                        <label>Vendor Contact Details:</label>
                        <asp:TextBox ID="txtContactDetailsVenPickup" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Is Client's Delivery Address other than above :</label>
                        <asp:CheckBox ID="chkClientDelAddress" runat="server" Checked="false" Enabled="true" onchange="EnableMatPickupLocInt()" />


                        <div class="full-width">
                            <label>Enter Client's Delivery Address & Concerned Person Contact Details:</label>
                            <asp:TextBox ID="txtClientAddress" runat="server"
                                CssClass="form-control"
                                Enabled="false" TextMode="MultiLine" colspan="2"
                                Rows="2" />
                        </div>

                        <label>Incoterms:</label>
                        <asp:DropDownList ID="ddlInco2" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Mode Of Transport:</label>
                        <asp:DropDownList ID="ddlModeOfTransport" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>Container Stuffing Possible:</label>
                        <asp:DropDownList ID="ddlIsStuffing" runat="server" CssClass="form-control">
                        </asp:DropDownList>

                        <label>No. Of Containers Required:</label>
                        <asp:TextBox ID="txtIsNoOfContainersRequired" runat="server"
                            CssClass="form-control"
                            Enabled="false" />
                        <asp:HiddenField ID="hdnTotalContainers" runat="server" />

                        <label>No. Of Trucks Required:</label>
                        <asp:TextBox ID="txtNoOfTrucks" runat="server" CssClass="form-control"
                            Enabled="true" />

                        <label>Readiness Date Of Pickup:</label>
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
                            CssClass="form-control"
                            onblur="return ValidateModeOfTravel();"
                            onchange="EnableModeOfTravel()">
                        </asp:DropDownList>

                        <label>Packing List:</label>
                        <asp:FileUpload ID="fileUpload1" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" onblur="return ValidatePackingListInternational();" />

                        <label>Invoice Instruction:</label>
                        <asp:FileUpload ID="fileUpload2" runat="server" Enabled="true"
                            CssClass="form-control"
                            BorderStyle="Groove" />

                        <div class="full-width">
                            <label>Remarks:</label>
                            <asp:TextBox ID="txtRemarksInternational" runat="server"
                                CssClass="form-control"
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
                                                <td>
                                                    <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("CONTAINER_TYPE_NAME") %>'></asp:Label>
                                                    <asp:HiddenField ID="hfContainerType" runat="server" Value='<%# Eval("CONTAINER_TYPE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQtyContainer" runat="server" CssClass="qty-input-container form-control"
                                                        oninput="updateContainerTotal()"></asp:TextBox>
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
                        Width="100%" OnClick="btnSubmit_Click"
                        OnClientClick="return ValidateAllOutwardInt();" />

                    <asp:Button ID="btnTourList" CssClass="button" runat="server" Text="View List"
                        Width="100%" />
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

                                     <asp:Label ID="lblAddress12" runat="server" Visible="false" Text='<%# Eval("ADDRESS1") %>' />
                                     <asp:Label ID="lblAddress22" runat="server" Visible="false" Text='<%# Eval("ADDRESS2") %>' /> 
                                     <asp:Label ID="lblAddress2" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />
                                     <asp:Label ID="lblCity2" runat="server" Visible="false" Text='<%# Eval("CITY") %>' />
                                     <asp:Label ID="lblState2" runat="server" Visible="false" Text='<%# Eval("STATE") %>' />
                                    <asp:Label ID="lblCountry2" runat="server" Visible="false" Text='<%# Eval("COUNTRY") %>' />
                                    <asp:Label ID="lblPinCode2" runat="server" Visible="false" Text='<%# Eval("PINCODE") %>' />
                                    <asp:Label ID="lblEmail2" runat="server" Visible="false" Text='<%# Eval("EMAIL") %>' />
                                    <asp:Label ID="lblPhone2" runat="server" Visible="false" Text='<%# Eval("PHONE") %>' />

                                    <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                        runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                            <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                            <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                           <asp:BoundField DataField="ADDRESS1" HeaderText="ADDRESS1" />
                            <asp:BoundField DataField="ADDRESS2" HeaderText="ADDRESS2" />
                            <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                            <asp:BoundField DataField="CITY" HeaderText="CITY" />
                            <asp:BoundField DataField="STATE" HeaderText="STATE" />
                             <asp:BoundField DataField="COUNTRY" HeaderText="COUNTRY" />
                            <asp:BoundField DataField="PINCODE" HeaderText="PINCODE" />
                            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                            <asp:BoundField DataField="PHONE" HeaderText="PHONE" />
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


