<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="AddDesign.aspx.cs"
    Inherits="PROJECT_DMS_AddDesign" Title="CIPLTMS- Add Design" %>

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

    <script src="../../Scripts/NumericValidation.js" type="text/javascript"></script>

    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 1000;
        }
    </style>

    <style type="text/css">
        .textboxcenter {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: center;
            border-radius: 4px;
            background-color: transparent;
            /*background-color: whitesmoke;*/
            /*background-color: #D8D8D8;*/
        }

        .textboxright {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            height: 26px;
            /*background-color: transparent;*/
            border-color: lightblue;
            /*background-color: #D8D8D8;*/
        }

        .textboxleft {
            /*width: 100%;*/
            padding: 5px 5px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: left;
            border-radius: 4px;
            /*background-color: transparent;*/
            border-color: lightblue;
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




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdDesignEnggFlag" runat="server" />
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>


    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Add Design:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
                </legend>

                <div class="form-grid form-grid-3">
                    <div class="full-width button-group">

                        <asp:Button ID="btnAddRow" CssClass="button" Width="100%" runat="server" Text="Add Row"
                            OnClick="btnAddRow_Click" />
                        <asp:Button ID="btnRemoveRow" CssClass="button" Width="100%" runat="server" Text="Remove Row"
                            OnClick="btnRemoveRow_Click" />
                        <asp:Button ID="btnSaveDesign" CssClass="button" Width="100%" runat="server" Text="Send To Design"
                            OnClick="btnSaveDesign_Click" />
                        <asp:Button ID="btnDesignList" CssClass="button" Width="100%" runat="server" Text="Design List"
                            OnClick="btnDesignList_Click" />

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
                ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="Vertical" Width="100%" HorizontalAlign="Center"
                OnRowCommand="gvDesignDetails_RowCommand" OnRowDataBound="gvDesignDetails_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sr.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedByID" runat="server" Text='<%# Eval("CREATED_BY_ID") %>' Visible="false" />
                            <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("STATUS_ID") %>' Visible="false" />
                            <asp:Label ID="lblDrawingIDInList" runat="server" Visible="false" />
                            <asp:TextBox ID="txtSrNo" runat="server" Text='<%# Eval("SR_NO") %>' Width="50px"
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxcenter" Height="26px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgStatus" CommandArgument="STATUS" runat="server" Height="40PX" Width="40PX" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Get JOB No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnGetJOBNo" CommandArgument="GET_JOB_NO"
                                runat="server" Text="Get JOB No."
                                CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOB No.">
                        <ItemTemplate>
                            <asp:Label ID="lblJOBUnitID" runat="server" Text='<%# Eval("JOB_UNIT_ID") %>' Visible="false" />
                            <asp:TextBox ID="txtJOBNo" runat="server" Text='<%# Eval("JOB_NO") %>' Width="150px" Height="26px" Enabled="true"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--5 Get Drawing No.--%>
                    <asp:TemplateField HeaderText="Get Drawing No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnGetDrawingNo" CommandArgument="GET_DRAWING_NO"
                                runat="server" Text="Get Drawing No."
                                CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Equipment/Design Description">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("DESCRIPTION") %>'
                                Width="300PX" TextMode="MultiLine" Rows="2" CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:TextBox ID="txtUOM" runat="server" Text='<%# Eval("UOM") %>' Width="100px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("QUANTITY") %>' Width="60px"
                                onkeypress="return inNumberKey(this, event);" CssClass="textboxright" Height="26px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reqd. Date By Project Team">
                        <ItemTemplate>
                            <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Visible="false" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>' />
                            <asp:TextBox ID="txtReqdDateByProjectTeam" runat="server" Width="80%" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarReqdDateByProjectTeam" PopupButtonID="imgbtnReqdDateByProjectTeam"
                                runat="server" TargetControlID="txtReqdDateByProjectTeam" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnReqdDateByProjectTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Reqd. Date By Project Team Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="250PX" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--11--%>
                    <asp:TemplateField HeaderText="Planned Start Date By Design Team">
                        <ItemTemplate>
                            <asp:Label ID="lblPlannedStartDateByDesignTeam" runat="server" Visible="false" Text='<%# Eval("PLANNED_START_DATE_BY_DESIGN_TEAM") %>' />
                            <asp:TextBox ID="txtPlannedStartDateByDesignTeam" runat="server" Width="80%" Text='<%# Eval("PLANNED_START_DATE_BY_DESIGN_TEAM") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarPlannedStartDateByDesignTeam" PopupButtonID="imgbtnPlannedStartDateByDesignTeam"
                                runat="server" TargetControlID="txtPlannedStartDateByDesignTeam" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnPlannedStartDateByDesignTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Planned Start Date By Design Team Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--12--%>
                    <asp:TemplateField HeaderText="Planned Completion Date By Design Team">
                        <ItemTemplate>
                            <asp:Label ID="lblPlannedCompletionDateByDesignTeam" runat="server" Visible="false" Text='<%# Eval("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM") %>' />
                            <asp:TextBox ID="txtPlannedCompletionDateByDesignTeam" runat="server" Width="80%" Text='<%# Eval("PLANNED_COMPLETION_DATE_BY_DESIGN_TEAM") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarPlannedCompletionDateByDesignTeam" PopupButtonID="imgbtnPlannedCompletionDateByDesignTeam"
                                runat="server" TargetControlID="txtPlannedCompletionDateByDesignTeam" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnPlannedCompletionDateByDesignTeam" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Planned Completion Date By Design Team Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Drawing No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDrawingNo" runat="server" Text='<%# Eval("DRAWING_NO") %>' Width="250px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Client Drawing No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClientDrawingNo" runat="server" Text='<%# Eval("CLIENT_DRAWING_NO") %>' Width="250px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contractor Drawing No.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtContractorDrawingNo" runat="server" Text='<%# Eval("CONTRACTOR_DRAWING_NO") %>' Width="250px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--14--%>
                    <asp:TemplateField HeaderText="Document Link">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDocumentLink" runat="server" Text='<%# Eval("DOCUMENT_LINK") %>' Width="250px" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Drawing Revisioin Number">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDrawingRevNo" runat="server" Width="100%" Height="26px"
                                CssClass="textboxleft">
                                <asp:ListItem Text="00" Value="0" />
                                <asp:ListItem Text="01" Value="1" />
                                <asp:ListItem Text="02" Value="2" />
                                <asp:ListItem Text="03" Value="3" />
                                <asp:ListItem Text="04" Value="4" />
                                <asp:ListItem Text="05" Value="5" />
                                <asp:ListItem Text="06" Value="6" />
                                <asp:ListItem Text="07" Value="7" />
                                <asp:ListItem Text="08" Value="8" />
                                <asp:ListItem Text="09" Value="9" />
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="11" Value="11" />
                                <asp:ListItem Text="12" Value="12" />
                                <asp:ListItem Text="13" Value="13" />
                                <asp:ListItem Text="14" Value="14" />
                                <asp:ListItem Text="15" Value="15" />
                                <asp:ListItem Text="16" Value="16" />
                                <asp:ListItem Text="17" Value="17" />
                                <asp:ListItem Text="18" Value="18" />
                                <asp:ListItem Text="19" Value="19" />
                                <asp:ListItem Text="20" Value="20" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--16--%>
                    <asp:TemplateField HeaderText="Working Status">
                        <ItemTemplate>
                            <asp:TextBox ID="txtWorkingStatus" runat="server" Text='<%# Eval("WORKING_STATUS") %>' Width="300PX" TextMode="MultiLine"
                                Rows="2" CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--17--%>
                    <asp:TemplateField HeaderText="Expected Completion Date">
                        <ItemTemplate>
                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Visible="false" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>' />
                            <asp:TextBox ID="txtExpectedCompletionDate" runat="server" Width="80%" Text='<%# Eval("EXPECTED_COMPLETION_DATE") %>'
                                onkeyDown="javascript:preventInput(event);" CssClass="textboxleft" Height="26px" />
                            <ajax:CalendarExtender ID="calendarExpectedCompletionDate" PopupButtonID="imgbtnExpectedCompletionDate"
                                runat="server" TargetControlID="txtExpectedCompletionDate" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnExpectedCompletionDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Expected Completion Date Calendar" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--18--%>
                    <asp:TemplateField HeaderText="Responsible Design Engineer">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlResponsibleDesignEngineer" runat="server" Width="100%" Height="26px"
                                CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Attachment1">
                            <ItemTemplate>
                                <asp:FileUpload ID="fileUploadAttachment1" runat="server" Width="200px" BorderStyle="Groove" Height="26px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Attachment2">
                            <ItemTemplate>
                                <asp:FileUpload ID="fileUploadAttachment2" runat="server" Width="200px" BorderStyle="Groove" Height="26px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("REMARKS") %>' Width="300PX" TextMode="MultiLine"
                                Rows="2" CssClass="textboxleft" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Save Design" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnSaveDesign" CommandArgument="SAVE_DESIGN"
                                runat="server" Text="Send To Design"
                                CssClass="cancelbutton" Width="95%" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="2px" />
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




    <%--JOB DETAIL START--%>
    <asp:Button ID="btnShowPopupJOBDetail" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mpeJOBDetail" runat="server" TargetControlID="btnShowPopupJOBDetail"
        PopupControlID="pnlPopupJOBDetail" CancelControlID="imgBtnCancelJOBDetail" BackgroundCssClass="modalBackground">
    </ajax:ModalPopupExtender>
    <asp:Panel ID="pnlPopupJOBDetail" runat="server"
        CssClass="popup-edit">
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgBtnCancelJOBDetail" ImageUrl="~/Images/cancelled_img.png" runat="server" />
                </td>
            </tr>
        </table>


        <div class="popup-layout">

            <div class="form-grid-container">
                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblJOBRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="form-grid form-grid-3">

                        <label>Company</label>
                        <asp:DropDownList ID="ddlCompanySearch" runat="server" CssClass="form-control" />

                        <label>JOB No.</label>
                        <asp:TextBox ID="txtJOBNoSearch" runat="server" CssClass="form-control" />

                        <asp:Button ID="btnSearchJOBNo" CssClass="button" runat="server" Text="Search"
                            Width="100%" OnClick="btnSearchJOBNo_Click" />
                    </div>
                </fieldset>
            </div>

            <div class="popup-grid-container">

                <asp:Label ID="lblJOBMsg" runat="server" />

                <asp:GridView
                    CssClass="popup-grid"
                    ID="gvJOBDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center" OnRowCommand="gvJOBDetail_RowCommand">
                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Get JOB No." HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <asp:Label ID="lblJOBNo" runat="server" Visible="false" Text='<%# Eval("JOB_NO") %>' />
                                <asp:Label ID="lblJOBUnitIDInJOBList" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT_ID") %>' />
                                <asp:Label ID="lblJOBUnit" runat="server" Visible="false" Text='<%# Eval("JOB_UNIT") %>' />
                                <asp:Button ID="btnGetJOBNo" CommandArgument="GET" ToolTip="Get JOB No." Width="100%"
                                    runat="server" Text="Get JOB No." CssClass="button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOB_NO" HeaderText="JOB_NO" />
                        <asp:BoundField DataField="JOB_UNIT" HeaderText="UNIT" />
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
    <%--JOB DETAIL END--%>



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


        <div class="popup-layout">

            <div class="form-grid-container">

                <fieldset class="filter-card">
                    <legend>
                        <asp:Label ID="lblDrawingRecords" runat="server" Text="Records[0]" /></legend>

                    <div class="popup-grid-container">

                        <asp:GridView
                            CssClass="popup-grid"
                            ID="gvDrawingDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Both" Width="100%" HorizontalAlign="Center"
                            OnRowCommand="gvDrawingDetail_RowCommand">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Get" HeaderStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawingNo" runat="server" Visible="false" Text='<%# Eval("DRAWING_NO") %>' />
                                        <asp:Label ID="lblDrawingID" runat="server" Visible="false" Text='<%# Eval("DRAWING_ID") %>' />
                                        <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# Eval("DESCRIPTION") %>' />
                                        <asp:Label ID="lblQuantity" runat="server" Visible="false" Text='<%# Eval("QUANTITY") %>' />
                                        <asp:Label ID="lblUOM" runat="server" Visible="false" Text='<%# Eval("UOM") %>' />
                                        <asp:Label ID="lblReqdDateByProjectTeam" runat="server" Visible="false" Text='<%# Eval("REQD_DATE_BY_PROJECT_TEAM") %>' />
                                        <asp:Button ID="btnGetDrawingDetail" CommandArgument="GET" ToolTip="Get" Width="100%"
                                            runat="server" Text="Get" CssClass="button" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DRAWING_NO" HeaderText="Drawing No." />
                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                                <asp:BoundField DataField="QUANTITY" HeaderText="Quantity" />
                                <asp:BoundField DataField="REQD_DATE_BY_PROJECT_TEAM" HeaderText="Reqd. Date by Project Team" />
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
        </div>

    </asp:Panel>
    <%--DRAWING DETAIL END--%>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
