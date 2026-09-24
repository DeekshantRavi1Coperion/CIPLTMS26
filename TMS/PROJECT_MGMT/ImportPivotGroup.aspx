<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportPivotGroup.aspx.cs"
    Inherits="PROJECT_MGMT_ImportPivotGroup" Title="Untitled Page" %>

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
    <script type="text/javascript" language="javascript">

        function ValidatefileUploadProjectPivotGroup() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadProjectPivotGroup = document.getElementById('<%=fileUploadProjectPivotGroup.ClientID %>').value;
            var divfileUploadProjectPivotGroup = document.getElementById("divfileUploadProjectPivotGroup");
            var lblfileUploadProjectPivotGroup = document.getElementById('<%=lblfileUploadProjectPivotGroup.ClientID %>');

            if (fileUploadProjectPivotGroup == '') {
                document.getElementById('<%=fileUploadProjectPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadProjectPivotGroup.style.display = "block";
                lblfileUploadProjectPivotGroup.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadProjectPivotGroup.toLowerCase())) {
                    document.getElementById('<%=fileUploadProjectPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadProjectPivotGroup.style.display = "block";
                    lblfileUploadProjectPivotGroup.innerHTML = "Please enter only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadProjectPivotGroup.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadProjectPivotGroup.style.display = "none";
                    lblfileUploadProjectPivotGroup.innerHTML = "";
                    return false;
                }
            }
        }


        function ValidateAll() {

            var check = true;
            if (ValidatefileUploadProjectPivotGroup()) { return false; }
            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Import Project Pivot Group:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <div class="full-width">

                        <label>Browse Project Pivot Group</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadProjectPivotGroup" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadProjectPivotGroup" style="display: none;">
                                        <asp:Label ID="lblfileUploadProjectPivotGroup" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div class="full-width button-group">
                        <asp:Button ID="btnGetProjectPivotGroup" CssClass="button" Width="100%" runat="server"
                            Text="Get Project Pivot Group" OnClientClick="return ValidateAll();" OnClick="btnGetProjectPivotGroup_Click" />
                        <asp:Button ID="btnImportProjectPivotGroup" CssClass="button" Width="100%" runat="server"
                            Text="Import Pivot Group" OnClick="btnImportProjectPivotGroup_Click" />
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
                ID="gvProjectPivotGroup" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                HorizontalAlign="Center" OnRowDataBound="gvProjectPivotGroup_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="JOB_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblJobNo" runat="server" Visible="true" Text='<%# Eval("JOB_NO" ) %>' />
                            <asp:Label ID="lblSerialNo" runat="server" Visible="false" Text='<%# Eval("SERIAL_NO" ) %>' />
                            <asp:Label ID="lblPivotGroup" runat="server" Visible="false" Text='<%# Eval("PIVOT_GROUP" ) %>' />
                            <asp:Label ID="lbl0" runat="server" Visible="false" Text='<%# Eval("0" ) %>' />
                            <asp:Label ID="lblC" runat="server" Visible="false" Text='<%# Eval("C" ) %>' />
                            <asp:Label ID="lblE" runat="server" Visible="false" Text='<%# Eval("E" ) %>' />
                            <asp:Label ID="lblF" runat="server" Visible="false" Text='<%# Eval("F" ) %>' />
                            <asp:Label ID="lblI" runat="server" Visible="false" Text='<%# Eval("I" ) %>' />
                            <asp:Label ID="lblN" runat="server" Visible="false" Text='<%# Eval("N" ) %>' />
                            <asp:Label ID="lblOI" runat="server" Visible="false" Text='<%# Eval("OI" ) %>' />
                            <asp:Label ID="lblP" runat="server" Visible="false" Text='<%# Eval("P" ) %>' />
                            <asp:Label ID="lblTotal" runat="server" Visible="false" Text='<%# Eval("TOTAL" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PIVOT_GROUP" HeaderText="PIVOT_GROUP" />
                    <asp:BoundField DataField="0" HeaderText="0" />
                    <asp:BoundField DataField="C" HeaderText="C" />
                    <asp:BoundField DataField="E" HeaderText="E" />
                    <asp:BoundField DataField="F" HeaderText="F" />
                    <asp:BoundField DataField="I" HeaderText="I" />
                    <asp:BoundField DataField="N" HeaderText="N" />
                    <asp:BoundField DataField="OI" HeaderText="OI" />
                    <asp:BoundField DataField="P" HeaderText="P" />
                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
                    <asp:TemplateField HeaderText="GROSS_MARGIN_LINE">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlGrossMarginLine" runat="server" Width="100%" Enabled="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF1">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF3" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF4" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UDF5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUDF5" runat="server" />
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

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
