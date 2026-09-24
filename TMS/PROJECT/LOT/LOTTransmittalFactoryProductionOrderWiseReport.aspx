<%@ Page Title="CIPLTMS-Transmittal To Factory Report" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="LOTTransmittalFactoryProductionOrderWiseReport.aspx.cs" Inherits="PROJECT_LOT_LOTTransmittalFactoryProductionOrderWiseReport" %>

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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>




    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Production Order No. Wise LOT Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>(Production Order Date) Start Date</label>
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

                    <label>JOB No.</label>
                    <asp:TextBox ID="txtJOBNo" runat="server" 
                        CssClass="form-control"/>


                    <label>LOT For</label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                                <asp:DropDownList ID="ddlLOTMainItems" runat="server" 
                                    CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlLOTMainItems_SelectedIndexChanged" />
                            </td>
                            <td style="width: 40%">
                                <asp:DropDownList ID="ddlLOTMainSubItems" runat="server" 
                                    CssClass="form-control"
                                    onblur="return ValidateLOTMainSubItems();" />
                            </td>
                        </tr>
                    </table>

                    <label>Product Code</label>
                    <asp:TextBox ID="txtProductCode" runat="server" 
                        CssClass="form-control"/>


                    <label>Product Desc</label>
                    <asp:TextBox ID="txtProductDesc" runat="server" 
                        CssClass="form-control"/>


                    <label>Production Order No.</label>
                    <asp:TextBox ID="txtProductionOrderNo" runat="server"
                        CssClass="form-control"/>

                    <label>Is Part of Production?</label>
                    <asp:CheckBox ID="chkIsPartOfProduction" runat="server" />


                    <label>Drawing No.</label>
                    <asp:TextBox ID="txtDrawingNo" runat="server" 
                        CssClass="form-control"/>


                    <label>Company</label>
                    <asp:DropDownList ID="ddlCompany" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" OnClientClick="return ValidateAllSearch();" />

                <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                    Text="Export" OnClick="btnExport_Click" />
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
                    <asp:TemplateField HeaderText="Drawing (.pdf)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                            <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' />

                            <asp:Label ID="lblSIAttachment1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                            <asp:ImageButton ID="imgSIBtnAttachment1" Height="20px" Width="20px" CommandArgument="ViewSIATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drawing (.dwg/.dxf)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSIAttachment2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                            <asp:ImageButton ID="imgSIBtnAttachment2" Height="20px" Width="20px" CommandArgument="ViewSIATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drg.3" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSIAttachment3" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' />
                            <asp:ImageButton ID="imgSIBtnAttachment3" Height="20px" Width="20px" CommandArgument="ViewSIATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drg.4" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSIAttachment4" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' />
                            <asp:ImageButton ID="imgSIBtnAttachment4" Height="20px" Width="20px" CommandArgument="ViewSIATTACHMENT4"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Add. Attachment" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment1" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT1"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Add. Att.2" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment2" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT2"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Add. Att.3" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT3_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment3" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT3"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Add. Att.4" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT4_NAME") %>' />
                            <asp:ImageButton ID="imgBtnAttachment4" Height="20px" Width="20px" CommandArgument="ViewATTACHMENT4"
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="JOB_NO" HeaderText="JOB No" />
                    <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No" />
                    <asp:BoundField DataField="PRODUCTION_ORDER_DATE" HeaderText="Production Order Date" />
                    <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Expected Completion Date" />
                    <asp:BoundField DataField="STATUS_NAME" HeaderText="Status" />
                    <asp:BoundField DataField="LOT_NO" HeaderText="LOT No" />
                    <asp:BoundField DataField="LOT_DATE" HeaderText="LOT Date" />
                    <asp:BoundField DataField="LOT_FOR" HeaderText="LOT For" />
                    <asp:BoundField DataField="DRAWING_NO" HeaderText="Drawing No" />
                    <asp:BoundField DataField="DRAWING_DESC" HeaderText="Drawing Desc" />
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />
                    <asp:BoundField DataField="PRODUCT_DESC" HeaderText="Product Desc" />
                    <asp:BoundField DataField="IS_PART_OF_PRODUCTION_STATUS_REPORT" HeaderText="Is Part Of Production Status Report/Main Drawing?" />
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />
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

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
