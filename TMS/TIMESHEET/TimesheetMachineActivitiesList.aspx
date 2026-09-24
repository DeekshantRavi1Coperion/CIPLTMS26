<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="TimesheetMachineActivitiesList.aspx.cs" Inherits="TIMESHEET_TimesheetMachineActivitiesList" %>

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
            var Activity = document.getElementById('<%=txtActivityToU.ClientID %>').value;
            if (Activity == '') {
                document.getElementById('<%=txtActivityToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtActivityToU.ClientID %>').style.borderColor = "";
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

            return check;
        }

        function ConfirmSaving() {

            if (confirm("Would you like to update activity?")) {
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
                <legend>Activity List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /><</legend>
                <div class="form-grid form-grid-3">
                    <label>Activity</label>
                    <asp:TextBox ID="txtActivity" runat="server" CssClass="form-control" />

                    &nbsp;
                 &nbsp;
                 <asp:Button ID="btnSearch"
                     OnClick="btnSearch_Click"
                     runat="server"
                     Text="Search"
                     CssClass="button" />

                    &nbsp;
                 &nbsp;
                 <asp:Button
                     ID="btnAddNew"
                     CssClass="button"
                     runat="server"
                     Text="Add New"
                     PostBackUrl="~/TIMESHEET/AddTimesheetMachineActivities.aspx" />

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
                ID="gvActivityList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvActivityList_RowCommand"
                OnRowDataBound="gvActivityList_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityID" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_PID") %>' />
                            <asp:Label ID="lblActivityName" runat="server" Visible="false" Text='<%# Eval("ACTIVITY_NAME") %>' />

                            <asp:ImageButton ID="imgProperties" ToolTip="Edit Activity Details...!!" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="SR_NO" HeaderText="SR_NO" />
                    <asp:BoundField DataField="ACTIVITY_NAME" HeaderText="ACTIVITY_NAME" />
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


        <div class="form-grid-container">
            <fieldset class="form-card">
                <legend>
                    <asp:Label ID="lblLegend" runat="server" Text="Activity Details" />
                </legend>

                <div class="form-grid form-grid-2">
                    <label>Activity</label>
                    <asp:TextBox ID="txtActivityToU" runat="server" CssClass="form-control" />
                </div>
            </fieldset>

            <div class="full-width button-group">
                <asp:Button
                    ID="btnUpdate"
                    CssClass="button"
                    runat="server"
                    Text="Submit"
                    OnClientClick="return ValidateAll();"
                    Width="100%"
                    OnClick="btnUpdate_Click" />
            </div>
            <div class="full-width">
                <asp:Panel ID="pnlUpdateMsg" Visible="false" runat="server">
                    <asp:Label ID="lblUpdateMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

        </div>

    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
