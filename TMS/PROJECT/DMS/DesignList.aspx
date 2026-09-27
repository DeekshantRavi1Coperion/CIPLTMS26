<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="DesignList.aspx.cs"
    Inherits="PROJECT_DMS_DesignList" Title="CIPLTMS- Design List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        .textboxcenter {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxright {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            height: 26px;
            /*background-color: transparent;*/
            border-color: lightblue;
            /*background-color: #D8D8D8;*/
        }

        .textboxleft {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: transparent;*/
            border-color: lightblue;
        }
    </style>


    <script type="text/javascript">

        function pageLoad() {

            if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }

            document.getElementById('<%=txtDateByProjectTeamToEdit.ClientID %>').value = document.getElementById('<%=hdDateByProjectTeamToEdit.ClientID %>').value;

            document.getElementById('<%=txtPlannedStartDateByDesignTeamToEdit.ClientID %>').value = document.getElementById('<%=hdPlannedStartDateByDesignTeamToEdit.ClientID %>').value;
            document.getElementById('<%=txtPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value = document.getElementById('<%=hdPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value;
            document.getElementById('<%=txtExpectedCompletionDateToEdit.ClientID %>').value = document.getElementById('<%=hdExpectedCompletionDateToEdit.ClientID %>').value;
        }

        function clientChangedDateByProjectTeamToEdit(sender, args) {
            document.getElementById('<%=hdDateByProjectTeamToEdit.ClientID %>').value = document.getElementById('<%=txtDateByProjectTeamToEdit.ClientID %>').value;
        }

        function clientChangedPlannedStartDateByDesignTeamToEdit(sender, args) {
            document.getElementById('<%=hdPlannedStartDateByDesignTeamToEdit.ClientID %>').value = document.getElementById('<%=txtPlannedStartDateByDesignTeamToEdit.ClientID %>').value;
        }

        function clientChangedPlannedCompletionDateByDesignTeamToEdit(sender, args) {
            document.getElementById('<%=hdPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value = document.getElementById('<%=txtPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value;
        }

        function clientChangedExpectedCompletionDateToEdit(sender, args) {
            document.getElementById('<%=hdExpectedCompletionDateToEdit.ClientID %>').value = document.getElementById('<%=txtExpectedCompletionDateToEdit.ClientID %>').value;
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

        function ValidateAllSearch() {

            if (ValidateDateRange()) {
                return false;
            }

            return true;
        }
    </script>



    <script type="text/javascript">

        function EnableEndDateSearch() {
            var sign = document.getElementById('<%=ddlSignMainSearch.ClientID %>');
            var signText = sign.options[sign.selectedIndex].innerHTML;

            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";

            var signindex = sign.selectedIndex;

            if (signText == "BETWEEN" || signindex == 0) {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = false;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = false;

                if (document.getElementById('<%=chkSelectDates.ClientID %>').checked) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else {
                document.getElementById('<%=txtEndDateSearch.ClientID %>').disabled = true;
                document.getElementById('<%=imgbtnEndDateSearch.ClientID %>').disabled = true;
            }
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




    <script type="text/javascript">

        function ValidateProjectCategoryInTimesheet() {
            var ProjectCatetory = document.getElementById('<%=ddlProjectCategoryInTimesheet.ClientID %>').selectedIndex;
            if (ProjectCatetory == '' || ProjectCatetory == '0') {
                document.getElementById('<%=ddlProjectCategoryInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectCategoryInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProjectNumberInTimesheet() {
            var ProjectNumber = document.getElementById('<%=ddlProjectNumberInTimesheet.ClientID %>').selectedIndex;
            if (ProjectNumber == '' || ProjectNumber == '0') {
                document.getElementById('<%=ddlProjectNumberInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNumberInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCategoryInTimesheet() {
            var Category = document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>').selectedIndex;
            if (Category == '' || Category == '0') {
                document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSerialNumberInTimesheet() {
            var SerialNumber = document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>').selectedIndex;
            if (SerialNumber == '' || SerialNumber == '0') {
                document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSizeInTimesheet() {
            var Size = document.getElementById('<%=ddlSizeInTimesheet.ClientID %>').selectedIndex;
            if (Size == '' || Size == '0') {
                document.getElementById('<%=ddlSizeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSizeInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRevInTimesheet() {
            var Rev = document.getElementById('<%=ddlRevInTimesheet.ClientID %>').selectedIndex;
            if (Rev == '' || Rev == '0') {
                document.getElementById('<%=ddlRevInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRevInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSheetsInTimesheet() {
            var Sheets = document.getElementById('<%=txtSheetsInTimesheet.ClientID %>').value;
            if (Sheets == '') {
                document.getElementById('<%=txtSheetsInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSheetsInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingIDInTimesheet() {
            var DrawingID = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value;
            if (DrawingID == '') {
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJOBNoInTimesheet() {
            var jobNo = document.getElementById('<%=txtJOBNoInTimesheet.ClientID %>').value;
            if (jobNo == '') {
                document.getElementById('<%=txtJOBNoInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateTypeOfDrawingInTimesheet() {
            var TypeOfDrawing = document.getElementById('<%=ddlTypeOfDrawingInTimesheet.ClientID %>').selectedIndex;
            if (TypeOfDrawing == '' || TypeOfDrawing == '0') {
                document.getElementById('<%=ddlTypeOfDrawingInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfDrawingInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateStartTimeInTimesheet() {
            var StartTime = document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(StartTime);
            if (StartTime == '' || StartTime == '__:__' || StartTime == '00:00') {
                document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

        function ValidateEndTimeInTimesheet() {
            var EndTime = document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(EndTime);
            if (EndTime == '' || EndTime == '__:__' || EndTime == '00:00') {
                document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

        function ValidateTimeSpentInTimesheet() {
            var TimeSpent = document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(TimeSpent);
            if (TimeSpent == '' || TimeSpent == '00:00') {
                document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }


        function ValidateTimeSpentInTimesheet() {
            var TimeSpent = document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').value;
            if (TimeSpent == '' || TimeSpent == "00:00") {
                document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateWorkDescriptionInTimesheet() {
            var WorkDescription = document.getElementById('<%=txtWorkDescriptionInTimesheet.ClientID %>').value;
            if (WorkDescription == '') {
                document.getElementById('<%=txtWorkDescriptionInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtWorkDescriptionInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }


    </script>

    <script type="text/javascript">

        function ValidateAllTimesheet() {
            var check = true;



            if (document.getElementById('<%=dvDrawing.ClientID %>').style.display == 'block') {

                if (ValidateProjectCategoryInTimesheet()) {
                    check = false;
                }

                if (ValidateProjectNumberInTimesheet()) {
                    check = false;
                }

                if (ValidateCategoryInTimesheet()) {
                    check = false;
                }

                if (ValidateSerialNumberInTimesheet()) {
                    check = false;
                }

                if (ValidateSizeInTimesheet()) {
                    check = false;
                }

                if (ValidateRevInTimesheet()) {
                    check = false;
                }

                if (ValidateSheetsInTimesheet()) {
                    check = false;
                }

                if (ValidateDrawingIDInTimesheet()) {
                    check = false;
                }

                if (ValidateJOBNoInTimesheet()) {
                    check = false;
                }

                if (ValidateTypeOfDrawingInTimesheet()) {
                    check = false;
                }


            }



            if (ValidateStartTimeInTimesheet()) {
                check = false;
            }

            if (ValidateEndTimeInTimesheet()) {
                check = false;
            }

            if (ValidateTimeRangeInTimesheet()) {
                check = false;
            }



            if (ValidateTimeSpentInTimesheet()) {
                check = false;
            }

            if (ValidateWorkDescriptionInTimesheet()) {
                check = false;
            }




            if (check) {
                if (confirm("Would you like to submit timesheet?")) {
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

        function ValidateTimeRangeInTimesheet() {
            var StartTime = document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').value;

            if (StartTime == '') {
                StartTime = "00:00";
            }

            var StartTimeHRS = Number(StartTime.match(/^(\d+)/)[1]);
            var StartTimeMNTS = Number(StartTime.match(/:(\d+)/)[1]);

            var StartTimeHours = StartTimeHRS.toString();
            var StartTimeMinutes = StartTimeMNTS.toString();

            if (StartTimeHRS < 10) StartTimeHours = "0" + StartTimeHours;
            if (StartTimeMNTS < 10) StartTimeMinutes = "0" + StartTimeMinutes;

            var date1 = new Date();
            date1.setHours(StartTimeHours);
            date1.setMinutes(StartTimeMinutes);



            var EndTime = document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').value;

            if (EndTime == '') {
                EndTime = "00:00";
            }

            var EndTimeHRS = Number(EndTime.match(/^(\d+)/)[1]);
            var EndTimeMNTS = Number(EndTime.match(/:(\d+)/)[1]);

            var EndTimeHours = EndTimeHRS.toString();
            var EndTimeMinutes = EndTimeMNTS.toString();

            if (EndTimeHRS < 10) EndTimeHours = "0" + EndTimeHours;
            if (EndTimeMNTS < 10) EndTimeMinutes = "0" + EndTimeMinutes;

            var date2 = new Date();
            date2.setHours(EndTimeHours);
            date2.setMinutes(EndTimeMinutes);

            var diff = date2.getTime() - date1.getTime();

            var hours = Math.floor(diff / (1000 * 60 * 60));
            diff -= hours * (1000 * 60 * 60);

            var mins = Math.floor(diff / (1000 * 60));
            diff -= mins * (1000 * 60);

            if (hours < 10) hours = "0" + hours;
            if (mins < 10) mins = "0" + mins;


            if (date2.getTime() < date1.getTime()) {
                document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>

    <script type="text/javascript">

        function StartTime() {

            var hour = document.getElementById('<%=ddlStartTimeHInTimesheet.ClientID %>');
            var minute = document.getElementById('<%=ddlStartTimeMInTimesheet.ClientID %>');

            var H;
            var M;

            H = hour.options[hour.selectedIndex].value;
            M = minute.options[minute.selectedIndex].value;

            if (parseInt(H) < 10) {
                H = "0" + H;
            }

            if (parseInt(M) < 10) {
                M = "0" + M;
            }

            document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').value = H + ":" + M;


            GetTotalTimespent(document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').value, document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').value)

        }

        function EndTime() {

            var hour = document.getElementById('<%=ddlEndTimeHInTimesheet.ClientID %>');
            var minute = document.getElementById('<%=ddlEndTimeMInTimesheet.ClientID %>');

            var H;
            var M;

            H = hour.options[hour.selectedIndex].value;
            M = minute.options[minute.selectedIndex].value;

            if (parseInt(H) < 10) {
                H = "0" + H;
            }

            if (parseInt(M) < 10) {
                M = "0" + M;
            }

            document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').value = H + ":" + M;

            GetTotalTimespent(document.getElementById('<%=txtStartTimeInTimesheet.ClientID %>').value, document.getElementById('<%=txtEndTimeInTimesheet.ClientID %>').value)
        }

        function GetTotalTimespent(startTime, endTime) {
            var startHours;
            var startMinutes;
            var startTotalMinutes;

            startHours = startTime.split(':')[0];
            startMinutes = startTime.split(':')[1];
            startTotalMinutes = (parseInt(startHours) * 60 + parseInt(startMinutes));


            var endHours;
            var endMinutes;
            var endTotalMinutes;

            endHours = endTime.split(':')[0];
            endMinutes = endTime.split(':')[1];
            endTotalMinutes = (parseInt(endHours) * 60 + parseInt(endMinutes));

            var totalSpentMinutes;
            totalSpentMinutes = (endTotalMinutes - startTotalMinutes);

            var spentHours;
            var spentMinutes;

            spentHours = parseInt(totalSpentMinutes / 60);
            spentMinutes = parseInt(totalSpentMinutes % 60);

            if (parseInt(spentHours) > 0) {
                if (parseInt(spentHours) < 10) {
                    spentHours = "0" + spentHours;
                }
            }
            else {
                spentHours = "00";
            }

            if (parseInt(spentMinutes) > 0) {
                if (parseInt(spentMinutes) < 10) {
                    spentMinutes = "0" + spentMinutes;
                }
            }
            else {
                spentMinutes = "00";
            }

            document.getElementById('<%=txtTimeSpentInTimesheet.ClientID %>').value = spentHours + ":" + spentMinutes;
        }

    </script>


    <script type="text/javascript">
        function GetCategoryText() {
            var ddlCategory = document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>');
            var ddlCategory = ddlCategory.options[ddlCategory.selectedIndex].text;
            if (ddlCategory == 'Select' || ddlCategory == '')
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value + '.' + ddlCategory + "."
            }
        }

        function GetSerialNumberText() {
            var ddlSerialNumber = document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>');
            var ddlSerialNumber = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text;
            if (ddlSerialNumber == 'Select' || ddlSerialNumber == '')
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value + ddlSerialNumber + '.';
            }
        }

        function GetSizeText() {
            var ddlSize = document.getElementById('<%=ddlSizeInTimesheet.ClientID %>');
            var ddlSize = ddlSize.options[ddlSize.selectedIndex].text;
            if (ddlSize == 'Select' || ddlSize == '')
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value + ddlSize + '.';
            }
        }
    </script>

    <script type="text/javascript">
        function GetDrawingID() {

            var ddlProjectCategory = document.getElementById('<%=ddlProjectCategoryInTimesheet.ClientID %>');
            var ddlProjectCategoryIndex = document.getElementById('<%=ddlProjectCategoryInTimesheet.ClientID %>').selectedIndex;
            var ddlProjectCategoryText = ddlProjectCategory.options[ddlProjectCategory.selectedIndex].text;
            if (ddlProjectCategoryIndex == '' || ddlProjectCategoryIndex == '0') {
                var ddlProjectCategoryText = ''
            }
            else {
                var ddlProjectCategoryText = ddlProjectCategory.options[ddlProjectCategory.selectedIndex].text;
            }



            var ddlProjectNumber = document.getElementById('<%=ddlProjectNumberInTimesheet.ClientID %>');
            var ddlProjectNumberIndex = document.getElementById('<%=ddlProjectNumberInTimesheet.ClientID %>').selectedIndex;
            var ddlProjectNumberText = ddlProjectNumber.options[ddlProjectNumber.selectedIndex].text;
            if (ddlProjectNumberIndex == '' || ddlProjectNumberIndex == '0') {
                var ddlProjectNumberText = '';
            }
            else {
                var ddlProjectNumberText = ddlProjectNumber.options[ddlProjectNumber.selectedIndex].text + '.';
            }

            var ddlCategory = document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>');
            var ddlCategoryIndex = document.getElementById('<%=ddlCategoryInTimesheet.ClientID %>').selectedIndex;
            var ddlCategoryText = ddlCategory.options[ddlCategory.selectedIndex].text;
            if (ddlCategoryIndex == '' || ddlCategoryIndex == '0') {
                var ddlCategoryText = '';
            }
            else {
                var ddlCategoryText = ddlCategory.options[ddlCategory.selectedIndex].text + '.';
            }



            var ddlSerialNumber = document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>');
            var ddlSerialNumberIndex = document.getElementById('<%=ddlSerialNumberInTimesheet.ClientID %>').selectedIndex;
            var ddlSerialNumberText = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text;
            if (ddlSerialNumberIndex == '' || ddlSerialNumberIndex == '0') {
                var ddlSerialNumberText = '';
            }
            else {
                var ddlSerialNumberText = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text + '.';
            }



            var ddlSize = document.getElementById('<%=ddlSizeInTimesheet.ClientID %>');
            var ddlSizeIndex = document.getElementById('<%=ddlSizeInTimesheet.ClientID %>').selectedIndex;
            var ddlSizeText = ddlSize.options[ddlSize.selectedIndex].text;
            if (ddlSizeIndex == '' || ddlSizeIndex == '0') {
                var ddlSizeText = '';
            }
            else {
                var ddlSizeText = ddlSize.options[ddlSize.selectedIndex].text + '.';
            }



            var ddlRev = document.getElementById('<%=ddlRevInTimesheet.ClientID %>');
            var ddlRevIndex = document.getElementById('<%=ddlRevInTimesheet.ClientID %>').selectedIndex;
            var ddlRevText = ddlRev.options[ddlRev.selectedIndex].text;
            if (ddlRevIndex == '' || ddlRevIndex == '0') {
                var ddlRevText = '';
            }
            else {
                var ddlRevText = ddlRev.options[ddlRev.selectedIndex].text + '.';
            }


            var sheets = document.getElementById('<%=txtSheetsInTimesheet.ClientID %>').value;
            if (sheets == '') {
                var sheetsText = '';
            }
            else {
                var sheetsText = document.getElementById('<%=txtSheetsInTimesheet.ClientID %>').value;
            }

            document.getElementById('<%=hdDrawingID.ClientID %>').value = ddlProjectCategoryText + ddlProjectNumberText +
                ddlCategoryText + ddlSerialNumberText +
                ddlSizeText + ddlRevText + sheetsText;

            document.getElementById('<%=txtDrawingIDInTimesheet.ClientID %>').value = document.getElementById('<%=hdDrawingID.ClientID %>').value;

        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
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
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>




    <script type="text/Javascript">

        function ValidateUpdateStatus() {
            var UpdateStatus = document.getElementById('<%=ddlUpdateStatus.ClientID %>').selectedIndex;
            if (UpdateStatus == '' || UpdateStatus == '0') {
                document.getElementById('<%=ddlUpdateStatus.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUpdateStatus.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAllUpdateStatus() {
            var check = true;

            if (ValidateUpdateStatus()) {
                check = false;
            }

            return check;

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

        function ValidateJobNo() {
            var JOBNo = document.getElementById('<%=txtJOBNoToEdit.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCategory() {
            var CategoryItems = document.getElementById('<%=ddlCategoryToEdit.ClientID %>').selectedIndex;

            if (CategoryItems == '' || CategoryItems == '0') {
                document.getElementById('<%=ddlCategoryToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryToEdit.ClientID %>').style.borderColor = "";
                return false;
            }

            <%--var DesignResponsibleEnggFlag = document.getElementById('<%=hdDesignResponsibleEnggFlag.ClientID %>').value;
            var CategoryItems = document.getElementById('<%=ddlCategoryToEdit.ClientID %>').selectedIndex;

            if (DesignResponsibleEnggFlag==0) {
                if (CategoryItems == '' || CategoryItems == '0') {
                    document.getElementById('<%=ddlCategoryToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlCategoryToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=ddlCategoryToEdit.ClientID %>').style.borderColor = "";
                return false;
            }--%>
        }

        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescriptionToEdit.ClientID %>').value;
            if (Description == '') {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUOM() {
            var UOM = document.getElementById('<%=txtUOMToEdit.ClientID %>').value;
            if (UOM == '') {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtUOMToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantityToEdit.ClientID %>').value;
            if (Quantity == '' || parseInt(Quantity) == 0) {
                document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantityToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlannedStartDateByDesignTeam() {
            var PlannedStartDateByDesignTeam = document.getElementById('<%=txtPlannedStartDateByDesignTeamToEdit.ClientID %>').value;
            if (PlannedStartDateByDesignTeam == '') {
                document.getElementById('<%=txtPlannedStartDateByDesignTeamToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlannedStartDateByDesignTeamToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePlannedCompletionDateByDesignTeam() {
            var PlannedCompletionDateByDesignTeam = document.getElementById('<%=txtPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value;
            if (PlannedCompletionDateByDesignTeam == '') {
                document.getElementById('<%=txtPlannedCompletionDateByDesignTeamToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPlannedCompletionDateByDesignTeamToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingNumber() {
            var DrawingNumber = document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').value;
            if (DrawingNumber == '') {
                document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingNumberToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDocumentLink() {
            var DesignResponsibleEnggFlag = document.getElementById('<%=hdDesignResponsibleEnggFlag.ClientID %>').value;
            var DrawingLink = document.getElementById('<%=txtDocumentLinkToEdit.ClientID %>').value;

            if (DesignResponsibleEnggFlag == 0) {
                if (DrawingLink == '') {
                    document.getElementById('<%=txtDocumentLinkToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDocumentLinkToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }
            else {
                document.getElementById('<%=txtDocumentLinkToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateResponsibleDesignEngineer() {
            var ResponsibleDesignEngineer = document.getElementById('<%=ddlRespDesignEnggToEdit.ClientID %>').selectedIndex;
            if (ResponsibleDesignEngineer == '' || ResponsibleDesignEngineer == '0') {
                document.getElementById('<%=ddlRespDesignEnggToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRespDesignEnggToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateWorkingStatus() {
            var WorkingStatus = document.getElementById('<%=txtWorkingStatusToEdit.ClientID %>').value;
            if (WorkingStatus == '') {
                document.getElementById('<%=txtWorkingStatusToEdit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtWorkingStatusToEdit.ClientID %>').style.borderColor = "";
                return false;
            }
        }



    </script>


    <script type="text/Javascript">
        function ValidateDateRangeToEdit() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdPlannedStartDateByDesignTeamToEdit.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdPlannedCompletionDateByDesignTeamToEdit.ClientID %>').value.split("-");
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


    <script type="text/javascript">

        function ValidateAllToEdit() {
            var check = true;
            var IsPlanned = 0;

            var DesignResponsibleEnggFlag = document.getElementById('<%=hdDesignResponsibleEnggFlag.ClientID %>').value;

            if (ValidateJobNo()) {
                check = false;
            }

            if (ValidateDescription()) {
                check = false;
            }

            if (ValidateUOM()) {
                check = false;
            }

            if (ValidateQuantity()) {
                check = false;
            }

            if (ValidateDrawingNumber()) {
                check = false;
            }

            if (parseInt(DesignResponsibleEnggFlag) == 0) {

                if (ValidateCategory()) {
                    check = false;
                }

                if (ValidateDocumentLink()) {
                    check = false;
                }
            }



            if (parseInt(DesignResponsibleEnggFlag) > 0) {

                if (ValidatePlannedStartDateByDesignTeam()) {
                    check = false;
                }

                if (ValidatePlannedCompletionDateByDesignTeam()) {
                    check = false;
                }

                if (ValidateDateRange()) {
                    return false;
                }

                if (ValidateResponsibleDesignEngineer()) {
                    check = false;
                }

                if (ValidateWorkingStatus()) {
                    check = false;
                }
            }

            if (check) {
                if (confirm("Would you like to update design details?")) {
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
        function ValidateAllJobNo() {

            var check = true;

            if (ValidateJobNo()) {
                check = false;
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


    <script type="text/Javascript">
        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectDates.ClientID %>').checked;
            var datetype = document.getElementById('<%=ddlSignMainSearch.ClientID %>').selectedIndex;
            if (chk == true) {
                if (datetype == 0) {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                    document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
                }
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
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
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDesignResponsibleEnggFlag" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Design Detailed List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">


                    <label>Date Filter:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlOnWhichDateMainSearch" runat="server" Width=""
                                    CssClass="form-control">
                                    <asp:ListItem Text="Reqd. Date By Project Team" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Expected Completion Date" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Planned Dates By Design Team" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSignMainSearch" runat="server"
                                    CssClass="form-control"
                                    onchange="EnableEndDateSearch()">
                                    <asp:ListItem Text="BETWEEN" Value="BETWEEN"></asp:ListItem>
                                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <label>Start Date:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
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
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control" />
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td style="width: 20%;" align="center">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                            <td style="width: 10%;">
                                <asp:CheckBox ID="chkSelectDates" runat="server" onchange="EnableDisableDates()" />
                            </td>
                        </tr>
                    </table>

                    <label>Drawing No.:</label>
                    <asp:TextBox ID="txtDrawingNoMainSearch" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Category:</label>
                    <asp:DropDownList ID="ddlCategoryMainSearch" runat="server"
                        CssClass="form-control" />

                    <label>JOB No.:</label>
                    <asp:TextBox ID="txtJOBNoMainSearch" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Status:</label>
                    <asp:DropDownList ID="ddlPostingStatusMainSearch" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Design Checker:</label>
                    <asp:DropDownList ID="ddlDesignCheckerMainSearch" runat="server"
                        CssClass="form-control" />

                    <label>Responsible Design Engineer:</label>
                    <asp:DropDownList ID="ddlRespDesignEnggMainSearch" runat="server"
                        CssClass="form-control" />

                    <label>Created By:</label>
                    <asp:DropDownList ID="ddlCreatedByMainSearch" runat="server"
                        CssClass="form-control" />

                    &nbsp;
                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                        OnClientClick="return ValidateAllSearch();" OnClick="btnSearch_Click" />

                    &nbsp;
                    <asp:Button ID="btnAddNew" CssClass="button" Width="100%" runat="server" Text="Add New Design"
                        OnClick="btnAddNew_Click" />


                    <label>Update Status:</label>
                    <asp:DropDownList ID="ddlUpdateStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    &nbsp;
                    <asp:Button ID="btnUpdateStatus" CssClass="button" runat="server" Text="Update"
                        OnClick="btnUpdateStatus_Click" OnClientClick="return ValidateAllUpdateStatus();" />

                </div>
            </fieldset>
        </div>

        <div class="employee-grid-container">

            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView ID="gvDesignDetails"
                CssClass="employee-grid"
                runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvDesignDetails_RowCommand"
                OnRowDataBound="gvDesignDetails_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="Sr.No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="50px"
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.(Rev.)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewAddAtt" CommandArgument="VIEW_ADD_ATT"
                                runat="server" ImageUrl="~/Images/pdficon1.png" Height="30PX" Width="30PX" ToolTip="View Additional Attachment" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" CommandArgument="VIEW_DETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png" Height="30PX" Width="30PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnSendMail" CommandArgument="SEND_MAIL" ToolTip="Send Mail"
                                runat="server" ImageUrl="~/Images/NEWICONS/email05.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Eval("STATUS") %>' Visible="false" />
                            <asp:Label ID="lblRecordID" runat="server" Text='<%# Eval("RECORD_ID") %>' Visible="false" />
                            <asp:Label ID="lblDrawingID" runat="server" Text='<%# Eval("DRAWING_ID") %>' Visible="false" />
                            <asp:Label ID="lblPMID" runat="server" Text='<%# Eval("PM_ID") %>' Visible="false" />

                            <asp:Label ID="lblIsRevised" runat="server" Text='<%# Eval("IS_REVISED") %>' Visible="false" />

                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />

                            <asp:Label ID="lblCreatedByID" runat="server" Text='<%# Eval("CREATED_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblOpenByID" runat="server" Text='<%# Eval("OPEN_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblSendToCheckingByID" runat="server" Text='<%# Eval("SENT_TO_CHECKING_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblCheckedByID" runat="server" Text='<%# Eval("CHECKED_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblClosedByID" runat="server" Text='<%# Eval("CLOSED_BY_ID") %>' Visible="false" />

                            <asp:Label ID="lblSentToAmendmentByID" runat="server" Text='<%# Eval("SENT_TO_AMENDMENT_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblAmendmentCount" runat="server" Text='<%# Eval("AMENDMENT_COUNT") %>' Visible="false" />
                            <asp:Label ID="lblAmendmentFlag" runat="server" Text='<%# Eval("AMENDMENT_FLAG") %>' Visible="false" />
                            <asp:Label ID="lblEditedFlag" runat="server" Text='<%# Eval("EDITED_FLAG") %>' Visible="false" />
                            <asp:Label ID="lblAmendedByID" runat="server" Text='<%# Eval("AMENDED_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblAmendedOpenByID" runat="server" Text='<%# Eval("AMENDED_OPEN_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblAmendedSendToCheckingByID" runat="server" Text='<%# Eval("AMENDED_SENT_TO_CHECKING_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblAmendedCheckedByID" runat="server" Text='<%# Eval("AMENDED_CHECKED_BY_ID") %>' Visible="false" />

                            <asp:Label ID="lblDesignResponsibleEnggID" runat="server" Text='<%# Eval("DESIGN_RESPONSIBLE_ENGG_ID") %>' Visible="false" />
                            <asp:Label ID="lblDesignResponsibleEngg" runat="server" Text='<%# Eval("DESIGN_RESPONSIBLE_ENGG") %>' Visible="false" />


                            <asp:Label ID="lblDesignResponsibleEnggEmpRecordID" runat="server" Text='<%# Eval("DESIGN_RESP_ENGG_EMP_RECORD_ID") %>' Visible="false" />

                            <asp:Label ID="lblDesignCheckerID" runat="server" Text='<%# Eval("DESIGN_CHECKER_ID") %>' Visible="false" />
                            <asp:Label ID="lblDesignChecker" runat="server" Text='<%# Eval("DESIGN_CHECKER") %>' Visible="false" />

                            <asp:Label ID="lblDesignCheckerEmpRecordID" runat="server" Text='<%# Eval("DESIGN_CHECKER_EMP_RECORD_ID") %>' Visible="false" />

                            <asp:Label ID="lblIsGeneratedMailSent" runat="server" Text='<%# Eval("IS_GENERATED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsOpenMailSent" runat="server" Text='<%# Eval("IS_OPEN_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsSendToCheckingMailSent" runat="server" Text='<%# Eval("IS_SENT_TO_CHECKING_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsCheckedMailSent" runat="server" Text='<%# Eval("IS_CHECKED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsClosedMailSent" runat="server" Text='<%# Eval("IS_CLOSED_MAIL_SENT") %>' Visible="false" />

                            <asp:Label ID="lblIsSendToAmendmentMailSent" runat="server" Text='<%# Eval("IS_SENT_TO_AMENDMENT_MAIL_SENT") %>' Visible="false" />

                            <asp:Label ID="lblIsAmendedMailSent" runat="server" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendedOpenMailSent" runat="server" Text='<%# Eval("IS_AMENDED_OPEN_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendedSendToCheckingMailSent" runat="server" Text='<%# Eval("IS_AMENDED_SENT_TO_CHECKING_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendedCheckedMailSent" runat="server" Text='<%# Eval("IS_AMENDED_CHECKED_MAIL_SENT") %>' Visible="false" />

                            <asp:Label ID="lblJOBUnit" runat="server" Text='<%# Eval("JOB_UNIT") %>' Visible="false" />
                            <asp:Label ID="lblJOBUnitID" runat="server" Text='<%# Eval("JOB_UNIT_ID") %>' Visible="false" />
                            <asp:Label ID="lblShortJOBNo" runat="server" Text='<%# Eval("SHORT_JOB_NO") %>' Visible="false" />

                            <asp:Label ID="lblAdditionalAttachmentName" runat="server" Text='<%# Eval("ADDITIONAL_ATTACHMENT_NAME") %>' Visible="false" />

                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" Height="40PX" Width="40PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" CommandArgument="EDIT" ToolTip="Edit design" runat="server"
                                Text="Edit" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="JOB No.">
                        <ItemTemplate>
                            <asp:Label ID="lblJOBNo" runat="server" Text='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="50px"
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reqd. Date By Project Team">
                        <ItemTemplate>
                            <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Planned Start Date By Design Team">
                        <ItemTemplate>
                            <asp:Label ID="lblPlannedStartDateByDesignTeam" runat="server" Visible="false" Text='<%# Eval("PLANNED_START_DATE_BY_DESIGN_TEAM") %>' />
                            <asp:TextBox ID="txtPlannedStartDateByDesignTeam" runat="server" Width="80%" Text='<%# Eval("PLANNED_START_DATE_BY_DESIGN_TEAM") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarPlannedStartDateByDesignTeam" PopupButtonID="imgbtnPlannedStartDateByDesignTeam"
                                runat="server" TargetControlID="txtPlannedStartDateByDesignTeam" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnPlannedStartDateByDesignTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Planned Start Date By Design Team Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Planned Completion Date By Design Team">
                        <ItemTemplate>
                            <asp:Label ID="lblPlannedCompletionDateByDesignTeam" runat="server" Visible="false" Text='<%# Eval("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM") %>' />
                            <asp:TextBox ID="txtPlannedCompletionDateByDesignTeam" runat="server" Width="80%" Text='<%# Eval("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarPlannedCompletionDateByDesignTeam" PopupButtonID="imgbtnPlannedCompletionDateByDesignTeam"
                                runat="server" TargetControlID="txtPlannedCompletionDateByDesignTeam" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnPlannedCompletionDateByDesignTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Planned Completion Date By Design Team Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drawing No.">
                        <ItemTemplate>
                            <asp:Label ID="lblDrawingNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Client Drawing No.">
                        <ItemTemplate>
                            <asp:Label ID="lblClientDrawingNo" runat="server" Text='<%# Eval("CLIENT_DRAWING_NO") %>' Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Contractor Drawing No.">
                        <ItemTemplate>
                            <asp:Label ID="lblContractorDrawingNo" runat="server" Text='<%# Eval("CONTRACTOR_DRAWING_NO") %>' Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Document Link">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumentLink" runat="server" Text='<%# Eval("DOCUMENT_LINK") %>' Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing Revisioin Number">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingRevNo" runat="server" Text='<%# Eval("DRAWING_REV_NO") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Working Status">
                        <ItemTemplate>
                            <asp:TextBox ID="txtWorkingStatus" runat="server" Text='<%# Eval("WORKING_STATUS") %>' Width="300PX" TextMode="MultiLine"
                                Rows="2" CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Expected Completion Date">
                        <ItemTemplate>
                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Visible="false" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' />
                            <asp:TextBox ID="txtExpectedCompletionDate" runat="server" Width="80%" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarExpectedCompletionDate" PopupButtonID="imgbtnExpectedCompletionDate"
                                runat="server" TargetControlID="txtExpectedCompletionDate" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnExpectedCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Expected Completion Date Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Hours Spent">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTimeSpent" runat="server" Text='<%# Eval("TOTAL_HOURS_SPENT") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Responsible Design Engineer">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlResponsibleDesignEngineer" runat="server" Width="100%" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drawing Link">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingLink" runat="server" Text='<%# Eval("DRAWING_LINK") %>' Width="300PX" CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Design Checker">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDesignChecker" runat="server" Width="200px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Width="300PX" TextMode="MultiLine"
                                CssClass="textboxleft"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--Text='<%# Eval("REMARKS") %>' onkeyDown="javascript:preventInput(event);" --%>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Button ID="btnAddTimesheet" CommandArgument="ADD_TIMESHEET" ToolTip="Add Timesheet" runat="server"
                                Text="Add Timesheet" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>

                            <asp:Button ID="btnAssign" CommandArgument="ASSIGN" ToolTip="Assign to responsible design engineer" runat="server"
                                Text="Assign" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            <asp:Button ID="btnSendToChecking" CommandArgument="SEND_TO_CHECKING" ToolTip="Design send to design checker" runat="server"
                                Text="Send to checking" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            <asp:Button ID="btnCheck" CommandArgument="CHECK" ToolTip="Check design" runat="server"
                                Text="Check" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            <%--<asp:Button ID="btnClose" CommandArgument="CLOSE" ToolTip="Close design" runat="server"
                                    Text="Close" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" Visible="false" />--%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>

                            <asp:Button ID="btnSendToAmendment" CommandArgument="SEND_TO_AMENDMENT" ToolTip="Send design to amendment" runat="server"
                                Text="Send to amendment" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>

                            <asp:Button ID="btnSendToCorrection" CommandArgument="SEND_TO_CORRECTION" ToolTip="Send design to amendment" runat="server"
                                Text="Send to correction" CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

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





   <%-- DESIGN DETAIL START--%>
    <asp:Button ID="btnDesignDetails" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDesignDetails" runat="server" TargetControlID="btnDesignDetails"
        PopupControlID="pnlbtnDesignDetailsPopup" CancelControlID="imgBtnDesignDetailsPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnDesignDetailsPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnDesignDetailsPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">
            <fieldset class="form-card">
                <legend>Update Design Details</legend>

                <div class="form-grid form-grid-2">

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompanyToEdit" runat="server"
                        CssClass="form-control" Enabled="false" />

                    <label>JOB Number</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:TextBox ID="txtJOBNoToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetJOBNoToEdit" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNoToEdit_Click" />
                            </td>
                        </tr>
                    </table>

                    <label>Drawing Number</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:TextBox ID="txtDrawingNumberToEdit" runat="server"
                                    CssClass="form-control"
                                    Enabled="true" Style="text-transform: uppercase" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetDrawingDetailsToEdit" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetDrawingDetailsToEdit_Click" OnClientClick="return ValidateAllJobNo();" />
                            </td>
                        </tr>
                    </table>

                    <label>Client Drawing No.</label>
                    <asp:TextBox ID="txtClientDrawingNumberToEdit" runat="server" CssClass="form-control" />

                    <label>Contractor Drawing No.</label>
                    <asp:TextBox ID="txtContractorDrawingNumberToEdit" runat="server" CssClass="form-control" />

                    <label>Description</label>
                    <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control" />

                    <label>UOM</label>
                    <asp:TextBox ID="txtUOMToEdit" runat="server" CssClass="form-control" />

                    <label>Quantity</label>
                    <asp:TextBox ID="txtQuantityToEdit" runat="server" CssClass="form-control"
                        onkeypress="return inNumberKey(this, event);" />

                    <label>Reqd. Date By Project Team</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDateByProjectTeamToEdit" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdDateByProjectTeamToEdit" runat="server" />
                                <ajax:CalendarExtender ID="calendarDateByProjectTeamToEdit" PopupButtonID="imgBtnDateByProjectTeamToEdit" runat="server"
                                    TargetControlID="txtDateByProjectTeamToEdit" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedDateByProjectTeamToEdit">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnDateByProjectTeamToEdit" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>


                    <asp:Panel ID="pnlDesign" runat="server">


                        <label>Expected Completion Date</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtExpectedCompletionDateToEdit" runat="server" onkeyDown="javascript:preventInput(event);" CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdExpectedCompletionDateToEdit" runat="server" />
                                    <ajax:CalendarExtender ID="calendarExpectedCompletionDateToEdit" PopupButtonID="imgBtnExpectedCompletionDateToEdit" runat="server"
                                        TargetControlID="txtExpectedCompletionDateToEdit" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedExpectedCompletionDateToEdit">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnExpectedCompletionDateToEdit" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Calendar" />
                                </td>
                            </tr>
                        </table>


                        <label>Planned Start Date By Design Team</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPlannedStartDateByDesignTeamToEdit" runat="server" onkeyDown="javascript:preventInput(event);"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdPlannedStartDateByDesignTeamToEdit" runat="server" />
                                    <ajax:CalendarExtender ID="calendarPlannedStartDateByDesignTeamToEdit" PopupButtonID="imgBtnPlannedStartDateByDesignTeamToEdit" runat="server"
                                        TargetControlID="txtPlannedStartDateByDesignTeamToEdit" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedPlannedStartDateByDesignTeamToEdit">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnPlannedStartDateByDesignTeamToEdit" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Calendar" />
                                </td>
                            </tr>
                        </table>


                        <label>Planned Completion Date By Design Team</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPlannedCompletionDateByDesignTeamToEdit" runat="server" onkeyDown="javascript:preventInput(event);"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdPlannedCompletionDateByDesignTeamToEdit" runat="server" />
                                    <ajax:CalendarExtender ID="calendarPlannedCompletionDateByDesignTeamToEdit"
                                        PopupButtonID="imgBtnPlannedCompletionDateByDesignTeamToEdit" runat="server"
                                        TargetControlID="txtPlannedCompletionDateByDesignTeamToEdit" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedPlannedCompletionDateByDesignTeamToEdit">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnPlannedCompletionDateByDesignTeamToEdit" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Calendar" />
                                </td>
                            </tr>
                        </table>

                        <label>Responsible Design Engineer</label>
                        <asp:DropDownList ID="ddlRespDesignEnggToEdit" runat="server"
                            CssClass="form-control" />

                        <label>Working Status</label>
                        <asp:TextBox ID="txtWorkingStatusToEdit" runat="server"
                            CssClass="form-control" />

                    </asp:Panel>


                    <label>Category</label>
                    <asp:DropDownList ID="ddlCategoryToEdit" runat="server"
                        CssClass="form-control" />

                    <label>Drawing Revisioin Number</label>
                    <asp:DropDownList ID="ddlDrawingRevisioinNumberToEdit" runat="server" CssClass="form-control">
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
                        <asp:ListItem Text="11" Value="11" />
                        <asp:ListItem Text="12" Value="12" />
                        <asp:ListItem Text="13" Value="13" />
                        <asp:ListItem Text="14" Value="14" />
                        <asp:ListItem Text="15" Value="15" />
                        <asp:ListItem Text="16" Value="16" />
                        <asp:ListItem Text="17" Value="17" />
                        <asp:ListItem Text="18" Value="18" />
                        <asp:ListItem Text="19" Value="19" />
                        <asp:ListItem Text="20" Value="20" />
                    </asp:DropDownList>

                    <label>Document Link</label>
                    <asp:TextBox ID="txtDocumentLinkToEdit" runat="server" CssClass="form-control" />

                    <label>Remarks</label>
                    <div class="full-width">
                        <asp:TextBox ID="txtRemarksToEdit" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine" Rows="2"></asp:TextBox>
                    </div>

                </div>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdateDesignDetails" runat="server" Width="100%" Text="Save Design" CssClass="button"
                    OnClick="btnUpdateDesignDetails_Click" OnClientClick="return ValidateAllToEdit();" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlDesignDetail" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblDesignDetail" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>

        </div>

    </asp:Panel>
    <%-- DESIGN DETAIL START--%>




    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeViewInPDF" runat="server" TargetControlID="btnViewInPDF"
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
            id="iframeViewDrawingDetailsInPDF"
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>




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


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompanySearch" runat="server" CssClass="form-control" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />
                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblJOBUnitIDInJOBList" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT_ID") %>' />
                                <asp:Label ID="lblJOBUnit" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No."
                                    runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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


    <%--DRAWING DETAIL START--%>
    <asp:Button ID="btnShowPopupDrawingDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDrawingDetail" runat="server" TargetControlID="btnShowPopupDrawingDetail"
        PopupControlID="pnlPopupDrawingDetail" CancelControlID="imgBtnCancelDrawingDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupDrawingDetail" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDrawingDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-layout">

            <div class="form-grid-container">

                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblDrawingRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="popup-grid-container">

                        <asp:GridView
                            CssClass="popup-grid"
                            ID="gvDrawingDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowCommand="gvDrawingDetail_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Get" HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawingID" runat="server" Visible="false" Text='<%# Eval("DRAWING_ID") %>' />
                                        <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                        <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>' />
                                        <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                                        <asp:Label ID="lblUOM" runat="server" Visible="false" Text='<%# Eval("UOM") %>' />
                                        <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Visible="false" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>' />
                                        <asp:Button ID="btnGetDrawingDetail" CommandArgument="GET" ToolTip="Get" Width="100%"
                                            runat="server" Text="Get" CssClass="cancelbutton" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DRAWING_NO" HeaderText="DRAWING_NO" />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                                <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                                <asp:BoundField DataField="REQD_DATE_BY_PROJECT_TEAM" HeaderText="REQD_DATE_BY_PROJECT_TEAM" />
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
        </div>

    </asp:Panel>
    <%--DRAWING DETAIL END--%>


     <%--ADD TIMESHEET START--%>
    <asp:Button ID="btnShowPopupAddTimesheet" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddTimesheet" runat="server" TargetControlID="btnShowPopupAddTimesheet"
        PopupControlID="pnlPopupAddTimesheet" CancelControlID="imgBtnCancelAddTimesheet" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAddTimesheet" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddTimesheet" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="form-entry-container">

            <fieldset class="form-card">
                <legend>Add Timesheet
                </legend>

                <div class="form-grid form-grid-2">

                    <div style="display: block;" id="dvDrawing" runat="server">
                        <label>Project Category</label>
                        <asp:DropDownList ID="ddlProjectCategoryInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />


                        <label>Project Number</label>
                        <asp:DropDownList ID="ddlProjectNumberInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Category</label>
                        <asp:DropDownList ID="ddlCategoryInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Serial Number</label>
                        <asp:DropDownList ID="ddlSerialNumberInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Size</label>
                        <asp:DropDownList ID="ddlSizeInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Rev.</label>
                        <asp:DropDownList ID="ddlRevInTimesheet" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Sheets</label>
                        <asp:TextBox ID="txtSheetsInTimesheet" runat="server"
                            CssClass="form-control"
                            onkeypress="return isNumber(event)"
                            onkeyup="GetDrawingID()" />

                        <label>Drawing ID</label>
                        <asp:TextBox ID="txtDrawingIDInTimesheet" runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />
                        <asp:HiddenField ID="hdDrawingID" runat="server" />


                        <label>JOB Number</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtJOBNoInTimesheet" runat="server"
                                        CssClass="form-control" Enabled="false" />
                                </td>

                                <td style="width: 10%">
                                    <asp:Button ID="btnGetTimesheetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                        OnClick="btnGetTimesheetJOBNo_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>


                        <label>Type Of Drawing</label>
                        <asp:DropDownList ID="ddlTypeOfDrawingInTimesheet" runat="server"
                            CssClass="form-control" />

                    </div>

                </div>

                <div class="form-grid form-grid-2">
                    <div class="full-width">
                        <label style="color: Blue; font-family: Calibri;">
                            Note: Please enter 24 Hour format time.</label>
                    </div>
                </div>
                <div class="form-grid form-grid-2">

                    <label>Start Time</label>
                    <table width="100%">
                        <tr>
                            <td>H:</td>
                            <td>
                                <asp:DropDownList ID="ddlStartTimeHInTimesheet" runat="server"
                                    CssClass="form-control"
                                    onchange="StartTime()">
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
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                    <asp:ListItem Text="13" Value="13" />
                                    <asp:ListItem Text="14" Value="14" />
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="16" Value="16" />
                                    <asp:ListItem Text="17" Value="17" />
                                    <asp:ListItem Text="18" Value="18" />
                                    <asp:ListItem Text="19" Value="19" />
                                    <asp:ListItem Text="20" Value="20" />
                                    <asp:ListItem Text="21" Value="21" />
                                    <asp:ListItem Text="22" Value="22" />
                                    <asp:ListItem Text="23" Value="23" />
                                </asp:DropDownList>

                            </td>
                            <td>&nbsp;</td>
                            <td>M:</td>
                            <td>
                                <asp:DropDownList ID="ddlStartTimeMInTimesheet" runat="server"
                                    CssClass="form-control"
                                    onchange="StartTime()">
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
                                    <asp:ListItem Text="10 " Value="10" />
                                    <asp:ListItem Text="11 " Value="11" />
                                    <asp:ListItem Text="12 " Value="12" />
                                    <asp:ListItem Text="13 " Value="13" />
                                    <asp:ListItem Text="14 " Value="14" />
                                    <asp:ListItem Text="15 " Value="15" />
                                    <asp:ListItem Text="16 " Value="16" />
                                    <asp:ListItem Text="17 " Value="17" />
                                    <asp:ListItem Text="18 " Value="18" />
                                    <asp:ListItem Text="19 " Value="19" />
                                    <asp:ListItem Text="20 " Value="20" />
                                    <asp:ListItem Text="21 " Value="21" />
                                    <asp:ListItem Text="22 " Value="22" />
                                    <asp:ListItem Text="23 " Value="23" />
                                    <asp:ListItem Text="24 " Value="24" />
                                    <asp:ListItem Text="25 " Value="25" />
                                    <asp:ListItem Text="26 " Value="26" />
                                    <asp:ListItem Text="27 " Value="27" />
                                    <asp:ListItem Text="28 " Value="28" />
                                    <asp:ListItem Text="29 " Value="29" />
                                    <asp:ListItem Text="30 " Value="30" />
                                    <asp:ListItem Text="31 " Value="31" />
                                    <asp:ListItem Text="32 " Value="32" />
                                    <asp:ListItem Text="33 " Value="33" />
                                    <asp:ListItem Text="34 " Value="34" />
                                    <asp:ListItem Text="35 " Value="35" />
                                    <asp:ListItem Text="36 " Value="36" />
                                    <asp:ListItem Text="37 " Value="37" />
                                    <asp:ListItem Text="38 " Value="38" />
                                    <asp:ListItem Text="39 " Value="39" />
                                    <asp:ListItem Text="40 " Value="40" />
                                    <asp:ListItem Text="41 " Value="41" />
                                    <asp:ListItem Text="42 " Value="42" />
                                    <asp:ListItem Text="43 " Value="43" />
                                    <asp:ListItem Text="44 " Value="44" />
                                    <asp:ListItem Text="45 " Value="45" />
                                    <asp:ListItem Text="46 " Value="46" />
                                    <asp:ListItem Text="47 " Value="47" />
                                    <asp:ListItem Text="48 " Value="48" />
                                    <asp:ListItem Text="49 " Value="49" />
                                    <asp:ListItem Text="50 " Value="50" />
                                    <asp:ListItem Text="51 " Value="51" />
                                    <asp:ListItem Text="52 " Value="52" />
                                    <asp:ListItem Text="53 " Value="53" />
                                    <asp:ListItem Text="54 " Value="54" />
                                    <asp:ListItem Text="55 " Value="55" />
                                    <asp:ListItem Text="56 " Value="56" />
                                    <asp:ListItem Text="57 " Value="57" />
                                    <asp:ListItem Text="58 " Value="58" />
                                    <asp:ListItem Text="59 " Value="59" />
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtStartTimeInTimesheet" runat="server"
                                    CssClass="form-control"
                                    Text="00:00"
                                    onkeypress="return inNumberKey(this, event);" />
                            </td>
                        </tr>
                    </table>


                    <label>End Time</label>
                    <table width="100%">
                        <tr>
                            <td>H:</td>
                            <td>
                                <asp:DropDownList ID="ddlEndTimeHInTimesheet" runat="server"
                                    CssClass="form-control"
                                    onchange="EndTime()">
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
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                    <asp:ListItem Text="13" Value="13" />
                                    <asp:ListItem Text="14" Value="14" />
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="16" Value="16" />
                                    <asp:ListItem Text="17" Value="17" />
                                    <asp:ListItem Text="18" Value="18" />
                                    <asp:ListItem Text="19" Value="19" />
                                    <asp:ListItem Text="20" Value="20" />
                                    <asp:ListItem Text="21" Value="21" />
                                    <asp:ListItem Text="22" Value="22" />
                                    <asp:ListItem Text="23" Value="23" />
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>M:</td>
                            <td>
                                <asp:DropDownList ID="ddlEndTimeMInTimesheet" runat="server"
                                    CssClass="form-control"
                                    onchange="EndTime()">
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
                                    <asp:ListItem Text="10 " Value="10" />
                                    <asp:ListItem Text="11 " Value="11" />
                                    <asp:ListItem Text="12 " Value="12" />
                                    <asp:ListItem Text="13 " Value="13" />
                                    <asp:ListItem Text="14 " Value="14" />
                                    <asp:ListItem Text="15 " Value="15" />
                                    <asp:ListItem Text="16 " Value="16" />
                                    <asp:ListItem Text="17 " Value="17" />
                                    <asp:ListItem Text="18 " Value="18" />
                                    <asp:ListItem Text="19 " Value="19" />
                                    <asp:ListItem Text="20 " Value="20" />
                                    <asp:ListItem Text="21 " Value="21" />
                                    <asp:ListItem Text="22 " Value="22" />
                                    <asp:ListItem Text="23 " Value="23" />
                                    <asp:ListItem Text="24 " Value="24" />
                                    <asp:ListItem Text="25 " Value="25" />
                                    <asp:ListItem Text="26 " Value="26" />
                                    <asp:ListItem Text="27 " Value="27" />
                                    <asp:ListItem Text="28 " Value="28" />
                                    <asp:ListItem Text="29 " Value="29" />
                                    <asp:ListItem Text="30 " Value="30" />
                                    <asp:ListItem Text="31 " Value="31" />
                                    <asp:ListItem Text="32 " Value="32" />
                                    <asp:ListItem Text="33 " Value="33" />
                                    <asp:ListItem Text="34 " Value="34" />
                                    <asp:ListItem Text="35 " Value="35" />
                                    <asp:ListItem Text="36 " Value="36" />
                                    <asp:ListItem Text="37 " Value="37" />
                                    <asp:ListItem Text="38 " Value="38" />
                                    <asp:ListItem Text="39 " Value="39" />
                                    <asp:ListItem Text="40 " Value="40" />
                                    <asp:ListItem Text="41 " Value="41" />
                                    <asp:ListItem Text="42 " Value="42" />
                                    <asp:ListItem Text="43 " Value="43" />
                                    <asp:ListItem Text="44 " Value="44" />
                                    <asp:ListItem Text="45 " Value="45" />
                                    <asp:ListItem Text="46 " Value="46" />
                                    <asp:ListItem Text="47 " Value="47" />
                                    <asp:ListItem Text="48 " Value="48" />
                                    <asp:ListItem Text="49 " Value="49" />
                                    <asp:ListItem Text="50 " Value="50" />
                                    <asp:ListItem Text="51 " Value="51" />
                                    <asp:ListItem Text="52 " Value="52" />
                                    <asp:ListItem Text="53 " Value="53" />
                                    <asp:ListItem Text="54 " Value="54" />
                                    <asp:ListItem Text="55 " Value="55" />
                                    <asp:ListItem Text="56 " Value="56" />
                                    <asp:ListItem Text="57 " Value="57" />
                                    <asp:ListItem Text="58 " Value="58" />
                                    <asp:ListItem Text="59 " Value="59" />
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtEndTimeInTimesheet" runat="server"
                                    CssClass="form-control"
                                    Text="00:00"
                                    onkeypress="return inNumberKey(this, event);" />
                            </td>
                        </tr>
                    </table>


                    <label>Total Time Spent</label>
                    <asp:TextBox ID="txtTimeSpentInTimesheet" runat="server"
                        CssClass="form-control"
                        onkeyDown="javascript:preventInput(event);" />

                    <div class="full-width">
                        <label>Work Description</label>
                        <asp:TextBox ID="txtWorkDescriptionInTimesheet" runat="server"
                            CssClass="form-control"
                            Enabled="true" TextMode="MultiLine" Rows="2" />
                    </div>

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSubmitTimesheet" CssClass="button" runat="server" Text="Save"
                    OnClientClick="return ValidateAllTimesheet();" Width="100%" OnClick="btnSubmitTimesheet_Click" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlMsgTimesheet" Visible="false" runat="server">
                    <asp:Label ID="lblMsgTimesheet" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>



            <fieldset class="filter-card">
                <legend style="text-align: center;">Todays' Timesheet
                        <asp:Label ID="lblTimehseetRerocds" runat="server" Text="Records[0]"></asp:Label>
                </legend>


                <div class="employee-grid-container">

                    <asp:GridView
                        CssClass="employee-grid"
                        ID="gvTimesheet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="ENTRY_DATE" HeaderText="Entry Date" />
                            <asp:BoundField DataField="START_TIME" HeaderText="Start Date" />
                            <asp:BoundField DataField="END_TIME" HeaderText="End Date" />
                            <asp:BoundField DataField="REMARKS" HeaderText="Work Descreption" />
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
    <%--ADD TIMESHEET END--%>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
