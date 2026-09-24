<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="PivotGroups.aspx.cs" Inherits="MR_WORKFLOW_PivotGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icon04.png" />
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

        function ValidateName() {
            var Name = document.getElementById('<%=txtNameToU.ClientID %>').value;
            if (Name == '') {
                document.getElementById('<%=txtNameToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtNameToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescriptionToU.ClientID %>').value;
            if (Description == '') {
                document.getElementById('<%=txtDescriptionToU.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescriptionToU.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateAll() {
            if (ValidatefileUploadAttachment1()) { return false; }

            if (ValidateName()) { return false; }
            if (ValidateDescription()) { return false; }
        }

    </script>




    <script type="text/javascript">
        var GridId = "<%=gvPivotGroup.ClientID %>";
        var ScrollHeight = 475;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

    </script>

    <style type="text/css">
        .myGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .myGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .myGrid th {
                padding: 4px 2px;
                color: #fff;
                background-color: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .myGrid .alt {
                background-color: #EFEFEF;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Material Requisition List:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Code</label>
                    <asp:TextBox
                        ID="txtCode"
                        runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Name</label>
                    <asp:TextBox
                        ID="txtName"
                        runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <label>Description</label>
                    <asp:TextBox
                        ID="txtDescription"
                        runat="server"
                        CssClass="form-control"></asp:TextBox>

                    <div class="full-width button-group">

                        <asp:Button ID="btnSearch"
                            CssClass="button"
                            runat="server"
                            Width="100%"
                            Text="Search"
                            OnClick="btnSearch_Click" />

                        <asp:Button ID="btnCreateNew"
                            CssClass="button"
                            runat="server"
                            Width="100%"
                            Text="Add New Pivot Group"
                            OnClick="btnCreateNew_Click" />

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
                ID="gvPivotGroup" runat="server"
                AutoGenerateColumns="false" CellPadding="4"
                ForeColor="#333333" GridLines="Both" Width="100%"
                HorizontalAlign="Center"
                OnRowCommand="gvPivotGroup_RowCommand"
                OnRowDataBound="gvPivotGroup_RowDataBound">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:TemplateField HeaderText="Edit"
                        HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:Label ID="lblPid" runat="server" Visible="false" Text='<%# Eval("PID") %>'></asp:Label>
                            <asp:Label ID="lblCode" runat="server" Visible="false" Text='<%# Eval("CODE") %>'></asp:Label>
                            <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# Eval("NAME") %>'></asp:Label>
                            <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                            <asp:Label ID="lblPivotGroup" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP") %>'></asp:Label>

                            <asp:ImageButton ID="imgProperties" ToolTip="Update ticket" CommandArgument="PROPERTIES"
                                runat="server" ImageUrl="~/Images/royal_search.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="CODE" HeaderText="Code"></asp:BoundField>
                    <asp:BoundField DataField="NAME" HeaderText="Name"></asp:BoundField>
                    <asp:BoundField DataField="DESCRIPTION" HeaderText="Description"></asp:BoundField>
                    <asp:BoundField DataField="PIVOT_GROUP" HeaderText="Pivot Group"></asp:BoundField>
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
    <asp:ModalPopupExtender ID="MpeInsertUpdatePivotGroup" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
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
                <legend>Pivot Group</legend>

                <div class="form-grid form-grid-2">
                    
                    <label>Code</label>
                    <asp:TextBox ID="txtCodeToU"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Name:</label>
                    <asp:TextBox ID="txtNameToU"
                        runat="server"
                        CssClass="form-control" />

                    <div class="full-width">
                        <label>Description:</label>
                        <asp:TextBox ID="txtDescriptionToU"
                            runat="server"
                            CssClass="form-control" />
                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSubmit"
                    runat="server"
                    CssClass="button"
                    Width="100%"
                    OnClientClick="return ValidateAll();"
                    Text="Submit"
                    OnClick="btnSubmit_Click" />
            </div>
            <div class="full-width">
                <asp:Panel ID="Panel1" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" />
                </asp:Panel>
            </div>
        </div>
        
    </asp:Panel>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
