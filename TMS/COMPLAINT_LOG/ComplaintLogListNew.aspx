<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ComplaintLogListNew.aspx.cs" Inherits="COMPLAINT_LOG_ComplaintLogListNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
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
            document.getElementById('<%=txtStartDate.ClientID %>').value = document.getElementById('<%=hdStartDate.ClientID %>').value;
            document.getElementById('<%=txtEndDate.ClientID %>').value = document.getElementById('<%=hdEndDate.ClientID %>').value;


            if (document.getElementById('<%=hdTargetCompletionDate.ClientID %>') != null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>') != null) {
                document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value;
                document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value = document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value;
                document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value;
            }

            if (document.getElementById('<%=hdActualCompletionDate.ClientID %>') != null) {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').value = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value;
                document.getElementById('<%=hdActualCompletionDate.ClientID %>').value = document.getElementById('<%=txtActualCompletionDate.ClientID %>').value;
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').value = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value;
            }
        }

        function clientChangedSearch(sender, args) {

            document.getElementById('<%=hdStartDate.ClientID %>').value = document.getElementById('<%=txtStartDate.ClientID %>').value;
            document.getElementById('<%=hdEndDate.ClientID %>').value = document.getElementById('<%=txtEndDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return false;
            }
        }

        function clientChangedTargetDate(sender, args) {
            document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value = document.getElementById('<%=txtTargetCompletionDate.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);


            if (document.getElementById('<%=hdTargetCompletionDate.ClientID %>') != null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>') != null) {
                if (endFormatedDate < formatedDate) {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                    alert("Target completion date must be equal or greater than complaint received date!");
                    return true;
                }
                else {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }


        function clientChangedActualDate(sender, args) {
            document.getElementById('<%=hdActualCompletionDate.ClientID %>').value = document.getElementById('<%=txtActualCompletionDate.ClientID %>').value;


            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                //document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                //alert("Actual completion date must be equal or greater than target completion date!");
                //return true;
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
            else {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdEndDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return true;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function ValidateAllNew() {
            var check = true;
            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">

        function ValidateResponsibleDepartment() {
            var hdStatusID = document.getElementById('<%=hdStatusID.ClientID %>').value;
            var hdPropertyValue = document.getElementById('<%=hdPropertyValue.ClientID %>').value;

            var ResponsibleDepartment = document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').selectedIndex;
            if (hdStatusID == '1' && hdPropertyValue == '0') {
                if (ResponsibleDepartment == '' || ResponsibleDepartment == '0') {
                    document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateResponsiblePerson() {
            var hdStatusID = document.getElementById('<%=hdStatusID.ClientID %>').value;
            var hdPropertyValue = document.getElementById('<%=hdPropertyValue.ClientID %>').value;
            var ResponsibleDepartment = document.getElementById('<%=ddlResponsibleDepartment.ClientID %>');

            var ResponsibleDepartmentID = ResponsibleDepartment.options[ResponsibleDepartment.selectedIndex].value;

            var ResponsiblePerson = document.getElementById('<%=ddlResponsiblePerson.ClientID %>').selectedIndex;


            if ((hdStatusID == '2' && hdPropertyValue == '0') || ResponsibleDepartmentID == '8' || ResponsibleDepartmentID == '18') {
                if (ResponsiblePerson == '' || ResponsiblePerson == '0') {
                    document.getElementById('<%=ddlResponsiblePerson.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlResponsiblePerson.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlResponsibleDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ValidateBusinessUnit() {
            var BusinessUnit = document.getElementById('<%=ddlBusinessUnit.ClientID %>').selectedIndex;
            if (BusinessUnit == '' || BusinessUnit == '0') {
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProposedActions() {
            var ProposedActions = document.getElementById('<%=txtProposedActions.ClientID %>').value;
            if (ProposedActions == '') {
                document.getElementById('<%=txtProposedActions.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProposedActions.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRootCause() {
            var RootCause = document.getElementById('<%=txtRootCause.ClientID %>').value;
            if (RootCause == '') {
                document.getElementById('<%=txtRootCause.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRootCause.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateCorrectiveAction() {
            var CorrectiveAction = document.getElementById('<%=txtCorrectiveAction.ClientID %>').value;
            if (CorrectiveAction == '') {
                document.getElementById('<%=txtCorrectiveAction.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCorrectiveAction.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidatefileUploadVisitReport1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport1 = document.getElementById('<%=fileUploadVisitReport1.ClientID %>').value;
            var divfileUploadVisitReport1 = document.getElementById("divfileUploadVisitReport1");
            var lblfileUploadVisitReport1 = document.getElementById('<%=lblfileUploadVisitReport1.ClientID %>');

            if (fileUploadVisitReport1 == '') {
                document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport1.style.display = "none";
                lblfileUploadVisitReport1.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport1.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport1.style.display = "block";
                    lblfileUploadVisitReport1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport1.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport1.style.display = "none";
                    lblfileUploadVisitReport1.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidatefileUploadVisitReport2() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport2 = document.getElementById('<%=fileUploadVisitReport2.ClientID %>').value;
            var divfileUploadVisitReport2 = document.getElementById("divfileUploadVisitReport2");
            var lblfileUploadVisitReport2 = document.getElementById('<%=lblfileUploadVisitReport2.ClientID %>');

            if (fileUploadVisitReport2 == '') {
                document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport2.style.display = "none";
                lblfileUploadVisitReport2.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport2.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport2.style.display = "block";
                    lblfileUploadVisitReport2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport2.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport2.style.display = "none";
                    lblfileUploadVisitReport2.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidatefileUploadVisitReport3() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadVisitReport3 = document.getElementById('<%=fileUploadVisitReport3.ClientID %>').value;
            var divfileUploadVisitReport3 = document.getElementById("divfileUploadVisitReport3");
            var lblfileUploadVisitReport3 = document.getElementById('<%=lblfileUploadVisitReport3.ClientID %>');

            if (fileUploadVisitReport3 == '') {
                document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "";
                divfileUploadVisitReport3.style.display = "none";
                lblfileUploadVisitReport3.innerHTML = "";
                return false;
            }
            else {
                if (!regex.test(fileUploadVisitReport3.toLowerCase())) {
                    document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadVisitReport3.style.display = "block";
                    lblfileUploadVisitReport3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadVisitReport3.ClientID %>').style.borderColor = "";
                    divfileUploadVisitReport3.style.display = "none";
                    lblfileUploadVisitReport3.innerHTML = "";
                    return false;
                }
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
                    lblfileUploadAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
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
                    lblfileUploadAttachment2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .msg file!";
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

    <script type="text/Javascript">
        function ValidateTargetDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=txtComplanitRcvdDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (document.getElementById('<%=hdTargetCompletionDate.ClientID %>') != null && document.getElementById('<%=txtTargetCompletionDate.ClientID %>') != null) {
                if (endFormatedDate < formatedDate) {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                    alert("Target completion date must be equal or greater than complaint received date!");
                    return true;
                }
                else {
                    document.getElementById('<%=txtTargetCompletionDate.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateActualDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdTargetCompletionDate.ClientID %>').value.split("-");
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


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdActualCompletionDate.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                //document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "#F7627F";
                //alert("Actual completion date must be equal or greater than target completion date!");
                //return true;
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
            else {
                document.getElementById('<%=txtActualCompletionDate.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAllUpdation() {

            var check = true;

            if (document.getElementById('<%=ddlResponsibleDepartment.ClientID %>') != null) {
                if (ValidateResponsibleDepartment()) {
                    return false;
                }
                if (ValidateResponsiblePerson()) {
                    return false;
                }

                if (ValidateTargetDateRange()) {
                    return false;
                }
            }


            if (document.getElementById('<%=ddlBusinessUnit.ClientID %>') != null) {
                if (ValidateBusinessUnit()) {
                    return false;
                }

                if (ValidateProposedActions()) {
                    return false;
                }

                if (ValidateRootCause()) {
                    return false;
                }

                if (ValidateCorrectiveAction()) {
                    return false;
                }

                if (ValidateActualDateRange()) {
                    return false;
                }

                if (document.getElementById('<%=fileUploadVisitReport1.ClientID %>') != null) {
                    if (ValidatefileUploadVisitReport1()) {
                        return false;
                    }

                    if (ValidatefileUploadVisitReport2()) {
                        return false;
                    }

                    if (ValidatefileUploadVisitReport3()) {
                        return false;
                    }
                }

                if (document.getElementById('<%=fileUploadAttachment1.ClientID %>') != null) {
                    if (ValidatefileUploadAttachment1()) {
                        return false;
                    }

                    if (ValidatefileUploadAttachment2()) {
                        return false;
                    }
                }
            }

            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Complaint Log List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">
                    <label>Start Date</label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDate"
                                    PopupButtonID="imgbtnStartDate" runat="server"
                                    TargetControlID="txtStartDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnStartDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>End Date</label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDate"
                                    PopupButtonID="imgbtnEndDate" runat="server"
                                    TargetControlID="txtEndDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnEndDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Complaint Log No.</label>
                    <asp:TextBox ID="txtComplaintLogNo" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Responsible Person</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    &nbsp;
                 &nbsp;
                 <asp:Button ID="btnSearch"
                     OnClick="btnSearch_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />
                </div>
            </fieldset>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <asp:GridView 
                 CssClass="employee-grid"
                ID="gvComplaintLogList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                HorizontalAlign="Center" OnRowCommand="gvComplaintLogList_RowCommand" OnRowDataBound="gvComplaintLogList_RowDataBound"
                AllowPaging="True" OnPageIndexChanging="gvComplaintLogList_PageIndexChanging">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="VIEW">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_1">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT_ONE") %>' />
                            <asp:ImageButton ID="btnAttachment1" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_2">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT_TWO") %>' />
                            <asp:ImageButton ID="btnAttachment2" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_3">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT_THREE") %>' />
                            <asp:ImageButton ID="btnAttachment3" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FILE_4">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT_FOUR") %>' />
                            <asp:ImageButton ID="btnAttachment4" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT4"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SEND_MAIL" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnSendMail" CommandArgument="SEND_MAIL" runat="server" ImageUrl="~/Images/NEWICONS/email05.png"
                                Visible="false" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT">
                        <ItemTemplate>
                            <asp:Label ID="lblComplaintLogID" runat="server" Visible="false" Text='<%# Eval("COMPLAINT_LOG_ID") %>' />
                            <asp:Label ID="lblComplaintLogNo" runat="server" Visible="false" Text='<%# Eval("COMPLAINT_LOG_NO") %>' />
                            <asp:Label ID="lblComplaintRcvdOn" runat="server" Visible="false" Text='<%# Eval("COMPLAINT_RECEIVED_ON") %>' />
                            <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# Eval("STATUS_ID") %>' />
                            <asp:Label ID="lblStatusName" runat="server" Visible="false" Text='<%# Eval("STATUS_NAME") %>' />
                            <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblAddress" runat="server" Visible="false" Text='<%# Eval("ADDRESS") %>' />
                            <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                            <asp:Label ID="lblPlant" runat="server" Visible="false" Text='<%# Eval("PLANT") %>' />
                            <asp:Label ID="lblComplaintDescription" runat="server" Visible="false" Text='<%# Eval("COMPLAINT_DESCRIPTION") %>' />
                            <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblPoNo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                            <asp:Label ID="lblItemName" runat="server" Visible="false" Text='<%# Eval("ITEM_NAME") %>' />
                            <asp:Label ID="lblModelNo" runat="server" Visible="false" Text='<%# Eval("MODEL_NO") %>' />
                            <asp:Label ID="lblServiceTypeID" runat="server" Visible="false" Text='<%# Eval("SERVICE_TYPE_ID") %>' />
                            <asp:Label ID="lblServiceType" runat="server" Visible="false" Text='<%# Eval("SERVICE_TYPE") %>' />
                            <asp:Label ID="lblBusinessUnitID" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT_ID") %>' />
                            <asp:Label ID="lblBusinessUnit" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT") %>' />
                            <asp:Label ID="lblManufacturerName" runat="server" Visible="false" Text='<%# Eval("MANUFACTURER_NAME") %>' />
                            <asp:Label ID="lblProposedActions" runat="server" Visible="false" Text='<%# Eval("PROPOSED_ACTIONS") %>' />
                            <asp:Label ID="lblRootCause" runat="server" Visible="false" Text='<%# Eval("ROOT_CAUSE") %>' />
                            <asp:Label ID="lblTargetCompletionDate" runat="server" Visible="false" Text='<%# Eval("TARGET_COMPLETION_DATE") %>' />
                            <asp:Label ID="lblActualCompletionDate" runat="server" Visible="false" Text='<%# Eval("ACTUAL_COMPLETION_DATE") %>' />
                            <asp:Label ID="lblRespPersonLessonLearnt" runat="server" Visible="false" Text='<%# Eval("RESP_PERSON_LESSON_LEARNT") %>' />
                            <asp:Label ID="lblRespHODLessonLearnt" runat="server" Visible="false" Text='<%# Eval("RESP_DEPT_HOD_LESSON_LEARNT") %>' />
                            <asp:Label ID="lblServiceHODLessonLearnt" runat="server" Visible="false" Text='<%# Eval("SERVICE_DEPT_HOD_LESSON_LEARNT") %>' />
                            <asp:Label ID="lblCorrectiveAction" runat="server" Visible="false" Text='<%# Eval("CORRECTIVE_ACTION") %>' />
                            <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                            <asp:Label ID="lblCreatedOn" runat="server" Visible="false" Text='<%# Eval("CREATED_ON") %>' />
                            <asp:Label ID="lblRespDeptAssignedByID" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_DEPT_ASSIGNED_BY_ID") %>' />
                            <asp:Label ID="lblRespDeptAssignedBy" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_DEPT_ASSIGNED_BY") %>' />
                            <asp:Label ID="lblRespDeptAssignedOn" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_DEPT_ASSIGNED_ON") %>' />
                            <asp:Label ID="lblRespDeptID" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_DEPT_ID") %>' />
                            <asp:Label ID="lblRespDeptName" runat="server" Visible="false" Text='<%# Eval("DEPARTMENT_NAME") %>' />
                            <asp:Label ID="lblRespPersonAssignedByID" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_PERSON_ASSIGNED_BY_ID") %>' />
                            <asp:Label ID="lblRespPersonAssignedBy" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_PERSON_ASSIGNED_BY") %>' />
                            <asp:Label ID="lblRespPersonAssignedByOn" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_PERSON_ASSIGNED_ON") %>' />
                            <asp:Label ID="lblRespPersonID" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_PERSON_ID") %>' />
                            <asp:Label ID="lblRespPersonName" runat="server" Visible="false" Text='<%# Eval("RESPONSIBLE_PERSON") %>' />
                            <asp:Label ID="lblResolvedBy" runat="server" Visible="false" Text='<%# Eval("RESOLVED_BY") %>' />
                            <asp:Label ID="lblResolvedOn" runat="server" Visible="false" Text='<%# Eval("RESOLVED_ON") %>' />
                            <asp:Label ID="lblApprovedBy" runat="server" Visible="false" Text='<%# Eval("APPROVED_BY") %>' />
                            <asp:Label ID="lblApprovedOn" runat="server" Visible="false" Text='<%# Eval("APPROVED_ON") %>' />
                            <asp:Label ID="lblClosedBy" runat="server" Visible="false" Text='<%# Eval("CLOSED_BY") %>' />
                            <asp:Label ID="lblClosedOn" runat="server" Visible="false" Text='<%# Eval("CLOSED_ON") %>' />
                            <asp:Label ID="lblIsNewMailSent" runat="server" Visible="false" Text='<%# Eval("IS_NEW_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsDeptAssignedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_DEPT_ASSIGNED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsPersonAssignedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_PERSON_ASSIGNED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsResolvedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_RESOLVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsApprovedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' />
                            <asp:Label ID="lblIsClosedMailSent" runat="server" Visible="false" Text='<%# Eval("IS_CLOSED_MAIL_SENT") %>' />
                            <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnAction" CommandArgument="ACTION" runat="server" Text="Close" CssClass="cancelbutton" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="COMPLAINT_LOG_NO" HeaderText="COMPLAINT_NO " />
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COMPLAINT_RECEIVED_ON" HeaderText="COMPLAINT_RECEIVED_ON" />
                    <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS_NAME" />
                    <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                    <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" />
                    <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                    <asp:BoundField DataField="PLANT" HeaderText="PLANT" />
                    <asp:BoundField DataField="COMPLAINT_DESCRIPTION" HeaderText="COMPLAINT_DESCRIPTION" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM_NAME" />
                    <asp:BoundField DataField="MODEL_NO" HeaderText="MODEL_NO" />
                    <asp:BoundField DataField="SERVICE_TYPE" HeaderText="SERVICE_TYPE" />
                    <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT" />
                    <asp:BoundField DataField="MANUFACTURER_NAME" HeaderText="MANUFACTURER_NAME" />
                    <asp:BoundField DataField="PROPOSED_ACTIONS" HeaderText="PROPOSED_ACTIONS" />
                    <asp:BoundField DataField="ROOT_CAUSE" HeaderText="ROOT_CAUSE" />
                    <asp:BoundField DataField="TARGET_COMPLETION_DATE" HeaderText="TARGET_COMPLETION_DATE" />
                    <asp:BoundField DataField="ACTUAL_COMPLETION_DATE" HeaderText="ACTUAL_COMPLETION_DATE" />
                    <asp:BoundField DataField="RESP_PERSON_LESSON_LEARNT" HeaderText="RESP_PERSON_LESSON_LEARNT" />
                    <asp:BoundField DataField="RESP_DEPT_HOD_LESSON_LEARNT" HeaderText="RESP_DEPT_HOD_LESSON_LEARNT" />
                    <asp:BoundField DataField="SERVICE_DEPT_HOD_LESSON_LEARNT" HeaderText="SERVICE_DEPT_HOD_LESSON_LEARNT" />
                    <asp:BoundField DataField="CORRECTIVE_ACTION" HeaderText="CORRECTIVE_ACTION" />
                    <asp:BoundField DataField="RESPONSIBLE_DEPT_ASSIGNED_BY" HeaderText="RESP_DEPT_ASSIGNED_BY" />
                    <asp:BoundField DataField="RESPONSIBLE_DEPT_ASSIGNED_ON" HeaderText="RESP_DEPT_ASSIGNED_ON" />
                    <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT_NAME" />
                    <asp:BoundField DataField="RESPONSIBLE_PERSON_ASSIGNED_BY" HeaderText="RESP_PERSON_ASSIGNED_BY" />
                    <asp:BoundField DataField="RESPONSIBLE_PERSON_ASSIGNED_ON" HeaderText="RESP_PERSON_ASSIGNED_ON" />
                    <asp:BoundField DataField="RESPONSIBLE_PERSON" HeaderText="RESPONSIBLE_PERSON" />
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


    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdStatusID" runat="server" />
        <asp:HiddenField ID="hdPropertyValue" runat="server" />


        <div class="form-entry-container">

            <fieldset class="form-card">
                <asp:Label ID="lblLegend" runat="server" />

                <asp:Panel ID="pnlNewComplaint" runat="server" Enabled="false">
                    <div class="form-grid form-grid-2">

                        <label>Date of Complaint Received</label>
                        <asp:TextBox ID="txtComplanitRcvdDate" runat="server" ReadOnly="true"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Location</label>
                        <asp:TextBox ID="txtLocation" runat="server"
                            CssClass="form-control"
                            Enabled="false" />


                        <label>Customer Name</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 80%;">
                                    <asp:TextBox ID="txtCustomerNameNew" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 20%;">
                                    <asp:TextBox ID="txtCustomerCode" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>

                        <label>Address</label>
                        <div class="full-width">
                            <asp:TextBox ID="txtAddress"
                                Enabled="true"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="2" />
                        </div>

                        <label>Plant</label>
                        <asp:TextBox ID="txtPlant" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNo" runat="server"
                            CssClass="form-control"
                            Enabled="false" />


                        <label>Customer PO Number</label>
                        <asp:TextBox ID="txtPONumber" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Item Name</label>
                        <asp:TextBox ID="txtItemName" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Model No.</label>
                        <asp:TextBox ID="txtModelNo" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>Type Of Service</label>
                        <asp:TextBox ID="txtTypeOfService" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

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
                </asp:Panel>
                <asp:Panel ID="pnlAssignment" runat="server">
                    <label>Responsible Department</label>
                    <asp:DropDownList ID="ddlResponsibleDepartment"
                        runat="server" Width="100%"
                        CssClass="form-control"
                        OnSelectedIndexChanged="ddlResponsibleDepartment_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>

                    <label>Responsible Person</label>
                    <asp:DropDownList ID="ddlResponsiblePerson"
                        runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <asp:Label ID="lblTargetDate" runat="server" Text="Target Completion Date:"></asp:Label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox
                                    CssClass="form-control"
                                    ID="txtTargetCompletionDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                <asp:HiddenField ID="hdTargetCompletionDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarTargetCompletionDate"
                                    PopupButtonID="imgbtnTargetCompletionDate"
                                    runat="server" TargetControlID="txtTargetCompletionDate"
                                    Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedTargetDate">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnTargetCompletionDate" runat="server"
                                    ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <asp:Panel ID="pnlResolve" runat="server">

                    <label>Business Unit</label>
                    <asp:DropDownList ID="ddlBusinessUnit" runat="server"
                        CssClass="form-control" />


                    <label>Equip. Manufacturer Name</label>
                    <asp:TextBox ID="txtEquipmentManufacturerName" runat="server"
                        CssClass="form-control" />

                    <label>Proposed Actions</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtProposedActions"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>


                    <label>Root Cause</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRootCause"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <label>Actual Completion Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox
                                    CssClass="form-control"
                                    ID="txtActualCompletionDate" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdActualCompletionDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarActualCompletionDate" PopupButtonID="imgBtnActualCompletionDate"
                                    runat="server" TargetControlID="txtActualCompletionDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedActualDate">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnActualCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>


                    <label>Corrective Action</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtCorrectiveAction"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <label>Lesson Learnt(Resp. Person)</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRespPersonLessonLearnt"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>

                    <asp:Panel ID="pnlVisitReportSummary" runat="server">

                        <label>Sanction No.</label>
                        <asp:DropDownList ID="ddlSanctionNo" runat="server"
                            CssClass="form-control"
                            OnSelectedIndexChanged="ddlSanctionNo_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>

                        <label>Visit Report Summary 1</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadVisitReport1" runat="server"
                                        Enabled="false"
                                        BorderStyle="Groove"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadVisitReport1" style="display: none;">
                                        <asp:Label ID="lblfileUploadVisitReport1" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <label>Visit Report Summary 2</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadVisitReport2" runat="server"
                                        Enabled="false"
                                        BorderStyle="Groove"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadVisitReport2" style="display: none;">
                                        <asp:Label ID="lblfileUploadVisitReport2" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <label>Visit Report Summary 3</label>
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadVisitReport3" runat="server"
                                        Enabled="false"
                                        BorderStyle="Groove"
                                        CssClass="form-control" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadVisitReport3" style="display: none;">
                                        <asp:Label ID="lblfileUploadVisitReport3" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>


                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlAttachFiles" runat="server">
                    <label>Other Attachment 1</label>
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadAttachment1" runat="server"
                                    Enabled="false"
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

                    <label>Other Attachment 2</label>
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fileUploadAttachment2" runat="server"
                                    Enabled="false"
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
                </asp:Panel>

                <asp:Panel ID="pnlViewComplaintFiles" runat="server">
                    <label>View Compaint File 1</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtViewAttachment1" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                    OnClick="btnViewAttachment1_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>View Compaint File 2</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtViewAttachment2" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnViewAttachment2" Height="20px" Width="20px" runat="server"
                                    OnClick="btnViewAttachment2_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnlViewRelovedFiles" runat="server">
                    <label>View Other Attachment 1</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtViewAttachment3" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnViewAttachment3" Height="20px" Width="20px" runat="server"
                                    OnClick="btnViewAttachment3_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>View Other Attachment 2</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtViewAttachment4" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnViewAttachment4" Height="20px" Width="20px" runat="server"
                                    OnClick="btnViewAttachment4_Click" />
                            </td>
                        </tr>
                    </table>

                </asp:Panel>

                <asp:Panel ID="pnlRespDeptHODLessonLearnt" runat="server">
                    <label>Lesson Learnt(Resp. Dept. HOD)</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRespDeptHODLessonLearnt"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlServiceDeptHODLessonLearnt" runat="server">
                    <label>Lesson Learnt(Service Dept. HOD)</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtServiceDeptHODLessonLearnt"
                            Enabled="true"
                            runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="2" />
                    </div>
                </asp:Panel>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSubmit"
                    runat="server"
                    Text="Save"
                    CssClass="button"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnSubmit_Click" />
            </div>


            <div class="full-width">
                <asp:Panel ID="pnlSuccessMsg" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblSuccessMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
            </div>
        </div>




    </asp:Panel>

    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
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

    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnShowPDFFile"
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
            id="iframeViewComplaintLogInPDF"
            runat="server">
        </iframe>
    </asp:Panel>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
