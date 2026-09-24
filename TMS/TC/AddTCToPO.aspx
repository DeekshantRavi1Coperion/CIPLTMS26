<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="AddTCToPO.aspx.cs"
    Inherits="TC_AddTCToPO" Title="CIPLTMS- Upload TC To PO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

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
        .textboxtstatustext {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 5px;
            background-color: lightpink;
        }

        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: palegreen;*/
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdStartDateSearch.ClientID %>').value = document.getElementById('<%=txtStartDateSearch.ClientID %>').value;
            document.getElementById('<%=hdEndDateSearch.ClientID %>').value = document.getElementById('<%=txtEndDateSearch.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
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

        function ValidatefileTC1ToUploadExtn() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC1ToUpload;
            var divfileTC1ToUpload;
            var lblfileTC1ToUpload;

            if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != null) {

                fileTC1ToUpload = document.getElementById('<%=fileTC1ToUpload.ClientID %>').value;
                divfileTC1ToUpload = document.getElementById("divfileTC1ToUpload");
                lblfileTC1ToUpload = document.getElementById('<%=lblfileTC1ToUpload.ClientID %>');

                fileTC1ToUpload = fileTC1ToUpload.split(" ").join("")
                fileTC1ToUpload = fileTC1ToUpload.split("(").join("")
                fileTC1ToUpload = fileTC1ToUpload.split(")").join("")

                if (!regex.test(fileTC1ToUpload.toLowerCase())) {
                    document.getElementById('<%=fileTC1ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC1ToUpload.style.display = "block";
                    lblfileTC1ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {
                    document.getElementById('<%=fileTC1ToUpload.ClientID %>').style.borderColor = "";
                    divfileTC1ToUpload.style.display = "none";
                    lblfileTC1ToUpload.innerHTML = "";
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ValidatefileTC2ToUploadExtn() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC2ToUpload;
            var divfileTC2ToUpload;
            var lblfileTC2ToUpload;

            if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != null) {

                fileTC2ToUpload = document.getElementById('<%=fileTC2ToUpload.ClientID %>').value;
                divfileTC2ToUpload = document.getElementById("divfileTC2ToUpload");
                lblfileTC2ToUpload = document.getElementById('<%=lblfileTC2ToUpload.ClientID %>');

                fileTC2ToUpload = fileTC2ToUpload.split(" ").join("")
                fileTC2ToUpload = fileTC2ToUpload.split("(").join("")
                fileTC2ToUpload = fileTC2ToUpload.split(")").join("")

                if (!regex.test(fileTC2ToUpload.toLowerCase())) {
                    document.getElementById('<%=fileTC2ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC2ToUpload.style.display = "block";
                    lblfileTC2ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {
                    document.getElementById('<%=fileTC2ToUpload.ClientID %>').style.borderColor = "";
                    divfileTC2ToUpload.style.display = "none";
                    lblfileTC2ToUpload.innerHTML = "";
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ValidatefileTC3ToUploadExtn() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC3ToUpload;
            var divfileTC3ToUpload;
            var lblfileTC3ToUpload;

            if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != null) {

                fileTC3ToUpload = document.getElementById('<%=fileTC3ToUpload.ClientID %>').value;
                divfileTC3ToUpload = document.getElementById("divfileTC3ToUpload");
                lblfileTC3ToUpload = document.getElementById('<%=lblfileTC3ToUpload.ClientID %>');

                fileTC3ToUpload = fileTC3ToUpload.split(" ").join("")
                fileTC3ToUpload = fileTC3ToUpload.split("(").join("")
                fileTC3ToUpload = fileTC3ToUpload.split(")").join("")

                if (!regex.test(fileTC3ToUpload.toLowerCase())) {
                    document.getElementById('<%=fileTC3ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC3ToUpload.style.display = "block";
                    lblfileTC3ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {
                    document.getElementById('<%=fileTC3ToUpload.ClientID %>').style.borderColor = "";
                    divfileTC3ToUpload.style.display = "none";
                    lblfileTC3ToUpload.innerHTML = "";

                    return false;
                }
            }
            else {
                return false;
            }
        }







        function ValidatefileTC1ToUpload() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC1ToUpload;
            var divfileTC1ToUpload;
            var lblfileTC1ToUpload;

            if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != null) {

                fileTC1ToUpload = document.getElementById('<%=fileTC1ToUpload.ClientID %>').value;
                divfileTC1ToUpload = document.getElementById("divfileTC1ToUpload");
                lblfileTC1ToUpload = document.getElementById('<%=lblfileTC1ToUpload.ClientID %>');

                if (fileTC1ToUpload == '') {
                    document.getElementById('<%=fileTC1ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC1ToUpload.style.display = "block";
                    lblfileTC1ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {

                    fileTC1ToUpload = fileTC1ToUpload.split(" ").join("")
                    fileTC1ToUpload = fileTC1ToUpload.split("(").join("")
                    fileTC1ToUpload = fileTC1ToUpload.split(")").join("")

                    if (!regex.test(fileTC1ToUpload.toLowerCase())) {
                        document.getElementById('<%=fileTC1ToUpload.ClientID %>').style.borderColor = "#F7627F";
                        divfileTC1ToUpload.style.display = "block";
                        lblfileTC1ToUpload.innerHTML = "Please enter only .pdf file!";

                        return true;
                    }
                    else {
                        document.getElementById('<%=fileTC1ToUpload.ClientID %>').style.borderColor = "";
                        divfileTC1ToUpload.style.display = "none";
                        lblfileTC1ToUpload.innerHTML = "";
                        return false;
                    }
                }
            }
            else {
                return false;
            }
        }

        function ValidatefileTC2ToUpload() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC2ToUpload;
            var divfileTC2ToUpload;
            var lblfileTC2ToUpload;

            if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != null) {

                fileTC2ToUpload = document.getElementById('<%=fileTC2ToUpload.ClientID %>').value;
                divfileTC2ToUpload = document.getElementById("divfileTC2ToUpload");
                lblfileTC2ToUpload = document.getElementById('<%=lblfileTC2ToUpload.ClientID %>');

                if (fileTC2ToUpload == '') {

                    document.getElementById('<%=fileTC2ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC2ToUpload.style.display = "block";
                    lblfileTC2ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {

                    fileTC2ToUpload = fileTC2ToUpload.split(" ").join("")
                    fileTC2ToUpload = fileTC2ToUpload.split("(").join("")
                    fileTC2ToUpload = fileTC2ToUpload.split(")").join("")

                    if (!regex.test(fileTC2ToUpload.toLowerCase())) {
                        document.getElementById('<%=fileTC2ToUpload.ClientID %>').style.borderColor = "#F7627F";
                        divfileTC2ToUpload.style.display = "block";
                        lblfileTC2ToUpload.innerHTML = "Please enter only .pdf file!";

                        return true;
                    }
                    else {
                        document.getElementById('<%=fileTC2ToUpload.ClientID %>').style.borderColor = "";
                        divfileTC2ToUpload.style.display = "none";
                        lblfileTC2ToUpload.innerHTML = "";
                        return false;
                    }
                }
            }
            else {
                return false;
            }
        }

        function ValidatefileTC3ToUpload() {

            var allowedFiles = [".pdf", ".PDF"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");

            var fileTC3ToUpload;
            var divfileTC3ToUpload;
            var lblfileTC3ToUpload;

            if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != null) {

                fileTC3ToUpload = document.getElementById('<%=fileTC3ToUpload.ClientID %>').value;
                divfileTC3ToUpload = document.getElementById("divfileTC3ToUpload");
                lblfileTC3ToUpload = document.getElementById('<%=lblfileTC3ToUpload.ClientID %>');

                if (fileTC3ToUpload == '') {
                    document.getElementById('<%=fileTC3ToUpload.ClientID %>').style.borderColor = "#F7627F";
                    divfileTC3ToUpload.style.display = "block";
                    lblfileTC3ToUpload.innerHTML = "Please enter only .pdf file!";

                    return true;
                }
                else {

                    fileTC3ToUpload = fileTC3ToUpload.split(" ").join("")
                    fileTC3ToUpload = fileTC3ToUpload.split("(").join("")
                    fileTC3ToUpload = fileTC3ToUpload.split(")").join("")

                    if (!regex.test(fileTC3ToUpload.toLowerCase())) {
                        document.getElementById('<%=fileTC3ToUpload.ClientID %>').style.borderColor = "#F7627F";
                        divfileTC3ToUpload.style.display = "block";
                        lblfileTC3ToUpload.innerHTML = "Please enter only .pdf file!";

                        return true;
                    }
                    else {
                        document.getElementById('<%=fileTC3ToUpload.ClientID %>').style.borderColor = "";
                        divfileTC3ToUpload.style.display = "none";
                        lblfileTC3ToUpload.innerHTML = "";

                        return false;
                    }
                }
            }
            else {
                return false;
            }
        }


        function ValidateTC1TeamToUpload() {
            var AssignedTo = document.getElementById('<%=ddlTC1TeamToUpload.ClientID %>').selectedIndex;
            if (AssignedTo == '' || AssignedTo == 0) {
                document.getElementById('<%=ddlTC1TeamToUpload.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTC1TeamToUpload.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTC2TeamToUpload() {
            var AssignedTo = document.getElementById('<%=ddlTC2TeamToUpload.ClientID %>').selectedIndex;
            if (AssignedTo == '' || AssignedTo == 0) {
                document.getElementById('<%=ddlTC2TeamToUpload.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTC2TeamToUpload.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTC3TeamToUpload() {
            var AssignedTo = document.getElementById('<%=ddlTC3TeamToUpload.ClientID %>').selectedIndex;
            if (AssignedTo == '' || AssignedTo == 0) {
                document.getElementById('<%=ddlTC3TeamToUpload.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTC3TeamToUpload.ClientID %>').style.borderColor = "";
                return false;
            }
        }




        function ValidateAllUpload() {
            var check = true;

            if (ValidateAllFiles()) {
                check = false;
            }


            if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != null) {
                var val = document.getElementById('<%=fileTC1ToUpload.ClientID %>').value;
                if (val != '') {

                    if (ValidatefileTC1ToUploadExtn()) {
                        check = false;
                    }


                    if (ValidateTC1TeamToUpload()) {
                        check = false;
                    }
                }
            }

            if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != null) {
                var val = document.getElementById('<%=fileTC2ToUpload.ClientID %>').value;
                if (val != '') {

                    if (ValidatefileTC2ToUploadExtn()) {
                        check = false;
                    }


                    if (ValidateTC2TeamToUpload()) {
                        check = false;
                    }
                }
            }

            if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != null) {
                var val = document.getElementById('<%=fileTC3ToUpload.ClientID %>').value;
                if (val != '') {

                    if (ValidatefileTC3ToUploadExtn()) {
                        check = false;
                    }


                    if (ValidateTC3TeamToUpload()) {
                        check = false;
                    }
                }
            }

            if (check) {
                if (confirm("Would you like to upload TC?")) {
                    document.getElementById('<%=hdPostingConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdPostingConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }



        function ValidateAllFiles() {
            var check = false;

            var emptyCount = 0;
            var nonUploadedCount = 0;

            var hdTC1SeqNo = 0;
            var hdTC2SeqNo = 0;
            var hdTC3SeqNo = 0;

            var fileTC1ToUpload = 0;
            var fileTC2ToUpload = 0;
            var fileTC3ToUpload = 0;

            if (document.getElementById('<%=hdTC1SeqNo.ClientID %>') != null) {
                hdTC1SeqNo = document.getElementById('<%=hdTC1SeqNo.ClientID %>').value;
            }

            if (document.getElementById('<%=hdTC2SeqNo.ClientID %>') != null) {
                hdTC2SeqNo = document.getElementById('<%=hdTC2SeqNo.ClientID %>').value;
            }

            if (document.getElementById('<%=hdTC3SeqNo.ClientID %>') != null) {
                hdTC3SeqNo = document.getElementById('<%=hdTC3SeqNo.ClientID %>').value;
            }



            if (document.getElementById('<%=hdTC1SeqNo.ClientID %>') != null) {
                hdTC1SeqNo = document.getElementById('<%=hdTC1SeqNo.ClientID %>').value;
            }

            if (document.getElementById('<%=hdTC2SeqNo.ClientID %>') != null) {
                hdTC2SeqNo = document.getElementById('<%=hdTC2SeqNo.ClientID %>').value;
            }

            if (document.getElementById('<%=hdTC3SeqNo.ClientID %>') != null) {
                hdTC3SeqNo = document.getElementById('<%=hdTC3SeqNo.ClientID %>').value;
            }

            if (parseInt(hdTC1SeqNo) == 0) {
                emptyCount = emptyCount + 1;
            }

            if (parseInt(hdTC2SeqNo) == 0) {
                emptyCount = emptyCount + 1;
            }

            if (parseInt(hdTC3SeqNo) == 0) {
                emptyCount = emptyCount + 1;
            }


            if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != null) {
                fileTC1ToUpload = document.getElementById('<%=fileTC1ToUpload.ClientID %>').value;

                if (fileTC1ToUpload == '') {
                    nonUploadedCount = nonUploadedCount + 1;
                }
            }

            if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != null) {
                fileTC2ToUpload = document.getElementById('<%=fileTC2ToUpload.ClientID %>').value;

                if (fileTC2ToUpload == '') {
                    nonUploadedCount = nonUploadedCount + 1;
                }
            }

            if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != null) {
                fileTC3ToUpload = document.getElementById('<%=fileTC3ToUpload.ClientID %>').value;

                if (fileTC3ToUpload == '') {
                    nonUploadedCount = nonUploadedCount + 1;
                }
            }

            if (emptyCount > 0) {

                for (var i = 1; i <= emptyCount; i++) {

                    if (parseInt(hdTC1SeqNo) == 0 && parseInt(document.getElementById('<%=hdUploadedFlag.ClientID %>').value) == 0) {

                        if (emptyCount == nonUploadedCount) {
                            if (ValidatefileTC1ToUpload()) {
                                check = true;
                            }
                        }

                        if (check) {
                            break;
                        }
                        else {

                            if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != null) {

                                if (document.getElementById('<%=fileTC1ToUpload.ClientID %>') != '') {
                                    document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "1";
                                }
                            }
                            else {
                                document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "0";
                            }
                        }

                    }

                    else if (parseInt(hdTC2SeqNo) == 0 && parseInt(document.getElementById('<%=hdUploadedFlag.ClientID %>').value) == 0) {


                        if (emptyCount == nonUploadedCount) {
                            if (ValidatefileTC2ToUpload()) {
                                check = true;
                            }
                        }

                        if (check) {
                            break;
                        }
                        else {
                            if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != null) {

                                if (document.getElementById('<%=fileTC2ToUpload.ClientID %>') != '') {
                                    document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "1";
                                }
                            }
                            else {
                                document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "0";
                            }
                        }

                    }

                    else if (parseInt(hdTC3SeqNo) == 0 && parseInt(document.getElementById('<%=hdUploadedFlag.ClientID %>').value) == 0) {


                        if (emptyCount == nonUploadedCount) {
                            if (ValidatefileTC3ToUpload()) {
                                check = true;
                            }
                        }

                        if (check) {
                            break;
                        }
                        else {
                            if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != null) {

                                if (document.getElementById('<%=fileTC3ToUpload.ClientID %>') != '') {
                                    document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "1";
                                }
                            }
                            else {
                                document.getElementById('<%=hdUploadedFlag.ClientID %>').value = "0";
                            }
                        }

                    }
                }
            }

            return check;
        }


        function ValidateAllSearch() {

            var check = true;
            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }

    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <script type="text/javascript">
        function CheckForIdentityRemarks() {
            var checkIdentity = document.getElementById('<%=chkIsCompleted.ClientID %>').checked;
            document.getElementById('<%=txtIscompletedRemarks.ClientID %>').value = "";
            if (checkIdentity) {
                document.getElementById('<%=txtIscompletedRemarks.ClientID %>').disabled = false;
            }
            else {
                document.getElementById('<%=txtIscompletedRemarks.ClientID %>').disabled = true;
            }
        }
    </script>


    <script type="text/javascript">        
        var GridId = "<%=gvPOList.ClientID %>";
        var ScrollHeight = 380;
        window.onload = function () {
            if (GridId != null) {

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
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hdPostingConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDeletionConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Procurement Status- Upload TC:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>
                    <label>End Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </asp:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>PO No.:</label>
                    <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Vendor Name:</label>
                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Company:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" />

                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Checked By:</label>
                    <asp:DropDownList ID="ddlCheckedBy" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Item Code:</label>
                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>

                    <label>Exclude CIDF:</label>
                    <asp:CheckBox ID="chkExcludeCIDF" runat="server" />

                    <label>Exclude Engineering Service:</label>
                    <asp:CheckBox ID="chkExcludeEngineeringService" runat="server" />

                </div>
            </fieldset>
            <div class="full-width">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClientClick="return ValidateAllSearch();" OnClick="btnSearch_Click" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                    OnClick="btnExport_Click" />
            </div>
        </div>
        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>


            <asp:GridView ID="gvPOList" CssClass="employee-grid"
                runat="server" CellPadding="4" ForeColor="#333333"
                AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="gvPOList_RowDataBound"
                OnRowCommand="gvPOList_RowCommand">
                <Columns>

                    <asp:TemplateField HeaderText="View PO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewPO" Height="30px" Width="30px" CommandArgument="ViewPO"
                                runat="server" ImageUrl="~/Images/pdficon3.png" ToolTip="View PO in PDF" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Upload TC" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnUploadTC" CommandArgument="UPLOAD_TC" ToolTip="Upload TC" runat="server"
                                Text="Upload TC" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox ID="txtStatus" runat="server" Enabled="false" CssClass="textboxtstatustext" Width="150px"
                                Text='<%# Eval("STATUS_NAME") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TC1" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnTCAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                ImageUrl="~/Images/pdficon3.png"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TC2" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnTCAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                ImageUrl="~/Images/pdficon3.png"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="TC3" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnTCAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                ImageUrl="~/Images/pdficon3.png"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO No.">
                        <ItemTemplate>
                            <asp:Label ID="lblPORecordID" runat="server" Visible="false" Text='<%# Eval("PO_RECORD_ID") %>' />

                            <asp:Label ID="lblTCRecordID1" runat="server" Visible="false" Text='<%# Eval("TC_RECORD_ID1") %>' />
                            <asp:Label ID="lblTCRecordID2" runat="server" Visible="false" Text='<%# Eval("TC_RECORD_ID2") %>' />
                            <asp:Label ID="lblTCRecordID3" runat="server" Visible="false" Text='<%# Eval("TC_RECORD_ID3") %>' />

                            <asp:Label ID="lblTCStatusID1" runat="server" Visible="false" Text='<%# Eval("STATUS_ID1") %>' />
                            <asp:Label ID="lblTCStatusID2" runat="server" Visible="false" Text='<%# Eval("STATUS_ID2") %>' />
                            <asp:Label ID="lblTCStatusID3" runat="server" Visible="false" Text='<%# Eval("STATUS_ID3") %>' />

                            <asp:Label ID="lblTCAttachment1" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT_NAME1") %>' />
                            <asp:Label ID="lblTCAttachment2" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT_NAME2") %>' />
                            <asp:Label ID="lblTCAttachment3" runat="server" Visible="false" Text='<%# Eval("TC_ATTACHMENT_NAME3") %>' />

                            <asp:Label ID="lblAssignedToID1" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_TO_ID1") %>' />
                            <asp:Label ID="lblAssignedToID2" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_TO_ID2") %>' />
                            <asp:Label ID="lblAssignedToID3" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_TO_ID3") %>' />

                            <asp:Label ID="lblTCSeqNo1" runat="server" Visible="false" Text='<%# Eval("TC_SEQ_NO1") %>' />
                            <asp:Label ID="lblTCSeqNo2" runat="server" Visible="false" Text='<%# Eval("TC_SEQ_NO2") %>' />
                            <asp:Label ID="lblTCSeqNo3" runat="server" Visible="false" Text='<%# Eval("TC_SEQ_NO3") %>' />

                            <asp:Label ID="lblTC1IdentificationReferenceNo" runat="server" Visible="false" Text='<%# Eval("IDENTIFICATION_REFERENCE_NO1") %>' />
                            <asp:Label ID="lblTC2IdentificationReferenceNo" runat="server" Visible="false" Text='<%# Eval("IDENTIFICATION_REFERENCE_NO2") %>' />
                            <asp:Label ID="lblTC3IdentificationReferenceNo" runat="server" Visible="false" Text='<%# Eval("IDENTIFICATION_REFERENCE_NO3") %>' />

                            <asp:Label ID="lblUploadedRemarks1" runat="server" Visible="false" Text='<%# Eval("UPLOADED_REMARKS1") %>' />
                            <asp:Label ID="lblUploadedRemarks2" runat="server" Visible="false" Text='<%# Eval("UPLOADED_REMARKS2") %>' />
                            <asp:Label ID="lblUploadedRemarks3" runat="server" Visible="false" Text='<%# Eval("UPLOADED_REMARKS3") %>' />

                            <asp:Label ID="lblAcceptedRemarks1" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_REMARKS1") %>' />
                            <asp:Label ID="lblAcceptedRemarks2" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_REMARKS2") %>' />
                            <asp:Label ID="lblAcceptedRemarks3" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_REMARKS3") %>' />

                            <asp:Label ID="lblRejectedRemarks1" runat="server" Visible="false" Text='<%# Eval("REJECTED_REMARKS1") %>' />
                            <asp:Label ID="lblRejectedRemarks2" runat="server" Visible="false" Text='<%# Eval("REJECTED_REMARKS2") %>' />
                            <asp:Label ID="lblRejectedRemarks3" runat="server" Visible="false" Text='<%# Eval("REJECTED_REMARKS3") %>' />



                            <asp:Label ID="lblPONo" runat="server" Visible="true" Text='<%# Eval("PO_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO Date">
                        <ItemTemplate>
                            <asp:Label ID="lblPODate" runat="server" Visible="true" Text='<%# Eval("PO_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Vendor Name">
                        <ItemTemplate>
                            <asp:Label ID="lblVendorCode" runat="server" Visible="false" Text='<%# Eval("VENDOR_CODE") %>' />
                            <asp:Label ID="lblVendorName" runat="server" Visible="true" Text='<%# Eval("VENDOR_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB No.">
                        <ItemTemplate>
                            <asp:Label ID="lblJOBNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPOFirstItemName" runat="server" Visible="true" Text='<%# Eval("PO_FIRST_ITEM") %>' />
                            <asp:Label ID="lblPOFirstItemCode" runat="server" Visible="false" Text='<%# Eval("PO_FIRST_PRODCODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                            <asp:Label ID="lblUnit" runat="server" Visible="true" Text='<%# Eval("UNIT_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Checked By">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckedBy" runat="server" Visible="true" Text='<%# Eval("CHECKED_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>



        </div>

    </div>






    <%-- SHOW DETAIL START--%>
    <asp:Button ID="btnShowDetailFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="mpeDetail" runat="server" TargetControlID="btnShowDetailFile"
        PopupControlID="pnlViewDetailPopup" CancelControlID="imgBtnCancelDetailFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewDetailPopup" runat="server" CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>



        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>Upload PO TC(s)</legend>

                <div class="form-grid form-grid-2">

                    <label>PO No.:</label>
                    <asp:TextBox ID="txtPONoToUpload" runat="server"
                        CssClass="form-control"
                        Enabled="false"></asp:TextBox>

                    <label>PO Date:</label>
                    <asp:TextBox ID="txtPODateToUpload" runat="server"
                        CssClass="form-control"
                        Enabled="false"></asp:TextBox>

                    <label>Vendor Name:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%;">
                                <asp:TextBox ID="txtVendorNameToUpload" runat="server"
                                    CssClass="form-control"
                                    Enabled="false"></asp:TextBox></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtVendorCodeToUpload" runat="server"
                                    CssClass="form-control"
                                    Enabled="false"></asp:TextBox></td>
                        </tr>
                    </table>

                    <label>Item Name:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%;">
                                <asp:TextBox ID="txtItemNameToUpload" runat="server"
                                    CssClass="form-control"
                                    Enabled="false"></asp:TextBox></td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtItemCodeToUpload" runat="server"
                                    CssClass="form-control"
                                    Enabled="false"></asp:TextBox></td>
                        </tr>
                    </table>

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNoToUpload" runat="server"
                        CssClass="form-control"
                        Enabled="false"></asp:TextBox>

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnitToUpload" runat="server"
                        CssClass="form-control"
                        Enabled="false"></asp:TextBox>

                    <label>Is completed?</label>
                    <asp:CheckBox ID="chkIsCompleted" runat="server" onchange="CheckForIdentityRemarks()" />

                    <div class="full-width">
                        <label>Is completed Remarks:</label>
                        <asp:TextBox ID="txtIscompletedRemarks" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="2" Enabled="false" />
                    </div>
                </div>

                <div class="form-grid form-grid-2">

                    <div class="full-width">

                        <fieldset class="form-card">
                            <legend>TC1</legend>

                            <asp:HiddenField ID="hdUploadedFlag" runat="server" Value="0" />
                            <asp:Label ID="lblAttachOrViewTxtTC1" runat="server" Text="Attach " />(TC1):

                                <asp:Panel ID="pnlViewTC1" runat="server" Visible="false">

                                    <asp:HiddenField ID="hdRemovedTC1ID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdTC1SeqNo" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdTC1ToView" runat="server" Value="0" />
                                    <asp:TextBox ID="txtTC1ToView" runat="server"
                                        Enabled="false"
                                        CssClass="form-control" />

                                    <asp:ImageButton ID="imgBtnViewTC1ToView" runat="server" Height="30px" Width="30px"
                                        ImageUrl="~/Images/viewdetails.png" OnClick="imgBtnViewTC1ToView_Click" ToolTip="View" />

                                    <asp:ImageButton ID="imgBtnRemoveTC1ToView" runat="server" Height="25px" Width="25px"
                                        ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveTC1ToView_Click" ToolTip="Remove" />

                                </asp:Panel>

                            <asp:Panel ID="pnlUploadTC1ToUpload" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdFileTC1ToUpload" runat="server" Value="0" />

                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="fileTC1ToUpload" runat="server"
                                                            CssClass="form-control"
                                                            BorderStyle="Groove" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="divfileTC1ToUpload" style="display: none;">
                                                            <asp:Label ID="lblfileTC1ToUpload" runat="server" ForeColor="Red" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndofileTC1ToUpload" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndofileTC1ToUpload_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <label>Assigned To (TC1):</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlTC1TeamToUpload" runat="server"
                                            CssClass="form-control" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnStatus1" Height="50px" Width="50px" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>

                            <label>Remarks (TC1):</label>
                            <asp:TextBox ID="txtTC1RemarksToUpload" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />

                            <label>Identification Reference No. (TC1):</label>
                            <asp:TextBox ID="txtTC1IdentificationReferenceNo" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />

                        </fieldset>
                    </div>

                </div>



                <div class="form-grid form-grid-2">

                    <div class="full-width">

                        <fieldset class="form-card">
                            <legend>TC2</legend>

                            <asp:Label ID="lblAttachOrViewTxtTC2" runat="server" Text="Attach " />(TC2):                                    
                            <asp:Panel ID="pnlViewTC2" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdRemovedTC2ID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdTC2SeqNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdTC2ToView" runat="server" Value="0" />
                                            <asp:TextBox ID="txtTC2ToView" runat="server"
                                                Enabled="false" CssClass="form-control" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewTC2ToView" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/viewdetails.png" OnClick="imgBtnViewTC2ToView_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveTC2ToView" runat="server" Height="25px" Width="25px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveTC2ToView_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadTC2ToUpload" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdFileTC2ToUpload" runat="server" Value="0" />

                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="fileTC2ToUpload" runat="server"
                                                            CssClass="form-control"
                                                            BorderStyle="Groove" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="divfileTC2ToUpload" style="display: none;">
                                                            <asp:Label ID="lblfileTC2ToUpload" runat="server" ForeColor="Red" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndofileTC2ToUpload" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndofileTC2ToUpload_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <label>Assigned To (TC2):</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlTC2TeamToUpload" runat="server"
                                            CssClass="form-control" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnStatus2" Height="50px" Width="50px" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>

                            <label>Remarks (TC2):</label>
                            <asp:TextBox ID="txtTC2RemarksToUpload" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />

                            <label>Identificaton Reference No. (TC2):</label>
                            <asp:TextBox ID="txtTC2IdentificationReferenceNo" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />
                        </fieldset>
                    </div>
                </div>


                <div class="form-grid form-grid-2">

                    <div class="full-width">

                        <fieldset class="form-card">
                            <legend>TC3</legend>

                            <asp:Label ID="lblAttachOrViewTxtTC3" runat="server" Text="Attach " />(TC3):
                            <asp:Panel ID="pnlViewTC3" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 75%;">
                                            <asp:HiddenField ID="hdRemovedTC3ID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdTC3SeqNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdTC3ToView" runat="server" Value="0" />
                                            <asp:TextBox ID="txtTC3ToView" runat="server"
                                                Enabled="false"
                                                CssClass="form-control" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnViewTC3ToView" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/viewdetails.png" OnClick="imgBtnViewTC3ToView_Click" ToolTip="View" />
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnRemoveTC3ToView" runat="server" Height="25px" Width="25px"
                                                ImageUrl="~/Images/NEWICONS/Deleted01.png" OnClick="imgBtnRemoveTC3ToView_Click" ToolTip="Remove" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlUploadTC3ToUpload" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 90%;">
                                            <asp:HiddenField ID="hdFileTC3ToUpload" runat="server" Value="0" />
                                            <table width1="100%">
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="fileTC3ToUpload" runat="server"
                                                            CssClass="form-control"
                                                            BorderStyle="Groove" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="divfileTC3ToUpload" style="display: none;">
                                                            <asp:Label ID="lblfileTC3ToUpload" runat="server" ForeColor="Red" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:ImageButton ID="imgBtnUndofileTC3ToUpload" runat="server" Height="30px" Width="30px"
                                                ImageUrl="~/Images/LOT/undo2.png" OnClick="imgBtnUndofileTC3ToUpload_Click" ToolTip="Undo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <label>Assigned To (TC3):</label>
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlTC3TeamToUpload" runat="server"
                                            CssClass="form-control" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnStatus3" Height="50px" Width="50px" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                            </table>

                            <label>Remarks (TC3):</label>
                            <asp:TextBox ID="txtTC3RemarksToUpload" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />

                            <label>Identification Reference No. (TC3):</label>
                            <asp:TextBox ID="txtTC3IdentificationReferenceNo" runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine" Rows="2" />
                        </fieldset>
                    </div>
                </div>


            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpload" CssClass="button" Width="100%" runat="server" Text="Upload TC"
                    OnClick="btnUpload_Click" OnClientClick="return ValidateAllUpload();" />
            </div>

        </div>

    </asp:Panel>
    <%-- SHOW DETAIL END--%>

    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
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
            id="iframeViewPOInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
