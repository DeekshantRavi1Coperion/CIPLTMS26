<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="WorkerList.aspx.cs"
    Inherits="HR_WorkerList"
    Title="CIPLTMS - Worker Convenience List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            display: inline-block;
            border-radius: 4px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">
                <legend>Filters</legend>

                <div class="form-filter-grid">

                    <label>Worker Name:</label>
                    <asp:DropDownList ID="ddlWorker" runat="server"
                        CssClass="form-control" />

                    <label>Worker Code:</label>
                    <asp:TextBox ID="txtWorkerCode" runat="server"
                        CssClass="form-control"></asp:TextBox>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                    OnClick="btnSearch_Click" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>Worker Convenience List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvWorkerList" runat="server" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvWorkerList_RowDataBound" OnRowCommand="gvWorkerList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                <asp:Label ID="lblEssWorkerRecordID" runat="server" Visible="false" Text='<%# Eval("ESS_WORKER_RECORD_ID") %>' />
                                <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID") %>' />
                                <asp:Label ID="lblWorkerName" runat="server" Visible="false" Text='<%# Eval("WORKER_NAME") %>' />
                                <asp:Label ID="lblWorkerCode" runat="server" Visible="false" Text='<%# Eval("EMPLOYEE_ID") %>' />
                                <asp:Label ID="lblUnit" runat="server" Visible="false" Text='<%# Eval("UNIT") %>' />
                                <asp:Label ID="lblConvenienceAmt" runat="server" Visible="false" Text='<%# Eval("CONVENIENCE_AMT") %>' />

                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                    ToolTip="Edit Worker Convenience" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="WORKER_NAME" HeaderText="WORKER_NAME" />
                        <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="WORKER_CODE" />
                        <asp:BoundField DataField="UNIT" HeaderText="UNIT" />
                        <asp:BoundField DataField="CONVENIENCE_AMT" HeaderText="CONVENIENCE_AMT" />
                    </Columns>
                </asp:GridView>


            </div>

        </fieldset>

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
                    <asp:Label ID="lblLegend" runat="server" /></legend>

                <div class="form-grid form-grid-2">

                    <label>Worker Name:</label>
                    <asp:TextBox ID="txtWorkerName" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                    <label>Worker Code:</label>
                    <asp:TextBox ID="txtWorkerCodeNew" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                    <label>Unit:</label>
                    <asp:TextBox ID="txtUnit" runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                    <label>Convenience Amount:</label>
                    <asp:TextBox ID="txtConvenienceAmt" runat="server"
                        onkeypress="return inNumberKeyWithDecimal(this, event);"
                        CssClass="form-control" />

                </div>

            </fieldset>

            <div class="full-width button-group">
                <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update"
                    OnClick="btnUpdate_Click" Width="100%" />

            </div>

        </div>


    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
