<%@ Page Title="CIPLTMS-Update Expected Completion Date" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTTransmittalFactoryListToUpdateExpectedCompDate.aspx.cs" Inherits="PROJECT_LOT_LOTTransmittalFactoryListToUpdateExpectedCompDate" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

        .textboxlightgreen {
            padding: 5px 2px 5px 5px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 5px;
            background-color: lightgreen;
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

    <script type="text/javascript" language="javascript">
        function pageLoad() {

            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            document.getElementById('<%=txtNewExpectedCompletionDate.ClientID %>').value = document.getElementById('<%=hdNewExpectedCompletionDate.ClientID %>').value;
        }

        function clientChangedNewExpectedCompletionDate(sender, args) {
            document.getElementById('<%=hdNewExpectedCompletionDate.ClientID %>').value = document.getElementById('<%=txtNewExpectedCompletionDate.ClientID %>').value;
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


        function ValidateProductionOrderNumber() {
            var productionOrderNumber = document.getElementById('<%=txtProductionOrderNumber.ClientID %>').value;
            if (productionOrderNumber == '') {
                document.getElementById('<%=txtProductionOrderNumber.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductionOrderNumber.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAllSearch() {


            if (ValidateProductionOrderNumber()) {
                return false;
            }

            if (ValidateDateRange()) {
                return false;
            }
            return true;
        }

        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;


        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdUpdationFlag" runat="server" />
    <asp:HiddenField ID="hdProductionManagerIDFlag" runat="server" />
    <asp:HiddenField ID="hdOldRevNo" runat="server" />
    <asp:HiddenField ID="hdRemovedSubitemIDs" runat="server" />





    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>LOT List- Update Expected Completion Date:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Start Date Calendar" />
                            </td>
                        </tr>
                    </table>
                    <label>End Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" /></td>
                        </tr>
                    </table>

                    <label>TF No.</label>
                    <asp:TextBox ID="txtTFNo" runat="server"
                        CssClass="form-control" />


                    <label>Customer Name</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"
                        CssClass="form-control" />


                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server"
                        CssClass="form-control" />

                    <label>LOT For</label>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" AutoPostBack="true" />
                            </td>
                            <td style="width: 50%;">
                                <asp:DropDownList ID="ddlLOTMainSubitems" runat="server"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>
                    <label>Tag Number</label>
                    <asp:TextBox ID="txtTagNumber" runat="server"
                        CssClass="form-control" />

                    <label>Drawing Number</label>
                    <asp:TextBox ID="txtDrawingNumber" runat="server"
                        CssClass="form-control" />


                    <label>Production Order Number</label>
                    <asp:TextBox ID="txtProductionOrderNumber" runat="server"
                        CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllSearch();" />
            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
            <asp:GridView
                CssClass="employee-grid"
                ID="gvLOTTFList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvLOTTFList_RowCommand" OnRowDataBound="gvLOTTFList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                            <asp:Label ID="lblTFNo" runat="server" Visible="false" Text='<%# Eval("TF_NO") %>' />
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                            <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                            <asp:Label ID="lblLOTDate" runat="server" Visible="false" Text='<%# Eval("DATE") %>' />
                            <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                            <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                            <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                            <asp:Label ID="lblItemName" runat="server" Visible="false" Text='<%# Eval("ITEM_NAME") %>' />
                            <asp:Label ID="lblImpNotes" runat="server" Visible="false" Text='<%# Eval("IMP_NOTES") %>' />

                            <asp:Label ID="lblCreatedByID" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                            <asp:Label ID="lblJobPEID" runat="server" Visible="false" Text='<%# Eval("PE_ID") %>' />
                            <asp:Label ID="lblJobPMID" runat="server" Visible="false" Text='<%# Eval("PM_ID") %>' />

                            <asp:ImageButton ID="imgBtnRevise" CommandArgument="REVISE" runat="server" ImageUrl="~/Images/LOT/edit5.png"
                                Height="35px" Width="35px" ToolTip="Update expected completion date" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subitem" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnViewSubitemDetail" Height="20px" Width="20px" CommandArgument="ViewSubitemDETAIL"
                                runat="server" ImageUrl="~/Images/viewdetails.png" ToolTip="View Subitem Details" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" Height="20px" Width="20px" CommandArgument="ViewDETAIL"
                                runat="server" ImageUrl="~/Images/pdficon3.png" ToolTip="View LOT Detail in PDF" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Additional Att." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment1" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.2" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment2" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.3" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT3_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment3" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Att.4" HeaderStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT4_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment4" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT4"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TF_NO" HeaderText="TF_No" />
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
                    <asp:BoundField DataField="DATE" HeaderText="Date" />
                    <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Customer_Code" />
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer_Name" />
                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB_No" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO_No" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="Item_Name" />
                    <asp:BoundField DataField="IMP_NOTES" HeaderText="Imp_Notes" />

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

    <%-- UPDATE EXCEPTED COMPLETION DATE START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeReviseLOT" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="770px" Width="1200px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hdLOTMainSubitemID" runat="server" />
        <asp:HiddenField ID="hdProductionOrderNo" runat="server" />
        <asp:HiddenField ID="hdPONo" runat="server" />
        <asp:HiddenField ID="hdTFNo" runat="server" />


        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>TF No. [<asp:Label ID="lblTFNo" runat="server" />] Details</legend>

                <div class="form-grid form-grid-3">

                    <label>TFNo.</label>
                    <asp:TextBox ID="txtOldTFNoToEdit" runat="server"
                        Enabled="false"
                        CssClass="form-control" />


                    <label>Company</label>
                    <asp:HiddenField ID="hdCompanyToEdit" runat="server" />
                    <asp:TextBox ID="txtCompanyToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />


                    <label>JOB Number</label>
                    <asp:TextBox ID="txtJOBNoToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <label>Customer PO Number</label>
                    <asp:TextBox ID="txtPONoToEdit" runat="server" Enabled="false"
                        CssClass="form-control" />

                    <label>Customer Name</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCustomerNameToEdit" runat="server" Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="txtCustomerCodeToEdit" runat="server" Enabled="false"
                                    CssClass="form-control" />
                            </td>
                        </tr>
                    </table>

                    <label>Date</label>
                    <asp:TextBox ID="txtDateToEdit" runat="server" onkeyDown="javascript:preventInput(event);" Enabled="false"
                        CssClass="form-control"></asp:TextBox>


                    <label>Production Order Number</label>
                    <asp:TextBox ID="txtProductionOrderNumberToEdit" runat="server" Enabled="false"
                        CssClass="form-control"></asp:TextBox>

                    <label>Item</label>
                    <asp:TextBox ID="txtItemNameToEdit" runat="server"
                        CssClass="form-control"
                        Enabled="false" />



                    <fieldset class="filter-card">
                        <legend>
                            <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" />
                        </legend>

                        <div class="employee-grid-container">
                            <asp:GridView ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                OnRowDataBound="gvSubItem_RowDataBound">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                            <asp:Label ID="lblLOTTFMainSubitemID" runat="server" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' Visible="false" />
                                            <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                                            <asp:Label ID="lblNextStatusID" runat="server" Text='<%# Eval("NEXT_STATUS_ID") %>' Visible="false" />
                                            <asp:Label ID="lblLOTTFID" runat="server" Text='<%# Eval("LOT_TF_ID") %>' Visible="false" />

                                            <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                            <asp:Label ID="lblProductionOrderDate" runat="server" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' Visible="false" />
                                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />
                                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("PRODUCT_DESC") %>' Visible="false" />
                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />

                                            <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("TAG_NO") %>' Visible="false" />
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("SUBITEM_DESC") %>' Visible="false" />
                                            <asp:Label ID="lblLOTMainItemID" runat="server" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' Visible="false" />
                                            <asp:Label ID="lblLOTMainSubitemID" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' Visible="false" />
                                            <asp:Label ID="lblLOTFor" runat="server" Text='<%# Eval("LOT_MAIN_ITEM") %>' Visible="false" />
                                            <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Visible="false" />
                                            <asp:Label ID="lblOldRevNo" runat="server" Text='<%# Eval("OLD_REVISION_NO") %>' Visible="false" />
                                            <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Visible="false" />
                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="false" />
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Visible="false" />

                                            <asp:Label ID="lblIsRevised" runat="server" Text='<%# Eval("IS_REVISED") %>' Visible="false" />

                                            <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' Visible="false" />
                                            <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' Visible="false" />

                                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />
                                    <asp:BoundField DataField="TAG_NO" HeaderText="Tag No." />
                                    <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />
                                    <asp:BoundField DataField="DRAWING_NO" HeaderText="Drg/DOC.No" />

                                    <asp:TemplateField HeaderText="Rev.No.">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Width="40PX"
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

                                    <asp:TemplateField HeaderText="Is Part Of Prod. Status Report">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsPartOfProductionStatusReport" runat="server" Text='<%# Eval("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID") %>' Visible="false" />
                                            <asp:CheckBox ID="chkIsPartOfProductStatusReport" runat="server" Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="SI_ATTACHMENT1_NAME" HeaderText="Drawing (.pdf)" />
                                    <asp:BoundField DataField="SI_ATTACHMENT2_NAME" HeaderText="Drawing (.dwg/.dxf)" />
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
                        </div>
                    </fieldset>

                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarksToEdit" runat="server"
                        CssClass="form-control"
                        TextMode="MultiLine" Rows="2" />


                    <label>New Expected Completion Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtNewExpectedCompletionDate" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdNewExpectedCompletionDate" runat="server" />
                                <ajax:CalendarExtender ID="calendarNewExpectedCompletionDate" PopupButtonID="imgBtnNewExpectedCompletionDate" runat="server"
                                    TargetControlID="txtNewExpectedCompletionDate" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedNewExpectedCompletionDate">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnNewExpectedCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>

                </div>

            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnUpdateExpectedCompletionDate" runat="server" Width="100%" Text="Save" CssClass="button"
                    OnClick="btnUpdateExpectedCompletionDate_Click" OnClientClick="return ValidateAll();" />
            </div>

            <div class="full-width">
                <asp:Panel ID="pnlReviseMsg" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblReviseMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                </asp:Panel>
            </div>

        </div>


    </asp:Panel>
    <%-- UPDATE EXCEPTED COMPLETION DATE END --%>


    <%-- SHOW IMAGE FILE START--%>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowImageFile" runat="server" TargetControlID="btnShowImgFile"
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
    <%-- SHOW IMAGE FILE END--%>


    <%-- SHOW PDF FILE START--%>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowPDFFile" runat="server" TargetControlID="btnShowPDFFile"
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
            runat="server"></iframe>
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
            runat="server"></iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>


    <%-- SHOW SUBITEM DETAIL START--%>
    <asp:Button ID="btnShowSubitemDetailFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeSubitemDetail" runat="server" TargetControlID="btnShowSubitemDetailFile"
        PopupControlID="pnlViewSubitemDetailPopup" CancelControlID="imgBtnCancelSubitemDetailFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewSubitemDetailPopup" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelSubitemDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>Subitem Details:
                    <asp:Label ID="lblSubitemsSIRerords" runat="server" Text="Subitems Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>TF No.</label>
                        <asp:TextBox ID="txtTFNoSI" runat="server" Width="100%" Enabled="false" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSI" runat="server" Width="100%" Enabled="false" />


                    </div>
                </fieldset>
                <div class="full-width button-group">
                </div>
            </div>

            <div class="popup-grid-container">
                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvSubitemsSI" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvSubitemsSI_RowCommand" OnRowDataBound="gvSubitemsSI_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderText="Sr.No.">
                            <ItemTemplate>
                                <%-- <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                        <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />
                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />--%>

                                <asp:Label ID="lblLOTTFID" runat="server" Text='<%# Eval("LOT_TF_ID") %>' Visible="false" />
                                <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                                <asp:Label ID="lblLOTTFSubitemID" runat="server" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' Visible="false" />

                                <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="30PX"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drawing (.pdf)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drawing (.dwg/.dxf)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drawing.3" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drawing.4" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment4" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT4"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnStatus" CommandArgument="STATUS" runat="server" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                        <asp:TemplateField HeaderText="Tag No.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTagNoInList" runat="server" Text='<%# Eval("TAG_NO") %>' Style="text-transform: uppercase" Width="150PX"
                                    CssClass="textboxtagno" MaxLength="15" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DRAWING_NO" HeaderText="Drg/DOC.No" />

                        <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />


                        <asp:TemplateField HeaderText="Rev.No.">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Width="30px"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="30px"
                                    onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No." />
                        <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Completion Required By" />

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
    <%-- SHOW SUBITEM DETAIL END--%>





    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
