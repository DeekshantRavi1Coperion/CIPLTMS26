<%@ Page Title="CIPLTMS-Add PO Pivot Group Log" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="AddPOPivotGroupLog.aspx.cs"
    Inherits="REPORTS_PURCHASE_ORDER_PIVOT_GROUP_AddPOPivotGroupLog" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icons/Icon04.png" />

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script src="../../../Scripts/NumericValidation.js" type="text/javascript"></script>


    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textbox {
            width: 120px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
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
            width: 120px;
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


    <script type="text/javascript">



        function ValidatePONo() {
            var pono = document.getElementById('<%=txtPONo.ClientID %>').value;
            if (pono == '') {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJOBNo() {
            var jobno = document.getElementById('<%=txtJOBNo.ClientID %>').value;
            if (jobno == '') {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOldPivotGroup() {
            var oldPivotGroup = document.getElementById('<%=txtOldPivotGroup.ClientID %>').value;
            if (oldPivotGroup == '') {
                document.getElementById('<%=txtOldPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOldPivotGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateOldPivotGroupDesc() {
            var oldPivotGroup = document.getElementById('<%=txtOldPivotGroupDesc.ClientID %>').value;
            if (oldPivotGroup == '') {
                document.getElementById('<%=txtOldPivotGroupDesc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOldPivotGroupDesc.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateNewPivotGroup() {
            var newPivotGroup = document.getElementById('<%=txtNewPivotGroup.ClientID %>').value;
            if (newPivotGroup == '') {
                document.getElementById('<%=txtNewPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNewPivotGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateNewPivotGroupDesc() {
            var newPivotGroup = document.getElementById('<%=txtNewPivotGroupDesc.ClientID %>').value;
            if (newPivotGroup == '') {
                document.getElementById('<%=txtNewPivotGroupDesc.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNewPivotGroupDesc.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateAll() {
            var check = true;

            if (ValidateJOBNo()) {
                check = false;
            }

            if (ValidatePONo()) {
                check = false;
            }

            if (ValidateNewPivotGroup()) {
                check = false;
            }

            if (ValidateNewPivotGroupDesc()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to save pivot group?")) {
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


        function ValidateJOBNoAll() {
            var check = true;

            if (ValidateJOBNo()) {
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdSubitemConfirmValue" runat="server" />

    <div class="form-entry-container">
        <fieldset class="form-card">

            <legend>Add PO Pivot Group</legend>

            <div class="form-grid form-grid-2">


                <label>Unit:</label>
                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" />

                <label>JOB No.:</label>
                <asp:TextBox ID="txtJOBNo" runat="server" CssClass="form-control" />

                <div class="full-width">

                    <label>PO No:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtPONo" runat="server" Enabled="false"
                                    CssClass="form-control" />
                            </td>

                            <td style="width: 20%">
                                <asp:Button ID="btnGetPONo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetPONo_Click" OnClientClick="return ValidateJOBNoAll();" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="full-width">
                    <label>Old Pivot Group:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtOldPivotGroupDesc" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtOldPivotGroup" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="full-width">
                    <label>New Pivot Group:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 60%">
                                <asp:TextBox ID="txtNewPivotGroupDesc" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox ID="txtNewPivotGroup" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>
                            <td style="width: 20%">
                                <asp:Button ID="btnGetNewPivotGroup" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetNewPivotGroup_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

        </fieldset>

        <div class="full-width button-group">

            <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" CssClass="button"
                OnClick="btnSave_Click" OnClientClick="return ValidateAll();" />

            <asp:Button ID="btnPOPivotGroupLogReport" runat="server" Width="100%" Text="View List" CssClass="button"
                OnClick="btnPOPivotGroupLogReport_Click" Visible="false" />

        </div>

        <div class="full-width">

            <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" />
            </asp:Panel>

        </div>
    </div>





    <%--PO DETAIL START--%>
    <asp:Button ID="btnShowPopupPODetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePODetail" runat="server" TargetControlID="btnShowPopupPODetail"
        PopupControlID="pnlPopupPODetail" CancelControlID="imgBtnCancelPODetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupPODetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPODetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblPORecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <div class="full-width button-group">

                            <label>JOB No.:</label>
                            <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                            <label>PO No.:</label>
                            <asp:TextBox ID="txtPONoSearch" runat="server" CssClass="form-control" />

                            <asp:Button ID="btnSearchPONo" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchPONo_Click" />

                        </div>
                    </div>
                </fieldset>

            </div>

            <div class="employee-grid-container">
                <asp:Label ID="lblPOMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPODetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvPODetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get PO Detail">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />
                                <asp:Label ID="lblOldPivotGroup" runat="server" Visible="false" Text='<%# Eval("OLD_PIVOT_GROUP") %>' />
                                <asp:Label ID="lblOldPivotGroupDesc" runat="server" Visible="false" Text='<%# Eval("OLD_PIVOT_GROUP_DESC") %>' />
                                <asp:Button ID="btnGetPODetail" CommandArgument="GET" ToolTip="Get PO Detail" Width="100%"
                                    runat="server" Text="Get PO Detail" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="Job No." />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO No." />
                        <asp:BoundField DataField="OLD_PIVOT_GROUP" HeaderText="Old Pivot Group" />
                        <asp:BoundField DataField="OLD_PIVOT_GROUP_DESC" HeaderText="Old Pivot Group Desc" />
                        <asp:BoundField DataField="UNIT" HeaderText="Unit" />
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
    <%--PO DETAIL END--%>


    <%--PIVOT GROUP DETAIL START--%>
    <asp:Button ID="btnShowPopupPONewPGDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpePONewPGDetail" runat="server" TargetControlID="btnShowPopupPONewPGDetail"
        PopupControlID="pnlPopupPONewPGDetail" CancelControlID="imgBtnCancelPONewPGDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupPONewPGDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelPONewPGDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblPONewPGRecords" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <div class="full-width button-group">

                            <label>Pivot Group:</label>
                            <asp:TextBox ID="txtPivotGroupSearch" runat="server" CssClass="form-control" />

                            <label>Pivot Group Desc:</label>
                            <asp:TextBox ID="txtPivotGroupDescSearch" runat="server" CssClass="form-control" />

                            <asp:Button ID="btnSearchPONoNewPG" CssClass="button" runat="server" Text="Search"
                                Width="100%" OnClick="btnSearchPONoNewPG_Click" />

                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="employee-grid-container">
                <asp:Label ID="lblPONewPGMsg" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvPONewPGDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvPONewPGDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get Pivot Group">
                            <ItemTemplate>
                                <asp:Label ID="lblNewPivotGroup" runat="server" Visible="false" Text='<%# Eval("NEW_PIVOT_GROUP") %>' />
                                <asp:Label ID="lblNewPivotGroupDesc" runat="server" Visible="false" Text='<%# Eval("NEW_PIVOT_GROUP_DESC") %>' />
                                <asp:Label ID="lblLikelyDeliveryDate" runat="server" Visible="false" Text='<%# Eval("LIKELY_DELIVERY_DATE") %>' />
                                <asp:Label ID="lblPlanDateOfProcurement" runat="server" Visible="false" Text='<%# Eval("PLAN_DATE_OF_PROCUREMENT") %>' />
                                <asp:Button ID="btnGetPivotGroupetail" CommandArgument="GET" ToolTip="Get New Pviot Group" Width="100%"
                                    runat="server" Text="Get New Pivot Group" CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NEW_PIVOT_GROUP" HeaderText="New Pivot Group" />
                        <asp:BoundField DataField="NEW_PIVOT_GROUP_DESC" HeaderText="New Pivot Group Desc" />
                        <asp:BoundField DataField="SALE_ESTIMATE_BUDGET" HeaderText="Sale Estimate Budget" />
                        <%--<asp:TemplateField HeaderText="Sale Estimate Budget">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSaleEstimateBudgetInList" Width="100%" runat="server" onKeyUp="GetSaving(this)" Text='<%# Eval("SALE_ESTIMATE_BUDGET") %>'
                                        CssClass="textbox" onkeypress="return inNumberKeyWithDecimal(this, event);"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:BoundField DataField="LIKELY_DELIVERY_DATE" HeaderText="Likely Delivery Date" />
                        <asp:BoundField DataField="PLAN_DATE_OF_PROCUREMENT" HeaderText="Plan Date Of Procurement" />
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
    <%--PIVOT GROUP DETAIL END--%>
</asp:Content>
