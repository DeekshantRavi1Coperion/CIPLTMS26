<%@ Page Title="CIPLTMS-Transmittal To Factory List" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    EnableViewState="true" CodeFile="FabricationReport1.aspx.cs" Inherits="PROJECT_LOT_FabricationReport1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdUpdationFlag" runat="server" />
    <asp:HiddenField ID="hdProductionManagerIDFlag" runat="server" />
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 75%">
            <legend style="text-align: center;">Fabrication Report</legend>
            <table width="90%">
                <tr>
                    <td>Start Date:</td>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
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
                    </td>

                    <td>&nbsp;</td>

                    <td>End Date:</td>
                    <td style="width: 20%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                    <ajax:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                        runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" /></td>
                            </tr>
                        </table>
                    </td>


                    <td>&nbsp;</td>
                    <td>Internal Fabrication No.:</td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtInternalFabricationNo" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Company:</td>
                    <td>
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="25px">
                        </asp:DropDownList>
                    </td>

                    <td>&nbsp;</td>

                    <td>Sub Order No.:</td>
                    <td>
                        <asp:TextBox ID="txtSubOrderNo" runat="server" Width="100%" />
                    </td>

                    <td>&nbsp;</td>

                    <td>Equipment/Type:</td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%;">
                                    <asp:DropDownList ID="ddlEquipmentOrType" runat="server" Width="100%" Height="25px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Drawing Number:</td>
                    <td>
                        <asp:TextBox ID="txtDrawingNumber" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>

                    <td>% Work Done:</td>
                    <td>
                        <asp:TextBox ID="txtPercentageWorkDone" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>


                    <td>Production Order No.:</td>
                    <td>
                        <asp:TextBox ID="txtProductionOrderNo" runat="server" Width="100%" />
                    </td>


                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                            OnClick="btnSearch_Click" OnClientClick="return ValidateAllSearch();" />
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server"
                            Text="Export" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 400px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvFabricationList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" PageSize="10" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvFabricationList_RowDataBound">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:BoundField DataField="SR_NO" HeaderText="Sr.No." />
                        <asp:BoundField DataField="INTL_FABRICATION_NO" HeaderText="Internal Fabrication Number" />
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="ERP Code/ Product Code" />
                        <asp:BoundField DataField="SUB_ORDER_NO" HeaderText="Sub Order Number" />
                        <asp:BoundField DataField="PRODUCTION_NO" HeaderText="Production Order No." />
                        <asp:BoundField DataField="ED_DATE" HeaderText="Expected Delivery Date" />
                        <asp:BoundField DataField="RD_DATE" HeaderText="Required Delivery Date" />
                        <asp:BoundField DataField="EQUIPMENT" HeaderText="Equipment/Type " />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                        <asp:BoundField DataField="QTY" HeaderText="Qty" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="Drawing No." />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" />
                        <asp:BoundField DataField="PERCENTAGE_OF_WORK" HeaderText="% of Work Done " />
                        <asp:BoundField DataField="ED_OF_INSP_COMP" HeaderText="Expected Date of Insp/Comp" />


                        <%--                        <asp:BoundField DataField="UNIT_NAME" HeaderText="Unit" />

                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>

                                <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                                <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' />
                                <asp:Label ID="lblTFNo" runat="server" Visible="false" Text='<%# Eval("TF_NO") %>' />

                                <asp:ImageButton ID="btnViewDetail" Height="30px" Width="30px" CommandArgument="ViewDETAIL"
                                    runat="server" ImageUrl="~/Images/pdficon1.png" ToolTip="View LOT Detail in PDF" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drg.1">
                            <ItemTemplate>
                                <asp:Label ID="lblSIAttachment1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgSIBtnAttachment1" Height="30px" Width="30px" CommandArgument="ViewSIATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drg.2">
                            <ItemTemplate>
                                <asp:Label ID="lblSIAttachment2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgSIBtnAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drg.3">
                            <ItemTemplate>
                                <asp:Label ID="lblSIAttachment3" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT3_NAME") %>' />
                                <asp:ImageButton ID="imgSIBtnAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Drg.4">
                            <ItemTemplate>
                                <asp:Label ID="lblSIAttachment4" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT4_NAME") %>' />
                                <asp:ImageButton ID="imgSIBtnAttachment4" Height="30px" Width="30px" CommandArgument="ViewSIATTACHMENT4"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Add. Att.1">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment1" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT1_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment1" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT1"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Add. Att.2">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment2" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT2_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment2" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT2"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Add. Att.3">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment3" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT3_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment3" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT3"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Add. Att.4">
                            <ItemTemplate>
                                <asp:Label ID="lblAttachment4" runat="server" Visible="false" Text='<%# Eval("ATTACHMENT4_NAME") %>' />
                                <asp:ImageButton ID="imgBtnAttachment4" Height="30px" Width="30px" CommandArgument="ViewATTACHMENT4"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="TF_NO" HeaderText="TF No" />
                        <asp:BoundField DataField="DATE" HeaderText="Date" />
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="Status" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer Name" />
                        <asp:BoundField DataField="PO_NO" HeaderText="Customer PO No" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB No" />
                        <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT Main Item" />
                        <asp:BoundField DataField="LOT_MAIN_SUBITEM" HeaderText="Lot Main Subitem" />
                        <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No" />
                        <asp:BoundField DataField="EXPECTED_COMPLETION_DATE" HeaderText="Completion Required By" />
                        <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Subitem Desc" />
                        <asp:BoundField DataField="DRAWING_NO" HeaderText="Drawing No" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                        <asp:BoundField DataField="TAG_NO" HeaderText="Tag No" />
                        <asp:BoundField DataField="REVISION_NO" HeaderText="Revision No" />
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CATEGORY_ID") %>' />
                                <asp:Label ID="lblCategory" runat="server" Visible="true" Text='<%# Eval("CATEGORY") %>' />                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="IMP_NOTES" HeaderText="Imp Notes" />
                        
                        <asp:BoundField DataField="APPROVED_REMARKS" HeaderText="PM/PE Remarks" />
                        <asp:BoundField DataField="APPROVED_ON" HeaderText="PE/PM Approved On" />

                        <asp:BoundField DataField="PRODUCTION_ACCEPTED_REMARKS" HeaderText="Production Remarks" />
                        <asp:BoundField DataField="PRODUCTION_ACCEPTED_ON" HeaderText="Production Mngr Accepted On" />
                        
                        <asp:BoundField DataField="PLANNING_ACCEPTED_REMARKS" HeaderText="Plannig Remarks" />
                        <asp:BoundField DataField="PLANNING_ACCEPTED_ON" HeaderText="Planning Accepted On" />
                        
                        <asp:BoundField DataField="QUALITY_ACCEPTED_REMARKS" HeaderText="Quality Remarks" />
                        <asp:BoundField DataField="QUALITY_ACCEPTED_ON" HeaderText="Quality Accepted On" />
                        
                        <asp:BoundField DataField="AMENDED_REMARKS" HeaderText="Amended Remarks" />
                        <asp:BoundField DataField="AMENDED_ON" HeaderText="Amended On" />                                                                        
                        
                        <asp:BoundField DataField="ACTIVATION_TYPE" HeaderText="Activation_Type" />        --%>
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


    <%-- SHOW IMAGE FILE START--%>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowImageFile" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            <asp:Image ID="imgFile" runat="server" />
        </div>
    </asp:Panel>
    <%-- SHOW IMAGE FILE END--%>


    <%-- SHOW PDF FILE START--%>
    <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowPDFFile" runat="server" TargetControlID="btnShowPDFFile"
        PopupControlID="pnlViewPDFFilePopup" CancelControlID="imgBtnCancelPDFFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewPDFFilePopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPDFFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewPDFFile"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%-- SHOW PDF FILE END--%>


    <%-- VIEW DETAIL IN PDF START--%>
    <asp:Button ID="btnViewInPDF" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnViewInPDF"
        PopupControlID="pnlbtnViewInPDFPopup" CancelControlID="imgBtnViewInPDFPopup"
        BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlbtnViewInPDFPopup" runat="server" BackColor="White" Height="600px"
        Width="1050px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnViewInPDFPopup" ImageUrl="~/Images/cancelled_img.png"
                        runat="server" />
                </td>
            </tr>
        </table>
        <iframe style="margin-left: 25px; width: 1000px; height: 560px;" id="iframeViewTravelStatementInPDF"
            runat="server">
            <div style='overflow: auto; width: 1000px; height: 560px; border: 1px solid lightgray; margin-left: 25px;'>
            </div>
        </iframe>
    </asp:Panel>
    <%-- VIEW DETAIL IN PDF END--%>







    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
