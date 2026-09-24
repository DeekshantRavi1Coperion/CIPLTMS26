<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateStatusLOTDetail.aspx.cs" Inherits="PROJECT_LOT_UpdateStatusLOTDetail" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIPLTMS- Project Manager Approval/Amendment</title>
    <link rel="icon" href="../../Images/Icon04.png" />

    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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

</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <asp:HiddenField ID="hdUpdationFlag" runat="server" />

        <asp:HiddenField ID="hdPEID" runat="server" />
        <asp:HiddenField ID="hdPMID" runat="server" />
        <asp:HiddenField ID="hdProductionManagerIDFlag" runat="server" />
        <asp:HiddenField ID="hdTableName" runat="server" />
        <asp:HiddenField ID="hdTFNo" runat="server" />
        <asp:HiddenField ID="hdStatusID" runat="server" />

        <%--<asp:UpdatePanel runat="server" ID="uppanel">
            <ContentTemplate>--%>
        <%--<div align="center" style="margin-top: 30px;">--%>


        <div style='margin-top: 50px;' align="center">
            <fieldset style="width: 1000px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLOTDetailsLegendTxt" runat="server"></asp:Label>LOT Transmittal Detail
                </legend>

                <div style='overflow-y: scroll; overflow-x: hidden; width: 100%; height: 700px; border: 1px solid lightgray;'>
                    <table style="width: 900px" align="center">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">&nbsp;</td>
                            <td colspan="4">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>Company:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompany" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 18%;">JOB Number:
                            </td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>PO Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPONo" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 18%;">Customer Name:
                            </td>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 85%">
                                            <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" Width="100%" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;</td>
                            <td>TF Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTFNo" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Item:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtItemName" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div align="center">
                                    <fieldset style="width: 100%;">
                                        <legend style="text-align: center;">
                                            <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" /></legend>
                                        <div style='overflow-y: scroll; overflow-x: scroll; width: 900px; height: 300px; border: 1px solid lightgray;'>
                                            <asp:GridView ID="gvSubitems" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                                                OnRowCommand="gvSubitems_RowCommand" OnRowDataBound="gvSubitems_RowDataBound">
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr.No.">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Visible="false" />
                                                            <asp:Label ID="lblLOTTFID" runat="server" Text='<%# Eval("LOT_TF_ID") %>' Visible="false" />
                                                            <asp:Label ID="lblLOTTFSubitemID" runat="server" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' Visible="false" />
                                                            <asp:Label ID="lblLOTMainItemID" runat="server" Text='<%# Eval("LOT_MAIN_ITEM_ID") %>' Visible="false" />
                                                            <asp:Label ID="lblLOTMainSubitemID" runat="server" Text='<%# Eval("LOT_MAIN_SUBITEM_ID") %>' Visible="false" />
                                                            <asp:Label ID="lblLOTFor" runat="server" Text='<%# Eval("LOT_MAIN_ITEM") %>' Visible="false" />
                                                            <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("TAG_NO") %>' Visible="false" />

                                                            <asp:Label ID="lblProductionNumber" runat="server" Text='<%# Eval("PRODUCTION_ORDER_NO") %>' Visible="false" />
                                                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' Visible="false" />

                                                            <asp:Label ID="lblProductionOrderDate" runat="server" Text='<%# Eval("PRODUCTION_ORDER_DATE") %>' Visible="false" />
                                                            <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("PRODUCT_CODE") %>' Visible="false" />
                                                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("PRODUCT_DESC") %>' Visible="false" />
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' Visible="false" />
                                                            <asp:Label ID="lblIsPartOfProductionStatusReport" runat="server" Text='<%# Eval("IS_PART_OF_PRODUCTION_STATUS_REPORT_ID") %>' Visible="false" />

                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("SUBITEM_DESC") %>' Visible="false" />
                                                            <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Visible="false" />
                                                            <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REVISION_NO") %>' Visible="false" />
                                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CATEGORY_ID") %>' Visible="false" />
                                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="false" />
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Visible="false" />

                                                            <asp:Label ID="lblCreatedByID" runat="server" Visible="false" Text='<%# Eval("CREATED_BY") %>' />
                                                            <asp:Label ID="lblApprovedByID" runat="server" Visible="false" Text='<%# Eval("APPROVED_BY") %>' />
                                                            <asp:Label ID="lblAcceptedByID" runat="server" Visible="false" Text='<%# Eval("ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblQualityAcceptedByID" runat="server" Visible="false" Text='<%# Eval("QUALITY_ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblPlanningAcceptedByID" runat="server" Visible="false" Text='<%# Eval("PLANNING_ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblAmendmentCount" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_COUNT") %>' />
                                                            <asp:Label ID="lblAmendmentByID" runat="server" Visible="false" Text='<%# Eval("AMENDMENT_BY") %>' />
                                                            <asp:Label ID="lblAmendedByID" runat="server" Visible="false" Text='<%# Eval("AMENDED_BY") %>' />
                                                            <asp:Label ID="lblAmendedApprovedByID" runat="server" Visible="false" Text='<%# Eval("AMENDED_APPROVED_BY") %>' />
                                                            <asp:Label ID="lblAmendedAcceptedByID" runat="server" Visible="false" Text='<%# Eval("AMENDED_ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblAmendedQualityAcceptedByID" runat="server" Visible="false" Text='<%# Eval("AMENDED_QUALITY_ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblAmendedPlanningAcceptedByID" runat="server" Visible="false" Text='<%# Eval("AMENDED_PLANNING_ACCEPTED_BY") %>' />
                                                            <asp:Label ID="lblCompletedByID" runat="server" Visible="false" Text='<%# Eval("COMPLETED_BY") %>' />
                                                            <asp:Label ID="lblRevisedByID" runat="server" Visible="false" Text='<%# Eval("REVISED_BY") %>' />

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

                                                    <asp:BoundField DataField="LOT_MAIN_ITEM" HeaderText="LOT For" />

                                                    <asp:TemplateField HeaderText="Tag No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTagNoInList" runat="server" Text='<%# Eval("TAG_NO") %>' Style="text-transform: uppercase" Width="150PX"
                                                                CssClass="textboxtagno" MaxLength="15" onkeyDown="javascript:preventInput(event);"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="SUBITEM_DESC" HeaderText="Description" />
                                                    <asp:BoundField DataField="DRAWING_NO" HeaderText="Drg/DOC.No" />

                                                    <asp:TemplateField HeaderText="Rev.No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRevNo" runat="server" Text='<%# Eval("REVISION_NO_TEXT") %>' Width="30px"
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
                                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="30px"
                                                                onkeyDown="javascript:preventInput(event);" CssClass="textboxright"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Is Part Of Prod. Status Report">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIsPartOfProductStatusReport" runat="server" Enabled="false" />
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
                        <tr>
                            <td>Important Notes:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Amended Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAmendedRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>View Add. Att.:</td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment1" runat="server" Width="100%" Enabled="false" />
                                            <asp:HiddenField ID="hdViewAttachment1" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment1" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                            <td style="visibility: hidden">View Add. Att. 2:</td>
                            <td style="visibility: hidden">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment2" runat="server" Width="100%" Enabled="false" />
                                            <asp:HiddenField ID="hdViewAttachment2" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment2" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <panel runat="server" visible="false">
                             <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>View Add. Att. 3:</td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment3" runat="server" Width="100%" Enabled="false" />
                                            <asp:HiddenField ID="hdViewAttachment3" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment3" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment3_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                            <td>View Add. Att. 4:</td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtViewAttachment4" runat="server" Width="100%" Enabled="false" />
                                            <asp:HiddenField ID="hdViewAttachment4" runat="server" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnViewAttachment4" Height="20px" Width="20px" runat="server"
                                                OnClick="btnViewAttachment4_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                           
                        </panel>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <asp:Panel ID="pnlAmendmentRemarks" runat="server" Visible="false">
                            <tr>
                                <td>Amendment Remarks:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtAmendmentRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </asp:Panel>

                        <%--<asp:Panel ID="pnlAmendedRemarks" runat="server" Visible="false">
                            <tr>
                                <td>Amended Remarks:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtAmendedRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </asp:Panel>--%>

                        <asp:Panel ID="pnlPERemarks" runat="server" Visible="false">
                            <tr>
                                <td>Project Engineer Remarks:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtPERemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </asp:Panel>

                        <asp:Panel ID="pnlPMRemarks" runat="server" Visible="false">
                            <tr>
                                <td>Project Manager Remarks:</td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtPMRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </asp:Panel>

                        <tr>
                            <td>Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Approve LOT" CssClass="button"
                                                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" /></td>
                                        <td>&nbsp;</td>
                                        <td style="width: 45%;">
                                            <asp:Button ID="btnAmendment" runat="server" Width="100%" Text="Send To Amendment" CssClass="button"
                                                OnClick="btnAmendment_Click" OnClientClick="return ValidateRemarksForAmendment();" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Button ID="btnLOTList" runat="server" Width="100%" Text="Go To LOT List" CssClass="button" OnClick="btnLOTList_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </div>
            </fieldset>
        </div>


        <%-- SHOW IMAGE FILE START --%>
        <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
        <ajax:ModalPopupExtender ID="mpeViewViewImgFileAttachment" runat="server" TargetControlID="btnShowImgFile"
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
        <%-- SHOW IMAGE FILE END --%>

        <%-- SHOW PDF FILE START--%>
        <asp:Button ID="btnShowPDFFile" runat="server" Style="display: none" />
        <ajax:ModalPopupExtender ID="mpeViewViewPDFFileAttachment" runat="server" TargetControlID="btnShowPDFFile"
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



        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>

        <script type="text/javascript">

            function ValidateRemarks() {
                var Remarks = document.getElementById('<%=txtRemarks.ClientID %>').value;
                if (Remarks == '') {
                    document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtRemarks.ClientID %>').style.borderColor = "";
                    return false;
                }
            }


            function ValidateRemarksForAmendment() {
                var check = true;


                var hdProductionManagerIDFlag = document.getElementById('<%=hdProductionManagerIDFlag.ClientID %>').value;

                if (parseInt(hdProductionManagerIDFlag) > 0) {
                    if (ValidateRemarks()) {
                        check = false;
                    }
                }

                if (check) {
                    if (confirm("Would you like to send to amendment?")) {
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



            function ValidateAll() {
                if (confirm("Would you like to approve?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
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

        <script type="text/Javascript">
            function preventInput(event) {
                if (event.which != 9) {
                    event.preventDefault();
                }
            }
        </script>

    </form>
</body>
</html>
