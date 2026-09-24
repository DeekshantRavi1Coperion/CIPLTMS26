<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddUpdateTimesheetNew.aspx.cs" Inherits="TIMESHEET_AddUpdateTimesheetNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.1.js"></script>

    <script language="javascript" type="text/javascript">
        ///Restricts the From Date Calendar Extender
        function AllowFrom(sender, args) {

            $('div[id$="caltxtFrom_container"] div[class="ajax__calendar_day"]').each(function () {
                var date = new Date();
                alert(date);
                date = $(this).attr('date');
                if (date.getDay() != 0)
                    $(this).css('cursor', 'pointer').attr('disabled', 'disabled').css('text-decoration', 'line-through');
                else
                    $(this).css('font-weight', 'bold');
            });
        }
    </script>

    <script type="text/javascript" language="javascript">
        function pageLoad() {


            if (document.getElementById('<%=txtTimesheetDate.ClientID  %>')) {
                document.getElementById('<%=txtTimesheetDate.ClientID %>').value = document.getElementById('<%=hdTimesheetDate.ClientID %>').value;
            }

            var hdTimesheetDeptID = document.getElementById('<%=hdTimesheetDeptID.ClientID %>').value;
            if (hdTimesheetDeptID == "1") {
                document.getElementById('dvDrawing').style.display = 'block';
            }
            else {
                document.getElementById('dvDrawing').style.display = 'none';
            }
        }

        function clientChangedTimesheetDate(sender, args) {
            document.getElementById('<%=hdTimesheetDate.ClientID %>').value = document.getElementById('<%=txtTimesheetDate.ClientID %>').value;
        }
    </script>

    <script type="text/javascript">

        function ValidateProjectCategory() {
            var ProjectCatetory = document.getElementById('<%=ddlProjectCategory.ClientID %>').selectedIndex;
            if (ProjectCatetory == '' || ProjectCatetory == '0') {
                document.getElementById('<%=ddlProjectCategory.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectCategory.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProjectNumber() {
            var ProjectNumber = document.getElementById('<%=ddlProjectNumber.ClientID %>').selectedIndex;
            if (ProjectNumber == '' || ProjectNumber == '0') {
                document.getElementById('<%=ddlProjectNumber.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNumber.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCategory() {
            var Category = document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex;
            if (Category == '' || Category == '0') {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSerialNumber() {
            var SerialNumber = document.getElementById('<%=ddlSerialNumber.ClientID %>').selectedIndex;
            if (SerialNumber == '' || SerialNumber == '0') {
                document.getElementById('<%=ddlSerialNumber.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSerialNumber.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSize() {
            var Size = document.getElementById('<%=ddlSize.ClientID %>').selectedIndex;
            if (Size == '' || Size == '0') {
                document.getElementById('<%=ddlSize.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSize.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateRev() {
            var Rev = document.getElementById('<%=ddlRev.ClientID %>').selectedIndex;
            if (Rev == '' || Rev == '0') {
                document.getElementById('<%=ddlRev.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRev.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateSheets() {
            var Sheets = document.getElementById('<%=txtSheets.ClientID %>').value;
            if (Sheets == '') {
                document.getElementById('<%=txtSheets.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtSheets.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingID() {
            var DrawingID = document.getElementById('<%=txtDrawingID.ClientID %>').value;
            if (DrawingID == '') {
                document.getElementById('<%=txtDrawingID.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingID.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTypeOfDrawing() {
            var TypeOfDrawing = document.getElementById('<%=ddlTypeOfDrawing.ClientID %>').selectedIndex;
            if (TypeOfDrawing == '' || TypeOfDrawing == '0') {
                document.getElementById('<%=ddlTypeOfDrawing.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfDrawing.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTitle() {
            var Title = document.getElementById('<%=txtTitle.ClientID %>').value;
            if (Title == '') {
                document.getElementById('<%=txtTitle.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTitle.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateStartTime() {
            var StartTime = document.getElementById('<%=txtStartTime.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(StartTime);
            if (StartTime == '' || StartTime == '__:__' || StartTime == '00:00') {
                document.getElementById('<%=txtStartTime.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtStartTime.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtStartTime.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

        function ValidateEndTime() {
            var EndTime = document.getElementById('<%=txtEndTime.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(EndTime);
            if (EndTime == '' || EndTime == '__:__' || EndTime == '00:00') {
                document.getElementById('<%=txtEndTime.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtEndTime.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtEndTime.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

        function ValidateTimeSpent() {
            var TimeSpent = document.getElementById('<%=txtTimeSpent.ClientID %>').value;
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(TimeSpent);
            if (TimeSpent == '' || TimeSpent == '00:00') {
                document.getElementById('<%=txtTimeSpent.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                if (isValid) {
                    document.getElementById('<%=txtTimeSpent.ClientID %>').style.borderColor = "";
                    return false;
                }
                else {
                    document.getElementById('<%=txtTimeSpent.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
            }
        }

    </script>

    <script type="text/javascript">

        function ValidateAll() {
            var check = true;

            if (ValidateProjectCategory()) {
                return false;
            }

            if (ValidateProjectNumber()) {
                return false;
            }

            if (document.getElementById('dvDrawing').style.display == 'block') {
                if (ValidateCategory()) {
                    return false;
                }

                if (ValidateSerialNumber()) {
                    return false;
                }

                if (ValidateSize()) {
                    return false;
                }

                if (ValidateRev()) {
                    return false;
                }

                if (ValidateSheets()) {
                    return false;
                }

                if (ValidateDrawingID()) {
                    return false;
                }


                if (ValidateTypeOfDrawing()) {
                    return false;
                }
            }

            if (ValidateTitle()) {
                return false;
            }

            if (ValidateStartTime()) {
                return false;
            }

            if (ValidateEndTime()) {
                return false;
            }

            if (ValidateTimeRange()) {
                return false;
            }

            if (ValidateTimeSpent()) {
                return false;
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

    <script type="text/JavaScript">
        function validateHhMm(inputField) {
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

            if (isValid) {
                inputField.style.backgroundColor = '#bfa';
            } else {
                inputField.style.backgroundColor = '#fba';
            }

            return isValid;
        }

        function ValidateTime(inputField) {
            var isValid = /^([0-1]?[0-9]|2[0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField.value);

            if (isValid) {
                inputField.style.borderColor = '';
            }
            else {
                inputField.style.borderColor = '#F7627F';
            }
            return isValid;
        }
    </script>

    <script type="text/javascript">
        function GetTimeSpent() {
            var StartTime = document.getElementById('<%=txtStartTime.ClientID %>').value;

            var StartTimeHRS = Number(StartTime.match(/^(\d+)/)[1]);
            var StartTimeMNTS = Number(StartTime.match(/:(\d+)/)[1]);

            var StartTimeHours = StartTimeHRS.toString();
            var StartTimeMinutes = StartTimeMNTS.toString();

            if (StartTimeHRS < 10) StartTimeHours = "0" + StartTimeHours;
            if (StartTimeMNTS < 10) StartTimeMinutes = "0" + StartTimeMinutes;

            var date1 = new Date();
            date1.setHours(StartTimeHours);
            date1.setMinutes(StartTimeMinutes);



            var EndTime = document.getElementById('<%=txtEndTime.ClientID %>').value;

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

            document.getElementById('<%=hdTimeSpent.ClientID %>').value = hours + ":" + mins;
            document.getElementById('<%=txtTimeSpent.ClientID %>').value = document.getElementById('<%=hdTimeSpent.ClientID %>').value;
        }
    </script>

    <script type="text/javascript">
        function ValidateTimeRange() {
            var StartTime = document.getElementById('<%=txtStartTime.ClientID %>').value;

            var StartTimeHRS = Number(StartTime.match(/^(\d+)/)[1]);
            var StartTimeMNTS = Number(StartTime.match(/:(\d+)/)[1]);

            var StartTimeHours = StartTimeHRS.toString();
            var StartTimeMinutes = StartTimeMNTS.toString();

            if (StartTimeHRS < 10) StartTimeHours = "0" + StartTimeHours;
            if (StartTimeMNTS < 10) StartTimeMinutes = "0" + StartTimeMinutes;

            var date1 = new Date();
            date1.setHours(StartTimeHours);
            date1.setMinutes(StartTimeMinutes);



            var EndTime = document.getElementById('<%=txtEndTime.ClientID %>').value;

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
                document.getElementById('<%=txtStartTime.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=txtEndTime.ClientID %>').style.borderColor = "#F7627F";
                document.getElementById('<%=txtTimeSpent.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtStartTime.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtEndTime.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtTimeSpent.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function GetCategoryText() {
            var ddlCategory = document.getElementById('<%=ddlCategory.ClientID %>');
            var ddlCategory = ddlCategory.options[ddlCategory.selectedIndex].text;
            if (ddlCategory == 'Select' || ddlCategory == '')
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value + '.' + ddlCategory + "."
            }
        }

        function GetSerialNumberText() {
            var ddlSerialNumber = document.getElementById('<%=ddlSerialNumber.ClientID %>');
            var ddlSerialNumber = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text;
            if (ddlSerialNumber == 'Select' || ddlSerialNumber == '')
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value + ddlSerialNumber + '.';
            }
        }

        function GetSizeText() {
            var ddlSize = document.getElementById('<%=ddlSize.ClientID %>');
            var ddlSize = ddlSize.options[ddlSize.selectedIndex].text;
            if (ddlSize == 'Select' || ddlSize == '')
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value;
            else {
                document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=txtDrawingID.ClientID %>').value + ddlSize + '.';
            }
        }
    </script>

    <script type="text/javascript">
        function GetDrawingID() {

            var ddlProjectCategory = document.getElementById('<%=ddlProjectCategory.ClientID %>');
            var ddlProjectCategoryIndex = document.getElementById('<%=ddlProjectCategory.ClientID %>').selectedIndex;
            var ddlProjectCategoryText = ddlProjectCategory.options[ddlProjectCategory.selectedIndex].text;
            if (ddlProjectCategoryIndex == '' || ddlProjectCategoryIndex == '0') {
                var ddlProjectCategoryText = ''
            }
            else {
                var ddlProjectCategoryText = ddlProjectCategory.options[ddlProjectCategory.selectedIndex].text;
            }



            var ddlProjectNumber = document.getElementById('<%=ddlProjectNumber.ClientID %>');
            var ddlProjectNumberIndex = document.getElementById('<%=ddlProjectNumber.ClientID %>').selectedIndex;
            var ddlProjectNumberText = ddlProjectNumber.options[ddlProjectNumber.selectedIndex].text;
            if (ddlProjectNumberIndex == '' || ddlProjectNumberIndex == '0') {
                var ddlProjectNumberText = '';
            }
            else {
                var ddlProjectNumberText = ddlProjectNumber.options[ddlProjectNumber.selectedIndex].text + '.';
            }

            var ddlCategory = document.getElementById('<%=ddlCategory.ClientID %>');
            var ddlCategoryIndex = document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex;
            var ddlCategoryText = ddlCategory.options[ddlCategory.selectedIndex].text;
            if (ddlCategoryIndex == '' || ddlCategoryIndex == '0') {
                var ddlCategoryText = '';
            }
            else {
                var ddlCategoryText = ddlCategory.options[ddlCategory.selectedIndex].text + '.';
            }



            var ddlSerialNumber = document.getElementById('<%=ddlSerialNumber.ClientID %>');
            var ddlSerialNumberIndex = document.getElementById('<%=ddlSerialNumber.ClientID %>').selectedIndex;
            var ddlSerialNumberText = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text;
            if (ddlSerialNumberIndex == '' || ddlSerialNumberIndex == '0') {
                var ddlSerialNumberText = '';
            }
            else {
                var ddlSerialNumberText = ddlSerialNumber.options[ddlSerialNumber.selectedIndex].text + '.';
            }



            var ddlSize = document.getElementById('<%=ddlSize.ClientID %>');
            var ddlSizeIndex = document.getElementById('<%=ddlSize.ClientID %>').selectedIndex;
            var ddlSizeText = ddlSize.options[ddlSize.selectedIndex].text;
            if (ddlSizeIndex == '' || ddlSizeIndex == '0') {
                var ddlSizeText = '';
            }
            else {
                var ddlSizeText = ddlSize.options[ddlSize.selectedIndex].text + '.';
            }



            var ddlRev = document.getElementById('<%=ddlRev.ClientID %>');
            var ddlRevIndex = document.getElementById('<%=ddlRev.ClientID %>').selectedIndex;
            var ddlRevText = ddlRev.options[ddlRev.selectedIndex].text;
            if (ddlRevIndex == '' || ddlRevIndex == '0') {
                var ddlRevText = '';
            }
            else {
                var ddlRevText = ddlRev.options[ddlRev.selectedIndex].text + '.';
            }


            var sheets = document.getElementById('<%=txtSheets.ClientID %>').value;
            if (sheets == '') {
                var sheetsText = '';
            }
            else {
                var sheetsText = document.getElementById('<%=txtSheets.ClientID %>').value;
            }

            document.getElementById('<%=hdDrawingID.ClientID %>').value = ddlProjectCategoryText + ddlProjectNumberText +
                ddlCategoryText + ddlSerialNumberText +
                ddlSizeText + ddlRevText + sheetsText;

            document.getElementById('<%=txtDrawingID.ClientID %>').value = document.getElementById('<%=hdDrawingID.ClientID %>').value;

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdTimesheetDeptID" runat="server" />
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>

            <div class="form-entry-container">
                <fieldset class="form-card">
                    <legend>
                        <asp:Label ID="lblLegend" runat="server" Text="Add Timesheet"></asp:Label>
                    </legend>

                    <div class="form-grid form-grid-2">

                        <label>Project Category</label>
                        <asp:DropDownList ID="ddlProjectCategory" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />


                        <label>Project Number</label>
                        <asp:DropDownList ID="ddlProjectNumber" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Category</label>
                        <asp:DropDownList ID="ddlCategory" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Serial Number</label>
                        <asp:DropDownList ID="ddlSerialNumber" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Size</label>
                        <asp:DropDownList ID="ddlSize" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Rev.</label>
                        <asp:DropDownList ID="ddlRev" runat="server"
                            CssClass="form-control"
                            onchange="GetDrawingID();" />

                        <label>Sheets</label>
                        <asp:TextBox ID="txtSheets" runat="server"
                            CssClass="form-control"
                            onkeypress="return isNumber(event)" onkeyup="GetDrawingID()" />

                        <label>Drawing ID</label>
                        <asp:TextBox ID="txtDrawingID" runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />
                        <asp:HiddenField ID="hdDrawingID" runat="server" />


                        <label>Type Of Drawing</label>
                        <asp:DropDownList ID="ddlTypeOfDrawing" runat="server"
                            CssClass="form-control" />

                    </div>

                    <div class="form-grid form-grid-2">
                        <div class="full-width">
                            <label style="color: Blue; font-family: Calibri;">
                                Note: Please enter 24 Hour format time.</label>
                        </div>
                    </div>
                    <div class="form-grid form-grid-2">

                        <label>Start Time</label>
                        <asp:TextBox ID="txtStartTime" runat="server" meta:resourcekey="txttxtStartTimeResource"
                            CssClass="form-control"
                            ValidationGroup="vgrpUpdateTask" onchange="ValidateTime(this);"
                            onblur="return ValidateStartTime();" Text="00:00" onkeyup="GetTimeSpent();" />
                        <ajax:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtStartTime"
                            Mask="99:99" MaskType="Time" CultureName="en-us" MessageValidatorTip="true" runat="server">
                        </ajax:MaskedEditExtender>


                        <label>End Time</label>
                        <table width="100%">
                            <tr>
                                <td style="width: 35%;">
                                    <asp:TextBox ID="txtEndTime" runat="server" meta:resourcekey="txttxtStartTimeResource"
                                        CssClass="form-control"
                                        ValidationGroup="vgrpUpdateTask"
                                        onchange="ValidateTime(this);" Text="00:00" onkeyup="GetTimeSpent();" />
                                    <ajax:MaskedEditExtender ID="MaskedEditExtender3" TargetControlID="txtEndTime" Mask="99:99"
                                        MaskType="Time" CultureName="en-us" MessageValidatorTip="true" runat="server">
                                    </ajax:MaskedEditExtender>
                                </td>
                                <td style="width: 15%;">
                                    <asp:TextBox ID="txtTimeSpent" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" Text="00:00" />
                                    <asp:HiddenField ID="hdTimeSpent" runat="server" />
                                </td>
                            </tr>
                        </table>

                        <label>Title</label>
                        <asp:TextBox ID="txtTitle" runat="server"
                            CssClass="form-control" />


                        <asp:Label ID="lblTimesheetDate" runat="server" Text="Timesheet Date:"></asp:Label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTimesheetDate" runat="server" ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdTimesheetDate" runat="server" />
                                    <ajax:CalendarExtender ID="calendarTimesheetDate" PopupButtonID="imgbtnTimesheetDate"
                                        runat="server" TargetControlID="txtTimesheetDate" Format="dd-MMM-yyyy" OnClientShown="AllowFrom"
                                        OnClientDateSelectionChanged="clientChangedTimesheetDate">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnTimesheetDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Timesheet Date Calendar" />
                                </td>
                            </tr>
                        </table>


                    </div>
                    <div class="form-grid form-grid-2">

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

                    <asp:Button ID="btnTimesheet"
                        runat="server"
                        Text="Timesheet List"
                        CssClass="button"
                        OnClick="btnTimesheet_Click" Width="50%" />
                </div>

                <div class="full-width">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="30px">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>
                </div>


                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="employee-grid-container">
                        <asp:GridView
                            CssClass="employee-grid"
                            ID="gvTimesheetList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:BoundField DataField="ENTRY_DATE" HeaderText="Entry Date" />
                                <asp:BoundField DataField="START_TIME" HeaderText="Start Time" />
                                <asp:BoundField DataField="END_TIME" HeaderText="End Time" />
                                <asp:BoundField DataField="TIME_SPENT" HeaderText="Time Spent" />
                                <asp:BoundField DataField="TITLE" HeaderText="Title" />
                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
