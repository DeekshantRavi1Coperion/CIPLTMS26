<%@ Page Title="" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    CodeFile="ServiceHoursReport.aspx.cs" Inherits="TOUR_AND_TRAVELS_TRAVEL_ServiceHoursReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />

    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>



    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Service Working Report:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
                <div class="form-grid form-grid-3">

                    <label>Employee:</label>
                    <asp:DropDownList ID="ddlEmployee" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Year:</label>
                    <asp:DropDownList ID="ddlYear" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>

                    <label>Month:</label>
                    <asp:DropDownList ID="ddlMonth" runat="server" 
                        CssClass="form-control">
                    </asp:DropDownList>


                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CssClass="button"
                    Width="100%"/>

                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"
                    CssClass="button"
                    Width="100%"/>


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
                ID="gvReport" runat="server" AutoGenerateColumns="False"
                CellPadding="8" ForeColor="#333333" GridLines="Both" PageSize="15" Width="100%"
                HorizontalAlign="Center" OnRowDataBound="gvReport_RowDataBound" AllowPaging="True"
                ShowFooter="True">

                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" Height="35px" />

                <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                    <asp:BoundField DataField="Year" HeaderText="Year" />
                    <asp:BoundField DataField="Month" HeaderText="Month" />
                    <asp:BoundField DataField="WorkingHours" HeaderText="Working Hours" DataFormatString="{0:0.##}" />
                    <asp:BoundField DataField="TrainingHours" HeaderText="Training Hours" DataFormatString="{0:0.##}" />
                    <asp:BoundField DataField="TravellingHours" HeaderText="Travelling Hours" DataFormatString="{0:0.##}" />
                </Columns>

                <FooterStyle BackColor="#bfbfbf" ForeColor="Black" Font-Bold="True" HorizontalAlign="Center" Height="35px" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#bfbfbf" Font-Bold="True" ForeColor="Black" Height="40px" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" Height="35px" />
            </asp:GridView>

        </div>

    </div>


</asp:Content>
