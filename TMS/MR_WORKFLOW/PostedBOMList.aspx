<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PostedBOMList.aspx.cs" Inherits="MR_WORKFLOW_PostedBOMList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

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

        .textboxdrawings {
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
            background-color: transparent;
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





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdConfirmValueToU" runat="server" />


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Material Requisition List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">


                    <div class="full-width">
                        <label>Select/Unselect Dates</label>
                        <asp:CheckBox ID="chkSelectDates" runat="server"
                            Checked="true"
                            onchange="EnableDisableDates()" />

                        <asp:ImageButton ID="imgBtnClearAllFilters" runat="server"
                            ImageUrl="~/Images/NEWICONS/clear1.png"
                            Width="20px" Height="20px"
                            ToolTip="Clear Filters"
                            OnClientClick="return ClearAllFilters();" />

                        <asp:ImageButton ID="imgBtnAddNew" runat="server"
                            ImageUrl="~/Images/Icons/Add05.png"
                            Width="20px" Height="20px"
                            ToolTip="Add New BOM"
                            OnClick="imgBtnAddNew_Click" />
                    </div>



                    <label>Date Type</label>
                    <asp:DropDownList ID="ddlDateType" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="BOM Date" Value="BOM_DATE"></asp:ListItem>
                        <asp:ListItem Text="Mr date" Value="MR_DATE"></asp:ListItem>
                        <asp:ListItem Text="Delivery Required By" Value="DELIVERY_REQUIRED_BY"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Start Date</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDateSearch" runat="server" ReadOnly="true"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                <ajax:CalendarExtender ID="calendarStartDateSearch"
                                    PopupButtonID="imgbtnStartDateSearch"
                                    runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnStartDateSearch" runat="server"
                                    ImageUrl="~/Images/Calendar2.png"
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
                                <ajax:CalendarExtender ID="calendarEndDateSearch"
                                    PopupButtonID="imgbtnEndDateSearch"
                                    runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy"
                                    OnClientDateSelectionChanged="clientChangedSearch">
                                </ajax:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgbtnEndDateSearch" runat="server"
                                    ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="End Date Calendar" Width="20px" />
                            </td>
                        </tr>
                    </table>

                    <label>Unit</label>
                    <asp:DropDownList ID="ddlUnit" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                    </asp:DropDownList>

                    <label>Mr No.</label>
                    <asp:TextBox ID="txtMrNo" runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Mr Created By</label>
                    <asp:DropDownList ID="ddlMrCreatedBy" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>BOM No.</label>
                    <asp:TextBox ID="txtBOMNo" runat="server"
                        CssClass="form-control"></asp:TextBox>


                    <label>Job No.</label>
                    <asp:TextBox ID="txtJobNo" runat="server" CssClass="form-control"></asp:TextBox>


                    <label>Type</label>
                    <asp:DropDownList ID="ddlType" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Pivot Group</label>
                    <asp:DropDownList ID="ddlPivotGroup" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>


                    <label>Responsible For</label>
                    <asp:DropDownList ID="ddlResponsibleFor" runat="server"
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Is TC Required:</label>
                    <asp:DropDownList ID="ddlIsTCRequired" runat="server"
                        CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="2"></asp:ListItem>
                    </asp:DropDownList>


                    <div class="full-width button-group">

                        <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server"
                            Text="Search"
                            OnClientClick="return ValidateAllSearch();"
                            OnClick="btnSearch_Click" />

                    </div>
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
                ID="gvPostedBomList" runat="server"
                AutoGenerateColumns="false" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvPostedBomList_RowCommand"
                OnRowDataBound="gvPostedBomList_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:TemplateField HeaderText="Sr No."
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPid_InPostedList" runat="server" Text='<%# Eval("PID") %>' Visible="false" />
                            <asp:Label ID="lblUnitFid_InPostedList" runat="server" Text='<%# Eval("UNIT_FID") %>' Visible="false" />
                            <asp:Label ID="lblTypeFid_InPostedList" runat="server" Text='<%# Eval("TYPE_FID") %>' Visible="false" />
                            <asp:Label ID="lblMrNo_InPostedList" runat="server" Text='<%# Eval("MR_NO") %>' Visible="false" />
                            <asp:Label ID="lblMrDate_InPostedList" runat="server" Text='<%# Eval("MR_DATE") %>' Visible="false" />
                            <asp:Label ID="lblStatusFid_InPostedList" runat="server" Text='<%# Eval("STATUS_FID") %>' Visible="false" />
                            <asp:Label ID="lblStatus_InPostedList" runat="server" Text='<%# Eval("STATUS") %>' Visible="false" />
                            <asp:Label ID="lblBomNo_InPostedList" runat="server" Text='<%# Eval("BOM_NO") %>' Visible="false" />
                            <asp:Label ID="lblBomDate_InPostedList" runat="server" Text='<%# Eval("BOM_DATE") %>' Visible="false" />
                            <asp:Label ID="lblJobNo_InPostedList" runat="server" Text='<%# Eval("JOB_NO") %>' Visible="false" />
                            <asp:Label ID="lblDeliveryRequiredBy_InPostedList" runat="server" Text='<%# Eval("DELIVERY_REQUIRED_BY") %>' Visible="false" />
                            <asp:Label ID="lblAcceptableVendor1_InPostedList" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR1") %>' Visible="false" />
                            <asp:Label ID="lblAcceptableVendor2_InPostedList" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR2") %>' Visible="false" />
                            <asp:Label ID="lblAcceptableVendor3_InPostedList" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR3") %>' Visible="false" />
                            <asp:Label ID="lblAcceptableVendor4_InPostedList" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR4") %>' Visible="false" />
                            <asp:Label ID="lblAcceptableVendor5_InPostedList" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR5") %>' Visible="false" />
                            <asp:Label ID="lblRevisionNumber_InPostedList" runat="server" Text='<%# Eval("REVISION_NUMBER") %>' Visible="false" />
                            <asp:Label ID="lblBudgetedCost_InPostedList" runat="server" Text='<%# Eval("BUDGETED_COST") %>' Visible="false" />
                            <asp:Label ID="lblEstimatedCost_InPostedList" runat="server" Text='<%# Eval("ESTIMATED_COST") %>' Visible="false" />
                            <asp:Label ID="lblCostRelatedRemarks_InPostedList" runat="server" Text='<%# Eval("COST_RELATED_REMARKS") %>' Visible="false" />
                            <asp:Label ID="lblPivotGroupFid_InPostedList" runat="server" Text='<%# Eval("PIVOT_GROUP_FID") %>' Visible="false" />
                            <asp:Label ID="lblResponsibleForBomFid_InPostedList" runat="server" Text='<%# Eval("RESPONSIBLE_FOR_BOM_FID") %>' Visible="false" />
                            <asp:Label ID="lblIsTcRequired_InPostedList" runat="server" Text='<%# Eval("IS_TC_REQUIRED") %>' Visible="false" />
                            <asp:Label ID="lblCreatedByFid_InPostedList" runat="server" Text='<%# Eval("CREATED_BY_FID") %>' Visible="false" />
                            <asp:Label ID="lblCreatedRemarks_InPostedList" runat="server" Text='<%# Eval("CREATED_REMARKS") %>' Visible="false" />
                            <asp:Label ID="lblApprovedByFid_InPostedList" runat="server" Text='<%# Eval("APPROVED_BY_FID") %>' Visible="false" />
                            <asp:Label ID="lblApprovedRemarks_InPostedList" runat="server" Text='<%# Eval("APPROVED_REMARKS") %>' Visible="false" />
                            <asp:Label ID="lblAmendmentCount_InPostedList" runat="server" Text='<%# Eval("AMENDMENT_COUNT") %>' Visible="false" />
                            <asp:Label ID="lblAmendmentByFid_InPostedList" runat="server" Text='<%# Eval("AMENDMENT_BY_FID") %>' Visible="false" />
                            <asp:Label ID="lblAmendmentRemarks_InPostedList" runat="server" Text='<%# Eval("AMENDMENT_REMARKS") %>' Visible="false" />
                            <asp:Label ID="lblAmendedByFid_InPostedList" runat="server" Text='<%# Eval("AMENDED_BY_FID") %>' Visible="false" />
                            <asp:Label ID="lblAmendedRemarks_InPostedList" runat="server" Text='<%# Eval("AMENDED_REMARKS") %>' Visible="false" />
                            <asp:Label ID="lblAmendedApprovedByFid_InPostedList" runat="server" Text='<%# Eval("AMENDED_APPROVED_BY_FID") %>' Visible="false" />
                            <asp:Label ID="lblAmendedApprovedRemrks_InPostedList" runat="server" Text='<%# Eval("AMENDED_APPROVED_REMRKS") %>' Visible="false" />
                            <asp:Label ID="lblIsSentForApproval_InPostedList" runat="server" Text='<%# Eval("IS_SENT_FOR_APPROVAL") %>' Visible="false" />
                            <asp:Label ID="lblIsApprovalMailSent_InPostedList" runat="server" Text='<%# Eval("IS_APPROVAL_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsApprovedMailSent_InPostedList" runat="server" Text='<%# Eval("IS_APPROVED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendmentMailSent_InPostedList" runat="server" Text='<%# Eval("IS_AMENDMENT_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendedMailSent_InPostedList" runat="server" Text='<%# Eval("IS_AMENDED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblIsAmendedApprovedMailSent_InPostedList" runat="server" Text='<%# Eval("IS_AMENDED_APPROVED_MAIL_SENT") %>' Visible="false" />
                            <asp:Label ID="lblPeID_InPostedList" runat="server" Text='<%# Eval("PE_ID") %>' Visible="false" />
                            <asp:Label ID="lblPmID_InPostedList" runat="server" Text='<%# Eval("PM_ID") %>' Visible="false" />

                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("SR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Products"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <table width="10%">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnViewSubitemDetail"
                                            Height="50px" Width="50px"
                                            CommandArgument="VIEW_PRODUCT_LIST"
                                            runat="server"
                                            ImageUrl="~/Images/viewdetails.png"
                                            ToolTip="View Product Details" />
                                    </td>
                                    <td><b>[<asp:Label ID="lblProductsCountV" runat="server"
                                        Text='<%# Eval("PRODUCT_COUNTS") %>' />]</b></td>
                                </tr>
                            </table>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnViewDetail" CommandArgument="VIEW_DETAIL"
                                runat="server" ImageUrl="~/Images/pdficon1.png"
                                Height="50px" Width="50px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEditLOT"
                                CommandArgument="EDIT"
                                runat="server"
                                ImageUrl="~/Images/LOT/edit5.png"
                                Height="50px" Width="50px"
                                ToolTip="Edit BOM" />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Status"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton
                                ID="imgStatus"
                                CommandArgument="STATUS"
                                runat="server"
                                Enabled="false"
                                Height="50px" Width="50px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Send Mail"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnSendMail"
                                CommandArgument="SEND_MAIL"
                                runat="server" ImageUrl="~/Images/NEWICONS/email05.png"
                                Height="50px" Width="50px"
                                ToolTip="Send Email" />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Button ID="btnApproveBOM" CommandArgument="APPROVE"
                                ToolTip="Approve BOM" runat="server"
                                Text="Approve"
                                CssClass="cancelbutton"
                                Width="100%"
                                BorderColor="Yellow"
                                BorderStyle="Solid"
                                BorderWidth="2px" />


                            <asp:Button ID="btnAmendBOM" CommandArgument="AMEND"
                                ToolTip="Amend BOM" runat="server"
                                Text="Amend"
                                CssClass="cancelbutton"
                                Width="100%"
                                BorderColor="Yellow"
                                BorderStyle="Solid"
                                BorderWidth="2px" />

                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Cancel"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnCancel"
                                CommandArgument="CANCEL"
                                runat="server" ImageUrl="~/Images/Icons/REMOVE03.png"
                                Height="50px" Width="50px"
                                ToolTip="Cancel MR" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server"
                                Text='<%# Eval("TYPE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>

                            <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("UNIT_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mr No">
                        <ItemTemplate>

                            <asp:Label ID="lblMrNo" runat="server" Text='<%# Eval("MR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mr Date">
                        <ItemTemplate>

                            <asp:Label ID="lblMrDate" runat="server" Text='<%# Eval("MR_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bom No">
                        <ItemTemplate>

                            <asp:Label ID="lblBomNo" runat="server" Text='<%# Eval("BOM_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bom Date">
                        <ItemTemplate>

                            <asp:Label ID="lblBomDate" runat="server" Text='<%# Eval("BOM_DATE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Job No">
                        <ItemTemplate>

                            <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delivery Required By"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblDeliveryRequiredBy" runat="server" Text='<%# Eval("DELIVERY_REQUIRED_BY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acceptable Vendor1">
                        <ItemTemplate>
                            <asp:Label ID="lblAcceptableVendor1" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR1") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acceptable Vendor2">
                        <ItemTemplate>
                            <asp:Label ID="lblAcceptableVendor2" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR2") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acceptable Vendor3">
                        <ItemTemplate>
                            <asp:Label ID="LlblAcceptableVendor3" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR3") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acceptable Vendor4">
                        <ItemTemplate>
                            <asp:Label ID="lblAcceptableVendor4" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR4") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acceptable Vendor5">
                        <ItemTemplate>
                            <asp:Label ID="lblAcceptableVendor5" runat="server" Text='<%# Eval("ACCEPTABLE_VENDOR5") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Revision Number">
                        <ItemTemplate>
                            <asp:Label ID="lblRevisionNumber" runat="server" Text='<%# Eval("REVISION_NUMBER") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Budgeted Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblBudgetedCost" runat="server" Text='<%# Eval("BUDGETED_COST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Estimated Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblEstimatedCost" runat="server" Text='<%# Eval("ESTIMATED_COST") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cost Related Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblCostRelatedRemarks" runat="server" Text='<%# Eval("COST_RELATED_REMARKS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Pivot Group">
                        <ItemTemplate>
                            <asp:Label ID="lblPivotGroup" runat="server" Text='<%# Eval("PIVOT_GROUP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Responsible For">
                        <ItemTemplate>
                            <asp:Label ID="lblResponsibleForBom" runat="server" Text='<%# Eval("RESPONSIBLE_FOR_BOM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Is TC Required"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsTCRequired" Enabled="false"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amendment Count">
                        <ItemTemplate>
                            <asp:Label ID="lblAmendmentCount" runat="server" Text='<%# Eval("AMENDMENT_COUNT") %>' />
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






    <%-- EDIT/UPDATE START--%>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddUpdate" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <fieldset style="width: 97%; margin-left: 17px; margin-top: 10px;">
            <%--<legend style="text-align: center;">TF No. [<asp:Label ID="lblBOM" runat="server" />] Details</legend>--%>
            <legend style="text-align: center;">[
                <asp:Label ID="lblMrNoLegend" runat="server" Font-Bold="true" />
                ] Material Requisition Details</legend>
            <div style='overflow: auto; width: 99%; height: 680px; border: 1px solid lightgray; margin-left: 5px;'>

                <table width="95%" align="center">
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="9" align="center">
                            <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server" Height="50px">
                                <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="True" Font-Size="Large" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>Bom Date:</td>
                        <td>
                            <asp:TextBox ID="txtBomDateToU" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>&nbsp;</td>

                        <td>Bom No.:</td>
                        <td>
                            <asp:TextBox ID="txtBomNoToU" runat="server" Width="100%" Enabled="false" />
                        </td>

                        <td>&nbsp;</td>

                        <td>Job No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNoToU" runat="server" Width="100%" Enabled="false" />
                        </td>

                        <td>&nbsp;</td>

                        <td>
                            <asp:Button ID="btnGetBomDetailList" CssClass="button" Width="100%" runat="server"
                                Text="Get List"
                                OnClick="btnGetBomDetailList_Click" />
                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <%--Product List of selected BOM--%>
                <table width="95%" align="center">
                    <tr>
                        <td>
                            <div align="center">
                                <fieldset style="width: 100%;">
                                    <legend style="text-align: center;">
                                        <asp:Label ID="lblProductListRecords" runat="server" Text="Products Records[0]" />
                                    </legend>
                                    <%--<div style='overflow-y: scroll; overflow-x: scroll; width: 1200px; height: 300px; border: 1px solid lightgray;'>--%>

                                    <asp:HiddenField ID="hdScrollPositionY" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdScrollPositionX" runat="server" Value="0" />

                                    <div id="gridContainer" runat="server"
                                        style='overflow-y: auto; overflow-x: auto; width: 1200px; height: 300px; border: 1px solid lightgray;'
                                        onscroll="SetDivPosition()">

                                        <asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                                            OnRowDataBound="gvProductList_RowDataBound">
                                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                        <asp:Label ID="lblHPID" runat="server" Text='<%# Eval("H_PID") %>' Visible="false" />
                                                        <asp:Label ID="lblLPID" runat="server" Text='<%# Eval("L_PID") %>' Visible="false" />
                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                                        <asp:Label ID="lblProductDescription" runat="server" Text='<%# Eval("PRODUCT_DESCRIPTION") %>' Visible="false" />
                                                        <asp:Label ID="lblAdditionalDescription" runat="server" Text='<%# Eval("ADDITIONAL_DESCRIPTION") %>' Visible="false" />
                                                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />

                                                        <asp:Label ID="lblTotalQuantity" runat="server" Text='<%# Eval("TOTAL_QUANTITY") %>' Visible="false" />
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TOTAL_AMOUNT") %>' Visible="false" />
                                                        <asp:Label ID="lblPerUnitAmount" runat="server" Text='<%# Eval("PER_UNIT_AMOUNT") %>' Visible="false" />

                                                        <asp:Label ID="lblPostedQuantity" runat="server" Text='<%# Eval("POSTED_QUANTITY") %>' Visible="false" />
                                                        <asp:Label ID="lblPostedAmount" runat="server" Text='<%# Eval("POSTED_AMOUNT") %>' Visible="false" />

                                                        <asp:Label ID="lblRemainingQuantity" runat="server" Text='<%# Eval("REMAINING_QUANTITY") %>' Visible="false" />
                                                        <asp:Label ID="lblRemainingAmount" runat="server" Text='<%# Eval("REMAINING_AMOUNT") %>' Visible="false" />

                                                        <%--<asp:Label ID="lblIsBomFid" runat="server" Text='<%# Eval("IS_BOM_FID") %>' Visible="false" />--%>

                                                        <asp:CheckBox runat="server" ID="chkSelect"
                                                            OnCheckedChanged="chkSelect_CheckedChanged"
                                                            AutoPostBack="true"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>'
                                                            Width="50PX"
                                                            onkeyDown="javascript:preventInput(event);"
                                                            CssClass="textboxcenter">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />

                                                <asp:TemplateField HeaderText="Product Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProductDescription" runat="server"
                                                            Text='<%# Eval("PRODUCT_DESCRIPTION") %>'
                                                            Width="300PX"
                                                            CssClass="textboxleft"
                                                            TextMode="Multiline"
                                                            Rows="2">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Additional Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAdditionalDescription" runat="server"
                                                            Text='<%# Eval("ADDITIONAL_DESCRIPTION") %>'
                                                            Width="300PX"
                                                            CssClass="textboxleft"
                                                            TextMode="Multiline"
                                                            Rows="2">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="UOM" HeaderText="UOM" />

                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("REMAINING_QUANTITY") %>'
                                                            Width="50PX"
                                                            onkeyDown="javascript:preventInput(event);"
                                                            CssClass="textboxright">
                                                        </asp:TextBox>--%>

                                                        <asp:DropDownList ID="ddlQuantity" Width="90px" Height="26px" runat="server"
                                                            OnSelectedIndexChanged="ddlQuantity_SelectedIndexChanged" AutoPostBack="true" />


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("REMAINING_AMOUNT") %>'
                                                            Width="100PX"
                                                            onkeyDown="javascript:preventInput(event);"
                                                            CssClass="textboxright">
                                                        </asp:TextBox>
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
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <table width="95%" align="center">
                    <tr>
                        <td style="width: 15%;">Unit:</td>
                        <td style="width: 30%;">
                            <asp:DropDownList ID="ddlUnitToU" runat="server" Width="100%" Height="23px">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 15%;">Type:</td>
                        <td style="width: 30%;">
                            <asp:DropDownList ID="ddlTypeToU" runat="server" Width="100%" Height="23px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>Mr Date:</td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtMrDateToU" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                        <asp:HiddenField ID="hdMrDateToU" runat="server" />
                                        <ajax:CalendarExtender ID="calendarMrDateToU" PopupButtonID="imgbtnMrDateToU"
                                            runat="server" TargetControlID="txtMrDateToU" Format="dd-MMM-yyyy"
                                            OnClientDateSelectionChanged="clientChangedMrDate">
                                        </ajax:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgbtnMrDateToU" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Mr Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                        <td>Delivery Required By:</td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDeliveryRequiredByToU" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                        <asp:HiddenField ID="hdDeliveryRequiredByToU" runat="server" />
                                        <ajax:CalendarExtender ID="calendarDeliveryRequiredByToU" PopupButtonID="imgbtnDeliveryRequiredByToU"
                                            runat="server" TargetControlID="txtDeliveryRequiredByToU" Format="dd-MMM-yyyy"
                                            OnClientDateSelectionChanged="clientChangedDeliveryRequiredBy">
                                        </ajax:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="imgbtnDeliveryRequiredByToU" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Delivery Required By Date Calendar" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td>Acceptable Vendor 1:</td>
                        <td style="width: 30%;">
                            <asp:TextBox ID="txtAcceptableVendor1ToU" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Acceptable Vendor 2:</td>
                        <td>
                            <asp:TextBox ID="txtAcceptableVendor2ToU" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>

                        <td>Acceptable Vendor 3:</td>
                        <td>
                            <asp:TextBox ID="txtAcceptableVendor3ToU" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;</td>

                        <td>Acceptable Vendor 4:</td>
                        <td>
                            <asp:TextBox ID="txtAcceptableVendor4ToU" runat="server" Width="100%" />
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>

                        <td>Acceptable Vendor 5:</td>
                        <td>
                            <asp:TextBox ID="txtAcceptableVendor5ToU" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Revision No.:</td>
                        <td>
                            <asp:TextBox ID="txtRevisionNoToU" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>Budgeted Cost:</td>
                        <td>
                            <asp:TextBox ID="txtBudgetedCostToU" runat="server" Width="100%"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);"
                                Text="0" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Estimated Cost:</td>
                        <td>
                            <asp:TextBox ID="txtEstimatedCostToU" runat="server" Width="100%"
                                onpaste="return false"
                                onkeypress="return inNumberKeyWithDecimal(this, event);"
                                Text="0" />

                        </td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Cost Related Remarks:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtCostRelatedRemarksToU" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Pivot Group:</td>
                        <td>
                            <asp:DropDownList ID="ddlPivotGroupToU" runat="server" Width="100%" Height="23px">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="A35" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Gnu" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>Responsible for Po:</td>
                        <td>
                            <asp:DropDownList ID="ddlResponsibleForBOMToU" runat="server" Width="100%" Height="23px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <asp:Panel ID="pnlCreatedRemarksToU" runat="server" Visible="false">
                        <tr>
                            <td>Created Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtCreatedRemarksToU" runat="server" Width="100%" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </asp:Panel>

                    <asp:Panel ID="pnlAmendmentRemarksToU" runat="server" Visible="false">
                        <tr>
                            <td>Amendment Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAmendmentRemarksToU" runat="server" Width="100%" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </asp:Panel>

                    <asp:Panel ID="pnlEditAmendedRemarksToU" runat="server" Visible="false">
                        <tr>
                            <td>Edit/Amended Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtEditAmendedRemarksToU" runat="server" Width="100%" TextMode="MultiLine" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </asp:Panel>



                    <tr>
                        <td>Remarks:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtRemarksToU" runat="server" Width="100%" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Is TC Required?:</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkIsTCRequiredToU"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>


                    <asp:Panel ID="pnlSavingTypeToU" runat="server">
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdSavingType" runat="server" RepeatDirection="Horizontal" Width="100%">
                                    <asp:ListItem Text="Save for later" Value="0" Selected="True" />
                                    <asp:ListItem Text="Save & send for approval" Value="1" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </asp:Panel>


                    <asp:Panel ID="pnlSave" runat="server">
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="4">
                                <asp:Button ID="btnSave"
                                    runat="server"
                                    Width="100%"
                                    Text="Save" CssClass="button"
                                    OnClick="btnSave_Click"
                                    OnClientClick="return ValidateAllSave();" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </asp:Panel>


                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2" style="width: 45%;">
                            <asp:Panel ID="pnlUpdateStatus" runat="server">
                                <asp:Button ID="btnUpdateStatus"
                                    runat="server"
                                    Width="100%"
                                    Text="Update Status" CssClass="button"
                                    OnClick="btnUpdateStatus_Click"
                                    OnClientClick="return ValidateAllSave();" />
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 45%;">
                            <asp:Panel ID="pnlSendToAmendment" runat="server">
                                <asp:Button ID="btnSendToAmendment"
                                    runat="server"
                                    Width="100%"
                                    Text="Send To Amendment"
                                    CssClass="button"
                                    OnClick="btnSendToAmendment_Click"
                                    OnClientClick="return ValidateAllRemarks();" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </fieldset>


    </asp:Panel>
    <%-- EDIT/UPDATE STATUS END --%>


    <%-- EDIT/UPDATE START--%>
    <asp:Button ID="btnShowPopupShowBomList" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeShowBomList" runat="server" TargetControlID="btnShowPopupShowBomList"
        PopupControlID="pnlPopupShowBomList" CancelControlID="imgBtnCancelShowBomList" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupShowBomList" runat="server" BackColor="White" Height="630px" Width="1000px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelShowBomList" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>BOM List:
                    <asp:Label ID="lblBOMListRecords" runat="server" Text="BOM Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Bom From Date:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtBomStartDateSearchToS" runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdBomStartDateSearchToS" runat="server" />
                                    <ajax:CalendarExtender ID="calendarBomStartDateSearchToS"
                                        PopupButtonID="imgbtnBomStartDateSearchToS"
                                        runat="server" TargetControlID="txtBomStartDateSearchToS" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedBomSearch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnBomStartDateSearchToS" runat="server"
                                        ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Bom Start Date Calendar" />
                                </td>
                            </tr>
                        </table>

                        <label>Bom To Date</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtBomEndDateSearchToS" runat="server" ReadOnly="true"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:HiddenField ID="hdBomEndDateSearchToS" runat="server" />
                                    <ajax:CalendarExtender ID="calendarBomEndDateSearchToS"
                                        PopupButtonID="imgbtnBomEndDateSearchToS"
                                        runat="server" TargetControlID="txtBomEndDateSearchToS" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedBomSearch">
                                    </ajax:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnBomEndDateSearchToS" runat="server"
                                        ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Bom End Date Calendar" Width="20px" />

                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSelectDeselectBomDate" runat="server"
                                        Checked="true"
                                        onchange="EnableDisableBOMDates()" />
                                </td>
                            </tr>
                        </table>

                        <label>Bom No.</label>
                        <asp:TextBox ID="txtBomNoToS" runat="server" CssClass="form-control"/>


                        <label>Job No.</label>
                        <asp:TextBox ID="txtJobNoToS" runat="server" CssClass="form-control"/>


                        <div class="full-width button-group">

                            <asp:Button ID="btnSearchBomList" CssClass="button" Width="100%" runat="server"
                                Text="Get List"
                                OnClick="btnSearchBomList_Click" />

                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
               
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvBomList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvBomList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>

                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                <asp:Label ID="lblBomNo" runat="server" Text='<%# Eval("BOM_NO") %>' Visible="false" />
                                <asp:Label ID="lblBomDate" runat="server" Text='<%# Eval("BOM_DATE") %>' Visible="false" />
                                <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JOB_NO") %>' Visible="false" />
                                <%--<asp:Label ID="lblPivotGroup" runat="server" Text='<%# Eval("PIVOT_GROUP") %>' Visible="false" />--%>

                                <asp:Button ID="btnGetBOM"
                                    CommandArgument="GET_BOM"
                                    ToolTip="Get BOM"
                                    runat="server"
                                    Text="Get BOM"
                                    CssClass="cancelbutton"
                                    Width="95%"
                                    BorderColor="Yellow"
                                    BorderStyle="Solid"
                                    BorderWidth="2px" />

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="BOM_NO" HeaderText="Bom No" />
                        <asp:BoundField DataField="BOM_DATE" HeaderText="Bom Date" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="Job No" />
                        <%--<asp:BoundField DataField="PIVOT_GROUP" HeaderText="Pivot Group" />--%>
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
    <%-- EDIT/UPDATE STATUS END --%>


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


    <%-- SHOW SUBITEM DETAIL START--%>
    <asp:Button ID="btnShowProductDetailFile" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeProductDetail" runat="server" TargetControlID="btnShowProductDetailFile" BehaviorID="mpeProductDetailBID"
        PopupControlID="pnlViewProductDetailPopup" CancelControlID="imgBtnCancelProductDetailFile" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlViewProductDetailPopup" runat="server" BackColor="White" Height="500px" Width="1200px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelProductDetailFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>Add Products:
                    <asp:Label ID="lblPostedProductRecors" runat="server" Text="Products Records[0]" />
                    </legend>

                    <div class="employee-grid-container">

                        <asp:GridView
                            CssClass="employee-grid"
                            ID="gvPostedProductsList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSrNoPPL" runat="server" Text='<%# Eval("SR_NO") %>'
                                            Width="50PX"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textboxcenter">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="PRODUCT_CODE" HeaderText="Product Code" />

                                <asp:TemplateField HeaderText="Product Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProductDescriptionPPL" runat="server"
                                            Text='<%# Eval("PRODUCT_DESCRIPTION") %>'
                                            Width="300PX"
                                            CssClass="textboxleft"
                                            TextMode="Multiline"
                                            Rows="2">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Additional Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdditionalDescriptionPPL" runat="server"
                                            Text='<%# Eval("ADDITIONAL_DESCRIPTION") %>'
                                            Width="300PX"
                                            CssClass="textboxleft"
                                            TextMode="Multiline"
                                            Rows="2">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="UOM" HeaderText="UOM" />

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantityPPL" runat="server" Text='<%# Eval("POSTED_QUANTITY") %>'
                                            Width="50PX"
                                            onkeyDown="javascript:preventInput(event);"
                                            CssClass="textboxright">
                                        </asp:TextBox>
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
                </fieldset>
            </div>
        </div>

    </asp:Panel>
    <%-- SHOW SUBITEM DETAIL END--%>


    <%-- ADD LOT JOB APPROVERS START --%>
    <asp:Button ID="btnShowAddApproversPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeAddApprovers" runat="server" TargetControlID="btnShowAddApproversPopup"
        PopupControlID="pnlAddApprovers" CancelControlID="imgBtnCancelAddApprovers" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlAddApprovers" runat="server"
        CssClass="popup-pdf">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelAddApprovers" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <iframe
            class="popup-iframe"
            id="iframeAddApprovers"
            runat="server"></iframe>
    </asp:Panel>
    <%-- ADD LOT JOB APPROVERS END --%>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>

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

            if (document.getElementById('<%=chkSelectDeselectBomDate.ClientID %>').checked) {
                document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomStartDateSearchToS.ClientID %>').value;
                document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomEndDateSearchToS.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value = "";
                document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value = "";
            }


            <%--document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomStartDateSearchToS.ClientID %>').value;
            document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomEndDateSearchToS.ClientID %>').value;--%>
        }


        function clientChangedMrDate(sender, args) {
            document.getElementById('<%=hdMrDateToU.ClientID %>').value = document.getElementById('<%=txtMrDateToU.ClientID %>').value;
        }

        function clientChangedDeliveryRequiredBy(sender, args) {
            document.getElementById('<%=hdDeliveryRequiredByToU.ClientID %>').value = document.getElementById('<%=txtDeliveryRequiredByToU.ClientID %>').value;
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

        function clientChangedBomSearch(sender, args) {
            document.getElementById('<%=hdBomStartDateSearchToS.ClientID %>').value = document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value;
            document.getElementById('<%=hdBomEndDateSearchToS.ClientID %>').value = document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdBomStartDateSearchToS.ClientID %>').value.split("-");
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
            var endDateItems = document.getElementById('<%=hdBomEndDateSearchToS.ClientID %>').value.split("-");
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


        function EnableDisableDates() {
            var chk = document.getElementById('<%=chkSelectDates.ClientID %>').checked;

            if (chk == true) {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtStartDateSearch.ClientID %>').value = "";
                document.getElementById('<%=txtEndDateSearch.ClientID %>').value = "";
            }
        }

        function EnableDisableBOMDates() {
            var chk = document.getElementById('<%=chkSelectDeselectBomDate.ClientID %>').checked;

            if (chk == true) {
                document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomStartDateSearchToS.ClientID %>').value;
                document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value = document.getElementById('<%=hdBomEndDateSearchToS.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtBomStartDateSearchToS.ClientID %>').value = "";
                document.getElementById('<%=txtBomEndDateSearchToS.ClientID %>').value = "";
            }
        }


        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        window.onload = function () {

            var currentPosX = document.getElementById("<%=hdScrollPositionX.ClientID%>").value;
            var currentPosY = document.getElementById("<%=hdScrollPositionY.ClientID%>").value;

            var strCookX = currentPosX;
            var strCookY = currentPosY;


            document.getElementById("<%=gridContainer.ClientID%>").scrollLeft = strCookX;
            document.getElementById("<%=gridContainer.ClientID%>").scrollTop = strCookY;
        }

        function SetDivPosition() {
            var intX = document.getElementById("<%=gridContainer.ClientID%>").scrollLeft;
            var intY = document.getElementById("<%=gridContainer.ClientID%>").scrollTop;

            document.getElementById("<%=hdScrollPositionX.ClientID%>").value = intX
            document.getElementById("<%=hdScrollPositionY.ClientID%>").value = intY
        }

    </script>

    <script type="text/javascript">

        function ValidateType() {
            var Type = document.getElementById('<%=ddlTypeToU.ClientID %>').selectedIndex;
            if (Type == '' || Type == '0') {
                document.getElementById('<%=ddlTypeToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAcceptableVendor1() {
            var AcceptableVendor = document.getElementById('<%=txtAcceptableVendor1ToU.ClientID %>').value;
            if (AcceptableVendor == '') {
                document.getElementById('<%=txtAcceptableVendor1ToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAcceptableVendor1ToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAcceptableVendor2() {
            var AcceptableVendor = document.getElementById('<%=txtAcceptableVendor2ToU.ClientID %>').value;
            if (AcceptableVendor == '') {
                document.getElementById('<%=txtAcceptableVendor2ToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAcceptableVendor2ToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAcceptableVendor3() {
            var AcceptableVendor = document.getElementById('<%=txtAcceptableVendor3ToU.ClientID %>').value;
            if (AcceptableVendor == '') {
                document.getElementById('<%=txtAcceptableVendor3ToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAcceptableVendor3ToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAcceptableVendor4() {
            var AcceptableVendor = document.getElementById('<%=txtAcceptableVendor4ToU.ClientID %>').value;
            if (AcceptableVendor == '') {
                document.getElementById('<%=txtAcceptableVendor4ToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAcceptableVendor4ToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAcceptableVendor5() {
            var AcceptableVendor = document.getElementById('<%=txtAcceptableVendor5ToU.ClientID %>').value;
            if (AcceptableVendor == '') {
                document.getElementById('<%=txtAcceptableVendor5ToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtAcceptableVendor5ToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateRevisionNo() {
            var RevisionNo = document.getElementById('<%=txtRevisionNoToU.ClientID %>').value;
            if (RevisionNo == '') {
                document.getElementById('<%=txtRevisionNoToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtRevisionNoToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateBudgetedCost() {
            var BudgetedCost = document.getElementById('<%=txtBudgetedCostToU.ClientID %>').value;
            if (BudgetedCost == '' || BudgetedCost == 0) {
                document.getElementById('<%=txtBudgetedCostToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBudgetedCostToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEstimatedCost() {
            var EstimatedCost = document.getElementById('<%=txtEstimatedCostToU.ClientID %>').value;
            if (EstimatedCost == '' || EstimatedCost == 0) {
                document.getElementById('<%=txtEstimatedCostToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEstimatedCostToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatePivotGroup() {
            var PivotGroup = document.getElementById('<%=ddlPivotGroupToU.ClientID %>').selectedIndex;
            if (PivotGroup == '' || PivotGroup == '0') {
                document.getElementById('<%=ddlPivotGroupToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPivotGroupToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateResponsibleForPo() {
            var ResponsibleForPo = document.getElementById('<%=ddlResponsibleForBOMToU.ClientID %>').selectedIndex;
            if (ResponsibleForPo == '' || ResponsibleForPo == '0') {
                document.getElementById('<%=ddlResponsibleForBOMToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlResponsibleForBOMToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllSave() {

            var check = true;

            if (ValidateType()) { check = false; }
            if (ValidateAcceptableVendor1()) { check = false; }
            if (ValidateAcceptableVendor2()) { check = false; }
            if (ValidateAcceptableVendor3()) { check = false; }
            if (ValidateAcceptableVendor4()) { check = false; }
            if (ValidateAcceptableVendor5()) { check = false; }
            if (ValidateRevisionNo()) { check = false; }
            if (ValidateBudgetedCost()) { check = false; }
            if (ValidateEstimatedCost()) { check = false; }
            if (ValidatePivotGroup()) { check = false; }
            if (ValidateResponsibleForPo()) { check = false; }

            if (check) {
                if (confirm("Would you like to submit?")) {
                    document.getElementById('<%=hdConfirmValueToU.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValueToU.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }
        }


    </script>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


