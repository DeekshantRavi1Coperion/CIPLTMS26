<%@ Page Title="CIPLTMS- Machine Scheduling" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddMachineScheduling.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_AddMachineScheduling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .style1 {
            width: 10px;
        }
    </style>

    <style type="text/css">
        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            padding: 12px 16px;
            z-index: 1;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>

    <style type="text/css">
        .textboxleft {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxleftgreen {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightyellow;
        }

        .textboxcenter {
            width: 70px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxright {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxrightsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: lightyellow;
        }

        .textboxleftsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightyellow;
        }
    </style>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>

    <%--<script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
        }

        function clientChanged(sender, args) {
            document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;

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

            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdCurrentDate.ClientID %>').value.split("-");
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
                alert("Entry Date must be lesser than or equal to current date!");
                return false;
            }
        }
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
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

            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdCurrentDate.ClientID %>').value.split("-");
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
                alert("Entry Date must be lesser than or equal to current date!");
                return false;
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

    <script type="text/Javascript"> 

        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit == '' || Unit == '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    </script>--%>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;
            if (ValidateDateRange()) { return false; }
            if (ValidateUnit()) { return false; }
            return true;
        }
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function checkDec(el) {

            var ex = /^[0-9]+\:?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);

            }
            else {

            }
        }
    </script>

    <script type="text/Javascript">
        function checkDec1(el) {
            var totalMinuts = '0';
            var row = el.parentNode.parentNode;
            var lblTotalMinuts = row.cells[8].innerText;
            var lblLastTotalAdjustedHours = row.cells[9].innerText;

            var workingHours = row.cells[10].getElementsByTagName("input")[0].value;
            if (String(workingHours) != '') {
                var a = workingHours.split(':');
                var hours = a[0];
                var minuts = a[1];

                if (parseInt(hours) > 0) {
                    hours = hours;
                }
                else {
                    hours = 0;
                }


                if (parseInt(minuts) > 0 && parseInt(minuts) < 60) {
                    minuts = minuts;
                }
                else if (parseInt(minuts) > 59) {
                    row.cells[10].getElementsByTagName("input")[0].value = hours + ':00';
                    minuts = 0;
                }
                else {
                    minuts = 0;
                }

                var totalMinuts = Number((hours * 60) + Number(minuts));
                if (parseFloat(totalMinuts) > 0) {
                    row.cells[9].getElementsByTagName("input")[0].value = Number(row.cells[9].getElementsByTagName("input")[0].value) + Number(totalMinuts);
                }
                else {
                    row.cells[9].getElementsByTagName("input")[0].value = '';
                }
            }
            else {
                row.cells[9].getElementsByTagName("input")[0].value = '';
            }
        }
    </script>

    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 70%">
            <legend style="text-align: center;">Machine Scheduling</legend>
        </fieldset>
    </div>
    <br />
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <div align="center">
        <fieldset style="width: 95%;">
            <asp:HiddenField ID="hdMinuts" runat="server" />
            <asp:HiddenField ID="hdTotalMinuts" runat="server" />
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div id="dvScroll" style='overflow: scroll; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView ID="gvMachineDetail" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowDataBound="gvMachineDetail_RowDataBound" OnRowCommand="gvMachineDetail_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnAddNewRecord" ImageUrl="~/Images/Icons/ADD05.png" ToolTip="Add New Record"
                                            runat="server" CommandArgument="ADD" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnRemoveRecord" ImageUrl="~/Images/Icons/REMOVE02.png" ToolTip="Remove Record"
                                            runat="server" CommandArgument="REMOVE" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlUnit" Width="90px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Machine">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMachine" Width="250px" Height="26px" runat="server" 
                                            OnSelectedIndexChanged="ddlMachine_SelectedIndexChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlType" Width="90px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlActivity" Width="150px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduled From">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblScheduledFrom" runat="server" Visible="false" Text='<%# Eval("SCHEDULED_FROM") %>' />
                                                    <asp:TextBox ID="txtScheduledFrom" runat="server" Width="100px" Text='<%# Eval("SCHEDULED_FROM") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarScheduledFrom" PopupButtonID="imgbtnScheduledFrom"
                                                        runat="server" TargetControlID="txtScheduledFrom" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnScheduledFrom" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduled To">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblScheduledTo" runat="server" Visible="false" Text='<%# Eval("SCHEDULED_TO") %>' />
                                                    <asp:TextBox ID="txtScheduledTo" runat="server" Width="100px" Text='<%# Eval("SCHEDULED_TO") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarScheduledTo" PopupButtonID="imgbtnScheduledTo"
                                                        runat="server" TargetControlID="txtScheduledTo" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnScheduledTo" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="JOB No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJOBNo" Width="150px" runat="server" CssClass="textboxleftgreen" Text='<%# Eval("JOB_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Drawing No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDrawingNo" Width="200px" runat="server" CssClass="textboxleftgreen" Text='<%# Eval("DRAWING_NO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Equipment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEquipment" Width="250px" runat="server" TextMode="MultiLine" Rows="2" Columns="30" CssClass="textboxleftgreen"
                                            Text='<%# Eval("EQUIPMENT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Item Desc">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemDesc" Width="250px" runat="server" TextMode="MultiLine" Rows="2" Columns="30" CssClass="textboxleftgreen"
                                            Text='<%# Eval("ITEM_DESCRIPTION") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" Width="90px" runat="server" CssClass="textboxrightsmall" Text='<%# Eval("QUANTITY") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Date of Receipt of Material">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblDateofReceiptofMaterial" runat="server" Visible="false" Text='<%# Eval("DATE_OF_RECEIPT_OF_MATERIAL") %>' />
                                                    <asp:TextBox ID="txtDateofReceiptofMaterial" runat="server" Width="100px" Text='<%# Eval("DATE_OF_RECEIPT_OF_MATERIAL") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarDateofReceiptofMaterial" PopupButtonID="imgbtnDateofReceiptofMaterial"
                                                        runat="server" TargetControlID="txtDateofReceiptofMaterial" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnDateofReceiptofMaterial" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Date of Receipt of Material Calendar" Width="20px" /></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ED of Comp. by Planning">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblEDofCompbyPlanning" runat="server" Visible="false" Text='<%# Eval("ED_OF_COMP_BY_PLANNING") %>' />
                                                    <asp:TextBox ID="txtEDofCompbyPlanning" runat="server" Width="100px" Text='<%# Eval("ED_OF_COMP_BY_PLANNING") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarEDofCompbyPlanning" PopupButtonID="imgbtnEDofCompbyPlanning"
                                                        runat="server" TargetControlID="txtEDofCompbyPlanning" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnEDofCompbyPlanning" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ED of Comp. by Production">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblEDofCompbyProduction" runat="server" Visible="false" Text='<%# Eval("ED_OF_COMP_BY_PRODUCTION") %>' />
                                                    <asp:TextBox ID="txtEDofCompbyProduction" runat="server" Width="100px" Text='<%# Eval("ED_OF_COMP_BY_PRODUCTION") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarEDofCompbyProduction" PopupButtonID="imgbtnEDofCompbyProduction"
                                                        runat="server" TargetControlID="txtEDofCompbyProduction" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnEDofCompbyProduction" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" /></td>
                                            </tr>
                                        </table>



                                    </ItemTemplate>
                                </asp:TemplateField>

                                

                                

                                <asp:TemplateField HeaderText="Actual Date of Comp.">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%;">
                                                    <asp:Label ID="lblActualDateofComp" runat="server" Visible="false" Text='<%# Eval("ACTUAL_COMP_DATE") %>' />
                                                    <asp:TextBox ID="txtActualDateofComp" runat="server" Width="100px" Text='<%# Eval("ACTUAL_COMP_DATE") %>'
                                                        onkeyDown="javascript:preventInput(event);" CssClass="textboxleftsmall"></asp:TextBox>
                                                    <asp:CalendarExtender ID="calendarActualDateofComp" PopupButtonID="imgbtnActualDateofComp"
                                                        runat="server" TargetControlID="txtActualDateofComp" Format="dd-MMM-yyyy">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td style="width: 20%;">
                                                    <asp:ImageButton ID="imgbtnActualDateofComp" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                        ToolTip="Expected Delivery Of Inspection Calendar" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" Width="250px" runat="server" TextMode="MultiLine" Rows="2"
                                            Columns="50" CssClass="textboxleftgreen" Text='<%# Eval("REMARKS") %>' />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <input type="hidden" id="div_position" name="div_position" />
        </fieldset>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
