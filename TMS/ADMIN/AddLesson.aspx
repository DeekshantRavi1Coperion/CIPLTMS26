<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddLesson.aspx.cs" Inherits="ADMIN_AddLesson" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />
    <%-- <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../Styles/form.css" rel="stylesheet" />
    <link href="../Styles/grid.css" rel="stylesheet" />
    <link href="../Styles/popup.css" rel="stylesheet" />

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
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

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
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {

            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
        }

        function ValidateEmployee() {
            var Employee = document.getElementById('<%=ddlInitiatedBy.ClientID %>').selectedIndex;
            if (Employee == '' || Employee == '0') {
                document.getElementById('<%=ddlInitiatedBy.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlInitiatedBy.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDepartment() {
            var Department = document.getElementById('<%=ddlDepartment.ClientID %>').selectedIndex;
            if (Department == '' || Department == '0') {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDepartment.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlCompany.ClientID %>').selectedIndex;
            if (Unit == '' && Unit != '0') {
                document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCompany.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        function ValidateEquipments() {
            var Equipment = document.getElementById('<%=ddlEquipments.ClientID %>').selectedIndex;
            if (Equipment == '' || Equipment == '0') {
                document.getElementById('<%=ddlEquipments.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEquipments.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateProblemFacedRemark() {
            var ProblemFacedRemark = document.getElementById('<%=txtProblemFaced.ClientID %>').value;
            if (ProblemFacedRemark == '') {
                document.getElementById('<%=txtProblemFaced.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProblemFaced.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateLessonLearntRemark() {
            var LessonLearntRemark = document.getElementById('<%=txtLessonLearnt.ClientID %>').value;
            if (LessonLearntRemark == '') {
                document.getElementById('<%=txtLessonLearnt.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtLessonLearnt.ClientID %>').style.borderColor = "";
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

        function ValidateAll() {

            var check = true;

            if (ValidateEmployee()) {
                check = false;
            }

            if (ValidateDepartment()) {
                check = false;
            }

            if (ValidateUnit()) {
                check = false;
            }

            if (ValidateEquipments()) {
                check = false;
            }

            if (ValidateProblemFacedRemark()) {
                check = false;
            }

            if (ValidateLessonLearntRemark()) {
                check = false;
            }

            if (ValidateJobNo()) {
                check = false;
            }


            if (ValidateCustName()) {
                check = false;
            }

            if (ValidateCustCode()) {
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
        }

        <%-- function clientChangedSearch(sender, args) {

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
         }--%>
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

    <%-- <script type="text/Javascript">
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
                asdasd123213
                return false;
            }
            return true;
        }
    </script>
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <div align="center" width="100%" style="margin-top: 50px;">



        <div class="form-entry-container">

            <fieldset class="form-card">
                <legend>
                    <asp:Label ID="lblLegend" runat="server" Text="Add Lesson Learnt"></asp:Label></legend>

                <div class="form-grid form-grid-2">

                    <label>Date:</label>
                    <table style="width: 100%;">
                        <tr>
                            <td width="80%">
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




                    <label>Initiated By:</label>
                    <asp:DropDownList ID="ddlInitiatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Department:</label>
                    <asp:DropDownList ID="ddlDepartment" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />

                    <label>JOB Number:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:TextBox ID="txtJOBNo" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNo_Click" />

                                <asp:Button ID="btnAddApprover" runat="server" Width="100%" Text="Add Approver" CssClass="button"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Customer Name:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustomerName" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCode" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>

                    <label>Equipments:</label>
                    <asp:DropDownList ID="ddlEquipments" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Problem Faced:</label>
                    <asp:TextBox ID="txtProblemFaced" runat="server"
                        CssClass="form-control"
                        Enabled="true" TextMode="MultiLine"
                        Rows="2" />

                    <label>Lesson Learnt:</label>
                    <asp:TextBox ID="txtLessonLearnt" runat="server"
                        CssClass="form-control"
                        Enabled="true" TextMode="MultiLine"
                        Rows="2" />
                </div>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save"
                    OnClientClick="return ValidateAll();"
                    OnClick="btnSubmit_Click" Width="100%" />

                <asp:Button ID="btnLessonLearntList" CssClass="button" runat="server"
                    Text="Lesson Learnt List"
                    OnClick="btnLessonList_Click" Width="100%" />
            </div>


            <div class="full-width">

                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="30px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>

            </div>

        </div>


    </div>
    <br />
    <div>

        <%--JOB DETAIL START--%>
        <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
        <ajax:ModalPopupExtender ID="mpeJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
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



            <div class="popup-layout">

                <div class="popup-grid-container">
                    <fieldset class="filter-popup-card">
                        <legend>
                            <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" />
                        </legend>

                        <div class="form-grid form-grid-3">
                            <label>JOB No.:</label>
                            <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />


                            <label>Customer Code:</label>
                            <asp:TextBox ID="txtCustomerCodeSearch" runat="server" CssClass="form-control" />

                            <label>Customer Name.:</label>
                            <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />


                            <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchJOBNo_Click" />

                        </div>
                    </fieldset>
                </div>
            </div>

            <fieldset class="popup-grid-fieldset">
                <%--<div class="popup-grid-container">--%>
                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvJOBDetail_RowCommand">
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
                                    runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="JOB_UNIT" HeaderText="UNIT" />
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <%--</div>--%>
            </fieldset>

        </asp:Panel>
        <%--JOB DETAIL END--%>
    </div>
</asp:Content>
