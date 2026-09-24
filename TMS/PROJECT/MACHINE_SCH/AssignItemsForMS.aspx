<%@ Page Title="CIPLTMS- Assign Items To Machines" Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AssignItemsForMS.aspx.cs"
    Inherits="PROJECT_MACHINE_SCH_AssignItemsForMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="icon" href="../../Images/Icon04.png" />

    <%--<link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />    
    <link href="../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />

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

    <script type="text/Javascript">
        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }
    </script>


    <script type="text/javascript">

        function pageLoad() {
            document.getElementById('<%=txtDateToA.ClientID %>').value = document.getElementById('<%=hdDateToA.ClientID %>').value;
        }

        function clientChangedDate(sender, args) {
            document.getElementById('<%=hdDateToA.ClientID %>').value = document.getElementById('<%=txtDateToA.ClientID %>').value;
        }

    </script>

    <script type="text/javascript">

        function ValidateCategory() {
            var Category = document.getElementById('<%=ddlCategoryToA.ClientID %>').selectedIndex;
            if (Category == '' || Category == 0) {
                document.getElementById('<%=ddlCategoryToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=ddlCategoryToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateJOBNo() {
            var JOBNo = document.getElementById('<%=txtJOBNoToA.ClientID %>').value;
            if (JOBNo == '') {
                document.getElementById('<%=txtJOBNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtJOBNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateDrawingNo() {
            var DrawingNo = document.getElementById('<%=txtDrawingNoToA.ClientID %>').value;
            if (DrawingNo == '') {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateEquipment() {
            var Equipment = document.getElementById('<%=txtEquipmentToA.ClientID %>').value;
            if (Equipment == '') {
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateTagNo() {
            var TagNo = document.getElementById('<%=txtTagNoToA.ClientID %>').value;
            if (TagNo == '') {
                document.getElementById('<%=txtTagNoToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtTagNoToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }




        function ValidateItemName() {
            var ItemName = document.getElementById('<%=txtItemNameToA.ClientID %>').value;
            if (ItemName == '') {
                document.getElementById('<%=txtItemNameToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemNameToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateItemDetail() {
            var ItemDescription = document.getElementById('<%=txtItemDetailToA.ClientID %>').value;
            if (ItemDescription == '') {
                document.getElementById('<%=txtItemDetailToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtItemDetailToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidateQuantity() {
            var Quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;
            if (Quantity == '' || Quantity == 0) {
                document.getElementById('<%=txtQuantityToA.ClientID %>').style.borderColor = "#F7627F";
                return true;
            }
            else {
                document.getElementById('<%=txtQuantityToA.ClientID %>').style.borderColor = "";
                return false;
            }
        }

        function ValidatefileAttachment1() {
            var allowedFiles = [".jpg", ".jpeg", ".bmp", ".png", ".gif", ".pdf", ".JPG", ".JPEG", ".BMP", ".PNG", ".GIF", ".PDF", ".dxf", ".dwg", ".DXF", ".DWG"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileAttachment1 = document.getElementById('<%=uploadFileAttachment1.ClientID %>').value;
            var divfileAttachment1 = document.getElementById("divfileAttachment1");
            var lblfileAttachment1 = document.getElementById('<%=lblfileAttachment1.ClientID %>');


            if (fileAttachment1 == '') {
                document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                divfileAttachment1.style.display = "none";
                lblfileAttachment1.innerHTML = "";
                return false;
            }
            else {

                fileAttachment1 = fileAttachment1.split(" ").join("")
                fileAttachment1 = fileAttachment1.split("(").join("")
                fileAttachment1 = fileAttachment1.split(")").join("")

                if (!regex.test(fileAttachment1.toLowerCase())) {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "#F7627F";
                    divfileAttachment1.style.display = "block";
                    lblfileAttachment1.innerHTML = "Please enter only .pdf, .jpg, .jpeg, .bmp, .png, .gif, .dwg, .dxf file!";
                    return true;
                }
                else {
                    document.getElementById('<%=uploadFileAttachment1.ClientID %>').style.borderColor = "";
                    divfileAttachment1.style.display = "none";
                    lblfileAttachment1.innerHTML = "";
                    return false;
                }
            }
        }

    </script>


    <script type="text/javascript">

        function ValidateAllToList() {
            var check = true;
            var categoryVal = document.getElementById('<%=ddlCategoryToA.ClientID %>').selectedIndex;
           <%--var chkVal = document.getElementById('<%=chkNA.ClientID %>').checked;--%>

            if (ValidateJOBNo()) {
                check = false;
            }


            <%--if (chkVal == false) {

                if (ValidateDrawingNo()) {
                    check = false;
                }

                if (ValidateEquipment()) {
                    check = false;
                }

                if (ValidateTagNo()) {
                    check = false;
                }
            }
            else {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "";
            }--%>

            if (ValidateCategory()) {
                check = false;
            }

            if (ValidateDrawingNo()) {
                check = false;
            }


            if (categoryVal == 1) {
                if (ValidateEquipment()) {
                    check = false;
                }

                if (ValidateTagNo()) {
                    check = false;
                }
            }



            if (ValidateQuantity()) {
                check = false;
            }


            if (ValidateItemName()) {
                check = false;
            }

            if (ValidateItemDetail()) {
                check = false;
            }



            if (ValidatefileAttachment1()) {
                check = false;
            }

            return check;
        }

        function ValidateAllFromList() {
            var check = true;

            if (check) {
                if (confirm("Would you like to save assignment?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
            }

            return check;
        }


        function ValidateAll() {
            var check = true;
            <%--var chkVal = document.getElementById('<%=chkNA.ClientID %>').checked;--%>

            var categoryVal = document.getElementById('<%=ddlCategoryToA.ClientID %>').selectedIndex;


            if (ValidateJOBNo()) {
                check = false;
            }


            <%--if (chkVal == false) {

                if (ValidateDrawingNo()) {
                    check = false;
                }

                if (ValidateEquipment()) {
                    check = false;
                }

                if (ValidateTagNo()) {
                    check = false;
                }
            }
            else {
                document.getElementById('<%=txtDrawingNoToA.ClientID %>').style.borderColor = "";
                document.getElementById('<%=txtEquipmentToA.ClientID %>').style.borderColor = "";
            }--%>

            if (ValidateCategory()) {
                check = false;
            }

            if (ValidateDrawingNo()) {
                check = false;
            }


            if (categoryVal == 1) {

                if (ValidateEquipment()) {
                    check = false;
                }

                if (ValidateTagNo()) {
                    check = false;
                }

            }



            if (ValidateQuantity()) {
                check = false;
            }


            if (ValidateItemName()) {
                check = false;
            }

            if (ValidateItemDetail()) {
                check = false;
            }



            if (ValidatefileAttachment1()) {
                check = false;
            }


            if (check) {
                if (confirm("Would you like to save assignment?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return true;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return false;
                }
            }
            else {
                return false;
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

    <script type="text/javascript">
        function IncreaseQuantity() {
            var quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;

            document.getElementById('<%=txtQuantityToA.ClientID %>').value = parseInt(quantity) + 1;
        }

        function DecreaseQuantity() {
            var quantity = document.getElementById('<%=txtQuantityToA.ClientID %>').value;

            if (quantity > 0) {
                document.getElementById('<%=txtQuantityToA.ClientID %>').value = parseInt(quantity) - 1;
            }
            else {
                document.getElementById('<%=txtQuantityToA.ClientID %>').value = "0";
            }

        }



        <%--function EnableDisableDrawingValues() {

            var chkVal = document.getElementById('<%=chkNA.ClientID %>').checked;

            document.getElementById('<%=hdLOTTFSubitemID.ClientID %>').value = "0";
            document.getElementById('<%=txtDrawingNoToA.ClientID %>').value = "";
            document.getElementById('<%=txtEquipmentToA.ClientID %>').value = "";
            document.getElementById('<%=txtTagNoToA.ClientID %>').value = "";
            document.getElementById('<%=txtQuantityToA.ClientID %>').value = "0";

            if (chkVal) {
                document.getElementById('<%=btnGetDrawingNo.ClientID %>').style.visibility = 'hidden';
            }
            else {
                document.getElementById('<%=btnGetDrawingNo.ClientID %>').style.visibility = 'visible';
            }

        }--%>
    </script>

    <style type="text/css">
        .boxcss {
            width: 70%;
            border-radius: 10px;
            background-color: whitesmoke;
            /*opacity: 0.9;*/
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel1">
        <ContentTemplate>--%>


    <asp:HiddenField ID="hdIsNewRecord" Value="0" runat="server" />
    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Assign Items To Machine Shop:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>Unit:</label>
                    <asp:DropDownList ID="ddlCompanyToA" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyToA_SelectedIndexChanged" />

                    <label>Category:</label>
                    <asp:DropDownList ID="ddlCategoryToA" runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategoryToA_SelectedIndexChanged">
                    </asp:DropDownList>

                    <label>JOB Number:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 90%">
                                <asp:HiddenField ID="hdLOTTFID" runat="server" />
                                <asp:TextBox ID="txtJOBNoToA" runat="server"
                                    CssClass="form-control"
                                    Enabled="false" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetJOBNo" runat="server" Width="100%" Text="Get" CssClass="button"
                                    OnClick="btnGetJOBNo_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <label>Drawing Number:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtDrawingNoToA" runat="server"
                                    Enabled="false"
                                    CssClass="form-control" />
                            </td>

                            <td style="width: 10%">
                                <asp:Button ID="btnGetDrawingNo" runat="server" Width="100%"
                                    Text="Get" CssClass="button"
                                    OnClientClick="return ValidateAllJobNo();" OnClick="btnGetDrawingNo_Click"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>


                    <label>Additional Drawing:</label>
                    <asp:Panel ID="pnlViewAddDrawing" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 75%;">
                                    <asp:Label runat="server" Visible="false" ID="lblSRNoToA" Text="0" />
                                    <asp:TextBox ID="txtAddDrawingFilePathToA" runat="server"
                                        CssClass="form-control"
                                        Enabled="false" />
                                </td>
                                <td>&nbsp;</td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnViewAddDrawing" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/pdficon1.png" ToolTip="View"
                                        OnClick="imgBtnViewAddDrawing_Click" />
                                </td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnRemoveAddDrawing" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/NEWICONS/Deleted01.png" ToolTip="Remove"
                                        OnClick="imgBtnRemoveAddDrawing_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlUploadAddDrawing" runat="server" Visible="true">
                        <table width="100%">
                            <tr>
                                <td style="width: 90%;">

                                    <table widt="100%">
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="uploadFileAttachment1" runat="server" CssClass="form-control"
                                                    BorderStyle="Groove" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="divfileAttachment1" style="display: none;">
                                                    <asp:Label ID="lblfileAttachment1" runat="server" ForeColor="Red" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="imgBtnUndoAddDrawing" runat="server" Height="20px" Width="20px"
                                        ImageUrl="~/Images/Icon05.png" ToolTip="Undo" Visible="false"
                                        OnClick="imgBtnUndoAddDrawing_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>



                    <label>Equipment:</label>
                    <asp:HiddenField ID="hdLOTTFSubitemID" runat="server" />
                    <asp:Label runat="server" ID="lblLOTNo" Visible="false" />

                    <asp:Label runat="server" ID="lblSIDrawing1" Visible="false" />
                    <asp:Label runat="server" ID="lblSIDrawing2" Visible="false" />

                    <asp:TextBox ID="txtEquipmentToA" runat="server"
                        Enabled="false"
                        CssClass="form-control" />

                    <label>Tag No.:</label>
                    <asp:TextBox ID="txtTagNoToA" runat="server"
                        Enabled="false"
                        CssClass="form-control" />



                    <label>Item Name:</label>
                    <asp:TextBox ID="txtItemNameToA" runat="server"
                        CssClass="form-control" />

                    <label>Item Detail:</label>
                    <asp:TextBox ID="txtItemDetailToA" runat="server"
                        CssClass="form-control" />


                    <label>ED of Comp. by Planning/Production:</label>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDateToA" runat="server" onkeyDown="javascript:preventInput(event);"
                                    CssClass="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdDateToA" runat="server" />
                                <ajax:CalendarExtender ID="calendarDate" PopupButtonID="imgBtnDate" runat="server"
                                    TargetControlID="txtDateToA" Format="dd-MMM-yyyy" OnClientDateSelectionChanged="clientChangedDate">
                                </ajax:CalendarExtender>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                    ToolTip="Calendar" />
                            </td>
                        </tr>
                    </table>

                    <label>Allocated Quantity:</label>
                    <table width="100%">
                        <tr>
                            <td style="width: 80%;">
                                <asp:TextBox ID="txtQuantityToA" runat="server" Text="0"
                                    CssClass="form-control"
                                    onkeyDown="javascript:preventInput(event);" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/Icons/ADD05.png" onclick="IncreaseQuantity()" style="width: 20px; height: 20px;" alt="" />
                            </td>
                            <td style="width: 10%;">
                                <img src="../../Images/Icons/Remove01.png" onclick="DecreaseQuantity()" style="width: 20px; height: 20px;" alt="" />
                            </td>
                        </tr>
                    </table>

                    <div class="full-width">
                        <label>Remarks:</label>
                        <asp:TextBox ID="txtRemarkstoA" runat="server"
                            CssClass="form-control" />
                    </div>

                </div>

            </fieldset>
            <div class="full-width button-group">

                <asp:Button ID="btnAddToList" runat="server" Width="100%" Text="Add To List" CssClass="button"
                    OnClientClick="return ValidateAllToList();" OnClick="btnAddToList_Click" />

                <asp:Button ID="btnSaveDetails" runat="server" Width="100%" Text="Assign" CssClass="button"
                    OnClientClick="return ValidateAll();" OnClick="btnSaveDetails_Click" />


                <asp:Button ID="btnSaveFromList" runat="server" Width="100%" Text="Assign Below List" CssClass="button"
                    OnClick="btnSaveFromList_Click" OnClientClick="return ValidateAllFromList();" />

                <asp:Button ID="btnAssignedList" runat="server" Width="100%" Text="Assigned Items List" CssClass="button"
                    PostBackUrl="~/PROJECT/MACHINE_SCH/AssignedItemsListForMS.aspx" />

            </div>
        </div>

        <div class="employee-grid-container">
            <div align="center">
                <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                </asp:Panel>
            </div>

            <input type="hidden" id="div_position" name="div_position" />

            <asp:GridView
                CssClass="employee-grid"
                ID="gvSubitemsList" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvSubitemsList_RowCommand"
                OnRowDataBound="gvSubitemsList_RowDataBound">
                <%--<RowStyle BackColor="#E3EAEB" HorizontalAlign="Left"/>--%>
                <Columns>
                    <asp:TemplateField HeaderText="Sr No">
                        <ItemTemplate>

                            <asp:TextBox ID="txtSrNo" runat="server" Enabled="false" CssClass="textboxcenter" Text='<%# Eval("SR_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                            <asp:Label ID="lblLOTTFSubitemIDInList" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' />
                            <asp:Label ID="lblLOTSIDrawigName1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                            <asp:Label ID="lblLOTSIDrawigName2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                            <asp:Label ID="lblUnitID" runat="server" Visible="false" Text='<%# Eval("UNIT_ID") %>' />

                            <asp:ImageButton ID="imgBtnAddNewRecord" ImageUrl="~/Images/LOT/edit5.png" ToolTip="Edit Record"
                                runat="server" CommandArgument="EDIT" Width="35px" Height="35px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnRemoveRecord" ImageUrl="~/Images/Icons/REMOVE02.png" ToolTip="Remove Record"
                                runat="server" CommandArgument="REMOVE" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Add. Drawing" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:TextBox ID="txtAdditinoalDrawingFilePath" runat="server" CssClass="textfiles" Width="200px"
                                Visible="false" Enabled="false" Text='<%# Eval("ADD_DRAWING_FILE_NAME") %>'></asp:TextBox>

                            <asp:ImageButton ID="imgBtnViewAdditionalDrawing" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewADDDRAWING"
                                Width="35px" Height="35px" />
                            <%--<table width="100%">
                                    <tr>
                                        <td style="width: 80%;">
                                            <asp:TextBox ID="txtAdditinoalDrawingFilePath" runat="server" CssClass="textfiles" Width="200px"
                                                Enabled="false" Text='<%# Eval("ADD_DRAWING_File_NAME") %>'></asp:TextBox>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:ImageButton ID="imgBtnViewAdditionalDrawing" runat="server" ImageUrl="~/Images/pdficon1.png"
                                                CommandArgument="ViewADDDRAWING"
                                                Width="35px" Height="35px" />
                                        </td>
                                    </tr>
                                </table>--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing (.pdf)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnViewDrawing1" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewSIDRAWING1"
                                Width="35px" Height="35px" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing (.dwg/.dxf)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>

                            <asp:ImageButton ID="imgBtnViewDrawing2" runat="server" ImageUrl="~/Images/pdficon1.png"
                                CommandArgument="ViewSIDRAWING2"
                                Width="35px" Height="35px" />

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>

                            <asp:TextBox ID="txtUnit" Width="70px" runat="server" Enabled="false" CssClass="textboxleftyellow" Text='<%# Eval("UNIT_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCategory" Width="150px" runat="server" Enabled="false"
                                CssClass="textboxleftyellow" Text='<%# Eval("CATEGORY_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="JOB No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtJOBNo" Width="180px" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("JOB_NO") %>'
                                ToolTip='<%# Eval("JOB_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingNo" Width="200px" TextMode="MultiLine" runat="server" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("DRAWING_NO") %>'
                                ToolTip='<%# Eval("DRAWING_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Equipment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEquipment" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("EQUIPMENT") %>'
                                ToolTip='<%# Eval("EQUIPMENT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tag No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTagNo" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("TAG_NO") %>'
                                ToolTip='<%# Eval("TAG_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemName" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ITEM_NAME") %>'
                                ToolTip='<%# Eval("ITEM_NAME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Detail">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemDetail" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("ITEM_DETAIL") %>'
                                ToolTip='<%# Eval("ITEM_DETAIL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Expected Date of Completion by Planning/Production">
                        <ItemTemplate>
                            <asp:TextBox ID="txtExpectedDateofComp" runat="server" Width="100%"
                                Text='<%# Eval("EXPECTED_DATE_OF_COMP_BY_PLANNING") %>'
                                Enabled="false" CssClass="textboxleftsmall"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" Width="70px" runat="server" Enabled="false" CssClass="textboxrightsmall"
                                Text='<%# Eval("ALLOCATED_QUANTITY") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" Width="200px" runat="server" TextMode="MultiLine"
                                Columns="30" Enabled="false" CssClass="textboxleftyellow"
                                Text='<%# Eval("REMARKS") %>'
                                ToolTip='<%# Eval("REMARKS") %>' />
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


    <%--LOT JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJobDetailLOT" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJobDetailLOT" runat="server" TargetControlID="btnShowPopupJobDetailLOT"
        PopupControlID="pnlPopupJobDetailLOT" CancelControlID="imgBtnCancelJobDetailLOT" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJobDetailLOT" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJobDetailLOT" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecordsLOT" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoSearchLOT" runat="server" CssClass="form-control" />

                        <label>LOT No.:</label>
                        <asp:TextBox ID="txtLOTNoSearchLOT" runat="server" CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">
                    <asp:Button ID="btnSearchJOBNoLOT" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchJOBNoLOT_Click" />
                </div>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="lblJOBMsgLOT" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvJobDetailLOT" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvJobDetailLOT_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblLOTTFID" runat="server" Visible="false" Text='<%# Eval("LOT_TF_ID") %>' />
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblLOTno" runat="server" Visible="false" Text='<%# Eval("TF_NO") %>' />

                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TF_NO" HeaderText="LOT_NO" />
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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

    </asp:Panel>
    <%--LOT JOB DETAIL END--%>


    <%--FACT JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJobDetailFact" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJobDetailFact" runat="server" TargetControlID="btnShowPopupJobDetailFact"
        PopupControlID="pnlPopupJobDetailFact" CancelControlID="imgBtnCancelJobDetailFact" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJobDetailFact" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJobDetailFact" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecordsFact" runat="server" Text="Records[0]" />
                    </legend>

                    <div class="form-grid form-grid-3">

                        <label>Unit:</label>
                        <asp:TextBox ID="txtUnitSearchFact" runat="server"
                            CssClass="form-control"
                            Enabled="false" />

                        <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoSearchFact" runat="server"
                            CssClass="form-control" />

                    </div>
                </fieldset>
                <div class="full-width button-group">

                    <asp:Button ID="btnSearchJOBNoFact" CssClass="button" runat="server" Text="Search"
                        Width="100%" OnClick="btnSearchJOBNoFact_Click" />

                </div>
            </div>

            <div class="employee-grid-container">

                <asp:Label ID="lblJOBMsgFact" runat="server" />

                <asp:GridView
                    CssClass="employee-grid"
                    ID="gvJobDetailFact" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                    OnRowCommand="gvJobDetailFact_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />

                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="cancelbutton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
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

    </asp:Panel>
    <%--FACT JOB DETAIL END--%>


    <%--DRAWING DETAIL START--%>
    <asp:Button ID="btnShowPopupDrawingDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeDrawingDetail" runat="server" TargetControlID="btnShowPopupDrawingDetail"
        PopupControlID="pnlPopupDrawingDetail" CancelControlID="imgBtnCancelDrawingDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupDrawingDetail" runat="server" 
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelDrawingDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>
                    <asp:Label ID="lblDrawingRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">

                    <label>JOB Number:</label>
                        <asp:TextBox ID="txtJOBNoInDr" runat="server" CssClass="form-control" Enabled="false" />
                    
                    <label>LOT No.:</label>
                        <asp:TextBox ID="txtLOTNoInDr" runat="server" CssClass="form-control" Enabled="false" />
                    
                    <label>Drawing No.:</label>
                        <asp:TextBox ID="txtDrawingNoInDr" runat="server" CssClass="form-control"/>
                    
                    <label>Equipment:</label>
                        <asp:TextBox ID="txtEquipmentInDr" runat="server" CssClass="form-control" />

                </div>
            </fieldset>
            <div class="full-width button-group">
                
                <asp:Button ID="btnSearchDrawingNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchDrawingNo_Click" />

            </div>
        </div>

        <div class="employee-grid-container">
            <asp:Label ID="lblDrawingMsg" runat="server" />

            <asp:GridView 
                CssClass="employee-grid"
                ID="gvDrawingDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvDrawingDetail_RowCommand">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Get">
                                <ItemTemplate>
                                    <asp:Label ID="lblLOTTFSubitemIDInList" runat="server" Visible="false" Text='<%# Eval("LOT_TF_SUBITEM_ID") %>' />
                                    <asp:Label ID="lblLOTSIDrawigName1" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT1_NAME") %>' />
                                    <asp:Label ID="lblLOTSIDrawigName2" runat="server" Visible="false" Text='<%# Eval("SI_ATTACHMENT2_NAME") %>' />
                                    <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                    <asp:Label ID="lblEquipment" runat="server" Visible="false" Text='<%# Eval("EQUIPMENT") %>' />
                                    <asp:Label ID="lblTagNo" runat="server" Visible="false" Text='<%# Eval("TAG_NO") %>' />

                                    <asp:Button ID="btnGetDrawingDetail" CommandArgument="GET" ToolTip="Get Drawing Details" Width="100%"
                                        runat="server" Text="Get" CssClass="cancelbutton" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="JOB_NO" HeaderText="JOB No." />
                            <asp:BoundField DataField="TF_NO" HeaderText="LOT No." />
                            <asp:BoundField DataField="PRODUCTION_ORDER_NO" HeaderText="Production Order No." />
                            <asp:BoundField DataField="DRAWING_NO" HeaderText="Drawing No." />
                            <asp:BoundField DataField="EQUIPMENT" HeaderText="Equipment" />
                            <asp:BoundField DataField="TAG_NO" HeaderText="Tag No." />

                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" Width="100%" runat="server" Enabled="false" CssClass="textboxrightsmall" Text='<%# Eval("QUANTITY") %>' />
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

    </asp:Panel>
    <%--DRAWING DETAIL END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
