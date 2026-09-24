<%@ Page Title="CIPLTMS- Assign Items To Machines" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="ScheduledMachineListForMS.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_ScheduledMachineListForMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
        }

        .textboxleftorange {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: orange;
            color: white;
        }

        .textboxleftyellow {
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

        .textboxleftpink {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightpink;
        }

        .textboxcenter {
            width: 50px;
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

        .textfiles {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightgreen;
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
        window.onload = function () {
            var div = document.getElementById("dvAvailableDatesScroll");
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


    <script type="text/javascript">

        function clientChangedScheduleMonth(sender, args) {

            document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value = document.getElementById('<%=txtScheduledFromSchD.ClientID %>').value;
            <%--document.getElementById('<%=hdScheduledToSchD.ClientID %>').value = document.getElementById('<%=txtScheduledToSchD.ClientID %>').value;--%>

           <%-- document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value = "01-" + document.getElementById('<%=hdScheduledFromSchD.ClientID %>').value;
            document.getElementById('<%=hdScheduledToSchD.ClientID %>').value = "01-" + document.getElementById('<%=hdScheduledToSchD.ClientID %>').value;--%>
        }

        function clientChangedAddDate(sender, args) {

            document.getElementById('<%=hdAddDateSchD.ClientID %>').value = document.getElementById('<%=txtAddDateSchD.ClientID %>').value;
        }

        function clientChangedCommitedDateByShopIncharge(sender, args) {
            document.getElementById('<%=hdCommitedDateByShopInchargeSchD.ClientID %>').value = document.getElementById('<%=txtCommitedDateByShopInchargeSchD.ClientID %>').value;
        }

        function clientChangedDateOfReceiptOfMaterial(sender, args) {
            document.getElementById('<%=hdDateOfReceiptOfMaterialSchD.ClientID %>').value = document.getElementById('<%=txtDateOfReceiptOfMaterialSchD.ClientID %>').value;
        }
    </script>


    <script type="text/javascript" language="javascript">


        function onCalendarShownFrom() {
            var cal = $find("calendarScheduledFromSchD");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function onCalendarHiddenFrom() {
            var cal = $find("calendarScheduledFromSchD");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function callFrom(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarScheduledFromSchD");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <script type="text/javascript" language="javascript">


        function onCalendarShownTo() {
            var cal = $find("calendarScheduledToSchD");
            cal._switchMode("months", true);
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callTo);
                    }
                }
            }
        }

        function onCalendarHiddenTo() {
            var cal = $find("calendarScheduledToSchD");
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callTo);
                    }
                }
            }
        }

        function callTo(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendarScheduledToSchD");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
                    break;
            }
        }

    </script>


    <style type="text/css">
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

    <style type="text/css">
        label {
            display: block;
            font: 1rem 'Fira Sans', sans-serif;
        }

        input,
        label {
            margin: .4rem 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:HiddenField ID="hdModifyWorker" runat="server" Value="0" />
    <asp:HiddenField ID="hdViewScheduling" runat="server" Value="0" />
    <asp:HiddenField ID="hdCompleteScheduling" runat="server" Value="0" />--%>
    <asp:HiddenField ID="hdRowCommandFlags" runat="server" Value="0" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Re-Scheduling of Machine:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Machine:</label>
                    <asp:DropDownList ID="ddlMachine" runat="server" Width="100%" Height="26px" />

                    <asp:Button ID="btnSearch" runat="server" Width="100%" Text="Search" CssClass="button"
                        OnClick="btnSearch_Click" />

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
                ID="gvMachineList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvMachineList_RowCommand"
                OnRowDataBound="gvMachineList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>

                            <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" CssClass="textboxcenter" Text='<%# Eval("SR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <%--<asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />--%>
                            <%--<asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_FID") %>' />--%>
                            <%--<asp:Label ID="lblAssignedRecordID" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_RECORD_FID") %>' />--%>
                            <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_PID") %>' />
                            <%--<asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_FID") %>' />--%>

                            <asp:Label ID="lblWorkingStartTime" runat="server" Visible="false" Text='<%# Eval("WORKING_START_TIME") %>' />
                            <asp:Label ID="lblWorkingEndTime" runat="server" Visible="false" Text='<%# Eval("WORKING_END_TIME") %>' />

                            <asp:Button ID="btnReschedule" CommandArgument="RESCHEDULE" ToolTip="Re schedule machine" runat="server"
                                Text="Re-Schedule" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Machine Code">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMachineCode" Width="100%" runat="server"
                                Enabled="false"
                                CssClass="textboxleftyellow"
                                Text='<%# Eval("MACHINE_CODE") %>'
                                ToolTip='<%# Eval("MACHINE_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Machine Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMachineName" Width="100%" runat="server"
                                Enabled="false"
                                CssClass="textboxleftyellow"
                                Text='<%# Eval("MACHINE_NAME") %>'
                                ToolTip='<%# Eval("MACHINE_NAME") %>' />
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

        </div>

    </div>


    <%--SCHEDULE DATES START--%>
    <asp:Button ID="btnShowPopupScheduleDates" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeScheduleDates" runat="server" TargetControlID="btnShowPopupScheduleDates"
        PopupControlID="pnlPopupScheduleDates" CancelControlID="imgBtnCancelScheduleDates" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupScheduleDates" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelScheduleDates" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblLegendMachineNameSchD" runat="server" Text="Re-Schedule Machine" />:
                    <asp:Label ID="lblAvailableDatesRecords" runat="server" Text="Available Hours[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">


                        <div class="full-width button-group">

                        
                        <label>Machine Name:</label>
                        <asp:Label ID="lblMachineIDSchD" runat="server" Visible="false" />
                        <asp:TextBox ID="txtMachineNameSchD" runat="server" Enabled="false"
                            CssClass="form-control" />

                        <label>From Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtScheduledFromSchD" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdScheduledFromSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarScheduledFromSchD" runat="server"
                                        PopupButtonID="imgbtnScheduledFromSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarScheduledFromSchD" TargetControlID="txtScheduledFromSchD"
                                        OnClientDateSelectionChanged="clientChangedScheduleMonth">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnScheduledFromSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" Visible="false" />
                                </td>
                            </tr>
                        </table>

                        <asp:Button ID="btnGetScheduledHours" ToolTip="Get Scheduled Hours" runat="server"
                            Text="Get Scheduled Hours" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="2px" BackColor="LightCoral" OnClick="btnGetScheduledHours_Click"
                            OnClientClick="return ValidateAllMachineSchD();" />

                            </div>


                        <div class="full-width button-group">

                        <label>Add Day:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAddDateSchD" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdAddDateSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarAddDateSchD" runat="server"
                                        PopupButtonID="imgbtnAddDateSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarAddDateSchD" TargetControlID="txtAddDateSchD"
                                        OnClientDateSelectionChanged="clientChangedAddDate">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnAddDateSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="PO Month Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                        <asp:Button ID="btnAddDays" ToolTip="Add Days" runat="server"
                            Text="Add Days" CssClass="button" Width="100%" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="2px" BackColor="LightCoral"
                            OnClientClick="return ValidateAllMachineSchD();"
                            OnClick="btnAddDays_Click" />

                            </div>


                        <div class="full-width button-group">

                        <label>Commited Date By Shop Incharge:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCommitedDateByShopInchargeSchD" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdCommitedDateByShopInchargeSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarCommitedDateByShopInchargeSchD" runat="server"
                                        PopupButtonID="imgbtnCommitedDateByShopInchargeSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarCommitedDateByShopInchargeSchD" TargetControlID="txtCommitedDateByShopInchargeSchD"
                                        OnClientDateSelectionChanged="clientChangedCommitedDateByShopIncharge">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnCommitedDateByShopInchargeSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="Commited Date By Shop Incharge Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                        <label>Date Of Receipt Of Material:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDateOfReceiptOfMaterialSchD" runat="server"
                                        CssClass="form-control"
                                        onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                    <asp:HiddenField ID="hdDateOfReceiptOfMaterialSchD" runat="server" />
                                    <ajax:CalendarExtender ID="calendarDateOfReceiptOfMaterialSchD" runat="server"
                                        PopupButtonID="imgbtnDateOfReceiptOfMaterialSchD"
                                        Format="dd-MMM-yyyy"
                                        BehaviorID="calendarDateOfReceiptOfMaterialSchD" TargetControlID="txtDateOfReceiptOfMaterialSchD"
                                        OnClientDateSelectionChanged="clientChangedDateOfReceiptOfMaterial">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnDateOfReceiptOfMaterialSchD" runat="server" ImageUrl="~/Images/MS/cal1.png"
                                        ToolTip="Date Of Receipt Of Material Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>

                            </div>

                    </div>
                </fieldset>

                <div class="full-width button-group">

                    <asp:Button ID="btnValidateAndScheduleHours" runat="server" Width="100%"
                        Text="Validate & Schedule Hours" CssClass="button"
                        BorderColor="Yellow" BorderStyle="Solid"
                        OnClick="btnValidateAndScheduleHours_Click" OnClientClick="return ValidateAllMachineSchD();" />

                </div>

                <div class="full-width">
                    <div align="center">
                        <asp:Panel ID="pnlScheduleDatesMsg" Visible="false" runat="server">
                            <asp:Label ID="lblScheduleDatesMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="employee-grid-container">

                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>

                        <asp:GridView
                            CssClass="employee-grid"
                            ID="gvAvailableDatesList" runat="server" AutoGenerateColumns="false" CellPadding="5"
                            AlternatingRowStyle-CssClass="alt"
                            OnRowDataBound="gvAvailableDatesList_RowDataBound"
                            OnRowCommand="gvAvailableDatesList_RowCommand">

                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sr No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />
                                        <asp:Label ID="lblAssignedRecordID" runat="server" Visible="false" Text='<%# Eval("ASSIGNED_RECORD_FID") %>' />
                                        <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_FID") %>' />
                                        <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_FID") %>' />
                                        <asp:Label ID="lblTypeOfWorkID" runat="server" Visible="false" Text='<%# Eval("TYPE_OF_WORK_FID") %>' />
                                        <asp:Label ID="lblWorkerID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_FID") %>' />
                                        <asp:Label ID="lblEndTimeFlag" runat="server" Visible="false" Text='<%# Eval("END_TIME_FLAG") %>' />
                                        <asp:Label ID="lblScheduledDateDf" runat="server" Visible="false" Text='<%# Eval("SCHEDULED_DATE_DF") %>' />

                                        <asp:Label ID="lblAFH" runat="server" Visible="false" Text='<%# Eval("RCH_FH") %>' />
                                        <asp:Label ID="lblAFM" runat="server" Visible="false" Text='<%# Eval("RCH_FM") %>' />
                                        <asp:Label ID="lblATH" runat="server" Visible="false" Text='<%# Eval("RCH_TH") %>' />
                                        <asp:Label ID="lblATM" runat="server" Visible="false" Text='<%# Eval("RCH_TM") %>' />

                                        <asp:TextBox ID="txtSrNoToMSchInList" runat="server" Enabled="false" CssClass="textboxcenter"
                                            Text='<%# Eval("SR_NO") %>' />

                                        <asp:Label ID="lblPrimaryRowFlag" runat="server" Visible="false" Text='<%# Eval("PRIMARY_ROW_FLAG") %>' />
                                        <asp:Label ID="lblSrNoByDateToMSchInList" runat="server" Visible="false" Text='<%# Eval("SR_NO_BY_DATE") %>' />
                                        <asp:Label ID="lblValidationTooltipTypeIDs" runat="server" Visible="false" Text='<%# Eval("VALIDATION_TOOLTIP_IDS") %>' />



                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnRemove" ImageUrl="~/Images/Icons/no2.png"
                                            ToolTip="Remove Record"
                                            runat="server" CommandArgument="REMOVE" Width="25px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlTypeOfWorkToMSchInList" Width="80px"
                                            Height="26px" runat="server" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlActivityToMSchInList" Width="120px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Worker" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlWorkerMSchInList" Width="230px" Height="26px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Job No.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJobNoToMSchInList" runat="server"
                                            CssClass="textboxleftgreen"
                                            Width="130px" Text='<%# Eval("JOB_NO") %>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemNameToMSchInList" runat="server"
                                            TextMode="MultiLine"
                                            Columns="20"
                                            CssClass="textboxleftgreen"
                                            Text='<%# Eval("ITEM_NAME") %>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Get">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGetAssignedDrawingDetail" CommandArgument="GET_ASSIGNED_DRAWINGS"
                                            ToolTip="Get Assigned Drawing Details" Width="100%"
                                            runat="server" Text="Get" CssClass="cancelbutton"
                                            BorderStyle="Solid" BorderColor="Yellow" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledDateToMSchInList" runat="server" Width="105px"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Re-Scheduling From">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtAvailableFromToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftgreen"
                                                Text='<%# Eval("AVAILABLE_FROM") %>'></asp:TextBox>--%>

                                        <asp:Label ID="lblAvailableFromToMSchInList" runat="server" Visible="false" Text='<%# Eval("AVAILABLE_FROM") %>' />
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlAFH" runat="server" DataTextField="H" DataValueField="H" Width="50px"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlHours_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>H
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAFM" runat="server" DataTextField="M" DataValueField="M" Width="50px"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlHours_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlAFM_SelectedIndexChanged"--%>
                                                </td>
                                                <td>M
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Re-Scheduling To">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtAvailableToMSchInList" runat="server" Width="100%"
                                                CssClass="textboxleftgreen"
                                                Text='<%# Eval("AVAILABLE_TO") %>'></asp:TextBox>--%>

                                        <asp:Label ID="lblAvailableToMSchInList" runat="server" Visible="false" Text='<%# Eval("AVAILABLE_TO") %>' />
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlATH" runat="server" DataTextField="H" DataValueField="H" Width="50px"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlHours_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlATH_SelectedIndexChanged"--%>
                                                </td>
                                                <td>H
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlATM" runat="server" DataTextField="M" DataValueField="M" Width="50px"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlHours_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--OnSelectedIndexChanged="ddlATM_SelectedIndexChanged"--%>
                                                </td>
                                                <td>M
                                                </td>
                                            </tr>
                                        </table>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Re-Scheduled Hours">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLatestScheduledHoursToMSchInList" runat="server" Width="70px"
                                            Enabled="false" CssClass="textboxleftorange" Text="00:00"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance Hours">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkingStartTimeMSchInList" runat="server" Visible="false" Text='<%# Eval("WORKING_START_TIME") %>' />
                                        <asp:Label ID="lblWorkingEndTimeMSchInList" runat="server" Visible="false" Text='<%# Eval("WORKING_END_TIME") %>' />
                                        <%--<asp:Label ID="lblBalanceHoursToMSchInList" runat="server" Visible="false" Text='<%# Eval("BALANCE_HOURS") %>' />--%>

                                        <asp:TextBox ID="txtAvailableHoursToMSchInList" runat="server" Width="70px"
                                            Enabled="false" CssClass="textboxleftgreen"
                                            Text="00:00"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnStatus"
                                            Enabled="false"
                                            ImageUrl="~/Images/Status/yellow.png"
                                            ToolTip="In Process of Re-scheduling"
                                            runat="server" Width="25px" Height="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarksToMSchInList" Width="250px" runat="server"
                                            TextMode="MultiLine"
                                            Text='<%# Eval("REMARKS") %>' Columns="30" CssClass="textboxleftgreen" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Scheduled From" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledFromToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_FROM") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Scheduled To" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_TO") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Scheduled Hours" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScheduledHoursToMSchInList" runat="server" Width="100%"
                                            Enabled="false" CssClass="textboxleftpink"
                                            Text='<%# Eval("SCHEDULED_HOURS") %>'></asp:TextBox>
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
                <input type="hidden" id="div_position" name="div_position" />

            </div>

        </div>

    </asp:Panel>
    <%--SCHEDULE DATES END--%>



    <%--ASSIGNED DRAWINGS DETAILS START--%>
    <asp:Button ID="btnShowPopupAssignedDrawingDetails" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAssignedDrawingDetails" runat="server" TargetControlID="btnShowPopupAssignedDrawingDetails"
        PopupControlID="pnlPopupAssignedDrawingDetails" CancelControlID="imgBtnCancelAssignedDrawingDetails" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupAssignedDrawingDetails" runat="server" BackColor="White" Height="600px" Width="1200px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAssignedDrawingDetails" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>
                    <asp:Label ID="lblLegendAssignedDrawingDetails" runat="server" Text="Assigned Drawings List" />:
                    <asp:Label ID="lblAssignedDrawingList" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                            <label>JOB Number:</label>
                                <asp:TextBox ID="txtJobNoInAssignedDrg" runat="server" CssClass="form-control" />
                            
                      <label>Drawing Number:</label>
                               <asp:TextBox ID="txtDrawingNoInAssignedDrg" runat="server" CssClass="form-control" />
                          
                            <label>Item Name:</label>
                               <asp:TextBox ID="txtItemNameInAssignedDrg" runat="server" CssClass="form-control" />
                           

                                <asp:Button ID="btnSearchAssignedDrawingDetails" runat="server" Width="100%"
                                    Text="Search" CssClass="button"
                                    OnClick="btnSearchAssignedDrawingDetails_Click"
                                    BorderColor="Yellow" BorderStyle="Solid"
                                    BorderWidth="2px" BackColor="LightCoral" />
                            
                </div>
            </fieldset>
            <div class="full-width button-group">
                
            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                    <asp:Panel ID="pnlAssignedDrawingDetailsMsg" Visible="false" runat="server">
                        <asp:Label ID="lblAssignedDrawingDetailsMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

            <asp:GridView 
                CssClass="employee-grid"
                ID="gvAssignedDrawingList" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                 AlternatingRowStyle-CssClass="alt"
                                OnRowCommand="gvAssignedDrawingList_RowCommand">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <%--OnRowDataBound="AssignedDrawingList_RowDataBound"--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordIDInDrgList" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />
                                            <asp:TextBox ID="txtSrNoInDrgList" runat="server" Enabled="false" CssClass="textboxcenter"
                                                Text='<%# Eval("SR_NO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Get">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGetAssignedDrawingDetail" CommandArgument="GET_ASSIGNED_DRAWINGS"
                                                ToolTip="Get Assigned Drawing Details" Width="100%"
                                                runat="server" Text="Get" CssClass="cancelbutton"
                                                BorderStyle="Solid" BorderColor="Yellow" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assigned Drawing Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAssignedDrawingCodeInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="150px" Text='<%# Eval("ASSIGNED_DRAWING_CODE") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtJobNoInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="150px" Text='<%# Eval("JOB_NO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Drawing No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDrawingNoInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("DRAWING_NO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Equipment">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEquipmentInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("EQUIPMENT") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tag No.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTagNoInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("TAG_NO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemNameInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("ITEM_NAME") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Detail">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemDetailInDrgList" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                                Width="250px" Text='<%# Eval("ITEM_DETAIL") %>' />
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

        </div>

    </div>


        <div align="center">

            <fieldset style="width: 95%; margin-top: 10px;">
                <legend style="text-align: center;">
                    
                </legend>
                
                <div style='overflow: auto; width: 99%; height: 480px; border: 1px solid lightgray; margin-left: 5px;'>

                    <br />

                    

                    <fieldset style="width: 100%;">
                        <legend style="text-align: center;">
                            /legend>
                        <%--id="dvScroll" --%>
                        <div style='overflow: scroll; width: 100%; height: 300px; border: 1px solid lightgray;'>
                            

                        </div>
                        <%--<input type="hidden" id="div_position" name="div_position" />--%>
                    </fieldset>

                </div>
            </fieldset>

        </div>
    </asp:Panel>
    <%--ASSIGNED DRAWINGS DETAILS END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

