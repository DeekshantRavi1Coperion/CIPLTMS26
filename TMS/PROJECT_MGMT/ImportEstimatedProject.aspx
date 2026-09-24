<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportEstimatedProject.aspx.cs"
    Inherits="PROJECT_MGMT_ImportEstimatedProject" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icons/Icon04.png" />
<link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    
    function ValidateProjectNo() {
            var ProjectNo = document.getElementById('<%=txtProjectNo.ClientID %>').value;
            if (ProjectNo == '') {
                document.getElementById('<%=txtProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProjectNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
    function ValidatefileUploadEstimatedProject() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadEstimatedProject = document.getElementById('<%=fileUploadEstimatedProject.ClientID %>').value;
            var divfileUploadEstimatedProject = document.getElementById("divfileUploadEstimatedProject");
            var lblfileUploadEstimatedProject = document.getElementById('<%=lblfileUploadEstimatedProject.ClientID %>');

            if (fileUploadEstimatedProject == '') {
                document.getElementById('<%=fileUploadEstimatedProject.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadEstimatedProject.style.display = "block";
                lblfileUploadEstimatedProject.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadEstimatedProject.toLowerCase())) {
                    document.getElementById('<%=fileUploadEstimatedProject.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadEstimatedProject.style.display = "block";
                    lblfileUploadEstimatedProject.innerHTML = "Please enter only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadEstimatedProject.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadEstimatedProject.style.display = "none";
                    lblfileUploadEstimatedProject.innerHTML = "";
                    return false;
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
    
    function ValidateAll() {
            
            var check = true;                                                                        
            if (ValidatefileUploadEstimatedProject()) {return false;}                                                                                                                                                                     
            return check;
        }
        
         function ValidateAllNew() {
            
            var check = true;                                                                        
            if (ValidateProjectNo()) {return false;}                                                                                                                                                                     
            return check;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 50px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Import Estimated Project File</legend>
            <table width="100%">
                <tr>
                    <td>
                        Project No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectNo" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Browse Estimated Project:
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadEstimatedProject" runat="server" Width="100%" Height="29px"
                            BorderStyle="Groove" onblur="return ValidatefileUploadEstimatedProject();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnGetEstimatedFile" CssClass="button" Width="100%" runat="server"
                            Text="Get Estimated Project" OnClientClick="return ValidateAll();" OnClick="btnGetEstimatedFile_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="ImportEstimatedFile" CssClass="button" Width="100%" runat="server"
                            Text="Import Estimated Project" OnClick="ImportEstimatedFile_Click" OnClientClick="return ValidateAllNew();"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                    <td>
                        <div id="divfileUploadEstimatedProject" style="display: none;">
                            <asp:Label ID="lblfileUploadEstimatedProject" runat="server" ForeColor="Red" />
                        </div>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <div align="center">
        <asp:Panel ID="pnlMsg" Visible="false" runat="server">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
        </asp:Panel>
    </div>
    <br />
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]"></asp:Label></legend>
            <div style='overflow: auto; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvEstimatedProject" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                    HorizontalAlign="Center">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Visible="true" Text='<%# Eval("DESCRIPTION" ) %>' />
                                <asp:Label ID="lblScope" runat="server" Visible="false" Text='<%# Eval("SCOPE" ) %>' />
                                <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY" ) %>' />
                                <asp:Label ID="lblUnitRateINR" runat="server" Visible="false" Text='<%# Eval("UNIT_RATE_INR" ) %>' />
                                <asp:Label ID="lblPriceINR" runat="server" Visible="false" Text='<%# Eval("PRICE_INR" ) %>' />
                                <asp:Label ID="lblPAndF" runat="server" Visible="false" Text='<%# Eval("P_AND_F" ) %>' />
                                <asp:Label ID="lblPFD" runat="server" Visible="false" Text='<%# Eval("PFD" ) %>' />
                                <asp:Label ID="lblED" runat="server" Visible="false" Text='<%# Eval("ED" ) %>' />
                                <asp:Label ID="lblTotalED" runat="server" Visible="false" Text='<%# Eval("TOTAL_ED" ) %>' />
                                <asp:Label ID="lblST" runat="server" Visible="false" Text='<%# Eval("ST" ) %>' />
                                <asp:Label ID="lblSTAmt" runat="server" Visible="false" Text='<%# Eval("ST_AMT" ) %>' />
                                <asp:Label ID="lblTotalCostInclEDST" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_INCL_ED_ST" ) %>' />
                                <asp:Label ID="lblImportedEXWRateEURO" runat="server" Visible="false" Text='<%# Eval("IMPORTED_EX_W_RATE_EURO" ) %>' />
                                <asp:Label ID="lblImportedEXWPriceEURO" runat="server" Visible="false" Text='<%# Eval("IMPORTED_EX_W_PRICE_EURO" ) %>' />
                                <asp:Label ID="lblFOBPriceEURO" runat="server" Visible="false" Text='<%# Eval("FOB_PRICE_EURO" ) %>' />
                                <asp:Label ID="lblEquivRupeePrice" runat="server" Visible="false" Text='<%# Eval("EQUIV_RUPEE_PRICE" ) %>' />
                                <asp:Label ID="lblFullDuty" runat="server" Visible="false" Text='<%# Eval("FULL_DUTY" ) %>' />
                                <asp:Label ID="lblTotalImportCost" runat="server" Visible="false" Text='<%# Eval("TOTAL_IMPORT_COST" ) %>' />
                                <asp:Label ID="lblTotalCostINR" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_INR" ) %>' />
                                <asp:Label ID="lblTotalCostEURO" runat="server" Visible="false" Text='<%# Eval("TOTAL_COST_EURO" ) %>' />
                                <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("CATEGORY" ) %>' />
                                <asp:Label ID="lblCodes" runat="server" Visible="false" Text='<%# Eval("CODES") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SCOPE" HeaderText="SCOPE" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="UNIT_RATE_INR" HeaderText="UNIT_RATE_INR" />
                        <asp:BoundField DataField="PRICE_INR" HeaderText="PRICE_INR" />
                        <asp:BoundField DataField="P_AND_F" HeaderText="P_AND_F" />
                        <asp:BoundField DataField="PFD" HeaderText="PFD" />
                        <asp:BoundField DataField="ED" HeaderText="ED" />
                        <asp:BoundField DataField="TOTAL_ED" HeaderText="TOTAL_ED" />
                        <asp:BoundField DataField="ST" HeaderText="ST" />
                        <asp:BoundField DataField="ST_AMT" HeaderText="ST_AMT" />
                        <asp:BoundField DataField="TOTAL_COST_INCL_ED_ST" HeaderText="TOTAL_COST_INCL_ED_ST" />
                        <asp:BoundField DataField="IMPORTED_EX_W_RATE_EURO" HeaderText="IMPORTED_EX_W_RATE_EURO" />
                        <asp:BoundField DataField="IMPORTED_EX_W_PRICE_EURO" HeaderText="IMPORTED_EX_W_PRICE_EURO" />
                        <asp:BoundField DataField="FOB_PRICE_EURO" HeaderText="FOB_PRICE_EURO" />
                        <asp:BoundField DataField="EQUIV_RUPEE_PRICE" HeaderText="EQUIV_RUPEE_PRICE" />
                        <asp:BoundField DataField="FULL_DUTY" HeaderText="FULL_DUTY" />
                        <asp:BoundField DataField="TOTAL_IMPORT_COST" HeaderText="TOTAL_IMPORT_COST" />
                        <asp:BoundField DataField="TOTAL_COST_INR" HeaderText="TOTAL_COST_INR" />
                        <asp:BoundField DataField="TOTAL_COST_EURO" HeaderText="TOTAL_COST_EURO" />
                        <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" />
                        <asp:BoundField DataField="CODES" HeaderText="CODES" />
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
