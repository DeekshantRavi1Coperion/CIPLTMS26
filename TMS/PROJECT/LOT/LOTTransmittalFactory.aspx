<%@ Page Title="CIPLTMS-Transmittal To Factory" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="LOTTransmittalFactory.aspx.cs" Inherits="PROJECT_LOT_LOTTransmittalFactory" %>


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

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">

        function pageLoad() {
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
        }

        function clientChangedDate(sender, args) {
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
        }

    </script>

    <script type="text/javascript">

        function ValidateCustName() {
            var CustName = document.getElementById('<%=txtCustomerName.ClientID %>').value;
            if (CustName == '') {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCustCode() {
            var CustCode = document.getElementById('<%=txtCustomerCode.ClientID %>').value;
            if (CustCode == '') {
                document.getElementById('<%=txtCustomerCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }

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

        function ValidateTFNo() {
            var TFNo = document.getElementById('<%=txtTFNo.ClientID %>').value;
            if (TFNo == '') {
                document.getElementById('<%=txtTFNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTFNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePONo() {
            var PONo = document.getElementById('<%=txtPONo.ClientID %>').value;
            if (PONo == '') {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "";
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

    </script>



    <script type="text/javascript">

        function ValidateJobNoForProductionNo() {
            var JOBNo = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (JOBNo == '') {
                alert("Please close window and enter job number first...!!!");
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateProductionOrderNoAll() {
            var check = true;

            if (ValidateJobNoForProductionNo()) {
                check = false;
            }

            return check;
        }

    </script>

    <script type="text/javascript">

        function ValidatefileAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment1 = document.getElementById('<%=uploadFileAttachment1.ClientID %>').value;
            var divfileAttachment1 = document.getElementById("divfileAttachment1");
            var lblfileAttachment1 = document.getElementById('<%=lblfileAttachment1.ClientID %>');


            if (fileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                divfileAttachment1.style.display = "none";
                lblfileAttachment1.innerHTML = "";
                return false;
            }
            else {

                fileAttachment1 = fileAttachment1.split(" ").join("")
                fileAttachment1 = fileAttachment1.split("(").join("")
                fileAttachment1 = fileAttachment1.split(")").join("")

                if (!regex.test(fileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment1.style.display = "block";
                    lblfileAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                    divfileAttachment1.style.display = "none";
                    lblfileAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment2 = document.getElementById('<%=uploadFileAttachment2.ClientID %>').value;
            var divfileAttachment2 = document.getElementById("divfileAttachment2");
            var lblfileAttachment2 = document.getElementById('<%=lblfileAttachment2.ClientID %>');


            if (fileAttachment2 == '') {
                document.getElementById('<%=uploadFileAttachment2.ClientID %>').style.borderColor = "";
                divfileAttachment2.style.display = "none";
                lblfileAttachment2.innerHTML = "";
                return false;
            }
            else {

                fileAttachment2 = fileAttachment2.split(" ").join("")
                fileAttachment2 = fileAttachment2.split("(").join("")
                fileAttachment2 = fileAttachment2.split(")").join("")

                if (!regex.test(fileAttachment2.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment2.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment2.style.display = "block";
                    lblfileAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment2.ClientID %>').style.borderColor = "";
                    divfileAttachment2.style.display = "none";
                    lblfileAttachment2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment3 = document.getElementById('<%=uploadFileAttachment3.ClientID %>').value;
            var divfileAttachment3 = document.getElementById("divfileAttachment3");
            var lblfileAttachment3 = document.getElementById('<%=lblfileAttachment3.ClientID %>');

            if (fileAttachment3 == '') {
                document.getElementById('<%=uploadFileAttachment3.ClientID %>').style.borderColor = "";
                divfileAttachment3.style.display = "none";
                lblfileAttachment3.innerHTML = "";
                return false;
            }
            else {

                fileAttachment3 = fileAttachment3.split(" ").join("")
                fileAttachment3 = fileAttachment3.split("(").join("")
                fileAttachment3 = fileAttachment3.split(")").join("")

                if (!regex.test(fileAttachment3.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment3.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment3.style.display = "block";
                    lblfileAttachment3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment3.ClientID %>').style.borderColor = "";
                    divfileAttachment3.style.display = "none";
                    lblfileAttachment3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidatefileAttachment4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment4 = document.getElementById('<%=uploadFileAttachment4.ClientID %>').value;
            var divfileAttachment4 = document.getElementById("divfileAttachment4");
            var lblfileAttachment4 = document.getElementById('<%=lblfileAttachment4.ClientID %>');

            if (fileAttachment4 == '') {
                document.getElementById('<%=uploadFileAttachment4.ClientID %>').style.borderColor = "";
                divfileAttachment4.style.display = "none";
                lblfileAttachment4.innerHTML = "";
                return false;
            }
            else {

                fileAttachment4 = fileAttachment4.split(" ").join("")
                fileAttachment4 = fileAttachment4.split("(").join("")
                fileAttachment4 = fileAttachment4.split(")").join("")

                if (!regex.test(fileAttachment4.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment4.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment4.style.display = "block";
                    lblfileAttachment4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment4.ClientID %>').style.borderColor = "";
                    divfileAttachment4.style.display = "none";
                    lblfileAttachment4.innerHTML = "";
                    return false;
                }
            }
        }
    </script>


    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateCustName()) {
                check = false;
            }

            if (ValidateCustCode()) {
                check = false;
            }

            if (ValidateJobNo()) {
                check = false;
            }


            if (ValidateTFNo()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateItemName()) {
                check = false;
            }



            if (check) {
                if (confirm("Would you like to save LOT?")) {
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

            if (ValidateCustName()) {
                check = false;
            }

            if (ValidateCustCode()) {
                check = false;
            }

            if (ValidateJobNo()) {
                check = false;
            }


            if (ValidateTFNo()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateItemName()) {
                check = false;
            }



            if (check) {
                if (confirm("Would you like to preview LOT?")) {
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


        function ValidateJobNoForFT() {
            var check = true;

            if (ValidateJobNo()) {
                check = false;
            }

            return check;
        }


        function ValidateAllAttachments() {
            var check = true;

            if (ValidatefileAttachment1()) {
                check = false;
            }

            if (ValidatefileAttachment2()) {
                check = false;
            }

            if (ValidatefileAttachment3()) {
                check = false;
            }

            if (ValidatefileAttachment4()) {
                check = false;
            }

            return check;
        }

    </script>



    <script type="text/javascript">

        function ValidateProductionOrderNo() {
            var ProductionOrderNo = document.getElementById('<%=txtProductionOrderNo.ClientID %>').value;
            if (ProductionOrderNo == '') {
                document.getElementById('<%=txtProductionOrderNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductionOrderNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateProductionOrderDate() {
            var ProductionOrderDate = document.getElementById('<%=txtProductionOrderDate.ClientID %>').value;
            if (ProductionOrderDate == '') {
                document.getElementById('<%=txtProductionOrderDate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductionOrderDate.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProductCode() {
            var ProductCode = document.getElementById('<%=ddlProductCode.ClientID %>').selectedIndex;
            if (ProductCode == '' || ProductCode == 0) {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProductCode.ClientID %>').style.borderColor = "";
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

        function ValidateProductDesc() {
            var ProductDesc = document.getElementById('<%=txtProductDesc.ClientID %>').value;
            if (ProductDesc == '') {
                document.getElementById('<%=txtProductDesc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductDesc.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateExpectedCompletionDate() {
            var ExpectedCompletionDate = document.getElementById('<%=txtExpectedCompletionDate.ClientID %>').value;
            if (ExpectedCompletionDate == '') {
                document.getElementById('<%=txtExpectedCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtExpectedCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLOTMainItems() {
            var LOTMainItems = document.getElementById('<%=ddlLOTMainItems.ClientID %>').selectedIndex;
            if (LOTMainItems == '' || LOTMainItems == 0) {
                document.getElementById('<%=ddlLOTMainItems.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLOTMainItems.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLOTMainSubItems() {
            var LOTMainSubItems = document.getElementById('<%=ddlLOTMainSubItems.ClientID %>').selectedIndex;
            if (LOTMainSubItems == '' || LOTMainSubItems == 0) {
                document.getElementById('<%=ddlLOTMainSubItems.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlLOTMainSubItems.ClientID %>').style.borderColor = "";
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

        function ValidateDrgNo() {
            var DrgNo = document.getElementById('<%=txtDrgNo.ClientID %>').value;
            if (DrgNo == '') {
                document.getElementById('<%=txtDrgNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrgNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevNoText() {
            var RevNo = document.getElementById('<%=txtRevNoText.ClientID %>').value;
            if (RevNo == '') {
                document.getElementById('<%=txtRevNoText.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRevNoText.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCategory() {
            var Category = document.getElementById('<%=chkLstCategory.ClientID %>');
            var chkBox = Category.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < chkBox.length; i++) {
                if (chkBox[i].checked) {
                    counter++;
                }
            }

            if (counter == 0) {
                alert("Please select atleast one category...!!");
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateQuantity() {
            var IsPartOfProduction = document.getElementById('<%=chkIsPartOfProductionOrMainDrawing.ClientID %>').checked;
            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value;

            if (IsPartOfProduction == true) {
                if (Quantity == '' || parseInt(Quantity) == 0) {
                    document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtQuantity.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateFileSiDrawing1() {

            var SiDrawing1 = 0;
            var UploadSiDrawing1 = 0;

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawing1 = document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').value;
            var divFileSiDrawing1 = document.getElementById("divFileSiDrawing1");
            var lblFileSiDrawing1 = document.getElementById('<%=lblFileSiDrawing1.ClientID %>');



            if (document.getElementById('<%=hdSiDrawing1.ClientID %>') != null) {
                SiDrawing1 = document.getElementById('<%=hdSiDrawing1.ClientID %>').value;
            }
            else {
                SiDrawing1 = 0;
            }

            if (document.getElementById('<%=hdUploadSiDrawing1.ClientID %>') != null) {
                UploadSiDrawing1 = document.getElementById('<%=hdUploadSiDrawing1.ClientID %>').value;
            }
            else {
                UploadSiDrawing1 = 0;
            }


            var subitemUpdationFlag = document.getElementById('<%=hdSubitemUpdationFlag.ClientID %>').value;

            if (parseInt(subitemUpdationFlag) == 0) {
                if (FileSiDrawing1 == '') {
                    document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawing1.style.display = "none";
                    lblFileSiDrawing1.innerHTML = "";
                    return true;
                }
                else {

                    FileSiDrawing1 = FileSiDrawing1.split(" ").join("")
                    FileSiDrawing1 = FileSiDrawing1.split("(").join("")
                    FileSiDrawing1 = FileSiDrawing1.split(")").join("")

                    if (!regex.test(FileSiDrawing1.toLowerCase())) {
                        document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "#F7627F";
                        divFileSiDrawing1.style.display = "block";
                        lblFileSiDrawing1.innerHTML = "Please enter only .pdf file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "";
                        divFileSiDrawing1.style.display = "none";
                        lblFileSiDrawing1.innerHTML = "";
                        return false;
                    }
                }
            }
            else {

                if (parseInt(SiDrawing1) == 0 && parseInt(UploadSiDrawing1) == 1) {
                    if (FileSiDrawing1 == '') {
                        document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "#F7627F";
                        divFileSiDrawing1.style.display = "none";
                        lblFileSiDrawing1.innerHTML = "";
                        return true;
                    }
                    else {

                        FileSiDrawing1 = FileSiDrawing1.split(" ").join("")
                        FileSiDrawing1 = FileSiDrawing1.split("(").join("")
                        FileSiDrawing1 = FileSiDrawing1.split(")").join("")

                        if (!regex.test(FileSiDrawing1.toLowerCase())) {
                            document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "#F7627F";
                            divFileSiDrawing1.style.display = "block";
                            lblFileSiDrawing1.innerHTML = "Please enter only .pdf file!";
                            return true;
                        }
                        else {
                            document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "";
                            divFileSiDrawing1.style.display = "none";
                            lblFileSiDrawing1.innerHTML = "";
                            return false;
                        }
                    }
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawing1.ClientID %>').style.borderColor = "";
                    divFileSiDrawing1.style.display = "none";
                    lblFileSiDrawing1.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing2() {
            var allowedFiles = [".dxf", ".dwg", ".DXF", ".DWG"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawing2 = document.getElementById('<%=uploadFileSiDrawing2.ClientID %>').value;
            var divFileSiDrawing2 = document.getElementById("divFileSiDrawing2");
            var lblFileSiDrawing2 = document.getElementById('<%=lblFileSiDrawing2.ClientID %>');




            if (FileSiDrawing2 == '') {
                document.getElementById('<%=uploadFileSiDrawing2.ClientID %>').style.borderColor = "";
                divFileSiDrawing2.style.display = "none";
                lblFileSiDrawing2.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing2 = FileSiDrawing2.split(" ").join("")
                FileSiDrawing2 = FileSiDrawing2.split("(").join("")
                FileSiDrawing2 = FileSiDrawing2.split(")").join("")

                if (!regex.test(FileSiDrawing2.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawing2.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawing2.style.display = "block";
                    lblFileSiDrawing2.innerHTML = "Please enter only .dxf or .dwg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawing2.ClientID %>').style.borderColor = "";
                    divFileSiDrawing2.style.display = "none";
                    lblFileSiDrawing2.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawing3 = document.getElementById('<%=uploadFileSiDrawing3.ClientID %>').value;
            var divFileSiDrawing3 = document.getElementById("divFileSiDrawing3");
            var lblFileSiDrawing3 = document.getElementById('<%=lblFileSiDrawing3.ClientID %>');


            if (FileSiDrawing3 == '') {
                document.getElementById('<%=uploadFileSiDrawing3.ClientID %>').style.borderColor = "";
                divFileSiDrawing3.style.display = "none";
                lblFileSiDrawing3.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing3 = FileSiDrawing3.split(" ").join("")
                FileSiDrawing3 = FileSiDrawing3.split("(").join("")
                FileSiDrawing3 = FileSiDrawing3.split(")").join("")

                if (!regex.test(FileSiDrawing3.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawing3.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawing3.style.display = "block";
                    lblFileSiDrawing3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawing3.ClientID %>').style.borderColor = "";
                    divFileSiDrawing3.style.display = "none";
                    lblFileSiDrawing3.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateFileSiDrawing4() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var FileSiDrawing4 = document.getElementById('<%=uploadFileSiDrawing4.ClientID %>').value;
            var divFileSiDrawing4 = document.getElementById("divFileSiDrawing4");
            var lblFileSiDrawing4 = document.getElementById('<%=lblFileSiDrawing4.ClientID %>');


            if (FileSiDrawing4 == '') {
                document.getElementById('<%=uploadFileSiDrawing4.ClientID %>').style.borderColor = "";
                divFileSiDrawing4.style.display = "none";
                lblFileSiDrawing4.innerHTML = "";
                return false;
            }
            else {

                FileSiDrawing4 = FileSiDrawing4.split(" ").join("")
                FileSiDrawing4 = FileSiDrawing4.split("(").join("")
                FileSiDrawing4 = FileSiDrawing4.split(")").join("")

                if (!regex.test(FileSiDrawing4.toLowerCase())) {
                    document.getElementById('<%=uploadFileSiDrawing4.ClientID %>').style.borderColor = "#F7627F";
                    divFileSiDrawing4.style.display = "block";
                    lblFileSiDrawing4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileSiDrawing4.ClientID %>').style.borderColor = "";
                    divFileSiDrawing4.style.display = "none";
                    lblFileSiDrawing4.innerHTML = "";
                    return false;
                }
            }
        }

        function ValidateTagNo() {
            var TagNo = document.getElementById('<%=txtTagNo.ClientID %>').value;

            if (TagNo == '') {
                document.getElementById('<%=txtTagNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTagNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAddSubitem() {
            var check = true;
            var subitemUpdationFlag = document.getElementById('<%=hdSubitemUpdationFlag.ClientID %>').value;


            if (ValidateProductionOrderNo()) {
                check = false;
            }


            if (ValidateProductionOrderDate()) {
                check = false;
            }

            if (ValidateProductCode()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }

            if (ValidateProductDesc()) {
                check = false;
            }


            if (ValidateExpectedCompletionDate()) {
                check = false;
            }

            if (ValidateLOTMainItems()) {
                check = false;
            }

            if (ValidateLOTMainSubItems()) {
                check = false;
            }

            if (ValidateDescription()) {
                check = false;
            }

            if (ValidateDrgNo()) {
                check = false;
            }


            if (ValidateRevNoText()) {
                check = false;
            }

            if (ValidateCategory()) {
                check = false;
            }

            if (ValidateQuantity()) {
                check = false;
            }


            if (parseInt(subitemUpdationFlag) == 0) {

                if (ValidateFileSiDrawing1()) {
                    check = false;
                }

                if (ValidateFileSiDrawing2()) {
                    check = false;
                }

                if (ValidateFileSiDrawing3()) {
                    check = false;
                }

                if (ValidateFileSiDrawing4()) {
                    check = false;
                }
            }

            if (ValidateTagNo()) {
                check = false;
            }


            <%--if (check) {
                var msg;

                if (parseInt(subitemUpdationFlag) == 0) {
                    msg = "Would you like to add subitem?";
                }
                else {
                    msg = "Would you like to update subitem?";
                }

                if (confirm(msg)) {
                    document.getElementById('<%=hdSubitemConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdSubitemConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }--%>

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



    <%--  <script type="text/javascript">
        var GridId = "<%=gvSubItem.ClientID %>";
        var ScrollHeight = 210;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>--%>


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
    </style>


    <script type="text/javascript">

        function EnableRevisionNoTextboxOther() {
            var hdRevNoText = document.getElementById('<%=hdRevNoText.ClientID %>').value;
            var hdRevNoTextOld = document.getElementById('<%=hdRevNoTextOld.ClientID %>').value;

            var RevNo = document.getElementById('<%=ddlRevNo.ClientID %>');
            var RevNoIndex = document.getElementById('<%=ddlRevNo.ClientID %>').selectedIndex;
            var RevNoText = RevNo.options[RevNo.selectedIndex].innerHTML;

            if (RevNoIndex == 11) {
                document.getElementById('<%=txtRevNoText.ClientID %>').disabled = false;

                document.getElementById('<%=txtRevNoText.ClientID %>').value = hdRevNoTextOld;
            }
            else {
                document.getElementById('<%=txtRevNoText.ClientID %>').disabled = true;

                document.getElementById('<%=txtRevNoText.ClientID %>').value = RevNoText;
                document.getElementById('<%=hdRevNoText.ClientID %>').value = RevNoText;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />

    <div class="form-entry-container">
        <fieldset class="form-card">
            <legend>Add LOT [Transmittal To Factory]</legend>

            <div class="form-grid form-grid-2">

                <label>LOT Type</label>
                <asp:DropDownList ID="ddlLOTType" runat="server" Width="100%" Height="26px"
                    OnSelectedIndexChanged="ddlLOTType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="New" Value="1" />
                    <asp:ListItem Text="Revision" Value="2" />
                    <asp:ListItem Text="Warranty" Value="3" />
                </asp:DropDownList>

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
                                Enabled="false"
                                CssClass="form-control" />
                        </td>

                        <td style="width: 10%">
                            <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                OnClick="btnGetJOBNo_Click" />

                            <asp:Button ID="btnAddApprover" runat="server" Width="100%" Text="Add Approver" CssClass="button"
                                OnClick="btnAddApprover_Click" Visible="false" />
                        </td>
                    </tr>
                </table>


                <label>Customer PO Number</label>
                <asp:TextBox ID="txtPONo" runat="server"
                    CssClass="form-control" />

                <div class="full-width">
                    <label>Customer Name</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustomerName" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCode" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                </div>

                <label>Date</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" onkeyDown="javascript:preventInput(event);"
                                CssClass="form-control"></asp:TextBox>
                            <asp:HiddenField ID="hdDate" runat="server" />
                            <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate"
                                runat="server"
                                TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedDate">
                            </ajax:CalendarExtender>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Calendar" />
                        </td>
                    </tr>
                </table>

                <label>TF Number</label>
                <table width="100%">
                    <tr>
                        <td style="width: 80%">
                            <asp:TextBox ID="txtTFNo" runat="server"
                                Enabled="false"
                                CssClass="form-control" />
                        </td>
                        <td style="width: 20%">
                            <asp:Button ID="btnGetTFno" runat="server" Width="100%" Text="Get" CssClass="button"
                                OnClientClick="return ValidateJobNoForFT();" OnClick="btnGetTFno_Click" />
                        </td>
                    </tr>
                </table>

                <div class="full-width">
                    <label>Item</label>
                    <asp:TextBox ID="txtItemName" runat="server"
                        CssClass="form-control" />
                </div>

                <asp:Button ID="btnAddSubitem" runat="server"
                    Text="Add Subitem" CssClass="button" OnClick="btnAddSubitem_Click" />

                <%--ADD SUBITEM START--%>
                <%--ADD SUBITEM END--%>


                <div class="full-width">

                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>
                                <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" />
                            </legend>

                            <asp:GridView
                                CssClass="employee-grid"
                                ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                OnRowCommand="gvSubItem_RowCommand" OnRowDataBound="gvSubItem_RowDataBound">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />


                                            <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                            <asp:Label ID="lblProductionOrderDate" runat="server" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' Visible="false" />
                                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />
                                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("PRODUCT_DESC") %>' Visible="false" />
                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />
                                            <%--<asp:Label ID="lblIsPartOfProductionStatusReport" runat="server" Text='<%# Eval("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID") %>' Visible="false" />--%>

                                            <asp:Label ID="lblLOTMainItemID" runat="server" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' Visible="false" />
                                            <asp:Label ID="lblLOTMainSubitemID" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' Visible="false" />
                                            <asp:Label ID="lblLOTFor" runat="server" Text='<%# Eval("LOT_MAIN_ITEM") %>' Visible="false" />
                                            <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("TAG_NO") %>' Visible="false" />
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("SUBITEM_DESC") %>' Visible="false" />
                                            <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRG_NO") %>' Visible="false" />
                                            <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REV_NO") %>' Visible="false" />
                                            <asp:Label ID="lblRevNoText" runat="server" Text='<%# Eval("REV_NO_TEXT") %>' Visible="false" />


                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="false" />
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' Visible="false" />

                                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove" ImageUrl="~/Images/Icons/REMOVE03.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                                    <asp:TemplateField HeaderText="Tag No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTagNoInList" runat="server" Text='<%# Eval("TAG_NO") %>' Style="text-transform: uppercase" Width="150PX"
                                                CssClass="textboxtagno" MaxLength="15"></asp:TextBox>
                                            <%--onkeyDown="javascript:preventInput(event);"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />
                                    <asp:BoundField DataField="DRG_NO" HeaderText="Drg/DOC.No" />

                                    <asp:TemplateField HeaderText="Rev.No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REV_NO_TEXT") %>' Width="40PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="CATEGORY" HeaderText="Category" />

                                    <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No." />
                                    <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="Production Order Date" />
                                    <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Completion Required By" />
                                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                                    <asp:BoundField DataField="PRODUCT_DESC" HeaderText="Product Desc" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="40PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:BoundField DataField="IS_PART_OF_PRODUCTION_STATUS_REPORT" HeaderText="Is Part Of Prod. Status Report" />--%>

                                    <asp:TemplateField HeaderText="Is Part Of Production Status Report/Main Drawing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsPartOfProductionStatusReportID" runat="server" Text='<%# Eval("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID") %>' Visible="false" />
                                            <asp:CheckBox ID="chkIsPartOfProductionStatusReport" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="SI_ATTACHMENT1_NAME" HeaderText="Drawing (.pdf)" />
                                    <asp:BoundField DataField="SI_ATTACHMENT2_NAME" HeaderText="Drawing2 (.dwg/.dxf)" />
                                    <asp:BoundField DataField="SI_ATTACHMENT3_NAME" HeaderText="Drawing3" Visible="false" />
                                    <asp:BoundField DataField="SI_ATTACHMENT4_NAME" HeaderText="Drawing4" Visible="false" />

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
                </div>

                <div class="full-width">
                    <label>Important Notes</label>
                    <asp:TextBox ID="txtNotes" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />
                </div>

                <label>Attachments</label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="uploadFileAttachment1" runat="server"
                                CssClass="form-control"
                                BorderStyle="Groove" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divfileAttachment1" style="display: none;">
                                <asp:Label ID="lblfileAttachment1" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                </table>

                <asp:UpdatePanel runat="server" ID="UpdatePanel1" Visible="false">
                    <ContentTemplate>
                        <table style="width: 900px">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAddUpdateAttachments" runat="server" Width="25%" Text="Add Additional Attachments"
                                        CssClass="button" OnClick="btnAddUpdateAttachments_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>


                        <%--ADD ATTACHMENTS START--%>
                        <%--ADD ATTACHMENTS END--%>


                        <div class="full-width">
                            <div class="employee-grid-container">
                                <fieldset class="filter-card">
                                    <asp:GridView
                                        CssClass="employee-grid"
                                        ID="gvAttachments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                        OnRowCommand="gvAttachments_RowCommand" OnRowDataBound="gvAttachments_RowDataBound">
                                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("ATTACHMENT1_NAME") %>' Visible="false" />
                                                    <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("ATTACHMENT2_NAME") %>' Visible="false" />
                                                    <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("ATTACHMENT3_NAME") %>' Visible="false" />
                                                    <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("ATTACHMENT4_NAME") %>' Visible="false" />

                                                    <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove" ImageUrl="~/Images/Icons/REMOVE03.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="ATTACHMENT1_NAME" HeaderText="Attachment1" />
                                            <asp:BoundField DataField="ATTACHMENT2_NAME" HeaderText="Attachment2" />
                                            <asp:BoundField DataField="ATTACHMENT3_NAME" HeaderText="Attachment3" />
                                            <asp:BoundField DataField="ATTACHMENT4_NAME" HeaderText="Attachment4" />
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
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnAddUpdateAttachmentsToList" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnPreview" runat="server" Width="100%" Text="Preview" CssClass="button"
                OnClick="btnPreview_Click" OnClientClick="return ValidateAllForPreview();" />

            <asp:RadioButtonList ID="rdSavingType" runat="server" RepeatDirection="Horizontal" Width="100%">
                <asp:ListItem Text="Save for later" Value="0" Selected="True" />
                <asp:ListItem Text="Save & send for approval" Value="1" />
            </asp:RadioButtonList>

            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />

        </div>

        <div class="full-width">
            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>
        </div>
    </div>


    <%--ADD SUBITEM START--%>
    <asp:Button ID="btnShowPopupAddSubitems" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddSubitems" runat="server" TargetControlID="btnShowPopupAddSubitems"
        PopupControlID="pnlPopupAddSubitems" CancelControlID="imgBtnCancelAddSubitems" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddSubitems" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddSubitems" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdSRNo" runat="server" />
        <asp:HiddenField ID="hdSubitemUpdationFlag" runat="server" />
        <asp:HiddenField ID="hdLastSUID" runat="server" />

        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>Add Subitem</legend>

                <div class="form-grid form-grid-2">

                    <label>Production Order No.</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtProductionOrderNo" runat="server"
                                    CssClass="form-control" /></td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetProductionNumber" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetProductionNumber_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                        </tr>
                    </table>

                    <label>Production Order Date</label>
                    <asp:TextBox ID="txtProductionOrderDate" runat="server"
                        Enabled="false"
                        CssClass="form-control" />
                    <%--onkeyDown="javascript:preventInput(event);"--%>

                    <label>Product Code</label>
                    <asp:DropDownList ID="ddlProductCode" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProductCode_SelectedIndexChanged" />

                    <label>UOM</label>
                    <asp:TextBox ID="txtUOM" runat="server"
                        Enabled="false"
                        CssClass="form-control" />


                    <label>Product Description</label>
                    <asp:TextBox ID="txtProductDesc" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <b>
                        <label style="color: darkgreen;">Is Part of Production Status Report/Main Drawing?:</label></b>
                    <asp:CheckBox ID="chkIsPartOfProductionOrMainDrawing" runat="server" />


                    <label>Completion Required By</label>
                    <asp:TextBox ID="txtExpectedCompletionDate" runat="server"
                        CssClass="form-control"
                        Enabled="false" /><%--onkeyDown="javascript:preventInput(event);"--%>

                    <label>LOT For</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" />
                            </td>
                            <td style="width: 40%">
                                <asp:DropDownList ID="ddlLOTMainSubItems" runat="server" Width="100%" Height="26px"
                                    onblur="return ValidateLOTMainSubItems();" />
                            </td>
                        </tr>
                    </table>

                    <label>Drg./Doc No.</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtDrgNo" runat="server"
                                    CssClass="form-control"
                                    Style="text-transform: uppercase" />

                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetDMSDrawingNo" runat="server" Width="100%" Text="DMS" CssClass="button"
                                    OnClick="btnGetDMSDrawingNo_Click" OnClientClick="return ValidateProductionOrderNoAll();" /></td>
                        </tr>
                    </table>


                    <label>Description</label>
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="2" runat="server"
                        CssClass="form-control" />

                    <label>Rev. No.</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 70%;">

                                <asp:DropDownList ID="ddlRevNo" runat="server"
                                    CssClass="form-control"
                                    onchange="EnableRevisionNoTextboxOther()">
                                    <asp:ListItem Text="00" Value="0" />
                                    <asp:ListItem Text="01" Value="1" />
                                    <asp:ListItem Text="02" Value="2" />
                                    <asp:ListItem Text="03" Value="3" />
                                    <asp:ListItem Text="04" Value="4" />
                                    <asp:ListItem Text="05" Value="5" />
                                    <asp:ListItem Text="06" Value="6" />
                                    <asp:ListItem Text="07" Value="7" />
                                    <asp:ListItem Text="08" Value="8" />
                                    <asp:ListItem Text="09" Value="9" />
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="99 – Any Other Revision" Value="99" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRevNoText"
                                    runat="server"
                                    Text="00"
                                    CssClass="form-control"
                                    Enabled="false" />
                                <asp:HiddenField ID="hdRevNoText" runat="server" />
                                <asp:HiddenField ID="hdRevNoTextOld" runat="server" />
                            </td>
                        </tr>
                    </table>



                    <label>Quantity</label>
                    <asp:HiddenField ID="hdQuantity" runat="server" />
                    <asp:TextBox ID="txtQuantity" runat="server"
                        CssClass="form-control"
                        onkeypress="return inNumberKey(this, event);" />

                    <label>Category (For Factory)</label>
                    <asp:CheckBoxList ID="chkLstCategory" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                        onblur="return ValidateCategory();" />

                    <label>Tag No.</label>
                    <asp:TextBox ID="txtTagNo" runat="server"
                        CssClass="form-control"
                        Style="text-transform: uppercase" />


                    <div class="full-width">

                        <asp:Panel ID="pnlAttachFiles" runat="server">
                            <label>Drawing (.pdf)</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawing1" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 75%;">
                                                <asp:HiddenField ID="hdSiDrawing1" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawing1" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawing1" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawing1_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawing1" runat="server" Height="25px" Width="25px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawing1_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlUploadSiDrawing1" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">


                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawing1" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawing1" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawing1" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawing1" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawing1" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawing1_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <label>Drawing (.dwg/.dxf)</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawing2" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 75%;">
                                                <asp:HiddenField ID="hdSiDrawing2" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawing2" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawing2" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawing2_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawing2" runat="server" Height="25px" Width="25px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawing2_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlUploadSiDrawing2" runat="server" Visible="true">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawing2" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawing2" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawing2" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawing2" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawing2" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawing2_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <label>Drawing 3</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawing3" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 80%;">
                                                <asp:HiddenField ID="hdSiDrawing3" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawing3" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawing3" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawing3_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawing3" runat="server" Height="25px" Width="25px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawing3_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlUploadSiDrawing3" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawing3" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawing3" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawing3" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawing3" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawing3" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawing3_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>

                            <label>Drawing 4</label>
                            <div>
                                <asp:Panel ID="pnlViewSiDrawing4" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 75%;">
                                                <asp:HiddenField ID="hdSiDrawing4" runat="server" Value="0" />
                                                <asp:TextBox ID="txtSiDrawing4" runat="server" Width="100%" Enabled="false" CssClass="textboxdrawings" /></td>
                                            <td>&nbsp;</td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnViewSiDrawing4" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Cancelled02.png" OnClick="imgBtnViewSiDrawing4_Click" ToolTip="View" />
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnRemoveSiDrawing4" runat="server" Height="25px" Width="25px"
                                                    ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveSiDrawing4_Click" ToolTip="Remove" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlUploadSiDrawing4" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%;">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hdUploadSiDrawing4" runat="server" Value="0" />
                                                            <asp:FileUpload ID="uploadFileSiDrawing4" runat="server" Width="100%" Height="29px" BorderStyle="Groove" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="divFileSiDrawing4" style="display: none;">
                                                                <asp:Label ID="lblFileSiDrawing4" runat="server" ForeColor="Red" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:ImageButton ID="imgBtnUndoSiDrawing4" runat="server" Height="30px" Width="30px"
                                                    ImageUrl="~/Images/Icon05.png" OnClick="imgBtnUndoSiDrawing4_Click" ToolTip="Undo" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </asp:Panel>

                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateSubitemToList" CssClass="button" runat="server" Text="Add Subitem"
                    Width="100%" OnClick="btnAddUpdateSubitemToList_Click" OnClientClick="return ValidateAddSubitem();" />
            </div>
            <div class="full-width">
                <asp:Panel ID="pnlAddUpdatedSubitemsMsg" Visible="false" runat="server" Height="50px">
                    <%--<asp:Label ID="lblAddUpdatedSubitemsMsg" runat="server" Font-Bold="True" Font-Size="Large" />--%>
                    <asp:TextBox ID="txtAddUpdatedSubitemsMsg" runat="server" Font-Bold="True" Font-Size="Large" TextMode="MultiLine" Rows="2"
                        Enabled="false" Width="100%" CssClass="text-center" />
                </asp:Panel>
            </div>
        </div>

    </asp:Panel>
    <%--ADD SUBITEM END--%>



    <%--PRODUCTION ORDER NO DETAIL START--%>
    <asp:Button ID="btnShowPopupProductonOrderNoDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeProductonOrderNoDetail" runat="server" TargetControlID="btnShowPopupProductonOrderNoDetail"
        PopupControlID="pnlPopupProductonOrderNoDetail" CancelControlID="imgBtnCancelProductonOrderNoDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupProductonOrderNoDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelProductonOrderNoDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>Material Requisition List:
                    <asp:Label ID="lblProductonOrderNoRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearchPON" runat="server" CssClass="form-control" Enabled="false" />

                        <label>Production Order No.</label>
                        <asp:TextBox ID="txtProductionOrderNoSearchPON" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchProductionOrderNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchProductionOrderNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblProductonOrderNoMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvProductonOrderNoDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvProductonOrderNoDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblProductionOrderNo" runat="server" Visible="false" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' />
                                <asp:Label ID="lblClass" runat="server" Visible="false" Text='<%# Eval("CLASS") %>' />
                                <asp:Label ID="lblProductionOrderDate" runat="server" Visible="false" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' />
                                <%--<asp:Label ID="lblExpectedCompletionDate" runat="server" Visible="false" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' />--%>

                                <asp:Button ID="btnGetProductionOrderNo" CommandArgument="GET" ToolTip="Get Production Order No" Width="100%"
                                    runat="server" Text="Get" CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="PRODUCTION_ORDER_NO" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="PRODUCTION_ORDER_DATE" />
                        <%--<asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="EXPECTED_COMPLETION_DATE" />--%>
                        <asp:BoundField DataField="CLASS" HeaderText="CLASS" />
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
    <%--PRODUCTION ORDER NO DETAIL END--%>


   <%--DMS DRAWING NO LIST START--%>
    <asp:Button ID="btnShowPopupDMSDrawingNoList" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDMSDrawingNoList" runat="server" TargetControlID="btnShowPopupDMSDrawingNoList"
        PopupControlID="pnlPopupDMSDrawingNoList" CancelControlID="imgBtnCancelDMSDrawingNoList" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupDMSDrawingNoList" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDMSDrawingNoList" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblDMSDrawingNoListRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearchDMS" runat="server" CssClass="form-control" Enabled="false" />

                        <asp:Button ID="btnSearchDMSDrawingNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchDMSDrawingNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblDMSDrawingNoListMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvDMSDrawingNoList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvDMSDrawingNoList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="GET">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                <asp:Button ID="btnGetDMSDrawingNo" CommandArgument="GET" ToolTip="Get DMS Drawing No" Width="100%"
                                    runat="server" Text="Get" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
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
    <%--DMS DRAWING NO LIST END--%>


     <%--ADD ATTACHMENTS START--%>
    <asp:Button ID="btnShowPopupAddAttachments" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddUpdateAttachments" runat="server" TargetControlID="btnShowPopupAddAttachments"
        PopupControlID="pnlPopupAddAttachments" CancelControlID="imgBtnCancelAddAttachments" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddAttachments" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddAttachments" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend style="text-align: center;">Add/Update Additional Attachments</legend>

                <div class="form-grid form-grid-2">

                    <label>Attachment 2</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment2" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment2" style="display: none;">
                                    <asp:Label ID="lblfileAttachment2" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>


                    <label>Attachment 3</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment3" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment3ToEdit" style="display: none;">
                                    <asp:Label ID="lblfileAttachment3" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>


                    <label>Attachment 4</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:FileUpload ID="uploadFileAttachment4" runat="server"
                                    CssClass="form-control"
                                    Height="29px" BorderStyle="Groove" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divfileAttachment4ToEdit" style="display: none;">
                                    <asp:Label ID="lblfileAttachment4" runat="server" ForeColor="Red" />
                                </div>
                            </td>
                        </tr>
                    </table>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnAddUpdateAttachmentsToList" CssClass="button" runat="server" Text="Save Attachments"
                    Width="100%" OnClick="btnAddUpdateAttachmentsToList_Click" OnClientClick="return ValidateAllAttachments();" />
            </div>

        </div>
    </asp:Panel>
    <%--ADD ATTACHMENTS END--%>


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

                        <label>Customer PO No.</label>
                        <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />

                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />

                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Label ID="lblJOBMsg" runat="server" />
                </div>

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
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
    <%--JOB DETAIL END--%>
    
    <%-- ADD LOT JOB APPROVERS START --%>
    <asp:Button ID="btnShowAddApproversPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddApprovers" runat="server" TargetControlID="btnShowAddApproversPopup"
        PopupControlID="pnlAddApprovers" CancelControlID="imgBtnCancelAddApprovers" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlAddApprovers" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddApprovers" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            class="popup-iframe"
            id="iframeAddApprovers"
            runat="server">
        </iframe>
    </asp:Panel>
    <%-- ADD LOT JOB APPROVERS END --%>

    <%-- ADD LOT SUBITEM MANAGERS START --%>
    <asp:Button ID="btnShowAddSubitemManagersPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddSubitemManagers" runat="server" TargetControlID="btnShowAddSubitemManagersPopup"
        PopupControlID="pnlAddSubitemManagers" CancelControlID="imgBtnCancelAddSubitemManagers" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlAddSubitemManagers" runat="server" 
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddSubitemManagers" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe 
            class="popup-iframe"
            id="iframeAddSubitemManagers"
            runat="server">            
        </iframe>
    </asp:Panel>
    <%-- ADD LOT SUBITEM MANAGERS END --%>

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
            runat="server">
        </iframe>
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
            runat="server">
        </iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>

</asp:Content>
