<%@ Page Title="CIPLTMS- Assign Items To Machines" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="UpdateCompletedQuantity.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_UpdateCompletedQuantity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="icon" href="../../Images/Icon04.png" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }

        .style1 {
            width: 10px;
        }
    </style>

    <style type="text/css">
        .dropdown {
            position: relative;
            display: inline-block;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            padding: 12px 16px;
            z-index: 1;
        }

        .dropdown:hover .dropdown-content {
            display: block;
        }
    </style>

    <style type="text/css">
        .textboxleft {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxleftgreen {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightgreen;
        }

        .textboxleftyellow {
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: lightyellow;
        }

        .textboxcenter {
            width: 50px;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            /*background-color: #D8D8D8;*/
            background-color: transparent;
        }

        .textboxright {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: transparent;
        }

        .textboxrightsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: lightyellow;
        }

        .textboxleftsmall {
            width: 90px;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightyellow;
        }

        .textfiles {
            padding: 5px 2px 5px 2px;
            margin: 0px 5px 0px 5px;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            background-color: lightgreen;
        }
    </style>

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





    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>




    <script type="text/javascript">

        function ValidateActivityUCQ() {
            var Activity = document.getElementById('<%=ddlActivityUCQ.ClientID %>').selectedIndex;
            if (Activity == '' || Activity == 0) {
                document.getElementById('<%=ddlActivityUCQ.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlActivityUCQ.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateMachineUCQ() {
            var Machine = document.getElementById('<%=ddlMachineUCQ.ClientID %>').selectedIndex;
            if (Machine == '' || Machine == 0) {
                document.getElementById('<%=ddlMachineUCQ.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlMachineUCQ.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateCompletedQuantityUCQ() {
            var quantity = document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value;
            if (quantity == '' || parseInt(quantity) == 0) {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateAllMacninesUCQ() {
            var check = true;

            if (ValidateActivityUCQ()) {
                check = false;
            }

            if (ValidateMachineUCQ()) {
                check = false;
            }

            if (ValidateCompletedQuantityUCQ()) {
                check = false;
            }
            

            return check;
        }

    </script>


    <script type="text/javascript">

        function ValidateAllJobNo() {
            var check = true;

            if (ValidateJOBNo()) {
                check = false;
            }


            return check;
        }


    </script>

    <script type="text/javascript">

        function stopEnterKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if (evt.keyCode == 13) {
                return false;
            }
        }
        document.onkeypress = stopEnterKey;

    </script>

    <script type="text/javascript">

        function IncreaseQuantity() {
            var allocatedQuantity = document.getElementById('<%=txtAllocatedQuantityUCQ.ClientID %>').value;
            var quantity = document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value;
            var totalquantity = document.getElementById('<%=txtTotalCompletedQuantityUCQ.ClientID %>').value;
            var balancequantity = parseInt(allocatedQuantity) - parseInt(totalquantity)


            if (parseInt(quantity) < parseInt(balancequantity)) {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value = parseInt(quantity) + 1;
            }
            else {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value = balancequantity;
            }

            document.getElementById('<%=txtPendingQuantityUCQ.ClientID %>').value = parseInt(balancequantity) - parseInt(document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value)
        }

        function DecreaseQuantity() {
            var allocatedQuantity = document.getElementById('<%=txtAllocatedQuantityUCQ.ClientID %>').value;
            var quantity = document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value;
            var totalquantity = document.getElementById('<%=txtTotalCompletedQuantityUCQ.ClientID %>').value;
            var balancequantity = parseInt(allocatedQuantity) - parseInt(totalquantity)

            if (quantity > 0) {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value = parseInt(quantity) - 1;
            }
            else {
                document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value = "0";
            }

            document.getElementById('<%=txtPendingQuantityUCQ.ClientID %>').value = parseInt(balancequantity) - parseInt(document.getElementById('<%=txtCompletedQuantityUCQ.ClientID %>').value)

        }

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

    </script>



    <script type="text/javascript">
        window.onload = function () {
            var div = document.getElementById("dvScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        };
    </script>

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />
    <div align="center">
        <fieldset style="width: 70%">
            <legend style="text-align: center;">Update Completed Quantity</legend>
        </fieldset>
    </div>
    <div align="center">
        <fieldset style="width: 95%;">
            <table width="70%">
                <tr>
                    <td style="width: 10%;">Unit:</td>
                    <td style="width: 20%;">
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="100%" Height="26px" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="width: 10%;">JOB Number:</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtJOBNo" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="width: 10%;">LOT Number:</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtLOTNo" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td style="width: 10%;">Drawing Number:</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtDrawingNo" runat="server" Width="100%" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="width: 10%;">Equipment:</td>
                    <td style="width: 20%;">
                        <asp:TextBox ID="txtEquipment" runat="server" Width="100%" />
                    </td>
                    <td colspan="2">&nbsp;</td>
                    <td style="width: 20%;">
                        <asp:Button ID="btnSearch" runat="server" Width="100%" Text="Search" CssClass="button"
                            OnClick="btnSearch_Click" />
                    </td>       
                </tr>               
            </table>
            <br />
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>
        </fieldset>

        <br />

        <fieldset style="width: 95%;">
            <legend style="text-align: center;">
                <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>
            <%--<div id="gridContainer">--%>
            <div id="dvScroll" style='overflow: scroll; width: 100%; height: 450px; border: 1px solid lightgray;'>
                <%--<asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>--%>
                <%--<asp:GridView ID="gvSubitemsList" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvSubitemsList_RowCommand"
                    OnRowDataBound="gvSubitemsList_RowDataBound">--%>


                <asp:GridView ID="gvSubitemsList" runat="server" AutoGenerateColumns="false" CellPadding="5"
                    CssClass="myGrid" AlternatingRowStyle-CssClass="alt"
                    OnRowCommand="gvSubitemsList_RowCommand"
                    OnRowDataBound="gvSubitemsList_RowDataBound">

                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" CssClass="textboxcenter" Text='<%# Eval("SR_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                            ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>

                                <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_PID") %>' />
                                <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_FID") %>' />
                                <asp:Label ID="lblActivityID" runat="server" Visible="false" />
                                <asp:Label ID="lblMachineID" runat="server" Visible="false" />
                                <asp:Label ID="lblTypeOfWorkID" runat="server" Visible="false" />
                                <asp:Label ID="lblProductionManagerID" runat="server" Visible="false" Text='<%# Eval("MANAGER_ID") %>' />
                                <asp:Label ID="lblAdditinoalDrawingFilePath" runat="server" Visible="false" Text='<%# Eval("ADD_DRAWING_FILE_NAME") %>' />

                                <asp:Button ID="btnUpdateQuantity" CommandArgument="UPDATE_QUANTITY" ToolTip="Update Completed Quantity" runat="server"
                                    Text="Update" CssClass="cancelbutton" Width="100%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />

                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUnit" Width="70px" runat="server" Enabled="false" CssClass="textboxleftyellow" 
                                    Text='<%# Eval("UNIT_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%--<asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:TextBox ID="txtType" Width="70px" runat="server" Enabled="false" CssClass="textboxleftyellow" 
                                    Text='<%# Eval("TYPE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtActivity" Width="100px" runat="server" Enabled="false" CssClass="textboxleftyellow" 
                                Text='<%# Eval("ACTIVITY_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Machine">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMachine" Width="250px" runat="server" Enabled="false" CssClass="textboxleftyellow" 
                                Text='<%# Eval("MACHINE_NAME") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        
                        <asp:TemplateField HeaderText="LOT No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLOTNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("TF_NO") %>' ToolTip='<%# Eval("TF_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="JOB No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtJOBNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("JOB_NO") %>' ToolTip='<%# Eval("JOB_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Drawing No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDrawingNo" Width="200px" TextMode="MultiLine" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("DRAWING_NO") %>' ToolTip='<%# Eval("DRAWING_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Equipment">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEquipment" Width="200px" runat="server" TextMode="MultiLine"
                                    Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("EQUIPMENT") %>' ToolTip='<%# Eval("EQUIPMENT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item Desc">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemDesc" Width="200px" runat="server" TextMode="MultiLine"
                                    Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("ITEM_DESCRIPTION") %>' ToolTip='<%# Eval("ITEM_DESCRIPTION") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                       

                        <asp:TemplateField HeaderText="Expected Date of Completion by Planning/Production">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExpectedDateofComp" runat="server" Width="150px"
                                    Text='<%# Eval("EXPECTED_DATE_OF_COMP_BY_PLANNING") %>' Enabled="false" CssClass="textboxleftyellow"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Production Manager">
                            <ItemTemplate>
                                <asp:TextBox ID="txtProductionManager" Width="150px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                    Text='<%# Eval("PRODUCTION_MANAGER")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Allocated Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAllocatedQuantity" Width="70px" runat="server" Enabled="false" CssClass="textboxrightsmall"
                                    Text='<%# Eval("ALLOCATED_QUANTITY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Completed Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCompletedQuantity" Width="70px" runat="server" Enabled="false" CssClass="textboxrightsmall"
                                    Text='<%# Eval("COMPLETED_QUANTITY") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pending Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPendingQuantity" Width="70px" runat="server" Enabled="false" CssClass="textboxrightsmall"
                                    Text='<%# Eval("PENDING_QUANTITY") %>' />
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


                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <input type="hidden" id="div_position" name="div_position" />
            <%-- </div>--%>
        </fieldset>
    </div>


    <asp:Button ID="btnShowPopupUpdateCompletedQuantityDetails" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeUpdateCompletedQuantityDetails" runat="server" TargetControlID="btnShowPopupUpdateCompletedQuantityDetails"
        PopupControlID="pnlPopupUpdateCompletedQuantityDetails" CancelControlID="imgBtnCancelUpdateCompletedQuantityDetails" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupUpdateCompletedQuantityDetails" runat="server" BackColor="White" Height="500px" Width="1000px"
        Style="display: block">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelUpdateCompletedQuantityDetails" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>

        <div align="center">

            <fieldset style="width: 95%; margin-top: 10px;">
                <legend style="text-align: center;">Update Completed Quantity</legend>
                <div align="center">
                    <asp:Panel ID="pnlCompletedQuantityDetailsMsg" Visible="false" runat="server">
                        <asp:Label ID="lblCompletedQuantityDetailsMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>
                <div style='overflow: auto; width: 99%; height: 380px; border: 1px solid lightgray; margin-left: 5px;'>
                    <table width="95%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">Unit:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtUnitUCQ" runat="server" Width="100%" Enabled="false" />

                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">JOB Number:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtJOBNoUCQ" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">Drawing Number:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtDrawingNoUCQ" runat="server" Width="100%" Enabled="false" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Equipment:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtEquipmentUCQ" runat="server" Width="100%" Enabled="false" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">Activity:</td>
                            <td style="width: 30%;">
                                <%--<asp:TextBox ID="txtActivityUCQ" runat="server" Width="100%" Enabled="false" />--%>
                                <asp:DropDownList ID="ddlActivityUCQ" Width="100%" Height="26px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlActivityUCQ_SelectedIndexChanged" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Machine:</td>
                            <td style="width: 30%;">
                                <%--<asp:TextBox ID="txtMachineUCQ" runat="server" Width="100%" Enabled="false" />--%>
                                <asp:DropDownList ID="ddlMachineUCQ" Width="100%" Height="26px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td style="width: 15%;">Allocated Quantity:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtAllocatedQuantityUCQ" runat="server" Width="100%" Text="0" onkeyDown="javascript:preventInput(event);" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Total Completed Quantity:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtTotalCompletedQuantityUCQ" runat="server" Width="100%" Text="0" onkeyDown="javascript:preventInput(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15%;">Completed Quantity:</td>
                            <td style="width: 30%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txtCompletedQuantityUCQ" runat="server" Width="100%" Text="0" onkeyDown="javascript:preventInput(event);" />
                                        </td>
                                        <td style="width: 10%;">
                                            <img src="../../Images/Icons/ADD05.png" onclick="IncreaseQuantity()" style="width: 35px; height: 35px;" alt="" />
                                        </td>
                                        <td style="width: 10%;">
                                            <img src="../../Images/Icons/Remove01.png" onclick="DecreaseQuantity()" style="width: 30px; height: 30px;" alt="" />
                                        </td>
                                    </tr>
                                </table>


                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 15%;">Pending Quantity:</td>
                            <td style="width: 30%;">
                                <asp:TextBox ID="txtPendingQuantityUCQ" runat="server" Width="100%" Text="0" onkeyDown="javascript:preventInput(event);" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>Remarks:</td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRemarksUCQ" runat="server" Width="100%" TextMode="MultiLine" />
                            </td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Button ID="btnUpdateCompletedQuantity" runat="server" Width="100%" Text="Save Completed Quantity" CssClass="button"
                                    OnClientClick="return ValidateAllMacninesUCQ();" OnClick="btnUpdateCompletedQuantity_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </asp:Panel>

    <script type="text/Javascript">
        $(document).ready(function () {

            // here clone our gridview first
            var tab = $("#<%=gvSubitemsList.ClientID%>").clone(true);
            // clone again for freeze
            var tabFreeze = $("#<%=gvSubitemsList.ClientID%>").clone(true);

            // set width (for scroll)
            var totalWidth = $("#<%=gvSubitemsList.ClientID%>").outerWidth();
            var firstColWidth = $("#<%=gvSubitemsList.ClientID%> th:first-child").outerWidth();
            tabFreeze.width(firstColWidth);
            tab.width(totalWidth - firstColWidth);

            // here make 2 table 1 for freeze column 2 for all remain column

            tabFreeze.find("th:gt(0)").remove();
            tabFreeze.find("td:not(:first-child)").remove();

            tab.find("th:first-child").remove();
            tab.find("td:first-child").remove();

            // create a container for these 2 table and make 2nd table scrollable

            var container = $('<table border="0" cellpadding="0" cellspacing="0"><tr><td valign="top"><div id="FCol"></div></td><td valign="top"><div id="Col" style="width:320px; overflow:auto"></div></td></tr></table)');
            $("#FCol", container).html($(tabFreeze));
            $("#Col", container).html($(tab));

            // clear all html
            $("#gridContainer").html('');
            $("#gridContainer").append(container);
        });
    </script>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
