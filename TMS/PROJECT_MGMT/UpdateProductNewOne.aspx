<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateProductNewOne.aspx.cs"
    Inherits="PROJECT_MGMT_UpdateProductNewOne" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ValidateProject() {
            var Project = document.getElementById('<%=ddlProjectNo.ClientID %>').selectedIndex;
            if (Project== '' || Project== '0') {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEstimatedItem() {
            var EstimatedItem = document.getElementById('<%=ddlEstimatedItem.ClientID %>').selectedIndex;
            if (EstimatedItem== '' || EstimatedItem=='0') {
                document.getElementById('<%=ddlEstimatedItem.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEstimatedItem.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        
        function ValidateType() {
            var Type = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Type== '' || Type== '0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidateCategory() {
            var Category = document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex;
            if (Category== '' || Category== '0') {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategory.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                       
        function ValidateProductCode() {
            var ProductCode = document.getElementById('<%=txtProductCode.ClientID %>').value;
            if (ProductCode == '') {
                document.getElementById('<%=txtProductCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtProductCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateGSTHSN() {
            var HSN = document.getElementById('<%=txtGSTHSN.ClientID %>').value;
            if (HSN== '') {
                document.getElementById('<%=txtGSTHSN.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGSTHSN.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        
        function ValidateGSTRate() {
            var Rate = document.getElementById('<%=txtGSTRate.ClientID %>').value;
            if (Rate== '') {
                document.getElementById('<%=txtGSTRate.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGSTRate.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                
        function ValidatePurchaseUnit() {
            var PurchaseUnit = document.getElementById('<%=ddlPurchaseUnit.ClientID %>').selectedIndex;
            if (PurchaseUnit== '' || PurchaseUnit== '0') {
                document.getElementById('<%=ddlPurchaseUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPurchaseUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateStockUnit() {
            var StockUnit= document.getElementById('<%=ddlStockUnit.ClientID %>').selectedIndex;
            if (StockUnit== '' || StockUnit== '0') {
                document.getElementById('<%=ddlStockUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlStockUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidateSaleUnit() {
            var SaleUnit= document.getElementById('<%=ddlSaleUnit.ClientID %>').selectedIndex;
            if (SaleUnit== '' || SaleUnit== '0') {
                document.getElementById('<%=ddlSaleUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSaleUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidateTagNo() {
            var TagNo = document.getElementById('<%=txtTagNo.ClientID %>').value;
            if (TagNo== '') {
                document.getElementById('<%=txtTagNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTagNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateModelNo() {
            var ModelNo = document.getElementById('<%=txtModelNo.ClientID %>').value;
            if (ModelNo== '') {
                document.getElementById('<%=txtModelNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtModelNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        
        function ValidateUnit() {
            var Unit = document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex;
            if (Unit== '' || Unit== '0') {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidateGroup() {
            var Group = document.getElementById('<%=ddlGroup.ClientID %>').selectedIndex;
            if (Group== '' || Group== '0') {
                document.getElementById('<%=ddlGroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlGroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateSubgroup() {
            var Subgroup = document.getElementById('<%=ddlSubgroup.ClientID %>').selectedIndex;
            if (Subgroup== '' || Subgroup== '0') {
                document.getElementById('<%=ddlSubgroup.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSubgroup.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                                                                      
        function ValidateAll() {
            var check = true;
            if (ValidateProject()) {return false;}
            if (ValidateEstimatedItem()) {return false;}        
            if (ValidateType()) {return false;}
            if (ValidateCategory()) {return false;}
            if (ValidateProductCode()) {return false;}
            if (ValidateGSTHSN()) {return false;}
            if (ValidateGSTRate()) {return false;}             
            if (ValidatePurchaseUnit()) {return false;} 
            if (ValidateStockUnit()) {return false;} 
            if (ValidateSaleUnit()) {return false;}            
            if (ValidateTagNo()) {return false;}             
            if (ValidateUnit()) {return false;}  
            if (ValidateModelNo()) {return false;}  
            if (ValidateGroup()) {return false;}  
            if (ValidateSubgroup()) {return false;}            
            return check;
        }
        
    </script>

    <script type="text/Javascript">
        function checkDec(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>
            <div align="center" style="margin-top: 20px;">
                <fieldset style="width: 80%;">
                    <legend style="text-align: center;">
                        <asp:Label ID="lblLegend" runat="server" Text="Update Product"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project No.:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectNo" runat="server" Width="100%" Height="25px" onblur="return ValidateProject();"
                                    OnSelectedIndexChanged="ddlProjectNo_SelectedIndexChanged" AutoPostBack="true"
                                    Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" Width="100%" Height="25px" onblur="return ValidateType();"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Enabled="false">
                                    <asp:ListItem Text="Select" Value="0" />
                                    <asp:ListItem Text="Boughtout" Value="1" />
                                    <asp:ListItem Text="Assembled" Value="2" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Estimate Item:
                            </td>
                            <td colspan="4">
                                <asp:DropDownList ID="ddlEstimatedItem" runat="server" Width="100%" Height="25px"
                                    onblur="return ValidateEstimatedItem();" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="100%" Height="25px" onblur="return ValidateCategory();"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                    Enabled="false">
                                    <asp:ListItem Text="Select" Value="0" />
                                    <asp:ListItem Text="System" Value="1" />
                                    <asp:ListItem Text="Component" Value="2" />
                                    <asp:ListItem Text="Parts/Spare" Value="3" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Product Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductCode" runat="server" Width="100%" onblur="return ValidateProductCode();"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtDescription" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Additional Description:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAdditionalDescription" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                GST HSN:
                            </td>
                            <td>
                                <asp:TextBox ID="txtGSTHSN" runat="server" Width="100%" onblur="return ValidateGSTHSN();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                GST Rate:
                            </td>
                            <td>
                                <asp:TextBox ID="txtGSTRate" runat="server" Width="100%" onblur="return ValidateGSTRate();"
                                    onkeyup="checkDec(this);" onpaste="return false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Purchase Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPurchaseUnit" runat="server" Width="100%" Height="25px"
                                    onblur="return ValidatePurchaseUnit();">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Stock Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStockUnit" runat="server" Width="100%" Height="25px" onblur="return ValidateStockUnit();">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sale Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSaleUnit" runat="server" Width="100%" Height="25px" onblur="return ValidateSaleUnit();">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Tag No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTagNo" runat="server" Width="100%" onblur="return ValidateTagNo();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Model No.:
                            </td>
                            <td>
                                <asp:TextBox ID="txtModelNo" runat="server" Width="100%" onblur="return ValidateModelNo();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Unit:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" Height="25px" onblur="return ValidateUnit();"
                                    OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Additional Information:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtAdditionalInformation" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Technical Specification:
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtTechnicalSpecification" runat="server" Width="100%" TextMode="MultiLine"
                                    Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Group:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" runat="server" Width="100%" Height="25px" onblur="return ValidateGroup();"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Subgroup:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubgroup" runat="server" Width="100%" Height="25px" onblur="return ValidateSubgroup();">
                                    <asp:ListItem Text="Select" Value="0" />
                                    <asp:ListItem Text="Boughtout" Value="1" />
                                    <asp:ListItem Text="Assembled" Value="2" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="4">
                                <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                    OnClick="btnSubmit_Click" Width="100%" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="10px">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                    </asp:Panel>
                    <br />
                    <br />
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
