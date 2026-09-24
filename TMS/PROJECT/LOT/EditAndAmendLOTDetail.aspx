<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditAndAmendLOTDetail.aspx.cs" Inherits="PROJECT_LOT_EditAndAmendLOTDetail" %>

<!DOCTYPE html>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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


</head>
<body>
    <form id="form1" runat="server">
        <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
        </ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hdConfirmValue" runat="server" />
        <asp:HiddenField ID="hdUpdationFlag" runat="server" />

        <div align="center" style="margin-top: 20px;">
            <fieldset style="width: 95%;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblLOTDetailsLegendTxt" runat="server"></asp:Label>LOT Transmittal Detail
                </legend>
                <br />
                <table width="100%">
                    <tr>
                        <td>&nbsp;</td>
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
                            <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>LOT For:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLOTFor" runat="server" Width="100%" Height="26px"
                                onblur="return ValidateLOTFor();" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>JOB Number:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 90%">
                                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" Enabled="false" onblur="return ValidateJOBNo();" />
                                    </td>

                                    <td style="width: 10%">
                                        <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                            OnClick="btnGetJOBNo_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Production Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductionNumber" runat="server" Width="100%" onblur="return ValidateProductionNumber();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>Customer Name:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustName();" />
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustCode();" />
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
                        <td>PO Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPONo" runat="server" Width="100%" onblur="return ValidatePONo();" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Date:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" onkeyDown="javascript:preventInput(event);" Width="100%"></asp:TextBox>
                                        <asp:HiddenField ID="hdDate" runat="server" />
                                        <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate" runat="server"
                                            TargetControlID="txtDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedDate">
                                        </ajax:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                            ToolTip="Calendar"  Visible="false"/>
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
                        <td>TF Number:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="txtTFNo" runat="server" Width="100%" Enabled="false" onblur="return ValidateTFNo();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Button ID="btnGetTFno" runat="server" Width="100%" Text="Get" CssClass="button"
                                            OnClientClick="return ValidateJobNoForFT();" OnClick="btnGetTFno_Click" />
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
                        <td>Item:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtItemName" runat="server" Width="100%" TextMode="MultiLine" Rows="3" onblur="return ValidateItemName();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>


                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <fieldset>
                                        <legend style="text-align: center;">Add Subitems</legend>
                                    </fieldset>
                                </td>
                            </tr>

                            <tr>
                                <td>Description:
                                </td>

                                <td colspan="4">
                                    <asp:TextBox ID="txtDescription" runat="server" Width="100%"
                                        TextMode="MultiLine" Rows="3" onblur="return ValidateDescription();" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Drg./Doc No.:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDrgNo" runat="server" Width="100%" onblur="return ValidateDrgNo();" />
                                </td>
                                <td>&nbsp;</td>
                                <td>Rev. No.
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRevNo" runat="server" Width="100%" onblur="return ValidateRevNo();"
                                        onkeypress="return inNumberKey(this, event);" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>Category (For Factory):
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chkLstCategory" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                                        onblur="return ValidateCategoryToEdit();" />
                                </td>
                                <td>&nbsp;</td>
                                <td>No. Of Copies.
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 90%">
                                                <asp:TextBox ID="txtNoOfCopies" runat="server" Width="100%" onblur="return ValidateNoOfCopies();"
                                                    onkeypress="return inNumberKey(this, event);" />
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Button ID="btnAdd" runat="server" Width="100%" Text="Add" CssClass="button"
                                                    OnClick="btnAdd_Click" OnClientClick="return ValidateAddSubitem();" />
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
                                <td colspan="5">
                                    <div align="center">
                                        <fieldset style="width: 100%;">
                                            <legend style="text-align: center;">
                                                <asp:Label ID="lblSubitemsRecords" runat="server" Text="Subitems Records[0]" /></legend>
                                            <div style='overflow: auto; width: 100%; height: 200px; border: 1px solid lightgray;'>
                                                <asp:GridView ID="gvSubItem" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                                                    OnRowCommand="gvSubItem_RowCommand" OnRowDataBound="gvSubItem_RowDataBound">
                                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ToolTip="Edit" ImageUrl="~/Images/NEWICONS/Amendment01.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgRemove" CommandArgument="REMOVE" runat="server" ToolTip="Remove" ImageUrl="~/Images/Icons/REMOVE03.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLOTTFSubitemID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>'></asp:Label>
                                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="35%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Drg./Doc.No." HeaderStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrgOrDOCNo" runat="server" Text='<%# Eval("DRG_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rev.No." HeaderStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRevNo" runat="server" Text='<%# Eval("REV_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryID" runat="server" Visible="false" Text='<%# Eval("CATEGORY_ID") %>'></asp:Label>
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Copies" HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoOfCopies" runat="server" Text='<%# Eval("NO_OF_COPIES") %>'></asp:Label>
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

                        <%--EDIT SUBITEM START--%>
                        <asp:Button ID="btnShowPopupSubItemDetail" runat="server" Style="display: none" />
                        <ajax:ModalPopupExtender ID="modalPopupExtenderSubItemDetail" runat="server" TargetControlID="btnShowPopupSubItemDetail"
                            PopupControlID="pnlPopupSubItemDetail" CancelControlID="imgBtnCancelSubItemDetail" BackgroundCssClass="modalBackground">
                        </ajax:ModalPopupExtender>
                        <asp:Panel ID="pnlPopupSubItemDetail" runat="server" BackColor="White" Height="400px" Width="900px"
                            Style="display: block">
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:ImageButton ID="imgBtnCancelSubItemDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                                <legend style="text-align: center;">Edit Subitem Detail</legend>
                                <asp:HiddenField ID="hdSRNo" runat="server" />
                                <table style="width: 95%; margin-left: 20px;">
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Description:</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtDescriptionToEdit" TextMode="MultiLine" Rows="3" runat="server" Width="100%"
                                                onblur="return ValidateDescriptionToEdit();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Drg./Doc_No.:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDrgOrDOCNoToEdit" runat="server" Width="100%" onblur="return ValidateDrgNoToEdit();" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>Rev. No.:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRevNoToEdit" runat="server" Width="100%" onblur="return ValidateRevNoToEdit();" />
                                        </td>
                                    </tr>
                                    <td>&nbsp;
                                    </td>
                                    <tr>
                                        <td>Cat.:
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="chkLstCategoryTEdit" runat="server" RepeatDirection="Horizontal" TextAlign="Right" Width="100%"
                                                onblur="return ValidateCategoryToEdit();" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>No. Of Copies:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoOfCopiesToEdit" runat="server" Width="100%"
                                                onblur="return ValidateNoOfCopiesToEdit();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                                                Width="100%" OnClick="btnUpdate_Click" OnClientClick="return ValidateAddSubitemToEdit();" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <%--EDIT SUBITEM END--%>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <table width="100%">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trPERemarks" runat="server">
                        <td>Project Engineer Remarks:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtPERemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="3" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trPMRemarks" runat="server">
                        <td>Project Manager Remarks:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtPMRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="3" Enabled="false" />
                        </td>
                    </tr>                    
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trAmendmentRemarks" runat="server">
                        <td>Amendment Remarks:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtAmendmentRemarks" runat="server" Width="100%" TextMode="MultiLine" Rows="3" Enabled="false" />
                        </td>
                    </tr>
                   
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Important Notes:</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtNotes" runat="server" Width="100%" TextMode="MultiLine" Rows="4" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Drawing 1:</td>
                        <td>
                            <asp:FileUpload ID="uploadFileDrawing1" runat="server" Width="100%"
                                Height="29px" BorderStyle="Groove" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Drawing 2:</td>
                        <td>
                            <asp:FileUpload ID="uploadFileDrawing2" runat="server" Width="100%"
                                Height="29px" BorderStyle="Groove" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <div id="divfileDrawing1" style="display: none;">
                                <asp:Label ID="lblfileDrawing1" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                        <td>
                            <div id="divfileDrawing2" style="display: none;">
                                <asp:Label ID="lblfileDrawing2" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Drawing 3:</td>
                        <td>
                            <asp:FileUpload ID="uploadFileDrawing3" runat="server" Width="100%"
                                Height="29px" BorderStyle="Groove" />
                        </td>
                        <td>&nbsp;</td>
                        <td>Drawing 4:</td>
                        <td>
                            <asp:FileUpload ID="uploadFileDrawing4" runat="server" Width="100%"
                                Height="29px" BorderStyle="Groove" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                            <div id="divfileDrawing3" style="display: none;">
                                <asp:Label ID="lblfileDrawing3" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                        <td>
                            <div id="divfileDrawing4" style="display: none;">
                                <asp:Label ID="lblfileDrawing4" runat="server" ForeColor="Red" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </div>

        <%--JOB DETAIL START--%>
        <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
        <ajax:ModalPopupExtender ID="modalPopupExtenderJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
            PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
        </ajax:ModalPopupExtender>
        <asp:Panel ID="pnlPopupJOBDetail" runat="server" BackColor="White" Height="500px" Width="900px"
            Style="display: block">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                    </td>
                </tr>
            </table>
            <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
                <legend style="text-align: center;">
                    <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" /></legend>
                <asp:Label ID="lblJOBMsg" runat="server" />
                <table style="width: 95%; margin-left: 20px;">
                    <tr>
                        <td>JOB No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJOBNoSearch" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>PO No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPONoSearch" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>Customer Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomerCodeSearch" runat="server" Width="100%" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchJOBNo_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <div style='overflow: auto; width: 99%; height: 300px; border: 1px solid lightgray; margin-left: 5px;'>
                    <div align="center">
                        <asp:GridView ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Get JOB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                        <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                        <asp:Label ID="lblCustCode" runat="server" Visible="false" Text='<%# Eval("CUST_CODE") %>' />
                                        <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />

                                        <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No."
                                            runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CUST_CODE" HeaderText="CUST_CODE" />
                                <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                                <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
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
            </fieldset>
        </asp:Panel>
        <%--JOB DETAIL END--%>





        <script type="text/Javascript">
            function preventInput(event) {
                if (event.which != 9) {
                    event.preventDefault();
                }
            }
        </script>

        <script type="text/javascript">

            function pageLoad() {
                document.getElementById('<%=txtDate.ClientID %>').value = document.getElementById('<%=hdDate.ClientID %>').value;
            }

            function clientChangedDate(sender, args) {
                document.getElementById('<%=hdDate.ClientID %>').value = document.getElementById('<%=txtDate.ClientID %>').value;
            }

        </script>

        <script type="text/javascript">

            function ValidateLOTFor() {
                var LOTFor = document.getElementById('<%=ddlLOTFor.ClientID %>').selectedIndex;
                if (LOTFor == '' || LOTFor == 0) {
                    document.getElementById('<%=ddlLOTFor.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=ddlLOTFor.ClientID %>').style.borderColor = "";
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

            function ValidateProductionNumber() {
                var ProductionNumber = document.getElementById('<%=txtProductionNumber.ClientID %>').value;
                if (ProductionNumber == '') {
                    document.getElementById('<%=txtProductionNumber.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtProductionNumber.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateTFNo() {
                var TFNo = document.getElementById('<%=txtTFNo.ClientID %>').value;
                if (TFNo == '') {
                    document.getElementById('<%=txtTFNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtTFNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidatePONo() {
                var PONo = document.getElementById('<%=txtPONo.ClientID %>').value;
                if (PONo == '') {
                    document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateItemName() {
                var ItemName = document.getElementById('<%=txtItemName.ClientID %>').value;
                if (ItemName == '') {
                    document.getElementById('<%=txtItemName.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtItemName.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateDescription() {
                var Description = document.getElementById('<%=txtDescription.ClientID %>').value;
                if (Description == '') {
                    document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateDrgNo() {
                var DrgNo = document.getElementById('<%=txtDrgNo.ClientID %>').value;
                if (DrgNo == '') {
                    document.getElementById('<%=txtDrgNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDrgNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateRevNo() {
                var RevNo = document.getElementById('<%=txtRevNo.ClientID %>').value;
                if (RevNo == '') {
                    document.getElementById('<%=txtRevNo.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtRevNo.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateCategory() {
                var Category = document.getElementById('<%=chkLstCategory.ClientID %>');
                var chkBox = Category.getElementsByTagName("input");
                var counter = 0;
                for (var i = 0; i < chkBox.length; i++) {
                    if (chkBox[i].checked) {
                        counter++;
                    }
                }

                if (counter == 0) {
                    alert("Please select atleast one category...!!");
                    return true;
                }
                else {
                    return false;
                }
            }

            function ValidateNoOfCopies() {
                var NoOfCopies = document.getElementById('<%=txtNoOfCopies.ClientID %>').value;
                if (NoOfCopies == '') {
                    document.getElementById('<%=txtNoOfCopies.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtNoOfCopies.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

        </script>

        <script type="text/javascript">

            function ValidatefileDrawing1() {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                var fileDrawing1 = document.getElementById('<%=uploadFileDrawing1.ClientID %>').value;
                var divfileDrawing1 = document.getElementById("divfileDrawing1");
                var lblfileDrawing1 = document.getElementById('<%=lblfileDrawing1.ClientID %>');


                if (fileDrawing1 == '') {
                    document.getElementById('<%=uploadFileDrawing1.ClientID %>').style.borderColor = "";
                    divfileDrawing1.style.display = "none";
                    lblfileDrawing1.innerHTML = "";
                    return false;
                }
                else {
                    if (!regex.test(fileDrawing1.toLowerCase())) {
                        document.getElementById('<%=uploadFileDrawing1.ClientID %>').style.borderColor = "#F7627F";
                        divfileDrawing1.style.display = "block";
                        lblfileDrawing1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileDrawing1.ClientID %>').style.borderColor = "";
                        divfileDrawing1.style.display = "none";
                        lblfileDrawing1.innerHTML = "";
                        return false;
                    }
                }
            }

            function ValidatefileDrawing2() {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                var fileDrawing2 = document.getElementById('<%=uploadFileDrawing2.ClientID %>').value;
                var divfileDrawing2 = document.getElementById("divfileDrawing2");
                var lblfileDrawing2 = document.getElementById('<%=lblfileDrawing2.ClientID %>');


                if (fileDrawing2 == '') {
                    document.getElementById('<%=uploadFileDrawing2.ClientID %>').style.borderColor = "";
                    divfileDrawing2.style.display = "none";
                    lblfileDrawing2.innerHTML = "";
                    return false;
                }
                else {
                    if (!regex.test(fileDrawing2.toLowerCase())) {
                        document.getElementById('<%=uploadFileDrawing2.ClientID %>').style.borderColor = "#F7627F";
                        divfileDrawing2.style.display = "block";
                        lblfileDrawing2.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileDrawing2.ClientID %>').style.borderColor = "";
                        divfileDrawing2.style.display = "none";
                        lblfileDrawing2.innerHTML = "";
                        return false;
                    }
                }
            }

            function ValidatefileDrawing3() {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                var fileDrawing3 = document.getElementById('<%=uploadFileDrawing3.ClientID %>').value;
                var divfileDrawing3 = document.getElementById("divfileDrawing3");
                var lblfileDrawing3 = document.getElementById('<%=lblfileDrawing3.ClientID %>');

                if (fileDrawing3 == '') {
                    document.getElementById('<%=uploadFileDrawing3.ClientID %>').style.borderColor = "";
                    divfileDrawing3.style.display = "none";
                    lblfileDrawing3.innerHTML = "";
                    return false;
                }
                else {
                    if (!regex.test(fileDrawing3.toLowerCase())) {
                        document.getElementById('<%=uploadFileDrawing3.ClientID %>').style.borderColor = "#F7627F";
                        divfileDrawing3.style.display = "block";
                        lblfileDrawing3.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileDrawing3.ClientID %>').style.borderColor = "";
                        divfileDrawing3.style.display = "none";
                        lblfileDrawing3.innerHTML = "";
                        return false;
                    }
                }
            }

            function ValidatefileDrawing4() {
                var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF"];
                var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
                var fileDrawing4 = document.getElementById('<%=uploadFileDrawing4.ClientID %>').value;
                var divfileDrawing4 = document.getElementById("divfileDrawing4");
                var lblfileDrawing4 = document.getElementById('<%=lblfileDrawing4.ClientID %>');

                if (fileDrawing4 == '') {
                    document.getElementById('<%=uploadFileDrawing4.ClientID %>').style.borderColor = "";
                    divfileDrawing4.style.display = "none";
                    lblfileDrawing4.innerHTML = "";
                    return false;
                }
                else {
                    if (!regex.test(fileDrawing4.toLowerCase())) {
                        document.getElementById('<%=uploadFileDrawing4.ClientID %>').style.borderColor = "#F7627F";
                        divfileDrawing4.style.display = "block";
                        lblfileDrawing4.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif file!";
                        return true;
                    }
                    else {
                        document.getElementById('<%=uploadFileDrawing4.ClientID %>').style.borderColor = "";
                        divfileDrawing4.style.display = "none";
                        lblfileDrawing4.innerHTML = "";
                        return false;
                    }
                }
            }
        </script>


        <script type="text/javascript">

            function ValidateAll() {
                var check = true;

                if (ValidateLOTFor()) {
                    check = false;
                }

                if (ValidateCustName()) {
                    check = false;
                }

                if (ValidateCustCode()) {
                    check = false;
                }

                if (ValidateJobNo()) {
                    check = false;
                }

                if (ValidateProductionNumber()) {
                    check = false;
                }

                if (ValidateTFNo()) {
                    check = false;
                }

                if (ValidatePONo()) {
                    check = false;
                }

                if (ValidateItemName()) {
                    check = false;
                }

                if (ValidatefileDrawing1()) {
                    check = false;
                }

                if (ValidatefileDrawing2()) {
                    check = false;
                }

                if (ValidatefileDrawing3()) {
                    check = false;
                }

                if (ValidatefileDrawing4()) {
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

                return check;
            }

            function ValidateAddSubitem() {
                var check = true;

                if (ValidateDescription()) {
                    check = false;
                }

                if (ValidateDrgNo()) {
                    check = false;
                }

                if (ValidateRevNo()) {
                    check = false;
                }

                if (ValidateCategory()) {
                    check = false;
                }

                if (ValidateNoOfCopies()) {
                    check = false;
                }

                return check;
            }



            function ValidateJobNoForFT() {
                var check = true;

                if (ValidateJobNo()) {
                    check = false;
                }

                return check;
            }

        </script>


        <script type="text/javascript">

            function ValidateDescriptionToEdit() {
                var DescriptionToEdit = document.getElementById('<%=txtDescriptionToEdit.ClientID %>').value;
                if (DescriptionToEdit == '') {
                    document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDescriptionToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateDrgNoToEdit() {
                var DrgNoToEdit = document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').value;
                if (DrgNoToEdit == '') {
                    document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtDrgOrDOCNoToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateRevNoToEdit() {
                var RevNoToEdit = document.getElementById('<%=txtRevNoToEdit.ClientID %>').value;
                if (RevNoToEdit == '') {
                    document.getElementById('<%=txtRevNoToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtRevNoToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }

            function ValidateCategoryToEdit() {
                var CategoryToEdit = document.getElementById('<%=chkLstCategoryTEdit.ClientID %>');
                var chkBoxToEdit = CategoryToEdit.getElementsByTagName("input");
                var counter = 0;
                for (var i = 0; i < chkBoxToEdit.length; i++) {
                    if (chkBoxToEdit[i].checked) {
                        counter++;
                    }
                }

                if (counter == 0) {
                    alert("Please select atleast one category...!!");
                    return true;
                }
                else {
                    return false;
                }
            }

            function ValidateNoOfCopiesToEdit() {
                var NoOfCopiesToEdit = document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').value;
                if (NoOfCopiesToEdit == '') {
                    document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').style.borderColor = "#F7627F";
                    return true;
                }
                else {
                    document.getElementById('<%=txtNoOfCopiesToEdit.ClientID %>').style.borderColor = "";
                    return false;
                }
            }


            function ValidateAddSubitemToEdit() {
                var check = true;

                if (ValidateDescriptionToEdit()) {
                    check = false;
                }

                if (ValidateDrgNoToEdit()) {
                    check = false;
                }

                if (ValidateRevNoToEdit()) {
                    check = false;
                }

                if (ValidateCategoryToEdit()) {
                    check = false;
                }

                if (ValidateNoOfCopiesToEdit()) {
                    check = false;
                }

                return check;
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
    </form>
</body>
</html>
