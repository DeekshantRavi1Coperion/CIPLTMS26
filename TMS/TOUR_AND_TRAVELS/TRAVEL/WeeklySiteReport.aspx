<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="WeeklySiteReport.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_WeeklySiteReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .gv-clean input[type="text"] {
            border: none;
            border-bottom: 1px solid #ccc;
            background: white;
            outline: none;
            width: 95%;
        }

        .gv-clean select {
            background: white !important;
        }

        #gvOTReport td {
            padding: 4px;
        }

        .gv-clean input[type="submit"] {
            background-color: #ffffff !important;
            background-image: none !important;
            color: #555 !important;
            border: 1px solid #e5e5e5 !important;
            padding: 4px 10px;
            border-radius: 4px;
            cursor: pointer;
            box-shadow: none !important;
        }

        #gvOTReport th {
            position: sticky;
            top: 0;
            background-color: #1C5E55;
            color: white;
            z-index: 2;
        }

        #gvOTReport {
            border-collapse: collapse;
        }
    </style>


    <script type="text/javascript">
        function ToggleWeekendRow(checkbox) {
            var row = checkbox.closest("tr");
            var controls = row.querySelectorAll("input[type='text']:not([id*='txtActiveTime']), select");

            for (var i = 0; i < controls.length; i++) {
                controls[i].disabled = !checkbox.checked;
                if (!checkbox.checked) {
                    if (controls[i].tagName.toLowerCase() === "select") {
                        controls[i].selectedIndex = 0;
                    } else if (controls[i].type.toLowerCase() === "text") {
                        if (controls[i].id.indexOf("txtBreakTime") !== -1) {
                            controls[i].value = "0";
                        } else {
                            controls[i].value = "";
                        }
                    }
                }
            }

            if (!checkbox.checked) {
                var txtActiveTime = row.querySelector("input[id*='txtActiveTime']");
                if (txtActiveTime) {
                    txtActiveTime.value = "0";
                }
            }
        }
    </script>

    <script>

        function stayHere() {
            sessionStorage.setItem("scrollFix", "yes");
        }

        window.onload = function () {
            if (sessionStorage.getItem("scrollFix") === "yes") {
                document.getElementById('<%= gvOTReport.ClientID %>').scrollIntoView();
                sessionStorage.removeItem("scrollFix");
            }
        }
    </script>

    <script>
        document.addEventListener("input", function (e) {
            if (e.target.classList.contains("time-input")) {
                let row = e.target.closest("tr");

                let start = row.querySelector("[id*='txtStartTime']").value;
                let end = row.querySelector("[id*='txtFinishTime']").value;
                let breakTime = row.querySelector("[id*='txtBreakTime']").value;

                if (start && end) {

                    let startMinutes = convertToMinutes(start);
                    let endMinutes = convertToMinutes(end);

                    let breakMinutes = breakTime
                        ? convertDurationToMinutes(breakTime)
                        : 0;

                    if (endMinutes < startMinutes) {
                        endMinutes += 24 * 60;
                    }

                    let total = endMinutes - startMinutes - breakMinutes;

                    if (total >= 0) {

                        let hours = Math.floor(total / 60);
                        let mins = total % 60;

                        let finalTime =
                            String(hours).padStart(2, '0') + ":" +
                            String(mins).padStart(2, '0');

                        row.querySelector("[id*='txtActiveTime']").value = finalTime;
                    }
                    else {
                        row.querySelector("[id*='txtActiveTime']").value = "";
                    }
                }
                else {
                    row.querySelector("[id*='txtActiveTime']").value = "";
                }
            }
        });

        function convertToMinutes(time) {

            if (!time || time.indexOf(":") === -1)
                return 0;

            let parts = time.trim().split(" ");

            if (parts.length < 2)
                return 0;

            let timePart = parts[0];
            let modifier = parts[1].toUpperCase();

            let splitTime = timePart.split(":");

            if (splitTime.length < 2)
                return 0;

            let hours = parseInt(splitTime[0]);
            let minutes = parseInt(splitTime[1]);

            if (isNaN(hours) || isNaN(minutes))
                return 0;

            if (modifier === "PM" && hours !== 12)
                hours += 12;

            if (modifier === "AM" && hours === 12)
                hours = 0;

            return (hours * 60) + minutes;
        }

        function convertDurationToMinutes(time) {

            let parts = time.split(":");

            let hours = parseInt(parts[0]) || 0;
            let minutes = parseInt(parts[1]) || 0;

            return (hours * 60) + minutes;
        }

    </script>

    <script type="text/Javascript">

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
        var GridId = "<%=gvOTReport.ClientID %>";
        var ScrollHeight = 450;

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdatePanel runat="server" ID="uppanel">

        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>


        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">



                <div class="page-layout">

                    <div class="form-grid-container">
                        <fieldset class="filter-card">
                            <legend>Weekly Site Report</legend>
                            <div class="form-grid form-grid-3">

                                <td align="right">Tour Sanction Number:</td>
                                <asp:DropDownList ID="ddlTSNumber" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlTSNumber_SelectedIndexChanged"
                                    CssClass="form-control">
                                </asp:DropDownList>

                                <td align="right">Tour Start Date:</td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtStartDate" runat="server" Enabled="true" ReadOnly="true"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="calenderIcon" align="right" runat="server"
                                                ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Start Date Calendar" />
                                        </td>
                                    </tr>
                                </table>

                                <td align="right">Tour End Date:</td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtEndDate" runat="server" Enabled="true" ReadOnly="true"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" align="right" runat="server" ImageUrl="~/Images/Calendar2.png"
                                                ToolTip="Start Date Calendar" />
                                        </td>
                                    </tr>
                                </table>

                                <td align="right">Customer Name:</td>
                                <asp:TextBox ID="txtCustName" runat="server"
                                    CssClass="form-control"></asp:TextBox>

                                <td align="right">End User & Site:</td>
                                <asp:TextBox ID="txtEndUserSite" runat="server"
                                    CssClass="form-control"></asp:TextBox>


                                <td align="right">Coperion Employee Name:</td>
                                <asp:TextBox ID="txtEmployeeName" runat="server"
                                    CssClass="form-control"></asp:TextBox>

                                <td align="right">Coperion PO No.:</td>
                                <asp:TextBox ID="txtPoNo" runat="server"
                                    CssClass="form-control"></asp:TextBox>

                                <td align="right">Coperion Job No.:</td>
                                <asp:TextBox ID="txtJobNo" runat="server"
                                    CssClass="form-control"></asp:TextBox>

                                <td align="right">Remarks:</td>
                                <asp:TextBox ID="txtRemarks" runat="server"
                                    CssClass="form-control"
                                    Enabled="true" TextMode="MultiLine"
                                    Rows="2" />

                                <td align="right">FOC Type:</td>
                                <asp:DropDownList ID="ddlFOCType" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlFOCType_SelectedIndexChanged"
                                    CssClass="form-control">
                                </asp:DropDownList>



                            </div>
                        </fieldset>
                        <div class="full-width button-group">
                            <asp:Button ID="btnSubmit"
                                runat="server"
                                Text="Submit"
                                CssClass="button"
                                Width="100%"
                                OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>
                                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>


                            <div align="center">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                                </asp:Panel>
                            </div>

                            <asp:GridView
                                ID="gvOTReport"
                                runat="server" EnableViewState="true" ViewStateMode="Enabled"
                                AutoGenerateColumns="false" CellPadding="4"
                                ForeColor="#333333"
                                CssClass="employee-grid"
                                GridLines="Both" Width="100%" HorizontalAlign="Center"
                                OnRowCommand="gvOTReport_RowCommand" OnRowDataBound="gvOTReport_RowDataBound">
                                <Columns>

                                    <asp:BoundField DataField="DATE" HeaderText="DATE" />
                                    <asp:BoundField DataField="DAY" HeaderText="DAY" />
                                    <asp:TemplateField HeaderText="WEEKEND WORKING">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkWeekendWorking" runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ADD">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAddRow" runat="server"
                                                Text="+ Add"
                                                CommandName="AddRow"
                                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                UseSubmitBehavior="false"
                                                OnClientClick="stayHere();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="USE">
                                        <ItemTemplate>
                                            <asp:Button ID="btnUseRow"
                                                runat="server"
                                                Text="USE"
                                                CommandName="UseRow"
                                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                UseSubmitBehavior="false"
                                                OnClientClick="stayHere();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="REMOVE ROW">

                                        <ItemTemplate>
                                            <asp:Button ID="btnDeleteRow" runat="server"
                                                Text="-"
                                                CommandName="DeleteRow"
                                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                                UseSubmitBehavior="false"
                                                OnClientClick="stayHere();" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACTIVITY">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlActivity" runat="server" Width="100%">
                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Travel" Value="Travel"></asp:ListItem>
                                                <asp:ListItem Text="Work" Value="Work"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="START TIME">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStartTime" runat="server"
                                                Text='<%# Eval("START_TIME") %>'
                                                CssClass="time-input"
                                                placeholder="hh:mm AM/PM" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FINISH TIME">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFinishTime" runat="server"
                                                Text='<%# Eval("FINISH_TIME") %>'
                                                CssClass="time-input"
                                                placeholder="hh:mm AM/PM" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BREAK TIME">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBreakTime" runat="server"
                                                Text='<%# Eval("BREAK_TIME") %>'
                                                CssClass="time-input"
                                                placeholder="hh:mm" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ACTIVE TIME">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtActiveTime" runat="server"
                                                Text='<%# Eval("ACTIVE_TIME") %>'
                                                ReadOnly="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="REMARKS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarksGrid" runat="server" Text='<%# Eval("REMARKS") %>' Width="100%" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOC TYPE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlFOC" runat="server" Width="100%">
                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Chargeable" Value="Chargeable"></asp:ListItem>
                                                <asp:ListItem Text="FOC-Warranty" Value="FOC-Warranty"></asp:ListItem>
                                                <asp:ListItem Text="FOC-Training" Value="FOC-Training"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                            </asp:GridView>


                        </fieldset>
                    </div>


                    <div class="employee-grid-container">

                        <fieldset class="filter-card">
                            <legend>
                                <asp:Label ID="Label1" runat="server" Text="Records[0]" /></legend>

                            <asp:GridView ID="GridView1"
                                runat="server"
                                AutoGenerateColumns="false"
                                ShowHeader="true"
                                Width="100%"
                                CssClass="employee-grid"
                                GridLines="Both">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo"
                                                runat="server"
                                                Text='<%# Eval("SrNo") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtDate"
                                                runat="server"
                                                Width="85%"
                                                placeholder="Select Date">
                                            </asp:TextBox>

                                            <asp:ImageButton ID="imgCalendar"
                                                runat="server"
                                                ImageUrl="~/Images/Calendar2.png"
                                                Width="18px"
                                                Height="18px" />

                                            <asp:CalendarExtender
                                                ID="ceDate"
                                                runat="server"
                                                TargetControlID="txtDate"
                                                PopupButtonID="imgCalendar"
                                                Format="dd-MMM-yyyy">
                                            </asp:CalendarExtender>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- PLACE --%>
                                    <asp:TemplateField HeaderText="Place">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPlace" runat="server"
                                                Width="95%"
                                                placeholder="Enter Place">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- SITE INCHARGE NAME --%>
                                    <asp:TemplateField HeaderText="Site Incharge Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSiteIncharge" runat="server"
                                                Width="95%"
                                                placeholder="Enter Name">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- DESIGNATION --%>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDesignation" runat="server"
                                                Width="95%"
                                                placeholder="Enter Designation">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- SIGNATURE --%>
                                    <asp:TemplateField HeaderText="Signature">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSignature" runat="server"
                                                Width="95%"
                                                placeholder="Sign Here">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <RowStyle BackColor="#E3EAEB" Height="45px" />
                                <HeaderStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                            </asp:GridView>

                        </fieldset>
                    </div>


                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
