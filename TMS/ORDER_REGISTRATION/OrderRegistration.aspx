<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="OrderRegistration.aspx.cs"
    Inherits="ORDER_REGISTRATION_OrderRegistration" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            document.getElementById('<%=txtPODate.ClientID %>').value = document.getElementById('<%=hdPODate.ClientID %>').value;            
            document.getElementById('<%=hdPODate.ClientID %>').value = document.getElementById('<%=txtPODate.ClientID %>').value;
                        
            document.getElementById('<%=txtExchangeRateDate.ClientID %>').value = document.getElementById('<%=hdExchangeRateDate.ClientID %>').value;            
            document.getElementById('<%=hdExchangeRateDate.ClientID %>').value = document.getElementById('<%=txtExchangeRateDate.ClientID %>').value;
            
            document.getElementById('<%=txtDeliveryDate.ClientID %>').value = document.getElementById('<%=hdDeliveryDate.ClientID %>').value;             
            document.getElementById('<%=hdDeliveryDate.ClientID %>').value = document.getElementById('<%=txtDeliveryDate.ClientID %>').value;
            
            document.getElementById('<%=txtOrderRegistrationDate.ClientID %>').value = document.getElementById('<%=hdOrderRegistrationDate.ClientID %>').value;
            document.getElementById('<%=hdOrderRegistrationDate.ClientID %>').value = document.getElementById('<%=txtOrderRegistrationDate.ClientID %>').value;
        }

        function clientChangedPODate(sender, args) {
            document.getElementById('<%=hdPODate.ClientID %>').value = document.getElementById('<%=txtPODate.ClientID %>').value;
            document.getElementById('<%=txtPODate.ClientID %>').value = document.getElementById('<%=hdPODate.ClientID %>').value;            
        }   
        
         function clientChangedExchangeRateDate(sender, args) {
            document.getElementById('<%=hdExchangeRateDate.ClientID %>').value = document.getElementById('<%=txtExchangeRateDate.ClientID %>').value;
            document.getElementById('<%=txtExchangeRateDate.ClientID %>').value = document.getElementById('<%=hdExchangeRateDate.ClientID %>').value;            
        }   
        
        function clientChangedDeliveryDate(sender, args) {
            document.getElementById('<%=hdDeliveryDate.ClientID %>').value = document.getElementById('<%=txtDeliveryDate.ClientID %>').value;
            document.getElementById('<%=txtDeliveryDate.ClientID %>').value = document.getElementById('<%=hdDeliveryDate.ClientID %>').value;            
        }   
        
        function clientChangedOrderRegistrationDate(sender, args) {
            document.getElementById('<%=hdOrderRegistrationDate.ClientID %>').value = document.getElementById('<%=txtOrderRegistrationDate.ClientID %>').value;
            document.getElementById('<%=txtOrderRegistrationDate.ClientID %>').value = document.getElementById('<%=hdOrderRegistrationDate.ClientID %>').value;            
        }   
        
               
    </script>

    <script type="text/javascript">
    function ValidateOrderNo() {
            var OrderNo = document.getElementById('<%=txtOrderNo.ClientID %>').value;
            if (OrderNo == '') {
            alert("Please enter Order No.");
                document.getElementById('<%=txtOrderNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtOrderNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateGSSNo() {
            var GSSNo = document.getElementById('<%=txtGSSNo.ClientID %>').value;
            if (GSSNo == '') {
            alert("Please enter GSS No.");
                document.getElementById('<%=txtGSSNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGSSNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                    
        function ValidateCustomerName() {
            var CustomerName = document.getElementById('<%=txtCustomerName.ClientID %>').value;
            if (CustomerName  == '') {
            alert("Please enter Customer Name");
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCustomerName.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateEndUser() {
            var EndUser = document.getElementById('<%=txtEndUser.ClientID %>').value;
            if (EndUser == '') {
            alert("Please enter End User Name");
                document.getElementById('<%=txtEndUser.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEndUser.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidatePONo() {
            var PONo = document.getElementById('<%=txtPONo.ClientID %>').value;
            if (PONo == '') {
            alert("Please enter PO no.");
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtPONo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateHSNCode() {
            var HSNCode = document.getElementById('<%=txtHSNCode.ClientID %>').value;
            if (HSNCode == '') {
            alert("Please enter HSN code");
                document.getElementById('<%=txtHSNCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtHSNCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateDescription() {
            var Description = document.getElementById('<%=txtDescription.ClientID %>').value;
            if (Description == '') {
            alert("Please enter Description");
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDescription.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
     function ValidateBasicValueINR() {
            var BasicValueINR = document.getElementById('<%=txtBasicValueINR.ClientID %>').value;
            if (BasicValueINR == '') {
            alert("Please enter Basic Value");
                document.getElementById('<%=txtBasicValueINR.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtBasicValueINR.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
    function ValidateMaterialCost() {
            var MaterialCost = document.getElementById('<%=txtMaterialCost.ClientID %>').value;
            if (MaterialCost == '') {
            alert("Please enter Material Cost");
                document.getElementById('<%=txtMaterialCost.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtMaterialCost.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    

    function ValidateGrossMarginValue() {
            var GrossMarginValue = document.getElementById('<%=txtGrossMarginValue.ClientID %>').value;
            if (GrossMarginValue == '') {
                alert("Please enter Gross Margin Value");
                document.getElementById('<%=txtGrossMarginValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtGrossMarginValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }
         
        function ValidateDivision() {
            var Division = document.getElementById('<%=ddlDivision.ClientID %>').selectedIndex;
            if (Division == '' || Division=='0') {
            alert("Please select Division");
                document.getElementById('<%=ddlDivision.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDivision.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateBusinessUnit() {
            var BusinessUnit = document.getElementById('<%=ddlBusinessUnit.ClientID %>').selectedIndex;
            if (BusinessUnit == '' || BusinessUnit=='0') {
            alert("Please select Business Unit");
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlBusinessUnit.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateTargetGroupName() {
            var TargetGroupName = document.getElementById('<%=ddlTargetGroupName.ClientID %>').selectedIndex;
            if (TargetGroupName == '' || TargetGroupName=='0') {
            alert("Please select Target Group Name");
                document.getElementById('<%=ddlTargetGroupName.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTargetGroupName.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                            
       
        function ValidateIndustryCode() {
            var IndustryCode  = document.getElementById('<%=txtIndustryCode.ClientID %>').value;
            if (IndustryCode == '') {
            alert("Please enter Industry Code");
                document.getElementById('<%=txtIndustryCode.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtIndustryCode.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateTypeOfProject() {
            var TypeOfProject  = document.getElementById('<%=ddlTypeOfProject.ClientID %>').selectedIndex;
            if (TypeOfProject  == '' || TypeOfProject=='0') {
            alert("Please select Type of Project");
                document.getElementById('<%=ddlTypeOfProject.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlTypeOfProject.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateCustomerCharacter() {
            var CustomerCharacter = document.getElementById('<%=ddlCustomerCharacter.ClientID %>').selectedIndex;
            if (CustomerCharacter == '' || CustomerCharacter  =='0') {
            alert("Please select Customer Character");
                document.getElementById('<%=ddlCustomerCharacter.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCustomerCharacter.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateOrderBookingLocation() {
            var OrderBookingLocation = document.getElementById('<%=ddlOrderBookingLocation.ClientID %>').selectedIndex;
            if (OrderBookingLocation == '' || OrderBookingLocation =='0') {
             alert("Please select Order Booking Location");
                document.getElementById('<%=ddlOrderBookingLocation.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlOrderBookingLocation.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateProjectManager() {
            var ProjectManager = document.getElementById('<%=ddlProjectManager.ClientID %>').selectedIndex;
            if (ProjectManager == '' || ProjectManager =='0') {
            alert("Please select Project Manager");
                document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectManager.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateProjectEngineer() {
            var ProjectEngineer = document.getElementById('<%=ddlProjectEngineer.ClientID %>').selectedIndex;
            if (ProjectEngineer == '' || ProjectEngineer =='0') {
            alert("Please select Project Engineer");
                document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlProjectEngineer.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateSalesManager() {
            var SalesManager = document.getElementById('<%=ddlSalesManager.ClientID %>').selectedIndex;
            if (SalesManager == '' || SalesManager =='0') {
            alert("Please select Sales Manager");
                document.getElementById('<%=ddlSalesManager.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSalesManager.ClientID %>').style.borderColor = "";
                return false;
            }
        }
    
        function ValidateSalesEngineer() {
            var SalesEngineer = document.getElementById('<%=ddlSalesEngineer.ClientID %>').selectedIndex;
            if (SalesEngineer == '' || SalesEngineer =='0') {
            alert("Please select Sales Engineer");
                document.getElementById('<%=ddlSalesEngineer.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlSalesEngineer.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateQuotationNo() {
            var QuotationNo  = document.getElementById('<%=txtQuotationNo.ClientID %>').value;
            if (QuotationNo == '') {
            alert("Please enter Quotation No");
                document.getElementById('<%=txtQuotationNo.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuotationNo.ClientID %>').style.borderColor = "";
                return false;
            }
        }
             
       function ValidateDeliveryTerms() {
            var DeliveryTerms = document.getElementById('<%=ddlDeliveryTerms.ClientID %>').selectedIndex;
            if (DeliveryTerms == '' || DeliveryTerms =='0') {
            alert("Please select Delivery Terms");
                document.getElementById('<%=ddlDeliveryTerms.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlDeliveryTerms.ClientID %>').style.borderColor = "";
                return false;
            }
        }                                          
    </script>

    <script type="text/javascript">
    
    function ValidateAll() {
            var check = true;

            if( ValidateOrderNo()) {return false;}
   
            if( ValidateGSSNo()) {return false;}
                             
            if( ValidateCustomerName()) {return false;}
                 
            if( ValidateEndUser()) {return false;}
                   
            if( ValidatePONo()) {return false;}
                  
            if( ValidateHSNCode()) {return false;}
                    
            if( ValidateDescription()) {return false;}
             
            if( ValidateBasicValueINR()) {return false;}
                
            if( ValidateMaterialCost()) {return false;}

            if( ValidateGrossMarginValue()) {return false;}                            
                    
            if( ValidateDivision()) {return false;}
                  
            if( ValidateBusinessUnit()) {return false;}
                    
            if( ValidateTargetGroupName()) {return false;}                         
                   
            if( ValidateIndustryCode()) {return false;}
                
            if( ValidateTypeOfProject()) {return false;}
                    
            if( ValidateCustomerCharacter()) {return false;}
                    
            if( ValidateOrderBookingLocation()) {return false;}
             
            if( ValidateProjectManager()) {return false;}
                    
            if( ValidateProjectEngineer()) {return false;}
              
            if( ValidateSalesManager()) {return false;}
                
            if( ValidateSalesEngineer()) {return false;}
                  
            if( ValidateQuotationNo()) {return false;}
                      
            if( ValidateDeliveryTerms()) {return false;}
                                                                             
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

    <script type="text/Javascript">
        function checkDecNew1(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (ex.test(el.value) == false) {
                el.value = el.value.substring(0, el.value.length - 1);
                if (el.value == '') {
                    checkDecNew2();
                }
            }
            else {
                checkDecNew2();
            }
        }
        
        function checkDecNew2() {
        
        var BasicValue= '0';
        var ExchangeRate= '0';
        var BasicValueINR= '0';
        
        var MaterialCost= '0';
        
        var GrossMarginValue= '0';
        var GrossMarginPercentage= '0';
                
        var CIDEnggHours= '0';
        var CIDEnggHoursRate= '0';
        var CIDEnggHoursValue= '0';
        
        var CWGEnggHours= '0';
        var CWGEnggHoursRate= '0';
        var CWGEnggHoursValue= '0';
        
        var EstimatedTravelCost= '0';
        var Supervision= '0';
        
        var PurchaseRatePercentage= '0';
        var PurchaseRateValue= '0';
        
        var WarrantyCostPercentage= '0';
        var WarrantyCostValue= '0';
        
        var RoyaltyPercentage= '0';
        var RoyaltyValue= '0';
        
        var InsurancePercentage= '0';
        var InsuranceValue= '0';
        
        var Fraight= '0';
        var Commision1Percentage= '0';
        var Commision1Value= '0';
        
        var Commision2Percentage= '0';
        var Commision2Value= '0';
        
        var TotalValue= '0';
        
        
        if (document.getElementById('<%=txtBasicValue.ClientID %>').value != '') {
                BasicValue = document.getElementById('<%=txtBasicValue.ClientID %>').value;
            }
            else {
                BasicValue = '0';
            }
                
        if (document.getElementById('<%=txtExchangeRate.ClientID %>').value != '') {
                ExchangeRate = document.getElementById('<%=txtExchangeRate.ClientID %>').value;
            }
            else {
                ExchangeRate = '0';
            }
            
        BasicValueINR=parseFloat(BasicValue)*parseFloat(ExchangeRate);
        document.getElementById('<%=hdBasicValueINR.ClientID %>').value=Math.round(BasicValueINR);            
        document.getElementById('<%=txtBasicValueINR.ClientID %>').value=document.getElementById('<%=hdBasicValueINR.ClientID %>').value;
        
        
        if (document.getElementById('<%=txtMaterialCost.ClientID %>').value != '') {
                MaterialCost = Math.round(document.getElementById('<%=txtMaterialCost.ClientID %>').value);
            }
            else {
                MaterialCost= '0';
            }
            
                                                                                                                                                                                                                                                               
          if (document.getElementById('<%=txtCIDEnggHours.ClientID %>').value != '') {
                CIDEnggHours = document.getElementById('<%=txtCIDEnggHours.ClientID %>').value;
            }
            else {
                CIDEnggHours= '0';
            }
        
        if (document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').value != '') {
                CIDEnggHoursRate = document.getElementById('<%=txtCIDEnggHoursRate.ClientID %>').value;
            }
            else {
                CIDEnggHoursRate= '0';
            }
            
        CIDEnggHoursValue=parseFloat(CIDEnggHours)*parseFloat(CIDEnggHoursRate);
        document.getElementById('<%=hdCIDEnggHoursValue.ClientID %>').value=Math.round(CIDEnggHoursValue);            
        document.getElementById('<%=txtCIDEnggHoursValue.ClientID %>').value=document.getElementById('<%=hdCIDEnggHoursValue.ClientID %>').value  ;
        
        
                               
        if (document.getElementById('<%=txtCWGEnggHours.ClientID %>').value != '') {
                CWGEnggHours = document.getElementById('<%=txtCWGEnggHours.ClientID %>').value;
            }
            else {
                CWGEnggHours= '0';
            }
        
        if (document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').value != '') {
                CWGEnggHoursRate = document.getElementById('<%=txtCWGEnggHoursRate.ClientID %>').value;
            }
            else {
                CWGEnggHoursRate= '0';
            }
         
        CWGEnggHoursValue=parseFloat(CWGEnggHours)*parseFloat(CWGEnggHoursRate);
        document.getElementById('<%=hdCWGEnggHoursValue.ClientID %>').value=Math.round(CWGEnggHoursValue);            
        document.getElementById('<%=txtCWGEnggHoursValue.ClientID %>').value=document.getElementById('<%=hdCWGEnggHoursValue.ClientID %>').value  ;
        
        
           
           if (document.getElementById('<%=txtEstimatedTravelCost.ClientID %>').value != '') {
                EstimatedTravelCost = Math.round(document.getElementById('<%=txtEstimatedTravelCost.ClientID %>').value);
            }
            else {
                EstimatedTravelCost= '0';
            }
            
            
        
         if (document.getElementById('<%=txtSupervision.ClientID %>').value != '') {
                Supervision =Math.round(document.getElementById('<%=txtSupervision.ClientID %>').value);
            }
            else {
                Supervision= '0';
            }
            
            
                        
        if (document.getElementById('<%=txtPurchaseRatePercentage.ClientID %>').value != '') {
                PurchaseRatePercentage = document.getElementById('<%=txtPurchaseRatePercentage.ClientID %>').value;
            }
            else {
                PurchaseRatePercentage= '0';
            }
                      
        PurchaseRateValue=(parseFloat(MaterialCost)*parseFloat(PurchaseRatePercentage))/100;
        document.getElementById('<%=hdPurchaseRateValue.ClientID %>').value=Math.round(PurchaseRateValue);            
        document.getElementById('<%=txtPurchaseRateValue.ClientID %>').value=document.getElementById('<%=hdPurchaseRateValue.ClientID %>').value
        
        
        
        
        if (document.getElementById('<%=txtWarrantyCostPercentage.ClientID %>').value != '') {
                WarrantyCostPercentage = document.getElementById('<%=txtWarrantyCostPercentage.ClientID %>').value;
            }
            else {
                WarrantyCostPercentage= '0';
            }
                      
        WarrantyCostValue=(parseFloat(BasicValueINR)*parseFloat(WarrantyCostPercentage))/100;
        document.getElementById('<%=hdWarrantyCostValue.ClientID %>').value=Math.round(WarrantyCostValue);            
        document.getElementById('<%=txtWarrantyCostValue.ClientID %>').value=document.getElementById('<%=hdWarrantyCostValue.ClientID %>').value;    
            
        
         if (document.getElementById('<%=txtRoyaltyPercentage.ClientID %>').value != '') {
                RoyaltyPercentage = document.getElementById('<%=txtRoyaltyPercentage.ClientID %>').value;
            }
            else {
                RoyaltyPercentage= '0';
            }
                   
        RoyaltyValue=(parseFloat(BasicValueINR)*parseFloat(RoyaltyPercentage))/100;
        document.getElementById('<%=hdRoyaltyValue.ClientID %>').value=Math.round(RoyaltyValue);            
        document.getElementById('<%=txtRoyaltyValue.ClientID %>').value=document.getElementById('<%=hdRoyaltyValue.ClientID %>').value ;
        
        
        if (document.getElementById('<%=txtInsurancePercentage.ClientID %>').value != '') {
                InsurancePercentage = document.getElementById('<%=txtInsurancePercentage.ClientID %>').value;
            }
            else {
                InsurancePercentage= '0';
            }
                           
        InsuranceValue=(parseFloat(BasicValueINR)*parseFloat(InsurancePercentage))/100;
        document.getElementById('<%=hdInsuranceValue.ClientID %>').value=Math.round(InsuranceValue);            
        document.getElementById('<%=txtInsuranceValue.ClientID %>').value=document.getElementById('<%=hdInsuranceValue.ClientID %>').value
        
        
        if (document.getElementById('<%=txtFraight.ClientID %>').value != '') {
                Fraight = document.getElementById('<%=txtFraight.ClientID %>').value;
            }
            else {
                Fraight= '0';
            }
        
        
        if (document.getElementById('<%=txtCommision1Percentage.ClientID %>').value != '') {
                Commision1Percentage = document.getElementById('<%=txtCommision1Percentage.ClientID %>').value;
            }
            else {
                Commision1Percentage= '0';
            }
                            
        Commision1Value=(parseFloat(ExchangeRate)*parseFloat(Commision1Percentage))/100;
        document.getElementById('<%=hdCommision1Value.ClientID %>').value=Math.round(Commision1Value);            
        document.getElementById('<%=txtCommision1Value.ClientID %>').value=document.getElementById('<%=hdCommision1Value.ClientID %>').value
        
        
        
        if (document.getElementById('<%=txtCommision2Percentage.ClientID %>').value != '') {
                Commision2Percentage = document.getElementById('<%=txtCommision2Percentage.ClientID %>').value;
            }
            else {
                Commision2Percentage = '0';
            }            
       
        Commision2Value=(parseFloat(ExchangeRate)*parseFloat(Commision2Percentage))/100;
        document.getElementById('<%=hdCommision2Value.ClientID %>').value=Math.round(Commision2Value);            
        document.getElementById('<%=txtCommision2Value.ClientID %>').value=document.getElementById('<%=hdCommision2Value.ClientID %>').value
        
        
        
        
        TotalValue=parseFloat(MaterialCost)+
                    parseFloat(CIDEnggHoursValue)+
                    parseFloat(CWGEnggHoursValue)+
                    parseFloat(EstimatedTravelCost)+
                    parseFloat(Supervision)+
                    parseFloat(PurchaseRateValue)+
                    parseFloat(WarrantyCostValue)+
                    parseFloat(RoyaltyValue)+
                    parseFloat(InsuranceValue)+
                    parseFloat(Fraight)+
                    parseFloat(Commision1Value)+
                    parseFloat(Commision2Value);
                    
       document.getElementById('<%=hdTotalValue.ClientID %>').value=Math.round(TotalValue);
       document.getElementById('<%=txtTotalValue.ClientID %>').value=document.getElementById('<%=hdTotalValue.ClientID %>').value;
       
       
       
       
       GrossMarginValue=parseFloat(BasicValueINR)-(parseFloat(TotalValue)+parseFloat(Commision1Value)+parseFloat(Commision2Value));
       document.getElementById('<%=hdGrossMarginValue.ClientID %>').value=Math.round(GrossMarginValue);            
       document.getElementById('<%=txtGrossMarginValue.ClientID %>').value=document.getElementById('<%=hdGrossMarginValue.ClientID %>').value ;
                      
       GrossMarginPercentage=(parseFloat(GrossMarginValue)/parseFloat(BasicValueINR))*100;
       document.getElementById('<%=hdGrossMarginPercentage.ClientID %>').value=GrossMarginPercentage;            
       document.getElementById('<%=txtGrossMarginPercentage.ClientID %>').value=document.getElementById('<%=hdGrossMarginPercentage.ClientID %>').value ;
                               

        }                
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div align="center" style="margin-top: 10px;">
        <fieldset style="width: 60%;">
            <legend style="text-align: center;">Order Registration</legend>
            <table width="100%">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Height="50px">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" />
                </asp:Panel>
                <tr>
                    <td colspan="5" align="center">
                        <u><b>Commercial Summary</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Order No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderNo" runat="server" Width="100%" onblur="return ValidateOrderNo();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        GSS No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtGSSNo" runat="server" Width="100%" onblur="return ValidateGSSNo();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 65%;">
                                    <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" Enabled="false" onblur="return ValidateCustomerName();" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 10%;">
                                    <asp:TextBox ID="txtCustomerCode" runat="server" Width="100%" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnGetCustomer" CssClass="button" runat="server" Text="Get Customer"
                                        Width="100%" OnClick="btnGetCustomer_Click" /><%--OnClick="btnGetCustomer_Click"--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        End User:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndUser" runat="server" Width="100%" onblur="return ValidateEndUser();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        PO No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPONo" runat="server" Width="100%" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        PO Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPODate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdPODate" runat="server" />
                                    <asp:CalendarExtender ID="calendarPODate" PopupButtonID="imgbtnPODate" runat="server"
                                        TargetControlID="txtPODate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedPODate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnPODate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="PO Date Calendar" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        HSN Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtHSNCode" runat="server" Width="100%" onblur="return ValidateHSNCode();" />
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
                            Rows="2" onblur="return ValidateDescription();" />
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
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <u><b>Quotation Summary</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Basic Value:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtBasicValue" runat="server" Width="100%" onpaste="return false"
                                        onKeyUp="checkDecNew1(this)" />
                                </td>
                                <td style="width: 10%">
                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15%">
                                    Rate:
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtExchangeRate" runat="server" Text="1" Width="100%" onpaste="return false"
                                        onKeyUp="checkDecNew1(this)" />
                                </td>
                                <td style="width: 10%">
                                    Value(INR):
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtBasicValueINR" runat="server" Width="100%" Enabled="false" onblur="return ValidateBasicValueINR();" />
                                    <asp:HiddenField ID="hdBasicValueINR" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Exchange Rate Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtExchangeRateDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdExchangeRateDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarExchangeRateDate" PopupButtonID="imgbtnExchangeRateDate"
                                        runat="server" TargetControlID="txtExchangeRateDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedExchangeRateDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExchangeRateDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Exchange Rate Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Material Cost:
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaterialCost" runat="server" Width="100%" onblur="return ValidateMaterialCost();"
                            onpaste="return false" onKeyUp="checkDecNew1(this)" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Gross Margin Value:
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <asp:TextBox ID="txtGrossMarginValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdGrossMarginValue" runat="server" />
                                </td>
                                <td style="width: 15%">
                                    Gross Margin(%):
                                </td>
                                <td style="width: 15%">
                                    <asp:TextBox ID="txtGrossMarginPercentage" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdGrossMarginPercentage" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        CID Engg. Hours:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCIDEnggHours" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Rate:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCIDEnggHoursRate" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCIDEnggHoursValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdCIDEnggHoursValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        CWG Engg. Hours:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCWGEnggHours" runat="server" Width="100%" onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Rate:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCWGEnggHoursRate" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCWGEnggHoursValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdCWGEnggHoursValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Estimated Travel Cost:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEstimatedTravelCost" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                            onpaste="return false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Supervision:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSupervision" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                            onpaste="return false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Purchase Overhead/Mtrl Rate(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPurchaseRatePercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPurchaseRateValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdPurchaseRateValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Warranty Cost(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtWarrantyCostPercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWarrantyCostValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdWarrantyCostValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Royalty(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtRoyaltyPercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoyaltyValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdRoyaltyValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Insurance(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtInsurancePercentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInsuranceValue" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdInsuranceValue" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Fraight:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFraight" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                            onpaste="return false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Commision1(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCommision1Percentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommision1Value" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdCommision1Value" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Commision2(%):
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCommision2Percentage" runat="server" Width="100%" onKeyUp="checkDecNew1(this)"
                                        onpaste="return false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommision2Value" runat="server" Width="100%" Enabled="false" />
                                    <asp:HiddenField ID="hdCommision2Value" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Total:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalValue" runat="server" Width="100%" Enabled="false" />
                        <asp:HiddenField ID="hdTotalValue" runat="server" />
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
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <u><b>Customer And Product Summary</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Division:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivision" runat="server" Width="100%" onblur="return ValidateDivision();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Business Unit:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBusinessUnit" runat="server" Width="100%" onblur="return ValidateBusinessUnit();">
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
                        Target Group:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTargetGroupName" runat="server" Width="100%" onblur="return ValidateTargetGroupName();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Industry Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtIndustryCode" runat="server" Width="100%" onblur="return ValidateIndustryCode();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Type of Project
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTypeOfProject" runat="server" Width="100%" onblur="return ValidateTypeOfProject();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Customer Character:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomerCharacter" runat="server" Width="100%" onblur="return ValidateCustomerCharacter();">
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
                        Order Booking Location
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBookingLocation" runat="server" Width="100%" onblur="return ValidateOrderBookingLocation();">
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
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <u><b>Responsible Department Summary</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Project Manager:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProjectManager" runat="server" Width="100%" onblur="return ValidateProjectManager();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Project Engineer:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProjectEngineer" runat="server" Width="100%" onblur="return ValidateProjectEngineer();">
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
                        Sales Manager:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSalesManager" runat="server" Width="100%" onblur="return ValidateSalesManager();">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Sales Engineer:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSalesEngineer" runat="server" Width="100%" onblur="return ValidateSalesEngineer();">
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
                        Quotation No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuotationNo" runat="server" Width="100%" onblur="return ValidateQuotationNo();" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Estimate Attached:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstimateAttached" runat="server" Width="100%">
                            <asp:ListItem Text="No" Value="0" />
                            <asp:ListItem Text="Yes" Value="1" />
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
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <u><b>Payment Summary</b></u>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Payment Terms:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPaymentTerms" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Cradit Days
                    </td>
                    <td>
                        <asp:TextBox ID="txtCraditDays" runat="server" Width="100%" onKeyUp="checkDec(this)" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Delivery Terms:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDeliveryTerms" runat="server" Width="100%" onblur="return ValidateDeliveryTerms();">
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
                        LD:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLd" runat="server" Width="100%">
                            <asp:ListItem Text="No" Value="0" />
                            <asp:ListItem Text="Yes" Value="1" />
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
                        Delivery Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdDeliveryDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarDeliveryDate" PopupButtonID="imgbtnDeliveryDate"
                                        runat="server" TargetControlID="txtDeliveryDate" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedDeliveryDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnDeliveryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Delivery Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Date Of Order Registration:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtOrderRegistrationDate" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="hdOrderRegistrationDate" runat="server" />
                                    <asp:CalendarExtender ID="calendarOrderRegistrationDate" PopupButtonID="imgbtnOrderRegistrationDate"
                                        runat="server" TargetControlID="txtOrderRegistrationDate" Format="dd-MMM-yyyy"
                                        OnClientDateSelectionChanged="clientChangedOrderRegistrationDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnOrderRegistrationDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Order Registration Date Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Remarks:
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Enabled="true" TextMode="MultiLine"
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
                        &nbsp;
                    </td>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Save" OnClientClick="return ValidateAll();"
                                        Width="100%" OnClick="btnSubmit_Click" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                                <td align="center" style="width: 45%">
                                    <asp:Button ID="btnOrderList" CssClass="button" runat="server" Text="Order List"
                                        Width="100%" OnClick="btnOrderList_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </fieldset>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="600px" Width="1000px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblCustomerMsg" runat="server" Font-Bold="True" Visible="false" />
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 10px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <table style="width: 95%; margin-left: 20px;">
                <tr>
                    <td>
                        Customer Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustmerNameSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Customer Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerCodeSearch" runat="server" Width="100%" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSearchCustomer" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchCustomer_Click" /><%--OnClientClick="return ValidateSearch();"--%>
                    </td>
                </tr>
            </table>
            <br />
            <div style='overflow: auto; width: 99%; height: 390px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <div align="center">
                    <asp:GridView ID="gvCustomerList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" PageSize="8" Width="100%" HorizontalAlign="Center"
                        OnRowCommand="gvCustomerList_RowCommand">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="GET_CUSTOMER" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                    <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                    <asp:Label ID="lblPONo" runat="server" Visible="false" Text='<%# Eval("PO_NO") %>' />
                                    <asp:Label ID="lblPoDate" runat="server" Visible="false" Text='<%# Eval("PO_DATE") %>' />
                                    <asp:Button ID="btnGetCustomer" CommandArgument="GET_CUSTOMER" ToolTip="Get Customer"
                                        runat="server" Text="Get Customer" CssClass="cancelbutton" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                            <asp:BoundField DataField="PO_NO" HeaderText="PO_NO" />
                            <asp:BoundField DataField="PO_DATE" HeaderText="PO_DATE" />
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
</asp:Content>
