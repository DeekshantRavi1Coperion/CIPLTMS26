<%@ Page Language="C#" MasterPageFile="~/HOME.master" AutoEventWireup="true" CodeFile="OneStreamPosting.aspx.cs"
    Inherits="FINANCE_ONESTREAM_OneStreamPosting" Title="CIPLTMS - One Stream- Posting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<link href="../../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/HomeNew.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../../Styles/form.css" rel="stylesheet" />
    <link href="../../../Styles/filter.css" rel="stylesheet" />
    <link href="../../../Styles/grid.css" rel="stylesheet" />
    <link href="../../../Styles/popup.css" rel="stylesheet" />

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


        function ValidatefileUploadCostCenter() {
            var allowedFiles = [".csv", ".CSV"];
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            var fileUploadCostCenter = document.getElementById('<%=fileUploadCostCenter.ClientID %>').value;
            var divfileUploadCostCenter = document.getElementById("divfileUploadCostCenter");
            var lblfileUploadCostCenter = document.getElementById('<%=lblfileUploadCostCenter.ClientID %>');

            if (fileUploadCostCenter == '') {
                document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                divfileUploadCostCenter.style.display = "block";
                lblfileUploadCostCenter.innerHTML = "";
                return true;
            }
            else {
                if (!regex.test(fileUploadCostCenter.toLowerCase())) {
                    document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "block";
                    lblfileUploadCostCenter.innerHTML = "Please choose only .csv or .CSV file!";
                    return true;
                }
                else {
                    document.getElementById('<%=fileUploadCostCenter.ClientID %>').style.borderColor = "#F7627F";
                    divfileUploadCostCenter.style.display = "none";
                    lblfileUploadCostCenter.innerHTML = "";
                    return false;
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        function ValidateAll() {
            var check = true;

            if (ValidatefileUploadCostCenter()) { return false; }

            return check;
        }

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
        var GridId = "<%=gvOneStream.ClientID %>";
        var ScrollHeight = 380;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();

            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }

            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];

            for (var i = 0; i < cells.length; i++) {
                var width = headerCellWidths[i];
                cells[i].style.width = parseInt(width) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }



        function preventInput(event) {
            if (event.which != 9) {
                event.preventDefault();
            }
        }

        function Confirm() {
            var existedRecordsCount = document.getElementById('<%=hdExistedRecords.ClientID %>').value;
            var GVRowCount = document.getElementById('<%=hdGVRowCount.ClientID %>').value;

            if (parseInt(GVRowCount) > 0) {
                if (parseInt(existedRecordsCount) > 0) {
                    var confirm_value = document.createElement("input");
                    confirm_value.type = "hidden";
                    confirm_value.name = "Confirm Value";
                    if (confirm("There are " + existedRecordsCount + " records exist in system. Do you want to replace?")) {
                        document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 1;
                    }
                    else {
                        document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
                    }
                }
                else {
                    document.getElementById('<%=hdReplacementFlag.ClientID %>').value = 0;
                }
            }
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <ajax:ToolkitScriptManager ID="ScriptManager2" runat="server">
    </ajax:ToolkitScriptManager>
    <%--<asp:UpdatePanel runat="server" ID="uppanel">
        <ContentTemplate>--%>

    <asp:HiddenField ID="hdExistedRecords" runat="server" />
    <asp:HiddenField ID="hdGVRowCount" runat="server" />
    <asp:HiddenField ID="hdReplacementFlag" runat="server" />

    <div class="page-layout">

        <div class="filter-grid-container">
            <fieldset class="filter-card">

                <div class="form-filter-grid">

                    <div class="full-width button-group">

                        <asp:Button ID="btnGetFormat" CssClass="button" Width="100%" runat="server"
                            Text="Download Format" OnClick="btnGetFormat_Click" />


                        <label>Browse:</label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUploadCostCenter" runat="server"
                                        CssClass="form-control"
                                        BorderStyle="Groove" onblur="return ValidatefileUploadCostCenter();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divfileUploadCostCenter" style="display: none;">
                                        <asp:Label ID="lblfileUploadCostCenter" runat="server" ForeColor="Red" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </fieldset>
            <div class="full-width button-group">
                <asp:Button ID="btnGetOneStreamFile" CssClass="button" Width="100%" runat="server"
                    Text="Get Detail" OnClientClick="return ValidateAll();" OnClick="btnGetOneStreamFile_Click" />

                <asp:Button ID="btnSave" CssClass="button" Width="100%" runat="server"
                    Text="Save" OnClick="btnSave_Click" OnClientClick="Confirm();" />
            </div>
        </div>

        <fieldset class="employee-grid-fieldset">
            <legend>One Stream Posting:
                    <asp:Label ID="lblRecords" runat="server" Text="Records[0]" />
            </legend>

            <div class="employee-grid-container">
                <div align="center">
                    <asp:Panel ID="pnlMsg" Visible="false" runat="server">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Large" />
                    </asp:Panel>
                </div>

                <asp:UpdatePanel runat="server" ID="uppanel">
                    <ContentTemplate>
                        <asp:GridView
                            CssClass="employee-grid"
                            ID="gvOneStream" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="7" Width="100%"
                            HorizontalAlign="Center" OnRowDataBound="gvOneStream_RowDataBound">
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr_No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordID" runat="server" Visible="false" Text='<%# Eval("RECORD_ID" ) %>' />
                                        <asp:Label ID="lblSRNo" runat="server" Visible="true" Text='<%# Eval("SR_NO" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="F_Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFYear" runat="server" Visible="true" Text='<%# Eval("F_YEAR" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F_Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFPeriod" runat="server" Visible="true" Text='<%# Eval("F_PERIOD" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F_Quarter">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFQuarter" runat="server" Visible="true" Text='<%# Eval("F_QUARTER" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile_Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProfileName" runat="server" Visible="true" Text='<%# Eval("PROFILE_NAME" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tm">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTm" runat="server" Visible="true" Text='<%# Eval("TM" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tmt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTmt" runat="server" Visible="true" Text='<%# Eval("TMT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Et">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEt" runat="server" Visible="true" Text='<%# Eval("ET" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ett">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEtt" runat="server" Visible="true" Text='<%# Eval("ETT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAc" runat="server" Visible="true" Text='<%# Eval("AC" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Act">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAct" runat="server" Visible="true" Text='<%# Eval("ACT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fw">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFw" runat="server" Visible="true" Text='<%# Eval("FW" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fwt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFwt" runat="server" Visible="true" Text='<%# Eval("FWT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ic">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIc" runat="server" Visible="true" Text='<%# Eval("IC" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ict">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIct" runat="server" Visible="true" Text='<%# Eval("ICT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU1" runat="server" Visible="true" Text='<%# Eval("U1" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U1T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU1T" runat="server" Visible="true" Text='<%# Eval("U1T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU2" runat="server" Visible="true" Text='<%# Eval("U2" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U2T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU2T" runat="server" Visible="true" Text='<%# Eval("U2T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU3" runat="server" Visible="true" Text='<%# Eval("U3" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U3T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU3T" runat="server" Visible="true" Text='<%# Eval("U3T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U4">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU4" runat="server" Visible="true" Text='<%# Eval("U4" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U4T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU4T" runat="server" Visible="true" Text='<%# Eval("U4T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U5">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU5" runat="server" Visible="true" Text='<%# Eval("U5" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U5T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU5T" runat="server" Visible="true" Text='<%# Eval("U5T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU6" runat="server" Visible="true" Text='<%# Eval("U6" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U6T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU6T" runat="server" Visible="true" Text='<%# Eval("U6T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U7">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU7" runat="server" Visible="true" Text='<%# Eval("U7" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U7T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU7T" runat="server" Visible="true" Text='<%# Eval("U7T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U8">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU8" runat="server" Visible="true" Text='<%# Eval("U8" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="U8T">
                                    <ItemTemplate>
                                        <asp:Label ID="lblU8T" runat="server" Visible="true" Text='<%# Eval("U8T" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Raw_Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawAmount" runat="server" Visible="true" Text='<%# Eval("RAW_AMOUNT" ) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Converted_Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConvertedAmount" runat="server" Visible="true" Text='<%# Eval("CONVERTED_AMOUNT" ) %>' />
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
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </fieldset>

    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
