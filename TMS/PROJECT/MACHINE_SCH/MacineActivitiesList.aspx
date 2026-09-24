<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="MacineActivitiesList.aspx.cs" Inherits="PROJECT_MACHINE_SCH_MacineActivitiesList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript">

        function ValidateActivity() {
            var Activity = document.getElementById('<%=ddlActivityToU.ClientID %>').selectedIndex;
            if (Activity == '' || Activity == 0) {
                document.getElementById('<%=ddlActivityToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlActivityToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }



        function ValidateMachine() {
            var Machine = document.getElementById('<%=ddlMacnineToU.ClientID %>').selectedIndex;
            if (Machine == '' || Machine == 0) {
                document.getElementById('<%=ddlMacnineToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMacnineToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function ValidateAll() {
            var check = true;

            if (ValidateActivity()) {
                check = false;
            }

            if (ValidateMachine()) {
                check = false;
            }

            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to save machine activity?")) {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                return true;
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Machine Activity List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Activity:</label>
                    <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                    <label>Machine:</label>
                    <asp:DropDownList ID="ddlMacnine" runat="server" Width="100%" Height="26px">
                    </asp:DropDownList>

                </div>
            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click" />

                <asp:Button ID="btnAddNew" CssClass="button" runat="server" Text="Add New" OnClick="btnAddNew_Click" />

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
                ID="gvMahineActivityList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvMahineActivityList_RowCommand"
                OnRowDataBound="gvMahineActivityList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("MACHINE_ACTIVITY_PID") %>' />
                            <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_FID") %>' />
                            <asp:Label ID="lblMachineID" runat="server" Visible="false" Text='<%# Eval("MACHINE_FID") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Employee Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="SR_NO" HeaderText="SR_NO" />
                    <asp:BoundField DataField="ACTIVITY_NAME" HeaderText="ACTIVITY_NAME" />
                    <asp:BoundField DataField="MACHINE_NAME" HeaderText="MACHINE_NAME" />
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


    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
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


        <div class="form-entry-container">
            <fieldset class="form-card">

                <legend>
                    <asp:Label ID="lblLegend" runat="server" Text="Machine Activity Details" />
                </legend>

                <div class="form-grid form-grid-2">

                    <label>Activity:</label>
                    <asp:DropDownList ID="ddlActivityToU" runat="server" CssClass="form-control">
                    </asp:DropDownList>


                    <label>Machine:</label>
                    <asp:DropDownList ID="ddlMacnineToU" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>

            </fieldset>

            <div class="full-width button-group">

                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Submit" 
                    OnClientClick="return ValidateAll();"
                    Width="100%" OnClick="btnUpdate_Click" />

            </div>

            <div class="full-width">
                <div align="center">
                    <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                        <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
            </div>
        </div>

    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
