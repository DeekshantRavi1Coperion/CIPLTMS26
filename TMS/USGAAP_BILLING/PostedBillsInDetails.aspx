<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="PostedBillsInDetails.aspx.cs"
    Inherits="USGAAP_BILLING_PostedBillsInDetails" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../Images/Icon04.png" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
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
            document.getElementById('<%=txtStartDateSearch.ClientID %>').value = document.getElementById('<%=hdStartDateSearch.ClientID %>').value;
            document.getElementById('<%=txtEndDateSearch.ClientID %>').value = document.getElementById('<%=hdEndDateSearch.ClientID %>').value;                    
        }

        function clientChangedSearch(sender, args) {
            document.getElementById('<%=hdStartDateSearch.ClientID %>').value = document.getElementById('<%=txtStartDateSearch.ClientID %>').value;
            document.getElementById('<%=hdEndDateSearch.ClientID %>').value = document.getElementById('<%=txtEndDateSearch.ClientID %>').value;

            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }
            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);

            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return false;
            }
        }                      
    </script>

    <script type="text/Javascript">
        function ValidateDateRange() {
            var formatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var formatItems = formatLowerCase.split("-");
            var dateItems = document.getElementById('<%=hdStartDateSearch.ClientID %>').value.split("-");
            var monthIndex = formatItems.indexOf("mmm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var month;
            if (dateItems[monthIndex] == 'Jan') {
                month = 1;
            }
            else if (dateItems[monthIndex] == 'Feb') {
                month = 2;
            }
            else if (dateItems[monthIndex] == 'Mar') {
                month = 3;
            }
            else if (dateItems[monthIndex] == 'Apr') {
                month = 4;
            }
            else if (dateItems[monthIndex] == 'May') {
                month = 5;
            }
            else if (dateItems[monthIndex] == 'Jun') {
                month = 6;
            }
            else if (dateItems[monthIndex] == 'Jul') {
                month = 7;
            }
            else if (dateItems[monthIndex] == 'Aug') {
                month = 8;
            }
            else if (dateItems[monthIndex] == 'Sep') {
                month = 9;
            }
            else if (dateItems[monthIndex] == 'Oct') {
                month = 10;
            }
            else if (dateItems[monthIndex] == 'Nov') {
                month = 11;
            }
            else if (dateItems[monthIndex] == 'Dec') {
                month = 12;
            }

            month -= 1;
            var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);


            var endFormatLowerCase = "dd-MMM-yyyy".toLowerCase();
            var endFormatItems = endFormatLowerCase.split("-");
            var endDateItems = document.getElementById('<%=hdEndDateSearch.ClientID %>').value.split("-");
            var endMonthIndex = endFormatItems.indexOf("mmm");
            var endDayIndex = endFormatItems.indexOf("dd");
            var endYearIndex = endFormatItems.indexOf("yyyy");
            var endMonth;
            if (endDateItems[endMonthIndex] == 'Jan') {
                endMonth = 1;
            }
            else if (endDateItems[endMonthIndex] == 'Feb') {
                endMonth = 2;
            }
            else if (endDateItems[endMonthIndex] == 'Mar') {
                endMonth = 3;
            }
            else if (endDateItems[endMonthIndex] == 'Apr') {
                endMonth = 4;
            }
            else if (endDateItems[endMonthIndex] == 'May') {
                endMonth = 5;
            }
            else if (endDateItems[endMonthIndex] == 'Jun') {
                endMonth = 6;
            }
            else if (endDateItems[endMonthIndex] == 'Jul') {
                endMonth = 7;
            }
            else if (endDateItems[endMonthIndex] == 'Aug') {
                endMonth = 8;
            }
            else if (endDateItems[endMonthIndex] == 'Sep') {
                endMonth = 9;
            }
            else if (endDateItems[endMonthIndex] == 'Oct') {
                endMonth = 10;
            }
            else if (endDateItems[endMonthIndex] == 'Nov') {
                endMonth = 11;
            }
            else if (endDateItems[endMonthIndex] == 'Dec') {
                endMonth = 12;
            }
            endMonth -= 1;
            var endFormatedDate = new Date(endDateItems[endYearIndex], endMonth, endDateItems[endDayIndex]);

            if (endFormatedDate < formatedDate) {
                alert("Invalid Date Range");
                return true;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">


        function ValidatePostingCurrency() {
            var PostingCurrency = document.getElementById('<%=ddlPostingCurrency.ClientID %>').selectedIndex;
            if (PostingCurrency  == '' || PostingCurrency == '0') {
                document.getElementById('<%=ddlPostingCurrency.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlPostingCurrency.ClientID %>').style.borderColor = "";
                return false;
            }
        }


        function ValidateUnpostedValue() {            
            if(parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)<0)
            {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else
            {
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateEndMarket() {
            var EndMarket = document.getElementById('<%=ddlEndMarket.ClientID %>').selectedIndex;
            if (EndMarket  == '' || EndMarket=='0') {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlEndMarket.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateCountry() {
            var Country = document.getElementById('<%=ddlCountry.ClientID %>').selectedIndex;
            if (Country  == '' || Country=='0') {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCountry.ClientID %>').style.borderColor = "";
                return false;
            }
        }
                            
        function ValidateType() {
            var Type = document.getElementById('<%=ddlType.ClientID %>').selectedIndex;
            if (Type  == '' || Type=='0') {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlType.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        function ValidateRevenueType() {
            var RevenueType = document.getElementById('<%=ddlRevenueType.ClientID %>').selectedIndex;
            if (RevenueType  == '' || RevenueType=='0') {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlRevenueType.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        

        function ValidateAllNew() {

            var check = true; 
                       
            if (ValidatePostingCurrency()) {
                return false;
            }
                                                                          
            if (ValidateUnpostedValue()) {
                return false;
            }
            
            if (ValidateEndMarket()) {
                return false;
            }
            
            if (ValidateCountry()) {
                return false;
            }
                                   
            if (ValidateType()) {
                return false;
            }
            
            if (ValidateRevenueType()) {
                return false;
            }
                                                                                                  
            return true;
        }
        
        
        
        function ValidateDeletedReason() {
            var DeletedReason = document.getElementById('<%=txtDeletedReason.ClientID %>').value;
            if (DeletedReason  == '') {
                document.getElementById('<%=txtDeletedReason.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDeletedReason.ClientID %>').style.borderColor = "";
                return false;
            }
        }
        
        
        function ValidateDeleteNew() {

            var check = true; 
                       
            if (ValidateDeletedReason()) {
                return false;
            }
                                                                
            return true;
        }

        function ValidateAll() {

            var check = true;                                                
            if (ValidateDateRange()) {
                return false;
            }

            return true;
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
            var invoiceAmount = '0';
            var postedValue = '0';           
            var postingValue = '0';
            
            
            var oldUnpostedValue = '0'; 
            var unpostedValue = '0'; 
            
            var actualUnpostedValue = '0'; 
                     
            var editedValue = '0';            
            var newValue='0';
                 
                                                            
            postedValue= document.getElementById('<%=hdPostedValue.ClientID %>').value;            
            oldUnpostedValue= document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            unpostedValue= document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            
            actualUnpostedValue=document.getElementById('<%=hdActualUnpostedValue.ClientID %>').value;
            
                        
            if (document.getElementById('<%=txtInvoiceAmount.ClientID %>').value != '') {
                invoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            }
            else {
                invoiceAmount = '0';
            }
            
           
            if (document.getElementById('<%=txtPostedValue.ClientID %>').value != '') {
                postingValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            }
            else {
                postingValue = '0';
            } 
                                                                                                                                                    
         //Case 1 (Negative)
         if(parseFloat(postingValue) < parseFloat(postedValue))
         {                 
            editedValue=Math.round((parseFloat(postedValue) - parseFloat(postingValue)) * 100) / 100;                                               
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)+ parseFloat(editedValue)) * 100) / 100;                                                                                                                                                                                                                                                                                   
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
            document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);            
            document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);
            
             if(postingValue>=0)
             {                
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
             }
             else   
             {
             
                alert("Please enter posting value greater than or equal to 0!");             
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "#F7627F";
                return true;
             }
         }
         
         //Case 2 (Positive)
         if(parseFloat(postingValue) > parseFloat(postedValue))
         {           
            editedValue=Math.round((parseFloat(postingValue) - parseFloat(postedValue)) * 100) / 100;      
            
                                                   
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)- parseFloat(editedValue)) * 100) / 100;            
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
                                                                                                                                                                              
            if(parseFloat(editedValue)<=parseFloat(actualUnpostedValue))
            {
                document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);                                                
                document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);                                              
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
            else
            {                                   
                alert("You are allowed to edit posted value less than or equal to :"+ (parseFloat(postedValue)+parseFloat(actualUnpostedValue)));
                
                document.getElementById('<%=txtPostedValue.ClientID %>').value=(parseFloat(postedValue)+parseFloat(actualUnpostedValue));
                document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =(parseFloat(oldUnpostedValue)-parseFloat(actualUnpostedValue));;
                document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
                
                document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(document.getElementById('<%=txtPostedValue.ClientID %>').value);                
                editedValue=Math.round((parseFloat(document.getElementById('<%=hdNewPostingValue.ClientID %>').value) - parseFloat(postedValue)) * 100) / 100;
                document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);                 
                
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }    
               
                           
         }
         
         //Case 3
         if(parseFloat(postingValue) == parseFloat(postedValue))
         {
            editedValue=Math.round((parseFloat(postingValue) - parseFloat(postedValue)) * 100) / 100;                                     
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)- parseFloat(editedValue)) * 100) / 100;            
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
            
            document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);            
            document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);
         }
        }   
        
        
        
        function checkDecNew3() {               
            var invoiceAmount = '0';
            var postedValue = '0';           
            var postingValue = '0';
            
            
            var oldUnpostedValue = '0'; 
            var unpostedValue = '0'; 
            
            var actualUnpostedValue = '0'; 
                     
            var editedValue = '0';            
            var newValue='0';
                 
                                                            
            postedValue= document.getElementById('<%=hdPostedValue.ClientID %>').value;            
            oldUnpostedValue= document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            unpostedValue= document.getElementById('<%=hdUnpostedValue.ClientID %>').value;
            
            actualUnpostedValue=document.getElementById('<%=hdActualUnpostedValue.ClientID %>').value;
            
                        
            if (document.getElementById('<%=txtInvoiceAmount.ClientID %>').value != '') {
                invoiceAmount = document.getElementById('<%=txtInvoiceAmount.ClientID %>').value;
            }
            else {
                invoiceAmount = '0';
            }
            
           
            if (document.getElementById('<%=txtPostedValue.ClientID %>').value != '') {
                postingValue = document.getElementById('<%=txtPostedValue.ClientID %>').value;
            }
            else {
                postingValue = '0';
            } 
                                                                                                                                                    
         //Case 1 (Negative)
         if(parseFloat(postingValue) < parseFloat(postedValue))
         {                 
            editedValue=Math.round((parseFloat(postedValue) - parseFloat(postingValue)) * 100) / 100;                                               
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)+ parseFloat(editedValue)) * 100) / 100;                                                                                                                                                                                                                                                                                   
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
            document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);            
            document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);
            
             document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
             return false;
         }
         
         //Case 2 (Positive)
         if(parseFloat(postingValue) > parseFloat(postedValue))
         {           
            editedValue=Math.round((parseFloat(postingValue) - parseFloat(postedValue)) * 100) / 100;      
            
                                                   
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)- parseFloat(editedValue)) * 100) / 100;            
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
                                                                                                                                                                              
            if(parseFloat(editedValue)<=parseFloat(actualUnpostedValue))
            {
                document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);                                                
                document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);                                              
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
            }
            else
            {                                   
                alert("You are allowed to edit posted value less than or equal to :"+ (parseFloat(postedValue)+parseFloat(actualUnpostedValue)));
                
                document.getElementById('<%=txtPostedValue.ClientID %>').value=(parseFloat(postedValue)+parseFloat(actualUnpostedValue));
                document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =(parseFloat(oldUnpostedValue)-parseFloat(actualUnpostedValue));;
                document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
                
                document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(document.getElementById('<%=txtPostedValue.ClientID %>').value);                
                editedValue=Math.round((parseFloat(document.getElementById('<%=hdNewPostingValue.ClientID %>').value) - parseFloat(postedValue)) * 100) / 100;
                document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);                 
                
                document.getElementById('<%=txtUnpostedValue.ClientID %>').style.borderColor = "";
                return false;
           }                                              
         }
         
         //Case 3
         if(parseFloat(postingValue) == parseFloat(postedValue))
         {
            editedValue=Math.round((parseFloat(postingValue) - parseFloat(postedValue)) * 100) / 100;                                     
            unpostedValue = Math.round((parseFloat(document.getElementById('<%=hdUnpostedValue.ClientID %>').value)- parseFloat(editedValue)) * 100) / 100;            
            document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value =unpostedValue;
            document.getElementById('<%=txtUnpostedValue.ClientID %>').value = document.getElementById('<%=hdUnpostedValueEdited.ClientID %>').value;
            
            
            document.getElementById('<%=hdNewPostingValue.ClientID %>').value=parseFloat(postingValue);            
            document.getElementById('<%=hdEditedValue.ClientID %>').value=parseFloat(editedValue);
         }
        }                       
    </script>

    <script type="text/Javascript">
        function preventInput(event) {
           if(event.which!=9)
            {
            event.preventDefault();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>
    <div align="center" style="margin-top: 20px;">
        <fieldset style="width: 90%">
            <legend style="text-align: center;">Posted List In Detail</legend>
            <table width="90%">
                <tr>
                    <td>
                        Start Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtStartDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
                                    <asp:HiddenField ID="hdStartDateSearch" runat="server" />
                                    <asp:CalendarExtender ID="calendarStartDateSearch" PopupButtonID="imgbtnStartDateSearch"
                                        runat="server" TargetControlID="txtStartDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnStartDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="Start Date Calendar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        End Date:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEndDateSearch" runat="server" onkeyDown="javascript:preventInput(event);"
                                        Width="100%" />
                                    <asp:HiddenField ID="hdEndDateSearch" runat="server" />
                                    <asp:CalendarExtender ID="calendarEndDateSearch" PopupButtonID="imgbtnEndDateSearch"
                                        runat="server" TargetControlID="txtEndDateSearch" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedSearch">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="imgbtnEndDateSearch" runat="server" ImageUrl="~/Images/Calendar2.png"
                                        ToolTip="End Date Calendar" Width="20px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Invoice No.:
                    </td>
                    <td>
                        <asp:TextBox ID="txtBillNo" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Revenue Account:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRevenueAccount" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Company:
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" CssClass="button" Width="100%" runat="server" Text="Search"
                                        OnClientClick="return ValidateAll();" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" CssClass="button" Width="100%" runat="server" Text="Export"
                                        OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
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
    <div align="center">
        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <div style='overflow: auto; width: 100%; height: 500px; border: 1px solid lightgray;'>
                <asp:GridView ID="gvPostedList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" PageSize="20" Width="100%" HorizontalAlign="Center"
                    OnRowDataBound="gvPostedList_RowDataBound" OnRowCommand="gvPostedList_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="EDIT">
                            <ItemTemplate>
                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID") %>' />
                                <asp:Label ID="lblInvoiceNo" runat="server" Visible="false" Text='<%# Eval("INVOICE_NO") %>' />
                                <asp:Label ID="lblInvoiceDate" runat="server" Visible="false" Text='<%# Eval("INVOICE_DATE") %>' />
                                <asp:Label ID="lblCustomerCode" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_CODE") %>' />
                                <asp:Label ID="lblCustomerName" runat="server" Visible="false" Text='<%# Eval("CUSTOMER_NAME") %>' />
                                <asp:Label ID="lblJobNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblBusinessUnit" runat="server" Visible="false" Text='<%# Eval("BUSINESS_UNIT") %>' />
                                <asp:Label ID="lblProductCode" runat="server" Visible="false" Text='<%# Eval("PRODUCT_CODE") %>' />
                                <asp:Label ID="lblRevenueAccount" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT") %>' />
                                <asp:Label ID="lblRevenueAccountDesc" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_DESC") %>' />
                                <asp:Label ID="lblRevenueAccountType" runat="server" Visible="false" Text='<%# Eval("REVENUE_ACCOUNT_TYPE") %>' />
                                <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                                <asp:Label ID="lblProductRate" runat="server" Visible="false" Text='<%# Eval("PRODUCT_RATE") %>' />
                                <asp:Label ID="lblInvoiceAmount" runat="server" Visible="false" Text='<%# Eval("INVOICE_AMOUNT") %>' />
                                <asp:Label ID="lblLocation" runat="server" Visible="false" Text='<%# Eval("LOCATION") %>' />
                                <asp:Label ID="lblPostedValue" runat="server" Visible="false" Text='<%# Eval("POSTED_VALUE") %>' />
                                <asp:Label ID="lblPostedDate" runat="server" Visible="false" Text='<%# Eval("POSTED_DATE") %>' />
                                <asp:Label ID="lblUnpostedValue" runat="server" Visible="false" Text='<%# Eval("UNPOSTED_VALUE") %>' />
                                <asp:Label ID="lblActualUnpostedValue" runat="server" Visible="false" Text='<%# Eval("ACTUAL_UNPOSTED_VALUE") %>' />
                                <asp:Label ID="lblEndMarket" runat="server" Visible="false" Text='<%# Eval("END_MARKET_CODE") %>' />
                                <asp:Label ID="lblGeogrophy" runat="server" Visible="false" Text='<%# Eval("COUNTRY_ISO_CODE") %>' />
                                <asp:Label ID="lblPostingMonth" runat="server" Visible="false" Text='<%# Eval("POSTING_MONTH") %>' />
                                <asp:Label ID="lblPostingYear" runat="server" Visible="false" Text='<%# Eval("POSTING_YEAR") %>' />
                                <asp:Label ID="lblPostingCurrencyID" runat="server" Visible="false" Text='<%# Eval("POSTING_CURRENCY_ID") %>' />
                                <asp:Label ID="lblPostingCurrency" runat="server" Visible="false" Text='<%# Eval("POSTING_CURRENCY") %>' />
                                <asp:Label ID="lblTypeID" runat="server" Visible="false" Text='<%# Eval("TYPE_ID") %>' />
                                <asp:Label ID="lblRevenueTypeID" runat="server" Visible="false" Text='<%# Eval("REVENUE_TYPE_ID") %>' />
                                <asp:Label ID="lblUDF1" runat="server" Visible="false" Text='<%# Eval("UDF1") %>' />
                                <asp:Label ID="lblUDF2" runat="server" Visible="false" Text='<%# Eval("UDF2") %>' />
                                <asp:Label ID="lblUDF3" runat="server" Visible="false" Text='<%# Eval("UDF3") %>' />
                                <asp:Label ID="lblUDF4" runat="server" Visible="false" Text='<%# Eval("UDF4") %>' />
                                <asp:Label ID="lblUDF5" runat="server" Visible="false" Text='<%# Eval("UDF5") %>' />
                                <asp:ImageButton ID="imgProperties" CommandArgument="PROPERTIES" runat="server" ImageUrl="~/Images/royal_search.png"
                                    ToolTip="Edit Posted Bill" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                        <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE_NO" />
                        <asp:BoundField DataField="INVOICE_DATE" HeaderText="INVOICE_DATE" />
                        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="CUSTOMER_CODE" />
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="BUSINESS_UNIT" HeaderText="BUSINESS_UNIT" />
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="PRODUCT_CODE" />
                        <asp:BoundField DataField="REVENUE_ACCOUNT" HeaderText="REVENUE_ACCOUNT" />
                        <asp:BoundField DataField="REVENUE_ACCOUNT_DESC" HeaderText="ACCOUNT_DESC" />
                        <asp:BoundField DataField="REVENUE_ACCOUNT_TYPE" HeaderText="ACCOUNT_TYPE" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY" />
                        <asp:BoundField DataField="PRODUCT_RATE" HeaderText="PRODUCT_RATE" />
                        <asp:BoundField DataField="INVOICE_AMOUNT" HeaderText="INVOICE_AMOUNT" />
                        <asp:BoundField DataField="POSTED_VALUE" HeaderText="POSTED_VALUE" />
                        <asp:BoundField DataField="POSTED_DATE" HeaderText="POSTED_DATE" />
                        <asp:BoundField DataField="UNPOSTED_VALUE" HeaderText="UNPOSTED_VALUE" />
                        <asp:BoundField DataField="END_MARKET" HeaderText="END_MARKET" />
                        <asp:BoundField DataField="GEOGROPHY" HeaderText="GEOGROPHY" />
                        <asp:BoundField DataField="POSTING_MONTH" HeaderText="POSTING_MONTH" />
                        <asp:BoundField DataField="POSTING_YEAR" HeaderText="POSTING_YEAR" />
                        <asp:BoundField DataField="POSTING_CURRENCY" HeaderText="POSTING_CURRENCY" />
                        <asp:BoundField DataField="UDF1" HeaderText="UDF1" />
                        <asp:BoundField DataField="UDF2" HeaderText="UDF2" />
                        <asp:BoundField DataField="UDF3" HeaderText="UDF3" />
                        <asp:BoundField DataField="UDF4" HeaderText="UDF4" />
                        <asp:BoundField DataField="UDF5" HeaderText="UDF5" />
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
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup" CancelControlID="imgBtnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="710px" Width="900px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancel" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 0px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLegend" runat="server" />
            </legend>
            <div style='overflow: auto; width: 99%; height: 610px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 10px;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Invoice Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Job No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNo" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Custoner Name:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtCustName" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCustCode" runat="server" Width="100%" Enabled="false" />
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
                            Business Unit:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBusinessUnit" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Product Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductCode" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Revenue Account:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtRevAccount" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td style="width: 75%">
                                        <asp:TextBox ID="txtAccountDesc" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:TextBox ID="txtAccountType" runat="server" Width="100%" Enabled="false" />
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
                            Quantity:
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Product Rate:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductRate" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Invoice Amount:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceAmount" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Posted Value:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 70%">
                                        <asp:TextBox ID="txtPostedValue" runat="server" Width="100%" onkeyup="checkDecNew3(this);" />
                                        <asp:HiddenField ID="hdPostedValue" runat="server" />
                                        <%--<asp:HiddenField ID="hdNextPostingValue" runat="server" />--%>
                                        <asp:HiddenField ID="hdNewPostingValue" runat="server" />
                                        <asp:HiddenField ID="hdEditedValue" runat="server" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:DropDownList ID="ddlPostingCurrency" runat="server" Width="100%" onblur="return ValidatePostingCurrency();">
                                        </asp:DropDownList>
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
                        <%--<td>
                            Penging Unposted Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPendingUnpostedValue" runat="server" Width="100%" Enabled="false" />
                        </td>--%>
                        <%--<td>
                            Posted Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostedDate" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>--%>
                        <td>
                            Unposted Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUnpostedValue" runat="server" Width="100%" Enabled="false" />
                            <asp:HiddenField ID="hdUnpostedValue" runat="server" />
                            <asp:HiddenField ID="hdUnpostedValueEdited" runat="server" />
                            <asp:HiddenField ID="hdActualUnpostedValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            End Market:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEndMarket" runat="server" Width="100%" onblur="return ValidateEndMarket();">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtEndMarket" runat="server" Width="100%" Enabled="false" />--%>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Geogrophy:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCountry" runat="server" Width="100%" onblur="return ValidateCountry();">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtGeogrophy" runat="server" Width="100%" Enabled="false" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Posting Month-Year:
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 40%">
                                        <asp:DropDownList ID="ddlPostingMonth" runat="server" Width="100%">
                                            <asp:ListItem Value="Jan" Text="Jan" />
                                            <asp:ListItem Value="Feb" Text="Feb" />
                                            <asp:ListItem Value="Mar" Text="Mar" />
                                            <asp:ListItem Value="Apr" Text="Apr" />
                                            <asp:ListItem Value="May" Text="May" />
                                            <asp:ListItem Value="Jun" Text="Jun" />
                                            <asp:ListItem Value="Jul" Text="Jul" />
                                            <asp:ListItem Value="Aug" Text="Aug" />
                                            <asp:ListItem Value="Sep" Text="Sep" />
                                            <asp:ListItem Value="Oct" Text="Oct" />
                                            <asp:ListItem Value="Nov" Text="Nov" />
                                            <asp:ListItem Value="Dec" Text="Dec" />
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 60%">
                                        <asp:DropDownList ID="ddlPostingYear" runat="server" Width="100%">
                                            <asp:ListItem Value="2015" Text="2015" />
                                            <asp:ListItem Value="2016" Text="2016" />
                                            <asp:ListItem Value="2017" Text="2017" />
                                            <asp:ListItem Value="2018" Text="2018" />
                                            <asp:ListItem Value="2019" Text="2019" />
                                            <asp:ListItem Value="2020" Text="2020" />
                                            <asp:ListItem Value="2021" Text="2021" />
                                            <asp:ListItem Value="2022" Text="2022" />
                                            <asp:ListItem Value="2023" Text="2023" />
                                            <asp:ListItem Value="2024" Text="2024" />
                                            <asp:ListItem Value="2025" Text="2025" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100%" onblur="return ValidateType();"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
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
                            Revenue/Not Revenue Type:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRevenueType" runat="server" Width="100%" onblur="return ValidateRevenueType();">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            UDF1:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUDF1" runat="server" Width="100%" Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            UDF2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUDF2" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            UDF3:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUDF3" runat="server" Width="100%" Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            UDF4:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUDF4" runat="server" Width="100%" Enabled="true" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            UDF5:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUDF5" runat="server" Width="100%" Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:Button ID="btnUpdate" CssClass="button" runat="server" Text="Update" OnClientClick="return ValidateAllNew();"
                                Width="100%" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </asp:Panel>
    <asp:Button ID="btnShowImgFile" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowImgFile"
        PopupControlID="pnlViewImgFilePopup" CancelControlID="imgBtnCancelImgFile" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlViewImgFilePopup" runat="server" BackColor="White" Height="600px"
        Width="950px" Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelImgFile" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>
        <fieldset style="width: 95%; margin-left: 22px; margin-top: 0px;">
            <legend style="text-align: center;">
                <asp:Label ID="lblLedendDelete" runat="server" />
            </legend>
            <div style='overflow: auto; width: 99%; height: 500px; border: 1px solid lightgray;
                margin-left: 5px;'>
                <table style="width: 95%; height: 100%; margin-left: 10px;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Invoice Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDateDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Job No.:
                        </td>
                        <td>
                            <asp:TextBox ID="txtJobNoDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Custoner Name:
                        </td>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtCustNameDelete" runat="server" Width="100%" Enabled="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCustCodeDelete" runat="server" Width="100%" Enabled="false" />
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
                            Product Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductCodeDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Invoice Amount:
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceAmountDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Posted Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostedValueDelete" runat="server" Width="100%" Enabled="false" />
                            <table width="100%">
                                <tr>
                                    <td style="width: 70%">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Posted Currency:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostingCurrencyDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Posted Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostedDateDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Unposted Value:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUnpostedValueDelete" runat="server" Width="100%" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Deleted Reason:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtDeletedReason" runat="server" Width="100%" TextMode="MultiLine"
                                Rows="3" onblur="return ValidateDeletedReason();" />
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
                            <asp:Button ID="btnDelete" CssClass="button" runat="server" Text="Delete" OnClientClick="return ValidateDeleteNew();"
                                Width="100%" OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
