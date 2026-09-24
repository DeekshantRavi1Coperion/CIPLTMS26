<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="ImportTimesheet.aspx.cs"
    Inherits="TIMESHEET_ImportTimesheet" Title="CIPLTMS- Import Timesheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../Styles/form.css" rel="stylesheet" />
    <link href="../../Styles/filter.css" rel="stylesheet" />
    <link href="../../Styles/grid.css" rel="stylesheet" />
    <link href="../../Styles/popup.css" rel="stylesheet" />


    <link rel="icon" href="../Images/Icons/Icon04.png" />
    <link href="../../../Styles/ClearCrossInTextbox.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../Scripts/NumericValidation.js"></script>
    <script type="text/javascript" src="../../../Scripts/NegNumericValidation.js"></script>

    <style type="text/css">
        .textbox {
            width: 100%;
            padding: 5px 10px;
            margin: 1px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: #D8D8D8;
        }

        .textbox1 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #ebdef0;*/
        }

        .textbox2 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            /*background-color: #fadbd8;*/
        }

        .textbox3 {
            width: 100%;
            padding: 5px 10px;
            margin: 0px 0;
            box-sizing: border-box;
            border: none;
            display: inline-block;
            text-align: right;
            border-radius: 4px;
            background-color: #D8D8D8;
        }
    </style>

    <script type="text/javascript">


        function ValidatefileUploadTimesheet() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadTimesheet = document.getElementById('<%=fileUploadTimesheet.ClientID %>').value;
            var divfileUploadTimesheet = document.getElementById("divfileUploadTimesheet");
            var lblfileUploadTimesheet = document.getElementById('<%=lblfileUploadTimesheet.ClientID %>');

            if (fileUploadTimesheet == '') {
                document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadTimesheet.style.display = "block";
                lblfileUploadTimesheet.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadTimesheet.toLowerCase())) {
                    document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadTimesheet.style.display = "block";
                    lblfileUploadTimesheet.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadTimesheet.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadTimesheet.style.display = "none";
                    lblfileUploadTimesheet.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;

            if (ValidatefileUploadTimesheet()) { return false; }

            return check;
        }

    </script>


    <script type="text/javascript">

        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function Confirm() {
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;
            var GVRowCountNew = 0;

            if (GVRowCount == '')
                GVRowCountNew = 0;
            else
                GVRowCountNew = parseInt(GVRowCount);

            if (GVRowCountNew > 0) {

                if (confirm("Would you like to import timesheet?")) {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "1";
                    return false;
                }
                else {
                    document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                    return true;
                }
            }
            else {
                document.getElementById('<%=hdConfirmValue.ClientID %>').value = "0";
                return true;
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <asp:HiddenField ID="hdConfirmValue" runat="server" />
    <asp:HiddenField ID="hdGVRowCount" runat="server" />

    <div class="page-layout">

        <div class="form-grid-container">
            <fieldset class="filter-card">
                <legend>Import Timesheet:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" /></legend>



                <div class="form-grid form-grid-3">

                    <div class="full-width button-group">
                        <asp:Button
                            ID="btnGetFormat"
                            CssClass="button"
                            Width="100%"
                            runat="server"
                            Text="Download Format"
                            OnClick="btnGetFormat_Click" />


                        <label>Browse</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadTimesheet" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadTimesheet" style="display: none;">
                                        <asp:Label ID="lblfileUploadTimesheet" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="full-width button-group">
                        <asp:Button
                            ID="btnGetTimesheetDetails"
                            CssClass="button"
                            Width="100%"
                            runat="server"
                            Text="Get Detail" OnClientClick="return ValidateAll();"
                            OnClick="btnGetTimesheetDetails_Click" />

                        <asp:Button
                            ID="btnSave"
                            CssClass="button"
                            Width="100%"
                            runat="server"
                            Text="Save"
                            OnClick="btnSave_Click"
                            OnClientClick="Confirm();" />
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
                ID="gvTimesheet" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                HorizontalAlign="Center" OnRowDataBound="gvTimesheet_RowDataBound">
                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="User_Name">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlUserName" runat="server" Width="120px" />
                            <asp:Label ID="lblSRNo" runat="server" Visible="false" Text='<%# Eval("SR_NO" ) %>' />
                            <asp:Label ID="lblEmpRecordID" runat="server" Visible="false" Text='<%# Eval("EMP_RECORD_ID" ) %>' />
                            <asp:Label ID="lblProjectCategory" runat="server" Visible="false" Text='<%# Eval("PROJECT_CATETORY" ) %>' />
                            <asp:Label ID="lblProjectNumber" runat="server" Visible="false" Text='<%# Eval("PROJECT_NUMBER" ) %>' />
                            <asp:Label ID="lblJobNumber" runat="server" Visible="false" Text='<%# Eval("JOB_NUMBER" ) %>' />
                            <asp:Label ID="lblDrwaingCategory" runat="server" Visible="false" Text='<%# Eval("DRAWING_CATEGORY" ) %>' />
                            <asp:Label ID="lblSerialNumber" runat="server" Visible="false" Text='<%# Eval("SERIAL_NUMBER" ) %>' />
                            <asp:Label ID="lblSize" runat="server" Visible="false" Text='<%# Eval("SIZE" ) %>' />
                            <asp:Label ID="lblRev" runat="server" Visible="false" Text='<%# Eval("REV" ) %>' />
                            <asp:Label ID="lblSheets" runat="server" Visible="false" Text='<%# Eval("SHEETS" ) %>' />
                            <asp:Label ID="lblDrawingID" runat="server" Visible="false" Text='<%# Eval("DRAWING_ID" ) %>' />
                            <asp:Label ID="lblDrawingTypeID" runat="server" Visible="false" Text='<%# Eval("DRAWING_TYPE_ID" ) %>' />
                            <asp:Label ID="lblTimeSpent" runat="server" Visible="false" Text='<%# Eval("TIME_SPENT" ) %>' />
                            <asp:Label ID="lblTitle" runat="server" Visible="false" Text='<%# Eval("TITLE" ) %>' />
                            <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# Eval("REMARKS" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Entry_Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEntryDate" runat="server" Width="100px" onkeyDown="javascript:preventInput(event);" Text='<%# Eval("ENTRY_DATE") %>' />
                            <ajax:CalendarExtender ID="calendarEntryDate" PopupButtonID="imgbtnEntryDate"
                                runat="server" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy">
                            </ajax:CalendarExtender>
                            <asp:ImageButton ID="imgbtnEntryDate" runat="server" ImageUrl="~/Images/Calendar2.png"
                                ToolTip="Entry Date" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Project_Category" DataField="PROJECT_CATETORY" />
                    <asp:BoundField HeaderText="Project_Number" DataField="PROJECT_NUMBER" />
                    <asp:BoundField HeaderText="Job_Number" DataField="JOB_NUMBER" />
                    <asp:BoundField HeaderText="Drawing_Category" DataField="DRAWING_CATEGORY" />
                    <asp:BoundField HeaderText="Serial_Number" DataField="SERIAL_NUMBER" />
                    <asp:BoundField HeaderText="Size" DataField="SIZE" />
                    <asp:BoundField HeaderText="Rev" DataField="REV" />
                    <asp:BoundField HeaderText="Sheets" DataField="SHEETS" />
                    <asp:BoundField HeaderText="Drawing_ID" DataField="DRAWING_ID" />

                    <asp:TemplateField HeaderText="Drawing_Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDrawingType" runat="server" Width="120px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start_Time">
                        <ItemTemplate>
                            <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("START_TIME" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="End_Time">
                        <ItemTemplate>
                            <asp:Label ID="lblEndTime" runat="server" Text='<%# Eval("END_TIME" ) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Time_Spent" HeaderText="TIME_SPENT" />
                    <asp:BoundField DataField="Title" HeaderText="TITLE" />
                    <asp:BoundField DataField="Remarks" HeaderText="REMARKS" />
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
